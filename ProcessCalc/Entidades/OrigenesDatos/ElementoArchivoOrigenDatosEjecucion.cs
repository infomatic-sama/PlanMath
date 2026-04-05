using Microsoft.VisualBasic.Logging;
using PlanMath_para_Excel;
using PlanMath_para_Word;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using static PlanMath_para_Excel.PlanMath_Excel;
using UglyToad.PdfPig.Logging;
using static PlanMath_para_Word.Entradas;

namespace ProcessCalc.Entidades.OrigenesDatos
{
    public class ElementoArchivoOrigenDatosEjecucion : ElementoOrigenDatosEjecucion
    {
        public OpcionBusquedaNumero TipoOpcionBusqueda { get; set; }
        public string RutaArchivo { get; set; }
        public List<BusquedaArchivoEjecucion> Busquedas { get; set; }
        public FileStream lector { get; set; }
        public StreamReader lectorArchivo { get; set; }
        public bool MismaLecturaArchivo { get; set; }
        public TipoArchivo TipoArchivo { get; set; }
        public CredencialesFTP CredencialesFTP { get; set; }
        public OpcionSeleccionarArchivoEntrada ConfiguracionSeleccionArchivo { get; set; }
        public OpcionCarpetaEntrada ConfiguracionSeleccionCarpeta { get; set; }
        public bool ConfigSeleccionarArchivo { get; set; }
        public TipoFormatoArchivoEntrada TipoFormatoArchivo_Entrada { get; set; }
        public List<TextoBusqueda_DocumentoWord> ParametrosWord { get; set; }
        public Entrada_Desde_Word EntradaWord { get; set; }
        public bool UsarURL_Office { get; set; }
        public long CaracteresCorridos { get; set; }
        public long CaracteresDescartados {  get; set; }
        public string URLOffice_Original { get; set; }
        public double TiempoEspera { get; set; }
        public TipoTiempoEspera TipoTiempoEspera { set; get; }
        public bool EsperarArchivos { get; set; }
        public List<string> TextosInformacionIncluidos { get; set; }
        public bool IncluirTextosInformacion_DeNombresRutasArchivos { get; set; }
        public bool EntradaManual { get; set; }      
        public ConfiguracionArchivoEntrada ElementoOrigenDatosAsociado_Diseño { get; set; }
        public ElementoArchivoOrigenDatosEjecucion(int CantidadNumeros_Obtenidos_UltimaEjecucion,
            int CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
            int PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
            int PosicionFinalNumeros_Obtenidos_UltimaEjecucion) : base(CantidadNumeros_Obtenidos_UltimaEjecucion, 
                CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
                PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
                PosicionFinalNumeros_Obtenidos_UltimaEjecucion)
        {
            Busquedas = new List<BusquedaArchivoEjecucion>();
            TextosInformacionIncluidos = new List<string>();
        }

        public static void GuardarArchivoTemporalEntrada_FTP(string rutaTemporalArchivoFTP, HttpResponseMessage respuestaArchivoFTP)
        {
            StreamWriter guardaArchivoTemporalFTP = new StreamWriter(rutaTemporalArchivoFTP);

            StreamReader leerDatos = new StreamReader(respuestaArchivoFTP.Content.ReadAsStream());
            string datos = leerDatos.ReadToEnd();

            guardaArchivoTemporalFTP.Write(datos);
            guardaArchivoTemporalFTP.Close();
        }

        public static void GuardarArchivoTemporalEntrada_Manual(string rutaTemporalArchivo, string texto)
        {
            StreamWriter guardaArchivoTemporal = new StreamWriter(rutaTemporalArchivo);

            guardaArchivoTemporal.Write(texto);
            guardaArchivoTemporal.Close();
        }

        public static string ConcatenarTextosBusquedas_Obtenidos(List<string> textos)
        {
            string textoConcatenado = string.Empty;

            foreach(var item in textos)
            {
                textoConcatenado += item + "\n";
            }

            return textoConcatenado;
        }

