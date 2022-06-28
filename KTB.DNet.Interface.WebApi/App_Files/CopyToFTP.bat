@echo off

set INITIALDIR= ""
set FTPADDRESS= 172.17.30.221
set FTPUSERNAME=ftpuser
set FTPPASSWORD=Password.1
set LOCALDIR= "C:\Builds\1\DMS\Dev\src\KTB.DNet.Interface.WebApi\"
set REMOTEDIR= "\Builds\"


if "%FTPADDRESS%" == "" goto FTP_UPLOAD_USAGE
if "%FTPUSERNAME%" == "" goto FTP_UPLOAD_USAGE
if "%FTPPASSWORD%" == "" goto FTP_UPLOAD_USAGE
if "%LOCALDIR%" == "" goto FTP_UPLOAD_USAGE
if "%REMOTEDIR%" == "" goto FTP_UPLOAD_USAGE

:TEMP_NAME
set TMPFILE=%TMP%\%RANDOM%_ftpupload.tmp
if exist "%TMPFILE%" goto TEMP_NAME 

SET INITIALDIR=%CD%

echo user %FTPUSERNAME% %FTPPASSWORD% > %TMPFILE%
echo bin >> %TMPFILE%
echo lcd %LOCALDIR% >> %TMPFILE%

cd %LOCALDIR%

setlocal EnableDelayedExpansion
echo mkdir !REMOTEDIR! >> !TMPFILE!
echo cd %REMOTEDIR% >> !TMPFILE!
echo mput * >> !TMPFILE!
for /d /r %%d in (*) do (
    set CURRENT_DIRECTORY=%%d
    set RELATIVE_DIRECTORY=!CURRENT_DIRECTORY:%LOCALDIR%=!
    echo mkdir "!REMOTEDIR!/!RELATIVE_DIRECTORY:~1!" >> !TMPFILE!
    echo cd "!REMOTEDIR!/!RELATIVE_DIRECTORY:~1!" >> !TMPFILE!
    echo mput "!RELATIVE_DIRECTORY:~1!\*" >> !TMPFILE!
)

echo quit >> !TMPFILE!

endlocal EnableDelayedExpansion

ftp -n -i "-s:%TMPFILE%" %FTPADDRESS%

del %TMPFILE%

cd %INITIALDIR%

goto FTP_UPLOAD_EXIT

:FTP_UPLOAD_USAGE

:FTP_UPLOAD_EXIT
@echo on