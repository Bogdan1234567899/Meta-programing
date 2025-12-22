# CW06 (мінімальний рівень)

Це рішення містить **мінімальний рівень** для всіх 5 завдань з PDF.

## Як запустити
```bash
dotnet run --project src/CW06
```

## Як запустити тести (обов'язково для Завдання 2)
```bash
dotnet test
```

## Зміст
- **Завдання 1:** `ConfigParser.ParseSetting(string line)` — парсинг `key=value` з коректними винятками.
- **Завдання 2:** `OverflowMath.AddChecked/AddWrapped` + 4 unit-тести на переповнення.
- **Завдання 3:** `TempFileWriter : IDisposable` — запис у тимчасовий файл і `ObjectDisposedException` після `Dispose()`.
- **Завдання 4:** `TextTransforms.Transform` + стратегії `TrimToUpper`, `MaskDigits`.
- **Завдання 5:** `Counter` з подією `Changed` і двома підписками в демо.

> Примітка: для завдання 2 у вимогах мінімального рівня прямо вказано 3–4 unit-тести, тому вони включені.
