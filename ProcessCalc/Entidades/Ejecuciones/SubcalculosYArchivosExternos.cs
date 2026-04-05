using ProcessCalc.Controles.Ctrl_Entradas;
using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Xml;

namespace ProcessCalc.SubcalculosYArchivosExternos
{
    
}

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        [IgnoreDataMember]
        public MainWindow Ventana {  get; set; }
        public void ProcesarArchivoExterno(ElementoOperacionAritmeticaEjecucion operacion,
            ref string strMensajeLogResultados, ref string strMensajeLog, ElementoCalculoEjecucion itemCalculo,
            bool mostrarLog)
        {
            if (operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.EjecutarArchivo_MismaEjecucion)
            {
                if (mostrarLog)
                {
                    strMensajeLog += "Ejecutando en esta misma instancia de ejecución...\n";
                    strMensajeLogResultados += "Ejecutando en esta misma instancia de ejecución...\n";
                }

                var ejecucionArchivo = new ElementoArchivoExternoEjecucion();
                ejecucionArchivo.ConfiguracionSeleccionCarpeta = operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.ConfiguracionSeleccionCarpeta;
                ejecucionArchivo.ConfiguracionSeleccionArchivo = operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.ConfiguracionSeleccionArchivo;
                ejecucionArchivo.RutaArchivo = operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.RutaArchivoEntrada;
                ejecucionArchivo.ArchivoEntrada = operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo;

                List<string> RutasArchivos = new List<string>();
                HttpResponseMessage respuestaArchivoFTP = null;

                if (!ModoToolTips)
                {
                    string RutaArchivo_ = string.Empty;

                    if (operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.TipoArchivo == TipoArchivo.ServidorFTP)
                    {
                        RutaArchivo_ = ejecucionArchivo.ObtenerURLFTP_Ejecucion_Entrada(this, itemCalculo);

                        HttpClient archivoFTP = null;
                        NetworkCredential credenciales = null;

                        if (operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.CredencialesFTP != null)
                        {
                            if (operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.CredencialesFTP.UsuarioAnonimo)
                            {
                                credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.CredencialesFTP.NombreUsuario) &
                                    string.IsNullOrEmpty(operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.CredencialesFTP.Clave))
                                {
                                    credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.CredencialesFTP.NombreUsuario))
                                    {
                                        credenciales = new NetworkCredential("anonymous", RutaArchivo_.Split('/')[0]);
                                    }
                                    else
                                    {
                                        credenciales = new NetworkCredential(operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.CredencialesFTP.NombreUsuario,
                                            operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.CredencialesFTP.Clave);
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
                            try { Thread.Sleep(3000); } catch (Exception) { };
                            ConError = true;

                            if (mostrarLog)
                            {
                                log.Add("Error al obtener el archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                InformeResultados.TextoLog.Add("Error al obtener el archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                            }

                            Detener();
                            return;
                        }
                    }
                    else
                    if (operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.TipoArchivo == TipoArchivo.Internet)
                    {
                        RutaArchivo_ = ejecucionArchivo.RutaArchivo;
                        if (RutaArchivo_.Length >= 8 && !RutaArchivo_.Substring(0, 7).ToLower().Equals("http://") &&
                            !RutaArchivo_.Substring(0, 8).ToLower().Equals("https://"))
                            RutaArchivo_ = "http://" + RutaArchivo_;
                        else if (RutaArchivo_.Length >= 8 && !RutaArchivo_.Substring(0, 8).ToLower().Equals("https://") &&
                            !RutaArchivo_.Substring(0, 7).ToLower().Equals("http://"))
                            RutaArchivo_ = "https://" + RutaArchivo_;

                        HttpClient archivoFTP = new HttpClient();
                        //((HttpWebRequest)archivoFTP).Method = WebRequestMethods.Http.Get;

                        try
                        {
                            respuestaArchivoFTP = archivoFTP.GetAsync(RutaArchivo_).Result;
                        }
                        catch (Exception error)
                        {
                            try { Thread.Sleep(3000); } catch (Exception) { };
                            ConError = true;

                            if (mostrarLog)
                            {
                                log.Add("Error al obtener el archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                                InformeResultados.TextoLog.Add("Error al obtener el archivo '" + RutaArchivo_ + "' Error: " + error.Message + ".");
                            }

                            Detener();
                            return;
                        }
                    }
                    else
                        RutasArchivos = ejecucionArchivo.ObtenerRutaArchivoEjecucion_Operador(Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")), this, itemCalculo);
                }
                else
                {
                    RutasArchivos.Add(operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.RutaArchivoEntrada);
                }

                if (RutasArchivos != null)
                {
                    foreach (var RutaArchivo in RutasArchivos)
                    {
                        if (mostrarLog)
                        {
                            strMensajeLog += "Archivo: " + RutaArchivo + "\n";
                            strMensajeLogResultados += "Archivo: " + RutaArchivo + "\n";
                        }

                        bool existe = false;

                        if (operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                            operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.TipoArchivo == TipoArchivo.RedLocal)
                        {
                            existe = File.Exists(RutaArchivo);
                        }
                        else if (!ModoToolTips && operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.TipoArchivo == TipoArchivo.ServidorFTP)
                        {
                            if (respuestaArchivoFTP != null && respuestaArchivoFTP.StatusCode == HttpStatusCode.OK)
                                //if (respuestaArchivoFTP.ContentLength > 0)
                                existe = true;
                            else
                                existe = false;
                        }
                        else if (!ModoToolTips && operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.Archivo.TipoArchivo == TipoArchivo.Internet)
                        {
                            if (respuestaArchivoFTP != null && respuestaArchivoFTP.StatusCode == HttpStatusCode.OK)
                                existe = true;
                            else
                                existe = false;
                        }

                        if (existe)
                        {

                            if (mostrarLog)
                            {
                                strMensajeLog += "Las entradas del archivo tendrán las siguientes cantidades de " + operacion.ElementoDiseñoRelacionado.NombreCombo + "...\n";
                                strMensajeLogResultados += "Las entradas del archivo tendrán las siguientes cantidades de " + operacion.ElementoDiseñoRelacionado.NombreCombo + "...\n";
                            }

                            XmlReader guarda = XmlReader.Create(RutaArchivo);
                            DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                            Calculo calculo = (Calculo)objeto.ReadObject(guarda);
                            guarda.Close();

                            if(!ModoToolTips)
                                calculo.ProcesarIDs_Elementos(Ventana.VerificaArchivoAbierto_ConId(calculo.ID));
                            else
                            {
                                try
                                {
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(() =>
                                    {
                                        calculo.ProcesarIDs_Elementos(Ventana.VerificaArchivoAbierto_ConId(calculo.ID));
                                    });
                                    }
                                }
                                catch (Exception) { }
                            }

                                EjecucionCalculo ejecucionExternaArchivo = null;

                            if (operacion.ElementosOperacion.Any() && 
                                operacion.ElementoDiseñoRelacionado.EntradasOperandos_ArchivoExterno.Any())
                            {
                                foreach (var itemOperacion in operacion.ElementosOperacion)
                                {
                                    foreach (var configArchivoEntradaExterna in calculo.EntradasOperandos_ArchivoExterno)
                                    {
                                        if (configArchivoEntradaExterna != null)
                                        {
                                            //AsociacionEntradaOperando_ArchivoExterno configArchivoEntrada = null;

                                            var configuracionesArchivoEntrada = operacion.ElementoDiseñoRelacionado.EntradasOperandos_ArchivoExterno
                                                        .Where(i => i.IDEntrada_Externa == configArchivoEntradaExterna.Entrada.ID &&
                                                        i.Operacion == itemOperacion.ElementoDiseñoRelacionado).ToList();

                                            foreach (var configArchivoEntrada in configuracionesArchivoEntrada)
                                            {
                                                TipoTraspasoCantidades_ArchivoExterno TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.Ninguno;

                                                if (configArchivoEntrada != null)
                                                {
                                                    if (configArchivoEntrada.Configuracion.UsarConfiguracionOpuesta &&
                                                        configArchivoEntradaExterna.Configuracion.UsarConfiguracionOpuesta)
                                                    {
                                                        TipoTraspasoCantidades = configArchivoEntrada.Configuracion.TipoTraspasoCantidades;
                                                    }
                                                    else
                                                    {
                                                        if (configArchivoEntradaExterna.Configuracion.UsarConfiguracionOpuesta)
                                                        {
                                                            if (configArchivoEntradaExterna.Configuracion.Tipo == TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionArchivo)
                                                            {
                                                                TipoTraspasoCantidades = configArchivoEntradaExterna.Configuracion.TipoTraspasoCantidades;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (configArchivoEntrada.Configuracion.Tipo == TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionOperador)
                                                            {
                                                                TipoTraspasoCantidades = configArchivoEntrada.Configuracion.TipoTraspasoCantidades;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                    TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal;

                                                foreach (var itemCalculo_Archivo in calculo.Calculos)
                                                {
                                                    foreach (var operacionCalculo_Archivo in itemCalculo_Archivo.ElementosOperaciones
                                                        .Where(i => i.EntradaRelacionada != null &&
                                                        i.EntradaRelacionada.ID == configArchivoEntradaExterna.Entrada.ID))
                                                    {
                                                        configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config
                                                    .Add(new EjecucionExternaConfiguracion_Entrada());
                                                        configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault()
                                                            .IDElementoAsociado = operacionCalculo_Archivo.ID;

                                                        switch (TipoTraspasoCantidades)
                                                        {
                                                            case TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal:
                                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionNormal = true;
                                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionTraspaso = false;

                                                                strMensajeLog += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente.\n";
                                                                strMensajeLogResultados += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente.\n";

                                                                break;

                                                            case TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectado:
                                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionNormal = false;
                                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionTraspaso = true;
                                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().DatosEntrada = itemOperacion;
                                                                
                                                                strMensajeLog += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " tendrá la información de cantidades de " + itemOperacion.Nombre + "\n";
                                                                strMensajeLogResultados += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " tendrá la información de cantidades de " + itemOperacion.Nombre + "\n";

                                                                break;
                                                            case TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectadoEntradaOriginal:
                                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionNormal = true;
                                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionTraspaso = true;
                                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().DatosEntrada = itemOperacion;

                                                                strMensajeLog += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente y\n";
                                                                strMensajeLog += "tendrá la información de cantidades de " + itemOperacion.Nombre + "\n";

                                                                strMensajeLogResultados += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente y\n";
                                                                strMensajeLogResultados += "tendrá la información de cantidades de " + itemOperacion.Nombre + "\n";

                                                                break;
                                                        }                                                        
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var configArchivoEntradaExterna in calculo.EntradasOperandos_ArchivoExterno)
                                {
                                    if (configArchivoEntradaExterna != null)
                                    {
                                        TipoTraspasoCantidades_ArchivoExterno TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal;

                                        foreach (var itemCalculo_Archivo in calculo.Calculos)
                                        {
                                            foreach (var operacionCalculo_Archivo in itemCalculo_Archivo.ElementosOperaciones
                                                .Where(i => i.EntradaRelacionada != null &&
                                                i.EntradaRelacionada.ID == configArchivoEntradaExterna.Entrada.ID))
                                            {
                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config
                                            .Add(new EjecucionExternaConfiguracion_Entrada());
                                                configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault()
                                                    .IDElementoAsociado = operacionCalculo_Archivo.ID;

                                                switch (TipoTraspasoCantidades)
                                                {
                                                    case TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal:
                                                        configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionNormal = true;
                                                        configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionTraspaso = false;

                                                        strMensajeLog += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente.\n";
                                                        strMensajeLogResultados += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente.\n";

                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            
                            try
                            {
                                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                {
                                    Ventana.Dispatcher.Invoke(() =>
                                {
                                    if (operacion.ElementoDiseñoRelacionado.ConfigArchivoExterno.EjecutarArchivoSin_InfoVisual ||
                                    ModoToolTips)
                                    {
                                        ejecucionExternaArchivo = new EjecucionCalculo()
                                        {
                                            Calculo = calculo.ReplicarObjeto(),
                                            ModoEjecucionExterna = true,
                                            ModoToolTips = ModoToolTips,
                                            Ventana = this.Ventana
                                        };

                                        foreach (var itemCalculoEjecucion in ejecucionExternaArchivo.Calculo.Calculos)
                                        {
                                            foreach (var itemCalculoOriginal in calculo.Calculos)
                                            {
                                                foreach (var itemEntrada in itemCalculoEjecucion.ListaEntradas)
                                                {
                                                    var itemEntradaOriginal = itemCalculoOriginal.ListaEntradas.FirstOrDefault(i => i.ID == itemEntrada.ID);
                                                    if (itemEntradaOriginal != null)
                                                    {
                                                        itemEntrada.EjecucionesExternas_SubElementos_Config = itemEntradaOriginal.EjecucionesExternas_SubElementos_Config;

                                                        foreach (var itemElemento in itemCalculoEjecucion.ElementosOperaciones)
                                                        {
                                                            if (itemElemento.EntradaRelacionada != null &&
                                                            itemElemento.EntradaRelacionada.ID == itemEntradaOriginal.ID)
                                                            {
                                                                itemElemento.EntradaRelacionada.EjecucionesExternas_SubElementos_Config = itemEntrada.EjecucionesExternas_SubElementos_Config;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        ejecucionExternaArchivo.Iniciar();
                                    }
                                    else if(!Detenida)
                                    {
                                        ejecucionExternaArchivo = Ventana.EjecutarArchivoExterno(calculo, ModoToolTips);
                                    }
                                });
                                }
                            }
                            catch (Exception) { }

                            strMensajeLog += "Esperando los resultados de la ejecución...\n";
                            strMensajeLogResultados += "Esperando los resultados de la ejecución...\n";

                            while (ejecucionExternaArchivo != null && 
                                !ejecucionExternaArchivo.Finalizada)
                            {
                                if (Detenida)
                                {
                                    ejecucionExternaArchivo.Detener();
                                    return;
                                }
                            }

                            if (operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Any(i => i.Tipo != TipoElementoOperacion.Salida))
                            {
                                foreach (var itemOperacionPosterior in operacion.ElementoDiseñoRelacionado.ElementosPosteriores)
                                {
                                    var itemOperacion = ObtenerElementoEjecucion(itemOperacionPosterior);

                                    if (itemOperacion != null)
                                    {
                                        foreach (var elementoResultadoExterno in operacion.ElementoDiseñoRelacionado.ResultadosOperandos_ArchivoExterno
                                            .Where(i => i.Operacion == itemOperacion.ElementoDiseñoRelacionado))
                                        {
                                            var elementoResultado = ejecucionExternaArchivo.InformeResultados.Resultados.FirstOrDefault(i => i.ResultadoAsociado.ID == elementoResultadoExterno.IDResultado);

                                            if (elementoResultado != null)
                                            {
                                                if (elementoResultado.ValoresNumericos.Any())
                                                {
                                                    //operacion.ElementosOperacion.Clear();
                                                    var elementos = new List<EntidadNumero>();

                                                    elementos.AddRange(elementoResultado.ValoresNumericos.Select(i => new EntidadNumero()
                                                    {
                                                        Nombre = i.Nombre,
                                                        Numero = i.Numero,
                                                        Textos = i.Textos.ToList(),
                                                        Clasificadores_SeleccionarOrdenar = i.Clasificadores_SeleccionarOrdenar,
                                                    }));

                                                    foreach (var itemElemento in elementos)
                                                        itemElemento.ElementosSalidaOperacion_SeleccionarOrdenar.Add(itemOperacion);

                                                    operacion.Numeros.AddRange(elementos);

                                                    foreach (var itemElemento in elementos)
                                                        foreach (var itemClasificador in itemElemento.Clasificadores_SeleccionarOrdenar)
                                                        if (!operacion.Clasificadores_Cantidades.Contains(itemClasificador))
                                                                operacion.Clasificadores_Cantidades.Add(itemClasificador);
                                                }
                                                else
                                                {
                                                    operacion.Numeros.Add(new EntidadNumero()
                                                    {
                                                        Nombre = elementoResultado.Nombre,
                                                        Numero = elementoResultado.ValorNumerico,
                                                        Textos = elementoResultado.TextosInformacion.ToList()
                                                    });

                                                    operacion.Numeros.LastOrDefault().ElementosSalidaOperacion_SeleccionarOrdenar.Add(itemOperacion);
                                                    operacion.AgregarClasificadoresGenericos();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (ejecucionExternaArchivo != null)
                                {
                                    foreach (var elementoResultado in ejecucionExternaArchivo.InformeResultados.Resultados)
                                    {
                                        if (elementoResultado.ValoresNumericos.Any())
                                        {
                                            var elementos = new List<EntidadNumero>();

                                            elementos.AddRange(elementoResultado.ValoresNumericos.Select(i => new EntidadNumero()
                                            {
                                                Nombre = i.Nombre,
                                                Numero = i.Numero,
                                                Textos = i.Textos.ToList(),
                                                Clasificadores_SeleccionarOrdenar = i.Clasificadores_SeleccionarOrdenar,
                                            }));

                                            operacion.Numeros.AddRange(elementos);

                                            foreach (var itemElemento in elementos)
                                                foreach (var itemClasificador in itemElemento.Clasificadores_SeleccionarOrdenar)
                                                    if (!operacion.Clasificadores_Cantidades.Contains(itemClasificador))
                                                        operacion.Clasificadores_Cantidades.Add(itemClasificador);
                                        }
                                        else
                                        {
                                            operacion.Numeros.Add(new EntidadNumero()
                                            {
                                                Nombre = elementoResultado.Nombre,
                                                Numero = elementoResultado.ValorNumerico,
                                                Textos = elementoResultado.TextosInformacion.ToList()
                                            });

                                            operacion.AgregarClasificadoresGenericos();
                                        }
                                    }
                                }
                            }

                            strMensajeLogResultados += "El resultado de " + operacion.Nombre + " son las cantidades siguientes: \n";
                            strMensajeLog += "El resultado de " + operacion.Nombre + " son las cantidades siguientes: \n";

                            foreach (var item in operacion.Numeros)
                            {
                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(item))
                                {
                                    strMensajeLogResultados += item.Nombre + ", su valor es: " + item.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + item.ObtenerTextosInformacion_Cadena() + "\n";
                                    strMensajeLog += item.Nombre + ", su valor es: " + item.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + item.ObtenerTextosInformacion_Cadena() + "\n";
                                }
                            }
                        }
                        else
                        {
                            if (mostrarLog)
                            {
                                strMensajeLog += "No se encontró el archivo: " + RutaArchivo + "\n";
                                strMensajeLogResultados += "No se encontró el archivo: " + RutaArchivo + "\n";
                            }
                        }
                    }
                }
            }
            else
            {
                if (mostrarLog)
                {
                    strMensajeLog += "Ejecutando otra instancia de PlanMath con este archivo...\n";
                    strMensajeLogResultados += "Ejecutando otra instancia de PlanMath con este archivo...\n";
                }
            }
        }

        public void ProcesarSubCalculo(ElementoOperacionAritmeticaEjecucion operacion,
            ref string strMensajeLogResultados, ref string strMensajeLog, ElementoCalculoEjecucion itemCalculo,
            bool mostrarLog)
        {
            if (operacion.ElementoDiseñoRelacionado.ConfigSubCalculo.EjecutarSubCalculo_MismaEjecucion)
            {
                if (mostrarLog)
                {
                    strMensajeLog += "Ejecutando en esta misma instancia de ejecución...\n";
                    strMensajeLogResultados += "Ejecutando en esta misma instancia de ejecución...\n";
                }
                
                if (mostrarLog)
                {
                    strMensajeLog += "Las entradas del subcálculo tendrán las siguientes cantidades de " + operacion.ElementoDiseñoRelacionado.NombreCombo + "...\n";
                    strMensajeLogResultados += "Las entradas del subcálculo tendrán las siguientes cantidades de " + operacion.ElementoDiseñoRelacionado.NombreCombo + "...\n";
                }

                Calculo calculo = operacion.ElementoDiseñoRelacionado.ConfigSubCalculo.SubCalculo_Operacion;

                EjecucionCalculo ejecucionExternaArchivo = null;

                if (operacion.ElementosOperacion.Any() &&
                    operacion.ElementoDiseñoRelacionado.EntradasOperandos_SubCalculo.Any())
                {
                    foreach (var itemOperacion in operacion.ElementosOperacion)
                    {
                        foreach (var configArchivoEntradaExterna in calculo.EntradasOperandos_ArchivoExterno)
                        {
                            if (configArchivoEntradaExterna != null)
                            {

                                var configuracionesArchivoEntrada = operacion.ElementoDiseñoRelacionado.EntradasOperandos_SubCalculo
                                            .Where(i => i.IDEntrada_Externa == configArchivoEntradaExterna.Entrada.ID &&
                                            i.Operacion == itemOperacion.ElementoDiseñoRelacionado).ToList();

                                foreach (var configArchivoEntrada in configuracionesArchivoEntrada)
                                {
                                    TipoTraspasoCantidades_ArchivoExterno TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.Ninguno;

                                    if (configArchivoEntrada != null)
                                    {
                                        if (configArchivoEntrada.Configuracion.UsarConfiguracionOpuesta &&
                                            configArchivoEntradaExterna.Configuracion.UsarConfiguracionOpuesta)
                                        {
                                            TipoTraspasoCantidades = configArchivoEntrada.Configuracion.TipoTraspasoCantidades;
                                        }
                                        else
                                        {
                                            if (configArchivoEntradaExterna.Configuracion.UsarConfiguracionOpuesta)
                                            {
                                                if (configArchivoEntradaExterna.Configuracion.Tipo == TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionArchivo)
                                                {
                                                    TipoTraspasoCantidades = configArchivoEntradaExterna.Configuracion.TipoTraspasoCantidades;
                                                }
                                            }
                                            else
                                            {
                                                if (configArchivoEntrada.Configuracion.Tipo == TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionOperador)
                                                {
                                                    TipoTraspasoCantidades = configArchivoEntrada.Configuracion.TipoTraspasoCantidades;
                                                }
                                            }
                                        }
                                    }
                                    else
                                        TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal;

                                    foreach (var itemCalculo_Archivo in calculo.Calculos)
                                    {
                                        foreach (var operacionCalculo_Archivo in itemCalculo_Archivo.ElementosOperaciones
                                            .Where(i => i.EntradaRelacionada != null &&
                                            i.EntradaRelacionada.ID == configArchivoEntradaExterna.Entrada.ID))
                                        {
                                            configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config
                                        .Add(new EjecucionExternaConfiguracion_Entrada());
                                            configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault()
                                                .IDElementoAsociado = operacionCalculo_Archivo.ID;

                                            switch (TipoTraspasoCantidades)
                                            {
                                                case TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal:
                                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionNormal = true;
                                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionTraspaso = false;

                                                    strMensajeLog += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente.\n";
                                                    strMensajeLogResultados += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente.\n";

                                                    break;

                                                case TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectado:
                                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionNormal = false;
                                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionTraspaso = true;
                                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().DatosEntrada = itemOperacion;

                                                    strMensajeLog += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " tendrá la información de cantidades de " + itemOperacion.Nombre + "\n";
                                                    strMensajeLogResultados += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " tendrá la información de cantidades de " + itemOperacion.Nombre + "\n";

                                                    break;
                                                case TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectadoEntradaOriginal:
                                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionNormal = true;
                                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionTraspaso = true;
                                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().DatosEntrada = itemOperacion;

                                                    strMensajeLog += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente y\n";
                                                    strMensajeLog += "tendrá la información de cantidades de " + itemOperacion.Nombre + "\n";

                                                    strMensajeLogResultados += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente y\n";
                                                    strMensajeLogResultados += "tendrá la información de cantidades de " + itemOperacion.Nombre + "\n";

                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var configArchivoEntradaExterna in calculo.EntradasOperandos_ArchivoExterno)
                    {
                        if (configArchivoEntradaExterna != null)
                        {
                            TipoTraspasoCantidades_ArchivoExterno TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal;

                            foreach (var itemCalculo_Archivo in calculo.Calculos)
                            {
                                foreach (var operacionCalculo_Archivo in itemCalculo_Archivo.ElementosOperaciones
                                    .Where(i => i.EntradaRelacionada != null &&
                                    i.EntradaRelacionada.ID == configArchivoEntradaExterna.Entrada.ID))
                                {
                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config
                                .Add(new EjecucionExternaConfiguracion_Entrada());
                                    configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault()
                                        .IDElementoAsociado = operacionCalculo_Archivo.ID;

                                    switch (TipoTraspasoCantidades)
                                    {
                                        case TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal:
                                            configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionNormal = true;
                                            configArchivoEntradaExterna.Entrada.EjecucionesExternas_SubElementos_Config.LastOrDefault().EjecucionTraspaso = false;

                                            strMensajeLog += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente.\n";
                                            strMensajeLogResultados += "La variable o vector de entrada " + configArchivoEntradaExterna.Entrada.NombreCombo + " se obtendrá normalmente.\n";

                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                try
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(() =>
                    {
                        if (operacion.ElementoDiseñoRelacionado.ConfigSubCalculo.EjecutarSubCalculoSin_InfoVisual ||
                        ModoToolTips)
                        {
                            ejecucionExternaArchivo = new EjecucionCalculo()
                            {
                                Calculo = calculo.ReplicarObjeto(),
                                ModoEjecucionExterna = true,
                                ModoToolTips = ModoToolTips,
                                Ventana = this.Ventana
                            };

                            foreach (var itemCalculoEjecucion in ejecucionExternaArchivo.Calculo.Calculos)
                            {
                                foreach (var itemCalculoOriginal in calculo.Calculos)
                                {
                                    foreach (var itemEntrada in itemCalculoEjecucion.ListaEntradas)
                                    {
                                        var itemEntradaOriginal = itemCalculoOriginal.ListaEntradas.FirstOrDefault(i => i.ID == itemEntrada.ID);
                                        if (itemEntradaOriginal != null)
                                        {
                                            itemEntrada.EjecucionesExternas_SubElementos_Config = itemEntradaOriginal.EjecucionesExternas_SubElementos_Config;

                                            foreach (var itemElemento in itemCalculoEjecucion.ElementosOperaciones)
                                            {
                                                if (itemElemento.EntradaRelacionada != null &&
                                                itemElemento.EntradaRelacionada.ID == itemEntradaOriginal.ID)
                                                {
                                                    itemElemento.EntradaRelacionada.EjecucionesExternas_SubElementos_Config = itemEntrada.EjecucionesExternas_SubElementos_Config;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            ejecucionExternaArchivo.Iniciar();
                        }
                        else if(!Detenida)
                        {
                            ejecucionExternaArchivo = Ventana.EjecutarArchivoExterno(calculo, ModoToolTips);
                        }
                    });
                    }
                }
                catch (Exception) { }

                strMensajeLog += "Esperando los resultados de la ejecución...\n";
                strMensajeLogResultados += "Esperando los resultados de la ejecución...\n";

                while (ejecucionExternaArchivo != null &&
                      !ejecucionExternaArchivo.Finalizada)
                {
                    if (Detenida)
                    {
                        ejecucionExternaArchivo.Detener();
                        return;
                    }
                }

                if (operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Any(i => i.Tipo != TipoElementoOperacion.Salida))
                {
                    foreach (var itemOperacionPosterior in operacion.ElementoDiseñoRelacionado.ElementosPosteriores)
                    {
                        var itemOperacion = ObtenerElementoEjecucion(itemOperacionPosterior);

                        if (itemOperacion != null)
                        {
                            foreach (var elementoResultadoExterno in operacion.ElementoDiseñoRelacionado.ResultadosOperandos_SubCalculo
                                .Where(i => i.Operacion == itemOperacion.ElementoDiseñoRelacionado))
                            {
                                var elementoResultado = ejecucionExternaArchivo.InformeResultados.Resultados.FirstOrDefault(i => i.ResultadoAsociado.ID == elementoResultadoExterno.IDResultado);

                                if (elementoResultado != null)
                                {
                                    if (elementoResultado.ValoresNumericos.Any())
                                    {
                                        var elementos = new List<EntidadNumero>();

                                        elementos.AddRange(elementoResultado.ValoresNumericos.Select(i => new EntidadNumero()
                                        {
                                            Nombre = i.Nombre,
                                            Numero = i.Numero,
                                            Textos = i.Textos.ToList(),
                                            Clasificadores_SeleccionarOrdenar = i.Clasificadores_SeleccionarOrdenar,
                                        }));

                                        foreach (var itemElemento in elementos)
                                            itemElemento.ElementosSalidaOperacion_SeleccionarOrdenar.Add(itemOperacion);

                                        operacion.Numeros.AddRange(elementos);

                                        foreach (var itemElemento in elementos)
                                            foreach (var itemClasificador in itemElemento.Clasificadores_SeleccionarOrdenar)
                                                if (!operacion.Clasificadores_Cantidades.Contains(itemClasificador))
                                                    operacion.Clasificadores_Cantidades.Add(itemClasificador);
                                    }
                                    else
                                    {
                                        operacion.Numeros.Add(new EntidadNumero()
                                        {
                                            Nombre = elementoResultado.Nombre,
                                            Numero = elementoResultado.ValorNumerico,
                                            Textos = elementoResultado.TextosInformacion.ToList(),
                                        });

                                        operacion.Numeros.LastOrDefault().ElementosSalidaOperacion_SeleccionarOrdenar.Add(itemOperacion);
                                        operacion.AgregarClasificadoresGenericos();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var elementoResultado in ejecucionExternaArchivo.InformeResultados.Resultados)
                    {
                        if (elementoResultado.ValoresNumericos.Any())
                        {
                            var elementos = new List<EntidadNumero>();

                            elementos.AddRange(elementoResultado.ValoresNumericos.Select(i => new EntidadNumero()
                            {
                                Nombre = i.Nombre,
                                Numero = i.Numero,
                                Textos = i.Textos.ToList(),
                                Clasificadores_SeleccionarOrdenar = i.Clasificadores_SeleccionarOrdenar,
                            }));

                            operacion.Numeros.AddRange(elementos);

                            foreach (var itemElemento in elementos)
                                foreach (var itemClasificador in itemElemento.Clasificadores_SeleccionarOrdenar)
                                    if (!operacion.Clasificadores_Cantidades.Contains(itemClasificador))
                                        operacion.Clasificadores_Cantidades.Add(itemClasificador);
                        }
                        else
                        {
                            operacion.Numeros.Add(new EntidadNumero()
                            {
                                Nombre = elementoResultado.Nombre,
                                Numero = elementoResultado.ValorNumerico,
                                Textos = elementoResultado.TextosInformacion.ToList()
                            });

                            operacion.AgregarClasificadoresGenericos();
                        }
                    }
                }

                strMensajeLogResultados += "El resultado de " + operacion.Nombre + " son las cantidades siguientes: \n";
                strMensajeLog += "El resultado de " + operacion.Nombre + " son las cantidades siguientes: \n";

                foreach (var item in operacion.Numeros)
                {
                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(item))
                    {
                        strMensajeLogResultados += item.Nombre + ", su valor es: " + item.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + item.ObtenerTextosInformacion_Cadena() + "\n";
                        strMensajeLog += item.Nombre + ", su valor es: " + item.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + item.ObtenerTextosInformacion_Cadena() + "\n";
                    }
                }
                
                
                
            }
            else
            {
                if (mostrarLog)
                {
                    strMensajeLog += "Ejecutando otra instancia de PlanMath con este subcálculo...\n";
                    strMensajeLogResultados += "Ejecutando otra instancia de PlanMath con este subcálculo...\n";
                }
            }
        }
        
    }
}