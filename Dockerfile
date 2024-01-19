FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app/src
COPY src/*.csproj .
RUN dotnet restore
WORKDIR /app
COPY . .
WORKDIR /app/src
RUN dotnet publish -o /app/build -c Release

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build-env /app/build .
ENTRYPOINT [ "dotnet", "BlazorServerSignalRApp.dll", "--urls=http://0.0.0.0:80"]
