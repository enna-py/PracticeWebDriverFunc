using NUnit.Framework;
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

        Assert.IsEmpty(invalidLinks);
    }

    public void VerifyThatPageContainsSpecificWord(string pageText, string programmingLanguage)
    {
        Assert.That(pageText.Contains(programmingLanguage), "Page is not contain text that belong to the provided language");
    }

    public void VerifyThatFileIsDownloaded(string fullPath)
    {
        bool isFileDownloaded = File.Exists(fullPath);
        Assert.That(new FileInfo(fullPath).Length > 0, "Downloaded file is empty");
    }

    public void VerifyThatTextIsEqual(string actualText, string expectedText)
    {
        Assert.That(actualText.Equals(expectedText), $"Actual text '{actualText}' does not match expected text '{expectedText}'.");
    }
}
