using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerManagement
{
    public interface ITimerManager
    {
        Timer CreateTimer(String Name);
        bool DeleteTimer(Timer timer);
        List<Timer> GetAllTimers();
        List<Timer> GetTimersByName(String Name);
        List<Timer> GetTimersBetweenDates(DateTime from, DateTime to); 
        List<Timer> GetTimersLaterThan(DateTime date); 
        List<Timer> GetTimersEarlierThan(DateTime date);
        void SaveAll();
        void StopAndSaveAll();
    }
}
