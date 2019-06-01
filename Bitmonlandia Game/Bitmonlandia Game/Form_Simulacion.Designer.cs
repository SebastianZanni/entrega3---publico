namespace Bitmonlandia_Game
{
    partial class Form_Simulacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapa = new System.Windows.Forms.GroupBox();
            this.meses = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.mesesValor = new System.Windows.Forms.Label();
            this.bitmonsVivos = new System.Windows.Forms.Label();
            this.bitVivos = new System.Windows.Forms.Label();
            this.bitMuertos = new System.Windows.Forms.Label();
            this.muertos = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mapa
            // 
            this.mapa.Location = new System.Drawing.Point(75, 42);
            this.mapa.Name = "mapa";
            this.mapa.Size = new System.Drawing.Size(1200, 630);
            this.mapa.TabIndex = 0;
            this.mapa.TabStop = false;
            // 
            // meses
            // 
            this.meses.AutoSize = true;
            this.meses.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.meses.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.meses.Location = new System.Drawing.Point(127, 26);
            this.meses.Name = "meses";
            this.meses.Size = new System.Drawing.Size(66, 13);
            this.meses.TabIndex = 2;
            this.meses.Text = "Mes Actual: ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(539, 678);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(272, 62);
            this.button2.TabIndex = 3;
            this.button2.Text = "Comenzar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mesesValor
            // 
            this.mesesValor.AutoSize = true;
            this.mesesValor.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.mesesValor.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.mesesValor.Location = new System.Drawing.Point(261, 26);
            this.mesesValor.Name = "mesesValor";
            this.mesesValor.Size = new System.Drawing.Size(13, 13);
            this.mesesValor.TabIndex = 4;
            this.mesesValor.Text = "0";
            // 
            // bitmonsVivos
            // 
            this.bitmonsVivos.AutoSize = true;
            this.bitmonsVivos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bitmonsVivos.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bitmonsVivos.Location = new System.Drawing.Point(599, 26);
            this.bitmonsVivos.Name = "bitmonsVivos";
            this.bitmonsVivos.Size = new System.Drawing.Size(13, 13);
            this.bitmonsVivos.TabIndex = 6;
            this.bitmonsVivos.Text = "0";
            // 
            // bitVivos
            // 
            this.bitVivos.AutoSize = true;
            this.bitVivos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bitVivos.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bitVivos.Location = new System.Drawing.Point(437, 26);
            this.bitVivos.Name = "bitVivos";
            this.bitVivos.Size = new System.Drawing.Size(76, 13);
            this.bitVivos.TabIndex = 5;
            this.bitVivos.Text = "Bitmons Vivos:";
            // 
            // bitMuertos
            // 
            this.bitMuertos.AutoSize = true;
            this.bitMuertos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bitMuertos.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bitMuertos.Location = new System.Drawing.Point(923, 26);
            this.bitMuertos.Name = "bitMuertos";
            this.bitMuertos.Size = new System.Drawing.Size(13, 13);
            this.bitMuertos.TabIndex = 8;
            this.bitMuertos.Text = "0";
            // 
            // muertos
            // 
            this.muertos.AutoSize = true;
            this.muertos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.muertos.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.muertos.Location = new System.Drawing.Point(726, 26);
            this.muertos.Name = "muertos";
            this.muertos.Size = new System.Drawing.Size(88, 13);
            this.muertos.TabIndex = 7;
            this.muertos.Text = "Bitmons Muertos:";
            // 
            // Form_Simulacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1350, 752);
            this.ControlBox = false;
            this.Controls.Add(this.bitMuertos);
            this.Controls.Add(this.muertos);
            this.Controls.Add(this.bitmonsVivos);
            this.Controls.Add(this.bitVivos);
            this.Controls.Add(this.mesesValor);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.meses);
            this.Controls.Add(this.mapa);
            this.Name = "Form_Simulacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Simulacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox mapa;
        private System.Windows.Forms.Label meses;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label mesesValor;
        private System.Windows.Forms.Label bitmonsVivos;
        private System.Windows.Forms.Label bitVivos;
        private System.Windows.Forms.Label bitMuertos;
        private System.Windows.Forms.Label muertos;
    }
}