using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBManager
{
    public class DBManager
    {
        string path = @"..\..\..\DBManager\DB.xml";
        public void WriteToDB(string fileName, TimeSpan duration)
        {
            XDocument DBXmlVersion = XDocument.Load(path);

            FileHistory fileHistory = GetFileHistory(fileName);

            if (fileHistory == null)
            {
                DBXmlVersion.Element("timespans").Add(new XElement("timespan",
                        new XElement("name", fileName),
                        new XElement("today", duration.ToString()),
                        new XElement("thisweek", duration.ToString()),
                        new XElement("totaltime", duration.ToString())));
            }
            else
            {
                foreach (XElement file in DBXmlVersion.Element("timespans").Elements("timespan"))
                {
                    string name = file.Element("name").Value;

                    if (name == fileName)
                    {
                        TimeSpan newToday = duration + TimeSpan.Parse(file.Element("today").Value);
                        TimeSpan newThisWeek = duration + TimeSpan.Parse(file.Element("thisweek").Value);
                        TimeSpan newTotalTime = duration + TimeSpan.Parse(file.Element("totaltime").Value);

                        file.Element("today").Value = newToday.ToString();
                        file.Element("thisweek").Value = newThisWeek.ToString();
                        file.Element("totaltime").Value = newTotalTime.ToString();

                        break;
                    }
                }
            }
            DBXmlVersion.Save(path);
        }

        public FileHistory GetFileHistory(string fileName)
        {
            XDocument DBXmlVersion = XDocument.Load(path);

            var items = from element in DBXmlVersion.Element("timespans").Elements("timespan")
                        where element.Element("name").Value == fileName
                        select new FileHistory
                        {
                            FileName = element.Element("name").Value,
                            TodayTime = TimeSpan.Parse(element.Element("today").Value),
                            ThisWeekTime = TimeSpan.Parse(element.Element("thisweek").Value),
                            TotalTime = TimeSpan.Parse(element.Element("totaltime").Value)
                        };

            return (FileHistory)items;
        }

        public void StartNewDay()
        {
            XDocument DBXmlVersion = XDocument.Load(path);

            foreach (XElement file in DBXmlVersion.Element("timespans").Elements("timespan"))
            {
                file.Element("today").Value = TimeSpan.FromSeconds(0).ToString();
            }
        }

        public void StartNewWeek()
        {
            XDocument DBXmlVersion = XDocument.Load(path);

            foreach (XElement file in DBXmlVersion.Element("timespans").Elements("timespan"))
            {
                file.Element("thisweek").Value = TimeSpan.FromSeconds(0).ToString();
            }
        }
    }
}
