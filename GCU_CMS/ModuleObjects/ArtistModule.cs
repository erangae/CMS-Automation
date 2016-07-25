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
    public class ArtistModule : SeleniumBase
    {
        public ArtistModule(IWebDriver driver)
        {
            Driver = driver;
        }

        public void VerifyAddArtist(string str, string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            //Verify Add Artist
            Driver.FindElement(By.XPath("//li[3]/a/div/p")).Click();
            Driver.FindElement(By.CssSelector("i.fa.fa-plus-circle")).Click();
            TakeScreenshot("40." + methodName + ".AddArtistPage", tgName, tName);
            Driver.FindElement(By.Id("artist-name-en")).Clear();
            Driver.FindElement(By.Id("artist-name-en")).SendKeys("Artist"+str);
            Driver.FindElement(By.Id("artist-description-en")).Clear();
            Driver.FindElement(By.Id("artist-description-en")).SendKeys("Artist Description"+str);
            Driver.FindElement(By.Id("artist-picture")).Click();
            Driver.FindElement(By.Id("doc-file")).Clear();
            Driver.FindElement(By.Id("doc-file")).SendKeys(datapath+"\\Artist1.jpg");
            Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);
            Driver.FindElement(By.Id("artist-explore-screen-image")).Click();
            Driver.FindElement(By.Id("explore-doc-file")).Clear();
            Driver.FindElement(By.Id("explore-doc-file")).SendKeys(datapath + "\\Artist2.jpg");
            Driver.FindElement(By.XPath("(//button[@type='submit'])[2]")).Click();
            Thread.Sleep(5000);
            new SelectElement(Driver.FindElement(By.Id("artist-country"))).SelectByText("Korea, Republic of");
            Thread.Sleep(1000);
            TakeScreenshot("41." + methodName + ".EnteredArtistData", tgName, tName);
            Driver.FindElement(By.XPath("(//button[@type='submit'])[3]")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("The Artist has been saved.", Driver.FindElement(By.CssSelector("strong")).Text);
            Assert.AreEqual("Artist"+str, Driver.FindElement(By.CssSelector("div.left-align")).Text);
            TakeScreenshot("42." + methodName + ".ArtistCreated", tgName, tName);
            Assert.AreEqual("Korea, Republic of", Driver.FindElement(By.XPath("//div[@id='common-listing1']/div/table/tbody/tr/td[3]")).Text);
        }
    }
}
