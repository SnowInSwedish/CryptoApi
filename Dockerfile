FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ["CryptoApi/CryptoApi.csproj", "CryptoApi/"]
RUN dotnet restore "CryptoApi/CryptoApi.csproj"


COPY . .


WORKDIR "/src/CryptoApi"
RUN dotnet build "CryptoApi.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "CryptoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app


RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser


COPY --from=publish /app/publish .


EXPOSE 80


ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production


ENTRYPOINT ["dotnet", "CryptoApi.dll"]
