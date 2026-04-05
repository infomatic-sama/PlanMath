using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para SeleccionarArchivoEntrada.xaml
    /// </summary>
    public partial class SeleccionarArchivoEntrada : Window
    {
        public EntradaEspecifica VistaEntrada { get; set; }
        public Entrada Entrada { get; set; }
        public bool ModoEjecucion { get; set; }
        public string RutaCarpeta { get; set; }
        public string TextoSeleccionado { get; set; }
        public bool Pausado { get; set; }
        public ConfiguracionArchivoEntrada ArchivoEntrada { get; set; }
        public bool ModoArchivoExterno { get; set; }
        public string RutaArchivo { get; set; }
        public SeleccionarArchivoEntrada()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ModoArchivoExterno)
            {
                descripcionEntrada.Visibility = Visibility.Collapsed;
                titulo.Text = "Seleccionar archivo para ejecución\n";

                if (ArchivoEntrada != null)
                {
                    panelCarpeta.Visibility = Visibility.Collapsed;
                    panelArchivo.Visibility = Visibility.Collapsed;
                    btnSeleccionarCarpeta.Visibility = Visibility.Collapsed;
                    btnSeleccionarArchivo.Visibility = Visibility.Collapsed;
                    seleccionarArchivo.Visibility = Visibility.Collapsed;
                    archivoSeleccionado.Visibility = Visibility.Collapsed;
                    textoNombreArchivo.Visibility = Visibility.Collapsed;

                    switch (ArchivoEntrada.ConfiguracionSeleccionCarpeta)
                    {
                        case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                            switch (ArchivoEntrada.ConfiguracionSeleccionArchivo)
                            {
                                case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                    panelArchivo.Visibility = Visibility.Visible;
                                    btnSeleccionarArchivo.Visibility = Visibility.Visible;
                                    archivoSeleccionado.Visibility = Visibility.Visible;

                                    //if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                    //    carpetaSeleccionada.Text = (RutaCarpeta == null ? string.Empty : RutaCarpeta);
                                    //else
                                    if(!string.IsNullOrEmpty(RutaArchivo))
                                        archivoSeleccionado.Text = RutaArchivo;

                                    break;
                                case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                    if(!ModoEjecucion)
                                        plantilla.Visibility = Visibility.Visible;
                                    panelArchivo.Visibility = Visibility.Visible;
                                    btnSeleccionarCarpeta.Visibility = Visibility.Visible;
                                    panelCarpeta.Visibility = Visibility.Visible;
                                    seleccionarArchivo.Visibility = Visibility.Visible;
                                    textoNombreArchivo.Visibility = Visibility.Visible;

                                    //if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                    //    carpetaSeleccionada.Text = (RutaCarpeta == null ? string.Empty : RutaCarpeta);
                                    //else
                                    carpetaSeleccionada.Text = RutaCarpeta;
                                    ListarArchivos();

                                    if (!string.IsNullOrEmpty(ArchivoEntrada.RutaArchivoPlantilla))
                                    {
                                        archivoSeleccionado.Text = ArchivoEntrada.RutaArchivoPlantilla;
                                        seleccionarArchivo.Text = ArchivoEntrada.RutaArchivoPlantilla.Substring(ArchivoEntrada.RutaArchivoPlantilla.LastIndexOf("\\") + 1);
                                    }

                                    break;
                            }

                            break;

                        case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                            switch (ArchivoEntrada.ConfiguracionSeleccionArchivo)
                            {
                                case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                    panelArchivo.Visibility = Visibility.Visible;
                                    seleccionarArchivo.Visibility = Visibility.Visible;
                                    textoNombreArchivo.Visibility = Visibility.Visible;
                                    
                                    ListarArchivos();

                                    //if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                    //    carpetaSeleccionada.Text = (RutaCarpeta == null ? string.Empty : RutaCarpeta);
                                    //else
                                    if (!string.IsNullOrEmpty(RutaArchivo))
                                        seleccionarArchivo.Text = RutaArchivo.Substring(RutaArchivo.LastIndexOf("\\") + 1);


                                    break;

                                case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                    if (!ModoEjecucion)
                                        plantilla.Visibility = Visibility.Visible;
                                    panelArchivo.Visibility = Visibility.Visible;
                                    seleccionarArchivo.Visibility = Visibility.Visible;
                                    textoNombreArchivo.Visibility = Visibility.Visible;

                                    carpetaSeleccionada.Text = RutaCarpeta;
                                    ListarArchivos();

                                    if (!string.IsNullOrEmpty(ArchivoEntrada.RutaArchivoPlantilla))
                                    {
                                        archivoSeleccionado.Text = ArchivoEntrada.RutaArchivoPlantilla;
                                        seleccionarArchivo.Text = ArchivoEntrada.RutaArchivoPlantilla.Substring(ArchivoEntrada.RutaArchivoPlantilla.LastIndexOf("\\") + 1);
                                    }

                                    break;
                            }

                            break;
                    }
                }

                if(ModoEjecucion)
                {
                    cerrar.Visibility = Visibility.Collapsed;
                    pieEjecucion.Visibility = Visibility.Visible;
                }
                else
                {
                    cerrar.Visibility = Visibility.Visible;
                    pieEjecucion.Visibility = Visibility.Collapsed;
                }

                
            }
            else
            {
                if (ModoEjecucion)
                {
                    if (ArchivoEntrada != null)
                    {
                        panelCarpeta.Visibility = Visibility.Collapsed;
                        panelArchivo.Visibility = Visibility.Collapsed;
                        btnSeleccionarCarpeta.Visibility = Visibility.Collapsed;
                        btnSeleccionarArchivo.Visibility = Visibility.Collapsed;
                        seleccionarArchivo.Visibility = Visibility.Collapsed;
                        archivoSeleccionado.Visibility = Visibility.Collapsed;
                        textoNombreArchivo.Visibility = Visibility.Collapsed;

                        switch (ArchivoEntrada.ConfiguracionSeleccionCarpeta)
                        {
                            case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                                switch (ArchivoEntrada.ConfiguracionSeleccionArchivo)
                                {
                                    case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                    case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                        panelArchivo.Visibility = Visibility.Visible;
                                        seleccionarArchivo.Visibility = Visibility.Visible;
                                        textoNombreArchivo.Visibility = Visibility.Visible;

                                        panelCarpeta.Visibility = Visibility.Visible;

                                        //if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                        //    carpetaSeleccionada.Text = (RutaCarpeta == null ? string.Empty : RutaCarpeta);
                                        //else
                                        carpetaSeleccionada.Text = (RutaCarpeta == null ? string.Empty : RutaCarpeta);

                                        btnSeleccionarCarpeta.Visibility = Visibility.Visible;

                                        ListarArchivos();
                                        break;
                                }

                                break;

                            case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                                switch (ArchivoEntrada.ConfiguracionSeleccionArchivo)
                                {
                                    case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                    case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                                        panelArchivo.Visibility = Visibility.Visible;
                                        seleccionarArchivo.Visibility = Visibility.Visible;
                                        textoNombreArchivo.Visibility = Visibility.Visible;

                                        panelCarpeta.Visibility = Visibility.Visible;

                                        //if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                        //    carpetaSeleccionada.Text = (RutaCarpeta == null ? string.Empty : RutaCarpeta);
                                        //else
                                        carpetaSeleccionada.Text = (RutaCarpeta == null ? string.Empty : RutaCarpeta);

                                        ListarArchivos();

                                        break;
                                }

                                break;
                        }
                    }

                    cerrar.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (ArchivoEntrada != null)
                    {
                        panelCarpeta.Visibility = Visibility.Collapsed;
                        panelArchivo.Visibility = Visibility.Collapsed;
                        btnSeleccionarCarpeta.Visibility = Visibility.Collapsed;
                        btnSeleccionarArchivo.Visibility = Visibility.Collapsed;
                        seleccionarArchivo.Visibility = Visibility.Collapsed;
                        archivoSeleccionado.Visibility = Visibility.Collapsed;
                        textoNombreArchivo.Visibility = Visibility.Collapsed;

                        switch (ArchivoEntrada.ConfiguracionSeleccionCarpeta)
                        {
                            case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                                switch (ArchivoEntrada.ConfiguracionSeleccionArchivo)
                                {
                                    case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                        panelArchivo.Visibility = Visibility.Visible;
                                        btnSeleccionarArchivo.Visibility = Visibility.Visible;
                                        archivoSeleccionado.Visibility = Visibility.Visible;

                                        if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                            archivoSeleccionado.Text = (ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada);
                                        else if (Entrada.Tipo == TipoEntrada.Numero)
                                            archivoSeleccionado.Text = (ArchivoEntrada.RutaArchivoEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoEntrada);
                                        else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                                            archivoSeleccionado.Text = (ArchivoEntrada.RutaArchivoConjuntoTextosEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoConjuntoTextosEntrada);

                                        break;

                                    case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                    case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                        panelCarpeta.Visibility = Visibility.Visible;
                                        btnSeleccionarCarpeta.Visibility = Visibility.Visible;

                                        if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                            carpetaSeleccionada.Text = (ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada);
                                        else if (Entrada.Tipo == TipoEntrada.Numero)
                                            carpetaSeleccionada.Text = (ArchivoEntrada.RutaArchivoEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoEntrada);
                                        else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                                            carpetaSeleccionada.Text = (ArchivoEntrada.RutaArchivoConjuntoTextosEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoConjuntoTextosEntrada);

                                        break;
                                }

                                break;

                            case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                                switch (ArchivoEntrada.ConfiguracionSeleccionArchivo)
                                {
                                    case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                    case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                                        panelArchivo.Visibility = Visibility.Visible;
                                        seleccionarArchivo.Visibility = Visibility.Visible;
                                        textoNombreArchivo.Visibility = Visibility.Visible;

                                        ListarArchivos();

                                        if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                            seleccionarArchivo.Text = (ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada);
                                        else if (Entrada.Tipo == TipoEntrada.Numero)
                                            seleccionarArchivo.Text = (ArchivoEntrada.RutaArchivoEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoEntrada);
                                        else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                                            seleccionarArchivo.Text = (ArchivoEntrada.RutaArchivoConjuntoTextosEntrada == null ? string.Empty : ArchivoEntrada.RutaArchivoConjuntoTextosEntrada);

                                        break;
                                }

                                break;
                        }
                    }

                    cabeceraEjecucion.Visibility = Visibility.Collapsed;
                    pieEjecucion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void btnSeleccionarCarpeta_Click(object sender, RoutedEventArgs e)
        {
            if (ArchivoEntrada != null &
                (Entrada != null || ModoArchivoExterno))
            {
                if (ModoArchivoExterno)
                {
                    if (ArchivoEntrada.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada)
                    {
                        FolderBrowserDialog seleccionar = new FolderBrowserDialog();
                        seleccionar.ShowDialog();

                        if (ModoEjecucion)
                        {
                            TextoSeleccionado = archivoSeleccionado.Text;
                            carpetaSeleccionada.Text = seleccionar.SelectedPath;
                            RutaCarpeta = seleccionar.SelectedPath;

                            ListarArchivos();
                        }
                        else
                        {                            
                            ArchivoEntrada.RutaArchivoEntrada = seleccionar.SelectedPath;
                            carpetaSeleccionada.Text = ArchivoEntrada.RutaArchivoEntrada;
                            
                            ArchivoEntrada.NombreArchivoEntrada = seleccionar.SelectedPath;
                            RutaCarpeta = seleccionar.SelectedPath;

                            seleccionarArchivo.Text = string.Empty;
                            ListarArchivos();
                        }
                    }
                }
                else
                {
                    if (ArchivoEntrada.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada)
                    {
                        FolderBrowserDialog seleccionar = new FolderBrowserDialog();
                        seleccionar.ShowDialog();

                        if (ModoEjecucion)
                        {
                            TextoSeleccionado = seleccionar.SelectedPath;
                            carpetaSeleccionada.Text = TextoSeleccionado;
                            RutaCarpeta = carpetaSeleccionada.Text;

                            ListarArchivos();
                        }
                        else
                        {
                            if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                            {
                                ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada = seleccionar.SelectedPath;
                                carpetaSeleccionada.Text = ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada;
                            }
                            else if (Entrada.Tipo == TipoEntrada.Numero)
                            {
                                ArchivoEntrada.RutaArchivoEntrada = seleccionar.SelectedPath;
                                carpetaSeleccionada.Text = ArchivoEntrada.RutaArchivoEntrada;
                            }
                            else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                            {
                                ArchivoEntrada.RutaArchivoConjuntoTextosEntrada = seleccionar.SelectedPath;
                                carpetaSeleccionada.Text = ArchivoEntrada.RutaArchivoConjuntoTextosEntrada;
                            }

                            ArchivoEntrada.NombreArchivoEntrada = seleccionar.SelectedPath;
                            RutaCarpeta = carpetaSeleccionada.Text;

                            if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                VistaEntrada.MostrarOcultarSeObtieneConjuntoNumeros(true);
                            else if (Entrada.Tipo == TipoEntrada.Numero)
                                VistaEntrada.MostrarOcultarSeObtiene(true);
                            else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                                VistaEntrada.MostrarOcultarSeObtieneTextosInformacion(true);

                            ListarArchivos();
                        }
                    }
                }
            }
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            if (seleccionarArchivo.Visibility == Visibility.Visible)
            {
                if (ModoArchivoExterno)
                {
                    if (plantilla.Visibility == Visibility.Collapsed)
                    {
                        ArchivoEntrada.RutaArchivoEntrada = RutaCarpeta + "\\" + seleccionarArchivo.Text;
                        ArchivoEntrada.NombreArchivoEntrada = seleccionarArchivo.Text.Substring(seleccionarArchivo.Text.LastIndexOf("\\") + 1);
                    }
                    else if(plantilla.Visibility == Visibility.Visible)
                    {
                        ArchivoEntrada.RutaArchivoPlantilla = RutaCarpeta + "\\" + seleccionarArchivo.Text;
                        ArchivoEntrada.NombreArchivoPlantilla = seleccionarArchivo.Text;
                    }
                }
                else
                {
                    if (Entrada.Tipo == TipoEntrada.Numero)
                    {
                        ArchivoEntrada.RutaArchivoEntrada = seleccionarArchivo.Text;
                    }
                    else if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                    {
                        ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada = seleccionarArchivo.Text;
                    }
                    else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                    {
                        ArchivoEntrada.RutaArchivoConjuntoTextosEntrada = seleccionarArchivo.Text;
                    }

                    ArchivoEntrada.NombreArchivoEntrada = seleccionarArchivo.Text.Substring(seleccionarArchivo.Text.LastIndexOf("\\") + 1);

                    if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                        VistaEntrada.MostrarOcultarSeObtieneConjuntoNumeros(true);
                    else if (Entrada.Tipo == TipoEntrada.Numero)
                        VistaEntrada.MostrarOcultarSeObtiene(true);
                    else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                        VistaEntrada.MostrarOcultarSeObtieneTextosInformacion(true);
                }
            }
            else if (archivoSeleccionado.Visibility == Visibility.Visible)
            {
                if (ModoArchivoExterno)
                {
                    if (plantilla.Visibility == Visibility.Collapsed)
                    {
                        ArchivoEntrada.RutaArchivoEntrada = archivoSeleccionado.Text;
                        ArchivoEntrada.NombreArchivoEntrada = archivoSeleccionado.Text.Substring(archivoSeleccionado.Text.LastIndexOf("\\") + 1);
                    }
                    else if (plantilla.Visibility == Visibility.Visible)
                    {
                        ArchivoEntrada.RutaArchivoPlantilla = RutaCarpeta + "\\" + seleccionarArchivo.Text;
                        ArchivoEntrada.NombreArchivoPlantilla = seleccionarArchivo.Text;
                    }
                }
                else
                {
                    if (Entrada.Tipo == TipoEntrada.Numero)
                    {
                        ArchivoEntrada.RutaArchivoEntrada = archivoSeleccionado.Text;
                    }
                    else if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                    {
                        ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada = archivoSeleccionado.Text;
                    }
                    else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                    {
                        ArchivoEntrada.RutaArchivoConjuntoTextosEntrada = archivoSeleccionado.Text;
                    }

                    ArchivoEntrada.NombreArchivoEntrada = archivoSeleccionado.Text.Substring(archivoSeleccionado.Text.LastIndexOf("\\") + 1);

                    if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                        VistaEntrada.MostrarOcultarSeObtieneConjuntoNumeros(true);
                    else if (Entrada.Tipo == TipoEntrada.Numero)
                        VistaEntrada.MostrarOcultarSeObtiene(true);
                    else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                        VistaEntrada.MostrarOcultarSeObtieneTextosInformacion(true);
                }
            }

            DialogResult = true;
            Close();
        }

        private void btnSeleccionarArchivo_Click(object sender, RoutedEventArgs e)
        {
            if (Entrada != null || ModoArchivoExterno)
            {
                if (ModoArchivoExterno)
                {
                    Microsoft.Win32.OpenFileDialog abrir = new Microsoft.Win32.OpenFileDialog();

                    abrir.Filter = "Archivos de cálculo de PlanMath | *.pmcalc";
                    abrir.Title = "Abrir archivo de PlanMath";

                    if (abrir.ShowDialog() == true)
                    {
                        TextoSeleccionado = abrir.FileName;
                        archivoSeleccionado.Text = abrir.FileName;
                    }
                }
                else
                {
                    if (ModoEjecucion)
                    {
                        Microsoft.Win32.OpenFileDialog abrir = new Microsoft.Win32.OpenFileDialog();

                        if (Entrada.TipoOrigenDatos == TipoOrigenDatos.Excel)
                        {
                            abrir.Filter = "Archivos de Excel (planillas) | *.xlsx; *.xls";
                            abrir.Title = "Abrir archivo Excel";
                        }
                        else
                        {
                            switch (Entrada.TipoFormatoArchivo_Entrada)
                            {
                                case TipoFormatoArchivoEntrada.ArchivoTextoPlano:
                                    abrir.Filter = "Archivos de datos (texto plano) | *.txt; *.csv ; *.dat ; *.json ; *.xml";
                                    abrir.Title = "Abrir archivo";
                                    break;

                                case TipoFormatoArchivoEntrada.PDF:
                                    abrir.Filter = "Archivos PDF | *.pdf";
                                    abrir.Title = "Abrir archivo PDF";
                                    break;

                                case TipoFormatoArchivoEntrada.Word:
                                    abrir.Filter = "Archivos de Word | *.doc; *.docx";
                                    abrir.Title = "Abrir archivo Word";
                                    break;
                            }

                        }

                        if (abrir.ShowDialog() == true)
                        {
                            TextoSeleccionado = abrir.FileName;
                            archivoSeleccionado.Text = abrir.FileName;
                        }
                    }
                    else
                    {
                        Microsoft.Win32.OpenFileDialog abrir = new Microsoft.Win32.OpenFileDialog();

                        if (Entrada.TipoOrigenDatos == TipoOrigenDatos.Excel)
                        {
                            abrir.Filter = "Archivos de Excel (planillas) | *.xlsx; *.xls";
                            abrir.Title = "Abrir archivo Excel";
                        }
                        else
                        {
                            switch (Entrada.TipoFormatoArchivo_Entrada)
                            {
                                case TipoFormatoArchivoEntrada.ArchivoTextoPlano:
                                    abrir.Filter = "Archivos de datos (texto plano) | *.txt; *.csv ; *.dat ; *.json ; *.xml";
                                    abrir.Title = "Abrir archivo";
                                    break;

                                case TipoFormatoArchivoEntrada.PDF:
                                    abrir.Filter = "Archivos PDF | *.pdf";
                                    abrir.Title = "Abrir archivo PDF";
                                    break;

                                case TipoFormatoArchivoEntrada.Word:
                                    abrir.Filter = "Archivos de Word | *.doc; *.docx";
                                    abrir.Title = "Abrir archivo Word";
                                    break;
                            }
                        }

                        if (abrir.ShowDialog() == true)
                        {
                            if (Entrada.Tipo == TipoEntrada.Numero)
                            {
                                ArchivoEntrada.RutaArchivoEntrada = abrir.FileName;
                                archivoSeleccionado.Text = ArchivoEntrada.RutaArchivoEntrada;
                            }
                            else if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                            {
                                ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada = abrir.FileName;
                                archivoSeleccionado.Text = ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada;
                            }
                            else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                            {
                                ArchivoEntrada.RutaArchivoConjuntoTextosEntrada = abrir.FileName;
                                archivoSeleccionado.Text = ArchivoEntrada.RutaArchivoConjuntoTextosEntrada;
                            }

                            ArchivoEntrada.NombreArchivoEntrada = abrir.FileName.Substring(abrir.FileName.LastIndexOf("\\") + 1);

                            if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                                VistaEntrada.MostrarOcultarSeObtieneConjuntoNumeros(true);
                            else if (Entrada.Tipo == TipoEntrada.Numero)
                                VistaEntrada.MostrarOcultarSeObtiene(true);
                            else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                                VistaEntrada.MostrarOcultarSeObtieneTextosInformacion(true);
                        }
                    }
                }
            }
        }

        private void ListarArchivos()
        {
            if (!string.IsNullOrEmpty(RutaCarpeta) &&
                Directory.Exists(RutaCarpeta))
            {                
                string[] archivos = Directory.GetFiles(RutaCarpeta);
                seleccionarArchivo.Items.Clear();

                foreach (var rutaArchivo in archivos)
                {
                    string nombreArchivo = rutaArchivo.Substring(rutaArchivo.LastIndexOf("\\") + 1);

                    bool agregar = false;

                    string extensionArchivo = rutaArchivo.Substring(rutaArchivo.LastIndexOf(".") + 1).ToLower();

                    if (ModoArchivoExterno)
                    {
                        switch (extensionArchivo)
                        {
                            case "pmcalc":
                                agregar = true;
                                break;
                        }
                    }
                    else
                    {
                        switch (Entrada.TipoFormatoArchivo_Entrada)
                        {
                            case TipoFormatoArchivoEntrada.ArchivoTextoPlano:
                                switch (extensionArchivo)
                                {
                                    case "txt":
                                    case "csv":
                                    case "dat":
                                    case "json":
                                    case "xml":
                                    case "xlsx":
                                    case "xls":
                                        agregar = true;
                                        break;
                                }
                                break;

                            case TipoFormatoArchivoEntrada.PDF:
                                switch (extensionArchivo)
                                {
                                    case "pdf":
                                        agregar = true;
                                        break;
                                }
                                break;

                            case TipoFormatoArchivoEntrada.Word:
                                switch (extensionArchivo)
                                {
                                    case "doc":
                                    case "docx":
                                        agregar = true;
                                        break;
                                }
                                break;
                        }
                    }

                    if (agregar)
                        seleccionarArchivo.Items.Add(nombreArchivo);
                }                
            }
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            bool agregar = false;

            string extensionArchivo = seleccionarArchivo.Text.Substring(seleccionarArchivo.Text.LastIndexOf(".") + 1).ToLower();

            if (ModoArchivoExterno)
            {
                switch (extensionArchivo)
                {
                    case "pmcalc":
                        agregar = true;
                        break;
                }
            }
            else
            {
                switch (Entrada.TipoFormatoArchivo_Entrada)
                {
                    case TipoFormatoArchivoEntrada.ArchivoTextoPlano:
                        switch (extensionArchivo)
                        {
                            case "txt":
                            case "csv":
                            case "dat":
                            case "json":
                            case "xml":
                            case "xlsx":
                            case "xls":
                                agregar = true;
                                break;
                        }
                        break;

                    case TipoFormatoArchivoEntrada.PDF:
                        switch (extensionArchivo)
                        {
                            case "pdf":
                                agregar = true;
                                break;
                        }
                        break;

                    case TipoFormatoArchivoEntrada.Word:
                        switch (extensionArchivo)
                        {
                            case "doc":
                            case "docx":
                                agregar = true;
                                break;
                        }
                        break;
                }
            }

            if (!agregar)
            {
                System.Windows.MessageBox.Show("La extensión en el nombre de archvo no corresponde al tipo de archivo seleccionado.", "Extensión del archivo");
            }
            else
            {
                if (seleccionarArchivo.Text.Count(i => i == '*') > 2)
                {
                    agregar = false;
                    System.Windows.MessageBox.Show("Se permite solo un comodin en el nombre de archvo.", "Nombre del archivo");
                }
                else
                {
                    agregar = true;
                }

                if (agregar)
                {
                    TextoSeleccionado = seleccionarArchivo.Text;
                    DialogResult = true;
                    Close();
                }
            }
            
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnPausar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Pausado = true;
            Close();
        }
    }
}
