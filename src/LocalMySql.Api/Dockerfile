#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/LocalMySql.Api/LocalMySql.Api.csproj", "LocalMySql.Api/"]
RUN dotnet restore "LocalMySql.Api/LocalMySql.Api.csproj"
COPY ./src .
WORKDIR "/src/LocalMySql.Api"
RUN dotnet build "LocalMySql.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LocalMySql.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocalMySql.Api.dll"]