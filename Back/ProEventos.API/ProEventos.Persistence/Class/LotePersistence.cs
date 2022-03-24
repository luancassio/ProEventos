﻿using ProEventos.Domain.Moldels;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProEventos.Persistence.Class {
    public class LotePersistence : ILotePersistence {

        private readonly ProEventosContext _context;

        public LotePersistence(ProEventosContext context) {
            _context = context;
        }

        public async Task<Lote[]> GetAllLotesByEventoIdAsync(int eventoId) {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id) {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId
                                     && lote.Id == id);

            return await query.FirstOrDefaultAsync();
        }

  
    }
}
