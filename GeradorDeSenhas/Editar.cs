using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GeradorDeSenhas
{
    internal class Editar
    {
        public void EditarSenha()
        {
            Print print = new Print();  
            GeradorDeSenha gerarSenha = new GeradorDeSenha();

            bool descricaoValida = true;
            string novaDescricao;

            Console.Clear();
            print.PrintConsole("Editar uma senha\n");
            Console.WriteLine("Digite a descrição da senha que deseja editar");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine().Trim();

            if (SenhasSalvasDB.SenhasSalvas.ContainsKey(descricao))
            {
                int qntdCaracter = 0;
                bool qntdValida = true;
                bool verifica = false;
                Console.Clear();
                print.PrintConsole("Editar uma senha\n");
                Console.WriteLine($"Descrição: {descricao}, Senha: {SenhasSalvasDB.SenhasSalvas[descricao]}");

                while (qntdValida)
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
                        qntdValida = false;
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

                string novaSenha = gerarSenha.GerarSenha(qntdCaracter, incluirLetrasMaiusculas, incluirLetrasMinusculas, incluirNumeros, incluirCaracteresEspeciais);

                while (descricaoValida)
                {
                    print.PrintConsole("Editar uma senha\n");
                    Console.WriteLine("Deseja adicionar uma nova descrição? (s/n)");
                    verifica = Console.ReadLine().ToUpper() == "S";

                    if (verifica)
                    {
                        Console.Clear();
                        print.PrintConsole("Editar uma senha\n");
                        Console.WriteLine("Digite a nova descrição:");
                        Console.Write("Descrição:");
                        novaDescricao = Console.ReadLine().Trim();
                        if (string.IsNullOrWhiteSpace(novaDescricao))
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("\nDigite uma descrição válida!");
                        }
                        else if (SenhasSalvasDB.SenhasSalvas.ContainsKey(novaDescricao))
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("\n Já existe uma senha com essa descrição!");

                        }
                        else
                        {
                            SenhasSalvasDB.SenhasSalvas.Remove(descricao);
                            SenhasSalvasDB.SenhasSalvas.Add(novaDescricao, novaSenha);
                            Console.WriteLine($"Descrição: {novaDescricao} \nSenha atualizada: {novaSenha}");
                            descricaoValida = false;
                        }
                    }

                    else
                    {
                        Console.WriteLine("Descrição foi mantida");
                        SenhasSalvasDB.SenhasSalvas[descricao] = novaSenha;
                        Console.WriteLine($"Descrição: {descricao} \nSenha atualizada: {novaSenha}");
                        descricaoValida = false;
                    }
                }
            }
            else if (SenhasSalvasDB.SenhasSalvas.Count == 0)
            {
                Console.WriteLine("Ainda não foi salvo nenhuma senha! ");
            }
            else
            {
                Console.WriteLine("Senha não encontrada!");
            }
        }
    }
}
