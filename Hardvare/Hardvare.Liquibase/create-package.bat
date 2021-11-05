@ECHO OFF

IF EXIST "output\" (
    rmdir "output\" /s /q
)

if not exist output\ mkdir output

.\.tools\nuget.exe pack Hardvare.Liquibase.nuspec -OutputDirectory output -Version %1
