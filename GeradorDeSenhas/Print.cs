using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeSenhas
{
    internal class Print
    {
        public void PrintConsole(string text)
        {
            int width = Console.WindowWidth;
            int x = (width - text.Length) / 2;
            Console.SetCursorPosition(x, 1);
            Console.WriteLine(text);

            /*
             * width - text.Length: Calcula o espaço total disponível à esquerda e à direita do texto.
             Dividindo por 2, você obtém a quantidade de espaços que devem estar à esquerda do texto para centralizá-lo.
             */
        }
    }
}
