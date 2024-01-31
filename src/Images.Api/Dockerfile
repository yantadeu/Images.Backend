FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src


COPY ["src/Images.Api/Images.Api.csproj", "src/Images.Api/"]
RUN dotnet restore "src/Images.Api/Images.Api.csproj"
COPY . .
WORKDIR "/src/src/Images.Api"
RUN dotnet build "Images.Api.csproj" -c $configuration -o /app/build


FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Images.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Images.Api.dll"]
