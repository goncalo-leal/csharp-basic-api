FROM mcr.microsoft.com/mssql/server:2019-latest

WORKDIR /usr/src/app

COPY . /usr/src/app

ARG ACCEPT_EULA
ARG MSSQL_USER
ARG MSSQL_SA_PASSWORD
ARG MSSQL_PID

EXPOSE 1433

CMD /bin/bash ./entrypoint.sh