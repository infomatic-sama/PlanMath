using PlanMath_para_Word;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using static PlanMath_para_Word.Entradas;
using System.Windows;
using System.Windows.Controls;

namespace ProcessCalc.Entidades.Entradas
{
    public class ConfiguracionArchivoEntrada
    {
        public TipoArchivo TipoArchivo { get; set; }
        public string RutaArchivoEntrada { get; set; }
        public string RutaArchivoConjuntoNumerosEntrada { get; set; }
        public string RutaArchivoConjuntoTextosEntrada { get; set; }
        public string NombreArchivoEntrada { get; set; }
        public OpcionCarpetaEntrada ConfiguracionSeleccionCarpeta { get; set; }
        public OpcionSeleccionarArchivoEntrada ConfiguracionSeleccionArchivo { get; set; }
        public bool EstablecerLecturasNavegaciones_Busquedas {  get; set; }
        public List<LecturaNavegacion> LecturasNavegaciones { get; set; }
        public List<TextoBusqueda_DocumentoWord> ParametrosWord {  get; set; }
        public List<string> TextosInformacionFijos { get; set; }
        public string URLOffice_Original { get; set; }
        public bool UsarURLOffice_original { get; set; }
        public double TiempoEspera { get; set; }
        public TipoTiempoEspera TipoTiempoEspera { set; get; }
        public bool EsperarArchivos { get; set; }
        public bool IncluirTextosInformacion_DeNombresRutasArchivos { get; set; }
        public string RutaArchivoPlantilla { get; set; }
        public string NombreArchivoPlantilla { get; set; }
        public bool EntradaManual { get; set; }
        public ConfiguracionArchivoEntrada() 
        {
            TipoArchivo = TipoArchivo.EquipoLocal;
            ConfiguracionSeleccionCarpeta = OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion;
            ConfiguracionSeleccionArchivo = OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado;
            LecturasNavegaciones = new List<LecturaNavegacion>();
            ParametrosWord = new List<TextoBusqueda_DocumentoWord>();
            TextosInformacionFijos = new List<string>();
            URLOffice_Original = string.Empty;
            TipoTiempoEspera = TipoTiempoEspera.Minutos;
            TiempoEspera = 1;
        }

