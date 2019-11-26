using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Tabuleiro
    {
        public int Id { get; set; }
        public static int NUMERO_LINHAS = 6;
        public static int NUMERO_COLUNAS = 7;
        public int JogadorAtual { get; private set; } = new Random().Next(1, 3);
        public int? Vencedor { get; set; } = 0;
        public int quantidadeJogadas { get; private set; } = 0;
        //Utilizar coluna X linha
        [NotMapped]
        public int[,] TabuleiroJogo { get; set; }

        /// <summary>
        /// Não é possível para o Entity Framework mapear um array multidimensional.
        /// Por isso representamos ele como uma string de inteiros.
        /// TAMANHOCOLUNA;TAMANHOLINHA;VALORES...
        /// </summary>
        /// jsonIgnore significa que o valor não será serializado no objeto JSON.
        [JsonIgnore]
        public string InternalData
        {
            get
            {
                String internalData = TabuleiroJogo.GetLength(0).ToString() + ';' + TabuleiroJogo.GetLength(1).ToString() + ';';
                for (var coluna = 0; coluna < TabuleiroJogo.GetLength(0); coluna++)
                {
                    for (var linha = 0; linha < TabuleiroJogo.GetLength(1); linha++)
                    {
                        internalData += TabuleiroJogo[coluna, linha] + ";";
                    }
                }
                return internalData;
            }
            set
            {
                string internalData = value;
                var valores = internalData.Split(';');
                TabuleiroJogo = new int[int.Parse(valores[0]), int.Parse(valores[1])];
                for (var coluna = 0; coluna < TabuleiroJogo.GetLength(0); coluna++)
                {
                    for (var linha = 0; linha < TabuleiroJogo.GetLength(1); linha++)
                    {
                        TabuleiroJogo[coluna, linha] = int.Parse(valores[2 + (coluna * TabuleiroJogo.GetLength(1)) + linha]);
                    }
                }
            }
        }

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
        public Boolean Jogar(int coluna, int jogador)
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

            for(int linha = NUMERO_LINHAS - 1; linha >= 0; linha--)
            {
                if(TabuleiroJogo[coluna, linha] == 0)
                {
                    TabuleiroJogo[coluna, linha] = jogador;
                    quantidadeJogadas++;
                    this.MudaJogadorAtual();
                    return true;
                }
            }
            
            throw new ArgumentException("A coluna selecionada já está cheia.");
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
                this.Vencedor = resultado;
                return resultado;
            }

            resultado = this.VerificaColuna();
            if (resultado != 0)
            {
                this.Vencedor = resultado;
                return resultado;
            }

            resultado = this.VerificarVencedorDiagonal();
            if (resultado != 0)
            {
                this.Vencedor = resultado;
                return resultado;
            }

            if (this.VerificaEmpate())
            {
                this.Vencedor = -1;
                return -1;
            }

            this.Vencedor = 0;
            return 0;
        }

        /// <summary>
        /// Retornar se alguem ganhou na horizontal (Linha).
        /// </summary>
        /// <returns>Retorna 0 para nenhum jogador ou o numero do jogador que ganhou, em caso de vitória.</returns>
        public int VerificaLinha()
        {
            for (int linha = 0; linha < TabuleiroJogo.GetLength(1); linha++)
            {
                int contador = 1;
                for (int coluna = TabuleiroJogo.GetLength(0) - 1; coluna >= 1; coluna--)
                {
                    if (TabuleiroJogo[coluna, linha] == 0) { break; }
                    if (TabuleiroJogo[coluna, linha]
                        == TabuleiroJogo[coluna - 1, linha])
                    {
                        if (++contador == 4)
                        {
                            return TabuleiroJogo[coluna, linha];
                        }
                    }
                    else
                    {
                        contador = 1;
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
            for (int coluna = 0; coluna < TabuleiroJogo.GetLength(0); coluna++)
            {
                int contador = 1;
                for (int linha = TabuleiroJogo.GetLength(1) - 1; linha >= 1 ; linha--)
                {
                    if (TabuleiroJogo[coluna, linha] == 0) {
                        break;
                    }
                    if (TabuleiroJogo[coluna, linha]
                        == TabuleiroJogo[coluna, linha - 1])
                    {
                        if (++contador == 4)
                        {
                            return TabuleiroJogo[coluna, linha];
                        }
                    }
                    else
                    {
                        contador = 1;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Retornar se alguem ganhou na Diagonal.
        /// </summary>
        /// <returns>Retorna 0 para nenhum jogador ou o numero do jogador que ganhou, em caso de vitória.</returns>
        public int VerificarVencedorDiagonal()
        {
            for (int coluna = 0; coluna < TabuleiroJogo.GetLength(0); coluna++)
            {
                for (int linha = TabuleiroJogo.GetLength(1) - 1; linha >= 1 ; linha--)
                {
                    int resultado = VerificarDiagonal(coluna, linha);
                    if (resultado != 0)
                        return resultado;
                }
            }
            return 0;
        }

        private int VerificarDiagonal(int coluna, int linha)
        {
            if (TabuleiroJogo[coluna, linha] == 0)
                return 0;
            if (linha + 4 < this.TabuleiroJogo.GetLength(1))
            {
                if (coluna - 4 >= 0)
                {
                    int i;
                    for (i = 1; i < 4; i++)
                    {
                        if (TabuleiroJogo[coluna, linha] !=
                            TabuleiroJogo[coluna - i, linha + i])
                            break;
                    }
                    if (i == 4)
                    {
                        return TabuleiroJogo[coluna, linha];
                    }
                }
                if (coluna + 4 < this.TabuleiroJogo.GetLength(0))
                {
                    int i;
                    for (i = 1; i < 4; i++)
                    {
                        if (TabuleiroJogo[coluna, linha] !=
                            TabuleiroJogo[coluna + i, linha + i])
                            break;
                    }
                    if (i == 4)
                    {
                        return TabuleiroJogo[coluna, linha];
                    }
                }

            }

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
