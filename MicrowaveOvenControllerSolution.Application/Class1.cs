using MicrovaweOvenControllerSolution.Core.Abstracts;
using MicrowaveOvenControllerSolution.Core.Models;

namespace MicrowaveOvenControllerSolution.Application
{
    public class MicrowaveOvenHardwareController
    {
        private readonly IMicrowaveOvenHW ovenHW;
        private readonly OvenController ovenController;

        public MicrowaveOvenHardwareController(IMicrowaveOvenHW ovenHW, OvenController ovenController)
        {
            this.ovenHW = ovenHW;
            this.ovenController = ovenController;
        }

        void Connect()
        {
            ovenHW.StartButtonPressed += On_StartButtonPressed;
            ovenHW.StartButtonPressed
        }

        public bool IsActive { get; private set; }

        public bool IsLightOn { get; private set; }

        public bool IsDoorOpen { get; private set; }

        public bool IsHeaterOn { get; private set; }

        public TimeSpan TimeRemaining { get; private set; }

        public EventHandler On_StartButtonPressed = new EventHandler((sender, e) =>
        {

        });

        public void Start()
        {
            IsActive = true;

            ovenHW.StartButtonPressed += (sender, args) => { };
        }

        public void Open()
        {
            IsLightOn = true;

            ovenHW.DoorOpenChanged += (bool isOpen) => IsDoorOpen = isOpen;
        }

        public void Close()
        {
            IsLightOn = false;
        }
    }
}
