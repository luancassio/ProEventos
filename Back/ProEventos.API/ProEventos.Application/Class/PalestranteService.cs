using ProEventos.Application.Interface;
using ProEventos.Domain.Moldels;
using ProEventos.Persistence.Interface;
using System.Threading.Tasks;
using System;

namespace ProEventos.Application.Class {
    public class EventoPalestrante : IPalestranteService {

        private readonly IGenericsPersistence _genericsPersistence;
        private readonly IPalestrantePersistence _palestrantePersistence;

        public EventoPalestrante(IGenericsPersistence genericsPersistence, IPalestrantePersistence palestrantePersistence) {

            _genericsPersistence = genericsPersistence;
            _palestrantePersistence = palestrantePersistence; 
        }

        public async Task<Palestrante> AddPalestrante(Palestrante model) {
            try {

                _genericsPersistence.Add<Palestrante>(model);
                if (await _genericsPersistence.SaveChangesAsync()) {
                    return await _palestrantePersistence.GetPalestranteByIdAsync(model.Id, false);
                }
                return null;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model) {
            try {

                var palestrante = await _palestrantePersistence.GetPalestranteByIdAsync(palestranteId, false);
                if (palestrante == null) {
                    return null;
                }

                model.Id = palestrante.Id;
                _genericsPersistence.Update<Palestrante>(model);

                if (await _genericsPersistence.SaveChangesAsync()) {
                    return await _palestrantePersistence.GetPalestranteByIdAsync(palestranteId, false);
                }
                return null;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeletePalestrante(int palestranteId) {
            try {

                var palestrante = await _palestrantePersistence.GetPalestranteByIdAsync(palestranteId, false);

                if (palestrante == null) {
                    throw new Exception("Palestrante para deletar não encontrado!");
                }

                _genericsPersistence.Delete<Palestrante>(palestrante);
                return await _genericsPersistence.SaveChangesAsync();

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false) {
            try {

                var palestrantes = await _palestrantePersistence.GetAllPalestrantesAsync(includeEventos);

                if (palestrantes == null) {
                    return null;
                }

                return palestrantes;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false) {
            try {

                var palestrantes = await _palestrantePersistence.GetAllPalestrantesByNomeAsync(nome, includeEventos);

                if (palestrantes == null) {
                    return null;
                }

                return palestrantes;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false) {
            try {

                var palestrante = await _palestrantePersistence.GetPalestranteByIdAsync(palestranteId, includeEventos);

                if (palestrante == null) {
                    return null;
                }

                return palestrante;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

    }
}
