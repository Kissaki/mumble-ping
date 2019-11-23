@echo off

SET CONFIGURATION=Release
SET TARGETFRAMEWORK=netcoreapp3.0
SET VERSION=v0.1.0

CALL :package %VERSION% win10-x64
IF ERRORLEVEL 1 EXIT /B 1
CALL :package %VERSION% win-x64
IF ERRORLEVEL 1 EXIT /B 1
CALL :package %VERSION% linux-x64
IF ERRORLEVEL 1 EXIT /B 1
CALL :package %VERSION% osx-x64
IF ERRORLEVEL 1 EXIT /B 1

echo Finished publishing
EXIT /B 0

:package
SET FILENAME=mumble-ping_%~1_%~2.7z
DEL %FILENAME%
"C:\Program Files\7-zip\7z.exe" a %FILENAME% mumble-ping\bin\%CONFIGURATION%\%TARGETFRAMEWORK%\%~2\publish\
IF ERRORLEVEL 1 EXIT /B 1
EXIT /B 0
