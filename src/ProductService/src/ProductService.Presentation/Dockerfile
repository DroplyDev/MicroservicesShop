#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ProductService.Presentation/ProductService.Presentation.csproj", "src/ProductService.Presentation/"]
COPY ["src/ProductService.Infrastructure/ProductService.Infrastructure.csproj", "src/ProductService.Infrastructure/"]
COPY ["src/ProductService.Application/ProductService.Application.csproj", "src/ProductService.Application/"]
COPY ["src/ProductService.Contracts/ProductService.Contracts.csproj", "src/ProductService.Contracts/"]
COPY ["src/ProductService.Domain/ProductService.Domain.csproj", "src/ProductService.Domain/"]
RUN dotnet restore "src/ProductService.Presentation/ProductService.Presentation.csproj"
COPY . .
WORKDIR "/src/src/ProductService.Presentation"
RUN dotnet build "ProductService.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.Presentation.dll"]