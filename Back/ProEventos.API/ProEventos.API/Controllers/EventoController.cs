using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Data;
using ProEventos.API.Moldels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProEventos.API.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase {

        public readonly DataContext _context;
        public EventoController(DataContext context) { _context = context; }

        [HttpGet]
        public IEnumerable<Evento> GetAll() {
            return _context.Evento;
        }

        [HttpGet("{id}")]
        public Evento GetbyId(int id) {
            return _context.Evento.FirstOrDefault( evento => evento.EventoId == id);
        }
    }
}
