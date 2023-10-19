#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BoxCar.Services.Payment/BoxCar.Services.Payment.csproj", "BoxCar.Services.Payment/"]
COPY ["BoxCar.Integration.MessageBus/BoxCar.Integration.MessageBus.csproj", "BoxCar.Integration.MessageBus/"]
COPY ["BoxCar.Integration.Messages/BoxCar.Integration.Messages.csproj", "BoxCar.Integration.Messages/"]
COPY ["BoxCar.Shared/BoxCar.Shared.csproj", "BoxCar.Shared/"]
RUN dotnet restore "BoxCar.Services.Payment/BoxCar.Services.Payment.csproj"
COPY . .
WORKDIR "/src/BoxCar.Services.Payment"
RUN dotnet build "BoxCar.Services.Payment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoxCar.Services.Payment.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoxCar.Services.Payment.dll"]