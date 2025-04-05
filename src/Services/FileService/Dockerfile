FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./src/Services/FileService/FileService.Api/FileService.Api.csproj ./Services/FileService/FileService.Api/
COPY ./src/Services/FileService/FileService.Domain/FileService.Domain.csproj ./Services/FileService/FileService.Domain/
COPY ./src/Services/FileService/FileService.DAL/FileService.DAL.csproj ./Services/FileService/FileService.DAL/

RUN dotnet restore ./Services/FileService/FileService.Api/FileService.Api.csproj

COPY ./src .
RUN dotnet build ./Services/FileService/FileService.Api/FileService.Api.csproj -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/build .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "FileService.Api.dll"]
