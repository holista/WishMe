FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src 
COPY ["wishme-dotnet/WishMe.Service/WishMe.Service.csproj", "WishMe.Service/"]
RUN dotnet restore "WishMe.Service/WishMe.Service.csproj"

WORKDIR "/src/WishMe.Service"
COPY . .

RUN dotnet build "WishMe.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WishMe.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WishMe.Service.dll"]