        public List<string> ObtenerRutaArchivoEjecucion_Entrada(string rutaArchivoCalculo, EjecucionCalculo ejecucion,
            ElementoEjecucionCalculo itemCalculo, ElementoEntradaEjecucion entrada)
        {
            TextosInformacionIncluidos.Clear();

            if (TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word &&
                UsarURL_Office == true)
            {
                return new List<string>() { URLOffice_Original };
            }
            else
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
                                if (ejecucion.ModoToolTips && ejecucion.TooltipsCalculo != null)
                                {
                                    ruta = entrada.ElementoDiseñoRelacionado.RutaArchivoSeleccionado_Tooltip;
                                }
                                else
                                {
                                    SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                                    seleccionar.ModoEjecucion = true;
                                    seleccionar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                                    seleccionar.ArchivoEntrada = ElementoOrigenDatosAsociado_Diseño;
                                    seleccionar.RutaCarpeta = RutaArchivo;
                                    seleccionar.descripcionEntrada.Text = entrada.Nombre;
                                    seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                    bool selecciona = (bool)seleccionar.ShowDialog();
                                    if (selecciona == true)
                                    {
                                        ruta = ProcesarCarpetasContenedoras(seleccionar.RutaCarpeta, seleccionar.TextoSeleccionado);

                                        if (seleccionar.Pausado)
                                        {
                                            ejecucion.Pausar();
                                        }
                                    }
                                    else if (selecciona == false)
                                    {
                                        ejecucion.Detener();
                                        return null;
                                    }
                                }
                                break;

                            case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                                if (ConfigSeleccionarArchivo)
                                {
                                    if (ejecucion.ModoToolTips && ejecucion.TooltipsCalculo != null)
                                    {
                                        ruta = entrada.ElementoDiseñoRelacionado.RutaArchivoSeleccionado_Tooltip;
                                    }
                                    else
                                    {
                                        var seleccionar = new SeleccionarArchivoEntrada();
                                        seleccionar.ModoEjecucion = true;
                                        seleccionar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                                        seleccionar.ArchivoEntrada = ElementoOrigenDatosAsociado_Diseño;
                                        seleccionar.RutaCarpeta = RutaArchivo;
                                        seleccionar.descripcionEntrada.Text = entrada.Nombre;
                                        seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                        var selecciona = (bool)seleccionar.ShowDialog();
                                        if (selecciona == true)
                                        {
                                            ruta = ProcesarCarpetasContenedoras(seleccionar.RutaCarpeta, seleccionar.TextoSeleccionado);

                                            if (seleccionar.Pausado)
                                            {
                                                ejecucion.Pausar();
                                            }
                                        }
                                        else if (selecciona == false)
                                        {
                                            ejecucion.Detener();
                                            return null;
                                        }
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
                                ruta = rutaArchivoCalculo + "\\" + RutaArchivo;
                                break;

                            case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                if (ejecucion.ModoToolTips && ejecucion.TooltipsCalculo != null)
                                {
                                    ruta = entrada.ElementoDiseñoRelacionado.RutaArchivoSeleccionado_Tooltip;
                                }
                                else
                                {
                                    SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                                    seleccionar.ModoEjecucion = true;
                                    seleccionar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                                    seleccionar.ArchivoEntrada = ElementoOrigenDatosAsociado_Diseño;
                                    seleccionar.RutaCarpeta = rutaArchivoCalculo;
                                    seleccionar.descripcionEntrada.Text = entrada.Nombre;
                                    seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                    bool selecciona = (bool)seleccionar.ShowDialog();
                                    if (selecciona == true)
                                    {
                                        ruta = ProcesarCarpetasContenedoras(rutaArchivoCalculo, seleccionar.TextoSeleccionado);

                                        if (seleccionar.Pausado)
                                        {
                                            ejecucion.Pausar();
                                        }
                                    }
                                    else if (selecciona == false)
                                    {
                                        ejecucion.Detener();
                                        return null;
                                    }
                                }
                                break;

                            case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                                if (ConfigSeleccionarArchivo)
                                {
                                    if (ejecucion.ModoToolTips && ejecucion.TooltipsCalculo != null)
                                    {
                                        ruta = entrada.ElementoDiseñoRelacionado.RutaArchivoSeleccionado_Tooltip;
                                    }
                                    else
                                    {
                                        var seleccionar = new SeleccionarArchivoEntrada();
                                        seleccionar.ModoEjecucion = true;
                                        seleccionar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                                        seleccionar.ArchivoEntrada = ElementoOrigenDatosAsociado_Diseño;
                                        seleccionar.RutaCarpeta = rutaArchivoCalculo;
                                        seleccionar.descripcionEntrada.Text = entrada.Nombre;
                                        seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                        var selecciona = (bool)seleccionar.ShowDialog();
                                        if (selecciona == true)
                                        {
                                            ruta = ProcesarCarpetasContenedoras(rutaArchivoCalculo, seleccionar.TextoSeleccionado);

                                            if (seleccionar.Pausado)
                                            {
                                                ejecucion.Pausar();
                                            }
                                        }
                                        else if (selecciona == false)
                                        {
                                            ejecucion.Detener();
                                            return null;
                                        }
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

                return ObtenerRutasArchivos_Comodin(ruta);
            }
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
                            if(IncluirTextosInformacion_DeNombresRutasArchivos)
                                TextosInformacionIncluidos.Add(nombreArchivoItem.Substring(0, nombreArchivoItem.IndexOf(parteNombre) + 1));
                        }
                    }
                    else if (nombreArchivo.EndsWith("*"))
                    {
                        if (nombreArchivoItem.StartsWith(parteNombre))
                        {
                            rutasArchivos.Add(itemRuta);
                            if (IncluirTextosInformacion_DeNombresRutasArchivos)
                                TextosInformacionIncluidos.Add(nombreArchivoItem.Substring(nombreArchivoItem.IndexOf(parteNombre) + parteNombre.Length));
                        }
                    }
                    else if (nombreArchivo.Contains("*"))
                    {
                        if (nombreArchivoItem.StartsWith(partesNombre[0]) &
                            nombreArchivoItem.EndsWith(partesNombre[1]))
                        {
                            rutasArchivos.Add(itemRuta);

                            if (IncluirTextosInformacion_DeNombresRutasArchivos)
                            {
                                TextosInformacionIncluidos.Add(partesNombre[0]);
                                TextosInformacionIncluidos.Add(partesNombre[1]);

                                //TextosInformacionIncluidos.Add(nombreArchivoItem.Substring(0, nombreArchivoItem.IndexOf(parteNombre) + 1));
                                //TextosInformacionIncluidos.Add(nombreArchivoItem.Substring(nombreArchivoItem.IndexOf(parteNombre) + parteNombre.Length));
                            }
                        }
                    }
                }
            }
            else
            {
                rutasArchivos.Add(ruta);
            }

