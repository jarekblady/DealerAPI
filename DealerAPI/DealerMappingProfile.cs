using AutoMapper;
using DealerAPI.Entities;
using DealerAPI.Models;

namespace DealerAPI
{
    public class DealerMappingProfile : Profile
    {
        public DealerMappingProfile()
        {
            CreateMap<Dealer, DealerDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.Address.HouseNumber))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Car, CarDto>();
            
            CreateMap<CreateDealerDto, Dealer>()
                .ForMember(r => r.Address,
                    c => c.MapFrom(dto => new Address()
                    { City = dto.City, Street = dto.Street, HouseNumber = dto.HouseNumber, PostalCode = dto.PostalCode }));
            
            CreateMap<CreateCarDto, Car>();
        }
    }
}