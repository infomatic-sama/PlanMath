namespace PlanMath_para_Excel
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
            this.chkReemplazarCeldasActuales = new System.Windows.Forms.CheckBox();
            this.btnLimpiarAsignaciones = new System.Windows.Forms.Button();
            this.btnSeleccionMultipleNumeros = new System.Windows.Forms.Button();
            this.btnEsTexto = new System.Windows.Forms.Button();
            this.btnQuitarAsignaciones = new System.Windows.Forms.Button();
            this.chkTipoTextosInformacion = new System.Windows.Forms.CheckBox();
            this.btnAsignacionAutomatica = new System.Windows.Forms.Button();
            this.btnAsignarTextoInformacionNumero = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listaCeldasNumeros = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listaCeldasTextosInformacion = new System.Windows.Forms.ComboBox();
            this.listaAsignacionesTextosInformacion = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtNombreEntrada = new System.Windows.Forms.TextBox();
            this.lblNombreEntrada = new System.Windows.Forms.Label();
            this.cmbCalculos = new System.Windows.Forms.ComboBox();
            this.lblCalculo = new System.Windows.Forms.Label();
            this.cmbArchivoPlanMath = new System.Windows.Forms.ComboBox();
            this.btnQuitarArchivo = new System.Windows.Forms.Button();
            this.btnAgregarArchivo = new System.Windows.Forms.Button();
            this.lblArchivoPlanMath = new System.Windows.Forms.Label();
            this.txtTipoEntrada = new System.Windows.Forms.TextBox();
            this.lblTipoEntrada = new System.Windows.Forms.Label();
            this.txtCeldas = new System.Windows.Forms.TextBox();
            this.lblCeldas = new System.Windows.Forms.Label();
            this.buscarArchivo = new System.Windows.Forms.OpenFileDialog();
            this.txtHoja = new System.Windows.Forms.TextBox();
            this.lblHoja = new System.Windows.Forms.Label();
            this.btnActivarDesactivarSeleccionandoCeldas = new System.Windows.Forms.Button();
            this.btnDefinirEntrada = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkReemplazarCeldasActuales
            // 
            this.chkReemplazarCeldasActuales.AutoSize = true;
            this.chkReemplazarCeldasActuales.Location = new System.Drawing.Point(172, 86);
            this.chkReemplazarCeldasActuales.Name = "chkReemplazarCeldasActuales";
            this.chkReemplazarCeldasActuales.Size = new System.Drawing.Size(222, 36);
            this.chkReemplazarCeldasActuales.TabIndex = 60;
            this.chkReemplazarCeldasActuales.Text = "Reemplazar celdas actuales en \r\nla variable o vector de entrada";
            this.chkReemplazarCeldasActuales.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarAsignaciones
            // 
            this.btnLimpiarAsignaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiarAsignaciones.Location = new System.Drawing.Point(561, 307);
            this.btnLimpiarAsignaciones.Name = "btnLimpiarAsignaciones";
            this.btnLimpiarAsignaciones.Size = new System.Drawing.Size(58, 54);
            this.btnLimpiarAsignaciones.TabIndex = 59;
            this.btnLimpiarAsignaciones.Text = "Quitar todo";
            this.btnLimpiarAsignaciones.UseVisualStyleBackColor = true;
            this.btnLimpiarAsignaciones.Click += new System.EventHandler(this.btnLimpiarAsignaciones_Click);
            // 
            // btnSeleccionMultipleNumeros
            // 
            this.btnSeleccionMultipleNumeros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionMultipleNumeros.Location = new System.Drawing.Point(625, 262);
            this.btnSeleccionMultipleNumeros.Name = "btnSeleccionMultipleNumeros";
            this.btnSeleccionMultipleNumeros.Size = new System.Drawing.Size(100, 48);
            this.btnSeleccionMultipleNumeros.TabIndex = 58;
            this.btnSeleccionMultipleNumeros.Text = "Selección de Números";
            this.btnSeleccionMultipleNumeros.UseVisualStyleBackColor = true;
            this.btnSeleccionMultipleNumeros.Click += new System.EventHandler(this.btnSeleccionMultipleNumeros_Click);
            // 
            // btnEsTexto
            // 
            this.btnEsTexto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEsTexto.Location = new System.Drawing.Point(338, 337);
            this.btnEsTexto.Name = "btnEsTexto";
            this.btnEsTexto.Size = new System.Drawing.Size(74, 25);
            this.btnEsTexto.TabIndex = 57;
            this.btnEsTexto.Text = "Es texto";
            this.btnEsTexto.UseVisualStyleBackColor = true;
            this.btnEsTexto.Click += new System.EventHandler(this.btnEsTexto_Click);
            // 
            // btnQuitarAsignaciones
            // 
            this.btnQuitarAsignaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitarAsignaciones.Location = new System.Drawing.Point(497, 307);
            this.btnQuitarAsignaciones.Name = "btnQuitarAsignaciones";
            this.btnQuitarAsignaciones.Size = new System.Drawing.Size(58, 54);
            this.btnQuitarAsignaciones.TabIndex = 56;
            this.btnQuitarAsignaciones.Text = "Quitar";
            this.btnQuitarAsignaciones.UseVisualStyleBackColor = true;
            this.btnQuitarAsignaciones.Click += new System.EventHandler(this.btnQuitarAsignaciones_Click);
            // 
            // chkTipoTextosInformacion
            // 
            this.chkTipoTextosInformacion.AutoSize = true;
            this.chkTipoTextosInformacion.Location = new System.Drawing.Point(26, 95);
            this.chkTipoTextosInformacion.Name = "chkTipoTextosInformacion";
            this.chkTipoTextosInformacion.Size = new System.Drawing.Size(134, 20);
            this.chkTipoTextosInformacion.TabIndex = 55;
            this.chkTipoTextosInformacion.Text = "Cadenas de texto";
            this.chkTipoTextosInformacion.UseVisualStyleBackColor = true;
            this.chkTipoTextosInformacion.CheckedChanged += new System.EventHandler(this.chkTipoTextosInformacion_CheckedChanged);
            // 
            // btnAsignacionAutomatica
            // 
            this.btnAsignacionAutomatica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAsignacionAutomatica.Location = new System.Drawing.Point(625, 315);
            this.btnAsignacionAutomatica.Name = "btnAsignacionAutomatica";
            this.btnAsignacionAutomatica.Size = new System.Drawing.Size(100, 47);
            this.btnAsignacionAutomatica.TabIndex = 54;
            this.btnAsignacionAutomatica.Text = "Asignación de Cadenas";
            this.btnAsignacionAutomatica.UseVisualStyleBackColor = true;
            this.btnAsignacionAutomatica.Click += new System.EventHandler(this.btnAsignacionAutomatica_Click);
            // 
            // btnAsignarTextoInformacionNumero
            // 
            this.btnAsignarTextoInformacionNumero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAsignarTextoInformacionNumero.Location = new System.Drawing.Point(429, 307);
            this.btnAsignarTextoInformacionNumero.Name = "btnAsignarTextoInformacionNumero";
            this.btnAsignarTextoInformacionNumero.Size = new System.Drawing.Size(62, 54);
            this.btnAsignarTextoInformacionNumero.TabIndex = 53;
            this.btnAsignarTextoInformacionNumero.Text = "Asignar";
            this.btnAsignarTextoInformacionNumero.UseVisualStyleBackColor = true;
            this.btnAsignarTextoInformacionNumero.Click += new System.EventHandler(this.btnAsignarTextoInformacionNumero_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 344);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 52;
            this.label3.Text = "Número:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 51;
            this.label2.Text = "Cadena de texto:";
            // 
            // listaCeldasNumeros
            // 
            this.listaCeldasNumeros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaCeldasNumeros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listaCeldasNumeros.FormattingEnabled = true;
            this.listaCeldasNumeros.Location = new System.Drawing.Point(166, 339);
            this.listaCeldasNumeros.Name = "listaCeldasNumeros";
            this.listaCeldasNumeros.Size = new System.Drawing.Size(166, 24);
            this.listaCeldasNumeros.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(25, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(386, 23);
            this.label1.TabIndex = 49;
            this.label1.Text = "Asignar cadenas de texto a números:";
            // 
            // listaCeldasTextosInformacion
            // 
            this.listaCeldasTextosInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaCeldasTextosInformacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listaCeldasTextosInformacion.FormattingEnabled = true;
            this.listaCeldasTextosInformacion.Location = new System.Drawing.Point(166, 309);
            this.listaCeldasTextosInformacion.Name = "listaCeldasTextosInformacion";
            this.listaCeldasTextosInformacion.Size = new System.Drawing.Size(246, 24);
            this.listaCeldasTextosInformacion.TabIndex = 48;
            // 
            // listaAsignacionesTextosInformacion
            // 
            this.listaAsignacionesTextosInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaAsignacionesTextosInformacion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listaAsignacionesTextosInformacion.FullRowSelect = true;
            this.listaAsignacionesTextosInformacion.GridLines = true;
            this.listaAsignacionesTextosInformacion.HideSelection = false;
            this.listaAsignacionesTextosInformacion.Location = new System.Drawing.Point(28, 382);
            this.listaAsignacionesTextosInformacion.Name = "listaAsignacionesTextosInformacion";
            this.listaAsignacionesTextosInformacion.Size = new System.Drawing.Size(697, 355);
            this.listaAsignacionesTextosInformacion.TabIndex = 36;
            this.listaAsignacionesTextosInformacion.UseCompatibleStateImageBehavior = false;
            this.listaAsignacionesTextosInformacion.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Cadena de texto";
            this.columnHeader1.Width = 260;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Número";
            this.columnHeader2.Width = 178;
            // 
            // txtNombreEntrada
            // 
            this.txtNombreEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreEntrada.Location = new System.Drawing.Point(174, 234);
            this.txtNombreEntrada.Name = "txtNombreEntrada";
            this.txtNombreEntrada.Size = new System.Drawing.Size(551, 22);
            this.txtNombreEntrada.TabIndex = 43;
            this.txtNombreEntrada.Click += new System.EventHandler(this.txtNombreEntrada_Click);
            this.txtNombreEntrada.TextChanged += new System.EventHandler(this.txtNombreEntrada_TextChanged);
            // 
            // lblNombreEntrada
            // 
            this.lblNombreEntrada.AutoSize = true;
            this.lblNombreEntrada.Location = new System.Drawing.Point(23, 236);
            this.lblNombreEntrada.Name = "lblNombreEntrada";
            this.lblNombreEntrada.Size = new System.Drawing.Size(130, 32);
            this.lblNombreEntrada.TabIndex = 47;
            this.lblNombreEntrada.Text = "Nombre de variable \r\no vector de entrada:";
            // 
            // cmbCalculos
            // 
            this.cmbCalculos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCalculos.DisplayMember = "Nombre";
            this.cmbCalculos.FormattingEnabled = true;
            this.cmbCalculos.Location = new System.Drawing.Point(115, 183);
            this.cmbCalculos.Name = "cmbCalculos";
            this.cmbCalculos.Size = new System.Drawing.Size(610, 24);
            this.cmbCalculos.TabIndex = 42;
            this.cmbCalculos.ValueMember = "Nombre";
            this.cmbCalculos.Click += new System.EventHandler(this.cmbCalculos_Click);
            // 
            // lblCalculo
            // 
            this.lblCalculo.AutoSize = true;
            this.lblCalculo.Location = new System.Drawing.Point(25, 185);
            this.lblCalculo.Name = "lblCalculo";
            this.lblCalculo.Size = new System.Drawing.Size(55, 16);
            this.lblCalculo.TabIndex = 46;
            this.lblCalculo.Text = "Cálculo:";
            // 
            // cmbArchivoPlanMath
            // 
            this.cmbArchivoPlanMath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbArchivoPlanMath.DisplayMember = "Nombre";
            this.cmbArchivoPlanMath.FormattingEnabled = true;
            this.cmbArchivoPlanMath.Location = new System.Drawing.Point(115, 131);
            this.cmbArchivoPlanMath.Name = "cmbArchivoPlanMath";
            this.cmbArchivoPlanMath.Size = new System.Drawing.Size(483, 24);
            this.cmbArchivoPlanMath.TabIndex = 35;
            this.cmbArchivoPlanMath.ValueMember = "Ruta";
            this.cmbArchivoPlanMath.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.cmbArchivoPlanMath.SelectedIndexChanged += new System.EventHandler(this.cmbArchivoPlanMath_SelectedIndexChanged);
            this.cmbArchivoPlanMath.Click += new System.EventHandler(this.cmbArchivoPlanMath_Click);
            // 
            // btnQuitarArchivo
            // 
            this.btnQuitarArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitarArchivo.Location = new System.Drawing.Point(676, 122);
            this.btnQuitarArchivo.Name = "btnQuitarArchivo";
            this.btnQuitarArchivo.Size = new System.Drawing.Size(49, 40);
            this.btnQuitarArchivo.TabIndex = 41;
            this.btnQuitarArchivo.Text = "-";
            this.btnQuitarArchivo.UseVisualStyleBackColor = true;
            this.btnQuitarArchivo.Click += new System.EventHandler(this.btnQuitarArchivo_Click);
            // 
            // btnAgregarArchivo
            // 
            this.btnAgregarArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarArchivo.Location = new System.Drawing.Point(621, 122);
            this.btnAgregarArchivo.Name = "btnAgregarArchivo";
            this.btnAgregarArchivo.Size = new System.Drawing.Size(49, 40);
            this.btnAgregarArchivo.TabIndex = 37;
            this.btnAgregarArchivo.Text = "+";
            this.btnAgregarArchivo.UseVisualStyleBackColor = true;
            this.btnAgregarArchivo.Click += new System.EventHandler(this.btnAgregarArchivo_Click);
            // 
            // lblArchivoPlanMath
            // 
            this.lblArchivoPlanMath.Location = new System.Drawing.Point(23, 130);
            this.lblArchivoPlanMath.Name = "lblArchivoPlanMath";
            this.lblArchivoPlanMath.Size = new System.Drawing.Size(80, 43);
            this.lblArchivoPlanMath.TabIndex = 45;
            this.lblArchivoPlanMath.Text = "Archivo de PlanMath:";
            // 
            // txtTipoEntrada
            // 
            this.txtTipoEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTipoEntrada.Location = new System.Drawing.Point(243, 56);
            this.txtTipoEntrada.Name = "txtTipoEntrada";
            this.txtTipoEntrada.ReadOnly = true;
            this.txtTipoEntrada.Size = new System.Drawing.Size(169, 22);
            this.txtTipoEntrada.TabIndex = 40;
            this.txtTipoEntrada.TabStop = false;
            // 
            // lblTipoEntrada
            // 
            this.lblTipoEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTipoEntrada.AutoSize = true;
            this.lblTipoEntrada.Location = new System.Drawing.Point(199, 59);
            this.lblTipoEntrada.Name = "lblTipoEntrada";
            this.lblTipoEntrada.Size = new System.Drawing.Size(38, 16);
            this.lblTipoEntrada.TabIndex = 44;
            this.lblTipoEntrada.Text = "Tipo:";
            // 
            // txtCeldas
            // 
            this.txtCeldas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCeldas.Location = new System.Drawing.Point(84, 56);
            this.txtCeldas.Name = "txtCeldas";
            this.txtCeldas.ReadOnly = true;
            this.txtCeldas.Size = new System.Drawing.Size(97, 22);
            this.txtCeldas.TabIndex = 38;
            this.txtCeldas.TabStop = false;
            // 
            // lblCeldas
            // 
            this.lblCeldas.AutoSize = true;
            this.lblCeldas.Location = new System.Drawing.Point(23, 59);
            this.lblCeldas.Name = "lblCeldas";
            this.lblCeldas.Size = new System.Drawing.Size(53, 16);
            this.lblCeldas.TabIndex = 39;
            this.lblCeldas.Text = "Celdas:";
            // 
            // buscarArchivo
            // 
            this.buscarArchivo.Filter = "Archivos de PlanMath (*.pmcalc) | *.pmcalc";
            this.buscarArchivo.Title = "Buscar archivo de PlanMath...";
            // 
            // txtHoja
            // 
            this.txtHoja.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHoja.Location = new System.Drawing.Point(84, 19);
            this.txtHoja.Name = "txtHoja";
            this.txtHoja.ReadOnly = true;
            this.txtHoja.Size = new System.Drawing.Size(327, 22);
            this.txtHoja.TabIndex = 61;
            this.txtHoja.TabStop = false;
            // 
            // lblHoja
            // 
            this.lblHoja.AutoSize = true;
            this.lblHoja.Location = new System.Drawing.Point(25, 22);
            this.lblHoja.Name = "lblHoja";
            this.lblHoja.Size = new System.Drawing.Size(39, 16);
            this.lblHoja.TabIndex = 62;
            this.lblHoja.Text = "Hoja:";
            // 
            // btnActivarDesactivarSeleccionandoCeldas
            // 
            this.btnActivarDesactivarSeleccionandoCeldas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivarDesactivarSeleccionandoCeldas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivarDesactivarSeleccionandoCeldas.Image = global::PlanMath_para_Excel.Properties.Resources._03;
            this.btnActivarDesactivarSeleccionandoCeldas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActivarDesactivarSeleccionandoCeldas.Location = new System.Drawing.Point(481, 67);
            this.btnActivarDesactivarSeleccionandoCeldas.Name = "btnActivarDesactivarSeleccionandoCeldas";
            this.btnActivarDesactivarSeleccionandoCeldas.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnActivarDesactivarSeleccionandoCeldas.Size = new System.Drawing.Size(244, 44);
            this.btnActivarDesactivarSeleccionandoCeldas.TabIndex = 64;
            this.btnActivarDesactivarSeleccionandoCeldas.Text = "Comenzar a seleccionar celdas";
            this.btnActivarDesactivarSeleccionandoCeldas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActivarDesactivarSeleccionandoCeldas.UseVisualStyleBackColor = true;
            this.btnActivarDesactivarSeleccionandoCeldas.Click += new System.EventHandler(this.btnActivarDesactivarSeleccionandoCeldas_Click);
            // 
            // btnDefinirEntrada
            // 
            this.btnDefinirEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefinirEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefinirEntrada.Image = global::PlanMath_para_Excel.Properties.Resources._23;
            this.btnDefinirEntrada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefinirEntrada.Location = new System.Drawing.Point(429, 17);
            this.btnDefinirEntrada.Name = "btnDefinirEntrada";
            this.btnDefinirEntrada.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnDefinirEntrada.Size = new System.Drawing.Size(296, 44);
            this.btnDefinirEntrada.TabIndex = 63;
            this.btnDefinirEntrada.Text = "Enviar definición de variable o vector de entrada a PlanMath";
            this.btnDefinirEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDefinirEntrada.UseVisualStyleBackColor = true;
            this.btnDefinirEntrada.Click += new System.EventHandler(this.btnDefinirEntrada_Click);
            // 
            // DefinicionEntradaSeleccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnActivarDesactivarSeleccionandoCeldas);
            this.Controls.Add(this.btnDefinirEntrada);
            this.Controls.Add(this.txtHoja);
            this.Controls.Add(this.lblHoja);
            this.Controls.Add(this.chkReemplazarCeldasActuales);
            this.Controls.Add(this.btnLimpiarAsignaciones);
            this.Controls.Add(this.btnSeleccionMultipleNumeros);
            this.Controls.Add(this.btnEsTexto);
            this.Controls.Add(this.btnQuitarAsignaciones);
            this.Controls.Add(this.chkTipoTextosInformacion);
            this.Controls.Add(this.btnAsignacionAutomatica);
            this.Controls.Add(this.btnAsignarTextoInformacionNumero);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listaCeldasNumeros);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listaCeldasTextosInformacion);
            this.Controls.Add(this.listaAsignacionesTextosInformacion);
            this.Controls.Add(this.txtNombreEntrada);
            this.Controls.Add(this.lblNombreEntrada);
            this.Controls.Add(this.cmbCalculos);
            this.Controls.Add(this.lblCalculo);
            this.Controls.Add(this.cmbArchivoPlanMath);
            this.Controls.Add(this.btnQuitarArchivo);
            this.Controls.Add(this.btnAgregarArchivo);
            this.Controls.Add(this.lblArchivoPlanMath);
            this.Controls.Add(this.txtTipoEntrada);
            this.Controls.Add(this.lblTipoEntrada);
            this.Controls.Add(this.txtCeldas);
            this.Controls.Add(this.lblCeldas);
            this.Name = "DefinicionEntradaSeleccion";
            this.Size = new System.Drawing.Size(770, 748);
            this.Load += new System.EventHandler(this.DefinicionEntradaSeleccion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkReemplazarCeldasActuales;
        private System.Windows.Forms.Button btnLimpiarAsignaciones;
        private System.Windows.Forms.Button btnSeleccionMultipleNumeros;
        private System.Windows.Forms.Button btnEsTexto;
        private System.Windows.Forms.Button btnQuitarAsignaciones;
        internal System.Windows.Forms.CheckBox chkTipoTextosInformacion;
        private System.Windows.Forms.Button btnAsignacionAutomatica;
        private System.Windows.Forms.Button btnAsignarTextoInformacionNumero;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ComboBox listaCeldasNumeros;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox listaCeldasTextosInformacion;
        internal System.Windows.Forms.ListView listaAsignacionesTextosInformacion;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        internal System.Windows.Forms.TextBox txtNombreEntrada;
        private System.Windows.Forms.Label lblNombreEntrada;
        private System.Windows.Forms.Label lblCalculo;
        internal System.Windows.Forms.ComboBox cmbArchivoPlanMath;
        private System.Windows.Forms.Button btnQuitarArchivo;
        private System.Windows.Forms.Button btnAgregarArchivo;
        private System.Windows.Forms.Label lblArchivoPlanMath;
        internal System.Windows.Forms.TextBox txtTipoEntrada;
        private System.Windows.Forms.Label lblTipoEntrada;
        internal System.Windows.Forms.TextBox txtCeldas;
        private System.Windows.Forms.Label lblCeldas;
        private System.Windows.Forms.OpenFileDialog buscarArchivo;
        internal System.Windows.Forms.ComboBox cmbCalculos;
        internal System.Windows.Forms.TextBox txtHoja;
        private System.Windows.Forms.Label lblHoja;
        private System.Windows.Forms.Button btnDefinirEntrada;
        private System.Windows.Forms.Button btnActivarDesactivarSeleccionandoCeldas;
    }
}
