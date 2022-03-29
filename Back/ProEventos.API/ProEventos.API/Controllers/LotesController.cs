using Microsoft.AspNetCore.Mvc;
using System;
using ProEventos.Application.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Dto;

namespace ProEventos.API.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase {

        public readonly ILoteService _loteService;
        public LotesController(ILoteService loteService) {
            _loteService = loteService;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetAll(int eventoId) {
            try {
                var lotes = await _loteService.GetAllLotesByEventoIdsAsync(eventoId);
                if (lotes == null) {
                    return NoContent();
                }
          
                return Ok(lotes);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError, 
                    $"Error ao tentar recuperar lotes. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> SaveLotes(int eventoId, LoteDto[] models) {
            try {
                var lotes = await _loteService.SaveLotes(eventoId, models);
                if (lotes == null) {
                    return BadRequest("Erro ao tentar salvar lotes");
                }
                return Ok(lotes);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError, 
                    $"Error ao tentar salvar lotes. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId) {
            try {
                var lote = await _loteService.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) {
                    return NoContent();
                }

                return await _loteService.DeleteLote(lote.EventoId, lote.Id) ? 
                  Ok(new {message = "Deletado" }) : throw new Exception("Erro ao tentar deletar lote");
                
;
            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError,
                    $"Error ao tentar deletar lote. Erro: {ex.Message}");
            }
        }


    }
}
