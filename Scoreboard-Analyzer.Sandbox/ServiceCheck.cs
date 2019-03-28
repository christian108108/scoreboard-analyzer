using System;

namespace Scoreboard_Analyzer.Sandbox
{
    public class ServiceCheck
    {
        public readonly DateTime UpdateTime;

        public readonly bool Status;

        public ServiceCheck(DateTime _updateTime, bool _status)
        {
            this.UpdateTime = _updateTime;
            this.Status = _status;
        }
    }
}
