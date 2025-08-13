using MicrovaweOvenControllerSolution.Core.Abstracts;

namespace MicrowaveOvenControllerSolution.UnitTests.Fakes
{
    internal class FakeClockHW : IClockHW
    {
        /// <summary>
        /// Clock itelf
        /// </summary>
        public TimeSpan Clock { get; set; }

        /// <summary>
        /// Event
        /// </summary>
        public event EventHandler TimeIsElapsed;

        private Timer timer;
        private AutoResetEvent autoEvent = new AutoResetEvent(false);

        /// <summary>
        /// Fires every second
        /// </summary>
        /// <param name="_"></param>
        private void OnTimedEvent(object _)
        {
            var newClock = Clock.Add(TimeSpan.FromSeconds(-1));

            if (newClock == TimeSpan.Zero ||
               newClock < TimeSpan.Zero)
            {
                TimeIsElapsed?.Invoke(this, EventArgs.Empty);

                Clock = TimeSpan.Zero;

                return;
            }

            Clock = newClock;
        }

        public void Schedule(TimeSpan timeSpan)
        {
            Clock = timeSpan;

            timer = new Timer(OnTimedEvent!, autoEvent, 1000, 1000);
        }

        public void Stop()
        {
            timer.Dispose();

            Clock = TimeSpan.Zero;

            TimeIsElapsed?.Invoke(this, EventArgs.Empty);
        }
    }
}
