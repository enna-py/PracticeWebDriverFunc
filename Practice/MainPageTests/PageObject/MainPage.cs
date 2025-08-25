using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestProject1.PageObject;
public class MainPage
{
    public static string Url { get; } = "https://www.epam.com";

    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;
    public MainPage(IWebDriver driver, WebDriverWait wait)
    {
        this.wait = wait ?? throw new ArgumentException(nameof(wait));
        this.driver = driver ?? throw new ArgumentException(nameof(driver));
    }

    public IWebElement CareersLink => driver.FindElement(By.XPath("//a[@class = 'top-navigation__item-link js-op' and normalize-space()='Careers']"));
    public IWebElement KeywordField => wait.Until(ExpectedConditions.ElementIsVisible(By.Id("new_form_job_search-keyword")));
    public IWebElement Locations => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".recruiting-search__location")));
    public IWebElement FormJobSearch => driver.FindElement(By.XPath("//select[@id = 'new_form_job_search-location']"));
    public IWebElement RemoteOption => driver.FindElement(By.XPath("//input[contains(@name, 'remote')]"));
    public IWebElement SubmitButton => driver.FindElement(By.XPath("//button[@type='submit']"));
    public IWebElement LastListItem => driver.FindElement(By.XPath("//ul[contains(@class, 'search-result__list')]/li[last()]"));
    public IWebElement SearchIcon => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class = 'search-icon dark-icon header-search__search-icon']")));
    public IWebElement SearchPanel => wait.Until(driver => driver.FindElement(By.ClassName("header-search__panel")));
    public IWebElement SearchInput => SearchPanel.FindElement(By.Name("q"));
    public IWebElement FindButton => SearchPanel.FindElement(By.XPath(".//*[@class='search-results__input-holder']/following-sibling::button"));
    public IWebElement AboutLink => driver.FindElement(By.XPath("//a[@class = 'top-navigation__item-link js-op' and normalize-space()='About']"));
    public IWebElement EpamAtGlanceTitle => driver.FindElement(By.XPath("//div[@class='text-ui-23']//span[contains(text(),'EPAM at')]"));
    public IWebElement DownloadButton => driver.FindElement(By.XPath("//a[contains(., 'DOWNLOAD')]"));
    public IWebElement AcceptCookie => driver.FindElement(By.Id("onetrust-accept-btn-handler"));
    public IWebElement CookieBanner => driver.FindElement(By.Id("onetrust-banner-sdk"));
}
