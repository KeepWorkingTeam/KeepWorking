using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager
{
    public interface ITimerDataStorage
    {
        List<TimerData> GetAll();

        List<TimerData> GetByName(String name);
        
        TimerData CreateTimerData(string Name, DateTime creationTime);

        bool DeleteTimerData(int ID);

        bool SaveTimerData(TimerData timerData);
    }
}
