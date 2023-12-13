FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BonTech.Product.Api/BonTech.Product.Api.csproj", "BonTech.Product.Api/"]
RUN dotnet restore "BonTech.Product.Api/BonTech.Product.Api.csproj"
COPY . .
WORKDIR "/src/BonTech.Product.Api"
RUN dotnet build "BonTech.Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BonTech.Product.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BonTech.Product.Api.dll"]
