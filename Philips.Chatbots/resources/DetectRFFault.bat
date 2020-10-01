@ECHO OFF
SETLOCAL ENABLEDELAYEDEXPANSION
cd resources
ECHO Checking if RF Amplifier Dump files are present..
if exist RFAMPDUMP*.log (
    ECHO RF Amplifier Fault has occured at: 
	for /r %%a in (GRADDUMP*.log) do (
	set filename=%%a
	for /f "tokens=1,2,3 delims=_" %%a in ("!filename!") do ECHO %%b [yyyymmddhhmmss] 
	)
) else (
    ECHO RF Amplifier Fault has not occurred.
)


PAUSE