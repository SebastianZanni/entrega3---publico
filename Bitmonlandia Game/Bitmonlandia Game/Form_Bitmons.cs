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
    public partial class Form_Bitmons : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
           IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font fuente1;


        public Form_Bitmons()
        {
            InitializeComponent();
        }

        private void picDorvalo_Click(object sender, EventArgs e)
        {

        }

        private void lblDorv_Click(object sender, EventArgs e)
        {

        }

        private void Form_Bitmons_Load(object sender, EventArgs e)
        {
            byte[] fontData = Properties.Resources.Bit;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Bit.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Bit.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            fuente1 = new Font(fonts.Families[0], 16.0F);

            lblDorv.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            lblDoti.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            lblEnt.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            lblGofue.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            lblTaplan.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            lblWetar.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);

        }
    }
}
