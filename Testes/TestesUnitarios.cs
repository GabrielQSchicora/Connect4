using Connect4.Models;
using System;
using Xunit;

namespace TesteTabuleiro
{
    public class TestesUnitarios
    {
        [Fact]
        public void TesteVerificaCPF()
        {
            ApplicationUser user = new ApplicationUser();
            Assert.True(user.ValidaCPF("108.721.179-44"));

            Assert.False(user.ValidaCPF("108.721.179-42"));

            Assert.True(user.ValidaCPF("789.039.209-53"));

            Assert.False(user.ValidaCPF("789.039.209-54"));
        }

        [Fact]
        public void TesteGerador()
        {
            Torneio t = new Torneio();
            t.QuantidadeJogadores = 4;
            t.Jogadores.Add(new Jogador { });
            t.Jogadores.Add(new Jogador { });
            t.Jogadores.Add(new Jogador { });
            t.Jogadores.Add(new Jogador { });

            Assert.True(t.GerarJogos());
        }

        [Fact]
        public void TestTudoOcupado()
        {

            int[,] valor = new int[7, 6]{
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
              };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.False(t.VerificaEmpate());
            valor = new int[7, 6]{
                {1,2,1,1,2,1},
                {1,2,1,1,2,1},
                {2,1,2,2,1,2},
                {2,1,2,2,1,2},
                {1,2,1,1,2,1},
                {1,2,1,1,2,1},
                {2,1,2,2,1,2}
              };
            t = new Tabuleiro(valor);
            Assert.True(t.VerificaEmpate());

        }

        [Fact]
        public void TestVencedorColuna()
        {
            int[,] valor = new int[7, 6]{
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
              };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificaColuna());
            valor = new int[7, 6]{
                {1,2,1,1,2,1},
                {1,2,1,1,2,1},
                {2,1,2,2,1,2},
                {2,1,2,2,1,2},
                {1,2,1,1,2,1},
                {1,2,1,1,1,1},
                {2,1,2,2,1,2}
              };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificaColuna());


            valor = new int[7, 6]{
                {1,2,1,1,2,1},
                {1,2,1,1,2,1},
                {2,1,2,2,1,2},
                {2,1,2,2,1,2},
                {1,2,1,1,2,1},
                {1,2,1,2,1,1},
                {2,2,2,2,1,2}
              };
            t = new Tabuleiro(valor);
            Assert.Equal(2, t.VerificaColuna());

            valor = new int[7, 6]{
                {1,1,1,1,2,1},
                {1,2,1,1,2,1},
                {2,1,2,2,1,2},
                {2,1,2,2,1,2},
                {1,2,1,1,2,1},
                {1,2,1,2,1,1},
                {2,2,1,2,1,2}
              };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificaColuna());
        }



        [Fact]
        public void TestVencedorLinha()
        {
            int[,] valor = new int[7, 6]{
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
              };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificaLinha());
            valor = new int[7, 6]{
                {1,2,1,1,2,1},
                {1,2,1,1,2,1},
                {1,1,2,2,1,2},
                {1,1,2,2,1,2},
                {2,2,1,1,2,1},
                {1,2,1,1,1,1},
                {2,1,2,2,1,2}
              };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificaLinha());
            valor = new int[7, 6]{
                {1,2,1,1,2,1},
                {1,2,1,2,1,1},
                {2,1,2,2,1,2},
                {1,1,2,2,1,1},
                {2,2,1,1,2,1},
                {1,2,1,1,1,1},
                {2,1,2,2,1,1}
              };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificaLinha());
        }

        [Fact]
        public void TestVerificarVencedorDiagonal()
        {
            int[,] valor = new int[7, 6]{
                {0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                { 0,0,0,0,0,0 },
                { 0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificarVencedorDiagonal());
            valor = new int[7, 6]{
                {1,0,0,0,0,0},
                {1,0,0,0,0,0},
                {2,0,0,0,0,0},
                {1,2,0,0,0,0},
                {2,1,2,0,0,0},
                {1,1,2,2,0,0},
                {1,1,2,0,0,0},
                };
            t = new Tabuleiro(valor);
            Assert.Equal(2, t.VerificarVencedorDiagonal());
            //Existe um vencedor na diagnoal. Posicao [4,0] até [1,3]

            //Existe um vencedor na diagnoal. Posicao [0,0] até [3,3]
            valor = new int[7, 6]{
                {1,0,0,0,0,0},
                {1,1,2,2,0,0},
                {1,1,1,0,0,0},
                {2,2,2,1,0,0},
                {2,1,2,0,0,0},
                {1,1,1,2,0,0},
                {1,1,2,0,0,0},
                };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificarVencedorDiagonal());
            //Não existe vencedor na diagonal.
            valor = new int[7, 6]{
                {1,0,0,0,0,0},
                {1,1,2,2,0,0},
                {2,1,2,0,0,0},
                {1,2,2,1,0,0},
                {2,1,2,0,0,0},
                {1,1,1,0,0,0},
                {1,1,2,0,0,0},
                };
            t = new Tabuleiro(valor);
            Assert.Equal(2, t.VerificarVencedorDiagonal());
            //Existe um vencedor na diagonal.
            //Posição [6,1]
            valor = new int[7, 6]{
                {1,0,0,0,0,0},
                {1,1,2,1,0,0},
                {2,1,2,0,0,0},
                {1,2,2,1,1,0},
                {2,1,2,1,0,0},
                {2,1,1,1,0,0},
                {1,1,2,0,0,0},
                };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificarVencedorDiagonal());
            valor = new int[7, 6]{
                {1,0,0,0,0,0},
                {1,1,2,2,0,0},
                {2,1,2,0,0,0},
                {1,2,0,0,0,0},
                {2,1,1,0,0,0},
                {1,1,2,2,0,0},
                {1,1,2,0,0,0},
                };
            t = new Tabuleiro(valor);
            Assert.Equal(2, t.VerificarVencedorDiagonal());
        }
    }
}