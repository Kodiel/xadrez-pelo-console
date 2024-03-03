using System;
using System.Drawing;
using tabuleiro;
using xadrez;

namespace tabuleiro
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(8 - i + " ");
                Console.ResetColor();
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  a b c d e f g h");
            Console.ResetColor();

        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {


                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(8 - i + " ");
                Console.ResetColor();
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    ImprimirPeca(tabuleiro.Peca(i, j));
                }
                Console.WriteLine();
                Console.ResetColor();  
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  a b c d e f g h");
            Console.ResetColor();

        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + " ");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {


                if (peca.Cor == Cor.Azul)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(peca);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                    Console.ResetColor();
                }
            Console.Write(" ");
            }
        }
    }
}
