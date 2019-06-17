FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS builder

LABEL "com.github.actions.name"="Build & Deploy To Liara"
LABEL "com.github.actions.description"="just builds and deploys to liara.ir"
LABEL "com.github.actions.icon"="mic"
LABEL "com.github.actions.color"="purple"

LABEL "repository"="http://github.com/octocat/hello-world"
LABEL "homepage"="http://github.com/actions"
LABEL "maintainer"="Octocat <octocat@github.com>"

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    curl -sL https://deb.nodesource.com/setup_12.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

# Setup Liara
RUN npm i -g @liara/cli
# End Setup

RUN echo Env Vars
RUN printenv

COPY . /src

WORKDIR /src/Web/ClientApp

RUN npm i

WORKDIR /src/Web

RUN mkdir /build

RUN dotnet publish -c Release -o /build

WORKDIR /build

RUN ls

ENTRYPOINT [ "entrypoint.sh" ]

