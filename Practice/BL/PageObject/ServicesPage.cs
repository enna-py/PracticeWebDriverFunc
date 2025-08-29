using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.PageObject;
public class ServicesPage : BasePage
{
    public ServicesPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
    {
    }

    public IWebElement ServiceTextLine(string name) => driver.FindElement(By.XPath($"//div[@class='text']//span[contains(text(),'{name}')]"));
}
