using Microsoft.AspNetCore.Mvc;
using ProEventos.Persistence.Data;
using ProEventos.Domain.Moldels;
using System;
using System.Collections.Generic;
using System.Linq;
using ProEventos.Application.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase {

        public readonly IEventoService _eventoService;
        public EventoController(IEventoService eventoService) {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) {
                    return NotFound("Nenhum evento foi encontrado!");
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
                    return NotFound($"Nenhum evento com  id {eventoId} foi encontrado!");
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
                    return NotFound($"Nenhum evento com  o {tema} foi encontrado!");
                }
                return Ok(evento);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError, 
                    $"Error ao tentar recuperar evento por tema {tema}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model) {
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
        public async Task<IActionResult> Put(int eventoId, Evento model) {
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
                
                return await _eventoService.DeleteEvento(eventoId) ? 
                  Ok("Evento deletado com sucesso!") : BadRequest("Erro ao tentar deletar evento");
                
;
            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError,
                    $"Error ao tentar deletar evento. Erro: {ex.Message}");
            }
        }


    }
}
