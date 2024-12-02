namespace TextFilterApplication.Exceptions
{
    public class TextFilterException : Exception
    {
        public TextFilterException(string message) : base(message) { }
        public TextFilterException(string message, Exception inner) : base(message, inner) { }
    }

}
