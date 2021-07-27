namespace ReserbizAPP.LIB.Models
{
    public class TermVersion
        : Entity
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        public int TermId { get; set; }
        public Term Term { get; set; }

        public TermVersion()
        {

        }
    }
}