using Automation.Pages.Common;
using OpenQA.Selenium;

//using BunningsChallenge.Extensions;
//using NUnit.Framework;


namespace Automation.Pages
{
    public class HomePageCart : BasePage
    {
        private IWebDriver _driver;
        public HomePageCart(IWebDriver webDriver) : base(webDriver)
        {
            _driver = webDriver;
        }
        #region Elements
        private By searchWindowTxt = By.Id("custom-css-outlined-input");
        private By CartItemCountLbl = By.XPath("//*[@class='cartItemCount']");
        #endregion

        #region Methods
        public void EnterTextInSearchWindow(string itemToSearch)
        {
            FindElement(searchWindowTxt).SendKeys(Keys.Control + "a" + Keys.Delete);
            FindElement(searchWindowTxt).SendKeys(itemToSearch + Keys.Enter);
        }

        public bool VerifyItemCountInCart() => FindElement(CartItemCountLbl).Text.Contains("1");
        public string GetItemCountInCart() => FindElement(CartItemCountLbl).Text;

        
        #endregion
    }
}
