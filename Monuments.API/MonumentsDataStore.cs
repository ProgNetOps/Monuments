using Monuments.API.Models;

namespace Monuments.API;

public class MonumentsDataStore
{
    public List<MonumentsDto> Monuments { get; set; }

    public static MonumentsDataStore Instance { get;} = new MonumentsDataStore();

    public MonumentsDataStore()
    {
        //Initialize dummy data
        Monuments = new List<MonumentsDto>
        {
            new MonumentsDto
            {
                Id = 1,
                Name = "Sukur Cultural Landscape",
                Description ="A UNESCO World Heritage site featuring the Palace of the Hidi."
            },
            new MonumentsDto
            {
                Id = 2,
                Name = "Benin City Walls and Moat",
                Description ="Ancient earthworks and one of the world's largest man-made earthworks."
            },
            new MonumentsDto
            {
                Id = 3,
                Name = "First Storey Building",
                Description ="Nigeria's first-storey building built in 1845."
            },
            new MonumentsDto
            {
                Id = 4,
                Name = "Sukur Cultural Landscape",
                Description ="A UNESCO World Heritage site featuring the Palace of the Hidi."
            },
            new MonumentsDto
            {
                Id = 5,
                Name = "Ancient Kano City Walls & Gidan Makama",
                Description ="Historic walls and the palace of the Emir."
            },
            new MonumentsDto
            {
                Id = 6,
                Name = "National War Museum",
                Description ="Exhibits relics from the Civil War and Nigerian military history."
            },
            new MonumentsDto
            {
                Id = 7,
                Name = "Gobirau Minaret",
                Description ="An ancient structure dating back to the 14th century."
            },
            new MonumentsDto
            {
                Id = 8,
                Name = "Mapo Hall",
                Description ="A colonial-era city hall located on a hill."
            },
            new MonumentsDto
            {
                Id=9,
                Name=  "Osun-Osogbo Grove",
                Description ="The Osun-Osogbo grove is a sacred forest boasting of shrines, sculptures and art works in the honor of the goddess of fertility Osun"
                
            },
            new MonumentsDto
            {
                Id=10,
                Name=  "Tunga Dutse Rock Paintings",
                Description ="Tunga Dutse is further proof of human settlement in the Bauchi area. Legible writings (whose meanings are unknown) cover an area on the sandstone rock embarkment of about 4m in length in Dwall River. The writings are legible."
            },

        };

    }

}
