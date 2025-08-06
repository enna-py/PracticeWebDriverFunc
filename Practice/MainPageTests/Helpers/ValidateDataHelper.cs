using OpenQA.Selenium;

namespace TestProject1.Helpers;
public class ValidateDataHelper
{
    public void VerifyThatLinksContainsNeededWords(IWebDriver driver, string searchData)
    {
        var linkElements = driver.FindElements(By.CssSelector(".search-results__title-link"));

        var allowedWords = searchData.Split('/');

        var invalidLinks = linkElements
            .Where(link => !allowedWords.Any(word => link.Text.ToUpper().Contains(word)))
            .ToList();

        foreach (var link in invalidLinks)
        {
            Console.WriteLine("there is no expected words : " + link.Text);
        }

        Assert.IsEmpty(invalidLinks, "words are missed");
    }

    public void VerifyThatPageContainsSpecificWord(string pageText, string programmingLanguage)
    {
        Assert.That(pageText.Contains(programmingLanguage), "Page is not contain text that belong to the provided language");
    }
}
