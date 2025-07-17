namespace Config.Infrastructure;

public class ApiClientBuilder
{
    private string? _baseUri;
    private HttpMessageHandler? _handler;
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public ApiClientBuilder WithBaseUri(string uri)
    {
        if (string.IsNullOrWhiteSpace(uri)) throw new ArgumentException("Base URI cannot be null or empty", nameof(uri));
        _baseUri = uri;
        return this;
    }
    public ApiClientBuilder Version(string version)
    {
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("Version cannot be null or empty", nameof(version));
        _baseUri = $"{_baseUri?.TrimEnd('/')}/v{version.TrimStart('/')}";
        return this;
    }
    public ApiClientBuilder WithAuth()
    {
        // Extent for future auth logic
        return this;
    }
    public ApiClientBuilder WithLogging()
    {
        _handler = new HttpLoggingHandler(_handler ?? new HttpClientHandler());
        return this;
    }
    public T Build<T>()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_baseUri)) throw new InvalidOperationException("Base URI must be set before building the client.");
            var client = new HttpClient(_handler ?? new HttpClientHandler())
            {
                BaseAddress = new Uri(_baseUri)
            };
            _logger.Info($"{typeof(T).Name} client created successfully");
            return RestService.For<T>(client);
        }
        catch (Exception ex)
        {
            _logger.Info($"Failed to create API client: {ex.Message}");
            throw new InvalidOperationException("Failed to create API client", ex);
        }
    }
}
