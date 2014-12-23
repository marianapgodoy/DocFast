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
        public void DoCarrierTransfer()
        {
            SearchPolicyCreated();

            driver.FindElement(By.XPath("//a[contains(@href, 'javascript:ActionClick();')]")).Click();
            driver.FindElement(By.LinkText("Transfer for Approval")).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.Id("CaseManagerSearchEmail")).SendKeys("tmi");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath(".//*[@id='CaseManagerSearchButton']")).Click();

            Thread.Sleep(2000);

            string caseManagerID;
            
            if (newPol.Server == "UAT") // UAT
            {
                if (newPol.Supplier == "Lincoln") // Lincoln
                {
                    caseManagerID = "53";
                }
                else if (newPol.Supplier == "Standard" || newPol.Supplier == "Standard NY") // The Standard
                {
                    caseManagerID = "53";
                }
                else if (newPol.Supplier == "Genworth") // Genworth
                {
                    caseManagerID = "53";
                }
                else // Test Carrier
                {
                    caseManagerID = "53";
                }

            }
            else if (newPol.Server == "PROD") // PROD
            {
                caseManagerID = "77";

            }
            else // QA or QA2
            {
                if (newPol.Supplier.Equals("Lincoln")) // Lincoln - from tmigliorisi_CCM1
                {
                    caseManagerID = "300"; // Migliorisi, Teresita CCMA1
                }
                else if (newPol.Supplier.Equals("Standard")) // The Standard - from tmigliorisi_CCM2
                {
                    caseManagerID = "326"; // Migliorisi, Teresita CCMA2
                }
                else if (newPol.Supplier.Equals("Standard NY")) // The Standard - from tmigliorisi_CCM
                {
                    caseManagerID = "542"; // Migliorisi, Teresita CCMA
                }
                else if (newPol.Supplier.Equals("Genworth")) // Genworth - from tmigliorisi_CCM10
                {
                    caseManagerID = "1722"; // Migliorisi, Teresita CCMA10
                }
                else // Test Carrier - from PolicyEXUser_CCM_3
                {
                    caseManagerID = "53"; // PolicyEXUser_CCM_5
                }

            }

            IWebElement CCM = driver.FindElement(By.XPath(".//*[@id='divCaseManagerList']"));
            foreach (IWebElement option in CCM.FindElements(By.Name("CaseManagerListRadio")))
            {
                if (option.GetAttribute("value") == caseManagerID) 
                {
                    option.Click();
                    break;
                }
            }

            driver.FindElement(By.Id("SelectCaseManager")).Click();

            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//div[@class='ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable ui-resizable']/div[@class='ui-dialog-buttonpane ui-widget-content ui-helper-clearfix']/button[1]")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.XPath("//button[contains(@class, 'ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only')]")).Click();
            Thread.Sleep(2000);
        }
    }
}