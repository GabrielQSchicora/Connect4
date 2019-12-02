using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public Jogador Jogador1 { get; set; }
        [ForeignKey("Jogador1")]
        public int? Jogador1Id { get; set; }
        public Jogador Jogador2 { get; set; }
        [ForeignKey("Jogador2")]
        public int? Jogador2Id { get; set; }
        public Tabuleiro tabuleiro { get; set; }
        [ForeignKey("tabuleiro")]
        public int? TabuleiroId { get; set; }
        [InverseProperty("Jogos")]
        public Torneio Torneio { get; set; }
        [ForeignKey("Torneio")]
        public int? TorneioId { get; set; }
    }
}
