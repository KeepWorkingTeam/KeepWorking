﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager;

namespace TimerManagement
{
    public class Timer
    {
        private DBManager.TimerData _data;
        private bool _isStarted;
        public string Name { get => _data.Name; set => _data.Name = value; }
        public DateTime CreationDate => DateCopy(_data.CreationDate);
        public int ID => _data.ID;

        public Timer(TimerData data)
        {
            _data = data;
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

        public TimerData getReadyToSaveData()
        {
            TimerData data = new TimerData();
            data.ID = _data.ID;
            data.Name = String.Copy(_data.Name);
            data.CreationDate = CreationDate;
            data.StartedDateTimes = _data.StartedDateTimes;
            data.StoppedDateTimes = _data.StoppedDateTimes;
            data.TimeElapsed = _data.TimeElapsed;
            if(_isStarted)
            {
                data.StartedDateTimes.Add(DateTime.Now);
                DateTime firsTime = data.StartedDateTimes[data.StartedDateTimes.Count - 1];
                DateTime secondTime = data.StoppedDateTimes[data.StoppedDateTimes.Count - 1];
                data.TimeElapsed += (secondTime - firsTime);
            }
            return data;
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
