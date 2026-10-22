<div align="center">

[🇬🇧 English](README.md) | **🇨🇳 简体中文** | [🇪🇸 Español](README.es.md) | [🇧🇷 Português](README.pt-BR.md) | [🇩🇪 Deutsch](README.de.md) | [🇫🇷 Français](README.fr.md) | [🇯🇵 日本語](README.ja.md) | [🇰🇷 한국어](README.ko.md) | [🇷🇺 Русский](README.ru.md) | [🇺🇦 Українська](README.uk.md)

<img src="docs/logo.png" width="112" alt="Awake 图标">

# Awake — standalone

**在系统托盘中让 Windows 电脑保持唤醒。无需 PowerToys，无遥测，只有一个 `.exe`。**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**下载**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[功能](#-功能) ·
[使用方法](#-使用方法) ·
[命令行](#-命令行) ·
[从源码构建](#-从源码构建)

</div>

---

Awake 是 [Microsoft PowerToys](https://github.com/microsoft/PowerToys) 中的一个小工具，可以阻止
Windows 进入睡眠，例如在长时间下载、渲染或演示时。在 PowerToys 中，它只能与整个套件一起使用。
**这个分支把它变成了一个独立的应用**：下载一个文件，运行它，然后通过托盘图标控制一切。

## ✨ 功能

|   |   |
|---|---|
| 🪶 **独立运行** | 单个便携式 `Awake.exe`。无需安装程序、PowerToys 或 .NET 运行时。 |
| 🔒 **无遥测** | 代码中已移除所有 PowerToys 遥测和 ETW 事件。 |
| 🖱️ **托盘菜单** | 切换模式、保持屏幕常亮、选择计时器，两次点击即可完成。 |
| ⏱️ **计时器** | 保持唤醒 30 分钟 / 1 小时 / 2 小时，或自定义时长。 |
| 🖥️ **屏幕控制** | 只保持系统唤醒，或同时保持屏幕常亮。 |
| 🚀 **开机自启** | 菜单中直接提供“随 Windows 启动”开关。 |
| 💾 **记住状态** | 下次启动时恢复上次的模式。 |
| 🌍 **多语言界面** | 界面支持英语和俄语，根据 Windows 语言自动选择。 |
| ⌨️ **命令行** | 原版的所有命令行参数仍可在脚本中使用。 |

## 🚀 使用方法

1. [下载 `Awake.exe`](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe)。
2. 放到任意位置（例如 `C:\Tools\Awake`）并运行。
3. 点击托盘中的 ☕ 图标并选择模式。

> [!NOTE]
> 应用界面目前提供英语和俄语。

> [!TIP]
> 由于 exe 没有代码签名，首次运行时 Windows SmartScreen 可能会提示未知应用。
> 点击 **更多信息 → 仍要运行**，或者从源码自行构建。

### 模式

| 模式 | 行为 |
|------|------|
| **关闭** | 使用正常的电源计划 |
| **无限期** | 保持唤醒，直到手动关闭 |
| **定时** | 在所选时长内保持唤醒，然后恢复为关闭 |
| **直到指定时间** | 保持唤醒到设定的时刻（在设置文件中或用 `-e` 设置） |

### 设置文件

设置保存在 `%LOCALAPPDATA%\Awake\settings.json`（托盘菜单 → **Open settings file**）。
Awake 运行时修改会立即生效。自定义计时器以秒为单位：

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 分钟": 900, "1 小时": 3600, "3 小时": 10800 }
  }
}
```

`mode`：`0` = 关闭，`1` = 无限期，`2` = 定时，`3` = 直到指定时间。
日志写入 `%LOCALAPPDATA%\Awake\Logs`，保留 14 天。

## 💻 命令行

不带参数运行会启动托盘应用。带参数时，Awake 执行一次性会话：

| 参数 | 说明 |
|------|------|
| `-c`, `--use-config` | 使用设置文件的托盘模式（默认） |
| `-d`, `--display-on` | 同时保持屏幕常亮 |
| `-t`, `--time-limit <秒>` | 保持唤醒 N 秒后退出 |
| `-e`, `--expire-at <日期 时间>` | 保持唤醒到指定日期和时间 |
| `-p`, `--pid <pid>` | 在指定进程运行期间保持唤醒 |
| `-u`, `--use-parent-pid` | 在父进程运行期间保持唤醒 |

```powershell
Awake.exe -t 3600 -d                 # 1 小时，屏幕常亮
Awake.exe -e "2026-12-31 18:00"      # 直到某个时间点
Awake.exe -p 1234                    # 在进程 1234 运行期间
```

## 🔍 工作原理

Awake 调用 Win32 API
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)，
使用 `ES_SYSTEM_REQUIRED`（需要屏幕常亮时还会加上 `ES_DISPLAY_REQUIRED`）。
它不会修改你的电源计划，一旦关闭或退出 Awake，一切都会恢复正常。可以用 `powercfg /requests` 查看。

## 🔁 与 PowerToys Awake 的区别

| | PowerToys Awake | 本分支 |
|---|:---:|:---:|
| 需要安装 PowerToys | ✅ | ❌ |
| 遥测 | ✅ | ❌ |
| 无需 PowerToys 的托盘菜单 | ❌ | ✅ |
| 开机自启开关 | 通过 PowerToys | ✅ 在菜单中 |
| 单个便携式 exe | ❌ | ✅ |

## 🔧 从源码构建

需要 [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)。

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

生成的文件是 `publish/win-x64/Awake.exe`。推送形如 `v1.2.3` 的标签会自动构建并发布 GitHub Release。

## 🤝 参与贡献

欢迎提交 Bug 报告和 Pull Request。较大的改动请先创建
[issue](https://github.com/varsoug/awake-standalone/issues)。也欢迎改进本 README 的翻译。

## 📄 许可证与致谢

Awake 由 [Den Delimarsky](https://den.dev) 创建，是
[Microsoft PowerToys](https://github.com/microsoft/PowerToys) 的一部分。
本分支采用相同的 [MIT 许可证](LICENSE) 发布，与 Microsoft 无关，也未获其认可。

<div align="center">

如果这个工具让你的下载免于电脑睡眠，欢迎点个 ⭐

</div>
