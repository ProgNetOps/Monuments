using AutoMapper;

namespace Monuments.API.Profiles
{
    public class CityProfile:Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutMonumentsDto>()
                .ReverseMap();
            CreateMap<Entities.City, Models.CityDto>()
                .ReverseMap();
        }
    }
}
