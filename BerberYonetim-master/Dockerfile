# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sadece csproj + sln kopyala (cache verimli olsun)
COPY BerberYonetim.sln ./
COPY BerberYonetim/*.csproj ./BerberYonetim/

# Restore
RUN dotnet restore ./BerberYonetim/BerberYonetim.csproj

# Kalan her şeyi kopyala ve publish et
COPY . .
RUN dotnet publish ./BerberYonetim/BerberYonetim.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./

# Konteyner içinde 8080'i dinle
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# DLL adını projenin gerçek çıktısına göre bırak
ENTRYPOINT ["dotnet", "BerberYonetim.dll"]
