using Automation.Pages.Common;
using OpenQA.Selenium;


namespace Automation.Pages
{
    public class SearchResultsPage : BasePage
    {
        private IWebDriver _driver;
        public SearchResultsPage(IWebDriver webDriver) : base(webDriver)
        {
            _driver = webDriver;
        }

        #region Elements
        private By totalResultsLbl = By.XPath("//*[@class='totalResults']");
        private By ReviewAndCheckOutBtn = By.XPath("//span[contains(text(),'Review & checkout')]");

        private By SearchResults =
            By.XPath("//*[@class='Anchor__styledAnchor-sc-1gq32ow-0 SearchProductTilestyle__SearchProductTileWrapper-sc-7jrh24-0 eePBcM fIdhhe']");
        #endregion
        #region Methods
        public string GetItemName(int index) =>
            FindElement(By.XPath($"//*[@class='SearchComponentstyle__SearchComponentWrapper-sc-1l60lhw-11 iXiHCd']/article[{index}]/a/div[2]/div[@class='text-rating-container']/a")).
                GetAttribute("outerText");

        public string GetItemPrice(int index)=>
            FindElement(By.XPath($"//*[@class='SearchComponentstyle__SearchComponentWrapper-sc-1l60lhw-11 iXiHCd']/article[{index}]/a/div[3]/div[1]/p")).GetAttribute("innerText");

        public void AddAnItemToCart(int index) =>
            FindElement(By.XPath($"//*[@class='SearchComponentstyle__SearchComponentWrapper-sc-1l60lhw-11 iXiHCd']/article[{index}]/a/div[3]/div/div/button")).Click();

        public string GetTotalResults() => FindElement(totalResultsLbl).GetAttribute("innerText");
        public int GetSearchResultsInAPage() => FindElements(SearchResults).Count;

        public void ProceedToReviewAndCheckOut() => FindElement(ReviewAndCheckOutBtn).Click();


        #endregion
    }
}
