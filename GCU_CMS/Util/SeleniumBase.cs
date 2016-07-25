using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCU_CMS.Util
{
    public class SeleniumBase : BaseClass
    {
        public IWebDriver Driver;

        public void SendKeys(By by, string text)
        {
            Driver.FindElement(by).SendKeys(text);
        }

        public void Click(By by)
        {
            Driver.FindElement(by).Click();
        }

        public void Clear(By by)
        {
            Driver.FindElement(by).Clear();
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public int GetCount(By by)
        {
            return Driver.FindElements(by).Count;

        }
    }
}
