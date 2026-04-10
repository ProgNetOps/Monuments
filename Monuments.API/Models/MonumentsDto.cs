namespace Monuments.API.Models;

public class MonumentsDto
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
}
