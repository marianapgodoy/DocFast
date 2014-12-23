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
        public void DoAgentPrint()
        {
            SearchPolicyCreated();

            driver.FindElement(By.XPath("//a[contains(@href, 'javascript:ActionClick();')]")).Click();
            driver.FindElement(By.LinkText("Print and Hand Deliver")).Click();
            Thread.Sleep(4000);

            if (newPol.PrivateAttachment) // if policy has private attachment
            {
                if (driver.FindElement(By.XPath("//input[contains(@id, 'consumerAttachmentEmail_')]")).GetAttribute("value").Equals(String.Empty))
                {
                    driver.FindElement(By.XPath("//input[contains(@id, 'consumerAttachmentEmail_')]")).SendKeys(DocFast.Resource.Consumer_Email);
                }
            }
            
            driver.FindElement(By.Id("PrintAndHandDeliver")).Click();
            Thread.Sleep(4000);

            driver.SwitchTo().Window(driver.WindowHandles.ToList().Last()); // switches to the new window
            driver.Close(); // Now closes the new window
            Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles.ToList().Last()); // switches back to the parent window
            Thread.Sleep(10000);
            driver.FindElement(By.XPath("//button[contains(@class, 'ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only')]")).Click();
            Thread.Sleep(2000);
        }
    }
}