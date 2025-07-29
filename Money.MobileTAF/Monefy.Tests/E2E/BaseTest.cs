using Config.Infraestructure.Driver;

namespace Monefy.Tests
{
    public class BaseTest
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly string _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? Directory.
        GetCurrentDirectory();
        private TestLaunchConfig testLaunchConfigData;
        private AppManager appManager;
        private IMobileDriver _mobileDriver;
        public Scenario scenario;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var logsFolder = Path.Combine(_solutionRoot, "TestLogs");
            var logFileName = $"{TestContext.CurrentContext.Test.DisplayName}_{DateTime.Now:yyyyMMdd_HHmmss}";
            LoggerManager.Setup(logsFolder, logFileName);
            testLaunchConfigData = DataProvider.GetData<TestLaunchConfig>(Path.Combine(_solutionRoot, "Monefy.Tests/Config", "TestLaunchConfigData.json"));
            appManager = new AppManager(testLaunchConfigData.AppConfig);
            appManager.Install();
        }

        [SetUp]
        public void Setup()
        {
            _logger.Info("=========================================================");
            _mobileDriver = new MobileDriver(testLaunchConfigData);
            _logger.Info($"Test Case {TestContext.CurrentContext.Test.Name} started");
            scenario = new Scenario(_mobileDriver);
            scenario.LoginScreen.CompleteOnboardingFlow();

        }

        [TearDown]
        public void TearDown()
        {
            _logger.Info($"Test Case: {TestContext.CurrentContext.Result.Outcome.Status}");
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                _logger.Error($"Test Case {TestStatus.Failed} with message {TestContext.CurrentContext.Result.Message} ");
            }
            _logger.Info($"Test Case finished");
            _mobileDriver.TerminateApp();
            _mobileDriver?.Dispose();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            _logger.Info("=========================================================");
            LoggerManager.Shutdown();
            appManager.Uninstall();
        }
    }
}
