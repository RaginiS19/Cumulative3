namespace Cumulative3.Models
{
    /// <summary>
    /// Represents the error view model used to display error details.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the unique request identifier.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Returns true if the RequestId is not null or empty.
        /// Useful for conditionally displaying the RequestId in the view.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
