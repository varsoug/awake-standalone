// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.Win32;

namespace Awake.Core
{
    /// <summary>
    /// Manages the "Run at startup" entry in HKCU\Software\Microsoft\Windows\CurrentVersion\Run.
    /// </summary>
    internal static class StartupHelper
    {
        private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        private static string StartupCommand => $"\"{Environment.ProcessPath}\"";

        internal static bool IsEnabled()
        {
            try
            {
                using RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath);
                return key?.GetValue(Constants.AppName) is string value &&
                    string.Equals(value, StartupCommand, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Could not read startup registration: {ex.Message}");
                return false;
            }
        }

        internal static void Toggle()
        {
            try
            {
                using RegistryKey key = Registry.CurrentUser.CreateSubKey(RunKeyPath);

                if (IsEnabled())
                {
                    key.DeleteValue(Constants.AppName, throwOnMissingValue: false);
                    Logger.LogInfo("Removed Awake from startup.");
                }
                else
                {
                    key.SetValue(Constants.AppName, StartupCommand);
                    Logger.LogInfo($"Registered Awake to run at startup: {StartupCommand}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Could not change startup registration: {ex.Message}");
            }
        }
    }
}
