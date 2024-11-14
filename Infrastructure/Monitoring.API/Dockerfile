FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Infrastructure/Monitoring.API/Monitoring.API.csproj", "Infrastructure/Monitoring.API/"]
COPY ["Infrastructure/Monitoring.Data/Monitoring.Data.csproj", "Infrastructure/Monitoring.Data/"]
RUN dotnet restore "Infrastructure/Monitoring.API/Monitoring.API.csproj"
COPY . .
WORKDIR "/src/Infrastructure/Monitoring.API"
RUN dotnet build "Monitoring.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Monitoring.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Monitoring.API.dll"]
