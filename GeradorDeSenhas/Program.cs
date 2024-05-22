using System;

namespace GeradorDeSenhas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Senha senha = new Senha();
            Consultar consultar = new Consultar();
            Excluir excluir = new Excluir();
            Editar editar = new Editar();
            Print print = new Print();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Gerador de Senhas";
            print.PrintConsole("Bem Vindo ao Gerador de Senhas!");
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\n[1] - Gerar uma senha nova");
                Console.WriteLine("[2] - Consultar uma senha");
                Console.WriteLine("[3] - Editar uma senha");
                Console.WriteLine("[4] - Excluir uma senha");
                Console.WriteLine("[5] - Sair");
                Console.Write("Opção: ");
                string opcaoMenu = Console.ReadLine().Trim();

                switch (opcaoMenu)
                {
                    case "1":
                        senha.VerificacaoSenha();
                        break;
                    case "2":
                        consultar.ConsultarSenha();
                        break;
                    case "3":
                        editar.EditarSenha();
                        break;
                    case "4":
                        excluir.ExcluirSenha();
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
    }
}

