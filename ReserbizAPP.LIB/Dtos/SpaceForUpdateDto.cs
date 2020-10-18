using System.ComponentModel.DataAnnotations;

namespace ReserbizAPP.LIB.Dtos
{
    public class SpaceForUpdateDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int SpaceTypeId { get; set; }
    }
}