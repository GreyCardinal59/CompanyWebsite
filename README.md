# Тестовое задание в компанию МирТех


## Технологии
- **Backend**: ASP.NET Core, SQL Server
- **Frontend**: Angular, Bootstrap

## Быстрый запуск для проверки (Docker)

1. Клонировать репозиторий:
```
git clone https://github.com/GreyCardinal59/CompanyWebsite.git
cd CompanyWebsite
```

2. Запустить с помощью Docker Compose:
```
docker-compose up -d
```

3. Открыть в браузере: [http://localhost:8080](http://localhost:8080)

При запуске
- Создается база данных SQL Server
- Применяются миграции и сидирование
- Запуск бэка
- Запуск фронта

## Если вдруг нужен локальный запуск (господи помилуй)

### 1. Клонирование репозитория
```
git clone https://github.com/your-username/CompanyWebsite.git
cd CompanyWebsite
```

### 2. Настройка базы данных
1. Откройте файл `src/CompanyWebsite.Web/appsettings.json`
2. Измените строку подключения `DefaultConnection` на вашу:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=company_website;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
}
```
### 3. Запуск Backend
```
cd src/CompanyWebsite.Web
dotnet run
```
API будет доступно по адресу: http://localhost:5296

### 4. Запуск Frontend
```
cd client
npm install
npm start
```
Клиентское приложение будет доступно по адресу: http://localhost:4200


## Структура проекта
- `CompanyWebsite.Application` - бизнес-логика приложения
- `CompanyWebsite.Contracts` - DTO и контракты API
- `CompanyWebsite.Domain` - доменные модели
- `CompanyWebsite.Infrastructure.Mssql` - инфраструктурный слой (работа с БД)
- `CompanyWebsite.Presenters` - контроллеры API
- `CompanyWebsite.Web` - "точка входа"
- `client` - Angular клиент 

## SQL-скрипты

[Как запустить скрипты](https://github.com/GreyCardinal59/CompanyWebsite/blob/main/CompanyWebsite/sql-scripts-readme.md)
