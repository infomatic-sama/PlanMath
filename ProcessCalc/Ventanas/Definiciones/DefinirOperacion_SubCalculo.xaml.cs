using ProcessCalc.Controles.Archivos;
using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas.Definiciones
{
    /// <summary>
    /// Lógica de interacción para DefinirOperacion_SubCalculo.xaml
    /// </summary>
    public partial class DefinirOperacion_SubCalculo : Window
    {
        public ConfiguracionSubCalculo Config { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public bool ModoOperacion { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasOperandos_SubCalculo { get; set; }
        public List<AsociacionResultadoOperando_ArchivoExterno> ResultadosOperandos_SubCalculo { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasSubOperandos_SubCalculo { get; set; }
        public List<AsociacionResultadoOperando_ArchivoExterno> ResultadosSubOperandos_SubCalculo { get; set; }
        public List<Entrada> EntradasSubCalculo { get; set; }
        public List<Resultado> ResultadosSubCalculo { get; set; }
        public List<DiseñoOperacion> OperandosPosteriores { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosPosteriores { get; set; }
        public bool AbrirDefinicionSubCalculo { get; set; }
        public string NombreOperacion {  get; set; }
        bool Cargando;
        public DefinirOperacion_SubCalculo()
        {
            InitializeComponent();
        }


        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            AbrirDefinicionSubCalculo = false;
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            AbrirDefinicionSubCalculo = false;
            Close();
        }

        private void optDefinicionNormal_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Config != null)
                {
                    if (optDefinicionNormal.IsChecked == true)
                        Config.ModoSubCalculo_Simple = false;
                }
            }
        }

        private void optDefinicionSimple_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded &&
                !Cargando)
            {
                if (Config != null)
                {
                    if (optDefinicionSimple.IsChecked == true)
                    {
                        Config.SubCalculo_Operacion = new Calculo();
                        Config.SubCalculo_Operacion.AgregarCalculoInicial();
                        Config.SubCalculo_Operacion.ID = App.GenerarID_Elemento();
                        Config.SubCalculo_Operacion.ModoSubCalculo = true;
                        Config.ModoSubCalculo_Simple = true;
                        Config.SubCalculo_Operacion.NombreArchivo = NombreOperacion;

                        MessageBox.Show("Este subcálculo se ha restablecido al definirse como simple. Esto significa que se ha eliminado toda su configuración y elementos definidos (si se definieron antes). Si quieres recuperarlo, cierra esta ventana con el botón Cancelar.", "Subcálculo restablecido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Config != null)
            {
                Cargando = true;

                if (Config.ModoSubCalculo_Simple)
                    optDefinicionSimple.IsChecked = Config.ModoSubCalculo_Simple;
                else
                    optDefinicionNormal.IsChecked = true;

                opcionEjecucionSin_InfoVisual.IsChecked = Config.EjecutarSubCalculoSin_InfoVisual;
                opcionEjecucion_MismaEjecucion.IsChecked = Config.EjecutarSubCalculo_MismaEjecucion;

                CargarElementosArchivo_Calculo();
                ListarElementosArchivo_Calculo();

                Cargando = false;
            }
        }

        private void opcionEjecucionSin_InfoVisual_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Config != null)
                {
                    Config.EjecutarSubCalculoSin_InfoVisual = (bool)opcionEjecucionSin_InfoVisual.IsChecked;
                }
            }
        }

        private void opcionEjecucionSin_InfoVisual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Config != null)
                {
                    Config.EjecutarSubCalculoSin_InfoVisual = (bool)opcionEjecucionSin_InfoVisual.IsChecked;
                }
            }
        }

        private void opcionEjecucion_MismaEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Config != null)
                {
                    Config.EjecutarSubCalculo_MismaEjecucion = (bool)opcionEjecucion_MismaEjecucion.IsChecked;
                }
            }
        }

        private void opcionEjecucion_MismaEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Config != null)
                {
                    Config.EjecutarSubCalculo_MismaEjecucion = (bool)opcionEjecucion_MismaEjecucion.IsChecked;
                }
            }
        }

        private void ListarElementosArchivo_Calculo()
        {
            operandosEntradas.Children.Clear();
            operandosSalidas.Children.Clear();
            configEntradas.Children.Clear();

            if (ModoOperacion)
            {
                foreach (var item in SubOperandos)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;
                    botonOperando.MaxHeight = 35;

                    botonOperando.ContextMenu = new ContextMenu();

                    foreach (var itemMenu in EntradasSubCalculo)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = itemMenu.NombreCombo;
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = EntradasSubOperandos_SubCalculo.Where(i => i.SubOperacion == item & i.IDEntrada_Externa == itemMenu.ID).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosEntradas.Children.Add(botonOperando);
                }

                foreach (var item in SubOperandosPosteriores)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;
                    botonOperando.MaxHeight = 35;

                    botonOperando.ContextMenu = new ContextMenu();

                    foreach (var itemMenu in ResultadosSubCalculo)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = (!string.IsNullOrEmpty(itemMenu.Nombre) ? itemMenu.Nombre : "Variable o vector: " + itemMenu.SalidaRelacionada.NombreCombo);
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = ResultadosSubOperandos_SubCalculo.Where(i => i.SubOperacion == item & i.IDResultado == itemMenu.ID).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click1;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosSalidas.Children.Add(botonOperando);
                }

                foreach (var itemConfig in EntradasSubOperandos_SubCalculo)
                {
                    ConfigEntradas_ArchivoExterno config = new ConfigEntradas_ArchivoExterno();
                    var entrada = EntradasSubCalculo.FirstOrDefault(i => i.ID == itemConfig.IDEntrada_Externa);
                    config.NombreEntrada = entrada.NombreCombo.ToString();
                    config.IDEntrada = entrada.ID.ToString();
                    config.SubOperando = itemConfig.SubOperacion;
                    config.Config = itemConfig.Configuracion;

                    configEntradas.Children.Add(config);
                }

            }
            else
            {
                foreach (var item in Operandos)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;
                    botonOperando.MaxHeight = 35;

                    botonOperando.ContextMenu = new ContextMenu();

                    foreach (var itemMenu in EntradasSubCalculo)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = itemMenu.NombreCombo;
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = EntradasOperandos_SubCalculo.Where(i => i.Operacion == item & i.IDEntrada_Externa == itemMenu.ID).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosEntradas.Children.Add(botonOperando);
                }

                foreach (var item in OperandosPosteriores)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;
                    botonOperando.MaxHeight = 35;

                    botonOperando.ContextMenu = new ContextMenu();

                    foreach (var itemMenu in ResultadosSubCalculo)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = (!string.IsNullOrEmpty(itemMenu.Nombre) ? itemMenu.Nombre : "Variable o vector: " + itemMenu.SalidaRelacionada.NombreCombo);
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = ResultadosOperandos_SubCalculo.Where(i => i.Operacion == item & i.IDResultado == itemMenu.ID).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click1;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosSalidas.Children.Add(botonOperando);
                }

                foreach (var itemConfig in EntradasOperandos_SubCalculo)
                {
                    ConfigEntradas_ArchivoExterno config = new ConfigEntradas_ArchivoExterno();
                    var entrada = EntradasSubCalculo.FirstOrDefault(i => i.ID == itemConfig.IDEntrada_Externa);
                    config.NombreEntrada = entrada.NombreCombo.ToString();
                    config.IDEntrada = entrada.ID.ToString();
                    config.Operando = itemConfig.Operacion;
                    config.Config = itemConfig.Configuracion;

                    configEntradas.Children.Add(config);
                }
            }

            if (operandosSalidas.Children.Count == 0)
                textoResultados.Visibility = Visibility.Collapsed;
            else
                textoResultados.Visibility = Visibility.Visible;
        }

        private void OpcionMenu_Click1(object sender, RoutedEventArgs e)
        {
            object[] objetos = (object[])((CheckBox)sender).Tag;
            bool seleccionado = (bool)((CheckBox)sender).IsChecked;

            if (ModoOperacion)
            {
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[0];
                Resultado resultado = (Resultado)objetos[1];

                var dupla = ResultadosSubOperandos_SubCalculo.Where(i => i.SubOperacion == operando & i.IDResultado == resultado.ID).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        ResultadosSubOperandos_SubCalculo.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        ResultadosSubOperandos_SubCalculo.Add(new AsociacionResultadoOperando_ArchivoExterno { SubOperacion = operando, IDResultado = resultado.ID });
                    }
                }
            }
            else
            {
                DiseñoOperacion operando = (DiseñoOperacion)objetos[0];
                Resultado resultado = (Resultado)objetos[1];

                var dupla = ResultadosOperandos_SubCalculo.Where(i => i.Operacion == operando & i.IDResultado == resultado.ID).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        ResultadosOperandos_SubCalculo.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        ResultadosOperandos_SubCalculo.Add(new AsociacionResultadoOperando_ArchivoExterno { Operacion = operando, IDResultado = resultado.ID });
                    }
                }
            }

            CargarElementosArchivo_Calculo();
            ListarElementosArchivo_Calculo();
        }

        private void BotonOperando_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void OpcionMenu_Click(object sender, RoutedEventArgs e)
        {
            object[] objetos = (object[])((CheckBox)sender).Tag;
            bool seleccionado = (bool)((CheckBox)sender).IsChecked;

            if (ModoOperacion)
            {
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[0];
                Entrada entrada = (Entrada)objetos[1];

                var dupla = EntradasSubOperandos_SubCalculo.Where(i => i.SubOperacion == operando & i.IDEntrada_Externa == entrada.ID).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasSubOperandos_SubCalculo.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasSubOperandos_SubCalculo.Add(new AsociacionEntradaOperando_ArchivoExterno(TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionOperador) { SubOperacion = operando, IDEntrada_Externa = entrada.ID });
                    }
                }
            }
            else
            {
                DiseñoOperacion operando = (DiseñoOperacion)objetos[0];
                Entrada entrada = (Entrada)objetos[1];

                var dupla = EntradasOperandos_SubCalculo.Where(i => i.Operacion == operando & i.IDEntrada_Externa == entrada.ID).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasOperandos_SubCalculo.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasOperandos_SubCalculo.Add(new AsociacionEntradaOperando_ArchivoExterno(TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionOperador) { Operacion = operando, IDEntrada_Externa = entrada.ID });
                    }
                }
            }

            CargarElementosArchivo_Calculo();
            ListarElementosArchivo_Calculo();
        }

        public void CargarElementosArchivo_Calculo()
        {
            EntradasSubCalculo = Config.ObtenerEntradas_SubCalculo();
            ResultadosSubCalculo = Config.ObtenerResultados_SubCalculo();
        }

        private void btnDefinir_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            AbrirDefinicionSubCalculo = true;
            Close();
        }
    }
}