        public string ObtenerArchivoTextoPlano_Temporal(
            Entrada EntradaRelacionada, string NombreArchivoCalculo, string rutaArchivoCalculo,
             string NombreCalculo, string rutaSeleccionada = "")
        {
            string contenido = string.Empty;

            string rutaOriginal = string.Empty;

            if (!string.IsNullOrEmpty(rutaSeleccionada))
                rutaOriginal = rutaSeleccionada;
            else
            {
                switch (EntradaRelacionada.Tipo)
                {
                    case TipoEntrada.Numero:
                        rutaOriginal = ObtenerRutaArchivoEjecucion_Entrada(RutaArchivoEntrada, NombreArchivoCalculo, rutaArchivoCalculo, NombreCalculo, EntradaRelacionada);
                        break;

                    case TipoEntrada.ConjuntoNumeros:
                        rutaOriginal = ObtenerRutaArchivoEjecucion_Entrada(RutaArchivoConjuntoNumerosEntrada, NombreArchivoCalculo, rutaArchivoCalculo, NombreCalculo, EntradaRelacionada);
                        break;

                    case TipoEntrada.TextosInformacion:
                        rutaOriginal = ObtenerRutaArchivoEjecucion_Entrada(RutaArchivoConjuntoTextosEntrada, NombreArchivoCalculo, rutaArchivoCalculo, NombreCalculo, EntradaRelacionada);
                        break;
                }
            }

            switch (EntradaRelacionada.TipoFormatoArchivo_Entrada)
            {
                case TipoFormatoArchivoEntrada.ArchivoTextoPlano:
                    StreamReader leerTexto = new StreamReader(new FileStream(rutaOriginal, FileMode.Open));
                    contenido = leerTexto.ReadToEnd();
                    leerTexto.Close();

                    break;

                case TipoFormatoArchivoEntrada.PDF:
                    //try
                    //{
                    string rutaDestino = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();

                    StreamWriter guardarTexto = new StreamWriter(new FileStream(rutaDestino, FileMode.Create));
                    PdfDocument document = PdfDocument.Open(rutaOriginal);

                    foreach (UglyToad.PdfPig.Content.Page page in document.GetPages())
                    {
                        //guardarTexto.Write(page.Text);
                        ContentOrderTextExtractor.Options opciones = new ContentOrderTextExtractor.Options();
                        opciones.SeparateParagraphsWithDoubleNewline = true;
                        opciones.ReplaceWhitespaceWithSpace = true;

                        guardarTexto.Write(ContentOrderTextExtractor.GetText(page, opciones));
                    }

                    guardarTexto.Close();

                    leerTexto = new StreamReader(new FileStream(rutaDestino, FileMode.Open));
                    contenido = leerTexto.ReadToEnd();
                    leerTexto.Close();

                    File.Delete(rutaDestino);

                    break;

                case TipoFormatoArchivoEntrada.Word:
                    //try
                    //{
                    rutaDestino = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();

                    StreamWriter guardarTextoWord = new StreamWriter(new FileStream(rutaDestino, FileMode.Create));

                    if (ParametrosWord != null)
                    {
                        var TextosObtenidos = ObtenerTextosDocumento_Word(rutaOriginal);

                        foreach (var textoObtenido in TextosObtenidos)
                        {
                            guardarTextoWord.WriteLine(ProcesarSaltosLinea(textoObtenido));
                        }
                    }

                    guardarTextoWord.Close();

                    leerTexto = new StreamReader(new FileStream(rutaDestino, FileMode.Open));
                    contenido = leerTexto.ReadToEnd();
                    leerTexto.Close();

                    File.Delete(rutaDestino);

                    break;
            }

            return contenido;
        }

        private string ProcesarSaltosLinea(string cadena)
        {
            cadena = cadena.Replace("\r\n", "\r");
            cadena = cadena.Replace("\r", "\r\n");

            return cadena;
        }

        private List<string> ObtenerTextosDocumento_Word(string RutaDocumentoWord)
        {
            try
            {
                List<string> TextosObtenidos = PlanMath_Word.ObtenerTextosBusqueda_DocumentoWord(ParametrosWord, RutaDocumentoWord, string.Empty);
                PlanMath_para_Word.PlanMath_Word.CerrarAplicacionesWord(string.Empty);
                return TextosObtenidos;
            }
            catch (Exception e)
            {
                if (e.HResult == -2146824090)
                {
                    MessageBox.Show("El archivo Word: '" + RutaDocumentoWord + "' se encuentra abierto.");
                }
                else
                {
                    MessageBox.Show("Error al leer archivo Word: '" + e.Message + "'.");
                }
            }

            return new List<string>();
        }

