using System;

namespace JogoDaVelha
{
    class Jogador
    {
        public string Nome { get; }
        public char Simbolo { get; set; }
        public int Pontuacao { get; set; }

        public Jogador(string nome)
        {
            Nome = nome;
            Pontuacao = 0;
        }
    }

    class JogoVelha
    {
        private char[,] tabuleiro = new char[3, 3];
        private Jogador jogador1, jogador2;
        private Jogador jogadorAtual;

        public JogoVelha(string nomeJogador1, char simboloJogador1)
        {
            jogador1 = new Jogador(nomeJogador1);

            string[] nomesJogador2 = { "Alice", "Juan", "Carol", "Otavio", "Godofredo" };
            int indiceAleatorio = new Random().Next(nomesJogador2.Length);
            string nomeJogador2 = nomesJogador2[indiceAleatorio];

            jogador2 = new Jogador(nomeJogador2);

            jogador1.Simbolo = simboloJogador1;

            if (simboloJogador1 == 'X')
            {
                jogador2.Simbolo = 'O';
            }
            else
            {
                jogador2.Simbolo = 'X';
            }

            jogadorAtual = jogador1;

            InicializarTabuleiro();
        }

        private void InicializarTabuleiro()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tabuleiro[i, j] = ' ';
                }
            }
        }

        public bool FazerJogada(int linha, int coluna)
        {
            if (linha < 0 || linha >= 3 || coluna < 0 || coluna >= 3 || tabuleiro[linha, coluna] != ' ')
            {
                return false;
            }

            tabuleiro[linha, coluna] = jogadorAtual.Simbolo;
            return true;
        }

        public void ExibirTabuleiro()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(tabuleiro[i, j]);
                    if (j < 2) Console.Write(" | ");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("---------");
            }
        }

        private bool VerificarVitoria(char simbolo)
        {
            for (int i = 0; i < 3; i++)
            {
                if (tabuleiro[i, 0] == simbolo && tabuleiro[i, 1] == simbolo && tabuleiro[i, 2] == simbolo)
                    return true;
            }

            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[0, j] == simbolo && tabuleiro[1, j] == simbolo && tabuleiro[2, j] == simbolo)
                    return true;
            }

            if ((tabuleiro[0, 0] == simbolo && tabuleiro[1, 1] == simbolo && tabuleiro[2, 2] == simbolo) ||
                (tabuleiro[0, 2] == simbolo && tabuleiro[1, 1] == simbolo && tabuleiro[2, 0] == simbolo))
                return true;

            return false;
        }

        private bool VerificarEmpate()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                        return false;
                }
            }

            return true;
        }

        public void Jogar()
        {
            while (true)
            {
                Console.Clear();
                ExibirTabuleiro();

                int linha, coluna;

                if (jogadorAtual == jogador1)
                {
                    Console.WriteLine($"Vez de {jogador1.Nome} ({jogador1.Simbolo})");
                    Console.Write("Informe a linha (0-2): ");
                    linha = int.Parse(Console.ReadLine());
                    Console.Write("Informe a coluna (0-2): ");
                    coluna = int.Parse(Console.ReadLine());

                    while (!FazerJogada(linha, coluna))
                    {
                        Console.WriteLine("Jogada inválida. Tente novamente.");
                        Console.Write("Informe a linha (0-2): ");
                        linha = int.Parse(Console.ReadLine());
                        Console.Write("Informe a coluna (0-2): ");
                        coluna = int.Parse(Console.ReadLine());
                    }
                }
                else
                {
                    Console.WriteLine($"Vez de {jogador2.Nome} ({jogador2.Simbolo})");

                    do
                    {
                        linha = new Random().Next(3);
                        coluna = new Random().Next(3);
                    } while (!FazerJogada(linha, coluna));
                }

                if (VerificarVitoria(jogadorAtual.Simbolo))
                {
                    Console.Clear();
                    ExibirTabuleiro();
                    Console.WriteLine($"{jogadorAtual.Nome} venceu!");
                    jogadorAtual.Pontuacao++;
                    break;
                }
                else if (VerificarEmpate())
                {
                    Console.Clear();
                    ExibirTabuleiro();
                    Console.WriteLine("Empate!");
                    break;
                }

                jogadorAtual = (jogadorAtual == jogador1) ? jogador2 : jogador1;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Jogo da Velha");
            Console.Write("Informe o nome do jogador físico: ");
            string nomeJogadorFisico = Console.ReadLine();
            Console.Write("Informe o símbolo para o jogador físico (X ou O): ");
            char simboloJogadorFisico = char.Parse(Console.ReadLine());

            JogoVelha jogo = new JogoVelha(nomeJogadorFisico, simboloJogadorFisico);
            jogo.Jogar();
        }
    }
}

