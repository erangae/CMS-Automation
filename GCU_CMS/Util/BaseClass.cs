using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using GCU_CMS.ModuleObjects;
using System.Configuration;
using System.IO;

namespace GCU_CMS.Util
{
    public class BaseClass
    {
        public string datapath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Data"));
        public static LoginModule LoginModule;
        public static CompanyModule CompanyModule;
        public static DashboardModule DashboardModule;
        public static ArtistModule ArtistModule;
        public static GroupModule GroupModule;
        public static ContentModule ContentModule;
        public static ElementPresentTest ElementPresentTest;
        public static IWebDriver Driver;

        public static void Setup()
        {
            Console.WriteLine("E3");
            Driver = GetWebDriver(ConfigurationSettings.AppSettings["browser"]);
            string baseUrl = ConfigurationSettings.AppSettings["baseUrl"];
            Console.WriteLine("E4");
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(baseUrl);
            LoginModule = new LoginModule(Driver);
            CompanyModule = new CompanyModule(Driver);
            DashboardModule = new DashboardModule(Driver);
            ArtistModule = new ArtistModule(Driver);
            GroupModule = new GroupModule(Driver);
            ContentModule = new ContentModule(Driver);
            ElementPresentTest = new ElementPresentTest(Driver);
        }

        public static IWebDriver GetWebDriver(string browser)
        {
            IWebDriver driver = null;

            if (browser.Equals("firefox"))
            {
                driver = new FirefoxDriver();
            }

            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(120));
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(120));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(120));
            return driver;
        }

        public static void CreateFolders(String testGroupName, String testName)
        {
            String screenshotpath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Screenshots"));
            if (!Directory.Exists(screenshotpath + "/" + testGroupName))
                Directory.CreateDirectory(screenshotpath + "/" + testGroupName);
            if (!Directory.Exists(screenshotpath + "/" + testGroupName + "/" + testName))
                Directory.CreateDirectory(screenshotpath + "/" + testGroupName + "/" + testName);
        }

        public void TakeScreenshot(String filename, String gtName, String gName)
        {
            String screenshotpath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Screenshots"));
            try
            {
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                ss.SaveAsFile(@screenshotpath + "/" + gtName + "/" + gName + "/" + filename + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
