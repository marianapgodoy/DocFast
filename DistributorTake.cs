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
        public void DoDistributorTake()
        {
            LogOut();
            
            if (newPol.Server == "UAT") // UAT
            {
                if (newPol.Supplier == "Lincoln") // Lincoln
                {
                    username = "53";
                }
                else if (newPol.Supplier == "Standard" || newPol.Supplier == "Standard NY") // The Standard
                {
                    username = "53";
                }
                else if (newPol.Supplier == "Genworth") // Genworth
                {
                    username = "53";
                }
                else // Test Carrier
                {
                    username = "53";
                }

            }
            else if (newPol.Server == "PROD") // PROD
            {
                username = "77";

            }
            else // QA or QA2
            {
                if (newPol.Supplier.Equals("Lincoln")) // Lincoln - from tmigliorisi_DCM1
                {
                    username = "tmigliorisi_DCMA1"; // PolicyEXUser_DCM_4, DCM_4
                }
                else if (newPol.Supplier.Equals("Standard")) // The Standard - from tmigliorisi_DCM2
                {
                    username = "tmigliorisi_DCMA2"; // PolicyEXUser_DCM_4, DCM_4
                }
                else if (newPol.Supplier.Equals("Standard NY")) // The Standard NY - from PolicyEXUser_DCM_4
                {
                    username = "PolicyEXUser_DCMA_4"; // PolicyEXUser_DCM_5, DCM_5
                }
                else if (newPol.Supplier.Equals("Genworth")) // Genworth - from PolicyEXUser_DCM_4
                {
                    username = "PolicyEXUser_DCMA_4"; // PolicyEXUser_DCM_5, DCM_5
                }
                else // Test Carrier - from PolicyEXUser_DCM_4
                {
                    username = "PolicyEXUser_DCMA_4"; // PolicyEXUser_DCM_5, DCM_5
                }

            }
            
            GoToUrl();

            Thread.Sleep(2000);

            Login(username);

            Thread.Sleep(2000);

            SearchPolicyCreated();

            driver.FindElement(By.XPath("//a[contains(@href, 'javascript:ActionClick();')]")).Click();
            driver.FindElement(By.LinkText("Take Approver Ownership")).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.Id("TakeApproverOwnership")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.XPath("//button[contains(@class, 'ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only')]")).Click();
            Thread.Sleep(2000);
        }
    }
}