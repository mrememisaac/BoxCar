#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BoxCar.Admin.Api/BoxCar.Admin.Api.csproj", "BoxCar.Admin.Api/"]
COPY ["Admin.Core/BoxCar.Admin.Core.csproj", "Admin.Core/"]
COPY ["BoxCar.Admin.Domain/BoxCar.Admin.Domain.csproj", "BoxCar.Admin.Domain/"]
COPY ["BoxCar.Integration.MessageBus/BoxCar.Integration.MessageBus.csproj", "BoxCar.Integration.MessageBus/"]
COPY ["BoxCar.Integration.Messages/BoxCar.Integration.Messages.csproj", "BoxCar.Integration.Messages/"]
COPY ["BoxCar.Shared/BoxCar.Shared.csproj", "BoxCar.Shared/"]
COPY ["BoxCar.Admin.Persistence/BoxCar.Admin.Persistence.csproj", "BoxCar.Admin.Persistence/"]
RUN dotnet restore "BoxCar.Admin.Api/BoxCar.Admin.Api.csproj"
COPY . .
WORKDIR "/src/BoxCar.Admin.Api"
RUN dotnet build "BoxCar.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoxCar.Admin.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoxCar.Admin.Api.dll"]