using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeSenhas
{
    internal class Program
    {
        static string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
        static string letrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static string numeros = "1234567890";
        static string caracteresEspeciais = "!@#$%^&?";
        static bool continuar = true;
        static Dictionary<string, string> senhasSalvas = new Dictionary<string, string>();

        static Random random = new Random();

        static void Main(string[] args)
        {
            while (continuar)
            {
                Console.WriteLine("--------- Bem Vindo ao Gerado de Senhas ---------");
                Console.WriteLine("[1] - Gerar uma senha nova");
                Console.WriteLine("[2] - Consultar uma senha");
                Console.WriteLine("[3] - Editar uma senha");
                Console.WriteLine("[4] - Excluir uma senha");
                Console.WriteLine("[5] - Sair");
                Console.Write("Opção: ");
                int opcaoMenu = int.Parse(Console.ReadLine());

                switch (opcaoMenu)
                {

                    case 1:
                        Senha();
                        break;
                    case 2:
                        ConsultarSenha();
                        break;
                    case 3:
                        EditarSenha();
                        break;
                    case 4:
                        ExcluirSenha();
                        break;
                    case 5:
                        Console.WriteLine("Gerador de Senha finalizado!");
                        break;
                    default:
                        Console.WriteLine("Digite uma opção válida!");
                        break;
                }
            }
        }

        static void Senha()
        {   
            bool qntdValida = false;
            int qntdCaracter = 0;
            Console.Clear();
            
                Console.WriteLine("--------- Gerar uma Senha Nova ---------");
                while (!qntdValida)
                {
                    Console.WriteLine("Deseja incluir quantos caracteres em sua senha? (Max: 32)");
                    qntdCaracter = int.Parse(Console.ReadLine());
                    if (qntdCaracter > 32 || qntdCaracter <= 0)
                    {
                        Console.WriteLine("A quantidade de caracteres deve ser entre 1 e 32.");
                    }
                    else
                    {
                        qntdValida = true;
                    }
                }
                Console.WriteLine("Deseja incluir letras maiúsculas? (S/N)");
                bool incluirLetrasMaiusculas = Console.ReadLine().ToUpper() == "S";
                Console.WriteLine("Deseja incluir números? (S/N)");
                bool incluirNumeros = Console.ReadLine().ToUpper() == "S";
                Console.WriteLine("Deseja incluir caracteres especiais? (S/N)");
                bool incluirCaracteresEspeciais = Console.ReadLine().ToUpper() == "S";
                Console.WriteLine("Adicione uma descrição para a sua senha:");
                string descricao = Console.ReadLine();

            string senhaGerada = GerarSenha(qntdCaracter, incluirLetrasMaiusculas, incluirNumeros, incluirCaracteresEspeciais);

            senhasSalvas.Add(descricao, senhaGerada);

            Console.WriteLine($"Senha gerada: {senhaGerada}");
            


        }

        static string GerarSenha(int qntdCaracter, bool incluirLetrasMaiusculas, bool incluirNumeros, bool incluirCaracteresEspeciais)
        {
            List<char> caracteresDisponiveis = new List<char>();
            caracteresDisponiveis.AddRange(letrasMinusculas);

            if (incluirLetrasMaiusculas)
            {
                caracteresDisponiveis.AddRange(letrasMaiusculas);
            }

            if (incluirNumeros)
            {
                caracteresDisponiveis.AddRange(numeros);
            }

            if (incluirCaracteresEspeciais)
            {
                caracteresDisponiveis.AddRange(caracteresEspeciais);
            }

            string senhaGerada = "";

            for (int i = 0; i < qntdCaracter; i++)
            {
                int index = random.Next(0, caracteresDisponiveis.Count);
                senhaGerada += caracteresDisponiveis[index];
            }
            Console.Clear();

            return senhaGerada;

        }

        static void ConsultarSenha()
        {
            Console.Clear();

            Console.Write("Digite a descrição da senha que deseja consultar: ");
            string descricao = Console.ReadLine();

            if (senhasSalvas.ContainsKey(descricao))
            {
                Console.WriteLine($"Senha encontrada: {senhasSalvas[descricao]}\n");
            }
            else
            {
                Console.WriteLine("Senha não encontrada!\n");
            }
        }

        static void EditarSenha()
        {

        }

        static void ExcluirSenha()
        {

        }

    }
}
