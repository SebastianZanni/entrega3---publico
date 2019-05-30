using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace Bitmonlandia_Game
{
    class Simulador
    {
        public int dimensionMapa;
        public Johto region;
        //public bool flagBlink = false;
        public MapasConsola mapaconsola;
        public TerrenosConsola terrenosconsola;
        public int mesesSimulacion;
        Random random = new Random();
        Paraiso paraiso = new Paraiso();


        public Simulador(int dimensionMapa, Johto region, int mesesSimulacion)
        {
            this.dimensionMapa = dimensionMapa;
            this.region = region;
            this.mesesSimulacion = mesesSimulacion;
        }

        public void comenzarSimulacion()
        {
            int configMapa = dimensionMapa;
            int topTerreno = 0;
            int leftTerreno = 0;
            int topDatosTerreno = 0;
            int leftDatosTerreno = 0;

            Console.Clear();

            //Instancia clases
            MapasConsola mapaconsola = new MapasConsola();
            this.mapaconsola = mapaconsola;
            TerrenosConsola terrenosconsola = new TerrenosConsola();
            this.terrenosconsola = terrenosconsola;

            //Configura Ancho y Alto del Mapa
            Console.WindowWidth = mapaconsola.anchoMapas[configMapa];
            Console.WindowHeight = mapaconsola.altoMapas[configMapa];
            Console.SetWindowPosition(0, 0);

            //Escribe el Mapa
            Console.WriteLine(mapaconsola.mapas[configMapa]);
            int indiceTerreno = 0;
            string tipoTerreno = "";
            foreach (Terreno terreno in region.mapaRegion)
            {
                //Imprime todos los terrenos en el mapa ASCII.
                tipoTerreno = terreno.getTipo();
                Console.BackgroundColor = terrenosconsola.getBackGroundColor(tipoTerreno);
                Console.ForegroundColor = terrenosconsola.getForeGroundColor(tipoTerreno);
                topTerreno = mapaconsola.getTopTerreno(configMapa, indiceTerreno);
                leftTerreno = mapaconsola.getLeftTerreno(configMapa, indiceTerreno);

                foreach (string line in terrenosconsola.getAsciiTerreno(tipoTerreno))
                {
                    Console.SetCursorPosition(leftTerreno, topTerreno);
                    Console.Write(line);
                    topTerreno++;
                }
                Console.ResetColor();


                //Escribe cantidad de bitmons en terreno por tipo de bitmon.

                leftDatosTerreno = mapaconsola.getLeftDatosTerreno(configMapa, indiceTerreno, 0);
                // Console.BackgroundColor = ConsoleColor.Gray;
                //Console.ForegroundColor = ConsoleColor.Red;
                topDatosTerreno = mapaconsola.getTopDatosTerreno(configMapa, indiceTerreno, 0);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Dor:" + terreno.getNumTipoBitmon("Dorvalo")));


                topDatosTerreno = mapaconsola.getTopDatosTerreno(configMapa, indiceTerreno, 1);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Dot:" + terreno.getNumTipoBitmon("Doti")));

                topDatosTerreno = mapaconsola.getTopDatosTerreno(configMapa, indiceTerreno, 2);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Ent:" + terreno.getNumTipoBitmon("Ent")));

                topDatosTerreno = mapaconsola.getTopDatosTerreno(configMapa, indiceTerreno, 3);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Gof:" + terreno.getNumTipoBitmon("Gofue")));

                topDatosTerreno = mapaconsola.getTopDatosTerreno(configMapa, indiceTerreno, 4);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Tap:" + terreno.getNumTipoBitmon("Taplan")));

                topDatosTerreno = mapaconsola.getTopDatosTerreno(configMapa, indiceTerreno, 5);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Wet:" + terreno.getNumTipoBitmon("Wetar")));

                indiceTerreno++;
            }

            //Vuelve al origen
            Console.SetCursorPosition(0, 0);
            Console.SetWindowPosition(0, 0);

            Console.SetCursorPosition(3, 8);
            Console.Write(("Mapa inicial").PadRight(30));
            imprimirCantidadBitmonsInicial();
            Console.SetCursorPosition(0, 0);

            Console.ReadKey();

            //Loop principal de los meses de simulacion.
            for (int i = 0; i < mesesSimulacion; i++)
            {
                restarTiempoMensualVidaBitmons();
                validarMuertePorTiempoVidaBitmons();
                peleasBitmons();
                validarMuertePorPuntosVidaBitmons();
                breedBitmons();

                // Reproduccion de Ents cada 3 meses.
                if ((i + 1) % 3 == 0)
                {
                    breedEnts();
                }
                cambiarTipoTerrenos();
                moverBitmons();
                actualizarTerrenos();
                actualizarDatosMapaBitmons();
                Console.SetCursorPosition(3, 8);

                Console.Write(("Mes Actual: " + (i + 1)).PadRight(30));
                actualizarCantidadBitmonsMes();
                Console.SetCursorPosition(0, 0);

                Console.ReadKey();
            }

            Console.Clear();

            mostrarEstadisticas();
        }

        public void restarTiempoMensualVidaBitmons()
        {
            foreach (Terreno terreno in region.mapaRegion)
            {
                Bitmon bitmon;
                string tipoTerreno = terreno.getTipo();
                for (int i = terreno.bitmonsTerreno.Count() - 1; i >= 0; i--)
                {
                    bitmon = terreno.bitmonsTerreno[i];

                    if (bitmon.terrenosdebil.Contains(tipoTerreno))
                    {
                        bitmon.tiempoVida -= 2;
                        bitmon.tiempoVidaPerdido += 2;


                    }
                    else
                    {
                        bitmon.tiempoVida -= 1;
                        bitmon.tiempoVidaPerdido += 1;
                    }
                    //Aumenta los meses de vida de cada Bitmon.
                    bitmon.mesesDeVida += 1;
                }
            }
        }

        public void validarMuertePorTiempoVidaBitmons()
        {
            foreach (Terreno terreno in region.mapaRegion)
            {
                Bitmon bitmon;
                string tipoTerreno = terreno.getTipo();
                for (int i = terreno.bitmonsTerreno.Count() - 1; i >= 0; i--)
                {
                    bitmon = terreno.bitmonsTerreno[i];

                    if (bitmon.tiempoVida <= 0)
                    {
                        terreno.bitmonsTerreno.Remove(bitmon);
                        paraiso.Bithalla.Add(bitmon);
                    }
                }
            }
        }

        public void peleasBitmons()
        {
            //Resetea flag de pelea de los bitmons.
            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {
                    bitmon.yaPeleo = false;
                }
            }

            foreach (Terreno terreno in region.mapaRegion)
            {
                Bitmon bitmonPeleador;
                for (int i = terreno.bitmonsTerreno.Count() - 1; i >= 0; i--)
                {
                    bitmonPeleador = terreno.bitmonsTerreno[i];
                    if (bitmonPeleador.yaPeleo == false)
                    {
                        bitmonPeleador.pelear(terreno, bitmonPeleador);
                    }
                }
            }
        }

        public void validarMuertePorPuntosVidaBitmons()
        {
            foreach (Terreno terreno in region.mapaRegion)
            {
                Bitmon bitmon;
                string tipoTerreno = terreno.getTipo();
                for (int i = terreno.bitmonsTerreno.Count() - 1; i >= 0; i--)
                {
                    bitmon = terreno.bitmonsTerreno[i];

                    if (bitmon.puntosVida <= 0)
                    {
                        terreno.bitmonsTerreno.Remove(bitmon);
                        paraiso.Bithalla.Add(bitmon);
                    }
                }
            }
        }

        public void cambiarTipoTerrenos()

        {
            bool existeTaplan = false;
            bool existeGofue = false;
            Bitmon taplan = null;
            Bitmon gofue = null;
            foreach (Terreno terreno in region.mapaRegion)
            {
                if (terreno.getTipo() == "Desierto")
                {
                    foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                    {
                        if (bitmon.getTipo() == "Taplan" && bitmon.mesesDeVida > 0)
                        {
                            taplan = bitmon;
                            existeTaplan = true;

                            break;
                        }
                    }

                    if (existeTaplan == true)
                    {
                        taplan.transformarTerreno(terreno);
                    }
                }

                else if ((terreno.getTipo() == "Vegetacion" || terreno.getTipo() == "Nieve"))
                {
                    foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                    {
                        if (bitmon.getTipo() == "Gofue" && bitmon.mesesDeVida > 0)
                        {
                            gofue = bitmon;
                            existeGofue = true;

                            break;
                        }
                    }

                    if (existeGofue == true)
                    {
                        gofue.transformarTerreno(terreno);
                    }
                }
            }
        }

        public void breedBitmons()
        {
            //Resetea flag de pelea de los bitmons.
            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {
                    bitmon.yaSeReprodujo = false;
                }
            }

            foreach (Terreno terreno in region.mapaRegion)
            {
                Bitmon bitmonReproductor;
                for (int i = terreno.bitmonsTerreno.Count() - 1; i >= 0; i--)
                {
                    bitmonReproductor = terreno.bitmonsTerreno[i];
                    if ((bitmonReproductor.yaSeReprodujo == false) && (bitmonReproductor.getTipo() != "Ent") && (bitmonReproductor.mesesDeVida > 0))
                    {
                        bitmonReproductor.breed(region, terreno, bitmonReproductor);
                    }
                }
            }
        }

        public void breedEnts()
        {
            //Resetea flag de pelea de los bitmons.
            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {
                    bitmon.yaSeReprodujo = false;
                }
            }

            foreach (Terreno terreno in region.mapaRegion)
            {
                Bitmon bitmonReproductor;
                for (int i = terreno.bitmonsTerreno.Count() - 1; i >= 0; i--)
                {
                    bitmonReproductor = terreno.bitmonsTerreno[i];
                    if ((bitmonReproductor.getTipo() == "Ent") && (bitmonReproductor.mesesDeVida > 0))
                    {
                        bitmonReproductor.breed(region, terreno, bitmonReproductor);
                    }
                }
            }
        }


        public void moverBitmons()
        {
            //Resetea flag de movimiento de los bitmons.
            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {
                    bitmon.seMovio = false;
                }
            }
            //Mueve los bitmons de cada terreno.
            foreach (Terreno terreno in region.mapaRegion)
            {
                int filaTerreno = terreno.getFila();
                int colTerreno = terreno.getColumna();
                Bitmon bitmon;
                string tipoBitmon;
                string tipoNuevoTerreno;
                bool bitmonSeMovio;

                for (int i = terreno.bitmonsTerreno.Count() - 1; i >= 0; i--)
                {
                    bitmon = terreno.bitmonsTerreno[i];
                    tipoBitmon = bitmon.getTipo();
                    bitmonSeMovio = bitmon.seMovio;
                    int direccionMov = random.Next(0, 5);
                    if (bitmonSeMovio == false)
                    {
                        int[] nuevaPosicion = bitmon.moverse(terreno, region, direccionMov);

                        // Solo se puede mover un Bitmons tras vivir un mes.
                        if (bitmon.mesesDeVida > 0)
                        {
                            if ((filaTerreno != nuevaPosicion[0]) || (colTerreno != nuevaPosicion[1]))
                            {
                                tipoNuevoTerreno = region.mapaRegion[nuevaPosicion[0], nuevaPosicion[1]].getTipo();

                                if ((tipoNuevoTerreno != "Acuatico") && (tipoBitmon == "Wetar"))
                                {
                                    //Wetar no se puede mover a terrenos no acuaticos
                                }
                                else
                                {
                                    terreno.bitmonsTerreno.Remove(bitmon); //Remueve el bitmon del terreno actual
                                    region.mapaRegion[nuevaPosicion[0], nuevaPosicion[1]].bitmonsTerreno.Add(bitmon);
                                    bitmon.seMovio = true;
                                }
                            }
                        }

                    }


                }
            }
        }


        public void actualizarTerrenos()
        {
            int indiceTerreno = 0;
            string tipoTerreno = "";
            int leftTerreno = 0;
            int topTerreno = 0;

            foreach (Terreno terreno in region.mapaRegion)
            {
                //Imprime todos los terrenos en el mapa ASCII.
                tipoTerreno = terreno.getTipo();
                Console.BackgroundColor = terrenosconsola.getBackGroundColor(tipoTerreno);
                Console.ForegroundColor = terrenosconsola.getForeGroundColor(tipoTerreno);

                topTerreno = mapaconsola.getTopTerreno(dimensionMapa, indiceTerreno);
                leftTerreno = mapaconsola.getLeftTerreno(dimensionMapa, indiceTerreno);

                foreach (string line in terrenosconsola.getAsciiTerreno(tipoTerreno))
                {
                    Console.SetCursorPosition(leftTerreno, topTerreno);
                    Console.Write(line);
                    topTerreno++;
                }
                Console.ResetColor();

                indiceTerreno++;

            }
        }


        public void actualizarDatosMapaBitmons()
        {
            int indiceTerreno = 0;
            int leftTerreno = 0;
            int topTerreno = 0;
            int leftDatosTerreno = 0;
            int topDatosTerreno = 0;

            foreach (Terreno terreno in region.mapaRegion)
            {

                topTerreno = mapaconsola.getTopTerreno(dimensionMapa, indiceTerreno);
                leftTerreno = mapaconsola.getLeftTerreno(dimensionMapa, indiceTerreno);
                leftDatosTerreno = mapaconsola.getLeftDatosTerreno(dimensionMapa, indiceTerreno, 0);

                //Escribe cantidad de bitmons en terreno por tipo de bitmon.
                topDatosTerreno = mapaconsola.getTopDatosTerreno(dimensionMapa, indiceTerreno, 0);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Dor:" + terreno.getNumTipoBitmon("Dorvalo")));


                topDatosTerreno = mapaconsola.getTopDatosTerreno(dimensionMapa, indiceTerreno, 1);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Dot:" + terreno.getNumTipoBitmon("Doti")));

                topDatosTerreno = mapaconsola.getTopDatosTerreno(dimensionMapa, indiceTerreno, 2);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Ent:" + terreno.getNumTipoBitmon("Ent")));

                topDatosTerreno = mapaconsola.getTopDatosTerreno(dimensionMapa, indiceTerreno, 3);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Gof:" + terreno.getNumTipoBitmon("Gofue")));

                topDatosTerreno = mapaconsola.getTopDatosTerreno(dimensionMapa, indiceTerreno, 4);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Tap:" + terreno.getNumTipoBitmon("Taplan")));

                topDatosTerreno = mapaconsola.getTopDatosTerreno(dimensionMapa, indiceTerreno, 5);
                Console.SetCursorPosition(leftDatosTerreno, topDatosTerreno);
                Console.Write(padDatos("Wet:" + terreno.getNumTipoBitmon("Wetar")));

                indiceTerreno++;
            }
        }


        public void imprimirCantidadBitmonsInicial()
        {

            int contadorVivos = 0;

            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {
                    contadorVivos += 1;
                }
            }

            Console.SetCursorPosition(25, 8);
            Console.Write(("Bitmons iniciales: " + (contadorVivos.ToString()).PadRight(30)));
        }


        public void actualizarCantidadBitmonsMes()
        {

            int contadorVivos = 0;

            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {
                    contadorVivos += 1;
                }
            }

            Console.SetCursorPosition(25, 8);
            Console.Write(("Bitmons vivos: " + (contadorVivos.ToString()).PadRight(30)));
            Console.SetCursorPosition(50, 8);
            Console.Write(("Bitmons muertos: " + paraiso.Bithalla.Count()).PadRight(30));
            Console.SetCursorPosition(0, 0);
        }

        public void mostrarEstadisticas()
        {

            double contadorDorval = 0;
            double tiempoVidaTotalDorval = 0;
            double tiempoVidaPromedioDorval = 0;
            double cantidadHijosDorval = 0;
            double tasaBrutaNatalidadDorval = 0;
            double hijosPromedioDorval = 0;
            double cantidadVivosDorval = 0;
            double cantidadMuertosDorval = 0;

            double contadorGofue = 0;
            double tiempoVidaTotalGofue = 0;
            double tiempoVidaPromedioGofue = 0;
            double cantidadHijosGofue = 0;
            double tasaBrutaNatalidadGofue = 0;
            double hijosPromedioGofue = 0;
            double cantidadVivosGofue = 0;
            double cantidadMuertosGofue = 0;

            double contadorDoti = 0;
            double tiempoVidaTotalDoti = 0;
            double tiempoVidaPromedioDoti = 0;
            double cantidadHijosDoti = 0;
            double tasaBrutaNatalidadDoti = 0;
            double hijosPromedioDoti = 0;
            double cantidadVivosDoti = 0;
            double cantidadMuertosDoti = 0;

            double contadorEnt = 0;
            double tiempoVidaTotalEnt = 0;
            double tiempoVidaPromedioEnt = 0;
            double cantidadHijosEnt = 0;
            double tasaBrutaNatalidadEnt = 0;
            double hijosPromedioEnt = 0;
            double cantidadVivosEnt = 0;
            double cantidadMuertosEnt = 0;

            double contadorWetar = 0;
            double tiempoVidaTotalWetar = 0;
            double tiempoVidaPromedioWetar = 0;
            double cantidadHijosWetar = 0;
            double tasaBrutaNatalidadWetar = 0;
            double hijosPromedioWetar = 0;
            double cantidadVivosWetar = 0;
            double cantidadMuertosWetar = 0;

            double contadorTaplan = 0;
            double tiempoVidaTotalTaplan = 0;
            double tiempoVidaPromedioTaplan = 0;
            double cantidadHijosTaplan = 0;
            double tasaBrutaNatalidadTaplan = 0;
            double hijosPromedioTaplan = 0;
            double cantidadVivosTaplan = 0;
            double cantidadMuertosTaplan = 0;

            double tiempoVidaTotal = 0;
            double totalBitmonsVivos = 0;
            double totalBitmonsMuertos = 0;
            double tiempoVidaPromedioBitmon = 0;
            double tasaBrutaMortalidad = 0;


            Console.WindowWidth = 100;
            Console.WindowHeight = 60;


            Console.WriteLine(mapaconsola.logo);
            Console.WriteLine("\n");

            Console.WriteLine("**************************");
            Console.WriteLine("La simulacion ha terminado");
            Console.WriteLine("**************************\n");

            //Cuenta los vivos
            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {

                    switch (bitmon.getTipo())
                    {
                        case "Dorvalo":

                            tiempoVidaTotalDorval += bitmon.mesesDeVida;
                            cantidadHijosDorval += bitmon.cantidadDeHijos;
                            cantidadVivosDorval += 1;
                            break;
                        case "Doti":
                            tiempoVidaTotalDoti += bitmon.mesesDeVida;
                            cantidadHijosDoti += bitmon.mesesDeVida;
                            cantidadVivosDoti += 1;
                            break;
                        case "Ent":
                            tiempoVidaTotalEnt += bitmon.mesesDeVida;
                            cantidadHijosEnt += bitmon.mesesDeVida;
                            cantidadVivosEnt += 1;
                            break;

                        case "Gofue":
                            tiempoVidaTotalGofue += bitmon.mesesDeVida;
                            cantidadHijosGofue += bitmon.mesesDeVida;
                            cantidadVivosGofue += 1;
                            break;
                        case "Taplan":
                            tiempoVidaTotalTaplan += bitmon.mesesDeVida;
                            cantidadHijosTaplan += bitmon.mesesDeVida;
                            cantidadVivosTaplan += 1;
                            break;
                        case "Wetar":
                            tiempoVidaTotalWetar += bitmon.mesesDeVida;
                            cantidadHijosWetar += bitmon.mesesDeVida;
                            cantidadVivosWetar += 1;
                            break;
                    }
                    tiempoVidaTotal += bitmon.mesesDeVida;
                    totalBitmonsVivos += 1;
                }
            }

            //Cuenta los muertos
            foreach (Bitmon bitmon in paraiso.Bithalla)
            {
                switch (bitmon.getTipo())
                {
                    case "Dorvalo":
                        tiempoVidaTotalDorval += bitmon.mesesDeVida;
                        cantidadMuertosDorval += 1;
                        break;
                    case "Doti":
                        cantidadMuertosDoti += 1;
                        tiempoVidaTotalDoti += bitmon.mesesDeVida;
                        break;
                    case "Ent":
                        cantidadMuertosEnt += 1;
                        tiempoVidaTotalEnt += bitmon.mesesDeVida;
                        break;

                    case "Gofue":
                        cantidadMuertosGofue += 1;
                        tiempoVidaTotalGofue += bitmon.mesesDeVida;
                        break;
                    case "Taplan":
                        cantidadMuertosTaplan += 1;
                        tiempoVidaTotalTaplan += bitmon.mesesDeVida;
                        break;
                    case "Wetar":
                        cantidadMuertosWetar += 1;
                        tiempoVidaTotalWetar += bitmon.mesesDeVida;
                        break;
                }
                tiempoVidaTotal += bitmon.mesesDeVida;
                totalBitmonsMuertos += 1;
            }

            //Suma los contadores y vidas de vivos y muertos
            contadorDorval = cantidadMuertosDorval + cantidadVivosDorval;
            contadorDoti = cantidadMuertosDoti + cantidadVivosDoti;
            contadorEnt = cantidadMuertosEnt + cantidadVivosEnt;
            contadorGofue = cantidadMuertosGofue + cantidadVivosGofue;
            contadorTaplan = cantidadMuertosTaplan + cantidadVivosTaplan;
            contadorWetar = cantidadMuertosWetar + cantidadVivosWetar;

            //Calculo de indicadores
            tiempoVidaPromedioBitmon = (tiempoVidaTotal / (totalBitmonsMuertos + totalBitmonsVivos));

            if (contadorDorval == 0)
            {
                tiempoVidaPromedioDorval = 0;
                tasaBrutaNatalidadDorval = 0;
                hijosPromedioDorval = 0;
            }
            else
            {
                tiempoVidaPromedioDorval = (tiempoVidaTotalDorval / (contadorDorval));
                tasaBrutaNatalidadDorval = (cantidadHijosDorval / contadorDorval) * 100;
                hijosPromedioDorval = (cantidadHijosDorval / contadorDorval);
            }
            if (contadorDoti == 0)
            {
                tiempoVidaPromedioDoti = 0;
                tasaBrutaNatalidadDoti = 0;
                hijosPromedioDoti = 0;
            }
            else
            {
                tiempoVidaPromedioDoti = (tiempoVidaTotalDoti / (contadorDoti));
                tasaBrutaNatalidadDoti = (cantidadHijosDoti / contadorDoti) * 100;
                hijosPromedioDoti = (cantidadHijosDoti / contadorDoti);
            }
            
            if (contadorEnt == 0)
            {
                tiempoVidaPromedioEnt = 0;
                tasaBrutaNatalidadEnt = 0;
                hijosPromedioEnt = 0;
            }
            else
            {
                tiempoVidaPromedioEnt= (tiempoVidaTotalEnt / (contadorEnt));
                tasaBrutaNatalidadEnt = (cantidadHijosEnt / contadorEnt) * 100;
                hijosPromedioEnt = (cantidadHijosEnt / contadorEnt);
            }

            if (contadorWetar == 0)
            {
                tiempoVidaPromedioWetar = 0;
                tasaBrutaNatalidadWetar = 0;
                hijosPromedioWetar = 0;
            }
            else
            {
                tiempoVidaPromedioWetar = (tiempoVidaTotalWetar / (contadorWetar));
                hijosPromedioWetar = (cantidadHijosWetar / contadorWetar);
                tasaBrutaNatalidadWetar = (cantidadHijosWetar / contadorWetar) * 100;
            }
            if (contadorGofue == 0)
            {
                tiempoVidaPromedioGofue = 0;
                tasaBrutaNatalidadGofue = 0;
                hijosPromedioGofue = 0;
            }
            else { 
                tiempoVidaPromedioGofue = (tiempoVidaTotalGofue / (contadorGofue));
                tasaBrutaNatalidadGofue = (cantidadHijosGofue / contadorGofue) * 100;
                hijosPromedioGofue = (cantidadHijosGofue / contadorGofue);
            }
            if (contadorTaplan == 0)
            {
                tiempoVidaPromedioTaplan = 0;
                tasaBrutaNatalidadTaplan = 0;
                hijosPromedioTaplan = 0;
            }
            else {
                tiempoVidaPromedioTaplan = (tiempoVidaTotalTaplan / (contadorTaplan));
                tasaBrutaNatalidadTaplan = (cantidadHijosTaplan / contadorTaplan) * 100;
                hijosPromedioTaplan = (cantidadHijosTaplan / contadorTaplan);
            }

            tasaBrutaMortalidad = (totalBitmonsMuertos / (totalBitmonsMuertos + totalBitmonsVivos)) * 100;

            Console.WriteLine("Tiempo vida promedio de Bitmon: " + Math.Round(tiempoVidaPromedioBitmon, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("***************************************\n");

            Console.WriteLine("Tiempo vida promedio por especie Bitmon:");
            Console.WriteLine("***************************************");
            Console.WriteLine("Dorvalo: " + Math.Round(tiempoVidaPromedioDorval, 2, MidpointRounding.AwayFromZero)+ " meses");
            Console.WriteLine("Doti: " + Math.Round(tiempoVidaPromedioDoti, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Ent: " + Math.Round(tiempoVidaPromedioEnt, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Gofue: " + Math.Round(tiempoVidaPromedioGofue, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Taplan: " + Math.Round(tiempoVidaPromedioTaplan, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Wetar: " + Math.Round(tiempoVidaPromedioWetar, 2, MidpointRounding.AwayFromZero) + " meses \n\n");

            Console.WriteLine("Tasa bruta de natalidad por cada especie:");
            Console.WriteLine("****************************************");
            Console.WriteLine("Dorvalo: " + Math.Round(tasaBrutaNatalidadDorval, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Dorvalo");
            Console.WriteLine("Doti: " + Math.Round(tasaBrutaNatalidadDoti, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Doti");
            Console.WriteLine("Ent: " + Math.Round(tasaBrutaNatalidadEnt, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Ent");
            Console.WriteLine("Gofue: " + Math.Round(tasaBrutaNatalidadGofue, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Gofue");
            Console.WriteLine("Taplan: " + Math.Round(tasaBrutaNatalidadTaplan, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Taplan");
            Console.WriteLine("Wetar: " + Math.Round(tasaBrutaNatalidadWetar, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Wetar\n\n");

            Console.WriteLine("Tasa bruta de mortalidad. " + Math.Round(tasaBrutaMortalidad, 2, MidpointRounding.AwayFromZero) + " por cada 100 Bitmons");
            Console.WriteLine("***************************************\n");

            Console.WriteLine("Cantidad hijos promedio por cada especie:");
            Console.WriteLine("****************************************");
            Console.WriteLine("Dorval: " + Math.Round(hijosPromedioDorval, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Doti: " + Math.Round(hijosPromedioDoti, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Ent: " + Math.Round(hijosPromedioEnt, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Gofue: " + Math.Round(hijosPromedioGofue, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Taplan: " + Math.Round(hijosPromedioTaplan, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Wetar: " + Math.Round(hijosPromedioWetar, 2, MidpointRounding.AwayFromZero) + "\n\n");


            Console.WriteLine("Especies exintas:");
            Console.WriteLine("****************");
            if (cantidadVivosDorval == 0)
            {
                Console.WriteLine("Dorvalos");
            }
            if (cantidadVivosDoti == 0)
            {
                Console.WriteLine("Doti");
            }
            if (cantidadVivosEnt == 0)
            {
                Console.WriteLine("Ent");
            }
            if (cantidadVivosGofue == 0)
            {
                Console.WriteLine("Gofue");
            }
            if (cantidadVivosTaplan == 0)
            {
                Console.WriteLine("Taplan");
            }
            if (cantidadVivosWetar == 0)
            {
                Console.WriteLine("Wetar");
            }

            Console.WriteLine("\n");

            Console.WriteLine("Poblacion de Bitmons en Bithalla:");
            Console.WriteLine("****************************************");
            Console.WriteLine("Dorval: " + (cantidadMuertosDorval) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosDorval / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Doti: " + (cantidadMuertosDoti) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosDoti / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Ent: " + (cantidadMuertosEnt) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosEnt / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Gofue: " + (cantidadMuertosGofue) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosGofue / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Taplan: " + (cantidadMuertosTaplan) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosTaplan / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Wetar: " + (cantidadMuertosWetar) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosWetar / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");

            Console.WriteLine("\n");

            Console.WriteLine("Presione cualquier tecla, para escribir archivo de estadisticas TXT y cerrar la consola");
            Console.ReadKey();
            escribirEstadisticasTXT();

        }

        public void escribirEstadisticasTXT()
        {

            double contadorDorval = 0;
            double tiempoVidaTotalDorval = 0;
            double tiempoVidaPromedioDorval = 0;
            double cantidadHijosDorval = 0;
            double tasaBrutaNatalidadDorval = 0;
            double hijosPromedioDorval = 0;
            double cantidadVivosDorval = 0;
            double cantidadMuertosDorval = 0;

            double contadorGofue = 0;
            double tiempoVidaTotalGofue = 0;
            double tiempoVidaPromedioGofue = 0;
            double cantidadHijosGofue = 0;
            double tasaBrutaNatalidadGofue = 0;
            double hijosPromedioGofue = 0;
            double cantidadVivosGofue = 0;
            double cantidadMuertosGofue = 0;

            double contadorDoti = 0;
            double tiempoVidaTotalDoti = 0;
            double tiempoVidaPromedioDoti = 0;
            double cantidadHijosDoti = 0;
            double tasaBrutaNatalidadDoti = 0;
            double hijosPromedioDoti = 0;
            double cantidadVivosDoti = 0;
            double cantidadMuertosDoti = 0;

            double contadorEnt = 0;
            double tiempoVidaTotalEnt = 0;
            double tiempoVidaPromedioEnt = 0;
            double cantidadHijosEnt = 0;
            double tasaBrutaNatalidadEnt = 0;
            double hijosPromedioEnt = 0;
            double cantidadVivosEnt = 0;
            double cantidadMuertosEnt = 0;

            double contadorWetar = 0;
            double tiempoVidaTotalWetar = 0;
            double tiempoVidaPromedioWetar = 0;
            double cantidadHijosWetar = 0;
            double tasaBrutaNatalidadWetar = 0;
            double hijosPromedioWetar = 0;
            double cantidadVivosWetar = 0;
            double cantidadMuertosWetar = 0;

            double contadorTaplan = 0;
            double tiempoVidaTotalTaplan = 0;
            double tiempoVidaPromedioTaplan = 0;
            double cantidadHijosTaplan = 0;
            double tasaBrutaNatalidadTaplan = 0;
            double hijosPromedioTaplan = 0;
            double cantidadVivosTaplan = 0;
            double cantidadMuertosTaplan = 0;

            double tiempoVidaTotal = 0;
            double totalBitmonsVivos = 0;
            double totalBitmonsMuertos = 0;
            double tiempoVidaPromedioBitmon = 0;
            double tasaBrutaMortalidad = 0;


            string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "\\Estadisticas_Bitmonlandia.txt";

            FileStream file;
            StreamWriter writer;
            TextWriter tipoOut = Console.Out;

            file = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write);
            writer = new StreamWriter(file);
            Console.SetOut(writer);

            Console.WriteLine(mapaconsola.logo);
            Console.WriteLine("\n\n");

            //Cuenta los vivos
            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {

                    switch (bitmon.getTipo())
                    {
                        case "Dorvalo":

                            tiempoVidaTotalDorval += bitmon.mesesDeVida;
                            cantidadHijosDorval += bitmon.cantidadDeHijos;
                            cantidadVivosDorval += 1;
                            break;
                        case "Doti":
                            tiempoVidaTotalDoti += bitmon.mesesDeVida;
                            cantidadHijosDoti += bitmon.mesesDeVida;
                            cantidadVivosDoti += 1;
                            break;
                        case "Ent":
                            tiempoVidaTotalEnt += bitmon.mesesDeVida;
                            cantidadHijosEnt += bitmon.mesesDeVida;
                            cantidadVivosEnt += 1;
                            break;

                        case "Gofue":
                            tiempoVidaTotalGofue += bitmon.mesesDeVida;
                            cantidadHijosGofue += bitmon.mesesDeVida;
                            cantidadVivosGofue += 1;
                            break;
                        case "Taplan":
                            tiempoVidaTotalTaplan += bitmon.mesesDeVida;
                            cantidadHijosTaplan += bitmon.mesesDeVida;
                            cantidadVivosTaplan += 1;
                            break;
                        case "Wetar":
                            tiempoVidaTotalWetar += bitmon.mesesDeVida;
                            cantidadHijosWetar += bitmon.mesesDeVida;
                            cantidadVivosWetar += 1;
                            break;
                    }
                    tiempoVidaTotal += bitmon.mesesDeVida;
                    totalBitmonsVivos += 1;
                }
            }

            //Cuenta los muertos
            foreach (Bitmon bitmon in paraiso.Bithalla)
            {
                switch (bitmon.getTipo())
                {
                    case "Dorvalo":
                        tiempoVidaTotalDorval += bitmon.mesesDeVida;
                        cantidadMuertosDorval += 1;
                        break;
                    case "Doti":
                        cantidadMuertosDoti += 1;
                        tiempoVidaTotalDoti += bitmon.mesesDeVida;
                        break;
                    case "Ent":
                        cantidadMuertosEnt += 1;
                        tiempoVidaTotalEnt += bitmon.mesesDeVida;
                        break;

                    case "Gofue":
                        cantidadMuertosGofue += 1;
                        tiempoVidaTotalGofue += bitmon.mesesDeVida;
                        break;
                    case "Taplan":
                        cantidadMuertosTaplan += 1;
                        tiempoVidaTotalTaplan += bitmon.mesesDeVida;
                        break;
                    case "Wetar":
                        cantidadMuertosWetar += 1;
                        tiempoVidaTotalWetar += bitmon.mesesDeVida;
                        break;
                }
                tiempoVidaTotal += bitmon.mesesDeVida;
                totalBitmonsMuertos += 1;
            }

            //Suma los contadores y vidas de vivos y muertos
            contadorDorval = cantidadMuertosDorval + cantidadVivosDorval;
            contadorDoti = cantidadMuertosDoti + cantidadVivosDoti;
            contadorEnt = cantidadMuertosEnt + cantidadVivosEnt;
            contadorGofue = cantidadMuertosGofue + cantidadVivosGofue;
            contadorTaplan = cantidadMuertosTaplan + cantidadVivosTaplan;
            contadorWetar = cantidadMuertosWetar + cantidadVivosWetar;

            //Calculo de indicadores
            tiempoVidaPromedioBitmon = (tiempoVidaTotal / (totalBitmonsMuertos + totalBitmonsVivos));

            if (contadorDorval == 0)
            {
                tiempoVidaPromedioDorval = 0;
                tasaBrutaNatalidadDorval = 0;
                hijosPromedioDorval = 0;
            }
            else
            {
                tiempoVidaPromedioDorval = (tiempoVidaTotalDorval / (contadorDorval));
                tasaBrutaNatalidadDorval = (cantidadHijosDorval / contadorDorval) * 100;
                hijosPromedioDorval = (cantidadHijosDorval / contadorDorval);
            }
            if (contadorDoti == 0)
            {
                tiempoVidaPromedioDoti = 0;
                tasaBrutaNatalidadDoti = 0;
                hijosPromedioDoti = 0;
            }
            else
            {
                tiempoVidaPromedioDoti = (tiempoVidaTotalDoti / (contadorDoti));
                tasaBrutaNatalidadDoti = (cantidadHijosDoti / contadorDoti) * 100;
                hijosPromedioDoti = (cantidadHijosDoti / contadorDoti);
            }

            if (contadorEnt == 0)
            {
                tiempoVidaPromedioEnt = 0;
                tasaBrutaNatalidadEnt = 0;
                hijosPromedioEnt = 0;
            }
            else
            {
                tiempoVidaPromedioEnt = (tiempoVidaTotalEnt / (contadorEnt));
                tasaBrutaNatalidadEnt = (cantidadHijosEnt / contadorEnt) * 100;
                hijosPromedioEnt = (cantidadHijosEnt / contadorEnt);
            }

            if (contadorWetar == 0)
            {
                tiempoVidaPromedioWetar = 0;
                tasaBrutaNatalidadWetar = 0;
                hijosPromedioWetar = 0;
            }
            else
            {
                tiempoVidaPromedioWetar = (tiempoVidaTotalWetar / (contadorWetar));
                hijosPromedioWetar = (cantidadHijosWetar / contadorWetar);
                tasaBrutaNatalidadWetar = (cantidadHijosWetar / contadorWetar) * 100;
            }
            if (contadorGofue == 0)
            {
                tiempoVidaPromedioGofue = 0;
                tasaBrutaNatalidadGofue = 0;
                hijosPromedioGofue = 0;
            }
            else
            {
                tiempoVidaPromedioGofue = (tiempoVidaTotalGofue / (contadorGofue));
                tasaBrutaNatalidadGofue = (cantidadHijosGofue / contadorGofue) * 100;
                hijosPromedioGofue = (cantidadHijosGofue / contadorGofue);
            }
            if (contadorTaplan == 0)
            {
                tiempoVidaPromedioTaplan = 0;
                tasaBrutaNatalidadTaplan = 0;
                hijosPromedioTaplan = 0;
            }
            else
            {
                tiempoVidaPromedioTaplan = (tiempoVidaTotalTaplan / (contadorTaplan));
                tasaBrutaNatalidadTaplan = (cantidadHijosTaplan / contadorTaplan) * 100;
                hijosPromedioTaplan = (cantidadHijosTaplan / contadorTaplan);
            }

            tasaBrutaMortalidad = (totalBitmonsMuertos / (totalBitmonsMuertos + totalBitmonsVivos)) * 100;

            Console.WriteLine("Tiempo vida promedio de Bitmon: " + Math.Round(tiempoVidaPromedioBitmon, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("***************************************\n");

            Console.WriteLine("Tiempo vida promedio por especie Bitmon:");
            Console.WriteLine("***************************************");
            Console.WriteLine("Dorvalo: " + Math.Round(tiempoVidaPromedioDorval, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Doti: " + Math.Round(tiempoVidaPromedioDoti, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Ent: " + Math.Round(tiempoVidaPromedioEnt, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Gofue: " + Math.Round(tiempoVidaPromedioGofue, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Taplan: " + Math.Round(tiempoVidaPromedioTaplan, 2, MidpointRounding.AwayFromZero) + " meses");
            Console.WriteLine("Wetar: " + Math.Round(tiempoVidaPromedioWetar, 2, MidpointRounding.AwayFromZero) + " meses \n\n");

            Console.WriteLine("Tasa bruta de natalidad por cada especie:");
            Console.WriteLine("****************************************");
            Console.WriteLine("Dorvalo: " + Math.Round(tasaBrutaNatalidadDorval, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Dorvalo");
            Console.WriteLine("Doti: " + Math.Round(tasaBrutaNatalidadDoti, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Doti");
            Console.WriteLine("Ent: " + Math.Round(tasaBrutaNatalidadEnt, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Ent");
            Console.WriteLine("Gofue: " + Math.Round(tasaBrutaNatalidadGofue, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Gofue");
            Console.WriteLine("Taplan: " + Math.Round(tasaBrutaNatalidadTaplan, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Taplan");
            Console.WriteLine("Wetar: " + Math.Round(tasaBrutaNatalidadWetar, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Wetar\n\n");

            Console.WriteLine("Tasa bruta de mortalidad. " + Math.Round(tasaBrutaMortalidad, 2, MidpointRounding.AwayFromZero) + " por cada 100 Bitmons");
            Console.WriteLine("***************************************\n");

            Console.WriteLine("Cantidad hijos promedio por cada especie:");
            Console.WriteLine("****************************************");
            Console.WriteLine("Dorval: " + Math.Round(hijosPromedioDorval, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Doti: " + Math.Round(hijosPromedioDoti, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Ent: " + Math.Round(hijosPromedioEnt, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Gofue: " + Math.Round(hijosPromedioGofue, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Taplan: " + Math.Round(hijosPromedioTaplan, 2, MidpointRounding.AwayFromZero));
            Console.WriteLine("Wetar: " + Math.Round(hijosPromedioWetar, 2, MidpointRounding.AwayFromZero) + "\n\n");


            Console.WriteLine("Especies exintas:");
            Console.WriteLine("****************");
            if (cantidadVivosDorval == 0)
            {
                Console.WriteLine("Dorvalos");
            }
            if (cantidadVivosDoti == 0)
            {
                Console.WriteLine("Doti");
            }
            if (cantidadVivosEnt == 0)
            {
                Console.WriteLine("Ent");
            }
            if (cantidadVivosGofue == 0)
            {
                Console.WriteLine("Gofue");
            }
            if (cantidadVivosTaplan == 0)
            {
                Console.WriteLine("Taplan");
            }
            if (cantidadVivosWetar == 0)
            {
                Console.WriteLine("Wetar");
            }

            Console.WriteLine("\n");

            Console.WriteLine("Poblacion de Bitmons en Bithalla:");
            Console.WriteLine("****************************************");
            Console.WriteLine("Dorval: " + (cantidadMuertosDorval) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosDorval / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Doti: " + (cantidadMuertosDoti) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosDoti / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Ent: " + (cantidadMuertosEnt) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosEnt / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Gofue: " + (cantidadMuertosGofue) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosGofue / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Taplan: " + (cantidadMuertosTaplan) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosTaplan / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            Console.WriteLine("Wetar: " + (cantidadMuertosWetar) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosWetar / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");

            Console.SetOut(tipoOut);
            writer.Close();
            file.Close();

        }



        //private void OnBlinkTimer(object source, ElapsedEventArgs e)
        //{

        //    int indiceTerreno = 0;
        //    string tipoTerreno = "";
        //    int topTerreno = 0;
        //    int leftTerreno = 0;

        //    foreach (Terreno terreno in this.region.mapaRegion)
        //    {
        //        //Imprime todos los terrenos en el mapa ASCII.
        //        if (flagBlink == false)
        //        {
        //            tipoTerreno = "Blanco";
        //            Console.BackgroundColor = ConsoleColor.Black;
        //            Console.ForegroundColor = ConsoleColor.White;
        //            flagBlink = true;
        //        }
        //        else
        //        {
        //            tipoTerreno = terreno.getTipo();
        //            Console.BackgroundColor = this.terrenosconsola.getBackGroundColor(tipoTerreno);
        //            Console.ForegroundColor = this.terrenosconsola.getForeGroundColor(tipoTerreno);
        //            flagBlink = false;
        //        }

        //        topTerreno = this.mapaconsola.getTopTerreno(this.dimensionMapa, indiceTerreno);
        //        leftTerreno = this.mapaconsola.getLeftTerreno(this.dimensionMapa, indiceTerreno);

        //        foreach (string line in this.terrenosconsola.getAsciiTerreno(tipoTerreno))
        //        {
        //            Console.SetCursorPosition(leftTerreno, topTerreno);
        //            Console.Write(line);
        //            topTerreno++;
        //        }
        //        Console.ResetColor();

        //        indiceTerreno++;
        //    }
        //}

        private static string padDatos(string dato)
        {
            string datos = " " + dato;
            return datos.PadRight(10);
        }
    }
}
