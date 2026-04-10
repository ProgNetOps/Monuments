using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monuments.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Slogan { get; set; }
        public ICollection<Monument> Monuments { get; set; } = [];
    }
}
