FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/AuraPlatform/AuraPlatform.csproj src/AuraPlatform/
RUN dotnet restore src/AuraPlatform/AuraPlatform.csproj

COPY src/ src/
RUN dotnet publish src/AuraPlatform/AuraPlatform.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

EXPOSE 8006

COPY --from=build /app/publish .

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD curl -f http://localhost:8006/healthz || exit 1

ENTRYPOINT ["dotnet", "AuraPlatform.dll"]
