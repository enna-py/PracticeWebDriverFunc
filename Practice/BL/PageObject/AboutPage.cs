using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BL.PageObject;
public class AboutPage : BasePage
{
    public AboutPage(IWebDriver driver, WebDriverWait wait)
        : base(driver, wait)
    {

    }

    public IWebElement DownloadButton => driver.FindElement(By.XPath("//a[contains(., 'DOWNLOAD')]"));
}
