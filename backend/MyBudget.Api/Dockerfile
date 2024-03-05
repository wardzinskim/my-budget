#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyBudget.Api/MyBudget.Api.csproj", "MyBudget.Api/"]
COPY ["Core/MyBudget.Application/MyBudget.Application.csproj", "Core/MyBudget.Application/"]
COPY ["Core/MyBudget.Infrastructure/MyBudget.Infrastructure.csproj", "Core/MyBudget.Infrastructure/"]
RUN dotnet restore "./MyBudget.Api/./MyBudget.Api.csproj"
COPY . .
WORKDIR "/src/MyBudget.Api"
RUN dotnet build "./MyBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBudget.Api.dll"]