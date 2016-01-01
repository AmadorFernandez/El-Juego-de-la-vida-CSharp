using System;
using System.IO;

namespace ElJuegoDeLaVida
{
    class Program
    {

        struct  Tablero
        {
            private const int ANCHO = 80;
            private const int ALTO = 24;
            private static string[,] generacion = new string[ALTO,ANCHO];
            private const string VIVA = "*";
            private const string MUERTA = " ";

            


            ///¿Como estoy?
            ///  <summary>
            /// Obtiene una submatriz e indica el estado de la célula
            /// </summary>
            /// <param name="fila">Indica la fila inicial de la célula en la matriz madre</param>
            /// <param name="columna">Indica la columna inicial de la célula en la matriz madre</param>
            /// <param name="grande">La matriz en la que se encuentra la célula</param>
            /// <param name="pequeña">La matriz hija con la célula y sus vecinos</param>
            /// <returns>true si la célula esta viva y false si esta muerta</returns>
            private  bool EstadoCelula(int fila, int columna, bool[,] grande)
            {

                //===========================================================//
                //La matriz hija
                bool[,] pequeña = new bool[3,3];

                //==========================================================//
                //Son los indices para la posión en la matriz
                int indiceF = 0;
                int indiceC = 0;
                //==========================================================//
                //==========================================================//
                //Indices para la matriz hija
                int x = 0;
                int y = 0;
                //=========================================================//
                //=========================================================//
                //Guarda el estado de la célula a observar y sus vecinas
                bool vecina = true;
                bool celula = true;
                //=========================================================//
                //Contadores para el estado de las vecinas
                int cuentaVivas = 0;
                //========================================================//

                //========================================================================//
                //Cargamos fila y columna inicial en los bucles para recorrer la matriz
                for (int i = fila; y < pequeña.GetLength(0); i++)
                {
                    for (int j = columna; x < pequeña.GetLength(1); j++)
                    {
                        //========================================================//
                        //Restamos 1 a cada indice para situarnos en la posición a
                        //observar
                        indiceF = (i - 1) % grande.GetLength(0);
                        indiceC = (j - 1) % grande.GetLength(1);
                        //========================================================//

                        //========================================================//
                        //En caso de obtener un valor negativo enviamos el indice 
                        //al final y restamos 1 para no salirnos de rango
                        if (indiceC < 0)
                        {
                            indiceC = grande.GetLength(1) - 1;
                        }
                        if (indiceF < 0)
                        {
                            indiceF = grande.GetLength(0) - 1;
                        }
                        //========================================================//

                        //==================================================================================================================================//
                        //Aplicamos aritmética modular a cada indice para recorrer ambas matrices de forma circular
                        pequeña[y % pequeña.GetLength(0), x % pequeña.GetLength(1)] = grande[indiceF % grande.GetLength(0), indiceC % grande.GetLength(1)];
                        //==================================================================================================================================//

                        x++; //Incrementamos x para avanzar en la columna
                    }
                    x = 0;//Reiniciamos x para un nuevo recorrido
                    y++;//Incrementamos para avanzar en la fila
                }


                //=============================================================================================================================================//
                //Recorremos la matriz hija para saber que estado le corresponde a la célula:

                for (int i = 0; i < pequeña.GetLength(0); i++)
                {
                    for (int j = 0; j < pequeña.GetLength(1); j++)
                    {

                        if (i == 1 && j == 1)
                        {
                            //Evita contar la pocisición central para no tener en cuenta 
                            //la célula por la que preguntamos
                            celula = pequeña[i, j];
                            
                        }
                        else
                        {
                           //Guarda el estado de la vecina
                           vecina = pequeña[i , j];

                            if (vecina)//Si esta viva
                            {
                                cuentaVivas++;
                            }
                          
                        }

                      
                    }
                }

               if(celula  && (cuentaVivas == 2 || cuentaVivas == 3))

                    return true;

               
                if (!celula && cuentaVivas == 3)

                    return true;

                if (celula  && (cuentaVivas > 3 || cuentaVivas < 2))
                {
                    return false;
                }

                return false;

            }


