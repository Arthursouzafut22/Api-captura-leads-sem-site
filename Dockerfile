# ---------- Build ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia só os .csproj primeiro, para aproveitar o cache do restore
COPY LeadSemSiteAPI/LeadSemSiteAPI.csproj LeadSemSiteAPI/
COPY LeadSemSite.Application/LeadSemSite.Application.csproj LeadSemSite.Application/
COPY LeadSemSite.Domain/LeadSemSite.Domain.csproj LeadSemSite.Domain/
COPY LeadSemSite.Infrastructure/LeadSemSite.Infrastructure.csproj LeadSemSite.Infrastructure/

RUN dotnet restore LeadSemSiteAPI/LeadSemSiteAPI.csproj

# Copia o restante do código e publica
COPY . .
RUN dotnet publish LeadSemSiteAPI/LeadSemSiteAPI.csproj -c Release -o /app/publish --no-restore

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# A Render injeta a variável PORT; o ASP.NET precisa escutar nela
ENV ASPNETCORE_ENVIRONMENT=Production
CMD ["sh", "-c", "ASPNETCORE_URLS=http://+:${PORT:-8080} exec dotnet LeadSemSiteAPI.dll"]