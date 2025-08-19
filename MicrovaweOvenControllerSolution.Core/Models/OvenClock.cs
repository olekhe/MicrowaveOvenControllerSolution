namespace MicrowaveOvenControllerSolution.Core.Models
{
    /// <summary>
    /// Countdown clock implementation
    /// </summary>
    public class OvenClock
    {
        /// <summary>
        /// Fake implementation of the clock
        /// </summary>

        private Timer? timer;
        private AutoResetEvent autoEvent = new AutoResetEvent(false);

        /// <summary>
        /// Clock itelf
        /// </summary>
        public TimeSpan? Clock { get; private set; }

        /// <summary>
        /// Event
        /// </summary>
        public event EventHandler? TimeIsElapsed;

        /// <summary>
        /// Fires every second
        /// </summary>
        /// <param name="_"></param>
        private void OnTimedEvent(object _)
        {
            Clock = Clock?.Add(TimeSpan.FromSeconds(-1));

            if (Clock == TimeSpan.Zero || Clock < TimeSpan.Zero)
            {
                timer!.Change(Timeout.Infinite, Timeout.Infinite);

                TimeIsElapsed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Schedules a timer
        /// </summary>
        /// <param name="timeSpan"></param>
        public void Schedule(TimeSpan? timeSpan)
        {
            if (!Clock.HasValue)
            {
                timer = new Timer(OnTimedEvent!, autoEvent, 1000, 1000);
            }

            Clock = timeSpan;
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            timer?.Dispose();

            Clock = null;
        }
    }
}
