namespace Config.Infraestructure.DTOs;

using System.Text.Json.Serialization;

public class AppiumConfig
{
        [JsonPropertyName("platformName")]
        public PlatformType PlatformName { get; set; }

        [JsonPropertyName("platformVersion")]
        public string PlatformVersion { get; set; }

        [JsonPropertyName("DeviceName")]
        public string DeviceName { get; set; }

        [JsonPropertyName("AppPackage")]
        public string AppPackage { get; set; }

        [JsonPropertyName("AppActivity")]
        public string AppActivity { get; set; }

        [JsonPropertyName("AutomationName")]
        public string AutomationName { get; set; }

        [JsonPropertyName("NoReset")]
        public bool NoReset { get; set; }

        [JsonPropertyName("NewCommandTimeout")]
        public int NewCommandTimeout { get; set; }

        [JsonPropertyName("autoGrantPermissions")]
        public bool AutoGrantPermissions { get; set; }

        [JsonPropertyName("disableWindowAnimation")]
        public bool DisableWindowAnimation { get; set; }

        [JsonPropertyName("udid")]
        public string Udid { get; set; }
}