using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Collections;
using System.Runtime.InteropServices;

namespace Challenge4
{
    class Program
    {
        const string fileName = "integers";
        //libreria utilizada para forzar a liberar memoria 
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        //metodo utilizado para forzar la liberación de memoria
        public static void alzheimer()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, 1024);
        }

        static void Main(string[] args)
        {
            int entero = 0, buscado = 0, pruebas, cont = 0;
            long pos = 0;
            string linea = "";
            linea = Console.ReadLine();
            int.TryParse(linea, out pruebas);
            BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));
            long length = reader.BaseStream.Length;
            long cantidad = length / 4;
            int k = 0;
            pos = cantidad;
            ArrayList enter;
            ArrayList menores = new ArrayList();
            ArrayList mayores = new ArrayList();

            for (int j = 0; j < pruebas; j++)
            {
                buscado = 0;
                linea = Console.ReadLine();
                int.TryParse(linea, out buscado);
                if (buscado <= 50)
                {
                    if (menores.Count < buscado)
                    {
                        long valorIni = 0;
                        long valorFin = 0;
                        int ant;
                        for (int i = 0; i < 1074; i++)
                        {
                            if (menores.Count < buscado)
                            {
                                k = 0;
                                valorIni = valorFin;
                                valorFin = (valorFin + 100000);

                                enter = new ArrayList();

                                entero = 0;
                                ant = 0;
                                reader.BaseStream.Seek(0, 0);
                                while (k < pos)
                                {
                                    entero = reader.ReadInt32();
                                    if (entero >= valorIni && entero < valorFin)
                                    {
                                        enter.Add(entero);
                                        ant++;
                                        if (ant == 20000)
                                        {
                                            alzheimer();
                                            ant = 0;
                                        }
                                    }
                                    k++;
                                }
                                k = 0;
                                //Console.WriteLine("escritos: " + enter.Count);
                                enter.Sort();
                                //Console.WriteLine("ordenado");
                                ant = 0;
                                valorIni--;
                                foreach (int ent in enter)
                                {
                                    if ((valorIni + 1) != ent)
                                    {
                                        cont++;
                                        valorIni++;
                                        menores.Add(valorIni);
                                        //Console.WriteLine("Perdido nº " + cont + ": " + valorIni);
                                    }
                                    valorIni = ent;
                                }
                                enter = null;
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                            }
                        }

                    }

                    Console.WriteLine(menores[buscado - 1]);
                }
                    //mayor de 50
                else
                {
                    if (mayores.Count > (100 - buscado))
                    {
                        Console.WriteLine(mayores[100 - buscado]);
                    }
                    else
                    {
                        if (mayores.Count < buscado)
                        {
                            long valorIni = 2147483548;
                            long valorFin = 0;
                            int ant;
                            for (int i = 0; i < 1074; i++)
                            {
                                if (mayores.Count < (100 - buscado))
                                {
                                    k = 0;
                                    valorFin = valorIni;
                                    valorIni = (valorIni - 100000);
                                    enter = new ArrayList();
                                    entero = 0;
                                    ant = 0;
                                    reader.BaseStream.Seek(0, 0);
                                    while (k < pos)
                                    {
                                        entero = reader.ReadInt32();
                                        if (entero >= valorIni && entero < valorFin)
                                        {
                                            enter.Add(entero);
                                            ant++;
                                            if (ant == 20000)
                                            {
                                                alzheimer();
                                                ant = 0;
                                            }
                                        }
                                        k++;
                                    }
                                    k = 0;
                                    //Console.WriteLine("escritos: " + enter.Count);
                                    enter.Sort();
                                    //Console.WriteLine("ordenado");
                                    ant = 0;
                                    valorIni--;
                                    foreach (int ent in enter)
                                    {
                                        if ((valorIni + 1) != ent)
                                        {
                                            cont++;
                                            valorIni++;
                                            mayores.Add(valorIni);
                                            //Console.WriteLine("Perdido nº " + cont + ": " + valorIni);
                                        }
                                        valorIni = ent;
                                    }
                                    enter = null;
                                    GC.Collect();
                                    GC.WaitForPendingFinalizers();
                                    SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                                }
                            }
                        }
                        mayores.Reverse();
                        Console.WriteLine(mayores[100-buscado]);
                    }
                   // Console.ReadLine();
                }
            }
        }
    }
}


