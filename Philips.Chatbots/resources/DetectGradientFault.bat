@ECHO OFF
SETLOCAL ENABLEDELAYEDEXPANSION
ECHO %CD%
cd /D "%~dp0"
ECHO Checking if Gradient Dump files are present..
if exist GRADDUMP*.log (
    ECHO Gradient Fault has occured at: 
	for /r %%a in (GRADDUMP*.log) do (
	set filename=%%a
	for /f "tokens=1,2,3 delims=_" %%a in ("!filename!") do ECHO %%b [yyyymmddhhmmss] 
	)
) else (
    ECHO Gradient Fault has not occurred.
)


PAUSE