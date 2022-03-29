using System;
using System.ComponentModel.DataAnnotations;


namespace ProEventos.Application.Dto {
    public class LoteDto {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string DataFim { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [Range(50, 500, ErrorMessage = "Quantidade de pessoas deve ser no mínimo 50 e no máximo 500!")]
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public EventoDto Evento { get; set; }
    }
}
