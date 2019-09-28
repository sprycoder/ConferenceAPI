FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Conference.API/Conference.API.csproj", "Conference.API/"]
RUN dotnet restore "Conference.API/Conference.API.csproj"
COPY . .
WORKDIR "/src/Conference.API"
RUN dotnet build "Conference.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Conference.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Conference.API.dll"]
