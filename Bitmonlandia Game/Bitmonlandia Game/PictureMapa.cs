using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bitmonlandia_Game
{
    public class PictureMapa : PictureBox
    {
        public string TipoTerreno { get; set; }
        public int numBitomns { get; set; }
        public int fila {get;set; }
        public int columna { get; set; }

        public void Agrandar()
        {
            for (var i = 1; i < 21; i++)
            {
                System.Threading.Thread.Sleep(10);

                //Top Left
                //this.Height = this.Height + 5;
                //this.Width = this.Width + 5;

                //Centro
                this.Top = this.Top - (int)2.5;
                this.Height = this.Height + 5;
                this.Width = this.Width + 5;
                this.Left = this.Left - (int)2.5;

                //Top centro
                //this.Height = this.Height + 5;
                //this.Width = this.Width + 5;
                //this.Left = this.Left - (int)2.5;
                System.Windows.Forms.Application.DoEvents();

            }
            this.BringToFront();
        }

        public void Achicar()
        {
            for (var i = 1; i < 21; i++)
            {
                System.Threading.Thread.Sleep(10);

                //Top Left
                //this.Height = this.Height - 5;
                //this.Width = this.Width - 5;

                //Centro
                this.Top = this.Top + (int)2.5;
                this.Height = this.Height - 5;
                this.Width = this.Width - 5;
                this.Left = this.Left + (int)2.5;

                //Top centro
                //this.Height = this.Height - 5;
                //this.Width = this.Width - 5;
                //this.Left = this.Left + (int)2.5;
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}
