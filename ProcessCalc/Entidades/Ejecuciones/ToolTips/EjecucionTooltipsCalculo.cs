using ProcessCalc.Controles;
using ProcessCalc.Controles.Calculos;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Ejecuciones.ToolTips;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xml;
using System.Xml.Linq;

namespace ProcessCalc.Entidades.Ejecuciones.ToolTips
{
    public class EjecucionTooltipsCalculo
    {
        public Calculo Calculo { get; set; }
        public EjecucionCalculo Ejecucion { get; set; }
        public List<DatosTooltips_Elementos> InfoTooltips { get; set; }
        public Calculo UltimoEstadoCalculo { get; set; }
        public MainWindow Ventana {  get; set; }
        public ComparadorObjetos ComparadorObjetos {  get; set; }
        public List<AsociacionToolTipElemento> AsociacionesToolTipsElementos { get; set; }
        public EjecucionTooltipsCalculo()
        {
            InfoTooltips = new List<DatosTooltips_Elementos>();
            ComparadorObjetos = new ComparadorObjetos();
            AsociacionesToolTipsElementos = new List<AsociacionToolTipElemento>();
        }

        public bool VerificarCambiosCalculo()
        {
            bool hayCambios = false;

            ComparadorObjetos._paresVisitados.Clear();

            if (!ComparadorObjetos.CompararObjetos(Calculo, UltimoEstadoCalculo))
                hayCambios = true;

            //DataContractSerializer objetoGuardado = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });

            //MemoryStream flujoCalculoGuardado = new MemoryStream();
            //objetoGuardado.WriteObject(flujoCalculoGuardado, UltimoEstadoCalculo);

            //MemoryStream flujoCalculo = new MemoryStream();
            //objetoGuardado.WriteObject(flujoCalculo, Calculo);

            //if (!Calculo.VerificarIgualdad(flujoCalculo.ToArray(), flujoCalculoGuardado.ToArray()))
            //    hayCambios = true;

            //flujoCalculo.Close();
            //flujoCalculoGuardado.Close();

            return hayCambios;
        }

        public void VerificarCambiosEntradas()
        {
            List<Entrada> entradas = new List<Entrada>();
            foreach (var itemCalculo in Calculo.Calculos)
                entradas.AddRange(itemCalculo.ListaEntradas);

            List<Entrada> entradasUltimoCalculo = new List<Entrada>();
            foreach (var itemCalculo in UltimoEstadoCalculo.Calculos)
                entradasUltimoCalculo.AddRange(itemCalculo.ListaEntradas);

            foreach (var itemEntrada in entradas)
            {
                var entradaUltimoEstado = entradasUltimoCalculo.FirstOrDefault(i => i.ID == itemEntrada.ID);

                if (entradaUltimoEstado != null)
                {
                    itemEntrada.ConCambios_ToolTips = itemEntrada.VerificarCambios(entradaUltimoEstado, ComparadorObjetos);
                }
                else
                {
                    itemEntrada.ConCambios_ToolTips = true;
                }
            }
        }

        public void VerificarCambiosOperaciones()
        {
            List<DiseñoOperacion> operaciones = new List<DiseñoOperacion>();
            foreach (var itemCalculo in Calculo.Calculos)
                operaciones.AddRange(itemCalculo.ElementosOperaciones);

            List<DiseñoOperacion> operacionesUltimoCalculo = new List<DiseñoOperacion>();
            foreach (var itemCalculo in UltimoEstadoCalculo.Calculos)
                operacionesUltimoCalculo.AddRange(itemCalculo.ElementosOperaciones);

            foreach (var itemOperacion in operaciones)
            {
                var operacionUltimoEstado = operacionesUltimoCalculo.FirstOrDefault(i => i.ID == itemOperacion.ID);

                if (operacionUltimoEstado != null)
                {
                    itemOperacion.ConCambios_ToolTips = itemOperacion.VerificarCambios(operacionUltimoEstado, ComparadorObjetos);
                }
                else
                {
                    itemOperacion.ConCambios_ToolTips = true;
                }

                if (operacionUltimoEstado != null)
                {
                    foreach (var itemInterno in itemOperacion.ElementosDiseñoOperacion)
                    {
                        var operacionInternaUltimoEstado = operacionUltimoEstado.ElementosDiseñoOperacion.FirstOrDefault(i => i.ID == itemInterno.ID);

                        if (operacionInternaUltimoEstado != null)
                        {
                            itemInterno.ConCambios_ToolTips = itemInterno.VerificarCambios(operacionInternaUltimoEstado, ComparadorObjetos);
                        }
                        else
                        {
                            itemInterno.ConCambios_ToolTips = true;
                        }
                    }
                }
            }
        }

