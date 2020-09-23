using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Test
{
    class BooksTest : TestBase
    {
        [TestCase("13,53 €", "Yra pakankamai", TestName = "Testing top 1 book price & status in shop")]
        public static void TestCheckFirstBookInTopPrice(string price, string status)
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .ClickTopButton()
                      .NavigateToBookByLink()
                      .CheckFirstBookInTopPrice(price)
                      .CheckFirstBookInTopStatusInKaunasStore(status);
        }

        [TestCase("1", TestName = "Testing filtered book count")]
        public static void TestFilterBook(string filteredBooksCount)
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .FilterBook()
                      .CheckFilteredBooksCount(filteredBooksCount)
                      .ClickClearFilterButton();
        }

        [TestCase("testavimas", "Rasta 9 rezult. užklausai testavimas", TestName = "Testing search by \"testavimas\" text")]
        public static void TestSearch(string searchText, string searchResultText)
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .DoSearch(searchText)
                      .CheckSearchResult(searchText, searchResultText);
        }    

        [TestCase("Advanced Automated Software Testing",  TestName = "Testing search by \"Advanced Automated Software Testing\" book & buy")]
        public static void TestFilterBookAndBuy(string searchText)
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .DoSearch(searchText)                      
                      .ClickBuySearchedAutoTestBookButton()
                      .ClickViewShopCartLink()
                      .ClickRecycleButton()
                      .ClickAddBooksLink();
        }

        [TestCase("55", "55,00 €", TestName = "Coupon test")]
        public static void TestBuyCoupon(string couponPrice, string moneyResult)
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .BuyCoupon(couponPrice, moneyResult)
                      .ClickRecycleButton();
        }
    }
}
