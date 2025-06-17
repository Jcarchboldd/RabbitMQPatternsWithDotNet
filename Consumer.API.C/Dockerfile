FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Consumer.API.C/Consumer.API.C.csproj", "Consumer.API.C/"]
COPY ["Messaging.Common/Messaging.Common.csproj", "Messaging.Common/"]
RUN dotnet restore "Consumer.API.C/Consumer.API.C.csproj"
COPY . .
WORKDIR "/src/Consumer.API.C"
RUN dotnet build "Consumer.API.C.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Consumer.API.C.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consumer.API.C.dll"]
