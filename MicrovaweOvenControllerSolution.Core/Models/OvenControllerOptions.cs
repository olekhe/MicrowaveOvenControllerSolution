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
        public double DefaultTimeMinutes { get; set; }

        /// <summary>
        /// DEfault unstance
        /// </summary>
        public static OvenControllerOptions Default => new OvenControllerOptions
        {
            DefaultTimeMinutes = 1D
        };
    }
}
