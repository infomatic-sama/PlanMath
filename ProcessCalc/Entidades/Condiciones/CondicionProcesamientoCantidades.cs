using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Condiciones
{
    public class CondicionProcesamientoCantidades
    {
        public TipoOpcionCondicionProcesamientoCantidades Tipo { get; set; }
        public TipoOpcionElementoCondicionProcesamientoCantidades TipoElemento { get; set; }
        public CondicionImplicacionTextosInformacion CondicionesCantidades { get; set; }
        public bool FiltrarPorElementos { get; set; }
        public bool FiltrarPorNumeros { get; set; }
        public bool AplicarProcesamiento_SinCondiciones { get; set; }
        public TipoOpcionElementoAccionProcesamientoCantidades TipoElementoAccion { get; set; }
        public TipoOpcionElementoAccionProcesamientoCantidades TipoElementoAOperar_OperacionAlInsertar { get; set; }
        public TipoOpcionElementoAccion_InsertarProcesamientoCantidades TipoElementoAccion_Insertar { get; set; }
        public TipoOpcionElementoAccion_InsertarProcesamientoCantidades TipoElemento_OperacionAlInsertar { get; set; }
        public TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades TipoUbicacionAccion_Insertar { get; set; }
        public string ValorFijo_OperacionAlInsertar { get; set; }
        public bool NoInsertarCantidad_EnPosicion { get; set; }
        public bool AplicarProcesamiento_CantidadesInsertadas_Operandos { get; set; }
        public bool AplicarProcesamiento_SoloCantidadesInsertadas_Operandos { get; set; }
        public bool ReiniciarAcumulacion_OperacionPorFilas {  get; set; }
        public TipoOperacion_AlInsertar_ProcesamientoCantidades Operacion_AlInsertar { get; set; }
        public bool AlInsertar_Operar { get; set; }
        public double ValorFijo_Insercion { get; set; }
        public bool NoIncluirTextosInformacion_CantidadAInsertar { get; set; }
        public bool EsInsercionEdicion { get; set; }
        public bool DesplazarsePosicionAnterior { get; set; }
        public bool DesplazarsePosicionPosterior { get; set; }
        public double ValorPosicion_TipoElementoAccion_Insertar { get; set; }
        public double ValorPosicion_UbicacionAccion_Insertar { get; set; }
        public double ValorPosicion_TipoElemento_OperacionAlInsertar { get; set; }
        public bool InsertarValorFijo { get; set; }
        public CondicionProcesamientoCantidades()
        {
            FiltrarPorNumeros = true;
            OperandosInsertar_CantidadesProcesamientoCantidades = new List<DiseñoOperacion>();
            SubOperandosInsertar_CantidadesProcesamientoCantidades = new List<DiseñoElementoOperacion>();
            TipoElementoAccion_Insertar = TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual;
            Operacion_AlInsertar = TipoOperacion_AlInsertar_ProcesamientoCantidades.InversoSumaResta;
            TipoUbicacionAccion_Insertar = TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadActual;
            OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar = new List<DiseñoOperacion>();
            SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar = new List<DiseñoElementoOperacion>();
            TipoElemento_OperacionAlInsertar = TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual;
        }
        [IgnoreDataMember]
        public ElementoEjecucionCalculo UltimoItemOperando { get; set; }
        [IgnoreDataMember]
        public EntidadNumero UltimoItemOperando_Numero { get; set; }
        [IgnoreDataMember]
        public ElementoDiseñoOperacionAritmeticaEjecucion UltimoItemOperando_DiseñoOperacion { get; set; }
        public List<DiseñoOperacion> OperandosInsertar_CantidadesProcesamientoCantidades { get; set; }
        public List<DiseñoOperacion> OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosInsertar_CantidadesProcesamientoCantidades { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar { get; set; }
        public ConfiguracionRedondearCantidades ConfigRedondeo_OperacionInterna { get; set; }
        public CondicionProcesamientoCantidades ReplicarObjeto()
        {
            CondicionProcesamientoCantidades condicion = new CondicionProcesamientoCantidades();
            condicion.Tipo = Tipo;
            condicion.TipoElemento = TipoElemento;
            condicion.CondicionesCantidades = CondicionesCantidades?.ReplicarObjeto();
            condicion.FiltrarPorElementos = FiltrarPorElementos;
            condicion.FiltrarPorNumeros = FiltrarPorNumeros;
            condicion.AplicarProcesamiento_SinCondiciones = AplicarProcesamiento_SinCondiciones;
            condicion.TipoElementoAccion = TipoElementoAccion;
            condicion.TipoElementoAOperar_OperacionAlInsertar = TipoElementoAOperar_OperacionAlInsertar;
            condicion.TipoElemento_OperacionAlInsertar = TipoElemento_OperacionAlInsertar;
            condicion.TipoUbicacionAccion_Insertar = TipoUbicacionAccion_Insertar;
            condicion.TipoElementoAccion_Insertar = TipoElementoAccion_Insertar;
            condicion.AplicarProcesamiento_CantidadesInsertadas_Operandos = AplicarProcesamiento_CantidadesInsertadas_Operandos;
            condicion.AplicarProcesamiento_SoloCantidadesInsertadas_Operandos = AplicarProcesamiento_SoloCantidadesInsertadas_Operandos;
            condicion.ReiniciarAcumulacion_OperacionPorFilas = ReiniciarAcumulacion_OperacionPorFilas;
            condicion.OperandosInsertar_CantidadesProcesamientoCantidades = OperandosInsertar_CantidadesProcesamientoCantidades.ToList();
            condicion.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar = OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.ToList();
            condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades = SubOperandosInsertar_CantidadesProcesamientoCantidades.ToList();
            condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar = SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.ToList();
            condicion.Operacion_AlInsertar = Operacion_AlInsertar;
            condicion.AlInsertar_Operar = AlInsertar_Operar;
            condicion.EsInsercionEdicion = EsInsercionEdicion;
            condicion.ValorFijo_Insercion = ValorFijo_Insercion;
            condicion.ValorFijo_OperacionAlInsertar = ValorFijo_OperacionAlInsertar;
            condicion.InsertarValorFijo = InsertarValorFijo;
            condicion.ConfigRedondeo_OperacionInterna = ConfigRedondeo_OperacionInterna?.CopiarObjeto();
            condicion.NoIncluirTextosInformacion_CantidadAInsertar = NoIncluirTextosInformacion_CantidadAInsertar;
            condicion.NoInsertarCantidad_EnPosicion = NoInsertarCantidad_EnPosicion;
            condicion.DesplazarsePosicionAnterior = DesplazarsePosicionAnterior;
            condicion.DesplazarsePosicionPosterior = DesplazarsePosicionPosterior;
            condicion.ValorPosicion_TipoElementoAccion_Insertar = ValorPosicion_TipoElementoAccion_Insertar;
            condicion.ValorPosicion_TipoElemento_OperacionAlInsertar = ValorPosicion_TipoElemento_OperacionAlInsertar;
            condicion.ValorPosicion_UbicacionAccion_Insertar = ValorPosicion_UbicacionAccion_Insertar;

            return condicion;
        }

        public string MostrarEtiquetaCondiciones()
        {
            string texto = string.Empty;

            switch(Tipo)
            {
                case TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes:
                    string strCantidades = string.Empty;

                    switch (TipoElemento)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            strCantidades = "variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            strCantidades = "variables, vectores de entrada o retornados";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            strCantidades = "variables, vectores de entrada, retornados o retornados de esta operación";
                            break;
                    }

                    string strCantidadesAccion = string.Empty;

                    switch (TipoElementoAccion)
                    {
                        case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                            strCantidadesAccion = "variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                            strCantidadesAccion = "variables, vectores de entrada o retornados";
                            break;

                        case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                            strCantidadesAccion = "variables, vectores de entrada, retornados o retornados de esta operación";
                            break;
                    }

                    texto += "Insertar nuevas " + strCantidadesAccion;

                    if (CondicionesCantidades != null)
                    {
                        texto += ", que los " + strCantidades;
                        texto += " cumplan las siguientes condiciones (si/entonces): ";
                        EtiquetaCondicionImplicacionTextosInformacion etiqueta = new EtiquetaCondicionImplicacionTextosInformacion();
                        etiqueta.Condicion = CondicionesCantidades;
                        etiqueta.MostrarEtiquetaCondiciones();
                        texto += etiqueta.Texto;
                    }
                                        
                    //if (FiltrarPorElementos &
                    //    FiltrarPorNumeros)
                    //    texto += " para los elementos y sus números.";
                    //else if (FiltrarPorNumeros &
                    //    !FiltrarPorNumeros)
                    //    texto += " para los elementos.";
                    //else if (!FiltrarPorNumeros &
                    //    FiltrarPorNumeros)
                    //    texto += " para los números de los elementos.";
                    
                    break;

                case TipoOpcionCondicionProcesamientoCantidades.QuitarCantidadActual:
                    strCantidades = string.Empty;

                    switch (TipoElemento)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            strCantidades = "variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            strCantidades = "variables, vectores de entrada o retornados";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            strCantidades = "variables, vectores de entrada, retornados o retornados de esta operación";
                            break;
                    }

                    strCantidadesAccion = string.Empty;

                    switch (TipoElementoAccion)
                    {
                        case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                            strCantidadesAccion = "variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                            strCantidadesAccion = "variables, vectores de entrada o retornados";
                            break;

                        case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                            strCantidadesAccion = "variables, vectores de entrada, retornados o retornados de esta operación";
                            break;
                    }

                    texto += "Quitar " + strCantidadesAccion;

                    if (CondicionesCantidades != null)
                    {
                        texto += ", que los " + strCantidades;
                        texto += " que cumplan las siguientes condiciones (si/entonces): ";
                        EtiquetaCondicionImplicacionTextosInformacion etiqueta = new EtiquetaCondicionImplicacionTextosInformacion();
                        etiqueta.Condicion = CondicionesCantidades;
                        etiqueta.MostrarEtiquetaCondiciones();
                        texto += etiqueta.Texto;
                    }

                    //if (FiltrarPorElementos &
                    //    FiltrarPorNumeros)
                    //    texto += " para los elementos y sus números.";
                    //else if (FiltrarPorNumeros &
                    //    !FiltrarPorNumeros)
                    //    texto += " para los elementos.";
                    //else if (!FiltrarPorNumeros &
                    //    FiltrarPorNumeros)
                    //    texto += " para los números de los elementos.";                    

                    break;

                case TipoOpcionCondicionProcesamientoCantidades.MantenerPosicíonActual_Procesamiento:
                    strCantidades = string.Empty;

                    switch (TipoElemento)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            strCantidades = "variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            strCantidades = "variables, vectores de entrada o retornados";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            strCantidades = "variables, vectores de entrada, retornados o retornados de esta operación";
                            break;
                    }

                    texto += "Mantener el procesamiento en las cantidades actuales, que son " + strCantidades;

                    if (CondicionesCantidades != null)
                    {
                        texto += " que cumplan las siguientes condiciones (si/entonces): ";
                        EtiquetaCondicionImplicacionTextosInformacion etiqueta = new EtiquetaCondicionImplicacionTextosInformacion();
                        etiqueta.Condicion = CondicionesCantidades;
                        etiqueta.MostrarEtiquetaCondiciones();
                        texto += etiqueta.Texto;
                    }

                    //if (FiltrarPorElementos &
                    //    FiltrarPorNumeros)
                    //    texto += " para los elementos y sus números.";
                    //else if (FiltrarPorNumeros &
                    //    !FiltrarPorNumeros)
                    //    texto += " para los elementos.";
                    //else if (!FiltrarPorNumeros &
                    //    FiltrarPorNumeros)
                    //    texto += " para los números de los elementos.";
                    
                    break;

                case TipoOpcionCondicionProcesamientoCantidades.DetenerProcesamiento:
                    strCantidades = string.Empty;

                    switch (TipoElemento)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            strCantidades = "variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            strCantidades = "variables, vectores de entrada o retornados";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            strCantidades = "variables, vectores de entrada, retornados o retornados de esta operación";
                            break;
                    }

                    texto += "Detener el procesamiento en las cantidades actuales, que son " + strCantidades;

                    if (CondicionesCantidades != null)
                    {
                        texto += " que cumplan las siguientes condiciones (si/entonces): ";
                        EtiquetaCondicionImplicacionTextosInformacion etiqueta = new EtiquetaCondicionImplicacionTextosInformacion();
                        etiqueta.Condicion = CondicionesCantidades;
                        etiqueta.MostrarEtiquetaCondiciones();
                        texto += etiqueta.Texto;
                    }

                    //if (FiltrarPorElementos &
                    //    FiltrarPorNumeros)
                    //    texto += " para los elementos y sus números.";
                    //else if (FiltrarPorNumeros &
                    //    !FiltrarPorNumeros)
                    //    texto += " para los elementos.";
                    //else if (!FiltrarPorNumeros &
                    //    FiltrarPorNumeros)
                    //    texto += " para los números de los elementos.";

                    break;
            }

            return texto;
        }

        public void QuitarElemento(DiseñoOperacion elemento)
        {
            CondicionesCantidades?.QuitarReferenciasCondicionesElemento(elemento);

            if (elemento.EntradaRelacionada != null)
                CondicionesCantidades?.QuitarReferenciasCondicionesEntrada(elemento.EntradaRelacionada);
        }

        public void QuitarSubElemento(DiseñoElementoOperacion elemento)
        {
            CondicionesCantidades.QuitarReferenciasCondicionesElemento_Interno(elemento);

            if (elemento.EntradaRelacionada != null)
                CondicionesCantidades.QuitarReferenciasCondicionesEntrada(elemento.EntradaRelacionada);
        }
    }
}
