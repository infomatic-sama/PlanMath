using Microsoft.Win32;
using ProcessCalc.Entidades;
using ProcessCalc.Ventanas;
using ProcessCalc.Vistas;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para EntradaEspecifica.xaml
    /// </summary>
    public partial class EntradaEspecifica : UserControl
    {
        public DiseñoCalculo Calculo { get; set; }
        public DiseñoCalculo CalculoEntradas { get; set; }
        public EntradaEspecifica()
        {
            entr = new Entrada();
            InitializeComponent();
        }

        public ConfigEntrada VentanaEntrada { get; set; }
        private void nombreEntrada_TextChanged(object sender, TextChangedEventArgs e)
        {
            entr.Nombre = nombreEntrada.Text;
            entr.NombreEditado = true;
        }        

        private void seleccionarTipo_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarTipoEntrada seleccionar = new SeleccionarTipoEntrada();
            seleccionar.MostrarOpcionTextosInformacion = Calculo.EsEntradasArchivo;
            seleccionar.ShowDialog();
            if (seleccionar.TipoSeleccionado != TipoEntrada.Ninguno)
            {
                ActualizarTipo(seleccionar.TipoSeleccionado);
            }
        }

        private void btnNumeroFijo_Click(object sender, RoutedEventArgs e)
        {
            IngresarNumeroFijo ingresar = new IngresarNumeroFijo();
            ingresar.textos.Entrada = new Entrada("Entrada");
            ingresar.textos.Entrada.ID = App.GenerarID_Elemento();
            ingresar.textos.Entrada.Textos.AddRange(entr.Textos);
            ingresar.Numero = entr.NumeroFijo;
            ingresar.txtNumero.Text = entr.NumeroFijo.ToString();
            ingresar.ShowDialog();
            if (ingresar.numeroCambiado)
            {
                if (entr.TipoOpcionNumero != TipoOpcionNumeroEntrada.NumeroFijo)
                {
                    Ventana.CerrarPestañasEntradaModificada(entr);

                    entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.Ninguno;

                    MostrarOcultarSeDigitaConjuntoNumeros(false);
                    MostrarOcultarSeObtieneConjuntoNumeros(false);
                    MostrarOcultarConjuntoNumerosFijo(false);

                    entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.Ninguno;
                    MostrarOcultarSeDigitaTextosInformacion(false);
                    MostrarOcultarTextosInformacionFijos(false);
                    MostrarOcultarSeObtieneTextosInformacion(false);
                }

                entr.NumeroFijo = ingresar.Numero;
                entr.Textos.Clear();
                entr.Textos.AddRange(ingresar.textos.Entrada.Textos);

                entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.NumeroFijo;
                
                MostrarOcultarSeDigita(false);
                MostrarOcultarSeObtiene(false);
                MostrarOcultarNumeroFijo(true);

                Ventana.CalculoActual.QuitarConfiguracionEntrada_Ejecucion(entr);
            }
        }

        private void btnOtraEntrada_Click(object sender, RoutedEventArgs e)
        {
            VistaEntradas.AgregarNuevaEntrada();
        }

        private void btnQuitarEntrada_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Quitar la entrada de forma permanente?", "Quitar entrada", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                VistaEntradas.QuitarEntrada(this);
        }

        private void btnSeDigita_Click(object sender, RoutedEventArgs e)
        {
            if (entr.TipoOpcionNumero != TipoOpcionNumeroEntrada.SeDigita)
            {
                Ventana.CerrarPestañasEntradaModificada(entr);

                entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.Ninguno;

                MostrarOcultarSeDigitaConjuntoNumeros(false);
                MostrarOcultarSeObtieneConjuntoNumeros(false);
                MostrarOcultarConjuntoNumerosFijo(false);

                entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.Ninguno;
                MostrarOcultarTextosInformacionFijos(false);
                MostrarOcultarSeObtieneTextosInformacion(false);
                MostrarOcultarSeDigitaTextosInformacion(false);
            }

            entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.SeDigita;            

            MostrarOcultarNumeroFijo(false);
            MostrarOcultarSeObtiene(false);
            MostrarOcultarSeDigita(true);

            Ventana.CalculoActual.AgregarConfiguracionEntrada_Ejecucion(entr);
        }

        private void btnSeObtiene_Click(object sender, RoutedEventArgs e)
        {
            if (entr.TipoOpcionNumero != TipoOpcionNumeroEntrada.SeObtiene)
            {
                entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.Ninguno;

                MostrarOcultarSeDigitaConjuntoNumeros(false);
                MostrarOcultarSeObtieneConjuntoNumeros(false);
                MostrarOcultarConjuntoNumerosFijo(false);

                entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.Ninguno;
                MostrarOcultarTextosInformacionFijos(false);
                MostrarOcultarSeObtieneTextosInformacion(false);
                MostrarOcultarSeDigitaTextosInformacion(false);

                Ventana.CerrarPestañasEntradaModificada(entr);

                SeleccionarOrigenDatos seleccionar = new SeleccionarOrigenDatos();
                seleccionar.ShowDialog();

                entr.TipoFormatoArchivo_Entrada = seleccionar.TipoFormatoArchivo;

                if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.Archivo)
                {
                    //OpenFileDialog abrir = new OpenFileDialog();
                    //abrir.Filter = "Archivos de datos (texto plano) | *.txt; *.csv ; *.dat ; *.json ; *.xml";
                    //abrir.Title = "Abrir archivo";

                    //if (abrir.ShowDialog() == true)
                    //{
                        //entr.RutaArchivoEntrada = abrir.FileName;
                        //entr.NombreArchivoEntrada = abrir.FileName.Substring(abrir.FileName.LastIndexOf("\\") + 1);

                        Ventana.AgregarTabEntradaArchivo(ref entr, this);

                        entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                        entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.SeObtiene;
                    
                    MostrarOcultarSeDigita(false);
                        MostrarOcultarNumeroFijo(false);
                        MostrarOcultarSeObtiene(true);

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                    //}
                }
                else if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                {
                    Ventana.AgregarTabEntradaURL(ref entr, this);

                    entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                    entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.SeObtiene;
                    MostrarOcultarSeDigita(false);
                    MostrarOcultarNumeroFijo(false);
                    MostrarOcultarSeObtiene(true);

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.Excel)
                {
                    //OpenFileDialog abrir = new OpenFileDialog();
                    //abrir.Filter = "Archivos de Excel | *.xlsx; *.xls";
                    //abrir.Title = "Abrir archivo de Excel";

                    //if (abrir.ShowDialog() == true)
                    //{
                    //    entr.RutaArchivoEntrada = abrir.FileName;
                    //    entr.NombreArchivoEntrada = abrir.FileName.Substring(abrir.FileName.LastIndexOf("\\") + 1);

                        Ventana.AgregarTabEntradaArchivo_Excel(ref entr, this);

                        entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                        entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.SeObtiene;
                        MostrarOcultarSeDigita(false);
                        MostrarOcultarNumeroFijo(false);
                        MostrarOcultarSeObtiene(true);

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                    //}
                }
            }
            else
            {
                if (entr.TipoOrigenDatos == TipoOrigenDatos.Archivo)
                {
                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoEntradaNumero) && ((VistaArchivoEntradaNumero)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaArchivo(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                {
                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaURLEntradaNumero) && ((VistaURLEntradaNumero)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaURL(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.Excel)
                {
                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoExcelEntradaNumero) && ((VistaArchivoExcelEntradaNumero)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaArchivo_Excel(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
            }

            Ventana.CalculoActual.QuitarConfiguracionEntrada_Ejecucion(entr);
        }

        private void btnSeObtieneOtroOrigen_Click(object sender, RoutedEventArgs e)
        {
            Ventana.CerrarPestañasEntradaModificada(entr);

            entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
            columnaSeObtieneOtroOrigen.Visibility = Visibility.Collapsed;

            var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoEntradaNumero) && ((VistaArchivoEntradaNumero)T.Content).Entrada == entr) select T).FirstOrDefault();
            if (tab != null)
            {
                Ventana.QuitarTab(ref tab);
            }

            tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoExcelEntradaNumero) && ((VistaArchivoExcelEntradaNumero)T.Content).Entrada == entr) select T).FirstOrDefault();
            if (tab != null)
            {
                Ventana.QuitarTab(ref tab);
            }

            MostrarOcultarSeObtiene(false);
            btnSeObtiene_Click(this, e);
        }

        private void btnConjuntoNumerosFijo_Click(object sender, RoutedEventArgs e)
        {
            if (entr.TipoOpcionConjuntoNumeros != TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)
            {
                entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
                MostrarOcultarSeDigita(false);
                MostrarOcultarSeObtiene(false);
                MostrarOcultarNumeroFijo(false);

                entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.Ninguno;
                MostrarOcultarSeDigitaTextosInformacion(false);
                MostrarOcultarTextosInformacionFijos(false);
                MostrarOcultarSeObtieneTextosInformacion(false);

                Ventana.CerrarPestañasEntradaModificada(entr);
                Ventana.AgregarTabConjuntoNumerosEntrada(ref entr, this);

                entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo;
                MostrarOcultarSeDigitaConjuntoNumeros(false);
                MostrarOcultarConjuntoNumerosFijo(true);
                MostrarOcultarSeObtieneConjuntoNumeros(false);

                if (VentanaEntrada != null)
                    VentanaEntrada.Close();
            }
            else
            {
                var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaConjuntoNumerosEntrada) && ((VistaConjuntoNumerosEntrada)T.Content).Entrada == entr) select T).FirstOrDefault();
                if (tab != null)
                {
                    Ventana.contenido.SelectedItem = tab;
                }
                else
                {
                    Ventana.AgregarTabConjuntoNumerosEntrada(ref entr, this);
                }

                if (VentanaEntrada != null)
                    VentanaEntrada.Close();
            }

            Ventana.CalculoActual.QuitarConfiguracionEntrada_Ejecucion(entr);
        }

        private void btnSeDigitaConjunto_Click(object sender, RoutedEventArgs e)
        {
            if (entr.TipoOpcionConjuntoNumeros != TipoOpcionConjuntoNumerosEntrada.SeDigita)
            {
                Ventana.CerrarPestañasEntradaModificada(entr);

                entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
                MostrarOcultarSeDigita(false);
                MostrarOcultarSeObtiene(false);
                MostrarOcultarNumeroFijo(false);

                entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.Ninguno;
                MostrarOcultarTextosInformacionFijos(false);
                MostrarOcultarSeObtieneTextosInformacion(false);
                MostrarOcultarSeDigitaTextosInformacion(false);
            }

            entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeDigita;

            MostrarOcultarConjuntoNumerosFijo(false);
            MostrarOcultarSeObtieneConjuntoNumeros(false);
            MostrarOcultarSeDigitaConjuntoNumeros(true);

            Ventana.CalculoActual.AgregarConfiguracionEntrada_Ejecucion(entr);
        }

        private void btnSeObtieneConjunto_Click(object sender, RoutedEventArgs e)
        {
            if (entr.TipoOpcionConjuntoNumeros != TipoOpcionConjuntoNumerosEntrada.SeObtiene)
            {
                entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
                MostrarOcultarSeDigita(false);
                MostrarOcultarSeObtiene(false);
                MostrarOcultarNumeroFijo(false);

                entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.Ninguno;
                MostrarOcultarTextosInformacionFijos(false);
                MostrarOcultarSeObtieneTextosInformacion(false);
                MostrarOcultarSeDigitaTextosInformacion(false);

                Ventana.CerrarPestañasEntradaModificada(entr);
                SeleccionarOrigenDatos seleccionar = new SeleccionarOrigenDatos();
                seleccionar.ShowDialog();

                entr.TipoFormatoArchivo_Entrada = seleccionar.TipoFormatoArchivo;

                if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.Archivo)
                {
                    //OpenFileDialog abrir = new OpenFileDialog();
                    //abrir.Filter = "Archivos de datos (texto plano) | *.txt; *.csv ; *.dat ; *.json ; *.xml";
                    //abrir.Title = "Abrir archivo";

                    //if (abrir.ShowDialog() == true)
                    //{
                        //entr.RutaArchivoConjuntoNumerosEntrada = abrir.FileName;
                        //entr.NombreArchivoEntrada = abrir.FileName.Substring(abrir.FileName.LastIndexOf("\\") + 1);

                        Ventana.AgregarTabEntradaArchivoConjuntoNumeros(ref entr, this);

                        entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                        entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeObtiene;
                        MostrarOcultarSeDigitaConjuntoNumeros(false);
                        MostrarOcultarConjuntoNumerosFijo(false);
                        MostrarOcultarSeObtieneConjuntoNumeros(true);
                    //}

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                {
                    Ventana.AgregarTabEntradaURLConjuntoNumeros(ref entr, this);

                    entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                    entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeObtiene;
                    MostrarOcultarSeDigitaConjuntoNumeros(false);
                    MostrarOcultarConjuntoNumerosFijo(false);
                    MostrarOcultarSeObtieneConjuntoNumeros(true);

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.Excel)
                {
                    //OpenFileDialog abrir = new OpenFileDialog();
                    //abrir.Filter = "Archivos de Excel | *.xlsx; *.xls";
                    //abrir.Title = "Abrir archivo de Excel";

                    //if (abrir.ShowDialog() == true)
                    //{
                    //    entr.RutaArchivoConjuntoNumerosEntrada = abrir.FileName;
                    //    entr.NombreArchivoEntrada = abrir.FileName.Substring(abrir.FileName.LastIndexOf("\\") + 1);

                        Ventana.AgregarTabEntradaArchivo_Excel_ConjuntoNumeros(ref entr, this);

                        entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                        entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeObtiene;
                        MostrarOcultarSeDigitaConjuntoNumeros(false);
                        MostrarOcultarConjuntoNumerosFijo(false);
                        MostrarOcultarSeObtieneConjuntoNumeros(true);
                    //}

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
            }
            else
            {
                if (entr.TipoOrigenDatos == TipoOrigenDatos.Archivo)
                {
                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) && ((VistaArchivoEntradaConjuntoNumeros)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaArchivoConjuntoNumeros(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                {

                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaURLEntradaConjuntoNumeros) && ((VistaURLEntradaConjuntoNumeros)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaURLConjuntoNumeros(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.Excel)
                {
                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros) && ((VistaArchivoExcelEntradaConjuntoNumeros)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaArchivo_Excel_ConjuntoNumeros(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
            }

            Ventana.CalculoActual.QuitarConfiguracionEntrada_Ejecucion(entr);
        }

        private void nombreEntrada_LostFocus(object sender, RoutedEventArgs e)
        {
            Ventana.ActualizarNombresDescripcionesEntrada(entr);
        }

        private void btnSeObtieneOtroOrigenConjuntoNums_Click(object sender, RoutedEventArgs e)
        {
            Ventana.CerrarPestañasEntradaModificada(entr);

            entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.Ninguno;
            columnaSeObtieneOtroOrigenConjuntoNums.Visibility = Visibility.Collapsed;

            var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) && ((VistaArchivoEntradaConjuntoNumeros)T.Content).Entrada == entr) select T).FirstOrDefault();
            if (tab != null)
            {
                Ventana.QuitarTab(ref tab);
            }

            tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros) && ((VistaArchivoExcelEntradaConjuntoNumeros)T.Content).Entrada == entr) select T).FirstOrDefault();
            if (tab != null)
            {
                Ventana.QuitarTab(ref tab);
            }

            entr.ListaArchivos.Clear();

            MostrarOcultarSeObtieneConjuntoNumeros(false);

            btnSeObtieneConjunto_Click(this, e);
        }

        private string MostrarTextoArchivoCarpetaSeleccionada()
        {
            string TextoSeObtiene = string.Empty;
            string cantidadArchivos = string.Empty;
            string TipoNumeros = string.Empty;

            if (entr.Tipo == TipoEntrada.Numero)
            {
                //cantidadArchivos = entr.RutaArchivoEntrada;
                TipoNumeros = "la variable de número";
            }
            else if (entr.Tipo == TipoEntrada.ConjuntoNumeros)
            {
                //cantidadArchivos = entr.RutaArchivoConjuntoNumerosEntrada;
                TipoNumeros = "el vector de números";
            }
            else if (entr.Tipo == TipoEntrada.TextosInformacion)
            {
                //cantidadArchivos = entr.RutaArchivoConjuntoTextosEntrada;
                TipoNumeros = "el vector de cadenas de texto";
            }

            cantidadArchivos = entr.ListaArchivos.Count.ToString();

            TextoSeObtiene = "Se obtendrá " + TipoNumeros + " de la entrada, de " + cantidadArchivos + " archivos.";

            return TextoSeObtiene;
        }

        private void btnTextosInformacionFijos_Click(object sender, RoutedEventArgs e)
        {
            if (entr.TipoOpcionTextosInformacion != TipoOpcionTextosInformacionEntrada.TextosInformacionFijos)
            {
                entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
                MostrarOcultarSeDigita(false);
                MostrarOcultarSeObtiene(false);
                MostrarOcultarNumeroFijo(false);
                
                entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.Ninguno;
                MostrarOcultarSeDigitaConjuntoNumeros(false);
                MostrarOcultarConjuntoNumerosFijo(false);
                MostrarOcultarSeObtieneConjuntoNumeros(false);

                Ventana.CerrarPestañasEntradaModificada(entr);
                Ventana.AgregarTabTextosInformacionEntrada(ref entr, this);

                entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.TextosInformacionFijos;
                MostrarOcultarSeDigitaTextosInformacion(false);
                MostrarOcultarTextosInformacionFijos(true);
                MostrarOcultarSeObtieneTextosInformacion(false);

                if (VentanaEntrada != null)
                    VentanaEntrada.Close();
            }
            else
            {
                var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaTextosInformacionEntrada) && ((VistaTextosInformacionEntrada)T.Content).Entrada == entr) select T).FirstOrDefault();
                if (tab != null)
                {
                    Ventana.contenido.SelectedItem = tab;
                }
                else
                {
                    Ventana.AgregarTabTextosInformacionEntrada(ref entr, this);
                }

                if (VentanaEntrada != null)
                    VentanaEntrada.Close();
            }
        }

        private void btnSeDigitaTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (entr.TipoOpcionTextosInformacion != TipoOpcionTextosInformacionEntrada.SeDigita)
            {
                Ventana.CerrarPestañasEntradaModificada(entr);

                entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
                MostrarOcultarSeDigita(false);
                MostrarOcultarSeObtiene(false);
                MostrarOcultarNumeroFijo(false);

                entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.Ninguno;
                MostrarOcultarConjuntoNumerosFijo(false);
                MostrarOcultarSeObtieneConjuntoNumeros(false);
                MostrarOcultarSeDigitaConjuntoNumeros(false);
            }

            entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.SeDigita;
            MostrarOcultarTextosInformacionFijos(false);
            MostrarOcultarSeObtieneTextosInformacion(false);
            MostrarOcultarSeDigitaTextosInformacion(true);
        }

        private void btnSeObtieneTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (entr.TipoOpcionTextosInformacion != TipoOpcionTextosInformacionEntrada.SeObtiene)
            {
                entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
                MostrarOcultarSeDigita(false);
                MostrarOcultarSeObtiene(false);
                MostrarOcultarNumeroFijo(false);

                entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.Ninguno;

                MostrarOcultarSeDigitaConjuntoNumeros(false);
                MostrarOcultarSeObtieneConjuntoNumeros(false);
                MostrarOcultarConjuntoNumerosFijo(false);

                Ventana.CerrarPestañasEntradaModificada(entr);
                SeleccionarOrigenDatos seleccionar = new SeleccionarOrigenDatos();
                seleccionar.ShowDialog();

                entr.TipoFormatoArchivo_Entrada = seleccionar.TipoFormatoArchivo;

                if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.Archivo)
                {
                    Ventana.AgregarTabEntradaArchivoTextosInformacion(ref entr, this);

                    entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                    entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.SeObtiene;
                    MostrarOcultarSeDigitaTextosInformacion(false);
                    MostrarOcultarTextosInformacionFijos(false);
                    MostrarOcultarSeObtieneTextosInformacion(true);
                    //}

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                {
                    Ventana.AgregarTabEntradaURLTextosInformacion(ref entr, this);

                    entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                    entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.SeObtiene;
                    MostrarOcultarSeDigitaTextosInformacion(false);
                    MostrarOcultarTextosInformacionFijos(false);
                    MostrarOcultarSeObtieneTextosInformacion(true);

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (seleccionar.TipoOrigenDatos == TipoOrigenDatos.Excel)
                {

                    Ventana.AgregarTabEntradaArchivo_Excel_TextosInformacion(ref entr, this);

                    entr.TipoOrigenDatos = seleccionar.TipoOrigenDatos;
                    entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.SeObtiene;
                    MostrarOcultarSeDigitaTextosInformacion(false);
                    MostrarOcultarTextosInformacionFijos(false);
                    MostrarOcultarSeObtieneTextosInformacion(true);

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
            }
            else
            {
                if (entr.TipoOrigenDatos == TipoOrigenDatos.Archivo)
                {
                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoEntradaTextosInformacion) && ((VistaArchivoEntradaTextosInformacion)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaArchivoTextosInformacion(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                {

                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaURLEntradaTextosInformacion) && ((VistaURLEntradaTextosInformacion)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaURLTextosInformacion(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.Excel)
                {
                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion) && ((VistaArchivoExcelEntradaTextosInformacion)T.Content).Entrada == entr) select T).FirstOrDefault();
                    if (tab != null)
                    {
                        Ventana.contenido.SelectedItem = tab;
                    }
                    else
                    {
                        Ventana.AgregarTabEntradaArchivo_Excel_TextosInformacion(ref entr, this);
                    }

                    if (VentanaEntrada != null)
                        VentanaEntrada.Close();
                }
            }
        }

        private void btnSeObtieneOtroOrigenTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            Ventana.CerrarPestañasEntradaModificada(entr);

            entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.Ninguno;
            columnaSeObtieneOtroOrigenTextosInformacion.Visibility = Visibility.Collapsed;

            var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoEntradaTextosInformacion) && ((VistaArchivoEntradaTextosInformacion)T.Content).Entrada == entr) select T).FirstOrDefault();
            if (tab != null)
            {
                Ventana.QuitarTab(ref tab);
            }

            tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion) && ((VistaArchivoExcelEntradaTextosInformacion)T.Content).Entrada == entr) select T).FirstOrDefault();
            if (tab != null)
            {
                Ventana.QuitarTab(ref tab);
            }

            MostrarOcultarSeObtieneTextosInformacion(false);

            btnSeObtieneTextosInformacion_Click(this, e);
        }

        private void opcionesNombresCantidades_Click(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                if (entr.DefinicionOpcionesNombresCantidades == null)
                    entr.DefinicionOpcionesNombresCantidades = new Entidades.TextosInformacion.DefinicionTextoNombresCantidades();

                ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
                establecer.Operandos = new List<DiseñoOperacion>() { new DiseñoOperacion() { ID = App.GenerarID_Elemento(), EntradaRelacionada = entr, Tipo = TipoElementoOperacion.Entrada } };
                establecer.ListaElementos = Calculo.ElementosOperaciones.Except(establecer.Operandos).ToList();
                establecer.TextosNombre = entr.DefinicionOpcionesNombresCantidades.ReplicarObjeto();
                
                if(establecer.ShowDialog() == true)
                {
                    entr.DefinicionOpcionesNombresCantidades = establecer.TextosNombre;
                }
            }
        }

        private void opcionCantidadesComoTextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.AgregarCantidad_ComoTextoInformacion = (bool)opcionCantidadesComoTextosInformacion.IsChecked;
            }
        }

        private void opcionCantidadesComoTextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.AgregarCantidad_ComoTextoInformacion = (bool)opcionCantidadesComoTextosInformacion.IsChecked;
            }
        }

        private void opcionConfirmarComprobarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.ComprobarConfirmarCantidades_Ejecucion = (bool)opcionConfirmarComprobarCantidades.IsChecked;
            }
        }

        private void opcionConfirmarComprobarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.ComprobarConfirmarCantidades_Ejecucion = (bool)opcionConfirmarComprobarCantidades.IsChecked;
            }
        }

        private void opcionesTextosInformacionPredefinidosDigitacion_Click(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                EstablecerConjuntosTextosInformacionPredefinidos_Digitacion establecer = new EstablecerConjuntosTextosInformacionPredefinidos_Digitacion();
                establecer.Calculo = CalculoEntradas;
                establecer.Entrada = entr;
                establecer.ShowDialog();
            }
        }

        private void opcionListaNumerosPredefinidosDigitacion_Click(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                EstablecerListaNumerosPredefinidos_Digitacion establecer = new EstablecerListaNumerosPredefinidos_Digitacion();
                establecer.Entrada = entr;

                if (Calculo.EsEntradasArchivo)
                    establecer.ListaEntradasAnteriores = Calculo.ListaEntradas.Where(i => Calculo.ListaEntradas.IndexOf(i) < Calculo.ListaEntradas.IndexOf(Entrada) && i.Tipo == TipoEntrada.TextosInformacion).ToList();

                if (establecer.ListaEntradasAnteriores != null && establecer.ListaEntradasAnteriores.Any())
                    establecer.ConTextosInformacion_EntradasAnteriores = true;

                establecer.ShowDialog();
            }
        }

        private void opcionCantidadFijaNumerosDefinidosDigitacion_Click(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                EstablecerCantidadFijaNumerosDefinidos_Digitacion establecer = new EstablecerCantidadFijaNumerosDefinidos_Digitacion();
                establecer.Entrada = entr;
                establecer.ShowDialog();
            }
        }

        private void opcionCantidadFijaTextosDefinidosDigitacion_Click(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                EstablecerCantidadFijaNumerosDefinidos_Digitacion establecer = new EstablecerCantidadFijaNumerosDefinidos_Digitacion();
                establecer.EsTextos = true;
                establecer.Entrada = entr;
                establecer.ShowDialog();
            }
        }

        private void opcionesSeleccionCantidades_Click(object sender, RoutedEventArgs e)
        {
            if(entr != null)
            {
                ConfigSeleccionCantidades_Entrada config = new ConfigSeleccionCantidades_Entrada();
                config.Definicion = entr.SeleccionNumeros.CopiarObjeto();
                config.opcionesEjecucion.Visibility = Visibility.Visible;
                if(config.ShowDialog() == true)
                {
                    entr.SeleccionNumeros = config.Definicion;
                }
            }
        }

        private void subir_Click(object sender, RoutedEventArgs e)
        {
            if (Calculo.EsEntradasArchivo &&
                    Calculo.ListaEntradas.Contains(entr))
            {
                int indiceInicial = 0;
                int indiceFinal = Calculo.ListaEntradas.Count - 1;

                int indiceElemento = Calculo.ListaEntradas.IndexOf(entr);

                if((indiceElemento >= indiceInicial &&
                    indiceElemento <= indiceFinal) &&
                    (indiceElemento - 1 >= indiceInicial &&
                    indiceElemento - 1 <= indiceFinal))
                {
                    Calculo.ListaEntradas.RemoveAt(indiceElemento);
                    Calculo.ListaEntradas.Insert(indiceElemento - 1, entr);
                    VistaEntradas.ListarEntradas();

                    Calculo.ReordenarElementos_EntradasGenerales();
                }
            }
        }

        private void bajar_Click(object sender, RoutedEventArgs e)
        {
            int indiceInicial = 0;
            int indiceFinal = Calculo.ListaEntradas.Count - 1;

            int indiceElemento = Calculo.ListaEntradas.IndexOf(entr);

            if ((indiceElemento >= indiceInicial &&
                indiceElemento <= indiceFinal) &&
                (indiceElemento + 1 >= indiceInicial &&
                indiceElemento + 1 <= indiceFinal))
            {
                Calculo.ListaEntradas.RemoveAt(indiceElemento);
                Calculo.ListaEntradas.Insert(indiceElemento + 1, entr);
                VistaEntradas.ListarEntradas();

                Calculo.ReordenarElementos_EntradasGenerales();
            }
        }

        private void opcionEjecutarEntradaDesdeCalculos_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                entr.EjecutarDeFormaGeneral = (bool)!opcionEjecutarEntradaDesdeCalculos.IsChecked;
            }
        }

        private void opcionEjecutarEntradaDesdeCalculos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                entr.EjecutarDeFormaGeneral = (bool)!opcionEjecutarEntradaDesdeCalculos.IsChecked;
            }
        }
    }
}
