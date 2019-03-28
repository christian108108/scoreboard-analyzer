using System;
using System.Collections.Generic;
using System.Linq;

namespace Scoreboard_Analyzer.Sandbox
{
    public class Service
    {
        public readonly string Name;

        public readonly int XValue;

        public List<bool> UptimeHistory;

        public Service(string _name, int _xValue)
        {
            this.Name = _name;
            this.XValue = _xValue;
            this.UptimeHistory = new List<bool>();
        }

        public decimal GetUptime()
        {
            // count all the times the service is up
            Func<bool, bool> ifTrue = x => x;
            int upCount = this.UptimeHistory.Count(ifTrue);

            // calculate a percentage of uptime
            return upCount / UptimeHistory.Count;
        }

        public int GetViolations()
        {
            int violations = 0;
            int downtimeCounter = 0;
            foreach (var check in this.UptimeHistory)
            {
                // if service is down in this check, increment the counter
                if (!check)
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
