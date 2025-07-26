namespace Monefy.Tests
{
    public class BaseTest
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly string _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? Directory.
        GetCurrentDirectory();
        public Scenario scenario;
        private AppiumDriver _driver;
        private AppiumConfig _configDataProvider;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var logsFolder = Path.Combine(_solutionRoot, "TestLogs");
            var logFileName = $"{TestContext.CurrentContext.Test.DisplayName}_{DateTime.Now:yyyyMMdd_HHmmss}";
            LoggerManager.Setup(logsFolder, logFileName);
            _logger.Info("=== ONE GLOBAL SETUP STARTED ===");
            AppInstaller.Install();
        }

        [SetUp]
        public void Setup()
        {
            _configDataProvider = new ConfigDataProvider().GetConfigData(Path.Combine(_solutionRoot, "Monefy.Tests/Config", "DesiredCapabilities.json"));
            _driver = DriverFactory.CreateDriver(_configDataProvider);
            scenario = new Scenario(_driver);
            _logger.Info($"======== Test Case {TestContext.CurrentContext.Test.Name} started ========");
        }

        [TearDown]
        public void TearDown()
        {
            _logger.Info($"Test Case: {TestContext.CurrentContext.Result.Outcome}");

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                _logger.Info($"Test Case failed with message: {TestContext.CurrentContext.Result.Message}");
            }
            _driver?.Dispose();
            _logger.Info($"======== Test Case {TestContext.CurrentContext.Test.Name} finished ========");
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            AppInstaller.Uninstall();
            _driver?.Quit();
            LoggerManager.Shutdown();
            _logger.Info("=== ONE GLOBAL TEARDOWN FINISHED ===");
        }

    }
}
