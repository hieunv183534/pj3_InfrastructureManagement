#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InfrastructureManagement.Api/InfrastructureManagement.Api.csproj", "InfrastructureManagement.Api/"]
COPY ["InfrastructureManagement.Core/InfrastructureManagement.Core.csproj", "InfrastructureManagement.Core/"]
COPY ["InfrastructureManagement.Infrastructure/InfrastructureManagement.Infrastructure.csproj", "InfrastructureManagement.Infrastructure/"]
RUN dotnet restore "InfrastructureManagement.Api/InfrastructureManagement.Api.csproj"
COPY . .
WORKDIR "/src/InfrastructureManagement.Api"
RUN dotnet build "InfrastructureManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InfrastructureManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InfrastructureManagement.Api.dll"]