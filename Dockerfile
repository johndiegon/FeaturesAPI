#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FeaturesAPI/FeaturesWPP.API.csproj", "FeaturesAPI/"]
RUN dotnet restore "FeaturesAPI/FeaturesAPI.csproj"
COPY . .
WORKDIR "/src/FeaturesAPI"
RUN dotnet build "FeaturesAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FeaturesAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeaturesAPI.dll"]