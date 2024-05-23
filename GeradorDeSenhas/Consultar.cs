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
            var filtroLista = SenhasSalvasDB.SenhasSalvas.Where(senhas => senhas.Key.Contains(descricao));

            if (string.IsNullOrEmpty(descricao))
            {
                Console.WriteLine("Não existe descrição vazia!");
                return;
            }
            else if(SenhasSalvasDB.SenhasSalvas.ContainsKey(descricao))
            {
                Console.WriteLine($"Descrição: {descricao}, Senha: {SenhasSalvasDB.SenhasSalvas[descricao]}");
                encontrou = true;
            }
            else if (filtroLista.Any())
            {
                foreach (var item in filtroLista)
                {
                    Console.WriteLine($"Descrição: {item.Key}, Senha: {item.Value}");
                    encontrou = true;
                }
            }  
            
            if (!encontrou)
            {
                Console.WriteLine($"Senha não encontrada!");
            }
        }
    }
}
