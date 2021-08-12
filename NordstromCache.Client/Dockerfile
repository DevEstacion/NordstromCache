FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NordstromCache.Client/NordstromCache.Client.csproj", "NordstromCache.Client/"]
RUN dotnet restore "NordstromCache.Client/NordstromCache.Client.csproj"
COPY . .
WORKDIR "/src/NordstromCache.Client"
RUN dotnet build "NordstromCache.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NordstromCache.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NordstromCache.Client.dll"]
