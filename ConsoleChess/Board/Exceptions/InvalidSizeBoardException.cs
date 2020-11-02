using System;

namespace Board.Exceptions
{
    /// <summary>
    /// The class of the InvalidSizeBoardException.
    /// </summary>
    /// <remarks>The InvalidSizeBoardException is thrown when trying to create a null or too large board.</remarks>
    class InvalidSizeBoardException : ApplicationException
    {
        /// <summary>
        /// The main constructor for a InvalidSizeBoardException.
        /// </summary>
        /// <param name="message">Message explaining the exception.</param>
        public InvalidSizeBoardException(string message)
            : base(message) { }
    }
}
