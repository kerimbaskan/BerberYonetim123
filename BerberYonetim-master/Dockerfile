# syntax=docker/dockerfile:1

# 1) Çalışma zamanı imajı
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
# İçeride 8080’i dinle
ENV ASPNETCORE_URLS=http://+:8080

# 2) Build imajı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Yalnızca csproj'u kopyala ve restore et (cache için)
COPY ["BerberYonetim/BerberYonetim.csproj", "BerberYonetim/"]
RUN dotnet restore "BerberYonetim/BerberYonetim.csproj"

# Tüm kaynak kodu kopyala
COPY . .
WORKDIR "/src/BerberYonetim"

# Derle
RUN dotnet build "BerberYonetim.csproj" -c Release -o /app/build

# 3) Publish
FROM build AS publish
RUN dotnet publish "BerberYonetim.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4) Final imaj
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BerberYonetim.dll"]
