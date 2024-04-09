FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DesafioCadastroPessoaFisicaTests/DesafioCadastroPessoaFisicaTests.csproj", "DesafioCadastroPessoaFisicaTests/"]
COPY ["DesafioCadastroPessoaFisica.Infraestructe/DesafioCadastroPessoaFisica.Infraestructe.csproj", "DesafioCadastroPessoaFisica.Infraestructe/"]
RUN dotnet restore "DesafioCadastroPessoaFisicaTests/DesafioCadastroPessoaFisicaTests.csproj"
COPY . .
WORKDIR "/src/DesafioCadastroPessoaFisicaTests"
RUN dotnet build "DesafioCadastroPessoaFisicaTests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DesafioCadastroPessoaFisicaTests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioCadastroPessoaFisicaTests.dll"]
