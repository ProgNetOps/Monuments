using Monuments.API.Models;

namespace Monuments.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        //This static instance is now replaced by the singleton instance registered in the DI container
        //public static CitiesDataStore Instance { get; } = new CitiesDataStore();

        public CitiesDataStore() {
            Cities = new List<CityDto> {
                new CityDto
                {
                    Id = 1,
                    Name = "Abia",
                    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",
                    Monuments = [
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
                            }
                        ]
                },
                new CityDto
                {
                    Id = 2,
                    Name = "Bauchi",
                    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",
                    Monuments = [
                         new MonumentsDto
                            {
                                Id = 1,
                                Name = "First Storey Building",
                                Description ="Nigeria's first-storey building built in 1845."
                            },
                            new MonumentsDto
                            {
                                Id = 2,
                                Name = "Sukur Cultural Landscape",
                                Description ="A UNESCO World Heritage site featuring the Palace of the Hidi."
                            }
                        ]
                },
                new CityDto
                {
                    Id = 3,
                    Name = "Lagos",
                    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",
                    Monuments = [
                        new MonumentsDto
                            {
                                Id = 1,
                                Name = "Ancient Kano City Walls & Gidan Makama",
                                Description ="Historic walls and the palace of the Emir."
                            },
                            new MonumentsDto
                            {
                                Id = 2,
                                Name = "National War Museum",
                                Description ="Exhibits relics from the Civil War and Nigerian military history."
                            }
                        ]
                },
                new CityDto
                {
                    Id = 4,
                    Name = "Osun",
                    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",
                    Monuments = [
                        new MonumentsDto
                            {
                                Id = 1,
                                Name = "Gobirau Minaret",
                                Description ="An ancient structure dating back to the 14th century."
                            },
                            new MonumentsDto
                            {
                                Id = 2,
                                Name = "Mapo Hall",
                                Description ="A colonial-era city hall located on a hill."
                            }
                        ]
                },
                new CityDto
                {
                    Id = 5,
                    Name = "Ogun",
                    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",
                    Monuments = [
                        new MonumentsDto
                            {
                                Id=1,
                                Name=  "Osun-Osogbo Grove",
                                Description ="The Osun-Osogbo grove is a sacred forest boasting of shrines, sculptures and art works in the honor of the goddess of fertility Osun"

                            },
                            new MonumentsDto
                            {
                                Id=2,
                                Name=  "Tunga Dutse Rock Paintings",
                                Description ="Tunga Dutse is further proof of human settlement in the Bauchi area. Legible writings (whose meanings are unknown) cover an area on the sandstone rock embarkment of about 4m in length in Dwall River. The writings are legible."
                            }
                        ]
                }
                //new CityDto
                //{
                //    Id = 6,
                //    Name = "Enugu",
                //    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",

                //},
                //new CityDto
                //{
                //    Id = 7,
                //    Name = "Oyo",
                //    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",

                //},
                //new CityDto
                //{
                //    Id = 8,
                //    Name = "Kogi",
                //    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",

                //},
                //new CityDto
                //{
                //    Id = 9,
                //    Name = "Rivers",
                //    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",

                //},
                //new CityDto
                //{
                //    Id = 10,
                //    Name = "Ebonyi",
                //    Slogan = "A UNESCO World Heritage site featuring the Palace of the Hidi.",

                //}
            };
        
        
        }
    }
}
