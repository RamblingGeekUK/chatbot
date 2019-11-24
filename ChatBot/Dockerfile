FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /build

COPY ["ChatBot/ChatBot.csproj", "ChatBot/"]
RUN dotnet restore "ChatBot/ChatBot.csproj"

COPY . ./
RUN dotnet publish -c Release -o out -r linux-arm

ENV Twitch_botusername=jonny5bot
ENV Twitch_channel=RamblingGeek
ENV Twitch_ID=1zjxpor2hcsvxumid29zcroddmbiq7
ENV Twitch_OAuth=65mq2z6e3buntpbre0ipsv2htrm6yhjvc2fqjcaudpugc063t3
ENV Twitch_token=1zkwk2c2xuxicjvl3m324gf4zxzu0g

FROM mcr.microsoft.com/dotnet/core/sdk:2.1.603-stretch-arm32v7
WORKDIR /build
COPY --from=build /build/out/ .

CMD ["dotnet", "ChatBot.dll"]