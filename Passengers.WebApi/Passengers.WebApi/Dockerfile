#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Passengers.WebApi/Passengers.WebApi.csproj", "Passengers.WebApi/"]
COPY ["Passengers.ApplicationServices/Passengers.ApplicationServices.csproj", "Passengers.ApplicationServices/"]
COPY ["Passengers.ApplicationServices.Shared/Passengers.ApplicationServices.Shared.csproj", "Passengers.ApplicationServices.Shared/"]
COPY ["Passengers.Core/Passengers.Core.csproj", "Passengers.Core/"]
COPY ["Passengers.DataAccess/Passengers.DataAccess.csproj", "Passengers.DataAccess/"]
RUN dotnet restore "Passengers.WebApi/Passengers.WebApi.csproj"
COPY . .
WORKDIR "/src/Passengers.WebApi"
RUN dotnet build "Passengers.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Passengers.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Passengers.WebApi.dll"]