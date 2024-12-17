# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the build configuration to Release by default
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the csproj file and restore the dependencies
COPY ["backend.csproj", "./"]

# Restore the dependencies
RUN dotnet restore "./backend.csproj"

# Copy the rest of the source code into the container
COPY . .

# Build the project in Release configuration
RUN dotnet build "./backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application to the /app/publish folder
FROM build AS publish
RUN dotnet publish "./backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the official .NET runtime image for the final production container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expose the ports that your app will run on
EXPOSE 8080
EXPOSE 8081

# Copy the published application from the build stage
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "backend.dll"]