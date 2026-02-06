# Steg 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiera projekt filer
COPY ["CryptoApi/CryptoApi.csproj", "CryptoApi/"]
RUN dotnet restore "CryptoApi/CryptoApi.csproj"

# Kopiera källkod
COPY . .

# Bygg
WORKDIR "/src/CryptoApi"
RUN dotnet build "CryptoApi.csproj" -c Release -o /app/build

# Steg 2: Publish stage
FROM build AS publish
RUN dotnet publish "CryptoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Steg 3: Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Skapa non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Kopiera app
COPY --from=publish /app/publish .

# Exponera port
EXPOSE 80

# Environment
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:80/api/crypto/health || exit 1

# Starta
ENTRYPOINT ["dotnet", "CryptoApi.dll"]
