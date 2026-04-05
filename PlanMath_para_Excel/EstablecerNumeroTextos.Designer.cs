namespace PlanMath_para_Excel
{
    partial class EstablecerNumeroTextos
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
            this.contenedorTablas = new System.Windows.Forms.TabControl();
            this.btnAsignarTextosInformacion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contenedorTablas
            // 
            this.contenedorTablas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contenedorTablas.Location = new System.Drawing.Point(12, 46);
            this.contenedorTablas.Name = "contenedorTablas";
            this.contenedorTablas.SelectedIndex = 0;
            this.contenedorTablas.Size = new System.Drawing.Size(1155, 405);
            this.contenedorTablas.TabIndex = 7;
            // 
            // btnAsignarTextosInformacion
            // 
            this.btnAsignarTextosInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAsignarTextosInformacion.Location = new System.Drawing.Point(886, 467);
            this.btnAsignarTextosInformacion.Name = "btnAsignarTextosInformacion";
            this.btnAsignarTextosInformacion.Size = new System.Drawing.Size(172, 29);
            this.btnAsignarTextosInformacion.TabIndex = 5;
            this.btnAsignarTextosInformacion.Text = "Seleccionar números";
            this.btnAsignarTextosInformacion.UseVisualStyleBackColor = true;
            this.btnAsignarTextosInformacion.Click += new System.EventHandler(this.btnAsignarTextosInformacion_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(913, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Selecciona las celdas que contienen los números entre las elegibles (color distin" +
    "to). Automáticamente las otras columnas serán las de cadenas de texto:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(1064, 467);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(103, 29);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // EstablecerNumeroTextos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 508);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contenedorTablas);
            this.Controls.Add(this.btnAsignarTextosInformacion);
            this.Name = "EstablecerNumeroTextos";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Establecer la columna del número y las de las cadenas de texto";
            this.Load += new System.EventHandler(this.EstablecerNumeroTextos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl contenedorTablas;
        private System.Windows.Forms.Button btnAsignarTextosInformacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
    }
}