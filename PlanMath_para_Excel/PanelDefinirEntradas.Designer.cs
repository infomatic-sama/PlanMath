
namespace PlanMath_para_Excel
{
    partial class PanelDefinirEntradas
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLibro = new System.Windows.Forms.TextBox();
            this.lblLibro = new System.Windows.Forms.Label();
            this.btnDefinirEntrada = new System.Windows.Forms.Button();
            this.lblEnvio = new System.Windows.Forms.Label();
            this.iconoEnvioOk = new System.Windows.Forms.PictureBox();
            this.selecciones = new System.Windows.Forms.TabControl();
            this.btnNuevaEntrada = new System.Windows.Forms.Button();
            this.btnQuitarDefinicionEntrada = new System.Windows.Forms.Button();
            this.btnQuitarTodasDefiniciones = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iconoEnvioOk)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLibro
            // 
            this.txtLibro.Location = new System.Drawing.Point(84, 102);
            this.txtLibro.Name = "txtLibro";
            this.txtLibro.ReadOnly = true;
            this.txtLibro.Size = new System.Drawing.Size(327, 22);
            this.txtLibro.TabIndex = 0;
            this.txtLibro.TabStop = false;
            // 
            // lblLibro
            // 
            this.lblLibro.AutoSize = true;
            this.lblLibro.Location = new System.Drawing.Point(23, 105);
            this.lblLibro.Name = "lblLibro";
            this.lblLibro.Size = new System.Drawing.Size(40, 16);
            this.lblLibro.TabIndex = 5;
            this.lblLibro.Text = "Libro:";
            // 
            // btnDefinirEntrada
            // 
            this.btnDefinirEntrada.Image = global::PlanMath_para_Excel.Properties.Resources._23;
            this.btnDefinirEntrada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefinirEntrada.Location = new System.Drawing.Point(26, 18);
            this.btnDefinirEntrada.Name = "btnDefinirEntrada";
            this.btnDefinirEntrada.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnDefinirEntrada.Size = new System.Drawing.Size(322, 58);
            this.btnDefinirEntrada.TabIndex = 0;
            this.btnDefinirEntrada.Text = "Enviar todas las definiciones de \r\nvariables o vectores de entradas a PlanMath";
            this.btnDefinirEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDefinirEntrada.UseVisualStyleBackColor = true;
            this.btnDefinirEntrada.Click += new System.EventHandler(this.btnDefinirEntrada_Click);
            // 
            // lblEnvio
            // 
            this.lblEnvio.Location = new System.Drawing.Point(647, 20);
            this.lblEnvio.Name = "lblEnvio";
            this.lblEnvio.Size = new System.Drawing.Size(210, 58);
            this.lblEnvio.TabIndex = 16;
            this.lblEnvio.Text = "Se ha enviado la definición de la variable o vector de entrada al archivo de cálc" +
    "ulo.";
            this.lblEnvio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iconoEnvioOk
            // 
            this.iconoEnvioOk.Image = global::PlanMath_para_Excel.Properties.Resources._24;
            this.iconoEnvioOk.Location = new System.Drawing.Point(599, 34);
            this.iconoEnvioOk.Name = "iconoEnvioOk";
            this.iconoEnvioOk.Size = new System.Drawing.Size(32, 32);
            this.iconoEnvioOk.TabIndex = 21;
            this.iconoEnvioOk.TabStop = false;
            // 
            // selecciones
            // 
            this.selecciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selecciones.Location = new System.Drawing.Point(26, 143);
            this.selecciones.Name = "selecciones";
            this.selecciones.SelectedIndex = 0;
            this.selecciones.Size = new System.Drawing.Size(901, 100);
            this.selecciones.TabIndex = 22;
            // 
            // btnNuevaEntrada
            // 
            this.btnNuevaEntrada.Image = global::PlanMath_para_Excel.Properties.Resources._23;
            this.btnNuevaEntrada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevaEntrada.Location = new System.Drawing.Point(417, 91);
            this.btnNuevaEntrada.Name = "btnNuevaEntrada";
            this.btnNuevaEntrada.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnNuevaEntrada.Size = new System.Drawing.Size(218, 45);
            this.btnNuevaEntrada.TabIndex = 23;
            this.btnNuevaEntrada.Text = "Nueva definición de \r\nvariable o vector de entrada";
            this.btnNuevaEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNuevaEntrada.UseVisualStyleBackColor = true;
            this.btnNuevaEntrada.Click += new System.EventHandler(this.btnNuevaEntrada_Click);
            // 
            // btnQuitarDefinicionEntrada
            // 
            this.btnQuitarDefinicionEntrada.Image = global::PlanMath_para_Excel.Properties.Resources._06;
            this.btnQuitarDefinicionEntrada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuitarDefinicionEntrada.Location = new System.Drawing.Point(641, 91);
            this.btnQuitarDefinicionEntrada.Name = "btnQuitarDefinicionEntrada";
            this.btnQuitarDefinicionEntrada.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnQuitarDefinicionEntrada.Size = new System.Drawing.Size(211, 45);
            this.btnQuitarDefinicionEntrada.TabIndex = 24;
            this.btnQuitarDefinicionEntrada.Text = "Quitar definición de \r\nvariable o vector de entrada";
            this.btnQuitarDefinicionEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuitarDefinicionEntrada.UseVisualStyleBackColor = true;
            this.btnQuitarDefinicionEntrada.Click += new System.EventHandler(this.btnQuitarDefinicionEntrada_Click);
            // 
            // btnQuitarTodasDefiniciones
            // 
            this.btnQuitarTodasDefiniciones.Image = global::PlanMath_para_Excel.Properties.Resources._06;
            this.btnQuitarTodasDefiniciones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuitarTodasDefiniciones.Location = new System.Drawing.Point(352, 18);
            this.btnQuitarTodasDefiniciones.Name = "btnQuitarTodasDefiniciones";
            this.btnQuitarTodasDefiniciones.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnQuitarTodasDefiniciones.Size = new System.Drawing.Size(241, 58);
            this.btnQuitarTodasDefiniciones.TabIndex = 25;
            this.btnQuitarTodasDefiniciones.Text = "Quitar todas las definiciones de \r\nvariables o vectores de entradas\r\n";
            this.btnQuitarTodasDefiniciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuitarTodasDefiniciones.UseVisualStyleBackColor = true;
            this.btnQuitarTodasDefiniciones.Click += new System.EventHandler(this.btnQuitarTodasDefiniciones_Click);
            // 
            // PanelDefinirEntradas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnQuitarTodasDefiniciones);
            this.Controls.Add(this.btnQuitarDefinicionEntrada);
            this.Controls.Add(this.btnNuevaEntrada);
            this.Controls.Add(this.selecciones);
            this.Controls.Add(this.iconoEnvioOk);
            this.Controls.Add(this.lblEnvio);
            this.Controls.Add(this.btnDefinirEntrada);
            this.Controls.Add(this.txtLibro);
            this.Controls.Add(this.lblLibro);
            this.Name = "PanelDefinirEntradas";
            this.Size = new System.Drawing.Size(930, 246);
            ((System.ComponentModel.ISupportInitialize)(this.iconoEnvioOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.TextBox txtLibro;
        private System.Windows.Forms.Label lblLibro;
        private System.Windows.Forms.Button btnDefinirEntrada;
        internal System.Windows.Forms.Label lblEnvio;
        internal System.Windows.Forms.PictureBox iconoEnvioOk;
        internal System.Windows.Forms.TabControl selecciones;
        internal System.Windows.Forms.Button btnNuevaEntrada;
        internal System.Windows.Forms.Button btnQuitarDefinicionEntrada;
        internal System.Windows.Forms.Button btnQuitarTodasDefiniciones;
    }
}
