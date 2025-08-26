# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR ./
COPY . .
RUN dotnet restore ./src/RealEstatePortal.API/RealEstatePortal.API.csproj
RUN dotnet publish ./src/RealEstatePortal.API/RealEstatePortal.API.csproj -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "RealEstatePortal.API.dll"]