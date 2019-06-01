using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Bitmonlandia_Game
{
    public partial class panelBitmonsConfig2 : UserControl
    {
        public int fila { get; set; }
        public int columna { get; set; }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
    IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font fuente1;

        public Label labelDorvalo
        {
            get
            { return this.lblDorv; }
            set
            {
                lblDorv = value;
            }
        }

        public Label labelGofue
        {
            get
            { return this.lblGofue; }
            set
            {
                lblGofue = value;
            }
        }

        public Label labelDoti
        {
            get
            { return this.lblDoti; }
            set
            {
                lblDoti = value;
            }
        }

        public Label labelEnt
        {
            get
            { return this.lblEnt; }
            set
            {
                lblEnt = value;
            }
        }

        public Label labelTaplan
        {
            get
            { return this.lblTaplan; }
            set
            {
                lblTaplan = value;
            }
        }

        public Label labelWetar
        {
            get
            { return this.lblWetar; }
            set
            {
                lblWetar = value;
            }
        }

        public panelBitmonsConfig2()
        {
            InitializeComponent();
        }

        private void panelBitmonsConfig2_Load(object sender, EventArgs e)
        {

            byte[] fontData = Properties.Resources._04B_30__;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources._04B_30__.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources._04B_30__.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            fuente1 = new Font(fonts.Families[0], 16.0F);

            lblDorv.Font = new Font(fonts.Families[0], 8, FontStyle.Regular);
            lblDoti.Font = new Font(fonts.Families[0], 8, FontStyle.Regular);
            lblEnt.Font = new Font(fonts.Families[0], 8, FontStyle.Regular);
            lblGofue.Font = new Font(fonts.Families[0],8, FontStyle.Regular);
            lblTaplan.Font = new Font(fonts.Families[0], 8, FontStyle.Regular);
            lblWetar.Font = new Font(fonts.Families[0], 8, FontStyle.Regular);
        }


    }
}
