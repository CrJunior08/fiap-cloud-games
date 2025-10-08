# Base para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os projetos para restaurar
COPY ["src/FCG.Web/FCG.Web.csproj", "src/FCG.Web/"]
COPY ["src/FCG.Application/FCG.Application.csproj", "src/FCG.Application/"]
COPY ["src/FCG.Domain/FCG.Domain.csproj", "src/FCG.Domain/"]
COPY ["src/FCG.Infrastructure/FCG.Infrastructure.csproj", "src/FCG.Infrastructure/"]



# Restaura dependências
RUN dotnet restore "./src/FCG.Web/FCG.Web.csproj"

# Copia o restante
COPY . .

# Compila o projeto
WORKDIR "/src/src/FCG.Web"
RUN dotnet publish "./FCG.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FCG.Web.dll"]
