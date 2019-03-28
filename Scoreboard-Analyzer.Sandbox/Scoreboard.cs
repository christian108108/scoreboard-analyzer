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

        public void ServiceCheck(Bitmap _screenshot)
        {
            foreach(var school in this.Schools)
            {
                foreach(var service in school.Services)
                {
                    var pixel = _screenshot.GetPixel(service.XValue, school.YValue);
                    // if the service is up (green-ish), then update the history
                    if(pixel.G > 100)
                    {
                        service.UptimeHistory.Add(true);
                    }
                    // if the service is down, update the history with false
                    else
                    {
                        service.UptimeHistory.Add(false);
                    }
                }
            }
        }
    }
}
