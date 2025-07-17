namespace Pet.Domain.Clients;

public class PetApi
{
    private readonly IPetApi _client;
    private readonly string _baseUri = Environment.GetEnvironmentVariable("BASE_URI") ?? "http://localhost:8080/api";

    private string _version = "3";
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public PetApi()
    {
        try
        {
            _client = new ApiClientBuilder()
                .WithBaseUri(_baseUri)
                .Version(_version)
                .WithLogging()
                .Build<IPetApi>();
            _logger.Info($"PetApi client created");

        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error initializing PetApi client. Please check your configuration.");
            throw new ArgumentException("Failed to initialize PetApi client. Check your configuration.", ex);
        }
    }
    public IPetApi Client => _client;
}