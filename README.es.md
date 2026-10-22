<div align="center">

[🇬🇧 English](README.md) | [🇨🇳 简体中文](README.zh-CN.md) | **🇪🇸 Español** | [🇧🇷 Português](README.pt-BR.md) | [🇩🇪 Deutsch](README.de.md) | [🇫🇷 Français](README.fr.md) | [🇯🇵 日本語](README.ja.md) | [🇰🇷 한국어](README.ko.md) | [🇷🇺 Русский](README.ru.md) | [🇺🇦 Українська](README.uk.md)

<img src="docs/logo.png" width="112" alt="Logo de Awake">

# Awake — standalone

**Mantén tu PC con Windows despierto desde la bandeja del sistema. Sin PowerToys, sin telemetría, solo un `.exe`.**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**Descargar**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[Características](#-características) ·
[Uso](#-uso) ·
[Línea de comandos](#-línea-de-comandos) ·
[Compilación](#-compilar-desde-el-código-fuente)

</div>

---

Awake es una pequeña herramienta de [Microsoft PowerToys](https://github.com/microsoft/PowerToys)
que impide que Windows entre en suspensión, por ejemplo durante una descarga larga, un renderizado
o una presentación. En PowerToys solo funciona junto con el resto del paquete. **Este fork la
convierte en una aplicación independiente**: descarga un archivo, ejecútalo y controla todo desde
el icono de la bandeja.

## ✨ Características

|   |   |
|---|---|
| 🪶 **Independiente** | Un único `Awake.exe` portátil. Sin instalador, sin PowerToys y sin runtime de .NET. |
| 🔒 **Sin telemetría** | Se eliminó del código toda la telemetría de PowerToys y los eventos ETW. |
| 🖱️ **Menú de bandeja** | Cambia de modo, mantén la pantalla encendida o elige un temporizador en dos clics. |
| ⏱️ **Temporizadores** | 30 min / 1 h / 2 h o tus propios intervalos. |
| 🖥️ **Control de pantalla** | Mantén despierto solo el sistema o también la pantalla. |
| 🚀 **Inicio automático** | Opción "Ejecutar al iniciar Windows" directamente en el menú. |
| 💾 **Recuerda el estado** | El último modo se restaura en el siguiente inicio. |
| 🌍 **Idiomas** | Interfaz en inglés y ruso, según el idioma de Windows. |
| ⌨️ **CLI** | Todas las opciones de línea de comandos originales siguen funcionando. |

## 🚀 Uso

1. [Descarga `Awake.exe`](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe).
2. Colócalo en cualquier carpeta (por ejemplo `C:\Tools\Awake`) y ejecútalo.
3. Haz clic en el icono ☕ de la bandeja y elige un modo.

> [!NOTE]
> La interfaz de la aplicación está disponible en inglés y ruso.

> [!TIP]
> Windows SmartScreen puede avisar de una aplicación desconocida la primera vez, porque el exe no
> está firmado. Haz clic en **Más información → Ejecutar de todas formas** o compílalo tú mismo.

### Modos

| Modo | Comportamiento |
|------|----------------|
| **Desactivado** | Se aplica tu plan de energía normal |
| **Indefinido** | Permanece despierto hasta que lo desactives |
| **Por intervalo** | Permanece despierto el tiempo elegido y luego se desactiva |
| **Hasta fecha y hora** | Permanece despierto hasta un momento dado (en el archivo de configuración o con `-e`) |

### Archivo de configuración

La configuración está en `%LOCALAPPDATA%\Awake\settings.json` (menú → **Open settings file**).
Los cambios se aplican al instante. Los temporizadores propios se indican en segundos:

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 minutos": 900, "1 hora": 3600, "3 horas": 10800 }
  }
}
```

`mode`: `0` = desactivado, `1` = indefinido, `2` = intervalo, `3` = hasta fecha y hora.
Los registros se guardan en `%LOCALAPPDATA%\Awake\Logs` durante 14 días.

## 💻 Línea de comandos

Sin argumentos se inicia la aplicación de bandeja. Con argumentos, Awake ejecuta una sesión única:

| Opción | Descripción |
|--------|-------------|
| `-c`, `--use-config` | Modo bandeja con el archivo de configuración (predeterminado) |
| `-d`, `--display-on` | Mantener también la pantalla encendida |
| `-t`, `--time-limit <segundos>` | Permanecer despierto N segundos y salir |
| `-e`, `--expire-at <fecha hora>` | Permanecer despierto hasta la fecha y hora indicadas |
| `-p`, `--pid <pid>` | Permanecer despierto mientras se ejecute un proceso |
| `-u`, `--use-parent-pid` | Permanecer despierto mientras se ejecute el proceso padre |

```powershell
Awake.exe -t 3600 -d                 # 1 hora, pantalla encendida
Awake.exe -e "2026-12-31 18:00"      # hasta un momento dado
Awake.exe -p 1234                    # mientras el proceso 1234 siga activo
```

## 🔍 Cómo funciona

Awake llama a la API Win32
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)
con `ES_SYSTEM_REQUIRED` (y `ES_DISPLAY_REQUIRED` si la pantalla debe seguir encendida).
No modifica tu plan de energía y todo vuelve a la normalidad en cuanto Awake se desactiva o se
cierra. Puedes comprobarlo con `powercfg /requests`.

## 🔁 Diferencias con PowerToys Awake

| | PowerToys Awake | Este fork |
|---|:---:|:---:|
| Requiere PowerToys instalado | ✅ | ❌ |
| Telemetría | ✅ | ❌ |
| Menú de bandeja sin PowerToys | ❌ | ✅ |
| Inicio con Windows | mediante PowerToys | ✅ en el menú |
| Un único exe portátil | ❌ | ✅ |

## 🔧 Compilar desde el código fuente

Requiere el [SDK de .NET 9](https://dotnet.microsoft.com/download/dotnet/9.0).

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

El resultado es `publish/win-x64/Awake.exe`. Al subir una etiqueta como `v1.2.3` se compila y
publica automáticamente una release en GitHub.

## 🤝 Contribuir

Los informes de errores y los pull requests son bienvenidos. Para cambios grandes, abre primero un
[issue](https://github.com/varsoug/awake-standalone/issues). Las correcciones de traducción también son bienvenidas.

## 📄 Licencia y créditos

Awake fue creado por [Den Delimarsky](https://den.dev) como parte de
[Microsoft PowerToys](https://github.com/microsoft/PowerToys).
Este fork se distribuye bajo la misma [licencia MIT](LICENSE) y no está afiliado ni respaldado por Microsoft.

<div align="center">

Si esta herramienta salvó tu descarga de un PC dormido, considera darle una ⭐

</div>
