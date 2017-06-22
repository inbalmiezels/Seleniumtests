using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginTests
{
       
    [TestClass]
    public class LoginUT
    {
        const string PASSWORD_ID = "pw";
        const string USERNAME_ID = "username";
        const int WEB_OPERATION_SLEEP_MS = 5000;

        /**
         *  Attempts login into 'walla mail'
         *  user        The username that will be used to attempt login
         *  password    The password that will be used to attempt login
         *  helloText   'hello' string that indicates a successful login
         *  returns     true if successful, false otherwise
         */
        public bool CheckLogin(string user, string password, string helloText)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://friends.walla.co.il");
            Thread.Sleep(WEB_OPERATION_SLEEP_MS);
            var userField = driver.FindElement(By.Id(USERNAME_ID));
            var passField = driver.FindElement(By.Id(PASSWORD_ID));
            var loginButton = driver.FindElement(By.ClassName("common-button-1"));
            userField.SendKeys(user);
            passField.SendKeys(password);
            loginButton.Click();
            Thread.Sleep(WEB_OPERATION_SLEEP_MS);
            IWebElement helloUser;
            try
            {
                helloUser = driver.FindElement(By.ClassName("user-name-button"));
            }
            catch (NoSuchElementException)
            {
                // In case the element wasn't found it would throw an exception
                driver.Close();
                return false;
            }
            bool res = helloUser.Text == helloText;
            driver.Close();
            return res;

        }

        /**
         *  Tests a 'walla mail' login page given correct credentials
         */
        [TestMethod]
        public void TestLoginSuccessful()
        {
            string user="seltest";
            string password="Password1";
            string helloText = "שלום Sel Tes";
            bool isLoginSuccess=CheckLogin(user, password, helloText);
            Assert.IsTrue(isLoginSuccess);
            
        }

        /**
         *  Tests a 'walla mail' login page given incorrect credentials
         */
        [TestMethod]
        public void TestLoginFailure()
        {
            string user = "seltest";
            string password = "Password12";
            string helloText = "שלום Sel Tes";
            bool isLoginSuccess = CheckLogin(user, password, helloText);
            Assert.IsFalse(isLoginSuccess);

        }

        public static void Main()
        {
        }
    }
}
