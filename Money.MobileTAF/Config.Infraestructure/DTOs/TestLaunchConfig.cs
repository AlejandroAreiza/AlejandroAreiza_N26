namespace Config.Infraestructure.DTOs;

public class TestLaunchConfig
{
        public required DesiredCapabilities DesiredCapabilities { get; set; }
        public required AppConfig AppConfig { get; set; }
}
public class DesiredCapabilities
{
        [JsonPropertyName("platformName")]
        public PlatformType PlatformName { get; set; }

        [JsonPropertyName("platformVersion")]
        public required string PlatformVersion { get; set; }

        [JsonPropertyName("DeviceName")]
        public required string DeviceName { get; set; }

        [JsonPropertyName("AppPackage")]
        public required string AppPackage { get; set; }

        [JsonPropertyName("AppActivity")]
        public required string AppActivity { get; set; }

        [JsonPropertyName("AutomationName")]
        public required string AutomationName { get; set; }

        [JsonPropertyName("NoReset")]
        public bool NoReset { get; set; }

        [JsonPropertyName("NewCommandTimeout")]
        public int NewCommandTimeout { get; set; }

        [JsonPropertyName("autoGrantPermissions")]
        public bool AutoGrantPermissions { get; set; }

        [JsonPropertyName("disableWindowAnimation")]
        public bool DisableWindowAnimation { get; set; }

        [JsonPropertyName("udid")]
        public required string Udid { get; set; }
}
public class AppConfig
{
        public string AppName { get; set; }
        public string AppId { get; set; }
}