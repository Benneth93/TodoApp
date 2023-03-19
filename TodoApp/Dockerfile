FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app


COPY . .
RUN dotnet publish TodoApp.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5192
ENV ASPNETCORE_ENVIRONMENT=DevelopmentDocker
EXPOSE 5192
EXPOSE 1433

CMD ["dotnet", "TodoApp.dll", "--urls", "http://0.0.0.0:5192"]