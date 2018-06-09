@rem remove the temp file should it remain in place from previous attempt
set /p vbasicFilePath="Path of VB file to compile: "
@del %vbasicFilePath%_temp.vb

@rem  the compiler requires .vb extension, so we'll have to copy our source to a temporary file
copy %vbasicFilePath% %vbasicFilePath%_temp.vb /y

@rem prepare string without the .txt extension
set str=%vbasicFilePath%
set str=%vbasicFilePath:.txt=%

@rem the actual build. check/update the path to your compiler according it's location on your system
@rem if you imported more namespaces in you source filem don't forget to add corresponding libraries
@rem
rem C:\Windows\Microsoft.NET\Framework\v2.0.50727\vbc.exe /platform:AnyCPU /r:Microsoft.VisualBasic.dll, system.management.dll,system.data.dll,mscorlib.dll,system.windows.forms.dll /target:exe %vbasicFilePath%_temp.vb /out:%str%.exe
%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\vbc.exe %vbasicFilePath%

@rem compiler will print out syntax errors it may have found in your code
@rem a pause is added to keep compiler messages on screen, useful for debugging

pause

@rem clean up the temporary file
del %vbasicFilePath%_temp.vb