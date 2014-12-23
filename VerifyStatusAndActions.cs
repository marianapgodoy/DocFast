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

        private void VerifyStatusAndActions(string performed)
        {
            Thread.Sleep(3000);

            driver.FindElement(By.PartialLinkText(polNbr)).Click();

            if (currentUser.Equals("Carrier")) // Carrier Dashboard
            {
                if (performed.Equals("Save")) // Save as Incomplete
                {
                    if (!newPol.AgentRole.Equals("CC") && !newPol.AgentRole.Equals("CCRO"))
                    {
                        if (driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd() == "Status: Pending Delivery")
                        {
                            IWebElement actionsList = driver.FindElement(By.ClassName("actions-list-detail"));
                            string expectedActions = String.Empty;

                            foreach (IWebElement option in actionsList.FindElements(By.ClassName("actions-list-detail-row")))
                            {
                                if (option.GetAttribute("text") != String.Empty)
                                {
                                    expectedActions += option.GetAttribute("text") + ";";
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        Assert.AreEqual("Status: Incomplete", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                    }
                }
                else if (performed.Equals("Send")) // Send Policy
                {
                    if (newPol.Flow.Equals("CDAC"))
                    {
                        if (newPol.DistributorRole.Equals("CC") || newPol.DistributorRole.Equals("CCRO")) 
                        {
                            if (newPol.AgentRole.Equals("CC") || newPol.AgentRole.Equals("CCRO")) // Sent to Consumer
                            {
                                Assert.AreEqual("Status: Sent to Consumer", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                            }
                            else // Sent to Agent
                            {
                                Assert.AreEqual("Status: Sent to Agent", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                            }
                        }
                        else // Sent to Distributor
                        {
                            Assert.AreEqual("Status: Sent to Distributor", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                        }
                    }
                    else if (newPol.Flow.Equals("CAC-DW") || newPol.Flow.Equals("CAC-CA"))
                    {
                        if (newPol.AgentRole.Equals("CC") || newPol.AgentRole.Equals("CCRO")) // Sent to Consumer
                        {
                            Assert.AreEqual("Status: Sent to Consumer", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                        }
                        else // Sent to Agent
                        {
                            Assert.AreEqual("Status: Sent to Agent", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                        }
                    }
                    else // DTC, Sent to Consumer
                    {
                        Assert.AreEqual("Status: Sent to Consumer", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                    }
                }
            }
            else if (currentUser.Equals("Agent")) // Agent Dashboard
            {
                if (performed.Equals("Save")) // Save as Incomplete
                {
                    Assert.AreEqual("Status: Pending Delivery", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                }
                else if (performed.Equals("Send")) // Send Policy
                {
                    if (newPol.Flow.Equals("CDAC"))
                    {
                        if (newPol.DistributorRole.Equals("CC") || newPol.DistributorRole.Equals("CCRO"))
                        {
                            if (newPol.AgentRole.Equals("CC") || newPol.AgentRole.Equals("CCRO")) // Sent to Consumer
                            {
                                Assert.AreEqual("Status: Sent to Consumer", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                            }
                            else // Sent to Agent
                            {
                                Assert.AreEqual("Status: Sent to Agent", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                            }
                        }
                        else // Sent to Distributor
                        {
                            Assert.AreEqual("Status: Sent to Distributor", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                        }
                    }
                    else if (newPol.Flow.Equals("CAC-DW") || newPol.Flow.Equals("CAC-CA"))
                    {
                        if (newPol.AgentRole.Equals("CC") || newPol.AgentRole.Equals("CCRO")) // Sent to Consumer
                        {
                            Assert.AreEqual("Status: Sent to Consumer", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                        }
                        else // Sent to Agent
                        {
                            Assert.AreEqual("Status: Sent to Agent", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                        }
                    }
                    
                }
            }
            else // Distributor
            {
                if (performed == "Save") // Save as Incomplete
                {
                    Assert.AreEqual("Status: Pending Delivery", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                }
                else if (performed == "Send") // Send Policy
                {
                    if (newPol.Flow == "CDAC")
                    {
                        if (newPol.DistributorRole == "CC" || newPol.DistributorRole == "CCRO")
                        {
                            if (newPol.AgentRole == "CC" || newPol.AgentRole == "CCRO") // Sent to Consumer
                            {
                                Assert.AreEqual("Status: Sent to Consumer", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                            }
                            else // Sent to Agent
                            {
                                Assert.AreEqual("Status: Sent to Agent", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                            }
                        }
                        else // Sent to Distributor
                        {
                            Assert.AreEqual("Status: Sent to Distributor", driver.FindElement(By.Id("PolicyStatusLabel")).Text.TrimEnd());
                        }
                    }
                    
                }
            }
        }

       
        

              
    }
}