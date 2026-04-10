namespace Monuments.API.Models
{
    /// <summary>
    /// DTO for a city without monuments
    /// </summary>
    public class CityWithoutMonumentsDto
    {
        /// <summary>
        /// Id of the city
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the state
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Slogan of the state
        /// </summary>
        public string? Slogan { get; set; }
    }
}
