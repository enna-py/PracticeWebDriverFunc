using OpenQA.Selenium.Chrome;

namespace MainPageTests.DriverInitialization;
public static class DriverCreation
{
    public static ChromeDriver CreateDriver(string downloadPath = @"C:\\Users\\jvnr3\\Downloads")
    {
        var options = new ChromeOptions();
        options.AddArguments("--start-maximized", "--disable-infobars", "--disable-extensions", "--disable-notifications");
        options.AddUserProfilePreference("download.default_directory", downloadPath);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("safebrowsing.enabled", true);

        return new ChromeDriver(options);
    }
}