# Backend

Backend que actúa como proxy entre el frontend y la API externa FakeRestAPI. Implementa Clean Architecture y expone endpoints para libros (Books) y autores (Authors).

## Instalación y ejecución

1. Clonar el repositorio:

```bash
git clone https://github.com/johansantana/LibraryBackend
cd LibraryBackend
```

2. Restaurar dependencias y compilar:

```bash
dotnet restore
dotnet build
```

3. Ejecutar la API:

```bash
Copy code
dotnet run --project .\Api\Api.csproj
```
