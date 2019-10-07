namespace ReserbizAPP.LIB.Models
{
    public class Miscellaneous : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }


        #region "Reference Properties"

        public Term Term { get; set; }
        public int TermId { get; set; }

        #endregion
    }
}