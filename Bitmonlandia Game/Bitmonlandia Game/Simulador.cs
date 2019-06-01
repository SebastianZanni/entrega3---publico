using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Windows.Forms;

namespace Bitmonlandia_Game
{
    public class Simulador
    {
        public int dimensionMapa;
        public Johto region;
        public int mesesSimulacion;
        Random random = new Random();
        Paraiso paraiso = new Paraiso();
       
        public Simulador(int dimensionMapa, Johto region, int mesesSimulacion)
        {
            this.dimensionMapa = dimensionMapa;
            this.region = region;
            this.mesesSimulacion = mesesSimulacion;
        }

        public int bitmonsIniciales(Form_Simulacion sender)
        {
            sender.cantidadVivosInicial = actualizarCantidadBitmonsMes();

            return sender.cantidadVivosInicial;
        }

        public void simularMes(Form_Simulacion sender)
        {
           //Loop principal
        //
                restarTiempoMensualVidaBitmons();
                validarMuertePorTiempoVidaBitmons();
                peleasBitmons();
                validarMuertePorPuntosVidaBitmons();
                breedBitmons();

                // Reproduccion de Ents cada 3 meses.
                if ((sender.contadorMeses + 1) % 3 == 0)
                {
                    breedEnts();
                }
                cambiarTipoTerrenos();
                moverBitmons();
                actualizarTerrenos(sender);
                actualizarDatosMapaBitmons(sender);
                
                sender.cantidadVivos= actualizarCantidadBitmonsMes();
                sender.cantidadMuertos = paraiso.Bithalla.Count();

                 if (sender.contadorMeses== mesesSimulacion-1)
                 {
                    sender.botonSimular.Text = "Finalizar";
                    //mostrarEstadisticas();
                 }
                
            //mostrarEstadisticas();
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


        public void actualizarTerrenos(Form_Simulacion sender)
        {
            sender.grupoMapa.SuspendLayout();
            foreach (PictureMapa picture in sender.matriz)
            {
                switch (region.mapaRegion[picture.fila, picture.columna].tipo)
                {
                    case "Volcan":
                        picture.Image = Bitmonlandia_Game.Properties.Resources.volcanDefinitivo;
                        picture.Refresh();
                        
                        break;

                    case "Nieve":
                        picture.Image = Bitmonlandia_Game.Properties.Resources.nieveDefinitivo;
                        picture.Refresh();
                        break;


                    case "Vegetacion":
                        picture.Image = Bitmonlandia_Game.Properties.Resources.vegetacionDefinitivo;
                        picture.Refresh();
                        break;


                    case "Acuatico":
                        picture.Image = Bitmonlandia_Game.Properties.Resources.acuaticoDefinitivo;
                        picture.Refresh();
                        break;


                    case "Desierto":
                        picture.Image = Bitmonlandia_Game.Properties.Resources.desiertoDefinitivo;
                        picture.Refresh();
                        break;

                    default:
                        break;

                }
            }
            sender.grupoMapa.ResumeLayout();
        }


        public void actualizarDatosMapaBitmons(Form_Simulacion sender)
        {
            switch (sender.simulador.dimensionMapa)
            {
                case 3:
                    foreach( Control  c in sender.grupoMapa.Controls)
                    {
                        if (c.GetType() == typeof(panelBitmonsConfig1))
                        {
                            panelBitmonsConfig1 panel1 =(panelBitmonsConfig1) c;
                            panel1.labelDorvalo.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Dorvalo").ToString();
                            panel1.labelDoti.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Doti").ToString();
                            panel1.labelEnt.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Ent").ToString();
                            panel1.labelGofue.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Gofue").ToString();
                            panel1.labelTaplan.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Taplan").ToString();
                            panel1.labelWetar.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Wetar").ToString();
                            panel1.Visible = true;
                        }
                        
                    }
                    
                    break;

                case 4:

                    foreach (Control c in sender.grupoMapa.Controls)
                    {
                        if (c.GetType() == typeof(panelBitmonsConfig2))
                        {
                            panelBitmonsConfig2 panel1 = (panelBitmonsConfig2)c;
                            panel1.labelDorvalo.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Dorvalo").ToString();
                            panel1.labelDoti.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Doti").ToString();
                            panel1.labelEnt.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Ent").ToString();
                            panel1.labelGofue.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Gofue").ToString();
                            panel1.labelTaplan.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Taplan").ToString();
                            panel1.labelWetar.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Wetar").ToString();
                            panel1.Visible = true;
                        }

                    }

                    break;
                case 5:
                    foreach (Control c in sender.grupoMapa.Controls)
                    {
                        if (c.GetType() == typeof(panelBitmonsConfig3))
                        {
                            panelBitmonsConfig3 panel1 = (panelBitmonsConfig3)c;
                            panel1.labelDorvalo.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Dorvalo").ToString();
                            panel1.labelDoti.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Doti").ToString();
                            panel1.labelEnt.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Ent").ToString();
                            panel1.labelGofue.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Gofue").ToString();
                            panel1.labelTaplan.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Taplan").ToString();
                            panel1.labelWetar.Text = sender.simulador.region.mapaRegion[panel1.fila, panel1.columna].getNumTipoBitmon("Wetar").ToString();
                            panel1.Visible = true;
                        }

                    }
                    break;
                default:
                    break;
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
        }


        public int actualizarCantidadBitmonsMes()
        {

            int contadorVivos = 0;

            foreach (Terreno terreno in region.mapaRegion)
            {
                foreach (Bitmon bitmon in terreno.bitmonsTerreno)
                {
                    contadorVivos += 1;
                }
            }
            return contadorVivos;
        }

        public void mostrarEstadisticas(Form_Estadisticas sender)
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

            //tiemposdevida
            sender.tiempoVidaProm = Math.Round(tiempoVidaPromedioBitmon, 2, MidpointRounding.AwayFromZero);
           
            sender.tiempoVidaPromDorv = Math.Round(tiempoVidaPromedioDorval, 2, MidpointRounding.AwayFromZero);
            sender.tiempoVidaPromDoti = Math.Round(tiempoVidaPromedioDoti, 2, MidpointRounding.AwayFromZero);
            sender.tiempoVidaPromGof=  Math.Round(tiempoVidaPromedioGofue, 2, MidpointRounding.AwayFromZero);
            sender.tiempoVidaPromEnt = Math.Round(tiempoVidaPromedioEnt, 2, MidpointRounding.AwayFromZero);
            sender.tiempoVidaPromTap = Math.Round(tiempoVidaPromedioTaplan, 2, MidpointRounding.AwayFromZero);
            sender.tiempoVidaPromWet = Math.Round(tiempoVidaPromedioWetar, 2, MidpointRounding.AwayFromZero);

        
            //Tasas de natalidad
            sender.tasaBrutaNatDorv = Math.Round(tasaBrutaNatalidadDorval, 2, MidpointRounding.AwayFromZero);
            sender.tasaBrutaNatDoti = Math.Round(tasaBrutaNatalidadDoti, 2, MidpointRounding.AwayFromZero);
            sender.tasaBrutaNatEnt = Math.Round(tasaBrutaNatalidadEnt, 2, MidpointRounding.AwayFromZero);
            sender.tasaBrutaNatGof = Math.Round(tasaBrutaNatalidadGofue, 2, MidpointRounding.AwayFromZero);
            sender.tasaBrutaNatTap = Math.Round(tasaBrutaNatalidadTaplan, 2, MidpointRounding.AwayFromZero);
            sender.tasaBrutaNatWet = Math.Round(tasaBrutaNatalidadWetar, 2, MidpointRounding.AwayFromZero);

            //Tasa de mortalidad
            sender.tasaBrutaMort = Math.Round(tasaBrutaMortalidad, 2, MidpointRounding.AwayFromZero);


            //Hijos
            sender.cantHijosDorv = Math.Round(hijosPromedioDorval, 2, MidpointRounding.AwayFromZero);
            sender.cantHijosDoti = Math.Round(hijosPromedioDoti, 2, MidpointRounding.AwayFromZero);
            sender.cantHijosEnt = Math.Round(hijosPromedioEnt, 2, MidpointRounding.AwayFromZero);
            sender.cantHijosGof = Math.Round(hijosPromedioGofue, 2, MidpointRounding.AwayFromZero);
            sender.cantHijosTap = Math.Round(hijosPromedioTaplan, 2, MidpointRounding.AwayFromZero);
            sender.cantHijosWet = Math.Round(hijosPromedioWetar, 2, MidpointRounding.AwayFromZero);
            

            //Extintos
            if (cantidadVivosDorval == 0)
            {
                sender.extintos+= "Dorvalos \n";
            }
            if (cantidadVivosDoti == 0)
            {
                sender.extintos += "Doti \n";
            }
            if (cantidadVivosEnt == 0)
            {
                sender.extintos += "Ent \n";
            }
            if (cantidadVivosGofue == 0)
            {
                sender.extintos += "Gofue \n";
            }
            if (cantidadVivosTaplan == 0)
            {
                sender.extintos += "Taplan \n";
            }
            if (cantidadVivosWetar == 0)
            {
                sender.extintos += "Wetar \n";
            }

            Console.WriteLine("\n");

            Console.WriteLine("Poblacion de Bitmons en Bithalla:");
            Console.WriteLine("****************************************");
            sender.cantidadMuertosDorv = cantidadMuertosDorval;
            sender.cantidadMuertosDoti = cantidadMuertosDoti;
            sender.cantidadMuertosEnt = cantidadMuertosEnt;
            sender.cantidadMuertosGof = cantidadMuertosGofue;
            sender.cantidadMuertosTap = cantidadMuertosTaplan;
            sender.cantidadMuertosWet = cantidadMuertosWetar;

            sender.porcentajeBitDorv = Math.Round(((cantidadMuertosDorval / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero);
            sender.porcentajeBitDoti = Math.Round(((cantidadMuertosDoti / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero);
            sender.porcentajeBitEnt = Math.Round(((cantidadMuertosEnt / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero);
            sender.porcentajeBitGof = Math.Round(((cantidadMuertosGofue / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero);
            sender.porcentajeBitTap = Math.Round(((cantidadMuertosTaplan / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero);
            sender.porcentajeBitWet = Math.Round(((cantidadMuertosWetar / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero);

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

            file = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write);
            writer = new StreamWriter(file);


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
            writer.WriteLine("ESTADISTICAS SIMULACION");
            writer.WriteLine("");
            writer.WriteLine("");
            writer.WriteLine("Tiempo vida promedio de Bitmon: " + Math.Round(tiempoVidaPromedioBitmon, 2, MidpointRounding.AwayFromZero) + " meses");
            writer.WriteLine("***************************************\n");
            writer.WriteLine("");
            writer.WriteLine("Tiempo vida promedio por especie Bitmon:");
            writer.WriteLine("***************************************");
            writer.WriteLine("Dorvalo: " + Math.Round(tiempoVidaPromedioDorval, 2, MidpointRounding.AwayFromZero) + " meses");
            writer.WriteLine("Doti: " + Math.Round(tiempoVidaPromedioDoti, 2, MidpointRounding.AwayFromZero) + " meses");
            writer.WriteLine("Ent: " + Math.Round(tiempoVidaPromedioEnt, 2, MidpointRounding.AwayFromZero) + " meses");
            writer.WriteLine("Gofue: " + Math.Round(tiempoVidaPromedioGofue, 2, MidpointRounding.AwayFromZero) + " meses");
            writer.WriteLine("Taplan: " + Math.Round(tiempoVidaPromedioTaplan, 2, MidpointRounding.AwayFromZero) + " meses");
            writer.WriteLine("Wetar: " + Math.Round(tiempoVidaPromedioWetar, 2, MidpointRounding.AwayFromZero) + " meses \n\n");
            writer.WriteLine("");
            writer.WriteLine("Tasa bruta de natalidad por cada especie:");
            writer.WriteLine("****************************************");
            writer.WriteLine("Dorvalo: " + Math.Round(tasaBrutaNatalidadDorval, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Dorvalo");
            writer.WriteLine("Doti: " + Math.Round(tasaBrutaNatalidadDoti, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Doti");
            writer.WriteLine("Ent: " + Math.Round(tasaBrutaNatalidadEnt, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Ent");
            writer.WriteLine("Gofue: " + Math.Round(tasaBrutaNatalidadGofue, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Gofue");
            writer.WriteLine("Taplan: " + Math.Round(tasaBrutaNatalidadTaplan, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Taplan");
            writer.WriteLine("Wetar: " + Math.Round(tasaBrutaNatalidadWetar, 2, MidpointRounding.AwayFromZero) + " hijos por cada 100 Wetar\n\n");
            writer.WriteLine("");
            writer.WriteLine("Tasa bruta de mortalidad. " + Math.Round(tasaBrutaMortalidad, 2, MidpointRounding.AwayFromZero) + " por cada 100 Bitmons");
            writer.WriteLine("***************************************\n");
            writer.WriteLine("");
            writer.WriteLine("Cantidad hijos promedio por cada especie:");
            writer.WriteLine("****************************************");
            writer.WriteLine("Dorval: " + Math.Round(hijosPromedioDorval, 2, MidpointRounding.AwayFromZero));
            writer.WriteLine("Doti: " + Math.Round(hijosPromedioDoti, 2, MidpointRounding.AwayFromZero));
            writer.WriteLine("Ent: " + Math.Round(hijosPromedioEnt, 2, MidpointRounding.AwayFromZero));
            writer.WriteLine("Gofue: " + Math.Round(hijosPromedioGofue, 2, MidpointRounding.AwayFromZero));
            writer.WriteLine("Taplan: " + Math.Round(hijosPromedioTaplan, 2, MidpointRounding.AwayFromZero));
            writer.WriteLine("Wetar: " + Math.Round(hijosPromedioWetar, 2, MidpointRounding.AwayFromZero) + "\n\n");

            writer.WriteLine("");
            writer.WriteLine("Especies exintas:");
            writer.WriteLine("****************");
            if (cantidadVivosDorval == 0)
            {
                writer.WriteLine("Dorvalos");
            }
            if (cantidadVivosDoti == 0)
            {
                writer.WriteLine("Doti");
            }
            if (cantidadVivosEnt == 0)
            {
                writer.WriteLine("Ent");
            }
            if (cantidadVivosGofue == 0)
            {
                writer.WriteLine("Gofue");
            }
            if (cantidadVivosTaplan == 0)
            {
                writer.WriteLine("Taplan");
            }
            if (cantidadVivosWetar == 0)
            {
                writer.WriteLine("Wetar");
            }

            writer.WriteLine("");

            writer.WriteLine("Poblacion de Bitmons en Bithalla:");
            writer.WriteLine("****************************************");
            writer.WriteLine("Dorval: " + (cantidadMuertosDorval) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosDorval / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            
            writer.WriteLine("Doti: " + (cantidadMuertosDoti) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosDoti / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            writer.WriteLine("Ent: " + (cantidadMuertosEnt) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosEnt / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            writer.WriteLine("Gofue: " + (cantidadMuertosGofue) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosGofue / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            writer.WriteLine("Taplan: " + (cantidadMuertosTaplan) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosTaplan / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            writer.WriteLine("Wetar: " + (cantidadMuertosWetar) + " --> Equivalentes al: " + Math.Round(((cantidadMuertosWetar / totalBitmonsMuertos) * 100), 1, MidpointRounding.AwayFromZero) + " %");
            writer.Close();
            file.Close();

        }



    }
}
