using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using BusinessLogic.Annotations;

namespace KeepWorkingTest
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void TestCreationTimer()
        {
            TimerManager tm = new TimerManager();
            tm.CreateTimer("Timer");
        }
        
        [TestMethod]
        public void TestCreationStartStop()
        {
            TimerManager tm = new TimerManager();
            tm.CreateTimer("Timer");
            foreach (var timer in tm.GetAllTimers())
            {
                timer.Start();
            }

            for (int i = 0; i < 10000; i++)
            {
                i++;
                i--;
                i *= 3;
                i /= 3;
            }
            foreach (var timer in tm.GetAllTimers())
            {
                timer.Stop();
            }
            Assert.IsTrue(tm.GetTimersByName("Timer")[0].TimeElapsed > TimeSpan.Zero);
        }

        [TestMethod]
        public void TestCreateDelete()
        {
            TimerManager tm = new TimerManager();
            int sizeBefore = tm.GetTimersByName("Timer").Count;
            int ID = tm.CreateTimer("Timer").ID;
            Assert.IsNotNull(tm.GetTimersByName("Timer")[0]);
            Assert.IsTrue(sizeBefore < tm.GetTimersByName("Timer").Count);
            foreach (var timer in tm.GetTimersByName("Timer"))
            {
                if(timer.ID == ID)
                    tm.DeleteTimer(timer);
            }
            Assert.IsTrue(tm.GetTimersByName("Timer").Count == sizeBefore);
        }
        [TestMethod]
        public void TestSaving()
        {
            TimerManager tm = new TimerManager();
            tm.SaveAll();
        }
    }
}