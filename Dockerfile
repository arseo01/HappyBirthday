# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Image used for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the backend project file to the container
COPY ["HappyBirthDay/backend/backend.csproj", "backend/"]

# Restore the project's dependencies
RUN dotnet restore "backend/backend.csproj"

# Copy the remaining project files into the container
COPY . .

# Build the project
RUN dotnet build "backend/backend.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "backend/backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backend.dll"]