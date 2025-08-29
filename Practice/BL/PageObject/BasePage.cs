using Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject1.PageObject;

namespace BL.PageObject;
public abstract class BasePage
{
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait;
    public static string downloadPath { get; } = Config.DownloadSettings.FullPath;
    public static string Url { get; } = "https://www.epam.com";
    public BasePage(IWebDriver driver, WebDriverWait wait)
    {
        this.wait = wait ?? throw new ArgumentException(nameof(wait));
        this.driver = driver ?? throw new ArgumentException(nameof(driver));
    }

    public IWebElement AcceptCookies => driver.FindElement(By.Id("onetrust-accept-btn-handler"));
    public IWebElement CookieBanner => driver.FindElement(By.Id("onetrust-banner-sdk"));
    public BasePage AcceptCookie()
    {
        wait.Until(driver =>
        {
            return CookieBanner.Displayed;
        });

        AcceptCookies.Click();

        wait.Until(driver =>
        {
            return !CookieBanner.Displayed;
        });
        return this;
    }

    public void Navigate()
    {
        driver.Url = Config.AppSettings.BaseUrl;
        driver.Manage().Window.Maximize();
    }
}
