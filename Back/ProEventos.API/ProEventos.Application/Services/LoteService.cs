using AutoMapper;
using ProEventos.Application.Dto;
using ProEventos.Application.Interface;
using ProEventos.Domain.Moldels;
using ProEventos.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Class {
    public class LoteService : ILoteService {

        private readonly IGenericsPersistence _genericsPersistence;
        private readonly ILotePersistence _lotePersistence;
        private readonly IMapper _mapper;
        public LoteService(IGenericsPersistence genericsPersistence,
                           ILotePersistence lotePersistence,
                           IMapper mapper) {

            _genericsPersistence = genericsPersistence;
            _lotePersistence = lotePersistence;
            _mapper = mapper;

        }
        public async Task AddLote(int eventoId, LoteDto model) {
            try {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;

                _genericsPersistence.Add<Lote>(lote);

                await _genericsPersistence.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models) {
            try {
                var lotes = await _lotePersistence.GetAllLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models) {
                    if (model.Id == 0) {
                        await AddLote(eventoId, model);
                    } else {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                        model.EventoId = eventoId;

                        _mapper.Map(model, lote);

                        _genericsPersistence.Update<Lote>(lote);

                        await _genericsPersistence.SaveChangesAsync();
                    }
                }

                var loteRetorno = await _lotePersistence.GetAllLotesByEventoIdAsync(eventoId);

                return _mapper.Map<LoteDto[]>(loteRetorno);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId) {
            try {
                var lote = await _lotePersistence.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote para delete não encontrado.");

                _genericsPersistence.Delete<Lote>(lote);
                return await _genericsPersistence.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> GetAllLotesByEventoIdsAsync(int eventoId) {
            try {
                var lotes = await _lotePersistence.GetAllLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                var resultado = _mapper.Map<LoteDto[]>(lotes);

                return resultado;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId) {
            try {
                var lote = await _lotePersistence.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) return null;

                var resultado = _mapper.Map<LoteDto>(lote);

                return resultado;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

    }
}