            ///Inicio aleatorio
            ///  <summary>
            /// Carga una generación aleatoria
            /// </summary>
            /// <returns>Una matriz string con la generación</returns>
            public  string[,]  GeneracionALEA()
            {
                bool[,] matrizBools = new bool[ALTO,ANCHO];
                Random rnd = new Random();
                int guarda = 0;

                for (int i = 0; i < matrizBools.GetLength(0); i++)
                {
                    for (int j = 0; j < matrizBools.GetLength(1); j++)
                    {
                        guarda = rnd.Next(2);

                        if (guarda == 0)
                        {
                            matrizBools[i, j] = false;
                        }
                        else
                        {
                            matrizBools[i, j] = true;
                        }

                        if (matrizBools[i,j] == true)
                        {
                            generacion[i, j] = VIVA;
                        }
                        else
                        {
                            generacion[i, j] = MUERTA;
                        }
                    }
                }

                return generacion;



            }


            ///Para pintar
            ///  <summary>
            /// Pinta en la tablero en la consola
            /// </summary>
            /// <param name="a">Es el tablero a pintar</param>
            public  void PintaTablero(string[,] a)
            {


                Console.SetCursorPosition(0,0);
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        
                        Console.Write(a[i,j]);
                    }

                    
                }
            }

            /// <summary>
            /// Avanza el tablero a la siguiente generación
            /// </summary>
            /// <param name="a">Es el tablero a avanzar</param>
            public  string[,] AvanzarGeneracio(string[,] a)
            {
                bool[,] Conversion = new bool[ALTO,ANCHO];
                bool[,] NuevaGeneracion = new bool[ALTO,ANCHO];

                for (int i = 0; i < Conversion.GetLength(0); i++)
                {
                    for (int j = 0; j < Conversion.GetLength(1); j++)
                    {
                        if (a[i, j] == VIVA)
                        {
                            Conversion[i, j] = true;
                        }
                        else
                        {
                            Conversion[i, j] = false;
                        }


                    }
                }

                for (int i = 0; i < generacion.GetLength(0); i++)
                {
                    for (int j = 0; j < generacion.GetLength(1); j++)
                    {
                        NuevaGeneracion[i, j] = EstadoCelula(i, j, Conversion);

                        if (NuevaGeneracion[i,j])
                        {
                            generacion[i, j] = VIVA;
                        }
                        else
                        {
                            generacion[i, j] = MUERTA;
                        }
                    }
                }

                return generacion;
            }


            ///Guardar
            ///  <summary>
            /// Guarda el tablero en un fichero
            /// </summary>
            /// <param name="a">Tablero a guardar</param>
            public void GuardarTablero(string[,] a)
            {
                generacion = a;
                StreamWriter fichero = new StreamWriter("PartidasGuardadas.txt");
                

                for (int i = 0; i < generacion.GetLength(0); i++)
                {
                    for (int j = 0; j < generacion.GetLength(1); j++)
                    {
                        fichero.Write(a[i,j]);

                     }                                       
                }

                fichero.Close();

            }

