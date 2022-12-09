namespace Api.CrossCutting.Exceptions
{
    [Serializable]
    public class RecordIsNullException : Exception
    {
        public RecordIsNullException() { }

        
    public RecordIsNullException(string message)
        : base(message) { }

    public RecordIsNullException(string message, Exception inner)
        : base(message, inner) { }
    }
}