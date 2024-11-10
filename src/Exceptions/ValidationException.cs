using System;
namespace MediatrSample.Api.Exceptions
{
    /// <summary>
    /// Exception with user friendly message for handling client validation errors
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// </summary>
        /// <param name="message">User friendly message to return to client</param>
        public ValidationException(string message) : base(message)
        {

        }
    }
}
