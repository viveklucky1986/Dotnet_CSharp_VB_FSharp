@echo off
set /p vbasicFilePath="Path of VB file to compile: "
%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\vbc.exe %vbasicFilePath%
exit /b 1