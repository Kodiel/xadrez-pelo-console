using System;
using xadrez;
using tabuleiro;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tabuleiro = new Tabuleiro(8, 8);

                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 3));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(0, 2));

                Tela.ImprimirTabuleiro(tabuleiro);
            }
            catch (TabuleiroException tab)
            {
                Console.WriteLine(tab.Message);
            }

            Console.ReadLine();
        }
    }
}