        private string ObtenerRutaArchivoEjecucion_Entrada(string RutaArchivo, string NombreArchivoCalculo, string rutaArchivoCalculo,
             string NombreCalculo, Entrada EntradaRelacionada)
        {
            string ruta = string.Empty;

            switch (ConfiguracionSeleccionCarpeta)
            {
                case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                    switch (ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            ruta = RutaArchivo;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                            seleccionar.ModoEjecucion = false;
                            seleccionar.Entrada = EntradaRelacionada;
                            seleccionar.ArchivoEntrada = EntradaRelacionada.ListaArchivos.FirstOrDefault();
                            seleccionar.RutaCarpeta = RutaArchivo;
                            seleccionar.descripcionEntrada.Text = EntradaRelacionada.Nombre;
                            seleccionar.titulo.Text += "\nEjecución del cálculo " + NombreArchivoCalculo + " - " + NombreCalculo;

                            bool selecciona = (bool)seleccionar.ShowDialog();
                            if (selecciona == true)
                            {
                                ruta = ProcesarCarpetasContenedoras(RutaArchivo, seleccionar.TextoSeleccionado);

                            }
                            else if (selecciona == false)
                            {
                                return string.Empty;
                            }

                            break;

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                            if (EntradaRelacionada.ConfigSeleccionarArchivoURL)
                            {
                                seleccionar = new SeleccionarArchivoEntrada();
                                seleccionar.ModoEjecucion = false;
                                seleccionar.Entrada = EntradaRelacionada;
                                seleccionar.RutaCarpeta = RutaArchivo.Remove(RutaArchivo.LastIndexOf("\\"));
                                seleccionar.descripcionEntrada.Text = EntradaRelacionada.Nombre;
                                seleccionar.titulo.Text += "\nEjecución del cálculo " + NombreArchivoCalculo + " - " + NombreCalculo;

                                selecciona = (bool)seleccionar.ShowDialog();
                                if (selecciona == true)
                                {
                                    ruta = ProcesarCarpetasContenedoras(RutaArchivo.Remove(RutaArchivo.LastIndexOf("\\")), seleccionar.TextoSeleccionado);

                                }
                                else if (selecciona == false)
                                {
                                    return string.Empty;
                                }
                            }
                            else
                            {
                                ruta = RutaArchivo;
                            }

                            break;
                    }

                    break;

                case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                    switch (ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            if (EntradaRelacionada.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                                ruta = RutaArchivo;
                            else
                                ruta = rutaArchivoCalculo + "\\" + RutaArchivo;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:

                            SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                            seleccionar.ModoEjecucion = true;
                            seleccionar.Entrada = EntradaRelacionada;
                            seleccionar.ArchivoEntrada = EntradaRelacionada.ListaArchivos.FirstOrDefault();
                            seleccionar.RutaCarpeta = rutaArchivoCalculo;
                            seleccionar.descripcionEntrada.Text = EntradaRelacionada.Nombre;
                            seleccionar.titulo.Text += "\nEjecución del cálculo " + NombreArchivoCalculo + " - " + NombreCalculo;

                            bool selecciona = (bool)seleccionar.ShowDialog();
                            if (selecciona == true)
                            {
                                ruta = ProcesarCarpetasContenedoras(rutaArchivoCalculo, seleccionar.TextoSeleccionado);
                            }
                            else if (selecciona == false)
                            {
                                return string.Empty;
                            }

                            break;

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                            if (EntradaRelacionada.ConfigSeleccionarArchivoURL)
                            {
                                seleccionar = new SeleccionarArchivoEntrada();
                                seleccionar.ModoEjecucion = true;
                                seleccionar.Entrada = EntradaRelacionada;
                                seleccionar.RutaCarpeta = rutaArchivoCalculo;
                                seleccionar.descripcionEntrada.Text = EntradaRelacionada.Nombre;
                                seleccionar.titulo.Text += "\nEjecución del cálculo " + NombreArchivoCalculo+ " - " + NombreCalculo;

                                selecciona = (bool)seleccionar.ShowDialog();
                                if (selecciona == true)
                                {
                                    ruta = ProcesarCarpetasContenedoras(rutaArchivoCalculo, seleccionar.TextoSeleccionado);
                                }
                                else if (selecciona == false)
                                {
                                    return string.Empty;
                                }
                            }
                            else
                            {
                                ruta = rutaArchivoCalculo + "\\" + RutaArchivo;
                            }

                            break;
                    }

                    break;
            }

            return ruta;
            
        }
        private string ProcesarCarpetasContenedoras(string carpeta, string archivo)
        {
            if (archivo.Contains(".../"))
            {
                while (true)
                {
                    int indiceCadenaCarpeta = archivo.LastIndexOf(".../");
                    if (indiceCadenaCarpeta >= 0)
                    {
                        archivo = archivo.Remove(indiceCadenaCarpeta, ".../".Length);

                        int indiceSubCarpeta = carpeta.LastIndexOf("\\");

                        if (indiceSubCarpeta > -1)
                            carpeta = carpeta.Remove(indiceSubCarpeta);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return carpeta + "\\" + archivo;
        }

        public void LlenarComboRutas(ComboBox combo, string ruta, string rutaCalculo)
        {
            combo.Items.Clear();

            List<string> rutas = new List<string>();

            if(File.Exists(ruta))
                rutas = ObtenerRutasArchivos_Comodin(ruta);
            else
                rutas = ObtenerRutasArchivos_Comodin(rutaCalculo + "\\" + ruta);

            foreach (var item in rutas)
            {
                combo.Items.Add(item.ToString());
            }

            if(combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        private List<string> ObtenerRutasArchivos_Comodin(string ruta)
        {
            List<string> rutasArchivos = new List<string>();

            string nombreArchivo = ruta.Substring(ruta.LastIndexOf("\\") + 1);

            if (string.IsNullOrEmpty(nombreArchivo))
            {
                nombreArchivo = Guid.NewGuid().ToString() + ".txt";
                ruta = ruta + nombreArchivo;

                File.Create(ruta).Close();
            }

            nombreArchivo = nombreArchivo.Remove(nombreArchivo.LastIndexOf("."));

            if (nombreArchivo.Contains("*"))
            {
                string carpeta = ruta.Remove(ruta.LastIndexOf("\\"));
                string[] archivos = Directory.GetFiles(carpeta);

                string[] partesNombre = nombreArchivo.Split("*");
                string parteNombre = nombreArchivo.Replace("*", string.Empty);

                foreach (var itemRuta in archivos)
                {
                    string nombreArchivoItem = itemRuta.Substring(itemRuta.LastIndexOf("\\") + 1);
                    nombreArchivoItem = nombreArchivoItem.Remove(nombreArchivoItem.LastIndexOf("."));

                    if (nombreArchivo.StartsWith("*"))
                    {
                        if (nombreArchivoItem.EndsWith(parteNombre))
                        {
                            rutasArchivos.Add(itemRuta);
                        }
                    }
                    else if (nombreArchivo.EndsWith("*"))
                    {
                        if (nombreArchivoItem.StartsWith(parteNombre))
                        {
                            rutasArchivos.Add(itemRuta);
                        }
                    }
                    else if (nombreArchivo.Contains("*"))
                    {
                        if (nombreArchivoItem.StartsWith(partesNombre[0]) &
                            nombreArchivoItem.EndsWith(partesNombre[1]))
                        {
                            rutasArchivos.Add(itemRuta);
                        }
                    }
                }
            }
            else
            {
                rutasArchivos.Add(ruta);
            }

            return rutasArchivos;
        }

        public ConfiguracionArchivoEntrada CopiarObjeto()
        {
            ConfiguracionArchivoEntrada config = new ConfiguracionArchivoEntrada();

            config.EsperarArchivos = EsperarArchivos;
            config.RutaArchivoEntrada = RutaArchivoEntrada;
            config.TipoArchivo = TipoArchivo;
            config.URLOffice_Original = URLOffice_Original;
            config.RutaArchivoConjuntoNumerosEntrada = RutaArchivoConjuntoNumerosEntrada;
            config.RutaArchivoConjuntoTextosEntrada = RutaArchivoConjuntoTextosEntrada;
            config.NombreArchivoEntrada = NombreArchivoEntrada;
            config.ConfiguracionSeleccionCarpeta = ConfiguracionSeleccionCarpeta;
            config.ConfiguracionSeleccionArchivo = ConfiguracionSeleccionArchivo;
            config.EstablecerLecturasNavegaciones_Busquedas = EstablecerLecturasNavegaciones_Busquedas;
            config.LecturasNavegaciones = LecturasNavegaciones.ToList();
            config.ParametrosWord = ParametrosWord.ToList();
            config.TextosInformacionFijos = TextosInformacionFijos;
            config.UsarURLOffice_original = UsarURLOffice_original;
            config.TiempoEspera = TiempoEspera;
            config.TipoTiempoEspera = TipoTiempoEspera;
            config.IncluirTextosInformacion_DeNombresRutasArchivos = IncluirTextosInformacion_DeNombresRutasArchivos;
            config.RutaArchivoPlantilla = RutaArchivoPlantilla;

            return config;
        }
    }
}
