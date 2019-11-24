FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster-arm32v7 AS build
WORKDIR /src
COPY ["ChatBot/ChatBot.csproj", "ChatBot/"]
RUN dotnet restore "ChatBot/ChatBot.csproj"
COPY . .
WORKDIR "/src/ChatBot"
RUN dotnet build "ChatBot.csproj" -c Release -o /app/build -r linux-arm

FROM build AS publish
RUN dotnet publish "ChatBot.csproj" -c Release -o /app/publish -r linux-arm

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ["dotnet", "ChatBot.dll"]