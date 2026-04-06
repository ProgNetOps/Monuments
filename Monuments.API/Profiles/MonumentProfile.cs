using AutoMapper;

namespace Monuments.API.Profiles
{
    public class MonumentProfile:Profile
    {
        public MonumentProfile()
        {
            CreateMap<Entities.Monument, Models.MonumentsDto>()
                .ReverseMap();
            CreateMap<Entities.Monument, Models.MonumentForCreationDto>()
                .ReverseMap();
            CreateMap<Entities.Monument, Models.MonumentForUpdateDto>()
                .ReverseMap();
        }
    }
}
