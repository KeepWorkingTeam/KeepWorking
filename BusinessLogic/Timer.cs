using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager;

namespace TimerManagement
{
    class Timer
    {
        private DBManager.TimerData _data;
        private bool _isStarted;
        public string Name { get => _data.Name; set => _data.Name = value; }

        public Timer()
        {
            _data = new TimerData();
        }

        public Timer(TimerData data)
        {
            _data = data;
        }

        public Timer(String Name)
        {
            _data = new TimerData();
            _data.Name = Name;
        }

        public TimeSpan TimeElapsed
        {
            get
            {
                if (!_isStarted)
                    return _data.TimeElapsed;
                DateTime firstDate = _data.StartedDateTimes[_data.StartedDateTimes.Count - 1];
                DateTime secondDate = DateTime.Now;
                return _data.TimeElapsed + (secondDate - firstDate);
            }
        }
        public DateTime CreationDate => DateCopy(_data.CreationDate);
        public void Start()
        {
            _isStarted = true;
            _data.StartedDateTimes.Add(DateTime.Now);
        }                                                            
        public void Stop()                                           
        {
            _isStarted = false;
            _data.StartedDateTimes.Add(DateTime.Now);
            DateTime firsTime = _data.StartedDateTimes[_data.StartedDateTimes.Count - 1];
            DateTime secondTime = _data.StoppedDateTimes[_data.StoppedDateTimes.Count - 1];
            _data.TimeElapsed += (secondTime - firsTime);
        }

        public List<DateTime> getAllStartedTimes()
        {
            List<DateTime> all = new List<DateTime>();
            foreach (var started in _data.StartedDateTimes)
                all.Add(DateCopy(started));
            return all;
        }
        public List<DateTime> getAllStoppedTimes()
        {
            List<DateTime> all = new List<DateTime>();
            foreach (var stopped in _data.StoppedDateTimes)
                all.Add(DateCopy(stopped));
            return all;
        }
        private DateTime DateCopy(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }
    }
}
