FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy of csproj and restore  as distinct layers
COPY *.sln .
COPY ./src/SeoulAir.Device.Api/*.csproj ./src/SeoulAir.Device.Api/
COPY ./src/SeoulAir.Device.Domain/*.csproj ./src/SeoulAir.Device.Domain/
COPY ./src/SeoulAir.Device.Domain.Services/*.csproj ./src/SeoulAir.Device.Domain.Services/

RUN dotnet restore

# copy everything else and build app
COPY *.sln .
COPY ./src/SeoulAir.Device.Api/. ./src/SeoulAir.Device.Api/
COPY ./src/SeoulAir.Device.Domain/. ./src/SeoulAir.Device.Domain/
COPY ./src/SeoulAir.Device.Domain.Services/. ./src/SeoulAir.Device.Domain.Services/

WORKDIR /app/src/SeoulAir.Device.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build /app/src/SeoulAir.Device.Api/out ./
COPY ./resources/data.csv ./resources/
ENV ASPNETCORE_URLS=http://+:5500
ENTRYPOINT ["dotnet","SeoulAir.Device.Api.dll"]