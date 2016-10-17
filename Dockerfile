FROM microsoft/dotnet::1.0.0-preview2-sdk
WORKDIR /usr/src/BowlingGame
COPY . /usr/src/BowlingGame
RUN dotnet restore