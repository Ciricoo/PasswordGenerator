using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                var opcaoMenu = Console.ReadLine();

                switch (opcaoMenu)
                {
                    case "1":
                        Senha();
                        break;
                    case "2":
                        ConsultarSenha();
                        break;
                    case "3":
                        EditarSenha();
                        break;
                    case "4":
                        ExcluirSenha();
                        break;
                    case "5":
                        Console.WriteLine("Gerador de Senha finalizado!");
                        continuar = false;
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
            bool descricaoValida = false;
            int qntdCaracter = 0;
            string descricao = "";
            Console.Clear();

            Console.WriteLine("--------- Gerar uma nova Senha ---------");
            while (!qntdValida)
            {
                Console.WriteLine("Deseja incluir quantos caracteres em sua senha? (Max: 32)");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Você deve inserir um valor para a quantidade de caracter!");
                }
                else if (!int.TryParse(input, out qntdCaracter) || qntdCaracter > 32 || qntdCaracter <= 0)
                {
                    Console.WriteLine("A quantidade de caracter deve estar entre 1 e 32.");
                }
                else
                {
                    qntdValida = true;
                }
            }
            Console.WriteLine("Deseja incluir letras maiúsculas? (S/N)");
            bool incluirLetrasMaiusculas = Console.ReadLine().ToUpper() == "S";
            Console.WriteLine("Deseja incluir letras maiúsculas? (S/N)");
            bool incluirLetrasMinusculas = Console.ReadLine().ToUpper() == "S";
            Console.WriteLine("Deseja incluir números? (S/N)");
            bool incluirNumeros = Console.ReadLine().ToUpper() == "S";
            Console.WriteLine("Deseja incluir caracteres especiais? (S/N)");
            bool incluirCaracteresEspeciais = Console.ReadLine().ToUpper() == "S";

            if (!incluirLetrasMaiusculas && !incluirNumeros && !incluirLetrasMinusculas && !incluirCaracteresEspeciais)
            {
                Console.Clear();
                Console.WriteLine("Pelo menos uma opção deve ser selecionada!");
                return;
            }

            while (!descricaoValida)
            {
                Console.WriteLine("Adicione uma descrição para a sua senha:");
                descricao = Console.ReadLine();
                if (string.IsNullOrEmpty(descricao))
                {
                    Console.WriteLine("Digite uma descrição válida!");
                }
                else if (senhasSalvas.ContainsKey(descricao))
                {
                    Console.WriteLine("Já existe uma senha com essa descrição!");
                }
                else
                {
                    descricaoValida = true;
                }
            }

            string senhaGerada = GerarSenha(qntdCaracter, incluirLetrasMaiusculas, incluirLetrasMinusculas, incluirNumeros, incluirCaracteresEspeciais);

            senhasSalvas.Add(descricao, senhaGerada);

            Console.WriteLine($"Senha gerada: {senhaGerada}");

        }

        //GERAR SENHA
        static string GerarSenha(int qntdCaracter, bool incluirLetrasMaiusculas, bool incluirLetrasMinusculas, bool incluirNumeros, bool incluirCaracteresEspeciais)
        {
            List<char> caracteresSenha = new List<char>();


            if (incluirLetrasMaiusculas)
            {
                caracteresSenha.AddRange(letrasMaiusculas);
            }

            if (incluirLetrasMinusculas)
            {
                caracteresSenha.AddRange(letrasMinusculas);
            }

            if (incluirNumeros)
            {
                caracteresSenha.AddRange(numeros);
            }

            if (incluirCaracteresEspeciais)
            {
                caracteresSenha.AddRange(caracteresEspeciais);
            }

            string senhaGerada = "";

            for (int i = 0; i < qntdCaracter; i++)
            {
                int index = random.Next(caracteresSenha.Count);
                senhaGerada += caracteresSenha[index];
            }
            Console.Clear();

            return senhaGerada;

        }

        //CONSULTAR
        static void ConsultarSenha()
        {
            bool encontrou = false;
            Console.Clear();

            Console.WriteLine("Digite a descrição da senha que deseja consultar");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine();

            foreach (var senha in senhasSalvas)
            {
                if (senha.Key.ToLower().Contains(descricao))
                {
                    Console.WriteLine($"Descrição: {senha.Key}, Senha: {senha.Value}");
                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine($"Senha não encontrada!");
            }
        }

        //EDITAR

        static void EditarSenha()
        {
            Console.Clear();
            Console.WriteLine("--------- Editar uma senha ---------");
            Console.WriteLine("Digite a descrição da senha que deseja editar");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine();

            if (senhasSalvas.ContainsKey(descricao))
            {
                int qntdCaracter = 0;
                bool qntdValida = false;
                Console.Clear();
                Console.WriteLine($"Está é a sua senha: {senhasSalvas[descricao]}");
                Console.WriteLine("Digite a nova descrição (ou pressione Enter para manter a mesma)");
                Console.Write("Descrição:");
                string novaDescricao = Console.ReadLine();
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
                Console.WriteLine("Deseja incluir letras maiúsculas? (S/N)");
                bool incluirLetrasMinusculas = Console.ReadLine().ToUpper() == "S";
                Console.WriteLine("Deseja incluir números? (S/N)");
                bool incluirNumeros = Console.ReadLine().ToUpper() == "S";
                Console.WriteLine("Deseja incluir caracteres especiais? (S/N)");
                bool incluirCaracteresEspeciais = Console.ReadLine().ToUpper() == "S";

                if (!incluirLetrasMaiusculas && !incluirNumeros && !incluirLetrasMinusculas && !incluirCaracteresEspeciais)
                {
                    Console.Clear();
                    Console.WriteLine("Pelo menos uma opção deve ser selecionada!");
                    return;
                }

                string novaSenha = GerarSenha(qntdCaracter, incluirLetrasMaiusculas, incluirLetrasMinusculas, incluirNumeros, incluirCaracteresEspeciais);

                if (string.IsNullOrEmpty(novaDescricao))
                {
                    senhasSalvas[descricao] = novaSenha;
                }
                else
                {
                    senhasSalvas.Remove(descricao);
                    senhasSalvas.Add(novaDescricao, novaSenha);
                }
                Console.WriteLine($"Senha atualizada: {novaSenha}");
            }
            else if (senhasSalvas.Count == 0)
            {
                Console.WriteLine("Ainda não foi salvo nenhuma senha! ");
            }
            else
            {
                Console.WriteLine("Senha não encontrada!");
            }
        }

        //EXCLUIR

        static void ExcluirSenha()
        {
            Console.Clear();
            Console.WriteLine("--------- Excluir uma senha ---------");
            Console.WriteLine("Digite a descrição da senha que deseja excluir");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine();

            if (senhasSalvas.ContainsKey(descricao))
            {
                Console.WriteLine($"Senha: {senhasSalvas[descricao]} foi removida!");
                senhasSalvas.Remove(descricao);
            }
            else if (senhasSalvas.Count == 0)
            {
                Console.WriteLine("Ainda não foi salvo nenhuma senha! ");
            }
            else
            {
                Console.WriteLine("Senha não encontrada!\n");
            }

        }
    }
}
