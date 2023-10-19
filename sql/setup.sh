# Wait for SQL Server to be started and then run the sql script
./wait-for-it.sh sqlserver:1433 --timeout=0 --strict -- sleep 5s && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabaseOrders.sql -U sa -P "$SA_PASSWORD" && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabaseWarehouse.sql -U sa -P "$SA_PASSWORD" && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabaseShopping.sql -U sa -P "$SA_PASSWORD" && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabaseCatalogue.sql -U sa -P "$SA_PASSWORD" && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabaseAdmin.sql -U sa -P "$SA_PASSWORD"
