using Microsoft.EntityFrameworkCore;
using Monuments.API.DbContexts;
using Monuments.API.Entities;

namespace Monuments.API.Services
{
    public class MonumentRepository : IMonumentRepository
    {
        private readonly MonumentsDbContext _context;
        private readonly IQueryable<City> _allCities;
        private readonly IQueryable<Monument> _allMonuments;

        public MonumentRepository(MonumentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _allMonuments = _context.Monuments;
            _allCities = _context.Cities;
        }

      

        //Check if city exists
        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _allCities
                .AsNoTracking()
                .AnyAsync(c => c.Id == cityId);
        }

        public async Task<IEnumerable<City>?> GetCitiesAsync()
        {
            return await _allCities
                .OrderBy(n => n.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(IEnumerable<City>?,PaginationMetadata)> GetCitiesAsync(string? name, 
            string? searchQuery, int pageNumber, int pageSize)
        {
            //Collection to start with
            //var collection = _context.Cities as IQueryable<City>;
            var collection = _allCities;

            //FILTERING
            if (string.IsNullOrWhiteSpace(name) is false)
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            //SEARCHING
            if (string.IsNullOrWhiteSpace(searchQuery) is false)
            {
                searchQuery = searchQuery.Trim();
                collection = collection
                    .Where(c => c.Name.Contains(searchQuery)
                || c.Slogan != null && c.Slogan.Contains(searchQuery));
            }

            var totalItemCount = await collection.AsNoTracking().CountAsync();

            PaginationMetadata? paginationMetadata = new PaginationMetadata(totalItemCount,pageSize,pageNumber);

            var collectionToReturn = await collection
                .OrderBy(c=>c.Name)
                .Skip(pageSize*(pageNumber-1))
                .Take(pageSize)//PAGINATION
                .AsNoTracking()
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);

        }

        public async Task<City?> GetCityAsync(int cityId,
            bool includeMonuments)
        {
            return includeMonuments is false

                ? await _allCities
                .AsNoTracking()
                .FirstOrDefaultAsync(city => city.Id == cityId)

                : await _allCities
                .Include(m => m.Monuments)
                .AsNoTracking()
                .FirstOrDefaultAsync(city => city.Id == cityId);
        }

        public async Task<Monument?> GetMonumentForCityAsync(int cityId, int monumentId)
        {
            return await _allMonuments
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CityId == cityId && m.Id==monumentId);

        }

        public async Task<IEnumerable<Monument>?> GetMonumentsForCityAsync(int cityId)
        {
            return await _allMonuments
                .Where(m => m.CityId == cityId)
                .OrderBy(x=>x.Name)
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task AddMonumentToCityAsync(int cityId, Monument monument)
        {
            var city = await GetCityAsync(cityId,false);

            if (city is not null)
            {
                //Adds it on the in-memory representation of the object
                //not the database, so, call needs not be async
                city.Monuments.Add(monument);
            }
        }

        public void DeleteMonument(Monument monument)
        {
            //Remove() method is not available for the IQueryable property
            _context.Monuments.Remove(monument);
        }

        public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return await _allCities.AnyAsync(c => c.Id == cityId
            && c.Name == cityName);
        }
    }
}
