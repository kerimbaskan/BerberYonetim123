# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sadece csproj'u kopyalayarak restore cache'ini doğru kullan
COPY Berber/BerberYonetim/*.csproj Berber/BerberYonetim/

# Gerekirse çözüm dosyan da varsa kopyalayabilirsin (opsiyonel)
# COPY BerberYonetim.sln .

RUN dotnet restore Berber/BerberYonetim/BerberYonetim.csproj

# Şimdi tüm kaynak kodu kopyala
COPY . .

# Release publish
RUN dotnet publish Berber/BerberYonetim/BerberYonetim.csproj -c Release -o /app/publish

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Konteynerda 8080'i dinleyelim (HTTPS kullanmayacağız)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
# Giriş dll'ini çalıştır
ENTRYPOINT ["dotnet", "BerberYonetim.dll"]
