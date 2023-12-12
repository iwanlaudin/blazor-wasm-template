FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["src/BlazorWasmTemplate.Application/BlazorWasmTemplate.Application.csproj", "BlazorWasmTemplate.Application/"]
COPY ["src/BlazorWasmTemplate.Components/BlazorWasmTemplate.Components.csproj", "BlazorWasmTemplate.Components/"]
COPY ["src/BlazorWasmTemplate.Client/BlazorWasmTemplate.Client.csproj", "BlazorWasmTemplate.Client/"]

RUN dotnet restore "BlazorWasmTemplate.Client/BlazorWasmTemplate.Client.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "src/BlazorWasmTemplate.Client/BlazorWasmTemplate.Client.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "src/BlazorWasmTemplate.Client/BlazorWasmTemplate.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BlazorWasmTemplate.Client.dll"]