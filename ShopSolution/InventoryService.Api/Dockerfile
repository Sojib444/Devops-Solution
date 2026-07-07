FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ShopSolution.Shared/ShopSolution.Shared.csproj ShopSolution.Shared/
COPY InventoryService.Domain/InventoryService.Domain.csproj InventoryService.Domain/
COPY InventoryService.Application/InventoryService.Application.csproj InventoryService.Application/
COPY InventoryService.Infrastructure/InventoryService.Infrastructure.csproj InventoryService.Infrastructure/
COPY InventoryService.Api/InventoryService.Api.csproj InventoryService.Api/
COPY *.slnx .

RUN dotnet restore "InventoryService.Api/InventoryService.Api.csproj"

COPY . .
RUN dotnet publish "InventoryService.Api/InventoryService.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.Api.dll"]
