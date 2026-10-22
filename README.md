<div align="center">

**🇬🇧 English** | [🇨🇳 简体中文](README.zh-CN.md) | [🇪🇸 Español](README.es.md) | [🇧🇷 Português](README.pt-BR.md) | [🇩🇪 Deutsch](README.de.md) | [🇫🇷 Français](README.fr.md) | [🇯🇵 日本語](README.ja.md) | [🇰🇷 한국어](README.ko.md) | [🇷🇺 Русский](README.ru.md) | [🇺🇦 Українська](README.uk.md)

<img src="docs/logo.png" width="112" alt="Awake logo">

# Awake — standalone

**Keep your Windows PC awake from the system tray. No PowerToys, no telemetry, just one `.exe`.**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**Download**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[Features](#-features) ·
[Usage](#-usage) ·
[Command line](#-command-line) ·
[Building](#-building-from-source)

</div>

---

Awake is a small tool from [Microsoft PowerToys](https://github.com/microsoft/PowerToys) that stops
Windows from going to sleep, for example during a long download, a render or a presentation.
In PowerToys it only works together with the rest of the suite. **This fork makes it a
self-contained app**: download one file, run it, and control everything from the tray icon.

## ✨ Features

|   |   |
|---|---|
| 🪶 **Standalone** | One portable `Awake.exe`. No installer, no PowerToys, no .NET runtime needed. |
| 🔒 **No telemetry** | All PowerToys telemetry and ETW events are removed from the code. |
| 🖱️ **Tray menu** | Switch modes, keep the screen on, pick a timer. All in two clicks. |
| ⏱️ **Timers** | Keep awake for 30 min / 1 h / 2 h, or set your own presets. |
| 🖥️ **Screen control** | Keep only the system awake, or the display too. |
| 🚀 **Autostart** | "Run at Windows startup" toggle right in the menu. |
| 💾 **Remembers state** | The last mode is restored on the next launch. |
| 🌍 **Localized** | English and Russian interface, picked from your Windows language. |
| ⌨️ **CLI** | All the original command-line options still work for scripts. |

## 🚀 Usage

1. [Download `Awake.exe`](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe).
2. Put it anywhere (for example `C:\Tools\Awake`) and run it.
3. Click the ☕ icon in the tray and pick a mode.

```text
 Keep awake on interval          ▸  30 minutes / 1 hour / 2 hours
 Keep awake until expiration date and time
 Keep awake indefinitely
 Off (keep using the selected power plan)
 ───────────────────────────────
 Keep screen on
 ───────────────────────────────
 Run at Windows startup
 Open settings file
 ───────────────────────────────
 Exit
```

> [!TIP]
> Windows SmartScreen may warn about an unknown app on first launch, because the exe is not
> code-signed. Click **More info → Run anyway**, or build it yourself from source.

### Modes

| Mode | Behavior |
|------|----------|
| **Off** | Your normal power plan applies |
| **Indefinitely** | Stays awake until you switch it off |
| **On interval** | Stays awake for the chosen time, then switches back to Off |
| **Until date/time** | Stays awake until a set moment (set it in the settings file or with `-e`) |

### Settings file

Settings live in `%LOCALAPPDATA%\Awake\settings.json` (tray menu → **Open settings file**).
Changes are applied immediately while Awake is running. Custom timer presets are in seconds:

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 minutes": 900, "1 hour": 3600, "3 hours": 10800 }
  }
}
```

`mode`: `0` = Off, `1` = Indefinitely, `2` = Interval, `3` = Until date/time.
Logs are written to `%LOCALAPPDATA%\Awake\Logs` and kept for 14 days.

## 💻 Command line

Run without arguments to start the tray app. With arguments, Awake runs a one-off session:

| Option | Description |
|--------|-------------|
| `-c`, `--use-config` | Tray mode with the settings file (the default) |
| `-d`, `--display-on` | Keep the display on as well |
| `-t`, `--time-limit <seconds>` | Stay awake for N seconds, then exit |
| `-e`, `--expire-at <date time>` | Stay awake until the given date and time |
| `-p`, `--pid <pid>` | Stay awake while a process is running |
| `-u`, `--use-parent-pid` | Stay awake while the parent process is running |

```powershell
Awake.exe -t 3600 -d                 # 1 hour, screen on
Awake.exe -e "2026-12-31 18:00"      # until a moment in time
Awake.exe -p 1234                    # while process 1234 is alive
```

## 🔍 How it works

Awake calls the Win32 API
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)
with `ES_SYSTEM_REQUIRED` (and `ES_DISPLAY_REQUIRED` when the screen should stay on).
It does not change your power plan, and everything goes back to normal as soon as Awake is
switched off or closed. You can check it with `powercfg /requests`.

## 🔁 Differences from PowerToys Awake

| | PowerToys Awake | This fork |
|---|:---:|:---:|
| Needs PowerToys installed | ✅ | ❌ |
| Telemetry | ✅ | ❌ |
| Tray menu without PowerToys | ❌ | ✅ |
| Run at startup toggle | via PowerToys | ✅ in the menu |
| Single portable exe | ❌ | ✅ |

## 🔧 Building from source

Requires the [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

The result is `publish/win-x64/Awake.exe`. Pushing a tag like `v1.2.3` builds and publishes
a GitHub release automatically.

## 🤝 Contributing

Bug reports and pull requests are welcome. Please open an
[issue](https://github.com/varsoug/awake-standalone/issues) first for larger changes.
Translation fixes for this README are welcome too.

## 📄 License and credits

Awake was created by [Den Delimarsky](https://den.dev) as part of
[Microsoft PowerToys](https://github.com/microsoft/PowerToys).
This fork is distributed under the same [MIT license](LICENSE).
It is not affiliated with or endorsed by Microsoft.

<div align="center">

If this tool saved your download from a sleeping PC, consider giving it a ⭐

</div>
