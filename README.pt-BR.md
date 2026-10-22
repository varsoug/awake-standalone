<div align="center">

[🇬🇧 English](README.md) | [🇨🇳 简体中文](README.zh-CN.md) | [🇪🇸 Español](README.es.md) | **🇧🇷 Português** | [🇩🇪 Deutsch](README.de.md) | [🇫🇷 Français](README.fr.md) | [🇯🇵 日本語](README.ja.md) | [🇰🇷 한국어](README.ko.md) | [🇷🇺 Русский](README.ru.md) | [🇺🇦 Українська](README.uk.md)

<img src="docs/logo.png" width="112" alt="Logo do Awake">

# Awake — standalone

**Mantenha seu PC com Windows acordado pela bandeja do sistema. Sem PowerToys, sem telemetria, apenas um `.exe`.**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**Baixar**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[Recursos](#-recursos) ·
[Como usar](#-como-usar) ·
[Linha de comando](#-linha-de-comando) ·
[Compilação](#-compilar-a-partir-do-código-fonte)

</div>

---

O Awake é uma pequena ferramenta do [Microsoft PowerToys](https://github.com/microsoft/PowerToys)
que impede o Windows de entrar em suspensão, por exemplo durante um download longo, uma
renderização ou uma apresentação. No PowerToys, ele só funciona junto com o restante do pacote.
**Este fork o transforma em um aplicativo independente**: baixe um arquivo, execute e controle
tudo pelo ícone da bandeja.

## ✨ Recursos

|   |   |
|---|---|
| 🪶 **Independente** | Um único `Awake.exe` portátil. Sem instalador, sem PowerToys e sem runtime do .NET. |
| 🔒 **Sem telemetria** | Toda a telemetria do PowerToys e os eventos ETW foram removidos do código. |
| 🖱️ **Menu da bandeja** | Troque de modo, mantenha a tela ligada ou escolha um timer em dois cliques. |
| ⏱️ **Timers** | 30 min / 1 h / 2 h ou seus próprios intervalos. |
| 🖥️ **Controle da tela** | Mantenha acordado só o sistema ou também a tela. |
| 🚀 **Inicialização automática** | Opção "Executar ao iniciar o Windows" direto no menu. |
| 💾 **Lembra o estado** | O último modo é restaurado na próxima execução. |
| 🌍 **Idiomas** | Interface em inglês e russo, conforme o idioma do Windows. |
| ⌨️ **CLI** | Todas as opções de linha de comando originais continuam funcionando. |

## 🚀 Como usar

1. [Baixe o `Awake.exe`](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe).
2. Coloque-o em qualquer pasta (por exemplo `C:\Tools\Awake`) e execute.
3. Clique no ícone ☕ na bandeja e escolha um modo.

> [!NOTE]
> A interface do aplicativo está disponível em inglês e russo.

> [!TIP]
> O Windows SmartScreen pode avisar sobre um aplicativo desconhecido na primeira execução, pois o
> exe não é assinado. Clique em **Mais informações → Executar assim mesmo** ou compile você mesmo.

### Modos

| Modo | Comportamento |
|------|---------------|
| **Desligado** | Vale o seu plano de energia normal |
| **Indefinidamente** | Fica acordado até você desligar |
| **Por intervalo** | Fica acordado pelo tempo escolhido e depois desliga |
| **Até data e hora** | Fica acordado até um momento definido (no arquivo de configurações ou com `-e`) |

### Arquivo de configurações

As configurações ficam em `%LOCALAPPDATA%\Awake\settings.json` (menu → **Open settings file**).
As alterações são aplicadas na hora. Timers personalizados são definidos em segundos:

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 minutos": 900, "1 hora": 3600, "3 horas": 10800 }
  }
}
```

`mode`: `0` = desligado, `1` = indefinidamente, `2` = intervalo, `3` = até data e hora.
Os logs ficam em `%LOCALAPPDATA%\Awake\Logs` por 14 dias.

## 💻 Linha de comando

Sem argumentos, o aplicativo da bandeja é iniciado. Com argumentos, o Awake executa uma sessão única:

| Opção | Descrição |
|-------|-----------|
| `-c`, `--use-config` | Modo bandeja com o arquivo de configurações (padrão) |
| `-d`, `--display-on` | Manter a tela ligada também |
| `-t`, `--time-limit <segundos>` | Ficar acordado por N segundos e sair |
| `-e`, `--expire-at <data hora>` | Ficar acordado até a data e hora indicadas |
| `-p`, `--pid <pid>` | Ficar acordado enquanto um processo estiver em execução |
| `-u`, `--use-parent-pid` | Ficar acordado enquanto o processo pai estiver em execução |

```powershell
Awake.exe -t 3600 -d                 # 1 hora, tela ligada
Awake.exe -e "2026-12-31 18:00"      # até um momento definido
Awake.exe -p 1234                    # enquanto o processo 1234 estiver ativo
```

## 🔍 Como funciona

O Awake chama a API Win32
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)
com `ES_SYSTEM_REQUIRED` (e `ES_DISPLAY_REQUIRED` quando a tela deve continuar ligada).
Ele não altera o seu plano de energia, e tudo volta ao normal assim que o Awake é desligado ou
fechado. Você pode verificar com `powercfg /requests`.

## 🔁 Diferenças em relação ao PowerToys Awake

| | PowerToys Awake | Este fork |
|---|:---:|:---:|
| Requer o PowerToys instalado | ✅ | ❌ |
| Telemetria | ✅ | ❌ |
| Menu da bandeja sem PowerToys | ❌ | ✅ |
| Iniciar com o Windows | via PowerToys | ✅ no menu |
| Um único exe portátil | ❌ | ✅ |

## 🔧 Compilar a partir do código-fonte

Requer o [SDK do .NET 9](https://dotnet.microsoft.com/download/dotnet/9.0).

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

O resultado é `publish/win-x64/Awake.exe`. Ao enviar uma tag como `v1.2.3`, uma release no
GitHub é compilada e publicada automaticamente.

## 🤝 Contribuindo

Relatórios de bugs e pull requests são bem-vindos. Para mudanças maiores, abra primeiro uma
[issue](https://github.com/varsoug/awake-standalone/issues). Correções de tradução também são bem-vindas.

## 📄 Licença e créditos

O Awake foi criado por [Den Delimarsky](https://den.dev) como parte do
[Microsoft PowerToys](https://github.com/microsoft/PowerToys).
Este fork é distribuído sob a mesma [licença MIT](LICENSE) e não é afiliado nem endossado pela Microsoft.

<div align="center">

Se esta ferramenta salvou seu download de um PC adormecido, considere dar uma ⭐

</div>
