using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Jogo
    {
        public Jogador Jogador1 { get; set; }
        public Jogador Jogador2 { get; set; }
        public Tabuleiro tabuleiro { get; set; }
    }
}
