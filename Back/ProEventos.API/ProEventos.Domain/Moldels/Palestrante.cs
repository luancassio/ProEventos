using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;

namespace ProEventos.Domain.Moldels {
    public class Palestrante {
        public int Id { get; set; }
        public string MiniCurriculo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<RedeSocial> RedeSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }

    }
}
