namespace ReserbizAPP.LIB.Dtos
{
    public class SpaceTypeOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Rate { get; set; }
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