            if(EsperarArchivos)
            {
                EsperandoArchivos(rutasArchivos);
            }

            return rutasArchivos;
        }

        private void EsperandoArchivos(List<string> archivos)
        {
            bool ExistenTodosArchivos = VerificarExistenciaArchivos(archivos);

            if(!ExistenTodosArchivos)
            {
                int segundos = 0;

                if (TipoTiempoEspera == TipoTiempoEspera.Segundos)
                {
                    segundos = (int)TiempoEspera;
                }
                else if (TipoTiempoEspera == TipoTiempoEspera.Minutos)
                {
                    segundos = (int)(TiempoEspera * 60);
                }
                else if (TipoTiempoEspera == TipoTiempoEspera.Horas)
                {
                    segundos = (int)(TiempoEspera * 3600);
                }
                else if (TipoTiempoEspera == TipoTiempoEspera.Dias)
                {
                    segundos = (int)(TiempoEspera * 86400);
                }

                int tiempo = 0;

                while (!ExistenTodosArchivos && tiempo < segundos)
                {
                    Thread.Sleep(1000);
                    tiempo++;
                    ExistenTodosArchivos = VerificarExistenciaArchivos(archivos);
                }
            }
        }

        private bool VerificarExistenciaArchivos(List<string> archivos)
        {
            bool Existen = true;
            foreach (var archivo in archivos)
            {
                if (!File.Exists(archivo))
                    Existen = false;
            }

            return Existen;
        }

