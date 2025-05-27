FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["StockWorker/StockWorker.csproj", "StockWorker/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["ExternalServices/ExternalServices.csproj", "ExternalServices/"]
COPY ["Repository/Repository.csproj", "Repository/"]

RUN dotnet restore "StockWorker/StockWorker.csproj"

COPY . .

WORKDIR "/src/StockWorker"
RUN dotnet build "StockWorker.csproj" -c Release -o /app/build -v diag

FROM build AS publish
RUN dotnet publish "StockWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Establece variable para que escuche en todas las interfaces
ENV ASPNETCORE_URLS=http://+:8081
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockWorker.dll"]
