using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeSenhas
{   
    internal class GeradorDeSenha
    {
        public string GerarSenha(int qntdCaracter, bool incluirLetrasMaiusculas, bool incluirLetrasMinusculas, bool incluirNumeros, bool incluirCaracteresEspeciais)
        {
            Random random = new Random();

            string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
            string letrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numeros = "1234567890";
            string caracteresEspeciais = "!@#$%^&?";

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
    }
}
