using AutoMapper;
using ProEventos.Application.Class.Dto;
using ProEventos.Application.Interface;
using ProEventos.Domain.Moldels;
using ProEventos.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Application.Class {
    public class EventoService : IEventoService {

        private readonly IGenericsPersistence _genericsPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IMapper _mapper;


        public EventoService(IGenericsPersistence genericsPersistence, 
                             IEventoPersistence eventoPersistence,
                             IMapper mapper) {

            _genericsPersistence = genericsPersistence;
            _eventoPersistence = eventoPersistence;
            _mapper = mapper;

        }
        public async Task<EventoDto> AddEvento(EventoDto eventoDto) {
            try {
                var evento = _mapper.Map<Evento>(eventoDto);

                _genericsPersistence.Add<Evento>(evento);// add evento que recebi no parametro
                if (await _genericsPersistence.SaveChangesAsync()) {
                    // caso o evento foi salvo com sucesso, ele vai entrar no if e vai me retorna o evento salvo
                    var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto eventoDto) {
            try {
                // vai buscar o evento que quero atualizar
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null) { // se não retornou nada returno null
                    return null;
                }

                eventoDto.Id = evento.Id;
                _mapper.Map(eventoDto, evento);

                _genericsPersistence.Update<Evento>(evento); // se encontrou vai atualizar 

                if (await _genericsPersistence.SaveChangesAsync()) {
                    // se ele foi atualizado ele vai retorno o evento
                    var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
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
        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false) {
            try {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);

                if (eventos == null) {
                    return null;
                }

                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async  Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false) {
            try {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos == null) {
                    return null;
                }
                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false) {
            try {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);

                if (evento == null) {
                    return null;
                }
                var resultado = _mapper.Map<EventoDto>(evento);

                return resultado;

            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

    }
}
