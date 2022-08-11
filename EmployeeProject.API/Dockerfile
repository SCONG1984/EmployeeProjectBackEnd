#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EmployeeProject.API/EmployeeProject.API.csproj", "EmployeeProject.API/"]
COPY ["EmployeeProject.Application.Service/EmployeeProject.Application.Service.csproj", "EmployeeProject.Application.Service/"]
COPY ["EmployeeProject.Domain.Entity/EmployeeProject.Domain.Entity.csproj", "EmployeeProject.Domain.Entity/"]
COPY ["EmployeeProject.Domain.Repository/EmployeeProject.Domain.Repository.csproj", "EmployeeProject.Domain.Repository/"]
COPY ["EmployeeProject.Infrastructure.ExcelRepository/EmployeeProject.Infrastructure.ExcelRepository.csproj", "EmployeeProject.Infrastructure.ExcelRepository/"]
COPY ["EmployeeProject.Application.ExcelReader/EmployeeProject.Application.ExcelReader.csproj", "EmployeeProject.Application.ExcelReader/"]
RUN dotnet restore "EmployeeProject.API/EmployeeProject.API.csproj"
COPY . .
WORKDIR "/src/EmployeeProject.API"
RUN dotnet build "EmployeeProject.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeProject.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeProject.API.dll"]