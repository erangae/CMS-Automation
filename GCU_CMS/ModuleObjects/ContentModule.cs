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
using System.Diagnostics;

namespace GCU_CMS.ModuleObjects
{
    public class ContentModule : SeleniumBase
    {
        DateTime today = DateTime.Now;

        public ContentModule(IWebDriver driver)
        {
            Driver = driver;
        }

        public void VerifyAddContent(string type, string level, string media, string pathcontent, string pathpreview, string owner, string str, string tgName, string tName)
        {
            string methodName = new System.Diagnostics.StackFrame(0).GetMethod().Name;
            //Verify Add Content
            Driver.FindElement(By.XPath("//li[2]/a/div/p")).Click();
            Driver.FindElement(By.CssSelector("i.fa.fa-plus-circle")).Click();
            TakeScreenshot("60." + methodName + ".AddContentPage", tgName, tName);
            Driver.FindElement(By.Id("title-english")).Clear();
            Driver.FindElement(By.Id("title-english")).SendKeys(media + "En_" + type + "_" + str);
            Driver.FindElement(By.Id("title-korean")).Clear();
            Driver.FindElement(By.Id("title-korean")).SendKeys(media + "Ko_" + type + "_" + str);
            new SelectElement(Driver.FindElement(By.Id("type"))).SelectByText(type);
            //Driver.FindElement(By.Id("editor1")).Clear();
            //Driver.FindElement(By.Id("editor1")).SendKeys("Image Description"+str);
            //Driver.FindElement(By.LinkText("Korean")).Click();
            //Driver.FindElement(By.Id("editor2")).Clear();
            //Driver.FindElement(By.Id("editor2")).SendKeys("Image1");
            if (type == "Fixed Price")
            {
                Driver.FindElement(By.Id("fixed-price")).Clear();
                Driver.FindElement(By.Id("fixed-price")).SendKeys("100");
            }
            else if (type == "Fixed Countdown")
            {
                Driver.FindElement(By.Id("fc-fixed-price")).Clear();
                Driver.FindElement(By.Id("fc-fixed-price")).SendKeys("100");
                new SelectElement(Driver.FindElement(By.Name("countdown_time_zone"))).SelectByText("US/Alaska -09:00");
                Thread.Sleep(3000);
                Driver.FindElement(By.Id("datepicker1")).Click();
                Thread.Sleep(3000);
                //Driver.FindElement(By.Id("datepicker1")).Clear();
                Driver.FindElement(By.Id("datepicker1")).SendKeys(Keys.Control + "a");
                Console.WriteLine(today.AddDays(7).ToString("dd-MM-yyyy"));
                Driver.FindElement(By.Id("datepicker1")).SendKeys(today.AddDays(7).ToString("dd-MM-yyyy")+" 1:00:00");
                //Assert.AreEqual("16-7-2016 1:00:00", driver.FindElement(By.Id("datepicker1")).GetAttribute("value"));
                Thread.Sleep(3000);
                //Driver.FindElement(By.CssSelector("span.glyphicon.glyphicon-chevron-right")).Click();
                //Driver.FindElement(By.XPath("//th[3]/span")).Click();

                //Thread.Sleep(3000);
                //Driver.FindElement(By.XPath("//div[@id='MACCDatePicker1']/div[2]/ul/li/div/div/div/div/ul/li/div/div/table/tbody/tr[2]/td[2]")).Click();
                //Thread.Sleep(3000);
                //Driver.FindElement(By.CssSelector("span.glyphicon.glyphicon-time")).Click();
                //Thread.Sleep(3000);
                //Driver.FindElement(By.CssSelector("span.glyphicon.glyphicon-chevron-up")).Click();

            }
            else if (type == "Duration Countdown")
            {
                Driver.FindElement(By.Id("dc-duration")).Clear();
                Driver.FindElement(By.Id("dc-duration")).SendKeys("24");
                Driver.FindElement(By.Id("dc-fixed-price")).Clear();
                Driver.FindElement(By.Id("dc-fixed-price")).SendKeys("100");
            }
            //Driver.FindElement(By.XPath("//div[@id='sizzle1468000877587']/div/table/tbody/tr[2]/td[3]")).Click();
            new SelectElement(Driver.FindElement(By.Id("level"))).SelectByText(level);
            // ERROR: Caught exception [Error: locator strategy either id or name must be specified explicitly.]

            Thread.Sleep(2000);

            
            //Driver.FindElement(By.XPath("//span/input")).Click();

            IWebElement element = Driver.FindElement(By.Id("fileupload"));

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0," + element.Location.Y + ")");

            IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript("arguments[0].click();", element);

            Console.WriteLine(pathcontent);
            Process.Start(pathcontent);

            if (media == "Image")
            {
                Assert.AreEqual("", Driver.FindElement(By.CssSelector("canvas")).Text);
            }
            else if (media == "Video")
            {
                Assert.AreEqual("", Driver.FindElement(By.Id("myVideo")).Text);
            }


            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            Assert.AreEqual("Remove", Driver.FindElement(By.CssSelector("button.removeFile")).Text);
            if (pathpreview == "Snap")
            {
                Assert.AreEqual("Take Snap", Driver.FindElement(By.CssSelector("button.startTime")).Text);
                Driver.FindElement(By.CssSelector("button.startTime")).Click();
                Thread.Sleep(1000);
                Assert.AreEqual("", Driver.FindElement(By.CssSelector("#snap_show_preview > img")).Text);
                Thread.Sleep(1000);
            }
            else if (pathpreview == "")
            {

            }
            else
            {

                Driver.FindElement(By.Id("preview_file_video")).Click();

                IWebElement element1 = Driver.FindElement(By.Id("preview_fileupload"));

                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0," + element1.Location.Y + ")");

