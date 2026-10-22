<div align="center">

[🇬🇧 English](README.md) | [🇨🇳 简体中文](README.zh-CN.md) | [🇪🇸 Español](README.es.md) | [🇧🇷 Português](README.pt-BR.md) | [🇩🇪 Deutsch](README.de.md) | **🇫🇷 Français** | [🇯🇵 日本語](README.ja.md) | [🇰🇷 한국어](README.ko.md) | [🇷🇺 Русский](README.ru.md) | [🇺🇦 Українська](README.uk.md)

<img src="docs/logo.png" width="112" alt="Logo d'Awake">

# Awake — standalone

**Gardez votre PC Windows éveillé depuis la zone de notification. Sans PowerToys, sans télémétrie, un seul `.exe`.**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**Télécharger**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[Fonctionnalités](#-fonctionnalités) ·
[Utilisation](#-utilisation) ·
[Ligne de commande](#-ligne-de-commande) ·
[Compilation](#-compiler-depuis-les-sources)

</div>

---

Awake est un petit outil de [Microsoft PowerToys](https://github.com/microsoft/PowerToys) qui
empêche Windows de se mettre en veille, par exemple pendant un long téléchargement, un rendu ou une
présentation. Dans PowerToys, il ne fonctionne qu'avec le reste de la suite. **Ce fork en fait une
application autonome** : téléchargez un fichier, lancez-le et contrôlez tout depuis l'icône de la
zone de notification.

## ✨ Fonctionnalités

|   |   |
|---|---|
| 🪶 **Autonome** | Un seul `Awake.exe` portable. Pas d'installateur, pas de PowerToys, pas de runtime .NET. |
| 🔒 **Aucune télémétrie** | Toute la télémétrie PowerToys et les événements ETW ont été retirés du code. |
| 🖱️ **Menu de notification** | Changez de mode, gardez l'écran allumé, choisissez une minuterie en deux clics. |
| ⏱️ **Minuteries** | 30 min / 1 h / 2 h ou vos propres durées. |
| 🖥️ **Contrôle de l'écran** | Garder seulement le système éveillé, ou aussi l'écran. |
| 🚀 **Démarrage automatique** | Option « Lancer au démarrage de Windows » directement dans le menu. |
| 💾 **Mémorise l'état** | Le dernier mode est restauré au lancement suivant. |
| 🌍 **Langues** | Interface en anglais et en russe, selon la langue de Windows. |
| ⌨️ **CLI** | Toutes les options de ligne de commande d'origine fonctionnent toujours. |

## 🚀 Utilisation

1. [Téléchargez `Awake.exe`](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe).
2. Placez-le où vous voulez (par exemple `C:\Tools\Awake`) et lancez-le.
3. Cliquez sur l'icône ☕ dans la zone de notification et choisissez un mode.

> [!NOTE]
> L'interface de l'application est disponible en anglais et en russe.

> [!TIP]
> Windows SmartScreen peut afficher un avertissement au premier lancement, car l'exe n'est pas
> signé. Cliquez sur **Informations complémentaires → Exécuter quand même**, ou compilez-le vous-même.

### Modes

| Mode | Comportement |
|------|--------------|
| **Désactivé** | Votre mode de gestion de l'alimentation habituel s'applique |
| **Indéfiniment** | Reste éveillé jusqu'à ce que vous le désactiviez |
| **Pendant une durée** | Reste éveillé pendant la durée choisie, puis se désactive |
| **Jusqu'à une date et heure** | Reste éveillé jusqu'à un moment donné (dans le fichier de paramètres ou avec `-e`) |

### Fichier de paramètres

Les paramètres se trouvent dans `%LOCALAPPDATA%\Awake\settings.json` (menu → **Open settings file**).
Les modifications sont appliquées immédiatement. Les minuteries personnalisées sont en secondes :

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 minutes": 900, "1 heure": 3600, "3 heures": 10800 }
  }
}
```

`mode` : `0` = désactivé, `1` = indéfiniment, `2` = durée, `3` = jusqu'à une date et heure.
Les journaux sont écrits dans `%LOCALAPPDATA%\Awake\Logs` et conservés 14 jours.

## 💻 Ligne de commande

Sans argument, l'application de la zone de notification démarre. Avec des arguments, Awake lance une session unique :

| Option | Description |
|--------|-------------|
| `-c`, `--use-config` | Mode zone de notification avec le fichier de paramètres (par défaut) |
| `-d`, `--display-on` | Garder aussi l'écran allumé |
| `-t`, `--time-limit <secondes>` | Rester éveillé N secondes, puis quitter |
| `-e`, `--expire-at <date heure>` | Rester éveillé jusqu'à la date et l'heure indiquées |
| `-p`, `--pid <pid>` | Rester éveillé tant qu'un processus s'exécute |
| `-u`, `--use-parent-pid` | Rester éveillé tant que le processus parent s'exécute |

```powershell
Awake.exe -t 3600 -d                 # 1 heure, écran allumé
Awake.exe -e "2026-12-31 18:00"      # jusqu'à un moment donné
Awake.exe -p 1234                    # tant que le processus 1234 est actif
```

## 🔍 Fonctionnement

Awake appelle l'API Win32
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)
avec `ES_SYSTEM_REQUIRED` (et `ES_DISPLAY_REQUIRED` si l'écran doit rester allumé).
Il ne modifie pas votre mode d'alimentation, et tout redevient normal dès qu'Awake est désactivé
ou fermé. Vous pouvez le vérifier avec `powercfg /requests`.

## 🔁 Différences avec PowerToys Awake

| | PowerToys Awake | Ce fork |
|---|:---:|:---:|
| Nécessite PowerToys installé | ✅ | ❌ |
| Télémétrie | ✅ | ❌ |
| Menu de notification sans PowerToys | ❌ | ✅ |
| Lancement au démarrage | via PowerToys | ✅ dans le menu |
| Un seul exe portable | ❌ | ✅ |

## 🔧 Compiler depuis les sources

Nécessite le [SDK .NET 9](https://dotnet.microsoft.com/download/dotnet/9.0).

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

Le résultat est `publish/win-x64/Awake.exe`. L'envoi d'un tag comme `v1.2.3` compile et publie
automatiquement une release GitHub.

## 🤝 Contribuer

Les rapports de bugs et les pull requests sont les bienvenus. Pour les changements importants,
ouvrez d'abord une [issue](https://github.com/varsoug/awake-standalone/issues). Les corrections de traduction sont aussi bienvenues.

## 📄 Licence et crédits

Awake a été créé par [Den Delimarsky](https://den.dev) dans le cadre de
[Microsoft PowerToys](https://github.com/microsoft/PowerToys).
Ce fork est distribué sous la même [licence MIT](LICENSE) et n'est ni affilié à Microsoft ni approuvé par Microsoft.

<div align="center">

Si cet outil a sauvé votre téléchargement d'un PC endormi, pensez à lui donner une ⭐

</div>
