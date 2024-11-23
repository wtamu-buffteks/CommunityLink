# Stage 1: Build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CommunityLink.csproj", "./"]
RUN dotnet restore "./CommunityLink.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet publish -c Release -o /app

# Stage 2: Runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "CommunityLink.dll"]