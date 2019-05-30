using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmonlandia_Game
{
    class TerrenosConsola
    {
         public string[] getAsciiTerreno(string tipo)
        {

            switch (tipo)
            {
                case "Volcan":
                   string[] volcan =
                        {
                            @"__      __  ",
                            @"\ \    / /  ",
                            @" \ \  / /__ ", 
                            @"  \ \/ / _ \", 
                            @"   \  / (_) ", 
                            @"    \/ \___/", 
                            @"            ", 
                        };
                    return volcan;

                case "Nieve":
                   string[] nieve =
                        {
                            @" _   _ _    ",
                            @"| \ | (_)   ",
                            @"|  \| |_    ", 
                            @"| . ` | |   ", 
                            @"| |\  | |   ", 
                            @"|_| \_|_|   ", 
                            @"            ", 
                        };
                    return nieve;

                case "Vegetacion":
                   string[] vegetacion =
						{
						    @"__      __  ",
						    @"\ \    / /  ", 
						    @" \ \  / /_  ", 
						    @"  \ \/ / _ \",
						    @"   \  /  __/",
						    @"    \/ \___|",
						    @"            ",
						 };   
                    return vegetacion;

                case "Acuatico":
                   string[] acuatico =
                        {
                            @"  ___       ",
                            @" / _ \      ", 
                            @"/ /_\ \ ___ ", 
                            @"|  _  |/ __|",
                            @"| | | | (__ ",
                            @"|_| |_|\___|",
                            @"            ",
                         };  
                    return acuatico;

                case "Desierto":
                   string[] desierto =
                        {
                            @" ____       ",
                            @"|  _ \      ", 
                            @"| | | | ___ ", 
                            @"| | | |/ _ \",
                            @"| |_| |  __/",
                            @"|_____/\___|",
                            @"            ",
                         }; 
                    return desierto;

                case "Blanco":
                    string[] blanco =
                         {
                            @"            ",
                            @"            ",
                            @"            ",
                            @"            ",
                            @"            ",
                            @"            ",
                            @"            ",
                        };
                    return blanco;

                default:
                    string[] vacio = { };
                    return vacio;
            }
       }

        public ConsoleColor getBackGroundColor(string tipo)
        {

            switch (tipo)
            {
                case "Volcan":
                    return ConsoleColor.Red;

                case "Nieve":
                    return ConsoleColor.White;

                case "Vegetacion":
                    return ConsoleColor.Green;

                case "Acuatico":
                    return ConsoleColor.Blue;

                case "Desierto":
                    return ConsoleColor.Yellow;

                default:
                    return ConsoleColor.White;
            }
        }

        public ConsoleColor getForeGroundColor(string tipo)
        {

            switch (tipo)
            {
                case "Volcan":
                    return ConsoleColor.Yellow;

                case "Nieve":
                    return ConsoleColor.Blue;

                case "Vegetacion":
                    return ConsoleColor.Black;

                case "Acuatico":
                    return ConsoleColor.White;

                case "Desierto":
                    return ConsoleColor.DarkRed;

                default:
                    return ConsoleColor.White;
            }

        }

    }
}
