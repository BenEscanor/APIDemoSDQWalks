using System.ComponentModel.DataAnnotations;

namespace SDQWalksAPI.Models.Dtos
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MinLength(400)]
        public double LenghInKm { get; set; }
        public string WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
