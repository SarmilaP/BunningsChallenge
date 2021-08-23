using Automation.Pages.Common;
using OpenQA.Selenium;

namespace Automation.Pages
{
    public class ReviewCartPage : BasePage
    {
        private IWebDriver _driver;
        public ReviewCartPage(IWebDriver webDriver) : base(webDriver)
        {
            _driver = webDriver;
        }
        #region Elements

        private readonly By reviewCartTitle = By.Id("reviewCart");
        private readonly By itemsInclickAndCollectLbl = By.Id("clickAndCollectCartItems");
        private readonly By removeItemBtn = By.Name("confirm delete button");
        private readonly By cancelBtn = By.Name("cancel delete button");
        private readonly By emptyCartLbl = By.XPath("//*[@class='MuiTypography-root MuiTypography-h1']");
        private readonly By productNameLbl = By.XPath("//*[@class='ProductName ']");
        private readonly By itemCountTxt = By.XPath("//*[@class='quantityEdit']");
        private readonly By itemCountEditTxt = By.XPath("//*[@class='quantityEdit focus-visible']");
        private readonly By itemPriceLbl = By.XPath("//*[@class='Pricestyle__PriceWrap-sc-kv48nd-0 dARVlW productPrice price-medium-size']");

        #endregion

        #region Methods
        private void RemoveItem() => FindElement(removeItemBtn).Click();
        private IWebElement productNameLblt(int index) => FindElements(productNameLbl)[index];
        private IWebElement quantityCountTxt(int index) => FindElements(itemCountTxt)[index];
        private IWebElement quantityCountEditTxt(int index) => FindElements(itemCountEditTxt)[index];
        private IWebElement itemPriceLblInCart(int index) => FindElements(itemPriceLbl)[index];
        public string GetReviewCartTitle()=> FindElement(reviewCartTitle).GetAttribute("innerText");
        public string GetItemsCountInClickAndCollectLabel() => FindElement(itemsInclickAndCollectLbl).GetAttribute("outerText");
        public string GetEmptyCartText() => FindElement(emptyCartLbl).GetAttribute("innerText");
        public string GetItemNameInCart(int index) => productNameLblt(index).GetAttribute("outerText");
        public string GetItemCountInCart(int index) => quantityCountTxt(index).GetAttribute("defaultValue");
        public string GetItemPriceInCart(int index) => itemPriceLblInCart(index).GetAttribute("innerText");
        public void UpdateItemCountInTheCart(int index, int itemCount)
        {
            quantityCountTxt(index).SendKeys(Keys.Control + "a");
            quantityCountEditTxt(index).SendKeys(itemCount.ToString() + Keys.Enter);
            if (itemCount == 0)
                RemoveItem();
        }
        
        #endregion
    }
}
