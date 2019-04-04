using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Scoreboard_Analyzer.Sandbox
{
    public class Service
    {
        public readonly string Name;

        [JsonIgnore]
        public readonly int XValue;

        [JsonIgnore]
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
                var uptime = (decimal)upCount / (decimal)ServiceCheckHistory.Count;

                return Decimal.Round(uptime * 100, 2);
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
                    if (downtimeCounter == 12)
                    {
                        violations++;
                        downtimeCounter = 0;
                    }
                }
                return violations;
            }
        }

        public int ChecksUntilViolation
        {
            get
            {
                // count failures from history backwards
                int failCount = 0;
                for(int i = this.ServiceCheckHistory.Count; i-- > 0; )
                {
                    // if the status is down, count it
                    if(!this.ServiceCheckHistory[i].Status)
                    {
                        failCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                return 12-(failCount % 12);
            }
        }
    }
}
