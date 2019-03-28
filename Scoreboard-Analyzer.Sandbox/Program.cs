using System;
using System.Collections.Generic;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Scoreboard_Analyzer.Sandbox
{
    public class Service
    {
        public string Name;

        public decimal Uptime;

        public List<bool> UptimeHistory;

        public int Violations;

        public int XValue;

        public Service(string _name, int _xValue)
        {
            this.Name = _name;
            this.Uptime = 0;
            this.UptimeHistory = new List<bool>();
            this.Violations = 0;
            this.XValue = _xValue;
        }
}

    public class School
    {
        public string Name;

        public List<Service> Services;

        public int TotalViolations;

        public decimal TotalUptime;

        public int YValue;

        public School(string _name, int _yvalue, List<Service> _services)
        {
            this.Name = _name;
            this.YValue = _yvalue;
            this.Services = _services;
        }
    }

    public class Scoreboard
    {
        public List<School> Schools;

        public Scoreboard(IList<string> _schoolNames, IList<string> _serviceNames, int _firstColX, int _lastColX, int _firstRowY, int _lastRowY)
        {
            this.Schools = new List<School>();

            int colWidth = (_lastColX - _firstColX) / (_serviceNames.Count - 1);
            int rowHeight = (_lastRowY - _firstRowY) / (_schoolNames.Count - 1);

            List<int> schoolCoordsY = new List<int>();
            List<int> serviceCoordsX = new List<int>();
            
            for(int y=_firstRowY; y<=_lastRowY; y+=rowHeight)
            {
                schoolCoordsY.Add(y);
            }

            for(int x=_firstColX; x<=_lastColX; x+=colWidth)
            {
                serviceCoordsX.Add(x);
            }

            List<Service> _services = new List<Service>();
            for(int j=0; j<_serviceNames.Count; j++)
            {
                Service _service = new Service(_serviceNames[j], serviceCoordsX[j]);
                _services.Add(_service);
            }

            for(int i=0; i<_schoolNames.Count; i++)
            {
                School _school = new School(_schoolNames[i], schoolCoordsY[i], _services);
                this.Schools.Add(_school);
            }


        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            #region Dimensions
            // x values of the first and last rows
            int FIRST_COL = 83;
            int LAST_COL = 689;

            // y value of the first and last columns
            int FIRST_ROW = 155;
            int LAST_ROW = 760;
            #endregion

            List<string> schoolNames = new List<string>();

            List<string> serviceNames = new List<string>();

            string input = "";
            Console.WriteLine("Enter the school names in order.");
            while(true)
            {
                input = Console.ReadLine();
                if(String.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                schoolNames.Add(input);
            }

            Console.WriteLine("Enter the service names in order.");
            while(true)
            {
                input = Console.ReadLine();
                if(String.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                serviceNames.Add(input);
            }

            Scoreboard scoreboard = new Scoreboard(schoolNames, serviceNames, FIRST_COL, LAST_COL, FIRST_ROW, LAST_ROW);


            Bitmap scoreboardBitmap = new Bitmap($"screenshots/example9.jpg");

            // scoreboardBitmap.Save("screenshots/tada.bmp");

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
    }
}
