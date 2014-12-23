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

        public string username { get; set; }
        public string currentUser { get; set; }

        public void DoAction()
        {
            Login(username);
           
            if (newPol.Flow == "CDAC") // CDAC actions
            {
                if (currentUser == "Carrier")
                {
                    CreateCDAC();
                    SearchPolicyCreated();
                    VerifyStatusAndActions(newPol.SaveOrSend);

                    Thread.Sleep(4000);

                    driver.FindElement(By.Id("GoToDashboardLink")).Click();

                    if (newPol.Actor.Equals("CCM") || newPol.Actor.Equals("CCMA"))
                    {
                        if (newPol.Action == "Send")
                        {
                            DoCarrierSend();
                        }
                        else if (newPol.Action == "Cancel")
                        {
                            DoCarrierCancel();
                        }
                        else if (newPol.Action == "Print")
                        {
                            DoCarrierPrint();
                        }
                        else if (newPol.Action == "Approve")
                        {
                            DoCarrierApprove();
                        }
                        else if (newPol.Action == "Transfer")
                        {
                            DoCarrierTransfer();
                        }
                        else if (newPol.Action == "Change")
                        {
                            DoCarrierChange();
                        }
                        else if (newPol.Action == "Resend")
                        {
                            DoCarrierResend();
                        }
                        else if (newPol.Action == "Take")
                        {
                            DoCarrierTake();
                        }
          
                    }

                    LogOut();
                    
                    if (newPol.Actor == "DCM")
                    {
                        currentUser = "Distributor";
                        GoToUrl();
                        username = newPol.Distributor;
                        DoAction();
                    }
                    else if (newPol.Actor == "DCMA")
                    {
                        currentUser = "Distributor";
                       
                    }
                    else if (newPol.Actor == "Agent")
                    {
                        if (!newPol.DistributorRole.Equals("CC") && !newPol.DistributorRole.Equals("CCRO"))
                        {
                            GoToUrl();
                            username = newPol.Distributor;
                            Login(username);
                            DoDistributorEdeliver();
                            LogOut();
                        }

                        currentUser = "Agent";
                        GoToUrl();
                        username = newPol.Agent;
                        DoAction();

                    }
                    
                }

                if (currentUser == "Distributor")
                {
                    
                    if (newPol.Action == "e-Deliver")
                    {
                        DoDistributorEdeliver();
                    }
                    else if (newPol.Action == "Print")
                    {
                        DoDistributorPrint();
                    }
                    else if (newPol.Action == "Change")
                    {
                        DoDistributorChange();
                    }
                    else if (newPol.Action == "Resend")
                    {
                        if (!newPol.DistributorRole.Equals("CC") && !newPol.DistributorRole.Equals("CCRO"))
                        {
                            DoDistributorEdeliver();

                        }

                        DoDistributorResend();
                    }
                    else if (newPol.Action == "Transfer")
                    {
                        DoDistributorTransfer();
                    }
                    else if (newPol.Action == "TransferSubGA")
                    {
                        DoDistributorTransferSubGA();
                    }
                    else if (newPol.Action == "Take")
                    {
                        DoDistributorTake();
                    }
                    else if (newPol.Action == "CancelChange")
                    {
                        DoDistributorChange();

                        Thread.Sleep(2000);

                        DoDistributorCancelChange();
                    }
                    else if (newPol.Action == "RequestExt")
                    {
                        DoDistributorRequestExt();
                    }

                    LogOut();

                    if (newPol.Actor == "Agent")
                    {
                        currentUser = "Agent";
                        GoToUrl();
                        username = newPol.Agent;
                        DoAction();
                    }
                    else
                    {
                        currentUser = String.Empty;
                    }

                }

                if (currentUser == "Agent")
                {
                    
                    if (newPol.Action == "e-Deliver")
                    {
                        DoAgentEdeliver();
                    }
                    else if (newPol.Action == "Print")
                    {
                        DoAgentPrint();
                    }
                    else if (newPol.Action == "Change")
                    {
                        DoAgentChange();
                    }
                    else if (newPol.Action == "Decline")
                    {
                        DoAgentDecline();
                    }
                    else if (newPol.Action == "CancelChange")
                    {
                        DoAgentChange();

                        Thread.Sleep(2000);

                        DoAgentCancelChange();
                    }
                    else if (newPol.Action == "RequestExt")
                    {
                        DoAgentRequestExt();
                    }

                    LogOut();

                    currentUser = String.Empty;
                }
                
            }
            else if (newPol.Flow == "CAC-DW")
            {
                if (currentUser == "Carrier")
                {
                    CreateCACDW();
                    SearchPolicyCreated();
                    VerifyStatusAndActions(newPol.SaveOrSend);

                    Thread.Sleep(4000);

                    driver.FindElement(By.Id("GoToDashboardLink")).Click();

                    if (newPol.Actor == "CCM")
                    {
                        
                        if (newPol.Action == "Send")
                        {
                            DoCarrierSend();
                        }
                        else if (newPol.Action == "Cancel")
                        {
                            DoCarrierCancel();
                        }
                        else if (newPol.Action == "Print")
                        {
                            DoCarrierPrint();
                        }
                        else if (newPol.Action == "Approve")
                        {
                            DoCarrierApprove();
                        }
                        else if (newPol.Action == "Transfer")
                        {
                            DoCarrierTransfer();
                        }
                        else if (newPol.Action == "Change")
                        {
                            DoCarrierChange();
                        }
                        else if (newPol.Action == "Resend")
                        {
                            DoCarrierResend();
                        }
                        else if (newPol.Action == "Take")
                        {
                            DoCarrierTake();
                        }
                    }

                    LogOut();


                    if (newPol.Actor == "Agent")
                    {
                        currentUser = "Agent";
                        GoToUrl();
                        username = newPol.Agent;
                        DoAction();
                        currentUser = String.Empty;
                    }
                }


                if (currentUser == "Agent")
                {
                   
                    if (newPol.Action == "e-Deliver")
                    {
                        DoAgentEdeliver();
                    }
                    else if (newPol.Action == "Print")
                    {
                        DoAgentPrint();
                    }
                    else if (newPol.Action == "Change")
                    {
                        DoAgentChange();
                    }
                    else if (newPol.Action == "Decline")
                    {
                        DoAgentDecline();
                    }
                    else if (newPol.Action == "CancelChange")
                    {
                        DoAgentChange();

                        Thread.Sleep(2000);

                        DoAgentCancelChange();
                    }
                    else if (newPol.Action == "RequestExt")
                    {
                        DoAgentRequestExt();
                    }

                    LogOut();

                 }
            }
            else if (newPol.Flow == "CAC-CA")
            {
                if (currentUser == "Carrier")
                {
                    CreateCACCA();
                    SearchPolicyCreated();
                    VerifyStatusAndActions(newPol.SaveOrSend);

                    Thread.Sleep(4000);

                    driver.FindElement(By.Id("GoToDashboardLink")).Click();

                    if (newPol.Actor == "CCM")
                    {
                        
                        if (newPol.Action == "Send")
                        {
                            DoCarrierSend();
                        }
                        else if (newPol.Action == "Cancel")
                        {
                            DoCarrierCancel();
                        }
                        else if (newPol.Action == "Print")
                        {
                            DoCarrierPrint();
                        }
                        else if (newPol.Action == "Approve")
                        {
                            DoCarrierApprove();
                        }
                        else if (newPol.Action == "Transfer")
                        {
                            DoCarrierTransfer();
                        }
                        else if (newPol.Action == "Change")
                        {
                            DoCarrierChange();
                        }
                        else if (newPol.Action == "Resend")
                        {
                            DoCarrierResend();
                        }
                        else if (newPol.Action == "Take")
                        {
                            DoCarrierTake();
                        }
                    }

                    LogOut();

                    if (newPol.Actor == "Agent")
                    {
                        currentUser = "Agent";
                        GoToUrl();
                        username = newPol.Agent;
                        DoAction();
                        currentUser = String.Empty;
                    }
                }


                if (currentUser == "Agent")
                {
                    
                    if (newPol.Action == "e-Deliver")
                    {
                        DoAgentEdeliver();
                    }
                    else if (newPol.Action == "Print")
                    {
                        DoAgentPrint();
                    }
                    else if (newPol.Action == "Change")
                    {
                        DoAgentChange();
                    }
                    else if (newPol.Action == "Decline")
                    {
                        DoAgentDecline();
                    }
                    else if (newPol.Action == "CancelChange")
                    {
                        DoAgentChange();

                        Thread.Sleep(2000);

                        DoAgentCancelChange();
                    }
                    else if (newPol.Action == "RequestExt")
                    {
                        DoAgentRequestExt();
                    }

                    LogOut();

                }
            }
            else // DTC
            {
                if (currentUser == "Carrier")
                {
                    CreateDTC();
                    SearchPolicyCreated();
                    VerifyStatusAndActions(newPol.SaveOrSend);

                    Thread.Sleep(4000);

                    driver.FindElement(By.Id("GoToDashboardLink")).Click();

                   
                    if (newPol.Action == "Send")
                    {
                        DoCarrierSend();
                    }
                    else if (newPol.Action == "Cancel")
                    {
                        DoCarrierCancel();
                    }
                    else if (newPol.Action == "Print")
                    {
                        DoCarrierPrint();
                    }
                    else if (newPol.Action == "Approve")
                    {
                        DoCarrierApprove();
                    }
                    else if (newPol.Action == "Transfer")
                    {
                        DoCarrierTransfer();
                    }
                    else if (newPol.Action == "Change")
                    {
                        DoCarrierChange();
                    }
                    else if (newPol.Action == "Resend")
                    {
                        DoCarrierResend();
                    }
                    else if (newPol.Action == "Take")
                    {
                        DoCarrierTake();
                    }
                }

                LogOut();
            }

            
        }

        
        private void SearchPolicyCreated()
        {
            Thread.Sleep(7000);

            driver.FindElement(By.XPath(".//*[@id='SearchTextBox']")).Clear();
            driver.FindElement(By.XPath(".//*[@id='SearchTextBox']")).SendKeys(polNbr);

            driver.FindElement(By.XPath(".//*[@id='A3']/img")).Click();

            Thread.Sleep(2000);
        }

        
        private void SendOrSave()
        {
            if (newPol.SaveOrSend == "Send") // Send Policy
            {
                driver.FindElement(By.XPath(".//*[@id='add-policy-btn']")).Click();
            }
            else // Save as Incomplete
            {
                driver.FindElement(By.XPath(".//*[@id='btnSaveAsIncomplete']")).Click();
            }

            Thread.Sleep(25000);

            driver.FindElement(By.Id("GoToDashboardLink")).Click();
        }

        public string polNbr { get; set; }
        
    }
}