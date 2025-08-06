using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.PageObject;
public class MainPageSteps
{
    public MainPage mainPage;
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public MainPageSteps(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        this.mainPage = new MainPage(driver, this.wait);
    }

    public MainPageSteps Open()
    {
        driver.Url = MainPage.Url;
        driver.Manage().Window.Maximize();
        return this;
    }

    public MainPageSteps FindCarrersLinks(string programmingLanguage, string location)
    {
        mainPage.CareersLink.Click();

        mainPage.KeywordField.SendKeys(programmingLanguage);
        mainPage.Locations.SendKeys(location);

        mainPage.RemoteOption.Click();
        mainPage.SubmitButton.Click();

        mainPage.LastListItem.FindElement(By.XPath(".//button[text()='View and apply']")).Click();
        return this;
    }

    public MainPageSteps Search(string phrase)
    {
        mainPage.SearchIcon.Click();

        var searchPanelWait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Search panel has not been found"
        };

        searchPanelWait.Until(driver => mainPage.SearchPanel);
        var searchInput = mainPage.SearchInput;

        var clickAndSendKeysActions = new Actions(driver);

        clickAndSendKeysActions.Click(searchInput)
            .Pause(TimeSpan.FromSeconds(1))
            .SendKeys(phrase)
            .Perform();

        mainPage.FindButton.Click();
        return this;
    }
}
