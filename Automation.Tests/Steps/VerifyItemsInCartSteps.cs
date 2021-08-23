using System.Text.RegularExpressions;
using Automation.Core;
using Automation.Pages;
using BoDi;
using NUnit.Framework;
using TechTalk.SpecFlow;
using OpenQA.Selenium;

namespace BunningsChallenge.Test
{
    [Binding]
    public class VerifyItemsInCartSteps
    {
        private IWebDriver _driver;
        private readonly IObjectContainer _objectContainer;
        private HomePageCart _homePage;
        private SearchResultsPage _searchResultPage;
        private ReviewCartPage _reviewCartPage;
        private ScenarioContext _scenarioContext;
        public VerifyItemsInCartSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _driver = _objectContainer.Resolve<IWebDriver>();
            _homePage = new HomePageCart(_driver);
            _searchResultPage = new SearchResultsPage(_driver);
            _reviewCartPage = new ReviewCartPage(_driver);
            _scenarioContext = scenarioContext;
        }

        
        [Given(@"I have navigated to the Bunnings site")]
        public void GivenIHaveNavigatedToTheBunningsSite()
        {
            _driver.Navigate().GoToUrl("https://www.bunnings.com.au/");
            _driver.Manage().Window.Maximize();
        }

        [Given(@"I search for (\w*)")]
        [When(@"I search for (\w*)")]
        public void GivenISearchForHammer(string searchItem)
        {
            _homePage.EnterTextInSearchWindow(searchItem);
        }

        [When(@"I search for item and add the first item to cart")]
        public void GivenISearchForItemAndAddTheFirstItemToCart(Table table)
        {
            var index = 1;
            foreach (var row in table.Rows)
            {
                _homePage.EnterTextInSearchWindow(row["SearchItem"]);
                _scenarioContext[$"ItemPrice{index}"] = _searchResultPage.GetItemPrice(1);
                _scenarioContext[$"ItemName{index++}"] = _searchResultPage.GetItemName(1);
                _searchResultPage.AddAnItemToCart(1);
            }
            
        }


        [Given(@"I search for (\w*) and add it to the cart")]
        public void GivenISearchForHammerAndAddItToTheCart(string searchItem)
        {
            _homePage.EnterTextInSearchWindow(searchItem);
            _scenarioContext["ItemPrice"] = _searchResultPage.GetItemPrice(1);
            _scenarioContext["ItemName"] = _searchResultPage.GetItemName(1);
            _searchResultPage.AddAnItemToCart(1);
            _searchResultPage.ProceedToReviewAndCheckOut();

        }

        [When(@"I add the first item listed to the cart")]
        public void WhenIAddTheFirstItemListedToTheCart()
        {
            _scenarioContext["ItemPrice"] = _searchResultPage.GetItemPrice(1);
            _scenarioContext["ItemName"] = _searchResultPage.GetItemName(1);
            _searchResultPage.AddAnItemToCart(1);
            _searchResultPage.ProceedToReviewAndCheckOut();
        }

        [When(@"I update the (.*) in the cart")]
        public void WhenIUpdateTheInTheCart(int itemCount)
        {
            
            _reviewCartPage.UpdateItemCountInTheCart(0, itemCount);
        }

        [Then(@"I verify the result in the cart macth the (.*)")]
        public void ThenIVerifyTheResultInTheCartMacthTheSelectedItem(int itemCount)
        {
            var itemPrice = _scenarioContext["ItemPrice"].ToString().Remove(0, 1);
            var expectedPrice = "$" + float.Parse(itemPrice) * itemCount;
            if (itemCount > 1)
                expectedPrice = expectedPrice + "\r\n\r\nItem price: $" + itemPrice;
            var itemName = _scenarioContext["ItemName"].ToString();
            VerifyItemsInCartMatchTheItemAddedToTheCart(itemCount, 0, expectedPrice, itemName);
        }

        [Then(@"I verify the items in the cart match the (.*) items added")]
        public void ThenIVerifyTheItemsInTheCartMatchTheItemsAdded(int itemsInCart)
        {
            _searchResultPage.ProceedToReviewAndCheckOut();
            for (var i = 1; i <= itemsInCart; i++)
            {
                var itemPrice = _scenarioContext[$"ItemPrice{i}"].ToString().Remove(0, 1);
                var expectedPrice = "$" + float.Parse(itemPrice) * 1;
                var itemName = _scenarioContext[$"ItemName{i}"].ToString();
                VerifyItemsInCartMatchTheItemAddedToTheCart(itemsInCart, i-1, expectedPrice, itemName);
            }
        }

        [Then(@"I verify the results of the search are displayed")]
        public void ThenIVerifyTheResultsOfTheSearchAreDisplayed()
        {
            var numberOfItemsForSearch = Regex.Match(_searchResultPage.GetTotalResults(), @"\d+").Value;
            Assert.IsTrue(int.Parse(numberOfItemsForSearch) > 0, "Search did not return any results and it is not true");
            Assert.IsFalse(_searchResultPage.GetSearchResultsInAPage()>36, "Do not display too many items in a page ");
        }


        public void VerifyItemsInCartMatchTheItemAddedToTheCart(int itemCount, int index, string expectedPrice, string itemName)
        {
            if (itemCount != 0 )
            {
                Assert.AreEqual("1. Review cart", _reviewCartPage.GetReviewCartTitle(), $"Title does not match {_reviewCartPage.GetReviewCartTitle()}");
                Assert.AreEqual($"Items for Click & Collect ({itemCount})", _reviewCartPage.GetItemsCountInClickAndCollectLabel(), "Item count in click and collect label do not match");
                Assert.AreEqual(itemName, _reviewCartPage.GetItemNameInCart(index), "Item name in the cart does not match the item name selected");
                Assert.AreEqual(expectedPrice, _reviewCartPage.GetItemPriceInCart(index), "Item price in the cart does not match the item selected");
            }
            else
                Assert.AreEqual("Your cart is empty!", _reviewCartPage.GetEmptyCartText(), "Empty cart text does not match");
        }

    }
}
