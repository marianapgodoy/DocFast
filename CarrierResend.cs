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
        public void DoCarrierResend()
        {
            SearchPolicyCreated();

            driver.FindElement(By.XPath("//a[contains(@href, 'javascript:ActionClick();')]")).Click();
            driver.FindElement(By.LinkText("Resend")).Click();
            Thread.Sleep(4000);

            if (!newPol.AgentRole.Equals("CC") && !newPol.AgentRole.Equals("CCRO") && !newPol.Flow.Equals("DTC"))
            {
                driver.FindElement(By.XPath(".//*[@id='ResendNotfication']")).Click();
            }
            else
            {
                Thread.Sleep(2000);

                if (newPol.DistributorRole == "App")
                {
                    driver.FindElement(By.XPath(".//*[@id='ResendNotfication']")).Click();
                }
                else
                {
                    driver.FindElement(By.XPath(".//*[@id='AgentResendNotfication']")).Click();
                }
            }

            Thread.Sleep(10000);
            driver.FindElement(By.XPath("//button[contains(@class, 'ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only')]")).Click();
            Thread.Sleep(2000);
        }
    }
}