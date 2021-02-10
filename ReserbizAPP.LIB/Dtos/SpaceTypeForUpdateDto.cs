using System.ComponentModel.DataAnnotations;

namespace ReserbizAPP.LIB.Dtos
{
    public class SpaceTypeForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public float Rate { get; set; }
    }
}