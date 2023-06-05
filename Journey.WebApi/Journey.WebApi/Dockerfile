#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Journey.WebApi/Journey.WebApi.csproj", "Journey.WebApi/"]
COPY ["Journey.ApplicationServices.Shared/Journey.ApplicationServices.Shared.csproj", "Journey.ApplicationServices.Shared/"]
COPY ["Journey.ApplicationServices/Journey.ApplicationServices.csproj", "Journey.ApplicationServices/"]
COPY ["Journey.Core/Journey.Core.csproj", "Journey.Core/"]
COPY ["Journey.DataAccess/Journey.DataAccess.csproj", "Journey.DataAccess/"]
RUN dotnet restore "Journey.WebApi/Journey.WebApi.csproj"
COPY . .
WORKDIR "/src/Journey.WebApi"
RUN dotnet build "Journey.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Journey.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Journey.WebApi.dll"]