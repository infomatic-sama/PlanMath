namespace PlanMath_para_Word
{
    partial class MenuOpciones : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MenuOpciones()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.group1 = this.Factory.CreateRibbonGroup();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.opcionesPlanMath = this.Factory.CreateRibbonTab();
            this.opcionesEntradas = this.Factory.CreateRibbonGroup();
            this.definirEntrada = this.Factory.CreateRibbonButton();
            this.enviarEntradas = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.definirEntradaManual = this.Factory.CreateRibbonButton();
            this.enviarEntradasManuales = this.Factory.CreateRibbonButton();
            this.opcionesResultados = this.Factory.CreateRibbonGroup();
            this.definirResultados = this.Factory.CreateRibbonButton();
            this.obtenerResultados = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.opcionesPlanMath.SuspendLayout();
            this.opcionesEntradas.SuspendLayout();
            this.opcionesResultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // group1
            // 
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "PlanMath";
            this.tab1.Name = "tab1";
            // 
            // opcionesPlanMath
            // 
            this.opcionesPlanMath.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.opcionesPlanMath.Groups.Add(this.opcionesEntradas);
            this.opcionesPlanMath.Groups.Add(this.opcionesResultados);
            this.opcionesPlanMath.Label = "PlanMath";
            this.opcionesPlanMath.Name = "opcionesPlanMath";
            // 
            // opcionesEntradas
            // 
            this.opcionesEntradas.Items.Add(this.definirEntrada);
            this.opcionesEntradas.Items.Add(this.enviarEntradas);
            this.opcionesEntradas.Items.Add(this.separator1);
            this.opcionesEntradas.Items.Add(this.definirEntradaManual);
            this.opcionesEntradas.Items.Add(this.enviarEntradasManuales);
            this.opcionesEntradas.Label = "Entradas de cálculos";
            this.opcionesEntradas.Name = "opcionesEntradas";
            // 
            // definirEntrada
            // 
            this.definirEntrada.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.definirEntrada.Image = global::PlanMath_para_Word.Properties.Resources._23;
            this.definirEntrada.Label = "Definir variables o vectores de entradas";
            this.definirEntrada.Name = "definirEntrada";
            this.definirEntrada.ShowImage = true;
            this.definirEntrada.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.definirEntrada_Click);
            // 
            // enviarEntradas
            // 
            this.enviarEntradas.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.enviarEntradas.Image = global::PlanMath_para_Word.Properties.Resources._25;
            this.enviarEntradas.Label = "Enviar variables o vectores de entradas";
            this.enviarEntradas.Name = "enviarEntradas";
            this.enviarEntradas.ShowImage = true;
            this.enviarEntradas.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.enviarEntradas_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // definirEntradaManual
            // 
            this.definirEntradaManual.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.definirEntradaManual.Image = global::PlanMath_para_Word.Properties.Resources._01_definir;
            this.definirEntradaManual.Label = "Definir variables o vectores de entradas manuales";
            this.definirEntradaManual.Name = "definirEntradaManual";
            this.definirEntradaManual.ShowImage = true;
            this.definirEntradaManual.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.definirEntradaManual_Click_1);
            // 
            // enviarEntradasManuales
            // 
            this.enviarEntradasManuales.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.enviarEntradasManuales.Image = global::PlanMath_para_Word.Properties.Resources._02_enviar;
            this.enviarEntradasManuales.Label = "Enviar variables o vectores de entradas manuales";
            this.enviarEntradasManuales.Name = "enviarEntradasManuales";
            this.enviarEntradasManuales.ShowImage = true;
            this.enviarEntradasManuales.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.enviarEntradasManuales_Click_1);
            // 
            // opcionesResultados
            // 
            this.opcionesResultados.Items.Add(this.definirResultados);
            this.opcionesResultados.Items.Add(this.obtenerResultados);
            this.opcionesResultados.Label = "Resultados de cálculos";
            this.opcionesResultados.Name = "opcionesResultados";
            // 
            // definirResultados
            // 
            this.definirResultados.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.definirResultados.Label = "Definir variables o vectores retornados";
            this.definirResultados.Name = "definirResultados";
            this.definirResultados.ShowImage = true;
            this.definirResultados.Visible = false;
            // 
            // obtenerResultados
            // 
            this.obtenerResultados.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.obtenerResultados.Label = "Obtener variables o vectores retornados";
            this.obtenerResultados.Name = "obtenerResultados";
            this.obtenerResultados.ShowImage = true;
            this.obtenerResultados.Visible = false;
            // 
            // MenuOpciones
            // 
            this.Name = "MenuOpciones";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.opcionesPlanMath);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.opcionesPlanMath.ResumeLayout(false);
            this.opcionesPlanMath.PerformLayout();
            this.opcionesEntradas.ResumeLayout(false);
            this.opcionesEntradas.PerformLayout();
            this.opcionesResultados.ResumeLayout(false);
            this.opcionesResultados.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab opcionesPlanMath;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup opcionesEntradas;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton definirEntrada;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton enviarEntradas;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup opcionesResultados;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton definirResultados;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton obtenerResultados;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton definirEntradaManual;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton enviarEntradasManuales;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
    }

    partial class ThisRibbonCollection
    {
        internal MenuOpciones Ribbon1
        {
            get { return this.GetRibbon<MenuOpciones>(); }
        }
    }
}
