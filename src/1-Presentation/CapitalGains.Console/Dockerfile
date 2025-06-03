#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/1-Presentation/CapitalGains.Console/CapitalGains.Console.csproj", "src/1-Presentation/CapitalGains.Console/"]
COPY ["src/4-Infrastructure/CapitalGains.CrossCutting/CapitalGains.CrossCutting.csproj", "src/4-Infrastructure/CapitalGains.CrossCutting/"]
COPY ["src/3-Core/CapitalGains.Domain/CapitalGains.Domain.csproj", "src/3-Core/CapitalGains.Domain/"]
COPY ["src/2-Application/CapitalGains.Application/CapitalGains.Application.csproj", "src/2-Application/CapitalGains.Application/"]
RUN dotnet restore "src/1-Presentation/CapitalGains.Console/CapitalGains.Console.csproj"
COPY . .
WORKDIR "/src/src/1-Presentation/CapitalGains.Console"
RUN dotnet build "CapitalGains.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CapitalGains.Console.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CapitalGains.Console.dll"]
CMD ["/bin/bash"]