        public string ObtenerURLFTP_Ejecucion_Entrada(EjecucionCalculo ejecucion, ElementoEjecucionCalculo itemCalculo, ElementoEntradaEjecucion entrada)
        {
            string url = string.Empty;

            switch (ConfiguracionSeleccionArchivo)
            {
                case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                    url = RutaArchivo;
                    break;

                case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                    EscribirURLEntrada escribir = new EscribirURLEntrada();
                    escribir.textoURL.Text = "URL FTP";
                    escribir.seleccionarURL.Text = RutaArchivo;
                    escribir.descripcionEntrada.Text = entrada.Nombre;
                    escribir.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                    bool selecciona = (bool)escribir.ShowDialog();
                    if (selecciona == true)
                    {
                        url = escribir.TextoSeleccionado;
                    }
                    else if (selecciona == false)
                    {
                        if (escribir.Pausado)
                        {
                            ejecucion.Pausar();
                        }
                        else
                        {
                            ejecucion.Detener();
                            return null;
                        }
                    }

                    break;

                case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                    if (ConfigSeleccionarArchivo)
                    {
                        escribir = new EscribirURLEntrada();
                        escribir.textoURL.Text = "URL FTP";
                        escribir.seleccionarURL.Text = RutaArchivo;
                        escribir.descripcionEntrada.Text = entrada.Nombre;
                        escribir.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                        selecciona = (bool)escribir.ShowDialog();
                        if (selecciona == true)
                        {
                            url = escribir.TextoSeleccionado;

                            if (escribir.Pausado)
                            {
                                ejecucion.Pausar();
                            }
                        }
                        else if (selecciona == false)
                        {
                            ejecucion.Detener();
                            return null;
                        }
                    }
                    else
                    {
                        url = RutaArchivo;
                    }

                    break;
            }

            if (url.Length >= 7 && !url.Substring(0, 6).ToLower().Equals("ftp://") &&
                !url.Substring(0, 7).ToLower().Equals("ftps://"))
                url = "ftp://" + url;
            else if (url.Length >= 7 && !url.Substring(0, 7).ToLower().Equals("ftps://") &&
                !url.Substring(0, 6).ToLower().Equals("ftp://"))
                url = "ftps://" + url;

            return url;
        }

        public string ProcesarCarpetasContenedoras(string carpeta, string archivo)
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

        public override BusquedaArchivoEjecucion ObtenerBusquedaRelacionadaCondicion(BusquedaTextoArchivo busqueda)
        {
            List<BusquedaArchivoEjecucion> listaBusquedas = new List<BusquedaArchivoEjecucion>();
            foreach (var itemBusqueda in Busquedas)
            {
                if (itemBusqueda.EsConjuntoBusquedas)
                {
                    foreach (var itemBusquedaConjunto in itemBusqueda.ConjuntoBusquedas)
                    {
                        listaBusquedas.Add(itemBusquedaConjunto);
                    }
                }
                listaBusquedas.Add(itemBusqueda);
            }

            BusquedaArchivoEjecucion busquedaEncontrada = (from BusquedaArchivoEjecucion B in listaBusquedas where B.BusquedaRelacionada_Diseño == busqueda select B).FirstOrDefault();
            return busquedaEncontrada;
        }

