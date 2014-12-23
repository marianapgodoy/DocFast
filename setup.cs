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
        Policy newPol = new Policy();
        
        [SetUp]
        public void Setup()
        {
            GetFile();
                      
        }

        
        [Test]
        public void TestScenario()
        {
            for (int row = 257; row < dtExcel.Rows.Count; row++) // Loop file rows 
            {
                newPol.Server = dtExcel.Rows[row].ItemArray[0].ToString();
                newPol.Browser = dtExcel.Rows[row].ItemArray[1].ToString();
                newPol.Flow = dtExcel.Rows[row].ItemArray[2].ToString();
                newPol.DistributorRole = dtExcel.Rows[row].ItemArray[3].ToString();
                newPol.AgentRole = dtExcel.Rows[row].ItemArray[4].ToString();
                newPol.Product = dtExcel.Rows[row].ItemArray[5].ToString();
                newPol.AnnualPremium = Convert.ToBoolean(dtExcel.Rows[row].ItemArray[6].ToString());
                newPol.ESV = Convert.ToBoolean(dtExcel.Rows[row].ItemArray[7].ToString());
                newPol.CCQ = Convert.ToBoolean(dtExcel.Rows[row].ItemArray[8].ToString());
                newPol.CQ = Convert.ToBoolean(dtExcel.Rows[row].ItemArray[9].ToString());
                newPol.Additional = Convert.ToBoolean(dtExcel.Rows[row].ItemArray[10].ToString());
                newPol.PrivateAttachment = Convert.ToBoolean(dtExcel.Rows[row].ItemArray[11].ToString());
                newPol.NewAgent = Convert.ToBoolean(dtExcel.Rows[row].ItemArray[12].ToString());
                newPol.PIEmail = Convert.ToBoolean(dtExcel.Rows[row].ItemArray[13].ToString());
                newPol.SaveOrSend = dtExcel.Rows[row].ItemArray[14].ToString();
                newPol.Actor = dtExcel.Rows[row].ItemArray[15].ToString();
                newPol.Action = dtExcel.Rows[row].ItemArray[16].ToString();
                newPol.Supplier = dtExcel.Rows[row].ItemArray[17].ToString();
                newPol.DistributorID = dtExcel.Rows[row].ItemArray[18].ToString();
                newPol.DCMID = dtExcel.Rows[row].ItemArray[19].ToString();
                newPol.AgentID = dtExcel.Rows[row].ItemArray[20].ToString();
                newPol.AgentSearch = dtExcel.Rows[row].ItemArray[21].ToString();
                newPol.Carrier = dtExcel.Rows[row].ItemArray[22].ToString();
                newPol.Distributor = dtExcel.Rows[row].ItemArray[23].ToString();
                newPol.Agent = dtExcel.Rows[row].ItemArray[24].ToString();
                
                GoToUrl();

                currentUser = "Carrier";
                username = newPol.Carrier;
                DoAction();
            }
            
        }

        public struct Policy
        {
            public string Server;
            public string Browser;
            public string Flow;
            public string DistributorRole;
            public string AgentRole;
            public string Product;
            public bool AnnualPremium;
            public bool ESV;
            public bool CCQ;
            public bool CQ;
            public bool Additional;
            public bool PrivateAttachment;
            public bool NewAgent;
            public bool PIEmail;
            public string SaveOrSend;
            public string Actor;
            public string Action;
            public string Supplier;
            public string DistributorID;
            public string DCMID;
            public string AgentID;
            public string AgentSearch;
            public string Carrier;
            public string Distributor;
            public string Agent;
  
        }

        DataTable dtExcel = new DataTable();

        public void GetFile()
        {
            DataSet ds_Data = new DataSet();
            OleDbConnection oleCon = new OleDbConnection();

            string strExcelFile = DocFast.Resource.FilePath;
            oleCon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strExcelFile + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;
            string SpreadSheetName = "";

            OleDbDataAdapter Adapter = new OleDbDataAdapter();
            OleDbConnection conn = new OleDbConnection(oleCon.ConnectionString);

            string strQuery;
            conn.Open();
            
            DataTable ExcelSheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            SpreadSheetName = "QA Test Carrier$"; 

            strQuery = "select * from [" + SpreadSheetName + "] ";
            OleDbCommand cmd = new OleDbCommand(strQuery, conn);
            Adapter.SelectCommand = cmd;

            Adapter.Fill(dtExcel);
            conn.Close();
        }

        public IWebDriver driver { get; set; }

        private IWebDriver InitializeDriver(string Browser)
        {
            // Create a new instance of the driver.
            
            // Internet Explorer
            if (Browser == "IE")
            {

                var options = new InternetExplorerOptions();
                driver = new InternetExplorerDriver(@"C:\Users\Tery\Documents\Totalperformance\ePolicy\SeleniumTcs\Drivers", options);

            }
            // Chrome
            else if (Browser == "Ch")
            {
               driver = new ChromeDriver(@"C:\Users\Tery\Documents\Totalperformance\ePolicy\SeleniumTcs\Drivers");
            }
            // Firefox
            else if (Browser == "FF")
            {
                driver = new FirefoxDriver();

            }
            // Safari
            else
            {
                driver = new SafariDriver();

            }

            return driver;

        }

        private void GoToUrl()
        {
            IWebDriver driver = InitializeDriver(newPol.Browser);

            if (newPol.Server == "QA")
            {
                driver.Navigate().GoToUrl(DocFast.Resource.QA_Url);
            }
            else if (newPol.Server == "UAT")
            {
                driver.Navigate().GoToUrl(DocFast.Resource.UAT_Url);
            }
            else if (newPol.Server == "QA2")
            {
                driver.Navigate().GoToUrl(DocFast.Resource.QA2_Url);
            }
            else if (newPol.Server == "PROD")
            {
                driver.Navigate().GoToUrl(DocFast.Resource.Prod_Url);
            }

        }

        private void Login(string username)
        {
            IWebElement user = driver.FindElement(By.XPath("//input[@name='user']"));
            user.SendKeys(username);

            IWebElement password = driver.FindElement(By.XPath("//input[@name='password']"));
            password.Clear();
            password.SendKeys(DocFast.Resource.User_Pwd);

            // Now submit the form. WebDriver will find the form from the element
            user.Submit();
            Thread.Sleep(2000);
        }

        private void LogOut()
        {
            IWebElement a1 = driver.FindElement(By.Id("A1"));
            a1.Click();

            Thread.Sleep(2000);

            //Close the browser
            driver.Quit();
        }

       
        private void CheckPIEmail()
        {
            Thread.Sleep(2000);

            if (newPol.PIEmail)
            {
                driver.FindElement(By.Id("ConsumerEmail")).SendKeys(DocFast.Resource.Consumer_Email);
            }

        }

       
    }
}