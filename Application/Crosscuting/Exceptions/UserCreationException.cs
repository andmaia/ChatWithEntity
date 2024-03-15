namespace Application.Crosscuting.Exceptions
{
    public class UserCreationException : Exception
    {
        public UserCreationException(string? message, Exception ex) : base(message)
        {
        }
    }
}
