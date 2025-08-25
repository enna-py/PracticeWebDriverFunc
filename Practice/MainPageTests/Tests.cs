using MainPageTests.DriverInitialization;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Net;
using System.Text.RegularExpressions;
using TestProject1.Helpers;
using TestProject1.PageObject;

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
        [SetUp]
        public void SetUp()
        {
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
            mainPageSteps
                .Open()
                .FindCarrersLinks(programmingLanguage, location);

            string pageText = driver.PageSource;

            validateDataHelper.VerifyThatPageContainsSpecificWord(pageText, programmingLanguage);
        }
        [Test]
        [TestCase("BLOCKCHAIN/Cloud/Automation")]
        public void GlobalSearchWorkAsExpected(string searchData)
        {
            mainPageSteps
                .Open()
                .Search(searchData);

            validateDataHelper.VerifyThatLinksContainsNeededWords(driver, searchData);
        }

        [Test]
        [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
        public void DownloadFeatureWorkAsExpected(string fileName)
        {
            var downloadPath = @"C:\\Users\\jvnr3\\Downloads\\";
            var fullPath = Path.Combine(downloadPath, fileName);

            mainPageSteps
                .Open()
                .GoToAboutPage()
                .AcceptCookie()
                .ScrollToWebElement(mainPageSteps.mainPage.DownloadButton)
                .ClickDownloadButton();

            validateDataHelper.VerifyThatFileIsDownloaded(fullPath);
        }

        [Test]
        public void TitleOfTheArticleMatchesWithTitleInTheCarousel()
        {
            mainPageSteps
                .Open()
                .ClickInsightsLink()
                .AcceptCookie()
                .ClickRightArrow()
                .ClickRightArrow();

            var textFromCarousel = mainPageSteps.mainPage.SliderText.Text;

            mainPageSteps
                .ClickReadMoreButton();

            var textFromArticle = mainPageSteps.mainPage.PageLabel.Text;

            validateDataHelper.VerifyThatTextIsEqual(textFromArticle, textFromCarousel);
        }
    }
}