                IJavaScriptExecutor executor1 = (IJavaScriptExecutor)Driver;
                executor1.ExecuteScript("arguments[0].click();", element1);
                Process.Start(pathpreview);
                Assert.AreEqual("", Driver.FindElement(By.CssSelector("canvas")).Text);
                Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
                Assert.AreEqual("Remove", Driver.FindElement(By.CssSelector("#preview_files > button.removeFile")).Text);

            }
            TakeScreenshot("61." + methodName + ".EnteredContentData", tgName, tName);
            Driver.FindElement(By.XPath("(//button[@type='button'])[5]")).Click();
            Driver.FindElement(By.Name("tags")).Click();
            TakeScreenshot("62." + methodName + ".AddTagPage", tgName, tName);
            Driver.FindElement(By.Name("tags")).Clear();
            Driver.FindElement(By.Name("tags")).SendKeys("#"+media);
            TakeScreenshot("63." + methodName + ".EnteredTag", tgName, tName);
            Driver.FindElement(By.XPath("(//button[@type='button'])[6]")).Click();
            if (owner == "Artist")
            {
                new SelectElement(Driver.FindElement(By.Id("content_owner_type"))).SelectByText("Artists");
                Thread.Sleep(1000);
                Driver.FindElement(By.Id("search")).Clear();
                Driver.FindElement(By.Id("search")).SendKeys(str);
                Driver.FindElement(By.XPath("//input[@value='Search Artists']")).Click();
                Thread.Sleep(2000);
                //Driver.FindElement(By.XPath("//td/div/input")).Click();
                TakeScreenshot("64." + methodName + ".SelectArtistPage", tgName, tName);
                Driver.FindElement(By.CssSelector("td > div.checkbox > label.image-checkbox-label")).Click();
                Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            }
            else if (owner == "Group")
            {
                Driver.FindElement(By.Id("search")).Clear();
                Driver.FindElement(By.Id("search")).SendKeys(str);
                Driver.FindElement(By.XPath("//input[@value='Search Groups']")).Click();
                Thread.Sleep(2000);
                //Driver.FindElement(By.Id("group_133")).Click();
                TakeScreenshot("65." + methodName + ".SelecteGroupPage", tgName, tName);
                Driver.FindElement(By.CssSelector("td > div.checkbox > label.image-checkbox-label")).Click();
                Driver.FindElement(By.XPath("(//button[@type='button'])[7]")).Click();
                Assert.AreEqual("Artist" + str, Driver.FindElement(By.CssSelector("#data5 > #list-wrapper > #common-listing > #common-listing1 > div.table-responsive > table.table > tbody > tr > td.post-title > div.left-align")).Text);
                //Driver.FindElement(By.Id("grartist_311")).Click();
                TakeScreenshot("66." + methodName + ".SelectGroupArtistPage", tgName, tName);
                Driver.FindElement(By.CssSelector("#data5 > #list-wrapper > #common-listing > #common-listing1 > div.table-responsive > table.table > tbody > tr > td > div.checkbox > label.image-checkbox-label")).Click();
                Driver.FindElement(By.XPath("(//button[@type='submit'])[2]")).Click();
            }
            TakeScreenshot("67." + methodName + ".ContentCreated", tgName, tName);
            Assert.AreEqual("The content has been saved.", Driver.FindElement(By.CssSelector("strong")).Text);
            Assert.AreEqual(media + "En_" + type + "_" + str, Driver.FindElement(By.CssSelector("td.post-title")).Text);
            Assert.AreEqual("user_"+str, Driver.FindElement(By.XPath("//div[@id='common-listing1']/div/table/tbody/tr/td[3]")).Text);
            Assert.AreEqual("#"+media, Driver.FindElement(By.XPath("//div[@id='common-listing1']/div/table/tbody/tr/td[5]")).Text);
            Assert.AreEqual("English, Korean", Driver.FindElement(By.XPath("//div[@id='common-listing1']/div/table/tbody/tr/td[6]")).Text);
            Assert.AreEqual("Draft", Driver.FindElement(By.XPath("//div[@id='common-listing1']/div/table/tbody/tr/td[8]")).Text);
            Driver.FindElement(By.XPath("//div[@id='common-listing1']/div/table/tbody/tr/td[5]")).Click();
            Driver.FindElement(By.XPath("//td[2]/div/div/div[3]")).Click();
            Thread.Sleep(1000);
            TakeScreenshot("68." + methodName + ".ContentPublishPage", tgName, tName);
            Assert.AreEqual("Set Publish Date", Driver.FindElement(By.Id("myModalLabel")).Text);
            //Driver.FindElement(By.XPath("//div[@id='sizzle1468001069546']/div/table/tbody/tr[2]/td[7]")).Click();
            Console.WriteLine(today.ToString("dd-MM-yyyy"));

            Driver.FindElement(By.Id("datepicker1")).Click();
            Driver.FindElement(By.XPath("(//td[@class='day  active'])")).Click();

            //Driver.FindElement(By.Id("datepicker1")).Clear();
            //Console.WriteLine(today.ToString("dd-MM-yyyy"));
            //Driver.FindElement(By.Id("datepicker1")).SendKeys(today.ToString("dd-MM-yyyy"));

            Driver.FindElement(By.XPath("//button[@type='submit']")).Click();


            ElementPresentTest.WaitForTextPresentXpath("Published", ("//div[@id='common-listing1']/div/table/tbody/tr/td[8]"));
            TakeScreenshot("69." + methodName + ".ContentPublished", tgName, tName);
            Assert.AreEqual("Published", Driver.FindElement(By.XPath("//div[@id='common-listing1']/div/table/tbody/tr/td[8]")).Text);
            Console.WriteLine("OK");
        }
    }
}
