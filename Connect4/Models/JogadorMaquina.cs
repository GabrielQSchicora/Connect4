using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class JogadorMaquina : Jogador
    {
        public String URLServico { get; set; }
        public String NomeMaquina { get; set; }
        public override string Nome { get => "(Computador) " + NomeMaquina; }
    }
}
