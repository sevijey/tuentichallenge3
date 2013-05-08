using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Challenge3
{

class Program
    {

static void Main(string[] args)
        {
            int scripts;
            char car;
            string linea = "";
            string frase="";
            string cad1="";
            string cad2="";
            char c1, c2='.',c3='.';
            string resultado = "";
            string resultadoFrases = "";
            string resultadoFinal = "";
            string lineaOrdenada = "";
            int cont=0;
            bool dentro=false;
            ArrayList numers = new ArrayList();
            ArrayList fraseshijas = new ArrayList();
            Dictionary<int, string> crono = new Dictionary<int, string>(); 
            linea = Console.ReadLine();
            int.TryParse(linea, out scripts);
            for (int i = 0; i < scripts; i++)
            {
                //redimensiono el tamaño de bytes de entrada de la consola de windows
                //Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                byte[] bytes = new byte[32767];
                Stream inputStream = Console.OpenStandardInput(bytes.Length);
                Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, bytes.Length)); 
                
                linea = Console.ReadLine();
                //presupongo empieza en punto
                car = linea[0];
                //CREO diccionario cronologico
                for (int j = 1; j < linea.Length; j++)
                {
                    if (linea[j].CompareTo('.') == 0 || linea[j].CompareTo('<') == 0 || linea[j].CompareTo('>') == 0)
                    {
                        if (car.CompareTo('.') == 0)
                        {
                            foreach (var pair in crono)
                            {
                                if (frase.CompareTo(pair.Value) == 0)
                                {
                                    resultado = "invalid";
                                    //Console.WriteLine(resultado);
                                    Console.WriteLine("resultado :" + resultado + "ITER" + cont);
                                }
                            }
                            if (resultado.CompareTo("invalid") != 0)
                            {
                                cont++;
                                crono.Add(cont, frase);
                            }
                        }
                        else
                        {
                            fraseshijas.Add(frase);
                        }
                        car = linea[j];
                        frase = "";
                    }
                    else
                    {
                        frase += linea[j];
                    }                    
                }
                if (car.CompareTo('.') == 0)
                {
                    foreach (var pair in crono)
                    {
                        if (frase.CompareTo(pair.Value) == 0)
                        {
                            resultado = "invalid";
                            //Console.WriteLine("resultado AL SALIR DEL FOR:" + resultado);
                        }
                    }
                    if (resultado.CompareTo("invalid") != 0)
                    {
                        cont++;
                        crono.Add(cont, frase);
                    }
                }
                else
                {
                    fraseshijas.Add(frase);
                }
                frase = "";
                //MUESTRO CRONO
                //foreach (var pair in crono)
                //{
                //    Console.WriteLine(pair.Key + " - " + pair.Value);
                //}
                //Console.WriteLine("resultado:" + resultado);

                cont = 0;
                //resultado = "";
                //FIN creo diccionario cronologico

                //EMPIEZO A COLOCAR 
                c1 = '.';
                for (int j = 1; j < linea.Length; j++)
                {
                    if (linea[j].CompareTo('.') == 0 || linea[j].CompareTo('<') == 0 || linea[j].CompareTo('>') == 0)
                    {
                        if (cont == 0)
                        {
                            cad1 = frase;
                            c2 = linea[j];
                            cont++;
                        }
                        else
                        {
                            cad2 = frase;
                            c3 = linea[j];
                            cont++;
                        }
                        frase = "";
                    }
                    else
                    {
                        frase += linea[j];
                    }
                    if (cont == 2)
                    {
                        if (c2 == '.')
                        {
                            lineaOrdenada += c1+cad1;
                            c2 = c3;
                            cad1 = cad2;
                            cont = 1;
                        }
                        else if (c2 == '>')
                        {
                            lineaOrdenada += c1 + cad1 + c2 + cad2;
                            c1 = c3;
                            c2 = '.';
                            cad1 = "";
                            cad2 = "";
                            cont = 0;
                        }
                        else if (c2 == '<')
                        {
                            lineaOrdenada += c2 + cad2 + c1 + cad1;
                            c1 = c3;
                            c2 = '.';
                            cad1 = "";
                            cad2 = "";
                            cont = 0;
                        }
                    }
                }//fin for linea

                if (c2 == '.')
                {
                    if (cad1.CompareTo("") == 0)
                    {
                        lineaOrdenada += '.' + frase;
                    }
                    else
                    {
                        lineaOrdenada += '.' + cad1;
                    }                    
                }
                else if (c2 == '>')
                {
                    lineaOrdenada += c1 + cad1 + '>' + frase;
                }
                else if (c2 == '<')
                {
                    lineaOrdenada += '<' + frase + c1 + cad1;
                 }

                //lineaOrdenada += c3 + frase;
               // Console.WriteLine(lineaOrdenada);
                frase = "";
                cad1 = "";
                cad2 = "";
                //c1 = '.';
                c2 = '.';
                c3 = '.';
                //COMPROBANDO CON ORDEN CRONOLOGICO
                //presupongo empieza en punto
                //car = linea[0];
                for (int j = 1; j < lineaOrdenada.Length; j++)
                {
                    if (lineaOrdenada[j].CompareTo('.') == 0 || lineaOrdenada[j].CompareTo('<') == 0 || lineaOrdenada[j].CompareTo('>') == 0)
                    {
                        foreach (var pair in crono)
                        {
                            if (frase.CompareTo(pair.Value) == 0)
                            {
                                //Console.WriteLine("pasa");
                                resultadoFrases += pair.Key.ToString() + frase + ",";
                                dentro = true;
                            }                                                          
                        }
                        if (!dentro)
                        {
                            resultadoFrases += frase + ",";
                        }
                        dentro = false;
                        frase = "";
                    }
                    else
                    {
                        frase += lineaOrdenada[j];
                    }

                }//Fin for lineaOrdenada
                foreach (var pair in crono)
                {
                    if (frase.CompareTo(pair.Value) == 0)
                    {
                        resultadoFrases += pair.Key.ToString() + frase + ",";
                        dentro = true;
                    }
                }
                if (!dentro)
                {
                    resultadoFrases += frase;
                }
                dentro = false;
                frase = "";

                //ultimas comprobaciones
                for (int j = 0; j < resultadoFrases.Length; j++)
                {
                    int num;
                    if (int.TryParse(resultadoFrases[j].ToString(), out num))
                    {
                        numers.Add(num);
                    }
                    else
                    {
                        if (j == resultadoFrases.Length - 1)
                        {
                            if (resultadoFrases[j].CompareTo(',') != 0)
                            {
                                resultadoFinal += resultadoFrases[j];
                            }
                        }
                        else
                        {
                            resultadoFinal += resultadoFrases[j];
                        }
                    }
                }
                //orden cronolo
                int num1 = 0;
                foreach (int num in numers)
                {
                    if (num < num1)
                    {
                        resultado = "invalid";
                    }
                    num1 = num;
                }
                //numeros duplicados
                num1 = 0;
                numers.Sort();
                foreach (int num in numers)
                {
                    if (num == num1)
                    {
                        resultado = "invalid";
                    }
                    num1 = num;
                }

                //2 frases iguales
                string frahija="";
                fraseshijas.Sort();
                foreach (string fra in fraseshijas)
                {
                    if (frahija.CompareTo(fra) == 0)
                    {
                        if (resultado.CompareTo("invalid") != 0)
                        {
                            resultado = "valid";
                        }
                    }
                    frahija = fra;
                }
                



                if (resultado.CompareTo("")==0)
                {
                    resultado = resultadoFinal;
                }
                Console.WriteLine(resultado);

                lineaOrdenada = "";
                resultadoFrases = "";
                resultadoFinal = "";
                resultado = "";
                crono = new Dictionary<int, string>();
                numers = new ArrayList();
                fraseshijas = new ArrayList();
                }//FIN FOR DE SCRIPTS
           // Console.WriteLine(resultadoFrases);
            Console.ReadLine();


 }

    }
}

