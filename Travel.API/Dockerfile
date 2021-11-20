#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Travel.API/Travel.API.csproj", "Travel.API/"]
RUN dotnet restore "Travel.API/Travel.API.csproj"
COPY . .
WORKDIR "/src/Travel.API"
RUN dotnet build "Travel.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Travel.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Travel.API.dll"]