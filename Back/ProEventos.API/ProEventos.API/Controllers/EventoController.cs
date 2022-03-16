using Microsoft.AspNetCore.Mvc;
using ProEventos.Persistence.Data;
using ProEventos.Domain.Moldels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProEventos.API.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase {

        public readonly ProEventosContext _context;
        public EventoController(ProEventosContext context) { _context = context; }

        [HttpGet]
        public IEnumerable<Evento> GetAll() {
            return _context.Eventos;
        }

        [HttpGet("{id}")]
        public Evento GetbyId(int id) {
            return _context.Eventos.FirstOrDefault( evento => evento.Id == id);
        }
    }
}
