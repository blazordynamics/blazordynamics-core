#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["samples/ServerSideSampleApp/ServerSideSampleApp.csproj", "samples/ServerSideSampleApp/"]
COPY ["src/BlazorDynamics/BlazorDynamics.csproj", "src/BlazorDynamics/"]
COPY ["src/BlazorDynamics.Extensions/BlazorDynamics.Extensions.csproj", "src/BlazorDynamics.Extensions/"]
COPY ["src/BlazorDynamics.Core/BlazorDynamics.Core.csproj", "src/BlazorDynamics.Core/"]
COPY ["src/BlazorDynamics.DynamicUI.JsonSchema/BlazorDynamics.DynamicUI.JsonSchema.csproj", "src/BlazorDynamics.DynamicUI.JsonSchema/"]
COPY ["src/BlazorDynamics.Contracts/BlazorDynamics.Contracts.csproj", "src/BlazorDynamics.Contracts/"]
COPY ["src/BlazorDynamics.Common/BlazorDynamics.Common.csproj", "src/BlazorDynamics.Common/"]
COPY ["src/BlazorDynamics.UISchema/BlazorDynamics.UISchema.csproj", "src/BlazorDynamics.UISchema/"]
COPY ["src/security/BlazorDynamics.Licensing/BlazorDynamics.Licensing.csproj", "src/security/BlazorDynamics.Licensing/"]
COPY ["src/security/BlazorDynamics.Licensing.Core/BlazorDynamics.Licensing.Core.csproj", "src/security/BlazorDynamics.Licensing.Core/"]
COPY ["samples/SharedDemos/SharedDemos.csproj", "samples/SharedDemos/"]
COPY ["src/BlazorDynamics.Components/BlazorDynamics.Forms.Components.csproj", "src/BlazorDynamics.Components/"]
COPY ["src/BlazorDynamics.Components.Commons/BlazorDynamics.Forms.Commons.csproj", "src/BlazorDynamics.Components.Commons/"]
COPY ["src/BlazorDynamics.Forms.Actions/BlazorDynamics.Forms.Actions.csproj", "src/BlazorDynamics.Forms.Actions/"]
COPY ["src/implementations/BlazorDynamics.HTMLComponents/BlazorDynamics.HTMLComponents.csproj", "src/implementations/BlazorDynamics.HTMLComponents/"]
COPY ["src/implementations/BlazorDynamics.MudBlazorComponents/BlazorDynamics.MudBlazorComponents.csproj", "src/implementations/BlazorDynamics.MudBlazorComponents/"]
COPY ["src/implementations/BlazorDynamics.RadzenComponents/BlazorDynamics.RadzenComponents.csproj", "src/implementations/BlazorDynamics.RadzenComponents/"]
RUN dotnet restore "./samples/ServerSideSampleApp/./ServerSideSampleApp.csproj"
COPY . .
WORKDIR "/src/samples/ServerSideSampleApp"
RUN dotnet build "./ServerSideSampleApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ServerSideSampleApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerSideSampleApp.dll"]