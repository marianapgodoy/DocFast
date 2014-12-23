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
        public void DoAgentChange()
        {
            SearchPolicyCreated();

            driver.FindElement(By.XPath("//a[contains(@href, 'javascript:ActionClick();')]")).Click();
            driver.FindElement(By.LinkText("Change Request")).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.Id("ChangeRequestReason0")).SendKeys("Change Request test");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("ChangeRequest")).Click();
            Thread.Sleep(10000);
            
            driver.FindElement(By.XPath("//button[contains(@class, 'ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only')]")).Click();

            Thread.Sleep(2000);
        }
    }
}