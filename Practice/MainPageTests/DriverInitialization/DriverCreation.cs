using OpenQA.Selenium.Chrome;

namespace MainPageTests.DriverInitialization;
public static class DriverCreation
{
    public static ChromeDriver CreateDriver()
    {
        var options = new ChromeOptions();
        options.AddArguments("--start-maximized", "--disable-infobars", "--disable-extensions");

        return new ChromeDriver(options);
    }
}
