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
    public class DashboardModule : SeleniumBase
    {
        public DashboardModule(IWebDriver driver)
        {
            Driver = driver;
        }

        public void VerifySuperSuperAdminDashboard(string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            //Verify Dashboard components
            TakeScreenshot("10." + methodName + ".SuperSuperAdminDashboardPage", tgName, tName);
            Assert.AreEqual("Company", Driver.FindElement(By.XPath("//div/p")).Text);
            Assert.AreEqual("Master Data", Driver.FindElement(By.XPath("//li[2]/a/div/p")).Text);
            Assert.AreEqual("Access Control", Driver.FindElement(By.XPath("//li[3]/a/div/p")).Text);
            Assert.AreEqual("Content Management", Driver.FindElement(By.XPath("//li[4]/a/div/p")).Text);
            Assert.AreEqual("Artists and Groups", Driver.FindElement(By.XPath("//li[5]/a/div/p")).Text);
            Assert.AreEqual("Users and Activities", Driver.FindElement(By.XPath("//li[6]/a/div/p")).Text);                        
        }

        public void VerifyCompanyAdminDashboard(string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            //Verify Dashboard components
            TakeScreenshot("11." + methodName + ".CompanyAdminDashboardPage", tgName, tName);
            Assert.AreEqual("Access Control", Driver.FindElement(By.XPath("//div/p")).Text);
            Assert.AreEqual("Content Management", Driver.FindElement(By.XPath("//li[2]/a/div/p")).Text);
            Assert.AreEqual("Artists and Groups", Driver.FindElement(By.XPath("//li[3]/a/div/p")).Text);
        }

        public void NavigateToDashboard(string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            Driver.FindElement(By.CssSelector("img[alt=\"logo\"]")).Click();
        }
    }
}
