using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class JogadorMaquina : Jogador
    {
        public String URLServico { get; set; }
        public String Nome { get; set; }
    }
}
