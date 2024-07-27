using AutoMapper;
using Examen2Poo.Database.Entities;
using Examen2Poo.Dto.Amortitation;
using Examen2Poo.Dto.Client;
namespace Examen2Poo.API.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {

            MapsForClients();
            MapsForAmortizations();
            MapsForPrueba();

        }

        private void MapsForClients()
        {

            CreateMap<ClientEntity, ClientDto>()
           .ForMember(dest => dest.Amortitations,
               opt => opt.MapFrom(src => src.Amortitations
                   .Select(pt => pt.Amortization.NombreClave).ToList()));

            CreateMap<ClientCreateDto, ClientEntity>();
            CreateMap<ClientEditDto, ClientEntity>();
            // CreateMap<>
        }

        private void MapsForPrueba()
        {
            CreateMap<ClientEntity, dtoprueba>()
                .ForMember(dest => dest.AmortizationIds,opt => opt
                .MapFrom(src => src.Amortitations.Select(pt => pt.Amortization.NombreClave).ToList()));
            CreateMap<AmortizationEntity, AmortitationDto>();
            CreateMap<AmortitationCreateDto, AmortitationDto>();
            CreateMap<AmortitationEditDto, AmortitationDto>();
        }
        private void MapsForAmortizations()
        {
            CreateMap<AmortizationEntity, AmortitationDto>();
            CreateMap<AmortitationCreateDto, AmortitationDto>();
            CreateMap<AmortitationEditDto, AmortitationDto>();
        }
    }
}
