using ProEventos.Domain.Moldels;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface {
    public interface IEventoPersistence {
        
        Task<Evento[]> GetAllEventosByTemaAsync(int UserId, string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(int UserId, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int UserId, int eventoId, bool includePalestrantes = false);

    }
}
