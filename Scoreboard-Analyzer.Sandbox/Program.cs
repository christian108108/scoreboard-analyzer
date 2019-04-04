using System;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Newtonsoft.Json;

namespace Scoreboard_Analyzer.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Dimensions
            // x values of the first and last rows
            int FIRST_COL_X = 146;
            int LAST_COL_X = 959;

            // y value of the first and last columns
            int FIRST_ROW_Y = 125;
            int LAST_ROW_Y = 505;
            #endregion

            // just using this for easy debugging
            string[] schoolNames = {"UNCW", "KSU", "USA", "UF", "CSU", "CU", "UTC", "UCF"};
            string[] serviceNames = {"DB", "DNS", "ECOM", "MGR", "POP3", "SMTP", "SSH", "WWW"};

            // create scoreboard object from given information
            Scoreboard scoreboard = new Scoreboard(schoolNames, serviceNames, FIRST_COL_X, LAST_COL_X, FIRST_ROW_Y, LAST_ROW_Y);

            // getting all file names
            string[] filePaths = Directory.GetFiles("screenshots/","*.png");

            // loop through the 10 example screenshots
            foreach(var path in filePaths)
            {
                // process service check and add to scoreboard history
                scoreboard.ProcessServiceCheck(path);
            }

            string output = JsonConvert.SerializeObject(scoreboard, Formatting.Indented);

            Console.WriteLine($"Results as of {DateTime.Now}");
            Console.WriteLine(output);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter("results.txt"))
            {
                outputFile.WriteLine($"Results as of {DateTime.Now} EST");
                outputFile.WriteLine(output);
            }
        }
    }
}
