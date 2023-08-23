using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*******************
  Autores:
    Gabriela Lima
    Luana Malagodi
*********************/

namespace JogoDaVelha
{
    class Jogador
    {
        public string Nome { get; }
        public string Simbolo { get; set; }
        public int Pontuacao { get; set; }

        public Jogador(string nome)
        {
            Nome = nome;
            Pontuacao = 0;
        }
    }

    class JogoVelha
    {
        private string[,] tabuleiro = new string[3, 3];
        private Jogador jogador1, jogador2;
        private Jogador jogadorAtual;

        public JogoVelha(string nomeJogador1, string simboloJogador1)
        {
            jogador1 = new Jogador(nomeJogador1);

            string[] nomesJogador2 = { "Alice", "Juan", "Carol", "Otavio", "Godofredo" };
            int indiceAleatorio = new Random().Next(nomesJogador2.Length);
            string nomeJogador2 = nomesJogador2[indiceAleatorio];

            jogador2 = new Jogador(nomeJogador2);

            jogador1.Simbolo = simboloJogador1;

            if (simboloJogador1 == "[X]")
            {
                jogador2.Simbolo = "[O]";
            }
            else
            {
                jogador2.Simbolo = "[X]";
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
                    tabuleiro[i, j] = "[ ]";
                }
            }
        }

        public bool FazerJogada(int resposta)
        {
            int linha, coluna;

            if (resposta == 1)
            {
                linha = 0;
                coluna = 0;               
            }
            else if (resposta == 2)
            {
                linha = 0;
                coluna = 1;             
            }
            else if (resposta == 3)
            {
                linha = 0;
                coluna = 2;               
            }
            else if (resposta == 4)
            {
                linha = 1;
                coluna = 0;
            }
            else if (resposta == 5)
            {
                linha = 1;
                coluna = 1;
            }
            else if (resposta == 6)
            {
                linha = 1;
                coluna = 2;
            }
            else if (resposta == 7)
            {
                linha = 2;
                coluna = 0;
            }
            else if (resposta == 8)
            {
                linha = 2;
                coluna = 1;
            }
            else if (resposta == 9)
            {
                linha = 2;
                coluna = 2;
            } else
            {
                linha = 0;
                coluna = 0;
            }

            if (linha < 0 || linha >= 3 || coluna < 0 || coluna >= 3 || tabuleiro[linha, coluna] != "[ ]")
            {
                return false;
            }

            tabuleiro[linha, coluna] = jogadorAtual.Simbolo;
            return true;
        }

        public void ExibirTabuleiro()
        {
            for (int i = 0; i < 3; i++) //imprimindo a matriz
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(tabuleiro[i, j]);
                }
                Console.WriteLine();
            }
        }

        private bool VerificarVitoria(string simbolo)
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
                    if (tabuleiro[i, j] == "[ ]")
                        return false;
                }
            }

            return true;
        }

        public void Jogar()
        {
            while (true)
            {

                ExibirTabuleiro();

                int resposta;                             

                if (jogadorAtual == jogador1)
                {
                    Console.WriteLine($"Sua vez, {jogador1.Nome} {jogador1.Simbolo}.");
                    Console.Write("Informe a posição (1-9): ");
                    resposta = int.Parse(Console.ReadLine());
                    while (!FazerJogada(resposta))
                    {
                        Console.WriteLine("Jogada inválida. Tente novamente.");
                        Console.Write("Informe a posição (1-9): ");
                        resposta = int.Parse(Console.ReadLine());
                    }
                }
                else
                {
                    Console.WriteLine($"Vez de {jogador2.Nome} (bot) {jogador2.Simbolo}");
                    do
                    {
                        resposta = new Random().Next(10);                        
                    } while (!FazerJogada(resposta));
                }

                if (VerificarVitoria(jogadorAtual.Simbolo))
                {
                    
                    ExibirTabuleiro();
                    Console.WriteLine($"Parabéns para {jogadorAtual.Nome} que venceu o Jogo da Velha!");
                    Console.ReadLine();
                    jogadorAtual.Pontuacao++;
                    break;
                }
                else if (VerificarEmpate())
                {
                    
                    ExibirTabuleiro();
                    Console.WriteLine("Deu Velha! O jogo acabou em um empate.");
                    Console.ReadLine();
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
            string simbolo;           

            Console.WriteLine("Seja bem-vindo(a) ao Jogo da Velha.");
            Console.Write("Informe o nome de quem vai jogar: ");
            string nomeJogadorFisico = Console.ReadLine();
            Console.Write("Informe o símbolo em que deseja jogar (X ou O) em maiúsculo: ");
            simbolo = Console.ReadLine();
            string simboloJogadorFisico = $"[{simbolo}]";
            
            Console.WriteLine("");
            Console.WriteLine("Digite a posição (1-9) que deseja marcar.");
            Console.WriteLine("Sendo elas:\n 1 | 2 | 3");
            Console.WriteLine("-----------");
            Console.WriteLine(" 4 | 5 | 6");
            Console.WriteLine("-----------");
            Console.WriteLine(" 7 | 8 | 9");
            Console.WriteLine("Bom jogo!"); //instruções

            JogoVelha jogo = new JogoVelha(nomeJogadorFisico, simboloJogadorFisico);
            jogo.Jogar();
        }
    }
}
