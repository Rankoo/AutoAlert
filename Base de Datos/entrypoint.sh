#!/bin/bash
set -e
/opt/mssql/bin/sqlservr &

SA_USER="${SA_USER:-sa}"
SA_PASSWORD="${SA_PASSWORD:?SA_PASSWORD no definido}"
SCRIPT_PATH="${SCRIPT_PATH:-/usr/src/app/scriptAutoAlertDB.sql}"
retries=0

until /opt/mssql-tools/bin/sqlcmd -S localhost -U "$SA_USER" -P "$SA_PASSWORD" -Q "SELECT 1" >/dev/null 2>&1
do
  sleep 2
  retries=$((retries+1))
  if [ $retries -gt 60 ]; then
    echo "SQL Server no arrancó a tiempo"
    exit 1
  fi
done

echo "Ejecutando script $SCRIPT_PATH"
/opt/mssql-tools/bin/sqlcmd -S localhost -U "$SA_USER" -P "$SA_PASSWORD" -i "$SCRIPT_PATH"

wait