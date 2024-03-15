namespace Application.Crosscuting.Helpers
{
    public class ServiceResult<T>
    {
        public ServiceResult()
        {
        }

        public ServiceResult(T data, string messageError, bool success)
        {
            Data = data;
            MessageError = messageError;
            Success = success;
        }

        public T Data { get; set; }
        public string MessageError { get; set; }
        public bool Success { get; set; }
    }
}
