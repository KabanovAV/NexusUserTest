# NexusUserTest

**NexusUserTest** — веб-приложение для организации тестирования пользователей. Проект включает пользовательский интерфейс, административную часть и серверное API. Архитектура разделяет предметную область, прикладную логику, инфраструктуру и представление.

## ✨ Основные возможности

- Управление пользователями.
- Управление группами и их участниками.
- Работа со специализациями.
- Управление темами тестирования.
- Создание и редактирование вопросов и ответов.
- Настройка параметров тестирования.
- Разделение пользовательского и административного интерфейсов.
- Обработка запросов через Web API.
- Аутентификация на основе JWT.

## 🛠 Технологии

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?logo=blazor&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity_Framework_Core-512BD4?logo=dotnet&logoColor=white)

- **Backend:** ASP.NET Core Web API
- **Frontend:** Blazor
- **База данных:** PostgreSQL
- **Доступ к данным:** Entity Framework Core
- **Авторизация:** JWT

## 🏗 Архитектура проекта

Решение `NexusUserTest.sln` состоит из нескольких проектов:

```text
src/
├── Client/
│   ├── NexusUserTest.Client/         # Веб-приложение
│   ├── NexusUserTest.Admin/          # Административный интерфейс
│   ├── NexusUserTest.User/           # Пользовательский интерфейс
│   └── NexusUserTest.Shared/         # Общие компоненты и сервисы
│
├── Server/
│   ├── NexusUserTest.WebApi/         # Серверное Web API
│   ├── NexusUserTest.Application/    # Прикладная логика
│   ├── NexusUserTest.Domain/         # Предметная область
│   └── NexusUserTest.Infrastructure/ # Работа с данными и инфраструктура
│
└── NexusUserTest.Common/             # Общие типы и компоненты
```

## 🚀 Запуск проекта

### 1. Клонирование репозитория

```bash
git clone https://github.com/KabanovAV/NexusUserTest.git
cd NexusUserTest
```

### 2. Восстановление зависимостей и сборка

```bash
dotnet restore NexusUserTest.sln
dotnet build NexusUserTest.sln
```

### 3. Настройка конфигурации

Настройте подключение к PostgreSQL и параметры JWT в файлах `appsettings.json`.

### 4. Запуск Web API

```bash
dotnet run --project src/Server/NexusUserTest.WebApi/NexusUserTest.WebApi.csproj
```

### 5. Запуск клиентского приложения

В отдельном терминале выполните:

```bash
dotnet run --project src/Client/NexusUserTest.Client/NexusUserTest.Client.csproj
```

---

⭐ *Проект создан для практики и развития навыков разработки на .NET.*
