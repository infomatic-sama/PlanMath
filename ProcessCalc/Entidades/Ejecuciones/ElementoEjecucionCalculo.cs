using ProcessCalc.Controles;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Ejecuciones;
using ProcessCalc.Entidades.Ejecuciones.ToolTips;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProcessCalc.Entidades.ElementoDiseñoOperacionAritmeticaEjecucion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public class ElementoEjecucionCalculo
    {
        public TipoElementoEjecucion Tipo { get; set; }
        public bool DefinicionSimple_TextosInformacion { get; set; }
        public int HashCode_ElementoDiseñoOperacionCalculo { get; set; }
        public DiseñoOperacion ElementoDiseñoRelacionado { get; set; }
        public EstadoEjecucion Estado { get; set; }
        public List<EtapaOperacionEjecucion> Etapas { get; set; }
        public int CantidadSubElementos { get; set; }
        public int CantidadSubElementosProcesados { get; set; }
        public int CantidadEtapas { get; set; }
        public int CantidadEtapasProcesadas { get; set; }
        public DiseñoCalculo ElementoDiseñoCalculoRelacionado { get; set; }
        public string Nombre { get; set; }
        public List<string> Textos { get; set; }
        public List<EtapaEjecucion> etapasBuscarEntradas { get; set; }
        public bool ConsiderarProcesamiento_SeleccionarOrdenar { get; set; }
        public bool ConsiderarProcesamiento_Agrupamiento { get; set; }
        public List<DuplaTextoInformacion_Cantidad_SeleccionarOrdenar> TextoInformacionAnterior_SeleccionOrdenamiento { get; set; }
        public bool ConsiderarProcesamiento_CondicionFlujo { get; set; }
        public List<string> TextosInformacionInvolucrados_CondicionSeleccionarOrdenar { get; set; }
        public List<EntidadNumero> NumerosFiltrados_Condiciones { get; set; }
        public List<EntidadNumero> NumerosElementosFiltrados_Condiciones { get; set; }
        public int HashCode_NumeroAgregacion_Ejecucion { get; set; }
        public bool SeleccionEntradasNoSeleccionada { get; set; }
        public bool SeleccionEntradasProcesadaSeleccion { get; set; }
        public bool ContieneSalida_Ejecucion { get; set; }
        
        [IgnoreDataMember]
        public List<Clasificador> Clasificadores_Cantidades { get; set; }
        [IgnoreDataMember]
        public bool FinalIndicePosicionClasificadores { get; set; }
        [IgnoreDataMember]
        public int IndicePosicionClasificadores { get; set; }
        [IgnoreDataMember]
        public int CantidadNumeros_Clasificador { get; set; }
        public List<CondicionTextosInformacion> CondicionesTextosInformacion_SeleccionOrdenamiento { get; set; }
        public List<CondicionTextosInformacion> CondicionesTextosInformacion_SeleccionEntradas { get; set; }
        public List<AsignacionImplicacion_TextosInformacion> Relaciones_TextosInformacion { get; set; }
        public int CantidadTextosInformacion_SeleccionarOrdenar { get; set; }
        public bool DefinicionSimple_CondicionesFlujo { get; set; }
        public List<CondicionFlujo> CondicionesFlujo_SeleccionOrdenamiento { get; set; }
        public ElementoOperacionAritmeticaEjecucion itemOperacion_Ejecucion_DefinicionNombres { get; set; }
        [IgnoreDataMember]
        public bool DetenerProcesamiento;
        [IgnoreDataMember]
        public bool MantenerProcesamiento_Operandos;
        [IgnoreDataMember]
        public bool MantenerProcesamiento_Resultados;        
        [IgnoreDataMember]
        public List<ResultadoCondicionProcesamientoCantidades> ResultadosCondiciones_ProcesamientoCantidades { get; set; }
        [IgnoreDataMember]
        public List<ResultadoCondicionProcesamientoCantidades_Filas> ResultadosCondiciones_ProcesamientoCantidades_Filas {  get; set; }
        public bool ReiniciarAcumulacion_OperarPorFilas { get; set; }
        public int PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar { get; set; }
        public int PosicionActualNumero_CondicionesOperador_Implicacion { get; set; }
        public int TotalElementos_CondicionesOperador_SeleccionarOrdenar
        {
            get
            {
                if (this.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                {
                    if (((ElementoOperacionAritmeticaEjecucion)this).Numeros.Any())
                        return ((ElementoOperacionAritmeticaEjecucion)this).Numeros.Count;
                    else
                        return 1;
                }
                else if (this.GetType() == typeof(ElementoConjuntoTextosEntradaEjecucion))
                {
                    if (((ElementoConjuntoTextosEntradaEjecucion)this).FilasTextosInformacion.Any())
                        return ((ElementoConjuntoTextosEntradaEjecucion)this).FilasTextosInformacion.Count;
                    else
                        return 1;
                }
                else if (this.GetType() == typeof(ElementoEntradaEjecucion))
                {
                    if (((ElementoEntradaEjecucion)this).Numeros.Any())
                        return ((ElementoEntradaEjecucion)this).Numeros.Count;
                    else
                        return 1;
                }
                else
                    return 0;
            }
        }
        public List<string> TextosInformacion_ElementoActual_Implicacion(EntidadNumero CantidadCumpleCondiciones)
        {
            {
                if (this.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                {
                    if (((ElementoOperacionAritmeticaEjecucion)this).Numeros.Any())
                    {
                        if (PosicionActualNumero_CondicionesOperador_Implicacion <= ((ElementoOperacionAritmeticaEjecucion)this).Numeros.Count - 1)
                        {
                            if (((ElementoEntradaEjecucion)this).Numeros[PosicionActualNumero_CondicionesOperador_Implicacion] == CantidadCumpleCondiciones)
                                return CantidadCumpleCondiciones.Textos;
                            else
                                return new List<string>();
                        }
                        else
                            return Textos;
                    }
                    else
                        return Textos;
                }
                else if (this.GetType() == typeof(ElementoConjuntoTextosEntradaEjecucion))
                {
                    if (((ElementoConjuntoTextosEntradaEjecucion)this).FilasTextosInformacion.Any())
                    {
                        if (PosicionActualNumero_CondicionesOperador_Implicacion <= ((ElementoConjuntoTextosEntradaEjecucion)this).FilasTextosInformacion.Count - 1)
                            return ((ElementoConjuntoTextosEntradaEjecucion)this).FilasTextosInformacion[PosicionActualNumero_CondicionesOperador_Implicacion].TextosInformacion;
                        else
                            return Textos;
                    }
                    else
                        return Textos;
                }
                else if (this.GetType() == typeof(ElementoEntradaEjecucion))
                {
                    if (((ElementoEntradaEjecucion)this).Numeros.Any())
                    {
                        if (PosicionActualNumero_CondicionesOperador_Implicacion <= ((ElementoEntradaEjecucion)this).Numeros.Count - 1)
                        {
                            if (((ElementoEntradaEjecucion)this).Numeros[PosicionActualNumero_CondicionesOperador_Implicacion] == CantidadCumpleCondiciones)
                                return CantidadCumpleCondiciones.Textos;
                            else
                                return new List<string>();
                        }
                        else
                            return Textos;
                    }
                    else
                        return Textos;
                }
                else
                    return Textos;
            }
        }
        public List<string> TodosTextosInformacion_ElementoActual_Implicacion(EntidadNumero CantidadCumpleCondiciones)
        {
            {
                if (this.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                {
                    if (((ElementoOperacionAritmeticaEjecucion)this).Numeros.Any())
                    {
                        List<string> textos = new List<string>();
                        if (((ElementoOperacionAritmeticaEjecucion)this).Numeros.Contains(CantidadCumpleCondiciones))
                        {
                            textos.AddRange(CantidadCumpleCondiciones.Textos);
                        }

                        return textos;
                    }
                    else
                        return Textos;
                }
                else if (this.GetType() == typeof(ElementoConjuntoTextosEntradaEjecucion))
                {
                    if (((ElementoConjuntoTextosEntradaEjecucion)this).FilasTextosInformacion.Any())
                    {
                        List<string> textos = new List<string>();

                        foreach (var item in ((ElementoConjuntoTextosEntradaEjecucion)this).FilasTextosInformacion.Select(item => item.TextosInformacion))
                            textos.AddRange(item);

                        return textos;
                    }
                    else
                        return Textos;
                }
                else if (this.GetType() == typeof(ElementoEntradaEjecucion))
                {
                    if (((ElementoEntradaEjecucion)this).Numeros.Any())
                    {
                        List<string> textos = new List<string>();
                        if (((ElementoEntradaEjecucion)this).Numeros.Contains(CantidadCumpleCondiciones))
                        {
                            textos.AddRange(CantidadCumpleCondiciones.Textos);
                        }

                        return textos;
                    }
                    else
                        return Textos;
                }
                else
                    return Textos;
            }
        }
        [IgnoreDataMember]
        public int CantidadFilasInsertadas_ProcesamientoCantidades;        
        [IgnoreDataMember]
        public bool ToolTipMostrado { get; set; }
        [IgnoreDataMember]
        public bool ToolTipAMostrar { get; set; }

        public bool ConModificaciones_ToolTipMostrado(EjecucionTooltipsCalculo TooltipsCalculo, EjecucionCalculo ejecucion)
        {
            //get
            {
                if (GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion))
                {
                    if (((ElementoDiseñoOperacionAritmeticaEjecucion)this).ElementosOperacion.Any(i => i.ToolTipMostrado))
                        return true;
                    else
                        return false;
                }
                else if (GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                {
                    if (((ElementoOperacionAritmeticaEjecucion)this).ElementosOperacion.Any(i => i.ToolTipMostrado))
                        return true;
                    else
                        return false;
                }
                else if (GetType() == typeof(ElementoCalculoEjecucion))
                {
                    if (((ElementoCalculoEjecucion)this).ElementoDiseñoCalculoRelacionado.ElementosOperaciones.Any(i => {
                        var elementoEjecucion = ejecucion.ObtenerElementoEjecucion(i);
                        var elementoDiseño = TooltipsCalculo.ObtenerElementoDiseño(this.ElementoDiseñoCalculoRelacionado.ID, i.ID);
                        return (elementoEjecucion != null && elementoEjecucion.ConModificaciones_ToolTipMostrado(TooltipsCalculo, ejecucion)) ||
                        (elementoDiseño != null && (elementoDiseño.ConCambios_ToolTips || elementoDiseño.Actualizar_ToolTips));
                    }))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }
        [IgnoreDataMember]
        public bool VolverAActualizar {  get; set; }
        [IgnoreDataMember]
        public List<Clasificador> Clasificadores_Cantidades_Originales_Temp { get; set; }
        public ElementoEjecucionCalculo()
        {
            CantidadSubElementos = 1;
            Textos = new List<string>();
            etapasBuscarEntradas = new List<EtapaEjecucion>();
            ConsiderarProcesamiento_SeleccionarOrdenar = true;
            ConsiderarProcesamiento_Agrupamiento = true;
            TextoInformacionAnterior_SeleccionOrdenamiento = new List<DuplaTextoInformacion_Cantidad_SeleccionarOrdenar>();
            ConsiderarProcesamiento_CondicionFlujo = true;
            NumerosFiltrados_Condiciones = new List<EntidadNumero>();
            NumerosElementosFiltrados_Condiciones = new List<EntidadNumero>();
            Clasificadores_Cantidades = new List<Clasificador>();
            Clasificadores_Cantidades_Originales_Temp = new List<Clasificador>();
            ResultadosCondiciones_ProcesamientoCantidades = new List<ResultadoCondicionProcesamientoCantidades>();
            ResultadosCondiciones_ProcesamientoCantidades_Filas = new List<ResultadoCondicionProcesamientoCantidades_Filas>();
        }
        private static int ContarInterseccion(IEnumerable<ElementoEjecucionCalculo> lista1, IEnumerable<ElementoEjecucionCalculo> lista2)
        {
            int contarIguales = 0;

            foreach (var item1 in lista1)
            {
                foreach (var item2 in lista2)
                {
                    if (item1.HashCode_ElementoDiseñoOperacionCalculo == item2.HashCode_ElementoDiseñoOperacionCalculo)
                        contarIguales++;
                }
            }

            return contarIguales;
        }

        public void OrdenarTextosInformacion(OrdenacionNumeros ordenacion)
        {
            if (ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
            {
                if (ordenacion.OrdenarTextosDeMenorAMayor)
                    Textos = Textos.OrderBy(i => i).ToList();
                else if (ordenacion.OrdenarTextosDeMayorAMenor)
                    Textos = Textos.OrderByDescending(i => i).ToList();
            }
        }

        public void OrdenarTextosInformacion_SinOrdenarCantidades(OrdenacionNumeros ordenacion)
        {
            if (ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
            {
                if (ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades)
                    Textos = Textos.OrderBy(i => i).ToList();
                else if (ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades)
                    Textos = Textos.OrderByDescending(i => i).ToList();
            }
        }

        public void EstablecerListasCantidadesOriginales_Operandos() { }
        public void RestaurarListasCantidadesOriginales_Operandos() { }

        public string ObtenerTextosInformacion_Cadena()
        {
            string cadena = string.Empty;

            if (Textos.Any(i => !string.IsNullOrEmpty(i)))
            {
                cadena += " Cadenas de texto: ";

                foreach (var itemTexto in Textos.Where(i => !string.IsNullOrEmpty(i)))
                {
                    cadena += itemTexto + ((itemTexto == Textos.LastOrDefault()) ? "." : ", ");
                }
            }

            return cadena;
        }

        public void InicializarPosicionesClasificadores_Operandos()
        {
            if (GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
            {
                foreach (var itemOperacion in ((ElementoOperacionAritmeticaEjecucion)this).ElementosOperacion)
                {
                    itemOperacion.IndicePosicionClasificadores = 0;
                }
            }
        }

        public void AumentarPosicionesClasificadores_Operandos()
        {
            if (GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
            {
                foreach (var itemOperacion in ((ElementoOperacionAritmeticaEjecucion)this).ElementosOperacion)
                {
                    if (itemOperacion.IndicePosicionClasificadores < itemOperacion.Clasificadores_Cantidades.Count - 1)
                        itemOperacion.IndicePosicionClasificadores++;
                }
            }
        }

        public void SetearToolTipMostrado_ElementosInternos(EjecucionCalculo ejecucion)
        {
            
            {
                foreach (var itemEntrada in ElementoDiseñoRelacionado.ElementosAnteriores.Where(i => i.Tipo == TipoElementoOperacion.Entrada))
                {
                    var elementoEntradaEjecucion = ejecucion.ObtenerElementoEjecucion(itemEntrada);

                    if (elementoEntradaEjecucion != null)
                    {
                        foreach (var item in ElementoDiseñoRelacionado.ElementosDiseñoOperacion)
                        {
                            var elementoEjecucion = ejecucion.ObtenerSubElementoEjecucion(item);
                            if (elementoEjecucion != null &&
                                elementoEjecucion.EntradaEjecucion == elementoEntradaEjecucion)
                            {
                                if (elementoEntradaEjecucion.ToolTipMostrado)
                                {
                                    elementoEjecucion.ToolTipAMostrar = true;
                                }
                            }
                        }
                    }
                }

                foreach (var itemEntrada in ElementoDiseñoRelacionado.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.Entrada))
                {
                    var elementoEntradaEjecucion = ejecucion.ObtenerElementoEjecucion(itemEntrada);

                    if (elementoEntradaEjecucion != null)
                    {
                        foreach (var item in ElementoDiseñoRelacionado.ElementosDiseñoOperacion)
                        {
                            var elementoEjecucion = ejecucion.ObtenerSubElementoEjecucion(item);
                            if (elementoEjecucion != null &&
                                elementoEjecucion.OperacionEjecucion == elementoEntradaEjecucion)
                            {
                                if (elementoEntradaEjecucion.ToolTipMostrado)
                                {
                                    elementoEjecucion.ToolTipAMostrar = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
