FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY Web/bin/Release/netcoreapp2.2/publish .
ENTRYPOINT ["dotnet", "Web.dll"]

