sc stop TestService
timeout /t 5 /nobreak > NUL
sc delete TestService