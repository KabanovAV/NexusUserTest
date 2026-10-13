# NexusUserTest

**Веб-приложение для организации тестирования пользователей.**

Проект позволяет управлять пользователями, группами, темами, вопросами и настройками тестирования.

Основные возможности

Управление пользователями.

Управление группами и их участниками.

Работа со специализациями.

Управление темами тестирования.

Создание и редактирование вопросов и ответов.

Настройка параметров тестирования.

Разделение пользовательского интерфейса и административной части.

Серверная обработка запросов через Web API.

Аутентификация на основе JWT.

## 🛠 Технологии

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?logo=blazor&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-512BD4?logo=dotnet&logoColor=white)

- **Backend:** ASP.NET Core Web API
- **Frontend:** Blazor
- **База данных:** PostgreSQL
- **Доступ к данным:** Entity Framework Core
- **Авторизация:** JWT

Архитектура проекта

Решение NexusUserTest.sln организовано в несколько проектов.

src/
├── Client/
│   ├── NexusUserTest.Client/       # Веб-приложение и точка входа
│   ├── NexusUserTest.Admin/        # Административный интерфейс
│   ├── NexusUserTest.User/         # Пользовательский интерфейс
│   └── NexusUserTest.Shared/       # Общие компоненты и клиентские сервисы
│
├── Server/
│   ├── NexusUserTest.WebApi/       # Серверное Web API
│   ├── NexusUserTest.Application/  # Прикладная логика и сервисы
│   ├── NexusUserTest.Domain/       # Предметная область
│   └── NexusUserTest.Infrastructure/ # База данных и инфраструктура
│
└── NexusUserTest.Common/           # Общие типы и компоненты

## 🚀 Запуск проекта

**1. Клонируйте репозиторий**

```bash
git clone https://github.com/KabanovAV/NexusUserTest.git
cd NexusUserTest
```

**2. Восстановите зависимости и соберите проект**

```bash
dotnet restore NexusUserTest.sln
dotnet build NexusUserTest.sln
```

**3. Настройте подключение к PostgreSQL и параметры JWT** в файлах конфигурации `appsettings.json`.

**4. Запустите Web API**

```bash
dotnet run --project src/Server/NexusUserTest.WebApi/NexusUserTest.WebApi.csproj
```

**5. Запустите клиентское приложение** в отдельном терминале

```bash
dotnet run --project src/Client/NexusUserTest.Client/NexusUserTest.Client.csproj
```

## 📁 Структура проекта

- `src/Client` — клиентское приложение и интерфейсы
- `src/Server` — Web API, бизнес-логика и работа с данными
- `src/NexusUserTest.Common` — общие компоненты

---

⭐ *Проект создан для практики и развития навыков разработки на .NET.*
