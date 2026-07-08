FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY UserService.Domain/UserService.Domain.csproj UserService.Domain/
COPY UserService.Application/UserService.Application.csproj UserService.Application/
COPY UserService.Infrastructure/UserService.Infrastructure.csproj UserService.Infrastructure/
COPY UserService.Api/UserService.Api.csproj UserService.Api/
COPY *.slnx .

RUN dotnet restore "UserService.Api/UserService.Api.csproj"

COPY . .
RUN dotnet publish "UserService.Api/UserService.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
