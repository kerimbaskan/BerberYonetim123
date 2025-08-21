# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY BerberYonetim.sln ./
COPY BerberYonetim/*.csproj BerberYonetim/
RUN dotnet restore BerberYonetim/BerberYonetim.csproj

COPY . .
# ⬇️  ÖNEMLİ: SG'yi kapatan bayraklar
RUN dotnet publish BerberYonetim/BerberYonetim.csproj -c Release -o /app/publish /p:UseAppHost=false \
    -p:UseRazorSourceGenerator=false \
    -p:RazorCompileOnBuild=false \
    -p:RazorCompileOnPublish=false
# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "BerberYonetim.dll"]
