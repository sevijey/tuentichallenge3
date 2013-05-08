using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Challenge2
{
    class Program
    {
        static void Main(string[] args)
        {
            int palabras;
            string linea="";
            string salida = "";
            ArrayList sal = new ArrayList();
            string line;
            int cont = 0;
            for (int i = 0; i < 4; i++)
            {
                linea = Console.ReadLine();
            }
            int.TryParse(linea, out palabras);
            linea = Console.ReadLine();
            for (int i = 0; i < palabras; i++)
            {
                linea = Console.ReadLine();                
                // Read the file and display it line by line.
                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"C:\Users\Jose\Desktop\dist\smallDictionary");
                while ((line = file.ReadLine()) != null)
                {
                    //compruebo que la palabra tiene el mismo tamaño
                    if (linea.Length == line.Length)
                    {
                        //compruebo que no sea la misma palabra
                        if(linea.CompareTo(line)!=0)
                        {
                            char[] cadena1 = linea.ToCharArray();
                            //ordeno la primera palabra a comparar
                            Array.Sort(cadena1);
                            char[] cadena2 = line.ToCharArray();
                            //ordeno la segunda palabra a comparar
                            Array.Sort(cadena2);
                            //compruebo que tengan las mismas letras
                            for (int j = 0; j < linea.Length; j++)
                            {
                                if (cadena1[j] == cadena2[j])
                                {
                                    cont++;
                                }
                            }
                            //si tienen las mismas letras las agrego a un ArrayList
                            if (cont == linea.Length)
                            {
                                sal.Add(line);
                            }                                
                            cont = 0;
                        }
                    }                    
                }
                file.Close();
                //ordeno el array
                sal.Sort();
                foreach (string sale in sal)
                {
                    salida += " " + sale;
                }
                //las muestro por pantalla
                Console.WriteLine(linea + " ->" + salida);
                salida = "";
                sal=new ArrayList();
            }
        }
    }
}
