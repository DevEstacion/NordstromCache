FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NordstromCache.csproj", "./"]
RUN dotnet restore "NordstromCache.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "NordstromCache.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NordstromCache.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NordstromCache.dll"]
