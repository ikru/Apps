# Useful commands (Run from src folder)

# Build run
# docker build --rm -t interview:0.1 .; docker rm -f interview; docker run --env ASPNETCORE_ENVIRONMENT=Development --rm -p 8080:80 --name interview interview:0.1
# docker build --rm -t interview:0.1 .
# docker rm -f interview
# docker run --env ASPNETCORE_ENVIRONMENT=Development --rm -p 8080:80 --name interview interview:0.1

# Tests
# docker build --target test -t interview:test .; docker run --rm --name interview_test interview:test
# docker build --target test -t interview:test .

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Interview/*.csproj ./
RUN DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0 dotnet restore

# Copy everything else and build
COPY Interview ./
RUN dotnet publish -c Debug -o out

FROM build AS test
RUN apt update && \
    apt -y install unzip && \
    apt -y install procps && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg
WORKDIR /tests
COPY ./ ./
RUN DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0 dotnet build Interview.Tests/Interview.Tests.csproj
ENTRYPOINT ["dotnet", "test", "Interview.Tests/Interview.Tests.csproj"]

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
RUN apt update && \
    apt -y install unzip && \
    apt -y install procps && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Interview.dll"]