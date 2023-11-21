FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Printer/Printer.csproj", "Printer/"]
RUN dotnet restore "Printer/Printer.csproj"
COPY . .
WORKDIR "/src/Printer"
RUN dotnet build "Printer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Printer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Printer.dll"]
