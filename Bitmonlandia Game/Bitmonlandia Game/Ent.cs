using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmonlandia_Game
{
    public class Ent:Bitmon
    {
        public int maxTiempoVida = 15;
        public int minTiempoVida = 5;
        public int maxpuntosAtaque = 15;
        public int minpuntosAtaque = 5;
        public int maxpuntosVida = 80;
        public int minpuntosVida = 30;

        Random random = new Random();

        public Ent()
        {
            terrenosafin = new List<string>();
            terrenosafin.Add("Vegetacion");
            terrenosafin.Add("Desierto");

            terrenosdebil = new List<string>();
            terrenosdebil.Add("Volcan");
            terrenosdebil.Add("Nieve");

            enemigos = new Dictionary<string, double>();
            enemigos.Add("Gofue",0.5);

            bitmonsAfines = new List<string>();

            puntosVida = random.Next(minpuntosVida, maxpuntosVida+1);
            puntosVidaOriginal = puntosVida;
            tiempoVida = random.Next(minTiempoVida, maxTiempoVida+1);
            puntosAtaque = random.Next(minpuntosAtaque, maxpuntosAtaque+1);
            tipo = "Ent";
        }
        public override int[] moverse(Terreno terrenoActual, Johto region, int direccionMov)
        {
            int filaActual = terrenoActual.getFila();
            int colActual = terrenoActual.getColumna();
            return (new int[] { filaActual,colActual });
        }
        public override string getTipo()
        {
            return (tipo);
        }

        public override void breed(Johto region, Terreno terreno, Bitmon reproductor)
        {
            TiposBitmons tipos = new TiposBitmons();
            
            int randomBitmon = random.Next(0, 6);
            string tipoBitmon = tipos.tipo[randomBitmon];
            int randomTerrenoFila = random.Next(0, region.alto);
            int randomTerrenoCol = random.Next(0, region.ancho);

            if ((region.mapaRegion[randomTerrenoFila, randomTerrenoCol].getTipo() != "Volcan") && (region.mapaRegion[randomTerrenoFila, randomTerrenoCol].getTipo() != "Acuatico"))
            {
                region.mapaRegion[randomTerrenoFila, randomTerrenoCol].bitmonsTerreno.Add(new Ent());
                reproductor.tiempoVida += reproductor.tiempoVidaPerdido * 0.3;
                reproductor.cantidadDeHijos += 1;
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

