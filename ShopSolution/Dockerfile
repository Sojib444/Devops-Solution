FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only project files to leverage Docker layer caching for restore
COPY *.slnx .
COPY UserService.Api/*.csproj UserService.Api/
COPY UserService.Application/*.csproj UserService.Application/
COPY UserService.Domain/*.csproj UserService.Domain/
COPY UserService.Infrastructure/*.csproj UserService.Infrastructure/

RUN dotnet restore "UserService.Api/UserService.Api.csproj"

# Copy the rest of the source and publish
COPY . .
RUN dotnet publish "UserService.Api/UserService.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
