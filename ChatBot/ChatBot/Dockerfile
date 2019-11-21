FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

ENV Twitch_botusername=jonny5bot
ENV Twitch_channel=RamblingGeek
ENV Twitch_ID=1zjxpor2hcsvxumid29zcroddmbiq7
ENV Twitch_OAuth=65mq2z6e3buntpbre0ipsv2htrm6yhjvc2fqjcaudpugc063t3
ENV Twitch_token=1zkwk2c2xuxicjvl3m324gf4zxzu0g

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ChatBot/ChatBot.csproj", "ChatBot/"]
RUN dotnet restore "ChatBot/ChatBot.csproj"
COPY . .
WORKDIR "/src/ChatBot"
RUN dotnet build "ChatBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatBot.dll"]