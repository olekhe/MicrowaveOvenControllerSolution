using MicrovaweOvenControllerSolution.Core.Abstracts;

namespace MicrowaveOvenControllerSolution.Core.Models
{
    /// <summary>
    /// Oven controller: implementation by Oleksandr Herhelezhyu (olek.he@outtlook.com)
    /// </summary>
    public class OvenController
    {
        private const short DEFAULT_TIME_ADDED_MINUTES = 1;

        /// <summary>
        /// Hardware interface
        /// </summary>
        private readonly IMicrowaveOvenHW ovenHW;

        /// <summary>
        /// Clock interface
        /// </summary>
        private readonly IClockHW clockHW;

        /// <summary>
        /// Default ctor of the oven controller
        /// </summary>
        /// <param name="ovenHW">Hardware injected interface</param>
        public OvenController(IMicrowaveOvenHW ovenHW, IClockHW clockHW)
        {
            this.ovenHW = ovenHW;
            this.clockHW = clockHW;

            this.ovenHW.DoorOpenChanged += (bool isOpen) => IsDoorOpen = isOpen;
            this.ovenHW.StartButtonPressed += new EventHandler((_, _) => Start());

            this.clockHW.TimeIsElapsed += new EventHandler((_, _) => TurnHeaterOff());
        }

        /// <summary>
        /// Time remaining
        /// </summary>
        private TimeSpan TimeRemaining
        {
            get => this.clockHW.Clock;
            set
            {
                if (value == TimeSpan.Zero)
                {
                    this.clockHW.Stop();

                    return;
                }

                this.clockHW.Schedule(value);
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
            TimeRemaining = TimeRemaining.Add(TimeSpan.FromMinutes(DEFAULT_TIME_ADDED_MINUTES));

            this.ovenHW.TurnOnHeater();
        }

        /// <summary>
        /// Turn heater off on the controller and hardware
        /// </summary>
        private void TurnHeaterOff()
        {
            TimeRemaining = TimeSpan.Zero;

            this.ovenHW.TurnOffHeater();
        }
    }
}
