namespace Config.Infrastructure;

public class HttpLoggingHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("➡️ HTTP Request:");
            sb.AppendLine($"Method: {request.Method}");
            sb.AppendLine($"URI: {request.RequestUri}");

            if (request.Content != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                sb.AppendLine("Request Body:");
                sb.AppendLine(requestBody);
            }

            _logger.Info(sb.ToString());

            var response = await base.SendAsync(request, cancellationToken);

            var responseSb = new StringBuilder();
            responseSb.AppendLine("⬅️ HTTP Response:");
            responseSb.AppendLine($"Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");

            if (response.Content != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                responseSb.AppendLine("Response Body:");
                responseSb.AppendLine(responseBody);
            }

            _logger.Info(responseSb.ToString());

            return response;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Exception during HTTP call to {request.RequestUri}");
            throw;
        }
    }
}