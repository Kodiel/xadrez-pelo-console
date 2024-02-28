using System;
using xadrez;
using tabuleiro;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez('a', 1);

            Console.WriteLine(pos.ToPosicao());

            Console.ReadLine();
        }
    }
}