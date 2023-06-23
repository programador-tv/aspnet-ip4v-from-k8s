# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0.100-preview.4-bookworm-slim-amd64 AS build

WORKDIR /app

COPY . .

RUN dotnet publish ./api.ipv4.csproj -c release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0.0-preview.4-bookworm-slim-amd64

WORKDIR /app
COPY --from=build /out ./

CMD "./api.ipv4"