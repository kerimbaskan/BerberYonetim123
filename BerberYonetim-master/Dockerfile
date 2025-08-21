# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sadece csproj + sln kopyala (cache verimli olsun)
COPY BerberYonetim123.sln ./
COPY BerberYonetim123/*.csproj ./BerberYonetim123/

# Restore
RUN dotnet restore ./BerberYonetim123/BerberYonetim123.csproj

# Kalan her şeyi kopyala ve publish et
COPY . .
RUN dotnet publish ./BerberYonetim123/BerberYonetim123.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./

# Konteyner içinde 8080'i dinle
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# DLL adını projenin gerçek çıktısına göre bırak
ENTRYPOINT ["dotnet", "BerberYonetim123.dll"]
