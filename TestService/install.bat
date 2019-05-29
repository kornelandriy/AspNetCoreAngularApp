sc create TestService binPath= "%~dp0TestService.exe"
sc failure TestService actions= restart/60000/restart/60000/restart/60000 reset= 86400
sc start TestService
sc config TestService start=auto