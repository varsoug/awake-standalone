// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Awake.Core
{
    /// <summary>
    /// Replaces the PowerToys RunnerHelper: runs a callback once the target process exits.
    /// </summary>
    internal static class ProcessWatcher
    {
        internal static void WaitForProcessExit(int processId, Action onExit)
        {
            Task.Run(() =>
            {
                try
                {
                    using Process process = Process.GetProcessById(processId);
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Could not wait for process {processId}: {ex.Message}");
                }

                onExit();
            });
        }
    }
}
