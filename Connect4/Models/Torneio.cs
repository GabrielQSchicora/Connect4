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
        public virtual IList<Jogador> Jogadores { get; set; } = new List<Jogador>();
        public virtual IList<Jogo> Jogos { get; set; }


        public Boolean GerarJogos()
        {
            List<Jogo> jogosTurno = new List<Jogo>();
            List<Jogo> jogosReturno = new List<Jogo>();

            //Quantidade de jogos -> (((this.QuantidadeJogadores - 1) * 2) * this.QuantidadeJogadores) / 2;

            for(int i = 0; i < this.QuantidadeJogadores; i++)
            {
                for(int j = i+1; j < this.QuantidadeJogadores; j++)
                {
                    Jogo turno = new Jogo
                    {
                        Jogador1 = Jogadores[i],
                        Jogador2 = Jogadores[j],
                        tabuleiro = new Tabuleiro()
                    };
                    Jogo returno = new Jogo
                    {
                        Jogador1 = Jogadores[j],
                        Jogador2 = Jogadores[i],
                        tabuleiro = new Tabuleiro()
                    };
                    jogosTurno.Add(turno);
                    jogosReturno.Add(returno);
                }
            }

            jogosTurno = this.ShuffleList(jogosTurno);
            jogosReturno = this.ShuffleList(jogosReturno);

            jogosTurno.AddRange(jogosReturno);
            //this.Jogos.Clear();
            this.Jogos = jogosTurno;

            return true;
        }

        private List<Jogo> ShuffleList(List<Jogo> inputList)
        {
            List<Jogo> randomList = new List<Jogo>();

            Random r = new Random();
            int randomIndex;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }

            return randomList;
        }
    }
}
