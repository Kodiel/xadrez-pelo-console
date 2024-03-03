using System;
using xadrez;
using tabuleiro;
using xadrez_console.xadrez;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tabuleiro);

                    Console.WriteLine();
                    Console.Write("Origem:");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                    bool[,] posicoesPossiveis = partida.tabuleiro.Peca(origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tabuleiro, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino:");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ExecutaMovimento(origem, destino);
                }
            }
            catch (TabuleiroException tab)
            {
                Console.WriteLine(tab.Message);
            }

            Console.ReadLine();
        }
    }
}