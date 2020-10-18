namespace ReserbizAPP.LIB.Dtos
{
    public class SpaceOptionDto : EntityOptionDto
    {
        public int SpaceTypeId { get; set; }
        public bool IsNotOccupied { get; set; }
        public int OccupiedByContractId { get; set; }
    }
}