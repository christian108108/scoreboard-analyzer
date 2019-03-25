using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Scoreboard_Analyzer.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            // make headless Firefox instance
            // NOTE: you must install selenium-gecko-driver from chocolatey or from https://github.com/mozilla/geckodriver/releases
            FirefoxOptions option = new FirefoxOptions();
            option.AddArgument("--headless");
            var driver = new FirefoxDriver(option);

            // navigate to scoreboard URL
            driver.Navigate().GoToUrl("https://bing.com");

            // take screenshot
            Screenshot ss = ((ITakesScreenshot) driver).GetScreenshot();
            
            // create unique filename that contains current datetime
            var filename = DateTime.Now.ToString("s").Replace(':', '-');

            // save screenshot in current directory
            ss.SaveAsFile($"screenshots/{filename}.png", ScreenshotImageFormat.Png);
        }
    }
}
