# Base stage for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN mkdir -p /home/app/.aspnet/https && \
    chown -R app:app /home/app/.aspnet/https && \
    mkdir -p /keys && chmod -R 777 /keys

USER app
WORKDIR /app
COPY ./certs/.aspnet/https/Client.Customers.pfx /home/app/.aspnet/https/Client.Customers.pfx
COPY ["nlog.config", "."]
EXPOSE 7000 7001

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Client.Customers.Presentation/Client.Customers.Presentation.csproj", "src/Client.Customers.Presentation/"]
COPY ["src/Client.Customers.Application/Client.Customers.Application.csproj", "src/Client.Customers.Application/"]

# Restore dependencies
RUN dotnet restore "./src/Client.Customers.Presentation/Client.Customers.Presentation.csproj"

# Copy the rest of the source code
COPY . .

# Build the project
WORKDIR "/src/src/Client.Customers.Presentation/"
RUN dotnet build "./Client.Customers.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "./Client.Customers.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.Customers.Presentation.dll"]