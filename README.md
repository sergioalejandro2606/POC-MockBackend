# TransferenciasMockApi - .NET 10

## Credenciales de demostración
Username: `user`
Password: `pwd`

## Ejecutar
Abre el `.csproj` en Visual Studio, restaura NuGet y ejecuta con F5.
En Development, OpenAPI estará disponible en `/openapi/v1.json` si la plantilla tiene el paquete `Microsoft.AspNetCore.OpenApi`.

## Endpoints
- POST `/api/auth/login` (público)
- POST `/api/transfers` (Bearer)
- GET `/api/movements/last?iduser=102&transactionalId=qwerty12345` (Bearer)
- PUT `/api/users` (Bearer)
- DELETE `/api/users` (Bearer y body JSON según el ejercicio)

Login de ejemplo:
`{"username":"user","password":"pwd"}`

Copia `access_token` y úsalo como `Authorization: Bearer <token>`.

La autenticación JWT es real (HMAC SHA-256, firma, issuer, audience y expiración). Los datos son ficticios y se almacenan en memoria, por lo que se pierden al reiniciar. Cambia la clave y credenciales de ejemplo antes de cualquier exposición fuera de localhost. El proyecto no realiza transferencias bancarias reales.
