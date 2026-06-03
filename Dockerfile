FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia todo el código y restaura dependencias
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Construye la imagen final para ejecutar la API
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "tabloidetek_Backend.dll"]