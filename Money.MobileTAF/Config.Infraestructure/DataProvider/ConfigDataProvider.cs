namespace Config.Infraestructure.DataProvider;

public class ConfigDataProvider
{
    public AppiumConfig GetConfigData(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty.", nameof(path));
        try
        {
            var json = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<AppiumConfig>(json);
            if (config == null)
                throw new InvalidOperationException("Failed to deserialize AppiumConfigDto from the provided JSON.");
            return config;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to read or deserialize the configuration file at {path}.", ex);
        }
    }

}
