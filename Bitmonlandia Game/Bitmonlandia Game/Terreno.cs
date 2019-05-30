using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmonlandia_Game
{
    public class Terreno

    {
        public List<Bitmon> bitmonsTerreno;
        public string tipo;
        public int fila;
        public int columna;

        public Terreno(string tipo)
        {
            bitmonsTerreno = new List<Bitmon>();
            this.tipo = tipo;
        }
        public int numBitmons()
        {
            return bitmonsTerreno.Count;
        }

      public string getTipo()
        {
            return tipo;
        }
        public string getTipoAbrev()
        {
            return tipo.Substring(0, 2);
        }

        public int getNumTipoBitmon(string tipoBitmon)
        {
            int contador = 0;
            foreach (Bitmon bitmon in bitmonsTerreno)
            {
                if (bitmon.getTipo() == tipoBitmon)
                {
                    contador++;
                }
            }
            return contador;
        }
        public void setTipo(string tipoTerreno)
        {
            tipo = tipoTerreno;
        }

        public int getFila()
        {
            return fila;
        }

        public int getColumna()
        {
            return columna;
        }

        public void setFila(int fila)
        {
            this.fila = fila;

        }

        public void setColumna(int columna)
        {
            this.columna = columna;
        }
    }
}

