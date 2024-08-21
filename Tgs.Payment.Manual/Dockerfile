#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM hub.lumigame.com/base/dotnet/core/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["Tgs.Payment.Manual/Tgs.Payment.Manual.csproj", "Tgs.Payment.Manual/"]
COPY ["Tgs.Payment.Manual.Core/Tgs.Payment.Manual.Core.csproj", "Tgs.Payment.Manual.Core/"]
COPY ["Tgs.Payment.ManualDeposit/Tgs.Payment.ManualDeposit.csproj", "Tgs.Payment.ManualDeposit/"]
RUN dotnet restore "./Tgs.Payment.Manual/./Tgs.Payment.Manual.csproj"
COPY . .
WORKDIR "/src/Tgs.Payment.Manual"
RUN dotnet build "./Tgs.Payment.Manual.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tgs.Payment.Manual.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM hub.lumigame.com/base/dotnet/core/aspnet:8.0-alpine-24h1.1 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tgs.Payment.Manual.dll"]