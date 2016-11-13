using System;

namespace EEProtocol
{
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
