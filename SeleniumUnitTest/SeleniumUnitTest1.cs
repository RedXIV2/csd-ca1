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
               
                // get celsius element
                IWebElement systolicElement = driver.FindElement(By.Id("BP_Systolic"));

                // enter 10 in element
                systolicElement.SendKeys(Keys.Backspace);
                systolicElement.SendKeys(Keys.Backspace);
                systolicElement.SendKeys(Keys.Backspace);
                systolicElement.SendKeys("140");
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
    }
}
