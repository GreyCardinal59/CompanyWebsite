# Скрипты по ТЗ

## Как использовать

### Через Docker

```powershell
# Копирование скрипта в контейнер
docker cp sql-scripts-docker.sql companywebsite-db-1:/tmp/

# Подключение к контейнеру и выполнение скрипта
docker exec -it companywebsite-db-1 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd -d company_website -i /tmp/sql-scripts-docker.sql -C -y 0
```

### Для локального запуска (SSMS)

1. Откройте SQL Server Management Studio
2. Подключитесь к вашему серверу SQL Server
3. Откройте файл `sql-scripts.sql`
4. Выделите нужный скрипт и нажмите F5 для выполнения