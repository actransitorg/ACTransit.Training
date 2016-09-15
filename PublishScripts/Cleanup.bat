cd ..
del /s /a H *.vspscc
del /s /a H *.vssscc
del /s /a H *.suo
del /s /a H *.user
for /D /R %%D in (*) do rd /s/q %%D\.vs
for /D /R %%D in (*) do rd /s/q %%D\obj
for /D /R %%D in (*) do rd /s/q %%D\Debug
for /D /R %%D in (*) do rd /s/q %%D\Nuget
for /D /R %%D in (*) do rd /s/q %%D\Release
for /D %%D in (ACTransit.TRaining\Packages\*) do rd /s/q %%D
del /q ACTransit.Training\Web\bin\*
pause