using BL.PageObject;
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
    private readonly ValidateDataHelper validateDataHelper;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public StepDefenitions(IWebDriver driver, WebDriverWait wait, ScenarioContext scenarioContext)
    {
        this.wait = wait;
        this.driver = driver;
        mainPage = new MainPage(driver, wait);
        aboutPage = new AboutPage(driver, wait);
        careersPage = new CareersPage(driver, wait);
        insightsPage = new InsightsPage(driver, wait);
        searchPage = new SearchPage(driver, wait);
        validateDataHelper = new ValidateDataHelper();
        _scenarioContext = scenarioContext;
    }

    [Given(@"I navigate to the website")]
    public void GivenINavigateToTheWebsite()
    {
        mainPage.Navigate();
    }

    [When(@"I click on Careers in the top menu")]
    public void WhenIClickOnCareersInTheTopMenu()
    {
        mainPage.CareersLink.Click();
    }

    [When(@"I write the name of programming language ""([^""]*)"" in the search box")]
    public void WhenIWriteTheNameOfProgrammingLanguageInTheSearchBox(string programmingLanguage)
    {
        careersPage.KeywordField.SendKeys(programmingLanguage);
    }

    [When(@"I select ""([^""]*)"" from the location dropdown")]
    public void WhenISelectFromTheLocationDropdown(string location)
    {
        careersPage.Locations.SendKeys(location);
    }

    [When(@"I select the Remote option")]
    public void WhenISelectTheOption()
    {
        careersPage.RemoteOption.Click();
    }

    [When(@"I click on the Find button")]
    public void WhenIClickOnTheFindButton()
    {
        careersPage.SubmitButton.Click();
    }

    [When(@"I open the latest element in the list of results")]
    public void WhenIOpenTheLatestElementInTheListOfResults()
    {
        careersPage.LastListItem.Click();
    }

    [Then(@"I verify that the programming language in the job description matches ""([^""]*)""")]
    public void ThenIVerifyThatTheProgrammingLanguageInTheJobDescriptionMatches(string language)
    {
        string pageText = driver.PageSource;
        validateDataHelper.VerifyThatPageContainsSpecificWord(pageText, language);
    }

    [When(@"I click on the Magnifier icon")]
    public void WhenIClickOnTheMagnifierIcon()
    {
        mainPage.SearchIcon.Click();
    }

    [When(@"I type ""([^""]*)"" into the search box")]
    public void WhenITypeIntoTheSearchBox(string text)
    {
        mainPage.Search(text);
    }

    [When(@"I click the Find button")]
    public void WhenIClickTheButton()
    {
        mainPage.FindButton.Click();
    }

    [When(@"I accept cookies")]
    public void WhenIAcceptCokies()
    {
        aboutPage.AcceptCookie();
    }

    [Then(@"I verify that the search results contain ""([^""]*)""")]
    public void ThenIVerifyThatTheSearchResultsContain(string text)
    {
        validateDataHelper.VerifyThatLinksContainsNeededWords(driver, text);
    }

    [When(@"I select About from the top menu")]
    public void WhenISelectAboutFromTheTopMenu(string about)
    {
        mainPage.AboutLink.Click();
    }

    [When(@"I scroll down to the EPAM at a Glance section")]
    public void WhenIScrollDownToTheSection()
    {
        var actions = new Actions(driver);
        actions.ScrollToElement(aboutPage.DownloadButton);
        actions.Perform();
    }

    [When(@"I click on the ""([^""]*)"" button")]
    public void WhenIClickOnTheButton(string download)
    {
        aboutPage.DownloadButton.Click();
    }

    [Then(@"the file ""([^""]*)"" should be downloaded successfully")]
    public void ThenTheFileShouldBeDownloadedSuccessfully()
    {
        validateDataHelper.VerifyThatFileIsDownloaded(BasePage.downloadPath);
    }

    [When(@"I select Insights from the top menu")]
    public void WhenISelectFromTheTopMenu()
    {
        wait.Until(driver =>
        {
            return mainPage.InsightsLink.Displayed;
        });

        mainPage.InsightsLink.Click();
    }

    [When(@"I swipe the carousel twice")]
    public void WhenISwipeTheCarouselTwice()
    {
        insightsPage
            .ClickRightArrow()
            .ClickRightArrow();
    }

    [When(@"I note the name of the article")]
    public void WhenINoteTheNameOfTheArticle()
    {
        _scenarioContext["sliderText"] = insightsPage.SliderText.Text;
    }

    [When(@"I click on the Read More button")]
    public void WhenIClickOnTheReadMoreButton(string p0)
    {
        insightsPage.ReadMoreButton.Click();
    }

    [Then(@"the title of the article should match the title in the carousel")]
    public void ThenTheTitleOfTheArticleShouldMatchTheTitleInTheCarousel()
    {
        insightsPage.PageLabel.Text.Equals(_scenarioContext["sliderText"]);
    }

    [When(@"I click on Services in the top menu")]
    public void WhenIClickOnServicesInTheTopMenu()
    {
        throw new PendingStepException();
    }

    [When(@"I select a specific service category ""([^""]*)""")]
    public void WhenISelectASpecificServiceCategory(string service)
    {
        throw new PendingStepException();
    }

    [Then(@"I verify that the page contains the correct title")]
    public void ThenIVerifyThatThePageContainsTheCorrectTitle()
    {
        throw new PendingStepException();
    }

    [Then(@"I verify that the section Our Related Expertise is displayed on the page")]
    public void ThenIVerifyThatTheSectionIsDisplayedOnThePage()
    {
        throw new PendingStepException();
    }

}
