using System;
using System.Collections.Generic;

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
        static string text = "";

        static Random random = new Random();
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Gerador de Senhas";
            Print(text = "Bem Vindo ao Gerador de Senhas!");
            
            while (continuar)
            {
                Console.WriteLine("\n[1] - Gerar uma senha nova");
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

        static void Print(string text)
        {
            int width = Console.WindowWidth;
            int x = (width - text.Length) / 2;
            Console.SetCursorPosition(x, 1);
            Console.WriteLine(text);
        }
        static void Senha()
        {
            bool qntdValida = false;
            bool descricaoValida = false;
            int qntdCaracter = 0;
            string descricao = "";
            Console.Clear();

            Print(text = "Gerar uma nova Senha\n");
            while (!qntdValida)
            {
                Console.WriteLine("Deseja incluir quantos caracteres em sua senha? (Max: 32)");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Você deve inserir um valor para a quantidade de caracter!");
                }
                else if (!int.TryParse(input, out qntdCaracter))
                {
                    Console.WriteLine("Digite um número inteiro válido");
                }
                else if (qntdCaracter > 32 || qntdCaracter <= 0)
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
            Console.WriteLine("Deseja incluir letras minusculas? (S/N)");
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

            Console.WriteLine($"Descrição: {descricao} \nSenha gerada: {senhaGerada}");

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

            Print(text = "Consultar senha\n");
            Console.WriteLine("Digite a descrição da senha que deseja consultar");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine();

            foreach (var senha in senhasSalvas)
            {
                if (senha.Key.IndexOf(descricao, StringComparison.OrdinalIgnoreCase) >=0)
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
            bool descricaoValida = false;
            Console.Clear();
            Print(text = "Editar uma senha\n");
            Console.WriteLine("Digite a descrição da senha que deseja editar");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine();

            if (senhasSalvas.ContainsKey(descricao))
            {
                int qntdCaracter = 0;
                bool qntdValida = false;
                string novaDescricao = "";
                Console.Clear();
                Console.WriteLine($"Descrição: {descricao}, Senha: {senhasSalvas[descricao]}");
                while (!descricaoValida)
                {
                    Console.WriteLine("Digite a nova descrição (ou pressione Enter para manter a mesma)");
                    Console.Write("Descrição:");
                    novaDescricao = Console.ReadLine();
                    if (senhasSalvas.ContainsKey(novaDescricao))
                    {
                        Console.WriteLine("Já existe uma senha com essa descrição!");
                    }
                    else
                    {
                        descricaoValida = true;
                    }
                }
                
                while (!qntdValida)
                {
                    Console.WriteLine("Deseja incluir quantos caracteres em sua senha? (Max: 32)");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Você deve inserir um valor para a quantidade de caracter!");
                    }
                    else if (!int.TryParse(input, out qntdCaracter))
                    {
                        Console.WriteLine("Digite um número inteiro válido");
                    }
                    else if (qntdCaracter > 32 || qntdCaracter <= 0)
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

                string novaSenha = GerarSenha(qntdCaracter, incluirLetrasMaiusculas, incluirLetrasMinusculas, incluirNumeros, incluirCaracteresEspeciais);

                if (string.IsNullOrEmpty(novaDescricao))
                {
                    senhasSalvas[descricao] = novaSenha;
                    Console.WriteLine($"Descrição: {descricao} \nSenha atualizada: {novaSenha}");
                }
                else
                {
                    senhasSalvas.Remove(descricao);
                    senhasSalvas.Add(novaDescricao, novaSenha);
                    Console.WriteLine($"Descrição: {novaDescricao} \nSenha atualizada: {novaSenha}");
                }
                
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
            Print(text = "Excluir uma senha\n");
            Console.WriteLine("Digite a descrição da senha que deseja excluir");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine();

            if (senhasSalvas.ContainsKey(descricao))
            {
                Console.WriteLine($"Descrição: {descricao} e Senha: {senhasSalvas[descricao]} foi removida!");
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

