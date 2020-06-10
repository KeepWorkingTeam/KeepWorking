using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager
{
    interface ITimerDataStorage
    {
        List<TimerData> getAll();

        List<TimerData> getByName(String name);
        
        TimerData CreateTimerData(string Name, DateTime creationTime);

        bool DeleteTimerData(int ID);
    }
}
