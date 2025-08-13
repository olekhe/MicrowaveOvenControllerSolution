using MicrowaveOvenControllerSolution.Core.Models;
using MicrowaveOvenControllerSolution.UnitTests.Fakes;

namespace MicrowaveOvenControllerSolution
{
    public class Tests
    {
        private FakeMicrowaveOvenHW microwaveOvenHwFake;
        private FakeClockHW clockHwFake;
        private OvenController ovenController;

        [SetUp]
        public void Setup()
        {
            microwaveOvenHwFake = new FakeMicrowaveOvenHW();
        }

        [Test]
        public void LightsIsOn_When_DoorOpen()
        {
            clockHwFake = new FakeClockHW();

            ovenController = new OvenController(microwaveOvenHwFake, clockHwFake);

            microwaveOvenHwFake.OpenDoor();

            Assert.IsTrue(ovenController.IsLightOn);
        }

        [Test]
        public void LightsIsOff_When_OpenDoor_CloseDoor()
        {
            clockHwFake = new FakeClockHW();

            ovenController = new OvenController(microwaveOvenHwFake, clockHwFake);

            microwaveOvenHwFake.OpenDoor();

            Assert.IsTrue(ovenController.IsLightOn);

            microwaveOvenHwFake.CloseDoor();

            Assert.IsFalse(ovenController.IsLightOn);
        }

        [Test]
        public void Heater_Stops_When_OpenDoor()
        {
            clockHwFake = new FakeClockHW();

            ovenController = new OvenController(microwaveOvenHwFake, clockHwFake);

            microwaveOvenHwFake.Start();

            Assert.IsTrue(ovenController.IsHeaterOn);

            microwaveOvenHwFake.OpenDoor();

            Assert.IsFalse(ovenController.IsHeaterOn);
        }

        [Test]
        public void Nothing_Happens_When_Start_With_OpenDoor()
        {
            clockHwFake = new FakeClockHW();

            ovenController = new OvenController(microwaveOvenHwFake, clockHwFake);

            microwaveOvenHwFake.OpenDoor();

            microwaveOvenHwFake.Start();

            Assert.IsFalse(ovenController.IsHeaterOn);
        }


        [Test]
        public void Heater_Runs_For_1_Minute_OnStart()
        {
            clockHwFake = new FakeClockHW();

            ovenController = new OvenController(microwaveOvenHwFake, clockHwFake);

            microwaveOvenHwFake.Start();

            Assert.IsTrue(ovenController.IsHeaterOn);

            Assert.True(clockHwFake.Clock == TimeSpan.FromMinutes(1));
        }

        [Test]
        public void Heater_Runs_For_2_Minute_On_Twice_Start()
        {
            clockHwFake = new FakeClockHW();

            ovenController = new OvenController(microwaveOvenHwFake, clockHwFake);

            microwaveOvenHwFake.Start();

            Assert.IsTrue(ovenController.IsHeaterOn);

            Assert.True(clockHwFake.Clock == TimeSpan.FromMinutes(1));

            microwaveOvenHwFake.Start();

            microwaveOvenHwFake.Start();

            var expectedTimeSpan = TimeSpan.FromMinutes(3);

            Assert.True(clockHwFake.Clock == expectedTimeSpan);
        }
    }
}