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
    public partial class Form1 : Form

    {

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font fuente;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            byte[] fontData = Properties.Resources._04B_30__;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources._04B_30__.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources._04B_30__.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            fuente = new Font(fonts.Families[0], 16.0F);

            label1.Font =  new Font(fonts.Families[0] , 35, FontStyle.Regular);
            label2.Font = new Font(fonts.Families[0], 13, FontStyle.Regular);
            button1.Font= new Font(fonts.Families[0], 13, FontStyle.Regular);
            button2.Font= new Font(fonts.Families[0], 18, FontStyle.Regular);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}
