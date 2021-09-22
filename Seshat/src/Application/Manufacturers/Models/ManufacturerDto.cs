using AutoMapper;
using Seshat.Application.Common.Mappings;
using Seshat.Domain.Entities;

namespace Seshat.Application.Manufacturers.Models
{
    public class ManufacturerDto : IMapFrom<Manufacturer>
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Manufacturer, ManufacturerDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.PublicIdentifier));
        }
    }
}