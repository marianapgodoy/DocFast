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
        public void DoAgentEdeliver()
        {
            SearchPolicyCreated();

            driver.FindElement(By.XPath("//a[contains(@href, 'javascript:ActionClick();')]")).Click();
            driver.FindElement(By.LinkText("e-Sign and e-Deliver to Consumer")).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//input[@class='checkbox']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@id='action-bar-btn-continue']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@id='ds_hldrBdy_navnexttext_btnInline']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@title='Sign Here']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@id='action-bar-bottom-btn-finish']")).Click();

            Thread.Sleep(4000);
            if (driver.FindElement(By.XPath("//input[contains(@id, 'FTFAnswerYes')]")).Displayed)
            {
                if (driver.FindElement(By.XPath("//input[contains(@id, 'FTFAnswerYes')]")).Selected)
                {
                    driver.FindElement(By.XPath("//input[contains(@id, 'FTFAnswerNo')]")).Click();
                }
            }

            Thread.Sleep(4000);
            if (driver.FindElement(By.XPath("//input[contains(@id, 'txtEmail')]")).GetAttribute("value").Equals(String.Empty))
            {
                driver.FindElement(By.XPath("//input[contains(@id, 'txtEmail')]")).SendKeys(DocFast.Resource.Consumer_Email);
            }

            driver.FindElement(By.XPath("//*[@id='AgentResendNotfication']")).Click();
            
            Thread.Sleep(10000);
            driver.FindElement(By.XPath("//button[contains(@class, 'ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only')]")).Click();
            Thread.Sleep(2000);
        }
    }
}