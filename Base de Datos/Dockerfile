FROM mcr.microsoft.com/mssql/server:2019-latest
USER root
WORKDIR /usr/src/app

RUN apt-get update && apt-get install -y curl apt-transport-https gnupg2 ca-certificates lsb-release && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && apt-get update && ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools unixodbc-dev && ln -s /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd && apt-get clean && rm -rf /var/lib/apt/lists/*

COPY entrypoint.sh /usr/src/app/entrypoint.sh
COPY scriptAutoAlertDB.sql /usr/src/app/scriptAutoAlertDB.sql
RUN chmod +x /usr/src/app/entrypoint.sh

USER mssql
EXPOSE 1433

CMD ["/usr/src/app/entrypoint.sh"]