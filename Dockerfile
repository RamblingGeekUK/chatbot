FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /build

COPY ["ChatBot/ChatBot.csproj", "ChatBot/"]
RUN dotnet restore "ChatBot/ChatBot.csproj"

COPY . ./
RUN dotnet publish -c Release -o out -r linux-arm

FROM mcr.microsoft.com/dotnet/core/sdk:2.1.603-stretch-arm32v7
WORKDIR /build
COPY --from=build /build/out/ .

CMD ["dotnet", "ChatBot.dll"]