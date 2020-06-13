using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace DBManager
{
    public class TimerDataStorage : ITimerDataStorage
    {
        private string path = @"..\..\..\DBManager\DB.xml";
        public TimerData CreateTimerData(string Name, DateTime creationTime)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTimerData(int ID)
        {
            XDocument dB = XDocument.Load(path);

            foreach (var element in dB.Element("timers").Elements("timer").Where(x => x.Element("ID").Value == ID.ToString()))
            {
                element.Remove();
                dB.Save(path);
                return true;
            }

            return false;
        }

        public List<TimerData> GetAll()
        {
            XDocument dB = XDocument.Load(path);

            var result = from element in dB.Element("timers").Elements("timer")
                         select new TimerData
                         {
                             ID = int.Parse(element.Element("ID").Value),
                             Name = element.Element("Name").Value,
                             TimeElapsed = TimeSpan.Parse(element.Element("TimeElapsed").Value),
                             CreationDate = DateTime.Parse(element.Element("CreationDate").Value),
                             StartedDateTimes = JsonConvert.DeserializeObject<List<DateTime>>(element.Element("StartedDateTimes").Value),
                             StoppedDateTimes = JsonConvert.DeserializeObject<List<DateTime>>(element.Element("StoppedDateTimes").Value)
                         };

            return result.ToList<TimerData>() ?? new List<TimerData>();
        }

        public List<TimerData> GetByName(string name)
        {
            XDocument dB = XDocument.Load(path);

            var result = from element in dB.Element("timers").Elements("timer")
                         where element.Element("Name").Value == name
                         select new TimerData
                         {
                             ID = int.Parse(element.Element("ID").Value),
                             Name = element.Element("Name").Value,
                             TimeElapsed = TimeSpan.Parse(element.Element("TimeElapsed").Value),
                             CreationDate = DateTime.Parse(element.Element("CreationDate").Value),
                             StartedDateTimes = JsonConvert.DeserializeObject<List<DateTime>>(element.Element("StartedDateTimes").Value),
                             StoppedDateTimes = JsonConvert.DeserializeObject<List<DateTime>>(element.Element("StoppedDateTimes").Value)
                         };

            return result.ToList<TimerData>() ?? new List<TimerData>();
        }

        public TimerData GetByID(int ID)
        {
            XDocument dB = XDocument.Load(path);

            var result = from element in dB.Element("timers").Elements("timer")
                         where element.Element("ID").Value == ID.ToString()
                         select new TimerData
                         {
                             ID = int.Parse(element.Element("ID").Value),
                             Name = element.Element("Name").Value,
                             TimeElapsed = TimeSpan.Parse(element.Element("TimeElapsed").Value),
                             CreationDate = DateTime.Parse(element.Element("CreationDate").Value),
                             StartedDateTimes = JsonConvert.DeserializeObject<List<DateTime>>(element.Element("StartedDateTimes").Value),
                             StoppedDateTimes = JsonConvert.DeserializeObject<List<DateTime>>(element.Element("StoppedDateTimes").Value)
                         };

            return (TimerData)result;
        }

        public bool SaveTimerData(TimerData timerData)
        {
            throw new NotImplementedException();
        }
    }
}
