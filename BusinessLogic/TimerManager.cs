using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager;
using TimerManagement;

namespace BusinessLogic
{
    public class TimerManager : ITimerManager
    {
        private DBManager.ITimerDataStorage _storage;
        private List<Timer> _allTimers;

        public TimerManager()
        {
            _storage = new TimerDataStorage();
            _allTimers = GetAllTimers();
        }

        public Timer CreateTimer(string Name)
        {
            Timer newTimer = new Timer(_storage.CreateTimerData(Name, DateTime.Now));
            _allTimers.Add(newTimer);
            return newTimer;
        }

        public bool DeleteTimer(Timer timer)
        {
            if (timer == null) return false;
            _allTimers.Remove(timer);
            return _storage.DeleteTimerData(timer.ID);
        }

        public List<Timer> GetAllTimers()
        {
            if (_allTimers != null)
            {
                return _allTimers;
            }
            List<TimerData> data = _storage.GetAll();
            List<Timer> timers = new List<Timer>();
            foreach (var timerData in data)
            {
                timers.Add(new Timer(timerData));
            }
            return _allTimers = timers;
        }

        public List<Timer> GetTimersByName(String Name)
        {
            List<Timer> result = new List<Timer>();
            foreach (var timer in GetAllTimers())
            {
                if(timer.Name.Contains(Name))
                    result.Add(timer);
            }
            return result;
        }

        public List<Timer> GetTimersByID(Int32 id)
        {
            List<Timer> result = new List<Timer>();
            foreach (var timer in GetAllTimers())
            {
                if (timer.ID == id)
                    result.Add(timer);
            }
            return result;
        }

        public List<Timer> GetTimersBetweenDates(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public List<Timer> GetTimersLaterThan(DateTime date)
        {
            throw new NotImplementedException();

        }

        public List<Timer> GetTimersEarlierThan(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            foreach (var timer in GetAllTimers())
            {
                _storage.SaveTimerData(timer.getReadyToSaveData());
            }
        }

        public void StopAndSaveAll()
        {
            foreach (var timer in GetAllTimers())
            {
                timer.Stop();
                _storage.SaveTimerData(timer.getReadyToSaveData());
            }
        }
    }
}
