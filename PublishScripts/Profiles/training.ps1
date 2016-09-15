function Get-Profile {
    $scriptPath = $pwd
	$repoPath = [System.IO.Path]::GetFullPath("$scriptPath\..")
    $databaseFilesPath = "$scriptPath\Database"
    $solutionPath = [System.IO.Path]::GetFullPath("$scriptPath\..\ACTransit.Training")
    $startupPath = "$solutionPath\Web"
    $appDataPath = "$startupPath\App_Data"
    $defaultDbFile = "$appDataPath\Training.mdf"
	$sqlExecutePath = (dir -Path "$repoPath\*SQLExecute.exe" -Recurse | Sort-Object LastAccessTime -Descending | Select-Object -First 1).FullName

    @{ `
        ScriptPath=$scriptPath; `
		SqlExecutePath=$sqlExecutePath; `
        DatabaseFilesPath=$databaseFilesPath; `
        SolutionPath=$solutionPath; `
        StartupPath=$startupPath; `
        AppDataPath=$appDataPath; `
        DefaultDbFile=$defaultDbFile; `
		Databases=@("EmployeeDW","MaintenanceDW","SchedulingDW","Training"); `
        DbCount=4; `
		TestMode="false"; `
		PathInDbName=$false; `
    }
}

Get-Profile