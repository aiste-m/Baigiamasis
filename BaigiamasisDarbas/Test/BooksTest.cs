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
