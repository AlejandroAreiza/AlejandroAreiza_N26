namespace Pet.Tests;

public class BaseTest
{
    public IPetApi PetApi { get; private set; }
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
    private static readonly string _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? Directory.GetCurrentDirectory();

    [SetUp]
    public void Setup()
    {
        var configFilePath = Path.Combine(_solutionRoot, "nlog.config");
        var logsFolder = Path.Combine(_solutionRoot, "TestLogs");
        var logFileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}";
        LoggerManager.Setup(configFilePath, logsFolder, logFileName);
        PetApi = new PetApi().Client;
        _logger.Info($"Test Case {TestContext.CurrentContext.Test.Name} started");
    }

    [TearDown]
    public void TearDown()
    {
        _logger.Info($"Test Case finished with status: {TestContext.CurrentContext.Result.Outcome}");
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            _logger.Info($"Test Case failed with message: {TestContext.CurrentContext.Result.Message}");
        }
    }
    
    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        LoggerManager.Shutdown();
    }
}
