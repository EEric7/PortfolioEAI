namespace PortfolioEAI.Web.Domain.Exceptions
{
    internal class BusinessRuleViolationException : Exception
    {
        /// <summary>
        /// Represents an exception that is thrown when a business rule is violated.
        /// This exception is used to indicate that a specific business rule has not been satisfied,
        /// which may prevent the successful execution of an operation or process.
        /// It is typically used in domain-driven design to enforce business logic constraints.
        /// </summary>
        /// <param name="message"></param>
        public BusinessRuleViolationException(string message) : base(message) { }
        
        /// <summary>
        /// Represents an exception that is thrown when a business rule is violated.
        /// This exception is used to indicate that a specific business rule has not been satisfied,
        /// which may prevent the successful execution of an operation or process.
        /// It is typically used in domain-driven design to enforce business logic constraints.
        /// This constructor allows for an inner exception to be specified, providing additional context about the error.
        /// The inner exception can be used to capture the original exception that caused the business rule violation,
        /// allowing for better debugging and error handling.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BusinessRuleViolationException(string message, Exception innerException) : base(message, innerException) { }
    }
}