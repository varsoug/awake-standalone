<div align="center">

[🇬🇧 English](README.md) | [🇨🇳 简体中文](README.zh-CN.md) | [🇪🇸 Español](README.es.md) | [🇧🇷 Português](README.pt-BR.md) | [🇩🇪 Deutsch](README.de.md) | [🇫🇷 Français](README.fr.md) | [🇯🇵 日本語](README.ja.md) | **🇰🇷 한국어** | [🇷🇺 Русский](README.ru.md) | [🇺🇦 Українська](README.uk.md)

<img src="docs/logo.png" width="112" alt="Awake 로고">

# Awake — standalone

**시스템 트레이에서 Windows PC가 절전 모드로 들어가지 않게 합니다. PowerToys 없이, 텔레메트리 없이, `.exe` 하나로.**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**다운로드**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[기능](#-기능) ·
[사용법](#-사용법) ·
[명령줄](#-명령줄) ·
[빌드](#-소스에서-빌드)

</div>

---

Awake는 [Microsoft PowerToys](https://github.com/microsoft/PowerToys)에 포함된 작은 도구로,
긴 다운로드나 렌더링, 프레젠테이션 중에 Windows가 절전 모드로 들어가는 것을 막아 줍니다.
PowerToys에서는 전체 제품군과 함께 있어야만 동작합니다. **이 포크는 Awake를 독립 실행형 앱으로 만든 것입니다**.
파일 하나를 내려받아 실행하고, 트레이 아이콘에서 모든 것을 제어하세요.

## ✨ 기능

|   |   |
|---|---|
| 🪶 **독립 실행** | 휴대용 `Awake.exe` 하나. 설치 프로그램, PowerToys, .NET 런타임이 필요 없습니다. |
| 🔒 **텔레메트리 없음** | PowerToys 텔레메트리와 ETW 이벤트를 코드에서 모두 제거했습니다. |
| 🖱️ **트레이 메뉴** | 모드 전환, 화면 켜두기, 타이머 선택을 두 번의 클릭으로. |
| ⏱️ **타이머** | 30분 / 1시간 / 2시간 또는 직접 정한 시간. |
| 🖥️ **화면 제어** | 시스템만 깨어 있게 하거나 화면도 켜 둘 수 있습니다. |
| 🚀 **자동 시작** | 메뉴에서 바로 "Windows 시작 시 실행"을 켜고 끌 수 있습니다. |
| 💾 **상태 기억** | 다음 실행 때 마지막 모드를 복원합니다. |
| 🌍 **다국어** | 영어와 러시아어 인터페이스를 Windows 언어에 맞춰 표시합니다. |
| ⌨️ **CLI** | 원본의 모든 명령줄 옵션을 그대로 사용할 수 있습니다. |

## 🚀 사용법

1. [`Awake.exe`를 다운로드](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe)합니다.
2. 원하는 폴더(예: `C:\Tools\Awake`)에 두고 실행합니다.
3. 트레이의 ☕ 아이콘을 클릭하고 모드를 선택합니다.

> [!NOTE]
> 앱 인터페이스는 영어와 러시아어로 제공됩니다.

> [!TIP]
> exe에 코드 서명이 없어서 처음 실행할 때 Windows SmartScreen이 경고를 표시할 수 있습니다.
> **추가 정보 → 실행**을 클릭하거나 소스에서 직접 빌드하세요.

### 모드

| 모드 | 동작 |
|------|------|
| **끄기** | 평소 전원 관리 옵션이 적용됩니다 |
| **무기한** | 끌 때까지 깨어 있습니다 |
| **시간 지정** | 선택한 시간 동안 깨어 있다가 꺼집니다 |
| **날짜와 시간까지** | 지정한 시점까지 깨어 있습니다 (설정 파일 또는 `-e`로 지정) |

### 설정 파일

설정은 `%LOCALAPPDATA%\Awake\settings.json`에 저장됩니다 (트레이 메뉴 → **Open settings file**).
Awake가 실행 중이면 변경 사항이 즉시 적용됩니다. 사용자 지정 타이머는 초 단위입니다.

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15분": 900, "1시간": 3600, "3시간": 10800 }
  }
}
```

`mode`: `0` = 끄기, `1` = 무기한, `2` = 시간 지정, `3` = 날짜와 시간까지.
로그는 `%LOCALAPPDATA%\Awake\Logs`에 기록되며 14일 동안 보관됩니다.

## 💻 명령줄

인수 없이 실행하면 트레이 앱이 시작됩니다. 인수를 주면 Awake는 일회성 세션을 실행합니다.

| 옵션 | 설명 |
|------|------|
| `-c`, `--use-config` | 설정 파일을 사용하는 트레이 모드 (기본값) |
| `-d`, `--display-on` | 화면도 켜 둡니다 |
| `-t`, `--time-limit <초>` | N초 동안 깨어 있다가 종료합니다 |
| `-e`, `--expire-at <날짜 시간>` | 지정한 날짜와 시간까지 깨어 있습니다 |
| `-p`, `--pid <pid>` | 지정한 프로세스가 실행되는 동안 깨어 있습니다 |
| `-u`, `--use-parent-pid` | 부모 프로세스가 실행되는 동안 깨어 있습니다 |

```powershell
Awake.exe -t 3600 -d                 # 1시간, 화면 켜짐
Awake.exe -e "2026-12-31 18:00"      # 지정한 시점까지
Awake.exe -p 1234                    # 프로세스 1234가 살아 있는 동안
```

## 🔍 동작 방식

Awake는 Win32 API
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)를
`ES_SYSTEM_REQUIRED`(화면도 켜 둘 때는 `ES_DISPLAY_REQUIRED`도)와 함께 호출합니다.
전원 관리 옵션은 바꾸지 않으며, Awake를 끄거나 종료하면 바로 원래대로 돌아갑니다. `powercfg /requests`로 확인할 수 있습니다.

## 🔁 PowerToys Awake와의 차이점

| | PowerToys Awake | 이 포크 |
|---|:---:|:---:|
| PowerToys 설치 필요 | ✅ | ❌ |
| 텔레메트리 | ✅ | ❌ |
| PowerToys 없는 트레이 메뉴 | ❌ | ✅ |
| 시작 시 실행 설정 | PowerToys에서 | ✅ 메뉴에서 |
| 단일 휴대용 exe | ❌ | ✅ |

## 🔧 소스에서 빌드

[.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)가 필요합니다.

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

결과물은 `publish/win-x64/Awake.exe`입니다. `v1.2.3` 같은 태그를 푸시하면 GitHub 릴리스가 자동으로 빌드되고 게시됩니다.

## 🤝 기여하기

버그 신고와 풀 리퀘스트를 환영합니다. 큰 변경은 먼저
[issue](https://github.com/varsoug/awake-standalone/issues)를 열어 주세요. 번역 개선도 환영합니다.

## 📄 라이선스 및 크레딧

Awake는 [Den Delimarsky](https://den.dev)가
[Microsoft PowerToys](https://github.com/microsoft/PowerToys)의 일부로 만들었습니다.
이 포크는 동일한 [MIT 라이선스](LICENSE)로 배포되며, Microsoft와 관련이 없고 Microsoft의 보증을 받지 않았습니다.

<div align="center">

이 도구가 잠든 PC로부터 다운로드를 지켜 줬다면 ⭐ 를 눌러 주세요

</div>
