namespace MicrowaveOvenControllerSolution.Core.Models
{
    /// <summary>
    /// Options for the controller
    /// </summary>
    public class OvenControllerOptions
    {
        /// <summary>
        /// Default time
        /// </summary>
        public double AddedTimeMinutes { get; set; }

        /// <summary>
        /// Added time minutes
        /// </summary>
        public static OvenControllerOptions Default => new OvenControllerOptions
        {
            AddedTimeMinutes = 1D
        };
    }
}
