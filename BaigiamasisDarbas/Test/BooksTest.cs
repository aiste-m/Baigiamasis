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

        [TestCase("13,53 €", "Yra pakankamai", TestName = "Testing top 1 book price")]
        public static void TestCheckFirstBookInTopPrice(string price, string status)
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .ClickTopButton()
                      .NavigateToBookByLink()
                      .CheckFirstBookInTopPrice(price)
                      .CheckFirstBookInTopStatusInKaunasStore(status);
        }

        [Test]
        public static void TestFilterBook()
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .FilterBook()
                      .CheckFilteredBooksCount("1")
                      .ClickClearFilterButton();
        }


        [TestCase("testavimas", "Rasta 9 rezult. užklausai testavimas", TestName = "Testing search by \"testavimas\" text")]
        public static void TestSearch(string searchText, string searchResultText)
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .DoSearch(searchText, searchResultText)
                      .CheckSearchResult(searchText, searchResultText);
        }
    

        [TestCase("Advanced Automated Software Testing", "Rasta 1 rezult. užklausai Advanced Automated Software Testing", TestName = "Testing search by \"Advanced Automated Software Testing\" text & buy")]
        public static void TestFilterBookAndBuy(string searchText, string searchResultText)
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .DoSearch(searchText, searchResultText)
                      .CheckSearchResult(searchText, searchResultText)
                      .ClickBuySearchedAutoTestBookButton()
                      .ClickViewShopCartLink()
                      .ClickRecycleButton()
                      .ClickAddBooksLink();
        }      

        [Test]
        public static void TestBuyCoupon()
        {
            _booksPage.NavigateToPage()
                      .AcceptCookies()
                      .BuyCoupon("55", "55,00 €")
                      .ClickRecycleButton();
        }
    }
}
