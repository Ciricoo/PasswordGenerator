using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GeradorDeSenhas
{
    internal class Excluir
    {
        public void ExcluirSenha()
        {
            Print print = new Print();

            Console.Clear();
            print.PrintConsole("Excluir uma senha\n");
            Console.WriteLine("Digite a descrição da senha que deseja excluir");
            Console.Write("Descrição:");
            string descricao = Console.ReadLine().Trim();

            if (SenhasSalvasDB.SenhasSalvas.ContainsKey(descricao))
            {
                Console.WriteLine($"Descrição: {descricao} e Senha: {SenhasSalvasDB.SenhasSalvas[descricao]} foi removida!");
                SenhasSalvasDB.SenhasSalvas.Remove(descricao);
            }
            else if (SenhasSalvasDB.SenhasSalvas.Count == 0)
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
