using System.Collections.Generic;
using System.Linq;

namespace Scoreboard_Analyzer.Sandbox
{
    public class Service
    {
        public readonly string Name;

        public readonly int XValue;

        public List<ServiceCheck> ServiceCheckHistory;

        public Service(string _name, int _xValue)
        {
            this.Name = _name;
            this.XValue = _xValue;
            this.ServiceCheckHistory = new List<ServiceCheck>();
        }

        public decimal Uptime
        {
            get
            {
                // count how many times the service has been up in its history
                int upCount = 0;
                foreach (var serviceCheck in ServiceCheckHistory)
                {
                    if (serviceCheck.Status)
                    {
                        upCount++;
                    }
                }

                // calculate a percentage of uptime
                return (decimal)upCount / (decimal)ServiceCheckHistory.Count;
            }
        }

        public int Violations
        {
            get
            {
                // how many checks it takes to equal 1 SLA violation
                int violations = 0;
                int downtimeCounter = 0;
                foreach (var check in this.ServiceCheckHistory)
                {
                    // if service is down in this check, increment the counter
                    if (!check.Status)
                    {
                        downtimeCounter++;
                    }

                    // if the service is up, reset the counter
                    else
                    {
                        downtimeCounter = 0;
                    }

                    // if the service has been down 3 times in a row, then fail give the service a violation
                    if (downtimeCounter == 3)
                    {
                        violations++;
                        downtimeCounter = 0;
                    }
                }
                return violations;
            }
        }
    }
}
