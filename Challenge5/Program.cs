using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
//problema resuelto utilizando backtracking y seleccion optima

namespace Challenge5
{
    class Program
    {
        public static int M;
        public static int N;
        public static int Z;//segundos(1 movimiento por segundo)
        public static int G;//numero de gemas   
        public static int x, y, xAux, yAux,valorAux;
        public static int pruebas;
        public static string linea;
        public static string datosTablero;
        public static int mejorSolucion = 0;
        public static int movimientos = 4;
		//movimientos posibles
        public static int[] despx = { -1, 1, 0, 0 };
        public static int[] despy = { 0, 0, -1, 1 };

		//metodo recursivo
        static void snake(ref int[,] tablero, int x, int y, int xAnt, int yAnt,ref int segundos,ref int solucion)
        {
                int posibilidad = -1;
                int restado = 0;
                do
                {
                    posibilidad = posibilidad + 1;
                    int nX = x + despx[posibilidad];
                    int nY = y + despy[posibilidad];
                    if (esAceptable(xAnt, yAnt, nX, nY))
                    {
						//anotar
                        solucion += tablero[nX, nY];
                        restado = tablero[nX, nY];
                        tablero[nX, nY] = 0;
                        segundos=segundos+1;
                        if (esSolucion(segundos))
                        {
                             if (solucion > mejorSolucion)
                            {
                                mejorSolucion = solucion;
                            }
                        }
                        else
                        {
							//llamada recursiva
                            snake(ref tablero, nX, nY, x, y,ref segundos,ref solucion);
                        }
						//desanotar
                        solucion -= restado;
                        tablero[nX, nY] = restado;
                        segundos=segundos-1;
                    }
                } while (posibilidad != movimientos -1);
        }

        public static bool esSolucion(int segundos)
        {
            bool resul = false;
            if (segundos == Z)
            {
                resul = true;
            }
            return resul;
        }

        public static bool esAceptable(int xAnt, int yAnt, int nX, int nY)
        {
            bool resul = false;
            if (nX == xAnt && nY == yAnt)
            {
                return resul;
            }
            if ((nX >= 0) && (nX < M ) && (nY >= 0) && (nY < N ))
            {
                resul = true;
            }
            return resul;
        }

        static void Main(string[] args)
        {
            int[,] tablero;
            linea = "";
            datosTablero = "";
            linea = Console.ReadLine();
            int.TryParse(linea, out pruebas);   

            for (int i = 0; i < pruebas; i++)
            {
                //leo tamaño del tablero
                linea = Console.ReadLine();
                while (linea.Length == 0)
                {
                    linea = Console.ReadLine();
                }
                if (linea[1] == ',')
                {
                    int.TryParse(linea[0].ToString(), out M);
                    int.TryParse(linea[2].ToString(), out N);
                }
                else
                {
                    string m = linea[0].ToString() + linea[1].ToString();
                    int.TryParse(m, out M);
                    string n = linea[3].ToString() + linea[4].ToString();
                    int.TryParse(m, out N);
                }
				
                //creo el tablero
                tablero = new int[M, N];
                for (int j = 0; j < M; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        tablero[j, k] = 0;
                    }
                }

                //leo posicion inicial
                linea = Console.ReadLine();
                while (linea.Length == 0)
                {
                    linea = Console.ReadLine();
                }
                if (linea[1] == ',')
                {
                    int.TryParse(linea[0].ToString(), out x);
                    int.TryParse(linea[2].ToString(), out y);
                }
                else
                {
                    string X = linea[0].ToString() + linea[1].ToString();
                    int.TryParse(X, out x);
                    string Y = linea[3].ToString() + linea[4].ToString();
                    int.TryParse(Y, out y);
                }

                //leo el numero de segundos o movimientos
                linea = Console.ReadLine();
                while (linea.Length == 0)
                {
                    linea = Console.ReadLine();
                }
                int.TryParse(linea, out Z);

                //leo el numero de gemas
                linea = Console.ReadLine();
                while (linea.Length == 0)
                {
                    linea = Console.ReadLine();
                }
                int.TryParse(linea, out G);

                //configuro el numero de bytes permitidos a traves de la consola de windows
                //Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                byte[] bytes = new byte[16384];
                Stream inputStream = Console.OpenStandardInput(bytes.Length);
                Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, bytes.Length));

                //leo los datos inciales del tablero
                datosTablero = Console.ReadLine();
                if (datosTablero == null)
                {
                    datosTablero = Console.ReadLine();
                }
                //cargo los datos iniciales en el tablero
                string cad="";
                ArrayList valores = new ArrayList();
                foreach (char c in datosTablero)
                {
                    if (c != '#')
                    {
                        cad += c;
                    }
                    else
                    {
                        valores.Add(cad);
                        cad = "";
                    }                   

                }
                valores.Add(cad);
                string sxAux="";
                string syAux = "";
                string svalorAux="";
                int cont = 0;
                foreach (string cadena in valores)
                {
                    for (int j = 0; j < cadena.Length; j++)
                    {
                        if (cont == 0)
                        {
                            if (cadena[j] != ',')
                            {
                                sxAux += cadena[j];                                
                            }
                            else
                            {
                                cont++;
                            }
                        }
                        else if (cont == 1)
                        {
                            if (cadena[j] != ',')
                            {
                                syAux += cadena[j];                                
                            }
                            else
                            {
                                cont++;
                            }
                        }
                        else
                        {
                            svalorAux += cadena[j];                            
                        }
                    }
                    cont = 0;

                    int.TryParse(sxAux, out xAux);
                    int.TryParse(syAux, out yAux);
                    int.TryParse(svalorAux, out valorAux);
                    tablero[xAux, yAux] = valorAux;

                    sxAux = "";
                    syAux = "";
                    svalorAux = "";
                    xAux = 0;
                    yAux = 0;
                    valorAux = 0;
                }

                //lamo al metodo recursivo
                int sol=0;
                int seg = 0;
                snake(ref tablero, x, y, 0, 0, ref seg, ref sol);
                Console.WriteLine(mejorSolucion);

                mejorSolucion = 0;
                linea = "";
                datosTablero = "";
            }
         //   Console.ReadLine();
        }
    }
}
