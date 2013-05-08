using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Challenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> valores = new List<int>();
            int pruebas, monedas;
            int aux = 0;
            string val = "";
            string linea = Console.ReadLine();
            int.TryParse(linea, out pruebas);
            for (int i = 0; i < pruebas; i++)
            {
                linea = Console.ReadLine();
                int.TryParse(linea, out monedas);
                linea = Console.ReadLine();
                for (int j = 0; j < linea.Length; j++)
                {
                    if (linea[j].CompareTo(' ') != 0)
                    {
                        val += linea[j];
                    }
                    else
                    {
                        int.TryParse(val, out aux);
                        valores.Add(aux);
                        val = "";
                        aux = 0;
                    }
                }
                int.TryParse(val, out aux);
                valores.Add(aux);
                val = "";
                aux = 0;
              //  for (int k = 0; k < 2; k++)
             //   {
                    int min, max, bit;
                    min = valores.Min();
                    max = valores.Max();
                    bit = (monedas / min);
                    monedas = (monedas % min);
                    monedas += (bit * max);
               // }
                Console.WriteLine(monedas);
                valores = new List<int>();
                monedas = 0;
            }
        }
    }
}