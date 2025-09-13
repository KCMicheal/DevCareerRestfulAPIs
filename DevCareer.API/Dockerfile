FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5192

ENV ASPNETCORE_URLS=http://+:5192

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["DevCareer.API/DevCareer.API.csproj", "DevCareer.API/"]
COPY ["DevCareer.Data/DevCareer.Data.csproj", "DevCareer.Data/"]
RUN dotnet restore "DevCareer.API/DevCareer.API.csproj"
COPY . .
WORKDIR "/src/DevCareer.API"
RUN dotnet build "DevCareer.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "DevCareer.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevCareer.API.dll"]
