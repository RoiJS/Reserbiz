using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Dtos
{
    public class SpaceDetailDto : IEntityDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int SpaceTypeId { get; set; }
        public string SpaceTypeName { get; set; }
        public float SpaceTypeRate { get; set; }
        public bool IsNotOccupied { get; set; }
        public int OccupiedByContractId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeletable { get; set; }
    }
}