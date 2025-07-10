# Используем официальный образ Node.js
FROM node:20 as build

# Устанавливаем рабочую директорию
WORKDIR /app

# Устанавливаем Angular CLI глобально
RUN npm install -g @angular/cli

# Копируем файлы package.json и package-lock.json
COPY client/package*.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем исходный код приложения
COPY client/ ./

# Проверяем содержимое директории
RUN ls -la

# Собираем приложение
RUN ng build

# Проверяем результат сборки
RUN ls -la dist/

# Используем nginx для раздачи статических файлов
FROM nginx:alpine

# Копируем собранное приложение
COPY --from=build /app/dist/client/browser/ /usr/share/nginx/html/

# Удаляем стандартный index.html от nginx
RUN rm -f /usr/share/nginx/html/index.html && \
    if [ -f /usr/share/nginx/html/index.csr.html ]; then \
    mv /usr/share/nginx/html/index.csr.html /usr/share/nginx/html/index.html; \
    fi

# Копируем конфигурацию nginx
COPY client/nginx.conf /etc/nginx/conf.d/default.conf

# Проверяем содержимое директории
RUN ls -la /usr/share/nginx/html/

# Открываем порт
EXPOSE 80

# Запускаем nginx
CMD ["nginx", "-g", "daemon off;"] 