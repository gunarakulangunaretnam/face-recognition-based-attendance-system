Set oShell = CreateObject ("Wscript.Shell") 
Dim strArgs
strArgs = "cmd /c Dataset_Creator_Run.bat"
oShell.Run strArgs, 0, false