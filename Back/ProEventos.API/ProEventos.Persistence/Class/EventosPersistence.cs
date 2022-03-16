using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Moldels;
using ProEventos.Persistence.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface {
    public class EventosPersistence : IEventoPersistence {

        private readonly ProEventosContext _context;

        public EventosPersistence(ProEventosContext context) {
            _context = context;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false) {

            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedeSociais);
            // chamar esse metodo se includePalestrante for true,
            // vai incluir os palestrantes no retorno da consulta
            if (includePalestrantes) {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }
            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false) {

            IQueryable<Evento> query = _context.Eventos
                           .Include(e => e.Lotes)
                           .Include(e => e.RedeSociais);
            // chamar esse metodo se includePalestrante for true,
            // vai incluir os palestrantes no retorno da consulta
            if (includePalestrantes) {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

                                            
            query = query.OrderBy(e => e.Id) // vai retorna todos temas que foi passado pelo parametro
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false) {

            IQueryable<Evento> query = _context.Eventos
                          .Include(e => e.Lotes)
                          .Include(e => e.RedeSociais);
            // chamar esse metodo se includePalestrante for true,
            // vai incluir os palestrantes no retorno da consulta
            if (includePalestrantes) {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderBy(e => e.Id) 
                .Where(e => e.Id == eventoId);// vai retorna apeenas um

            return await query.FirstOrDefaultAsync();
        }

    }
}
