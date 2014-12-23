using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Data;
using System.Data.OleDb;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace DocFast
{
    [TestFixture]
    public partial class RT
    {
        public void DoCarrierCancel()
        {
            SearchPolicyCreated();

            driver.FindElement(By.XPath("//a[contains(@href, 'javascript:ActionClick();')]")).Click();
            driver.FindElement(By.LinkText("Cancel Delivery")).Click();
            Thread.Sleep(4000);

            if (driver.FindElement(By.Id("RequestSelected0")).Displayed)
            {
                IWebElement CancelReason = driver.FindElement(By.XPath(".//*[@id='RequestSelected0']"));
                foreach (IWebElement option in CancelReason.FindElements(By.TagName("option")))
                {
                    if (option.GetAttribute("value") == "23") // 3 Other, 19 Reissue as paper, 20 Coverage not as expected, 21 Too expensive, 22 No longer interested, 23 Life situation change (ex: divorce, death, move etc.) etc.
                    {
                        option.Click();
                        break;
                    }
                }
            }

            Thread.Sleep(2000);
            driver.FindElement(By.Id("DeclinedOfferReason0")).SendKeys("Cancel test");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("ButtonSubmitDecline")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.XPath("//button[contains(@class, 'ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only')]")).Click();
            Thread.Sleep(2000);
        }
    }
}