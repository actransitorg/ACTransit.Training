param (
    [string]$drive = "E:", #$(throw "-FilePath is required."),    
    [string]$serviceName="SynProcessServer",
	[Parameter(Mandatory=$true)]
	[string]$profile
)

function Setup-MSSQLLocalDb {
	 $result = & $sqlLocalDb info MSSQLLocalDB
	 $notFound = $result[0].Contains('is not created.')
	 if ($notFound) {
		"* Creating MSSQLLocalDb"
		& $sqlLocalDb create
	 }
}

function Restart-SqlLocalDb {
	& $sqlLocalDb stop
	& $sqlLocalDb start
}

function Invoke-SQL {
    param(
        [string] $dataSource = "(localdb)\MSSQLLocalDB",
        [string] $database = "master",
        [string] $sqlCommand = $(throw "Please specify a query.")
      )

    $connectionString = "Data Source=$dataSource;Integrated Security=SSPI;Initial Catalog=$database"
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $command = new-object System.Data.SqlClient.SqlCommand($sqlCommand, $connection)
    $connection.Open()
	$command.ExecuteNonQuery() 
	If (!$?) { throw [System.IO.Exception] "Error exeuting $sqlCommand" }
    $connection.Close()
}

function AttachDatabases() {
	ForEach ($db in $config["Databases"]) {
		$mdf = "$appDataPath\$db.mdf"
		$ldf = "$appDataPath\$db`_log.ldf"
		$dbpath = if ($config["PathInDbName"] -eq $true) { $mdf } else { $db }

		"Attaching $db..."
		$cmd = "
		IF EXISTS (SELECT * FROM master.sys.databases WHERE [name] = `'$appDataPath\$db`') 
		BEGIN 
			exec master.sys.sp_detach_db [$appDataPath\$db]
		END
		IF EXISTS (SELECT * FROM master.sys.databases WHERE [name] = `'$db`') 
		BEGIN 
			exec master.sys.sp_detach_db [$db]
		END
		IF EXISTS (SELECT * FROM master.sys.databases WHERE [name] = `'$mdf`') 
		BEGIN 
			exec master.sys.sp_detach_db [$mdf]
		END
		IF NOT EXISTS (SELECT * FROM master.sys.databases WHERE [name] = `'$dbpath`') 
		BEGIN
			CREATE DATABASE [$dbpath] ON (FILENAME = `'$mdf`'),(FILENAME = `'$ldf`') FOR ATTACH 
		END"
		Invoke-SQL -sqlCommand $cmd
	}
}

function PublishRequired(){                  
    if (!(Test-Path $appDataPath)){
        throw "Database path not found! $appDataPath"            
    }                
    if (!(Test-Path $publishedPath)){
        return $true
    }        
        
    $content = Get-Content $publishedPath                
    if (!($content -eq $appDataPath)){
        return $true
    }

    $files=Get-ChildItem "$appDataPath\*.mdf" -ErrorAction Stop #| Measure-Object
    if (!($files.Length -eq $dbCount)) {
        return $true
    }
    return $false
}

function Get-CurrentPath {
	return "$(Split-Path $PSCommandPath -Parent)";
}

function Get-SqlPath {
	$SqlPath = "$($env:ProgramW6432)\Microsoft SQL Server"
	if (!(test-path $SqlPath)) { $SqlPath = "$($env:ProgramFiles)\Microsoft SQL Server" }
	if (!(test-path $SqlPath)) { throw [Exception] "Could not find SQL Server installation" }
	return $SqlPath;
}

function Get-SqlLocalDb {
	$ErrorActionPreference= "SilentlyContinue"
	$sqlLocalDb = (gci -Path $SqlPath -Filter SqlLocalDb.exe -Recurse | Sort-Object LastAccessTime -Descending | Select-Object -First 1).FullName
	$ErrorActionPreference = "Stop";
	if ($sqlLocalDb -eq $null) { throw [Exception] "Could not find SqlLocalDb.exe" }
	return $sqlLocalDb;
}

function Get-Config {
    $configFile = "$currentPath\profiles\$profile.ps1"
	if (!(test-path $configFile)) { throw [Exception] "Cannot find $configFile" }
	return gc $configFile | Out-String | iex
}

Try
{
    $currentPath = Get-CurrentPath
    cd $currentPath
	"* Current Path: $currentPath" 
	$SqlPath = Get-SqlPath
	"* Sql Path: $SqlPath"
	$sqlLocalDb = Get-SqlLocalDb
	"* SqlLocalDb Path: $sqlLocalDb"
    $config = Get-Config
}
Catch
{
	write-host "Caught an exception, rolling back." -ForegroundColor Red
	write-host "Exception Type: $($_.Exception.GetType().FullName)" -ForegroundColor Red
	write-host "Exception Message: $($_.Exception.Message)" -ForegroundColor Red
    return -2
}
$scriptPath = "$($config["ScriptPath"])"
$dbCount = [int]$($config["DbCount"])
$databaseFilesPath = "$($config["DatabaseFilesPath"])"
$appDataPath = "$($config["AppDataPath"])"
$publishedPath = [System.IO.Path]::GetFullPath("$appDataPath\published.txt")
$defaultDbFile = "$($config["DefaultDbFile"])"
$testMode = "$($config["TestMode"])"
  	
if (PublishRequired) {
    Try
    {
		$archiveDbPath = "$appDataPath\Archive_" + (get-date -Format "yyyyMMdd_HHmmss")  +"\"
		if (!(Test-Path $archiveDbPath)){
			New-Item -ItemType directory $archiveDbPath
		}

		Setup-MSSQLLocalDb
		Restart-SqlLocalDb
		Move-Item "$appDataPath\*.*" $archiveDbPath
		Copy-Item "$databaseFilesPath\*.mdf" $appDataPath
		Copy-Item "$databaseFilesPath\*.ldf" $appDataPath
		Set-ItemProperty "$appDataPath\*.*" -name IsReadOnly -value $false
		$appDataPath | out-file $publishedPath
		AttachDatabases
		pushd

		#$files = Get-ChildItem "$scriptPath\*.sql" -ErrorAction Stop #| Measure-Object
        #$files | ForEach-Object -ErrorAction SilentlyContinue { 
        #    "-----------------------------------------------------------------------"
		#	"$($config["SqlExecutePath"]) --testmode=$testMode --scriptdir=$scriptPath --datadir=$appDataPath\ --currentdir=$appDataPath $_"
		#	& $($config["SqlExecutePath"]) --testmode=$testMode --scriptdir=$scriptPath --datadir=$appDataPath\ --currentdir=$appDataPath $_
		#	if ($LASTEXITCODE -ne 0){
		#		throw [System.IO.Exception] "sqlexecute failed with error code $LASTEXITCODE."
		#	}
        #}
    }
    Catch
    {
		write-host "Caught an exception, rolling back." -ForegroundColor Red
		write-host "Exception Type: $($_.Exception.GetType().FullName)" -ForegroundColor Red
		write-host "Exception Message: $($_.Exception.Message)" -ForegroundColor Red

		del $publishedPath
		del "$appDataPath\*.*" | Where { ! $_.PSIsContainer }
		Copy-Item "$archiveDbPath\*.*" $appDataPath
		del $archiveDbPath -Recurse
    }
	popd
    return               
}
else {
	"Publish is not required"
}
    

