#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5200

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lift.Buddy.Api/Lift.Buddy.API.csproj", "Lift.Buddy.Api/"]
COPY ["Lift.Buddy.Core/Lift.Buddy.Core.csproj", "Lift.Buddy.Core/"]
RUN dotnet restore "Lift.Buddy.Api/Lift.Buddy.API.csproj"
COPY . .
WORKDIR "/src/Lift.Buddy.Api"
RUN dotnet build "Lift.Buddy.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lift.Buddy.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
ENV ASPNETCORE_URLS http://+:5200
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lift.Buddy.API.dll"]