            public string[,] CargarTablero()
            {
                string sline = String.Empty;
                StreamReader fichero = new StreamReader("PartidasGuardadas.txt");
                

                for (int i = 0; i < generacion.GetLength(0); i++)
                {
                    for (int j = 0; j < generacion.GetLength(1); j++)
                    {
                        generacion[i, j] = ((char)fichero.Read()).ToString();
                    }
                }
                fichero.Close();

                return generacion;






            }

        }

        static void Controles()
        {
            ConsoleKeyInfo op = new ConsoleKeyInfo(); //Para la gestión de lo que quiera el usuario.
            int x = 0;
            int y = 0;
            do
            {
                Console.Clear();
                Console.SetCursorPosition(x, y);
                Console.WriteLine("\n\n\t\t\tCONTROLES");
                Console.WriteLine("================================================================================");
                Console.WriteLine(" I --> Avanza una generación\n");
                Console.WriteLine(" A --> Avanza de forma automática\n");
                Console.WriteLine(" R --> Reinicia a un nuevo tablero\n");
                Console.WriteLine(" S --> Salva la partida\n");
                Console.WriteLine(" M --> Regresa al menú principal\n");
                Console.SetCursorPosition(50, 24);
                Console.Write("®By Amador Fernández González");
                op = Console.ReadKey(true);
            } while (op.Key != ConsoleKey.M);
            return;
        }

        static void ArrancaJuego()
        {
            ConsoleKey op; //Para la gestión de lo que quiera el usuario.
            Tablero miTablero = new Tablero(); //Genera la estructura que usaremos.
            string[,] juego = new string[25, 80];
            int cont = 0;

            Console.Clear();
            juego = miTablero.GeneracionALEA();
            Console.WriteLine("\n\n\t\tI --> Iterar    A --> Automatico");
            
            do
            {
                
                Console.Title = ("Generación: " + cont);
                op = Console.ReadKey(true).Key;

                switch (op)
                {
                    case ConsoleKey.I:
                        
                        miTablero.PintaTablero(juego);
                        miTablero.AvanzarGeneracio(juego);
                        cont++;
                        break;
                    case ConsoleKey.A:
                        do
                        {
                            Console.Title = ("Generación: " + cont);
                            miTablero.PintaTablero(juego);
                            miTablero.AvanzarGeneracio(juego);
                            cont++; 
                        } while (!Console.KeyAvailable);
                        break;
                    case ConsoleKey.R:
                        
                        juego = miTablero.GeneracionALEA();
                        break;
                    case ConsoleKey.S:
                        miTablero.GuardarTablero(juego);
                        break;




                }





            } while (op != ConsoleKey.M);
            return;


        }

        static void ArrancaJuego(string[,] a)
        {
            ConsoleKey op; //Para la gestión de lo que quiera el usuario.
            Tablero miTablero = new Tablero(); //Genera la estructura que usaremos.
            string[,] juego = new string[25, 80];
            int cont = 0;

            Console.Clear();
            juego = a;
            Console.WriteLine("\n\n\t\tI --> Iterar    A --> Automatico");

            do
            {

                Console.Title = ("Generación: " + cont);
                op = Console.ReadKey(true).Key;

                switch (op)
                {
                    case ConsoleKey.I:

                        miTablero.PintaTablero(juego);
                        juego =  miTablero.AvanzarGeneracio(juego);
                        cont++;
                        break;
                    case ConsoleKey.A:
                        do
                        {
                            Console.Title = ("Generación: " + cont);
                            miTablero.PintaTablero(juego);
                            miTablero.AvanzarGeneracio(juego);
                            cont++;
                        } while (!Console.KeyAvailable);
                        break;
                    case ConsoleKey.R:

                        juego = miTablero.GeneracionALEA();
                        break;
                    case ConsoleKey.S:
                        miTablero.GuardarTablero(juego);
                        break;




                }





            } while (op != ConsoleKey.M);
            return;


        }

        static void UIMenu()
        {
            //==============================================================//
            Tablero miTablero = new Tablero();
            string elec = String.Empty;
            string[,] juego = new string[25, 80];
            int x = 0;
            int y = 0;


            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("\n\n\t\t\tBIENVENIDO A EL JUEGO DE LA VIDA");
                Console.WriteLine("================================================================================");
                Console.WriteLine(" 1.Ver controles\n");
                Console.WriteLine(" 2.Jugar\n");
                Console.WriteLine(" 3.Cargar partida\n");
                Console.WriteLine(" 0.Salir\n");
                x = Console.CursorLeft;
                y = Console.CursorTop;
                Console.SetCursorPosition(50, 24);
                Console.Write("®By Amador Fernández González");
                Console.SetCursorPosition(x, y);
                Console.Write(" Elija una opción: ");
                elec = Console.ReadLine();

                switch (elec)
                {
                    case "0":
                        return;

                    case "1":
                        Controles(); 
                        break;
                    case "2":
                        ArrancaJuego(); //Cede el control y arranca el juego
                        break;
                    case "3":
                       juego =  miTablero.CargarTablero();
                        ArrancaJuego(juego);

                        break;
                }
                
            } while (true);







        }

        static void Main(string[] args)
        {



            try
            {
                UIMenu();
            }
            catch (Exception e)
            {
                Console.WriteLine(" UPS ha ocurrido un error inesperado y necesito cerrarme. ");
                
            }




        






        }
    }
}
