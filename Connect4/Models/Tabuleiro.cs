using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Tabuleiro
    {
        public static int NUMERO_LINHAS = 6;
        public static int NUMERO_COLUNAS = 7;
        public int JogadorAtual { get; private set; } = new Random().Next(1, 3);
        //Utilizar coluna X linha
        public int[,] TabuleiroJogo { get; set; }

        /// <summary>
        /// Construtor. Cria o tabuleiro com todas as posições vazias.
        /// </summary>
        public Tabuleiro()
        {
            TabuleiroJogo = new int[NUMERO_COLUNAS, NUMERO_LINHAS];
            for (int coluna = 0; coluna < NUMERO_COLUNAS; coluna++)
            {
                for (int linha = 0; linha < NUMERO_LINHAS; linha++)
                {
                    TabuleiroJogo[coluna, linha] = 0;
                }
            }
        }

        /// <summary>
        /// Construtor. Cria o tabuleiro a partir de um array passado via parâmetro
        /// </summary>
        /// <param name="tabuleiro">Um array com os dados para o tabuleiro.</param>
        public Tabuleiro(int[,] tabuleiro)
        {
            TabuleiroJogo = tabuleiro;
        }

        /// <summary>
        /// Realiza uma jogada, em uma coluna selecionada pelo jogador
        /// </summary>
        /// <param name="jogador">O autor da jogada.</param>
        /// <param name="posicao">A coluna da jogada.</param>
        public void Jogar(int coluna, int jogador)
        {
            if(jogador != this.JogadorAtual)
            {
                throw new Exception("Não é a sua vez de jogar.");
            }

            if(coluna < 0)
            {
                throw new Exception("A posição deve ser maior que zero.");
            }else if (coluna > NUMERO_COLUNAS - 1)
            {
                throw new Exception($"A posição deve ser menor que {NUMERO_COLUNAS}.");
            }

            var jogou = false;

            for(int linha = NUMERO_LINHAS - 1; linha >= 0; linha--)
            {
                if(TabuleiroJogo[coluna, linha] == 0)
                {
                    TabuleiroJogo[coluna, linha] = jogador;
                    jogou = true;
                    this.MudaJogadorAtual();
                    break;
                }
            }

            if (!jogou)
            {
                throw new ArgumentException("A coluna selecionada já está cheia.");
            }
        }

        /// <summary>
        /// Retornar se existe algum ganhador.
        /// </summary>
        /// <returns>Retorna 0 se o jogo não acabou. -1 Se o jogo terminou empatado ou um número do jogador vencedor em caso de alguma vitória.</returns>
        public int VerificaVencedor()
        {
            int resultado;

            resultado = this.VerificaLinha();
            if (resultado != 0)
            {
                return resultado;
            }

            resultado = this.VerificaColuna();
            if (resultado != 0)
            {
                return resultado;
            }

            resultado = this.VerificaDiagonal();
            if (resultado != 0)
            {
                return resultado;
            }

            if (this.VerificaEmpate())
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Retornar se alguem ganhou na horizontal (Linha).
        /// </summary>
        /// <returns>Retorna 0 para nenhum jogador ou o numero do jogador que ganhou, em caso de vitória.</returns>
        public int VerificaLinha()
        {
            for (int Linha = NUMERO_LINHAS - 1; Linha >= 0; Linha--)
            {
                int acertosConsecultivos = 1;
                for (int Coluna = NUMERO_COLUNAS - 2; Coluna >= 0 ; Coluna--)
                {
                    if (TabuleiroJogo[Coluna + 1, Linha] != TabuleiroJogo[Coluna, Linha])
                    {
                        acertosConsecultivos = 1;
                    }
                    else
                    {
                        acertosConsecultivos += 1;

                        if (acertosConsecultivos == 4)
                        {
                            return TabuleiroJogo[Coluna + 1, Linha];
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Retornar se alguem ganhou na vertical (Coluna).
        /// </summary>
        /// <returns>Retorna 0 para nenhum jogador ou o numero do jogador que ganhou, em caso de vitória.</returns>
        public int VerificaColuna()
        {
            for (int Coluna = NUMERO_COLUNAS - 1; Coluna >= 0; Coluna--)
            {
                int acertosConsecultivos = 1;

                if (TabuleiroJogo[Coluna, 0] == 0)
                {
                    continue;
                }

                for (int Linha = NUMERO_LINHAS - 2; Linha >= 0; Linha--)
                {
                    if (TabuleiroJogo[Coluna, Linha] == 0)
                    {
                        break;
                    }

                    if (TabuleiroJogo[Coluna, Linha + 1] != TabuleiroJogo[Coluna, Linha])
                    {
                        acertosConsecultivos = 1;
                    }
                    else
                    {
                        acertosConsecultivos += 1;

                        if (acertosConsecultivos == 4)
                        {
                            return TabuleiroJogo[Coluna, Linha + 1];
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Retornar se alguem ganhou na Diagonal.
        /// </summary>
        /// <returns>Retorna 0 para nenhum jogador ou o numero do jogador que ganhou, em caso de vitória.</returns>
        public int VerificaDiagonal()
        {
            return 0;
        }

        /// <summary>
        /// Retornar se o jogo está empatado.
        /// </summary>
        /// <returns>Retorna true para jogo empatado e false para jogo em andamento.</returns>
        public Boolean VerificaEmpate()
        {
            for(int i = 0; i < NUMERO_COLUNAS; i++)
            {
                if(TabuleiroJogo[i, 0] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Troca o jogador. 
        /// </summary>
        private void MudaJogadorAtual()
        {
            if(JogadorAtual == 1)
            {
                JogadorAtual = 2;
            }
            else
            {
                JogadorAtual = 1;
            }
        }
    }
}
