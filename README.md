# Awake (standalone)

A standalone fork of [PowerToys Awake](https://github.com/microsoft/PowerToys/tree/main/src/modules/awake)
that works **without PowerToys** installed. A single `Awake.exe` that keeps your computer
(and optionally the screen) awake, controlled from the system tray.

Форк утилиты Awake из PowerToys, который работает **без PowerToys**: один `Awake.exe`,
не дающий компьютеру (и, по желанию, экрану) уснуть. Управление — из значка в трее.

## What changed compared to PowerToys Awake

- No dependency on PowerToys (Settings UI, runner, interop, group policy wrapper).
- **All telemetry removed** (no ETW / PowerToys telemetry events).
- Tray menu works on its own: modes, "Keep screen on", interval presets, **Run at Windows startup**, **Open settings file**, Exit.
- Running `Awake.exe` with no arguments starts the tray app and remembers the last mode.
- Settings: `%LOCALAPPDATA%\Awake\settings.json`; logs: `%LOCALAPPDATA%\Awake\Logs` (kept for 14 days).
- Single self-contained executable (no .NET install needed), icons embedded.
- Russian UI translation (picked automatically from the Windows language).

## Download

Grab `Awake.exe` from the [Releases](../../releases) page, put it anywhere and run it.
Right-click (or left-click) the tray icon to pick a mode.

## Tray modes

| Mode | What it does |
|------|--------------|
| Off | Normal power plan behavior |
| Keep awake indefinitely | Until you switch it off |
| Keep awake on interval | 30 min / 1 h / 2 h (customizable) |
| Keep screen on | Also prevents the display from turning off |

Custom interval presets can be set in `settings.json` (values are in seconds):

```json
"customTrayTimes": { "15 minutes": 900, "3 hours": 10800 }
```

## Command line

Any argument switches Awake to one-shot CLI mode (like the original):

| Option | Description |
|--------|-------------|
| `-c`, `--use-config` | Tray mode with the settings file (default with no arguments) |
| `-d`, `--display-on` | Keep the display on as well |
| `-t`, `--time-limit <seconds>` | Stay awake for N seconds, then exit |
| `-e`, `--expire-at <date/time>` | Stay awake until a date/time, e.g. `-e "2026-12-31 18:00"` |
| `-p`, `--pid <pid>` | Stay awake while the given process is running |
| `-u`, `--use-parent-pid` | Stay awake while the parent process is running |

```bat
Awake.exe -t 3600 -d
```

## Building

Requires the [.NET 9 SDK](https://dotnet.microsoft.com/download).

```bat
dotnet test src\Awake.UnitTests
dotnet publish src\Awake\Awake.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o publish\win-x64
```

## Credits and license

Awake was created by [Den Delimarsky](https://den.dev) and is part of
[Microsoft PowerToys](https://github.com/microsoft/PowerToys). This fork is distributed under the
same [MIT license](LICENSE). It is not affiliated with or endorsed by Microsoft.