        public void LimpiarElementos()
        {
            EstablecerEstadoCambiosToolTips_Entradas(false, ObtenerCalculo(Calculo.ID));
            EstablecerEstadoCambiosToolTips_Entradas(false, UltimoEstadoCalculo);
            EstablecerEstadoCambiosToolTips_Operaciones(false, ObtenerCalculo(Calculo.ID));
            EstablecerEstadoCambiosToolTips_Operaciones(false, UltimoEstadoCalculo);
            this.Ejecucion.EntradasProcesadas.Clear();
        }

        public void EstablecerEstadoCambiosToolTips_Entradas(bool conCambios, Calculo itemCalculo)
        {
            if (itemCalculo != null)
            {
                foreach (var itemCalculoDiseño in itemCalculo.Calculos)
                {
                    foreach (var itemEntrada in itemCalculoDiseño.ListaEntradas)
                    {
                        itemEntrada.ConCambios_ToolTips = conCambios;

                        if (!conCambios ||
                            itemEntrada.EjecutarDeFormaGeneral)
                            itemEntrada.EntradaProcesada = null;
                    }

                    foreach (var itemElemento in itemCalculoDiseño.ElementosOperaciones)
                    {
                        if (itemElemento.Tipo == TipoElementoOperacion.Entrada &&
                            itemElemento.EntradaRelacionada != null &&
                            itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null)
                        {
                            itemElemento.EntradaRelacionada.ConCambios_ToolTips = conCambios;
                            if (!conCambios ||
                                itemElemento.EntradaRelacionada.EjecutarDeFormaGeneral)
                                itemElemento.EntradaRelacionada.EntradaProcesada = null;
                        }
                    }
                }
            }
        }

