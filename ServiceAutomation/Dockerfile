FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 7000
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ServiceAutomation.Canvas.WebApi/ServiceAutomation.Canvas.WebApi.csproj", "ServiceAutomation.Canvas.WebApi/"]
COPY ["ServiceAutomation.DataAccess/ServiceAutomation.DataAccess.csproj", "ServiceAutomation.DataAccess/"]
COPY ["ServiceAutomation.Migrations/ServiceAutomation.DataAccess.Migrations.csproj", "ServiceAutomation.Migrations/"]
RUN dotnet restore "ServiceAutomation.Canvas.WebApi/ServiceAutomation.Canvas.WebApi.csproj"
COPY . .
WORKDIR "/src/ServiceAutomation.Canvas.WebApi"
RUN dotnet build "ServiceAutomation.Canvas.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceAutomation.Canvas.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceAutomation.Canvas.WebApi.dll"] 