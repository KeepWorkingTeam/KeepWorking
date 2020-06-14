using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DBManager
{
    [Serializable]
    public class TimerData
    {
        public int ID { get; set; }
        public TimeSpan TimeElapsed { get; set; }
        public String Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<DateTime> StartedDateTimes { get; set; }
        public List<DateTime> StoppedDateTimes { get; set; }

        public TimerData()
        {
            StartedDateTimes = new List<DateTime>();
            StoppedDateTimes = new List<DateTime>();
            Name = "";
        }
    }
}
