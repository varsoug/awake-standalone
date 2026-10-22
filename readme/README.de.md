<div align="center">

[🇬🇧 English](../README.md) | [🇨🇳 简体中文](README.zh-CN.md) | [🇪🇸 Español](README.es.md) | [🇧🇷 Português](README.pt-BR.md) | **🇩🇪 Deutsch** | [🇫🇷 Français](README.fr.md) | [🇯🇵 日本語](README.ja.md) | [🇰🇷 한국어](README.ko.md) | [🇷🇺 Русский](README.ru.md) | [🇺🇦 Українська](README.uk.md)

<img src="../docs/logo.png" width="112" alt="Awake-Logo">

# Awake — standalone

**Hält deinen Windows-PC über das Infobereich-Symbol wach. Ohne PowerToys, ohne Telemetrie, nur eine `.exe`.**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](../LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**Herunterladen**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[Funktionen](#-funktionen) ·
[Verwendung](#-verwendung) ·
[Kommandozeile](#-kommandozeile) ·
[Erstellen](#-aus-dem-quellcode-erstellen)

</div>

---

Awake ist ein kleines Tool aus [Microsoft PowerToys](https://github.com/microsoft/PowerToys), das
verhindert, dass Windows in den Energiesparmodus wechselt, etwa bei einem langen Download, einem
Rendervorgang oder einer Präsentation. In PowerToys funktioniert es nur zusammen mit dem Rest der
Suite. **Dieser Fork macht daraus eine eigenständige App**: eine Datei herunterladen, starten und
alles über das Symbol im Infobereich steuern.

## ✨ Funktionen

|   |   |
|---|---|
| 🪶 **Eigenständig** | Eine portable `Awake.exe`. Kein Installer, kein PowerToys, keine .NET-Runtime nötig. |
| 🔒 **Keine Telemetrie** | Sämtliche PowerToys-Telemetrie und ETW-Ereignisse wurden aus dem Code entfernt. |
| 🖱️ **Infobereich-Menü** | Modus wechseln, Bildschirm anlassen, Timer wählen – mit zwei Klicks. |
| ⏱️ **Timer** | 30 Min. / 1 Std. / 2 Std. oder eigene Vorgaben. |
| 🖥️ **Bildschirmsteuerung** | Nur das System wach halten oder auch den Bildschirm. |
| 🚀 **Autostart** | Schalter „Mit Windows starten“ direkt im Menü. |
| 💾 **Merkt sich den Zustand** | Der letzte Modus wird beim nächsten Start wiederhergestellt. |
| 🌍 **Sprachen** | Oberfläche auf Englisch und Russisch, je nach Windows-Sprache. |
| ⌨️ **CLI** | Alle ursprünglichen Kommandozeilenoptionen funktionieren weiterhin. |

## 🚀 Verwendung

1. [`Awake.exe` herunterladen](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe).
2. In einen beliebigen Ordner legen (z. B. `C:\Tools\Awake`) und starten.
3. Auf das ☕-Symbol im Infobereich klicken und einen Modus wählen.

> [!NOTE]
> Die Oberfläche der App ist auf Englisch und Russisch verfügbar.

> [!TIP]
> Windows SmartScreen kann beim ersten Start vor einer unbekannten App warnen, da die exe nicht
> signiert ist. Klicke auf **Weitere Informationen → Trotzdem ausführen** oder erstelle sie selbst.

### Modi

| Modus | Verhalten |
|-------|-----------|
| **Aus** | Es gilt dein normaler Energiesparplan |
| **Unbegrenzt** | Bleibt wach, bis du es ausschaltest |
| **Für eine Zeitspanne** | Bleibt für die gewählte Zeit wach und schaltet dann ab |
| **Bis Datum und Uhrzeit** | Bleibt bis zu einem Zeitpunkt wach (in der Einstellungsdatei oder mit `-e`) |

### Einstellungsdatei

Die Einstellungen liegen in `%LOCALAPPDATA%\Awake\settings.json` (Menü → **Open settings file**).
Änderungen werden sofort übernommen. Eigene Timer werden in Sekunden angegeben:

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 Minuten": 900, "1 Stunde": 3600, "3 Stunden": 10800 }
  }
}
```

`mode`: `0` = aus, `1` = unbegrenzt, `2` = Zeitspanne, `3` = bis Datum und Uhrzeit.
Protokolle werden in `%LOCALAPPDATA%\Awake\Logs` geschrieben und 14 Tage aufbewahrt.

## 💻 Kommandozeile

Ohne Argumente startet die Infobereich-App. Mit Argumenten führt Awake eine einmalige Sitzung aus:

| Option | Beschreibung |
|--------|--------------|
| `-c`, `--use-config` | Infobereich-Modus mit Einstellungsdatei (Standard) |
| `-d`, `--display-on` | Auch den Bildschirm eingeschaltet lassen |
| `-t`, `--time-limit <Sekunden>` | N Sekunden wach bleiben, dann beenden |
| `-e`, `--expire-at <Datum Uhrzeit>` | Bis zum angegebenen Zeitpunkt wach bleiben |
| `-p`, `--pid <pid>` | Wach bleiben, solange ein Prozess läuft |
| `-u`, `--use-parent-pid` | Wach bleiben, solange der übergeordnete Prozess läuft |

```powershell
Awake.exe -t 3600 -d                 # 1 Stunde, Bildschirm an
Awake.exe -e "2026-12-31 18:00"      # bis zu einem Zeitpunkt
Awake.exe -p 1234                    # solange Prozess 1234 läuft
```

## 🔍 Funktionsweise

Awake ruft die Win32-API
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)
mit `ES_SYSTEM_REQUIRED` auf (und `ES_DISPLAY_REQUIRED`, wenn der Bildschirm an bleiben soll).
Dein Energiesparplan wird nicht verändert, und alles ist wieder normal, sobald Awake ausgeschaltet
oder beendet wird. Prüfen kannst du das mit `powercfg /requests`.

## 🔁 Unterschiede zu PowerToys Awake

| | PowerToys Awake | Dieser Fork |
|---|:---:|:---:|
| PowerToys muss installiert sein | ✅ | ❌ |
| Telemetrie | ✅ | ❌ |
| Infobereich-Menü ohne PowerToys | ❌ | ✅ |
| Mit Windows starten | über PowerToys | ✅ im Menü |
| Eine portable exe | ❌ | ✅ |

## 🔧 Aus dem Quellcode erstellen

Benötigt das [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

Das Ergebnis ist `publish/win-x64/Awake.exe`. Beim Pushen eines Tags wie `v1.2.3` wird
automatisch ein GitHub-Release erstellt und veröffentlicht.

## 🤝 Mitwirken

Fehlerberichte und Pull Requests sind willkommen. Bei größeren Änderungen bitte zuerst ein
[Issue](https://github.com/varsoug/awake-standalone/issues) eröffnen. Übersetzungskorrekturen sind ebenfalls willkommen.

## 📄 Lizenz und Danksagung

Awake wurde von [Den Delimarsky](https://den.dev) als Teil von
[Microsoft PowerToys](https://github.com/microsoft/PowerToys) entwickelt.
Dieser Fork steht unter derselben [MIT-Lizenz](../LICENSE) und ist nicht mit Microsoft verbunden oder von Microsoft unterstützt.

<div align="center">

Wenn dieses Tool deinen Download vor einem schlafenden PC gerettet hat, gib ihm gern einen ⭐

</div>
