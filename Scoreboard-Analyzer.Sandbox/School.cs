using System.Collections.Generic;
using System.Linq;

namespace Scoreboard_Analyzer.Sandbox
{
    public class School
    {
        public readonly string Name;

        public readonly int YValue;

        public List<Service> Services;

        public School(string _name, int _yvalue, List<Service> _services)
        {
            this.Name = _name;
            this.YValue = _yvalue;
            this.Services = _services;
        }

        public decimal TotalUptime
        {
            get
            {
                List<decimal> uptime = new List<decimal>();
                foreach (var service in Services)
                {
                    uptime.Add(service.Uptime);
                }
                return uptime.Average();
            }
        }

        public int TotalViolations
        {
            get
            {
                // count all the violations from all the services in this team
                int count = 0;
                foreach (var service in Services)
                {
                    count += service.Violations;
                }
                return count;
            }
        }
    }
}
