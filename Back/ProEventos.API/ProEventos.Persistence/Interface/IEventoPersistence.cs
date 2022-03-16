using ProEventos.Domain.Moldels;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface {
    public interface IEventoPersistence {
        
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);

    }
}
