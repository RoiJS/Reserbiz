FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["ReserbizAPP.API/ReserbizAPP.API.csproj", "ReserbizAPP.API/"]
RUN dotnet restore "ReserbizAPP.API/ReserbizAPP.API.csproj"
COPY . .
WORKDIR "/src/ReserbizAPP.API"
RUN dotnet build "ReserbizAPP.API.csproj" -c Release -o /app/build
COPY ReserbizAPP.API/Templates /app/build/Templates

FROM build AS publish
RUN dotnet publish "ReserbizAPP.API.csproj" -c Release -o /app/publish
COPY ReserbizAPP.API/Templates /app/publish/Templates

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Entry point for DockerDevelopment
ENTRYPOINT ["dotnet", "ReserbizAPP.API.dll", "--environment=DockerDevelopment"]

# Entry point for Production
# ENTRYPOINT ["dotnet", "ReserbizAPP.API.dll", "--environment=Production"]