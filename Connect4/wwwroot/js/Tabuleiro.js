var tabuleiroDiv;
var quantidadeJogadas = 0;
var tabuleiroId = document.getElementById('tabuleiroId').value;
var jogadorAtual = 0;
var tabuleiroJogo;

obterJogoServidor();

function obterJogoServidor() {
    var xhttp = new XMLHttpRequest();
    xhttp.responseType = 'json'
    var URLObterJogo = "api/Jogo/Obter/" + tabuleiroId;
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.response == null) {
                window.location = "/Identity/Account/Login?ReturnUrl=%2FTabuleiro.html";
            } else {
                MontarTabuleiro(this.response);

                jogadorAtual = this.response.JogadorAtual;
                tabuleiroJogo = this.response.tabuleiroJogo;

                document.getElementById('players').querySelector('li:nth-child(1)').classList.remove = 'playing';
                document.getElementById('players').querySelector('li:nth-child(2)').classList.remove = 'playing';
                document.getElementById('players').querySelector('li:nth-child(' + jogadorAtual + ')').classList.add = 'playing';
            }
        }
    };
    //Prepara uma chamada GET no Servidor.
    xhttp.open("GET", URLObterJogo, true);
    //Envia a chamada.
    xhttp.send();
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
    for (var i = linhas - 1; i >= 0; i--) {
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

function Jogar(coluna, linha) {
    var xhttp = new XMLHttpRequest();
    xhttp.responseType = 'json'
    var URLJogar = "api/Jogo/Jogar/?coluna=" + coluna + "&jogador=" + jogadorAtual;
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            AtualizarPosicao(coluna, linha, jogadorAtual);

            quantidadeJogadas++;
            document.getElementById('jogadas').querySelector('span').innerText = quantidadeJogadas;
        }
    };
    //Prepara uma chamada GET no Servidor.
    xhttp.open("POST", URLJogar, true);
    //Envia a chamada.
    xhttp.send("tabuleiro=" + tabuleiroJogo);
}