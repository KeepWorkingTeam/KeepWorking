using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager
{
    public class FileHistory
    {
        public string FileName { get; set; } 
        public TimeSpan TodayTime { get; set; }
        public TimeSpan ThisWeekTime { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
