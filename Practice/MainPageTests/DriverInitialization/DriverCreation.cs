using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPageTests.DriverInitialization;
public class DriverCreation
{
    public static ChromeDriver CreateDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-infobars");
        options.AddArgument("--disable-extensions");
        return new ChromeDriver(options);
    }
}
