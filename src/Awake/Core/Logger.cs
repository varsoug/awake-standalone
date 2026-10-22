// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Awake.Core
{
    /// <summary>
    /// Minimal file logger that replaces the PowerToys ManagedCommon logger.
    /// Logs are written to %LOCALAPPDATA%\Awake\Logs.
    /// </summary>
    internal static class Logger
    {
        private const int LogRetentionDays = 14;

        internal static string LogDirectoryPath { get; } = Path.Combine(Constants.DataDirectoryPath, "Logs");

        internal static void InitializeLogger()
        {
            try
            {
                Directory.CreateDirectory(LogDirectoryPath);

                string logFilePath = Path.Combine(LogDirectoryPath, $"Log_{DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.log");

                Trace.Listeners.Add(new TextWriterTraceListener(new StreamWriter(new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))));
                Trace.AutoFlush = true;

                Task.Run(DeleteOldLogs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to initialize the logger: {ex.Message}");
            }
        }

        internal static void LogInfo(string? message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            Log("Info", message, memberName, sourceFilePath);
        }

        internal static void LogError(string? message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            Log("Error", message, memberName, sourceFilePath);
        }

        private static void Log(string level, string? message, string memberName, string sourceFilePath)
        {
            string caller = $"{Path.GetFileNameWithoutExtension(sourceFilePath)}::{memberName}";
            Trace.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}] [{level}] {caller}");
            Trace.Indent();
            Trace.WriteLine(message ?? string.Empty);
            Trace.Unindent();
        }

        private static void DeleteOldLogs()
        {
            try
            {
                DateTime threshold = DateTime.Now.AddDays(-LogRetentionDays);

                foreach (FileInfo file in new DirectoryInfo(LogDirectoryPath).GetFiles("Log_*.log").Where(f => f.LastWriteTime < threshold))
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to clean up old logs: {ex.Message}");
            }
        }
    }
}
