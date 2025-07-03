FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BaseHW/BaseHW.csproj", "BaseHW/"]
RUN dotnet restore "BaseHW/BaseHW.csproj"
COPY . .
WORKDIR "/src/BaseHW"
RUN dotnet build "BaseHW.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseHW.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaseHW.dll"] 