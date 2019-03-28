using System;
using System.Collections.Generic;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Scoreboard_Analyzer.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Dimensions
            // x values of the first and last rows
            int FIRST_COL_X = 83;
            int LAST_COL_X = 689;

            // y value of the first and last columns
            int FIRST_ROW_Y = 155;
            int LAST_ROW_Y = 760;
            #endregion

            List<string> schoolNames = AskUserForSchoolNames();
            List<string> serviceNames = AskUserForServiceNames();

            Scoreboard scoreboard = new Scoreboard(schoolNames, serviceNames, FIRST_COL_X, LAST_COL_X, FIRST_ROW_Y, LAST_ROW_Y);

            // loop through the 10 example screenshots
            for(int i=0; i<10; i++)
            {
                Bitmap scoreboardBitmap = new Bitmap($"screenshots/example{i}.jpg");
                scoreboard.ServiceCheck(scoreboardBitmap);
            }


        }

        /// <summary>
        /// takes screenshot of webpage and saves it as a .png file
        /// </summary>
        /// <param name="url">url of scoreboard</param>
        /// <param name="outputFolder">name of output folder</param>
        /// <param name="filename">name of image. Will default to a unique filename based on current DateTime</param>
        public static void TakeScreenshot(string url, string outputFolder="screenshots", string filename="")
        {
            // make headless Firefox instance
            // NOTE: you must install selenium-gecko-driver from chocolatey or from https://github.com/mozilla/geckodriver/releases
            FirefoxOptions option = new FirefoxOptions();
            option.AddArgument("--headless");
            var driver = new FirefoxDriver(option);

            // navigate to scoreboard URL
            driver.Navigate().GoToUrl(url);

            // take screenshot
            Screenshot ss = ((ITakesScreenshot) driver).GetScreenshot();
            
            // if user didn't specify a filename, create unique filename that contains current datetime
            if(String.IsNullOrWhiteSpace(filename))
            {
                filename = DateTime.Now.ToString("s").Replace(':', '-');
            }

            // if user really wants to save it in the current directory, let them
            if(String.IsNullOrWhiteSpace(outputFolder))
            {
                ss.SaveAsFile($"{filename}.png", ScreenshotImageFormat.Png);
                return;
            }

            // save screenshot in specified output folder directory
            ss.SaveAsFile($"{outputFolder}/{filename}.png", ScreenshotImageFormat.Png);
        }

        public static List<string> AskUserForSchoolNames()
        {
            List<string> schoolNames = new List<string>();

            // take user input for school and service names
            // Hit [ENTER] when done
            string input;
            Console.WriteLine("Enter the school names in order. Hit [ENTER] when done.");
            while(true)
            {
                input = Console.ReadLine();
                if(String.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                schoolNames.Add(input);
            }

            return schoolNames;
        }

        public static List<string> AskUserForServiceNames()
        {
            List<string> serviceNames = new List<string>();

            // take user input for service names
            // Hit [ENTER] when dones
            string input;
            Console.WriteLine("Enter the service names in order. Hit [ENTER] when done.");
            while(true)
            {
                input = Console.ReadLine();
                if(String.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                serviceNames.Add(input);
            }

            return serviceNames;
        }
    }
}
