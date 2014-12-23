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
        
        private void DistributorPolicyRole()
        {
            Thread.Sleep(2000);

            IWebElement distributorPolicyRole = driver.FindElement(By.XPath(".//*[@id='distributorPolicyRole']"));

            if (newPol.DistributorRole == "App")
            {
                foreach (IWebElement option in distributorPolicyRole.FindElements(By.TagName("option")))
                {
                    if (option.GetAttribute("value") == "1")
                    {
                        option.Click();
                        break;
                    }
                }
            }
            else if (newPol.DistributorRole == "CC")
            {
                foreach (IWebElement option in distributorPolicyRole.FindElements(By.TagName("option")))
                {
                    if (option.GetAttribute("value") == "3")
                    {
                        option.Click();
                        break;
                    }
                }
            }
            else if (newPol.DistributorRole == "CCRO")
            {
                foreach (IWebElement option in distributorPolicyRole.FindElements(By.TagName("option")))
                {
                    if (option.GetAttribute("value") == "4")
                    {
                        option.Click();
                        break;
                    }
                }
            }

            Thread.Sleep(2000);
        }

        private void AgentPolicyRole()
        {
            Thread.Sleep(2000);

            IWebElement agentPolicyRole = driver.FindElement(By.XPath(".//*[@id='agentPolicyRole']"));

            if (newPol.AgentRole == "App")
            {
                foreach (IWebElement option in agentPolicyRole.FindElements(By.TagName("option")))
                {
                    if (option.GetAttribute("value") == "1")
                    {
                        option.Click();
                        break;
                    }
                }
            }
            else if (newPol.AgentRole == "CC")
            {
                foreach (IWebElement option in agentPolicyRole.FindElements(By.TagName("option")))
                {
                    if (option.GetAttribute("value") == "3")
                    {
                        option.Click();
                        break;
                    }
                }
            }
            else if (newPol.AgentRole == "CCRO")
            {
                foreach (IWebElement option in agentPolicyRole.FindElements(By.TagName("option")))
                {
                    if (option.GetAttribute("value") == "4")
                    {
                        option.Click();
                        break;
                    }
                }
            }
        }

       
    }
}