using ProEventos.Domain.Moldels;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface {
    public interface IPalestrantePersistence {

        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false);
    }
}
