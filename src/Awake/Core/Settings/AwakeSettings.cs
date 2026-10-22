// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using System.Text.Json.Serialization;

namespace Awake.Core.Settings
{
    public class AwakeSettings
    {
        public const string ModuleName = "Awake";

        public AwakeSettings()
        {
            Name = ModuleName;
            Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
            Properties = new AwakeProperties();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("properties")]
        public AwakeProperties Properties { get; set; }
    }
}
