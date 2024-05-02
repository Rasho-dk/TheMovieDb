using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WsamTest
{
    [TestClass]
    public class BlazorTests
    {
        private static readonly string DriverDirectory = "C:\\Users\\shero\\OneDrive\\Dokumenter\\Selenimtest\\chrome-win64";

        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);
         
        }
       


        

        [TestMethod]
        public void TestMethod1()
        {
            _driver.Navigate().GoToUrl("https://localhost:44380/");

            var element = _driver.FindElement(By.TagName("h1"));
            Assert.AreEqual("Hello, world!", element.Text);
        }
    }
}