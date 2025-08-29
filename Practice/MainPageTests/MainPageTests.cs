using log4net;
using log4net.Config;
using MainPageTests.DriverInitialization;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TestProject1.Helpers;
using TestProject1.PageObject;
using System;
using System.IO;

namespace MainPageTests;
public class Tests
{
    [TestFixture]
    public class MainPaageTests
    {
        private ChromeDriver driver;
        private readonly WebDriverWait? wait;
        private MainPageSteps mainPageSteps;
        private ValidateDataHelper validateDataHelper; 
        protected ILog Log
        {
            get { return LogManager.GetLogger(this.GetType()); }
        }

        [SetUp]
        public void SetUp()
        {
            XmlConfigurator.Configure(new FileInfo("Log.config"));
            driver = DriverCreation.CreateDriver();
            mainPageSteps = new MainPageSteps(driver);
            validateDataHelper = new ValidateDataHelper();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        [TestCase("C#", "All Locations")]
        public void CareersSearchWorkAsExpected(string programmingLanguage, string location)
        {
            Log.Info("Opening main page...");
            mainPageSteps.Open();

            Log.Info($"Searching careers with language: {programmingLanguage}, location: {location}...");
            mainPageSteps.FindCarrersLinks(programmingLanguage, location);

            Log.Info("Getting page source...");
            string pageText = driver.PageSource;

            Log.Info($"Validating that page contains word: {programmingLanguage}...");
            validateDataHelper.VerifyThatPageContainsSpecificWord(pageText, programmingLanguage);

            Log.Info("Test CareersSearchWorkAsExpected passed successfully.");
        }

        [Test]
        [TestCase("BLOCKCHAIN/Cloud/Automation")]
        public void GlobalSearchWorkAsExpected(string searchData)
        {
            Log.Info("Opening main page...");
            mainPageSteps.Open();

            Log.Info($"Performing global search with query: {searchData}...");
            mainPageSteps.Search(searchData);

            Log.Info("Validating that search results contain expected word...");
            validateDataHelper.VerifyThatLinksContainsNeededWords(driver, searchData);

            Log.Info("Test GlobalSearchWorkAsExpected passed successfully.");
        }

        [Test]
        [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
        public void DownloadFeatureWorkAsExpected(string fileName)
        {
            var downloadPath = @"C:\\Users\\jvnr3\\Downloads\\";
            var fullPath = Path.Combine(downloadPath, fileName);

            Log.Info("Opening main page...");
            mainPageSteps.Open();

            Log.Info("Navigating to About page...");
            mainPageSteps.GoToAboutPage();

            Log.Info("Accepting cookie banner...");
            mainPageSteps.AcceptCookie();

            Log.Info("Scrolling to download button...");
            mainPageSteps.ScrollToWebElement(mainPageSteps.mainPage.DownloadButton);

            Log.Info("Clicking download button...");
            mainPageSteps.ClickDownloadButton();

            Log.Info($"Validating that file {fileName} was downloaded...");
            validateDataHelper.VerifyThatFileIsDownloaded(fullPath);

            Log.Info("Test DownloadFeatureWorkAsExpected passed successfully.");
        }

        [Test]
        public void TitleOfTheArticleMatchesWithTitleInTheCarousel()
        {
            Log.Info("Opening main page...");
            mainPageSteps.Open();

            Log.Info("Navigating to Insights page...");
            mainPageSteps.ClickInsightsLink();

            Log.Info("Accepting cookie banner...");
            mainPageSteps.AcceptCookie();

            Log.Info("Clicking right arrow twice...");
            mainPageSteps
                .ClickRightArrow()
                .ClickRightArrow();

            Log.Info("Getting text from carousel...");
            var textFromCarousel = mainPageSteps.mainPage.SliderText.Text;

            Log.Info("Clicking 'Read more' button...");
            mainPageSteps.ClickReadMoreButton();

            Log.Info("Getting text from article page...");
            var textFromArticle = mainPageSteps.mainPage.PageLabel.Text;

            Log.Info("Validating that carousel title matches article title...");
            validateDataHelper.VerifyThatTextIsEqual(textFromArticle, textFromCarousel);
            Log.Info("Test TitleOfTheArticleMatchesWithTitleInTheCarousel passed successfully.");
        }
    }
}