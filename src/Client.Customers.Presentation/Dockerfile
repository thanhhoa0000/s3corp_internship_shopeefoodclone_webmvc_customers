# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN mkdir -p /home/app/.aspnet/https

COPY ./certs/.aspnet/https/Client.Customers.pfx /home/app/.aspnet/https/Client.Customers.pfx

RUN chown -R app:app /home/app/.aspnet/https

RUN mkdir -p /keys && chmod -R 777 /keys

USER app
WORKDIR /app
COPY ["nlog.config", "."]
EXPOSE 7000
EXPOSE 7001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Client.Customers.Presentation/Client.Customers.Presentation.csproj", "src/Client.Customers.Presentation/"]
COPY ["src/Client.Customers.Application/Client.Customers.Application.csproj", "src/Client.Customers.Application/"]
RUN dotnet restore "./src/Client.Customers.Presentation/Client.Customers.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Client.Customers.Presentation/"
RUN dotnet build "./Client.Customers.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Client.Customers.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.Customers.Presentation.dll"]