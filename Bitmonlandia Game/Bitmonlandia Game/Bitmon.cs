using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmonlandia_Game
{
    public abstract class Bitmon
    {
        public double tiempoVida;
        public double tiempoVidaPerdido=0;
        public int mesesDeVida = 0;
        public double puntosVida;
        public double puntosVidaOriginal;
        public int puntosAtaque;
        public string tipo;
        public bool seMovio = false;
        public List<string> terrenosafin;
        public List<string> terrenosdebil;
        public bool yaPeleo = false;
        public Dictionary<string, double> enemigos;
        public List<string> bitmonsAfines;
        public int cantidadDeHijos= 0;
        public bool yaSeReprodujo = false;
    
        public Bitmon()
        {

        }

        public abstract int[] moverse(Terreno terreno,Johto region,int direccionMov);
        public abstract void pelear(Terreno terreno,Bitmon peleador);
        public abstract void breed(Johto region, Terreno terreno, Bitmon reproductor);
        public abstract string getTipo();
        public abstract void transformarTerreno(Terreno terreno);
    }
}
