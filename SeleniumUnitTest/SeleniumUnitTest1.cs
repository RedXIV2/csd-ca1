// automated user acceptance testing using Selenium
// C# binding to WebDriver (Selenium 2.0)
// tests app deployed on Azure e.g. http://gc-temperatureconverter-qa.azurewebsites.net
// as in runsettings file (SeleniumTest.runsettings)

// MSTest
using Microsoft.VisualStudio.TestTools.UnitTesting;

// NuGet install Selenium WebDriver package and Support Classes
using OpenQA.Selenium;

// NuGet install PhantomJS driver (or Chrome or Firefox...)
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumUnitTest
{
    [TestClass]
    public class SeleniumUnitTest1
    {
        // .runsettings file contains test run parameters
        // e.g. URI for app
        // test context for this run
       
        private TestContext testContextInstance;

        // test harness uses this property to initliase test context
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // URI for web app being tested
        private String webAppUri;

        // .runsettings property overriden in vsts test runner
        // release task
        [TestInitialize]                // run before each unit test
        public void Setup()
        { 
            this.webAppUri = testContextInstance.Properties["webAppUri"].ToString();
        }

        // one unit test
        [TestMethod]
        public void HighBloodPressureTextValidation()
        {
            // PhantomJSDriver will work in hosted agent
            // in VSTS (others won't)
            using (IWebDriver driver = new PhantomJSDriver())
            {
                // any exception below result in a test fail

                // navigate to URI for temperature converter
                // web app running on IIS express
                driver.Navigate().GoToUrl(webAppUri);
               
                // get Systolic element
                IWebElement systolicElement = driver.FindElement(By.Id("BP_Systolic"));

                // clear text box with beautiful example of recursion
                clearTextBox(systolicElement);

                // enter 140 in element           
                systolicElement.SendKeys("140");

                //move focus from Systolic TextBox to allow page to automatically update
                systolicElement.SendKeys(Keys.Tab);


                // submit the form
                //driver.FindElement(By.Id("convertform")).Submit();

                IWebElement bodyText = driver.FindElement(By.TagName("body"));

                System.Diagnostics.Debug.WriteLine(bodyText.Text);

                StringAssert.Contains(bodyText.Text, "High Blood Pressure");

                // explictly wait for result with "fahrenheit" item
                //IWebElement fahrenheitElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                //Boolean rightTextShow = new WebDriverWait(driver, TimeSpan.FromSeconds(10))  
                //.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id=\"form1\"]/div[3]"), "High Blood Pressure"));
                //.Until(ExpectedConditions.ElementExists((By.Id("fahrenheit"))));

                // item comes back like "Faherheit: 50"
                //String fahrenheit = fahrenheitElement.Text.ToString();

                // 10 Celsius = 50 Fahrenheit, assert it
                //StringAssert.EndsWith(fahrenheit, "50");
                //Assert.Equals(rightTextShow, true);

                driver.Quit();
            }
        }

        [TestMethod]
        public void PreHighBloodPressureTextValidation()
        {

            using (IWebDriver driver = new PhantomJSDriver())
            {
                driver.Navigate().GoToUrl(webAppUri);

                IWebElement diastolicElement = driver.FindElement(By.Id("BP_Diastolic"));

                clearTextBox(diastolicElement);
                diastolicElement.SendKeys("80");
                diastolicElement.SendKeys(Keys.Tab);

                IWebElement bodyText = driver.FindElement(By.TagName("body"));
                System.Diagnostics.Debug.WriteLine(bodyText.Text);

                StringAssert.Contains(bodyText.Text, "Pre-High Blood Pressure");

                driver.Quit();
            }
        }

        [TestMethod]
        public void NormalBloodPressureTextValidation()
        {
            using (IWebDriver driver = new PhantomJSDriver())
            {
                driver.Navigate().GoToUrl(webAppUri);

                IWebElement diastolicElement = driver.FindElement(By.Id("BP_Diastolic"));

                clearTextBox(diastolicElement);
                diastolicElement.SendKeys("50");
                diastolicElement.SendKeys(Keys.Tab);

                IWebElement bodyText = driver.FindElement(By.TagName("body"));
                System.Diagnostics.Debug.WriteLine(bodyText.Text);
                StringAssert.Contains(bodyText.Text, "Normal Blood Pressure");

                driver.Quit();
            }
        }

        [TestMethod]
        public void LowBloodPressureTextValidation()
        {
            using (IWebDriver driver = new PhantomJSDriver())
            {
                driver.Navigate().GoToUrl(webAppUri);

                
                IWebElement systolicElement = driver.FindElement(By.Id("BP_Systolic"));

                clearTextBox(systolicElement);
                systolicElement.SendKeys("70");
                systolicElement.SendKeys(Keys.Tab);

                IWebElement diastolicElement = driver.FindElement(By.Id("BP_Diastolic"));

                clearTextBox(diastolicElement);
                diastolicElement.SendKeys("52");
                diastolicElement.SendKeys(Keys.Tab);

                IWebElement bodyText = driver.FindElement(By.TagName("body"));
                System.Diagnostics.Debug.WriteLine(bodyText.Text);
                StringAssert.Contains(bodyText.Text, "Low Blood Pressure");

                driver.Quit();
            }
        }

        //Utility to clear text from textboxes as IWebElement.clear() has proven unreliable
        private void clearTextBox(IWebElement boxToClear)
        {
            if(boxToClear.GetAttribute("value").Length > 0)
            {
                boxToClear.SendKeys(Keys.Backspace);
                clearTextBox(boxToClear);
            }
            return;
        }

    }
}
