
using PlanMath_para_Excel;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml;
using static PlanMath_para_Excel.Entradas;
using PlanMath_para_Word;
using static PlanMath_para_Word.Entradas;
using System.Text;
using ProcessCalc.Entidades.OrigenesDatos;
using ProcessCalc.Controles;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static PlanMath_para_Excel.PlanMath_Excel;
using System.Diagnostics;
using ProcessCalc.Entidades.Operaciones;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        private void ObtenerDatosEntradaEspecifica(ElementoEjecucionCalculo item, bool mostrarLog, ElementoCalculoEjecucion itemCalculo, bool modoRepeticion = false)
        {
            ElementoEntradaEjecucion entrada = (ElementoEntradaEjecucion)item;
            ElementoEntradaEjecucion elementoEntrada = null;

            if (ModoToolTips)
            {
                var elemento = this.TooltipsCalculo?.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID);
                if (elemento != null && elemento.EntradaRelacionada != null)
                    elementoEntrada = elemento.EntradaRelacionada.EntradaProcesada;
            }
            else
            {
                elementoEntrada = item.ElementoDiseñoRelacionado.EntradaRelacionada.EntradaProcesada;
            }

            if (elementoEntrada == null ||
                (itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo && item.ElementoDiseñoRelacionado.EntradaRelacionada.EjecutarDeFormaGeneral ||
                modoRepeticion))
            {
                item.Estado = EstadoEjecucion.Iniciado;
                
                var infoEjecucionExterna = item.ElementoDiseñoRelacionado.EntradaRelacionada.EjecucionesExternas_SubElementos_Config
                    .FirstOrDefault(i => i.IDElementoAsociado == item.ElementoDiseñoRelacionado.ID);

                if (!ModoEjecucionExterna ||
                    (ModoEjecucionExterna &&
                    infoEjecucionExterna != null &&
                    infoEjecucionExterna.EjecucionNormal &&
                    !infoEjecucionExterna.EjecucionTraspaso))
                {                    
                    if (entrada.TipoEntrada != TipoEntrada.TextosInformacion)
                    {
                        if (mostrarLog)
                        {
                            string info = string.Empty;

                            if (Calculo.MostrarInformacionElementos_InformeResultados &&
                                !string.IsNullOrEmpty(entrada.ElementoDiseñoRelacionado.Info))
                            {
                                info = " (" + entrada.ElementoDiseñoRelacionado.Info + ") ";
                            }

                            if (mostrarLog && !string.IsNullOrEmpty(entrada.Nombre))
                            {
                                log.Add("Obteniendo datos de la variable o vector de números de entrada '" + entrada.Nombre + "'..." + info);
                                InformeResultados.TextoLog.Add("Obteniendo datos de la variable o vector de números de entrada '" + entrada.Nombre + "'..." + info);
                            }
                            else
                            {
                                log.Add("Obteniendo datos de la variable o vector de números de entrada..." + info);
                                InformeResultados.TextoLog.Add("Obteniendo datos de la variable o vector de números de entrada..." + info);
                            }
                        }

                        //if (mostrarLog && !string.IsNullOrEmpty(entrada.Descripcion))
                        //    log.Add("Información y comentarios de la entrada: " + entrada.Descripcion + "...");

                        switch (entrada.TipoEntrada)
                        {
                            case TipoEntrada.ConjuntoNumeros:
                                switch (entrada.TipoEntradaConjuntoNumeros)
                                {
                                    case TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo:
                                        entrada.CantidadSubElementosProcesados++;

                                        if (mostrarLog)
                                        {
                                            log.Add("El vector de números de entrada '" + entrada.Nombre + "' son datos fijos definidos.");
                                            InformeResultados.TextoLog.Add("El vector de números de entrada '" + entrada.Nombre + "' son datos fijos definidos.");
                                        }

                                        int posicion = 0;
                                        foreach (var itemNumero in entrada.Numeros)
                                        {
                                            if (Pausada) Pausar();
                                            if (Detenida) return;

                                            posicion++;
                                            itemNumero.PosicionElemento_Lectura = posicion;
                                            EstablecerNombreCantidad(itemNumero, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades, posicion, entrada);
                                        }

                                        if (entrada.UtilizarCantidadNumeros &
                                                entrada.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.AgregarCantidadNumeros)
                                        {
                                            entrada.Numeros.Add(new EntidadNumero("Cantidad de números del vector de entrada '" + entrada.Nombre + "'", entrada.Numeros.Count));
                                            entrada.Numeros.Last().Textos.Add("cantidad");
                                        }

                                        //string strNumeros = ObtenerCadenaNumeros(numeros.Numeros);
                                        //InformeResultados.TextoLog.Add("Se obtuvo la entrada " + entrada.Nombre + " que es un conjunto de números fijos definidos: " + strNumeros + ".");
                                        break;
                                    case TipoOpcionConjuntoNumerosEntrada.SeDigita:

                                        if (!ModoToolTips)
                                        {
                                            DigitarConjuntoNumeros digitar = new DigitarConjuntoNumeros();
                                            digitar.CalculoActual = Calculo;
                                            digitar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                                            digitar.CantidadDecimalesCantidades = Calculo.CantidadDecimalesCantidades;
                                            digitar.descripcionEntrada.Text = entrada.Nombre;
                                            digitar.titulo.Text = "Digitar vector de números de entrada " + "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                            entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ObtenerTextosInformacion_EntradasAnteriores_Conjuntos(this);
                                            digitar.ConjuntoTextosInformacionDigitacion = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ConjuntoTextosInformacion_Digitacion;

                                            digitar.UtilizarPrimerConjunto_Automaticamente = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.UtilizarPrimerConjunto_Automaticamente;
                                            digitar.UtilizarSoloTextosPredefinidos = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.UtilizarSoloTextosPredefinidos;
                                            digitar.SeleccionarNumeroDeOpciones = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionarNumeroDeOpciones;

                                            entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ObtenerTextosInformacion_EntradasAnteriores_OpcionesListasNumeros(this);

                                            List<OpcionListaNumeros_Digitacion> opciones = new List<OpcionListaNumeros_Digitacion>();
                                            opciones.AddRange(entrada.ElementoDiseñoRelacionado.EntradaRelacionada.OpcionesListaNumeros_Ejecucion);
                                            opciones.AddRange(entrada.ElementoDiseñoRelacionado.EntradaRelacionada.OpcionesListaNumeros);

                                            digitar.OpcionesListaNumeros = opciones;

                                            if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DigitarEjecucionCantidadNumerosDigitacion)
                                            {
                                                digitar.FijarCantidadNumerosDigitacion = true;

                                                DigitarNumero digitarCantidad = new DigitarNumero();
                                                digitarCantidad.textos.Entrada = new Entrada("Entrada");
                                                digitarCantidad.descripcionEntrada.Text = "Cantidad de números del vector de entrada " + entrada.Nombre;
                                                digitarCantidad.titulo.Text += "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                                                digitarCantidad.textos.ConjuntosTextosInformacionDigitacion = new List<ConjuntoTextosInformacion_Digitacion>();
                                                digitarCantidad.OpcionesListaNumeros = new List<Entradas.OpcionListaNumeros_Digitacion>();

                                                bool digitaCantidad = (bool)digitarCantidad.ShowDialog();
                                                if (digitaCantidad == true)
                                                {
                                                    digitar.CantidadNumerosDigitacion = (int)digitarCantidad.Numero;

                                                    if (digitar.Pausado)
                                                    {
                                                        Pausar();
                                                    }
                                                }
                                                else if (digitaCantidad == false)
                                                {
                                                    Detener();
                                                    return;
                                                }

                                            }
                                            else if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.FijarCantidadNumerosDigitacion)
                                            {
                                                digitar.FijarCantidadNumerosDigitacion = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.FijarCantidadNumerosDigitacion;
                                                digitar.CantidadNumerosDigitacion = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.CantidadNumerosDigitacion;
                                            }

                                            digitar.CargarTextosDigitados(-1);

                                            bool digita = (bool)digitar.ShowDialog();
                                            if (digita == true)
                                            {
                                                entrada.Numeros.AddRange(digitar.Numeros);

                                                posicion = 0;
                                                foreach (var itemNumero in entrada.Numeros)
                                                {
                                                    itemNumero.Textos.AddRange(GenerarTextosInformacion(entrada.TextosInformacionFijos));
                                                    posicion++;
                                                    EstablecerNombreCantidad(itemNumero, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades, posicion, entrada);
                                                }

                                                if (digitar.Pausado)
                                                {
                                                    Pausar();
                                                }
                                            }
                                            else if (digita == false)
                                            {
                                                Detener();
                                                return;
                                            }

                                            entrada.CantidadSubElementosProcesados++;

                                            if (mostrarLog)
                                            {
                                                log.Add("Se digitó el vector de números de entrada '" + entrada.Nombre + "'.");
                                                InformeResultados.TextoLog.Add("Se digitó el vector de números de entrada '" + entrada.Nombre + "'.");
                                            }
                                        }
                                        else if (ModoToolTips && ModoEjecucionExterna && this.TooltipsCalculo == null)
                                        {
                                            entrada.Numeros.AddRange(item.ElementoDiseñoRelacionado.Cantidades_Digitacion_Tooltip.Select(i => i.CopiarObjeto()));
                                        }
                                        //strNumeros = ObtenerCadenaNumeros(numeros.Numeros);
                                        //InformeResultados.TextoLog.Add("Se obtuvo la entrada " + entrada.Nombre + " que es un conjunto de números digitados: " + strNumeros + ".");
                                        //else
                                        //{
                                        //    numeros.Numeros.Clear();
                                        //    numeros.Numeros.AddRange(numeros.ElementoDiseñoRelacionado.Cantidades_Digitacion_Tooltip);
                                        //}
                                        break;

                                    case TipoOpcionConjuntoNumerosEntrada.SeObtiene:

                                        if (entrada.OrigenesDatos.Any())
                                        {
                                            foreach (var itemOrigenDatos in entrada.OrigenesDatos)
                                            {
                                                if (Pausada) Pausar();
                                                if (Detenida) return;

                                                string rutaTemporalArchivoFTP = string.Empty;
                                                string RutaArchivo_ = string.Empty;

                                                switch (itemOrigenDatos.TipoOrigenDatos)
                                                {
                                                    case TipoOrigenDatos.Archivo:
                                                        ElementoArchivoOrigenDatosEjecucion archivo = (ElementoArchivoOrigenDatosEjecucion)itemOrigenDatos;

                                                        RutaArchivo_ = string.Empty;
                                                        List<string> RutasArchivos = new List<string>();

                                                        HttpResponseMessage respuestaArchivoFTP = null;

                                                        if (archivo.TipoArchivo == TipoArchivo.ServidorFTP)
                                                        {
                                                            RutaArchivo_ = archivo.ObtenerURLFTP_Ejecucion_Entrada(this, itemCalculo, entrada);

                                                            NetworkCredential credenciales = null;
                                                            HttpClient archivoFTP = null;

                                                            if (archivo.CredencialesFTP != null)
                                                            {
                                                                if (archivo.CredencialesFTP.UsuarioAnonimo)
                                                                {
                                                                    credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                                                                }
                                                                else
                                                                {
                                                                    if (string.IsNullOrEmpty(archivo.CredencialesFTP.NombreUsuario) &
                                                                        string.IsNullOrEmpty(archivo.CredencialesFTP.Clave))
                                                                    {
                                                                        credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (string.IsNullOrEmpty(archivo.CredencialesFTP.NombreUsuario))
                                                                        {
                                                                            credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                                                                        }
                                                                        else
                                                                        {
                                                                            credenciales = new NetworkCredential(archivo.CredencialesFTP.NombreUsuario,
                                                                                archivo.CredencialesFTP.Clave);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                                credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);

                                                            archivoFTP = new HttpClient(new HttpClientHandler() { Credentials = credenciales });
                                                            //archivoFTP.Credentials = credenciales;
                                                            //((FtpWebRequest)archivoFTP).Method = WebRequestMethods.Ftp.DownloadFile;

                                                            try
                                                            {
                                                                respuestaArchivoFTP = archivoFTP.GetAsync(RutaArchivo_).Result;
                                                            }
                                                            catch (Exception error)
                                                            {
                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                ;
                                                                ConError = true;

                                                                if (mostrarLog)
                                                                {
                                                                    log.Add("Error al obtener el vector de números de entrada '" + entrada.Nombre + "' del archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                                                    InformeResultados.TextoLog.Add("Error al obtener el vector de números de entrada '" + entrada.Nombre + "' del archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                                                }

                                                                Detener();
                                                                return;
                                                            }
                                                        }
                                                        else
                                                        if (archivo.TipoArchivo == TipoArchivo.Internet)
                                                        {
                                                            RutaArchivo_ = archivo.RutaArchivo;
                                                            if (RutaArchivo_.Length >= 8 && !RutaArchivo_.Substring(0, 7).ToLower().Equals("http://") &&
                                                                !RutaArchivo_.Substring(0, 8).ToLower().Equals("https://"))
                                                                RutaArchivo_ = "http://" + RutaArchivo_;
                                                            else if (RutaArchivo_.Length >= 8 && !RutaArchivo_.Substring(0, 8).ToLower().Equals("https://") &&
                                                                !RutaArchivo_.Substring(0, 7).ToLower().Equals("http://"))
                                                                RutaArchivo_ = "https://" + RutaArchivo_;

                                                            HttpClient archivoFTP = new HttpClient();
                                                            //((HttpWebRequest)archivoFTP).Method = WebRequestMethods.Ftp.DownloadFile;

                                                            try
                                                            {
                                                                respuestaArchivoFTP = archivoFTP.GetAsync(RutaArchivo_).Result;
                                                            }
                                                            catch (Exception error)
                                                            {
                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                ;
                                                                ConError = true;

                                                                if (mostrarLog)
                                                                {
                                                                    log.Add("Error al obtener el vector de números de entrada '" + entrada.Nombre + "' del archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                                                    InformeResultados.TextoLog.Add("Error al obtener el vector de números de entrada '" + entrada.Nombre + "' del archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                                                }

                                                                Detener();
                                                                return;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if(!string.IsNullOrEmpty(Calculo.RutaArchivo))
                                                                RutasArchivos = archivo.ObtenerRutaArchivoEjecucion_Entrada(Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")), this, itemCalculo, entrada);
                                                        }

                                                        if (RutasArchivos != null)
                                                        {
                                                            foreach (var RutaArchivo in RutasArchivos)
                                                            {
                                                                if (Pausada) Pausar();
                                                                if (Detenida) return;

                                                                bool valido = false;

                                                                if (archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                                                                {
                                                                    if (archivo.UsarURL_Office)
                                                                    {
                                                                        if (!string.IsNullOrEmpty(RutaArchivo) &&
                                                                            (PlanMath_Word.VerificarURL_Documento(RutaArchivo) ||
                                                                            File.Exists(RutaArchivo)))
                                                                        {
                                                                            valido = true;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (!valido)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(RutaArchivo) &&
                                                                            File.Exists(RutaArchivo))
                                                                            {
                                                                                valido = true;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                    valido = true;

                                                                if (valido)
                                                                {
                                                                    if (mostrarLog)
                                                                    {
                                                                        if (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.TextoPantalla)
                                                                        {
                                                                            if (archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word &&
                                                                                archivo.EntradaManual && !ModoToolTips)
                                                                            {
                                                                                log.Add("El archivo Word: '" + archivo.EntradaWord.RutaDocumento + "' se abrirá automáticamente. Esperando a que se envíe el vector de números de entrada desde Word...");
                                                                                InformeResultados.TextoLog.Add("El archivo Word: '" + archivo.EntradaWord.RutaDocumento + "' se abrirá automáticamente. Esperando a que se envíe el vector de números de la entrada desde Word...");

                                                                                PlanMath_Word.Abrir_Documento(archivo.EntradaWord.RutaDocumento);
                                                                            }
                                                                            else
                                                                            {
                                                                                if (!ModoToolTips)
                                                                                {
                                                                                    log.Add("Buscando en el archivo '" + RutaArchivo + "' el vector de números de entrada '" + entrada.Nombre + "'...");
                                                                                    InformeResultados.TextoLog.Add("Buscando en el archivo '" + RutaArchivo + "' el vector de números de entrada '" + entrada.Nombre + "'...");
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            log.Add("Esperando el archivo en pantalla del vector de números de entrada '" + entrada.Nombre + "'...");
                                                                            InformeResultados.TextoLog.Add("Esperando el archivo en pantalla del vector de números de entrada '" + entrada.Nombre + "'...");
                                                                        }
                                                                    }

                                                                    bool existe = false;

                                                                    if (archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word &&
                                                                        archivo.UsarURL_Office)
                                                                    {
                                                                        existe = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                                                                            archivo.TipoArchivo == TipoArchivo.RedLocal)
                                                                        {
                                                                            existe = File.Exists(RutaArchivo);
                                                                        }
                                                                        else if (archivo.TipoArchivo == TipoArchivo.ServidorFTP)
                                                                        {
                                                                            if (respuestaArchivoFTP != null && respuestaArchivoFTP.StatusCode == HttpStatusCode.OK)
                                                                            //if(respuestaArchivoFTP.ContentLength > 0)
                                                                            {
                                                                                //respuestaArchivoFTP = ((FtpWebRequest)archivoFTP).GetResponse();
                                                                                existe = true;
                                                                            }
                                                                            else
                                                                                existe = false;
                                                                        }
                                                                        else if (archivo.TipoArchivo == TipoArchivo.Internet)
                                                                        {
                                                                            if (respuestaArchivoFTP != null && respuestaArchivoFTP.StatusCode == HttpStatusCode.OK)
                                                                            {
                                                                                existe = true;
                                                                            }
                                                                            else
                                                                                existe = false;
                                                                        }
                                                                    }

                                                                    rutaTemporalArchivoFTP = string.Empty;
                                                                    string rutaArchivo_Busquedas = RutaArchivo;

                                                                    if (existe)
                                                                    {
                                                                        bool archivoTemporal = false;
                                                                        bool EntradaManual = false;

                                                                        if (archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word &&
                                                                                archivo.EntradaManual)
                                                                        {
                                                                            if (ModoToolTips)
                                                                            {
                                                                                EntradaManual = true;
                                                                                ObtenerDatosToolTipEntrada_ObtenerEjecucionNormal((ElementoOperacionAritmeticaEjecucion)item, true);
                                                                            }
                                                                            else
                                                                            {
                                                                                bool encontrado = false;
                                                                                List<string> textos = ObtenerEntradaTextoBusqueda_EnvioDesdeWord(archivo.EntradaWord, ref encontrado);

                                                                                string textoBusqueda = ElementoArchivoOrigenDatosEjecucion.ConcatenarTextosBusquedas_Obtenidos(textos);

                                                                                rutaArchivo_Busquedas = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();
                                                                                ElementoArchivoOrigenDatosEjecucion.GuardarArchivoTemporalEntrada_Manual(rutaArchivo_Busquedas, textoBusqueda);
                                                                                archivoTemporal = true;
                                                                            }
                                                                        }
                                                                        //else
                                                                        //{
                                                                        if (!ModoToolTips ||
                                                                        (ModoToolTips && !EntradaManual))
                                                                        {
                                                                            if (!itemOrigenDatos.EstablecerLecturasNavegaciones_Busquedas)
                                                                            {
                                                                                bool ConError_ = ConError;

                                                                                entrada.LeerBusquedasEntradaArchivo(archivo, entrada, entrada, archivo.MismaLecturaArchivo, mostrarLog,
                                                                                    ref ConError_, this, archivosAbiertos, ArchivosOrigenesDatos_Abiertos, ref rutaTemporalArchivoFTP, ref rutaArchivo_Busquedas, respuestaArchivoFTP,
                                                                                    log, InformeResultados.TextoLog, null, archivoTemporal);

                                                                                ConError = ConError_;
                                                                            }
                                                                            else
                                                                            {
                                                                                foreach (var itemLecturaNavegacion in itemOrigenDatos.LecturasNavegaciones)
                                                                                {
                                                                                    if (Pausada) Pausar();
                                                                                    if (Detenida) return;

                                                                                    bool ConError_ = ConError;

                                                                                    entrada.LeerBusquedasEntradaArchivo(archivo, entrada, entrada, itemLecturaNavegacion.MismaLecturaBusquedasArchivo, mostrarLog,
                                                                                    ref ConError_, this, archivosAbiertos, ArchivosOrigenesDatos_Abiertos, ref rutaTemporalArchivoFTP, ref rutaArchivo_Busquedas, respuestaArchivoFTP,
                                                                                    log, InformeResultados.TextoLog, itemLecturaNavegacion, archivoTemporal);

                                                                                    ConError = ConError_;

                                                                                }
                                                                            }

                                                                            //if (numeros.Numeros.Count == archivo.Busquedas.Count)
                                                                            //{
                                                                            //    if (mostrarLog)
                                                                            //        log.Add("Se encontraron todos los números del conjunto de números de la entrada '" + entrada.Nombre + "' en el archivo.");

                                                                            //    //strNumeros = ObtenerCadenaNumeros(numeros.Numeros);
                                                                            //    //InformeResultados.TextoLog.Add("Se obtuvo la entrada " + entrada.Nombre + " que es un conjunto de números obtenidos de un archivo: " + strNumeros + ".");
                                                                            //}
                                                                            foreach (var busqueda in archivo.Busquedas)
                                                                            {
                                                                                busqueda.ReiniciarBusqueda();
                                                                            }
                                                                            //}
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        try { Thread.Sleep(3000); } catch (Exception) { }
                                                                        ;
                                                                        ConError = true;

                                                                        if (mostrarLog)
                                                                        {
                                                                            log.Add("No se encontró el archivo '" + RutaArchivo + "'.");
                                                                            InformeResultados.TextoLog.Add("No se encontró el archivo '" + RutaArchivo + "'.");
                                                                        }

                                                                        Detener();
                                                                        return;
                                                                    }

                                                                }
                                                            }
                                                        }

                                                        break;

                                                    case TipoOrigenDatos.DesdeInternet:
                                                        ElementoInternetOrigenDatosEjecucion url = (ElementoInternetOrigenDatosEjecucion)itemOrigenDatos;

                                                        url.URL = url.ObtenerURLEjecucion_Entrada(this, itemCalculo, entrada);
                                                        url.ObjetoURL.URL = url.URL;

                                                        if (mostrarLog)
                                                        {
                                                            log.Add("Buscando en la URL '" + url.URL + "' la  entrada '" + entrada.Nombre + "'...");
                                                            InformeResultados.TextoLog.Add("Buscando en la URL '" + url.URL + "' la  entrada '" + entrada.Nombre + "'...");
                                                        }

                                                        if (!string.IsNullOrEmpty(url.URL))
                                                        {
                                                            if (url.EstablecerParametrosEjecucion)
                                                            {
                                                                EstablecerParametrosURL_Ejecucion establecerParametros = new EstablecerParametrosURL_Ejecucion();
                                                                establecerParametros.url.Text = url.URL;
                                                                establecerParametros.Entrada = url.ObjetoURL;
                                                                establecerParametros.ShowDialog();
                                                            }

                                                            if (!itemOrigenDatos.EstablecerLecturasNavegaciones_Busquedas)
                                                            {
                                                                rutaTemporalArchivoFTP = string.Empty;
                                                                RutaArchivo_ = string.Empty;
                                                                respuestaArchivoFTP = null;

                                                                bool ConError_ = ConError;

                                                                entrada.LeerBusquedasEntradaURL(url, entrada, entrada, url.MismaLecturaArchivo, mostrarLog, ref ConError_,
                                                                    this, archivosAbiertos, ArchivosOrigenesDatos_Abiertos, ref rutaTemporalArchivoFTP, ref RutaArchivo_, respuestaArchivoFTP, log, InformeResultados.TextoLog);

                                                                ConError = ConError_;
                                                            }
                                                            else
                                                            {
                                                                foreach (var itemLecturaNavegacion in itemOrigenDatos.LecturasNavegaciones)
                                                                {
                                                                    if (Pausada) Pausar();
                                                                    if (Detenida) return;

                                                                    rutaTemporalArchivoFTP = string.Empty;
                                                                    RutaArchivo_ = string.Empty;
                                                                    respuestaArchivoFTP = null;

                                                                    bool ConError_ = ConError;

                                                                    entrada.LeerBusquedasEntradaURL(url, entrada, entrada, itemLecturaNavegacion.MismaLecturaBusquedasArchivo, mostrarLog, ref ConError_,
                                                                        this, archivosAbiertos, ArchivosOrigenesDatos_Abiertos, ref rutaTemporalArchivoFTP, ref RutaArchivo_, respuestaArchivoFTP, log, InformeResultados.TextoLog,
                                                                        itemLecturaNavegacion);

                                                                    ConError = ConError_;
                                                                }
                                                            }

                                                            //if (url.MismaLecturaArchivo) url.lector.Close();
                                                            //if (numeros.Numeros.Count == url.Busquedas.Count)
                                                            //{
                                                            //    if (mostrarLog)
                                                            //        log.Add("Se encontraron todos los números del conjunto de números de la entrada '" + entrada.Nombre + "' en la URL.");

                                                            //    //strNumeros = ObtenerCadenaNumeros(numeros.Numeros);
                                                            //    //InformeResultados.TextoLog.Add("Se obtuvo la entrada " + entrada.Nombre + " que es un conjunto de números obtenidos de un archivo: " + strNumeros + ".");
                                                            //}
                                                        }
                                                        else
                                                        {
                                                            try { Thread.Sleep(3000); } catch (Exception) { }
                                                            ;
                                                            ConError = true;

                                                            if (mostrarLog)
                                                            {
                                                                log.Add("La URL '" + url.URL + "' no es válida.");
                                                                InformeResultados.TextoLog.Add("La URL '" + url.URL + "' no es válida.");
                                                            }

                                                            Detener();
                                                            return;
                                                        }

                                                        break;

                                                    case TipoOrigenDatos.Excel:
                                                        ElementoArchivoExcelOrigenDatosEjecucion excel = (ElementoArchivoExcelOrigenDatosEjecucion)itemOrigenDatos;

                                                        foreach (var itemParametrosExcel in excel.ParametrosExcel)
                                                        {
                                                            if (Pausada) Pausar();
                                                            if (Detenida) return;

                                                            bool valido = false;

                                                            if (excel.UsarURLLibro)
                                                            {
                                                                if (!string.IsNullOrEmpty(itemParametrosExcel.URLOffice_Original) &&
                                                                    (PlanMath_Excel.VerificarURL_Libro(itemParametrosExcel.URLOffice_Original) ||
                                                                    File.Exists(itemParametrosExcel.URLOffice_Original)))
                                                                {
                                                                    valido = true;
                                                                    itemParametrosExcel.Libro = itemParametrosExcel.URLOffice_Original;
                                                                }
                                                            }

                                                            if (!valido)
                                                            {
                                                                itemParametrosExcel.Libro = excel.ObtenerRutaArchivoEjecucion_Entrada(Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")), this, itemCalculo, entrada);

                                                                if (!string.IsNullOrEmpty(itemParametrosExcel.Libro) &&
                                                                File.Exists(itemParametrosExcel.Libro))
                                                                {
                                                                    valido = true;
                                                                }
                                                            }

                                                            if (valido)
                                                            {
                                                                List<PlanMath_Excel.NumeroObtenido> numerosObtenidos = new List<PlanMath_Excel.NumeroObtenido>();
                                                                bool encontrado = false;

                                                                EntidadNumero numeroAgregar = new EntidadNumero(string.Empty, 0);

                                                                if (itemParametrosExcel.EntradaManual && !ModoToolTips)
                                                                {
                                                                    log.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se abrirá automáticamente. Esperando a que se envíe el vector de numeros de la entrada desde Excel...");
                                                                    InformeResultados.TextoLog.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se abrirá automáticamente. Esperando a que se envíe el vector de numeros de la entrada desde Excel...");

                                                                    PlanMath_Excel.Abrir_Libro(itemParametrosExcel.Libro);

                                                                    if (itemParametrosExcel.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.ConjuntoNumeros)
                                                                    {
                                                                        encontrado = false;
                                                                        numerosObtenidos = ObtenerEntradaConjuntoNumeros_EnvioDesdeExcel(itemParametrosExcel, ref encontrado);
                                                                    }
                                                                    else if (itemParametrosExcel.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.Numero)
                                                                    {
                                                                        encontrado = false;
                                                                        numeroAgregar.Numero = ObtenerEntrada_EnvioDesdeExcel(itemParametrosExcel, ref encontrado);
                                                                        numeroAgregar.Textos.AddRange(GenerarTextosInformacion(entrada.TextosInformacionFijos));
                                                                    }
                                                                }
                                                                else if (itemParametrosExcel.EntradaManual &&
                                                                    ModoToolTips)
                                                                {
                                                                    ObtenerDatosToolTipEntrada_ObtenerEjecucionNormal((ElementoOperacionAritmeticaEjecucion)item, true);
                                                                    encontrado = true;
                                                                }
                                                                else
                                                                {
                                                                    if (mostrarLog && !ModoToolTips)
                                                                    {
                                                                        log.Add("Buscando en el archivo Excel '" + itemParametrosExcel.Libro + "' el vector de números de entrada " + entrada.Nombre + "...");
                                                                        InformeResultados.TextoLog.Add("Buscando en el archivo Excel '" + itemParametrosExcel.Libro + "' el vector de números de entrada " + entrada.Nombre + "...");
                                                                    }

                                                                    if (itemParametrosExcel.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.ConjuntoNumeros)
                                                                    {
                                                                        try
                                                                        {
                                                                            numerosObtenidos = PlanMath_Excel.ObtenerEntrada_ConjuntoNumeros_En_Excel(itemParametrosExcel, ref encontrado, this.GUID_EjecucionCalculo);

                                                                        }
                                                                        catch (Exception e)
                                                                        {
                                                                            if (e.HResult == -2146827284)
                                                                            {
                                                                                if (!ModoToolTips)
                                                                                {
                                                                                    //MessageBox.Show("El archivo Excel: '" + excel.ParametrosExcel.Libro + "' se encuentra abierto. Por favor ciérralo para continuar.");
                                                                                    log.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se encuentra abierto. Esperando a que se envíe el vector de numeros de la entrada desde Excel...");
                                                                                    InformeResultados.TextoLog.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se encuentra abierto. Esperando a que se envíe el vector de numeros de la entrada desde Excel...");

                                                                                    try { Thread.Sleep(3000); } catch (Exception) { }
                                                                                    ;
                                                                                    encontrado = false;
                                                                                    numerosObtenidos = ObtenerEntradaConjuntoNumeros_EnvioDesdeExcel(itemParametrosExcel, ref encontrado);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                                ;
                                                                                ConError = true;

                                                                                if (mostrarLog)
                                                                                {
                                                                                    log.Add("Error al leer archivo Excel: '" + e.Message + "'.");
                                                                                    InformeResultados.TextoLog.Add("Error al leer archivo Excel: '" + e.Message + "'.");
                                                                                }

                                                                                Detener();
                                                                                return;
                                                                            }
                                                                        }

                                                                        //if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades.DefinirNombresDespuesEjecucion_Elemento)
                                                                        //{
                                                                        //    foreach (var itemNumero in numeros.Numeros)
                                                                        //    {
                                                                        //        EstablecerNombreCantidad(itemNumero, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades, itemNumero.PosicionElemento_DefinicionNombres, null, entrada, null, false);
                                                                        //    }
                                                                        //}

                                                                    }
                                                                    else if (itemParametrosExcel.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.Numero)
                                                                    {
                                                                        try
                                                                        {
                                                                            numeroAgregar.Numero = PlanMath_Excel.ObtenerEntrada_En_Excel(itemParametrosExcel, ref encontrado, this.GUID_EjecucionCalculo);
                                                                        }
                                                                        catch (Exception e)
                                                                        {
                                                                            if (e.HResult == -2146827284)
                                                                            {
                                                                                //MessageBox.Show("El archivo Excel: '" + excel.ParametrosExcel.Libro + "' se encuentra abierto. Por favor ciérralo para continuar.");
                                                                                log.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se encuentra abierto. Esperando a que se envíe el vector de numeros de la entrada desde Excel...");
                                                                                InformeResultados.TextoLog.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se encuentra abierto. Esperando a que se envíe el vector de numeros de la entrada desde Excel...");

                                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                                ;
                                                                                encontrado = false;
                                                                                numeroAgregar.Numero = ObtenerEntrada_EnvioDesdeExcel(itemParametrosExcel, ref encontrado);
                                                                                numeroAgregar.Textos.AddRange(GenerarTextosInformacion(entrada.TextosInformacionFijos));
                                                                            }
                                                                            else
                                                                            {
                                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                                ;
                                                                                ConError = true;

                                                                                if (mostrarLog)
                                                                                {
                                                                                    log.Add("Error al leer archivo Excel: '" + e.Message + "'.");
                                                                                    InformeResultados.TextoLog.Add("Error al leer archivo Excel: '" + e.Message + "'.");
                                                                                }

                                                                                Detener();
                                                                                return;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                if (itemParametrosExcel.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.ConjuntoNumeros)
                                                                {
                                                                    int indiceNumero = 1;
                                                                    foreach (var num in numerosObtenidos)
                                                                    {
                                                                        if (Pausada) Pausar();
                                                                        if (Detenida) return;

                                                                        EntidadNumero numeroAgregar_Item = new EntidadNumero(string.Empty, num.Numero);
                                                                        numeroAgregar_Item.Textos.AddRange(GenerarTextosInformacion(num.TextosInformacion.ToList()));
                                                                        numeroAgregar_Item.Textos.AddRange(GenerarTextosInformacion(entrada.TextosInformacionFijos));

                                                                        if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                                                            EstablecerNombreCantidad(numeroAgregar_Item, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades, indiceNumero, entrada);

                                                                        numeroAgregar.PosicionElemento_DefinicionNombres = indiceNumero;

                                                                        entrada.Numeros.Add(numeroAgregar_Item);

                                                                        indiceNumero++;
                                                                    }
                                                                }
                                                                else if (itemParametrosExcel.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.Numero)
                                                                {
                                                                    posicion = (entrada.Numeros.Where((i) => i.Textos.Count == 0).Count() + 1);

                                                                    EstablecerNombreCantidad(numeroAgregar, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades, posicion, entrada);

                                                                    entrada.Numeros.Add(numeroAgregar);
                                                                }


                                                                if (mostrarLog)
                                                                {
                                                                    if (encontrado)
                                                                    {
                                                                        log.Add("Se encontró un conjunto de celdas de la entrada '" + entrada.Nombre + "' en el archivo Excel '" + itemParametrosExcel.Libro + "'.");
                                                                        InformeResultados.TextoLog.Add("Se encontró un conjunto de celdas de la entrada '" + entrada.Nombre + "' en el archivo Excel '" + itemParametrosExcel.Libro + "'.");
                                                                    }
                                                                    else
                                                                    {
                                                                        try { Thread.Sleep(3000); } catch (Exception) { }
                                                                        ;
                                                                        ConError = true;

                                                                        log.Add("No se encontró un conjunto de celdas de la entrada '" + entrada.Nombre + "'.");
                                                                        InformeResultados.TextoLog.Add("No se encontró un conjunto de celdas de la entrada '" + entrada.Nombre + "'.");

                                                                        Detener();
                                                                        return;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (mostrarLog)
                                                                {
                                                                    log.Add("No se encontró el archivo Excel '" + itemParametrosExcel.Libro + "'.");
                                                                    InformeResultados.TextoLog.Add("No se encontró el archivo Excel '" + itemParametrosExcel.Libro + "'.");
                                                                }
                                                            }

                                                            entrada.CantidadSubElementosProcesados++;
                                                        }

                                                        int posicion_ = 0;
                                                        foreach (var itemNumero in entrada.Numeros)
                                                        {
                                                            if (Pausada) Pausar();
                                                            if (Detenida) return;

                                                            posicion_++;
                                                            EstablecerNombreCantidad(itemNumero, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades, posicion_, entrada);
                                                        }

                                                        if (entrada.UtilizarCantidadNumeros)
                                                        {
                                                            EntidadNumero numeroAgregar = new EntidadNumero("Cantidad de números del vector de entrada '" + entrada.Nombre + "'", entrada.Numeros.Count);
                                                            numeroAgregar.Textos.Add("cantidad");

                                                            if (entrada.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.AgregarCantidadNumeros)
                                                            {
                                                                entrada.Numeros.Add(numeroAgregar);
                                                            }
                                                            else if (entrada.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.UtilizarSoloCantidaadNumeros)
                                                            {
                                                                entrada.Numeros.Clear();
                                                                entrada.Numeros.Add(numeroAgregar);
                                                            }
                                                        }

                                                        break;
                                                }
                                            }
                                        }

                                        break;
                                }

                                if (entrada.OperacionInterna != TipoOperacionAritmeticaEjecucion.Ninguna)
                                {
                                    bool error = ConError;
                                    string logError = string.Empty;

                                    switch (entrada.OperacionInterna)
                                    {
                                        case TipoOperacionAritmeticaEjecucion.Suma:
                                        case TipoOperacionAritmeticaEjecucion.Resta:
                                        case TipoOperacionAritmeticaEjecucion.Multiplicacion:
                                        case TipoOperacionAritmeticaEjecucion.Division:

                                            ElementoEntradaEjecucion.RealizarOperacionInterna(entrada, ref error, ref logError, this);
                                            ConError = error;

                                            if (ConError)
                                            {
                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                ;
                                                log.Add(logError);
                                                InformeResultados.TextoLog.Add(logError);

                                                Detener();
                                                return;
                                            }

                                            break;

                                        case TipoOperacionAritmeticaEjecucion.Potencia:
                                        case TipoOperacionAritmeticaEjecucion.Raiz:

                                            ElementoEntradaEjecucion.RealizarOperacionInterna_PotenciaRaiz(entrada, ref error, ref logError, this);
                                            ConError = error;

                                            if (ConError)
                                            {
                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                ;
                                                log.Add(logError);
                                                InformeResultados.TextoLog.Add(logError);

                                                Detener();
                                                return;
                                            }

                                            break;

                                        case TipoOperacionAritmeticaEjecucion.Porcentaje:

                                            ElementoEntradaEjecucion.RealizarOperacionInterna_Porcentaje(entrada, ref error, ref logError, this);
                                            ConError = error;

                                            if (ConError)
                                            {
                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                ;
                                                log.Add(logError);
                                                InformeResultados.TextoLog.Add(logError);

                                                Detener();
                                                return;
                                            }

                                            break;

                                        case TipoOperacionAritmeticaEjecucion.Logaritmo:

                                            ElementoEntradaEjecucion.RealizarOperacionInterna_Logaritmo(entrada, ref error, ref logError, this);
                                            ConError = error;

                                            if (ConError)
                                            {
                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                ;
                                                log.Add(logError);
                                                InformeResultados.TextoLog.Add(logError);

                                                Detener();
                                                return;
                                            }

                                            break;

                                        case TipoOperacionAritmeticaEjecucion.Factorial:

                                            ElementoEntradaEjecucion.RealizarOperacionInterna_Factorial(entrada, ref error, ref logError, this, log);
                                            ConError = error;

                                            if (ConError)
                                            {
                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                ;
                                                log.Add(logError);
                                                InformeResultados.TextoLog.Add(logError);

                                                Detener();
                                                return;
                                            }

                                            break;

                                        case TipoOperacionAritmeticaEjecucion.Inverso:

                                            ElementoEntradaEjecucion.RealizarOperacionInterna_Inverso(entrada, ref error, ref logError, this);
                                            ConError = error;

                                            if (ConError)
                                            {
                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                ;
                                                log.Add(logError);
                                                InformeResultados.TextoLog.Add(logError);

                                                Detener();
                                                return;
                                            }

                                            break;

                                        case TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar:

                                            ElementoEntradaEjecucion.RealizarOperacionInterna_SeleccionarOrdenar(entrada, this);
                                            //ConError = error;

                                            //if (ConError)
                                            //{
                                            //    Thread.Sleep(3000);
                                            //    log.Add(logError);

                                            //    Thread.CurrentThread.Abort();
                                            //}

                                            break;

                                        case TipoOperacionAritmeticaEjecucion.LimpiarDatos:
                                            ElementoEntradaEjecucion.RealizarOperacionInterna_LimpiarDatos(entrada, this);
                                            break;

                                        case TipoOperacionAritmeticaEjecucion.RedondearCantidades:
                                            ElementoEntradaEjecucion.RealizarOperacionInterna_RedondearCantidades(entrada, this);
                                            break;
                                    }
                                }

                                //foreach(var itemNumero in numeros.Numeros)
                                //    numeros.Textos.AddRange(itemNumero.Textos);

                                if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.AgregarCantidad_ComoTextoInformacion)
                                {
                                    foreach (var itemNumero in entrada.Numeros)
                                    {
                                        if (Pausada) Pausar();
                                        if (Detenida) return;

                                        itemNumero.Textos.Add(itemNumero.Numero.ToString());
                                    }
                                }

                                if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ComprobarConfirmarCantidades_Ejecucion)
                                {
                                    DigitarConjuntoNumeros digitar = new DigitarConjuntoNumeros();
                                    digitar.CalculoActual = Calculo;
                                    digitar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                                    digitar.CantidadDecimalesCantidades = Calculo.CantidadDecimalesCantidades;
                                    digitar.descripcionEntrada.Text = entrada.Nombre;
                                    digitar.Title = "Comprobar y confirmar vector de números de entrada";
                                    digitar.titulo.Text = "Comprobar y confirmar vector de números de entrada " + "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                    digitar.Numeros.AddRange(entrada.Numeros);
                                    digitar.CargarTextosDigitados(-1);

                                    bool digita = (bool)digitar.ShowDialog();
                                    if (digita == true)
                                    {
                                        entrada.Numeros.Clear();
                                        entrada.Numeros.AddRange(digitar.Numeros);

                                        if (digitar.Pausado)
                                        {
                                            Pausar();
                                        }
                                    }
                                    else if (digita == false)
                                    {
                                        Detener();
                                        return;
                                    }

                                }

                                if (entrada.OperacionInterna != TipoOperacionAritmeticaEjecucion.Ninguna &&
                                    entrada.OperacionInterna != TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                                {
                                    if (entrada.UtilizarCantidadNumeros)
                                    {
                                        EntidadNumero numeroAgregar = new EntidadNumero("Cantidad de números del vector de entrada '" + entrada.Nombre + "'", entrada.Numeros.Count);
                                        numeroAgregar.Textos.Add("cantidad");

                                        if (entrada.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.AgregarCantidadNumeros)
                                        {
                                            entrada.Numeros.Add(numeroAgregar);
                                        }
                                        else if (entrada.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.UtilizarSoloCantidaadNumeros)
                                        {
                                            entrada.Numeros.Clear();
                                            entrada.Numeros.Add(numeroAgregar);
                                        }
                                    }
                                }

                                entrada.SeleccionarFiltrarNumeros(entrada.OrigenesDatos.LastOrDefault());

                                var registroCantidadesObtenidas_UltimaEjecucion = App.CargarElementosObtenidas_Entradas_UltimaEjecucion(Calculo.ID, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ID);
                                registroCantidadesObtenidas_UltimaEjecucion.CantidadNumeros_Obtenidos_UltimaEjecucion = entrada.Numeros.Count;

                                if (entrada.OrigenesDatos.LastOrDefault() != null)
                                {
                                    registroCantidadesObtenidas_UltimaEjecucion.PosicionInicialNumeros_Obtenidos_UltimaEjecucion = entrada.OrigenesDatos.LastOrDefault().PosicionInicialNumeros_Obtenidos_UltimaEjecucion;
                                    registroCantidadesObtenidas_UltimaEjecucion.PosicionFinalNumeros_Obtenidos_UltimaEjecucion = entrada.OrigenesDatos.LastOrDefault().PosicionFinalNumeros_Obtenidos_UltimaEjecucion;
                                }

                                App.GuardarElementosObtenidas_Entradas_UltimaEjecucion(registroCantidadesObtenidas_UltimaEjecucion);

                                OrdenarCantidades_Operacion_Despues(entrada, entrada.ElementoDiseñoRelacionado.OrdenarNumerosDespuesEjecucion, entrada.ElementoDiseñoRelacionado.OrdenarNumeros_AntesEjecucion);

                                break;


                        }

                    }
                    else if (entrada.TipoEntrada == TipoEntrada.TextosInformacion)
                    {
                        if (mostrarLog)
                        {
                            string info = string.Empty;

                            if (Calculo.MostrarInformacionElementos_InformeResultados &&
                                !string.IsNullOrEmpty(entrada.ElementoDiseñoRelacionado.Info))
                            {
                                info = " (" + entrada.ElementoDiseñoRelacionado.Info + ") ";
                            }

                            if (mostrarLog && !string.IsNullOrEmpty(entrada.Nombre))
                            {
                                log.Add("Obteniendo datos del vector de números de entrada '" + entrada.Nombre + "'..." + info);
                                InformeResultados.TextoLog.Add("Obteniendo datos del vector de números de entrada '" + entrada.Nombre + "'..." + info);
                            }
                            else
                            {
                                log.Add("Obteniendo datos del vector de números de entrada..." + info);
                                InformeResultados.TextoLog.Add("Obteniendo datos del vector de números de entrada..." + info);
                            }
                        }

                        switch (entrada.TipoEntrada)
                        {
                            case TipoEntrada.TextosInformacion:
                                ElementoConjuntoTextosEntradaEjecucion textos = (ElementoConjuntoTextosEntradaEjecucion)entrada;
                                switch (textos.TipoEntradaConjuntoTextos)
                                {
                                    case TipoOpcionTextosInformacionEntrada.TextosInformacionFijos:
                                        textos.CantidadSubElementosProcesados++;

                                        if (mostrarLog)
                                        {
                                            log.Add("El el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "' son datos fijos definidos.");
                                            InformeResultados.TextoLog.Add("El el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "' son datos fijos definidos.");
                                        }

                                        break;
                                    case TipoOpcionTextosInformacionEntrada.SeDigita:

                                        if (!ModoToolTips)
                                        {
                                            DigitarConjuntoTextos digitar = new DigitarConjuntoTextos();
                                            digitar.descripcionEntrada.Text = textos.Nombre;

                                            textos.ElementoDiseñoRelacionado.EntradaRelacionada.ObtenerTextosInformacion_EntradasAnteriores_Conjuntos(this);
                                            digitar.ConjuntoTextosInformacionDigitacion = textos.ElementoDiseñoRelacionado.EntradaRelacionada.ConjuntoTextosInformacion_Digitacion;
                                            digitar.UtilizarSoloTextosPredefinidos = textos.ElementoDiseñoRelacionado.EntradaRelacionada.UtilizarSoloTextosPredefinidos;

                                            digitar.titulo.Text = "Digitar el vector de vectores de cadenas de texto de entrada " + "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                            if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DigitarEjecucionCantidadTextosDigitacion)
                                            {
                                                digitar.FijarCantidadTextosDigitacion = true;

                                                DigitarNumero digitarCantidad = new DigitarNumero();
                                                digitarCantidad.textos.Entrada = new Entrada("Entrada");
                                                digitarCantidad.descripcionEntrada.Text = "Cantidad de vectores de cadenas de texto del vector " + entrada.Nombre;
                                                digitarCantidad.titulo.Text += "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                                                digitarCantidad.textos.ConjuntosTextosInformacionDigitacion = new List<ConjuntoTextosInformacion_Digitacion>();
                                                digitarCantidad.OpcionesListaNumeros = new List<Entradas.OpcionListaNumeros_Digitacion>();

                                                bool digitaCantidad = (bool)digitarCantidad.ShowDialog();
                                                if (digitaCantidad == true)
                                                {
                                                    digitar.CantidadTextosDigitacion = (int)digitarCantidad.Numero;

                                                    if (digitar.Pausado)
                                                    {
                                                        Pausar();
                                                    }
                                                }
                                                else if (digitaCantidad == false)
                                                {
                                                    Detener();
                                                    return;
                                                }

                                            }
                                            else if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.FijarCantidadTextosDigitacion)
                                            {
                                                digitar.FijarCantidadTextosDigitacion = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.FijarCantidadTextosDigitacion;
                                                digitar.CantidadTextosDigitacion = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.CantidadTextosDigitacion;
                                            }

                                            if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DigitarEjecucionCantidadFilasTextosDigitacion)
                                            {
                                                digitar.FijarCantidadFilasTextosDigitacion = true;

                                                DigitarNumero digitarCantidad = new DigitarNumero();
                                                digitarCantidad.textos.Entrada = new Entrada("Entrada");
                                                digitarCantidad.descripcionEntrada.Text = "Cantidad de vectores de cadenas de texto del vector de entrada " + entrada.Nombre;
                                                digitarCantidad.titulo.Text += "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                                                digitarCantidad.textos.ConjuntosTextosInformacionDigitacion = new List<ConjuntoTextosInformacion_Digitacion>();
                                                digitarCantidad.OpcionesListaNumeros = new List<Entradas.OpcionListaNumeros_Digitacion>();

                                                bool digitaCantidad = (bool)digitarCantidad.ShowDialog();
                                                if (digitaCantidad == true)
                                                {
                                                    digitar.CantidadFilasTextosDigitacion = (int)digitarCantidad.Numero;

                                                    if (digitar.Pausado)
                                                    {
                                                        Pausar();
                                                    }
                                                }
                                                else if (digitaCantidad == false)
                                                {
                                                    Detener();
                                                    return;
                                                }

                                            }
                                            else if (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.FijarCantidadFilasTextosDigitacion)
                                            {
                                                digitar.FijarCantidadFilasTextosDigitacion = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.FijarCantidadFilasTextosDigitacion;
                                                digitar.CantidadFilasTextosDigitacion = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.CantidadFilasTextosDigitacion;
                                            }


                                            bool digita = (bool)digitar.ShowDialog();
                                            if (digita == true)
                                            {
                                                textos.FilasTextosInformacion.AddRange(digitar.FilasTextos);

                                                if (digitar.Pausado)
                                                {
                                                    Pausar();
                                                }
                                            }
                                            else if (digita == false)
                                            {
                                                Detener();
                                                return;
                                            }

                                            textos.CantidadSubElementosProcesados++;

                                            if (mostrarLog)
                                            {
                                                log.Add("Se digitó el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "'.");
                                                InformeResultados.TextoLog.Add("Se digitó la entrada '" + entrada.Nombre + "'.");
                                            }
                                        }

                                        break;

                                    case TipoOpcionTextosInformacionEntrada.SeObtiene:

                                        if (entrada.OrigenesDatos.Any())
                                        {
                                            foreach (var itemOrigenDatos in entrada.OrigenesDatos)
                                            {
                                                if (Pausada) Pausar();
                                                if (Detenida) return;

                                                string rutaTemporalArchivoFTP = string.Empty;
                                                string RutaArchivo_ = string.Empty;

                                                switch (itemOrigenDatos.TipoOrigenDatos)
                                                {
                                                    case TipoOrigenDatos.Archivo:

                                                        ElementoArchivoOrigenDatosEjecucion archivo = (ElementoArchivoOrigenDatosEjecucion)itemOrigenDatos;

                                                        RutaArchivo_ = string.Empty;
                                                        List<string> RutasArchivos = new List<string>();

                                                        HttpResponseMessage respuestaArchivoFTP = null;

                                                        if (archivo.TipoArchivo == TipoArchivo.ServidorFTP)
                                                        {
                                                            RutaArchivo_ = archivo.ObtenerURLFTP_Ejecucion_Entrada(this, itemCalculo, entrada);

                                                            NetworkCredential credenciales = null;
                                                            HttpClient archivoFTP = null;

                                                            if (archivo.CredencialesFTP != null)
                                                            {
                                                                if (archivo.CredencialesFTP.UsuarioAnonimo)
                                                                {
                                                                    credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                                                                }
                                                                else
                                                                {
                                                                    if (string.IsNullOrEmpty(archivo.CredencialesFTP.NombreUsuario) &
                                                                        string.IsNullOrEmpty(archivo.CredencialesFTP.Clave))
                                                                    {
                                                                        credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (string.IsNullOrEmpty(archivo.CredencialesFTP.NombreUsuario))
                                                                        {
                                                                            credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                                                                        }
                                                                        else
                                                                        {
                                                                            credenciales = new NetworkCredential(archivo.CredencialesFTP.NombreUsuario,
                                                                                archivo.CredencialesFTP.Clave);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                                credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);

                                                            archivoFTP = new HttpClient(new HttpClientHandler() { Credentials = credenciales });
                                                            //archivoFTP.Credentials = credenciales;
                                                            //((FtpWebRequest)archivoFTP).Method = WebRequestMethods.Ftp.DownloadFile;

                                                            try
                                                            {
                                                                respuestaArchivoFTP = archivoFTP.GetAsync(RutaArchivo_).Result;
                                                            }
                                                            catch (Exception error)
                                                            {
                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                ;
                                                                ConError = true;

                                                                if (mostrarLog)
                                                                {
                                                                    log.Add("Error al obtener el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "' del archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                                                    InformeResultados.TextoLog.Add("Error al obtener el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "' del archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                                                }

                                                                Detener();
                                                                return;
                                                            }
                                                        }
                                                        else if (archivo.TipoArchivo == TipoArchivo.Internet)
                                                        {
                                                            RutaArchivo_ = archivo.RutaArchivo;
                                                            if (RutaArchivo_.Length >= 8 && !RutaArchivo_.Substring(0, 7).ToLower().Equals("http://") &&
                                                                !RutaArchivo_.Substring(0, 8).ToLower().Equals("https://"))
                                                                RutaArchivo_ = "http://" + RutaArchivo_;
                                                            else if (RutaArchivo_.Length >= 8 && !RutaArchivo_.Substring(0, 8).ToLower().Equals("https://") &&
                                                                !RutaArchivo_.Substring(0, 7).ToLower().Equals("http://"))
                                                                RutaArchivo_ = "https://" + RutaArchivo_;

                                                            HttpClient archivoFTP = new HttpClient();
                                                            //((HttpWebRequest)archivoFTP).Method = WebRequestMethods.Ftp.DownloadFile;

                                                            try
                                                            {
                                                                respuestaArchivoFTP = archivoFTP.GetAsync(RutaArchivo_).Result;
                                                            }
                                                            catch (Exception error)
                                                            {
                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                ;
                                                                ConError = true;

                                                                if (mostrarLog)
                                                                {
                                                                    log.Add("Error al obtener el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "' del archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                                                    InformeResultados.TextoLog.Add("Error al obtener el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "' del archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                                                }

                                                                Detener();
                                                                return;
                                                            }
                                                        }
                                                        else
                                                            RutasArchivos = archivo.ObtenerRutaArchivoEjecucion_Entrada(Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")), this, itemCalculo, entrada);

                                                        foreach (var RutaArchivo in RutasArchivos)
                                                        {
                                                            if (Pausada) Pausar();
                                                            if (Detenida) return;

                                                            bool valido = false;

                                                            if (archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                                                            {
                                                                if (archivo.UsarURL_Office)
                                                                {
                                                                    if (!string.IsNullOrEmpty(RutaArchivo) &&
                                                                        (PlanMath_Word.VerificarURL_Documento(RutaArchivo) ||
                                                                        File.Exists(RutaArchivo)))
                                                                    {
                                                                        valido = true;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (!valido)
                                                                    {
                                                                        if (!string.IsNullOrEmpty(RutaArchivo) &&
                                                                        File.Exists(RutaArchivo))
                                                                        {
                                                                            valido = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                                valido = true;

                                                            if (valido)
                                                            {
                                                                if (mostrarLog)
                                                                {
                                                                    if (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.TextoPantalla)
                                                                    {
                                                                        log.Add("Buscando en el archivo '" + RutaArchivo + "' el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "'...");
                                                                        InformeResultados.TextoLog.Add("Buscando en el archivo '" + RutaArchivo + "' el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "'...");
                                                                    }
                                                                    else
                                                                    {
                                                                        log.Add("Esperando el archivo en pantalla del vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "'...");
                                                                        InformeResultados.TextoLog.Add("Esperando el archivo en pantalla del vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "'...");
                                                                    }
                                                                }

                                                                bool existe = false;

                                                                if (archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word &&
                                                                    archivo.UsarURL_Office)
                                                                {
                                                                    existe = true;
                                                                }
                                                                else
                                                                {

                                                                    if (archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                                                                    archivo.TipoArchivo == TipoArchivo.RedLocal)
                                                                    {
                                                                        existe = File.Exists(RutaArchivo);
                                                                    }
                                                                    else if (archivo.TipoArchivo == TipoArchivo.ServidorFTP)
                                                                    {
                                                                        if (respuestaArchivoFTP != null && respuestaArchivoFTP.StatusCode == HttpStatusCode.OK)
                                                                        //if(respuestaArchivoFTP.ContentLength > 0)
                                                                        {
                                                                            //respuestaArchivoFTP = ((FtpWebRequest)archivoFTP).GetResponse();
                                                                            existe = true;
                                                                        }
                                                                        else
                                                                            existe = false;
                                                                    }
                                                                    else if (archivo.TipoArchivo == TipoArchivo.Internet)
                                                                    {
                                                                        if (respuestaArchivoFTP != null && respuestaArchivoFTP.StatusCode == HttpStatusCode.OK)
                                                                        {
                                                                            existe = true;
                                                                        }
                                                                        else
                                                                            existe = false;
                                                                    }
                                                                }

                                                                rutaTemporalArchivoFTP = string.Empty;
                                                                string rutaArchivo_Busquedas = RutaArchivo;

                                                                if (existe)
                                                                {
                                                                    if (!itemOrigenDatos.EstablecerLecturasNavegaciones_Busquedas)
                                                                    {
                                                                        bool ConError_ = ConError;

                                                                        entrada.LeerBusquedasEntradaArchivo_TextosInformacion(archivo, textos, entrada, archivo.MismaLecturaArchivo, mostrarLog,
                                                                            ref ConError_, this, archivosAbiertos, ArchivosOrigenesDatos_Abiertos, ref rutaTemporalArchivoFTP, ref rutaArchivo_Busquedas, respuestaArchivoFTP, log,
                                                                            InformeResultados.TextoLog);

                                                                        ConError = ConError_;

                                                                    }
                                                                    else
                                                                    {
                                                                        foreach (var itemLecturaNavegacion in itemOrigenDatos.LecturasNavegaciones)
                                                                        {
                                                                            if (Pausada) Pausar();
                                                                            if (Detenida) return;

                                                                            bool ConError_ = ConError;
                                                                            string rutaTemporalArchivo = string.Empty;

                                                                            entrada.LeerBusquedasEntradaArchivo_TextosInformacion(archivo, textos, entrada, itemLecturaNavegacion.MismaLecturaBusquedasArchivo, mostrarLog,
                                                                            ref ConError_, this, archivosAbiertos, ArchivosOrigenesDatos_Abiertos, ref rutaTemporalArchivoFTP, ref rutaArchivo_Busquedas, respuestaArchivoFTP, log, InformeResultados.TextoLog,
                                                                            itemLecturaNavegacion);

                                                                            ConError = ConError_;

                                                                        }
                                                                    }

                                                                    foreach (var busqueda in archivo.Busquedas)
                                                                    {
                                                                        busqueda.ReiniciarBusqueda();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    try { Thread.Sleep(3000); } catch (Exception) { }
                                                                    ;
                                                                    ConError = true;

                                                                    if (mostrarLog)
                                                                    {
                                                                        log.Add("No se encontró el archivo '" + RutaArchivo + "'.");
                                                                        InformeResultados.TextoLog.Add("No se encontró el archivo '" + RutaArchivo + "'.");
                                                                    }

                                                                    Detener();
                                                                    return;
                                                                }
                                                            }
                                                        }

                                                        break;

                                                    case TipoOrigenDatos.DesdeInternet:
                                                        ElementoInternetOrigenDatosEjecucion url = (ElementoInternetOrigenDatosEjecucion)itemOrigenDatos;

                                                        url.URL = url.ObtenerURLEjecucion_Entrada(this, itemCalculo, entrada);
                                                        url.ObjetoURL.URL = url.URL;

                                                        if (mostrarLog)
                                                        {
                                                            log.Add("Buscando en la URL '" + url.URL + "' el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "'...");
                                                            InformeResultados.TextoLog.Add("Buscando en la URL '" + url.URL + "' el vector de vectores de cadenas de texto de entrada '" + entrada.Nombre + "'...");
                                                        }

                                                        if (!string.IsNullOrEmpty(url.URL))
                                                        {
                                                            if (url.EstablecerParametrosEjecucion)
                                                            {
                                                                EstablecerParametrosURL_Ejecucion establecerParametros = new EstablecerParametrosURL_Ejecucion();
                                                                establecerParametros.url.Text = url.URL;
                                                                establecerParametros.Entrada = url.ObjetoURL;
                                                                establecerParametros.ShowDialog();
                                                            }

                                                            if (!itemOrigenDatos.EstablecerLecturasNavegaciones_Busquedas)
                                                            {
                                                                rutaTemporalArchivoFTP = string.Empty;
                                                                RutaArchivo_ = string.Empty;
                                                                respuestaArchivoFTP = null;

                                                                bool ConError_ = ConError;

                                                                entrada.LeerBusquedasEntradaURL_TextosInformacion(url, textos, entrada, url.MismaLecturaArchivo, mostrarLog, ref ConError_,
                                                                    this, archivosAbiertos, ArchivosOrigenesDatos_Abiertos, ref rutaTemporalArchivoFTP, ref RutaArchivo_, respuestaArchivoFTP, log, InformeResultados.TextoLog);

                                                                ConError = ConError_;
                                                            }
                                                            else
                                                            {
                                                                foreach (var itemLecturaNavegacion in itemOrigenDatos.LecturasNavegaciones)
                                                                {
                                                                    if (Pausada) Pausar();
                                                                    if (Detenida) return;

                                                                    rutaTemporalArchivoFTP = string.Empty;
                                                                    RutaArchivo_ = string.Empty;
                                                                    respuestaArchivoFTP = null;

                                                                    bool ConError_ = ConError;

                                                                    entrada.LeerBusquedasEntradaURL_TextosInformacion(url, textos, entrada, url.MismaLecturaArchivo, mostrarLog, ref ConError_,
                                                                    this, archivosAbiertos, ArchivosOrigenesDatos_Abiertos, ref rutaTemporalArchivoFTP, ref RutaArchivo_, respuestaArchivoFTP, log, InformeResultados.TextoLog,
                                                                    itemLecturaNavegacion);

                                                                    ConError = ConError_;
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {
                                                            try { Thread.Sleep(3000); } catch (Exception) { }
                                                            ;
                                                            ConError = true;

                                                            if (mostrarLog)
                                                            {
                                                                log.Add("La URL '" + url.URL + "' no es válida.");
                                                                InformeResultados.TextoLog.Add("La URL '" + url.URL + "' no es válida.");
                                                            }

                                                            Detener();
                                                            return;
                                                        }

                                                        break;

                                                    case TipoOrigenDatos.Excel:
                                                        ElementoArchivoExcelOrigenDatosEjecucion excel = (ElementoArchivoExcelOrigenDatosEjecucion)itemOrigenDatos;

                                                        foreach (var itemParametrosExcel in excel.ParametrosExcel)
                                                        {
                                                            if (Pausada) Pausar();
                                                            if (Detenida) return;

                                                            bool valido = false;

                                                            if (excel.UsarURLLibro)
                                                            {
                                                                if (!string.IsNullOrEmpty(itemParametrosExcel.URLOffice_Original) &&
                                                                    (PlanMath_Excel.VerificarURL_Libro(itemParametrosExcel.URLOffice_Original) ||
                                                                    File.Exists(itemParametrosExcel.URLOffice_Original)))
                                                                {
                                                                    valido = true;
                                                                    itemParametrosExcel.Libro = itemParametrosExcel.URLOffice_Original;
                                                                }
                                                            }

                                                            if (!valido)
                                                            {
                                                                itemParametrosExcel.Libro = excel.ObtenerRutaArchivoEjecucion_Entrada(Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")), this, itemCalculo, entrada);

                                                                if (!string.IsNullOrEmpty(itemParametrosExcel.Libro) &&
                                                                File.Exists(itemParametrosExcel.Libro))
                                                                {
                                                                    valido = true;
                                                                }
                                                            }

                                                            if (mostrarLog)
                                                            {
                                                                log.Add("Buscando en el archivo Excel '" + itemParametrosExcel.Libro + "' el vector de vectores de cadenas de texto de entrada " + entrada.Nombre + "...");
                                                                InformeResultados.TextoLog.Add("Buscando en el archivo Excel '" + itemParametrosExcel.Libro + "' el vector de vectores de cadenas de texto de entrada " + entrada.Nombre + "...");
                                                            }

                                                            if (valido)
                                                            {
                                                                List<List<string>> textosObtenidos = new List<List<string>>();
                                                                bool encontrado = false;

                                                                if (itemParametrosExcel.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.TextosInformacion)
                                                                {
                                                                    try
                                                                    {
                                                                        textosObtenidos = PlanMath_Excel.ObtenerEntrada_ConjuntoTextosInformacion_En_Excel(itemParametrosExcel, ref encontrado, this.GUID_EjecucionCalculo);

                                                                    }
                                                                    catch (Exception e)
                                                                    {
                                                                        if (e.HResult == -2146827284)
                                                                        {
                                                                            if (!ModoToolTips)
                                                                            {
                                                                                //MessageBox.Show("El archivo Excel: '" + excel.ParametrosExcel.Libro + "' se encuentra abierto. Por favor ciérralo para continuar.");
                                                                                log.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se encuentra abierto. Esperando a que se envíe el vector de vectores de cadenas de texto de entrada desde Excel...");
                                                                                InformeResultados.TextoLog.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se encuentra abierto. Esperando a que se envíe el vector de vectores de cadenas de texto de entrada desde Excel...");

                                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                                ;
                                                                                encontrado = false;
                                                                                textosObtenidos = ObtenerEntradaConjuntoTextosInformacion_EnvioDesdeExcel(itemParametrosExcel, ref encontrado);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            try { Thread.Sleep(3000); } catch (Exception) { }
                                                                            ;
                                                                            ConError = true;

                                                                            if (mostrarLog)
                                                                            {
                                                                                log.Add("Error al leer archivo Excel: '" + e.Message + "'.");
                                                                                InformeResultados.TextoLog.Add("Error al leer archivo Excel: '" + e.Message + "'.");
                                                                            }

                                                                            Detener();
                                                                            return;
                                                                        }
                                                                    }

                                                                    textos.FilasTextosInformacion.AddRange(textosObtenidos.Select(item => new FilaTextosInformacion_Entrada() { TextosInformacion = GenerarTextosInformacion(item) }));
                                                                }
                                                                else if (itemParametrosExcel.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.Numero)
                                                                {
                                                                    string texto = string.Empty;
                                                                    try
                                                                    {
                                                                        texto = PlanMath_Excel.ObtenerEntrada_En_Excel(itemParametrosExcel, ref encontrado, this.GUID_EjecucionCalculo).ToString();
                                                                    }
                                                                    catch (Exception e)
                                                                    {
                                                                        if (e.HResult == -2146827284)
                                                                        {
                                                                            if (!ModoToolTips)
                                                                            {
                                                                                //MessageBox.Show("El archivo Excel: '" + excel.ParametrosExcel.Libro + "' se encuentra abierto. Por favor ciérralo para continuar.");
                                                                                log.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se encuentra abierto. Esperando a que se envíe el vector de vectores de cadenas de texto de entrada desde Excel...");
                                                                                InformeResultados.TextoLog.Add("El archivo Excel: '" + itemParametrosExcel.Libro + "' se encuentra abierto. Esperando a que se envíe el vector de vectores de cadenas de texto de entrada desde Excel...");

                                                                                try { Thread.Sleep(3000); } catch (Exception) { }
                                                                                ;
                                                                                encontrado = false;
                                                                                texto = ObtenerEntrada_EnvioDesdeExcel(itemParametrosExcel, ref encontrado).ToString();
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            try { Thread.Sleep(3000); } catch (Exception) { }
                                                                            ;
                                                                            ConError = true;

                                                                            if (mostrarLog)
                                                                            {
                                                                                log.Add("Error al leer archivo Excel: '" + e.Message + "'.");
                                                                                InformeResultados.TextoLog.Add("Error al leer archivo Excel: '" + e.Message + "'.");
                                                                            }

                                                                            Detener();
                                                                            return;
                                                                        }
                                                                    }

                                                                    if (!textos.FilasTextosInformacion.Any(itemTexto => (itemTexto.TextosInformacion.Any(j => j.Trim().ToLower() == texto.Trim().ToLower()))))
                                                                        textos.FilasTextosInformacion.Add(new FilaTextosInformacion_Entrada() { TextosInformacion = new List<string>() { texto } });
                                                                }

                                                                if (mostrarLog)
                                                                {
                                                                    if (encontrado)
                                                                    {
                                                                        log.Add("Se encontró un conjunto de celdas de la entrada '" + entrada.Nombre + "' en el archivo Excel '" + itemParametrosExcel.Libro + "'.");
                                                                        InformeResultados.TextoLog.Add("Se encontró un conjunto de celdas de la entrada '" + entrada.Nombre + "' en el archivo Excel '" + itemParametrosExcel.Libro + "'.");
                                                                    }
                                                                    else
                                                                    {
                                                                        try { Thread.Sleep(3000); } catch (Exception) { }
                                                                        ;
                                                                        ConError = true;

                                                                        log.Add("No se encontró un conjunto de celdas de la entrada '" + entrada.Nombre + "'.");
                                                                        InformeResultados.TextoLog.Add("No se encontró un conjunto de celdas de la entrada '" + entrada.Nombre + "'.");

                                                                        Detener();
                                                                        return;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (mostrarLog)
                                                                {
                                                                    log.Add("No se encontró el archivo Excel '" + itemParametrosExcel.Libro + "'.");
                                                                    InformeResultados.TextoLog.Add("No se encontró el archivo Excel '" + itemParametrosExcel.Libro + "'.");
                                                                }
                                                            }

                                                            textos.CantidadSubElementosProcesados++;
                                                        }

                                                        break;
                                                }
                                            }
                                        }

                                        break;
                                }

                                if (textos.ElementoDiseñoRelacionado.EntradaRelacionada.UtilizarTextosInformacionUnicos)
                                    textos.FilasTextosInformacion = textos.FilasTextosInformacion.Select(item => new FilaTextosInformacion_Entrada() { TextosInformacion = item.TextosInformacion.Distinct().ToList() }).ToList();

                                if (textos.ElementoDiseñoRelacionado.EntradaRelacionada.ComprobarConfirmarCantidades_Ejecucion)
                                {
                                    DigitarConjuntoTextos digitar = new DigitarConjuntoTextos();
                                    digitar.descripcionEntrada.Text = textos.Nombre;
                                    digitar.Title = "Comprobar y confirmar el vector de vectores de cadenas de texto de entrada";
                                    digitar.titulo.Text = "Comprobar y confirmar el vector de vectores de cadenas de texto de entrada " + "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                    digitar.FilasTextos.AddRange(textos.FilasTextosInformacion);

                                    bool digita = (bool)digitar.ShowDialog();
                                    if (digita == true)
                                    {
                                        textos.FilasTextosInformacion.Clear();
                                        textos.FilasTextosInformacion.AddRange(digitar.FilasTextos);

                                        if (digitar.Pausado)
                                        {
                                            Pausar();
                                        }
                                    }
                                    else if (digita == false)
                                    {
                                        Detener();
                                        return;
                                    }
                                }

                                textos.SeleccionarFiltrarNumeros(textos.OrigenesDatos.LastOrDefault());

                                var registroCantidadesObtenidas_UltimaEjecucion = App.CargarElementosObtenidas_Entradas_UltimaEjecucion(Calculo.ID, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ID);
                                registroCantidadesObtenidas_UltimaEjecucion.CantidadTextosInformacion_Obtenidos_UltimaEjecucion = textos.FilasTextosInformacion.Count;

                                if (textos.OrigenesDatos.LastOrDefault() != null)
                                {
                                    registroCantidadesObtenidas_UltimaEjecucion.PosicionInicialNumeros_Obtenidos_UltimaEjecucion = textos.OrigenesDatos.LastOrDefault().PosicionInicialNumeros_Obtenidos_UltimaEjecucion;
                                    registroCantidadesObtenidas_UltimaEjecucion.PosicionFinalNumeros_Obtenidos_UltimaEjecucion = textos.OrigenesDatos.LastOrDefault().PosicionFinalNumeros_Obtenidos_UltimaEjecucion;
                                }

                                App.GuardarElementosObtenidas_Entradas_UltimaEjecucion(registroCantidadesObtenidas_UltimaEjecucion);

                                //AgregarTextosInformacionEntrada_Definicion(textos, itemCalculo);

                                break;
                        }


                    }
                }

                if (ModoEjecucionExterna &&
                    infoEjecucionExterna != null &&
                    infoEjecucionExterna.EjecucionTraspaso)
                {
                    if (!infoEjecucionExterna.EjecucionNormal)
                    {
                        if (entrada.GetType() == typeof(ElementoEntradaEjecucion) &&
                            entrada.TipoEntradaConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)
                        {
                            entrada.Numeros.Clear();
                        }
                    }

                    if (infoEjecucionExterna.DatosEntrada != null)
                    {
                        var datosEntrada = infoEjecucionExterna.DatosEntrada;

                        if (datosEntrada.GetType() == typeof(ElementoEntradaEjecucion))
                        {
                            var numerosEntrada = (ElementoEntradaEjecucion)datosEntrada;

                            if (entrada.TipoEntrada != TipoEntrada.TextosInformacion)
                            {
                                if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                                {
                                    entrada.Numeros.AddRange(numerosEntrada.Numeros);

                                }
                            }
                        }
                        else if (datosEntrada.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                        {
                            var numerosEntrada = (ElementoOperacionAritmeticaEjecucion)datosEntrada;

                            if (entrada.TipoEntrada != TipoEntrada.TextosInformacion)
                            {                                
                                if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                                {
                                    entrada.Numeros.AddRange(numerosEntrada.Numeros
                                        .Select(i => new EntidadNumero()
                                        {
                                            Numero = i.Numero,
                                            Textos = i.Textos.ToList(),
                                            Clasificadores_SeleccionarOrdenar = i.Clasificadores_SeleccionarOrdenar.ToList(),
                                        }));
                                }
                            }
                        }

                        foreach (var itemClasificador in entrada.Numeros.SelectMany(i => i.Clasificadores_SeleccionarOrdenar).Distinct())
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            if (!entrada.Clasificadores_Cantidades.Contains(itemClasificador))
                                entrada.Clasificadores_Cantidades.Add(itemClasificador);
                        }
                    }
                    else
                    {
                        var datosEntrada = infoEjecucionExterna.DatosSubEntrada;

                        if (datosEntrada.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion))
                        {
                            var numerosEntrada = (ElementoDiseñoOperacionAritmeticaEjecucion)datosEntrada;

                            if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                            {
                                if (entrada.TipoEntrada != TipoEntrada.TextosInformacion)
                                {
                                    entrada.Numeros.AddRange(numerosEntrada.Numeros
                                        .Select(i => new EntidadNumero()
                                        {
                                            Numero = i.Numero,
                                            Textos = i.Textos.ToList(),
                                            Clasificadores_SeleccionarOrdenar = i.Clasificadores_SeleccionarOrdenar.ToList(),
                                        }));
                                }
                            }
                        }

                        foreach (var itemClasificador in entrada.Numeros.SelectMany(i => i.Clasificadores_SeleccionarOrdenar).Distinct())
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            if (!entrada.Clasificadores_Cantidades.Contains(itemClasificador))
                                entrada.Clasificadores_Cantidades.Add(itemClasificador);
                        }
                    }
                }

                if (ModoToolTips && this.TooltipsCalculo != null)
                {
                    if (itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)
                    {
                        var elemento = this.TooltipsCalculo?.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID);

                        if (elemento != null)
                        {
                            if (elemento.EntradaRelacionada != null)
                            {
                                elemento.EntradaRelacionada.EntradaProcesada = entrada.CopiarObjeto();
                            }

                            Calculo.EstablecerEntradaProcesada_TextosInformacion(elemento.EntradaRelacionada,
                                elemento.EntradaRelacionada.ID);
                        }
                    }
                }
                else
                {
                    if (itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)
                        item.ElementoDiseñoRelacionado.EntradaRelacionada.EntradaProcesada = entrada.CopiarObjeto();
                }
            }
            else
            {

                if (((ElementoEntradaEjecucion)item).TipoEntrada == TipoEntrada.ConjuntoNumeros)
                {
                    if (((ElementoEntradaEjecucion)item).TipoEntradaConjuntoNumeros != TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)
                    {
                        ((ElementoEntradaEjecucion)item).Numeros.AddRange(((ElementoEntradaEjecucion)elementoEntrada).Numeros);

                        if (item.GetType() == typeof(ElementoEntradaEjecucion))
                        {
                            if (!item.Clasificadores_Cantidades.Any())
                            {
                                ((ElementoEntradaEjecucion)item).AgregarClasificadoresGenericos_DesdeCantidades();
                            }
                        }
                    }
                }
                else if (((ElementoEntradaEjecucion)item).TipoEntrada == TipoEntrada.TextosInformacion)
                {
                    if (((ElementoConjuntoTextosEntradaEjecucion)item).TipoEntradaConjuntoTextos != TipoOpcionTextosInformacionEntrada.TextosInformacionFijos)
                    {
                        ((ElementoConjuntoTextosEntradaEjecucion)item).FilasTextosInformacion.AddRange(((ElementoConjuntoTextosEntradaEjecucion)item.ElementoDiseñoRelacionado.EntradaRelacionada.EntradaProcesada).FilasTextosInformacion);
                    }
                }
            }

            if (((ElementoEntradaEjecucion)item).TipoEntrada == TipoEntrada.ConjuntoNumeros)
            {
                OrdenarCantidades_Operacion_Despues((ElementoEntradaEjecucion)item, item.ElementoDiseñoRelacionado.OrdenarNumerosDespuesEjecucion,
                    item.ElementoDiseñoRelacionado.OrdenarNumeros_DespuesEjecucion);
            }

            if (item.GetType() == typeof(ElementoEntradaEjecucion))
            {
                if (!item.Clasificadores_Cantidades.Any())
                {
                    ((ElementoEntradaEjecucion)item).AgregarClasificadoresGenericos();
                }
            }

            if (mostrarLog)
            {
                string strMensajeLog = string.Empty;
                string strMensajeLogResultados = string.Empty;

                strMensajeLog += "Las variables o vectores obtenidos de " + item.Nombre + " son:\n";
                strMensajeLogResultados += "Las variables o vectores obtenidos de " + item.Nombre + " son:\n";

                //ElementoEntradaEjecucion entrada = (ElementoEntradaEjecucion)item;

                switch(entrada.TipoEntrada)
                {

                    case TipoEntrada.ConjuntoNumeros:
                        
                        foreach (var itemNumero in entrada.Numeros)
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemNumero))
                            {
                                strMensajeLog += itemNumero.Nombre + ": " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                                strMensajeLogResultados += itemNumero.Nombre + ": " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                            }
                        }

                        break;

                    case TipoEntrada.TextosInformacion:
                        ElementoConjuntoTextosEntradaEjecucion textos = (ElementoConjuntoTextosEntradaEjecucion)entrada;

                        foreach(var itemFilaTexto in textos.FilasTextosInformacion)
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            foreach (var itemTexto in itemFilaTexto.TextosInformacion)
                            {
                                if (Pausada) Pausar();
                                if (Detenida) return;

                                strMensajeLog += itemTexto + ((itemTexto == itemFilaTexto.TextosInformacion.LastOrDefault()) ? "." : ", ");
                                strMensajeLogResultados += itemTexto + ((itemTexto == itemFilaTexto.TextosInformacion.LastOrDefault()) ? "." : ", ");

                            }

                            strMensajeLog += "\n";
                            strMensajeLogResultados += "\n";

                        }

                        break;
                }

                if (!string.IsNullOrEmpty(strMensajeLog))
                    log.Add(strMensajeLog);
                if (!string.IsNullOrEmpty(strMensajeLogResultados))
                    InformeResultados.TextoLog.Add(strMensajeLogResultados);
            }

            item.Estado = EstadoEjecucion.Procesado;

            if (!modoRepeticion)
            {
                if (ModoToolTips && TooltipsCalculo != null)
                    EntradasProcesadas.Add(TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID));
                else
                    EntradasProcesadas.Add(item.ElementoDiseñoRelacionado);

                CantidadElementosEjecucionProcesados++;                
            }

            ActualizarPorcentajeAvance();

            try { Thread.Sleep(10); } catch (Exception) { };
            if (Pausada) Pausar();
            if (Detenida) return;
        }

        private void ObtenerDatosEntradaEspecificaCalculo(ElementoEjecucionCalculo item, ElementoEjecucionCalculo itemSalidaCalculo, bool mostrarLog)
        {
            item.Estado = EstadoEjecucion.Iniciado;
            ElementoEntradaEjecucion entrada = (ElementoEntradaEjecucion)item;

            if (mostrarLog)
            {
                string info = string.Empty;

                if (Calculo.MostrarInformacionElementos_InformeResultados &&
                    !string.IsNullOrEmpty(entrada.ElementoDiseñoRelacionado.Info))
                {
                    info = " (" + entrada.ElementoDiseñoRelacionado.Info + ") ";
                }

                if (mostrarLog && !string.IsNullOrEmpty(entrada.Nombre))
                {
                    log.Add("Obteniendo variables o vectores de entrada de '" + entrada.Nombre + "'..." + info);
                    InformeResultados.TextoLog.Add("Obteniendo variables o vectores de entrada de '" + entrada.Nombre + "'..." + info);
                }
                else
                {
                    log.Add("Obteniendo variables o vectores de entrada..." + info);
                    InformeResultados.TextoLog.Add("Obteniendo variables o vectores de entrada de '" + entrada.Nombre + "'..." + info);
                }
            }
                        
            if (itemSalidaCalculo.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
            {
                ElementoOperacionAritmeticaEjecucion itemOperacionSalidaCalculo = (ElementoOperacionAritmeticaEjecucion)itemSalidaCalculo;

                switch (entrada.TipoEntrada)
                {
                    case TipoEntrada.ConjuntoNumeros:
                        entrada.Nombre = itemSalidaCalculo.ElementoDiseñoRelacionado.NombreCombo;

                        item.Clasificadores_Cantidades.Clear();

                        log.Add("Se obtienen los números del vector de entrada '" + entrada.Nombre + entrada.ObtenerTextosInformacion_Cadena() + "':\n");
                        InformeResultados.TextoLog.Add("Se obtienen los números del vector de entrada '" + entrada.Nombre + entrada.ObtenerTextosInformacion_Cadena() + "':\n");

                        foreach (var itemNumeroCalculo in itemOperacionSalidaCalculo.Numeros)
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            EntidadNumero numero = itemNumeroCalculo.CopiarObjeto(null, null, entrada, true);

                            entrada.Numeros.Add(numero);

                            if (mostrarLog)
                            {
                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                {
                                    log.Add("Se obtiene el número '" + numero.Nombre + "' , su valor es: " + numero.Numero.ToString() + numero.ObtenerTextosInformacion_Cadena() + ".");
                                    InformeResultados.TextoLog.Add("Se obtiene el número '" + numero.Nombre + "' , su valor es: " + numero.Numero.ToString() + numero.ObtenerTextosInformacion_Cadena() + ".");
                                }
                            }
                        }

                        item.CantidadSubElementosProcesados++;
                        break;
                }
            }
            else if (itemSalidaCalculo.GetType() == typeof(ElementoEntradaEjecucion))
            {
                ElementoEntradaEjecucion itemOperacionSalidaCalculo = (ElementoEntradaEjecucion)itemSalidaCalculo;

                ElementoEntradaEjecucion entradaConjunto = (ElementoEntradaEjecucion)item;
                entradaConjunto.Nombre = itemSalidaCalculo.ElementoDiseñoRelacionado.NombreCombo;
                entradaConjunto.Numeros.Clear();

                log.Add("Se obtienen los números del vector de entrada '" + entradaConjunto.Nombre + entradaConjunto.ObtenerTextosInformacion_Cadena() + "':\n");
                InformeResultados.TextoLog.Add("Se obtienen los números del vector de entrada '" + entradaConjunto.Nombre + entradaConjunto.ObtenerTextosInformacion_Cadena() + "':\n");

                item.Clasificadores_Cantidades.Clear();

                foreach (var itemNumeroCalculo in itemOperacionSalidaCalculo.Numeros)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    entradaConjunto.Numeros.Add(itemNumeroCalculo.CopiarObjeto(null, null, entrada, true));

                    if (mostrarLog)
                    {
                        if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemNumeroCalculo))
                        {
                            log.Add("Se obtiene el número '" + itemNumeroCalculo.Nombre + "' , su valor es: " + itemNumeroCalculo.Numero.ToString() + itemNumeroCalculo.ObtenerTextosInformacion_Cadena() + ".");
                            InformeResultados.TextoLog.Add("Se obtiene el número '" + itemNumeroCalculo.Nombre + "' , su valor es: " + itemNumeroCalculo.Numero.ToString() + itemNumeroCalculo.ObtenerTextosInformacion_Cadena() + ".");
                        }
                    }
                }

                item.CantidadSubElementosProcesados++;
            }

            item.Estado = EstadoEjecucion.Procesado;

            if(ModoToolTips && TooltipsCalculo != null)
                EntradasProcesadas.Add(TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID));
            else
                EntradasProcesadas.Add(item.ElementoDiseñoRelacionado);

            CantidadElementosEjecucionProcesados++;
            ActualizarPorcentajeAvance();

            if (Pausada) Pausar();
            if (Detenida) return;
        }
        private void TraspasarDatosEntradaEspecifica(ElementoEjecucionCalculo item, ElementoEjecucionCalculo itemOriginal)
        {
            item.Estado = EstadoEjecucion.Iniciado;
            ElementoEntradaEjecucion entrada = (ElementoEntradaEjecucion)item;

            switch (entrada.TipoEntrada)
            {
                case TipoEntrada.ConjuntoNumeros:
                    ElementoEntradaEjecucion numeros = entrada;
                    ElementoEntradaEjecucion numerosOriginal = (ElementoEntradaEjecucion)itemOriginal;

                    switch (numeros.TipoEntradaConjuntoNumeros)
                    {
                        case TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo:
                        case TipoOpcionConjuntoNumerosEntrada.SeDigita:
                        case TipoOpcionConjuntoNumerosEntrada.SeObtiene:

                            numeros.Numeros.Clear();

                            numeros.Numeros.AddRange(numerosOriginal.Numeros);
                            numeros.Nombre = itemOriginal.Nombre;

                            numeros.Textos.Clear();

                            numeros.Textos.AddRange(GenerarTextosInformacion(itemOriginal.Textos));

                            item.Clasificadores_Cantidades.Clear();
                            foreach (var itemClasificador in numerosOriginal.Numeros.SelectMany(i => i.Clasificadores_SeleccionarOrdenar))
                            {
                                if (!item.Clasificadores_Cantidades.Contains(itemClasificador))
                                    item.Clasificadores_Cantidades.Add(itemClasificador);
                            }

                            numeros.CantidadSubElementosProcesados++;
                            break;
                    }

                    break;

                    //case TipoEntrada.Calculo:

                    //    entrada.ValorNumerico = itemOriginal.ValorNumerico;

                    //    entrada.CantidadSubElementosProcesados++;

                    //    break;
            }

            item.Estado = EstadoEjecucion.Procesado;

            if(ModoToolTips && TooltipsCalculo != null)
                EntradasProcesadas.Add(TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID));
            else
                EntradasProcesadas.Add(item.ElementoDiseñoRelacionado);

            CantidadElementosEjecucionProcesados++;
            ActualizarPorcentajeAvance();

            if (Pausada) Pausar();
            if (Detenida) return;
        }

        private void ObtenerDatosEntradas(EtapaEjecucion etapa, ElementoCalculoEjecucion itemCalculo, bool mostrarLog)
        {
            foreach (var item in etapa.Elementos)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                if (item.Tipo == TipoElementoEjecucion.Entrada)
                {
                    ProcesarItemEntrada(item, itemCalculo, mostrarLog);
                }
            }
        }

        private void ProcesarItemEntrada(ElementoEjecucionCalculo item, ElementoCalculoEjecucion itemCalculo, bool mostrarLog, bool modoRepeticion = false)
        {
            //var comEntrada = new ComStaWorker();

            //comEntrada.Run(() =>
            //{
            try
                {
                    ElementoEntradaEjecucion entrada = (ElementoEntradaEjecucion)item;

                    if (((!((ElementoEntradaEjecucion)item).EntradaNoSeleccionada) &&
                        ((itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo &&
                        item.ElementoDiseñoRelacionado.EntradaRelacionada.EjecutarDeFormaGeneral) ||
                        !itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)) ||
                        modoRepeticion)
                    {
                        DiseñoOperacion elementoAsociado = null;
                        ElementoEjecucionCalculo salidaCalculoAnterior = null;

                        if (item.ElementoDiseñoCalculoRelacionado == null &&
                            !itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo &&
                            item.ElementoDiseñoRelacionado.EntradaRelacionada.EjecutarDeFormaGeneral)
                        {
                            var elementoCalculo = this.Calculo.ObtenerCalculoEntradas();
                            var elementoCalculoAnterior = elementoCalculo.ElementosOperaciones.
                                FirstOrDefault(i => i.EntradaRelacionada.ID == item.ElementoDiseñoRelacionado.EntradaRelacionada.ID);

                            ElementoCalculoEjecucion calculoAnteriorEjecucion = null;
                            foreach (var itemEtapa in etapasCalculo)
                            {
                                if (calculoAnteriorEjecucion == null)
                                {
                                    calculoAnteriorEjecucion = (from ElementoCalculoEjecucion C in itemEtapa.Elementos where C.ElementoDiseñoCalculoRelacionado == elementoCalculo select C).FirstOrDefault();
                                    if (calculoAnteriorEjecucion != null)
                                    {
                                        foreach (var itemEtapa_BuscarDesdeCalculo in calculoAnteriorEjecucion.etapasBuscarEntradas)
                                        {
                                            salidaCalculoAnterior = (from S in itemEtapa_BuscarDesdeCalculo.Elementos where S.ElementoDiseñoRelacionado == elementoCalculoAnterior select S).FirstOrDefault();
                                            if (salidaCalculoAnterior != null)
                                                break;
                                        }
                                    }
                                }
                            }

                            if (salidaCalculoAnterior != null)
                            {
                                elementoAsociado = elementoCalculoAnterior;
                            }
                        }
                        else
                        {
                            if (item.ElementoDiseñoCalculoRelacionado != null)
                            {
                                ElementoCalculoEjecucion calculoAnteriorEjecucion = null;
                                foreach (var itemEtapa in etapasCalculo)
                                {
                                    if (calculoAnteriorEjecucion == null)
                                    {
                                        calculoAnteriorEjecucion = (from ElementoCalculoEjecucion C in itemEtapa.Elementos where C.ElementoDiseñoCalculoRelacionado == item.ElementoDiseñoCalculoRelacionado select C).FirstOrDefault();
                                        if (calculoAnteriorEjecucion != null)
                                        {
                                            foreach (var itemEtapa_BuscarDesdeCalculo in calculoAnteriorEjecucion.etapasBuscarEntradas)
                                            {
                                                salidaCalculoAnterior = (from S in itemEtapa_BuscarDesdeCalculo.Elementos where S.ElementoDiseñoRelacionado == item.ElementoDiseñoRelacionado.EntradaRelacionada.ElementoSalidaCalculoAnterior select S).FirstOrDefault();
                                                if (salidaCalculoAnterior != null)
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (salidaCalculoAnterior != null)
                            {
                                elementoAsociado = item.ElementoDiseñoRelacionado.EntradaRelacionada.ElementoSalidaCalculoAnterior;
                            }
                        }

                        if ((!ModoToolTips ||
                            ((ModoToolTips && ModoEjecucionExterna && this.TooltipsCalculo == null) ||
                            (ModoToolTips && this.TooltipsCalculo != null &&
                            this.TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID) != null &&
                            this.TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID).EntradaRelacionada != null &&
                            ((this.TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID).Actualizar_ToolTips) ||
                            (this.TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID).EntradaRelacionada.ConCambios_ToolTips) ||
                            (!(this.TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID).EntradaRelacionada.ConCambios_ToolTips) &&
                            salidaCalculoAnterior != null & elementoAsociado != null) ||
                            (this.TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID).EntradaRelacionada.EjecutarDeFormaGeneral) ||
                            this.TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID).EntradaRelacionada.TipoOpcionConjuntoNumeros ==
                            TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)))) ||
                            modoRepeticion)
                        {
                            if (item.Estado != EstadoEjecucion.Procesado)
                            {
                                if (AgrupadorCambio != item.ElementoDiseñoRelacionado.AgrupadorContenedor)
                                {
                                    AgrupadorActual = item.ElementoDiseñoRelacionado.AgrupadorContenedor;
                                    AgrupadorCambio = item.ElementoDiseñoRelacionado.AgrupadorContenedor;
                                }

                                if (!ModoToolTips || (ModoToolTips && this.TooltipsCalculo != null &&
                            !modoRepeticion) || (ModoToolTips && ModoEjecucionExterna && this.TooltipsCalculo == null))
                                {
                                    var elemento = this.TooltipsCalculo?.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID);

                                    if (!ModoToolTips || (ModoToolTips && ModoEjecucionExterna && elemento == null) || ((elemento != null && elemento.EntradaRelacionada != null && (elemento.EntradaRelacionada.ConCambios_ToolTips ||
                                        elemento.Actualizar_ToolTips)) || (salidaCalculoAnterior != null && elementoAsociado != null) ||
                                        (itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo && item.ElementoDiseñoRelacionado.EntradaRelacionada.EjecutarDeFormaGeneral) ||
                                        elemento.EntradaRelacionada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo))
                                    {

                                        if (salidaCalculoAnterior != null &
                                    elementoAsociado != null)
                                        {
                                            ObtenerDatosEntradaEspecificaCalculo(item, salidaCalculoAnterior, mostrarLog);

                                        }
                                        else if ((itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo && item.ElementoDiseñoRelacionado.EntradaRelacionada.EjecutarDeFormaGeneral) ||
                                            (!itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo && !item.ElementoDiseñoRelacionado.EntradaRelacionada.EjecutarDeFormaGeneral))
                                            ObtenerDatosEntradaEspecifica(item, mostrarLog, itemCalculo, modoRepeticion);

                                        if (item.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades != null)
                                        {
                                            if (item.GetType() == typeof(ElementoEntradaEjecucion))
                                            {
                                                int posicion = 1;
                                                foreach (var itemNumero in ((ElementoEntradaEjecucion)item).Numeros)
                                                {
                                                    if (Pausada) Pausar();
                                                    if (Detenida) return;

                                                    EstablecerNombreCantidad(itemNumero, item.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades, posicion, entrada);
                                                    posicion++;
                                                }
                                            }
                                        }

                                        if (item.GetType() == typeof(ElementoEntradaEjecucion))
                                        {
                                            foreach (var itemNumero in ((ElementoEntradaEjecucion)item).Numeros)
                                            {
                                                if (Pausada) Pausar();
                                                if (Detenida) return;

                                                RealizarOperacionesCadenasTexto(itemNumero.Textos, item.ElementoDiseñoRelacionado.ConfigOperaciones_CadenasTexto);
                                                QuitarTextosInformacion_Repetidos(itemNumero.Textos, item.ElementoDiseñoRelacionado.QuitarTextosInformacion_Repetidos);
                                            }
                                        }

                                        if (item.GetType() == typeof(ElementoEntradaEjecucion))
                                            OrdenarCantidades_Operacion_Despues((ElementoEntradaEjecucion)item, item.ElementoDiseñoRelacionado.OrdenarNumerosDespuesEjecucion, item.ElementoDiseñoRelacionado.OrdenarNumeros_DespuesEjecucion);


                                        if (item.ContieneSalida_Ejecucion)
                                        {
                                            ElementoOperacionAritmeticaEjecucion operacion = null;

                                            operacion = new ElementoOperacionAritmeticaEjecucion();
                                            operacion.CantidadSubElementos = item.CantidadSubElementos;
                                            operacion.CantidadSubElementosProcesados = item.CantidadSubElementosProcesados;
                                            operacion.ElementoDiseñoCalculoRelacionado = item.ElementoDiseñoCalculoRelacionado;
                                            operacion.ElementoDiseñoRelacionado = item.ElementoDiseñoRelacionado;
                                            operacion.Estado = item.Estado;
                                            operacion.HashCode_ElementoDiseñoOperacionCalculo = item.HashCode_ElementoDiseñoOperacionCalculo;
                                            //operacion.ContieneSalida_Ejecucion = item.ContieneSalida_Ejecucion;
                                            operacion.Tipo = TipoElementoEjecucion.OperacionAritmetica;

                                            if (salidaCalculoAnterior != null)
                                            {
                                                if (salidaCalculoAnterior.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                                                    operacion.TipoOperacion = ((ElementoOperacionAritmeticaEjecucion)salidaCalculoAnterior).TipoOperacion;
                                            }

                                            if (entrada.TipoEntrada == TipoEntrada.ConjuntoNumeros)
                                            {
                                                ElementoEntradaEjecucion entradaConjunto = (ElementoEntradaEjecucion)item;
                                                foreach (var itemNumero in entradaConjunto.Numeros)
                                                {
                                                    if (Pausada) Pausar();
                                                    if (Detenida) return;

                                                    EntidadNumero numero = new EntidadNumero();
                                                    numero.Numero = itemNumero.Numero;
                                                    numero.Nombre = itemNumero.Nombre;
                                                    numero.Textos.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                                                    numero.Clasificadores_SeleccionarOrdenar.AddRange(itemNumero.Clasificadores_SeleccionarOrdenar);

                                                    foreach (var itemClasificador in numero.Clasificadores_SeleccionarOrdenar)
                                                    {
                                                        if (!operacion.Clasificadores_Cantidades.Contains(itemClasificador))
                                                            operacion.Clasificadores_Cantidades.Add(itemClasificador);
                                                    }

                                                    operacion.Numeros.Add(numero);
                                                }
                                            }

                                            if (!modoRepeticion)
                                                Salidas.Add(operacion);
                                        }
                                    }
                                }
                            }

                        }

                        if (item.GetType() == typeof(ElementoEntradaEjecucion))
                        {
                            if (!item.Clasificadores_Cantidades.Any())
                            {
                                ((ElementoEntradaEjecucion)item).AgregarClasificadoresGenericos();
                            }
                        }

                        if (ModoToolTips && this.TooltipsCalculo != null &&
                            !modoRepeticion)
                        {
                            var elemento = this.TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID);

                            if ((elemento != null && elemento.EntradaRelacionada != null && (elemento.EntradaRelacionada.ConCambios_ToolTips ||
                                elemento.Actualizar_ToolTips)) || (salidaCalculoAnterior != null && elementoAsociado != null) ||
                                (elemento != null && elemento.EntradaRelacionada != null &&
                                elemento.EntradaRelacionada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)) //|| elemento.EntradaRelacionada.EjecutarDeFormaGeneral || esEntradaDesdeOtroCalculo_EntradasGenerales))
                            {

                                EstablecerToolTipEntrada(item, elementoAsociado);
                                EstablecerToolTip_EntradaNoSeleccionada(item, true);
                                item.ToolTipMostrado = true;
                            }
                            else
                            {

                                item.Clasificadores_Cantidades.Clear();

                                ObtenerDatosToolTipEntrada(item, elementoAsociado);

                                if (item.GetType() == typeof(ElementoEntradaEjecucion))
                                {
                                    if (!item.Clasificadores_Cantidades.Any())
                                    {
                                        ((ElementoEntradaEjecucion)item).AgregarClasificadoresGenericos_DesdeCantidades();
                                    }
                                }

                                EstablecerToolTip_EntradaNoSeleccionada(item, true);
                                item.ToolTipMostrado = true;
                            }
                        }

                    }
                    else
                    {
                        {

                            item.Estado = EstadoEjecucion.Procesado;

                            if (ModoToolTips && TooltipsCalculo != null)
                                EntradasProcesadas.Add(TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID));
                            else
                                EntradasProcesadas.Add(item.ElementoDiseñoRelacionado);
                        }

                        if (ModoToolTips && this.TooltipsCalculo != null && !modoRepeticion)
                        {

                            {
                                EstablecerToolTip_EntradaNoSeleccionada(item, false);
                                item.ToolTipMostrado = true;
                            }
                        }

                        CantidadElementosEjecucionProcesados++;
                        ActualizarPorcentajeAvance();

                        if (Pausada) Pausar();
                        if (Detenida) return;
                    }

                    if (entrada.TipoEntrada == TipoEntrada.TextosInformacion)
                    {

                        ElementoConjuntoTextosEntradaEjecucion textos = (ElementoConjuntoTextosEntradaEjecucion)entrada;
                        AgregarTextosInformacionEntrada_Definicion(textos, itemCalculo);
                    AgregarTextosInformacionEntrada_DefinicionListasCadenas(textos, itemCalculo);

                        if (ModoToolTips)
                        {
                            if (itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)
                            {
                                item.ElementoDiseñoRelacionado.EntradaRelacionada.EntradaProcesada = entrada.CopiarObjeto();

                                Calculo.EstablecerEntradaProcesada_TextosInformacion(item.ElementoDiseñoRelacionado.EntradaRelacionada,
                                    item.ElementoDiseñoRelacionado.EntradaRelacionada.ID);
                            }
                        }
                        else
                        {
                            if (itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)
                                item.ElementoDiseñoRelacionado.EntradaRelacionada.EntradaProcesada = entrada.CopiarObjeto();
                        }

                    }
                }
                catch (COMException)
                {

                }
            //});
        }

        public void ObtenerDatosToolTipEntrada_ObtenerEjecucionNormal(ElementoOperacionAritmeticaEjecucion item, 
            bool DesdeOffice_EnvioManuales)
        {
            if (DesdeOffice_EnvioManuales &&
                !item.ElementoDiseñoRelacionado.DesdeOffice_EnvioManual)
            {
                item.Numeros.AddRange(item.ElementoDiseñoRelacionado.Cantidades_Digitacion_Tooltip.Select(i => i.CopiarObjeto()));
                item.AgregarClasificadoresGenericos();

                item.ElementoDiseñoRelacionado.DesdeOffice_EnvioManual = DesdeOffice_EnvioManuales;
            }
        }

        public double ObtenerEntrada_EnvioDesdeExcel(Entrada_Desde_Excel entrada, ref bool encontrado)
        {
            double numero = 0;

            EnviarAExcel_UsoEntrada(entrada);
            string rutaArchivo = App.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Excel + "\\" + GUID_EjecucionCalculo + "-" + entrada.Nombre + ".dat";

            while (true)
            {
                if (Pausada) Pausar();

                if (File.Exists(rutaArchivo))
                {
                    numero = LeerEnvioEntradaEjecucion_DesdeExcel(rutaArchivo);
                    encontrado = true;
                    File.Delete(rutaArchivo);//App.ArchivosTemporalesAEliminar.Add(rutaArchivo);
                    break;
                }
                try { Thread.Sleep(1000); } catch (Exception) { };

                if (Pausada) Pausar();
                if (Detenida) return 0;
            }

            return numero;
        }

        public List<PlanMath_Excel.NumeroObtenido> ObtenerEntradaConjuntoNumeros_EnvioDesdeExcel(Entrada_Desde_Excel entrada, ref bool encontrado)
        {
            List<PlanMath_Excel.NumeroObtenido> numeros = null;

            EnviarAExcel_UsoEntrada(entrada);
            string rutaArchivo = App.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Excel + "\\" + GUID_EjecucionCalculo + "-" + entrada.Nombre + ".dat";

            while (true)
            {
                if (Pausada) Pausar();

                if (File.Exists(rutaArchivo))
                {
                    numeros = LeerEnvioEntrada_ConjuntoNumeros_Ejecucion_DesdeExcel(rutaArchivo);
                    encontrado = true;
                    File.Delete(rutaArchivo); //App.ArchivosTemporalesAEliminar.Add(rutaArchivo);
                    break;
                }
                try { Thread.Sleep(1000); } catch (Exception) { };

                if (Pausada) Pausar();
                if (Detenida) return new List<NumeroObtenido>();
            }

            return numeros;
        }

        public List<string> ObtenerEntradaTextoBusqueda_EnvioDesdeWord(Entrada_Desde_Word entrada, ref bool encontrado)
        {
            List<string> textos = new List<string>();

            EnviarAWord_UsoEntrada(entrada);
            string rutaArchivo = App.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Word + "\\" + GUID_EjecucionCalculo + "-" + entrada.Nombre + ".dat";

            while (true)
            {
                if (Pausada) Pausar();

                if (File.Exists(rutaArchivo))
                {
                    textos = LeerEnvioEntrada_TextoBusqueda_Ejecucion_DesdeWord(rutaArchivo);
                    encontrado = true;
                    File.Delete(rutaArchivo); //App.ArchivosTemporalesAEliminar.Add(rutaArchivo);
                    break;
                }
                try { Thread.Sleep(1000); } catch (Exception) { };

                if (Pausada) Pausar();
                if (Detenida) return new List<string>();
            }

            return textos;
        }

        public List<List<string>> ObtenerEntradaConjuntoTextosInformacion_EnvioDesdeExcel(Entrada_Desde_Excel entrada, ref bool encontrado)
        {
            List<List<string>> textos = null;

            EnviarAExcel_UsoEntrada(entrada);
            string rutaArchivo = App.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Excel + "\\" + GUID_EjecucionCalculo + "-" + entrada.Nombre + ".dat";

            while (true)
            {
                if (Pausada) Pausar();

                if (File.Exists(rutaArchivo))
                {
                    textos = LeerEnvioEntrada_ConjuntoTextosInformacion_Ejecucion_DesdeExcel(rutaArchivo);
                    encontrado = true;
                    File.Delete(rutaArchivo); //App.ArchivosTemporalesAEliminar.Add(rutaArchivo);
                    break;
                }
                try { Thread.Sleep(1000); } catch (Exception) { };

                if (Pausada) Pausar();
                if (Detenida) return new List<List<string>>();
            }

            return textos;
        }

        public string EnviarAExcel_UsoEntrada(Entrada_Desde_Excel entrada)
        {
            string rutaArchivo = App.RutaArchivo_PlanMath_para_Excel + "\\Ejec-" + Guid.NewGuid().ToString().Split('-')[0] + ".dat";
            XmlWriter escribirArchivo = XmlWriter.Create(rutaArchivo);
            escribirArchivo.WriteStartElement("EnvioEntrada");
            escribirArchivo.WriteElementString("ID_EjecucionCalculo", GUID_EjecucionCalculo);
            //escribirArchivo.WriteEndElement();
            escribirArchivo.WriteElementString("EjecucionCalculo", "Ejecución del " + DateTime.Now.ToShortDateString() + " a las " + DateTime.Now.ToShortTimeString());
            //escribirArchivo.WriteEndElement();
            escribirArchivo.WriteStartElement("DefinicionEntrada");
            DataContractSerializer objeto = new DataContractSerializer(typeof(Entrada_Desde_Excel), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(escribirArchivo, entrada);
            escribirArchivo.WriteEndElement();
            escribirArchivo.WriteEndElement();
            escribirArchivo.Close();
            return rutaArchivo;
        }

        public double LeerEnvioEntradaEjecucion_DesdeExcel(string rutaArchivo)
        {
            XmlReader leerArchivo = XmlReader.Create(rutaArchivo);
            leerArchivo.ReadStartElement();
            //leerArchivo.ReadStartElement();

            double numero = leerArchivo.ReadElementContentAsDouble(); //0;
            //string strNumero = leerArchivo.ReadElementContentAsDouble();
            //double.TryParse(strNumero, out numero);

            leerArchivo.Close();

            return numero;
        }

        public List<PlanMath_Excel.NumeroObtenido> LeerEnvioEntrada_ConjuntoNumeros_Ejecucion_DesdeExcel(string rutaArchivo)
        {
            XmlReader leerArchivo = XmlReader.Create(rutaArchivo);
            leerArchivo.ReadStartElement();
            leerArchivo.ReadStartElement();

            DataContractSerializer objeto = new DataContractSerializer(typeof(List<PlanMath_Excel.NumeroObtenido>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            List<PlanMath_Excel.NumeroObtenido> numeros = (List<PlanMath_Excel.NumeroObtenido>)objeto.ReadObject(leerArchivo);
            leerArchivo.Close();

            return numeros;
        }

        public List<List<string>> LeerEnvioEntrada_ConjuntoTextosInformacion_Ejecucion_DesdeExcel(string rutaArchivo)
        {
            XmlReader leerArchivo = XmlReader.Create(rutaArchivo);
            leerArchivo.ReadStartElement();
            leerArchivo.ReadStartElement();

            DataContractSerializer objeto = new DataContractSerializer(typeof(List<List<string>>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            List<List<string>> textos = (List<List<string>>)objeto.ReadObject(leerArchivo);
            leerArchivo.Close();

            return textos;
        }

        public List<string> LeerEnvioEntrada_TextoBusqueda_Ejecucion_DesdeWord(string rutaArchivo)
        {
            XmlReader leerArchivo = XmlReader.Create(rutaArchivo);
            leerArchivo.ReadStartElement();
            leerArchivo.ReadStartElement();

            DataContractSerializer objeto = new DataContractSerializer(typeof(List<string>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            List<string> textos = (List<string>)objeto.ReadObject(leerArchivo);
            leerArchivo.Close();

            return textos;
        }

        public List<string> ObtenerEntradaConjuntoTextos_EnvioDesdeWord(Entrada_Desde_Word entrada)
        {
            List<string> textos = null;

            EnviarAWord_UsoEntrada(entrada);
            string rutaArchivo = App.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Word + "\\" + GUID_EjecucionCalculo + "-" + entrada.Nombre + ".dat";

            while (true)
            {
                if (Pausada) Pausar();

                if (File.Exists(rutaArchivo))
                {
                    textos = LeerEnvioEntrada_ConjuntoTextos_Ejecucion_DesdeWord(rutaArchivo);
                    File.Delete(rutaArchivo); //App.ArchivosTemporalesAEliminar.Add(rutaArchivo);
                    break;
                }
                try { Thread.Sleep(1000); } catch (Exception) { };

                if (Pausada) Pausar();
                if (Detenida) return new List<string>();
            }

            return textos;
        }

        public string EnviarAWord_UsoEntrada(Entrada_Desde_Word entrada)
        {
            string rutaArchivo = App.RutaArchivo_PlanMath_para_Word + "\\Ejec-" + Guid.NewGuid().ToString().Split('-')[0] + ".dat";
            XmlWriter escribirArchivo = XmlWriter.Create(rutaArchivo);
            escribirArchivo.WriteStartElement("EnvioEntrada");
            escribirArchivo.WriteElementString("ID_EjecucionCalculo", GUID_EjecucionCalculo);
            //escribirArchivo.WriteEndElement();
            escribirArchivo.WriteElementString("EjecucionCalculo", "Ejecución del " + DateTime.Now.ToShortDateString() + " a las " + DateTime.Now.ToShortTimeString());
            //escribirArchivo.WriteEndElement();
            escribirArchivo.WriteStartElement("DefinicionEntrada");
            DataContractSerializer objeto = new DataContractSerializer(typeof(Entrada_Desde_Word), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(escribirArchivo, entrada);
            escribirArchivo.WriteEndElement();
            escribirArchivo.WriteEndElement();
            escribirArchivo.Close();
            return rutaArchivo;
        }

        public List<string> LeerEnvioEntrada_ConjuntoTextos_Ejecucion_DesdeWord(string rutaArchivo)
        {
            XmlReader leerArchivo = XmlReader.Create(rutaArchivo);
            leerArchivo.ReadStartElement();
            leerArchivo.ReadStartElement();

            DataContractSerializer objeto = new DataContractSerializer(typeof(List<string>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            List<string> numeros = (List<string>)objeto.ReadObject(leerArchivo);
            leerArchivo.Close();

            return numeros;
        }
    }
}