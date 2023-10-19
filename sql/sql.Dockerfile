FROM mcr.microsoft.com/mssql/server:2017-latest 

ARG PROJECT_DIR=/tmp/devdatabase
RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
COPY sql/InitializeDatabaseOrders.sql ./
COPY sql/InitializeDatabaseCatalogue.sql ./
COPY sql/InitializeDatabaseShopping.sql ./
COPY sql/InitializeDatabaseWarehouse.sql ./
COPY sql/InitializeDatabaseAdmin.sql ./
COPY sql/wait-for-it.sh ./
COPY sql/entrypoint.sh ./
COPY sql/setup.sh ./

CMD ["/bin/bash", "entrypoint.sh"]