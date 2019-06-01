using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Bitmonlandia_Game
{
    public partial class Form_Simulacion : Form
    {
        public int config;
        public int mesesSimulacion;
        public Johto region;
        public PictureMapa[,] matriz;
        public UserControl[,] matrizConfigs;
        public Simulador simulador;
        public int contadorMeses = 0;
        public int cantidadVivos { get; set; }
        public int cantidadVivosInicial { get; set; }
        public int cantidadMuertos { get; set; }
        public GroupBox grupoMapa
        {
            get
            { return this.mapa; }
            set
            {
                mapa = value;
            }
        }

        public Button botonSimular
        {
            get
            { return this.button2; }
            set
            {
                button2 = value;
            }
        }
        

        Form_Bitmons popup = new Form_Bitmons();

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
           IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font fuente;

        public Form_Simulacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void Form_Simulacion_Load(object sender, EventArgs e)
        {

            byte[] fontData = Properties.Resources._04B_30__;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources._04B_30__.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources._04B_30__.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            fuente = new Font(fonts.Families[0], 16.0F);
            button2.Font = new Font(fonts.Families[0], 16, FontStyle.Regular);
            meses.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            mesesValor.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            bitmonsVivos.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            bitVivos.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            bitMuertos.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            muertos.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);

            mapaInicial(simulador);

            cantidadVivosInicial = simulador.bitmonsIniciales(this);
            bitmonsVivos.Text = cantidadVivosInicial.ToString();
        }

        public void mapaInicial(Simulador simulador)
        {
            string tipoTerreno;
            int numBitmons;
            matriz = new PictureMapa[simulador.dimensionMapa, simulador.dimensionMapa];
            int alto=0;
            int ancho=0;
            int offset = 0;
            panelBitmonsConfig1 panel1= null;
            panelBitmonsConfig2 panel2 = null;
            panelBitmonsConfig3 panel3 = null;

            switch (simulador.dimensionMapa)
            {
                case 3:
                    alto = 210;
                    ancho = 267;
                    offset = 133;
                    break;
                case 4:
                    alto = (int)157.5;
                    ancho = 200;
                    offset = 100;
                    break;
                case 5:
                    alto = 126;
                    ancho = 160;
                    offset = 80;
                    break;
                default:
                    break;
            }
            
            for (int i = 0; i < config; i++)
            {
                for (int j = 0; j < config; j++)
                {

                    mapa.SuspendLayout();

                    matriz[i, j] = new PictureMapa();
                    mapa.Controls.Add(matriz[i, j]);

                    switch (simulador.dimensionMapa)
                    {
                        case 3:
                            panel1 = new panelBitmonsConfig1();
                            mapa.Controls.Add(panel1);
                            //matrizConfigs[i, j] = panel1;
                            panel1.Width = offset;
                            panel1.Height = alto;
                            panel1.Top = i * alto;
                            panel1.Left = j * (ancho + offset) + ancho;
                            panel1.labelDorvalo.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Dorvalo").ToString();
                            panel1.labelDoti.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Doti").ToString();
                            panel1.labelEnt.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Ent").ToString();
                            panel1.labelGofue.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Gofue").ToString();
                            panel1.labelTaplan.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Taplan").ToString();
                            panel1.labelWetar.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Wetar").ToString();
                            panel1.BorderStyle = BorderStyle.FixedSingle;
                            panel1.Visible = true;
                            panel1.fila = i;
                            panel1.columna = j;
                            break;

                        case 4:
                            panel2 = new panelBitmonsConfig2();
                            mapa.Controls.Add(panel2);
                            //matrizConfigs[i, j] = panel2;
                            panel2.Width = offset;
                            panel2.Height = alto;
                            panel2.Top = i * alto;
                            panel2.Left = j * (ancho + offset) + ancho;
                            panel2.labelDorvalo.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Dorvalo").ToString();
                            panel2.labelDoti.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Doti").ToString();
                            panel2.labelEnt.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Ent").ToString();
                            panel2.labelGofue.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Gofue").ToString();
                            panel2.labelTaplan.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Taplan").ToString();
                            panel2.labelWetar.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Wetar").ToString();
                            panel2.BorderStyle = BorderStyle.FixedSingle;
                            panel2.Visible = true;
                            panel2.fila = i;
                            panel2.columna = j;
                            break;
                        case 5:
                            panel3 = new panelBitmonsConfig3();
                            mapa.Controls.Add(panel3);
                            //matrizConfigs[i, j] = panel3;
                            panel3.Width = offset;
                            panel3.Height = alto;
                            panel3.Top = i * alto;
                            panel3.Left = j * (ancho + offset) + ancho;
                            panel3.labelDorvalo.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Dorvalo").ToString();
                            panel3.labelDoti.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Doti").ToString();
                            panel3.labelEnt.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Ent").ToString();
                            panel3.labelGofue.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Gofue").ToString();
                            panel3.labelTaplan.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Taplan").ToString();
                            panel3.labelWetar.Text = simulador.region.mapaRegion[i, j].getNumTipoBitmon("Wetar").ToString();
                            panel3.BorderStyle = BorderStyle.FixedSingle;
                            panel3.Visible = true;
                            panel3.fila = i;
                            panel3.columna = j;
                            break;
                        default:
                            break;
                    }

                    matriz[i, j].Width = ancho;
                    matriz[i, j].Height = alto;
                    matriz[i, j].Top = i * alto;
                    matriz[i, j].Left = j * (ancho+offset);
                    matriz[i, j].BorderStyle = BorderStyle.FixedSingle;
                    matriz[i, j].Visible = true;
                    tipoTerreno = simulador.region.mapaRegion[i, j].getTipo();
                    numBitmons = simulador.region.mapaRegion[i, j].bitmonsTerreno.Count();

                    matriz[i, j].TipoTerreno = tipoTerreno;
                    matriz[i, j].numBitomns = numBitmons;
                    matriz[i, j].fila = i;
                    matriz[i, j].columna = j;

                    matriz[i, j].SizeMode = PictureBoxSizeMode.StretchImage;

                    switch (tipoTerreno)
                    {
                        case "Volcan":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.volcanDefinitivo;
                            break;

                        case "Nieve":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.nieveDefinitivo;
                            break;


                        case "Vegetacion":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.vegetacionDefinitivo;
                            break;


                        case "Acuatico":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.acuaticoDefinitivo;
                            break;


                        case "Desierto":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.desiertoDefinitivo;
                            break;

                        default:
                            break;

                    }
                    mapa.ResumeLayout();

                }
            }
           
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text!= "Finalizar")
            {
                if (contadorMeses == 0)
                {
                    button2.Enabled = false;
                    button2.Text = "Mes Siguiente";
                    simulador.simularMes(this);
                    button2.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                    simulador.simularMes(this);
                    button2.Enabled = true;
                }

            }
            else
            {
                Form_Estadisticas estadisticas = new Form_Estadisticas();
                simulador.mostrarEstadisticas(estadisticas);
                this.Close();
                var est = estadisticas.ShowDialog();   

                if (estadisticas.DialogResult == DialogResult.OK)
                {
                    estadisticas.Dispose();
                }

            }
            

            contadorMeses++;
            mesesValor.Text = contadorMeses.ToString();
            bitmonsVivos.Text = cantidadVivos.ToString();
            bitMuertos.Text = cantidadMuertos.ToString();
        }

    }
}
