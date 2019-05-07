namespace learning_together_api.Exceptions
{
    using System;
    using System.Globalization;

    public class AppException : Exception
    {
        protected AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}