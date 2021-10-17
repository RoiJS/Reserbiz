namespace ReserbizAPP.LIB.Models
{
    public class ErrorLog : Entity
    {
        public string Message { get; set; }
        public string Stacktrace { get; set; }
        public string Source { get; set; }
        public string UserInfo { get; set; }
    }
}