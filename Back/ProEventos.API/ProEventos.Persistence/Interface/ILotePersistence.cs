using ProEventos.Domain.Moldels;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface {
    public interface ILotePersistence {
        Task<Lote[]> GetAllLotesByEventoIdAsync(int eventoId);
        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);

    }
}
