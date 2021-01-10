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
        public BooksPage ClickRecycleButton()
        {
            recycleButton.Click();
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
