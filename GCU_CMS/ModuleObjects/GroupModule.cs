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

namespace GCU_CMS.ModuleObjects
{
    public class GroupModule : SeleniumBase
    {
        public GroupModule(IWebDriver driver)
        {
            Driver = driver;
        }

        public void VerifyAddGroup(string str, string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            //Verify Add Group
            Driver.FindElement(By.XPath("//li[3]/a/div/p")).Click();
            Driver.FindElement(By.LinkText("Artist Management")).Click();
            Driver.FindElement(By.LinkText("Group Management")).Click();
            Driver.FindElement(By.CssSelector("i.fa.fa-plus-circle")).Click();
            TakeScreenshot("50." + methodName + ".AddGroupPage", tgName, tName);
            Driver.FindElement(By.Id("group-name-en")).Clear();
            Driver.FindElement(By.Id("group-name-en")).SendKeys("Group"+str);
            Driver.FindElement(By.Id("group-description-en")).Clear();
            Driver.FindElement(By.Id("group-description-en")).SendKeys("Group Description"+str);
            Driver.FindElement(By.Id("group-profile-pic-url")).Click();
            Driver.FindElement(By.Id("doc-file")).Clear();
            Driver.FindElement(By.Id("doc-file")).SendKeys(datapath + "\\Group1.jpg");
            Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);
            Driver.FindElement(By.Id("group-explore-screen-image")).Click();
            Driver.FindElement(By.Id("explore-doc-file")).Clear();
            Driver.FindElement(By.Id("explore-doc-file")).SendKeys(datapath + "\\Group2.jpg");
            Driver.FindElement(By.XPath("(//button[@type='submit'])[2]")).Click();
            Thread.Sleep(5000);
            TakeScreenshot("51." + methodName + ".EnteredGroupData", tgName, tName);
            //Driver.FindElement(By.CssSelector("button.close")).Click();
            Driver.FindElement(By.XPath("(//button[@type='button'])[5]")).Click();
            //Driver.FindElement(By.Id("artist_302")).Click();
            Driver.FindElement(By.XPath("//div[2]/ul/li/input")).Click();
            TakeScreenshot("52." + methodName + ".SelectedArtist", tgName, tName);
            Thread.Sleep(1000);
            Assert.AreEqual("Group Artist or Order has been changed.", Driver.FindElement(By.XPath("//div[@id='TabInfo1Part1']/div")).Text);
            Driver.FindElement(By.CssSelector("i.fa.fa-list")).Click();
            TakeScreenshot("53." + methodName + ".GroupCreated", tgName, tName);
            Assert.AreEqual("Group"+str, Driver.FindElement(By.CssSelector("div.left-align")).Text);

        }
    }
}
