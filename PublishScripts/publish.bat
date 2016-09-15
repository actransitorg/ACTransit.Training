cd %~dp0
echo * Current Directory: %~dp0
powershell.exe -ExecutionPolicy Bypass .\publish.ps1 %*
echo Errorlevel: %ERRORLEVEL%
if %ERRORLEVEL%==1 (powershell.exe -ExecutionPolicy Bypass %~dp0publish.ps1 %*)