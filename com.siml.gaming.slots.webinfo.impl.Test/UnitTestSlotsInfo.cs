using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using com.siml.gaming.slots.webinfo.impl;
using com.siml.gaming.slots.webinfo.contract;
using System.ServiceModel;

namespace com.siml.gaming.slots.webinfo.impl.Test
{
    [TestClass]
    public class UnitTestSlotsInfo
    {
        ISlotsInfo service = null;

        private void Log(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        [TestInitialize]
        public void TearUp()
        {
            service = new SlotsInfoService();
        }

        bool allPassed;
        [TestMethod]
        public void TestMachineJackpots()
        {
            allPassed = true;
            GetJackpots("BORD");
            GetJackpots("CARO");
            GetJackpots("CCTY");
            GetJackpots("FLAM");
            GetJackpots("GOLD");
            //GetJackpots("FAKE");
            GetJackpots("GRAN");
            GetJackpots("MERO");
            GetJackpots("MORU");
            GetJackpots("SIBA");
            GetJackpots("WC01");
            GetJackpots("WIND");
            if (allPassed == false)
                Assert.Fail();
        }
        private void GetJackpots(string casino)
        {
            GetSlotMachineJackpotResponse response = service.GetMachineJackpots(new GetSlotMachineJackpotRequest() { Casino = casino, SlotMachine = "10101", NumberOfLines = 10 });
            if (response.Success)
                Log(casino + " worked." + (response.Jackpots.Count > 0 ? (response.Jackpots[0].JPotType + " " + response.Jackpots[0].HitDate + " " + response.Jackpots[0].Amount) : ""));
            else
            {
                Log(casino + " failed.\nError: " + response.Errors[0].ErrorMessage);
                allPassed = false;
            }
        }
    }
}
