# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5084
ENV ASPNETCORE_URLS=http://0.0.0.0:5084
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DB_CONNECTION_STRING="server=changeme;port=3306;database=changeme;user=changeme;password=changeme;"
USER $APP_UID

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FleetDepot.Api/FleetDepot.Api.csproj", "src/FleetDepot.Api/"]
COPY ["src/FleetDepot.Dal/FleetDepot.Dal.csproj", "src/FleetDepot.Dal/"]
COPY ["src/FleetDepot.Core/FleetDepot.Core.csproj", "src/FleetDepot.Core/"]
COPY ["src/FleetDepot.Types/FleetDepot.Types.csproj", "src/FleetDepot.Types/"]
RUN dotnet restore "./src/FleetDepot.Api/FleetDepot.Api.csproj"
COPY . .
WORKDIR "/src/src/FleetDepot.Api"
RUN dotnet build "./FleetDepot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FleetDepot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetDepot.Api.dll"]