        public string ObtenerArchivoTextoPlano_Temporal(string rutaOriginal, bool archivoTemporal, Encoding codificacion,
            ref bool mostrarLog, List<string> log, ref bool ConError, EjecucionCalculo ejecucion, Entrada entradaRelacionada,
            ElementoEjecucionCalculo itemEjecucion)
        {
            string rutaDestino = string.Empty;

            switch (TipoFormatoArchivo_Entrada)
            {
                case TipoFormatoArchivoEntrada.ArchivoTextoPlano:
                    rutaDestino = rutaOriginal;
                    break;

                case TipoFormatoArchivoEntrada.PDF:
                    //try
                    //{
                    rutaDestino = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();

                    StreamWriter guardarTexto = new StreamWriter(new FileStream(rutaDestino, FileMode.Create), codificacion);

                    try
                    {
                        PdfDocument document = PdfDocument.Open(rutaOriginal);

                        foreach (Page page in document.GetPages())
                        {
                            //guardarTexto.Write(page.Text);
                            ContentOrderTextExtractor.Options opciones = new ContentOrderTextExtractor.Options();
                            opciones.SeparateParagraphsWithDoubleNewline = true;
                            opciones.ReplaceWhitespaceWithSpace = true;
                            
                            guardarTexto.Write(ProcesarSaltosLinea(ContentOrderTextExtractor.GetText(page, opciones)));
                        }
                    }
                    catch (Exception) { }

                    guardarTexto.Close();
                    //documentoPDF.Close();

                    if (archivoTemporal)
                        File.Delete(rutaOriginal);

                    //}
                    //catch(Exception)
                    //{

                    //}
                    break;

                case TipoFormatoArchivoEntrada.Word:
                    //try
                    //{
                    rutaDestino = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();

                    StreamWriter guardarTextoWord = new StreamWriter(new FileStream(rutaDestino, FileMode.Create), codificacion);

                    if (ParametrosWord != null)
                    {
                        var TextosObtenidos = ObtenerTextosDocumento_Word(rutaOriginal, ref mostrarLog, log, ref ConError, ejecucion);

                        foreach (var textoObtenido in TextosObtenidos)
                        {
                            guardarTextoWord.WriteLine(ProcesarSaltosLinea(textoObtenido));
                        }
                    }

                    guardarTextoWord.Close();

                    if (archivoTemporal)
                        File.Delete(rutaOriginal);

                    //}
                    //catch(Exception)
                    //{

                    //}
                    break;

                case TipoFormatoArchivoEntrada.TextoPantalla:
                    //try
                    //{
                    if (!ejecucion.ModoToolTips)
                    {
                        rutaDestino = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();
                        StreamWriter guardarTextoPantalla = new StreamWriter(new FileStream(rutaDestino, FileMode.Create), codificacion);

                        DigitarTextoPantalla digitarTexto = new DigitarTextoPantalla();
                        digitarTexto.descripcionEntrada.Text = entradaRelacionada.Nombre;

                        bool digita = (bool)digitarTexto.ShowDialog();
                        if (digita == true)
                        {
                            string textoObtenido = digitarTexto.Texto;
                            guardarTextoPantalla.WriteLine(textoObtenido);

                            guardarTextoPantalla.Close();

                            File.Delete(rutaOriginal);

                            if (digitarTexto.Pausado)
                            {
                                ejecucion.Pausar();
                            }
                        }
                        else if (digita == false)
                        {
                            guardarTextoPantalla.Close();
                            File.Delete(rutaOriginal);

                            ejecucion.Detener();
                        }
                    }
                    else
                    {
                        rutaDestino = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();
                        StreamWriter guardarTextoPantalla = new StreamWriter(new FileStream(rutaDestino, FileMode.Create), codificacion);

                        string textoObtenido = itemEjecucion.ElementoDiseñoRelacionado.TextoEnPantalla_Digitacion_Tooltip;
                        itemEjecucion.ElementoDiseñoRelacionado.DesdeTextoEnPantalla = true;
                        guardarTextoPantalla.WriteLine(textoObtenido);

                        guardarTextoPantalla.Close();

                        File.Delete(rutaOriginal);
                    }

                        //}
                        //catch(Exception)
                        //{

                        //}
                        break;
            }

            return rutaDestino;
        }

        public List<string> ObtenerTextosDocumento_Word(string RutaDocumentoWord, ref bool mostrarLog, List<string> log, ref bool ConError,
            EjecucionCalculo ejecucion)
        {
            try
            {
                List<string> TextosObtenidos = PlanMath_Word.ObtenerTextosBusqueda_DocumentoWord(ParametrosWord, RutaDocumentoWord, ejecucion.GUID_EjecucionCalculo);
                return TextosObtenidos;
            }
            catch (Exception e)
            {
                if (e.HResult == -2146824090)
                {
                    if (!ejecucion.ModoToolTips)
                    {
                        log.Add("El archivo Word: '" + RutaDocumentoWord + "' se encuentra abierto. Esperando a que se envíe el vector de cadenas de textos de la entrada desde Word...");
                        try { Thread.Sleep(3000); } catch (Exception) { }
                        ;
                        List<string> TextosObtenidos = ejecucion.ObtenerEntradaConjuntoTextos_EnvioDesdeWord(EntradaWord);
                        return TextosObtenidos;
                    }
                }
                else
                {
                    try { Thread.Sleep(3000); } catch (Exception) { };
                    ConError = true;

                    if (mostrarLog)
                        log.Add("Error al leer archivo Word: '" + e.Message + "'.");

                    ejecucion.Detener();
                    return null;
                }
            }

            return new List<string>();
        }

        private string ProcesarSaltosLinea(string cadena)
        {
            //for(int indice = 0; indice < cadena.Length; indice++)
            //{
            //    if (indice < cadena.Length && 
            //        cadena[indice] == '\r' &&
            //        cadena[indice + 1] != '\n')
            //    {
            //        cadena.Insert(indice + 1, "\n");
            //    }
            //    else if(indice == cadena.Length -1 &&
            //        cadena[indice] == '\r')
            //    {
            //        cadena.Insert(indice, "\n");
            //    }
            //}

            cadena = cadena.Replace("\r\n", "\r");
            cadena = cadena.Replace("\r", "\r\n");

            return cadena;
        }
    }
}
