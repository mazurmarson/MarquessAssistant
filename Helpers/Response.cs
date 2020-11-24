namespace MarqueesAssistant.API.Helpers
{
    public class Response<T>
    {
        public Response() {}

        public Response(T response)
        {

        }

        public T Data  { get; set;}
    }
}