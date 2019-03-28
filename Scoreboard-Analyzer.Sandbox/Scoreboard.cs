using System;
using System.Collections.Generic;
using System.Drawing;

namespace Scoreboard_Analyzer.Sandbox
{
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

        /// <summary>
        /// Updates scoreboard with proper uptime information
        /// </summary>
        /// <param name="_screenshot">screenshot of the scoreboard</param>
        public void ServiceCheck(Bitmap _screenshot)
        {
            // TESTING. PLS REPLACE LATER!!!
            DateTime timeOfServiceCheck = DateTime.Now;

            // loop through each school
            foreach(var school in this.Schools)
            {
                // loop through each service for each school
                foreach(var service in school.Services)
                {
                    // grab the pixel from the specific school and service
                    var pixel = _screenshot.GetPixel(service.XValue, school.YValue);
                    
                    // if the service is up, then update the history with a true. False if it's not green.
                    service.ServiceCheckHistory.Add(new ServiceCheck(timeOfServiceCheck, IsGreen(pixel)));
                }
            }
        }

        /// <summary>
        /// Checks to see if pixel is green-ish
        /// </summary>
        /// <param name="_pixel">Pixel from bitmap</param>
        /// <returns>Returns true if pixel is green. False if pixel is not green</returns>
        public static bool IsGreen(Color _pixel)
        {
            return _pixel.G > 100;
        }
    }
}
