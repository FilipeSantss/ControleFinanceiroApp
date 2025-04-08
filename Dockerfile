# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante dos arquivos e publica o aplicativo
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa de execução
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Define a porta em que o aplicativo será executado
EXPOSE 80

# Comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "ControleFinanceiroRebuild.dll"]
