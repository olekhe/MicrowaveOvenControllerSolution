using MicrowaveOvenControllerSolution.Core.Abstracts;

namespace MicrowaveOvenControllerSolution.UnitTests.Fakes
{
    /// <summary>
    /// Fake for microwave hw
    /// </summary>
    internal class FakeMicrowaveOvenHW : IMicrowaveOvenHW
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool DoorOpen { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event Action<bool>? DoorOpenChanged;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event EventHandler? StartButtonPressed;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void TurnOffHeater()
        {
            // nothing to do
        }

        public void TurnOnHeater()
        {
            // nothing to do
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Start()
        {
            StartButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void OpenDoor()
        {
            DoorOpenChanged?.Invoke(true);
            DoorOpen = true;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CloseDoor()
        {
            DoorOpenChanged?.Invoke(false);
            DoorOpen = false;
        }
    }
}
