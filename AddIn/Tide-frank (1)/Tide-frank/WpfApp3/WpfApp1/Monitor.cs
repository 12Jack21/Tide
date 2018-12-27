using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TideLearnigRecord
{
    [Serializable]
    public class Monitor
    {
        private string _MonitorSoftware;
        private TimeSpan _UsingTime;
        public Monitor()
        {

        }
        public Monitor(string ms, TimeSpan ut)
        {
            _MonitorSoftware = ms;
            _UsingTime = ut;
        }
        public string MonitorSoftware
        {
            get { return _MonitorSoftware; }
        }
        public TimeSpan UsingTime
        {
            get { return _UsingTime; }
        }
    }
}
