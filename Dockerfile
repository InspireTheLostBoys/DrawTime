# build .NET app:
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as buildnet

WORKDIR /src

COPY WebAPI/BookStoresWebAPI/BookStoresWebAPI/WebApi.csproj .
RUN dotnet restore

COPY WebAPI .
RUN dotnet build BookStoresWebAPI.sln -c Release

# RUN dotnet test ...
RUN dotnet publish BookStoresWebAPI.sln -c Release -o /dist


# build Vue app:
FROM node:12.18.1-alpine as buildvue

WORKDIR /src

COPY client/package.json .
RUN npm install

# webpack build
COPY client .
RUN npm run build


# Copy results from both places into production container:
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

# copy .net content
COPY --from=buildnet /dist .
# copy vue content into .net's static files folder:
COPY --from=buildvue /src/dist /app/wwwroot

CMD ["dotnet", "WebApi.dll"]
