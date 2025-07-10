FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5296

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CompanyWebsite.sln", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["src/CompanyWebsite.Application/CompanyWebsite.Application.csproj", "src/CompanyWebsite.Application/"]
COPY ["src/CompanyWebsite.Contracts/CompanyWebsite.Contracts.csproj", "src/CompanyWebsite.Contracts/"]
COPY ["src/CompanyWebsite.Domain/CompanyWebsite.Domain.csproj", "src/CompanyWebsite.Domain/"]
COPY ["src/CompanyWebsite.Infrastructure.Mssql/CompanyWebsite.Infrastructure.Mssql.csproj", "src/CompanyWebsite.Infrastructure.Mssql/"]
COPY ["src/CompanyWebsite.Presenters/CompanyWebsite.Presenters.csproj", "src/CompanyWebsite.Presenters/"]
COPY ["src/CompanyWebsite.Web/CompanyWebsite.Web.csproj", "src/CompanyWebsite.Web/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]

RUN dotnet restore "CompanyWebsite.sln"
COPY . .
WORKDIR "/src/src/CompanyWebsite.Web"
RUN dotnet build "CompanyWebsite.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompanyWebsite.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5296
ENTRYPOINT ["dotnet", "CompanyWebsite.Web.dll"] 