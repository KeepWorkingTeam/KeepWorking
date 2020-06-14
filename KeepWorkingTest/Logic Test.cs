using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;

namespace KeepWorkingTest
{
    [TestClass]
    public class Logic_Test
    {
        [TestMethod]
        public void TestCreationTimer()
        {
            TimerManager tm = new TimerManager();
            tm.CreateTimer("Timer");
        }
    }
}