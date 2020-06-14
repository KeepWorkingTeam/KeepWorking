using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBManager;
using System.Collections.Generic;

namespace KeepWorkingTest
{
    [TestClass]
    public class DbUnitTest1
    {
        ITimerDataStorage timerDataStorage = new TimerDataStorage();

        [TestMethod]
        public void GetAllNotNullable()
        {
            var allTimers = timerDataStorage.GetAll();
            Assert.IsNotNull(allTimers);
        }

        [TestMethod]
        public void AddDeleteTimer()
        {
            var allTimers = timerDataStorage.GetAll();
            var timer = timerDataStorage.CreateTimerData("Test timer", DateTime.Now);
            Assert.IsNotNull(timer);
            Assert.AreEqual(allTimers.Count + 1, timerDataStorage.GetAll().Count);

            timerDataStorage.DeleteTimerData(timer.ID);
            Assert.AreEqual(allTimers.Count, timerDataStorage.GetAll().Count);
        }

        [TestMethod]
        public void GetChangeSaveCheck()
        {
            var timer = timerDataStorage.CreateTimerData("GetChangeSaveCheck", DateTime.Now);
            
            timer.Name = "NewTimerName";
            timerDataStorage.SaveTimerData(timer);

            var allTimers = timerDataStorage.GetAll();
            var timerEdited = timerDataStorage.GetByID(timer.ID);
            Assert.AreEqual(timerEdited.Name, "NewTimerName");

            timerDataStorage.DeleteTimerData(timer.ID);
        }

        [TestMethod]
        public void GetNames()
        {
            var timer1 = timerDataStorage.CreateTimerData("TestGetName", DateTime.Now);
            var timer2 = timerDataStorage.CreateTimerData("TestGetName", DateTime.Now);

            List<TimerData> named = timerDataStorage.GetByName("TestGetName");

            Assert.IsTrue(named.Count >= 2);
            timerDataStorage.DeleteTimerData(timer1.ID);
            timerDataStorage.DeleteTimerData(timer2.ID);
        }
    }
}
