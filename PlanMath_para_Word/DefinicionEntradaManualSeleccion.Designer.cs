namespace PlanMath_para_Word
{
    partial class DefinicionEntradaManualSeleccion
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
            this.btnDefinirEntrada = new System.Windows.Forms.Button();
            this.txtNombreEntrada = new System.Windows.Forms.TextBox();
            this.lblNombreEntrada = new System.Windows.Forms.Label();
            this.cmbCalculos = new System.Windows.Forms.ComboBox();
            this.lblCalculo = new System.Windows.Forms.Label();
            this.cmbArchivoPlanMath = new System.Windows.Forms.ComboBox();
            this.btnQuitarArchivo = new System.Windows.Forms.Button();
            this.btnAgregarArchivo = new System.Windows.Forms.Button();
            this.lblArchivoPlanMath = new System.Windows.Forms.Label();
            this.buscarArchivo = new System.Windows.Forms.OpenFileDialog();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkReemplazarLecturasArchivos = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnDefinirEntrada
            // 
            this.btnDefinirEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefinirEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefinirEntrada.Image = global::PlanMath_para_Word.Properties.Resources._23;
            this.btnDefinirEntrada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefinirEntrada.Location = new System.Drawing.Point(422, 17);
            this.btnDefinirEntrada.Name = "btnDefinirEntrada";
            this.btnDefinirEntrada.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnDefinirEntrada.Size = new System.Drawing.Size(303, 44);
            this.btnDefinirEntrada.TabIndex = 64;
            this.btnDefinirEntrada.Text = "Enviar definición de variable o vector de entrada a PlanMath";
            this.btnDefinirEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDefinirEntrada.UseVisualStyleBackColor = true;
            this.btnDefinirEntrada.Click += new System.EventHandler(this.btnDefinirEntrada_Click);
            // 
            // txtNombreEntrada
            // 
            this.txtNombreEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreEntrada.Location = new System.Drawing.Point(155, 192);
            this.txtNombreEntrada.Name = "txtNombreEntrada";
            this.txtNombreEntrada.Size = new System.Drawing.Size(566, 22);
            this.txtNombreEntrada.TabIndex = 70;
            this.txtNombreEntrada.TextChanged += new System.EventHandler(this.txtNombreEntrada_TextChanged);
            // 
            // lblNombreEntrada
            // 
            this.lblNombreEntrada.AutoSize = true;
            this.lblNombreEntrada.Location = new System.Drawing.Point(19, 194);
            this.lblNombreEntrada.Name = "lblNombreEntrada";
            this.lblNombreEntrada.Size = new System.Drawing.Size(130, 32);
            this.lblNombreEntrada.TabIndex = 73;
            this.lblNombreEntrada.Text = "Nombre de variable \r\no vector de entrada:";
            // 
            // cmbCalculos
            // 
            this.cmbCalculos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCalculos.DisplayMember = "Nombre";
            this.cmbCalculos.FormattingEnabled = true;
            this.cmbCalculos.Location = new System.Drawing.Point(155, 141);
            this.cmbCalculos.Name = "cmbCalculos";
            this.cmbCalculos.Size = new System.Drawing.Size(566, 24);
            this.cmbCalculos.TabIndex = 69;
            this.cmbCalculos.ValueMember = "Nombre";
            this.cmbCalculos.Click += new System.EventHandler(this.cmbCalculos_Click);
            // 
            // lblCalculo
            // 
            this.lblCalculo.AutoSize = true;
            this.lblCalculo.Location = new System.Drawing.Point(21, 143);
            this.lblCalculo.Name = "lblCalculo";
            this.lblCalculo.Size = new System.Drawing.Size(55, 16);
            this.lblCalculo.TabIndex = 72;
            this.lblCalculo.Text = "Cálculo:";
            // 
            // cmbArchivoPlanMath
            // 
            this.cmbArchivoPlanMath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbArchivoPlanMath.DisplayMember = "Nombre";
            this.cmbArchivoPlanMath.FormattingEnabled = true;
            this.cmbArchivoPlanMath.Location = new System.Drawing.Point(155, 89);
            this.cmbArchivoPlanMath.Name = "cmbArchivoPlanMath";
            this.cmbArchivoPlanMath.Size = new System.Drawing.Size(439, 24);
            this.cmbArchivoPlanMath.TabIndex = 66;
            this.cmbArchivoPlanMath.ValueMember = "Ruta";
            this.cmbArchivoPlanMath.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.cmbArchivoPlanMath.SelectedIndexChanged += new System.EventHandler(this.cmbArchivoPlanMath_SelectedIndexChanged);
            this.cmbArchivoPlanMath.Click += new System.EventHandler(this.cmbArchivoPlanMath_Click);
            // 
            // btnQuitarArchivo
            // 
            this.btnQuitarArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitarArchivo.Location = new System.Drawing.Point(672, 80);
            this.btnQuitarArchivo.Name = "btnQuitarArchivo";
            this.btnQuitarArchivo.Size = new System.Drawing.Size(49, 40);
            this.btnQuitarArchivo.TabIndex = 68;
            this.btnQuitarArchivo.Text = "-";
            this.btnQuitarArchivo.UseVisualStyleBackColor = true;
            this.btnQuitarArchivo.Click += new System.EventHandler(this.btnQuitarArchivo_Click);
            // 
            // btnAgregarArchivo
            // 
            this.btnAgregarArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarArchivo.Location = new System.Drawing.Point(617, 80);
            this.btnAgregarArchivo.Name = "btnAgregarArchivo";
            this.btnAgregarArchivo.Size = new System.Drawing.Size(49, 40);
            this.btnAgregarArchivo.TabIndex = 67;
            this.btnAgregarArchivo.Text = "+";
            this.btnAgregarArchivo.UseVisualStyleBackColor = true;
            this.btnAgregarArchivo.Click += new System.EventHandler(this.btnAgregarArchivo_Click);
            // 
            // lblArchivoPlanMath
            // 
            this.lblArchivoPlanMath.Location = new System.Drawing.Point(19, 88);
            this.lblArchivoPlanMath.Name = "lblArchivoPlanMath";
            this.lblArchivoPlanMath.Size = new System.Drawing.Size(80, 43);
            this.lblArchivoPlanMath.TabIndex = 71;
            this.lblArchivoPlanMath.Text = "Archivo de PlanMath:";
            // 
            // buscarArchivo
            // 
            this.buscarArchivo.Filter = "Archivos de PlanMath (*.pmcalc) | *.pmcalc";
            this.buscarArchivo.Title = "Buscar archivo de PlanMath...";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "Variable de número",
            "Vector de números",
            "Vector de cadenas de texto"});
            this.cmbTipo.Location = new System.Drawing.Point(155, 240);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(278, 24);
            this.cmbTipo.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 76;
            this.label1.Text = "Tipo:";
            // 
            // chkReemplazarLecturasArchivos
            // 
            this.chkReemplazarLecturasArchivos.AutoSize = true;
            this.chkReemplazarLecturasArchivos.Location = new System.Drawing.Point(24, 289);
            this.chkReemplazarLecturasArchivos.Name = "chkReemplazarLecturasArchivos";
            this.chkReemplazarLecturasArchivos.Size = new System.Drawing.Size(513, 20);
            this.chkReemplazarLecturasArchivos.TabIndex = 78;
            this.chkReemplazarLecturasArchivos.Text = "Reemplazar las lecturas de archivos existentes en la variable o vector de entrada" +
    "";
            this.chkReemplazarLecturasArchivos.UseVisualStyleBackColor = true;
            // 
            // DefinicionEntradaManualSeleccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkReemplazarLecturasArchivos);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombreEntrada);
            this.Controls.Add(this.lblNombreEntrada);
            this.Controls.Add(this.cmbCalculos);
            this.Controls.Add(this.lblCalculo);
            this.Controls.Add(this.cmbArchivoPlanMath);
            this.Controls.Add(this.btnQuitarArchivo);
            this.Controls.Add(this.btnAgregarArchivo);
            this.Controls.Add(this.lblArchivoPlanMath);
            this.Controls.Add(this.btnDefinirEntrada);
            this.Name = "DefinicionEntradaManualSeleccion";
            this.Size = new System.Drawing.Size(770, 748);
            this.Load += new System.EventHandler(this.DefinicionEntradaManualSeleccion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDefinirEntrada;
        internal System.Windows.Forms.TextBox txtNombreEntrada;
        private System.Windows.Forms.Label lblNombreEntrada;
        internal System.Windows.Forms.ComboBox cmbCalculos;
        private System.Windows.Forms.Label lblCalculo;
        internal System.Windows.Forms.ComboBox cmbArchivoPlanMath;
        private System.Windows.Forms.Button btnQuitarArchivo;
        private System.Windows.Forms.Button btnAgregarArchivo;
        private System.Windows.Forms.Label lblArchivoPlanMath;
        private System.Windows.Forms.OpenFileDialog buscarArchivo;
        public System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox chkReemplazarLecturasArchivos;
    }
}
