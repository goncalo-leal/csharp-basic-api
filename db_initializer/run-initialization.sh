# Wait to be sure that SQL Server came up
sleep 60s

# Run the setup script to create the DB and the schema in the DB
# -S server | -U user | -P password | -d database name | -i input file | -Q "cmdline query"
/opt/mssql-tools/bin/sqlcmd -S localhost -U ${MSSQL_USER} -P ${MSSQL_SA_PASSWORD} -d master -i create-database.sql