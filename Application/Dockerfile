# Define a imagem base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Application.csproj", "./"]
RUN dotnet restore "./Application.csproj"
COPY . .
RUN dotnet tool install --global dotnet-ef --version 7.0.14

COPY wait-for-it.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/wait-for-it.sh

CMD ["wait-for-it.sh", "mysql:3306", "--", "dotnet", "ef", "database", "update"]

# Build da aplicação
WORKDIR "/src/."
RUN dotnet build "Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.dll"]
