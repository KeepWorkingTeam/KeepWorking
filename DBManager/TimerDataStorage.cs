using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;

namespace DBManager
{
    public class TimerDataStorage : ITimerDataStorage
    {
        public static readonly string SolutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.FullName;
        private string path = SolutionPath + @"\DBManager\DB.xml";
        private string pathID = SolutionPath + @"\DBManager\TimerID.json";
        public TimerData CreateTimerData(string Name, DateTime creationTime)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<timers>\n</timers>");
            }

            XDocument dB = XDocument.Load(path);

            TimerData timerData = new TimerData
            {
                ID = GetFreshTimerID(),
                Name = Name,
                TimeElapsed = TimeSpan.Zero,
                CreationDate = creationTime,
                StartedDateTimes = new List<DateTime>(),
                StoppedDateTimes = new List<DateTime>()
            };

            dB.Element("timers").Add(new XElement("timer",
                        new XElement("Name", timerData.Name),
                        new XElement("ID", timerData.ID.ToString()),
                        new XElement("TimeElapsed", timerData.TimeElapsed.ToString()),
                        new XElement("CreationDate", timerData.CreationDate.ToString("dd.MM.yyyy HH:mm:ss")),
                        new XElement("StartedDateTimes", JsonConvert.SerializeObject(timerData.StartedDateTimes)),
                        new XElement("StoppedDateTimes", JsonConvert.SerializeObject(timerData.StoppedDateTimes))));

            dB.Save(path);

            return timerData;
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
            if (!File.Exists(path))
            {
                return new List<TimerData>();
            }

            XDocument dB = XDocument.Load(path);

            var result = from element in dB.Element("timers").Elements("timer")
                         select new TimerData
                         {
                             ID = int.Parse(element.Element("ID").Value),
                             Name = element.Element("Name").Value,
                             TimeElapsed = TimeSpan.Parse(element.Element("TimeElapsed").Value),
                             CreationDate = DateTime.ParseExact(element.Element("CreationDate").Value, "dd.MM.yyyy HH:mm:ss", new DateTimeFormatInfo()),
                             StartedDateTimes = JsonConvert.DeserializeObject<List<DateTime>>(element.Element("StartedDateTimes").Value),
                             StoppedDateTimes = JsonConvert.DeserializeObject<List<DateTime>>(element.Element("StoppedDateTimes").Value)
                         };

            return result.ToList<TimerData>() ?? new List<TimerData>();
        }

        public List<TimerData> GetByName(string name)
        {
            if (!File.Exists(path))
            {
                return new List<TimerData>();
            }

            XDocument dB = XDocument.Load(path);

            var result = GetAll().Where(x => x.Name.Equals(name));

            return result.ToList<TimerData>() ?? new List<TimerData>();
        }

        public TimerData GetByID(int iD)
        {
            XDocument dB = XDocument.Load(path);

            var result = GetAll().First(x => x.ID == iD);

            return result != null ? (TimerData)result: new TimerData();
        }

        public bool SaveTimerData(TimerData timerData)
        {
            DeleteTimerData(timerData.ID);
            XDocument dB = XDocument.Load(path);

            dB.Element("timers").Add(new XElement("timer",
                        new XElement("Name", timerData.Name),
                        new XElement("ID", timerData.ID.ToString()),
                        new XElement("TimeElapsed", timerData.TimeElapsed.ToString()),
                        new XElement("CreationDate", timerData.CreationDate.ToString()),
                        new XElement("StartedDateTimes", JsonConvert.SerializeObject(timerData.StartedDateTimes)),
                        new XElement("StoppedDateTimes", JsonConvert.SerializeObject(timerData.StoppedDateTimes))));

            dB.Save(path);
            return true;
        }

        private int GetFreshTimerID()
        {
            if (!File.Exists(pathID))
            {
                File.WriteAllText(pathID, "0");
            }

            int iD = int.Parse(File.ReadAllText(pathID));
            iD++;

            File.WriteAllText(pathID, iD.ToString());
            return iD;
        }
    }
}
