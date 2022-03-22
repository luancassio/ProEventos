using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Class.Dto {
    public class EventoDto {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório!")]
        [MaxLength(50, ErrorMessage = "O {0} deve ter no máximo 50 caracteres!")]
        public string Tema { get; set; }

        [Range(50, 500, ErrorMessage = "Quantidade de pessoas deve ser no mínimo 50 e no máximo 500!")]
        public int QtdPessoa { get; set; }

        [Display(Name ="Imagem")]
        [RegularExpression(@".*\.(jpe?g|png)$", ErrorMessage ="Formato da {0} não suportado!")]
        [Required(ErrorMessage ="{0} é obrigatório!")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório!")]
        [Phone(ErrorMessage ="O campo {0} está inválido!")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Informe um {0} válido!")]
        [Required(ErrorMessage = "Campo {0} é obrigatório!")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedeSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}
