# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and restore as distinct layers
COPY DevCareerRestfulAPIs.sln ./
COPY DevCareer.API/DevCareer.API.csproj DevCareer.API/
COPY DevCareer.Data/DevCareer.Data.csproj DevCareer.Data/
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish DevCareer.API/DevCareer.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DevCareer.API.dll"]
