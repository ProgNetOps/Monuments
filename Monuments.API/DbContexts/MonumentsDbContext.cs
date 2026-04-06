using Microsoft.EntityFrameworkCore;
using Monuments.API.Entities;
using Monuments.API.Models;

namespace Monuments.API.DbContexts
{
    public class MonumentsDbContext:DbContext
    {
        public MonumentsDbContext(DbContextOptions<MonumentsDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Monument> Monuments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<City> cities = [
                new City
                {
                    Id = 1,
                    Name = "Abia",
                    Slogan = $"God's Own State",
                   
                },
                new City
                {
                    Id = 2,
                    Name = "Bauchi",
                    Slogan = $"Pearl of Tourism",
                    
                },
                new City
                {
                    Id = 3,
                    Name = "Lagos",
                    Slogan = $"Center of Excellence",
                    
                }];

            List<Monument> monuments = [
            new Monument
            {
                Id = 1,
                Name = "Sukur Cultural Landscape",
                CityId = 1,
                Description ="A UNESCO World Heritage site featuring the Palace of the Hidi."
            },
            new Monument
            {
                Id = 2,
                Name = "Benin City Walls and Moat",
                CityId= 1,
                Description ="Ancient earthworks and one of the world's largest man-made earthworks."
            },
            new Monument
            {
                Id = 3,
                Name = "First Storey Building",
                CityId = 2,
                Description ="Nigeria's first-storey building built in 1845."
            },
            new Monument
            {
                Id = 4,
                Name = "Sukur Cultural Landscape",
                CityId=2,
                Description ="A UNESCO World Heritage site featuring the Palace of the Hidi."
            },
            new Monument
            {
                Id = 5,
                Name = "Ancient Kano City Walls & Gidan Makama",
                CityId=3,
                Description ="Historic walls and the palace of the Emir."
            },
            new Monument
            {
                Id = 6,
                Name = "National War Museum",
                CityId=3,
                Description ="Exhibits relics from the Civil War and Nigerian military history."
            }];

            modelBuilder.Entity<City>().HasData(cities);
            modelBuilder.Entity<Monument>().HasData(monuments);


            base.OnModelCreating(modelBuilder);
        }
    }
}
