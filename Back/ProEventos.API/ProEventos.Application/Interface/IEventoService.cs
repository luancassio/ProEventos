using ProEventos.Application.Class.Dto;
using System.Threading.Tasks;

namespace ProEventos.Application.Interface {
    public interface IEventoService {
        Task<EventoDto> AddEvento(EventoDto model);
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int eventoId);

        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);    
        Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}
