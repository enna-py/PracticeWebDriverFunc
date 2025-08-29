using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.PageObject;

namespace BL.PageObject;
public class InsightsPage : BasePage
{
    public InsightsPage(IWebDriver driver, WebDriverWait wait)
        : base(driver, wait)
    {
        
    }

    public IReadOnlyCollection<IWebElement> Arrows => driver.FindElements(By.CssSelector(".slider__right-arrow"));
    public IWebElement SliderText => driver.FindElement(By.CssSelector(".owl-item.active .single-slide__content .text .text-ui-23"));
    public IWebElement ReadMoreButton => driver.FindElement(By.XPath("//a[@href=\"https://www.epam.com/insights/ebook/evolving-into-agentic-ai-turning-theory-into-action\"]"));
    public IWebElement PageLabel => driver.FindElement(By.XPath("//div[@class = 'top-upper-part']//span[@class ='museo-sans-light']"));

    public InsightsPage ClickRightArrow()
    {
        wait.Until(driver =>
        {
            return Arrows.ElementAt(0).Displayed;
        });

        var actions = new Actions(driver);
        actions.Click(Arrows.ElementAt(1));
        actions.Perform();

        return this;
    }
}
