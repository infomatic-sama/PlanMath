using Microsoft.Win32;
using ProcessCalc.Controles;
using ProcessCalc.Controles.Ctrl_Entradas;
using ProcessCalc.Entidades;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PlanMath_para_Excel.Entradas;

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaArchivoExcelEntradaConjuntoNumeros.xaml
    /// </summary>
    public partial class VistaArchivoExcelEntradaConjuntoNumeros : UserControl
    {
        private Entrada entr;
        public Entrada Entrada
        {
            get
            {
                return entr;
            }

            set
            {
                entr = value;
                MostrarTextoArchivoCarpetaSeleccionada();

                conjuntosCeldas.Children.Clear();
                foreach (var itemConjuntoCeldas in value.ParametrosExcel)
                {
                    ConjuntoCeldasExcel conjunto = new ConjuntoCeldasExcel();
                    conjuntosCeldas.Children.Add(conjunto);
                    conjunto.ParametrosExcel = itemConjuntoCeldas;
                    conjunto.Vista = this;
                }

                lblNombreEntrada.Content = value.Nombre;

                opcionURLLibro.IsChecked = entr.UsarURL_Office;

                if (entr.ParametrosExcel.Any())
                {
                    opcionEntradaManual.IsChecked = entr.ParametrosExcel.FirstOrDefault().EntradaManual;
                }
                
                MostrarOcultarElementosVisuales_EntradaManual(entr.EntradaArchivoManual);

                entr = value;

                opcionCantidadAgregarNumeros.IsChecked = entr.UtilizarCantidadNumeros;
                if (entr.UtilizarCantidadNumeros)
                    opcionCantidadAgregarNumeros_Checked(this, null);
                else
                    opcionCantidadAgregarNumeros_Unchecked(this, null);

                opcionUtilizacionCantidadAgregarNumeros.SelectedItem = (from ComboBoxItem I in opcionUtilizacionCantidadAgregarNumeros.Items where I.Uid == ((int)entr.OpcionCantidadNumeros).ToString() select I).FirstOrDefault();

                operacionInterna.SelectedItem = (from ComboBoxItem I in operacionInterna.Items where I.Uid == ((int)value.OperacionInterna).ToString() select I).FirstOrDefault();

                if (entr.EntradaArchivoManual)
                    opcionEntradaManual.IsEnabled = false;
            }
        }
        public EntradaEspecifica VistaEntrada { get; set; }
        public Calculo Calculo { get; set; }
        public VistaArchivoExcelEntradaConjuntoNumeros()
        {
            InitializeComponent();
        }

        private void btnCambiarArchivo_Click(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                seleccionar.Entrada = entr;
                seleccionar.VistaEntrada = VistaEntrada;
                seleccionar.ArchivoEntrada = entr.ListaArchivos.FirstOrDefault();
                seleccionar.RutaCarpeta = Calculo.RutaArchivo == null ? string.Empty : Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\"));
                seleccionar.ShowDialog();

                MostrarTextoArchivoCarpetaSeleccionada();
            }
        }

        private void MostrarTextoArchivoCarpetaSeleccionada()
        {
            string nombreArchivoCarpeta = string.Empty;

            if (entr.Tipo == TipoEntrada.Numero)
                nombreArchivoCarpeta = entr.ListaArchivos.FirstOrDefault().RutaArchivoEntrada;
            else if (entr.Tipo == TipoEntrada.ConjuntoNumeros)
                nombreArchivoCarpeta = entr.ListaArchivos.FirstOrDefault().RutaArchivoConjuntoNumerosEntrada;

            switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionCarpeta)
            {
                case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                    switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            lblRutaArchivo.Content = "Se obtendrá el vector de números de la variable o vector de entrada, del archivo " + nombreArchivoCarpeta;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            lblRutaArchivo.Content = "Se obtendrá el vector de números de la variable o vector de entrada, del archivo seleccionado en la carpeta " + nombreArchivoCarpeta;
                            break;

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                            lblRutaArchivo.Content = "Para algunas variables o vectores de entrada, se obtendrá el vector de números de la variable o vector de entrada, del archivo " + nombreArchivoCarpeta;
                            break;
                    }

                    break;

                case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                    switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            lblRutaArchivo.Content = "Se obtendrá el vector de números de la variable o vector de entrada, del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            lblRutaArchivo.Content = "Se obtendrá el cvector de números de la variable o vector de entrada, del archivo seleccionado en la carpeta en el momento de la ejecución.";
                            break;

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                            lblRutaArchivo.Content = "Para algunas variables o vectores de entrada, se obtendrá el vector de números de la variable o vector de entrada, del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                            break;
                    }

                    break;
            }
        }
    
        private void MostrarTextoConfiguracion_SeleccionArchivoCarpeta()
        {
            if (entr != null)
            {
                string strCarpeta = string.Empty;
                string strArchivo = string.Empty;

                switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionCarpeta)
                {
                    case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:
                        strCarpeta = "Buscar en la carpeta indicada.";
                        break;

                    case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:
                        strCarpeta = "Buscar en la carpeta en que está el archivo de cálculo en el momento de la ejecución.";
                        break;
                }

                switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionArchivo)
                {
                    case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                        strArchivo = "Utilizar el archivo indicado.";
                        break;

                    case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                        strArchivo = "Seleccionar el archivo, en el momento de la ejecución del archivo de cálculo.";
                        break;

                    case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                        strArchivo = "Seleccionar el archivo, según lo indique la variable o vector de entrada en las variables o vectores, en el momento de la ejecución del archivo de cálculo.";
                        break;
                }

                etiquetaDefinicionSeleccionArchivoCarpeta.Text = "Definición actual: " + strArchivo + " " + strCarpeta;
            }
            else
                etiquetaDefinicionSeleccionArchivoCarpeta.Text = "";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
        }

        private void configurarSeleccionCarpetaArchivo_Click(object sender, RoutedEventArgs e)
        {
            ConfigCarpetaArchivo configurar = new ConfigCarpetaArchivo();
            configurar.Entrada = entr;
            configurar.ArchivoEntrada = entr.ListaArchivos.FirstOrDefault();
            configurar.ShowDialog();
            MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
            MostrarTextoArchivoCarpetaSeleccionada();
        }

        private void opcionURLLibro_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UsarURL_Office = (bool)opcionURLLibro.IsChecked;
            }
        }

        private void opcionURLLibro_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UsarURL_Office = (bool)opcionURLLibro.IsChecked;
            }
        }

        private void opcionCantidadAgregarNumeros_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UtilizarCantidadNumeros = (bool)opcionCantidadAgregarNumeros.IsChecked;
                opcionUtilizacionCantidadAgregarNumeros.Visibility = Visibility.Visible;
            }
        }

        private void opcionCantidadAgregarNumeros_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UtilizarCantidadNumeros = (bool)opcionCantidadAgregarNumeros.IsChecked;
                opcionUtilizacionCantidadAgregarNumeros.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionUtilizacionCantidadAgregarNumeros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (entr != null)
            {
                entr.OpcionCantidadNumeros = (OpcionCantidadNumerosEntrada)int.Parse(((ComboBoxItem)opcionUtilizacionCantidadAgregarNumeros.SelectedItem).Uid); ;
            }
        }

        private void btnDefinirTextosInformacionFijos_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarOrdenarCantidades seleccionarOrdenar = new SeleccionarOrdenarCantidades();
            seleccionarOrdenar.listaTextos.TextosInformacion = Entrada.TextosInformacionFijos.ToList();

            bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
            if (definicionEstablecida)
            {
                Entrada.TextosInformacionFijos = seleccionarOrdenar.listaTextos.TextosInformacion;
            }
        }

        private void operacionInterna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoElementoOperacion tipo = (TipoElementoOperacion)int.Parse(((ComboBoxItem)operacionInterna.SelectedItem).Uid);

            if (entr != null)
                entr.OperacionInterna = tipo;

            btnDefinirTextosInformacionOperacionInterna.Visibility = Visibility.Collapsed;
            opcionesElementosFijosInverso.Visibility = Visibility.Collapsed;

            if (entr.OperacionInterna == TipoElementoOperacion.SeleccionarOrdenar)
                btnDefinirTextosInformacionOperacionInterna.Visibility = Visibility.Visible;

            if (entr.OperacionInterna == TipoElementoOperacion.Inverso)
            {
                opcionesElementosFijosInverso.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosInverso.Items where I.Uid == ((int)entr.OpcionElementosFijosInverso).ToString() select I).FirstOrDefault();
                opcionesElementosFijosInverso.Visibility = Visibility.Visible;
            }
        }

        private void btnDefinirTextosInformacionOperacionInterna_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarOrdenarCantidades seleccionarOrdenar = new SeleccionarOrdenarCantidades();
            seleccionarOrdenar.listaTextos.TextosInformacion = entr.TextosInformacion_OperacionInterna.ToList();

            bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
            if (definicionEstablecida)
            {
                entr.TextosInformacion_OperacionInterna = seleccionarOrdenar.listaTextos.TextosInformacion;
            }
        }

        private void opcionesElementosFijosInverso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entr != null)
                {
                    if (opcionesElementosFijosInverso.SelectedItem != null)
                        entr.OpcionElementosFijosInverso = (TipoOpcionElementosFijosOperacionInverso)int.Parse(((ComboBoxItem)opcionesElementosFijosInverso.SelectedItem).Uid);
                }
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void opcionEntradaManual_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                if (entr.ParametrosExcel.Any())
                {
                    foreach (var item in entr.ParametrosExcel)
                    {
                        item.EntradaManual = (bool)opcionEntradaManual.IsChecked;
                    }
                }
                
                MostrarOcultarElementosVisuales_EntradaManual((bool)opcionEntradaManual.IsChecked);
            }
        }

        private void opcionEntradaManual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                if (entr.ParametrosExcel.Any())
                {
                    foreach (var item in entr.ParametrosExcel)
                    {
                        item.EntradaManual = (bool)opcionEntradaManual.IsChecked;
                    }
                }

                MostrarOcultarElementosVisuales_EntradaManual((bool)opcionEntradaManual.IsChecked);
            }
        }

        private void MostrarOcultarElementosVisuales_EntradaManual(bool entradaManual)
        {
            if (entradaManual)
            {
                conjuntosCeldas.Visibility = Visibility.Collapsed;
            }
            else
            {
                conjuntosCeldas.Visibility = Visibility.Visible;
            }
        }
    }
}
