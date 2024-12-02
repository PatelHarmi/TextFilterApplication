namespace TextFilterApplication.Exceptions
{
    /// <summary>
    /// Exception for Text Filter.
    /// </summary>
    public class TextFilterException : Exception
    {
        public TextFilterException(string message) : base(message) { }
        public TextFilterException(string message, Exception inner) : base(message, inner) { }
    }

}
