# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy all project files
COPY . .

# Restore dependencies for the UI project (adjust path if needed)
RUN dotnet restore "UFAR.Classwork.UI/UFAR.Classwork.UI.csproj"

# Publish the UI project
WORKDIR "/src/UFAR.Classwork.UI"
RUN dotnet publish "UFAR.Classwork.UI.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose HTTP port
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "UFAR.Classwork.UI.dll"]
