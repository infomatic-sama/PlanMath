using ProcessCalc.Entidades;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using static PlanMath_para_Excel.Entradas;
using static PlanMath_para_Word.Entradas;

namespace ProcessCalc
{
    public partial class MainWindow : Window
    {
        public Thread Hilo;
        public bool ProcesoSegundoPlano_EnEjecucion;
        public bool ProcesandoEjecucion_ToolTips;
        public Thread HiloToolTips;
        public bool ProcesoToolTipsSegundoPlano_EnEjecucion;
        public Thread HiloCierre;

        public void IniciarProceso_SegundoPlano()
        {
            Hilo = new Thread(Proceso_Segundo_Plano);
            HiloToolTips = new Thread(EjecutarCalculo_ToolTips);
            HiloCierre = new Thread(CerrarAplicacion);
            ProcesoSegundoPlano_EnEjecucion = true;
            Hilo.Start();
            HiloToolTips.Start();
        }

        public void TerminarProceso_SegundoPlano()
        {
            ProcesoSegundoPlano_EnEjecucion = false;
        }

        public void Proceso_Segundo_Plano()
        {
            while (ProcesoSegundoPlano_EnEjecucion)
            {
                ProcesarEntradasEnviadas_DesdeExcel();
                ProcesarEntradasEnviadas_DesdeWord();
                VerificarCambios_EntradasSubCalculos();
            }

            try
            {
                if (HiloCierre.ThreadState == ThreadState.Unstarted)
                    HiloCierre.Start();
            }
            catch (Exception) { }
        }
        public void ProcesarEntradasEnviadas_DesdeExcel()
        {
            if (Directory.Exists(App.RutaCarpeta_PlanMath_EnvioEntradas_Desde_Excel) &&
                Calculos.Any())
            {
                string[] entradasEnviadas = Directory.GetFiles(App.RutaCarpeta_PlanMath_EnvioEntradas_Desde_Excel);
                bool conCambios = false;
                
                foreach (var archivo in entradasEnviadas)
                {
                    conCambios = false;
                    Entrada_Desde_Excel nuevaEntrada = Entrada.ObtenerEntrada_Excel_Archivo(archivo);

                    if (nuevaEntrada == null)
                    {
                        File.Delete(archivo);
                        continue;
                    }

                    foreach (var calculo in Calculos)
                    {
                        if (calculo.RutaArchivo == nuevaEntrada.ArchivoPlanMath)
                        {
                            List<Entrada> entradasEncontradas = new List<Entrada>();
                            DiseñoCalculo calculoEncontrado = (from DiseñoCalculo C in calculo.Calculos where C.Nombre == nuevaEntrada.NombreCalculo select C).FirstOrDefault();
                            
                            if (calculoEncontrado != null)
                            {
                                entradasEncontradas = Entrada.AgregarEntrada_Desde_Excel_Calculo(nuevaEntrada, calculoEncontrado);

                                //if (calculoEncontrado.EsEntradasArchivo)
                                //{
                                //    foreach(var entradaEncontrada in entradasEncontradas)
                                //        calculoEncontrado.AgregarEntrada_CalculoEntradas(entradaEncontrada);
                                //}

                                conCambios = true;
                            }
                            else
                            {
                                DiseñoCalculo nuevoCalculo = new DiseñoCalculo();
                                nuevoCalculo.ID = App.GenerarID_Elemento();
                                nuevoCalculo.Nombre = nuevaEntrada.NombreCalculo;

                                entradasEncontradas = Entrada.AgregarEntrada_Desde_Excel_Calculo(nuevaEntrada, nuevoCalculo);
                                calculo.Calculos.Add(nuevoCalculo);

                                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                                {
                                    Dispatcher.Invoke(() => ActualizarListasCalculos_PestañaActual());
                                }

                                conCambios = true;
                            }

                            if (conCambios && entradasEncontradas != null && entradasEncontradas.Any())
                            {
                                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                                {
                                    Dispatcher.Invoke(() =>
                                {
                                    foreach (var entradaEncontrada in entradasEncontradas)
                                    {
                                        ActualizarNombresDescripcionesEntrada(entradaEncontrada);
                                        ActualizarElementosEntradas(entradaEncontrada, true, calculoEncontrado);
                                        ActualizarListasEntradas_PestañaActual();
                                        ActualizarVistasEntradas_Actuales_Excel(entradaEncontrada);
                                    }
                                });
                                }
                            }
                        }
                    }

                    if(conCambios)
                        File.Delete(archivo);
                }
            }
        }

