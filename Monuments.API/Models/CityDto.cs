namespace Monuments.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Slogan { get; set; }
        public int NumberOfMonuments { get { return Monuments.Count; } }
        public ICollection<MonumentsDto>? Monuments { get; set; } = new List<MonumentsDto>();
    }
}
