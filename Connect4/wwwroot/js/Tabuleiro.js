var tabuleiroDiv;
var jogoId;
var authPlayerId;
var jogadorAtual;

function obterJogoServidor(id) {
    var xhttp = new XMLHttpRequest();
    xhttp.responseType = 'json'
    var URLObterJogo = "/api/Jogo/Obter/" + id;
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.response == null) {
                window.location = "/Identity/Account/Login?ReturnUrl=%2FTabuleiro.html";
            } else {
                MontarTabuleiro(this.response);
                verificaGanhador();
                jogadorAtual = this.response.jogadorAtual;
                trocaJogadorAtual(this.response.jogadorAtual);
                trocaQuantidadeJogadas(this.response.quantidadeJogadas);
            }
        }
    };
    //Prepara uma chamada GET no Servidor.
    xhttp.open("GET", URLObterJogo, true);
    //Envia a chamada.
    xhttp.send();
}

function trocaJogadorAtual(jogador) {
    document.getElementById('players').querySelector('li:nth-child(1)').classList.remove('playing');
    document.getElementById('players').querySelector('li:nth-child(2)').classList.remove('playing');
    document.getElementById('players').querySelector('li:nth-child(' + jogador + ')').classList.add('playing');
}

function trocaQuantidadeJogadas(quantidadeJogadas) {
    document.getElementById('jogadas').querySelector('span').innerText = quantidadeJogadas;
}

function MontarTabuleiro(Tabuleiro) {
    var TamanhoColunas = Tabuleiro.tabuleiroJogo.length,
        TamanhoLinhas = Tabuleiro.tabuleiroJogo[0].length;
    CriarTabuleiro(TamanhoColunas, TamanhoLinhas);
    for (coluna = 0; coluna < TamanhoColunas; coluna++) {
        for (linha = 0; linha < TamanhoLinhas; linha++) {
            AtualizarPosicao(coluna, linha, Tabuleiro.tabuleiroJogo[coluna][linha]);
        }
    }
}

function CriarTabuleiro(colunas, linhas) {
    tabuleiroDiv = document.getElementById("Tabuleiro");
    tabuleiroDiv.querySelectorAll('*').forEach(n => n.remove());
    for (var i = 0; i < linhas; i++) {
        var linhaDiv = CriarLinha(colunas);
        linhaDiv.id = 'linha-' + i;
        tabuleiroDiv.appendChild(linhaDiv);
    }
}

function CriarLinha(colunas) {
    var linha = document.createElement('div');
    linha.classList.add('row');
    for (var i = 0; i < colunas; i++) {
        var posicaoDiv = document.createElement('div');
        posicaoDiv.id = 'posCol-' + i;
        posicaoDiv.classList.add('square');
        posicaoDiv.setAttribute('onclick', 'Jogar(this, ' + i + ')')
        linha.appendChild(posicaoDiv);
    }
    return linha;
}

function AtualizarPosicao(coluna, linha, valor) {
    var posicaoDiv = document.querySelector("#Tabuleiro")
        .querySelector("#linha-" + linha)
        .querySelector("#posCol-" + coluna);

    posicaoDiv.classList.remove('Jogador1');
    posicaoDiv.classList.remove('Jogador2');
    if (valor == 1) {
        posicaoDiv.classList.add('Jogador1');
    } else if (valor == 2) {
        posicaoDiv.classList.add('Jogador2');
    }
}

function Jogar(element, coluna) {
    element.classList.add('disabled');
    var xhttp = new XMLHttpRequest();
    xhttp.responseType = 'json'
    var URLJogar = "/api/Jogo/Jogar/?coluna=" + coluna + "&JogoId=" + jogoId;
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                MontarTabuleiro(this.response);
                jogadorAtual = this.response.jogadorAtual;
                trocaJogadorAtual(this.response.jogadorAtual);
                trocaQuantidadeJogadas(this.response.quantidadeJogadas);
                verificaGanhador();
                element.classList.remove('disabled');
            } else {
                if (this.response != null && typeof this.response.Message !== 'undefined') {
                    alert(this.response.Message);
                }
            }
        }
    };

    xhttp.open("GET", URLJogar, true);
    xhttp.send();
}

function verificaGanhador() {
    var xhttp = new XMLHttpRequest();
    xhttp.responseType = 'json'
    var URLJogar = "/api/Jogo/VerificaVencedor/" + jogoId;
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.response != 0) {
                alert(this.response.message);
            }
        } else {
            if (this.response != null && typeof this.response.Message !== 'undefined') {
                alert(this.response.Message);
            }
        }
    };

    xhttp.open("GET", URLJogar, true);
    xhttp.send();
}

function VerificaJogadorAtual(id) {
    var xhttp = new XMLHttpRequest();
    xhttp.responseType = 'json'
    var URLObterJogo = "/api/Jogo/VerificaJogadorAtual/" + id;
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.response == null) {
                window.location = "/Identity/Account/Login?ReturnUrl=%2FTabuleiro.html";
            } else {
                if (this.response != jogadorAtual) {
                    obterJogoServidor(id);
                }
            }
        }
    };
    //Prepara uma chamada GET no Servidor.
    xhttp.open("GET", URLObterJogo, true);
    //Envia a chamada.
    xhttp.send();
}