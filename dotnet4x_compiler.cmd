@echo off
set /p csharpFilePath="Path of file to compile: "
%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe %csharpFilePath%
exit /b 1
