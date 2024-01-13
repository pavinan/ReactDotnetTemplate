FROM mcr.microsoft.com/devcontainers/javascript-node:20 as nodejs

WORKDIR /app

COPY ./ReactDotnetTemplate.WebSPA/ClientApp/package.json ./ReactDotnetTemplate.WebSPA/ClientApp/package.json
COPY ./ReactDotnetTemplate.WebSPA/ClientApp/package-lock.json ./ReactDotnetTemplate.WebSPA/ClientApp/package-lock.json

WORKDIR /app/ReactDotnetTemplate.WebSPA/ClientApp

RUN npm ci

COPY ./ReactDotnetTemplate.WebSPA/ClientApp/ /app/ReactDotnetTemplate.WebSPA/ClientApp/

RUN npm exec -- vite build

# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file(s) to the container

COPY ./ReactDotnetTemplate.WebSPA/ReactDotnetTemplate.WebSPA.csproj ./ReactDotnetTemplate.WebSPA/ReactDotnetTemplate.WebSPA.csproj

# Restore the NuGet packages
RUN dotnet restore ./ReactDotnetTemplate.WebSPA/ReactDotnetTemplate.WebSPA.csproj

# Copy the remaining source code to the container
COPY . .

# Build the application
RUN dotnet build ./ReactDotnetTemplate.WebSPA/ReactDotnetTemplate.WebSPA.csproj -c Release --no-restore

# Publish the application
RUN dotnet publish ./ReactDotnetTemplate.WebSPA/ReactDotnetTemplate.WebSPA.csproj -c Release --no-restore --output /app/publish

# Use the official .NET Core runtime image as the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the nodejs stage to the final stage
COPY --from=nodejs /app/ReactDotnetTemplate.WebSPA/wwwroot /app/ReactDotnetTemplate.WebSPA/wwwroot

# Copy the published output from the build stage to the final stage
COPY --from=build /app/publish .

# Expose the port that the application listens on
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ReactDotnetTemplate.WebSPA.dll"]
