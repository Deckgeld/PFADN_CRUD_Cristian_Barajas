#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ticket.WebApi/Ticket.WebApi.csproj", "Ticket.WebApi/"]
COPY ["Ticket.ApplicationServices.Shared/Ticket.ApplicationServices.Shared.csproj", "Ticket.ApplicationServices.Shared/"]
COPY ["Ticket.ApplicationServices/Ticket.ApplicationServices.csproj", "Ticket.ApplicationServices/"]
COPY ["Ticket.Core/Ticket.Core.csproj", "Ticket.Core/"]
COPY ["Ticket.DataAccess/Ticket.DataAccess.csproj", "Ticket.DataAccess/"]
RUN dotnet restore "Ticket.WebApi/Ticket.WebApi.csproj"
COPY . .
WORKDIR "/src/Ticket.WebApi"
RUN dotnet build "Ticket.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ticket.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ticket.WebApi.dll"]