        public void ProcesarEntradasEnviadas_DesdeWord()
        {
            if (Directory.Exists(App.RutaCarpeta_PlanMath_EnvioEntradas_Desde_Word) &&
                Calculos.Any())
            {
                string[] entradasEnviadas = Directory.GetFiles(App.RutaCarpeta_PlanMath_EnvioEntradas_Desde_Word);
                bool conCambios = false;

                foreach (var archivo in entradasEnviadas)
                {
                    conCambios = false;
                    Entrada_Desde_Word nuevaEntrada = Entrada.ObtenerEntrada_Word_Archivo(archivo);

                    if (nuevaEntrada == null)
                    {
                        File.Delete(archivo);
                        continue;
                    }

                    foreach (var calculo in Calculos)
                    {
                        if (calculo.RutaArchivo == nuevaEntrada.ArchivoPlanMath)
                        {
                            List<Entrada> entradasEncontradas = new List<Entrada>();
                            DiseñoCalculo calculoEncontrado = (from DiseñoCalculo C in calculo.Calculos where C.Nombre == nuevaEntrada.NombreCalculo select C).FirstOrDefault();

                            if (calculoEncontrado != null)
                            {
                                entradasEncontradas = Entrada.AgregarEntrada_Desde_Word_Calculo(nuevaEntrada, calculoEncontrado);

                                //if (calculoEncontrado.EsEntradasArchivo)
                                //{
                                //    foreach(var entradaEncontrada in entradasEncontradas)
                                //        calculoEncontrado.AgregarEntrada_CalculoEntradas(entradaEncontrada);
                                //}

                                conCambios = true;
                            }
                            else
                            {
                                DiseñoCalculo nuevoCalculo = new DiseñoCalculo();
                                nuevoCalculo.ID = App.GenerarID_Elemento();
                                nuevoCalculo.Nombre = nuevaEntrada.NombreCalculo;

                                entradasEncontradas = Entrada.AgregarEntrada_Desde_Word_Calculo(nuevaEntrada, nuevoCalculo);
                                calculo.Calculos.Add(nuevoCalculo);

                                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                                {
                                    Dispatcher.Invoke(() => ActualizarListasCalculos_PestañaActual());
                                }

                                conCambios = true;
                            }

                            if (conCambios && entradasEncontradas != null && entradasEncontradas.Any())
                            {
                                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                                {
                                    Dispatcher.Invoke(() =>
                                {
                                    foreach (var entradaEncontrada in entradasEncontradas)
                                    {
                                        ActualizarNombresDescripcionesEntrada(entradaEncontrada);
                                        ActualizarElementosEntradas(entradaEncontrada, true, calculoEncontrado);
                                        ActualizarListasEntradas_PestañaActual();
                                        ActualizarVistasEntradas_Actuales_Word(entradaEncontrada);
                                    }
                                });
                                }
                            }
                        }
                    }

                    if (conCambios)
                        File.Delete(archivo);
                }
            }
        }

