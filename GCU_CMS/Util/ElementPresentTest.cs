using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GCU_CMS.Util
{
    public class ElementPresentTest : SeleniumBase
    {
        public ElementPresentTest(IWebDriver driver)
        {
            Driver = driver;
        }

        public void WaitForElementPresentId(string argu1)
        {
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id(argu1))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
        }

        public void WaitForElementPresentCss(string argu1)
        {
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.CssSelector(argu1))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
        }

        public void WaitForElementPresentXpath(String Argu1)
        {
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.XPath(Argu1))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
        }

        public void WaitForTextPresentXpath(String Argu1, String Argu2)
        {
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (Argu1 == Driver.FindElement(By.XPath(Argu2)).Text) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
        }
    }
}