        public void EstablecerEstadoCambiosToolTips_SeleccionEntradas(bool conCambios, Calculo itemCalculo)
        {
            if (itemCalculo != null)
            {
                foreach (var itemCalculoDiseño in itemCalculo.Calculos)
                {
                    foreach (var itemElemento in itemCalculoDiseño.ElementosOperaciones)
                    {
                        if (itemElemento.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                        {
                            itemElemento.Actualizar_ToolTips = conCambios;
                        }
                    }
                }
            }

            Calculo itemCalculo_Diseño = ObtenerCalculo(Calculo.ID);

            if (itemCalculo_Diseño != null)
            {
                foreach (var itemCalculoDiseño in itemCalculo_Diseño.Calculos)
                {
                    foreach (var itemElemento in itemCalculoDiseño.ElementosOperaciones)
                    {
                        if (itemElemento.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                        {
                            itemElemento.Actualizar_ToolTips = conCambios;
                        }
                    }
                }
            }
        }

        public void EstablecerEstadoCambiosToolTips_Operaciones(bool conCambios, Calculo itemCalculo)
        {
            if (itemCalculo != null)
            {
                foreach (var itemCalculoDiseño in itemCalculo.Calculos)
                {
                    foreach (var itemElemento in itemCalculoDiseño.ElementosOperaciones)
                    {
                        if (itemElemento.Tipo != TipoElementoOperacion.Entrada)
                        {
                            itemElemento.ConCambios_ToolTips = conCambios;
                            itemElemento.Actualizar_ToolTips = conCambios;
                        }

                        foreach(var itemInterno in itemElemento.ElementosDiseñoOperacion)
                        {
                            if (itemInterno.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion)
                            {
                                itemInterno.ConCambios_ToolTips = conCambios;
                                itemInterno.Actualizar_ToolTips = conCambios;
                            }
                        }
                    }
                }
            }
        }

        public void EstablecerEstadoCambiosToolTips_EntradaEspecifica(bool conCambios, string IDEntrada)
        {
            Calculo itemCalculo = null;

            if (conCambios)
            {
                itemCalculo = Calculo;
            }
            else
            {
                itemCalculo = ObtenerCalculo(Calculo.ID);
            }

            foreach (var itemCalculoDiseño in itemCalculo.Calculos)
            {
                foreach (var itemEntrada in itemCalculoDiseño.ListaEntradas)
                {
                    if (itemEntrada.ID == IDEntrada)
                    {
                        itemEntrada.ConCambios_ToolTips = conCambios;

                        if (!conCambios)
                            itemEntrada.EntradaProcesada = null;
                    }
                }

                foreach (var itemElemento in itemCalculoDiseño.ElementosOperaciones)
                {
                    if (itemElemento.Tipo == TipoElementoOperacion.Entrada &&
                        itemElemento.EntradaRelacionada != null &&
                        itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null)
                    {
                        if (itemElemento.EntradaRelacionada.ID == IDEntrada)
                        {
                            itemElemento.EntradaRelacionada.ConCambios_ToolTips = conCambios;
                            if (!conCambios)
                                itemElemento.EntradaRelacionada.EntradaProcesada = null;
                        }
                    }
                }
            }
        }

        public Calculo ObtenerCalculo(string ID)
        {
            if (Ventana != null)
            {
                return Ventana.Calculos.FirstOrDefault(i => i.ID == ID);
            }
            else
                return null;
        }

        public DiseñoCalculo ObtenerCalculoDiseño(string IDCalculo, string ID)
        {
            var itemCalculo = ObtenerCalculo(IDCalculo);

            if (itemCalculo != null)
            {
                return itemCalculo.ObtenerCalculoDiseño(ID);
            }
            else
                return null;
        }

        public DiseñoOperacion ObtenerElementoDiseño(string IDCalculo, string ID)
        {
            var itemCalculo = ObtenerCalculo(IDCalculo);

            if (itemCalculo != null)
            {
                return itemCalculo.ObtenerElementoDiseño(ID);
            }
            else
                return null;
        }

        public Entrada ObtenerEntradaDiseño(string IDCalculo, string ID)
        {
            var itemCalculo = ObtenerCalculo(IDCalculo);

            if (itemCalculo != null)
            {
                return itemCalculo.ObtenerEntradaDiseño(ID);
            }
            else
                return null;
        }

        public DiseñoElementoOperacion ObtenerSubElementoDiseño(string IDCalculo, string ID)
        {
            var itemCalculo = ObtenerCalculo(IDCalculo);

            if (itemCalculo != null)
            {
                return itemCalculo.ObtenerSubElementoDiseño(ID);
            }
            else
                return null;
        }

        public void EstablecerElementoVisual_Tooltip_Elemento_Ventana(DatosTooltips_Elementos datosElemento)
        {
            if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
            {
                Ventana.Dispatcher.Invoke(() =>
            {
                if (datosElemento.Elemento != null)
                {                    
                    Ventana.ToolTip = new ToolTipElementoVisual();
                    Ventana.ToolTip.DatosTooltips_Elementos = datosElemento;
                    Ventana.ToolTip.EjecucionToolTips = this;

                    if(!datosElemento.Hora.HasValue)
                        datosElemento.Hora = DateTime.Now.TimeOfDay;
                }

            });
            }
        }

        public void EstablecerElementoVisual_Tooltip_SubElemento_Ventana(DatosTooltips_Elementos datosElemento)
        {
            if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
            {
                Ventana.Dispatcher.Invoke(() =>
            {
                if (datosElemento.SubElemento != null)
                {
                    Ventana.ToolTip = new ToolTipElementoVisual();
                    Ventana.ToolTip.DatosTooltips_Elementos = datosElemento;
                    Ventana.ToolTip.EjecucionToolTips = this;

                    if (!datosElemento.Hora.HasValue)
                        datosElemento.Hora = DateTime.Now.TimeOfDay;
                }
            });
            }
        }

        public void EstablecerElementoVisual_Tooltip_Calculo_Ventana(DatosTooltips_Elementos datosElemento)
        {
            if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
            {
                Ventana.Dispatcher.Invoke(() =>
            {
                if (datosElemento.Calculo != null)
                {
                    Ventana.ToolTip = new ToolTipElementoVisual();
                    Ventana.ToolTip.DatosTooltips_Elementos = datosElemento;
                    Ventana.ToolTip.EjecucionToolTips = this;

                    if (!datosElemento.Hora.HasValue)
                        datosElemento.Hora = DateTime.Now.TimeOfDay;
                }

                return false;
            });
            }
        }

        public void ActualizarElementosVisuales_ToolTips()
        {
            foreach(var itemDatosElemento in InfoTooltips)
            {                
                if (itemDatosElemento.Actualizar)
                {
                    itemDatosElemento.Hora = DateTime.Now.TimeOfDay;
                }
            }
        }

        public void MostrarElementoVisual_ToolTip(DatosTooltips_Elementos itemDatosElemento)
        {
            if (itemDatosElemento != null)
            {

                if (itemDatosElemento.Elemento != null)
                {
                    EstablecerElementoVisual_Tooltip_Elemento_Ventana(itemDatosElemento);
                }
                else if (itemDatosElemento.SubElemento != null)
                {
                    EstablecerElementoVisual_Tooltip_SubElemento_Ventana(itemDatosElemento);
                }
                else if (itemDatosElemento.Calculo != null)
                {
                    EstablecerElementoVisual_Tooltip_Calculo_Ventana(itemDatosElemento);
                }

                if (Ventana.ToolTip != null)
                    Ventana.popup.Child = Ventana.ToolTip;

                
                {
                    itemDatosElemento.EstablecerDatosTooltip_EnVentana();
                }
            }
            else
            {
                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                {
                    Ventana.Dispatcher.Invoke(() =>
                    {
                        Ventana.popup.Child = new ToolTipElementoVisual();
                    });
                }
            }
        }

        public void EstablecerDatosTooltip_Elemento_EntradaNoSeleccionada(DiseñoOperacion elemento, TipoOpcionToolTip tipo,
            bool Seleccionada)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Elemento = ObtenerElementoDiseño(Calculo.ID, elemento.ID),
                    //Entrada = elementoAsociado != null ? elementoAsociado.EntradaRelacionada : elemento.EntradaRelacionada,
                    Tipo = tipo,
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                //if (elementoAsociado != null)
                //{
                //    ObtenerDatosTooltip_Elemento(elementoAsociado, ref ValorNumerico, TextosInformacion, tipo);
                //}

                InfoTooltips.Add(elementoEncontrado);
                //EstablecerToolTips_Elemento(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_EntradaNoSeleccionada(elemento.NombreCombo, Seleccionada);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {

                //if (elementoAsociado != null)
                //{
                //    ObtenerDatosTooltip_Elemento(elementoAsociado, ref ValorNumerico, TextosInformacion, tipo);
                //}

                elementoEncontrado.EstabecerDatosTooltip_EntradaNoSeleccionada(elemento.NombreCombo, Seleccionada);
            }

            elementoEncontrado.Actualizar = true;
            //EstablecerElementosVisuales_Tooltips_Elemento(elementoEncontrado);
            //elementoEncontrado.EstablecerDatosTooltip_EnVentana();
        }

