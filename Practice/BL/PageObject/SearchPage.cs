using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace BL.PageObject;
public class SearchPage: BasePage
{
    public SearchPage(IWebDriver driver, WebDriverWait wait)
        : base(driver, wait)
    {

    }

    public ReadOnlyCollection<IWebElement> linkElements => wait.Until(driver => driver.FindElements(By.CssSelector(".search-results__title-link")));
}
