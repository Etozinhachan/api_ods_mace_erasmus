# syntax = docker/dockerfile:1.2

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["api_ods_mace_erasmus.csproj", "."]
RUN dotnet restore "./api_ods_mace_erasmus.csproj"
COPY . .
WORKDIR "/src/."
RUN --mount=type=secret,id=_env,dst=/etc/secrets/.env cat /etc/secrets/.env
RUN dotnet build "api_ods_mace_erasmus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api_ods_mace_erasmus.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api_ods_mace_erasmus.dll"]