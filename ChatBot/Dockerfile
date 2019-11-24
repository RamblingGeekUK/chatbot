FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS base
WORKDIR /build

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

#copy & compile the source
COPY . ./
RUN dotnet publish -c Release -o out -r linux-arm

# use the arm version of SDK to run (on the pi)
FROM mcr.microsoft.com/dotnet/core/sdk:3.0.101-buster-arm32v7
WORKDIR /build
COPY --from=build /build/out/ .

CMD ["dotnet", "ChatBot.dll"]