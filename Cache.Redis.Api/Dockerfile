#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Cache.Redis.Api/Cache.Redis.Api.csproj", "Cache.Redis.Api/"]
COPY ["Cache.Redis.Data/Cache.Redis.Data.csproj", "Cache.Redis.Data/"]
COPY ["Cache.Redis.Domain/Cache.Redis.Domain.csproj", "Cache.Redis.Domain/"]
RUN dotnet restore "Cache.Redis.Api/Cache.Redis.Api.csproj"
COPY . .
WORKDIR "/src/Cache.Redis.Api"
RUN dotnet build "Cache.Redis.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cache.Redis.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cache.Redis.Api.dll"]
