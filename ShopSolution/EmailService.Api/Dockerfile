FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ShopSolution.Shared/ShopSolution.Shared.csproj ShopSolution.Shared/
COPY EmailService.Domain/EmailService.Domain.csproj EmailService.Domain/
COPY EmailService.Application/EmailService.Application.csproj EmailService.Application/
COPY EmailService.Infrastructure/EmailService.Infrastructure.csproj EmailService.Infrastructure/
COPY EmailService.Api/EmailService.Api.csproj EmailService.Api/
COPY *.slnx .

RUN dotnet restore "EmailService.Api/EmailService.Api.csproj"

COPY . .
RUN dotnet publish "EmailService.Api/EmailService.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EmailService.Api.dll"]
