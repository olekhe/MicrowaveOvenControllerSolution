using MicrovaweOvenControllerSolution.Core.Abstracts;

namespace MicrowaveOvenControllerSolution.Core.Models
{

    /// <summary>
    /// Oven controller: implementation by Oleksandr Herhelezhyu (olek.he@outlook.com)
    /// </summary>
    public class OvenController
    {
        /// <summary>
        /// Hardware interface
        /// </summary>
        private readonly IMicrowaveOvenHW ovenHW;
        private readonly OvenClock ovenControllerClock;

        /// <summary>
        /// Controller options
        /// </summary>
        public OvenControllerOptions Options { get; set; }

        /// <summary>
        /// Default ctor of the oven controller
        /// </summary>
        /// <param name="ovenHW">Hardware injected interface</param>
        public OvenController(IMicrowaveOvenHW ovenHW)
        {
            this.Options = OvenControllerOptions.Default;

            this.ovenControllerClock = new OvenClock();

            this.ovenHW = ovenHW;

            this.ovenHW.DoorOpenChanged += (bool isOpen) => IsDoorOpen = isOpen;
            this.ovenHW.StartButtonPressed += new EventHandler((_, _) => Start());

            this.ovenControllerClock.TimeIsElapsed += new EventHandler((_, _) => TurnHeaterOff());
        }

        /// <summary>
        /// Time remaining
        /// </summary>
        public TimeSpan? TimeRemaining
        {
            get => this.ovenControllerClock.Clock;
            set
            {
                if (!value.HasValue)
                {
                    this.ovenControllerClock.Stop();

                    return;
                }

                this.ovenControllerClock.Schedule(value);
            }
        }

        /// <summary>
        /// Indicates whether the light is on or off
        /// </summary>
        public bool IsLightOn { get; private set; }

        /// <summary>
        /// Indicates whether the Oven is turned on
        /// </summary>
        public bool IsHeaterOn => TimeRemaining > TimeSpan.Zero;

        /// <summary>
        /// Indicates whether the door is open or closed
        /// </summary>
        private bool IsDoorOpen
        {
            get { return this.ovenHW.DoorOpen; }
            set
            {
                IsLightOn = value;

                if (value && IsHeaterOn)
                {
                    TurnHeaterOff();
                }
            }
        }

        /// <summary>
        /// Starts
        /// </summary>
        private void Start()
        {
            if (IsDoorOpen)
            {
                return;
            }

            TurnHeaterOn();
        }

        /// <summary>
        /// Turn heater on on the controller and hardware
        /// </summary>
        private void TurnHeaterOn()
        {
            TimeRemaining = (TimeRemaining ?? TimeSpan.Zero).Add(TimeSpan.FromMinutes(this.Options.DefaultTimeMinutes));

            this.ovenHW.TurnOnHeater();
        }

        /// <summary>
        /// Turn heater off on the controller and hardware
        /// </summary>
        private void TurnHeaterOff()
        {
            TimeRemaining = null;

            this.ovenHW.TurnOffHeater();
        }
    }
}
