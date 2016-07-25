using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GCU_CMS.Util;
using OpenQA.Selenium.Interactions;

namespace GCU_CMS.ModuleObjects
{
    public class LoginModule : SeleniumBase
    {
        public LoginModule(IWebDriver driver)
        {
            Driver = driver;
        }

        public void LoginToCMS(string username, string password, string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            //Login to CMS
            Driver.FindElement(By.Id("inputfeedback1")).Clear();
            TakeScreenshot("1." + methodName + ".LoginPage", tgName, tName);
            Driver.FindElement(By.Id("inputfeedback1")).SendKeys(username);
            Driver.FindElement(By.Id("inputfeedback2")).Clear();
            Driver.FindElement(By.Id("inputfeedback2")).SendKeys(password);
            TakeScreenshot("2." + methodName + ".EnterCredentials", tgName, tName);
            Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
                        
            //Verify Loging
            Assert.AreEqual("Dashboard", Driver.FindElement(By.XPath("//li[3]/a")).Text);      
        }

        public void Logout(string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            Console.WriteLine(CompanyModule.str);
            Driver.FindElement(By.LinkText("Help")).Click();
            Thread.Sleep(1000);
            TakeScreenshot("99." + methodName + ".Logout", tgName, tName);
            Driver.FindElement(By.LinkText("Logout")).Click();
            TakeScreenshot("100." + methodName + ".AfterLogout", tgName, tName);
        }
    }
}
