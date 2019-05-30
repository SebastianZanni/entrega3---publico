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

    public partial class Form_Opciones : Form
    {

        public string valorRetorno { get; set; }
        public int meses { get; set; }
        public int configSeleccionada { get; set; }
        int mesesTextBox;

        public PictureMapa[,] matriz;
        public Johto configuracion1;
        public Johto configuracion2;
        public Johto configuracion3;
        public Johto configSeleccionadaGlobal;

        public void inicializar(int config, int tamaño)
        {
            // Eliminar elementos en cada pasada
            Johto configSeleccionada;
            string tipoTerreno;
            int numBitmons;

            switch (config)
            {
                case 3:
                    configSeleccionada = configuracion1;
                    break;
                case 4:
                    configSeleccionada = configuracion2;
                    break;
                case 5:
                    configSeleccionada = configuracion3;
                    break;
                default:
                    configSeleccionada = configuracion1;
                    break;
            }

            configSeleccionadaGlobal = configSeleccionada;

            int c = matrizMapa.Controls.Count;
            for (int i = c - 1; i >= 0; i--)
            {
                matrizMapa.Controls.Remove(matrizMapa.Controls[i]);
            }

            matriz = new PictureMapa[config, config];

            for (int i = 0; i < config; i++)
            {
                for (int j = 0; j < config; j++)
                {
                    matriz[i, j] = new PictureMapa();
                    matrizMapa.Controls.Add(matriz[i, j]);
                    matriz[i, j].Width = tamaño;
                    matriz[i, j].Height = tamaño;
                    matriz[i, j].Top = i*tamaño;
                    matriz[i, j].Left = j*tamaño;
                    matriz[i, j].BorderStyle = BorderStyle.FixedSingle;
                    matriz[i, j].Visible = true;
                    tipoTerreno = configSeleccionada.mapaRegion[i, j].getTipo();
                    numBitmons = configSeleccionada.mapaRegion[i, j].bitmonsTerreno.Count();

                    matriz[i, j].TipoTerreno = tipoTerreno;
                    matriz[i, j].numBitomns = numBitmons;

                    matriz[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    switch (tipoTerreno)
                    {
                        case "Volcan":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.volcanDef;
                            break;
                          
                        case "Nieve":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.nieveDef2;
                            break;


                        case "Vegetacion":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.vegDef;
                            break;


                        case "Acuatico":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.acuaticoDef;
                            break;


                        case "Desierto":
                            matriz[i, j].Image = Bitmonlandia_Game.Properties.Resources.desiertoDef;
                            break;

                        default:
                            break;
                            
                    }
                    matriz[i, j].Paint += newPicturePaint;

                }
            }
        }
    
        

        private void newPicturePaint(object sender, PaintEventArgs e)
        {

            using (Font myFont = new Font(fonts.Families[0], 20, FontStyle.Bold))
            {
                var pictureBox = (PictureMapa)sender;

                switch (pictureBox.TipoTerreno)
                {
                    case "Volcan":
                        e.Graphics.DrawString(pictureBox.numBitomns.ToString(), myFont, Brushes.Yellow, new Point(2, 2));
                        break;

                    case "Nieve":
                        e.Graphics.DrawString(pictureBox.numBitomns.ToString(), myFont, Brushes.Yellow, new Point(2, 2));
                        break;
                    case "Acuatico":
                        e.Graphics.DrawString(pictureBox.numBitomns.ToString(), myFont, Brushes.Yellow, new Point(2, 2));
                        break;
                    case "Desierto":
                        e.Graphics.DrawString(pictureBox.numBitomns.ToString(), myFont, Brushes.Yellow, new Point(2, 2));
                        break;
                    case "Vegetacion":
                        e.Graphics.DrawString(pictureBox.numBitomns.ToString(), myFont, Brushes.Yellow, new Point(2, 2));
                        break;

                    default:
                        break;
                }
               
            }

        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font fuente1;


        public Form_Opciones()
        {
            InitializeComponent();
            
        }


        private void Form_meses_simulacion_Load(object sender, EventArgs e)
        {
            byte[] fontData = Properties.Resources.Bit;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Bit.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Bit.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            fuente1 = new Font(fonts.Families[0], 16.0F);

            label1.Font = new Font(fonts.Families[0], 20, FontStyle.Regular);
            label2.Font= new Font(fonts.Families[0], 20, FontStyle.Regular);
            button1.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            button2.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            comboBox1.Font= new Font(fonts.Families[0], 12, FontStyle.Regular);
            textBox1.Font= new Font(fonts.Families[0], 12, FontStyle.Regular);
            comboBox1.Items.Add("Configuracion 1");
            comboBox1.Items.Add("Configuracion 2");
            comboBox1.Items.Add("Configuracion 3");
            comboBox1.SelectedIndex = 0;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Text = 1.ToString() ;

        }

        private void button1_Click(object sender, EventArgs e)

        {

            
            if ( textBox1.Text== "")
            {
                MessageBox.Show("Debes ingresar un numero para continuar!","Error");
                

            }
            else if ( int.TryParse(textBox1.Text, out mesesTextBox)== false)
            {
                MessageBox.Show("No puedes ingresar letras!","Error");
            }
            else if (int.TryParse(textBox1.Text, out mesesTextBox) == true)
            {
                this.meses = mesesTextBox;
                this.valorRetorno = "Continue";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.valorRetorno = "Menu";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
      
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = comboBox1.SelectedItem.ToString();
            switch (item) {
                case "Configuracion 1":
                    this.configSeleccionada = 1;
                    inicializar(3,120);
                    break;
                case "Configuracion 2":
                    this.configSeleccionada = 2;
                    inicializar(4,90);
                    break;
                case "Configuracion 3":
                    this.configSeleccionada = 3;
                    inicializar(5,72);
                    break;
                default:
                    this.configSeleccionada = 1;
                    break;
            }
        }

 
    }
}
