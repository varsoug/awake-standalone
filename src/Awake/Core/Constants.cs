// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;

namespace Awake.Core
{
    internal static class Constants
    {
        internal const string AppName = "Awake";
        internal const string FullAppName = AppName;
        internal const string TrayWindowId = "AwakeStandalone.MessageWindow";
        internal const string MutexName = "AwakeStandalone.SingleInstance";
        internal const string ExitEventName = "Local\\AwakeStandalone.ExitEvent";
        internal const string BuildRegistryLocation = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";

        // Awake build code name inherited from upstream PowerToys Awake. Used for exact logging.
        // Format of the build ID is: CODENAME_MMDDYYYY, where MMDDYYYY
        // is representative of the date when the last change was made.
        internal const string BuildId = "DIDACT_01182026_STANDALONE";

        internal static string DataDirectoryPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);
    }
}
