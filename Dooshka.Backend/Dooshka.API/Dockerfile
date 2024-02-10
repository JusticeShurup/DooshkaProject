#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 20000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Dooshka.API/Dooshka.API.csproj", "Dooshka.API/"]
COPY ["Dooshka.Application/Dooshka.Application.csproj", "Dooshka.Application/"]
COPY ["Dooshka.Domain/Dooshka.Domain.csproj", "Dooshka.Domain/"]
COPY ["Dooshka.Infrastructure/Dooshka.Infrastructure.csproj", "Dooshka.Infrastructure/"]
COPY ["Dooshka.Persistence/Dooshka.Persistence.csproj", "Dooshka.Persistence/"]
RUN dotnet restore "Dooshka.API/Dooshka.API.csproj"
COPY . .
WORKDIR "/src/Dooshka.API"
RUN dotnet build "Dooshka.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dooshka.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dooshka.API.dll"]