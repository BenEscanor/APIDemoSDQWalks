using System.ComponentModel.DataAnnotations;

namespace SDQWalksAPI.Models.Dtos
{
    public class AddregionRequestDto
    {
        [Required]
        [MaxLength (150, ErrorMessage ="The name has to be under 150 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage ="The code has to be mininum 3 characters")]
        [MaxLength(3, ErrorMessage ="The code has to be maximun 3 characters")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
