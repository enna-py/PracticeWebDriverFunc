using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MyProject.Tests
{
    [TestFixture]
    public class MainPageTest
    {
        private ChromeDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.epam.com/";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
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
            driver.FindElement(By.XPath("//a[@class = 'top-navigation__item-link js-op' and normalize-space()='Careers']")).Click();
            driver.FindElement(By.Id("new_form_job_search-keyword")).SendKeys(programmingLanguage);

            var locations = driver.FindElement(By.CssSelector(".recruiting-search__location"));

            locations.Click();
            locations.SendKeys(location);
            driver.FindElement(By.XPath("//select[@id = 'new_form_job_search-location']"));
            driver.FindElement(By.Name("remote")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var lastListItem = driver.FindElement(By.XPath("//ul[contains(@class, 'search-result__list')]/li[last()]"));

            lastListItem.FindElement(By.XPath(".//button[text()='View and apply']")).Click();

            string pageText = driver.PageSource;

            Assert.That(pageText.Contains(programmingLanguage), "Page is not contain text that belong to the provided language");
        }

        [Test]
        [TestCase("BLOCKCHAIN/Cloud/Automation")]
        public void GlobalSearchWorkAsExpected(string searchData)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class = 'search-icon dark-icon header-search__search-icon']"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class, 'header-search__panel')]//input[@name = 'q']"))).SendKeys(searchData);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span [@class= 'bth-text-layer']"))).Click();

            var linkElements = driver.FindElements(By.CssSelector(".search-results__title-link"));

            var allowedWords = searchData.Split('/');

            var invalidLinks = linkElements
                .Where(link => !allowedWords.Any(word => link.Text.ToUpper().Contains(word)))
                .ToList();

            foreach (var link in invalidLinks)
            {
                Console.WriteLine("there is no expected words : " + link.Text);
            }

            Assert.IsEmpty(invalidLinks, "words are missed");
        }
    }
}
