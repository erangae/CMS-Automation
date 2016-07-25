using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using GCU_CMS.Util;
using System.IO;

namespace GCU_CMS.Scripts
{
    [TestClass]
    public class SmokeTests : BaseClass
    {
        private TestContext _testContextInstance;
        public String type, level, media, pathcontent, pathpreview, owner;
        public String TestGroupName, TestName;
 
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        private static StringBuilder _verificationErrors;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _verificationErrors = new StringBuilder();
            Setup();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
            }
        }

        [TestInitialize]
        public void SetupTest()
        {
            _verificationErrors.Clear();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Console.WriteLine(_verificationErrors.ToString());
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK01_VerifyLoginAsSuperSuperAdmin()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                LoginModule.LoginToCMS("ssadmin@zeerowapp.com", "Nop@ss1234", TestGroupName, TestName);
                DashboardModule.VerifySuperSuperAdminDashboard(TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK02_VerifyCompanyCreationWithAdmin()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                CompanyModule.CreateACompany(TestGroupName, TestName);
                LoginModule.Logout(TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK03_VerifyLoginAsNewCompanyAdmin()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                var tmp = File.ReadAllText(@datapath+"\\path.txt");
                LoginModule.LoginToCMS("admin" + tmp + "@test.com", "password_" + tmp, TestGroupName, TestName);
                DashboardModule.VerifyCompanyAdminDashboard(TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK04_VerifyArtistCreation()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                var tmp = File.ReadAllText(@datapath + "\\path.txt");
                ArtistModule.VerifyAddArtist(tmp, TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK05_VerifyGroupCreation()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                var tmp = File.ReadAllText(@datapath + "\\path.txt");
                DashboardModule.NavigateToDashboard(TestGroupName, TestName);
                GroupModule.VerifyAddGroup(tmp, TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK06_VerifyContentCreation_Image_FixedPrice_Artist()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                type = "Fixed Price";
                level = "1";
                media = "Image";
                pathcontent = datapath + "\\Image.au3";
                pathpreview = "";
                owner = "Artist";
                var tmp = File.ReadAllText(@datapath + "\\path.txt");
                DashboardModule.NavigateToDashboard(TestGroupName, TestName);
                ContentModule.VerifyAddContent(type, level, media, pathcontent, pathpreview, owner, tmp, TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK07_VerifyContentCreation_Video_Free_Artist_Snap()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                type = "Free";
                level = "2";
                media = "Video";
                pathcontent = datapath + "\\Video.au3";
                pathpreview = "Snap";
                owner = "Artist";
                var tmp = File.ReadAllText(@datapath + "\\path.txt");
                DashboardModule.NavigateToDashboard(TestGroupName, TestName);
                ContentModule.VerifyAddContent(type, level, media, pathcontent, pathpreview, owner, tmp, TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK08_VerifyContentCreation_Video_FixedCountdown_Artist_Preview()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                type = "Fixed Countdown";
                level = "3";
                media = "Video";
                pathcontent = datapath + "\\Video.au3";
                pathpreview = datapath + "\\Image.au3";
                owner = "Artist";
                var tmp = File.ReadAllText(@datapath + "\\path.txt");
                DashboardModule.NavigateToDashboard(TestGroupName, TestName);
                ContentModule.VerifyAddContent(type, level, media, pathcontent, pathpreview, owner, tmp, TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void SMK09_VerifyContentCreation_Image_DurationCountdown_Group()
        {
            TestGroupName = GetType().Name;
            TestName = TestContext.TestName;
            CreateFolders(TestGroupName, TestName);
            try
            {
                type = "Duration Countdown";
                level = "4";
                media = "Image";
                pathcontent = datapath + "\\Image.au3";
                pathpreview = "";
                owner = "Group";
                var tmp = File.ReadAllText(@datapath + "\\path.txt");
                DashboardModule.NavigateToDashboard(TestGroupName, TestName);
                ContentModule.VerifyAddContent(type, level, media, pathcontent, pathpreview, owner, tmp, TestGroupName, TestName);
                LoginModule.Logout(TestGroupName, TestName);
            }
            catch (Exception e)
            {
                _verificationErrors.Append(e.Message);
                Assert.Fail(e.Message);
            }
        }
    }
}
