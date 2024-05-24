using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GeradorDeSenhas
{
    public class Consultar
    {
        public void ConsultarSenha()
        {
            Print print = new Print();
            bool encontrou = false;
            Console.Clear();

            print.PrintConsole("Consultar senha\n");
            Console.WriteLine("Digite a descrição da senha que deseja consultar");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine().Trim();

            if (SenhasSalvasDB.SenhasSalvas.ContainsKey(descricao))
            {
                Console.WriteLine($"Descrição: {descricao}, Senha: {SenhasSalvasDB.SenhasSalvas[descricao]}");
                encontrou = true;
            }
            else if (string.IsNullOrEmpty(descricao))
            {
                Console.WriteLine("Não existe descrição vazia!");
                return;
            }
            else
            {
                foreach (var senha in SenhasSalvasDB.SenhasSalvas)
                {
                    if (senha.Key.IndexOf(descricao, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Console.WriteLine($"Descrição: {senha.Key}, Senha: {senha.Value}");
                        encontrou = true;
                    }
                }
            }
            if (!encontrou)
            {
                Console.WriteLine($"Senha não encontrada!");
            }
        }
    }
}
