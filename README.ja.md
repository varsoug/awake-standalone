<div align="center">

[🇬🇧 English](README.md) | [🇨🇳 简体中文](README.zh-CN.md) | [🇪🇸 Español](README.es.md) | [🇧🇷 Português](README.pt-BR.md) | [🇩🇪 Deutsch](README.de.md) | [🇫🇷 Français](README.fr.md) | **🇯🇵 日本語** | [🇰🇷 한국어](README.ko.md) | [🇷🇺 Русский](README.ru.md) | [🇺🇦 Українська](README.uk.md)

<img src="docs/logo.png" width="112" alt="Awake のロゴ">

# Awake — standalone

**システムトレイから Windows PC のスリープを防ぎます。PowerToys 不要、テレメトリなし、`.exe` ひとつだけ。**

[![Release](https://img.shields.io/github/v/release/varsoug/awake-standalone?style=flat-square&color=2ea44f)](https://github.com/varsoug/awake-standalone/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/varsoug/awake-standalone/total?style=flat-square&color=0078d4)](https://github.com/varsoug/awake-standalone/releases)
[![Build](https://img.shields.io/github/actions/workflow/status/varsoug/awake-standalone/build.yml?branch=main&style=flat-square)](https://github.com/varsoug/awake-standalone/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/github/license/varsoug/awake-standalone?style=flat-square)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-0078d4?style=flat-square&logo=windows)
![.NET](https://img.shields.io/badge/.NET-9-512bd4?style=flat-square&logo=dotnet)

[**ダウンロード**](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe) ·
[機能](#-機能) ·
[使い方](#-使い方) ·
[コマンドライン](#-コマンドライン) ·
[ビルド](#-ソースからビルド)

</div>

---

Awake は [Microsoft PowerToys](https://github.com/microsoft/PowerToys) に含まれる小さなツールで、
長時間のダウンロードやレンダリング、プレゼンテーションの最中に Windows がスリープするのを防ぎます。
PowerToys では、スイート全体と一緒にしか動作しません。**このフォークはそれを単体アプリにしたものです**。
ファイルをひとつダウンロードして実行すれば、すべてトレイアイコンから操作できます。

## ✨ 機能

|   |   |
|---|---|
| 🪶 **単体で動作** | ポータブルな `Awake.exe` ひとつ。インストーラー、PowerToys、.NET ランタイムは不要です。 |
| 🔒 **テレメトリなし** | PowerToys のテレメトリと ETW イベントはコードからすべて削除しました。 |
| 🖱️ **トレイメニュー** | モード切り替え、画面の常時オン、タイマー選択が 2 クリックで行えます。 |
| ⏱️ **タイマー** | 30 分 / 1 時間 / 2 時間、または独自のプリセット。 |
| 🖥️ **画面の制御** | システムだけを起動状態にするか、画面も点けたままにするかを選べます。 |
| 🚀 **自動起動** | 「Windows の起動時に実行」の切り替えをメニューから直接操作できます。 |
| 💾 **状態を記憶** | 次回起動時に前回のモードを復元します。 |
| 🌍 **多言語** | 英語とロシア語のインターフェースを Windows の言語に合わせて表示します。 |
| ⌨️ **CLI** | オリジナルのコマンドラインオプションはすべてそのまま使えます。 |

## 🚀 使い方

1. [`Awake.exe` をダウンロード](https://github.com/varsoug/awake-standalone/releases/latest/download/Awake.exe)します。
2. 任意の場所（例: `C:\Tools\Awake`）に置いて実行します。
3. トレイの ☕ アイコンをクリックしてモードを選びます。

> [!NOTE]
> アプリのインターフェースは英語とロシア語に対応しています。

> [!TIP]
> exe にコード署名がないため、初回起動時に Windows SmartScreen が警告を表示することがあります。
> **詳細情報 → 実行** をクリックするか、ソースから自分でビルドしてください。

### モード

| モード | 動作 |
|--------|------|
| **オフ** | 通常の電源プランが適用されます |
| **無期限** | オフにするまでスリープしません |
| **時間指定** | 選んだ時間だけスリープせず、その後オフに戻ります |
| **日時指定** | 指定した日時までスリープしません（設定ファイルまたは `-e` で指定） |

### 設定ファイル

設定は `%LOCALAPPDATA%\Awake\settings.json` に保存されます（トレイメニュー → **Open settings file**）。
Awake の実行中に変更するとすぐに反映されます。独自のタイマーは秒単位で指定します。

```json
{
  "properties": {
    "keepDisplayOn": false,
    "mode": 0,
    "customTrayTimes": { "15 分": 900, "1 時間": 3600, "3 時間": 10800 }
  }
}
```

`mode`: `0` = オフ、`1` = 無期限、`2` = 時間指定、`3` = 日時指定。
ログは `%LOCALAPPDATA%\Awake\Logs` に書き込まれ、14 日間保持されます。

## 💻 コマンドライン

引数なしで実行するとトレイアプリが起動します。引数を付けると、Awake は 1 回限りのセッションを実行します。

| オプション | 説明 |
|------------|------|
| `-c`, `--use-config` | 設定ファイルを使うトレイモード（既定） |
| `-d`, `--display-on` | 画面もオンのままにする |
| `-t`, `--time-limit <秒>` | N 秒間スリープを防いでから終了する |
| `-e`, `--expire-at <日付 時刻>` | 指定した日時までスリープを防ぐ |
| `-p`, `--pid <pid>` | 指定したプロセスの実行中はスリープを防ぐ |
| `-u`, `--use-parent-pid` | 親プロセスの実行中はスリープを防ぐ |

```powershell
Awake.exe -t 3600 -d                 # 1 時間、画面オン
Awake.exe -e "2026-12-31 18:00"      # 指定した日時まで
Awake.exe -p 1234                    # プロセス 1234 が動いている間
```

## 🔍 仕組み

Awake は Win32 API
[`SetThreadExecutionState`](https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate)
を `ES_SYSTEM_REQUIRED`（画面も点けておく場合は `ES_DISPLAY_REQUIRED` も）付きで呼び出します。
電源プランは変更せず、Awake をオフにするか終了するとすぐに元に戻ります。`powercfg /requests` で確認できます。

## 🔁 PowerToys Awake との違い

| | PowerToys Awake | このフォーク |
|---|:---:|:---:|
| PowerToys のインストールが必要 | ✅ | ❌ |
| テレメトリ | ✅ | ❌ |
| PowerToys なしのトレイメニュー | ❌ | ✅ |
| 自動起動の切り替え | PowerToys 経由 | ✅ メニューから |
| 単一のポータブル exe | ❌ | ✅ |

## 🔧 ソースからビルド

[.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) が必要です。

```powershell
git clone https://github.com/varsoug/awake-standalone.git
cd awake-standalone
dotnet test src/Awake.UnitTests
dotnet publish src/Awake/Awake.csproj -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true -o publish/win-x64
```

出力は `publish/win-x64/Awake.exe` です。`v1.2.3` のようなタグをプッシュすると、GitHub リリースが自動でビルド・公開されます。

## 🤝 コントリビューション

バグ報告やプルリクエストを歓迎します。大きな変更の場合は、先に
[issue](https://github.com/varsoug/awake-standalone/issues) を作成してください。翻訳の改善も歓迎します。

## 📄 ライセンスとクレジット

Awake は [Den Delimarsky](https://den.dev) が
[Microsoft PowerToys](https://github.com/microsoft/PowerToys) の一部として作成しました。
このフォークは同じ [MIT ライセンス](LICENSE) で配布されており、Microsoft とは無関係で、Microsoft の承認も受けていません。

<div align="center">

このツールがスリープからダウンロードを救ってくれたら、ぜひ ⭐ をお願いします

</div>
