using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GeradorDeSenhas
{
    internal class Senha
    {

        public void VerificacaoSenha()
        {
            GeradorDeSenha gerarSenha = new GeradorDeSenha();
            Print print = new Print();

            bool qntdValida = false;
            bool descricaoValida = false;
            int qntdCaracter = 0;
            string descricao = "";
            Console.Clear();

            print.PrintConsole("Gerar uma nova Senha\n");
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
                descricao = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(descricao))
                {
                    Console.WriteLine("Digite uma descrição válida!");
                }
                else if (SenhasSalvasDB.SenhasSalvas.ContainsKey(descricao))
                {
                    Console.WriteLine("Já existe uma senha com essa descrição!");
                }
                else
                {
                    descricaoValida = true;
                }
            }

            string senhaGerada = gerarSenha.GerarSenha(qntdCaracter, incluirLetrasMaiusculas, incluirLetrasMinusculas, incluirNumeros, incluirCaracteresEspeciais);

            SenhasSalvasDB.SenhasSalvas.Add(descricao, senhaGerada);

            Console.Clear();
            print.PrintConsole("Gerar uma nova Senha\n");
            Console.WriteLine($"Descrição: {descricao} \nSenha gerada: {senhaGerada}");

        }
    }
}
