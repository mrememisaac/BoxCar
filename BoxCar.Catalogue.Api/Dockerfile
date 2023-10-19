#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BoxCar.Catalogue.Api/BoxCar.Catalogue.Api.csproj", "BoxCar.Catalogue.Api/"]
COPY ["BoxCar.Catalogue.Core/BoxCar.Catalogue.Core.csproj", "BoxCar.Catalogue.Core/"]
COPY ["BoxCar.Catalogue.Domain/BoxCar.Catalogue.Domain.csproj", "BoxCar.Catalogue.Domain/"]
COPY ["BoxCar.Integration.MessageBus/BoxCar.Integration.MessageBus.csproj", "BoxCar.Integration.MessageBus/"]
COPY ["BoxCar.Integration.Messages/BoxCar.Integration.Messages.csproj", "BoxCar.Integration.Messages/"]
COPY ["BoxCar.Shared/BoxCar.Shared.csproj", "BoxCar.Shared/"]
COPY ["BoxCar.Catalogue.Persistence/BoxCar.Catalogue.Persistence.csproj", "BoxCar.Catalogue.Persistence/"]
RUN dotnet restore "BoxCar.Catalogue.Api/BoxCar.Catalogue.Api.csproj"
COPY . .
WORKDIR "/src/BoxCar.Catalogue.Api"
RUN dotnet build "BoxCar.Catalogue.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoxCar.Catalogue.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoxCar.Catalogue.Api.dll"]