using AutoMapper;
using Seshat.Application.Common.Mappings;
using Seshat.Application.Manufacturers.Models;
using Seshat.Domain.Entities;

namespace Seshat.Application.Printers.Models
{
    public class PrinterDto : IMapFrom<Printer>
    {
        public string Id { get; set; } = null!;
        public string Model { get; set; } = null!;
        public ManufacturerDto Manufacturer { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Printer, PrinterDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.PublicIdentifier))
                .ForMember(dest => dest.Model, opt =>
                    opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Manufacturer, opt =>
                    opt.MapFrom(src => src.Manufacturer));
        }
    }
}