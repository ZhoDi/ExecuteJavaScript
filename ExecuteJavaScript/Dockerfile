#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ExecuteJavaScript/ExecuteJavaScript.csproj", "ExecuteJavaScript/"]
RUN dotnet restore "ExecuteJavaScript/ExecuteJavaScript.csproj"
COPY . .
WORKDIR "/src/ExecuteJavaScript"
RUN dotnet build "ExecuteJavaScript.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExecuteJavaScript.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExecuteJavaScript.dll"]
