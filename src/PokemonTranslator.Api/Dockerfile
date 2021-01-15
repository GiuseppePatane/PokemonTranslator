#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/PokemonTranslator.Api/PokemonTranslator.Api.csproj", "src/PokemonTranslator.Api/"]
COPY ["src/PokemonTranslator.Infrastructure/PokemonTranslator.Infrastructure.csproj", "src/PokemonTranslator.Infrastructure/"]
COPY ["src/PokemonTranslator.Infrastructure.ShakespeareClient/PokemonTranslator.Infrastructure.ShakespeareClient.csproj", "src/PokemonTranslator.Infrastructure.ShakespeareClient/"]
COPY ["src/PokemonTranslator.Core/PokemonTranslator.Core.csproj", "src/PokemonTranslator.Core/"]
COPY ["src/PokemonTranslator.Infrastructure.PokemonCient/PokemonTranslator.Infrastructure.PokemonCient.csproj", "src/PokemonTranslator.Infrastructure.PokemonCient/"]
RUN dotnet restore "src/PokemonTranslator.Api/PokemonTranslator.Api.csproj"
COPY . .
WORKDIR "/src/src/PokemonTranslator.Api"
RUN dotnet build "PokemonTranslator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonTranslator.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokemonTranslator.Api.dll"]