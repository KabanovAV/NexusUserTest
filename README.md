NexusUserTest
NexusUserTest — веб-приложение для организации тестирования пользователей. Проект включает пользовательский интерфейс, административную часть и серверное API. Архитектура разделяет предметную область, прикладную логику, инфраструктуру и представление.
Основные возможности
•	Управление пользователями.
•	Управление группами и их участниками.
•	Работа со специализациями.
•	Управление темами тестирования.
•	Создание и редактирование вопросов и ответов.
•	Настройка параметров тестирования.
•	Разделение пользовательского интерфейса и административной части.
•	Серверная обработка запросов через Web API.
•	Аутентификация на основе JWT.
🛠 Технологии



•	Backend: ASP.NET Core Web API
•	Frontend: Blazor
•	База данных: PostgreSQL
•	Доступ к данным: Entity Framework Core
•	Авторизация: JWT
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

🚀 Запуск проекта
1. Клонируйте репозиторий
git clone https://github.com/KabanovAV/NexusUserTest.git
cd NexusUserTest
2. Восстановите зависимости и соберите проект
dotnet restore NexusUserTest.sln
dotnet build NexusUserTest.sln
3. Настройте подключение к PostgreSQL и параметры JWT в файлах конфигурации appsettings.json.
4. Запустите Web API
dotnet run --project src/Server/NexusUserTest.WebApi/NexusUserTest.WebApi.csproj
5. Запустите клиентское приложение в отдельном терминале
dotnet run --project src/Client/NexusUserTest.Client/NexusUserTest.Client.csproj
📁 Структура проекта
•	src/Client — клиентское приложение и интерфейсы
•	src/Server — Web API, бизнес-логика и работа с данными
•	src/NexusUserTest.Common — общие компоненты
________________________________________
⭐ Проект создан для практики и развития навыков разработки на .NET.