        public DiseñoOperacion EstablecerDatosTooltip_Elemento_Digitacion(DiseñoOperacion elemento,
            ElementoEntradaEjecucion elementoAsociado, TipoOpcionToolTip tipo)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Elemento = ObtenerElementoDiseño(Calculo.ID, elemento.ID),
                    //Entrada = elementoAsociado != null ? elementoAsociado.EntradaRelacionada : elemento.EntradaRelacionada,
                    Tipo = tipo,
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                InfoTooltips.Add(elementoEncontrado);
                //EstablecerToolTips_Elemento(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_Digitacion(elemento.NombreCombo, elementoAsociado);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {
                elementoEncontrado.EstabecerDatosTooltip_Digitacion(elemento.NombreCombo, elementoAsociado);                
            }

            elementoEncontrado.Actualizar = true;
            //EstablecerElementosVisuales_Tooltips_Elemento(elementoEncontrado);
            //elementoEncontrado.EstablecerDatosTooltip_EnVentana();
            return elementoEncontrado.Elemento;
        }

        public DiseñoOperacion EstablecerDatosTooltip_Elemento_SeleccionArchivosEntrada(DiseñoOperacion elemento,
            ElementoEntradaEjecucion elementoAsociado, TipoOpcionToolTip tipo)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Elemento = ObtenerElementoDiseño(Calculo.ID, elemento.ID),
                    //Entrada = elementoAsociado != null ? elementoAsociado.EntradaRelacionada : elemento.EntradaRelacionada,
                    Tipo = tipo,
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                InfoTooltips.Add(elementoEncontrado);
                //EstablecerToolTips_Elemento(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_SeleccionArchivosEntrada(elemento.NombreCombo, elementoAsociado);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {
                elementoEncontrado.EstabecerDatosTooltip_SeleccionArchivosEntrada(elemento.NombreCombo, elementoAsociado);
            }

            elementoEncontrado.Actualizar = true;
            //EstablecerElementosVisuales_Tooltips_Elemento(elementoEncontrado);
            //elementoEncontrado.EstablecerDatosTooltip_EnVentana();
            return elementoEncontrado.Elemento;
        }

        public void EstablecerDatosTooltip_Elemento_NoDigitacion(DiseñoOperacion elemento,
            DiseñoOperacion elementoAsociado, TipoOpcionToolTip tipo)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Elemento = ObtenerElementoDiseño(Calculo.ID, elemento.ID),
                    //Entrada = elementoAsociado != null ? elementoAsociado.EntradaRelacionada : elemento.EntradaRelacionada,
                    Tipo = tipo,
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                InfoTooltips.Add(elementoEncontrado);
                //EstablecerToolTips_Elemento(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_NoDigitacion(elemento.NombreCombo);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {
                elementoEncontrado.EstabecerDatosTooltip_NoDigitacion(elemento.NombreCombo);
            }

            elementoEncontrado.Actualizar = true;
            //EstablecerElementosVisuales_Tooltips_Elemento(elementoEncontrado);
            //elementoEncontrado.EstablecerDatosTooltip_EnVentana();
        }

        public void EstablecerDatosTooltip_Elemento_NoSeleccionArchivosEntrada(DiseñoOperacion elemento, TipoOpcionToolTip tipo)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Elemento = ObtenerElementoDiseño(Calculo.ID, elemento.ID),
                    //Entrada = elementoAsociado != null ? elementoAsociado.EntradaRelacionada : elemento.EntradaRelacionada,
                    Tipo = tipo,
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                InfoTooltips.Add(elementoEncontrado);
                //EstablecerToolTips_Elemento(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_NoSeleccionArchivosEntrada(elemento.NombreCombo);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {
                elementoEncontrado.EstabecerDatosTooltip_NoSeleccionArchivosEntrada(elemento.NombreCombo);
            }

            elementoEncontrado.Actualizar = true;
            //EstablecerElementosVisuales_Tooltips_Elemento(elementoEncontrado);
            //elementoEncontrado.EstablecerDatosTooltip_EnVentana();
        }

        public void EstablecerDatosTooltip_Elemento_Digitacion_TextoEnPantalla(DiseñoOperacion elemento, TipoOpcionToolTip tipo)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Elemento = ObtenerElementoDiseño(Calculo.ID, elemento.ID),
                    //Entrada = elementoAsociado != null ? elementoAsociado.EntradaRelacionada : elemento.EntradaRelacionada,
                    Tipo = tipo,
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                InfoTooltips.Add(elementoEncontrado);
                //EstablecerToolTips_Elemento(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_Digitacion_TextoEnPantalla(elemento.NombreCombo);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {
                elementoEncontrado.EstabecerDatosTooltip_Digitacion_TextoEnPantalla(elemento.NombreCombo);
            }

            elementoEncontrado.Actualizar = true;
            //EstablecerElementosVisuales_Tooltips_Elemento(elementoEncontrado);
            //elementoEncontrado.EstablecerDatosTooltip_EnVentana();
        }

        public void EstablecerDatosTooltip_Elemento_SeleccionEntradas(DiseñoOperacion elemento,
            DiseñoOperacion elementoAsociado, TipoOpcionToolTip tipo, bool valorSeleccionEntradas)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Elemento = ObtenerElementoDiseño(Calculo.ID, elemento.ID),
                    //Entrada = elementoAsociado != null ? elementoAsociado.EntradaRelacionada : elemento.EntradaRelacionada,
                    Tipo = tipo,
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                InfoTooltips.Add(elementoEncontrado);
                //EstablecerToolTips_Elemento(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_SeleccionEntradas(elemento.NombreCombo, valorSeleccionEntradas);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {
                elementoEncontrado.EstabecerDatosTooltip_SeleccionEntradas(elemento.NombreCombo, valorSeleccionEntradas);
            }

            elementoEncontrado.Actualizar = true;
            //EstablecerElementosVisuales_Tooltips_Elemento(elementoEncontrado);
            //elementoEncontrado.EstablecerDatosTooltip_EnVentana();
        }
        public void EstablecerDatosTooltip_Elemento(DiseñoOperacion elemento,
            List<EntidadNumero> Elementos, TipoOpcionToolTip tipo, List<Clasificador> Clasificadores,
            bool OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
            bool OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Elemento = ObtenerElementoDiseño(Calculo.ID, elemento.ID),
                    Clasificadores_Operacion = Clasificadores,
                    Tipo = tipo,
                    Ventana = Ventana
                };


                InfoTooltips.Add(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_Elementos(Elementos, OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
                    OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);
            }
            else
            {
               
                elementoEncontrado.EstabecerDatosTooltip_Elementos(Elementos, OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
                    OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);                
            }

            elementoEncontrado.Actualizar = true;            
        }

        public void ObtenerDatosTooltip_Elemento(DiseñoOperacion elemento,
            List<EntidadNumero> Elementos, ElementoEntradaEjecucion item)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado != null)
            {
                Elementos = elementoEncontrado.InfoElementos_Numeros.Select(i => new EntidadNumero()
                {
                    Nombre = i.Nombre,
                    Numero = i.ValorNumerico,
                    Textos = i.TextosInformacion.ToList(),
                    Clasificadores_SeleccionarOrdenar = i.Clasificadores.ToList(),
                }).ToList();

                
                item.Numeros = Elementos;
                item.Clasificadores_Cantidades = Elementos.SelectMany(i => i.Clasificadores_SeleccionarOrdenar).Distinct().ToList();

                elementoEncontrado.Actualizar = true;
            }
        }

        public void ObtenerDatosTooltip_Elemento(DiseñoOperacion elemento,
            List<EntidadNumero> Elementos)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado != null)
            {
                Elementos = elementoEncontrado.InfoElementos_Numeros.Select(i => new EntidadNumero()
                {
                    Nombre = i.Nombre,
                    Numero = i.ValorNumerico,
                    Textos = i.TextosInformacion.ToList(),
                    Clasificadores_SeleccionarOrdenar = i.Clasificadores.ToList(),
                }).ToList();                
            }
        }

        public void ObtenerDatosTooltip_Elemento_Persistencia(DiseñoOperacion elemento,
            List<EntidadNumero> Elementos, ElementoOperacionAritmeticaEjecucion elementoEjecucion)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == elemento.ID);
            if (elementoEncontrado != null)
            {
                Elementos.AddRange(elementoEncontrado.InfoElementos_Numeros.Select(i => i.Numero.CopiarObjeto(null, null, elementoEjecucion)).ToList());
            }
        }

        public void ObtenerDatosTooltip_Elemento_Persistencia(DiseñoElementoOperacion elemento,
            List<EntidadNumero> Elementos, ElementoDiseñoOperacionAritmeticaEjecucion elementoEjecucion)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.SubElemento != null && i.SubElemento.ID == elemento.ID);
            if (elementoEncontrado != null)
            {
                Elementos.AddRange(elementoEncontrado.InfoElementos_Numeros.Select(i => i.Numero.CopiarObjeto(null, null, elementoEjecucion)).ToList());
            }
        }

        public void EstablecerDatosTooltip_Elemento(DiseñoElementoOperacion elemento,
            List<EntidadNumero> Numeros, TipoOpcionToolTip tipo, List<Clasificador> Clasificadores,
            bool OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
            bool OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.SubElemento != null && i.SubElemento.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    SubElemento = ObtenerSubElementoDiseño(Calculo.ID, elemento.ID),
                    Tipo = tipo,
                    Clasificadores_Operacion = Clasificadores.ToList(),
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                InfoTooltips.Add(elementoEncontrado);
                //EstablecerToolTips_SubElemento(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_Elementos(Numeros, OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion
                    , OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {
                elementoEncontrado.EstabecerDatosTooltip_Elementos(Numeros, OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion, 
                    OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);                
            }

            elementoEncontrado.Actualizar = true;
            //EstablecerElementosVisuales_Tooltips_SubElemento(elementoEncontrado);
            //elementoEncontrado.EstablecerDatosTooltip_EnVentana();
        }

        public void EstablecerDatosTooltip_Elemento(DiseñoCalculo elemento,
            List<ElementoOperacionAritmeticaEjecucion> Elementos, TipoOpcionToolTip tipo, List<Clasificador> Clasificadores,
            bool OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
            bool OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion)
        {
            var elementoEncontrado = InfoTooltips.FirstOrDefault(i => i.Calculo != null && i.Calculo.ID == elemento.ID);
            if (elementoEncontrado == null)
            {
                elementoEncontrado = new DatosTooltips_Elementos()
                {
                    Calculo = ObtenerCalculoDiseño(Calculo.ID, elemento.ID),
                    Tipo = tipo,
                    Clasificadores_Operacion = Clasificadores.ToList(),
                    //Hilo_ToolTip = new Thread(new ParameterizedThreadStart(AgregarDatosTooltip_EnVentana)),
                    Ventana = Ventana
                };

                InfoTooltips.Add(elementoEncontrado);

                List<EntidadNumero> ElementosOperacion = new List<EntidadNumero>();

                foreach (var item in Elementos)
                {
                    if (item.ElementosOperacion.Any())
                    {                        
                        ElementosOperacion.AddRange(item.Numeros.Select(i => i.CopiarObjeto()));
                    }
                }

                //EstablecerToolTips_Calculo(elementoEncontrado);
                elementoEncontrado.EstabecerDatosTooltip_Elementos(ElementosOperacion, OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion, 
                    OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);
                //AgregarDatosTooltip_EnVentana(elementoEncontrado);
            }
            else
            {
                List<EntidadNumero> ElementosOperacion = new List<EntidadNumero>();

                foreach (var item in Elementos)
                {
                    if (item.ElementosOperacion.Any())
                    {
                        ElementosOperacion.AddRange(item.Numeros.Select(i => i.CopiarObjeto()));
                    }
                }

                elementoEncontrado.EstabecerDatosTooltip_Elementos(ElementosOperacion, OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion, 
                    OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);                
            }

            elementoEncontrado.Actualizar = true;
        }
        
        public void QuitarElementos_ToolTips()
        {
            if (InfoTooltips != null &&
                Calculo != null)
            {
                var itemCalculo = ObtenerCalculo(Calculo.ID);

                while (InfoTooltips.Any(i => i.Elemento != null && !itemCalculo.Calculos
                .Where(o => !o.EsEntradasArchivo)
                .Any(j => !j.ElementosOperaciones.Any(k => k.ID == i.Elemento.ID))))
                {
                    var elementos = InfoTooltips.Where(i => i.Elemento != null && !itemCalculo.Calculos
                    .Where(o => !o.EsEntradasArchivo)
                    .Any(j => !j.ElementosOperaciones.Any(k => k.ID == i.Elemento.ID))).ToList();

                    while (elementos.Any())
                    {
                        //elementos.FirstOrDefault().DetenerHilo = true;
                        //try
                        //{
                        //    elementos.FirstOrDefault().Hilo_ToolTip.Interrupt();
                        //}
                        //catch (Exception) { }
                        //try { Thread.Sleep(Timeout.Infinite); } catch (Exception) { };
                        InfoTooltips.Remove(elementos.FirstOrDefault());
                        elementos.Remove(elementos.FirstOrDefault());
                    }
                }

                while (InfoTooltips.Any(i => i.SubElemento != null && !itemCalculo.Calculos.Where(o => !o.EsEntradasArchivo)
                    .Any(j => !j.ElementosOperaciones
                    .Any(k => !k.ElementosDiseñoOperacion.Any(l => l.ID == i.SubElemento.ID)))))
                {
                    var elementos = InfoTooltips.Where(i => i.SubElemento != null && !itemCalculo.Calculos.Where(o => !o.EsEntradasArchivo)
                    .Any(j => !j.ElementosOperaciones
                    .Any(k => !k.ElementosDiseñoOperacion.Any(l => l.ID == i.SubElemento.ID)))).ToList();

                    while (elementos.Any())
                    {
                        //elementos.FirstOrDefault().DetenerHilo = true;
                        //try
                        //{
                        //    elementos.FirstOrDefault().Hilo_ToolTip.Interrupt();
                        //}
                        //catch (Exception) { }
                        //try { Thread.Sleep(Timeout.Infinite); } catch (Exception) { };
                        InfoTooltips.Remove(elementos.FirstOrDefault());
                        elementos.Remove(elementos.FirstOrDefault());
                    }
                }

                while (InfoTooltips.Any(i => i.Calculo != null && !itemCalculo.Calculos.Any(j => j.ID == i.Calculo.ID)))
                {
                    var elementos = InfoTooltips.Where(i => i.Calculo != null && !Calculo.Calculos.Any(j => j.ID == i.Calculo.ID)).ToList();

                    while (elementos.Any())
                    {
                        //elementos.FirstOrDefault().DetenerHilo = true;
                        //try
                        //{
                        //    elementos.FirstOrDefault().Hilo_ToolTip.Interrupt();
                        //}
                        //catch (Exception) { }
                        //try { Thread.Sleep(Timeout.Infinite); } catch (Exception) { };
                        InfoTooltips.Remove(elementos.FirstOrDefault());
                        elementos.Remove(elementos.FirstOrDefault());
                    }
                }                
            }
        }

        public DatosTooltips_Elementos ObtenerDatosElementoCalculo_Tooltip(string IDDiseñoCalculo)
        {
            return InfoTooltips.FirstOrDefault(i => i.Calculo != null && i.Calculo.ID == IDDiseñoCalculo);
        }

        public DatosTooltips_Elementos ObtenerDatosElemento_Tooltip(string IDElemento)
        {
            return InfoTooltips.FirstOrDefault(i => i.Elemento != null && i.Elemento.ID == IDElemento);
        }

        public DatosTooltips_Elementos ObtenerDatosSubElemento_Tooltip(string IDSubElemento)
        {
            return InfoTooltips.FirstOrDefault(i => i.SubElemento != null && i.SubElemento.ID == IDSubElemento);
        }
    }
}

