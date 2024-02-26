using System;
using xadrez_console;
using xadrez_console.tabuleiro;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tabuleiro = new Tabuleiro(8, 8);

            Tela.ImprimirTabuleiro(tabuleiro);

            Console.ReadLine();
        }
    }
}