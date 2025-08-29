using BL.PageObject;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using TestProject1.Helpers;
using TestProject1.PageObject;

namespace BL.PageStepDefinitions;

[Binding]
public sealed class StepDefenitions
{
    private readonly MainPage mainPage;
    private readonly AboutPage aboutPage;
    private readonly CareersPage careersPage;
    private readonly InsightsPage insightsPage;
    private readonly SearchPage searchPage;
    private readonly ServicesPage servicesPage;
    private readonly ValidateDataHelper validateDataHelper;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public StepDefenitions(IWebDriver driver, WebDriverWait wait, ScenarioContext scenarioContext)
    {
        this.wait = wait;
        this.driver = driver;
        servicesPage = new ServicesPage(driver, wait);
        mainPage = new MainPage(driver, wait);
        aboutPage = new AboutPage(driver, wait);
        careersPage = new CareersPage(driver, wait);
        insightsPage = new InsightsPage(driver, wait);
        searchPage = new SearchPage(driver, wait);
        validateDataHelper = new ValidateDataHelper();
        _scenarioContext = scenarioContext;
    }
    protected ILog Log
    {
        get { return LogManager.GetLogger(this.GetType()); }
    }

    [Given(@"I navigate to the website")]
    public void GivenINavigateToTheWebsite()
    {
        Log.Info("Opening main page...");
        mainPage.Navigate();
    }

    [When(@"I click on Careers in the top menu")]
    public void WhenIClickOnCareersInTheTopMenu()
    {
        Log.Info("Clicking on Careers link...");
        mainPage.CareersLink.Click();
    }

    [When(@"I write the name of programming language ""([^""]*)"" in the search box")]
    public void WhenIWriteTheNameOfProgrammingLanguageInTheSearchBox(string programmingLanguage)
    {
        Log.Info($"Entering programming language: {programmingLanguage}...");
        careersPage.KeywordField.SendKeys(programmingLanguage);
    }

    [When(@"I select ""([^""]*)"" from the location dropdown")]
    public void WhenISelectFromTheLocationDropdown(string location)
    {
        Log.Info($"Selecting location: {location}...");
        careersPage.Locations.SendKeys(location);
    }

    [When(@"I select the Remote option")]
    public void WhenISelectTheOption()
    {
        Log.Info("Selecting Remote option...");
        careersPage.WaitUntilElementIsVisiable(careersPage.RemoteOption);
        careersPage.RemoteOption.Click();
    }

    [When(@"I click on the Find button")]
    public void WhenIClickOnTheFindButton()
    {
        Log.Info("Clicking on the Find button...");
        careersPage.WaitUntilElementIsVisiable(careersPage.SubmitButton);
        careersPage.SubmitButton.Click();
    }

    [When(@"I open the latest element in the list of results")]
    public void WhenIOpenTheLatestElementInTheListOfResults()
    {
        Log.Info("Opening the last job listing...");
        careersPage.WaitUntilElementIsVisiable(careersPage.LastListItem);
        careersPage.LastListItem.Click();
    }

    [Then(@"I verify that the programming language in the job description matches ""([^""]*)""")]
    public void ThenIVerifyThatTheProgrammingLanguageInTheJobDescriptionMatches(string language)
    {
        Log.Info("Getting page source...");
        string pageText = driver.PageSource;
        validateDataHelper.VerifyThatPageContainsSpecificWord(pageText, language);
    }

    [When(@"I click on the Magnifier icon")]
    public void WhenIClickOnTheMagnifierIcon()
    {
        Log.Info("Clicking on the magnifier icon...");
        mainPage.WaitUntilElementIsVisiable(mainPage.SearchIcon);
        mainPage.SearchIcon.Click();
    }

    [When(@"I type ""([^""]*)"" into the search box")]
    public void WhenITypeIntoTheSearchBox(string text)
    {
        Log.Info($"Typing '{text}' into the search box...");
        mainPage.Search(text);
    }

    [When(@"I click the Find button")]
    public void WhenIClickTheButton()
    {
        Log.Info("Clicking the Find button...");
        mainPage.WaitUntilElementIsVisiable(mainPage.FindButton);
        mainPage.FindButton.Click();
    }

    [When(@"I accept cookies")]
    public void WhenIAcceptCokies()
    {
        Log.Info("Accepting cookies...");
        aboutPage.AcceptCookie();
    }

