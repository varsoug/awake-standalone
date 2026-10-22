<div align="center">

<img src="docs/logo.png" width="112" alt="Логотип Awake">

# Awake — standalone

**Не даёт Windows уснуть, управление из трея. Без PowerToys, без телеметрии, один `.exe`.**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)

[**Скачать**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[Возможности](#-возможности) ·
[Использование](#-использование) ·
[Командная строка](#-командная-строка) ·
[Сборка](#-сборка-из-исходников) ·
[English](README.md)

</div>

---

Awake — небольшая утилита из [Microsoft PowerToys](https://github.com/microsoft/PowerToys), которая
не даёт Windows уйти в сон: во время долгой загрузки, рендера или презентации. В PowerToys она
работает только вместе со всем пакетом. **Этот форк делает её самостоятельной программой**:
скачал один файл, запустил, всё управление — через значок в трее.

## ✨ Возможности

|   |   |
|---|---|
| 🪶 **Автономность** | Один переносимый `Awake.exe`. Без установщика, PowerToys и .NET. |
| 🔒 **Без телеметрии** | Вся телеметрия PowerToys и события ETW удалены из кода. |
| 🖱️ **Меню в трее** | Режимы, экран, таймеры — в два клика. |
| ⏱️ **Таймеры** | 30 мин / 1 ч / 2 ч или свои варианты. |
| 🖥️ **Экран** | Не давать уснуть только системе или ещё и экрану. |
| 🚀 **Автозапуск** | Переключатель «Запускать вместе с Windows» прямо в меню. |
| 💾 **Помнит режим** | Последний режим восстанавливается при следующем запуске. |
| 🌍 **Русский язык** | Выбирается автоматически по языку Windows. |
| ⌨️ **CLI** | Все параметры командной строки оригинала работают. |

## 🚀 Использование

1. [Скачай `Awake.exe`](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe).
2. Положи в любую папку (например, `C:\Tools\Awake`) и запусти.
3. Нажми на значок ☕ в трее и выбери режим.

> [!TIP]
> При первом запуске SmartScreen может предупредить о неизвестном приложении: файл не подписан
> сертификатом. Нажми **Подробнее → Выполнить в любом случае** или собери программу сам.

### Режимы

| Режим | Что делает |
|-------|------------|
| **Выкл.** | Работает обычная схема электропитания |
| **Бессрочно** | Не засыпает, пока не выключишь |
| **В течение…** | Не засыпает выбранное время, затем выключается |
| **До даты и времени** | Не засыпает до заданного момента (в файле настроек или через `-e`) |

### Файл настроек

Настройки лежат в `%LOCALAPPDATA%\Awake\settings.json` (меню → **Открыть файл настроек**).
Изменения применяются сразу. Свои таймеры задаются в секундах:

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 минут": 900, "1 час": 3600, "3 часа": 10800 }
  }
}
```

`mode`: `0` — выкл., `1` — бессрочно, `2` — по таймеру, `3` — до даты.
Логи пишутся в `%LOCALAPPDATA%\Awake\Logs` и хранятся 14 дней.

## ⌨️ Командная строка

Без аргументов запускается приложение в трее. С аргументами — разовый сеанс:

| Параметр | Описание |
|----------|----------|
| `-c`, `--use-config` | Режим трея с файлом настроек (по умолчанию) |
| `-d`, `--display-on` | Не выключать также экран |
| `-t`, `--time-limit <секунды>` | Не засыпать N секунд, затем выйти |
| `-e`, `--expire-at <дата время>` | Не засыпать до указанного момента |
| `-p`, `--pid <pid>` | Не засыпать, пока работает процесс |
| `-u`, `--use-parent-pid` | Не засыпать, пока работает родительский процесс |

```powershell
Awake.exe -t 3600 -d                 # 1 час, экран включён
Awake.exe -e "2026-12-31 18:00"      # до заданного момента
Awake.exe -p 1234                    # пока жив процесс 1234
```

## ⚙️ Как это работает

Awake вызывает Win32 API
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)
с флагом `ES_SYSTEM_REQUIRED` (и `ES_DISPLAY_REQUIRED`, если экран тоже должен гореть).
Схема электропитания не меняется, и всё возвращается как было, как только Awake выключен или
закрыт. Проверить можно командой `powercfg /requests`.

## 🛠️ Сборка из исходников

Нужен [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

## 📄 Лицензия и авторы

Awake создал [Den Delimarsky](https://den.dev) в составе
[Microsoft PowerToys](https://github.com/microsoft/PowerToys).
Форк распространяется под той же [лицензией MIT](LICENSE) и не связан с Microsoft.

<div align="center">

Если утилита спасла твою загрузку от уснувшего ПК — поставь ⭐

</div>
