namespace ReserbizAPP.LIB.Models
{
    public class ErrorLog : Entity
    {
        public string Message { get; set; }
        public string Stacktrace { get; set; }
        public string Source { get; set; }
        public int UserId { get; set; }
        public Account User { get; set; }
    }
}