    [Then(@"I verify that the search results contain ""([^""]*)""")]
    public void ThenIVerifyThatTheSearchResultsContain(string text)
    {
        Log.Info("Verifying that search results contain the expected text...");
        validateDataHelper.VerifyThatLinksContainsNeededWords(driver, text);
    }

    [When(@"I select About from the top menu")]
    public void WhenISelectAboutFromTheTopMenu(string about)
    {
        Log.Info("Clicking on About link...");
        mainPage.WaitUntilElementIsVisiable(mainPage.AboutLink);
        mainPage.AboutLink.Click();
    }

    [When(@"I scroll down to the EPAM at a Glance section")]
    public void WhenIScrollDownToTheSection()
    {
        Log.Info("Scrolling down to the Download button...");
        var actions = new Actions(driver);
        actions.ScrollToElement(aboutPage.DownloadButton);
        actions.Perform();
    }

    [When(@"I click on the ""([^""]*)"" button")]
    public void WhenIClickOnTheButton(string download)
    {
        Log.Info("Clicking on the Download button...");
        aboutPage.WaitUntilElementIsVisiable(aboutPage.DownloadButton);
        aboutPage.DownloadButton.Click();
    }

    [Then(@"the file ""([^""]*)"" should be downloaded successfully")]
    public void ThenTheFileShouldBeDownloadedSuccessfully()
    {
        Log.Info("Verifying that the file was downloaded successfully...");
        validateDataHelper.VerifyThatFileIsDownloaded(BasePage.downloadPath);
    }

    [When(@"I select Insights from the top menu")]
    public void WhenISelectFromTheTopMenu()
    {
        Log.Info("Clicking on Insights link...");
        wait.Until(driver =>
        {
            return mainPage.InsightsLink.Displayed;
        });

        mainPage.InsightsLink.Click();
    }

    [When(@"I swipe the carousel twice")]
    public void WhenISwipeTheCarouselTwice()
    {
        Log.Info("Clicking right arrow twice...");
        insightsPage
            .ClickRightArrow()
            .ClickRightArrow();
    }

    [When(@"I note the name of the article")]
    public void WhenINoteTheNameOfTheArticle()
    {
        Log.Info("Getting text from carousel...");
        _scenarioContext["sliderText"] = insightsPage.SliderText.Text;
    }

    [When(@"I click on the Read More button")]
    public void WhenIClickOnTheReadMoreButton(string p0)
    {
        Log.Info("Clicking 'Read more' button...");
        insightsPage.WaitUntilElementIsVisiable(insightsPage.ReadMoreButton);
        insightsPage.ReadMoreButton.Click();
    }

    [Then(@"the title of the article should match the title in the carousel")]
    public void ThenTheTitleOfTheArticleShouldMatchTheTitleInTheCarousel()
    {
        Log.Info("Getting text from article page...");
        insightsPage.PageLabel.Text.Equals(_scenarioContext["sliderText"]);
    }

    [When(@"I click on Services in the top menu")]
    public void WhenIClickOnServicesInTheTopMenu()
    {
        Log.Info("Clicking on Services link...");
        mainPage.WaitUntilElementIsVisiable(mainPage.ServicesLink);
        mainPage.ServicesLink.Click();
    }

    [When(@"I select a specific service category ""([^""]*)""")]
    public void WhenISelectASpecificServiceCategory(string service)
    {
        Log.Info($"Selecting service category: {service}...");
        mainPage.WaitUntilElementIsVisiable(mainPage.ServiceCategory(service));
        mainPage.ServiceCategory(service).Click();
    }

    [Then(@"I verify that the page contains the ""([^""]*)"" title")]
    public void ThenIVerifyThatThePageContainsTheCorrectTitle(string title)
    {
        Log.Info("Verifying that the page contains the correct title...");
        mainPage.WaitUntilElementIsVisiable(servicesPage.ServiceTextLine(title));
    }

    [Then(@"I verify that the section Our Related Expertise is displayed on the page")]
    public void ThenIVerifyThatTheSectionIsDisplayedOnThePage()
    {
        Log.Info("Verifying that the 'Our Related Expertise' section is displayed on the page...");
        mainPage.WaitUntilElementIsVisiable(servicesPage.ServiceTextLine("Our Related Expertise"));
    }

}
