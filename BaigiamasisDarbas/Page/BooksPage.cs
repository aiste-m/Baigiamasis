using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Page
{
    public class BooksPage : PageBase
    {

        private const string booksPageAddress = "https://www.knygos.lt/";
        private const string top1BookAddress = "https://www.knygos.lt/lt/knygos/ten--kur-gieda-veziai/";         
        
        //Top book
        private IWebElement topLink => driver.FindElement(By.LinkText("Top"));                
        private IWebElement top1BookPrice => driver.FindElement(By.CssSelector(".prices > .book-price > .new-price"));
        private IWebElement top1BookInKaunasShop => driver.FindElement(By.CssSelector(".features:nth-child(2) .location-wrapper:nth-child(4) .in-store-statuses-text"));

        //Search
        private IWebElement searchInput => driver.FindElement(By.Id("product-search"));      
        private IWebElement searchBookCountResult => driver.FindElement(By.CssSelector("p > strong:nth-child(1)"));
               
        //Filter
        private IWebElement allBooksSideMenu => driver.FindElement(By.Id("all-products"));
        private IWebElement hobbiesCategory => driver.FindElement(By.LinkText("Pomėgiai"));
        private IWebElement gamesBooksCategory => driver.FindElement(By.LinkText("Žaidimų knygos"));
        private IWebElement topGoodsCheckbox => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(1) > .filter-block:nth-child(3) .checkmark"));
        private IWebElement publishingHouseList => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(3) > .filter-more-label > .filter-title"));
        private IWebElement publishingHouseObuolys => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(3) .item:nth-child(5) .checkmark"));
        private IWebElement balanceInWarehouseCheckbox => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(4) > .filter-block:nth-child(2) .checkmark"));
        private IWebElement filterResult => driver.FindElement(By.CssSelector(".desktop-view > span > strong:nth-child(2)"));
        private IWebElement clearFilterButton => driver.FindElement(By.CssSelector("#filters-desktop > div.pt-3 > button"));              
       
        //Add to cart
        private IWebElement buySearchedAutoTestBookButton => driver.FindElement(By.CssSelector("#add_to_cart_single_add_to_cart_1489740"));
        private IWebElement viewShopCartLink => driver.FindElement(By.LinkText("Peržiūrėti prekių krepšelį"));
        private IWebElement recycleButton => driver.FindElement(By.CssSelector(".ico-recycle-bin"));
        private IWebElement addBooksLink => driver.FindElement(By.LinkText("Išsirinkite prekių"));

        //Cupon test
        private IWebElement couponLink => driver.FindElement(By.LinkText("Dovanų kuponai"));
        private IWebElement customCouponPriceInput => driver.FindElement(By.Id("add_to_cart_single_custom_params_value_value_custom"));
        private IWebElement payMoney => driver.FindElement(By.CssSelector("body > main > div > div:nth-child(2) > div.col.mb-3 > div > div > div > div:nth-child(2) > div:nth-child(1) > div.col.text-right"));

        public BooksPage(IWebDriver webdriver) : base(webdriver)
        {           
        }
        public BooksPage NavigateToPage()
        {
            if (driver.Url != booksPageAddress)
                driver.Url = booksPageAddress;
            return this;
        }
        public BooksPage NavigateToBookByLink()
        {
            if (driver.Url != top1BookAddress)
                driver.Url = top1BookAddress;
            return this;
        }       
        public BooksPage ClickTopButton()
        {
            topLink.Click();
            return this;
        }
        public BooksPage ClickRecycleButton()
        {
            recycleButton.Click();
            return this;
        }
        public BooksPage ClickAddBooksLink()
        {
            addBooksLink.Click();
            return this;
        }
        public BooksPage ClickViewShopCartLink()
        {
            viewShopCartLink.Click();
            return this;
        }
        public BooksPage CheckFirstBookInTopPrice(string price)
        {          
            Assert.AreEqual(price, top1BookPrice.Text, $"Kaina nesutampa, turi buti {price}");           
            return this;
        }
        public BooksPage CheckFirstBookInTopStatusInKaunasStore(string status)
        {        
            Assert.AreEqual(status, top1BookInKaunasShop.Text, $"Statusas nesutampa, turi buti {status}");
            return this;
        }
        public BooksPage CheckFilteredBooksCount(string booksCount)
        {           
            Assert.AreEqual(booksCount, filterResult.Text, $"Kaina nesutampa, turi buti {booksCount}");
            return this;
        }
        public BooksPage DoSearch(string searchText)
        {
            searchInput.Click();
            searchInput.SendKeys(searchText);
            searchInput.SendKeys(Keys.Enter);      
            return this;
        }
        public BooksPage CheckSearchResult(string searchText, string searchResultText)
        {       
            Assert.AreEqual($"Rasta {searchBookCountResult.Text} rezult. užklausai {searchText}", searchResultText, $"Rezultatas nesutampa, turi buti {searchBookCountResult}");
            return this;
        } 
        public BooksPage FilterBook()
        {
            allBooksSideMenu.Click();
            hobbiesCategory.Click();
            gamesBooksCategory.Click();
            topGoodsCheckbox.Click();
            GetWait(2).Until(ExpectedConditions.ElementToBeClickable(publishingHouseList));
            publishingHouseList.Click();
            publishingHouseObuolys.Click();
            balanceInWarehouseCheckbox.Click();
            return this;
        }
        public BooksPage BuyCoupon(string customCouponPrice, string payMoneyResultText)
        {
            allBooksSideMenu.Click();
            couponLink.Click();
            customCouponPriceInput.Click();
            customCouponPriceInput.SendKeys(customCouponPrice);
            customCouponPriceInput.SendKeys(Keys.Enter);          
            Assert.AreEqual(payMoneyResultText, payMoney.Text, $"Rezultatas nesutampa, turi buti {payMoneyResultText}");
            return this;
        }
            public BooksPage ClickClearFilterButton()
        {
            clearFilterButton.Click();
            return this;
        }  
        public BooksPage ClickBuySearchedAutoTestBookButton()
        {
            buySearchedAutoTestBookButton.Click();
            return this;
        }
        public BooksPage AcceptCookies() 
        {
            Cookie myCookie = new Cookie("cookieconsent_status",
                                         "allow",
                                         ".knygos.lt",
                                         "/",
                                         DateTime.Now.AddDays(2));
            driver.Manage().Cookies.AddCookie(myCookie);
            driver.Navigate().Refresh(); 
            return this;
        }
    }
}
