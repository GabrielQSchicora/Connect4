using Connect4.Models;
using System;
using Xunit;

namespace TesteTabuleiro
{
    public class TestesUnitarios
    {
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
        public void TestVerificaDiagonal()
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
            Assert.Equal(0, t.VerificaDiagonal());
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
            Assert.Equal(2, t.VerificaDiagonal());
            //Existe um vencedor na diagnoal. Posicao [4,0] at� [1,3]
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
            Assert.Equal(2, t.VerificaDiagonal());
            //Existe um vencedor na diagnoal. Posicao [0,0] at� [3,3]
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
            Assert.Equal(1, t.VerificaDiagonal());
            //N�o existe vencedor na diagonal.
            valor = new int[7, 6]{
                {1,0,0,0,0,0},
                {1,1,2,2,0,0},
                {2,1,2,0,0,0},
                {1,2,2,1,0,0},
                {2,1,2,0,0,0},
                {1,1,1,1,0,0},
                {1,1,2,0,0,0},
              };
            t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificaDiagonal());
            //Existe um vencedor na diagonal.
            //Posi��o [6,1] at� Posi��o [3,4]
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
            Assert.Equal(0, t.VerificaDiagonal());
        }
    }
}