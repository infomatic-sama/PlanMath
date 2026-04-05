using PlanMath_para_Excel;
using ProcessCalc.Controles;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.OrigenesDatos;
using ProcessCalc.Entidades.Resultados;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using static ProcessCalc.Entidades.ElementoDiseñoOperacionAritmeticaEjecucion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        private Calculo calc;
        public Calculo Calculo 
        {
            get
            {
                return calc;
            }

            set
            {
                CalculoEstadoOriginal = value.ReplicarObjeto();
                calc = value;
            }
        }
        public Calculo CalculoEstadoOriginal { get; set; }
        public string GUID_ArchivoCalculo { get; set; }
        public string GUID_EjecucionCalculo { get; set; }
        public DateTime FechaHora { get; set; }
        [IgnoreDataMember]
        public Thread Hilo;
        private List<string> log;
        public List<string> TextoLog
        {
            get
            {
                List<string> logActual = new List<string>();
                while (log.Count > 0)
                {
                    logActual.Add(log.First());
                    log.Remove(log.First());
                }
                return logActual;
            }
            set
            {
                log = value;
            }
        }
        [IgnoreDataMember]
        public List<EtapaEjecucion> etapas;
        [IgnoreDataMember]
        public ProgresoEjecucionCalculo Progreso { get; set; }
        [IgnoreDataMember]
        private int CantidadElementosEjecucion;
        [IgnoreDataMember]
        private int CantidadElementosEjecucionProcesados;
        [IgnoreDataMember]
        public volatile bool Detenida;
        [IgnoreDataMember]
        public bool Pausada { get; set; }
        [IgnoreDataMember]
        public bool ConError { get; set; }
        [IgnoreDataMember]
        public volatile bool Finalizada;
        public InformeResultados InformeResultados { get; set; }
        [IgnoreDataMember]
        public List<ElementoOperacionAritmeticaEjecucion> Salidas = new List<ElementoOperacionAritmeticaEjecucion>();
        [IgnoreDataMember]
        public List<ElementoOperacionAritmeticaEjecucion> SalidasCalculo = new List<ElementoOperacionAritmeticaEjecucion>();
        [IgnoreDataMember]
        public List<EtapaEjecucion> etapasCalculo;
        [IgnoreDataMember]
        public DiseñoCalculo SubCalculoActual { get; set; }
        [IgnoreDataMember]
        public bool CambioSubcalculo { get; set; }
        [IgnoreDataMember]
        public List<EtapaEjecucion> etapasHistorial { get; set; }
        [IgnoreDataMember]
        public bool Iniciada { get; set; }
        [IgnoreDataMember]
        public List<FileStream> archivosAbiertos { get; set; }
        [IgnoreDataMember]
        public List<ElementoArchivoOrigenDatosEjecucion> ArchivosOrigenesDatos_Abiertos { get; set; }
        [IgnoreDataMember]
        public List<DiseñoOperacion> ElementosAgrupadores = new List<DiseñoOperacion>();
        [IgnoreDataMember]
        public DiseñoOperacion AgrupadorActual { get; set; }
        [IgnoreDataMember]
        public DiseñoOperacion AgrupadorCambio { get; set; }
        [IgnoreDataMember]
        public List<EtapaEjecucion> etapasProgreso { get; set; }
        [IgnoreDataMember]
        public List<EtapaEjecucion> etapasCalculosProgreso { get; set; }
        [IgnoreDataMember]
        public List<DiseñoOperacion> EntradasProcesadas { get; set; }
        [IgnoreDataMember]
        public bool ModoEjecucionExterna { get; set; }
        [IgnoreDataMember]
        public bool ModoToolTips {  get; set; }
        [IgnoreDataMember]
        public Clasificador itemClasificador { get; set; }
        public EjecucionCalculo()
        {
            Hilo = new Thread(RealizarCalculos);
            Hilo.SetApartmentState(ApartmentState.STA);
            Hilo.IsBackground = true;
            log = new List<string>();
            etapas = new List<EtapaEjecucion>();
            etapasCalculo = new List<EtapaEjecucion>();
            Progreso = new ProgresoEjecucionCalculo();
            InformeResultados = new InformeResultados();
            CantidadElementosEjecucion = 0;
            CantidadElementosEjecucionProcesados = 0;
            etapasHistorial = new List<EtapaEjecucion>();
            archivosAbiertos = new List<FileStream>();
            etapasProgreso = new List<EtapaEjecucion>();
            etapasCalculosProgreso = new List<EtapaEjecucion>();
            EntradasProcesadas = new List<DiseñoOperacion>();
            ArchivosOrigenesDatos_Abiertos = new List<ElementoArchivoOrigenDatosEjecucion>();
        }

        public EjecucionCalculo ReplicarObjeto()
        {
            EjecucionCalculo ejecucion = null;

            MemoryStream flujo = new MemoryStream();

            DataContractSerializer objeto = new DataContractSerializer(typeof(EjecucionCalculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(flujo, this);

            flujo.Position = 0;
            ejecucion = (EjecucionCalculo)objeto.ReadObject(flujo);

            return ejecucion;
        }
        public void Iniciar()
        {
            if (Hilo != null)
                Hilo.Start();

            GUID_EjecucionCalculo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() +
                DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() + "-" + Guid.NewGuid().ToString().Split('-')[0];

            Iniciada = true;
        }

        public void Detener()
        {
            if (Hilo != null)
            {
                CerrarArchivosAbiertos();
                Detenida = true;
            }
        }

        public void Pausar()
        {
            if (Hilo != null)
            {
                Pausada = true;
                while (Pausada) { }
            }
        }

        private void CerrarArchivosAbiertos()
        {
            while (archivosAbiertos.Any())
            {
                try
                {
                    archivosAbiertos.FirstOrDefault().Close();
                }
                catch (Exception) { };

                archivosAbiertos.Remove(archivosAbiertos.FirstOrDefault());
            }
        }
        private void RealizarCalculos()
        {
            log.Clear();
            InformeResultados.TextoLog.Clear();

            log.Add("Iniciando...");
            InformeResultados.TextoLog.Add("Iniciando...");

            etapasCalculo.Clear();

            if (ModoToolTips &&
                TooltipsCalculo != null &&
                !Detenida)
            {
                TooltipsCalculo.EstablecerEstadoCambiosToolTips_SeleccionEntradas(true, this.Calculo);
            }

            CantidadElementosEjecucion = 0;
            ElementosAgrupadores.AddRange(Calculo.ObtenerTodosAgrupadores());

            PrepararEtapasCalculos();
            PrepararConexionesElementosEtapas();

            if ((from E in etapasCalculo where (from Elemento in E.Elementos select Elemento).Any() select E).Any())
                etapasCalculosProgreso.AddRange((from E in etapasCalculo where (from Elemento in E.Elementos select Elemento).Any() select E).ToList());

            foreach (var itemEtapa in etapasCalculo)
            {
                if (Pausada) Pausar();
                if (Detenida) break;

                RealizarCalculosEtapa(itemEtapa);
            }

            SalidasCalculo = SalidasCalculo.Distinct().ToList();

            foreach (var itemSalida in SalidasCalculo)
            {
                ResultadoEjecucion resultado = new ResultadoEjecucion();
                Resultado resultadoDiseño = (from Resultado R in Calculo.ListaResultados where R.SalidaRelacionada == itemSalida.ElementoDiseñoRelacionado select R).FirstOrDefault();
                resultado.ResultadoAsociado = resultadoDiseño;
                resultado.SalidaRelacionada = itemSalida;
                resultadoDiseño.Clasificadores_Cantidades.AddRange(itemSalida.Clasificadores_Cantidades);

                if (resultadoDiseño != null)
                {
                    if(!string.IsNullOrEmpty(resultadoDiseño.Nombre))
                        resultado.Nombre = resultadoDiseño.Nombre + " - ";

                    if(!string.IsNullOrEmpty(resultadoDiseño.Descripcion))
                        resultado.Descripcion = resultadoDiseño.Descripcion;

                    resultado.NoMostrar_SiEsConjunto_SiNoTieneNumeros = resultadoDiseño.NoMostrar_SiEsConjunto_SiNoTieneNumeros;
                    resultado.NoMostrar_SiEsCero = resultadoDiseño.NoMostrar_SiEsCero;

                    switch (itemSalida.Tipo)
                    {
                        case TipoElementoEjecucion.Entrada:
                            ElementoEntradaEjecucion entrada = (ElementoEntradaEjecucion)itemSalida;

                            switch (entrada.TipoEntrada)
                            {
                                case TipoEntrada.ConjuntoNumeros:
                                    resultado.Tipo = "Vector de números";
                                    resultado.EsConjuntoNumeros = true;

                                    if (!string.IsNullOrEmpty(itemSalida.ElementoDiseñoRelacionado.NombreCombo))
                                        resultado.Nombre += "Variable o vector de entrada: " + itemSalida.ElementoDiseñoRelacionado.NombreCombo;

                                    resultado.TextosInformacion.AddRange(itemSalida.Textos);

                                    foreach (var itemNumero in entrada.Numeros)
                                    {
                                        string nombre = string.Empty;

                                        EntidadNumero numero = new EntidadNumero();
                                        numero.Numero = itemNumero.Numero;
                                        numero.Clasificadores_SeleccionarOrdenar.AddRange(itemNumero.Clasificadores_SeleccionarOrdenar);
                                        numero.Clasificadores_SeleccionarOrdenar_Resultados.AddRange(numero.Clasificadores_SeleccionarOrdenar);

                                        if (!string.IsNullOrEmpty(itemNumero.Nombre))
                                            numero.Nombre = itemNumero.Nombre; //+ " de " + resultado.Nombre;

                                        numero.Textos.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                                        resultado.ValoresNumericos.Add(numero);
                                    }
                                    break;
                            }

                            if (resultado.ResultadoAsociado.SalidaRelacionada.DefinicionOpcionesNombresResultados != null &&
                                    resultado.ResultadoAsociado.SalidaRelacionada.DefinicionOpcionesNombresResultados.OpcionesTextos.Any())
                            {
                                if (itemSalida.GetType() == typeof(ElementoEntradaEjecucion))
                                {
                                    int posicion = 1;
                                    foreach (var itemNumero in resultado.ValoresNumericos)
                                    {
                                        string nombreActual = itemNumero.Nombre;
                                        EstablecerNombreCantidad(itemNumero, itemSalida.ElementoDiseñoRelacionado.DefinicionOpcionesNombresResultados, posicion, itemSalida);

                                        itemNumero.TextoAcompañante = itemNumero.Nombre;
                                        itemNumero.Nombre = nombreActual;

                                        posicion++;
                                    }
                                }
                            }

                            break;

                        case TipoElementoEjecucion.OperacionAritmetica:
                            ElementoOperacionAritmeticaEjecucion operacion = (ElementoOperacionAritmeticaEjecucion)itemSalida;
                            
                            resultado.Tipo = "Vector de números";
                            resultado.EsConjuntoNumeros = true;

                            if (!string.IsNullOrEmpty(itemSalida.ElementoDiseñoRelacionado.NombreCombo))
                                resultado.Nombre += "Variable o vector de números retornados: " + itemSalida.ElementoDiseñoRelacionado.NombreCombo;

                            resultado.TextosInformacion.AddRange(itemSalida.Textos);

                            int indice = 1;
                            foreach (var itemNumero in operacion.Numeros)
                            {
                                string nombre = string.Empty;

                                EntidadNumero numero = new EntidadNumero();
                                numero.Numero = itemNumero.Numero;
                                numero.Clasificadores_SeleccionarOrdenar.AddRange(itemNumero.Clasificadores_SeleccionarOrdenar);
                                numero.Clasificadores_SeleccionarOrdenar_Resultados.AddRange(numero.Clasificadores_SeleccionarOrdenar);

                                if (!string.IsNullOrEmpty(itemNumero.Nombre))
                                    numero.Nombre = itemNumero.Nombre; //+ " de " + resultado.Nombre;

                                numero.Textos.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                                resultado.ValoresNumericos.Add(numero);

                                indice++;
                            }

                            if (resultado.ResultadoAsociado.SalidaRelacionada.DefinicionOpcionesNombresResultados != null &&
                                resultado.ResultadoAsociado.SalidaRelacionada.DefinicionOpcionesNombresResultados.OpcionesTextos.Any())
                            {
                                int posicion = 1;
                                foreach (var itemNumero in resultado.ValoresNumericos)
                                {
                                    string nombreActual = itemNumero.Nombre;
                                    EstablecerNombreCantidad(itemNumero, itemSalida.ElementoDiseñoRelacionado.DefinicionOpcionesNombresResultados, posicion, itemSalida);

                                    itemNumero.TextoAcompañante = itemNumero.Nombre;
                                    itemNumero.Nombre = nombreActual;

                                    posicion++;
                                }
                            }
                            
                            break;
                    }

                    InformeResultados.Resultados.Add(resultado);
                }
            }

            //if (SalidasCalculo.Count > 1)
            //{
            //    InformeResultados.TextoLog.Add("Los resultados totales de " + Calculo.Descripcion + " son:\n");
            //    foreach (var itemSalida in SalidasCalculo)
            //    {
            //        InformeResultados.TextoLog.Add(itemSalida.ValorNumerico.ToString() + "\n");
            //    }
            //    InformeResultados.TextoLog.Add("\n");
            //}
            //else if (SalidasCalculo.Count == 1)
            //{
            //    if(SalidasCalculo.First().)
            //    InformeResultados.TextoLog.Add("El resultado total de " + Calculo.Descripcion + " es: " + SalidasCalculo.First().ValorNumerico.ToString() + "\n");
            //}

            InformeResultados.TextoLog.Add("Los resultados totales son:\n");
            log.Add("Los resultados totales son:\n");
                        
            foreach (var itemSalida in SalidasCalculo)
            {
                var resultadoAsociado = InformeResultados.Resultados.FirstOrDefault(i => i.SalidaRelacionada == itemSalida);

                ElementoOperacionAritmeticaEjecucion salidaCalculo = (ElementoOperacionAritmeticaEjecucion)itemSalida;

                //string nombre = "de la operación " + itemSalida.ElementoDiseñoRelacionado.Nombre;
                //if (itemSalida.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Entrada)
                //    nombre = "de la entrada " + itemSalida.ElementoDiseñoRelacionado.EntradaRelacionada.Nombre;

                string nombreResultado = string.Empty;
                if (!string.IsNullOrEmpty(resultadoAsociado.Nombre))
                    nombreResultado = "de " + resultadoAsociado.Nombre;

                InformeResultados.TextoLog.Add("El resultado " + nombreResultado + " es: ");
                log.Add("El resultado " + nombreResultado + " es: ");

                foreach (var itemSalidaCalculo in salidaCalculo.Numeros)
                {
                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemSalidaCalculo))
                    {
                        InformeResultados.TextoLog.Add(itemSalidaCalculo.Nombre + ": " + itemSalidaCalculo.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemSalidaCalculo.ObtenerTextosInformacion_Cadena());
                        log.Add(itemSalidaCalculo.Nombre + ": " + itemSalidaCalculo.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemSalidaCalculo.ObtenerTextosInformacion_Cadena());
                    }
                }
                
            }
            

            log.Add("Finalizado");
            InformeResultados.TextoLog.Add("Finalizado");

            if (ModoToolTips &&
                TooltipsCalculo != null &&
                !Detenida)
            {
                TooltipsCalculo.LimpiarElementos();
            }

            //using var com = new ComStaWorker();
            //com.Run(() =>
            //{
                PlanMath_para_Excel.PlanMath_Excel.CerrarAplicacionesExcel(GUID_EjecucionCalculo);
                PlanMath_para_Word.PlanMath_Word.CerrarAplicacionesWord(GUID_EjecucionCalculo);
            //});

            LimpiarArchivosTemporales();

            Finalizada = true;
        }

        private void RealizarCalculo(ElementoCalculoEjecucion itemCalculo)
        {
            if (!itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)
            {
                log.Add("Iniciando cálculo " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + "...");
                InformeResultados.TextoLog.Add("Iniciando cálculo " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + "...");
            }
            else
            {
                log.Add("Iniciando archivo de cálculo."); //+ (itemCalculo.ElementoDiseñoCalculoRelacionado.ListaEntradas.Any() ? " Procesando las entradas generales..." : string.Empty));
                InformeResultados.TextoLog.Add("Iniciando archivo de cálculo.");
            }

            etapas.Clear();
            Salidas.Clear();

            PrepararEtapas(itemCalculo);
            PrepararConexionesElementosEtapas_Calculo();
            PrepararSubElementosEtapas_Calculo();
            
            if ((from E in etapas where (from Elemento in E.Elementos select Elemento).Any() select E).Any())
                etapasProgreso.AddRange((from E in etapas where (from Elemento in E.Elementos select Elemento).Any() select E).ToList());

            if (!itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)
            {
                List<ElementoEjecucionCalculo> itemsCalculo = new List<ElementoEjecucionCalculo>();
                foreach (var itemEtapa in etapas)
                {
                    itemsCalculo.AddRange((from E in itemEtapa.Elementos where E.Tipo == TipoElementoEjecucion.OperacionAritmetica select E).ToList());
                }

                log.Add("Para calcular y obtener el valor de " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + ", se realizan las operaciones siguientes:");
                InformeResultados.TextoLog.Add("Para calcular y obtener el valor de " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + ", se realizan las operaciones siguientes:");

                foreach (var itemOperacion in itemsCalculo.Distinct().ToList())
                {
                    log.Add(itemOperacion.ElementoDiseñoRelacionado.Nombre);
                    InformeResultados.TextoLog.Add(itemOperacion.ElementoDiseñoRelacionado.Nombre);
                }
            }

            bool mostrarLog = true;
            //if (itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)
            //    mostrarLog = false;

            foreach (var itemEtapa in etapas)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                ObtenerDatosEntradas(itemEtapa, itemCalculo, mostrarLog);                
                RealizarOperaciones(itemEtapa, itemCalculo, mostrarLog);
                
                itemCalculo.CantidadEtapasProcesadas++;
            }

            itemCalculo.etapasBuscarEntradas.AddRange(etapas);
            
        
            if((from E in etapas where (from Elemento in E.Elementos select Elemento).Any() select E).Any())
                etapasHistorial.AddRange((from E in etapas where (from Elemento in E.Elementos select Elemento).Any() select E).ToList());
            
            Salidas = Salidas.Distinct().ToList();

            if (ModoToolTips && this.TooltipsCalculo != null &&
                this.TooltipsCalculo.ObtenerCalculoDiseño(Calculo.ID, itemCalculo.ElementoDiseñoCalculoRelacionado.ID) != null &&
                !this.TooltipsCalculo.ObtenerCalculoDiseño(Calculo.ID, itemCalculo.ElementoDiseñoCalculoRelacionado.ID).EsEntradasArchivo
                &&
                            ((ElementoCalculoEjecucion)itemCalculo).ConModificaciones_ToolTipMostrado(TooltipsCalculo, this))
            {
                //if (Salidas.Any())
                //{
                    TooltipsCalculo.EstablecerDatosTooltip_Elemento(itemCalculo.ElementoDiseñoCalculoRelacionado,
                        Salidas.Select(i => (ElementoOperacionAritmeticaEjecucion)i).ToList(),
                        TipoOpcionToolTip.Calculo, new List<Clasificador>(), false, false);
                //}
                //else
                //{
                    //TooltipsCalculo.EstabecerDatosTooltip_Elemento(itemCalculo.ElementoDiseñoCalculoRelacionado,
                    //    ((ElementoCalculoEjecucion)itemCalculo).ValorNumerico,
                    //    ((ElementoCalculoEjecucion)itemCalculo).Textos,
                    //    TipoOpcionToolTip.Calculo);
                //}
            }

            if (!itemCalculo.ElementoDiseñoCalculoRelacionado.EsEntradasArchivo)
            {
                if (Salidas.Count > 1)
                {
                    log.Add("Los resultados de " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + " son:\n");
                    InformeResultados.TextoLog.Add("Los resultados de " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + " son:\n");
                    foreach (var itemSalida in Salidas)
                    {
                        ElementoOperacionAritmeticaEjecucion salidaCalculo = (ElementoOperacionAritmeticaEjecucion)itemSalida;

                        string nombre = itemSalida.ElementoDiseñoRelacionado.Nombre;
                        if (itemSalida.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Entrada)
                            nombre = itemSalida.ElementoDiseñoRelacionado.EntradaRelacionada.Nombre;

                        log.Add("El resultado de " + nombre + " es: ");
                        InformeResultados.TextoLog.Add("El resultado de " + nombre + " es: ");
                        foreach (var itemSalidaCalculo in salidaCalculo.Numeros)
                        {
                            if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemSalidaCalculo))
                            {
                                log.Add(itemSalidaCalculo.Nombre + ": " + itemSalidaCalculo.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemSalidaCalculo.ObtenerTextosInformacion_Cadena());
                                InformeResultados.TextoLog.Add(itemSalidaCalculo.Nombre + ": " + itemSalidaCalculo.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemSalidaCalculo.ObtenerTextosInformacion_Cadena());
                            }
                        }
                        
                    }
                }
                else if (Salidas.Count == 1)
                {
                    ElementoOperacionAritmeticaEjecucion salidaCalculo = (ElementoOperacionAritmeticaEjecucion)Salidas.First();

                    log.Add("El resultado de " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + " es: ");
                    InformeResultados.TextoLog.Add("El resultado de " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + " es: ");
                    foreach (var itemSalidaCalculo in salidaCalculo.Numeros)
                    {
                        if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemSalidaCalculo))
                        {
                            log.Add(itemSalidaCalculo.Nombre + ": " + itemSalidaCalculo.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemSalidaCalculo.ObtenerTextosInformacion_Cadena());
                            InformeResultados.TextoLog.Add(itemSalidaCalculo.Nombre + ": " + itemSalidaCalculo.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemSalidaCalculo.ObtenerTextosInformacion_Cadena());
                        }
                    }
                }

                log.Add("Cálculo " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + " terminado.");
                InformeResultados.TextoLog.Add("Cálculo " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre + " terminado.");
            }
        }

        private void PrepararEtapas(ElementoCalculoEjecucion itemCalculo)
        {
            var elementosEtapas = (from DiseñoOperacion E in itemCalculo.ElementoDiseñoCalculoRelacionado.ElementosOperaciones where E.ElementosAnteriores.Count == 0 select E).ToList();

            List<DiseñoOperacion> elementosIteraciones = new List<DiseñoOperacion>();

            while (elementosEtapas.Count > 0)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                EtapaEjecucion etapa = new EtapaEjecucion();
                PrepararElementosEtapas(ref elementosEtapas, ref etapa);

                etapas.Add(etapa);

                List<DiseñoOperacion> elementosAnteriores = elementosEtapas.ToList();
                elementosEtapas.Clear();

                elementosAnteriores.InsertRange(0, elementosIteraciones);
                elementosIteraciones.Clear();

                foreach (var itemAnterior in elementosAnteriores)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    elementosEtapas.AddRange((from DiseñoOperacion E in itemCalculo.ElementoDiseñoCalculoRelacionado.ElementosOperaciones
                                              where E.ElementosAnteriores.Contains(itemAnterior) & itemAnterior.ElementosPosteriores.Contains(E)
                                              select E).ToList());

                    List<DiseñoOperacion> elementosAQuitar = new List<DiseñoOperacion>();

                    foreach (var elementoVerificar_Posteriores in elementosEtapas)
                    {
                        foreach (var elementoPosterior_Buscado in elementosEtapas)
                        {
                            if (elementoVerificar_Posteriores != elementoPosterior_Buscado)
                            {
                                if (RecorrerElementosPosteriores_BusquedaElemento(elementoVerificar_Posteriores, elementoPosterior_Buscado))
                                {
                                    elementosAQuitar.Add(elementoPosterior_Buscado);
                                }
                            }
                        }                            
                    }

                    while (elementosAQuitar.Any())
                    {
                        if (Pausada) Pausar();
                        if (Detenida) return;

                        elementosIteraciones.Add(elementosAQuitar.First());
                        elementosEtapas.Remove(elementosAQuitar.First());
                        elementosAQuitar.Remove(elementosAQuitar.First());
                    }

                    elementosAQuitar = new List<DiseñoOperacion>();

                    foreach (var elementoVerificar_Condiciones in elementosEtapas)
                    {
                        if (Pausada) Pausar();
                        if (Detenida) return;

                        if (VerificarElementosAnteriores(elementoVerificar_Condiciones, etapas, elementosEtapas))
                        {
                            elementosAQuitar.Add(elementoVerificar_Condiciones);
                        }

                    }

                    while (elementosAQuitar.Any())
                    {
                        if (Pausada) Pausar();
                        if (Detenida) return;

                        elementosIteraciones.Add(elementosAQuitar.First());
                        elementosEtapas.Remove(elementosAQuitar.First());
                        elementosAQuitar.Remove(elementosAQuitar.First());
                    }
                }

                elementosEtapas = elementosEtapas.Distinct().ToList();

                if (elementosEtapas.Count > 0)
                {
                    OrdenarElementosEtapa(ref elementosEtapas);
                    VerificarLimpiarElementosYaAgregados(ref elementosEtapas);
                }
            }

            //foreach (var itemEtapaOperacion in etapas)
            //{
            //    foreach (var itemEtapa in itemEtapaOperacion.Elementos)
            //    {
            //        if (itemEtapa.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
            //        {
            //            foreach (var texto in ((ElementoOperacionAritmeticaEjecucion)itemEtapa).TextosInformacion_SeleccionOrdenamiento)
            //                EstablecerTextosInformacion_SeleccionarOrdenar(itemEtapa, texto.FirstOrDefault());
            //        }
            //    }
            //}

            CalcularCantidadElementos();
            itemCalculo.CantidadEtapas = etapas.Count;
        }

        private bool RecorrerElementosPosteriores_BusquedaElemento(DiseñoOperacion elemento, DiseñoOperacion elementoBuscado)
        {
            foreach (var elementoPosterior in elemento.ElementosPosteriores)
            {
                if (RecorrerElementosPosteriores_BusquedaElemento(elementoPosterior, elementoBuscado))
                    return true;
            }

            if (elemento == elementoBuscado)
                return true;
            else
                return false;
        }

        private bool VerificarElementosAnteriores_Condiciones(DiseñoOperacion elemento, List<EtapaEjecucion> etapas, List<DiseñoOperacion> elementosEtapa)
        {
            if (elemento.Tipo == TipoElementoOperacion.CondicionesFlujo |
                elemento.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
            {
                List<DiseñoOperacion> elementosCondiciones = new List<DiseñoOperacion>();

                if (elemento.Tipo == TipoElementoOperacion.CondicionesFlujo)
                {
                    foreach (var itemCondicion in elemento.CondicionesFlujo_SeleccionOrdenamiento)
                    {
                        itemCondicion.AgregarCondicionElementoDiseñoCondicion_Condiciones(ref elementosCondiciones);
                    }
                }

                if (elemento.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                {
                    List<CondicionTextosInformacion> CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionTextosInformacion>();

                    foreach (var itemCondicion in elemento.CondicionesTextosInformacion_SeleccionOrdenamiento)
                    {
                        CondicionesTextosInformacion_SeleccionOrdenamiento.Add(itemCondicion.Condiciones);
                    }

                    foreach (var itemCondicion in CondicionesTextosInformacion_SeleccionOrdenamiento)
                    {
                        if(itemCondicion != null)
                            itemCondicion.AgregarCondicionElementoDiseñoCondicion_Condiciones(ref elementosCondiciones);
                    }
                }

                foreach (var itemElementoCondicion in elementosCondiciones)
                {
                    if (!elementosEtapa.Contains(itemElementoCondicion))
                    {
                        bool encontrado = false;

                        foreach (var itemEtapaCondicion in etapas)
                        {
                            if (itemEtapaCondicion.Elementos.Any(i => i.ElementoDiseñoRelacionado == itemElementoCondicion))
                            {
                                encontrado = true;
                                break;
                            }
                        }

                        if (!encontrado)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            else
            {
                var definicionesTextos = Calculo.TextosInformacion.ElementosTextosInformacion.Where(i => i.OperacionRelacionada == elemento).ToList();

                foreach(var definicionTextos in definicionesTextos)
                {
                    List<DiseñoOperacion> elementosCondiciones = new List<DiseñoOperacion>();

                    List<CondicionImplicacionTextosInformacion> CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionImplicacionTextosInformacion>();

                    foreach (var itemCondicion in definicionTextos.Relaciones_TextosInformacion)
                    {
                        CondicionesTextosInformacion_SeleccionOrdenamiento.Add(itemCondicion.Condiciones_TextoCondicion);
                    }

                    foreach (var itemCondicion in CondicionesTextosInformacion_SeleccionOrdenamiento)
                    {
                        if(itemCondicion != null)
                            itemCondicion.AgregarCondicionElementoDiseñoCondicion_Condiciones(ref elementosCondiciones);
                    }

                    foreach (var itemElementoCondicion in elementosCondiciones)
                    {
                        if (!elementosEtapa.Contains(itemElementoCondicion))
                        {
                            bool encontrado = false;

                            foreach (var itemEtapaCondicion in etapas)
                            {
                                if (itemEtapaCondicion.Elementos.Any(i => i.ElementoDiseñoRelacionado == itemElementoCondicion))
                                {
                                    encontrado = true;
                                    break;
                                }
                            }

                            if (!encontrado)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
                
        }

        private bool VerificarElementosAnteriores(DiseñoOperacion elemento, List<EtapaEjecucion> etapas, List<DiseñoOperacion> elementosEtapa)
        {

            foreach (var itemElementoCondicion in elemento.ElementosAnteriores)
            {
                if (!elementosEtapa.Contains(itemElementoCondicion))
                {
                    bool encontrado = false;

                    foreach (var itemEtapaCondicion in etapas)
                    {
                        if (itemEtapaCondicion.Elementos.Any(i => i.ElementoDiseñoRelacionado == itemElementoCondicion))
                        {
                            encontrado = true;
                            break;
                        }
                    }

                    if (!encontrado)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool RecorrerElementosPosteriores_BusquedaElemento_Operacion(DiseñoElementoOperacion elemento, DiseñoElementoOperacion elementoBuscado)
        {
            foreach (var elementoPosterior in elemento.ElementosPosteriores)
            {
                if (RecorrerElementosPosteriores_BusquedaElemento_Operacion(elementoPosterior, elementoBuscado))
                    return true;
            }

            if (elemento == elementoBuscado)
                return true;
            else
                return false;
        }

        private bool VerificarElementosAnteriores_Condiciones_Operacion(DiseñoElementoOperacion elemento, List<EtapaOperacionEjecucion> etapas, List<DiseñoElementoOperacion> elementosEtapa, DiseñoOperacion operacionContenedora)
        {
            if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion & (
                elemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                elemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado |
                elemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                elemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
            {
                List<DiseñoElementoOperacion> elementosCondiciones = new List<DiseñoElementoOperacion>();

                if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion & (
                elemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                elemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                {
                    foreach (var itemCondicion in elemento.CondicionesFlujo_SeleccionOrdenamiento)
                    {
                        itemCondicion.AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref elementosCondiciones);
                    }
                }

                if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion & (
                    elemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                elemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                {
                    List<CondicionTextosInformacion> CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionTextosInformacion>();

                    foreach (var itemCondicion in elemento.CondicionesTextosInformacion_SeleccionOrdenamiento)
                    {
                        CondicionesTextosInformacion_SeleccionOrdenamiento.Add(itemCondicion.Condiciones);
                    }

                    foreach (var itemCondicion in CondicionesTextosInformacion_SeleccionOrdenamiento)
                    {
                        if(itemCondicion != null)
                            itemCondicion.AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref elementosCondiciones);
                    }
                }

                foreach (var itemElementoCondicion in elementosCondiciones)
                {
                    if (!elementosEtapa.Contains(itemElementoCondicion))
                    {
                        bool encontrado = false;

                        foreach (var itemEtapaCondicion in etapas)
                        {
                            if (itemEtapaCondicion.Elementos.Any(i => i.ElementoDiseñoRelacionado == itemElementoCondicion))
                            {
                                encontrado = true;
                                break;
                            }
                        }

                        if (!encontrado)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            else
            {
                var definicionesTextos = Calculo.TextosInformacion.ElementosTextosInformacion.Where(i => i.OperacionRelacionada == operacionContenedora).ToList();

                foreach(var definicionTextos in definicionesTextos)
                {
                    var definicionesTextosElemento = definicionTextos.Definiciones_TextosInformacion.Where(i => i.ElementoRelacionado == elemento).ToList();

                    foreach(var definicionTextosElemento in definicionesTextosElemento)
                    {
                        List<DiseñoElementoOperacion> elementosCondiciones = new List<DiseñoElementoOperacion>();

                        List<CondicionImplicacionTextosInformacion> CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionImplicacionTextosInformacion>();

                        foreach (var itemCondicion in definicionTextosElemento.Relaciones_TextosInformacion)
                        {
                            CondicionesTextosInformacion_SeleccionOrdenamiento.Add(itemCondicion.Condiciones_TextoCondicion);
                        }

                        foreach (var itemCondicion in CondicionesTextosInformacion_SeleccionOrdenamiento)
                        {
                            if(itemCondicion != null)
                                itemCondicion.AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref elementosCondiciones);
                        }

                        foreach (var itemElementoCondicion in elementosCondiciones)
                        {
                            if (!elementosEtapa.Contains(itemElementoCondicion))
                            {
                                bool encontrado = false;

                                foreach (var itemEtapaCondicion in etapas)
                                {
                                    if (itemEtapaCondicion.Elementos.Any(i => i.ElementoDiseñoRelacionado == itemElementoCondicion))
                                    {
                                        encontrado = true;
                                        break;
                                    }
                                }

                                if (!encontrado)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                return false;
            }
                
        }

        private bool VerificarElementosAnteriores_Operacion(DiseñoElementoOperacion elemento, List<EtapaOperacionEjecucion> etapas, List<DiseñoElementoOperacion> elementosEtapa)
        {

            foreach (var itemElementoCondicion in elemento.ElementosAnteriores)
            {
                if (!elementosEtapa.Contains(itemElementoCondicion))
                {
                    bool encontrado = false;

                    foreach (var itemEtapaCondicion in etapas)
                    {
                        if (itemEtapaCondicion.Elementos.Any(i => i.ElementoDiseñoRelacionado == itemElementoCondicion))
                        {
                            encontrado = true;
                            break;
                        }
                    }

                    if (!encontrado)
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        private bool RecorrerElementosPosteriores_BusquedaElemento_Calculo(DiseñoCalculo elemento, DiseñoCalculo elementoBuscado)
        {
            foreach (var elementoPosterior in elemento.ElementosPosteriores)
            {
                if (RecorrerElementosPosteriores_BusquedaElemento_Calculo(elementoPosterior, elementoBuscado))
                    return true;
            }

            if (elemento == elementoBuscado)
                return true;
            else
                return false;
        }

        private void VerificarLimpiarElementosYaAgregados(ref List<DiseñoOperacion> elementos)
        {
            List<DiseñoOperacion> elementoYaAgregados = new List<DiseñoOperacion>();

            DiseñoOperacion elemento = null;
            int indice = 0;
            do
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                if (elementos.Count > 0)
                {
                    elemento = elementos[indice];

                    foreach (var itemEtapa in etapas)
                    {
                        elementoYaAgregados = (from E in itemEtapa.Elementos where E.HashCode_ElementoDiseñoOperacionCalculo == elemento.GetHashCode() select E.ElementoDiseñoRelacionado).ToList();
                        if (elementoYaAgregados.Count > 0) break;
                    }

                    if (elementoYaAgregados.Count > 0)
                    {
                        elementos.Remove(elemento);
                        indice--;
                    }
                }

                indice++;

            } while (indice <= elementos.Count - 1);
        }

        private void OrdenarElementosEtapa(ref List<DiseñoOperacion> elementos)
        {
            List<DiseñoOperacion> listaOrdenada = new List<DiseñoOperacion>();
            List<DiseñoOperacion> elementosEncontrados = new List<DiseñoOperacion>();

            bool tieneAnterior = false;
            foreach (var itemElemento in elementos)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (elementos.Contains(itemAnterior))
                    {
                        tieneAnterior = true;
                    }
                }

                if (!tieneAnterior)
                {
                    elementosEncontrados.Add(itemElemento);
                }

                tieneAnterior = false;
            }

            while (elementosEncontrados.Count > 0)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                listaOrdenada.AddRange(elementosEncontrados.Distinct().ToList());

                List<DiseñoOperacion> elementosPosterioresEncontrados = new List<DiseñoOperacion>();
                foreach (var itemElementoAnterior in elementosEncontrados)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    tieneAnterior = false;
                    foreach (var itemElemento in elementos)
                    {
                        if (itemElemento != itemElementoAnterior)
                        {
                            foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                            {
                                if (elementosEncontrados.Contains(itemAnterior))
                                {
                                    tieneAnterior = true;
                                }
                            }

                            if (tieneAnterior)
                            {
                                elementosPosterioresEncontrados.Add(itemElemento);
                                tieneAnterior = false;
                            }
                        }
                    }
                }

                elementosEncontrados.Clear();
                elementosEncontrados.AddRange(elementosPosterioresEncontrados.Distinct().ToList());
            }

            elementos.Clear();
            elementos.AddRange(listaOrdenada.Distinct().ToList());
        }

        private void PrepararElementosEtapas(ref List<DiseñoOperacion> elementosEtapas, ref EtapaEjecucion etapa)
        {
            foreach (var item in elementosEtapas)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                if (item.Tipo == TipoElementoOperacion.Nota | 
                    item.Tipo == TipoElementoOperacion.AgrupadorOperaciones) continue;
                
                //ElementoEjecucionCalculo elemento = null;
                
                if (item.Tipo == TipoElementoOperacion.Entrada)
                {
                    ElementoEntradaEjecucion entrada = null;

                    var infoEjecucionExterna = item.EntradaRelacionada.EjecucionesExternas_SubElementos_Config.FirstOrDefault(i => i.IDElementoAsociado == item.ID);

                    if (ModoEjecucionExterna &&
                        infoEjecucionExterna != null &&
                        infoEjecucionExterna.EjecucionNormal &&
                        infoEjecucionExterna.EjecucionTraspaso)
                    {
                        switch (item.EntradaRelacionada.Tipo)
                        {
                            case TipoEntrada.Numero:
                                if (item.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.NumeroFijo)
                                {
                                    item.EntradaRelacionada.Tipo = TipoEntrada.ConjuntoNumeros;
                                    item.EntradaRelacionada.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
                                    item.EntradaRelacionada.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo;
                                    item.EntradaRelacionada.ConjuntoNumerosFijo.Add(new EntidadNumero()
                                    {
                                        Numero = item.EntradaRelacionada.NumeroFijo,
                                        Textos = item.EntradaRelacionada.Textos.ToList()
                                    });

                                    item.ModoEjecucionExterna_CantidadesFijas = true;
                                }
                                
                            break;
                        }                        
                    }

                    switch (item.EntradaRelacionada.Tipo)
                    {
                        case TipoEntrada.Numero:
                            ElementoEntradaEjecucion numero = new ElementoEntradaEjecucion();
                            if(item.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.NumeroFijo)
                                numero.TipoEntradaConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo;
                            else if (item.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeDigita)
                                numero.TipoEntradaConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeDigita;
                            else if (item.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeObtiene)
                                numero.TipoEntradaConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeObtiene;

                            entrada = numero;

                            if (numero.TipoEntradaConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)
                            {
                                numero.Numeros.Clear();
                                numero.Numeros.Add(new EntidadNumero()
                                {
                                    Numero = item.EntradaRelacionada.NumeroFijo,
                                    Nombre = item.EntradaRelacionada.Nombre,
                                    Textos = item.EntradaRelacionada.Textos
                                });
                            }
                            else
                            {
                                entrada.Nombre = item.EntradaRelacionada.Nombre;
                            }

                            entrada.TipoEntrada = TipoEntrada.ConjuntoNumeros;

                            if (numero.TipoEntradaConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeObtiene)
                            {
                                if (item.EntradaRelacionada.ListaArchivos != null &&
                                    item.EntradaRelacionada.ListaArchivos.Any() &&
                                    item.EntradaRelacionada.ListaArchivos.FirstOrDefault() != null)
                                    item.EntradaRelacionada.ListaArchivos.FirstOrDefault().RutaArchivoConjuntoNumerosEntrada = item.EntradaRelacionada.ListaArchivos.FirstOrDefault().RutaArchivoEntrada;

                                if (item.EntradaRelacionada.BusquedaNumero != null)
                                    item.EntradaRelacionada.BusquedasConjuntoNumeros.Add(item.EntradaRelacionada.BusquedaNumero);
                            }

                            break;
                        case TipoEntrada.ConjuntoNumeros:
                            ElementoEntradaEjecucion numeros = new ElementoEntradaEjecucion();
                            numeros.TipoEntradaConjuntoNumeros = item.EntradaRelacionada.TipoOpcionConjuntoNumeros;
                            numeros.UtilizarCantidadNumeros = item.EntradaRelacionada.UtilizarCantidadNumeros;
                            numeros.OpcionCantidadNumeros = item.EntradaRelacionada.OpcionCantidadNumeros;
                            entrada = numeros;

                            if (numeros.TipoEntradaConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)
                            {
                                numeros.Numeros.Clear();
                                numeros.Numeros.AddRange(item.EntradaRelacionada.ConjuntoNumerosFijo.Select(i => i.CopiarObjeto()));
                                numeros.Nombre = item.EntradaRelacionada.Nombre;
                            }
                            else
                            {
                                entrada.Nombre = item.EntradaRelacionada.Nombre;
                            }

                            entrada.TipoEntrada = TipoEntrada.ConjuntoNumeros;

                            break;
                        case TipoEntrada.TextosInformacion:
                            ElementoConjuntoTextosEntradaEjecucion textos = new ElementoConjuntoTextosEntradaEjecucion();
                            textos.TipoEntradaConjuntoTextos = item.EntradaRelacionada.TipoOpcionTextosInformacion;
                            entrada = textos;

                            if (textos.TipoEntradaConjuntoTextos == TipoOpcionTextosInformacionEntrada.TextosInformacionFijos)
                            {
                                textos.FilasTextosInformacion.Clear();
                                textos.FilasTextosInformacion.AddRange(item.EntradaRelacionada.ConjuntoTextosInformacionFijos);
                                textos.Nombre = item.EntradaRelacionada.Nombre;
                            }
                            else
                            {
                                entrada.Nombre = item.EntradaRelacionada.Nombre;
                            }

                            entrada.TipoEntrada = TipoEntrada.TextosInformacion;

                            break;
                        case TipoEntrada.Calculo:
                            if (item.EntradaRelacionada.ElementoSalidaCalculoAnterior.Tipo != TipoElementoOperacion.Nota)
                            {
                                DiseñoOperacion ElementoSalidaCalculoAnterior = null;

                                if (item.EntradaRelacionada.ElementoSalidaCalculoAnterior.Tipo == TipoElementoOperacion.Entrada)
                                {
                                    switch (item.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.Tipo)
                                    {
                                        case TipoEntrada.Numero:
                                            ElementoEntradaEjecucion numeroDesdeCalculo = new ElementoEntradaEjecucion();
                                            if (item.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.NumeroFijo)
                                                numeroDesdeCalculo.TipoEntradaConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo;
                                            else if (item.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeDigita)
                                                numeroDesdeCalculo.TipoEntradaConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeDigita;
                                            entrada = numeroDesdeCalculo;

                                            numeroDesdeCalculo.Numeros.Clear();
                                            numeroDesdeCalculo.Numeros.Add(new EntidadNumero()
                                            {
                                                Numero = item.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.NumeroFijo,
                                                Nombre = item.EntradaRelacionada.Nombre
                                            });

                                            entrada.TipoEntrada = TipoEntrada.ConjuntoNumeros;
                                            ElementoSalidaCalculoAnterior = item.EntradaRelacionada.ElementoSalidaCalculoAnterior;

                                            break;
                                        case TipoEntrada.ConjuntoNumeros:
                                            ElementoEntradaEjecucion numerosDesdeCalculo = new ElementoEntradaEjecucion();
                                            numerosDesdeCalculo.TipoEntradaConjuntoNumeros = item.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOpcionConjuntoNumeros;
                                            entrada = numerosDesdeCalculo;

                                            if (numerosDesdeCalculo.TipoEntradaConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)
                                            {
                                                numerosDesdeCalculo.Numeros.Clear();
                                                numerosDesdeCalculo.Numeros.AddRange(item.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConjuntoNumerosFijo);
                                                numerosDesdeCalculo.Nombre = item.EntradaRelacionada.Nombre; //+ " desde " + item.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.Nombre;
                                            }
                                            else
                                            {
                                                entrada.Nombre = item.EntradaRelacionada.Nombre; //+ " desde " + item.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.Nombre;
                                            }

                                            entrada.TipoEntrada = TipoEntrada.ConjuntoNumeros;
                                            ElementoSalidaCalculoAnterior = item.EntradaRelacionada.ElementoSalidaCalculoAnterior;

                                            break;
                                        case TipoEntrada.Calculo:

                                            ElementoEntradaEjecucion numerosDesdeCalculo_Entrada = new ElementoEntradaEjecucion();
                                            numerosDesdeCalculo_Entrada.TipoEntradaConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo;

                                            entrada = numerosDesdeCalculo_Entrada;
                                            entrada.TipoEntrada = TipoEntrada.ConjuntoNumeros;
                                            
                                            entrada.Nombre = item.EntradaRelacionada.Nombre;
                                            ElementoSalidaCalculoAnterior = item.EntradaRelacionada.ElementoSalidaCalculoAnterior;

                                            break;
                                    }
                                }
                                else
                                {
                                    ElementoSalidaCalculoAnterior = item.EntradaRelacionada.ElementoSalidaCalculoAnterior;

                                    ElementoEntradaEjecucion numerosDesdeCalculo = new ElementoEntradaEjecucion();
                                    numerosDesdeCalculo.TipoEntradaConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo;

                                    entrada = numerosDesdeCalculo;
                                    entrada.TipoEntrada = TipoEntrada.ConjuntoNumeros;
                                    
                                    entrada.Nombre = item.EntradaRelacionada.Nombre; //+ " desde " + operacionDesdeCalculo.Nombre;
                                }


                                DiseñoCalculo itemCalculoAnterior = null;
                                foreach (var itemEtapa in etapasCalculo)
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    if (itemCalculoAnterior == null)
                                        itemCalculoAnterior = (from C in itemEtapa.Elementos where C.ElementoDiseñoCalculoRelacionado.ElementosOperaciones.Contains(ElementoSalidaCalculoAnterior) select C.ElementoDiseñoCalculoRelacionado).FirstOrDefault();
                                }
                                entrada.ElementoDiseñoCalculoRelacionado = itemCalculoAnterior;

                            }
                            break;
                    }

                    if (entrada != null)
                    {
                        entrada.TextosInformacionFijos.AddRange(GenerarTextosInformacion(item.EntradaRelacionada.TextosInformacionFijos));
                        entrada.TextosInformacion_OperacionInterna.AddRange(GenerarTextosInformacion(item.EntradaRelacionada.TextosInformacion_OperacionInterna));


                        bool origenDatos = false;
                        switch (entrada.TipoEntrada)
                        {
                            case TipoEntrada.ConjuntoNumeros:
                                if (((ElementoEntradaEjecucion)entrada).TipoEntradaConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeObtiene)
                                {
                                    origenDatos = true;
                                }
                                break;
                            case TipoEntrada.TextosInformacion:
                                if (((ElementoConjuntoTextosEntradaEjecucion)entrada).TipoEntradaConjuntoTextos == TipoOpcionTextosInformacionEntrada.SeObtiene)
                                {
                                    origenDatos = true;
                                }
                                break;
                        }

                        if (origenDatos)
                        {
                            foreach (var itemOrigenDatos in item.EntradaRelacionada.ListaArchivos)
                            {
                                if (Pausada) Pausar();
                                if (Detenida) return;

                                switch (item.EntradaRelacionada.TipoOrigenDatos)
                                {
                                    case TipoOrigenDatos.Archivo:
                                        var registroCantidadesObtenidas_UltimaEjecucion = App.CargarElementosObtenidas_Entradas_UltimaEjecucion(Calculo.ID, item.EntradaRelacionada.ID);

                                        ElementoArchivoOrigenDatosEjecucion archivo = new ElementoArchivoOrigenDatosEjecucion(
                                            registroCantidadesObtenidas_UltimaEjecucion.CantidadNumeros_Obtenidos_UltimaEjecucion,
                                            registroCantidadesObtenidas_UltimaEjecucion.CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
                                            registroCantidadesObtenidas_UltimaEjecucion.PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
                                            registroCantidadesObtenidas_UltimaEjecucion.PosicionFinalNumeros_Obtenidos_UltimaEjecucion);

                                        archivo.TipoOrigenDatos = TipoOrigenDatos.Archivo;
                                        archivo.TipoArchivo = itemOrigenDatos.TipoArchivo;
                                        archivo.CredencialesFTP = item.EntradaRelacionada.CredencialesFTP;
                                        archivo.TipoOpcionBusqueda = item.EntradaRelacionada.OpcionBusquedaNumero;
                                        archivo.MismaLecturaArchivo = item.EntradaRelacionada.MismaLecturaBusquedasArchivo;
                                        archivo.ConfiguracionSeparadores = item.EntradaRelacionada.ConfiguracionSeparadores;
                                        archivo.ConfiguracionSeleccionCarpeta = itemOrigenDatos.ConfiguracionSeleccionCarpeta;
                                        archivo.ConfiguracionSeleccionArchivo = itemOrigenDatos.ConfiguracionSeleccionArchivo;
                                        archivo.ConfigSeleccionarArchivo = item.EntradaRelacionada.ConfigSeleccionarArchivoURL;
                                        archivo.LecturasNavegaciones = itemOrigenDatos.LecturasNavegaciones.ToList();
                                        archivo.EstablecerLecturasNavegaciones_Busquedas = itemOrigenDatos.EstablecerLecturasNavegaciones_Busquedas;
                                        archivo.TipoFormatoArchivo_Entrada = item.EntradaRelacionada.TipoFormatoArchivo_Entrada;
                                        archivo.ParametrosWord = itemOrigenDatos.ParametrosWord;
                                        archivo.EntradaWord = item.EntradaRelacionada.EntradaWord;
                                        archivo.UsarURL_Office = itemOrigenDatos.UsarURLOffice_original;
                                        archivo.URLOffice_Original = itemOrigenDatos.URLOffice_Original;
                                        archivo.TextosInformacionFijos = itemOrigenDatos.TextosInformacionFijos.ToList();
                                        archivo.EsperarArchivos = itemOrigenDatos.EsperarArchivos;
                                        archivo.TiempoEspera = itemOrigenDatos.TiempoEspera;
                                        archivo.TipoTiempoEspera = itemOrigenDatos.TipoTiempoEspera;
                                        archivo.IncluirTextosInformacion_DeNombresRutasArchivos = itemOrigenDatos.IncluirTextosInformacion_DeNombresRutasArchivos;
                                        archivo.EntradaManual = itemOrigenDatos.EntradaManual;
                                        archivo.ElementoOrigenDatosAsociado_Diseño = itemOrigenDatos;

                                        switch (entrada.TipoEntrada)
                                        {
                                            case TipoEntrada.ConjuntoNumeros:
                                                archivo.RutaArchivo = itemOrigenDatos.RutaArchivoConjuntoNumerosEntrada;
                                                foreach (var itemBusqueda in item.EntradaRelacionada.BusquedasConjuntoNumeros)
                                                {
                                                    BusquedaArchivoEjecucion busqueda = BusquedaArchivoEjecucion.PrepararBusquedas(itemBusqueda, this);
                                                    archivo.Busquedas.Add(busqueda);

                                                }
                                                entrada.CantidadSubElementos = archivo.Busquedas.Count + archivo.Busquedas.Sum(i => i.ConjuntoBusquedas.Count);
                                                break;

                                            case TipoEntrada.TextosInformacion:
                                                archivo.RutaArchivo = itemOrigenDatos.RutaArchivoConjuntoTextosEntrada;
                                                foreach (var itemBusqueda in item.EntradaRelacionada.BusquedasTextosInformacion)
                                                {
                                                    BusquedaArchivoEjecucion busqueda = BusquedaArchivoEjecucion.PrepararBusquedas(itemBusqueda, this);
                                                    archivo.Busquedas.Add(busqueda);

                                                }
                                                entrada.CantidadSubElementos = archivo.Busquedas.Count + archivo.Busquedas.Sum(i => i.ConjuntoBusquedas.Count);
                                                break;
                                        }

                                        entrada.OrigenesDatos.Add(archivo);
                                        break;
                                }
                            }

                            foreach (var itemOrigenDatos in item.EntradaRelacionada.ListaURLs)
                            {
                                if (Pausada) Pausar();
                                if (Detenida) return;

                                switch (item.EntradaRelacionada.TipoOrigenDatos)
                                {
                                    case TipoOrigenDatos.DesdeInternet:
                                        var registroCantidadesObtenidas_UltimaEjecucion_2 = App.CargarElementosObtenidas_Entradas_UltimaEjecucion(Calculo.ID, item.EntradaRelacionada.ID);

                                        ElementoInternetOrigenDatosEjecucion url = new ElementoInternetOrigenDatosEjecucion(
                                            registroCantidadesObtenidas_UltimaEjecucion_2.CantidadNumeros_Obtenidos_UltimaEjecucion,
                                            registroCantidadesObtenidas_UltimaEjecucion_2.CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
                                            registroCantidadesObtenidas_UltimaEjecucion_2.PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
                                            registroCantidadesObtenidas_UltimaEjecucion_2.PosicionFinalNumeros_Obtenidos_UltimaEjecucion);

                                        url.TipoOrigenDatos = TipoOrigenDatos.DesdeInternet;
                                        url.TipoOpcionBusqueda = item.EntradaRelacionada.OpcionBusquedaNumero;
                                        url.MismaLecturaArchivo = item.EntradaRelacionada.MismaLecturaBusquedasArchivo;
                                        url.ObjetoURL = new ObjetoURL(itemOrigenDatos.URLEntrada, itemOrigenDatos.ParametrosURL, itemOrigenDatos.HeadersURL);
                                        url.ConfiguracionSeparadores = item.EntradaRelacionada.ConfiguracionSeparadores;
                                        url.ConfigEscribirURL = item.EntradaRelacionada.ConfigSeleccionarArchivoURL;
                                        url.ConfiguracionEscribirURL = itemOrigenDatos.ConfiguracionEscribirURL;
                                        url.EstablecerParametrosEjecucion = itemOrigenDatos.EstablecerParametrosEjecucion;
                                        url.LecturasNavegaciones = itemOrigenDatos.LecturasNavegaciones.ToList();
                                        url.EstablecerLecturasNavegaciones_Busquedas = itemOrigenDatos.EstablecerLecturasNavegaciones_Busquedas;
                                        url.TextosInformacionFijos = itemOrigenDatos.TextosInformacionFijos.ToList();

                                        switch (entrada.TipoEntrada)
                                        {
                                            case TipoEntrada.ConjuntoNumeros:
                                                url.URL = itemOrigenDatos.URLEntrada;
                                                foreach (var itemBusqueda in item.EntradaRelacionada.BusquedasConjuntoNumeros)
                                                {
                                                    BusquedaArchivoEjecucion busqueda = BusquedaArchivoEjecucion.PrepararBusquedas(itemBusqueda, this);
                                                    url.Busquedas.Add(busqueda);

                                                }
                                                entrada.CantidadSubElementos = url.Busquedas.Count + url.Busquedas.Sum(i => i.ConjuntoBusquedas.Count);
                                                break;

                                            case TipoEntrada.TextosInformacion:
                                                url.URL = itemOrigenDatos.URLEntrada;
                                                foreach (var itemBusqueda in item.EntradaRelacionada.BusquedasTextosInformacion)
                                                {
                                                    BusquedaArchivoEjecucion busqueda = BusquedaArchivoEjecucion.PrepararBusquedas(itemBusqueda, this);
                                                    url.Busquedas.Add(busqueda);

                                                }
                                                entrada.CantidadSubElementos = url.Busquedas.Count + url.Busquedas.Sum(i => i.ConjuntoBusquedas.Count);
                                                break;
                                        }

                                        entrada.OrigenesDatos.Add(url);
                                        break;
                                }
                            }

                            switch (item.EntradaRelacionada.TipoOrigenDatos)
                            {
                                case TipoOrigenDatos.Excel:
                                    var registroCantidadesObtenidas_UltimaEjecucion_3 = App.CargarElementosObtenidas_Entradas_UltimaEjecucion(Calculo.ID, item.EntradaRelacionada.ID);

                                    ElementoArchivoExcelOrigenDatosEjecucion excel = new ElementoArchivoExcelOrigenDatosEjecucion(
                                        registroCantidadesObtenidas_UltimaEjecucion_3.CantidadNumeros_Obtenidos_UltimaEjecucion,
                                        registroCantidadesObtenidas_UltimaEjecucion_3.CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
                                        registroCantidadesObtenidas_UltimaEjecucion_3.PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
                                        registroCantidadesObtenidas_UltimaEjecucion_3.PosicionFinalNumeros_Obtenidos_UltimaEjecucion);

                                    excel.TipoOrigenDatos = TipoOrigenDatos.Excel;
                                    excel.ParametrosExcel = item.EntradaRelacionada.ParametrosExcel;
                                    excel.ConfiguracionSeleccionCarpeta = item.EntradaRelacionada.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionCarpeta;
                                    excel.ConfiguracionSeleccionArchivo = item.EntradaRelacionada.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionArchivo;
                                    excel.ConfigSeleccionarArchivo = item.EntradaRelacionada.ConfigSeleccionarArchivoURL;
                                    excel.UsarURLLibro = item.EntradaRelacionada.UsarURL_Office;
                                    
                                    switch (entrada.TipoEntrada)
                                    {
                                        case TipoEntrada.Numero:
                                            excel.RutaArchivo = item.EntradaRelacionada.ListaArchivos.FirstOrDefault().RutaArchivoEntrada;

                                            break;
                                        case TipoEntrada.ConjuntoNumeros:
                                            excel.RutaArchivo = item.EntradaRelacionada.ListaArchivos.FirstOrDefault().RutaArchivoConjuntoNumerosEntrada;

                                            break;
                                    }

                                    entrada.CantidadSubElementos = item.EntradaRelacionada.ParametrosExcel.Count;

                                    entrada.OrigenesDatos.Add(excel);
                                    break;
                            }
                        }

                        switch (item.EntradaRelacionada.OperacionInterna)
                        {
                            case TipoElementoOperacion.Ninguna:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Ninguna;
                                break;

                            case TipoElementoOperacion.Suma:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Suma;
                                break;

                            case TipoElementoOperacion.Resta:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Resta;
                                break;

                            case TipoElementoOperacion.Multiplicacion:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Multiplicacion;
                                break;

                            case TipoElementoOperacion.Division:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Division;
                                break;

                            case TipoElementoOperacion.Porcentaje:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Porcentaje;
                                break;

                            case TipoElementoOperacion.Potencia:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Potencia;
                                break;

                            case TipoElementoOperacion.Raiz:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Raiz;
                                break;

                            case TipoElementoOperacion.Logaritmo:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Logaritmo;
                                break;

                            case TipoElementoOperacion.Factorial:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Factorial;
                                break;

                            case TipoElementoOperacion.Inverso:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.Inverso;
                                break;

                            case TipoElementoOperacion.ContarCantidades:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.ContarCantidades;
                                break;

                            case TipoElementoOperacion.SeleccionarOrdenar:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar;
                                break;

                            case TipoElementoOperacion.RedondearCantidades:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.RedondearCantidades;
                                break;

                            case TipoElementoOperacion.LimpiarDatos:
                                entrada.OperacionInterna = TipoOperacionAritmeticaEjecucion.LimpiarDatos;
                                break;
                        }

                        entrada.ElementoDiseñoRelacionado = item;


                        //elemento = entrada;
                        entrada.Tipo = TipoElementoEjecucion.Entrada;
                        entrada.HashCode_ElementoDiseñoOperacionCalculo = item.GetHashCode();
                        entrada.ContieneSalida_Ejecucion = item.ContieneSalida;
                        etapa.Elementos.Add(entrada);
                    }
                }
                else if(item.Tipo != TipoElementoOperacion.Salida)
                {
                    ElementoOperacionAritmeticaEjecucion operacion = new ElementoOperacionAritmeticaEjecucion();
                    switch (item.Tipo)
                    {
                        case TipoElementoOperacion.Suma:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Suma;
                            break;
                        case TipoElementoOperacion.Resta:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Resta;
                            break;
                        case TipoElementoOperacion.Multiplicacion:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Multiplicacion;
                            break;
                        case TipoElementoOperacion.Division:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Division;
                            break;
                        case TipoElementoOperacion.Porcentaje:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Porcentaje;
                            break;
                        case TipoElementoOperacion.Potencia:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Potencia;
                            break;
                        case TipoElementoOperacion.Raiz:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Raiz;
                            break;
                        case TipoElementoOperacion.Logaritmo:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Logaritmo;
                            break;
                        case TipoElementoOperacion.Factorial:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Factorial;
                            break;
                        case TipoElementoOperacion.Inverso:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Inverso;
                            break;
                        case TipoElementoOperacion.ContarCantidades:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.ContarCantidades;
                            break;
                        case TipoElementoOperacion.SeleccionarOrdenar:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar;
                            operacion.DefinicionSimple_TextosInformacion = item.DefinicionSimple_TextosInformacion;

                            List<CondicionTextosInformacion> listaCondicionesTextos = new List<CondicionTextosInformacion>();

                            foreach (var itemCondiciones in item.CondicionesTextosInformacion_SeleccionOrdenamiento)
                            {
                                if (Pausada) Pausar();
                                if (Detenida) return;

                                if (itemCondiciones.Condiciones != null)
                                {
                                    itemCondiciones.Condiciones.PrepararTextosBusquedas();
                                    itemCondiciones.Condiciones.Operandos_AplicarCondiciones.AddRange(itemCondiciones.Operandos_AplicarCondiciones);
                                    //itemCondiciones.Condiciones.SubOperandos_AplicarCondiciones.AddRange(itemCondiciones.SubOperandos_AplicarCondiciones);
                                    listaCondicionesTextos.Add(itemCondiciones.Condiciones);
                                }
                            }

                            operacion.CondicionesTextosInformacion_SeleccionOrdenamiento.AddRange(listaCondicionesTextos);
                            break;
                        case TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;

                            //listaTextos = new List<List<string>>();
                            //textosInformacion = GenerarTextosInformacion(item.TextosInformacion_SeleccionOrdenamiento);

                            //foreach (var itemTexto in textosInformacion)
                            //{
                            //    listaTextos.Add(new List<string>() { itemTexto });
                            //}

                            //operacion.TextosInformacion_SeleccionOrdenamiento.AddRange(listaTextos);

                            break;
                        case TipoElementoOperacion.CondicionesFlujo:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.CondicionFlujo;
                            operacion.DefinicionSimple_CondicionesFlujo = item.DefinicionSimple_CondicionesFlujo;

                            operacion.CondicionesFlujo_SeleccionOrdenamiento.AddRange(item.CondicionesFlujo_SeleccionOrdenamiento);
                            break;

                        case TipoElementoOperacion.SeleccionarEntradas:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.SeleccionarEntradas;

                            operacion.CondicionesTextosInformacion_SeleccionEntradas.AddRange(item.CondicionesTextosInformacion_SeleccionEntradas);
                            break;

                        case TipoElementoOperacion.Espera:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Espera;
                            break;

                        case TipoElementoOperacion.LimpiarDatos:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.LimpiarDatos;
                            break;

                        case TipoElementoOperacion.RedondearCantidades:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.RedondearCantidades;
                            break;

                        case TipoElementoOperacion.ArchivoExterno:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.ArchivoExterno;
                            break;

                        case TipoElementoOperacion.SubCalculo:
                            operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.SubCalculo;
                            break;
                    }

                    operacion.ElementoDiseñoRelacionado = item;
                    //if (operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                    //{
                    operacion.Relaciones_TextosInformacion = ObtenerDefinicionesTextosInformacion_Operacion(operacion);
                    //}

                    //if (etapas.Count > 0)
                    //{
                    //List<int> HashCodes = new List<int>();
                    //foreach (var itemAnterior in item.ElementosAnteriores)
                    //{
                    //    foreach (var itemEtapa in etapas)
                    //    {
                    //        foreach (var itemEjecucion in itemEtapa.Elementos)
                    //        {
                    //            if (itemAnterior.GetHashCode() == itemEjecucion.HashCode_ElementoDiseñoOperacionCalculo) //& 
                    //                                                                                                     //itemEjecucion.ElementoDiseñoRelacionado.ElementosContenedoresOperacion.Contains(item))                                                                                //!itemEjecucion.HashCodes_ElementoDiseñoOperacion_Operacion.Contains(item.GetHashCode()))//)
                    //            {
                    //                //if (!HashCodes.Contains(itemEjecucion.HashCode_ElementoDiseñoOperacion))
                    //                //{
                    //                //foreach (var itemEtapa_Set in etapas)
                    //                //    foreach (var itemEjecucion_Set in itemEtapa.Elementos)
                    //                //        if (itemEjecucion_Set.HashCode_ElementoDiseñoOperacion == itemEjecucion.HashCode_ElementoDiseñoOperacion &
                    //                //            itemEjecucion.HashCode_ElementoDiseñoOperacion_Operacion == 0 & 
                    //                //            itemEjecucion != itemEjecucion_Set)
                    //                //            itemEjecucion_Set.HashCode_ElementoDiseñoOperacion_Operacion = item.GetHashCode();

                    //                //itemEjecucion.HashCodes_ElementoDiseñoOperacion_Operacion.Add(item.GetHashCode());
                    //                operacion.ElementosOperacion.Add(itemEjecucion);
                    //                //    HashCodes.Add(item.GetHashCode());
                    //                //}
                    //            }
                    //        }
                    //    }
                    //}

                    //operacion.ElementosOperacion = operacion.ElementosOperacion.Distinct().ToList();
                    //}

                    //if (operacion.Etapas.Count == 0)
                    //    operacion.CantidadSubElementos = operacion.ElementosOperacion.Count;
                    //else
                    //{
                    //    foreach (var itemOperEtapa in operacion.Etapas)
                    //        operacion.CantidadSubElementos += itemOperEtapa.Elementos.Count;
                    //}

                    //if (operacion.ElementosOperacion.Count > 1) operacion.EsConjuntoNumeros = true;

                    operacion.Nombre = item.Nombre;
                    //PrepararSubElementosOperacion(operacion);

                    //elemento = operacion;
                    operacion.Tipo = TipoElementoEjecucion.OperacionAritmetica;
                    operacion.HashCode_ElementoDiseñoOperacionCalculo = item.GetHashCode();
                    operacion.ContieneSalida_Ejecucion = item.ContieneSalida;
                    etapa.Elementos.Add(operacion);
                }
                
            }
        }

        private void PrepararEtapasCalculos()
        {
            var elementosEtapas = (from DiseñoCalculo E in Calculo.Calculos where E.ElementosAnteriores.Count == 0 select E).ToList();

            List<DiseñoCalculo> elementosIteraciones = new List<DiseñoCalculo>();

            while (elementosEtapas.Count > 0)
            {
                EtapaEjecucion etapa = new EtapaEjecucion();
                PrepararElementosEtapasCalculo(ref elementosEtapas, ref etapa);

                etapasCalculo.Add(etapa);

                List<DiseñoCalculo> elementosAnteriores = elementosEtapas.ToList();
                elementosEtapas.Clear();

                elementosAnteriores.AddRange(elementosIteraciones);
                elementosIteraciones.Clear();

                foreach (var itemAnterior in elementosAnteriores)
                {
                    elementosEtapas.AddRange((from DiseñoCalculo E in Calculo.Calculos
                                              where E.ElementosAnteriores.Contains(itemAnterior) & itemAnterior.ElementosPosteriores.Contains(E)
                                              select E).ToList());

                    List<DiseñoCalculo> elementosAQuitar = new List<DiseñoCalculo>();

                    foreach (var elementoVerificar_Posteriores in elementosEtapas)
                    {
                        foreach (var elementoPosterior_Buscado in elementosEtapas)
                        {
                            if (elementoVerificar_Posteriores != elementoPosterior_Buscado)
                            {
                                if (RecorrerElementosPosteriores_BusquedaElemento_Calculo(elementoVerificar_Posteriores, elementoPosterior_Buscado))
                                {
                                    elementosAQuitar.Add(elementoPosterior_Buscado);
                                }
                            }
                        }
                    }

                    while (elementosAQuitar.Any())
                    {
                        elementosIteraciones.Add(elementosAQuitar.First());
                        elementosEtapas.Remove(elementosAQuitar.First());
                        elementosAQuitar.Remove(elementosAQuitar.First());
                    }
                }

                elementosEtapas = elementosEtapas.Distinct().ToList();

                if (elementosEtapas.Count > 0)
                {
                    OrdenarElementosEtapa_Calculo(ref elementosEtapas);
                    VerificarLimpiarElementosYaAgregados_Calculo(ref elementosEtapas);
                }

            }


            CalcularCantidadElementosCalculo();
        }

        private void PrepararConexionesElementosEtapas_Calculo()
        {
            foreach (var itemEtapa in etapas)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                foreach (var item in itemEtapa.Elementos)
                {
                    if (item.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Nota |
                        item.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.AgrupadorOperaciones) continue;
                    
                    if (item.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.Entrada)
                    {
                        ElementoOperacionAritmeticaEjecucion operacion = (ElementoOperacionAritmeticaEjecucion)item;
                        switch (item.ElementoDiseñoRelacionado.Tipo)
                        {
                            case TipoElementoOperacion.Suma:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Suma;
                                break;
                            case TipoElementoOperacion.Resta:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Resta;
                                break;
                            case TipoElementoOperacion.Multiplicacion:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Multiplicacion;
                                break;
                            case TipoElementoOperacion.Division:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Division;
                                break;
                            case TipoElementoOperacion.Porcentaje:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Porcentaje;
                                break;
                            case TipoElementoOperacion.Potencia:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Potencia;
                                break;
                            case TipoElementoOperacion.Raiz:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Raiz;
                                break;
                            case TipoElementoOperacion.Logaritmo:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Logaritmo;
                                break;
                            case TipoElementoOperacion.Factorial:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Factorial;
                                break;
                            case TipoElementoOperacion.Inverso:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Inverso;
                                break;
                            case TipoElementoOperacion.ContarCantidades:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.ContarCantidades;
                                break;
                            case TipoElementoOperacion.SeleccionarOrdenar:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar;
                                break;
                            case TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;
                                break;
                            case TipoElementoOperacion.CondicionesFlujo:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.CondicionFlujo;
                                break;
                            case TipoElementoOperacion.SeleccionarEntradas:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.SeleccionarEntradas;
                                break;
                            case TipoElementoOperacion.Espera:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.Espera;
                                break;
                            case TipoElementoOperacion.LimpiarDatos:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.LimpiarDatos;
                                break;
                            case TipoElementoOperacion.RedondearCantidades:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.RedondearCantidades;
                                break;
                            case TipoElementoOperacion.ArchivoExterno:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.ArchivoExterno;
                                break;
                            case TipoElementoOperacion.SubCalculo:
                                operacion.TipoOperacion = TipoOperacionAritmeticaEjecucion.SubCalculo;
                                break;
                        }
                                                
                        {
                            List<DiseñoOperacion> itemsOrdenados = item.ElementoDiseñoRelacionado.ElementosAnteriores.OrderBy((i) =>
                            (from O in i.OrdenOperandos where O.ElementoPadre == item.ElementoDiseñoRelacionado select O).FirstOrDefault().Orden).ToList();

                            foreach (var itemAnterior in itemsOrdenados)
                            {
                                foreach (var itemEtapa_Anteriores in etapas)
                                {
                                    foreach (var itemEjecucion in itemEtapa_Anteriores.Elementos)
                                    {
                                        if (itemAnterior.GetHashCode() == itemEjecucion.HashCode_ElementoDiseñoOperacionCalculo)
                                        {
                                            if (!operacion.ElementosOperacion.Contains(itemEjecucion))
                                                operacion.ElementosOperacion.Add((ElementoOperacionAritmeticaEjecucion)itemEjecucion);
                                        }
                                    }
                                }
                            }
                        }
                        
                        if (operacion.Etapas.Count == 0)
                            operacion.CantidadSubElementos = operacion.ElementosOperacion.Count;
                        else
                        {
                            foreach (var itemOperEtapa in operacion.Etapas)
                                operacion.CantidadSubElementos += itemOperEtapa.Elementos.Count;
                        }
                    }
                }
            }
        }

        private void PrepararSubElementosEtapas_Calculo()
        {
            foreach (var itemEtapa in etapas)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                foreach (var item in itemEtapa.Elementos)
                {
                    if (item.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Nota |
                        item.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.AgrupadorOperaciones) continue;

                    if (item.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.Entrada &
                        item.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.Salida)
                    {
                        ElementoOperacionAritmeticaEjecucion operacion = (ElementoOperacionAritmeticaEjecucion)item;
                        
                        PrepararSubElementosOperacion(operacion);
                    }
                }
            }
        }

        private void PrepararConexionesElementosEtapas()
        {
            foreach (var itemEtapa in etapasCalculo)
            {
                foreach (var item in itemEtapa.Elementos)
                {
                    ElementoCalculoEjecucion calculo = (ElementoCalculoEjecucion)item;

                    foreach (var itemAnterior in item.ElementoDiseñoCalculoRelacionado.ElementosAnteriores)
                    {
                        foreach (var itemEjecucion in etapasCalculo.Last().Elementos)
                        {
                            if (itemAnterior.GetHashCode() == itemEjecucion.HashCode_ElementoDiseñoOperacionCalculo)
                            {
                                calculo.ElementosCalculo.Add(itemEjecucion);
                            }
                        }
                    }

                    calculo.ElementosCalculo = calculo.ElementosCalculo.Distinct().ToList();
                }
            }
        }
        private void PrepararElementosEtapasCalculo(ref List<DiseñoCalculo> elementosEtapas, ref EtapaEjecucion etapa)
        {
            foreach (var item in elementosEtapas)
            {
                ElementoEjecucionCalculo elemento = null;

                ElementoCalculoEjecucion calculo = new ElementoCalculoEjecucion();

                calculo.ElementoDiseñoCalculoRelacionado = item;
                elemento = calculo;

                elemento.Tipo = TipoElementoEjecucion.Calculo;
                elemento.HashCode_ElementoDiseñoOperacionCalculo = item.GetHashCode();
                etapa.Elementos.Add(elemento);
            }
        }

        private void VerificarLimpiarElementosYaAgregados_Calculo(ref List<DiseñoCalculo> elementos)
        {
            List<DiseñoCalculo> elementoYaAgregados = new List<DiseñoCalculo>();

            DiseñoCalculo elemento = null;
            int indice = 0;
            do
            {
                if (elementos.Count > 0)
                {
                    elemento = elementos[indice];

                    foreach (var itemEtapa in etapasCalculo)
                    {
                        elementoYaAgregados = (from E in itemEtapa.Elementos where E.HashCode_ElementoDiseñoOperacionCalculo == elemento.GetHashCode() select E.ElementoDiseñoCalculoRelacionado).ToList();
                        if (elementoYaAgregados.Count > 0) break;
                    }

                    if (elementoYaAgregados.Count > 0)
                    {
                        elementos.Remove(elemento);
                        indice--;
                    }
                }

                indice++;

            } while (indice <= elementos.Count - 1 & elementos.Count > 0);
        }

        private void OrdenarElementosEtapa_Calculo(ref List<DiseñoCalculo> elementos)
        {
            List<DiseñoCalculo> listaOrdenada = new List<DiseñoCalculo>();
            List<DiseñoCalculo> elementosEncontrados = new List<DiseñoCalculo>();

            bool tieneAnterior = false;
            foreach (var itemElemento in elementos)
            {
                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (elementos.Contains(itemAnterior))
                    {
                        tieneAnterior = true;
                    }
                }

                if (!tieneAnterior)
                {
                    elementosEncontrados.Add(itemElemento);
                }

                tieneAnterior = false;
            }

            while (elementosEncontrados.Count > 0)
            {
                listaOrdenada.AddRange(elementosEncontrados.Distinct().ToList());

                List<DiseñoCalculo> elementosPosterioresEncontrados = new List<DiseñoCalculo>();
                foreach (var itemElementoAnterior in elementosEncontrados)
                {
                    tieneAnterior = false;
                    foreach (var itemElemento in elementos)
                    {
                        if (itemElemento != itemElementoAnterior)
                        {
                            foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                            {
                                if (elementosEncontrados.Contains(itemAnterior))
                                {
                                    tieneAnterior = true;
                                }
                            }

                            if (tieneAnterior)
                            {
                                elementosPosterioresEncontrados.Add(itemElemento);
                                tieneAnterior = false;
                            }
                        }
                    }
                }

                elementosEncontrados.Clear();
                elementosEncontrados.AddRange(elementosPosterioresEncontrados.Distinct().ToList());
            }

            elementos.Clear();
            elementos.AddRange(listaOrdenada.Distinct().ToList());
        }
        private void CalcularCantidadElementos()
        {
            foreach (var itemEtapa in etapas)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                //CantidadElementosEjecucion += (from ElementoEjecucionCalculo E in itemEtapa.Elementos where E.Tipo == TipoElementoEjecucion.Entrada select E).Count();
                CantidadElementosEjecucion += (from E in itemEtapa.Elementos select E.HashCode_ElementoDiseñoOperacionCalculo).Distinct().Count();
            }
        }

        private void CalcularCantidadElementosCalculo()
        {
            foreach (var itemEtapa in etapasCalculo)
            {
                CantidadElementosEjecucion += (from E in itemEtapa.Elementos select E.HashCode_ElementoDiseñoOperacionCalculo).Distinct().Count();
            }
        }

        public void ActualizarPorcentajeAvance()
        {
            //System.Threading.Thread.Sleep(1000);

            if (CantidadElementosEjecucion > 0)
                Progreso.PorcentajeAvance = ((double)CantidadElementosEjecucionProcesados / (double)CantidadElementosEjecucion) * (double)100.0;
        }

        private void RealizarCalculosEtapa(EtapaEjecucion etapa)
        {
            foreach (var item in etapa.Elementos)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                SubCalculoActual = item.ElementoDiseñoCalculoRelacionado;
                CambioSubcalculo = true;

                string strMensajeLogResultados = string.Empty;
                string strMensajeLog = string.Empty;

                switch (item.Tipo)
                {
                    case TipoElementoEjecucion.Calculo:
                        if (item.Estado == EstadoEjecucion.Procesado) continue;
                        item.Estado = EstadoEjecucion.Iniciado;

                        ElementoCalculoEjecucion calculo = (ElementoCalculoEjecucion)item;
                        RealizarCalculo(calculo);

                        calculo.ElementosCalculo.Clear();

                        if (calculo.ElementoDiseñoCalculoRelacionado.ElementosPosteriores.Any())
                        {
                            foreach (var itemSalida in Salidas)
                            {                                
                                calculo.ElementosCalculo.Add(itemSalida);
                            }
                        }
                        else
                        {
                            SalidasCalculo.AddRange(Salidas);
                        }

                        item.Estado = EstadoEjecucion.Procesado;
                        CantidadElementosEjecucionProcesados++;

                        ActualizarPorcentajeAvance();
                        try { } catch (Exception) { };

                        if (!string.IsNullOrEmpty(strMensajeLog))
                            log.Add(strMensajeLog);
                        if (!string.IsNullOrEmpty(strMensajeLogResultados))
                            InformeResultados.TextoLog.Add(strMensajeLogResultados);

                        break;
                }
            }
        }

        private void PrepararSubElementosOperacion(ElementoOperacionAritmeticaEjecucion operacion)
        {
            var elementosOperacion = (from DiseñoElementoOperacion E in operacion.ElementoDiseñoRelacionado.ElementosDiseñoOperacion where E.ElementosAnteriores.Count == 0 select E).ToList();

            List<DiseñoElementoOperacion> elementosIteraciones = new List<DiseñoElementoOperacion>();

            while (elementosOperacion.Count > 0)
            {
                EtapaOperacionEjecucion etapa = new EtapaOperacionEjecucion();
                PrepararSubElementosEtapaOperacion(ref elementosOperacion, ref operacion, ref etapa);

                operacion.Etapas.Add(etapa);

                List<DiseñoElementoOperacion> elementosAnteriores = elementosOperacion.ToList();
                elementosOperacion.Clear();

                elementosAnteriores.InsertRange(0, elementosIteraciones);
                elementosIteraciones.Clear();

                foreach (var itemAnterior in elementosAnteriores)
                {
                    elementosOperacion.AddRange((from DiseñoElementoOperacion E in operacion.ElementoDiseñoRelacionado.ElementosDiseñoOperacion
                                                 where E.ElementosAnteriores.Contains(itemAnterior) & itemAnterior.ElementosPosteriores.Contains(E)
                                              select E).ToList());

                    List<DiseñoElementoOperacion> elementosAQuitar = new List<DiseñoElementoOperacion>();

                    foreach (var elementoVerificar_Posteriores in elementosOperacion)
                    {
                        foreach (var elementoPosterior_Buscado in elementosOperacion)
                        {
                            if (elementoVerificar_Posteriores != elementoPosterior_Buscado)
                            {
                                if (RecorrerElementosPosteriores_BusquedaElemento_Operacion(elementoVerificar_Posteriores, elementoPosterior_Buscado))
                                {
                                    elementosAQuitar.Add(elementoPosterior_Buscado);
                                }
                            }
                        }
                    }

                    while (elementosAQuitar.Any())
                    {
                        elementosIteraciones.Add(elementosAQuitar.First());
                        elementosOperacion.Remove(elementosAQuitar.First());
                        elementosAQuitar.Remove(elementosAQuitar.First());
                    }

                    elementosAQuitar = new List<DiseñoElementoOperacion>();

                    foreach (var elementoVerificar_Condiciones in elementosOperacion)
                    {
                        if (VerificarElementosAnteriores_Operacion(elementoVerificar_Condiciones, operacion.Etapas, elementosOperacion))
                        {
                            elementosAQuitar.Add(elementoVerificar_Condiciones);
                        }

                    }

                    while (elementosAQuitar.Any())
                    {
                        elementosIteraciones.Add(elementosAQuitar.First());
                        elementosOperacion.Remove(elementosAQuitar.First());
                        elementosAQuitar.Remove(elementosAQuitar.First());
                    }
                }

                elementosOperacion = elementosOperacion.Distinct().ToList();

                if (elementosOperacion.Count > 0)
                {
                    OrdenarElementosEtapaOperacion(ref elementosOperacion);
                    VerificarLimpiarElementosYaAgregados_Operacion(ref elementosOperacion, operacion);
                }

                //List<DiseñoElementoOperacion> elementosPosteriores = new List<DiseñoElementoOperacion>();
                //List<DiseñoElementoOperacion> elementosAQuitar = new List<DiseñoElementoOperacion>();
                //List<DiseñoElementoOperacion> elementosAAgregar = new List<DiseñoElementoOperacion>();

                //foreach (var item in elementosOperacion)
                //{
                //    foreach (var itemAgregar in (from DiseñoElementoOperacion E in item.ElementosPosteriores select E).ToList())
                //    {
                //        if (!(from E in elementosOperacion select E.Nombre).Contains(itemAgregar.Nombre))
                //            elementosPosteriores.Add(itemAgregar);
                //    }
                //}

                //elementosPosteriores = elementosPosteriores.Distinct().ToList();

                //foreach (var itemPosterior in elementosPosteriores)
                //{
                //    foreach (var itemPosterior2 in elementosPosteriores)
                //    {
                //        if (itemPosterior.ElementosPosteriores.Contains(itemPosterior2))
                //        {
                //            elementosAQuitar.Add(itemPosterior2);
                //        }
                //    }
                //}

                //elementosAQuitar = elementosAQuitar.Distinct().ToList();

                //while (elementosAQuitar.Count > 0)
                //{
                //    elementosPosteriores.Remove(elementosAQuitar.First());
                //    elementosAQuitar.Remove(elementosAQuitar.First());
                //}

                //foreach (var item in elementosOperacion)
                //{
                //    foreach (var itemContenedor in item.ElementosContenedoresOperacion)
                //    {
                //        if (!elementosPosteriores.Contains(itemContenedor) &
                //            !(from E in elementosOperacion select E.Nombre).Contains(itemContenedor.Nombre))
                //            elementosAAgregar.Add(item);
                //    }
                //}

                //elementosAAgregar = elementosAAgregar.Distinct().ToList();

                //elementosPosteriores.AddRange(elementosAAgregar);

                //elementosOperacion = elementosPosteriores.Distinct().ToList();







                //List<DiseñoElementoOperacion> elementosSiguienteEtapa = new List<DiseñoElementoOperacion>();

                //foreach (var item in elementosOperacion)
                //{
                //    elementosSiguienteEtapa.AddRange((from DiseñoElementoOperacion E in item.ElementosPosteriores select E).ToList());
                //}

                //elementosSiguienteEtapa = elementosSiguienteEtapa.Distinct().ToList();

                //if (elementosSiguienteEtapa != null && elementosSiguienteEtapa.Count > 0)
                //{
                //    List<DiseñoElementoOperacion> itemsPosterioresAQuitar = new List<DiseñoElementoOperacion>();
                //    List<DiseñoElementoOperacion> itemsAnterioresAAgregar = new List<DiseñoElementoOperacion>();

                //    foreach (var itemPosterior in elementosSiguienteEtapa)
                //    {
                //        foreach (var itemPosteriorPosterior in itemPosterior.ElementosPosteriores)
                //        {
                //            if (elementosSiguienteEtapa.Contains(itemPosteriorPosterior))
                //            {
                //                itemsPosterioresAQuitar.Add(itemPosteriorPosterior);
                //            }
                //        }

                //        foreach (var itemAnterior in itemPosterior.ElementosAnteriores)
                //        {
                //            if (itemsPosterioresAQuitar.Contains(itemPosterior))
                //            {
                //                itemsAnterioresAAgregar.Add(itemAnterior);
                //            }
                //        }
                //    }

                //    itemsAnterioresAAgregar = itemsAnterioresAAgregar.Distinct().ToList();

                //    while (itemsPosterioresAQuitar.Count > 0)
                //    {
                //        elementosSiguienteEtapa.Remove(itemsPosterioresAQuitar.First());
                //        itemsPosterioresAQuitar.Remove(itemsPosterioresAQuitar.First());
                //    }

                //    elementosSiguienteEtapa.AddRange(itemsAnterioresAAgregar);
                //}

                //elementosOperacion = elementosSiguienteEtapa.Distinct().ToList();
            }

            //foreach (var itemEtapaOperacion in operacion.Etapas)
            //{
            //    foreach (var itemEtapa in itemEtapaOperacion.Elementos)
            //    {
            //        foreach(var texto in itemEtapa.TextosInformacion_SeleccionOrdenamiento)
            //            EstablecerTextosInformacion_SeleccionarOrdenar_Operacion(itemEtapa, texto.FirstOrDefault(), operacion);
            //    }
            //}
        }

        private void PrepararSubElementosEtapaOperacion(ref List<DiseñoElementoOperacion> elementosOperacion, 
            ref ElementoOperacionAritmeticaEjecucion operacion,
            ref EtapaOperacionEjecucion etapa)
        {
            foreach (var item in elementosOperacion)
            {
                if (item.Tipo == TipoElementoDiseñoOperacion.Salida | item.Tipo == TipoElementoDiseñoOperacion.Nota) continue;

                ElementoDiseñoOperacionAritmeticaEjecucion elemento = new ElementoDiseñoOperacionAritmeticaEjecucion();

                elemento.ContieneSalida = item.ContieneSalida;
                elemento.HashCode_ElementoDiseñoOperacion = item.GetHashCode();
                elemento.ElementoDiseñoRelacionado = item;
                elemento.Nombre = item.Nombre;

                if (item.Tipo == TipoElementoDiseñoOperacion.Entrada)
                {
                    if (operacion.ElementoDiseñoRelacionado.EntradaRelacionada != null && 
                        operacion.ElementoDiseñoRelacionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null)
                    {
                        ElementoOperacionAritmeticaEjecucion subOperacionEntrada = (ElementoOperacionAritmeticaEjecucion)(from E in operacion.ElementosOperacion
                                                                             where
                 E.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                 ((ElementoOperacionAritmeticaEjecucion)E).ElementoDiseñoRelacionado == item.ElementoDiseñoRelacionado
                                                                             select E).FirstOrDefault();

                        if (subOperacionEntrada == null) continue;
                        
                        elemento.Nombre += " desde variable o vector de números retornados " + subOperacionEntrada.Nombre;

                        ElementoEntradaEjecucion entrada = new ElementoEntradaEjecucion();
                        entrada.ElementoDiseñoRelacionado = item.ElementoDiseñoRelacionado;
                        entrada.Estado = EstadoEjecucion.Ninguno;
                        entrada.Nombre = "Variable número en " + elemento.Nombre;
                        entrada.TipoEntrada = TipoEntrada.Calculo;
                        entrada.TipoEntradaConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo;

                        elemento.EntradaEjecucion = entrada;
                        
                        elemento.TipoInterno = TipoElementoOperacionEjecucion.Entrada;
                    }
                    else
                    {
                        ElementoEntradaEjecucion entradaNumero = (ElementoEntradaEjecucion)(from E in operacion.ElementosOperacion
                                                                                      where E.GetType() == typeof(ElementoEntradaEjecucion) && (
                              (ElementoEntradaEjecucion)E).ElementoDiseñoRelacionado == item.ElementoDiseñoRelacionado
                                                                                      select E).FirstOrDefault();

                        if (entradaNumero == null) continue;

                        elemento.EntradaEjecucion = entradaNumero;
                        elemento.Nombre += " desde " + entradaNumero.Nombre;
                                               
                        elemento.TipoInterno = TipoElementoOperacionEjecucion.Entrada;
                    }
                }
                else if (item.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion)
                {
                    elemento.TipoElemento = item.TipoOpcionOperacion;

                    ElementoOperacionAritmeticaEjecucion subOperacion = (ElementoOperacionAritmeticaEjecucion)(from E in operacion.ElementosOperacion
                                                                         where
             E.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
             ((ElementoOperacionAritmeticaEjecucion)E).ElementoDiseñoRelacionado == item.ElementoDiseñoRelacionado
                                                                         select E).FirstOrDefault();
                    
                    if (subOperacion == null) continue;

                    elemento.OperacionEjecucion = subOperacion;
                    elemento.Nombre += " desde " + subOperacion.Nombre;
                    elemento.TipoInterno = TipoElementoOperacionEjecucion.FlujoOperacion;

                    elemento.OperacionEjecucion.Relaciones_TextosInformacion = ObtenerDefinicionesTextosInformacion_Operacion(elemento.OperacionEjecucion);

                }
                else if (item.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion)
                {
                    elemento.TipoInterno = TipoElementoOperacionEjecucion.OpcionOperacion;
                    elemento.TipoElemento = item.TipoOpcionOperacion;

                    List<CondicionTextosInformacion> listaCondicionesTextos = new List<CondicionTextosInformacion>();
                    
                    foreach (var itemCondiciones in item.CondicionesTextosInformacion_SeleccionOrdenamiento)
                    {
                        itemCondiciones.Condiciones.Operandos_AplicarCondiciones.AddRange(itemCondiciones.Operandos_AplicarCondiciones);
                        itemCondiciones.Condiciones.SubOperandos_AplicarCondiciones.AddRange(itemCondiciones.SubOperandos_AplicarCondiciones);
                        listaCondicionesTextos.Add(itemCondiciones.Condiciones);
                    }

                    elemento.CondicionesTextosInformacion_SeleccionOrdenamiento.AddRange(listaCondicionesTextos);
                    elemento.CondicionesFlujo_SeleccionOrdenamiento.AddRange(item.CondicionesFlujo_SeleccionOrdenamiento);
                    
                    List<DiseñoElementoOperacion> itemsOrdenados = item.ElementosAnteriores.OrderBy((i) =>
                        (from O in i.OrdenOperandos where O.ElementoPadre == item select O).FirstOrDefault().Orden).ToList();

                    foreach (var itemAnterior in itemsOrdenados)
                    {
                        foreach (var itemEtapa in operacion.Etapas)
                        {
                            foreach (var itemAnteriorEjecucion in itemEtapa.Elementos)
                            {
                                if (itemAnterior.GetHashCode() == itemAnteriorEjecucion.HashCode_ElementoDiseñoOperacion)
                                {
                                    elemento.ElementosOperacion.Add(itemAnteriorEjecucion);
                                }
                            }
                        }
                    }

                    elemento.Relaciones_TextosInformacion = ObtenerDefinicionesTextosInformacion_ElementoOperacion(elemento);
                    
                }
                
                TraspasarObjetoDiseño_OperacionInterna_Preparar(elemento, operacion);
                etapa.Elementos.Add(elemento);
            }
        }

        private void OrdenarElementosEtapaOperacion(ref List<DiseñoElementoOperacion> elementos)
        {
            List<DiseñoElementoOperacion> listaOrdenada = new List<DiseñoElementoOperacion>();
            List<DiseñoElementoOperacion> elementosEncontrados = new List<DiseñoElementoOperacion>();

            bool tieneAnterior = false;
            foreach (var itemElemento in elementos)
            {
                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (elementos.Contains(itemAnterior))
                    {
                        tieneAnterior = true;
                    }
                }

                if (!tieneAnterior)
                {
                    elementosEncontrados.Add(itemElemento);
                }

                tieneAnterior = false;
            }

            while (elementosEncontrados.Count > 0)
            {
                listaOrdenada.AddRange(elementosEncontrados.Distinct().ToList());

                List<DiseñoElementoOperacion> elementosPosterioresEncontrados = new List<DiseñoElementoOperacion>();
                foreach (var itemElementoAnterior in elementosEncontrados)
                {
                    tieneAnterior = false;
                    foreach (var itemElemento in elementos)
                    {
                        if (itemElemento != itemElementoAnterior)
                        {
                            foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                            {
                                if (elementosEncontrados.Contains(itemAnterior))
                                {
                                    tieneAnterior = true;
                                }
                            }

                            if (tieneAnterior)
                            {
                                elementosPosterioresEncontrados.Add(itemElemento);
                                tieneAnterior = false;
                            }
                        }
                    }
                }

                elementosEncontrados.Clear();
                elementosEncontrados.AddRange(elementosPosterioresEncontrados.Distinct().ToList());
            }

            elementos.Clear();
            elementos.AddRange(listaOrdenada.Distinct().ToList());
        }

        private void VerificarLimpiarElementosYaAgregados_Operacion(ref List<DiseñoElementoOperacion> elementos, ElementoOperacionAritmeticaEjecucion operacion)
        {
            List<DiseñoElementoOperacion> elementoYaAgregados = new List<DiseñoElementoOperacion>();

            DiseñoElementoOperacion elemento = null;
            int indice = 0;
            do
            {
                if (elementos.Count > 0)
                {
                    elemento = elementos[indice];

                    foreach (var itemEtapa in operacion.Etapas)
                    {
                        elementoYaAgregados = (from E in itemEtapa.Elementos where E.HashCode_ElementoDiseñoOperacion == elemento.GetHashCode() select E.ElementoDiseñoRelacionado).ToList();
                        if (elementoYaAgregados.Count > 0) break;
                    }

                    if (elementoYaAgregados.Count > 0)
                    {
                        elementos.Remove(elemento);
                        indice--;
                    }
                }

                indice++;

            } while (indice <= elementos.Count - 1);
        }

        public ElementoEjecucionCalculo ObtenerElementoEjecucion(DiseñoOperacion elementoDiseño)
        {
            foreach (var itemEtapa in etapas)
            {
                var elemento = (from E in itemEtapa.Elementos where E.ElementoDiseñoRelacionado == elementoDiseño select E).FirstOrDefault();

                if (elemento != null)
                    return elemento;
            }

            return null;
        }

        public ElementoEjecucionCalculo ObtenerElementoEjecucion_EnHistorial(DiseñoOperacion elementoDiseño)
        {
            foreach (var itemEtapa in etapasHistorial)
            {
                var elemento = (from E in itemEtapa.Elementos where E.ElementoDiseñoRelacionado == elementoDiseño select E).FirstOrDefault();

                if (elemento != null)
                    return elemento;
            }

            return null;
        }

        public ElementoDiseñoOperacionAritmeticaEjecucion ObtenerSubElementoEjecucion(DiseñoElementoOperacion elementoDiseño)
        {
            foreach (var itemEtapa in etapas)
            {
                foreach (var item in itemEtapa.Elementos)
                {
                    if (item.Tipo == TipoElementoEjecucion.OperacionAritmetica)
                    {
                        ElementoOperacionAritmeticaEjecucion operacion = (ElementoOperacionAritmeticaEjecucion)item;
                        foreach (var etapa in operacion.Etapas)
                        {
                            var elemento = (from E in etapa.Elementos where E.ElementoDiseñoRelacionado == elementoDiseño select E).FirstOrDefault();

                            if (elemento != null)
                                return elemento;
                        }
                    }
                }
                
            }

            return null;
        }

        public void LimpiarArchivosTemporales()
        {
            try
            {
                foreach (var archivo in ArchivosOrigenesDatos_Abiertos)
                {
                    if (((archivo.TipoArchivo == TipoArchivo.ServidorFTP |
                                archivo.TipoArchivo == TipoArchivo.Internet) ||
                                ((archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                                archivo.TipoArchivo == TipoArchivo.RedLocal) &&
                                (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.ArchivoTextoPlano ||
                                archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla))) &
                        !string.IsNullOrEmpty(archivo.lector.Name) &
                        File.Exists(archivo.lector.Name))
                        File.Delete(archivo.lector.Name);
                }
            }
            catch (Exception)
            {
                
            }
        }
    }    
}
