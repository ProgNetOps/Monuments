using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monuments.API.Entities
{
    public class Monument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Description { get; set; }
        public City? City { get; set; }
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
    }
}
