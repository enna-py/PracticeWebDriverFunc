using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace BL.PageObject;
public class CareersPage : BasePage
{
    public CareersPage(IWebDriver driver, WebDriverWait wait)
        :base(driver, wait)
    {
        
    }
    public IWebElement KeywordField => wait.Until(ExpectedConditions.ElementIsVisible(By.Id("new_form_job_search-keyword")));
    public IWebElement Locations => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".recruiting-search__location")));
    public IWebElement FormJobSearch => driver.FindElement(By.XPath("//select[@id = 'new_form_job_search-location']"));
    public IWebElement RemoteOption => driver.FindElement(By.XPath("//input[contains(@name, 'remote')]"));
    public IWebElement SubmitButton => driver.FindElement(By.XPath("//button[@type='submit']"));
    public IWebElement LastListItem => driver.FindElement(By.XPath("//ul[contains(@class, 'search-result__list')]/li[last()]"));
}
