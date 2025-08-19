using MicrowaveOvenControllerSolution.Core.Models;
using MicrowaveOvenControllerSolution.UnitTests.Fakes;

namespace MicrowaveOvenControllerSolution
{
    public class Tests
    {
        private FakeMicrowaveOvenHW microwaveOvenHwFake;

        [SetUp]
        public void Setup()
        {
            microwaveOvenHwFake = new FakeMicrowaveOvenHW();
        }

        [Test]
        public void LightsIsOn_When_DoorOpen()
        {
            var ovenController = new OvenController(microwaveOvenHwFake);

            microwaveOvenHwFake.OpenDoor();

            Assert.IsTrue(ovenController.IsLightOn);
        }

        [Test]
        public void LightsIsOff_When_OpenDoor_CloseDoor()
        {
            var ovenController = new OvenController(microwaveOvenHwFake);

            microwaveOvenHwFake.OpenDoor();

            Assert.IsTrue(ovenController.IsLightOn);

            microwaveOvenHwFake.CloseDoor();

            Assert.IsFalse(ovenController.IsLightOn);
        }

        [Test]
        public void Heater_Stops_When_OpenDoor()
        {
            var ovenController = new OvenController(microwaveOvenHwFake);

            microwaveOvenHwFake.Start();

            Assert.IsTrue(ovenController.IsHeaterOn);

            microwaveOvenHwFake.OpenDoor();

            Assert.IsFalse(ovenController.IsHeaterOn);
        }

        [Test]
        public void Nothing_Happens_When_Start_With_OpenDoor()
        {
            var ovenController = new OvenController(microwaveOvenHwFake);

            microwaveOvenHwFake.OpenDoor();

            microwaveOvenHwFake.Start();

            Assert.IsFalse(ovenController.IsHeaterOn);
        }


        [Test]
        public void Heater_Runs_For_1_Minute_OnStart()
        {
            var ovenController = new OvenController(microwaveOvenHwFake);

            microwaveOvenHwFake.Start();

            Assert.IsTrue(ovenController.IsHeaterOn);

            Assert.True(ovenController.TimeRemaining == TimeSpan.FromMinutes(1));
        }

        [Test]
        public void Heater_Runs_For_2_Minute_On_Twice_Start()
        {
            var ovenController = new OvenController(microwaveOvenHwFake);

            microwaveOvenHwFake.Start();

            Assert.IsTrue(ovenController.IsHeaterOn);

            Assert.True(ovenController.TimeRemaining == TimeSpan.FromMinutes(1));

            microwaveOvenHwFake.Start();

            var expectedTimeSpan = TimeSpan.FromMinutes(2);

            Assert.True(ovenController.TimeRemaining == expectedTimeSpan);
        }

        [Test]
        public void Heater_Runs_For_55_Secs_On_Start()
        {
            var waitTime = 0.05D;

            var ovenController = new OvenController(microwaveOvenHwFake)
            {
                Options = new OvenControllerOptions
                {
                    DefaultTimeMinutes = waitTime
                }
            };

            microwaveOvenHwFake.Start();

            Assert.IsTrue(ovenController.IsHeaterOn);

            Assert.True(ovenController.TimeRemaining == TimeSpan.FromMinutes(waitTime));

            Thread.Sleep(TimeSpan.FromMinutes(waitTime + waitTime));

            Assert.False(ovenController.IsHeaterOn);
        }
    }
}