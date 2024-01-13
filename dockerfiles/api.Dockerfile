# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build



# Set the working directory inside the container
WORKDIR /app

# Copy the project file(s) to the container

COPY ./ReactDotnetTemplate.API/ReactDotnetTemplate.API.csproj ./ReactDotnetTemplate.API/ReactDotnetTemplate.API.csproj
COPY ./ReactDotnetTemplate.AppGateway/ReactDotnetTemplate.AppGateway.csproj ./ReactDotnetTemplate.AppGateway/ReactDotnetTemplate.AppGateway.csproj
COPY ./ReactDotnetTemplate.Application/ReactDotnetTemplate.Application.csproj ./ReactDotnetTemplate.Application/ReactDotnetTemplate.Application.csproj
COPY ./ReactDotnetTemplate.Infrastructure/ReactDotnetTemplate.Infrastructure.csproj ./ReactDotnetTemplate.Infrastructure/ReactDotnetTemplate.Infrastructure.csproj
COPY ./ReactDotnetTemplate.Models/ReactDotnetTemplate.Models.csproj ./ReactDotnetTemplate.Models/ReactDotnetTemplate.Models.csproj
COPY ./ReactDotnetTemplate.Persistence/ReactDotnetTemplate.Persistence.csproj ./ReactDotnetTemplate.Persistence/ReactDotnetTemplate.Persistence.csproj
COPY ./ReactDotnetTemplate.WebSPA/ReactDotnetTemplate.WebSPA.csproj ./ReactDotnetTemplate.WebSPA/ReactDotnetTemplate.WebSPA.csproj

# Restore the NuGet packages
RUN dotnet restore ./ReactDotnetTemplate.API/ReactDotnetTemplate.API.csproj

# Copy the remaining source code to the container
COPY . .

# Build the application
RUN dotnet build ./ReactDotnetTemplate.API/ReactDotnetTemplate.API.csproj -c Release --no-restore

# Publish the application
RUN dotnet publish ./ReactDotnetTemplate.API/ReactDotnetTemplate.API.csproj -c Release --no-restore --output /app/publish

# Use the official .NET Core runtime image as the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage to the final stage
COPY --from=build /app/publish .

# Expose the port that the application listens on
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ReactDotnetTemplate.API.dll"]
