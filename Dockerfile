FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["personapi-dotnet.csproj", "./"]
RUN dotnet restore "personapi-dotnet.csproj"

COPY . .
RUN dotnet build "personapi-dotnet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "personapi-dotnet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "personapi-dotnet.dll"]