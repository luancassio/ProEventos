using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Moldels;
using ProEventos.Persistence.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface {
    public class PalestrantePersistence : IPalestrantePersistence {

        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context) {
            _context = context;
        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false) {

            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedeSociais);

                if (includeEventos) {
                query = query.AsNoTracking().Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }
                return await query.OrderBy(p => p.Id).ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false) {

            IQueryable<Palestrante> query = _context.Palestrantes
               .Include(p => p.RedeSociais);

            if (includeEventos) {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }
            query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.User.PrimeiroNome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false) {

            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedeSociais);

            if (includeEventos) {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }
            query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

    }
}
