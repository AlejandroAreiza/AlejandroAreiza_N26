namespace Config.Infraestructure.DataProviders;

public static class DataProvider
{
    public static T GetData<T>(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty.", nameof(path));
        try
        {
            var json = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<T>(json);
            if (data == null)
                throw new InvalidOperationException($"Failed to deserialize {nameof(T)} from the provided file");
            return data;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to read or deserialize the configuration file at {path}.", ex);
        }
    }

}
