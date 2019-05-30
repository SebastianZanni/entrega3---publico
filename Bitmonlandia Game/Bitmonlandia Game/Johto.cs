using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmonlandia_Game
{
    public class Johto
    {

        public List<Terreno> terrenos;
        public int dimensionRegion;
        public int alto;
        public int ancho;
        Random random = new Random();
        public Terreno[,] mapaRegion;

        public Johto(int dimensionRegion, int alto, int ancho)
        {
            this.dimensionRegion = dimensionRegion;
            this.ancho = ancho;
            this.alto = alto;
            terrenos = new List<Terreno>();
            terrenos.Add(new Terreno("Volcan"));
            terrenos.Add(new Terreno("Acuatico"));
            terrenos.Add(new Terreno("Vegetacion"));
            terrenos.Add(new Terreno("Desierto"));
            terrenos.Add(new Terreno("Nieve"));
            mapaRegion = new Terreno[ancho, alto];
        }

        public void crearMapa()
        {

            for (int i = 0; i < mapaRegion.GetLength(0); i++)
            {
                for (int j = 0; j < mapaRegion.GetLength(1); j++)
                {
                    int num = terrenos.Count();
                    int terrenoRandom = random.Next(0, num);

                    Terreno terrenonuevo = new Terreno(terrenos[terrenoRandom].getTipo());
                    terrenonuevo.setColumna(j);
                    terrenonuevo.setFila(i);
                    mapaRegion[i, j] = terrenonuevo;
                }
            }
        }


        public string getTipoTerreno(int x, int y)
        {
            Terreno tmpterreno = mapaRegion[x, y];
            return tmpterreno.getTipo();
        }

        public void crearBitmons()
        {

            TiposBitmons tipos = new TiposBitmons();
            foreach (Terreno terrenoRegion in mapaRegion)
            {
                int numBitmons = random.Next(0,11);
                {

                    for (int i = 0; i < numBitmons; i++)
                    {
                        int randomBitmon = random.Next(0, 6);
                        string tipoBitmon = tipos.tipo[randomBitmon];

                        switch (tipoBitmon)
                        {
                            case "Dorvalo":
                                terrenoRegion.bitmonsTerreno.Add(new Dorvalo());
                                break;
                            case "Doti":
                                terrenoRegion.bitmonsTerreno.Add(new Doti());
                                break;
                            case "Ent":
                                if ((terrenoRegion.getTipo() == "Volcan") || (terrenoRegion.getTipo() == "Acuatico"))
                                {
                                    continue;
                                }

                                else
                                {
                                    terrenoRegion.bitmonsTerreno.Add(new Ent());
                                    break;
                                }

                            case "Gofue":
                                terrenoRegion.bitmonsTerreno.Add(new Gofue());
                                break;
                            case "Taplan":
                                terrenoRegion.bitmonsTerreno.Add(new Taplan());
                                break;
                            case "Wetar":
                                if (terrenoRegion.getTipo() != "Acuatico")
                                {
                                    continue;
                                }
                                else
                                {
                                    terrenoRegion.bitmonsTerreno.Add(new Wetar());
                                    break;
                                }
                        }
                    }


                }
            }

        }
    }
}
