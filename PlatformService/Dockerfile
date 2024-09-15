# Base Stage: Sets up the environment to run the app.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Build Stage: Gathers and compiles the source code. OR Gather Ingredients (Build Stage)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY PlatformService.csproj ./
RUN dotnet restore "./PlatformService.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "./PlatformService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish Stage: Prepares the compiled app for deployment. Or Mix and Bake (Publish Stage)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PlatformService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final Stage: Combines everything into a ready-to-run package. OR Serve the Cake (Final Stage)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlatformService.dll"]
