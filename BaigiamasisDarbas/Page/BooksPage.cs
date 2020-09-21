using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Page
{
    public class BooksPage : PageBase
    {

        private const string booksPageAddress = "https://www.knygos.lt/";

        private const string top1BookAddress = "https://www.knygos.lt/lt/knygos/ten--kur-gieda-veziai/";   
       
        private const string markersPageAddress = "https://www.knygos.lt/lt/knygos/zanras/kanceliarines-prekes-zymekliai/";
        private IWebElement topLink => driver.FindElement(By.LinkText("Top"));

        private IWebElement top1BookLink => driver.FindElement(By.LinkText("TEN, KUR GIEDA VĖŽIAI. Pasaulį parklupdžiusi istorija apie atsiskyrėlę iš Šiaurės Karolinos pelkių. Perkamiausia 2019 m. knyga – jau lietuviškai!"));
        
        private IWebElement firstBookinTop => driver.FindElement(By.CssSelector("# homepage > div.container > div > div.col-10.products-holder-wrapper > section > div:nth-child(1) > span.badges > span.badge.top10-book-place > span.top-number"));
        private IWebElement top1BookPrice => driver.FindElement(By.CssSelector(".prices > .book-price > .new-price"));
        private IWebElement top1BookName => driver.FindElement(By.CssSelector("h1 > span:nth-child(1)"));
        private IWebElement top1BookInKaunasShop => driver.FindElement(By.CssSelector(".features:nth-child(2) .location-wrapper:nth-child(4) .in-store-statuses-text"));


        private IWebElement searchInput => driver.FindElement(By.Id("product-search"));
        private IWebElement searchResult => driver.FindElement(By.CssSelector("p:nth-child(3)"));


        private IWebElement allBooksSideMenu => driver.FindElement(By.Id("all-products"));
        private IWebElement hobbiesCategory => driver.FindElement(By.LinkText("Pomėgiai"));
        private IWebElement gamesBooksCategory => driver.FindElement(By.LinkText("Žaidimų knygos"));
        private IWebElement recomendationCheckbox => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(1) > .filter-block:nth-child(5) .checkmark"));
        private IWebElement priceSliderMinValue => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(2) .min-slider-handle"));

        //  private IWebElement priceSlider => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(2) .slider-selection"));

        private IWebElement priceSliderMaxValue => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(2) .max-slider-handle"));
        private IWebElement isInWarehouseCheckbox => driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(4) > .filter-block:nth-child(2) .checkmark"));
        private IWebElement clearFilterButton => driver.FindElement(By.CssSelector("#filters-desktop > div.pt-3 > button"));

        //FIND BRANGIAUSIA MARKERI

        private IReadOnlyCollection<IWebElement> markers => driver.FindElements(By.ClassName("product"));
        private IWebElement markerPrice => driver.FindElement(By.CssSelector("# homepage > div.container > div.categories.row.books-row > div.col-10.products-holder-wrapper > section > div:nth-child(3) > a > div.book-properties-block > span.book-properties.book-price"));


        //ADD to CARt
        private IWebElement buySearchedAutoTestBookButton => driver.FindElement(By.CssSelector("#add_to_cart_single_add_to_cart_1489740"));

        private IWebElement viewShopCartLink => driver.FindElement(By.LinkText("Peržiūrėti prekių krepšelį"));

        private IWebElement recycleButton => driver.FindElement(By.CssSelector(".ico-recycle-bin"));
        private IWebElement addBooksLink => driver.FindElement(By.LinkText("Išsirinkite prekių"));

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
        public BooksPage NavigateToMarkersPage()
        {
            if (driver.Url != markersPageAddress)
                driver.Url = markersPageAddress;
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
            string priceFullText = price;
            Assert.AreEqual(priceFullText, top1BookPrice.Text, $"Kaina nesutampa, turi buti {price}");           
            return this;
        }


        public BooksPage CheckFirstBookInTopStatusInKaunasStore(string status)
        {
            string statusFullText = status;
            Assert.AreEqual(statusFullText, top1BookInKaunasShop.Text, $"Statusas nesutampa, turi buti {status}");
            return this;
        }

        public BooksPage DoSearch(string searchText)
        {
            searchInput.Click();
            searchInput.SendKeys(searchText);
            searchInput.SendKeys(Keys.Enter);
            return this;
        }

        public BooksPage CheckSearchResult(string searchResultText)
        {
            string searchResultFullText = searchResultText;
            Assert.AreEqual(searchResultFullText, searchResult.Text, $"Rezultatas nesutampa, turi buti {searchResult}");
            return this;
        }

        public BooksPage FilterBook()
        {
            allBooksSideMenu.Click();
            hobbiesCategory.Click();
            gamesBooksCategory.Click();
            recomendationCheckbox.Click();
            SetMinPriceSliderValue();
            return this;
        }

        public BooksPage SetMinPriceSliderValue()
        {

            var element = driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(2) .min-slider-handle"));
            //   var targetElement = driver.FindElement(By.CssSelector(".filter-wrapper:nth-child(2) .slider-selection"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element, 0, 0).ClickAndHold()
                .MoveToElement(element, 4, 0).Release().Perform();            
            return this;            
        }

        public BooksPage ClickClearFilterButton()
        {
            clearFilterButton.Click();
            return this;
        }

        public BooksPage FindMarkerWithHighestPrice()

        {
            foreach (IWebElement singleMarker in markers)
            {
                singleMarker.GetAttribute("value");

                if (singleMarker.GetAttribute("value") == "5.16")
                {
                    singleMarker.Click();
                }
                
            }
            return this;

        }

        public BooksPage ClickBuySearchedAutoTestBookButton()
        {
            buySearchedAutoTestBookButton.Click();
            return this;
        }
    }
}
