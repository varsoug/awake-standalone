// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Awake.Core.Settings
{
    /// <summary>
    /// Reads and writes the standalone settings file. Replaces the PowerToys settings library.
    /// Settings live in %LOCALAPPDATA%\Awake\settings.json.
    /// </summary>
    internal sealed class SettingsUtils
    {
        private const int MaxReadAttempts = 5;
        private const int ReadRetryDelayMs = 50;

        private readonly Lock _fileLock = new();

        internal static SettingsUtils Default { get; } = new();

        internal static string Serialize(AwakeSettings settings)
        {
            return JsonSerializer.Serialize(settings, AwakeJsonContext.Default.AwakeSettings);
        }

        internal string GetSettingsFilePath(string appName)
        {
            _ = appName;
            return Path.Combine(Constants.DataDirectoryPath, "settings.json");
        }

        internal T? GetSettings<T>(string appName)
            where T : class
        {
            string path = GetSettingsFilePath(appName);

            if (!File.Exists(path))
            {
                return null;
            }

            // The file may be read by the FileSystemWatcher callback while it is still being
            // written, so retry a few times before giving up.
            for (int attempt = 1; attempt <= MaxReadAttempts; attempt++)
            {
                try
                {
                    string json;
                    lock (_fileLock)
                    {
                        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                        using StreamReader reader = new(stream);
                        json = reader.ReadToEnd();
                    }

                    return JsonSerializer.Deserialize(json, typeof(T), AwakeJsonContext.Default) as T;
                }
                catch (Exception ex) when (ex is IOException or JsonException)
                {
                    if (attempt == MaxReadAttempts)
                    {
                        Logger.LogError($"Failed to read settings from {path}: {ex.Message}");
                        return null;
                    }

                    Thread.Sleep(ReadRetryDelayMs);
                }
            }

            return null;
        }

        internal void SaveSettings(string jsonSettings, string appName)
        {
            string path = GetSettingsFilePath(appName);

            try
            {
                // Re-format so that the file stays readable for people editing it by hand.
                using JsonDocument document = JsonDocument.Parse(jsonSettings);
                using MemoryStream buffer = new();
                using (Utf8JsonWriter writer = new(buffer, new JsonWriterOptions { Indented = true }))
                {
                    document.RootElement.WriteTo(writer);
                }

                lock (_fileLock)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                    File.WriteAllBytes(path, buffer.ToArray());
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to save settings to {path}: {ex.Message}");
            }
        }
    }
}
