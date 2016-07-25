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
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace GCU_CMS.ModuleObjects
{
    public class CompanyModule : SeleniumBase
    {
        public String str;
        DateTime today = DateTime.Now;

        public CompanyModule(IWebDriver driver)
        {
            Driver = driver;
        }

        public void CreateACompany(string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            //Create a company with Company Admin
            //Enter company details
            str = today.Month.ToString() + today.Day.ToString() + today.Hour.ToString() + today.Minute.ToString();
            Console.WriteLine(str);

            File.WriteAllText(@datapath + "\\path.txt", str);
            var tmp = File.ReadAllText(@datapath + "\\path.txt");
            Console.WriteLine(tmp);
            Driver.FindElement(By.CssSelector("div.icon-box > p")).Click();
            Driver.FindElement(By.CssSelector("i.fa.fa-plus-circle")).Click();
            TakeScreenshot("20." + methodName + ".AddCompanyPage", tgName, tName);
            Driver.FindElement(By.Id("company-name")).Clear();
            Driver.FindElement(By.Id("company-name")).SendKeys("ComName_" + str);
            Driver.FindElement(By.Id("company-address1")).Clear();
            Driver.FindElement(By.Id("company-address1")).SendKeys("ComAdd1_" + str);
            Driver.FindElement(By.Id("company-address2")).Clear();
            Driver.FindElement(By.Id("company-address2")).SendKeys("ComAdd2_" + str);
            Driver.FindElement(By.Id("company-city")).Clear();
            Driver.FindElement(By.Id("company-city")).SendKeys("ConCity_" + str);
            Driver.FindElement(By.Id("company-state")).Clear();
            Driver.FindElement(By.Id("company-state")).SendKeys("ComState_" + str);
            new SelectElement(Driver.FindElement(By.Id("company-country"))).SelectByText("Korea, Democratic People's Republic of");
            Driver.FindElement(By.Id("company-postcode")).Clear();
            Driver.FindElement(By.Id("company-postcode")).SendKeys("1234");
            Driver.FindElement(By.Id("company-phone")).Clear();
            Driver.FindElement(By.Id("company-phone")).SendKeys("1234");
            Driver.FindElement(By.Id("company-registration-no")).Clear();
            Driver.FindElement(By.Id("company-registration-no")).SendKeys("1234");
            new SelectElement(Driver.FindElement(By.Id("company-timezone"))).SelectByText("UTC +00:00");
            //Driver.FindElement(By.Id("company-logo")).Click();
            Thread.Sleep(2000);
            IWebElement element = Driver.FindElement(By.Id("company-logo"));

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0," + element.Location.Y + ")");

            IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript("arguments[0].click();", element);
            Console.WriteLine("A1");
            Driver.FindElement(By.Id("doc-file")).Clear();
            Driver.FindElement(By.Id("doc-file")).SendKeys(datapath + "\\zeerow_logo.png");
            Console.WriteLine("A2");
            Thread.Sleep(2000);
            //Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            //Driver.FindElement(By.XPath("//button[2]")).Click();
            TakeScreenshot("21." + methodName + ".EnteredCompanyData", tgName, tName);
            IWebElement element1 = Driver.FindElement(By.XPath("//button[2]"));

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0," + element1.Location.Y + ")");

            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)Driver;
            executor1.ExecuteScript("arguments[0].click();", element1);

            Console.WriteLine("A3");
            Thread.Sleep(5000);
            Console.WriteLine("A4");
            IWebElement element2 = Driver.FindElement(By.XPath("(//button[@type='button'])[3]"));

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0," + element2.Location.Y + ")");

            IJavaScriptExecutor executor2 = (IJavaScriptExecutor)Driver;
            executor2.ExecuteScript("arguments[0].click();", element2);

            IWebElement element3 = Driver.FindElement(By.XPath("(//button[@type='submit'])[2]"));

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0," + element3.Location.Y + ")");

            IJavaScriptExecutor executor3 = (IJavaScriptExecutor)Driver;
            executor3.ExecuteScript("arguments[0].click();", element2);

            Console.WriteLine("A5");
            //Enter Company Admin's Details
            TakeScreenshot("22." + methodName + ".CompanyAdminPage", tgName, tName);
            Driver.FindElement(By.Id("cmsuser-username")).Clear();
            Driver.FindElement(By.Id("cmsuser-username")).SendKeys("user_" + str);
            Driver.FindElement(By.Id("cmsuser-password")).Clear();
            Driver.FindElement(By.Id("cmsuser-password")).SendKeys("password_" + str);
            Driver.FindElement(By.Id("retype_password")).Clear();
            Driver.FindElement(By.Id("retype_password")).SendKeys("password_" + str);
            Driver.FindElement(By.Id("cmsuser-email")).Clear();
            Driver.FindElement(By.Id("cmsuser-email")).SendKeys("admin" + str + "@test.com");
            TakeScreenshot("23." + methodName + ".EnteredCompanyAdminData", tgName, tName);
            Driver.FindElement(By.XPath("(//button[@type='submit'])[2]")).Click();

            //Verify Company and Admin Creation
            Assert.AreEqual("The company has been saved.", Driver.FindElement(By.CssSelector("strong")).Text);
            Assert.AreEqual("The super admin has been saved.", Driver.FindElement(By.XPath("//div[2]/strong")).Text);
            Thread.Sleep(2000);
            TakeScreenshot("24." + methodName + ".CompanyCreated", tgName, tName);
            Assert.AreEqual("ComName_" + str, Driver.FindElement(By.CssSelector("td.post-title")).Text);
            Console.WriteLine(str);
        }
    }
}