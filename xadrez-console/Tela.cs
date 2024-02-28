using System;
using tabuleiro;

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
                    if (tabuleiro.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tabuleiro.Peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  a b c d e f g h");
            Console.ResetColor();

        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Azul)
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
        }
    }
}
