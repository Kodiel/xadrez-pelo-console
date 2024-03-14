using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console.xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool Terminada {  get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Azul;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tabuleiro.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = tabuleiro.RetirarPeca(destino);
            tabuleiro.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // roque pequeno 

            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tabuleiro.RetirarPeca(origemT);
                T.IncrementarQteMovimentos();
                tabuleiro.ColocarPeca(T, destinoT);
            }

            // roque grande 

            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tabuleiro.RetirarPeca(origemT);
                T.IncrementarQteMovimentos();
                tabuleiro.ColocarPeca(T, destinoT);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tabuleiro.RetirarPeca(destino);
            p.DecrementarQteMovimentos();
            if(pecaCapturada != null)
            {
                tabuleiro.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tabuleiro.ColocarPeca(p, origem);

            // roque pequeno 

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tabuleiro.RetirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                tabuleiro.ColocarPeca(T, origemT);
            }

            // roque grande  

            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tabuleiro.RetirarPeca(destinoT);
                T.IncrementarQteMovimentos();
                tabuleiro.ColocarPeca(T, origemT);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if(EstaEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if(EstaEmXeque(Adversaria(jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if(TesteXequemate(Adversaria(jogadorAtual)) == true)
            {
                
                Terminada = true;
            }
            else
            {
                turno++;
                MudaJogador();
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (tabuleiro.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe uma peça na posição escolhida.");
            }
            if (jogadorAtual != tabuleiro.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if (!tabuleiro.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existem movimentos possiveis para essa peça.");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!tabuleiro.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }

        private void MudaJogador()
        {
            if(jogadorAtual == Cor.Azul)
            {
                jogadorAtual = Cor.Vermelha;
            }
            else
            {
                jogadorAtual = Cor.Azul;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Azul)
            {
                return Cor.Vermelha;
            }
            else
            {
                return Cor.Azul;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não existe um rei da cor" +  cor + " no jogo.");
            }

            foreach(Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tabuleiro.Linhas;  i++)
                {
                    for (int j = 0; j < tabuleiro.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool TesteXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!TesteXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        public void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(tabuleiro, Cor.Azul));
            ColocarNovaPeca('b', 1, new Cavalo(tabuleiro, Cor.Azul));
            ColocarNovaPeca('c', 1, new Bispo(tabuleiro, Cor.Azul));
            ColocarNovaPeca('d', 1, new Dama(tabuleiro, Cor.Azul));
            ColocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Azul, this));
            ColocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Azul));
            ColocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Azul));
            ColocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Azul));
            ColocarNovaPeca('a', 2, new Peao(tabuleiro, Cor.Azul));
            ColocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Azul));
            ColocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Azul));
            ColocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Azul));
            ColocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Azul));
            ColocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Azul));
            ColocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Azul));
            ColocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Azul));

            ColocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('d', 8, new Dama(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Vermelha, this));
            ColocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Vermelha));
            ColocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Vermelha));
        }
    }
}
