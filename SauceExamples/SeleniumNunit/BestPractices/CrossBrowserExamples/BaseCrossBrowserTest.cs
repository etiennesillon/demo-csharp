﻿using Common;
using Common.SauceLabs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium3.Nunit.Scripts.BestPractices.CrossBrowserExamples
{
    [TestFixture]
    public class BaseCrossBrowserTest
    {
        [SetUp]
        public void ExecuteBeforeEveryTestMethod()
        {
            var sauceConfig = new SauceLabsCapabilities { IsDebuggingEnabled = true };
            SauceLabsCapabilities.BuildName = _sauceBuildName;
            //TODO move into external config
            //TODO add a factory method to create this driver easily
            var localExecution = false;
            if (localExecution)
            {
                Driver = new ChromeDriver();
                _isUsingSauceLabs = false;
            }
            else
            {
                Driver = new WebDriverFactory(sauceConfig).CreateSauceDriver(_browser, _browserVersion, _osPlatform);
                SauceReporter = new SauceJavaScriptExecutor(Driver);
                SauceReporter.SetTestName(TestContext.CurrentContext.Test.Name);
                _isUsingSauceLabs = true;
            }
        }

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            if (_isUsingSauceLabs)
            {
                var isPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
                SauceReporter.LogTestStatus(isPassed);
                SauceReporter.LogMessage("Test finished execution");
                SauceReporter.LogMessage(TestContext.CurrentContext.Result.Message);
            }

            Driver?.Quit();
        }

        private readonly string _browser;
        private readonly string _browserVersion;
        private readonly string _osPlatform;
        public SauceJavaScriptExecutor SauceReporter;
        private bool _isDebuggingOn;
        private static string _sauceBuildName;
        private bool _isUsingSauceLabs;

        public BaseCrossBrowserTest(string browser, string browserVersion, string osPlatform)
        {
            _browser = browser;
            _browserVersion = browserVersion;
            _osPlatform = osPlatform;
        }

        protected BaseCrossBrowserTest(string browser, string browserVersion, string osPlatform, bool isDebuggingOn)
        {
            _browser = browser;
            _browserVersion = browserVersion;
            _osPlatform = osPlatform;
            _isDebuggingOn = isDebuggingOn;
        }

        protected BaseCrossBrowserTest(string browser, string browserVersion, string osPlatform, bool isDebuggingOn,
            string buildName)
        {
            _browser = browser;
            _browserVersion = browserVersion;
            _osPlatform = osPlatform;
            _isDebuggingOn = isDebuggingOn;
            _sauceBuildName = buildName;
        }

        public IWebDriver Driver { get; set; }
    }
}