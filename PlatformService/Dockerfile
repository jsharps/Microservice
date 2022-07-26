FROM  mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

#COPY CSPROJ TO WORKING DIR
COPY *.csproj ./
RUN dotnet restore

#COPY EVERYTHING ELSE TO WORKING DIR
COPY . ./
RUN dotnet publish -c Release -o out

#BUILD RUNTIME IMAGE
FROM  mcr.microsoft.com/dotnet/aspnet:6.0 
WORKDIR /app

COPY --from=build-env /app/out .

#kicks of what we run

ENTRYPOINT [ "dotnet", "PlatformService.dll" ]