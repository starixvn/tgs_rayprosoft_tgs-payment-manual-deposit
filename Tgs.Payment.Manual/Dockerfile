#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BuildConfiguration=Release
RUN --mount=type=secret,id=nuget,dst=/pat cp /pat ~/.nuget/NuGet/NuGet.Config
RUN dotnet nuget remove source 'Microsoft Visual Studio Offline Packages' || true
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["Tgs.Payment.Manual/Tgs.Payment.Manual.csproj", "Tgs.Payment.Manual/"]
COPY ["Tgs.Payment.Manual.Core/Tgs.Payment.Manual.Core.csproj", "Tgs.Payment.Manual.Core/"]
COPY ["Tgs.Payment.ManualDeposit/Tgs.Payment.ManualDeposit.csproj", "Tgs.Payment.ManualDeposit/"]
RUN dotnet restore "Tgs.Payment.Manual/Tgs.Payment.Manual.csproj"
COPY . .
WORKDIR "/src/Tgs.Payment.Manual"
RUN dotnet build "Tgs.Payment.Manual.csproj" -c ${BuildConfiguration} -o /app/build

FROM build AS publish
ARG BuildConfiguration
RUN dotnet publish "Tgs.Payment.Manual.csproj" -c ${BuildConfiguration} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tgs.Payment.Manual.dll"]