// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;
using Awake.Core.Models;

namespace Awake.Core.Settings
{
    /// <summary>
    /// Compile-time JSON serialization metadata. Required because the published executable is
    /// trimmed, and trimmed apps cannot use reflection-based JSON serialization.
    /// </summary>
    [JsonSourceGenerationOptions(
        IncludeFields = true,
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true)]
    [JsonSerializable(typeof(AwakeSettings))]
    [JsonSerializable(typeof(SystemPowerCapabilities))]
    internal sealed partial class AwakeJsonContext : JsonSerializerContext
    {
    }
}
