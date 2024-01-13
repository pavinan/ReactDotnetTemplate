# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file(s) to the container

COPY ./ReactDotnetTemplate.AppGateway/ReactDotnetTemplate.AppGateway.csproj ./ReactDotnetTemplate.AppGateway/ReactDotnetTemplate.AppGateway.csproj

# Restore the NuGet packages
RUN dotnet restore ./ReactDotnetTemplate.AppGateway/ReactDotnetTemplate.AppGateway.csproj

# Copy the remaining source code to the container
COPY . .

# Build the application
RUN dotnet build ./ReactDotnetTemplate.AppGateway/ReactDotnetTemplate.AppGateway.csproj -c Release --no-restore

# Publish the application
RUN dotnet publish ./ReactDotnetTemplate.AppGateway/ReactDotnetTemplate.AppGateway.csproj -c Release --no-restore --output /app/publish

# Use the official .NET Core runtime image as the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage to the final stage
COPY --from=build /app/publish .

# Expose the port that the application listens on
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ReactDotnetTemplate.AppGateway.dll"]
