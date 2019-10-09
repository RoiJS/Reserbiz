namespace ReserbizAPP.LIB.Models
{
    public class AccountStatementMiscellaneous : Miscellaneous
    {
        public int AccountStatementId { get; set; }
        public AccountStatement AccountStatement { get; set; }
    }
}