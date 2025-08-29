using BL.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestProject1.PageObject;
public class MainPage : BasePage
{
    public MainPage(IWebDriver driver, WebDriverWait wait)
        : base(driver, wait)
    {
    }

    public IWebElement CareersLink => driver.FindElement(By.XPath("//a[@class = 'top-navigation__item-link js-op' and normalize-space()='Careers']"));
    public IWebElement SearchIcon => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class = 'search-icon dark-icon header-search__search-icon']")));
    public IWebElement SearchPanel => wait.Until(driver => driver.FindElement(By.ClassName("header-search__panel")));
    public IWebElement SearchInput => SearchPanel.FindElement(By.Name("q"));
    public IWebElement FindButton => SearchPanel.FindElement(By.XPath(".//*[@class='search-results__input-holder']/following-sibling::button"));
    public IWebElement AboutLink => driver.FindElement(By.XPath("//a[@class = 'top-navigation__item-link js-op' and normalize-space()='About']"));
    public IWebElement InsightsLink => driver.FindElement(By.XPath("//a[@class = 'top-navigation__item-link js-op' and normalize-space()='Insights']"));

    public void Search(string phrase)
    {
        SearchIcon.Click();

        var searchPanelWait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Search panel has not been found"
        };

        searchPanelWait.Until(driver => SearchPanel);
        var searchInput = SearchInput;

        var clickAndSendKeysActions = new Actions(driver);

        clickAndSendKeysActions.Click(searchInput)
            .Pause(TimeSpan.FromSeconds(1))
            .SendKeys(phrase)
            .Perform();

        FindButton.Click();
    }
}