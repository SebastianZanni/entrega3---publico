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
    public partial class Form_Estadisticas : Form
    {
        public double tiempoVidaProm = 0;
        public double tiempoVidaPromDorv = 0;
        public double tiempoVidaPromDoti = 0;
        public double tiempoVidaPromEnt = 0;
        public double tiempoVidaPromGof = 0;
        public double tiempoVidaPromTap = 0;
        public double tiempoVidaPromWet = 0;
        public double tasaBrutaNatDorv = 0;
        public double tasaBrutaNatDoti = 0;
        public double tasaBrutaNatEnt = 0;
        public double tasaBrutaNatGof = 0;
        public double tasaBrutaNatTap = 0;
        public double tasaBrutaNatWet = 0;
        public double tasaBrutaMort = 0;
        public double cantHijosDorv = 0;
        public double cantHijosDoti = 0;
        public double cantHijosEnt = 0;
        public double cantHijosGof = 0;
        public double cantHijosTap = 0;
        public double cantHijosWet = 0;
        public string extintos = "";
        public double cantidadMuertosDorv = 0;
        public double cantidadMuertosDoti = 0;
        public double cantidadMuertosEnt = 0;
        public double cantidadMuertosGof = 0;
        public double cantidadMuertosTap = 0;
        public double cantidadMuertosWet = 0;
        public double porcentajeBitDorv = 0;
        public double porcentajeBitDoti = 0;
        public double porcentajeBitEnt = 0;
        public double porcentajeBitGof = 0;
        public double porcentajeBitTap = 0;
        public double porcentajeBitWet = 0;


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font fuente;
        Font fuente1;

        public Form_Estadisticas()
        {
            InitializeComponent();
        }

        private void Form_Estadisticas_Load(object sender, EventArgs e)
        {
            byte[] fontData = Properties.Resources._04B_30__;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources._04B_30__.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources._04B_30__.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            fuente = new Font(fonts.Families[0], 16.0F);

            byte[] fontData1 = Properties.Resources.Bit;
            IntPtr fontPtr1 = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData1.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData1, 0, fontPtr1, fontData1.Length);
            uint dummy1 = 0;
            fonts.AddMemoryFont(fontPtr1, Properties.Resources.Bit.Length);
            AddFontMemResourceEx(fontPtr1, (uint)Properties.Resources.Bit.Length, IntPtr.Zero, ref dummy1);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr1);

            fuente1 = new Font(fonts.Families[1], 16.0F);

            button1.Font= new Font(fonts.Families[1], 20, FontStyle.Regular);

            // Fuentes labels no variables
            label1.Font = new Font(fonts.Families[0], 55, FontStyle.Regular);
            label2.Font= new Font(fonts.Families[1], 15, FontStyle.Regular);
            Dorvalo.Font= new Font(fonts.Families[1], 15, FontStyle.Regular);
            Doti.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Ent.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Gofue.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            taplan.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Wetar.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Dorvalo2.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Doti2.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Ent2.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Gofue2.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Taplan2.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            Wetar2.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            dorvalo3.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            doti3.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            ent3.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            gofue3.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            taplan3.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            wetar3.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            dorvalo4.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            doti4.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            ent4.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            gofue4.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            taplan4.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            wetar4.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);

            //Prom Vida
            lbltiemposVidaTitulo.Font= new Font(fonts.Families[0], 15, FontStyle.Regular);
            lblPromediosVida.Font= new Font(fonts.Families[1],15, FontStyle.Regular);

            lblvidaProm.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblvidaProm.Text = tiempoVidaProm.ToString();

            lblpromVidaDorv.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblpromVidaDorv.Text = tiempoVidaPromDorv.ToString();

            lblpromVidaDoti.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblpromVidaDoti.Text = tiempoVidaPromDoti.ToString();

            lblpromvidaEnt.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblpromvidaEnt.Text = tiempoVidaPromEnt.ToString();

            lblpromvidaGof.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblpromvidaGof.Text = tiempoVidaPromGof.ToString();

            lblpromvidaTap.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblpromvidaTap.Text = tiempoVidaPromTap.ToString();

            lblpromvidaWet.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblpromvidaWet.Text = tiempoVidaPromWet.ToString();

            //natalidad
            titulo2.Font = new Font(fonts.Families[0], 15, FontStyle.Regular);
        
            lblBrutaNataDorv.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblBrutaNataDorv.Text = tasaBrutaNatDorv.ToString();

            lblBrutaNataDoti.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblBrutaNataDoti.Text = tasaBrutaNatDoti.ToString();

            lblBrutaNataEnt.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblBrutaNataEnt.Text = tasaBrutaNatEnt.ToString();

            lblBrutaNataGofue.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblBrutaNataGofue.Text = tasaBrutaNatGof.ToString();

            lblBrutaNataTap.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblBrutaNataTap.Text = tasaBrutaNatTap.ToString();

            lblBrutaNataWet.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblBrutaNataWet.Text = tasaBrutaNatWet.ToString();

            //Mortalidad
            titulo3.Font= new Font(fonts.Families[0], 15, FontStyle.Regular);
            lbltasaBrutaMort.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lbltasaBrutaMort.Text = tasaBrutaMort.ToString();

            //Hijos
            titulo4.Font = new Font(fonts.Families[0], 15, FontStyle.Regular);

            lblhijosDorv.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblhijosDorv.Text = cantHijosDorv.ToString();

            lblhijosDoti.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblhijosDoti.Text = cantHijosDoti.ToString();

            lblhijosEnt.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblhijosEnt.Text = cantHijosEnt.ToString();

            lblhijosGofue.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblhijosGofue.Text = cantHijosGof.ToString();

            lblhijosTap.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblhijosTap.Text = cantHijosTap.ToString();

            lblhijosWet.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblhijosWet.Text = cantHijosWet.ToString();

            //Extintos
            titulo5.Font = new Font(fonts.Families[0], 15, FontStyle.Regular);
            lblextintas.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblextintas.Text = extintos.ToString();

            // Muertos Bithalla
            titulo6.Font = new Font(fonts.Families[0], 15, FontStyle.Regular);

            lblcantBithallaDorv.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblcantBithallaDorv.Text = cantidadMuertosDorv.ToString();
            lblcantBithallaDorv.Text += " --> "+porcentajeBitDorv+" %";
            

            lblcantBithallaDoti.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblcantBithallaDoti.Text = cantidadMuertosDoti.ToString();
            lblcantBithallaDoti.Text += " --> " + porcentajeBitDoti + " %";

            lblcantBithallaEnt.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblcantBithallaEnt.Text = cantidadMuertosEnt.ToString();
            lblcantBithallaEnt.Text += " --> " + porcentajeBitEnt + " %";

            lblcantBithallaGofue.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblcantBithallaGofue.Text = cantidadMuertosGof.ToString();
            lblcantBithallaGofue.Text += " --> " + porcentajeBitGof + " %";

            lblcantBithallaTaplan.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblcantBithallaTaplan.Text = cantidadMuertosTap.ToString();
            lblcantBithallaTaplan.Text += " --> " + porcentajeBitTap + " %";

            lblcantBithallaWet.Font = new Font(fonts.Families[1], 15, FontStyle.Regular);
            lblcantBithallaWet.Text = cantidadMuertosWet.ToString();
            lblcantBithallaWet.Text += " --> " + porcentajeBitWet + " %";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbltasaBrutaMort_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