namespace ProcessCalc
{
    public partial class MainWindow : Window
    {
        public List<EjecucionTooltipsCalculo> EjecucionesToolTips { get; set; }
        public EjecucionTooltipsCalculo EjecucionTraspaso { get; set; }
        public bool Mostrada 
        {
            get
            {
                if (Dispatcher != null)
                {
                    if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                    {
                        return Dispatcher.Invoke(() => true);
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public void AgregarEjecucionToolTip(Calculo calculo)
        {
            EjecucionesToolTips.Add(new Entidades.Ejecuciones.ToolTips.EjecucionTooltipsCalculo()
            {
                Calculo = calculo,
                UltimoEstadoCalculo = new Calculo(),
                Ventana = this
            });

            EjecucionesToolTips.LastOrDefault().EstablecerEstadoCambiosToolTips_Entradas(true, calculo);
        }

        public void QuitarEjecucionToolTip(Calculo calculo)
        {
            var ejecucionToolTips = EjecucionesToolTips.FirstOrDefault(i => i.Calculo == calculo);

            if (ejecucionToolTips != null)
            {
                try
                {
                    //if (!Dispatcher.HasShutdownStarted &&
                    //            !Dispatcher.HasShutdownFinished)
                    //{
                    //    Dispatcher.Invoke(() =>
                    {
                        if (ejecucionToolTips.Ejecucion != null)
                        {
                            ejecucionToolTips.Ejecucion.Detener();

                            while (!ejecucionToolTips.Ejecucion.Finalizada)
                            {

                            }
                        }
                    } //);
                    //}
                }
                catch (Exception) { }

                EjecucionesToolTips.Remove(ejecucionToolTips);
            }
        }
    }
}

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        public EjecucionTooltipsCalculo TooltipsCalculo { get; set; }
    }
}
