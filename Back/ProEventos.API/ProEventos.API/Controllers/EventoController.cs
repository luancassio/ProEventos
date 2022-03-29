using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ProEventos.Application.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Dto;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ProEventos.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ProEventos.API.Controllers {

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase {

        public readonly IEventoService _eventoService;
        public readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAccountService _accountService;

        public EventoController(IEventoService eventoService, 
                                IWebHostEnvironment hostEnvironment,
                                IAccountService accountService) {

            _eventoService = eventoService;
            _hostEnvironment = hostEnvironment;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                var eventos = await _eventoService.GetAllEventosAsync(User.GetUserId(), true);
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
                var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), eventoId, true);
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
                var evento = await _eventoService.GetAllEventosByTemaAsync(User.GetUserId(), tema, true);
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
                var evento = await _eventoService.AddEvento(User.GetUserId(), model);
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

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImage(int eventoId) {
            try {

                var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), eventoId, true);
                if (evento == null) {
                    return NoContent();
                }

                var file = Request.Form.Files[0];
                if (file.Length > 0) {
                    DeleteImage(evento.ImageUrl);
                    evento.ImageUrl =  await SaveImage(file);
                }
                var eventoRetorno = await _eventoService.UpdateEvento(User.GetUserId(), eventoId, evento);
                return Ok(eventoRetorno);

            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError,
                    $"Error ao tentar salvar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDto model) {
            try {
                var evento = await _eventoService.UpdateEvento(User.GetUserId(), eventoId, model);
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
                var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), eventoId, true);
                if (evento == null) {
                    return NoContent();
                }

                if (await _eventoService.DeleteEvento(User.GetUserId(), eventoId)) {
                    DeleteImage(evento.ImageUrl);
                    return Ok(new { message = "Deletado" });
                } else {
                    throw new Exception("Erro ao tentar deletar evento");
                }
                   
                
;
            } catch (Exception ex) {

                return this.StatusCode
                    (StatusCodes.Status500InternalServerError,
                    $"Error ao tentar deletar evento. Erro: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile) {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName)
                .Take(10).ToArray()).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);

            using (var fileStream =  new FileStream(imagePath, FileMode.Create)) {
                await imageFile.CopyToAsync(fileStream);
            }
                return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName) {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
            if (System.IO.File.Exists(imagePath)) {
                System.IO.File.Delete(imagePath);
            }
        }


    }
}
