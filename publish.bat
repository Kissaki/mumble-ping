@echo off

dotnet publish --configuration Release --self-contained false --runtime win10-x64
IF ERRORLEVEL 1 EXIT /B 1
dotnet publish --configuration Release --self-contained false --runtime win-x64
IF ERRORLEVEL 1 EXIT /B 1
dotnet publish --configuration Release --self-contained false --runtime linux-x64
IF ERRORLEVEL 1 EXIT /B 1
dotnet publish --configuration Release --self-contained false --runtime osx-x64
IF ERRORLEVEL 1 EXIT /B 1

echo Finished publishing
