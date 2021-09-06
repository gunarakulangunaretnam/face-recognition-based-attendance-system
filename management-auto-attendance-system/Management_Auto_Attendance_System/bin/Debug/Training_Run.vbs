Set oShell = CreateObject ("Wscript.Shell") 
Dim strArgs
strArgs = "cmd /c Training_Run.bat"
oShell.Run strArgs, 0, false