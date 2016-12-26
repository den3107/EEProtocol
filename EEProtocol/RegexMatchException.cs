using System;

namespace EEProtocol
{
    /// <summary>Exception that indicates something went wrong while searching in the protocol.</summary>
    class RegexMatchException : Exception
    {
        public RegexMatchException()
        {
        }

        public RegexMatchException(string message) : base(message)
        {
        }

        public RegexMatchException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
