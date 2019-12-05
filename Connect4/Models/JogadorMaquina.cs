using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class JogadorMaquina : Jogador
    {
        [Display(Name = "URL do serviço")]
        public String URLServico { get; set; }
        [Display(Name = "Nome da máquina")]
        public String NomeMaquina { get; set; }
        public override string Nome { get => "(Computador) " + NomeMaquina; }
    }
}
