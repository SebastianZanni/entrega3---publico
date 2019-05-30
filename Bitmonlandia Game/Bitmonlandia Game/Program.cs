using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmon
{
    class Program
    {
       
   //Creacion de las 3 cofiguraciones, con mapa y bitmons correspondientes.


            /* string opcion1 = "[Opción 1] Dimesiones mapa: 3x3, Terrenos(N° Bitmons iniciales): \n\n";
             string opcion2 = "[Opción 2] Dimesiones mapa: 4x4, Terrenos(N° Bitmons iniciales): \n\n";
             string opcion3 = "[Opción 3] Dimesiones mapa: 5x5, Terrenos(N° Bitmons iniciales): \n\n";

             int contadorfila = 0;
             foreach (Terreno terreno in config1.mapaRegion)
             {
                 opcion1 += terreno.getTipoAbrev() + "(" + terreno.numBitmons() + ") - ";
                 contadorfila++;
                 if (contadorfila%3 == 0)
                 {
                     opcion1=opcion1.Substring(0,opcion1.Length-2);
                     opcion1 += "\n";
                 }
             }
             contadorfila = 0;
             foreach (Terreno terreno in config2.mapaRegion)
             {
                 opcion2 += terreno.getTipoAbrev() + "(" + terreno.numBitmons() + ") - ";
                 contadorfila++;
                 if (contadorfila % 4 == 0)
                 {
                     opcion2 = opcion2.Substring(0, opcion2.Length - 2);
                     opcion2 += "\n";
                 }
             }
             contadorfila = 0;
             foreach (Terreno terreno in config3.mapaRegion)
             {
                 opcion3 += terreno.getTipoAbrev() + "(" + terreno.numBitmons() + ") - ";
                 contadorfila++;
                 if (contadorfila % 5 == 0)
                 {
                     opcion3 = opcion3.Substring(0, opcion3.Length - 2);
                     opcion3 += "\n";
                 }
             }

             // Definicion de terrenos,region y mundoBitmons
            // Console.WindowWidth = 90;
             //Console.WindowHeight = 50;
             Console.SetWindowPosition(0, 0);

             MapasConsola mapas = new MapasConsola();
             Console.WriteLine(mapas.logo);
             Console.WriteLine("Configuraciones iniciales disponibles: \n");
             Console.WriteLine("\nTipos Terreno--> Ac=Acuatico , De=Desierto , Ni=Nieve , Ve=Vegetacion , Vo=Volcan \n\n");
             Console.WriteLine(opcion1);
             Console.WriteLine(opcion2);
             Console.WriteLine(opcion3);
             Console.WriteLine("Elija una opción: ");

             int dimensionMapa = Convert.ToInt32(Console.ReadLine());
             dimensionMapa=dimensionMapa-1;

             Console.WriteLine("\nIngrese cuantos meses quiere simular: ");
             int mesesSimulacion = Convert.ToInt32(Console.ReadLine());


             Johto regionElegida;
             switch (dimensionMapa)
             {
                 case 0:
                      regionElegida = config1;
                     break;
                 case 1:
                     regionElegida = config2;
                     break;
                 case 2:
                     regionElegida = config3;
                     break;
                 default:
                     regionElegida = config1;
                     break;
             }


             Simulador simulador = new Simulador(dimensionMapa,regionElegida,mesesSimulacion);

             simulador.comenzarSimulacion();

         }*/
        }
    }


        

