@ECHO OFF

ECHO.
ECHO ------------------------------------------
ECHO Setting the build environment
ECHO ------------------------------------------
ECHO.
CALL "%VS100COMNTOOLS%\vsvars32.bat" > NUL 

CALL msbuild.exe "MSBuild.build" /target:Compile

PAUSE