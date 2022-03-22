using AutoMapper;
using ProEventos.Application.Class.Dto;
using ProEventos.Domain.Moldels;

namespace ProEventos.Application.Helpers {
    public class ProEventosProfile : Profile {
        
        public ProEventosProfile() {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();


        }
    }
}
