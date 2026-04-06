CampusAlerts

Система сповіщень для університетського кампусу. Події від датчиків відправляються потрібним ролям через Email, SMS і Console.

Як запустити

Потрібен .NET 8. Відкрити CampusAlerts.csproj в Visual Studio і натиснути F5.
Або в терміналі: dotnet run

Файли

CampusAlerts/ — сам проект
  AlertEvent.cs — клас події
  Interfaces.cs — інтерфейси
  AlertRouter.cs — головний клас
  Implementations.cs — реалізації (канали, логер, форматер)
  Program.cs — точка входу, тут створюються всі об'єкти

ARCHITECTURE.md — опис архітектури
DEPENDENCY_GRAPH.mmd — схема залежностей
RUN_RESULTS.md — що виводиться в консоль
README.md — цей файл


