FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

WORKDIR /src

COPY BookStoresWebAPI/BookStoresWebAPI/WebApi.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build BookStoresWebAPI.sln -c Release

# RUN dotnet test ...

RUN dotnet publish BookStoresWebAPI.sln  -c Release -o /dist


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim-arm32v7

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

# does not contain the trailing slash
ENV CORS_DOMAIN http://+:5000

COPY --from=build /dist .

CMD ["dotnet", "WebApi.dll"]
