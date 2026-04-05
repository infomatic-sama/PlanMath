namespace PlanMath_para_Word
{
    partial class DefinicionEntradaSeleccion
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
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.chkTipoTextosInformacion = new System.Windows.Forms.CheckBox();
            this.txtLinea = new System.Windows.Forms.TextBox();
            this.txtPagina = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnIrA = new System.Windows.Forms.Button();
            this.btnLimpiarAsignaciones = new System.Windows.Forms.Button();
            this.btnQuitarAsignaciones = new System.Windows.Forms.Button();
            this.listaTextos = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.agregarTexto = new System.Windows.Forms.Button();
            this.textoActual = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreEntrada = new System.Windows.Forms.TextBox();
            this.lblNombreEntrada = new System.Windows.Forms.Label();
            this.cmbCalculos = new System.Windows.Forms.ComboBox();
            this.lblCalculo = new System.Windows.Forms.Label();
            this.cmbArchivoPlanMath = new System.Windows.Forms.ComboBox();
            this.btnQuitarArchivo = new System.Windows.Forms.Button();
            this.btnAgregarArchivo = new System.Windows.Forms.Button();
            this.lblArchivoPlanMath = new System.Windows.Forms.Label();
            this.buscarArchivo = new System.Windows.Forms.OpenFileDialog();
            this.btnActivarDesactivarSeleccionandoTextos = new System.Windows.Forms.Button();
            this.btnDefinirEntrada = new System.Windows.Forms.Button();
            this.chkReemplazarLecturasArchivos = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmbTipo
            // 
            this.cmbTipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "Variable de número",
            "Vector de números"});
            this.cmbTipo.Location = new System.Drawing.Point(484, 364);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(192, 24);
            this.cmbTipo.TabIndex = 66;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // chkTipoTextosInformacion
            // 
            this.chkTipoTextosInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTipoTextosInformacion.AutoSize = true;
            this.chkTipoTextosInformacion.Location = new System.Drawing.Point(682, 366);
            this.chkTipoTextosInformacion.Name = "chkTipoTextosInformacion";
            this.chkTipoTextosInformacion.Size = new System.Drawing.Size(134, 20);
            this.chkTipoTextosInformacion.TabIndex = 65;
            this.chkTipoTextosInformacion.Text = "Cadenas de texto";
            this.chkTipoTextosInformacion.UseVisualStyleBackColor = true;
            this.chkTipoTextosInformacion.CheckedChanged += new System.EventHandler(this.chkTipoTextosInformacion_CheckedChanged);
            // 
            // txtLinea
            // 
            this.txtLinea.Location = new System.Drawing.Point(145, 312);
            this.txtLinea.Name = "txtLinea";
            this.txtLinea.ReadOnly = true;
            this.txtLinea.Size = new System.Drawing.Size(77, 22);
            this.txtLinea.TabIndex = 64;
            // 
            // txtPagina
            // 
            this.txtPagina.Location = new System.Drawing.Point(145, 277);
            this.txtPagina.Name = "txtPagina";
            this.txtPagina.ReadOnly = true;
            this.txtPagina.Size = new System.Drawing.Size(77, 22);
            this.txtPagina.TabIndex = 63;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 62;
            this.label3.Text = "Número de línea:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 61;
            this.label2.Text = "Página:";
            // 
            // btnIrA
            // 
            this.btnIrA.Location = new System.Drawing.Point(156, 348);
            this.btnIrA.Name = "btnIrA";
            this.btnIrA.Size = new System.Drawing.Size(66, 54);
            this.btnIrA.TabIndex = 60;
            this.btnIrA.Text = "Ir a ->";
            this.btnIrA.UseVisualStyleBackColor = true;
            this.btnIrA.Click += new System.EventHandler(this.btnIrA_Click);
            // 
            // btnLimpiarAsignaciones
            // 
            this.btnLimpiarAsignaciones.Location = new System.Drawing.Point(92, 348);
            this.btnLimpiarAsignaciones.Name = "btnLimpiarAsignaciones";
            this.btnLimpiarAsignaciones.Size = new System.Drawing.Size(58, 54);
            this.btnLimpiarAsignaciones.TabIndex = 59;
            this.btnLimpiarAsignaciones.Text = "Quitar todo";
            this.btnLimpiarAsignaciones.UseVisualStyleBackColor = true;
            this.btnLimpiarAsignaciones.Click += new System.EventHandler(this.btnLimpiarAsignaciones_Click);
            // 
            // btnQuitarAsignaciones
            // 
            this.btnQuitarAsignaciones.Location = new System.Drawing.Point(28, 348);
            this.btnQuitarAsignaciones.Name = "btnQuitarAsignaciones";
            this.btnQuitarAsignaciones.Size = new System.Drawing.Size(58, 54);
            this.btnQuitarAsignaciones.TabIndex = 58;
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
            this.listaTextos.FullRowSelect = true;
            this.listaTextos.GridLines = true;
            this.listaTextos.HideSelection = false;
            this.listaTextos.Location = new System.Drawing.Point(28, 408);
            this.listaTextos.Name = "listaTextos";
            this.listaTextos.Size = new System.Drawing.Size(790, 614);
            this.listaTextos.TabIndex = 57;
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
            this.agregarTexto.Location = new System.Drawing.Point(723, 254);
            this.agregarTexto.Name = "agregarTexto";
            this.agregarTexto.Size = new System.Drawing.Size(93, 68);
            this.agregarTexto.TabIndex = 56;
            this.agregarTexto.Text = "Agregar texto a la lista";
            this.agregarTexto.UseVisualStyleBackColor = true;
            this.agregarTexto.Click += new System.EventHandler(this.agregarTexto_Click);
            // 
            // textoActual
            // 
            this.textoActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textoActual.Location = new System.Drawing.Point(237, 234);
            this.textoActual.Multiline = true;
            this.textoActual.Name = "textoActual";
            this.textoActual.Size = new System.Drawing.Size(469, 108);
            this.textoActual.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 54;
            this.label1.Text = "Texto seleccionado:";
            // 
            // txtNombreEntrada
            // 
            this.txtNombreEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreEntrada.Location = new System.Drawing.Point(168, 189);
            this.txtNombreEntrada.Name = "txtNombreEntrada";
            this.txtNombreEntrada.Size = new System.Drawing.Size(347, 22);
            this.txtNombreEntrada.TabIndex = 50;
            this.txtNombreEntrada.Click += new System.EventHandler(this.txtNombreEntrada_Click);
            // 
            // lblNombreEntrada
            // 
            this.lblNombreEntrada.AutoSize = true;
            this.lblNombreEntrada.Location = new System.Drawing.Point(23, 192);
            this.lblNombreEntrada.Name = "lblNombreEntrada";
            this.lblNombreEntrada.Size = new System.Drawing.Size(126, 32);
            this.lblNombreEntrada.TabIndex = 53;
            this.lblNombreEntrada.Text = "Nombre variable \r\no vector de entrada:";
            // 
            // cmbCalculos
            // 
            this.cmbCalculos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCalculos.DisplayMember = "Nombre";
            this.cmbCalculos.FormattingEnabled = true;
            this.cmbCalculos.Location = new System.Drawing.Point(109, 138);
            this.cmbCalculos.Name = "cmbCalculos";
            this.cmbCalculos.Size = new System.Drawing.Size(707, 24);
            this.cmbCalculos.TabIndex = 49;
            this.cmbCalculos.ValueMember = "Nombre";
            this.cmbCalculos.Click += new System.EventHandler(this.cmbCalculos_Click);
            // 
            // lblCalculo
            // 
            this.lblCalculo.AutoSize = true;
            this.lblCalculo.Location = new System.Drawing.Point(25, 141);
            this.lblCalculo.Name = "lblCalculo";
            this.lblCalculo.Size = new System.Drawing.Size(55, 16);
            this.lblCalculo.TabIndex = 52;
            this.lblCalculo.Text = "Cálculo:";
            // 
            // cmbArchivoPlanMath
            // 
            this.cmbArchivoPlanMath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbArchivoPlanMath.DisplayMember = "Nombre";
            this.cmbArchivoPlanMath.FormattingEnabled = true;
            this.cmbArchivoPlanMath.Location = new System.Drawing.Point(109, 86);
            this.cmbArchivoPlanMath.Name = "cmbArchivoPlanMath";
            this.cmbArchivoPlanMath.Size = new System.Drawing.Size(580, 24);
            this.cmbArchivoPlanMath.TabIndex = 46;
            this.cmbArchivoPlanMath.ValueMember = "Ruta";
            this.cmbArchivoPlanMath.DropDown += new System.EventHandler(this.cmbArchivoPlanMath_DropDown);
            this.cmbArchivoPlanMath.SelectedIndexChanged += new System.EventHandler(this.cmbArchivoPlanMath_SelectedIndexChanged);
            this.cmbArchivoPlanMath.Click += new System.EventHandler(this.cmbArchivoPlanMath_Click);
            // 
            // btnQuitarArchivo
            // 
            this.btnQuitarArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitarArchivo.Location = new System.Drawing.Point(767, 77);
            this.btnQuitarArchivo.Name = "btnQuitarArchivo";
            this.btnQuitarArchivo.Size = new System.Drawing.Size(49, 40);
            this.btnQuitarArchivo.TabIndex = 48;
            this.btnQuitarArchivo.Text = "-";
            this.btnQuitarArchivo.UseVisualStyleBackColor = true;
            this.btnQuitarArchivo.Click += new System.EventHandler(this.btnQuitarArchivo_Click);
            // 
            // btnAgregarArchivo
            // 
            this.btnAgregarArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarArchivo.Location = new System.Drawing.Point(712, 77);
            this.btnAgregarArchivo.Name = "btnAgregarArchivo";
            this.btnAgregarArchivo.Size = new System.Drawing.Size(49, 40);
            this.btnAgregarArchivo.TabIndex = 47;
            this.btnAgregarArchivo.Text = "+";
            this.btnAgregarArchivo.UseVisualStyleBackColor = true;
            this.btnAgregarArchivo.Click += new System.EventHandler(this.btnAgregarArchivo_Click);
            // 
            // lblArchivoPlanMath
            // 
            this.lblArchivoPlanMath.Location = new System.Drawing.Point(23, 86);
            this.lblArchivoPlanMath.Name = "lblArchivoPlanMath";
            this.lblArchivoPlanMath.Size = new System.Drawing.Size(80, 43);
            this.lblArchivoPlanMath.TabIndex = 51;
            this.lblArchivoPlanMath.Text = "Archivo de PlanMath:";
            // 
            // buscarArchivo
            // 
            this.buscarArchivo.Filter = "Archivos de PlanMath (*.pmcalc) | *.pmcalc";
            this.buscarArchivo.Title = "Buscar archivo de PlanMath...";
            // 
            // btnActivarDesactivarSeleccionandoTextos
            // 
            this.btnActivarDesactivarSeleccionandoTextos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivarDesactivarSeleccionandoTextos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivarDesactivarSeleccionandoTextos.Image = global::PlanMath_para_Word.Properties.Resources._01;
            this.btnActivarDesactivarSeleccionandoTextos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActivarDesactivarSeleccionandoTextos.Location = new System.Drawing.Point(383, 17);
            this.btnActivarDesactivarSeleccionandoTextos.Name = "btnActivarDesactivarSeleccionandoTextos";
            this.btnActivarDesactivarSeleccionandoTextos.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnActivarDesactivarSeleccionandoTextos.Size = new System.Drawing.Size(242, 44);
            this.btnActivarDesactivarSeleccionandoTextos.TabIndex = 68;
            this.btnActivarDesactivarSeleccionandoTextos.Text = "Comenzar a seleccionar textos";
            this.btnActivarDesactivarSeleccionandoTextos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActivarDesactivarSeleccionandoTextos.UseVisualStyleBackColor = true;
            this.btnActivarDesactivarSeleccionandoTextos.Click += new System.EventHandler(this.btnActivarDesactivarSeleccionandoTextos_Click);
            // 
            // btnDefinirEntrada
            // 
            this.btnDefinirEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefinirEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefinirEntrada.Image = global::PlanMath_para_Word.Properties.Resources._23;
            this.btnDefinirEntrada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefinirEntrada.Location = new System.Drawing.Point(63, 17);
            this.btnDefinirEntrada.Name = "btnDefinirEntrada";
            this.btnDefinirEntrada.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnDefinirEntrada.Size = new System.Drawing.Size(303, 44);
            this.btnDefinirEntrada.TabIndex = 67;
            this.btnDefinirEntrada.Text = "Enviar definición de variable o vector de entrada a PlanMath";
            this.btnDefinirEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDefinirEntrada.UseVisualStyleBackColor = true;
            this.btnDefinirEntrada.Click += new System.EventHandler(this.btnDefinirEntrada_Click);
            // 
            // chkReemplazarLecturasArchivos
            // 
            this.chkReemplazarLecturasArchivos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkReemplazarLecturasArchivos.Location = new System.Drawing.Point(521, 182);
            this.chkReemplazarLecturasArchivos.Name = "chkReemplazarLecturasArchivos";
            this.chkReemplazarLecturasArchivos.Size = new System.Drawing.Size(316, 38);
            this.chkReemplazarLecturasArchivos.TabIndex = 79;
            this.chkReemplazarLecturasArchivos.Text = "Reemplazar las lecturas de archivos existentes en la variable o vector de entrada" +
    "";
            this.chkReemplazarLecturasArchivos.UseVisualStyleBackColor = true;
            // 
            // DefinicionEntradaSeleccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.chkReemplazarLecturasArchivos);
            this.Controls.Add(this.btnActivarDesactivarSeleccionandoTextos);
            this.Controls.Add(this.btnDefinirEntrada);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.chkTipoTextosInformacion);
            this.Controls.Add(this.txtLinea);
            this.Controls.Add(this.txtPagina);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnIrA);
            this.Controls.Add(this.btnLimpiarAsignaciones);
            this.Controls.Add(this.btnQuitarAsignaciones);
            this.Controls.Add(this.listaTextos);
            this.Controls.Add(this.agregarTexto);
            this.Controls.Add(this.textoActual);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombreEntrada);
            this.Controls.Add(this.lblNombreEntrada);
            this.Controls.Add(this.cmbCalculos);
            this.Controls.Add(this.lblCalculo);
            this.Controls.Add(this.cmbArchivoPlanMath);
            this.Controls.Add(this.btnQuitarArchivo);
            this.Controls.Add(this.btnAgregarArchivo);
            this.Controls.Add(this.lblArchivoPlanMath);
            this.Name = "DefinicionEntradaSeleccion";
            this.Size = new System.Drawing.Size(850, 1084);
            this.Load += new System.EventHandler(this.DefinicionEntradaSeleccion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTipo;
        internal System.Windows.Forms.CheckBox chkTipoTextosInformacion;
        internal System.Windows.Forms.TextBox txtLinea;
        internal System.Windows.Forms.TextBox txtPagina;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnIrA;
        private System.Windows.Forms.Button btnLimpiarAsignaciones;
        private System.Windows.Forms.Button btnQuitarAsignaciones;
        internal System.Windows.Forms.ListView listaTextos;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button agregarTexto;
        internal System.Windows.Forms.TextBox textoActual;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtNombreEntrada;
        private System.Windows.Forms.Label lblNombreEntrada;
        private System.Windows.Forms.Label lblCalculo;
        internal System.Windows.Forms.ComboBox cmbArchivoPlanMath;
        private System.Windows.Forms.Button btnQuitarArchivo;
        private System.Windows.Forms.Button btnAgregarArchivo;
        private System.Windows.Forms.Label lblArchivoPlanMath;
        private System.Windows.Forms.OpenFileDialog buscarArchivo;
        internal System.Windows.Forms.ComboBox cmbCalculos;
        private System.Windows.Forms.Button btnActivarDesactivarSeleccionandoTextos;
        private System.Windows.Forms.Button btnDefinirEntrada;
        public System.Windows.Forms.CheckBox chkReemplazarLecturasArchivos;
    }
}
