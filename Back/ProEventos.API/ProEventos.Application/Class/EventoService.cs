using ProEventos.Application.Interface;
using ProEventos.Domain.Moldels;
using ProEventos.Persistence.Interface;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application.Class {
    public class EventoService : IEventoService {

        private readonly IGenericsPersistence _genericsPersistence;
        private readonly IEventoPersistence _eventoPersistence;

        public EventoService(IGenericsPersistence genericsPersistence, IEventoPersistence eventoPersistence) {

            _genericsPersistence = genericsPersistence;
            _eventoPersistence = eventoPersistence; 
        }
        public async Task<Evento> AddEvento(Evento model) {
            try {

                _genericsPersistence.Add<Evento>(model);// add evento que recebi no parametro
                if (await _genericsPersistence.SaveChangesAsync()) {
                    // caso o evento foi salvo com sucesso, ele vai entrar no if e vai me retorna o evento salvo
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                return null;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Evento> UpdateEvento(int eventoId, Evento model) {
            try {

                // vai buscar o evento que quero atualizar
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null) { // se não retornou nada returno null
                    return null;
                }

                model.Id = evento.Id; 

                _genericsPersistence.Update<Evento>(model); // se encontrou vai atualizar 
                if (await _genericsPersistence.SaveChangesAsync()) {
                    // se ele foi atualizado ele vai retorno o evento
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                return null;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteEvento(int eventoId) {
            try {

                // vai buscar o evento que quero deletar
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null) {
                    throw new Exception("Evento para deletar não encontrado!");
                }

                _genericsPersistence.Delete<Evento>(evento); // se encontrou  deleta
                return await _genericsPersistence.SaveChangesAsync();

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false) {
            try {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);

                if (eventos == null) {
                    return null;
                }
                return eventos;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async  Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false) {
            try {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos == null) {
                    return null;
                }
                return eventos;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false) {
            try {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);

                if (evento == null) {
                    return null;
                }
                return evento;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

    }
}
