namespace Coelsa.Models
{
    public class Response<T>
    {
        public string Code { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

    }
}
