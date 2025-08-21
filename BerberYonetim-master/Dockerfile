# ---------- BUILD ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözüm ve csproj yollarını kopyala (cache için)
COPY BerberYonetim.sln ./
COPY Berber/BerberYonetim/*.csproj Berber/BerberYonetim/

# Restore
RUN dotnet restore Berber/BerberYonetim/BerberYonetim.csproj

# Tüm kaynakları kopyala ve publish et
COPY . .
RUN dotnet publish Berber/BerberYonetim/BerberYonetim.csproj -c Release -o /app/publish /p:UseAppHost=false \
    -p:UseRazorSourceGenerator=false -p:RazorCompileOnBuild=false -p:RazorCompileOnPublish=false

# ---------- RUNTIME ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "BerberYonetim.dll"]
