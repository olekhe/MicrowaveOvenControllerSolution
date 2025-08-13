namespace MicrovaweOvenControllerSolution.Core.Abstracts
{
    /// <summary>
    /// Gives a real-time clock possibilities to the controller
    /// </summary>
    public interface IClockHW
    {
        /// <summary>
        /// Schedule a clock fro a  given span (adds on top if already running)
        /// </summary>
        /// <param name="timeSpan"></param>
        void Schedule(TimeSpan timeSpan);

        /// <summary>
        /// Stops a clock
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets current clock timespan
        /// </summary>
        TimeSpan Clock { get; }

        /// <summary>
        /// Signals when times is over 
        /// </summary>
        event EventHandler TimeIsElapsed;
    }
}
