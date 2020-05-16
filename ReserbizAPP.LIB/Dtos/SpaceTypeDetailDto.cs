namespace ReserbizAPP.LIB.Dtos
{
    public class SpaceTypeDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rate { get; set; }
        public int AvailableSlot { get; set; }
        public int CurrentUsedSlot { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeletable { get; set; }
    }
}