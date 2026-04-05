using ProcessCalc.Controles;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Ejecuciones;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public class ElementoOperacionAritmeticaEjecucion : ElementoEjecucionCalculo
    {
        public TipoOperacionAritmeticaEjecucion TipoOperacion { get; set; }
        public List<ElementoOperacionAritmeticaEjecucion> ElementosOperacion { get; set; }
        public List<EntidadNumero> Numeros { get; set; }
        public int CantidadNumeros_Ejecucion { get; set; }
        public EntidadNumero EntidadNumero_Ejecucion_DefinicionNombres { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacionAgregadosInstancias_Resultado;
        [IgnoreDataMember]
        public List<string> TextosInformacionAgregadosInstancias_Operando; 
        public List<EntidadNumero> NumerosFiltrados { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> ListaCantidadesOriginalOperandos;
        [IgnoreDataMember]
        public List<EntidadNumero> ListaOriginal_Numeros;
        [IgnoreDataMember]
        public EntidadNumero NumeroResultado_PorFilas { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacion_CumplenCondiciones_Anteriores {  get; set; }
        [IgnoreDataMember]
        public bool TieneCondicionesTextos_NoCumplen {  get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacionInvolucrados_CondicionTextos { get; set; }
        [IgnoreDataMember]
        public bool ConsiderarOperandoCondicion_SiCumple {  get; set; }
        [IgnoreDataMember]
        public bool ActualizarTooltips_Operandos { get; set; }
        [IgnoreDataMember]
        public bool Seteo_ConsiderarOperandoCondicion_SiCumple { get; set; }        
        public ElementoOperacionAritmeticaEjecucion()
        {
            Etapas = new List<EtapaOperacionEjecucion>();
            CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionTextosInformacion>();
            CondicionesTextosInformacion_SeleccionEntradas = new List<CondicionTextosInformacion>();
            CondicionesFlujo_SeleccionOrdenamiento = new List<CondicionFlujo>();
            TextosInformacionAgregadosInstancias_Resultado = new List<string>();
            TextosInformacionAgregadosInstancias_Operando = new List<string>();            
            TextosInformacion_CumplenCondiciones_Anteriores = new List<string>();
            Numeros = new List<EntidadNumero>();
            ElementosOperacion = new List<ElementoOperacionAritmeticaEjecucion>();
            NumerosFiltrados = new List<EntidadNumero>();
            TextosInformacionInvolucrados_CondicionSeleccionarOrdenar = new List<string>();
            TextosInformacionInvolucrados_CondicionTextos = new List<string>();
        }

        public bool VerificarEjecucion_OperandosConCondiciones()
        {
            bool sinOperandos = true;

            foreach (var itemOperacion in ElementosOperacion)
            {
                bool operandoConNumeros = true;
                bool conNumeros = false;

                foreach (var subItem_ElementoOperacion in itemOperacion.Numeros)
                {
                    if ((!subItem_ElementoOperacion.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                        (subItem_ElementoOperacion.ElementosSalidaOperacion_Agrupamiento.Contains(this)))
                    {
                        if ((!subItem_ElementoOperacion.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
                    (subItem_ElementoOperacion.ElementosSalidaOperacion_CondicionFlujo.Contains(this)))
                        {
                            conNumeros = true;
                        }
                    }
                }

                if (!conNumeros) operandoConNumeros = false;

                if (operandoConNumeros) sinOperandos = false;
            }

            if (sinOperandos && ElementoDiseñoRelacionado != null && !ElementoDiseñoRelacionado.Ejecutar_SiTieneOtrosOperandosValidos)
                return false;
            else
                return true;
        }

        public void OrdenarElementos(List<EntidadNumero> Numeros, bool OrdenarNumerosDeMenorAMayor, bool OrdenarNumerosDeMayorAMenor,
            bool OrdenarPorCantidad, bool OrdenarPorNombre, 
            TipoOpcion_OrdenamientoNumerosSalidas Tipo_OrdenamientoNumeros,
            EjecucionCalculo ejecucion, List<OrdenacionNumeros> Ordenaciones,
            bool RevertirLista)
        {
            if (OrdenarNumerosDeMenorAMayor)
            {
                if (OrdenarPorCantidad)
                    Numeros = Numeros.OrderBy(i => i.Numero).ToList();
                else if (OrdenarPorNombre)
                {
                    //ElementosOperacion = ElementosOperacion.OrderBy(i => i.Nombre).ToList();
                    Numeros = ejecucion.OrdenarNumeros_Elemento(Numeros, Tipo_OrdenamientoNumeros,
                                        OrdenarNumerosDeMenorAMayor,
                                        OrdenarNumerosDeMayorAMenor, Ordenaciones, RevertirLista);
                }
            }
            else if (OrdenarNumerosDeMayorAMenor)
            {
                if (OrdenarPorCantidad)
                    Numeros = Numeros.OrderByDescending(i => i.Numero).ToList();
                else if (OrdenarPorNombre)
                {
                    //ElementosOperacion = ElementosOperacion.OrderByDescending(i => i.Nombre).ToList();
                    Numeros = ejecucion.OrdenarNumeros_Elemento(Numeros, Tipo_OrdenamientoNumeros,
                                        OrdenarNumerosDeMenorAMayor,
                                        OrdenarNumerosDeMayorAMenor, Ordenaciones, RevertirLista);
                }
            }
        }

        public void OrdenarTextosInformacion_ElementosLista(List<EntidadNumero> Numeros, OrdenacionNumeros ordenacion)
        {
            foreach (var item in Numeros)
            {
                item.OrdenarTextosInformacion(ordenacion);
            }
        }

        public void OrdenarTextosInformacion_ElementosLista_SinOrdenarCantidades(List<EntidadNumero> Numeros, OrdenacionNumeros ordenacion)
        {
            foreach (var item in Numeros)
            {
                item.OrdenarTextosInformacion(ordenacion);
            }
        }

        public void InicializarPosicionesElementosExterioresCondiciones(EjecucionCalculo ejecucion,
            bool SetearPosicion_Operacion = true)
        {
            if(Relaciones_TextosInformacion != null)
            {
                foreach(var itemRelacion in Relaciones_TextosInformacion)
                {
                    if (itemRelacion.Condiciones_TextoCondicion != null)
                    {
                        //itemRelacion.Condiciones_TextoCondicion.InicializarPosicionesElementosCondicion(ejecucion, itemRelacion.DiseñoTextosInformacion_Calculo);
                        foreach (var itemCondicion in itemRelacion.Condiciones_TextoCondicion.Condiciones)
                        {
                            itemCondicion.InicializarPosicionesElementosCondicion(ejecucion, itemRelacion.DiseñoTextosInformacion_Calculo);
                        }
                    }

                    itemRelacion.DiseñoTextosInformacion_Relacionado.InicializarPosicionesElementosEntradas();
                }
            }

            if(SetearPosicion_Operacion)
                PosicionActualNumero_CondicionesOperador_Implicacion = 0;
        }

        public void AumentarPosicionesElementosExterioresCondiciones(EjecucionCalculo ejecucion,
            bool SetearPosicion_Operacion = true)
        {
            if (Relaciones_TextosInformacion != null)
            {
                foreach (var itemRelacion in Relaciones_TextosInformacion)
                {
                    if (itemRelacion.Condiciones_TextoCondicion != null)
                    {
                        //itemRelacion.Condiciones_TextoCondicion.AumentarPosicionesElementosCondicion(ejecucion, this, itemRelacion.DiseñoTextosInformacion_Calculo);
                        foreach (var itemCondicion in itemRelacion.Condiciones_TextoCondicion.Condiciones)
                        {
                            itemCondicion.AumentarPosicionesElementosCondicion(ejecucion, this, itemRelacion.DiseñoTextosInformacion_Calculo);
                        }
                        //itemRelacion.Condiciones_TextoCondicion.AumentarPosicionesElementosCondicion_EntradasTextos(itemRelacion);
                    }
                }
            }

            if (SetearPosicion_Operacion)
                PosicionActualNumero_CondicionesOperador_Implicacion++;
        }

        public void AumentarPosicionesElementosOperandos(EjecucionCalculo ejecucion, ElementoOperacionAritmeticaEjecucion operandoActual)
        {
            foreach (var itemOperacion in this.ElementosOperacion)
            {
                if (itemOperacion != null && itemOperacion != this && itemOperacion != operandoActual)
                    itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
            }
        }

        public void InicializarPosicionesElementosOperandos(EjecucionCalculo ejecucion, ElementoOperacionAritmeticaEjecucion operandoActual)
        {
            foreach (var itemOperacion in this.ElementosOperacion)
            {
                if (itemOperacion != null && itemOperacion != this && itemOperacion != operandoActual)
                    itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
            }
        }

        public static bool FiltrarNumeros(EntidadNumero i, ElementoOperacionAritmeticaEjecucion elementoOperacion,
            List<EntidadNumero> NumerosFiltrados)
        {
            return (!i.ElementosSalidaOperacion_SeleccionarOrdenar.Any() || (i.ElementosSalidaOperacion_SeleccionarOrdenar.Any() &&
                            i.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(elementoOperacion))) &
                            (!i.ElementosSalidaOperacion_Agrupamiento.Any() || (i.ElementosSalidaOperacion_Agrupamiento.Any() &&
                            i.ElementosSalidaOperacion_Agrupamiento.Contains(elementoOperacion))) &
                            (!i.ElementosSalidaOperacion_CondicionFlujo.Any() || (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(elementoOperacion))) &
                            (!NumerosFiltrados.Any() || NumerosFiltrados.Contains(i));
        }

        public void LimpiarTextosInformacionAgregadosInstancias_Resultado_Operandos()
        {            
            foreach (var item in Relaciones_TextosInformacion)
            {
                item.TextosInformacionAgregadosInstancias_Resultado_Operandos.Clear();
                foreach(var itemInstancia in item.InstanciasAsignacion)
                {
                    foreach (var itemAsig in itemInstancia.Operandos_AsignarTextosInformacionA)
                        itemAsig.TextosInformacionAgregadosInstancias_Resultado_Operandos.Clear();
                }
            }
        }

        public void LimpiarTextosInformacionAgregadosInstancias_Resultado_Operacion()
        {
            TextosInformacionAgregadosInstancias_Resultado.Clear();
            foreach (var item in Relaciones_TextosInformacion)
            {
                item.TextosInformacionAgregadosInstancias_Resultado.Clear();
            }
        }

        public void LimpiarTextosInformacionAgregadosInstancias_Resultado_Condiciones()
        {
            foreach (var item in Relaciones_TextosInformacion)
            {
                item.TextosInformacionAgregadosInstancias_Resultado.Clear();
                foreach (var itemInstancia in item.InstanciasAsignacion)
                {
                    foreach (var itemAsig in itemInstancia.Operandos_AsignarTextosInformacionA)
                        itemAsig.TextosInformacionAgregadosInstancias_Resultado.Clear();
                }
            }
        }

        public void ProcesarCantidades(EjecucionCalculo ejecucion, ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperando, EntidadNumero numeroOperando, EntidadNumero numeroResultado, 
            Clasificador itemClasificador, List<EntidadNumero> NumerosResultado,
            bool? evaluarOperandoNumero_OperarPorFilas = null)
        {
            foreach (var itemCondiciones in ElementoDiseñoRelacionado.ProcesamientoCantidades)
            {
                //if (((itemCondiciones.TipoElemento == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados |
                //    itemCondiciones.TipoElemento == TipoOpcionElementoCondicionProcesamientoCantidades.Operando) &&
                //    (itemCondiciones.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperando?.ElementoDiseñoRelacionado))) ||
                //    itemCondiciones.TipoElemento == TipoOpcionElementoCondicionProcesamientoCantidades.Resultados)
                {
                    if (evaluarOperandoNumero_OperarPorFilas == null || (evaluarOperandoNumero_OperarPorFilas != null &&
                    (evaluarOperandoNumero_OperarPorFilas == true && itemCondiciones.FiltrarPorNumeros) ||
                    (!evaluarOperandoNumero_OperarPorFilas == false && itemCondiciones.FiltrarPorElementos)))
                    {
                        if ((numeroOperando != null && ((itemCondiciones.AplicarProcesamiento_CantidadesInsertadas_Operandos &&
                            (!numeroOperando.EsCantidadInsertada_ProcesamientoCantidades ||
                            numeroOperando.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (itemCondiciones.AplicarProcesamiento_SoloCantidadesInsertadas_Operandos &&
                            numeroOperando.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!itemCondiciones.AplicarProcesamiento_CantidadesInsertadas_Operandos &&
                            !itemCondiciones.AplicarProcesamiento_SoloCantidadesInsertadas_Operandos &&
                            !numeroOperando.EsCantidadInsertada_ProcesamientoCantidades))) &&

                        (numeroResultado != null && ((itemCondiciones.AplicarProcesamiento_CantidadesInsertadas_Operandos &&
                            (!numeroResultado.EsCantidadInsertada_ProcesamientoCantidades ||
                            numeroResultado.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (itemCondiciones.AplicarProcesamiento_SoloCantidadesInsertadas_Operandos &&
                            numeroResultado.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!itemCondiciones.AplicarProcesamiento_CantidadesInsertadas_Operandos &&
                            !itemCondiciones.AplicarProcesamiento_SoloCantidadesInsertadas_Operandos &&
                            !numeroResultado.EsCantidadInsertada_ProcesamientoCantidades))))
                        {
                            var itemOperandoParametro = itemOperando;

                            bool cumpleCondicionesProcesamiento = false;

                            if (itemCondiciones.CondicionesCantidades != null)
                            {
                                switch (itemCondiciones.TipoElemento)
                                {
                                    case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                                        cumpleCondicionesProcesamiento = itemCondiciones.CondicionesCantidades.EvaluarCondiciones(new AsignacionImplicacion_TextosInformacion(), ejecucion,
                                    this.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? this : null,
                                    this.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)this : null,
                                    itemOperandoParametro, numeroOperando, NumerosResultado);

                                        break;

                                    case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                                        cumpleCondicionesProcesamiento = itemCondiciones.CondicionesCantidades.EvaluarCondiciones(new AsignacionImplicacion_TextosInformacion(), ejecucion,
                                   this.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? this : null,
                                    this.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)this : null,
                                    this, numeroResultado, NumerosResultado);

                                        break;

                                    case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                                        cumpleCondicionesProcesamiento = itemCondiciones.CondicionesCantidades.EvaluarCondiciones(new AsignacionImplicacion_TextosInformacion(), ejecucion,
                                    this.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? this : null,
                                    this.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)this : null,
                                    itemOperandoParametro, numeroOperando, NumerosResultado);

                                        if (!cumpleCondicionesProcesamiento)
                                        {
                                            cumpleCondicionesProcesamiento = itemCondiciones.CondicionesCantidades.EvaluarCondiciones(new AsignacionImplicacion_TextosInformacion(), ejecucion,
                                   this.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? this : null,
                                    this.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)this : null,
                                    this, numeroResultado, NumerosResultado);
                                        }

                                        break;
                                }
                            }
                            else
                                cumpleCondicionesProcesamiento = true;

                            ResultadoCondicionProcesamientoCantidades resultadoCondicion = new ResultadoCondicionProcesamientoCantidades();
                            resultadoCondicion.ID = App.GenerarID_Elemento();

                            if (itemCondiciones.TipoElemento != TipoOpcionElementoCondicionProcesamientoCantidades.Resultados)
                            {
                                if (itemOperando != null &&
                                itemOperando.ElementoDiseñoRelacionado != null)
                                    EstablecerEstadoProcesamiento(resultadoCondicion, cumpleCondicionesProcesamiento, itemCondiciones, operacion, itemOperando.ElementoDiseñoRelacionado, itemOperando.ElementoDiseñoRelacionado.EntradaRelacionada);
                                else
                                    EstablecerEstadoProcesamiento(resultadoCondicion, cumpleCondicionesProcesamiento, itemCondiciones, operacion, null, null);
                            }
                            else
                            {
                                EstablecerEstadoProcesamiento(resultadoCondicion, cumpleCondicionesProcesamiento, itemCondiciones, operacion, null, null);
                            }

                            if (resultadoCondicion.InsertarCantidad_Procesamiento_Operandos &&
                                itemCondiciones.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperando?.ElementoDiseñoRelacionado))
                            {
                                resultadoCondicion.OperandosInsertar_CantidadesProcesamientoCantidades.Add(itemOperando?.ElementoDiseñoRelacionado);
                            }

                            if (cumpleCondicionesProcesamiento &&
                                itemCondiciones.NoIncluirTextosInformacion_CantidadAInsertar)
                            {
                                if (numeroOperando != null)
                                    numeroOperando.NoIncluirTextosInformacion_CantidadAInsertar = true;

                                if (numeroResultado != null)
                                    numeroResultado.NoIncluirTextosInformacion_CantidadAInsertar = true;
                            }

                            if (evaluarOperandoNumero_OperarPorFilas != null &&
                                evaluarOperandoNumero_OperarPorFilas == true && cumpleCondicionesProcesamiento &&
                                itemCondiciones.ReiniciarAcumulacion_OperacionPorFilas)
                            {
                                ReiniciarAcumulacion_OperarPorFilas = true;
                            }

                            operacion.ResultadosCondiciones_ProcesamientoCantidades.Add(resultadoCondicion);
                            operacion.ActualizarTooltips_Operandos = true;
                        }
                    }
                }
            }
        }
        public void ProcesarCantidades_Textos(EjecucionCalculo ejecucion, ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperando, EntidadNumero numeroOperando, EntidadNumero numeroResultado,
            List<EntidadNumero> ListaNumerosResultado, Clasificador itemClasificador, int filasAdicionalesResultado,
            bool? evaluarOperandoNumero_OperarPorFilas = null)
        {
            foreach (var itemCondiciones in ElementoDiseñoRelacionado.ProcesamientoTextosInformacion)
            {
                //if ((itemCondiciones.TipoElementoDesde == TipoOpcionElementoCondicionProcesamientoCantidades.Operando | 
                //    itemCondiciones.TipoElementoDesde == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados) && 
                //    !itemCondiciones.OperandosInsertar_CantidadesProcesamientoTextos.Contains(itemOperando.ElementoDiseñoRelacionado)) continue;

                if (evaluarOperandoNumero_OperarPorFilas == null || (evaluarOperandoNumero_OperarPorFilas != null &&
                    (evaluarOperandoNumero_OperarPorFilas == true && itemCondiciones.FiltrarPorNumeros) ||
                    (!evaluarOperandoNumero_OperarPorFilas == false && itemCondiciones.FiltrarPorElementos) ||
                    (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado &&
                    !evaluarOperandoNumero_OperarPorFilas == true && itemCondiciones.FiltrarPorNumeros) ||
                    (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos &&
                    evaluarOperandoNumero_OperarPorFilas == false && itemCondiciones.FiltrarPorNumeros)))
                {
                    if ((numeroOperando != null && ((itemCondiciones.AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades &&
                            (!numeroOperando.EsCantidadInsertada_ProcesamientoCantidades ||
                            numeroOperando.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (itemCondiciones.AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades &&
                            numeroOperando.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!itemCondiciones.AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades &&
                            !itemCondiciones.AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades &&
                            !numeroOperando.EsCantidadInsertada_ProcesamientoCantidades))) &&

                        (numeroResultado != null && ((itemCondiciones.AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades &&
                            (!numeroResultado.EsCantidadInsertada_ProcesamientoCantidades ||
                            numeroResultado.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (itemCondiciones.AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades &&
                            numeroResultado.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!itemCondiciones.AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades &&
                            !itemCondiciones.AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades &&
                            !numeroResultado.EsCantidadInsertada_ProcesamientoCantidades))))
                    {
                        bool cumpleCondicionesProcesamiento = false;

                        if (itemCondiciones.CondicionesTextosInformacion != null)
                        {
                            switch (itemCondiciones.TipoElementoDesde)
                            {
                                case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                                    cumpleCondicionesProcesamiento = itemCondiciones.CondicionesTextosInformacion.EvaluarCondiciones(new AsignacionImplicacion_TextosInformacion(), ejecucion,
                                this.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? this : null,
                                this.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)this : null,
                                itemOperando, numeroOperando, ListaNumerosResultado);

                                    break;

                                case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                                    cumpleCondicionesProcesamiento = itemCondiciones.CondicionesTextosInformacion.EvaluarCondiciones(new AsignacionImplicacion_TextosInformacion(), ejecucion,
                               this.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? this : null,
                                this.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)this : null,
                                this, numeroResultado, ListaNumerosResultado);

                                    break;

                                case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                                    cumpleCondicionesProcesamiento = itemCondiciones.CondicionesTextosInformacion.EvaluarCondiciones(new AsignacionImplicacion_TextosInformacion(), ejecucion,
                                this.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? this : null,
                                this.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)this : null,
                                itemOperando, numeroOperando, ListaNumerosResultado);

                                    if(!cumpleCondicionesProcesamiento)
                                    {
                                        cumpleCondicionesProcesamiento = itemCondiciones.CondicionesTextosInformacion.EvaluarCondiciones(new AsignacionImplicacion_TextosInformacion(), ejecucion,
                                this.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? this : null,
                                this.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)this : null,
                                this, numeroOperando, ListaNumerosResultado);
                                    }

                                    break;
                            }
                        }
                        else
                            cumpleCondicionesProcesamiento = true;

                        if (cumpleCondicionesProcesamiento && (((cumpleCondicionesProcesamiento && (((itemOperando == null || itemOperando.ElementoDiseñoRelacionado == null) || (itemOperando != null &&
                            itemOperando.ElementoDiseñoRelacionado != null && ((itemCondiciones.TipoElementoDesde == TipoOpcionElementoCondicionProcesamientoCantidades.Operando |
                        itemCondiciones.TipoElementoDesde == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados) && itemCondiciones.VerificarOperando(itemOperando.ElementoDiseñoRelacionado) ||
                itemCondiciones.VerificaEntrada(itemOperando.ElementoDiseñoRelacionado.EntradaRelacionada)))) || !(itemCondiciones.TipoElementoDesde == TipoOpcionElementoCondicionProcesamientoCantidades.Operando |
                        itemCondiciones.TipoElementoDesde == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados))
                && !itemCondiciones.AplicarProcesamiento_SinCondiciones) ||
                (itemCondiciones.AplicarProcesamiento_SinCondiciones))))
                        {
                            var TextosElementoResultado = ejecucion.SeleccionarCantidadProcesamiento_TextosInformacion_Resultado(ListaNumerosResultado, itemCondiciones.TipoUbicacionAccion_Insertar, itemCondiciones,
                                operacion.PosicionActualNumero_CondicionesOperador_Implicacion, (evaluarOperandoNumero_OperarPorFilas.HasValue && evaluarOperandoNumero_OperarPorFilas.Value ? 1 : 0) + 
                                filasAdicionalesResultado);

                            List<string> TextosElemento = new List<string>();

                            if (itemOperando != null)    
                                TextosElemento = ejecucion.SeleccionarCantidadUbicacionProcesamiento_TextosInformacion(itemOperando, itemCondiciones.UbicacionElementoAccion, itemCondiciones, itemClasificador);
                            else
                                TextosElemento = ejecucion.SeleccionarCantidadUbicacionProcesamiento_TextosInformacion_Resultado(ListaNumerosResultado, itemCondiciones.UbicacionElementoAccion, itemCondiciones,
                                    operacion.PosicionActualNumero_CondicionesOperador_Implicacion, (evaluarOperandoNumero_OperarPorFilas.HasValue && evaluarOperandoNumero_OperarPorFilas.Value ? 1 : 0) + 
                                filasAdicionalesResultado);

                            if(itemCondiciones.TipoElementoDesde != TipoOpcionElementoCondicionProcesamientoCantidades.Operando ||
                                itemCondiciones.TipoElementoDonde != TipoOpcionElementoAccionProcesamientoCantidades.Operando)
                                ProcesarTextosInformacion_Elementos_Condiciones(itemCondiciones, ejecucion, TextosElemento, TextosElementoResultado, itemClasificador);

                            foreach(var itemOperandoDonde in itemCondiciones.OperandosInsertar_CantidadesProcesamientoTextos_Donde)
                            {
                                var itemOperandoDonde_ElementoEjecucion = ejecucion.ObtenerElementoEjecucion(itemOperandoDonde);

                                if (itemOperandoDonde_ElementoEjecucion == itemOperando) continue;
                                TextosElemento = ejecucion.SeleccionarCantidadUbicacionProcesamiento_TextosInformacion((ElementoOperacionAritmeticaEjecucion)itemOperandoDonde_ElementoEjecucion, itemCondiciones.UbicacionElementoAccion, itemCondiciones, itemClasificador);
                                
                                ProcesarTextosInformacion_Elementos_Condiciones(itemCondiciones, ejecucion, TextosElemento, TextosElementoResultado, itemClasificador);
                            }

                            if (evaluarOperandoNumero_OperarPorFilas != null &&
                                evaluarOperandoNumero_OperarPorFilas == true && cumpleCondicionesProcesamiento &&
                            itemCondiciones.ReiniciarAcumulacion_OperacionPorFilas)
                            {
                                ReiniciarAcumulacion_OperarPorFilas = true;
                            }

                            //operacion.ActualizarTooltips_Operandos = true;
                        }

                    }
                }
            }
        }
            
        private void ProcesarTextosInformacion_Elementos_Condiciones(CondicionProcesamientoTextosInformacion itemCondiciones,
            EjecucionCalculo ejecucion, List<string> TextosElemento, List<string> TextosElementoResultado, Clasificador itemClasificador)
        {
            switch (itemCondiciones.Tipo)
            {
                case TipoOpcionCondicionProcesamientoTextosInformacion.InsertarTextosExistentes:                
                case TipoOpcionCondicionProcesamientoTextosInformacion.EditarTextos:

                    bool desdeOperandos = false;
                    bool desdeResultados = false;

                    switch (itemCondiciones.TipoElementoDesde)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            desdeOperandos = true;
                            desdeResultados = true;
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            desdeOperandos = true;

                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            desdeResultados = true;
                            break;
                    }

                    if (desdeOperandos)
                    {
                        foreach (var itemElementoDesde in itemCondiciones.OperandosInsertar_CantidadesProcesamientoTextos_Desde)
                        {
                            var elemento = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(itemElementoDesde);
                            var TextosElementoDesde = ejecucion.SeleccionarCantidadProcesamiento_TextosInformacion(elemento, itemCondiciones.TipoUbicacionAccion_Insertar, itemCondiciones, itemClasificador);

                            bool dondeOperandos = false;
                            bool dondeResultados = false;

                            switch (itemCondiciones.TipoElementoDonde)
                            {
                                case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                                    dondeOperandos = true;

                                    break;

                                case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                                    dondeOperandos = true;
                                    dondeResultados = true;

                                    break;

                                case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                                    dondeResultados = true;
                                    break;
                            }

                            if(dondeOperandos)
                            {
                                ejecucion.ProcesarTextosInformacionElemento(TextosElemento, TextosElementoDesde, itemCondiciones);
                                
                            }

                            if(dondeResultados)
                            {
                                ejecucion.ProcesarTextosInformacionElemento(TextosElementoResultado, TextosElementoDesde, itemCondiciones);

                            }
                        }
                    }

                    if (desdeResultados)
                    {
                        bool dondeOperandos = false;
                        bool dondeResultados = false;

                        switch (itemCondiciones.TipoElementoDonde)
                        {
                            case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                                dondeOperandos = true;
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                                dondeOperandos = true;
                                dondeResultados = true;
                                
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                                dondeResultados = true;

                                break;
                        }

                        if(dondeOperandos)
                        {
                            ejecucion.ProcesarTextosInformacionElemento(TextosElemento, TextosElementoResultado, itemCondiciones);
                            
                        }

                        if(dondeResultados)
                        {
                            ejecucion.ProcesarTextosInformacionElemento(TextosElementoResultado, TextosElementoResultado, itemCondiciones);

                        }
                    }

                    break;

                case TipoOpcionCondicionProcesamientoTextosInformacion.QuitarTextos:
                    desdeOperandos = false;
                    desdeResultados = false;

                    switch (itemCondiciones.TipoElementoDesde)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            desdeOperandos = true;
                            desdeResultados = true;
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            desdeOperandos = true;

                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            desdeResultados = true;
                            break;
                    }

                    if (desdeOperandos)
                    {
                        ejecucion.ProcesarTextosInformacionElemento(TextosElemento, TextosElemento, itemCondiciones);
                    }

                    if (desdeResultados)
                    {
                        ejecucion.ProcesarTextosInformacionElemento(TextosElementoResultado, TextosElementoResultado, itemCondiciones);
                    }
                    break;

                case TipoOpcionCondicionProcesamientoTextosInformacion.MantenerPosicíonActual_Procesamiento:
                    switch (itemCondiciones.TipoElementoDesde)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            MantenerProcesamiento_Operandos = true;
                            MantenerProcesamiento_Resultados = true;
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            MantenerProcesamiento_Operandos = true;
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            MantenerProcesamiento_Resultados = true;
                            break;
                    }
                    break;

                case TipoOpcionCondicionProcesamientoTextosInformacion.DetenerProcesamiento:
                    DetenerProcesamiento = true;
                    break;
            }
        }

        private void EstablecerEstadoProcesamiento(ResultadoCondicionProcesamientoCantidades resultadoCondicion, 
            bool cumpleCondicionesProcesamiento, CondicionProcesamientoCantidades itemCondiciones,
            ElementoOperacionAritmeticaEjecucion operacion, DiseñoOperacion operando = null, Entrada entrada = null)
        {
            if (cumpleCondicionesProcesamiento && ((((itemCondiciones.CondicionesCantidades != null &&
                (itemCondiciones.CondicionesCantidades.VerificarOperando(operando) ||
                itemCondiciones.CondicionesCantidades.VerificarOperandoValores(operando) ||
                itemCondiciones.CondicionesCantidades.VerificaEntrada(entrada))) || itemCondiciones.CondicionesCantidades == null)
                && !itemCondiciones.AplicarProcesamiento_SinCondiciones) ||
                (itemCondiciones.AplicarProcesamiento_SinCondiciones)))
            {
                switch (itemCondiciones.Tipo)
                {
                    case TipoOpcionCondicionProcesamientoCantidades.DetenerProcesamiento:
                        DetenerProcesamiento = true;
                        break;

                    case TipoOpcionCondicionProcesamientoCantidades.MantenerPosicíonActual_Procesamiento:

                        switch (itemCondiciones.TipoElemento)
                        {
                            case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                                MantenerProcesamiento_Operandos = true;
                                MantenerProcesamiento_Resultados = true;
                                break;

                            case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                                MantenerProcesamiento_Operandos = true;
                                break;

                            case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                                MantenerProcesamiento_Resultados = true;
                                break;
                        }

                        break;

                    case TipoOpcionCondicionProcesamientoCantidades.QuitarCantidadActual:
                        switch (itemCondiciones.TipoElemento)
                        {
                            case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                                resultadoCondicion.QuitarCantidad_Procesamiento_Operandos = true;
                                resultadoCondicion.QuitarCantidad_Procesamiento_Resultados = true;
                                break;

                            case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                                resultadoCondicion.QuitarCantidad_Procesamiento_Operandos = true;
                                break;

                            case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                                resultadoCondicion.QuitarCantidad_Procesamiento_Resultados = true;
                                break;
                        }

                        switch (itemCondiciones.TipoElementoAccion)
                        {
                            case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                                resultadoCondicion.QuitarElemento_Procesamiento_Operandos = true;
                                resultadoCondicion.QuitarElemento_Procesamiento_Resultados = true;
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                                resultadoCondicion.QuitarElemento_Procesamiento_Operandos = true;
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                                resultadoCondicion.QuitarElemento_Procesamiento_Resultados = true;
                                break;
                        }
                        break;

                    case TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes:
                        switch (itemCondiciones.TipoElemento)
                        {
                            case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                                resultadoCondicion.InsertarCantidad_Procesamiento_Operandos = true;
                                resultadoCondicion.InsertarCantidad_Procesamiento_Resultados = true;
                                break;

                            case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                                resultadoCondicion.InsertarCantidad_Procesamiento_Operandos = true;
                                break;

                            case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                                resultadoCondicion.InsertarCantidad_Procesamiento_Resultados = true;
                                break;
                        }

                        switch (itemCondiciones.TipoElementoAccion)
                        {
                            case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                                resultadoCondicion.InsertarElemento_Procesamiento_Operandos = true;
                                resultadoCondicion.InsertarElemento_Procesamiento_Resultados = true;
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                                resultadoCondicion.InsertarElemento_Procesamiento_Operandos = true;
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                                resultadoCondicion.InsertarElemento_Procesamiento_Resultados = true;
                                break;
                        }

                        resultadoCondicion.InsertarElemento_Procesamiento_Cantidad = itemCondiciones.TipoElementoAccion_Insertar;
                        resultadoCondicion.InsertarElemento_Procesamiento_Cantidad_ValorPosicion = itemCondiciones.ValorPosicion_TipoElementoAccion_Insertar;
                        resultadoCondicion.InsertarUbicacion_Procesamiento_Cantidad = itemCondiciones.TipoUbicacionAccion_Insertar;
                        resultadoCondicion.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion = itemCondiciones.ValorPosicion_UbicacionAccion_Insertar;
                        resultadoCondicion.InsertarElemento_Procesamiento_Cantidad_ValorFijo = itemCondiciones.ValorFijo_Insercion;
                        resultadoCondicion.InsertarElemento_Procesamiento_ValorFijo = itemCondiciones.InsertarValorFijo;

                        resultadoCondicion.OperacionesAlInsertar_ProcesamientoCantidades.Add(new DuplaOperacion_AlInsertar_ProcesamientoCantidades()
                        {
                            AlInsertar_Operar = itemCondiciones.AlInsertar_Operar,
                            Operacion_AlInsertar = itemCondiciones.Operacion_AlInsertar,
                            ConfigRedondeo = itemCondiciones.ConfigRedondeo_OperacionInterna,
                            TipoElementoAOperar_OperacionAlInsertar = itemCondiciones.TipoElementoAOperar_OperacionAlInsertar,
                            OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar = itemCondiciones.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.ToList(),
                            ValorFijo_OperacionAlInsertar = itemCondiciones.ValorFijo_OperacionAlInsertar,
                            TipoElemento_OperacionAlInsertar = itemCondiciones.TipoElemento_OperacionAlInsertar,
                            ValorPosicion_TipoElemento_OperacionAlInsertar = itemCondiciones.ValorPosicion_TipoElemento_OperacionAlInsertar
                        });

                        if(itemCondiciones.EsInsercionEdicion)
                        {
                            switch (itemCondiciones.TipoElemento)
                            {
                                case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                                    resultadoCondicion.QuitarCantidad_Procesamiento_Operandos = true;
                                    resultadoCondicion.QuitarCantidad_Procesamiento_Resultados = true;
                                    break;

                                case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                                    resultadoCondicion.QuitarCantidad_Procesamiento_Operandos = true;
                                    break;

                                case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                                    resultadoCondicion.QuitarCantidad_Procesamiento_Resultados = true;
                                    break;
                            }

                            switch (itemCondiciones.TipoElementoAccion)
                            {
                                case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                                    resultadoCondicion.QuitarElemento_Procesamiento_Operandos = true;
                                    resultadoCondicion.QuitarElemento_Procesamiento_Resultados = true;
                                    break;

                                case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                                    resultadoCondicion.QuitarElemento_Procesamiento_Operandos = true;
                                    break;

                                case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                                    resultadoCondicion.QuitarElemento_Procesamiento_Resultados = true;
                                    break;
                            }
                        }

                        if(itemCondiciones.TipoUbicacionAccion_Insertar == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior |
                            itemCondiciones.TipoUbicacionAccion_Insertar == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente |
                            itemCondiciones.TipoUbicacionAccion_Insertar == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecifica |
                            itemCondiciones.TipoUbicacionAccion_Insertar == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaAnteriores |
                            itemCondiciones.TipoUbicacionAccion_Insertar == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaSiguientes |
                            itemCondiciones.TipoUbicacionAccion_Insertar == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploAnteriores |
                            itemCondiciones.TipoUbicacionAccion_Insertar == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploSiguientes)
                            resultadoCondicion.NoInsertarCantidad_EnPosicion = itemCondiciones.NoInsertarCantidad_EnPosicion;

                        resultadoCondicion.Desplazamiento_PosicionAnterior = itemCondiciones.DesplazarsePosicionAnterior;
                        resultadoCondicion.Desplazamiento_PosicionPosterior = itemCondiciones.DesplazarsePosicionPosterior;

                        break;
                }

                //operacion.ActualizarTooltips_Operandos = true;
            }
        }

        public void LimpiarEstadoProcesamiento()
        {
            DetenerProcesamiento = false;
            MantenerProcesamiento_Operandos = false;
            MantenerProcesamiento_Resultados = false;
            ResultadosCondiciones_ProcesamientoCantidades.Clear();
        }

        public void LimpiarEstadoProcesamiento_Textos()
        {
            DetenerProcesamiento = false;
            MantenerProcesamiento_Operandos = false;
            MantenerProcesamiento_Resultados = false;
        }
        public ElementoOperacionAritmeticaEjecucion CopiarObjeto()
        {
            ElementoOperacionAritmeticaEjecucion elemento = new ElementoOperacionAritmeticaEjecucion();
            elemento.ConsiderarProcesamiento_Agrupamiento = ConsiderarProcesamiento_Agrupamiento;
            elemento.ConsiderarProcesamiento_CondicionFlujo = ConsiderarProcesamiento_CondicionFlujo;
            elemento.ConsiderarProcesamiento_SeleccionarOrdenar = ConsiderarProcesamiento_SeleccionarOrdenar;
            elemento.ElementoDiseñoCalculoRelacionado = ElementoDiseñoCalculoRelacionado;
            elemento.ElementoDiseñoRelacionado = ElementoDiseñoRelacionado;          
            elemento.Nombre = Nombre;
            elemento.Textos = Textos.ToList();

            return elemento;
        }

        public void NivelarCantidadElementos_OperandosPorFila_ProcesamientoCantidades(EjecucionCalculo ejecucion,
            ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion, ref int IndiceNumeroActualElementoOperacion,
            EntidadNumero numeroResultado, ref int CantidadNumeros, List<EntidadNumero> NumerosResultado,
            Clasificador itemClasificador)
        {
            foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
            {
                var itemResultado_Flags = itemOperacion.ResultadosCondiciones_ProcesamientoCantidades_Filas.FirstOrDefault(i => i.IDResultadoCondicionProcesamientoCantidades_Asociado == itemResultado.ID);

                if (itemResultado_Flags != null)
                {
                    var ListaNumerosOperandos = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar
                                                            .Any(i => i == itemOperacion.Clasificadores_Cantidades[itemOperacion.IndicePosicionClasificadores]) &&
                                    EntidadNumero.FiltrarNumeros(i, operacion, itemOperacion.NumerosFiltrados)).ToList();

                    int indiceBaseOperandos = itemOperacion.Numeros.IndexOf(ListaNumerosOperandos.FirstOrDefault());

                    var ListaNumerosResultados = NumerosResultado.Where(i => i.Clasificadores_SeleccionarOrdenar
                                                            .Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)).ToList();

                    int indiceBaseResultados = NumerosResultado.IndexOf(ListaNumerosResultados.FirstOrDefault());

                    if (itemResultado_Flags.FlagInsertarCantidad_Procesamiento_Operandos &&
                    itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                    {
                        var numeroOperando = ListaNumerosOperandos[IndiceNumeroActualElementoOperacion].CopiarObjeto(true, itemClasificador, operacion, true);

                        ProcesarBloqueInsertarCantidades_PorFilas(IndiceNumeroActualElementoOperacion, ListaNumerosOperandos, ListaNumerosOperandos,
                            ListaNumerosResultados, itemResultado, itemResultado_Flags, itemOperacion, numeroResultado, numeroOperando, ejecucion);

                        foreach (var itemNumeroInsertado in ListaNumerosOperandos)
                        {
                            if (itemNumeroInsertado.EsCantidadInsertada_ProcesamientoCantidadesTemp)
                            {
                                int indiceNumero = ListaNumerosOperandos.IndexOf(itemNumeroInsertado);
                                itemOperacion.Numeros.Insert(indiceBaseOperandos + indiceNumero, itemNumeroInsertado);
                                itemNumeroInsertado.EsCantidadInsertada_ProcesamientoCantidadesTemp = false;
                            }
                        }
                    }

                    if (itemResultado_Flags.FlagInsertarCantidad_Procesamiento_Resultados)
                    {
                        var numeroOperando = ListaNumerosResultados[IndiceNumeroActualElementoOperacion].CopiarObjeto(true, itemClasificador, operacion, true);

                        ProcesarBloqueInsertarCantidades_PorFilas(IndiceNumeroActualElementoOperacion, ListaNumerosResultados, ListaNumerosOperandos,
                            ListaNumerosResultados, itemResultado, itemResultado_Flags, itemOperacion, numeroResultado, numeroOperando, ejecucion);
                        
                        foreach(var itemNumeroInsertado in ListaNumerosResultados)
                        {
                            if(itemNumeroInsertado.EsCantidadInsertada_ProcesamientoCantidadesTemp)
                            {
                                int indiceNumero = ListaNumerosResultados.IndexOf(itemNumeroInsertado);
                                NumerosResultado.Insert(indiceBaseResultados + indiceNumero, itemNumeroInsertado);
                                itemNumeroInsertado.EsCantidadInsertada_ProcesamientoCantidadesTemp = false;
                            }
                        }
                    }
                }
            }
        }

        private void ProcesarBloqueInsertarCantidades_PorFilas(int IndiceNumeroActualElementoOperacion,
            List<EntidadNumero> ListaNumerosFiltrada, List<EntidadNumero> ListaNumerosOperandos,
            List<EntidadNumero> ListaNumerosResultados, ResultadoCondicionProcesamientoCantidades itemResultado,
            ResultadoCondicionProcesamientoCantidades_Filas itemResultado_Flags, ElementoOperacionAritmeticaEjecucion itemOperacion,
            EntidadNumero numeroResultado, EntidadNumero numeroOperando, EjecucionCalculo ejecucion)
        {
            if (IndiceNumeroActualElementoOperacion <= ListaNumerosFiltrada.Count - 1)
            {

                EntidadNumero numero_ = null;

                int indiceInsercion = -1;
                switch (itemResultado.InsertarUbicacion_Procesamiento_Cantidad)
                {
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadActual:
                        indiceInsercion = IndiceNumeroActualElementoOperacion;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior:
                        if (IndiceNumeroActualElementoOperacion > 0)
                            indiceInsercion = IndiceNumeroActualElementoOperacion - 1;
                        else
                        {
                            if (itemResultado_Flags.FlagNoInsertarCantidad_EnPosicion)
                            {
                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_Operandos = false;
                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_Resultados = false;
                            }
                        }
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente:
                        if (IndiceNumeroActualElementoOperacion <= ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = IndiceNumeroActualElementoOperacion + 1;
                        else
                        {
                            if (itemResultado_Flags.FlagNoInsertarCantidad_EnPosicion)
                            {
                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_Operandos = false;
                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_Resultados = false;
                            }
                        }
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecifica:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaAnteriores:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaSiguientes:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploAnteriores:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploSiguientes:
                        int indiceFijo = 0;

                        switch (itemResultado.InsertarUbicacion_Procesamiento_Cantidad)
                        {
                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecifica:
                                indiceFijo = (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaAnteriores:
                                indiceFijo = IndiceNumeroActualElementoOperacion - (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaSiguientes:
                                indiceFijo = IndiceNumeroActualElementoOperacion + (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploAnteriores:
                                indiceFijo = IndiceNumeroActualElementoOperacion - IndiceNumeroActualElementoOperacion * (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploSiguientes:
                                indiceFijo = IndiceNumeroActualElementoOperacion + IndiceNumeroActualElementoOperacion * (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                        }

                        if (indiceFijo >= 0 &&
                            indiceFijo <= ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = indiceFijo;
                        else
                        {
                            if (itemResultado_Flags.FlagNoInsertarCantidad_EnPosicion)
                            {
                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_Operandos = false;
                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_Resultados = false;
                            }
                        }
                        break;
                }

                int indicePosicion = -1;

                if (itemResultado.InsertarCantidad_Procesamiento_Operandos)
                {
                    if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                        numero_ = new EntidadNumero() { Numero = itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo };
                    else
                        numero_ = numeroOperando;

                    if (itemOperacion.IndicePosicionClasificadores > 0)
                    {
                        indicePosicion = ListaNumerosFiltrada.IndexOf(ListaNumerosFiltrada.LastOrDefault(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemOperacion.Clasificadores_Cantidades[itemOperacion.IndicePosicionClasificadores - 1])));
                    }
                }
                else if(itemResultado.InsertarCantidad_Procesamiento_Resultados)
                {
                    numero_ = new EntidadNumero()
                    {
                        Numero = itemResultado_Flags.FlagInsertarElemento_Procesamiento_ValorFijo ?
                        itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo : numeroResultado.Numero,
                        Textos = numeroResultado.NoIncluirTextosInformacion_CantidadAInsertar ? new List<string>() :
                        ejecucion.GenerarTextosInformacion(numeroResultado.Textos)
                    };
                    numero_.EsCantidadInsertada_ProcesamientoCantidades = true;
                    numeroResultado.EsCantidadInsertada_ProcesamientoCantidades = true;

                    numero_.EsCantidadInsertada_ProcesamientoCantidadesTemp = true;
                    numeroResultado.EsCantidadInsertada_ProcesamientoCantidadesTemp = true;

                    numero_.Numero = itemResultado.Operar_ProcesamientoCantidades(numero_.Numero, ListaNumerosFiltrada, this);

                    {
                        var listaClasificadores = ListaNumerosFiltrada.SelectMany(i => i.Clasificadores_SeleccionarOrdenar).Distinct().ToList();

                        if (listaClasificadores.Count > 0)
                        {
                            indicePosicion = ListaNumerosFiltrada.IndexOf(
                                ListaNumerosFiltrada.LastOrDefault(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == listaClasificadores[listaClasificadores.Count - 1])));
                        }
                    }
                }

                if (indicePosicion == -1 && indiceInsercion > -1)
                    indicePosicion = 0;

                if (itemResultado_Flags.FlagInsertarElemento_Procesamiento_Resultados &&
                                numeroResultado != null)
                {
                    ProcesarInsertarCantidades_PorFilas(itemResultado_Flags, itemResultado, ListaNumerosFiltrada, ListaNumerosResultados, numero_, itemOperacion, IndiceNumeroActualElementoOperacion, indiceInsercion, indicePosicion);
                }

                if (itemResultado_Flags.FlagInsertarElemento_Procesamiento_Operandos)
                {
                    ProcesarInsertarCantidades_PorFilas(itemResultado_Flags, itemResultado, ListaNumerosFiltrada, ListaNumerosOperandos, numero_, itemOperacion, IndiceNumeroActualElementoOperacion, indiceInsercion, indicePosicion);
                }
            }
        }

        private void ProcesarInsertarCantidades_PorFilas(ResultadoCondicionProcesamientoCantidades_Filas itemResultado_Flags,
            ResultadoCondicionProcesamientoCantidades itemResultado, List<EntidadNumero> ListaNumerosDesde, List<EntidadNumero> ListaNumerosHacia,
            EntidadNumero numero_PosicionActual, ElementoOperacionAritmeticaEjecucion itemOperacion, 
            int IndiceNumeroActualElementoOperacion, int indiceInsercion, int indicePosicion)
        {
            if (itemResultado_Flags.FlagInsertarElemento_Procesamiento_Resultados |
                            itemResultado_Flags.FlagInsertarElemento_Procesamiento_Operandos)
            {
                EntidadNumero numero_ = null;
                switch (itemResultado.InsertarElemento_Procesamiento_Cantidad)
                {
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual:
                        numero_ = numero_PosicionActual;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadAnterior:
                        if (IndiceNumeroActualElementoOperacion > 0)
                            numero_ = ListaNumerosDesde[IndiceNumeroActualElementoOperacion - 1];
                        else
                            numero_ = numero_PosicionActual;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadSiguiente:
                        if (IndiceNumeroActualElementoOperacion < ListaNumerosDesde.Count - 1)
                            numero_ = ListaNumerosDesde[IndiceNumeroActualElementoOperacion + 1];
                        else
                            numero_ = numero_PosicionActual;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecifica:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaAnteriores:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaSiguientes:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploAnteriores:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploSiguientes:
                        int indiceFijo = 0;

                        switch (itemResultado.InsertarElemento_Procesamiento_Cantidad)
                        {
                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecifica:
                                indiceFijo = (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaAnteriores:
                                indiceFijo = IndiceNumeroActualElementoOperacion - (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaSiguientes:
                                indiceFijo = IndiceNumeroActualElementoOperacion + (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploAnteriores:
                                indiceFijo = IndiceNumeroActualElementoOperacion - IndiceNumeroActualElementoOperacion * (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploSiguientes:
                                indiceFijo = IndiceNumeroActualElementoOperacion + IndiceNumeroActualElementoOperacion * (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                        }

                        if (indiceFijo >= 0 &&
                            indiceFijo <= ListaNumerosDesde.Count - 1)
                            numero_ = ListaNumerosDesde[indiceFijo];
                        else
                            numero_ = numero_PosicionActual;
                        break;
                }

                if (numero_ != null)
                {
                    if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                        numero_ = new EntidadNumero() { Numero = itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo };
                    else
                        numero_ = numero_.CopiarObjeto(true, itemOperacion.Clasificadores_Cantidades[itemOperacion.IndicePosicionClasificadores], this, true);

                    numero_.Numero = itemResultado.Operar_ProcesamientoCantidades(numero_.Numero, ListaNumerosHacia, this);
                    numero_.EsCantidadInsertada_ProcesamientoCantidadesTemp = true;
                    numero_.EsCantidadInsertada_ProcesamientoCantidades = true;

                    if (indiceInsercion > -1)
                    {
                        if(indicePosicion + indiceInsercion > ListaNumerosHacia.Count)
                            ListaNumerosHacia.Add(numero_);
                        else
                            ListaNumerosHacia.Insert(indicePosicion + indiceInsercion, numero_);
                    }
                    else
                        if (indicePosicion > -1)
                            ListaNumerosHacia.Insert(indicePosicion, numero_);
                        else
                            ListaNumerosHacia.Add(numero_);

                    if (numero_.NoIncluirTextosInformacion_CantidadAInsertar)
                    {
                        if (indiceInsercion > -1)
                            ListaNumerosHacia[indicePosicion + indiceInsercion].Textos.Clear();
                        else
                            if (indicePosicion > -1)
                            ListaNumerosHacia[indicePosicion].Textos.Clear();
                        else
                            ListaNumerosHacia.LastOrDefault().Textos.Clear();
                    }

                    itemOperacion.CantidadFilasInsertadas_ProcesamientoCantidades++;
                }
            }
        }

        public void ReiniciarAcumulacion_OperandosPorFila_ProcesamientoCantidades(ref double CantidadAcumulada)
        {
            if (ReiniciarAcumulacion_OperarPorFilas)
            {
                CantidadAcumulada = 0;
            }
        }

        public void LimpiarCantidadesInsertadas()
        {
            //ElementoDiseñoRelacionado.OperandosInsertar_CantidadesProcesamientoCantidades.Clear();
            //ElementoDiseñoRelacionado.SubOperandosInsertar_CantidadesProcesamientoCantidades.Clear();
            ReiniciarAcumulacion_OperarPorFilas = false;

            foreach (var itemOperacion in ElementosOperacion)
            {
                itemOperacion.ResultadosCondiciones_ProcesamientoCantidades_Filas.Clear();
            }
        }

        public List<EntidadNumero> ObtenerNumerosOperando(DiseñoOperacion item, 
            TipoOpcionElementoAccion_InsertarProcesamientoCantidades tipoPosicion,
            double ValorPosiciones)
        {
            List<EntidadNumero> NumerosObtenidos = new List<EntidadNumero>();
            List<EntidadNumero> NumerosSeleccionados = new List<EntidadNumero>();

            var itemOperandoEjecucion = ElementosOperacion.FirstOrDefault(i => i.ElementoDiseñoRelacionado == item);

            if (itemOperandoEjecucion != null)
            {
                NumerosObtenidos.AddRange(itemOperandoEjecucion.ElementosOperacion_Filtrados(this));
                NumerosSeleccionados.Add(SeleccionarCantidadOperando_OperacionAlInsertar_ProcesamientoCantidades(NumerosObtenidos, tipoPosicion,
                    itemOperandoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion, ValorPosiciones));
            }

            return NumerosSeleccionados;
        }

        public EntidadNumero SeleccionarCantidadOperando_OperacionAlInsertar_ProcesamientoCantidades(List<EntidadNumero> ListaNumeros, 
            TipoOpcionElementoAccion_InsertarProcesamientoCantidades tipoPosicion, int indiceLista, double ValorPosiciones)
        {
            EntidadNumero subItem_InsertarOperando = null;
            
            switch (tipoPosicion)
            {
                case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual:
                    if (indiceLista >= 0 &&
                    indiceLista < ListaNumeros.Count - 1)
                        subItem_InsertarOperando = ListaNumeros[indiceLista];
                    else
                        subItem_InsertarOperando = ListaNumeros.LastOrDefault();

                    break;

                case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadAnterior:
                    if (indiceLista > 0)
                        subItem_InsertarOperando = ListaNumeros[indiceLista - 1];
                    else
                        subItem_InsertarOperando = ListaNumeros[indiceLista];
                    break;

                case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadSiguiente:
                    if (indiceLista < ListaNumeros.Count - 1)
                        subItem_InsertarOperando = ListaNumeros[indiceLista + 1];
                    else
                        subItem_InsertarOperando = ListaNumeros[indiceLista];
                    break;

                case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecifica:
                case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaAnteriores:
                case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaSiguientes:
                case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploAnteriores:
                case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploSiguientes:
                    int indiceFijo = 0;

                    switch (tipoPosicion)
                    {
                        case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecifica:
                            indiceFijo = (int)ValorPosiciones;
                            break;

                        case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaAnteriores:
                            indiceFijo = indiceLista - (int)ValorPosiciones;
                            break;

                        case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaSiguientes:
                            indiceFijo = indiceLista + (int)ValorPosiciones;
                            break;

                        case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploAnteriores:
                            indiceFijo = indiceLista - indiceLista * (int)ValorPosiciones;
                            break;

                        case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploSiguientes:
                            indiceFijo = indiceLista + indiceLista * (int)ValorPosiciones;
                            break;

                    }

                    if (indiceFijo >= 0 &&
                        indiceFijo <= ListaNumeros.Count - 1)
                        subItem_InsertarOperando = ListaNumeros[indiceFijo];
                    else
                        subItem_InsertarOperando = ListaNumeros.LastOrDefault();
                    break;
            }

            return subItem_InsertarOperando;
        }

        public List<EntidadNumero> ElementosOperacion_Filtrados(ElementoOperacionAritmeticaEjecucion operacion)
        {
            return Numeros.Where(i => (((!i.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                    (i.ElementosSalidaOperacion_Agrupamiento.Contains(operacion))) &&
                                    ((!i.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
            (i.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion))) &&
            ((!i.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
            (i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))))).ToList();
        }
                
        public void LimpiarCantidades_Comportamiento()
        {
            if (Numeros.Any())
            {
                for(int indice = 0; indice < Numeros.Count; indice++)
                {
                    
                        if (!(Numeros[indice].EvaluarCondicionesLimpieza(ElementoDiseñoRelacionado.ConfigLimpiezaDatosResultados,
                                                            Numeros)))
                        {
                            Numeros.RemoveAt(indice);
                            indice--;
                        }
                    
                }
            }
        }

        public void RedondearCantidades_Comportamiento()
        {
            if (Numeros.Any())
            {
                for (int indice = 0; indice < Numeros.Count; indice++)
                {
                    Numeros[indice].Redondear(ElementoDiseñoRelacionado.ConfigRedondeoResultados);
                }
            }
        }

        public void AgregarClasificadoresGenericos()
        {

            Clasificador clasificadorGenerico = Clasificadores_Cantidades.FirstOrDefault(i => string.IsNullOrEmpty(i.CadenaTexto));

            if (clasificadorGenerico == null)
            {
                clasificadorGenerico = new Clasificador();
                clasificadorGenerico.CadenaTexto = string.Empty;

                if (Clasificadores_Cantidades.Any())
                    Clasificadores_Cantidades.Insert(0, clasificadorGenerico);
                else
                    Clasificadores_Cantidades.Add(clasificadorGenerico);
            }

            foreach (var itemElemento in Numeros)
            {
                var clasificadoresAQuitar_Item = itemElemento.Clasificadores_SeleccionarOrdenar.Where(i => string.IsNullOrEmpty(i.CadenaTexto)).ToList();

                while (clasificadoresAQuitar_Item.Any())
                {
                    itemElemento.Clasificadores_SeleccionarOrdenar.Remove(clasificadoresAQuitar_Item.FirstOrDefault());
                    clasificadoresAQuitar_Item.Remove(clasificadoresAQuitar_Item.FirstOrDefault());
                }

                if (!itemElemento.Clasificadores_SeleccionarOrdenar.Any() ||
                    string.IsNullOrEmpty(clasificadorGenerico.CadenaTexto))
                {
                    itemElemento.Clasificadores_SeleccionarOrdenar.Add(clasificadorGenerico);
                }
            }
        }
        
        public bool EsPrimerNumero(ElementoOperacionAritmeticaEjecucion itemOperacion, EntidadNumero numero, Clasificador itemClasificador,
            bool EsAgrupado_ConAgrupacionRecorrida)
        {
            bool esPrimerNumero = false;

            if (ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos && 
                itemOperacion == ElementosOperacion.FirstOrDefault(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion) & 
                numero == itemOperacion.Numeros.FirstOrDefault(item => item.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador) && 
                ((!item.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                (item.ElementosSalidaOperacion_Agrupamiento.Contains(this)))))
            {
                esPrimerNumero = true;
            }
            else if(ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado &&
                numero == itemOperacion.Numeros.FirstOrDefault(item => item.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador) && 
                ((!item.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                (item.ElementosSalidaOperacion_Agrupamiento.Contains(this)))) &&
                !EsAgrupado_ConAgrupacionRecorrida)
            {
                esPrimerNumero = true;
            }
            else if (ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas && 
                itemOperacion == ElementosOperacion.FirstOrDefault(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
            {
                esPrimerNumero = true;
            }
            else if (ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
            {
                esPrimerNumero = true;
            }

            return esPrimerNumero;
        }

        public EntidadNumero ObtenerValorElementoNeutro(EntidadNumero numeroOriginal)
        {
            EntidadNumero numeroElementoNeutro = new EntidadNumero();

            if (numeroOriginal != null)
            {
                switch (TipoOperacion)
                {
                    case TipoOperacionAritmeticaEjecucion.Suma:
                    case TipoOperacionAritmeticaEjecucion.Resta:
                        numeroElementoNeutro = new EntidadNumero()
                        {
                            Numero = 0
                        };

                        break;

                    case TipoOperacionAritmeticaEjecucion.Multiplicacion:
                    case TipoOperacionAritmeticaEjecucion.Division:
                    case TipoOperacionAritmeticaEjecucion.Potencia:
                    case TipoOperacionAritmeticaEjecucion.Raiz:
                        numeroElementoNeutro = new EntidadNumero()
                        {
                            Numero = 1
                        };

                        break;

                    case TipoOperacionAritmeticaEjecucion.Porcentaje:
                        if (ElementoDiseñoRelacionado.PorcentajeRelativo)
                        {
                            numeroElementoNeutro = new EntidadNumero()
                            {
                                Numero = 1
                            };
                        }
                        else
                        {
                            numeroElementoNeutro = new EntidadNumero()
                            {
                                Numero = 100
                            };
                        }

                        break;

                    case TipoOperacionAritmeticaEjecucion.Logaritmo:
                        numeroElementoNeutro = new EntidadNumero()
                        {
                            Numero = numeroOriginal.Numero
                        };
                        break;
                }
            }
            return numeroElementoNeutro;
        }

        public bool ObtenerCantidadElementosAgrupados_CambioAgrupacion(ElementoOperacionAritmeticaEjecucion itemOperacionActual)
        {
            var elementosSiguientes = ElementosOperacion.Where(i => i != ElementosOperacion.FirstOrDefault() && i != itemOperacionActual &&
            ElementosOperacion.IndexOf(i) > ElementosOperacion.IndexOf(itemOperacionActual))
                .Where(i => i.NumeroResultado_PorFilas != null)
                .ToList();

            bool cambioAgrupacion = false;

            if (elementosSiguientes.Any())
            {
                var elementoSiguiente = elementosSiguientes.FirstOrDefault();
                if(elementoSiguiente != null)
                {
                    if (itemOperacionActual.NumeroResultado_PorFilas.Agrupacion_PorSeparado != elementoSiguiente.NumeroResultado_PorFilas.Agrupacion_PorSeparado)
                        cambioAgrupacion = true;
                }
            }

            return cambioAgrupacion;
        }

        public void SetearNumeroResultadoSiguiente_Agrupaciones(bool conAgrupaciones, List<EntidadNumero> NumerosResultado, int indiceCantidad)
        {
            if (conAgrupaciones)
            {
                switch (TipoOperacion)
                {
                    case TipoOperacionAritmeticaEjecucion.Suma:
                    case TipoOperacionAritmeticaEjecucion.Resta:
                    case TipoOperacionAritmeticaEjecucion.Multiplicacion:
                    case TipoOperacionAritmeticaEjecucion.Division:

                        if (indiceCantidad + 1 <= NumerosResultado.Count - 1)
                        {
                            NumerosResultado[indiceCantidad + 1].Numero = NumerosResultado[indiceCantidad].Numero;
                        }

                        break;
                }
                
            }
        }

        public bool ObtenerCantidadElementosAgrupados_AgrupacionRecorrida(ElementoOperacionAritmeticaEjecucion itemOperacionActual)
        {
            var elementosAnteriores = ElementosOperacion.Count(i => i != itemOperacionActual &&
            ElementosOperacion.IndexOf(i) < ElementosOperacion.IndexOf(itemOperacionActual) &&
            i.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionRelacionada == ElementoDiseñoRelacionado).FirstOrDefault() != null &&
            itemOperacionActual.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionRelacionada == ElementoDiseñoRelacionado).FirstOrDefault() != null &&
            i.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionRelacionada == ElementoDiseñoRelacionado).FirstOrDefault().NombreAgrupacion == 
            itemOperacionActual.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionRelacionada == ElementoDiseñoRelacionado).FirstOrDefault().NombreAgrupacion);

            return elementosAnteriores > 0;
        }
    }
}
