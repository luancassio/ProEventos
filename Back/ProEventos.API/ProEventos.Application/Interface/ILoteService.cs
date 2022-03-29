using ProEventos.Application.Dto;
using System.Threading.Tasks;

namespace ProEventos.Application.Interface {
    public interface ILoteService {
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] model);
        Task<bool> DeleteLote(int eventoId, int loteId);

        Task<LoteDto[]> GetAllLotesByEventoIdsAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}
