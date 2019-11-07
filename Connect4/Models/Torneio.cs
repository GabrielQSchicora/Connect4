using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Torneio
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z0-9\s-_\@]*$", 
            ErrorMessage = "O nome do torneio não pode começar com um número, precisa começar com letra maiuscula e não aceita caracteres especiais (exceto - e _).")]
        public String Nome { get; set; }
        [Display(Name = "Quantidade de Jogadores")]
        [Range(4,16, ErrorMessage = "Quantidade de jogadores deve ser entre 4 e 16.")]
        public int QuantidadeJogadores { get; set; }
        [Display(Name = "Início")]
        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }
        public String Dono { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal Premiacao { get; set; }
        [InverseProperty("Jogador")]
        public virtual IList<Jogador> Jogadores { get; set; }
        public virtual IList<Jogo> Jogos { get; set; }

        public Torneio()
        {
            for (int i = 0; i < this.QuantidadeJogadores * 2; i++)
            {
                Jogos.Add(new Jogo());
            }
        }

        public void GerarJogos()
        {
            for(int i=0; i<this.QuantidadeJogadores * 2; i++)
            {
                for (int j = 0; j < this.QuantidadeJogadores; j++)
                {
                    if(Jogadores.IndexOf(i) == Jogadores.IndexOf(j))
                    {
                        continue;
                    }
                    Jogo j1 = new Jogo();
                    j1.Jogador1 = Jogadores.IndexOf(i);
                    j1.Jogador2 = Jogadores.IndexOf(j);
                }
            }
        }
    }
}
