FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FleetDepot.Ui/FleetDepot.Ui.csproj", "src/FleetDepot.Ui/"]
RUN dotnet restore "./src/FleetDepot.Ui/FleetDepot.Ui.csproj"
COPY . .
WORKDIR "/src/src/FleetDepot.Ui"
RUN dotnet build "./FleetDepot.Ui.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
RUN dotnet publish "./FleetDepot.Ui.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
ENV API_BASE_ADDRESS=http://localhost:5084
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY "src/FleetDepot.Ui/nginx.conf" /etc/nginx/nginx.conf