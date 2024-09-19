FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expose port 80 for the app
EXPOSE 80

# Run the application on port 80
ENV ASPNETCORE_URLS=http://+:80


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CommandsService.csproj", "."]
RUN dotnet restore "./CommandsService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CommandsService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CommandsService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommandsService.dll"]