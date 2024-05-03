# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:7.0
ARG DEBIAN_FRONTEND=noninteractive
RUN apt-get update -y
RUN apt-get install -y make

#Project dependencies
#Important: Nuget needs the project upfront so required dependencies can be loaded into the project. Only shared volume does not work in this case
COPY ./ ./icalfilter
WORKDIR ./icalfilter

#RUN mv config /config
RUN ls -a

CMD ["make"]