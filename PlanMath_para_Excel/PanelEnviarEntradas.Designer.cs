
namespace PlanMath_para_Excel
{
    partial class PanelEnviarEntradas
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelEnviarEntradas));
            this.lblEnvio = new System.Windows.Forms.Label();
            this.ejecucionSeleccionada = new System.Windows.Forms.ComboBox();
            this.btnEjecucionAnterior = new System.Windows.Forms.Button();
            this.btnEjecucionSiguiente = new System.Windows.Forms.Button();
            this.lblNombreEntrada = new System.Windows.Forms.Label();
            this.lblCalculo = new System.Windows.Forms.Label();
            this.lblArchivoPlanMath = new System.Windows.Forms.Label();
            this.txtTipoEntrada = new System.Windows.Forms.TextBox();
            this.lblTipoEntrada = new System.Windows.Forms.Label();
            this.txtLibro = new System.Windows.Forms.TextBox();
            this.lblLibro = new System.Windows.Forms.Label();
            this.txtCeldas = new System.Windows.Forms.TextBox();
            this.lblCeldas = new System.Windows.Forms.Label();
            this.txtHoja = new System.Windows.Forms.TextBox();
            this.lblHoja = new System.Windows.Forms.Label();
            this.txtEntrada = new System.Windows.Forms.TextBox();
            this.txtCalculo = new System.Windows.Forms.TextBox();
            this.txtArchivoPlanMath = new System.Windows.Forms.TextBox();
            this.lblNombresDescripciones = new System.Windows.Forms.Label();
            this.procesoActualizarEjecucionSeleccionada = new System.Windows.Forms.Timer(this.components);
            this.btnQuitarEjecucion = new System.Windows.Forms.Button();
            this.iconoEnvioOk = new System.Windows.Forms.PictureBox();
            this.btnEnviarEntrada = new System.Windows.Forms.Button();
            this.panelCeldasSeleccionadasAutomaticamente = new System.Windows.Forms.Panel();
            this.panelSeleccionarCeldas = new System.Windows.Forms.Panel();
            this.mensajeErrorSeleccion = new System.Windows.Forms.Label();
            this.establecerTextosNumeros = new System.Windows.Forms.Button();
            this.hojaSeleccionada = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.celdasSeleccionadas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnActivarDesactivarSeleccionandoCeldas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iconoEnvioOk)).BeginInit();
            this.panelCeldasSeleccionadasAutomaticamente.SuspendLayout();
            this.panelSeleccionarCeldas.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEnvio
            // 
            this.lblEnvio.Location = new System.Drawing.Point(364, 18);
            this.lblEnvio.Name = "lblEnvio";
            this.lblEnvio.Size = new System.Drawing.Size(210, 58);
            this.lblEnvio.TabIndex = 23;
            this.lblEnvio.Text = "Se ha enviado la variable o vector de entrada a la ejecución de cálculo.";
            this.lblEnvio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEnvio.Click += new System.EventHandler(this.lblEnvio_Click);
            // 
            // ejecucionSeleccionada
            // 
            this.ejecucionSeleccionada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ejecucionSeleccionada.DisplayMember = "Texto_Ejecucion";
            this.ejecucionSeleccionada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ejecucionSeleccionada.FormattingEnabled = true;
            this.ejecucionSeleccionada.Location = new System.Drawing.Point(96, 112);
            this.ejecucionSeleccionada.Name = "ejecucionSeleccionada";
            this.ejecucionSeleccionada.Size = new System.Drawing.Size(595, 24);
            this.ejecucionSeleccionada.TabIndex = 2;
            this.ejecucionSeleccionada.SelectedIndexChanged += new System.EventHandler(this.ejecucionSeleccionada_SelectedIndexChanged);
            // 
            // btnEjecucionAnterior
            // 
            this.btnEjecucionAnterior.Location = new System.Drawing.Point(18, 104);
            this.btnEjecucionAnterior.Name = "btnEjecucionAnterior";
            this.btnEjecucionAnterior.Size = new System.Drawing.Size(57, 38);
            this.btnEjecucionAnterior.TabIndex = 1;
            this.btnEjecucionAnterior.Text = "<";
            this.btnEjecucionAnterior.UseVisualStyleBackColor = true;
            this.btnEjecucionAnterior.Click += new System.EventHandler(this.btnEjecucionAnterior_Click);
            // 
            // btnEjecucionSiguiente
            // 
            this.btnEjecucionSiguiente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEjecucionSiguiente.Location = new System.Drawing.Point(777, 104);
            this.btnEjecucionSiguiente.Name = "btnEjecucionSiguiente";
            this.btnEjecucionSiguiente.Size = new System.Drawing.Size(57, 38);
            this.btnEjecucionSiguiente.TabIndex = 4;
            this.btnEjecucionSiguiente.Text = ">";
            this.btnEjecucionSiguiente.UseVisualStyleBackColor = true;
            this.btnEjecucionSiguiente.Click += new System.EventHandler(this.btnEjecucionSiguiente_Click);
            // 
            // lblNombreEntrada
            // 
            this.lblNombreEntrada.AutoSize = true;
            this.lblNombreEntrada.Location = new System.Drawing.Point(20, 460);
            this.lblNombreEntrada.Name = "lblNombreEntrada";
            this.lblNombreEntrada.Size = new System.Drawing.Size(122, 32);
            this.lblNombreEntrada.TabIndex = 41;
            this.lblNombreEntrada.Text = "Nombre variable o \r\nvector de entrada:";
            // 
            // lblCalculo
            // 
            this.lblCalculo.AutoSize = true;
            this.lblCalculo.Location = new System.Drawing.Point(22, 409);
            this.lblCalculo.Name = "lblCalculo";
            this.lblCalculo.Size = new System.Drawing.Size(55, 16);
            this.lblCalculo.TabIndex = 39;
            this.lblCalculo.Text = "Cálculo:";
            // 
            // lblArchivoPlanMath
            // 
            this.lblArchivoPlanMath.Location = new System.Drawing.Point(20, 354);
            this.lblArchivoPlanMath.Name = "lblArchivoPlanMath";
            this.lblArchivoPlanMath.Size = new System.Drawing.Size(80, 43);
            this.lblArchivoPlanMath.TabIndex = 38;
            this.lblArchivoPlanMath.Text = "Archivo de PlanMath:";
            // 
            // txtTipoEntrada
            // 
            this.txtTipoEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTipoEntrada.Location = new System.Drawing.Point(160, 315);
            this.txtTipoEntrada.Name = "txtTipoEntrada";
            this.txtTipoEntrada.ReadOnly = true;
            this.txtTipoEntrada.Size = new System.Drawing.Size(674, 22);
            this.txtTipoEntrada.TabIndex = 32;
            this.txtTipoEntrada.TabStop = false;
            // 
            // lblTipoEntrada
            // 
            this.lblTipoEntrada.AutoSize = true;
            this.lblTipoEntrada.Location = new System.Drawing.Point(20, 318);
            this.lblTipoEntrada.Name = "lblTipoEntrada";
            this.lblTipoEntrada.Size = new System.Drawing.Size(38, 16);
            this.lblTipoEntrada.TabIndex = 37;
            this.lblTipoEntrada.Text = "Tipo:";
            // 
            // txtLibro
            // 
            this.txtLibro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLibro.Location = new System.Drawing.Point(141, 172);
            this.txtLibro.Name = "txtLibro";
            this.txtLibro.ReadOnly = true;
            this.txtLibro.Size = new System.Drawing.Size(693, 22);
            this.txtLibro.TabIndex = 28;
            this.txtLibro.TabStop = false;
            // 
            // lblLibro
            // 
            this.lblLibro.AutoSize = true;
            this.lblLibro.Location = new System.Drawing.Point(20, 175);
            this.lblLibro.Name = "lblLibro";
            this.lblLibro.Size = new System.Drawing.Size(40, 16);
            this.lblLibro.TabIndex = 35;
            this.lblLibro.Text = "Libro:";
            // 
            // txtCeldas
            // 
            this.txtCeldas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCeldas.Location = new System.Drawing.Point(123, 57);
            this.txtCeldas.Name = "txtCeldas";
            this.txtCeldas.ReadOnly = true;
            this.txtCeldas.Size = new System.Drawing.Size(690, 22);
            this.txtCeldas.TabIndex = 31;
            this.txtCeldas.TabStop = false;
            // 
            // lblCeldas
            // 
            this.lblCeldas.AutoSize = true;
            this.lblCeldas.Location = new System.Drawing.Point(4, 60);
            this.lblCeldas.Name = "lblCeldas";
            this.lblCeldas.Size = new System.Drawing.Size(53, 16);
            this.lblCeldas.TabIndex = 33;
            this.lblCeldas.Text = "Celdas:";
            // 
            // txtHoja
            // 
            this.txtHoja.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHoja.Location = new System.Drawing.Point(123, 3);
            this.txtHoja.Name = "txtHoja";
            this.txtHoja.ReadOnly = true;
            this.txtHoja.Size = new System.Drawing.Size(690, 22);
            this.txtHoja.TabIndex = 29;
            this.txtHoja.TabStop = false;
            // 
            // lblHoja
            // 
            this.lblHoja.AutoSize = true;
            this.lblHoja.Location = new System.Drawing.Point(4, 9);
            this.lblHoja.Name = "lblHoja";
            this.lblHoja.Size = new System.Drawing.Size(39, 16);
            this.lblHoja.TabIndex = 30;
            this.lblHoja.Text = "Hoja:";
            // 
            // txtEntrada
            // 
            this.txtEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntrada.Location = new System.Drawing.Point(160, 460);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.ReadOnly = true;
            this.txtEntrada.Size = new System.Drawing.Size(674, 22);
            this.txtEntrada.TabIndex = 44;
            this.txtEntrada.TabStop = false;
            // 
            // txtCalculo
            // 
            this.txtCalculo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCalculo.Location = new System.Drawing.Point(160, 414);
            this.txtCalculo.Name = "txtCalculo";
            this.txtCalculo.ReadOnly = true;
            this.txtCalculo.Size = new System.Drawing.Size(674, 22);
            this.txtCalculo.TabIndex = 43;
            this.txtCalculo.TabStop = false;
            // 
            // txtArchivoPlanMath
            // 
            this.txtArchivoPlanMath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArchivoPlanMath.Location = new System.Drawing.Point(160, 366);
            this.txtArchivoPlanMath.Name = "txtArchivoPlanMath";
            this.txtArchivoPlanMath.ReadOnly = true;
            this.txtArchivoPlanMath.Size = new System.Drawing.Size(674, 22);
            this.txtArchivoPlanMath.TabIndex = 42;
            this.txtArchivoPlanMath.TabStop = false;
            // 
            // lblNombresDescripciones
            // 
            this.lblNombresDescripciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombresDescripciones.Location = new System.Drawing.Point(22, 512);
            this.lblNombresDescripciones.Name = "lblNombresDescripciones";
            this.lblNombresDescripciones.Size = new System.Drawing.Size(812, 88);
            this.lblNombresDescripciones.TabIndex = 45;
            this.lblNombresDescripciones.Text = resources.GetString("lblNombresDescripciones.Text");
            // 
            // procesoActualizarEjecucionSeleccionada
            // 
            this.procesoActualizarEjecucionSeleccionada.Enabled = true;
            this.procesoActualizarEjecucionSeleccionada.Tick += new System.EventHandler(this.procesoActualizarEjecucionSeleccionada_Tick);
            // 
            // btnQuitarEjecucion
            // 
            this.btnQuitarEjecucion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitarEjecucion.Image = global::PlanMath_para_Excel.Properties.Resources._06;
            this.btnQuitarEjecucion.Location = new System.Drawing.Point(707, 104);
            this.btnQuitarEjecucion.Name = "btnQuitarEjecucion";
            this.btnQuitarEjecucion.Size = new System.Drawing.Size(57, 38);
            this.btnQuitarEjecucion.TabIndex = 3;
            this.btnQuitarEjecucion.UseVisualStyleBackColor = true;
            this.btnQuitarEjecucion.Click += new System.EventHandler(this.btnQuitarEjecucion_Click);
            // 
            // iconoEnvioOk
            // 
            this.iconoEnvioOk.Image = global::PlanMath_para_Excel.Properties.Resources._24;
            this.iconoEnvioOk.Location = new System.Drawing.Point(316, 32);
            this.iconoEnvioOk.Name = "iconoEnvioOk";
            this.iconoEnvioOk.Size = new System.Drawing.Size(32, 32);
            this.iconoEnvioOk.TabIndex = 24;
            this.iconoEnvioOk.TabStop = false;
            this.iconoEnvioOk.Click += new System.EventHandler(this.iconoEnvioOk_Click);
            // 
            // btnEnviarEntrada
            // 
            this.btnEnviarEntrada.Image = global::PlanMath_para_Excel.Properties.Resources._25;
            this.btnEnviarEntrada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviarEntrada.Location = new System.Drawing.Point(18, 19);
            this.btnEnviarEntrada.Name = "btnEnviarEntrada";
            this.btnEnviarEntrada.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnEnviarEntrada.Size = new System.Drawing.Size(274, 58);
            this.btnEnviarEntrada.TabIndex = 0;
            this.btnEnviarEntrada.Text = "Enviar variable o vector de entrada \r\na la ejecución de PlanMath";
            this.btnEnviarEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnviarEntrada.UseVisualStyleBackColor = true;
            this.btnEnviarEntrada.Click += new System.EventHandler(this.btnEnviarEntrada_Click);
            // 
            // panelCeldasSeleccionadasAutomaticamente
            // 
            this.panelCeldasSeleccionadasAutomaticamente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCeldasSeleccionadasAutomaticamente.Controls.Add(this.lblHoja);
            this.panelCeldasSeleccionadasAutomaticamente.Controls.Add(this.txtHoja);
            this.panelCeldasSeleccionadasAutomaticamente.Controls.Add(this.lblCeldas);
            this.panelCeldasSeleccionadasAutomaticamente.Controls.Add(this.txtCeldas);
            this.panelCeldasSeleccionadasAutomaticamente.Location = new System.Drawing.Point(18, 210);
            this.panelCeldasSeleccionadasAutomaticamente.Name = "panelCeldasSeleccionadasAutomaticamente";
            this.panelCeldasSeleccionadasAutomaticamente.Size = new System.Drawing.Size(816, 105);
            this.panelCeldasSeleccionadasAutomaticamente.TabIndex = 46;
            // 
            // panelSeleccionarCeldas
            // 
            this.panelSeleccionarCeldas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSeleccionarCeldas.Controls.Add(this.mensajeErrorSeleccion);
            this.panelSeleccionarCeldas.Controls.Add(this.establecerTextosNumeros);
            this.panelSeleccionarCeldas.Controls.Add(this.hojaSeleccionada);
            this.panelSeleccionarCeldas.Controls.Add(this.label1);
            this.panelSeleccionarCeldas.Controls.Add(this.celdasSeleccionadas);
            this.panelSeleccionarCeldas.Controls.Add(this.label2);
            this.panelSeleccionarCeldas.Controls.Add(this.btnActivarDesactivarSeleccionandoCeldas);
            this.panelSeleccionarCeldas.Location = new System.Drawing.Point(18, 210);
            this.panelSeleccionarCeldas.Name = "panelSeleccionarCeldas";
            this.panelSeleccionarCeldas.Size = new System.Drawing.Size(816, 105);
            this.panelSeleccionarCeldas.TabIndex = 47;
            this.panelSeleccionarCeldas.Visible = false;
            // 
            // mensajeErrorSeleccion
            // 
            this.mensajeErrorSeleccion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mensajeErrorSeleccion.ForeColor = System.Drawing.Color.Red;
            this.mensajeErrorSeleccion.Location = new System.Drawing.Point(619, 16);
            this.mensajeErrorSeleccion.Name = "mensajeErrorSeleccion";
            this.mensajeErrorSeleccion.Size = new System.Drawing.Size(176, 34);
            this.mensajeErrorSeleccion.TabIndex = 71;
            this.mensajeErrorSeleccion.Text = "<mensaje>";
            // 
            // establecerTextosNumeros
            // 
            this.establecerTextosNumeros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.establecerTextosNumeros.Location = new System.Drawing.Point(653, 53);
            this.establecerTextosNumeros.Name = "establecerTextosNumeros";
            this.establecerTextosNumeros.Size = new System.Drawing.Size(142, 26);
            this.establecerTextosNumeros.TabIndex = 70;
            this.establecerTextosNumeros.Text = "Números y cadenas";
            this.establecerTextosNumeros.UseVisualStyleBackColor = true;
            this.establecerTextosNumeros.Click += new System.EventHandler(this.establecerTextosNumeros_Click);
            // 
            // hojaSeleccionada
            // 
            this.hojaSeleccionada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.hojaSeleccionada.Location = new System.Drawing.Point(246, 16);
            this.hojaSeleccionada.Name = "hojaSeleccionada";
            this.hojaSeleccionada.ReadOnly = true;
            this.hojaSeleccionada.Size = new System.Drawing.Size(358, 22);
            this.hojaSeleccionada.TabIndex = 68;
            this.hojaSeleccionada.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 69;
            this.label1.Text = "Hoja:";
            // 
            // celdasSeleccionadas
            // 
            this.celdasSeleccionadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.celdasSeleccionadas.Location = new System.Drawing.Point(246, 53);
            this.celdasSeleccionadas.Name = "celdasSeleccionadas";
            this.celdasSeleccionadas.ReadOnly = true;
            this.celdasSeleccionadas.Size = new System.Drawing.Size(358, 22);
            this.celdasSeleccionadas.TabIndex = 66;
            this.celdasSeleccionadas.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 67;
            this.label2.Text = "Celdas:";
            // 
            // btnActivarDesactivarSeleccionandoCeldas
            // 
            this.btnActivarDesactivarSeleccionandoCeldas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivarDesactivarSeleccionandoCeldas.Image = global::PlanMath_para_Excel.Properties.Resources._03;
            this.btnActivarDesactivarSeleccionandoCeldas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActivarDesactivarSeleccionandoCeldas.Location = new System.Drawing.Point(8, 19);
            this.btnActivarDesactivarSeleccionandoCeldas.Name = "btnActivarDesactivarSeleccionandoCeldas";
            this.btnActivarDesactivarSeleccionandoCeldas.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnActivarDesactivarSeleccionandoCeldas.Size = new System.Drawing.Size(162, 71);
            this.btnActivarDesactivarSeleccionandoCeldas.TabIndex = 65;
            this.btnActivarDesactivarSeleccionandoCeldas.Text = "Comenzar a seleccionar celdas";
            this.btnActivarDesactivarSeleccionandoCeldas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActivarDesactivarSeleccionandoCeldas.UseVisualStyleBackColor = true;
            this.btnActivarDesactivarSeleccionandoCeldas.Click += new System.EventHandler(this.btnActivarDesactivarSeleccionandoCeldas_Click);
            // 
            // PanelEnviarEntradas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnQuitarEjecucion);
            this.Controls.Add(this.lblNombresDescripciones);
            this.Controls.Add(this.txtEntrada);
            this.Controls.Add(this.txtCalculo);
            this.Controls.Add(this.txtArchivoPlanMath);
            this.Controls.Add(this.lblNombreEntrada);
            this.Controls.Add(this.lblCalculo);
            this.Controls.Add(this.lblArchivoPlanMath);
            this.Controls.Add(this.txtTipoEntrada);
            this.Controls.Add(this.lblTipoEntrada);
            this.Controls.Add(this.txtLibro);
            this.Controls.Add(this.lblLibro);
            this.Controls.Add(this.btnEjecucionSiguiente);
            this.Controls.Add(this.btnEjecucionAnterior);
            this.Controls.Add(this.ejecucionSeleccionada);
            this.Controls.Add(this.iconoEnvioOk);
            this.Controls.Add(this.lblEnvio);
            this.Controls.Add(this.btnEnviarEntrada);
            this.Controls.Add(this.panelSeleccionarCeldas);
            this.Controls.Add(this.panelCeldasSeleccionadasAutomaticamente);
            this.Name = "PanelEnviarEntradas";
            this.Size = new System.Drawing.Size(877, 703);
            this.Load += new System.EventHandler(this.PanelEnviarEntradas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconoEnvioOk)).EndInit();
            this.panelCeldasSeleccionadasAutomaticamente.ResumeLayout(false);
            this.panelCeldasSeleccionadasAutomaticamente.PerformLayout();
            this.panelSeleccionarCeldas.ResumeLayout(false);
            this.panelSeleccionarCeldas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox iconoEnvioOk;
        internal System.Windows.Forms.Label lblEnvio;
        private System.Windows.Forms.Button btnEjecucionAnterior;
        private System.Windows.Forms.Button btnEjecucionSiguiente;
        private System.Windows.Forms.Label lblNombreEntrada;
        private System.Windows.Forms.Label lblCalculo;
        private System.Windows.Forms.Label lblArchivoPlanMath;
        internal System.Windows.Forms.TextBox txtTipoEntrada;
        private System.Windows.Forms.Label lblTipoEntrada;
        internal System.Windows.Forms.TextBox txtLibro;
        private System.Windows.Forms.Label lblLibro;
        internal System.Windows.Forms.TextBox txtCeldas;
        private System.Windows.Forms.Label lblCeldas;
        internal System.Windows.Forms.TextBox txtHoja;
        private System.Windows.Forms.Label lblHoja;
        internal System.Windows.Forms.TextBox txtEntrada;
        internal System.Windows.Forms.TextBox txtCalculo;
        internal System.Windows.Forms.TextBox txtArchivoPlanMath;
        internal System.Windows.Forms.Label lblNombresDescripciones;
        internal System.Windows.Forms.ComboBox ejecucionSeleccionada;
        internal System.Windows.Forms.Timer procesoActualizarEjecucionSeleccionada;
        private System.Windows.Forms.Button btnQuitarEjecucion;
        private System.Windows.Forms.Panel panelCeldasSeleccionadasAutomaticamente;
        private System.Windows.Forms.Panel panelSeleccionarCeldas;
        private System.Windows.Forms.Button btnActivarDesactivarSeleccionandoCeldas;
        private System.Windows.Forms.Button establecerTextosNumeros;
        internal System.Windows.Forms.TextBox hojaSeleccionada;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox celdasSeleccionadas;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label mensajeErrorSeleccion;
        public System.Windows.Forms.Button btnEnviarEntrada;
    }
}
