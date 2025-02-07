#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
#EXPOSE 443

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PokemonAPI/PokemonAPI.csproj", "PokemonAPI/"]
RUN dotnet restore "PokemonAPI/PokemonAPI.csproj"
COPY . .
WORKDIR "/src/PokemonAPI"
RUN dotnet build "PokemonAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokemonAPI.dll"]