using MicrovaweOvenControllerSolution.Core.Abstracts;

namespace MicrowaveOvenControllerSolution.UnitTests.Fakes
{
    internal class FakeMicrowaveOvenHW : IMicrowaveOvenHW
    {
        public bool DoorOpen { get; set; }

        public event Action<bool> DoorOpenChanged;

        public event EventHandler StartButtonPressed;

        public void TurnOffHeater()
        {
            // nothing to do
        }

        public void TurnOnHeater()
        {
            // nothing to do
        }

        public void Start()
        {
            StartButtonPressed.Invoke(this, EventArgs.Empty);
        }

        public void OpenDoor()
        {
            DoorOpenChanged?.Invoke(true);
            DoorOpen = true;
        }

        public void CloseDoor()
        {
            DoorOpenChanged?.Invoke(false);
            DoorOpen = false;
        }
    }
}
