using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Dtos
{
    public class EntityOptionDto : IEntityOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public bool CanBeSelected
        {
            get
            {
                return IsDelete == false && IsActive == true;
            }
        }
    }
}