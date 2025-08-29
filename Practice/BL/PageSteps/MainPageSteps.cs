//using OpenQA.Selenium;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Support.UI;

//namespace TestProject1.PageObject;
//public class MainPageSteps
//{
//    public MainPage mainPage;
//    private readonly IWebDriver driver;
//    private readonly WebDriverWait wait;

//    public MainPageSteps(IWebDriver driver)
//    {
//        this.driver = driver;
//        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
//        this.mainPage = new MainPage(driver, this.wait);
//    }

//    public MainPageSteps Open()
//    {
//        driver.Url = MainPage.Url;
//        driver.Manage().Window.Maximize();
//        return this;
//    }

//    public MainPageSteps GoToAboutPage()
//    {
//        mainPage.AboutLink.Click();
//        return this;
//    }
//    public MainPageSteps ClickDownloadButton()
//    {
//        mainPage.DownloadButton.Click();
//        return this;
//    }

//    public MainPageSteps ScrollToWebElement(IWebElement element)
//    {
//        var actions = new Actions(driver);
//        actions.ScrollToElement(element);
//        actions.Perform();
//        return this;
//    }

//    public MainPageSteps FindCarrersLinks(string programmingLanguage, string location)
//    {
//        mainPage.CareersLink.Click();

//        mainPage.KeywordField.SendKeys(programmingLanguage);
//        mainPage.Locations.SendKeys(location);

//        mainPage.RemoteOption.Click();
//        mainPage.SubmitButton.Click();

//        mainPage.LastListItem.FindElement(By.XPath(".//button[text()='View and apply']")).Click();
//        return this;
//    }

//    public MainPageSteps Search(string phrase)
//    {
//        mainPage.SearchIcon.Click();

//        var searchPanelWait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
//        {
//            PollingInterval = TimeSpan.FromSeconds(0.25),
//            Message = "Search panel has not been found"
//        };

//        searchPanelWait.Until(driver => mainPage.SearchPanel);
//        var searchInput = mainPage.SearchInput;

//        var clickAndSendKeysActions = new Actions(driver);

//        clickAndSendKeysActions.Click(searchInput)
//            .Pause(TimeSpan.FromSeconds(1))
//            .SendKeys(phrase)
//            .Perform();

//        mainPage.FindButton.Click();
//        return this;
//    }

//    public MainPageSteps ClickInsightsLink()
//    {
//        wait.Until(driver =>
//        {
//            return mainPage.InsightsLink.Displayed;
//        });

//        mainPage.InsightsLink.Click();

//        return this;
//    }

//    public MainPageSteps AcceptCookie()
//    {
//        wait.Until(driver =>
//        {
//            return mainPage.CookieBanner.Displayed;
//        });
//        mainPage.AcceptCookies.Click();
//        wait.Until(driver =>
//        {
//            return !mainPage.CookieBanner.Displayed;
//        });
//        return this;
//    }

//    public MainPageSteps ClickRightArrow()
//    {
//        wait.Until(driver =>
//        {
//            return mainPage.Arrows.ElementAt(0).Displayed;
//        });

//        var actions = new Actions(driver);
//        actions.Click(mainPage.Arrows.ElementAt(1));
//        actions.Perform();

//        return this;
//    }

//    public MainPageSteps ClickReadMoreButton()
//    {
//        wait.Until(driver =>
//        {
//            return mainPage.ReadMoreButton.Displayed;
//        });

//        mainPage.ReadMoreButton.Click();

//        return this;
//    }
//}