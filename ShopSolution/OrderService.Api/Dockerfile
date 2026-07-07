FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ShopSolution.Shared/ShopSolution.Shared.csproj ShopSolution.Shared/
COPY OrderService.Domain/OrderService.Domain.csproj OrderService.Domain/
COPY OrderService.Application/OrderService.Application.csproj OrderService.Application/
COPY OrderService.Infrastructure/OrderService.Infrastructure.csproj OrderService.Infrastructure/
COPY OrderService.Api/OrderService.Api.csproj OrderService.Api/
COPY *.slnx .

RUN dotnet restore "OrderService.Api/OrderService.Api.csproj"

COPY . .
RUN dotnet publish "OrderService.Api/OrderService.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderService.Api.dll"]
