using Monuments.API.Entities;

namespace Monuments.API.Services
{
    public interface IMonumentRepository
    {
        Task<IEnumerable<City>?> GetCitiesAsync();
        Task<(IEnumerable<City>?, PaginationMetadata)> GetCitiesAsync(string? name, 
            string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool includeMonuments=false);
        Task<bool> CityExistsAsync(int cityId);
        Task<IEnumerable<Monument>?> GetMonumentsForCityAsync(int cityId);
        Task<Monument?> GetMonumentForCityAsync(int cityId, int monumentId);
        Task AddMonumentToCityAsync(int cityId, Monument monument);
        void DeleteMonument(Monument monument);
        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);
        Task<bool> SaveChangesAsync();
    }
}
