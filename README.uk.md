<div align="center">

[🇬🇧 English](README.md) | [🇨🇳 简体中文](README.zh-CN.md) | [🇪🇸 Español](README.es.md) | [🇧🇷 Português](README.pt-BR.md) | [🇩🇪 Deutsch](README.de.md) | [🇫🇷 Français](README.fr.md) | [🇯🇵 日本語](README.ja.md) | [🇰🇷 한국어](README.ko.md) | [🇷🇺 Русский](README.ru.md) | **🇺🇦 Українська**

<img src="docs/logo.png" width="112" alt="Логотип Awake">

# Awake — standalone

**Не дає Windows заснути, керування з трею. Без PowerToys, без телеметрії, лише один `.exe`.**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**Завантажити**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[Можливості](#-можливості) ·
[Використання](#-використання) ·
[Командний рядок](#-командний-рядок) ·
[Збирання](#-збирання-з-вихідного-коду)

</div>

---

Awake — невелика утиліта з [Microsoft PowerToys](https://github.com/microsoft/PowerToys), яка не
дає Windows перейти в режим сну: під час довгого завантаження, рендерингу чи презентації.
У PowerToys вона працює лише разом з усім пакетом. **Цей форк робить її самостійною програмою**:
завантажте один файл, запустіть, і все керування — через значок у треї.

## ✨ Можливості

|   |   |
|---|---|
| 🪶 **Автономність** | Один портативний `Awake.exe`. Без інсталятора, PowerToys і .NET. |
| 🔒 **Без телеметрії** | Уся телеметрія PowerToys і події ETW видалені з коду. |
| 🖱️ **Меню в треї** | Режими, екран, таймери — у два кліки. |
| ⏱️ **Таймери** | 30 хв / 1 год / 2 год або власні варіанти. |
| 🖥️ **Екран** | Не давати заснути лише системі або ще й екрану. |
| 🚀 **Автозапуск** | Перемикач запуску разом із Windows прямо в меню. |
| 💾 **Пам'ятає режим** | Останній режим відновлюється під час наступного запуску. |
| 🌍 **Мови** | Інтерфейс англійською та російською, обирається за мовою Windows. |
| ⌨️ **CLI** | Усі параметри командного рядка оригіналу працюють. |

## 🚀 Використання

1. [Завантажте `Awake.exe`](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe).
2. Покладіть його в будь-яку папку (наприклад, `C:\Tools\Awake`) і запустіть.
3. Натисніть на значок ☕ у треї та оберіть режим.

> [!NOTE]
> Інтерфейс програми доступний англійською та російською мовами.

> [!TIP]
> Під час першого запуску SmartScreen може попередити про невідому програму: файл не підписаний
> сертифікатом. Натисніть **Докладніше → Усе одно запустити** або зберіть програму самостійно.

### Режими

| Режим | Що робить |
|-------|-----------|
| **Вимк.** | Працює звичайна схема живлення |
| **Безстроково** | Не засинає, доки ви не вимкнете |
| **Протягом часу** | Не засинає обраний час, потім вимикається |
| **До дати й часу** | Не засинає до заданого моменту (у файлі налаштувань або через `-e`) |

### Файл налаштувань

Налаштування зберігаються в `%LOCALAPPDATA%\Awake\settings.json` (меню → **Open settings file**).
Зміни застосовуються одразу. Власні таймери задаються в секундах:

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 хвилин": 900, "1 година": 3600, "3 години": 10800 }
  }
}
```

`mode`: `0` — вимк., `1` — безстроково, `2` — за таймером, `3` — до дати.
Журнали пишуться в `%LOCALAPPDATA%\Awake\Logs` і зберігаються 14 днів.

## 💻 Командний рядок

Без аргументів запускається програма в треї. З аргументами — разовий сеанс:

| Параметр | Опис |
|----------|------|
| `-c`, `--use-config` | Режим трею з файлом налаштувань (за замовчуванням) |
| `-d`, `--display-on` | Також не вимикати екран |
| `-t`, `--time-limit <секунди>` | Не засинати N секунд, потім вийти |
| `-e`, `--expire-at <дата час>` | Не засинати до вказаного моменту |
| `-p`, `--pid <pid>` | Не засинати, доки працює процес |
| `-u`, `--use-parent-pid` | Не засинати, доки працює батьківський процес |

```powershell
Awake.exe -t 3600 -d                 # 1 година, екран увімкнено
Awake.exe -e "2026-12-31 18:00"      # до заданого моменту
Awake.exe -p 1234                    # доки працює процес 1234
```

## 🔍 Як це працює

Awake викликає Win32 API
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)
із прапорцем `ES_SYSTEM_REQUIRED` (і `ES_DISPLAY_REQUIRED`, якщо екран теж має світитися).
Схема живлення не змінюється, і все повертається як було, щойно Awake вимкнено або закрито.
Перевірити можна командою `powercfg /requests`.

## 🔁 Відмінності від PowerToys Awake

| | PowerToys Awake | Цей форк |
|---|:---:|:---:|
| Потрібен встановлений PowerToys | ✅ | ❌ |
| Телеметрія | ✅ | ❌ |
| Меню в треї без PowerToys | ❌ | ✅ |
| Автозапуск | через PowerToys | ✅ у меню |
| Один портативний exe | ❌ | ✅ |

## 🔧 Збирання з вихідного коду

Потрібен [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

Результат — `publish/win-x64/Awake.exe`. Після надсилання тегу на кшталт `v1.2.3` реліз на
GitHub збирається й публікується автоматично.

## 🤝 Участь

Повідомлення про помилки та pull request'и вітаються. Для великих змін спочатку створіть
[issue](https://github.com/varsoug/awake-standalone/issues). Виправлення перекладу теж вітаються.

## 📄 Ліцензія та автори

Awake створив [Den Delimarsky](https://den.dev) у складі
[Microsoft PowerToys](https://github.com/microsoft/PowerToys).
Форк поширюється за тією ж [ліцензією MIT](LICENSE) і не пов'язаний із Microsoft.

<div align="center">

Якщо утиліта врятувала ваше завантаження від сплячого ПК — поставте ⭐

</div>
