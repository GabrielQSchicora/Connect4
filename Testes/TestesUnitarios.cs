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
                {2,2,0,0,0,0},
                {1,1,0,0,0,0},
                {2,2,2,2,0,0},
                {1,2,2,1,0,0},
                {1,1,1,0,0,0},
                {2,1,2,1,2,1},
                {1,2,1,2,1,2},
              };
            t = new Tabuleiro(valor);
            Assert.Equal(2, t.VerificaColuna());


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
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificarDiagonal());
            valor = new int[7, 6]{
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,1,0,0,0},
                {0,0,0,1,0,0},
                {0,0,0,0,1,0},
                {0,0,0,0,0,1},
                };
            t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificarDiagonal());
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
            Assert.Equal(2, t.VerificarDiagonal());
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
            Assert.Equal(1, t.VerificarDiagonal());
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
            Assert.Equal(2, t.VerificarDiagonal());
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
            Assert.Equal(1, t.VerificarDiagonal());
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
            Assert.Equal(2, t.VerificarDiagonal());
        }
    }
}