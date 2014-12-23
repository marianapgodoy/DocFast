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
        private void CreateCACCA()
        {
            Thread.Sleep(7000);
            driver.FindElement(By.XPath("//a[contains(@href, 'javascript:PolicyCreate();')]")).Click();

            Thread.Sleep(10000);

            if (newPol.Product != string.Empty) // Product Type
            { 
                new SelectElement(driver.FindElement(By.Id("ProductTypeDropDown"))).SelectByText(newPol.Product);
            }

            // Policy Number
            IWebElement PolicyNumber = driver.FindElement(By.Id("PolicyNumber"));
            polNbr = DateTime.Now.ToString();
            polNbr = polNbr.Replace(":", "_");
            PolicyNumber.SendKeys(polNbr);

            // Annual Premium
            if (newPol.AnnualPremium)
            {
                IWebElement AnnualPremium = driver.FindElement(By.Id("AnnualPremium"));
                if (DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
                {
                    AnnualPremium.SendKeys("1234");
                }
                else
                {
                    AnnualPremium.SendKeys(DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
                }
            }

            Thread.Sleep(2000);

            // Delivery Date
            IWebElement DeliveryDate = driver.FindElement(By.Id("DeliveryDate"));
            DeliveryDate.Click();
            driver.FindElement(By.LinkText(DateTime.Now.Day.ToString())).Click();

            // PDF Document
            if (newPol.Supplier == "Lincoln")
            {
                driver.FindElement(By.Id("uploadDocumentMulti")).SendKeys(DocFast.Resource.PDF_LocationLincoln);
                new SelectElement(driver.FindElement(By.Id("LincolnMoneyGuard_Sample1_pdf_Template"))).SelectByText(DocFast.Resource.PDF_TemplateLincoln);
            }
            else if (newPol.Supplier == "Standard")
            {
                driver.FindElement(By.Id("uploadDocumentMulti")).SendKeys(DocFast.Resource.PDF_LocationStd);
                new SelectElement(driver.FindElement(By.Id("agentdocs2013_pdf_Template"))).SelectByText(DocFast.Resource.PDF_TemplateStd);
            }
            //else if (newPol.Supplier == "Standard NY")
            //{
            //    driver.FindElement(By.Id("uploadDocumentMulti")).SendKeys(DocFast.Resource.PDF_LocationStdNY);
            //    new SelectElement(driver.FindElement(By.Id("agentdocs2013_pdf_Template"))).SelectByText(DocFast.Resource.PDF_TemplateStdNY);
            //}
            else
            {
                driver.FindElement(By.Id("uploadDocumentMulti")).SendKeys(DocFast.Resource.PDF_LocationTest);
                new SelectElement(driver.FindElement(By.Id("1iPipeline_Sample_Policy_pdf_Template"))).SelectByText(DocFast.Resource.PDF_TemplateTest);
            }


            IWebElement ESVCbx = driver.FindElement(By.XPath(".//*[@id='EnforceSignerVisibilityCheckBox']"));
            // Enforce Signer Visibility
            if (newPol.ESV)
            {
                if (!ESVCbx.Selected)
                {
                    driver.FindElement(By.XPath(".//*[@id='EnforceSignerVisibilityCheckBox']")).Click();
                }
                driver.FindElement(By.Id("uploadDocumentMulti")).Click();
                driver.FindElement(By.Id("uploadDocumentMulti")).SendKeys(DocFast.Resource.PDFEsv_Location);

                new SelectElement(driver.FindElement(By.Id("1iPipelineAgentInstructions_pdf_Template"))).SelectByText(DocFast.Resource.PDFEsv_Template);
            }
            else
            {
                if (ESVCbx.Selected)
                {
                    driver.FindElement(By.XPath(".//*[@id='EnforceSignerVisibilityCheckBox']")).Click();
                }
            }

            Thread.Sleep(2000);

            // First Name and Last Name
            if (!newPol.CCQ) // PolicyEX ID Check
            {
                IWebElement FirstName = driver.FindElement(By.Id("FirstName"));
                FirstName.SendKeys(DocFast.Resource.CACCA_PI_FN);

                IWebElement LastName = driver.FindElement(By.Id("LastName"));
                LastName.SendKeys(DocFast.Resource.CACCA_PI_LN);
            }
            else // DocuSign ID Check
            {
                IWebElement FirstName = driver.FindElement(By.Id("FirstName"));
                FirstName.SendKeys(DocFast.Resource.PI_IDCheck_FN);

                IWebElement LastName = driver.FindElement(By.Id("LastName"));
                LastName.SendKeys(DocFast.Resource.PI_IDCheck_LN);
            }

            // Authentication
            if (!newPol.CCQ) // PolicyEX ID Check
            {
                if (newPol.Supplier != "Standard" && newPol.Supplier != "Standard NY")
                {
                    if (driver.FindElement(By.Id("chkConsumerQuizQuestion_1")).Selected)
                    {
                        driver.FindElement(By.Id("chkConsumerQuizQuestion_1")).Click();
                    }

                    if (DateTime.Now.Minute % 2 == 0)
                    {
                        driver.FindElement(By.Id("chkConsumerQuizQuestion_3")).Click();
                        IWebElement txtConsumerQuizAnswerChoiceText_3 = driver.FindElement(By.Id("txtConsumerQuizAnswerChoiceText_3"));
                        txtConsumerQuizAnswerChoiceText_3.SendKeys(DocFast.Resource.CACCA_PI_LN);
                    }
                    else if (DateTime.Now.Minute % 3 == 0)
                    {
                        driver.FindElement(By.Id("chkConsumerQuizQuestion_2")).Click();
                        IWebElement txtConsumerQuizAnswerChoiceText_2 = driver.FindElement(By.Id("txtConsumerQuizAnswerChoiceText_2"));
                        txtConsumerQuizAnswerChoiceText_2.SendKeys(DocFast.Resource.Birth_Date);
                    }
                    else
                    {
                        driver.FindElement(By.Id("chkConsumerQuizQuestion_1")).Click();
                        IWebElement txtConsumerQuizAnswerChoiceText_1 = driver.FindElement(By.Id("txtConsumerQuizAnswerChoiceText_1"));
                        txtConsumerQuizAnswerChoiceText_1.SendKeys(DocFast.Resource.SSN);
                    }
                }
                else
                {
                    IWebElement txtLastFourDigitsSSN = driver.FindElement(By.Id("LastFourDigitsSSN"));
                    txtLastFourDigitsSSN.SendKeys(DocFast.Resource.SSN);
                }
            }
            else // DocuSign ID Check
            {
                driver.FindElement(By.Id("DocuSignIDCheckCheckBox")).Click();

                IWebElement txtAddressLine1 = driver.FindElement(By.Id("AddressLine1"));
                txtAddressLine1.SendKeys(DocFast.Resource.PI_AddressLine1);

                IWebElement txtCity = driver.FindElement(By.Id("City"));
                txtCity.SendKeys(DocFast.Resource.PI_City);

                new SelectElement(driver.FindElement(By.Id("StateDropDown"))).SelectByValue(DocFast.Resource.PI_State);

                IWebElement txtZip = driver.FindElement(By.Id("Zip"));
                txtZip.SendKeys(DocFast.Resource.PI_Zip);

                driver.FindElement(By.Id("chkConsumerQuizQuestion_1")).Click();

            }

            Thread.Sleep(2000);

            // Additional Consumer
            if (newPol.Additional)
            {
                driver.FindElement(By.Id("AddNewConsumerLinkAboveTable")).Click();

                Thread.Sleep(7000);

                IWebElement FirstNameAdd = driver.FindElement(By.Id("NewConsumerFirstName"));
                FirstNameAdd.SendKeys(DocFast.Resource.CACCA_Add_FN);

                IWebElement LastNameAdd = driver.FindElement(By.Id("NewConsumerLastName"));
                LastNameAdd.SendKeys(DocFast.Resource.CACCA_PI_LN);

                IWebElement EmailAdd = driver.FindElement(By.Id("NewConsumerEmail"));
                EmailAdd.SendKeys(DocFast.Resource.Consumer_Email);

                IWebElement CQAdd = driver.FindElement(By.Id("txtConsumerQuizAnswerChoiceTextA_1"));
                CQAdd.SendKeys("1234");

                driver.FindElement(By.Id("btnSave")).Click();

                new SelectElement(driver.FindElement(By.Id("TemplateRolesDropDown0"))).SelectByValue(DocFast.Resource.Template_Add_Role);

            }

            Thread.Sleep(2000);

            // Private Attachment
            if (newPol.PrivateAttachment)
            {
                if (newPol.Server == "QA")
                {
                    driver.FindElement(By.Id("uploadPrivateDocumentMulti")).SendKeys(DocFast.Resource.PA_Location);

                    if (!newPol.CCQ)
                    {
                        new SelectElement(driver.FindElement(By.Id("PrivateConfidentialDocument_pdf_RecipientListCombo"))).SelectByText(DocFast.Resource.CACCA_PI_FN + " " + DocFast.Resource.CACCA_PI_LN);
                    }
                    else
                    {
                        new SelectElement(driver.FindElement(By.Id("PrivateConfidentialDocument_pdf_RecipientListCombo"))).SelectByText(DocFast.Resource.PI_IDCheck_FN + " " + DocFast.Resource.PI_IDCheck_LN);
                    }

                    if (DateTime.Now.Minute % 2 == 0)
                    {
                        new SelectElement(driver.FindElement(By.Id("PrivateConfidentialDocument_pdf_DocumentTypeCombo"))).SelectByIndex(1);
                    }
                    else if (DateTime.Now.Minute % 3 == 0)
                    {
                        new SelectElement(driver.FindElement(By.Id("PrivateConfidentialDocument_pdf_DocumentTypeCombo"))).SelectByIndex(2);
                    }
                    else
                    {
                        new SelectElement(driver.FindElement(By.Id("PrivateConfidentialDocument_pdf_DocumentTypeCombo"))).SelectByIndex(3);
                    }
                }
                else
                {
                    driver.FindElement(By.Id("uploadPrivateDocumentMulti")).SendKeys(DocFast.Resource.PA_Location);

                    if (!newPol.CCQ)
                    {
                        new SelectElement(driver.FindElement(By.Id("PrivateConfidentialDocument_pdf_RecipientListCombo"))).SelectByText(DocFast.Resource.CACCA_PI_FN + " " + DocFast.Resource.CACCA_PI_LN);
                    }
                    else
                    {
                        new SelectElement(driver.FindElement(By.Id("PrivateConfidentialDocument_pdf_RecipientListCombo"))).SelectByText(DocFast.Resource.PI_IDCheck_FN + " " + DocFast.Resource.PI_IDCheck_LN);
                    }

                    new SelectElement(driver.FindElement(By.Id("PrivateConfidentialDocument_pdf_DocumentTypeCombo"))).SelectByIndex(0);
                }
            }

            // Agent setup
            if (newPol.AgentID != "0") // No Agent
            {
                driver.FindElement(By.Id("AgentCheckBox")).Click();
                driver.FindElement(By.Id("find-agent")).Click();
                string searchField = newPol.AgentSearch.Substring(3,newPol.AgentSearch.Count()-3);

                if (newPol.AgentSearch.Contains("FN")) // Search by FN
                {
                    driver.FindElement(By.Id("AgentSearchFirstName")).SendKeys(searchField);
                }
                else if (newPol.AgentSearch.Contains("LN")) // Search by LN
                {
                    driver.FindElement(By.Id("AgentSearchLastName")).SendKeys(searchField);
                }
                else // Search by Email
                {
                    driver.FindElement(By.Id("AgentSearchEmail")).SendKeys(searchField);
                }

                driver.FindElement(By.Id("AgentSearchButton")).Click();

                Thread.Sleep(2000);
                    
                IWebElement Agent = driver.FindElement(By.XPath(".//*[@id='divAgentList']"));

                foreach (IWebElement option in Agent.FindElements(By.Name("AgentListRadio")))
                {
                    if (option.GetAttribute("value") == newPol.AgentID) // QA = 64 A_4, 400 IA_3, 334 Maggie's ; UAT = 631 
                    {
                        option.Click();
                        break;
                    }
                }

                driver.FindElement(By.Id("SelectAgent")).Click();

                Thread.Sleep(2000);

                // Agent's Role
                AgentPolicyRole();
            }
            else // Create new Agent
            {
                driver.FindElement(By.Id("AgentCheckBox")).Click();
                driver.FindElement(By.Id("find-agent")).Click();

                driver.FindElement(By.Id("AddNewAgent")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.Id("NewAgentFirstName")).SendKeys("Teresita");
                driver.FindElement(By.Id("NewAgentLastName")).SendKeys(polNbr.Replace(" ", "_"));
                driver.FindElement(By.Id("NewAgentEmail")).SendKeys("tmigliorisi@totalperform.com");
            }
            
            // PI Email setup
            CheckPIEmail();

            // Send Policy or Save as Incomplete
            SendOrSave();
            }
            
     }
}