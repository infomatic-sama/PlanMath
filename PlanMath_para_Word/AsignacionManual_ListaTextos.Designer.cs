namespace PlanMath_para_Word
{
    partial class AsignacionManual_ListaTextos
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
            this.txtLinea = new System.Windows.Forms.TextBox();
            this.txtPagina = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLimpiarAsignaciones = new System.Windows.Forms.Button();
            this.btnQuitarAsignaciones = new System.Windows.Forms.Button();
            this.listaTextos = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.agregarTexto = new System.Windows.Forms.Button();
            this.textoActual = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLinea
            // 
            this.txtLinea.Location = new System.Drawing.Point(134, 96);
            this.txtLinea.Name = "txtLinea";
            this.txtLinea.ReadOnly = true;
            this.txtLinea.Size = new System.Drawing.Size(77, 22);
            this.txtLinea.TabIndex = 77;
            // 
            // txtPagina
            // 
            this.txtPagina.Location = new System.Drawing.Point(134, 61);
            this.txtPagina.Name = "txtPagina";
            this.txtPagina.ReadOnly = true;
            this.txtPagina.Size = new System.Drawing.Size(77, 22);
            this.txtPagina.TabIndex = 76;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 75;
            this.label3.Text = "Número de línea:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 74;
            this.label2.Text = "Página:";
            // 
            // btnLimpiarAsignaciones
            // 
            this.btnLimpiarAsignaciones.Location = new System.Drawing.Point(81, 132);
            this.btnLimpiarAsignaciones.Name = "btnLimpiarAsignaciones";
            this.btnLimpiarAsignaciones.Size = new System.Drawing.Size(58, 54);
            this.btnLimpiarAsignaciones.TabIndex = 72;
            this.btnLimpiarAsignaciones.Text = "Quitar todo";
            this.btnLimpiarAsignaciones.UseVisualStyleBackColor = true;
            this.btnLimpiarAsignaciones.Click += new System.EventHandler(this.btnLimpiarAsignaciones_Click);
            // 
            // btnQuitarAsignaciones
            // 
            this.btnQuitarAsignaciones.Location = new System.Drawing.Point(17, 132);
            this.btnQuitarAsignaciones.Name = "btnQuitarAsignaciones";
            this.btnQuitarAsignaciones.Size = new System.Drawing.Size(58, 54);
            this.btnQuitarAsignaciones.TabIndex = 71;
            this.btnQuitarAsignaciones.Text = "Quitar";
            this.btnQuitarAsignaciones.UseVisualStyleBackColor = true;
            this.btnQuitarAsignaciones.Click += new System.EventHandler(this.btnQuitarAsignaciones_Click);
            // 
            // listaTextos
            // 
            this.listaTextos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaTextos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listaTextos.GridLines = true;
            this.listaTextos.HideSelection = false;
            this.listaTextos.HoverSelection = true;
            this.listaTextos.Location = new System.Drawing.Point(17, 192);
            this.listaTextos.Name = "listaTextos";
            this.listaTextos.Size = new System.Drawing.Size(804, 314);
            this.listaTextos.TabIndex = 70;
            this.listaTextos.UseCompatibleStateImageBehavior = false;
            this.listaTextos.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Texto";
            this.columnHeader1.Width = 507;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Página";
            this.columnHeader2.Width = 95;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Línea";
            this.columnHeader3.Width = 120;
            // 
            // agregarTexto
            // 
            this.agregarTexto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.agregarTexto.Location = new System.Drawing.Point(726, 38);
            this.agregarTexto.Name = "agregarTexto";
            this.agregarTexto.Size = new System.Drawing.Size(93, 68);
            this.agregarTexto.TabIndex = 69;
            this.agregarTexto.Text = "Agregar texto a la lista";
            this.agregarTexto.UseVisualStyleBackColor = true;
            this.agregarTexto.Click += new System.EventHandler(this.agregarTexto_Click);
            // 
            // textoActual
            // 
            this.textoActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textoActual.Location = new System.Drawing.Point(226, 18);
            this.textoActual.Multiline = true;
            this.textoActual.Name = "textoActual";
            this.textoActual.Size = new System.Drawing.Size(483, 108);
            this.textoActual.TabIndex = 68;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 67;
            this.label1.Text = "Texto seleccionado:";
            // 
            // AsignacionManual_ListaTextos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 530);
            this.Controls.Add(this.txtLinea);
            this.Controls.Add(this.txtPagina);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLimpiarAsignaciones);
            this.Controls.Add(this.btnQuitarAsignaciones);
            this.Controls.Add(this.listaTextos);
            this.Controls.Add(this.agregarTexto);
            this.Controls.Add(this.textoActual);
            this.Controls.Add(this.label1);
            this.Name = "AsignacionManual_ListaTextos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asignación manual de lista de textos";
            this.Load += new System.EventHandler(this.AsignacionManual_ListaTextos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.TextBox txtLinea;
        internal System.Windows.Forms.TextBox txtPagina;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLimpiarAsignaciones;
        private System.Windows.Forms.Button btnQuitarAsignaciones;
        internal System.Windows.Forms.ListView listaTextos;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button agregarTexto;
        internal System.Windows.Forms.TextBox textoActual;
        private System.Windows.Forms.Label label1;
    }
}