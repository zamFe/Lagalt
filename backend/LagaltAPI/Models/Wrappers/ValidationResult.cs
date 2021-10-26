namespace LagaltAPI.Models.Wrappers
{
    /// <summary> Simple data structure used to convey validation results and reasoning. </summary>
    public class ValidationResult
    {
        /// <summary> Whether validation succeeded. </summary>
        public bool Result { get; set; }

        /// <summary> Reason for why validation failed. </summary>
        public string RejectionReason { get; set; }

        // Constructor.
        public ValidationResult(bool result, string rejectionReason = "")
        {
            Result = result;
            RejectionReason = rejectionReason;
        }
    }
}
