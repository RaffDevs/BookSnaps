FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookSnaps.Webapp/BookSnaps.Webapp.csproj", "BookSnaps.Webapp/"]
COPY ["BookSnaps.Infra/BookSnaps.Infra.csproj", "BookSnaps.Infra/"]
COPY ["BookSnaps.Domain/BookSnaps.Domain.csproj", "BookSnaps.Domain/"]
COPY ["BookSnaps.Application/BookSnaps.Application.csproj", "BookSnaps.Application/"]
RUN dotnet restore "BookSnaps.Webapp/BookSnaps.Webapp.csproj"

COPY BookSnaps.Webapp/package*.json BookSnaps.Webapp/
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g npm@latest
WORKDIR /src/BookSnaps.Webapp
RUN npm i

WORKDIR /src
COPY . .

WORKDIR "/src/BookSnaps.Webapp"
RUN dotnet build "./BookSnaps.Webapp.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN npm run build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookSnaps.Webapp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookSnaps.Webapp.dll"]
