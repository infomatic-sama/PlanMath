using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Office.Tools.Excel;
using Microsoft.Win32;
using PlanMath_para_Excel;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Threading;
using System.Xml;
using static PlanMath_para_Excel.PlanMath_Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProcessCalc
{
    /// <summary>
    /// Lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HistorialArchivos ArchivosRecientes { get; set; }
        public static HistorialArchivos ArchivosAbiertos { get; set; }
        public static HistorialEjecuciones EjecucionesRecientes { get; set; }
        public static InformacionVentana InformacionVentana { get; set; }

        public const string Nombre_Archivo_ArchivosRecientes = "ArchivosRecientes.xml";
        public const string Nombre_Aplicacion = "PlanMath";
        public static string RutaArchivo_ArchivoRecientes = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Archivo_ArchivosRecientes;

        public const string Nombre_Archivo_EjecucionesRecientes = "EjecucionesRecientes.xml";
        public static string RutaArchivo_EjecucionesRecientes = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Archivo_EjecucionesRecientes;

        public const string Nombre_Archivo_InformacionVentana = "Ventana.xml";
        public static string RutaArchivo_InformacionVentana = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Archivo_InformacionVentana;
        public const int PosicionXPredeterminada = 50;
        public const int PosicionYPredeterminada = 50;
        
        public const string Nombre_Aplicacion_PlanMath_para_Excel = "PlanMath_para_Excel";
        public static string RutaArchivo_PlanMath_para_Excel = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Aplicacion_PlanMath_para_Excel;
        public static string RutaCarpeta_PlanMath_EnvioEntradas_Desde_Excel = RutaArchivo_PlanMath_para_Excel + "\\Entradas_Enviadas";
        public static string RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Excel = RutaArchivo_PlanMath_para_Excel + "\\Entradas_Enviadas_Ejecucion";

        public const string Nombre_Aplicacion_PlanMath_para_Word = "PlanMath_para_Word";
        public static string RutaArchivo_PlanMath_para_Word = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Aplicacion_PlanMath_para_Word;
        public static string RutaCarpeta_PlanMath_EnvioEntradas_Desde_Word = RutaArchivo_PlanMath_para_Word + "\\Entradas_Enviadas";
        public static string RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Word = RutaArchivo_PlanMath_para_Word + "\\Entradas_Enviadas_Ejecucion";

        public static string RutaArchivos_Temporales = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\Temporales";

        public const string Nombre_Archivo_CantidadElementosObtenidos_Entradas_UltimasEjecuciones = "CantidadesElementosObtenidos_Entradas_UltimasEjecuciones.xml";
        public static string RutaArchivo_CantidadElementosObtenidos_Entradas_UltimasEjecuciones = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Archivo_CantidadElementosObtenidos_Entradas_UltimasEjecuciones;

        public const string Nombre_Archivo_CantidadesDigitadas = "CantidadesDigitadas.xml";
        public static string RutaArchivo_CantidadesDigitadas = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Archivo_CantidadesDigitadas;

        public const string Nombre_Archivo_TextosDigitados = "TextosDigitados.xml";
        public static string RutaArchivo_TextosDigitados = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Archivo_TextosDigitados;

        public const string Nombre_Archivo_ArchivosAbiertos = "ArchivosAbiertos.xml";
        public static string RutaArchivo_ArchivoAbiertos = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\" + Nombre_Archivo_ArchivosAbiertos;

        public static List<string> ArchivosTemporalesAEliminar = new List<string>();
        public static Thread HiloCierre;
        public static List<MainWindow> VentanasAbiertas = new List<MainWindow>();

        public static TelemetryClient ClienteMetricasUso { get; private set; }

        public void Iniciar(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Application.Current.Shutdown();
            };

#if !DEBUG
            CrearClienteMetricasUso();            
            ClienteMetricasUso.TrackEvent("AplicacionEjecutableIniciado");
#endif
            HiloCierre = new Thread(CerrarAplicacion);

            CrearDirectorioDatos();

            CargarListaArchivosRecientes();
            CargarListaArchivosAbiertos();
            CargarListaEjecucionesRecientes();
            GuardarArchivosCantidadesDigitadas();
            GuardarArchivosTextosDigitados();
            QuitarRegistrosArchivosAbiertos_YaCerrados();

            string[] argumentos = Environment.GetCommandLineArgs().Skip(1).ToArray();
            List<string> archivos = new List<string>();
            OpcionArgumentos opciones = OpcionArgumentos.Ninguna;

            string ID_ListaCalculos = string.Empty;
            string archivoPlanMath = string.Empty;
            int indiceArgumento = -1;

            foreach (var argumento in argumentos)
            {
                indiceArgumento++;
                if (File.Exists(argumento))
                {
                    archivos.Add(argumento);
                }
                else
                {
                    switch (argumento)
                    {
                        case "ListarCalculos":
                            opciones = OpcionArgumentos.ListarCalculos;
                            break;
                    }
                    break;
                }
            }

            switch (opciones)
            {
                case OpcionArgumentos.ListarCalculos:
                    for(int indice = indiceArgumento + 1; indice <= argumentos.Length - 1; indice++)
                    {
                        if (string.IsNullOrEmpty(archivoPlanMath))
                        {
                            if(File.Exists(argumentos[indice]))
                                archivoPlanMath = argumentos[indice];
                        }
                        else if(string.IsNullOrEmpty(ID_ListaCalculos))
                        {
                            ID_ListaCalculos = argumentos[indice];
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(archivoPlanMath) &
                        !string.IsNullOrEmpty(ID_ListaCalculos))
                    {
                        EnviarListaCalculos(archivoPlanMath, ID_ListaCalculos);
                    }

                    Application.Current.Shutdown();
                        
                    break;
                    
            }

            if(opciones == OpcionArgumentos.Ninguna)
                AgregarVentana(archivos.ToArray());
        }

        public void CrearClienteMetricasUso()
        {
            var config = TelemetryConfiguration.CreateDefault();
            config.ConnectionString = "InstrumentationKey=ae139492-4e9c-42a8-963e-ecf4a03c7106;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/;ApplicationId=26129b0e-30ee-4aad-abe9-aa4ba2bf26c8";
        
            ClienteMetricasUso = new TelemetryClient(config);
        }

        public void CerrarAplicacion()
        {
            while(VentanasAbiertas.Any())
            {
                   
            }

            if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
            {
                try
                {
                    Dispatcher.Invoke(() =>
                {
                    ClienteMetricasUso?.Flush();

                    if (App.Current != null)
                        App.Current.Shutdown();
                });
                }
                catch (Exception) { }
            }
        }

        private void CrearDirectorioDatos()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion);

            if (!Directory.Exists(RutaArchivo_PlanMath_para_Excel))
                Directory.CreateDirectory(RutaArchivo_PlanMath_para_Excel);

            if (!Directory.Exists(RutaCarpeta_PlanMath_EnvioEntradas_Desde_Excel))
                Directory.CreateDirectory(RutaCarpeta_PlanMath_EnvioEntradas_Desde_Excel);

            if (!Directory.Exists(RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Excel))
                Directory.CreateDirectory(RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion_Desde_Excel);

            if (!Directory.Exists(RutaArchivos_Temporales))
                Directory.CreateDirectory(RutaArchivos_Temporales);            
        }

        public static bool VerificarArchivoAbierto(string RutaArchivo)
        {
            ((App)Application.Current).CargarListaArchivosAbiertos();
            return ArchivosAbiertos != null && ArchivosAbiertos.Historial.Any(i => i.RutaArchivo == RutaArchivo);
        }

        public async static void AgregarVentana(string[] archivos)
        {
            int indicePantalla = Random.Shared.Next(0, 2);
            var presentacion = MostrarPantallaPresentacion(indicePantalla);
            presentacion.Show();

            await Task.Delay(3000);

            MainWindow nueva = new MainWindow();
            VentanasAbiertas.Add(nueva);
            nueva.ProcesoAsociado = Process.GetCurrentProcess();

            if (CargarInformacionVentana())
            {
                nueva.Height = InformacionVentana.Height;
                nueva.Width = InformacionVentana.Width;

                nueva.Top = InformacionVentana.Top;
                nueva.Left = InformacionVentana.Left;

                nueva.WindowState = InformacionVentana.Estado;
            }
            nueva.Show();
            nueva.IniciarProceso_SegundoPlano();

            if (archivos != null)
            {
                foreach (string archivo in archivos)
                {
                    if (!string.IsNullOrEmpty(archivo) && File.Exists(archivo))
                    {
                        if(!VerificarArchivoAbierto(archivo))
                            nueva.AbrirArchivo(archivo);
                        else
                            MessageBox.Show("El archivo '" + archivo + "' ya está abierto.", "Archivo abierto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }

            presentacion.Close();
        }

        public static System.Windows.Window MostrarPantallaPresentacion(int indice)
        {
            switch(indice)
            {
                case 0:
                    return new PantallaPresentacion();

                case 1:
                    return new PantallaPresentacion2();

                default:
                    return new PantallaPresentacion();
            }
        }

        public void EnviarListaCalculos(string rutaArchivoPlanMath, string ID_ArchivoLista)
        {
            XmlReader guarda = XmlReader.Create(rutaArchivoPlanMath);
            DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            Calculo calculoArchivo = (Calculo)objeto.ReadObject(guarda);
            guarda.Close();

            ListaCalculos_ArchivoPlanMath lista = new ListaCalculos_ArchivoPlanMath();
            foreach (var calculo in calculoArchivo.Calculos)
            {
                PlanMath_Excel.CalculoArchivoPlanMath calc = new PlanMath_Excel.CalculoArchivoPlanMath();
                calc.Nombre = calculo.Nombre;
                lista.Calculos.Add(calc);
            }

            XmlWriter guardaLista = XmlWriter.Create(RutaArchivo_PlanMath_para_Excel + "\\" + ID_ArchivoLista + ".dat");
            DataContractSerializer objetoLista = new DataContractSerializer(typeof(ListaCalculos_ArchivoPlanMath), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objetoLista.WriteObject(guardaLista, lista);
            guardaLista.Close();
        }

        private static bool CargarInformacionVentana()
        {
            if (File.Exists(RutaArchivo_InformacionVentana))
            {
                try
                {
                    XmlReader guarda = XmlReader.Create(RutaArchivo_InformacionVentana);
                    DataContractSerializer objeto = new DataContractSerializer(typeof(InformacionVentana), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                    InformacionVentana = (InformacionVentana)objeto.ReadObject(guarda);
                    guarda.Close();
                    return true;
                }
                catch (Exception) 
                {
                    InformacionVentana = new InformacionVentana();
                    return false;
                }
            }
            else
            {
                InformacionVentana = new InformacionVentana();
                return false;
            }
        }

        public static void GuardarInformacionVentana()
        {
            XmlWriter guarda = XmlWriter.Create(RutaArchivo_InformacionVentana);
            DataContractSerializer objeto = new DataContractSerializer(typeof(InformacionVentana), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(guarda, InformacionVentana);
            guarda.Close();
        }

        public void CargarListaArchivosRecientes()
        {
            if (File.Exists(RutaArchivo_ArchivoRecientes))
            {
                try
                {
                    XmlReader guarda = XmlReader.Create(RutaArchivo_ArchivoRecientes);
                    DataContractSerializer objeto = new DataContractSerializer(typeof(HistorialArchivos), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                    ArchivosRecientes = (HistorialArchivos)objeto.ReadObject(guarda);
                    guarda.Close();
                }
                catch (Exception)
                {
                    ArchivosRecientes = new HistorialArchivos();
                }
            }
            else
            {
                ArchivosRecientes = new HistorialArchivos();
            }
        }

        private void CargarListaEjecucionesRecientes()
        {
            if (File.Exists(RutaArchivo_EjecucionesRecientes))
            {
                try
                {
                    XmlReader guarda = XmlReader.Create(RutaArchivo_EjecucionesRecientes);
                    DataContractSerializer objeto = new DataContractSerializer(typeof(HistorialEjecuciones), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                    EjecucionesRecientes = (HistorialEjecuciones)objeto.ReadObject(guarda);
                    guarda.Close();
                }
                catch (Exception)
                {
                    EjecucionesRecientes = new HistorialEjecuciones();
                }
            }
            else
            {
                EjecucionesRecientes = new HistorialEjecuciones();
            }
        }

        public void CargarListaArchivosAbiertos()
        {
            if (File.Exists(RutaArchivo_ArchivoAbiertos))
            {
                try
                {
                    XmlReader guarda = XmlReader.Create(RutaArchivo_ArchivoAbiertos);
                    DataContractSerializer objeto = new DataContractSerializer(typeof(HistorialArchivos), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                    ArchivosAbiertos = (HistorialArchivos)objeto.ReadObject(guarda);
                    guarda.Close();
                }
                catch (Exception)
                {
                    ArchivosAbiertos = new HistorialArchivos();
                }
            }
            else
            {
                ArchivosAbiertos = new HistorialArchivos();
            }
        }

        public void GuardarArchivosCantidadesDigitadas()
        {
            if (!File.Exists(RutaArchivo_CantidadesDigitadas))
            {
                XmlWriter guarda = XmlWriter.Create(RutaArchivo_CantidadesDigitadas);
                DataContractSerializer objeto = new DataContractSerializer(typeof(Registros_ListasCantidadesDigitadas), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                objeto.WriteObject(guarda, new Registros_ListasCantidadesDigitadas());
                guarda.Close();
            }
        }

        public void GuardarArchivosTextosDigitados()
        {
            if (!File.Exists(RutaArchivo_TextosDigitados))
            {
                XmlWriter guarda = XmlWriter.Create(RutaArchivo_TextosDigitados);
                DataContractSerializer objeto = new DataContractSerializer(typeof(Registros_ListasTextosDigitados), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                objeto.WriteObject(guarda, new Registros_ListasTextosDigitados());
                guarda.Close();
            }
        }

        public static InfoCantidadesElementosObtenidos_UltimaEjecucion CargarElementosObtenidas_Entradas_UltimaEjecucion(string IDArchivoCalculo, string IDEntrada)
        {
            if (File.Exists(RutaArchivo_CantidadElementosObtenidos_Entradas_UltimasEjecuciones))
            {
                try
                {
                    XmlReader guarda = XmlReader.Create(RutaArchivo_CantidadElementosObtenidos_Entradas_UltimasEjecuciones);
                    DataContractSerializer objeto = new DataContractSerializer(typeof(RegistroCantidadesObtenidas_UltimasEjecuciones), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                    var registros = (RegistroCantidadesObtenidas_UltimasEjecuciones)objeto.ReadObject(guarda);
                    guarda.Close();

                    var registroEncontrado = registros.Registros.FirstOrDefault(item => item.IDArchivoCalculo == IDArchivoCalculo &
                    item.IDEntrada == IDEntrada);

                    if (registroEncontrado != null)
                        return registroEncontrado;
                    else
                        return new InfoCantidadesElementosObtenidos_UltimaEjecucion();
                }
                catch (Exception)
                {
                    return new InfoCantidadesElementosObtenidos_UltimaEjecucion();
                }
            }
            else
            {
                return new InfoCantidadesElementosObtenidos_UltimaEjecucion();
            }
        }

        public static void GuardarElementosObtenidas_Entradas_UltimaEjecucion(InfoCantidadesElementosObtenidos_UltimaEjecucion registroAtualizado)
        {
            RegistroCantidadesObtenidas_UltimasEjecuciones registros;

            if (File.Exists(RutaArchivo_CantidadElementosObtenidos_Entradas_UltimasEjecuciones))
            {
                try
                {
                    XmlReader lee = XmlReader.Create(RutaArchivo_CantidadElementosObtenidos_Entradas_UltimasEjecuciones);
                    DataContractSerializer objetoRegistros = new DataContractSerializer(typeof(RegistroCantidadesObtenidas_UltimasEjecuciones), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                    registros = (RegistroCantidadesObtenidas_UltimasEjecuciones)objetoRegistros.ReadObject(lee);
                    lee.Close();

                    var registroEncontrado = registros.Registros.FirstOrDefault(item => item.IDArchivoCalculo == registroAtualizado.IDArchivoCalculo &
                    item.IDEntrada == registroAtualizado.IDEntrada);

                    if (registroEncontrado != null)
                    {
                        registroEncontrado.CantidadNumeros_Obtenidos_UltimaEjecucion = registroAtualizado.CantidadNumeros_Obtenidos_UltimaEjecucion;
                        registroEncontrado.CantidadTextosInformacion_Obtenidos_UltimaEjecucion = registroAtualizado.CantidadTextosInformacion_Obtenidos_UltimaEjecucion;
                        registroEncontrado.PosicionInicialNumeros_Obtenidos_UltimaEjecucion = registroAtualizado.PosicionInicialNumeros_Obtenidos_UltimaEjecucion;
                        registroEncontrado.PosicionFinalNumeros_Obtenidos_UltimaEjecucion = registroAtualizado.PosicionFinalNumeros_Obtenidos_UltimaEjecucion;
                    }
                    else
                    {
                        registros.Registros.Add(registroAtualizado);
                    }
                }
                catch (Exception)
                {
                    registros = new RegistroCantidadesObtenidas_UltimasEjecuciones();
                    registros.Registros.Add(registroAtualizado);
                }
            }
            else
            {
                registros = new RegistroCantidadesObtenidas_UltimasEjecuciones();
                registros.Registros.Add(registroAtualizado);
            }

            try
            {
                XmlWriter guarda = XmlWriter.Create(RutaArchivo_CantidadElementosObtenidos_Entradas_UltimasEjecuciones);
                DataContractSerializer objeto = new DataContractSerializer(typeof(RegistroCantidadesObtenidas_UltimasEjecuciones), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                objeto.WriteObject(guarda, registros);
                guarda.Close();
            }
            catch (Exception) { }
        }

        public static void GuardarListaArchivosRecientes()
        {
            XmlWriter guarda = XmlWriter.Create(RutaArchivo_ArchivoRecientes);
            DataContractSerializer objeto = new DataContractSerializer(typeof(HistorialArchivos), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(guarda, ArchivosRecientes);
            guarda.Close();
        }

        public static void GuardarListaArchivosAbiertos()
        {
            XmlWriter guarda = XmlWriter.Create(RutaArchivo_ArchivoAbiertos);
            DataContractSerializer objeto = new DataContractSerializer(typeof(HistorialArchivos), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(guarda, ArchivosAbiertos);
            guarda.Close();
        }

        public static void AgregarNuevoArchivoReciente(Calculo calculo, Process proceso)
        {
            AperturaArchivo nueva = new AperturaArchivo();
            nueva.DescripcionCalculo = calculo.Descripcion;
            nueva.NombreArchivo = calculo.NombreArchivo;
            nueva.RutaArchivo = calculo.RutaArchivo;
            nueva.FechaHora = DateTime.Now;
            nueva.IDProceso = proceso.Id;
            nueva.NombreProceso = proceso.ProcessName;
            //nueva.RutaEjecutableProceso = proceso.StartInfo.FileName;
            ArchivosRecientes.Historial.Add(nueva);
        }

        public static void AgregarNuevoArchivoAbierto(Calculo calculo, Process proceso)
        {
            AperturaArchivo nueva = new AperturaArchivo();
            nueva.DescripcionCalculo = calculo.Descripcion;
            nueva.NombreArchivo = calculo.NombreArchivo;
            nueva.RutaArchivo = calculo.RutaArchivo;
            nueva.FechaHora = DateTime.Now;
            nueva.IDProceso = proceso.Id;
            nueva.NombreProceso = proceso.ProcessName;
            //nueva.RutaEjecutableProceso = proceso.StartInfo.FileName;
            ArchivosAbiertos.Historial.Add(nueva);
            GuardarListaArchivosAbiertos();
        }

        public static void QuitarArchivoAbierto(Calculo calculo)
        {
            AperturaArchivo nueva = ArchivosAbiertos.Historial.FirstOrDefault(i => i.RutaArchivo == calculo.RutaArchivo);
            ArchivosAbiertos.Historial.Remove(nueva);
            GuardarListaArchivosAbiertos();
        }

        public void QuitarRegistrosArchivosAbiertos_YaCerrados()
        {
            bool conCambios = false;

            if (App.ArchivosAbiertos != null)
            {
                List<Process> procesos = Process.GetProcesses().ToList();

                while (App.ArchivosAbiertos.Historial.Any(i => !procesos.Any(j => j.Id == i.IDProceso &
                j.ProcessName == i.NombreProceso)))
                {
                    conCambios = true;
                    App.ArchivosAbiertos.Historial.Remove(
                        App.ArchivosAbiertos.Historial.FirstOrDefault
                        (i => !procesos.Any(j => j.Id == i.IDProceso &
                j.ProcessName == i.NombreProceso)));
                }
            }

            if(conCambios)
                GuardarListaArchivosAbiertos();
        }

        public static void ActualizarArchivoReciente(Calculo calculo)
        {
            AperturaArchivo apertura = (from AperturaArchivo A in ArchivosRecientes.Historial where A.RutaArchivo == calculo.RutaArchivo select A).FirstOrDefault();
            if (apertura != null)
            {
                apertura.DescripcionCalculo = calculo.Descripcion;
                apertura.FechaHora = DateTime.Now;
            }
        }

        public static AperturaArchivo BuscarAperturaArchivoReciente(Calculo calculo)
        {
            AperturaArchivo apertura = (from AperturaArchivo A in ArchivosRecientes.Historial where A.RutaArchivo == calculo.RutaArchivo select A).FirstOrDefault();
            return apertura;
        }

        public static void QuitarAperturaArchivoReciente(AperturaArchivo apertura)
        {
            ArchivosRecientes.Historial.Remove(apertura);
        }

        public static void GuardarListaEjecucionesRecientes()
        {
            XmlWriter guarda = XmlWriter.Create(RutaArchivo_EjecucionesRecientes);
            DataContractSerializer objeto = new DataContractSerializer(typeof(HistorialEjecuciones), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(guarda, EjecucionesRecientes);
            guarda.Close();
        }

        public static void AgregarNuevaEjecucionReciente(EjecucionCalculo ejecucion)
        {
            EjecucionArchivo nueva = new EjecucionArchivo();
            nueva.DescripcionCalculo = ejecucion.Calculo.Descripcion;
            nueva.NombreArchivo = ejecucion.Calculo.NombreArchivo;
            nueva.RutaArchivo = ejecucion.Calculo.RutaArchivo;
            nueva.FechaHora = DateTime.Now;
            nueva.Ejecucion = ejecucion.ReplicarObjeto();
            EjecucionesRecientes.Historial.Add(nueva);
        }

        public static void QuitarEjecucionArchivoReciente(EjecucionArchivo ejecucion)
        {
            EjecucionesRecientes.Historial.Remove(ejecucion);
        }

        public static void EliminarArchivosTemporales()
        {
            foreach (var itemRuta in ArchivosTemporalesAEliminar)
                if(File.Exists(itemRuta)) File.Delete(itemRuta);
        }

        public static string GenerarID_Elemento()
        {
            //string ID = DateTime.Now.Year.ToString();
            //ID += DateTime.Now.Month.ToString();
            //ID += DateTime.Now.Day.ToString();
            //ID += DateTime.Now.Hour.ToString();
            //ID += DateTime.Now.Minute.ToString();
            //ID += DateTime.Now.Second.ToString();
            //ID += DateTime.Now.Millisecond.ToString();
            string ID = Guid.NewGuid().ToString();
            return ID;
        }
    }

    public enum OpcionArgumentos
    {
        Ninguna = 0,
        ListarCalculos = 1
    }

sealed class ComStaWorker : IDisposable
    {
        private readonly Thread _t;
        private Dispatcher _disp;
        private readonly ManualResetEventSlim _ready = new(false);

        public ComStaWorker()
        {
            _t = new Thread(() =>
            {
                _disp = Dispatcher.CurrentDispatcher;   // STA + Dispatcher
                _ready.Set();
                Dispatcher.Run();                       // message loop
            });
            _t.IsBackground = true;
            _t.SetApartmentState(ApartmentState.STA);
            _t.Start();
            _ready.Wait();
        }

        public void Run(System.Action a)
        {
            // llamada síncrona, bloquea hasta que termine en el STA COM
            _disp.Invoke(a, System.Windows.Threading.DispatcherPriority.Normal);
        }

        public Task RunAsync(System.Action a) =>
            _disp.InvokeAsync(a, DispatcherPriority.Normal).Task;

        public Task<T> RunAsync<T>(Func<T> f) =>
            _disp.InvokeAsync(f, DispatcherPriority.Normal).Task;

        public Task ShutdownAsync() =>
            _disp.InvokeAsync(() => _disp.BeginInvokeShutdown(DispatcherPriority.Background)).Task;

        public void Dispose() { try { _disp?.BeginInvokeShutdown(DispatcherPriority.Background); } catch { } }
    }
}