        public void EjecutarCalculo_ToolTips()
        {
            while (ProcesoSegundoPlano_EnEjecucion)
            {
                try
                {
                    ProcesandoEjecucion_ToolTips = true;

                    foreach (var itemInfoToolTip in EjecucionesToolTips)
                    {
                        bool cerrarHilo = false;

                        if (itemInfoToolTip != null)
                        {
                            if (itemInfoToolTip.Ventana.Dispatcher.HasShutdownStarted ||
                                itemInfoToolTip.Ventana.Dispatcher.HasShutdownFinished)
                            {
                                cerrarHilo = true;
                            }
                            else
                            {
                                bool Mostrada = false;
                                bool Cerrando = false;

                                Dispatcher.Invoke(() =>
                                {
                                    Mostrada = itemInfoToolTip.Ventana.Mostrada;
                                    Cerrando = itemInfoToolTip.Ventana.Cerrando;

                                });

                                if (Mostrada && !Cerrando)
                                {
                                    itemInfoToolTip.ActualizarElementosVisuales_ToolTips();

                                    if (itemInfoToolTip.Ejecucion != null &&
                                        !itemInfoToolTip.Ejecucion.Detenida &&
                                        ((itemInfoToolTip.VerificarCambiosCalculo()) ||
                                            itemInfoToolTip.Calculo.HayCambios))
                                    {
                                        itemInfoToolTip.Ejecucion.Detener();
                                        //try
                                        //{
                                        //    itemInfoToolTip.Ejecucion.Hilo.Interrupt();
                                        //}
                                        //catch (Exception ex) { }
                                        //while(!Thread.CurrentThread.Join(Timeout.Infinite));

                                    }

                                    if ((itemInfoToolTip.Ejecucion == null ||
                                (itemInfoToolTip.Ejecucion != null &&
                                        (itemInfoToolTip.Ejecucion.Finalizada ||
                                        (!itemInfoToolTip.Ejecucion.Hilo.IsAlive |
                                        !itemInfoToolTip.Ejecucion.Hilo.ThreadState.HasFlag(ThreadState.Running) |
                                        itemInfoToolTip.Ejecucion.Hilo.ThreadState.HasFlag(ThreadState.WaitSleepJoin) |
                                        itemInfoToolTip.Ejecucion.Hilo.ThreadState.HasFlag(ThreadState.Stopped))))))
                                    {
                                        if (((itemInfoToolTip.VerificarCambiosCalculo()) ||

                                            itemInfoToolTip.Calculo.HayCambios))
                                        {

                                            //if(itemInfoToolTip.Ejecucion != null)
                                            //{
                                            //    PlanMath_para_Excel.PlanMath_Excel.CerrarAplicacionesExcel(itemInfoToolTip.Ejecucion.GUID_EjecucionCalculo);
                                            //}

                                            if (itemInfoToolTip.Calculo.HayCambios)
                                                itemInfoToolTip.Calculo.HayCambios = false;

                                            //if (itemInfoToolTip.Ejecucion != null &&
                                            //    !itemInfoToolTip.Ejecucion.Finalizada)
                                            //{
                                            //    itemInfoToolTip.Ejecucion.Detener();

                                            //    while (itemInfoToolTip.Ejecucion.Hilo.ThreadState == ThreadState.Running)
                                            //    {
                                            //        Thread.Sleep(10);
                                            //    }
                                            //}

                                            //if (itemInfoToolTip.Calculo.HayCambios)
                                            //    itemInfoToolTip.Calculo.HayCambios = false;
                                            //else

                                            itemInfoToolTip.VerificarCambiosEntradas();
                                            itemInfoToolTip.VerificarCambiosOperaciones();

                                            //itemInfoToolTip.QuitarElementos_ToolTips();
                                            //itemInfoToolTip.DetenerHilos();

                                            itemInfoToolTip.UltimoEstadoCalculo = itemInfoToolTip.Calculo.ReplicarObjeto();
                                            //itemInfoToolTip.ReplicarElementosVisuales();

                                            itemInfoToolTip.Ejecucion = new EjecucionCalculo()
                                            {
                                                ModoToolTips = true,
                                                Calculo = itemInfoToolTip.Calculo.ReplicarObjeto(),
                                                TooltipsCalculo = itemInfoToolTip,
                                                Ventana = itemInfoToolTip.Ventana
                                            };


                                            itemInfoToolTip.Ejecucion.Iniciar();

                                        }
                                    }
                                }
                                else
                                    cerrarHilo = true;
                            }

                            if (cerrarHilo)
                            {
                                if (itemInfoToolTip.Ejecucion != null &&
                                 !itemInfoToolTip.Ejecucion.Finalizada)
                                {
                                    itemInfoToolTip.Ejecucion.Detener();

                                    while (!itemInfoToolTip.Ejecucion.Finalizada)
                                    {
                                    }
                                }

                                //CerrarToolTips();

                                break;
                            }
                        }
                    }
                }
                catch (Exception) { }

                ProcesandoEjecucion_ToolTips = false;
            }

            try
            {
                if (HiloCierre.ThreadState == ThreadState.Unstarted)
                    HiloCierre.Start();
            }
            catch (Exception) { }
        }

        public void CerrandoHilos_ToolTips()
        {
            foreach (var itemInfoToolTip in EjecucionesToolTips)
            {
                if (itemInfoToolTip.Ejecucion != null &&
                             !itemInfoToolTip.Ejecucion.Finalizada)
                {
                    itemInfoToolTip.Ejecucion.Detener();

                    //try
                    //{
                    //    itemInfoToolTip.Ejecucion.Hilo.Interrupt();
                    //}
                    //catch (Exception ex) { }
                    //while(!Thread.CurrentThread.Join(Timeout.Infinite));
                    //PlanMath_para_Excel.PlanMath_Excel.CerrarAplicacionesExcel(itemInfoToolTip.Ejecucion.GUID_EjecucionCalculo);

                    while (!itemInfoToolTip.Ejecucion.Finalizada) ;
                    //{
                    //}
                }
            }
        }
        public void VerificarCambios_EntradasSubCalculos()
        {
            try
            {
                foreach (var itemArchivoCalculo in Calculos)
                {
                    if (itemArchivoCalculo != null)
                    {
                        foreach (var itemCalculo in itemArchivoCalculo?.Calculos)
                        {
                            foreach (var itemElemento in itemCalculo.ElementosOperaciones)
                            {
                                if (itemElemento.fechaUltimoEstadoArchivo != null)
                                {
                                    if (itemElemento.VerificarCambios_ArchivoSubCalculo())
                                    {
                                        itemElemento.ObtenerFechaUltimoEstadoArchivo();
                                        itemArchivoCalculo.HayCambios = true;
                                    }
                                }
                                else
                                {
                                    itemElemento.ObtenerFechaUltimoEstadoArchivo();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        public void CerrarAplicacion()
        {
            foreach (var itemInfoToolTip in EjecucionesToolTips)
            {
                if(itemInfoToolTip.Ejecucion != null)
                    while (itemInfoToolTip.Ejecucion != null && 
                        !itemInfoToolTip.Ejecucion.Finalizada) ;
            }

            while (Hilo.ThreadState == ThreadState.Running) ;
            while (HiloToolTips.ThreadState == ThreadState.Running) ;

            App.VentanasAbiertas.Remove(this);
        }
    }
}