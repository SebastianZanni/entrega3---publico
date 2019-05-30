using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmonlandia_Game
{
    public class Dorvalo:Bitmon
    {
        public int maxTiempoVida = 12;
        public int minTiempoVida = 3;
        public int maxpuntosAtaque = 50;
        public int minpuntosAtaque = 20;
        public int maxpuntosVida = 30;
        public int minpuntosVida = 15;
         Random random = new Random();

        public Dorvalo() 
        {
            terrenosafin = new List<string>();
            terrenosdebil = new List<string>();
            terrenosdebil.Add("Nieve");

            enemigos = new Dictionary<string, double>();
            enemigos.Add("Dorvalo",1.0);
            enemigos.Add("Wetar", 1.0);

            bitmonsAfines = new List<string>();
            bitmonsAfines.Add("Dorvalo");
            bitmonsAfines.Add("Gofue");
            bitmonsAfines.Add("Doti");

            puntosVida = random.Next(minpuntosVida, maxpuntosVida+1);
            puntosVidaOriginal = puntosVida;
            tiempoVida = random.Next(minTiempoVida, maxTiempoVida+1);
            puntosAtaque = random.Next(minpuntosAtaque, maxpuntosAtaque+1);
        }
        public override int[] moverse(Terreno terrenoActual, Johto region,int direccionMov)
        {

            int filaActual = terrenoActual.getFila();
            int colActual = terrenoActual.getColumna();
            int filaNueva = 0;
            int colNueva = 0;

            switch (direccionMov)
            {
                // Se mueve al terreno de arriba
                case 0:
                    filaNueva = filaActual - 2;
                    colNueva = colActual;
                    break;

                // Se mueve al terreno de la deracha
                case 1:
                    colNueva = colActual + 2;
                    filaNueva = filaActual;
                    break;

                // Se mueve al terreno de abajo
                case 2:
                    filaNueva = filaActual + 2;
                    colNueva = colActual;
                    break;
                // Se mueve al terreno de la izquierda
                case 3:
                    colNueva = colActual - 2;
                    filaNueva = filaActual;
                    break;
                // Se mantiene en el mismo terreno
                case 4:
                    colNueva = colActual;
                    filaNueva = filaActual;
                    break;
                default:
                    colNueva = colActual;
                    filaNueva = filaActual;
                    break;

            }

            //Valida si se puede mover o no , dependiendo de las dimensiones del mapa.
            if (puedeMoverse(colNueva, filaNueva, direccionMov, region) == true)
            {
                return (new int[] { filaNueva, colNueva });
            }
            else
            {
                return (new int[] { filaActual, colActual });
            }

        }

        private bool puedeMoverse(int colNueva, int filaNueva, int direccionMov, Johto region)
        {

            bool retorno = true;
            switch (direccionMov)
            {
                case 0:
                    if (filaNueva < 0)
                    {
                        retorno = false;
                    }
                    break;

                case 1:
                    if (colNueva / region.ancho > 0)
                    {
                        retorno = false;
                    }
                    break;
                case 2:
                    if (filaNueva / region.alto > 0)
                    {
                        retorno = false;
                    }
                    break;
                case 3:
                    if (colNueva < 0)
                    {
                        retorno = false;
                    }
                    break;
                case 4:
                    retorno = false;
                    break;

                default:
                    retorno = false;
                    break;
            }
            return retorno;
        }
        public override string getTipo()
        {
            return ("Dorvalo");
        }

        public override void breed(Johto region, Terreno terreno, Bitmon reproductor)
        {
            TiposBitmons tipos = new TiposBitmons();
            foreach (Bitmon bitmon in terreno.bitmonsTerreno)
            {
                if (bitmon != reproductor)
                {
                    if ((reproductor.bitmonsAfines.Contains(bitmon.getTipo()) && bitmon.bitmonsAfines.Contains(reproductor.getTipo())))
                    {

                        reproductor.yaSeReprodujo = true;
                        bitmon.yaSeReprodujo = true;

                        if (bitmon.cantidadDeHijos == reproductor.cantidadDeHijos)
                        {
                            int randomBitmon = random.Next(0, 6);
                            string tipoBitmon = tipos.tipo[randomBitmon];
                            int randomTerrenoFila = random.Next(0, region.alto );
                            int randomTerrenoCol = random.Next(0, region.ancho );

                            switch (tipoBitmon)
                            {
                                case "Dorvalo":
                                    region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Dorvalo());   
                                    break;
                                case "Doti":
                                    region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Doti());
                                    break;
                                case "Ent":
                                    break;

                                case "Gofue":
                                    region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Gofue());
                                    break;
                                case "Taplan":
                                    region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Taplan());
                                    break;
                                case "Wetar":
                                    if (region.mapaRegion[randomTerrenoFila, randomTerrenoCol].getTipo() != "Acuatico")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Wetar());
                                        break;
                                    }
                            }
                            bitmon.cantidadDeHijos += 1;
                            reproductor.cantidadDeHijos += 1;

                        }

                        else
                        {
                            int randomBitmon = random.Next(0, 7);
                            if (randomBitmon == 6)
                            {
                                if (reproductor.cantidadDeHijos > bitmon.cantidadDeHijos)
                                {
                                    foreach (KeyValuePair<int, string> pair in tipos.tipo)
                                    {
                                        if (pair.Value == reproductor.getTipo())
                                        {
                                            randomBitmon = pair.Key;
                                        }
                                    }
                                }

                                else if (reproductor.cantidadDeHijos < bitmon.cantidadDeHijos)
                                {
                                    foreach (KeyValuePair<int, string> pair in tipos.tipo)
                                    {
                                        if (pair.Value == bitmon.getTipo())
                                        {
                                            randomBitmon = pair.Key;
                                        }
                                    }
                                }
                            }

                            string tipoBitmon = tipos.tipo[randomBitmon];
                            int randomTerrenoFila = random.Next(0, region.alto);
                            int randomTerrenoCol = random.Next(0, region.ancho);

                            switch (tipoBitmon)
                            {
                                case "Dorvalo":
                                    region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Dorvalo());
                                    break;
                                case "Doti":
                                    region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Doti());
                                    break;
                                case "Ent":
                                    break;

                                case "Gofue":
                                    region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Gofue());
                                    break;
                                case "Taplan":
                                    region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Taplan());
                                    break;
                                case "Wetar":
                                    if (region.mapaRegion[randomTerrenoFila, randomTerrenoCol].getTipo() != "Acuatico")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Wetar());
                                        break;
                                    }
                            }
                            bitmon.cantidadDeHijos += 1;
                            reproductor.cantidadDeHijos += 1;
                        }

                        reproductor.yaSeReprodujo = true;
                        reproductor.tiempoVida += reproductor.tiempoVidaPerdido * 0.3;
                        bitmon.yaSeReprodujo = true;
                        bitmon.tiempoVida += bitmon.tiempoVidaPerdido * 0.3;

                        break;
                    }
                }
            }
        }

        public override void pelear(Terreno terreno, Bitmon peleador)
        {
            foreach (Bitmon bitmon in terreno.bitmonsTerreno)
            {
                if (bitmon != peleador && bitmon.puntosVida > 0 && peleador.puntosVida > 0)
                {
                    if ((peleador.enemigos.ContainsKey(bitmon.getTipo()) && bitmon.enemigos.ContainsKey(peleador.getTipo())))
                    {
                        double multiplicadorPeleador = peleador.enemigos[bitmon.getTipo()];
                        double multiplicadorBitmon = bitmon.enemigos[peleador.getTipo()];

                        bitmon.puntosVida -= peleador.puntosAtaque * multiplicadorPeleador;
                        bitmon.puntosVida -= peleador.puntosAtaque * multiplicadorPeleador;

                        peleador.yaPeleo = true;
                        bitmon.yaPeleo = true;

                        if (bitmon.puntosVida < 0 && peleador.puntosVida > 0)
                        {
                            peleador.puntosVida = peleador.puntosVidaOriginal;
                        }

                        else if (bitmon.puntosVida > 0 && peleador.puntosVida < 0)
                        {
                            bitmon.puntosVida = bitmon.puntosVidaOriginal;
                        }

                        break;
                    }
                }
            }
        }

        public override void transformarTerreno(Terreno terreno)
        {

        }
    }

}
