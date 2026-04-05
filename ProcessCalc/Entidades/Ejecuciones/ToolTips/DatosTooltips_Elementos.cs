using ProcessCalc.Controles.Calculos;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace ProcessCalc.Entidades.Ejecuciones.ToolTips
{
    public class DatosTooltips_Elementos
    {
        public DiseñoOperacion Elemento { get; set; }
        public DiseñoOperacion ElementoAsociadoEntrada_OtroCalculo { get; set; }
        public DiseñoElementoOperacion SubElemento { get; set; }
        public DiseñoCalculo Calculo { get; set; }
        public EntidadNumero Numero { get; set; }
        public TipoOpcionToolTip Tipo {  get; set; }
        public double ValorNumerico { get; set; }
        public List<string> TextosInformacion {  get; set; }
        public List<DatosTooltips_Elementos> InfoElementos_Numeros { get; set; }
        public string Nombre { get; set; }
        public MainWindow Ventana { get; set; }
        public bool Digitacion {  get; set; }
        public bool SeleccionArchivosEntrada { get; set; }
        public bool Actualizar { get; set; }
        public TimeSpan? Hora { get; set; }
        public bool SeleccionEntradas { get; set; }
        public bool EntradaNoSeleccionada { get; set; }
        public bool Digitacion_TextoEnPantalla { get; set; }
        public string TextoEnPantalla { get; set; }
        public List<Clasificador> Clasificadores { get; set; }
        public List<Clasificador> Clasificadores_Operacion {  get; set; }
        public void EstabecerDatosTooltip(string nombre, double ValorNumerico, List<string> TextosInformacion)
        {
            this.InfoElementos_Numeros.Clear();

            this.Nombre = nombre?.ToString();
            this.ValorNumerico = ValorNumerico;
            this.TextosInformacion = TextosInformacion.ToList();
        }

        public void EstabecerDatosTooltip_Digitacion(string nombre, ElementoEntradaEjecucion item)
        {
            if(item != null)
                Elemento.AgregarClasificadoresGenericos_CantidadesDigitacion_Entrada(item);
            
            this.Nombre = nombre.ToString();
            this.ValorNumerico = Elemento.ValorNumerico_Digitacion_Tooltip;
            this.TextosInformacion = Elemento.TextosInformacion_Digitacion_Tooltip;

            if (item != null)
            {
                this.Clasificadores_Operacion.Clear();
                this.Clasificadores_Operacion.AddRange(item.Clasificadores_Cantidades);
            }
            else
            {
                this.Clasificadores_Operacion.Clear();
                this.Clasificadores_Operacion.AddRange(Elemento.Clasificadores);
            }

            
            this.InfoElementos_Numeros = new List<DatosTooltips_Elementos>();
            
            this.InfoElementos_Numeros.AddRange(Elemento.Cantidades_Digitacion_Tooltip.Select(i =>
            new DatosTooltips_Elementos()
            {
                Numero = i.CopiarObjeto(),
                Clasificadores = i.Clasificadores_SeleccionarOrdenar.ToList()
            }).ToList());
            

            this.Digitacion = true;            
        }

        public void EstabecerDatosTooltip_SeleccionArchivosEntrada(string nombre, ElementoEntradaEjecucion item)
        {
            if (item != null)
                Elemento.AgregarClasificadoresGenericos_CantidadesDigitacion_Entrada(item);

            this.Nombre = nombre.ToString();
            
            this.SeleccionArchivosEntrada = true;
        }

        public void EstabecerDatosTooltip_NoDigitacion(string nombre)
        {
            this.Digitacion = false;
        }

        public void EstabecerDatosTooltip_NoSeleccionArchivosEntrada(string nombre)
        {
            this.SeleccionArchivosEntrada = false;
        }

        public void EstabecerDatosTooltip_Digitacion_TextoEnPantalla(string nombre)
        {
            this.Nombre = nombre.ToString();
            this.TextoEnPantalla = Elemento.TextoEnPantalla_Digitacion_Tooltip;

            this.Digitacion = true;
            this.Digitacion_TextoEnPantalla = true;
        }

        public void EstabecerDatosTooltip_SeleccionEntradas(string nombre, bool valorSeleccionEntradas)
        {
            this.Nombre = nombre.ToString();

            this.SeleccionEntradas = valorSeleccionEntradas;
        }

        public void EstabecerDatosTooltip_EntradaNoSeleccionada(string nombre, bool Seleccionada)
        {
            this.Nombre = nombre?.ToString();
            this.EntradaNoSeleccionada = !Seleccionada;

            if(!Seleccionada)
            {
                this.Digitacion = false;
            }
        }

        public DatosTooltips_Elementos()
        {
            InfoElementos_Numeros = new List<DatosTooltips_Elementos>();
            Clasificadores_Operacion = new List<Clasificador>();            
        }
        public void EstabecerDatosTooltip_Elementos(List<EntidadNumero> Numeros,
            bool OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
            bool OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion)
        {
            //if(!this.Digitacion)
            InfoElementos_Numeros.Clear();
            Clasificadores_Operacion.Clear();

            foreach (var item in Numeros)
            {
                InfoElementos_Numeros.Add(new DatosTooltips_Elementos()
                {
                    Nombre = item.Nombre?.ToString(),
                    Numero = item,
                    ValorNumerico = item.Numero,
                    TextosInformacion = item.Textos,
                    Clasificadores = item.Clasificadores_SeleccionarOrdenar
                });

                foreach (var itemClasificador in item.Clasificadores_SeleccionarOrdenar)
                {
                    if (!Clasificadores_Operacion.Contains(itemClasificador))
                        Clasificadores_Operacion.Add(itemClasificador);
                }
            }

            List<Clasificador> listaClasificadoresOrdenada = new List<Clasificador>();

            if (OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion)
            {
                listaClasificadoresOrdenada = Clasificadores_Operacion.OrderBy(i => i.CadenaTexto).ToList();
            }
            else if (OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion)
            {
                listaClasificadoresOrdenada = Clasificadores_Operacion.OrderByDescending(i => i.CadenaTexto).ToList();
            }

            if (listaClasificadoresOrdenada.Any())
            {
                Clasificadores_Operacion.Clear();
                Clasificadores_Operacion.AddRange(listaClasificadoresOrdenada);
            }
        }

        public void EstablecerDatosTooltip_EnVentana()
        {
            try
            {
                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                {
                    
                    {
                        Ventana.Dispatcher.Invoke(() =>
                {

                    try
                    {
                        if (Ventana.ToolTip != null)
                        {
                            Ventana.ToolTip.ElementoAsociado = Elemento;

                            Ventana.ToolTip.EstablecerDigitacion(Digitacion);
                            Ventana.ToolTip.EstablecerSeleccionArchivosEntrada(SeleccionArchivosEntrada);
                            Ventana.ToolTip.EstablecerDigitacion_TextoEnPantalla(Digitacion_TextoEnPantalla, TextoEnPantalla);
                            Ventana.ToolTip.EstablecerSeleccionEntradas(SeleccionEntradas);
                            Ventana.ToolTip.EstablecerSeleccionEntradas_EntradaNoSeleccionada(EntradaNoSeleccionada);

                            if (Digitacion &&
                            !Digitacion_TextoEnPantalla)
                            {
                                var ejecucion = this.Ventana.EjecucionesToolTips.FirstOrDefault(i => i.InfoTooltips.Any(j => j == this));
                                if (ejecucion != null)
                                {
                                    var elementoEjecucion = ejecucion.Ejecucion.ObtenerElementoEjecucion(Elemento);
                                    if (elementoEjecucion != null)
                                    {
                                        InfoElementos_Numeros.Clear();
                                        InfoElementos_Numeros.AddRange(elementoEjecucion.ElementoDiseñoRelacionado.Cantidades_Digitacion_Tooltip.Select(i => new DatosTooltips_Elementos()
                                        {
                                            Nombre = i.Nombre?.ToString(),
                                            ValorNumerico = i.Numero,
                                            TextosInformacion = i.Textos.ToList(),
                                            Clasificadores = i.Clasificadores_SeleccionarOrdenar.ToList()
                                        }));

                                    }
                                    else
                                    {
                                        if (Elemento.Tipo == TipoElementoOperacion.Entrada &&
                                            Elemento.EntradaRelacionada.Tipo == TipoEntrada.ConjuntoNumeros)
                                        {
                                            InfoElementos_Numeros.Clear();
                                            InfoElementos_Numeros.AddRange(Elemento.Cantidades_Digitacion_Tooltip.Select(i => new DatosTooltips_Elementos()
                                            {
                                                Nombre = i.Nombre?.ToString(),
                                                ValorNumerico = i.Numero,
                                                TextosInformacion = i.Textos.ToList(),
                                                Clasificadores = i.Clasificadores_SeleccionarOrdenar.ToList()
                                            }));
                                        }
                                    }
                                }

                            }

                            if (SeleccionEntradas)
                            {
                                Ventana.ToolTip.ElementoAsociado = Elemento;
                            }

                            Ventana.ToolTip.infoEstado.Visibility = System.Windows.Visibility.Collapsed;

                            if (!SeleccionEntradas)
                            {
                                Ventana.ToolTip.infoNumeros.Visibility = System.Windows.Visibility.Collapsed;
                                //Ventana.ToolTip.infoNumeros.Children.Clear();

                                if (InfoElementos_Numeros.Any())
                                {
                                    //Ventana.ToolTip.infoNumero.Visibility = System.Windows.Visibility.Collapsed;
                                    Ventana.ToolTip.infoNumeros.Visibility = System.Windows.Visibility.Visible;
                                    //Ventana.ToolTip.infoNumeros.Children.Clear();

                                    //foreach (var itemClasificador in Clasificadores_Operacion)
                                    //{
                                    //    if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                                    //    {
                                    //        Ventana.ToolTip.AgregarClasificador(itemClasificador.CadenaTexto);
                                    //    }

                                    //    foreach (var itemNumero in InfoElementos_Numeros.Where(i => i.Clasificadores.Any(i => i == itemClasificador)))
                                    //    {
                                    //        if (itemNumero.Numero != null)
                                    //        {
                                    //            Ventana.ToolTip.AgregarNumero(itemNumero.Numero.Nombre,
                                    //                itemNumero.Numero.Numero,
                                    //                itemNumero.Numero.Textos);
                                    //        }
                                    //        else
                                    //        {
                                    //            Ventana.ToolTip.AgregarNumero(itemNumero.Nombre,
                                    //                itemNumero.ValorNumerico,
                                    //                itemNumero.TextosInformacion);
                                    //        }
                                    //    }
                                    //}

                                    var grupos = new List<ClasificadorGrupo>();
                                    bool clasificadorNulo_Seteado = false;

                                    foreach (var itemClasificador in Clasificadores_Operacion.Distinct())
                                    {
                                        var numeros = InfoElementos_Numeros
                                            .Where(n => n.Clasificadores.Any(c => ReferenceEquals(c, itemClasificador)))
                                            .Select(n => n.Numero != null
                                                ? new NumeroItem
                                                {
                                                    Nombre = n.Numero.Nombre,
                                                    Valor = n.Numero.Numero,
                                                    Textos = n.Numero.Textos
                                                }
                                                : new NumeroItem
                                                {
                                                    Nombre = n.Nombre,
                                                    Valor = n.ValorNumerico,
                                                    Textos = n.TextosInformacion
                                                })
                                            .ToList();

                                        if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                                        Clasificadores_Operacion.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))
                                        && clasificadorNulo_Seteado)
                                            continue;

                                        if (string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                                            clasificadorNulo_Seteado = true;

                                        grupos.Add(new ClasificadorGrupo
                                        {
                                            Nombre = itemClasificador.CadenaTexto,
                                            Numeros = numeros
                                        });
                                    }

                                    // Enlaza al control (por ejemplo, el Ventana.ToolTip/Popup/panel)
                                    Ventana.ToolTip.infoNumeros.DataContext = new { Grupos = grupos };
                                }
                            }

                            if (EntradaNoSeleccionada)
                            {
                                Ventana.ToolTip.infoEntradaNoSeleccionada.Visibility = System.Windows.Visibility.Visible;
                                Ventana.ToolTip.infoNumeros.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            else
                            {
                                Ventana.ToolTip.infoEntradaNoSeleccionada.Visibility = System.Windows.Visibility.Collapsed;
                            }

                            if (Hora.HasValue)
                            {
                                Ventana.ToolTip.infoHora.Text = "Hora: " + Hora.Value.Hours.ToString("00") + ":" +
                Hora.Value.Minutes.ToString("00");
                                Ventana.ToolTip.infoHora.Visibility = System.Windows.Visibility.Visible;
                            }
                        }
                    }
                    catch (Exception) { }
                });

                        Actualizar = false;
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
