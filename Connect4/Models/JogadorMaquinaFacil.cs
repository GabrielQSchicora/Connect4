using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class JogadorMaquinaFacil : IJogadorComputador
    {
        public int RealizarJogada(Tabuleiro tabuleiro)
        {
            int jogada;
            do
            {
                jogada = new Random().Next(Tabuleiro.NUMERO_COLUNAS);
            } while (tabuleiro.TabuleiroJogo[0, jogada] != 0);

            return jogada;
        }
    }
}
