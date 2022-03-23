using Microsoft.AspNetCore.Mvc;
using ProEventos.Persistence.Data;
using ProEventos.Domain.Moldels;
using System;
using System.Collections.Generic;
using System.Linq;
using ProEventos.Application.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Class.Dto;

namespace ProEventos.API.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase {

        public readonly ILoteService _eventoService;
        public EventoController(ILoteService eventoService) {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) {
                    return NoContent();
                }
          
                return Ok(eventos);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError, 
                    $"Error ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetById(int eventoId) {
            try {
                var evento = await _eventoService.GetEventoByIdAsync(eventoId, true);
                if (evento == null) {
                    return NoContent();
                }
                return Ok(evento);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError, 
                    $"Error ao tentar recuperar evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema) {
            try {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) {
                    return NoContent();
                }
                return Ok(evento);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError, 
                    $"Error ao tentar recuperar evento por tema {tema}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model) {
            try {
                var evento = await _eventoService.AddEvento(model);
                if (evento == null) {
                    return BadRequest("Erro ao tentar adicionar evento");
                }
                return Ok(evento);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError, 
                    $"Error ao tentar salvar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDto model) {
            try {
                var evento = await _eventoService.UpdateEvento(eventoId, model);
                if (evento == null) {
                    return BadRequest("Erro ao tentar Atualizar evento");
                }
                return Ok(evento);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError, 
                    $"Error ao tentar atualizar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> Delete(int eventoId) {
            try {
                var evento = await _eventoService.GetEventoByIdAsync(eventoId, true);
                if (evento == null) {
                    return NoContent();
                }

                return await _eventoService.DeleteEvento(eventoId) ? 
                  Ok(new {message = "Deletado" }) : throw new Exception("Erro ao tentar deletar evento");
                
;
            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError,
                    $"Error ao tentar deletar evento. Erro: {ex.Message}");
            }
        }


    }
}
