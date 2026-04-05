using ProcessCalc.Controles;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.OrigenesDatos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using static ProcessCalc.Entidades.Condiciones.CondicionFlujo;
using static System.Net.WebRequestMethods;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class CondicionImplicacionTextosInformacion
    {
        public TipoOpcionElemento_Condicion_ImplicacionTextosInformacion TipoElementoCondicion { get; set; }
        public TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion TipoTextosInformacion_Valores { get; set; }
        public TipoOpcionElementoComparar_TextosInformacion TipoElementoComparar_TextosInformacion { get; set; }
        public DiseñoOperacion ElementoCondicion { get; set; }
        public DiseñoOperacion OperandoCondicion { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento_Condicion_TextosInformacion { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento_Condicion { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Condicion { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Condicion_TextosInformacion { get; set; }
        public TipoOpcionImplicacion_AsignacionTextoInformacion TipoOpcionCondicion_TextosInformacion { get; set; }
        public TipoOpcion_CondicionTextosInformacion_Implicacion TipoOpcionCondicion_ElementoOperacionEntrada { get; set; }
        public TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion TipoSubElemento_Condicion { get; set; }
        public TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion TipoSubElemento_Condicion_Valores { get; set; }
        public Entrada ElementoEntrada_Valores { get; set; }
        public DiseñoOperacion ElementoOperacion_Valores { get; set; }
        public Entrada EntradaCondicion { get; set; }
        public DiseñoTextosInformacion ElementoDefinicion_Valores { get; set; }
        public DiseñoListaCadenasTexto ElementoDefinicionListas_Valores { get; set; }
        public DiseñoElementoOperacion SubElementoOperacion_Valores { get; set; }
        public string Valores_Condicion { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Condicion_Valores { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion { get; set; }
        public CondicionImplicacionTextosInformacion CondicionContenedora { get; set; }
        public TipoConectorCondiciones_ConjuntoBusquedas TipoConector { get; set; }
        public List<CondicionImplicacionTextosInformacion> Condiciones { get; set; }
        public int CantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadNumerosCumplenCondicion { get; set; }
        public bool OpcionSaldoCantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaNumerosCumplenCondicion { get; set; }
        public DiseñoOperacion ElementoOperacion_Valores_ElementoAsociado { get; set; }
        public TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion TipoElemento_Valores { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento_Condicion_Elemento { get; set; }
        public int CantidadSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public int CantidadSubNumerosCumplenCondicion_TextosInformacion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion { get; set; }
        public int CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion { get; set; }
        public int CantidadSubNumerosCumplenCondicion_Valores { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_Valores { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_Valores { get; set; }
        public int CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public int CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion { get; set; }
        public bool SeguirAplicandoCondicion_AlFinalCantidadesOperando { get; set; }
        public bool SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores { get; set; }
        public bool ReiniciarPosicion_AlFinalCantidadesOperando { get; set; }
        public bool ReiniciarPosicion_AlFinalCantidadesOperando_Valores { get; set; }
        public bool NegarCondicion { get; set; }
        public bool CumpleCondicion_ElementoSinNumeros { get; set; }
        public bool CumpleCondicion_ElementoValores_SinNumeros { get; set; }
        public bool ConectorO_Excluyente { get; set; }
        public bool ConsiderarOperandoCondicion_SiCumple { get; set; }
        public bool ConsiderarIncluirCondicionesHijas { get; set; }
        public bool CantidadTextosInformacion_PorElemento { get; set; }
        public bool CantidadTextosInformacion_PorElemento_Valores { get; set; }
        public bool CantidadTextosInformacion_SoloCadenasCumplen { get; set; }
        public bool CantidadTextosInformacion_SoloCadenasCumplen_Valores { get; set; }
        public bool CantidadNumeros_PorElemento { get; set; }
        public bool CantidadNumeros_PorElemento_Valores { get; set; }
        public bool CadenasTextoSon_Clasificadores { get; set; }
        public bool CadenasTextoSon_Clasificadores_Valores { get; set; }
        public BusquedaTextoArchivo Busqueda_TextoBusqueda { get; set; }
        public TipoOpcionPosicion OpcionValorPosicion { get; set; }
        public bool QuitarEspaciosTemporalmente_CadenaCondicion { get; set; }
        public bool BuscarCualquierTextoInformacion_TextoBusqueda { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesCumplenCondicion { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesNoCumplenCondicion { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> SubOperandosVinculados_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosVinculados_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_CondicionAnterior_Total { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> SubOperandosVinculados_CondicionAnterior_Total { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosVinculados_CondicionAnterior_Total { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_CondicionAnterior_Temp { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_CondicionAnterior_Temp { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> SubOperandosVinculados_CondicionAnterior_Temp { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosVinculados_CondicionAnterior_Temp { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_CondicionAnterior_Total { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> SubOperandosVinculados_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosVinculados_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> SubOperandosVinculados_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosVinculados_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<InfoOpcion_VinculadosAnterior> OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior { get; set; }
        public bool IncluirNombreElementoConTextos { get; set; }
        public bool IncluirSoloNombreElemento { get; set; }
        public bool ContenedorCondiciones { get; set; }
        public bool ConsiderarSoloValores_ProcesamientoCantidades { get; set; }
        public bool ConsiderarValores_ProcesamientoCantidades { get; set; }
        public bool ConsiderarSoloValores_ProcesamientoCantidades_Valores { get; set; }
        public bool ConsiderarValores_ProcesamientoCantidades_Valores { get; set; }
        public bool ConsiderarTextosInformacionComoCantidades { get; set; }
        public bool EsOperandoActual { get; set; }
        public bool EsOperandoTextosActual { get; set; }
        public bool EsOperandoValoresTextosActual { get; set; }
        public bool EsOperandoValoresActual { get; set; }
        //[IgnoreDataMember]
        //public DiseñoOperacion OperandoCondicion_Posterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoElementoOperacion OperandoSubElemento_Condicion_Posterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoOperacion OperandoCondicion_Valores_Posterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoElementoOperacion OperandoSubElemento_Condicion_Valores_Posterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoOperacion OperandoCondicion_Anterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoElementoOperacion OperandoSubElemento_Condicion_Anterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoOperacion OperandoCondicion_Valores_Anterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoElementoOperacion OperandoSubElemento_Condicion_Valores_Anterior { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInvolucrados { get; set; }
        [IgnoreDataMember]
        public bool PrimeraCondicion { get; set; }
        [IgnoreDataMember]
        public ElementoInternetOrigenDatosEjecucion Busqueda_TextoBusqueda_Ejecucion {  get; set; }
        [IgnoreDataMember]
        public int PosicionActualImplicacion { get; set; }
        [IgnoreDataMember]
        public int PosicionActualInstanciaImplicacion { get; set; }
        [IgnoreDataMember]
        public int PosicionActualIteracionImplicacion { get; set; }
        [IgnoreDataMember]
        public bool Valor_Condicion { get; set; }
        public CondicionImplicacionTextosInformacion()
        {
            Valores_Condicion = string.Empty;
            TipoConector = TipoConectorCondiciones_ConjuntoBusquedas.InicioCondiciones;
            Condiciones = new List<CondicionImplicacionTextosInformacion>();
            CantidadNumerosCumplenCondicion = 2;
            OpcionCantidadNumerosCumplenCondicion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaNumerosCumplenCondicion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OperandosVinculados_CondicionAnterior = new List<ElementoEjecucionCalculo>();
            OperandosVinculados_AgregarCondicionAnterior = new List<ElementoEjecucionCalculo>();
            OperandosVinculados_CondicionContenedora = new List<ElementoEjecucionCalculo>();
            SubOperandosVinculados_CondicionAnterior = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            SubOperandosVinculados_AgregarCondicionAnterior = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            SubOperandosVinculados_CondicionContenedora = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            ElementosVinculados_CondicionAnterior = new List<ElementoEjecucionCalculo>();
            ElementosVinculados_AgregarCondicionAnterior = new List<ElementoEjecucionCalculo>();
            ElementosVinculados_CondicionContenedora = new List<ElementoEjecucionCalculo>();
            NumerosVinculados_CondicionAnterior = new List<EntidadNumero>();
            NumerosVinculados_AgregarCondicionAnterior = new List<EntidadNumero>();
            NumerosVinculados_CondicionContenedora = new List<EntidadNumero>();
            TipoElemento_Valores = TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos;
            OpcionSeleccionNumerosElemento_Condicion = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
            OpcionSeleccionNumerosElemento_Condicion_TextosInformacion = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
            OpcionSeleccionNumerosElemento_Condicion_Valores = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
            OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
            OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
            CantidadSubNumerosCumplenCondicion_OperacionEntrada = 2;
            OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_TextosInformacion = 2;
            OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = 2;
            OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_Valores = 2;
            OpcionCantidadSubNumerosCumplenCondicion_Valores = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_Valores = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = 2;
            CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = 2;
            OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            IncluirNombreElementoConTextos = true;
            TipoElementoComparar_TextosInformacion = TipoOpcionElementoComparar_TextosInformacion.TextosInformacion;
            ConsiderarOperandoCondicion_SiCumple = true;
            ConsiderarIncluirCondicionesHijas = true;
            TextosInvolucrados = new List<string>();
            OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior = new List<InfoOpcion_VinculadosAnterior>();
            OpcionValorPosicion = TipoOpcionPosicion.Ninguna;
            SeguirAplicandoCondicion_AlFinalCantidadesOperando = true;
            SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores = true;
            TipoSubElemento_Condicion_Valores = TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.Ninguno;
            ConsiderarValores_ProcesamientoCantidades = true;
            ConsiderarValores_ProcesamientoCantidades_Valores = true;
            ElementosVinculados_CondicionAnterior_Total = new List<ElementoEjecucionCalculo>();
            NumerosVinculados_CondicionAnterior_Total = new List<EntidadNumero>();
            OperandosVinculados_CondicionAnterior_Total = new List<ElementoEjecucionCalculo>();
            SubOperandosVinculados_CondicionAnterior_Total = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            ElementosVinculados_CondicionAnterior_Temp = new List<ElementoEjecucionCalculo>();
            NumerosVinculados_CondicionAnterior_Temp = new List<EntidadNumero>();
            OperandosVinculados_CondicionAnterior_Temp = new List<ElementoEjecucionCalculo>();
            SubOperandosVinculados_CondicionAnterior_Temp = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
        }

        public CondicionImplicacionTextosInformacion ReplicarObjeto()
        {
            CondicionImplicacionTextosInformacion condicion = new CondicionImplicacionTextosInformacion();

            condicion.CopiarObjeto(this, this.CondicionContenedora);

            //CondicionImplicacionTextosInformacion condicion = null;

            //MemoryStream flujo = new MemoryStream();

            //DataContractSerializer objeto = new DataContractSerializer(typeof(CondicionImplicacionTextosInformacion), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            //objeto.WriteObject(flujo, this);

            //flujo.Position = 0;
            //condicion = (CondicionImplicacionTextosInformacion)objeto.ReadObject(flujo);

            return condicion;
        }

        public void CopiarObjeto(CondicionImplicacionTextosInformacion condicionACopiar, 
            CondicionImplicacionTextosInformacion condicionContenedoraACopiar)
        {
            if (condicionContenedoraACopiar != null)
                this.CondicionContenedora = condicionContenedoraACopiar;

            this.Condiciones = new List<CondicionImplicacionTextosInformacion>();
            this.ElementoCondicion = condicionACopiar.ElementoCondicion;
            this.EntradaCondicion = condicionACopiar.EntradaCondicion;
            this.ElementoEntrada_Valores = condicionACopiar.ElementoEntrada_Valores;
            this.OperandoCondicion = condicionACopiar.OperandoCondicion;
            this.OperandoSubElemento_Condicion_TextosInformacion = condicionACopiar.OperandoSubElemento_Condicion_TextosInformacion;
            this.OperandoSubElemento_Condicion = condicionACopiar.OperandoSubElemento_Condicion;
            this.TipoConector = condicionACopiar.TipoConector;
            this.TipoElementoCondicion = condicionACopiar.TipoElementoCondicion;
            this.TipoOpcionCondicion_ElementoOperacionEntrada = condicionACopiar.TipoOpcionCondicion_ElementoOperacionEntrada;
            this.TipoOpcionCondicion_TextosInformacion = condicionACopiar.TipoOpcionCondicion_TextosInformacion;
            this.TipoSubElemento_Condicion = condicionACopiar.TipoSubElemento_Condicion;
            this.TipoSubElemento_Condicion_Valores = condicionACopiar.TipoSubElemento_Condicion_Valores;
            this.TipoTextosInformacion_Valores = condicionACopiar.TipoTextosInformacion_Valores;
            this.ElementoOperacion_Valores = condicionACopiar.ElementoOperacion_Valores;
            this.ElementoDefinicion_Valores = condicionACopiar.ElementoDefinicion_Valores;
            this.ElementoDefinicionListas_Valores = condicionACopiar.ElementoDefinicionListas_Valores;
            this.Valores_Condicion = condicionACopiar.Valores_Condicion;
            this.ElementoOperacion_Valores_ElementoAsociado = condicionACopiar.ElementoOperacion_Valores_ElementoAsociado;
            this.OperandoSubElemento_Condicion_Elemento = condicionACopiar.OperandoSubElemento_Condicion_Elemento;
            this.TipoElemento_Valores = condicionACopiar.TipoElemento_Valores;
            this.SubElementoOperacion_Valores = condicionACopiar.SubElementoOperacion_Valores;
            this.OpcionSeleccionNumerosElemento_Condicion = condicionACopiar.OpcionSeleccionNumerosElemento_Condicion;
            this.OpcionSeleccionNumerosElemento_Condicion_TextosInformacion = condicionACopiar.OpcionSeleccionNumerosElemento_Condicion_TextosInformacion;
            this.OpcionSeleccionNumerosElemento_Condicion_Valores = condicionACopiar.OpcionSeleccionNumerosElemento_Condicion_Valores;
            this.OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion = condicionACopiar.OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion;
            this.OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion = condicionACopiar.OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion;
            this.CantidadNumerosCumplenCondicion = condicionACopiar.CantidadNumerosCumplenCondicion;
            this.OpcionCantidadNumerosCumplenCondicion = condicionACopiar.OpcionCantidadNumerosCumplenCondicion;
            this.OpcionCantidadDeterminadaNumerosCumplenCondicion = condicionACopiar.OpcionCantidadDeterminadaNumerosCumplenCondicion;
            this.CantidadSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.CantidadSubNumerosCumplenCondicion_OperacionEntrada;
            this.OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada;
            this.CantidadSubNumerosCumplenCondicion_TextosInformacion = condicionACopiar.CantidadSubNumerosCumplenCondicion_TextosInformacion;
            this.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion;
            this.CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = condicionACopiar.CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion;
            this.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion;
            this.CantidadSubNumerosCumplenCondicion_Valores = condicionACopiar.CantidadSubNumerosCumplenCondicion_Valores;
            this.OpcionCantidadSubNumerosCumplenCondicion_Valores = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_Valores;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores;
            this.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = condicionACopiar.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
            this.CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = condicionACopiar.CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion;
            this.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
            this.OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion;
            this.IncluirNombreElementoConTextos = condicionACopiar.IncluirNombreElementoConTextos;
            this.IncluirSoloNombreElemento = condicionACopiar.IncluirSoloNombreElemento;
            this.ContenedorCondiciones = condicionACopiar.ContenedorCondiciones;
            this.TipoElementoComparar_TextosInformacion = condicionACopiar.TipoElementoComparar_TextosInformacion;
            this.NegarCondicion = condicionACopiar.NegarCondicion;
            this.QuitarEspaciosTemporalmente_CadenaCondicion = condicionACopiar.QuitarEspaciosTemporalmente_CadenaCondicion;
            this.SeguirAplicandoCondicion_AlFinalCantidadesOperando = condicionACopiar.SeguirAplicandoCondicion_AlFinalCantidadesOperando;
            this.SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores = condicionACopiar.SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores;
            this.ReiniciarPosicion_AlFinalCantidadesOperando = condicionACopiar.ReiniciarPosicion_AlFinalCantidadesOperando;
            this.ReiniciarPosicion_AlFinalCantidadesOperando_Valores = condicionACopiar.ReiniciarPosicion_AlFinalCantidadesOperando_Valores;
            this.OpcionSaldoCantidadNumerosCumplenCondicion = condicionACopiar.OpcionSaldoCantidadNumerosCumplenCondicion;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion;
            this.CumpleCondicion_ElementoSinNumeros = condicionACopiar.CumpleCondicion_ElementoSinNumeros;
            this.CumpleCondicion_ElementoValores_SinNumeros = condicionACopiar.CumpleCondicion_ElementoValores_SinNumeros;
            this.ConectorO_Excluyente = condicionACopiar.ConectorO_Excluyente;
            this.ConsiderarOperandoCondicion_SiCumple = condicionACopiar.ConsiderarOperandoCondicion_SiCumple;
            this.ConsiderarIncluirCondicionesHijas = condicionACopiar.ConsiderarIncluirCondicionesHijas;
            this.Busqueda_TextoBusqueda = condicionACopiar.Busqueda_TextoBusqueda;
            this.BuscarCualquierTextoInformacion_TextoBusqueda = condicionACopiar.BuscarCualquierTextoInformacion_TextoBusqueda;
            this.CantidadNumeros_PorElemento = condicionACopiar.CantidadNumeros_PorElemento;
            this.CantidadNumeros_PorElemento_Valores = condicionACopiar.CantidadNumeros_PorElemento_Valores;
            this.CantidadTextosInformacion_PorElemento = condicionACopiar.CantidadTextosInformacion_PorElemento;
            this.CantidadTextosInformacion_PorElemento_Valores = condicionACopiar.CantidadTextosInformacion_PorElemento_Valores;
            this.ConsiderarSoloValores_ProcesamientoCantidades = condicionACopiar.ConsiderarSoloValores_ProcesamientoCantidades;
            this.ConsiderarValores_ProcesamientoCantidades = condicionACopiar.ConsiderarValores_ProcesamientoCantidades;
            this.ConsiderarSoloValores_ProcesamientoCantidades_Valores = condicionACopiar.ConsiderarSoloValores_ProcesamientoCantidades_Valores;
            this.ConsiderarValores_ProcesamientoCantidades_Valores = condicionACopiar.ConsiderarValores_ProcesamientoCantidades_Valores;
            this.OpcionValorPosicion = condicionACopiar.OpcionValorPosicion;
            this.EsOperandoActual = condicionACopiar.EsOperandoActual;
            this.EsOperandoTextosActual = condicionACopiar.EsOperandoTextosActual;
            this.EsOperandoValoresTextosActual = condicionACopiar.EsOperandoValoresTextosActual;
            this.EsOperandoValoresActual = condicionACopiar.EsOperandoValoresActual;
            this.CantidadTextosInformacion_SoloCadenasCumplen = condicionACopiar.CantidadTextosInformacion_SoloCadenasCumplen;
            this.CantidadTextosInformacion_SoloCadenasCumplen_Valores = condicionACopiar.CantidadTextosInformacion_SoloCadenasCumplen_Valores;
            this.CadenasTextoSon_Clasificadores = condicionACopiar.CadenasTextoSon_Clasificadores;
            this.CadenasTextoSon_Clasificadores_Valores = condicionACopiar.CadenasTextoSon_Clasificadores_Valores;

            foreach (var itemCondicion in condicionACopiar.Condiciones)
            {
                this.Condiciones.Add(new CondicionImplicacionTextosInformacion());
                this.Condiciones.Last().CopiarObjeto(itemCondicion, this);
            }
        }

        public bool VerificaEntrada(Entrada entrada)
        {
            if (entrada != null & ElementoEntrada_Valores != null && 
                ElementoEntrada_Valores == entrada)
                return true;
            else
            {
                foreach (var itemCondicion in Condiciones)
                {
                    if (itemCondicion.VerificaEntrada(entrada))
                        return true;
                }

                return false;
            }
        }

        public bool VerificarOperando(DiseñoOperacion operando)
        {
            List<CondicionImplicacionTextosInformacion> condicionesAsociadasOperando = new List<CondicionImplicacionTextosInformacion>();
            List<CondicionImplicacionTextosInformacion> condicionesAsociadasOperando2 = new List<CondicionImplicacionTextosInformacion>();

            ObtenerCondicionOperandoCondicion_Condiciones(ref condicionesAsociadasOperando, operando);
            ObtenerCondicionElementoCondicion_Condiciones(ref condicionesAsociadasOperando2, operando);

            return condicionesAsociadasOperando.Any() | condicionesAsociadasOperando2.Any();
        }

        public bool VerificarOperandoValores(DiseñoOperacion operando)
        {
            List<CondicionImplicacionTextosInformacion> condicionesAsociadasOperando = new List<CondicionImplicacionTextosInformacion>();
            List<CondicionImplicacionTextosInformacion> condicionesAsociadasOperando2 = new List<CondicionImplicacionTextosInformacion>();

            ObtenerCondicionOperandoValoresCondicion_Condiciones(ref condicionesAsociadasOperando, operando);
            ObtenerCondicionElementoValoresCondicion_Condiciones(ref condicionesAsociadasOperando2, operando);

            return condicionesAsociadasOperando.Any() | condicionesAsociadasOperando2.Any();
        }

        public bool VerificarSubOperando(DiseñoElementoOperacion operando)
        {
            List<CondicionImplicacionTextosInformacion> condicionesAsociadasOperando = new List<CondicionImplicacionTextosInformacion>();
            
            ObtenerCondicionElementoDiseñoCondicion_Condiciones(ref condicionesAsociadasOperando, operando);

            return condicionesAsociadasOperando.Any();
        }

        public void QuitarCondicionesEntrada(Entrada entrada)
        {
            List<CondicionImplicacionTextosInformacion> condicionesAQuitar = new List<CondicionImplicacionTextosInformacion>();

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.QuitarCondicionesEntrada(entrada);

                if (itemCondicion.ElementoEntrada_Valores == entrada)
                    condicionesAQuitar.Add(itemCondicion);
            }

            while (condicionesAQuitar.Any())
            {
                Condiciones.Remove(condicionesAQuitar.FirstOrDefault());
                condicionesAQuitar.Remove(condicionesAQuitar.FirstOrDefault());
            }
        }

        public void QuitarReferenciasCondicionesElemento(DiseñoOperacion elemento)
        {
            foreach(var itemCondicion in Condiciones)
            {
                itemCondicion.QuitarReferenciasCondicionesElemento(elemento);
            }

            if (ElementoCondicion == elemento)
                ElementoCondicion = null;

            if (ElementoOperacion_Valores == elemento)
                ElementoOperacion_Valores = null;

            if (ElementoOperacion_Valores_ElementoAsociado == elemento)
                ElementoOperacion_Valores_ElementoAsociado = null;
        }

        public void QuitarReferenciasCondicionesEntrada(Entrada entrada)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.QuitarReferenciasCondicionesEntrada(entrada);
            }

            if (EntradaCondicion == entrada)
                EntradaCondicion = null;
        }

        public void QuitarReferenciasCondicionesElemento_Interno(DiseñoElementoOperacion elemento)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.QuitarReferenciasCondicionesElemento_Interno(elemento);
            }

            if (OperandoSubElemento_Condicion_TextosInformacion == elemento)
                OperandoSubElemento_Condicion_TextosInformacion = null;

            if (OperandoSubElemento_Condicion == elemento)
                OperandoSubElemento_Condicion = null;

            if (SubElementoOperacion_Valores == elemento)
                SubElementoOperacion_Valores = null;

            if (OperandoSubElemento_Condicion_Elemento == elemento)
                OperandoSubElemento_Condicion_Elemento = null;
        }

        public void ObtenerCondicionElementoCondicion_Condiciones(ref List<CondicionImplicacionTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (ElementoCondicion == elemento ||
                EsOperandoActual || EsOperandoTextosActual)
            {
                condiciones.Add(this);
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionElementoCondicion_Condiciones(ref condiciones, elemento);
            }
        }

        public void ObtenerCondicionElementoCondicion_Condiciones_VerificarPosicionActual(ref List<CondicionImplicacionTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (ElementoCondicion == elemento ||
                EsOperandoActual)
            {
                if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                OpcionSeleccionNumerosElemento_Condicion_TextosInformacion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                OpcionSeleccionNumerosElemento_Condicion_Valores == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion)
                {
                    condiciones.Add(this);
                }
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionElementoCondicion_Condiciones_VerificarPosicionActual(ref condiciones, elemento);
            }
        }

        public void ObtenerCondicionElementoValoresCondicion_Condiciones(ref List<CondicionImplicacionTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (ElementoOperacion_Valores_ElementoAsociado == elemento)
            {
                condiciones.Add(this);
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionElementoValoresCondicion_Condiciones(ref condiciones, elemento);
            }
        }

        public void ObtenerCondicionElementoDiseñoCondicion_Condiciones(ref List<CondicionImplicacionTextosInformacion> condiciones, DiseñoElementoOperacion elemento)
        {
            if (OperandoSubElemento_Condicion == elemento |
                SubElementoOperacion_Valores == elemento |
                OperandoSubElemento_Condicion_Elemento == elemento |
                OperandoSubElemento_Condicion_TextosInformacion == elemento)
            {
                condiciones.Add(this);
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionElementoDiseñoCondicion_Condiciones(ref condiciones, elemento);
            }
        }

        public void ObtenerCondicionOperandoCondicion_Condiciones(ref List<CondicionImplicacionTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (OperandoCondicion == elemento || 
                EsOperandoTextosActual || EsOperandoActual)
            {
                condiciones.Add(this);
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionOperandoCondicion_Condiciones(ref condiciones, elemento);
            }
        }

        public void ObtenerCondicionOperandoCondicion_Condiciones_VerificarPosicionActual(ref List<CondicionImplicacionTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (OperandoCondicion == elemento ||
                EsOperandoTextosActual)
            {
                if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                OpcionSeleccionNumerosElemento_Condicion_TextosInformacion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                OpcionSeleccionNumerosElemento_Condicion_Valores == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion)
                {
                    condiciones.Add(this);
                }                
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionOperandoCondicion_Condiciones_VerificarPosicionActual(ref condiciones, elemento);
            }
        }

        public void ObtenerCondicionOperandoValoresCondicion_Condiciones(ref List<CondicionImplicacionTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (ElementoOperacion_Valores == elemento)
            {
                condiciones.Add(this);
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionOperandoValoresCondicion_Condiciones(ref condiciones, elemento);
            }
        }

        public void ObtenerCondicionEntrada_Condiciones(ref List<CondicionImplicacionTextosInformacion> condiciones, Entrada entrada)
        {
            if (ElementoEntrada_Valores == entrada)
            {
                condiciones.Add(this);
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionEntrada_Condiciones(ref condiciones, entrada);
            }
        }

        public bool EvaluarCondiciones(
            AsignacionImplicacion_TextosInformacion asignacion,
            EjecucionCalculo ejecucion,
            ElementoOperacionAritmeticaEjecucion operacion,
            ElementoDiseñoOperacionAritmeticaEjecucion subOperacion,
            ElementoOperacionAritmeticaEjecucion operando,
            EntidadNumero numero,
            List<EntidadNumero> NumerosResultado)
        {           
            if(ContenedorCondiciones)
            {
                NumerosVinculados_CondicionAnterior_Total.Clear();
                OperandosVinculados_CondicionAnterior_Total.Clear();

            }

            TextosInvolucrados.Clear();

            bool valorCondicion = !ContenedorCondiciones ? EvaluarCondicion(asignacion,
                ejecucion,
                operacion,
                subOperacion,
                (operando != null && operando.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion)) ? operando : null,
                (operando != null && operando.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion)) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                numero,
                NumerosResultado) : true;

            if (valorCondicion)
            {
                if (CantidadTextosInformacion_SoloCadenasCumplen |
                    CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                {
                    asignacion.TextosInformacionImplicacion_CumplenCondicion.Clear();
                }

                asignacion.TextosInformacionImplicacion_CumplenCondicion.AddRange(TextosInvolucrados);                
            }

            if (Condiciones.Any())
            {
                if (!valorCondicion)
                {
                    NumerosVinculados_CondicionAnterior_Temp.Clear();
                    OperandosVinculados_CondicionAnterior_Temp.Clear();
                }

                List<bool> valoresCondicion = new List<bool>();

                if (!ContenedorCondiciones)
                    valoresCondicion.Add(valorCondicion);

                CondicionImplicacionTextosInformacion condicionAnterior = this;
                CondicionImplicacionTextosInformacion condicionContenedora = this;
                
                int indiceCondicion = 0;
                foreach (var itemCondicion in Condiciones)
                {
                    if (itemCondicion == Condiciones.FirstOrDefault())
                        itemCondicion.PrimeraCondicion = true;

                    if (itemCondicion != Condiciones.FirstOrDefault() &&
                        condicionContenedora != null)
                        itemCondicion.ActualizarElementosNumerosVinculados_CondicionesAnteriores(condicionContenedora);

                    if (condicionAnterior != null)
                        itemCondicion.ActualizarElementosNumerosVinculados_CondicionesAnteriores(condicionAnterior);

                    itemCondicion.PosicionActualImplicacion = PosicionActualImplicacion;
                    itemCondicion.PosicionActualInstanciaImplicacion = PosicionActualInstanciaImplicacion;
                    itemCondicion.PosicionActualIteracionImplicacion = PosicionActualIteracionImplicacion;

                    bool valorItemCondicion = itemCondicion.EvaluarCondiciones(asignacion,
                            ejecucion,
                            operacion,
                            subOperacion,
                            operando,
                            numero,
                            NumerosResultado);

                    if (!valorItemCondicion)
                    {
                        itemCondicion.NumerosVinculados_CondicionAnterior_Temp.Clear();
                        itemCondicion.OperandosVinculados_CondicionAnterior_Temp.Clear();
                    }
                    //else
                    //{
                    //    if (itemCondicion != Condiciones.FirstOrDefault())
                    //    {
                    //        itemCondicion.NumerosVinculados_CondicionAnterior_Temp = condicionAnterior.NumerosVinculados_CondicionAnterior_Temp.Intersect(itemCondicion.NumerosVinculados_CondicionAnterior_Temp).ToList();
                    //        itemCondicion.OperandosVinculados_CondicionAnterior_Temp = condicionAnterior.OperandosVinculados_CondicionAnterior_Temp.Intersect(itemCondicion.OperandosVinculados_CondicionAnterior_Temp).ToList();

                    //    }
                    //}

                    if (itemCondicion.CantidadTextosInformacion_SoloCadenasCumplen |
                        itemCondicion.CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                    {
                        TextosInvolucrados.Clear();
                    }

                    TextosInvolucrados.AddRange(itemCondicion.TextosInvolucrados);

                    valoresCondicion.Add(valorItemCondicion);

                    if (ContenedorCondiciones &&
                                itemCondicion == Condiciones.First() &&
                                !valorItemCondicion &&
                                Condiciones.Count == 1)
                    {
                        Valor_Condicion = false;
                        return false;
                    }

                    switch (itemCondicion.TipoConector)
                    {
                        case TipoConectorCondiciones_ConjuntoBusquedas.O:

                            if (itemCondicion != Condiciones.Last() &&
                            Condiciones[indiceCondicion + 1].TipoConector != itemCondicion.TipoConector)
                            {
                                if (!((itemCondicion.ConectorO_Excluyente &&
                                    valoresCondicion.Count(i => i) == 1) ||
                                    (!itemCondicion.ConectorO_Excluyente &&
                                    valoresCondicion.Contains(true))))
                                {
                                    EvaluacionesNoCumplenCondicion++;
                                    LimpiarVariables_ElementosVinculados(true);
                                    LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                    Valor_Condicion = false;
                                    return false;
                                }
                                else
                                {
                                    valoresCondicion.Clear();
                                    continue;
                                }
                            }
                            else if (itemCondicion == Condiciones.Last())
                            {
                                if (!((itemCondicion.ConectorO_Excluyente &&
                                    valoresCondicion.Count(i => i) == 1) ||
                                    (!itemCondicion.ConectorO_Excluyente &&
                                    valoresCondicion.Contains(true))))
                                {
                                    EvaluacionesNoCumplenCondicion++;
                                    LimpiarVariables_ElementosVinculados(true);
                                    LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                    Valor_Condicion = false;
                                    return false;
                                }
                            }

                            break;

                        case TipoConectorCondiciones_ConjuntoBusquedas.Y:

                            if (itemCondicion != Condiciones.Last() &&
                        Condiciones[indiceCondicion + 1].TipoConector != itemCondicion.TipoConector)
                            {
                                if (valoresCondicion.Contains(false))
                                {
                                    EvaluacionesNoCumplenCondicion++;
                                    LimpiarVariables_ElementosVinculados(true);
                                    LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                    Valor_Condicion = false;
                                    return false;
                                }
                                else
                                {
                                    valoresCondicion.Clear();
                                    continue;
                                }
                            }
                            else if (itemCondicion == Condiciones.Last() && valoresCondicion.Contains(false))
                            {
                                EvaluacionesNoCumplenCondicion++;
                                LimpiarVariables_ElementosVinculados(true);
                                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                Valor_Condicion = false;
                                return false;
                            }

                            break;
                    }

                    indiceCondicion++;
                    condicionAnterior = itemCondicion;

                }

                EvaluacionesCumplenCondicion++;

                if (OpcionSaldoCantidadNumerosCumplenCondicion)
                {
                    switch (OpcionCantidadNumerosCumplenCondicion)
                    {
                        case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                            if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion == 0)
                            {
                                LimpiarVariables_ElementosVinculados(true);
                                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                Valor_Condicion = false;
                                return false;
                            }

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion == EvaluacionesCumplenCondicion)
                            {
                                LimpiarVariables_ElementosVinculados(true);
                                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                Valor_Condicion = false;
                                return false;
                            }

                            break;

                        case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:
                            switch (OpcionCantidadDeterminadaNumerosCumplenCondicion)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion < CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        Valor_Condicion = false;
                                        return false;
                                    }
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion > CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        Valor_Condicion = false;
                                        return false;
                                    }
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion != CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        Valor_Condicion = false;
                                        return false;
                                    }
                                    break;
                            }

                            break;
                    }
                }
                else
                {
                    switch (OpcionCantidadNumerosCumplenCondicion)
                    {
                        case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                            if (EvaluacionesCumplenCondicion == 0)
                            {
                                LimpiarVariables_ElementosVinculados(true);
                                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                Valor_Condicion = false;
                                return false;
                            }

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (EvaluacionesNoCumplenCondicion > 0)
                            {
                                LimpiarVariables_ElementosVinculados(true);
                                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                Valor_Condicion = false;
                                return false;
                            }

                            break;

                        case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:
                            switch (OpcionCantidadDeterminadaNumerosCumplenCondicion)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (EvaluacionesCumplenCondicion < CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        Valor_Condicion = false;
                                        return false;
                                    }
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (EvaluacionesCumplenCondicion > CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        Valor_Condicion = false;
                                        return false;
                                    }
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (EvaluacionesCumplenCondicion != CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        Valor_Condicion = false;
                                        return false;
                                    }
                                    break;
                            }

                            break;
                    }
                }

                if (CondicionContenedora == null &&
                    ContenedorCondiciones)
                {
                    var condiciones = Condiciones.Where(i => i.Valor_Condicion).ToList();
                           
                    foreach(var condicion in condiciones)
                    {
                        OperandosVinculados_CondicionAnterior_Total.AddRange(condicion.OperandosVinculados_CondicionAnterior_Temp.Distinct());
                        SubOperandosVinculados_CondicionAnterior_Total.AddRange(condicion.SubOperandosVinculados_CondicionAnterior_Temp.Distinct());
                        ElementosVinculados_CondicionAnterior_Total.AddRange(condicion.ElementosVinculados_CondicionAnterior_Temp.Distinct());
                        NumerosVinculados_CondicionAnterior_Total.AddRange(condicion.NumerosVinculados_CondicionAnterior_Temp.Distinct());
                    }
                }

                LimpiarVariables_ElementosVinculados(false);
                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                if ((ConsiderarIncluirCondicionesHijas ||
                    (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones)))
                    EstablecerConsiderarOperando(ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null, numero);

                Valor_Condicion = true;
                return true;
                //}
                //else
                //    return valorCondicion;
            }
            else
            {
                if (valorCondicion)
                    EvaluacionesCumplenCondicion++;
                else
                    EvaluacionesNoCumplenCondicion++;

                if (OpcionSaldoCantidadNumerosCumplenCondicion)
                {
                    switch (OpcionCantidadNumerosCumplenCondicion)
                    {
                        case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                            if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion == 0)
                                valorCondicion = false;

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion == EvaluacionesCumplenCondicion)
                                valorCondicion = false;

                            break;

                        case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:
                            switch (OpcionCantidadDeterminadaNumerosCumplenCondicion)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion < CantidadNumerosCumplenCondicion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion > CantidadNumerosCumplenCondicion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion != CantidadNumerosCumplenCondicion)
                                        valorCondicion = false;
                                    break;
                            }

                            break;
                    }
                }
                else
                {
                    switch (OpcionCantidadNumerosCumplenCondicion)
                    {
                        case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                            if (EvaluacionesCumplenCondicion == 0)
                                valorCondicion = false;

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (EvaluacionesNoCumplenCondicion > 0)
                                valorCondicion = false;

                            break;

                        case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:
                            switch (OpcionCantidadDeterminadaNumerosCumplenCondicion)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (EvaluacionesCumplenCondicion < CantidadNumerosCumplenCondicion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (EvaluacionesCumplenCondicion > CantidadNumerosCumplenCondicion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (EvaluacionesCumplenCondicion != CantidadNumerosCumplenCondicion)
                                        valorCondicion = false;
                                    break;
                            }

                            break;
                    }
                }

                //LimpiarVariables_ElementosVinculados(!valorCondicion);

                if (valorCondicion && (ConsiderarIncluirCondicionesHijas ||
                    (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones)))
                    EstablecerConsiderarOperando(ejecucion,
                        (operando != null && operando.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion)) ? operando : null,
                        (operando != null && operando.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion)) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null, numero);

                Valor_Condicion = valorCondicion;
                return valorCondicion;
            }
        }

        public void AumentarPosicionesElementosCondicion_EntradasTextos(AsignacionImplicacion_TextosInformacion asignacion)
        {
            
                if (ElementoEntrada_Valores != null)

                {
                    var elementosEntrada_Totales = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == ElementoEntrada_Valores &
                                asignacion.DiseñoTextosInformacion_Relacionado.ElementosAnteriores.Contains(item)).ToList();

                    foreach (var itemEntrada in elementosEntrada_Totales)
                    {
                        itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                    }
                }

                if (EntradaCondicion != null)
                {
                    var elementosEntrada_Totales2 = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion &
                                    asignacion.DiseñoTextosInformacion_Relacionado.ElementosAnteriores.Contains(item)).ToList();

                    foreach (var itemEntrada in elementosEntrada_Totales2)
                    {
                        itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                    }
                }

                
        }

        private bool EvaluarCondicion(AsignacionImplicacion_TextosInformacion asignacion,
            EjecucionCalculo ejecucion,
            ElementoOperacionAritmeticaEjecucion operacionCondicionEjecucion,
            ElementoDiseñoOperacionAritmeticaEjecucion operacionInternaCondicionEjecucion,
            ElementoOperacionAritmeticaEjecucion operando,
            ElementoDiseñoOperacionAritmeticaEjecucion subOperando,
            EntidadNumero numero,
            List<EntidadNumero> NumerosResultado)
        {
            bool valorCondicion = false;
            bool sinNumerosTextos = false;
            bool sinNumerosTextos_Valores = false;

            List<string[]> listaValoresCondicion = new List<string[]>();
            string[] valoresCondicion = null;

            int CantidadTextosCondicion = 0;
            int CantidadTextosValoresCondicion = 0;

            int CantidadNumerosCondicion_TextosInformacion = 0;
            int CantidadNumerosCondicion_OperacionEntrada = 0;
            int CantidadNumerosValoresCondicion = 0;

            int NumerosCumplenCondicion_Elemento = 0;
            int NumerosNoCumplenCondicion_Elemento = 0;

            int NumerosCumplenCondicion_Valores = 0;
            int NumerosNoCumplenCondicion_Valores = 0;

            int TextosCumplenCondicion_TextosInformacion = 0;
            int TextosNoCumplenCondicion_TextosInformacion = 0;

            int TextosCumplenCondicion_Valores = 0;
            int TextosNoCumplenCondicion_Valores = 0;

            List<InformacionCantidadesTextosInformacion_CondicionImplicacion> CantidadesTextos = new List<InformacionCantidadesTextosInformacion_CondicionImplicacion>();
            int indiceCantidadesTextos_Valores = 0;

            List<string> TextosInvolucrados_ProcesamientoPorCantidad = new List<string>();

            List<InformacionCantidadesNumerosInformacion_CondicionImplicacion> CantidadesNumeros = new List<InformacionCantidadesNumerosInformacion_CondicionImplicacion>();

            ElementoEntradaEjecucion elementoEjecucionCondicion_Valores_ConjuntoEntrada = null;
            ElementoOperacionAritmeticaEjecucion elementoEjecucionCondicion_Valores_Operacion = null;
            ElementoDiseñoOperacionAritmeticaEjecucion elementoEjecucionCondicion_Valores_SubOperacion = null;

            var operandosValores = new List<ElementoEjecucionCalculo>();
            var elementosOperandoValores = new List<ElementoEjecucionCalculo>();
            var filasOperandoValores = new List<FilaTextosInformacion_Entrada>();
            var subElementosOperandoValores = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            var numerosOperandoValores = new List<EntidadNumero>();

            var operandosCondicion = new List<ElementoEjecucionCalculo>();
            var elementosOperandoCondicion = new List<ElementoEjecucionCalculo>();
            var filasOperandoCondicion = new List<FilaTextosInformacion_Entrada>();
            var subElementosOperandoCondicion = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            var numerosOperandoCondicion = new List<EntidadNumero>();

            int repeticiones = 1;
            int indiceInicial = 0;

            //List<List<string>> FilasTextosCondicion = new List<List<string>>();

            switch (TipoElementoCondicion)
            {
                case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion:

                    switch (TipoTextosInformacion_Valores)
                    {
                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada:

                            switch (OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion)
                            {
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                    if (ElementoEntrada_Valores != null)
                                    {
                                        var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                        var elementosEntrada_Totales = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == ElementoEntrada_Valores &&
                                        asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                        var listasTextos = asignacion.ObtenerTextos_CondicionEntrada(ElementoEntrada_Valores, elementosEntrada_Totales.FirstOrDefault().EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);

                                        repeticiones = listasTextos.Count;

                                        indiceInicial = elementosEntrada_Totales.FirstOrDefault().EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion;

                                    }
                                    break;

                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                    var listas = asignacion.ObtenerTextos_CondicionEntrada(ElementoEntrada_Valores);

                                    repeticiones = listas.Count;

                                    break;
                            }

                            break;
                    }

                    OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                        new InfoOpcion_VinculadosAnterior()
                        {
                            OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion_TextosInformacion,
                            OperandoRelacionado_Ejecucion = operando,
                            SubOperandoRelacionado_Ejecucion = subOperando
                        });

                    switch (OpcionSeleccionNumerosElemento_Condicion_TextosInformacion)
                    {
                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                            if (EntradaCondicion != null)
                            {
                                var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                var elementosEntrada_Totales = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion &&
                                asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                var listas = asignacion.ObtenerTextos_CondicionEntrada(EntradaCondicion, elementosEntrada_Totales.FirstOrDefault().EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);

                                repeticiones = listas.Count;

                                indiceInicial = elementosEntrada_Totales.FirstOrDefault().EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion;
                            }

                            break;

                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                            if (EntradaCondicion != null)
                            {
                                var listas = asignacion.ObtenerTextos_CondicionEntrada(EntradaCondicion);

                                repeticiones = listas.Count;
                            }


                            break;
                    }

                    break;
            }

            List<DuplaTextosInformacion_ProcesamientoCondicionImplicacion> duplasTextosInformacion_Elementos = new List<DuplaTextosInformacion_ProcesamientoCondicionImplicacion>();
            List<int> PosicionesImplicacion_CumplenCondicion_Valores = new List<int>();
            //int PosicionImplicacion_CumplenCondicion = 0;

            for (int PosicionActualNumero_CondicionesOperador_Implicacion_Entrada = indiceInicial;
                PosicionActualNumero_CondicionesOperador_Implicacion_Entrada < repeticiones;
                PosicionActualNumero_CondicionesOperador_Implicacion_Entrada++)
            {
                bool NumeroIncluidoEnOperando = false;
                bool valorCondicion_Iteracion = false;
                List<string> TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada = new List<string>();
                List<string> TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores = new List<string>();

                switch (TipoElementoCondicion)
                {
                    case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion:

                        List<ElementoEjecucionCalculo> elementos_CondicionEvaluar = new List<ElementoEjecucionCalculo>();
                        List<EntidadNumero> numeros_CondicionEvaluar = new List<EntidadNumero>();

                        //int PosicionActualNumero_CondicionesOperador_Implicacion = -1;

                        switch (TipoTextosInformacion_Valores)
                        {
                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacionFijos:
                                valoresCondicion = Valores_Condicion.Split('|');
                                listaValoresCondicion.Add(valoresCondicion);

                                NumerosVinculados_CondicionAnterior.Clear();
                                SubOperandosVinculados_CondicionAnterior.Clear();
                                OperandosVinculados_CondicionAnterior.Clear();
                                ElementosVinculados_CondicionAnterior.Clear();

                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada:

                                //PosicionActualNumero_CondicionesOperador_Implicacion = PosicionActualNumero_CondicionesOperador_Implicacion_Entrada;

                                switch (OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                        if (CantidadTextosInformacion_PorElemento_Valores)
                                        {
                                            var listas = asignacion.ObtenerTextos_CondicionEntrada(ElementoEntrada_Valores);
                                            foreach (var lista in listas)
                                            {
                                                if (lista.Count > 0)
                                                    listaValoresCondicion.Add(lista.ToArray());
                                            }
                                        }
                                        else
                                        {
                                            List<string> textos = new List<string>();

                                            var listas = asignacion.ObtenerTextos_CondicionEntrada(ElementoEntrada_Valores);
                                            foreach (var lista in listas)
                                                textos.AddRange(lista);

                                            valoresCondicion = textos.ToArray();
                                        }

                                        break;

                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                        int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(PosicionActualNumero_CondicionesOperador_Implicacion_Entrada,
                                            OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion,
                                            asignacion.TextosCondicion.Count);

                                        valoresCondicion = asignacion.ObtenerTextos_CondicionEntrada(ElementoEntrada_Valores,
                                            indicePosicion).ToArray();

                                        break;
                                }

                                if (valoresCondicion != null)
                                {
                                    if (valoresCondicion.Length > 0)
                                        listaValoresCondicion.Add(valoresCondicion.ToArray());
                                    CantidadTextosValoresCondicion += valoresCondicion.Length;
                                    ////CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());
                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = valoresCondicion.Length;
                                }
                                //else
                                //{
                                //    //CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());
                                //    //CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = 0;
                                //}

                                NumerosVinculados_CondicionAnterior.Clear();
                                SubOperandosVinculados_CondicionAnterior.Clear();
                                OperandosVinculados_CondicionAnterior.Clear();
                                ElementosVinculados_CondicionAnterior.Clear();

                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion:

                                if (ElementoOperacion_Valores != null ||
                                    EsOperandoValoresTextosActual)
                                {
                                    var elementoOperandoCondicion_Valores = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores);
                                    var subElementoOperandoCondicion_Valores = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerSubElementoEjecucion(SubElementoOperacion_Valores);

                                    if(EsOperandoValoresTextosActual)
                                    {
                                        elementoOperandoCondicion_Valores = operando;

                                        if(operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                            subElementoOperandoCondicion_Valores = (ElementoOperacionAritmeticaEjecucion)operando;
                                    }
                                    //OperandoCondicion_Valores_Posterior = ElementoOperacion_Valores;
                                    //OperandoSubElemento_Condicion_Valores_Posterior = SubElementoOperacion_Valores;

                                    //var elementoEjecucion = ejecucion.ObtenerElementoEjecucion(OperandoCondicion_Anterior);
                                    //var subElementoEjecucion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_Anterior);

                                    var elementoOperandoCondicion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(OperandoCondicion);
                                    var subElementoOperandoCondicion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_TextosInformacion);


                                    if (elementoOperandoCondicion_Valores != null &&
                                        subElementoOperandoCondicion_Valores != null)
                                    {
                                        elementoOperandoCondicion_Valores = (ElementoOperacionAritmeticaEjecucion)subElementoOperandoCondicion_Valores;
                                        elementoOperandoCondicion = (ElementoOperacionAritmeticaEjecucion)subElementoOperandoCondicion;
                                    }


                                    {

                                        if (elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion >=
                                                                (elementoOperandoCondicion_Valores).Numeros.Where(i =>
                                                                (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count))) ||
                                                (elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto))) &&
                                                                ((ConsiderarValores_ProcesamientoCantidades_Valores &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades_Valores &&
                            !ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Count)
                                        {
                                            if (ReiniciarPosicion_AlFinalCantidadesOperando_Valores)
                                            {
                                                elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
                                                
                                            }
                                            else
                                            {
                                                if (!SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores)
                                                    if (CumpleCondicion_ElementoValores_SinNumeros)
                                                        return true;
                                                    else
                                                        return false;
                                            }
                                        }

                                        List<EntidadNumero> listaCantidades = new List<EntidadNumero>();

                                        if (elementoOperandoCondicion_Valores != operando)
                                        {
                                            listaCantidades = (elementoOperandoCondicion_Valores).Numeros.Where(i =>
                                            (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count))) ||
                                                (elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto))) &&
                                            ((ConsiderarValores_ProcesamientoCantidades_Valores &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades_Valores &&
                            !ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();
                                        }
                                        else
                                        {
                                            listaCantidades = (elementoOperandoCondicion_Valores).Numeros.Where(i =>
                                            (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count))) ||
                                                (elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto))) &&
                                            ((ConsiderarValores_ProcesamientoCantidades_Valores &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades_Valores &&
                            !ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Where(i =>
                                                //(!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                                //(i.ElementosSalidaOperacion_Agrupamiento.Any() &&
                                                //i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion))) &
                                                (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                                (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionInternaCondicionEjecucion))) &
                                                (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                                (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionInternaCondicionEjecucion)))).ToList();
                                        }

                                        if (elementoOperandoCondicion_Valores.GetType() == typeof(ElementoEntradaEjecucion))
                                            elementoEjecucionCondicion_Valores_ConjuntoEntrada = (ElementoEntradaEjecucion)elementoOperandoCondicion_Valores;
                                        else if ((elementoOperandoCondicion_Valores.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion)))
                                            elementoEjecucionCondicion_Valores_SubOperacion = (ElementoDiseñoOperacionAritmeticaEjecucion)elementoOperandoCondicion_Valores;
                                        else if (elementoOperandoCondicion_Valores.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                                            elementoEjecucionCondicion_Valores_Operacion = (ElementoOperacionAritmeticaEjecucion)elementoOperandoCondicion_Valores;

                                        if (listaCantidades.Contains(numero))
                                        {
                                            switch (OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion)
                                            {
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                    operandosValores.Add(elementoOperandoCondicion_Valores);
                                                    elementosOperandoValores.Add(elementoOperandoCondicion_Valores);
                                                    //elementosOperandoValores.Add(elementoEjecucion);

                                                    numerosOperandoValores.Add(numero);

                                                    if (elementoOperandoCondicion_Valores != null &&
                                                    elementoOperandoCondicion_Valores != elementoOperandoCondicion)
                                                    {
                                                        if (elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion <
                                                                listaCantidades.Count)
                                                        {
                                                            int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                                OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion,
                                                                listaCantidades.Count);

                                                            numerosOperandoValores.Add(listaCantidades[indicePosicion]);
                                                        }
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                                    operandosValores.Add(elementoOperandoCondicion_Valores);
                                                    elementosOperandoValores.Add(elementoOperandoCondicion_Valores);

                                                    numerosOperandoValores.AddRange(listaCantidades);

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:

                                                    operandosValores.Add(elementoOperandoCondicion_Valores);
                                                    elementosOperandoValores.Add(elementoOperandoCondicion_Valores);

                                                    numerosOperandoValores.AddRange(listaCantidades.Where(i => listaCantidades.IndexOf(i) <= elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion));

                                                    break;
                                            }

                                        }
                                        else if (OperandosVinculados_CondicionAnterior.Contains(elementoOperandoCondicion_Valores)
                                            && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                                        {
                                            switch (ObtenerOpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior(elementoOperandoCondicion_Valores, null))
                                            {
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                    operandosValores.Add(elementoOperandoCondicion_Valores);

                                                    if (elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion <
                                                            listaCantidades.Count)
                                                    {
                                                        int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                            OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion,
                                                            listaCantidades.Count);

                                                        if (NumerosVinculados_CondicionAnterior.Contains(listaCantidades[indicePosicion]))
                                                            numerosOperandoValores.Add(listaCantidades[indicePosicion]);
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:

                                                    operandosValores.Add(elementoOperandoCondicion_Valores);

                                                    foreach (var item in listaCantidades)
                                                    {
                                                        if (NumerosVinculados_CondicionAnterior.Contains(item))
                                                        {
                                                            numerosOperandoValores.Add(item);

                                                            switch (TipoElementoComparar_TextosInformacion)
                                                            {
                                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                                    if (!IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion += item.Textos.Count;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = item.Textos.Count;
                                                                    }
                                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion++;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    }

                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;
                                                            }
                                                        }
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                                    operandosValores.Add(elementoOperandoCondicion_Valores);

                                                    foreach (var item in listaCantidades.Where(i => listaCantidades.IndexOf(i) <= elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion))
                                                    {
                                                        if (NumerosVinculados_CondicionAnterior.Contains(item))
                                                        {
                                                            numerosOperandoValores.Add(item);

                                                            switch (TipoElementoComparar_TextosInformacion)
                                                            {
                                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                                    if (!IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion += item.Textos.Count;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = item.Textos.Count;
                                                                    }
                                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion++;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    }

                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;
                                                            }
                                                        }
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                                    operandosValores.Add(elementoOperandoCondicion_Valores);

                                                    foreach (var item in listaCantidades)
                                                    {
                                                        numerosOperandoValores.Add(item);

                                                        switch (TipoElementoComparar_TextosInformacion)
                                                        {
                                                            case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                                if (!IncluirSoloNombreElemento)
                                                                {
                                                                    CantidadTextosValoresCondicion += item.Textos.Count;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = item.Textos.Count;
                                                                }
                                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                {
                                                                    CantidadTextosValoresCondicion++;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                }

                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                CantidadTextosValoresCondicion += 1;
                                                                ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                                CantidadTextosValoresCondicion += 1;
                                                                ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                CantidadTextosValoresCondicion += 1;
                                                                ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                break;
                                                        }

                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:

                                                    operandosValores.Add(elementoOperandoCondicion_Valores);

                                                    foreach (var item in listaCantidades.Where(i => listaCantidades.IndexOf(i) <= elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion))
                                                    {
                                                        numerosOperandoValores.Add(item);

                                                        switch (TipoElementoComparar_TextosInformacion)
                                                        {
                                                            case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                                if (!IncluirSoloNombreElemento)
                                                                {
                                                                    CantidadTextosValoresCondicion += item.Textos.Count;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = item.Textos.Count;
                                                                }
                                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                {
                                                                    CantidadTextosValoresCondicion++;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                }

                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                CantidadTextosValoresCondicion += 1;
                                                                ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                                CantidadTextosValoresCondicion += 1;
                                                                ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                CantidadTextosValoresCondicion += 1;
                                                                ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                break;
                                                        }

                                                    }

                                                    break;
                                            }
                                        }
                                        else
                                        {

                                            switch (OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion)
                                            {
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                                    operandosValores.Add(elementoOperandoCondicion_Valores);

                                                    foreach (var item in (elementoOperandoCondicion_Valores).Numeros.Where(i =>
                                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count))) ||
                                                (elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto))) &&
                                                    ((ConsiderarValores_ProcesamientoCantidades_Valores &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades_Valores &&
                            !ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList())
                                                    {

                                                        numerosOperandoValores.Add(item);

                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                                    operandosValores.Add(elementoOperandoCondicion_Valores);

                                                    foreach (var item in (elementoOperandoCondicion_Valores).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count))) ||
                                                (elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto)))).Where(i =>
                                                     (elementoOperandoCondicion_Valores).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoOperandoCondicion_Valores.IndicePosicionClasificadores < elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Count))) ||
                                                (elementoOperandoCondicion_Valores.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoOperandoCondicion_Valores.Clasificadores_Cantidades[elementoOperandoCondicion_Valores.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion &&
                                                    ((ConsiderarValores_ProcesamientoCantidades_Valores &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades_Valores &&
                            !ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList())
                                                    {
                                                        numerosOperandoValores.Add(item);

                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                    //listaCantidades = ((ElementoConjuntoNumerosEntradaEjecucion)elementoEjecucionCondicion).Numeros;
                                                    operandosValores.Add(elementoOperandoCondicion_Valores);

                                                    if (elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion <
                                                                listaCantidades.Count)
                                                    {
                                                        int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoOperandoCondicion_Valores.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                            OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion,
                                                            listaCantidades.Count);

                                                        numerosOperandoValores.Add(listaCantidades[indicePosicion]);


                                                    }


                                                    break;

                                            }


                                        }
                                    }
                                    
                                    NumerosVinculados_CondicionAnterior.Clear();
                                    OperandosVinculados_CondicionAnterior.Clear();

                                    List<string> Textos = new List<string>();
                                    List<string[]> ListaTextos = new List<string[]>();

                                    foreach (ElementoOperacionAritmeticaEjecucion itemOperando in operandosValores)
                                    {
                                        OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                                new InfoOpcion_VinculadosAnterior()
                                                {
                                                    OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion,
                                                    OperandoRelacionado_Ejecucion = itemOperando
                                                });

                                        if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                        {
                                            Textos = itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.ToList();
                                        }
                                        else
                                        {
                                            switch (OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion)
                                            {
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                                    foreach (var item in (itemOperando).Numeros.Intersect(numerosOperandoValores).ToList())
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()));

                                                            if (item.Textos.Count > 0)
                                                                ListaTextos.Add(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().ToArray());

                                                            switch (TipoElementoComparar_TextosInformacion)
                                                            {
                                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                                    if (!IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).Distinct().ToList().Count;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = item.Textos.Count;
                                                                    }
                                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion++;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    }

                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos));

                                                            if (item.Textos.Count > 0)
                                                                ListaTextos.Add(item.Textos.ToArray());

                                                            switch (TipoElementoComparar_TextosInformacion)
                                                            {
                                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                                    if (!IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion += item.Textos.Count;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = item.Textos.Count;
                                                                    }
                                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion++;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    }

                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;
                                                            }
                                                        }
                                                    }

                                                    if (CantidadTextosInformacion_PorElemento_Valores)
                                                        listaValoresCondicion.AddRange(ListaTextos);
                                                    else
                                                    {
                                                        if (Textos.Count > 0)
                                                            listaValoresCondicion.Add(Textos.ToArray());
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:

                                                    foreach (var item in (itemOperando).Numeros.Intersect(numerosOperandoValores).ToList())
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()));

                                                            if (item.Textos.Count > 0)
                                                                ListaTextos.Add(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().ToArray());

                                                            switch (TipoElementoComparar_TextosInformacion)
                                                            {
                                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                                    if (!IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = item.Textos.Count;
                                                                    }
                                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion++;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    }

                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos));

                                                            if (item.Textos.Count > 0)
                                                                ListaTextos.Add(item.Textos.ToArray());

                                                            switch (TipoElementoComparar_TextosInformacion)
                                                            {
                                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                                    if (!IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion += item.Textos.Count;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = item.Textos.Count;
                                                                    }
                                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                    {
                                                                        CantidadTextosValoresCondicion++;
                                                                        ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    }

                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;

                                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                    CantidadTextosValoresCondicion += 1;
                                                                    ////CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion++;
                                                                    break;
                                                            }
                                                        }
                                                    }

                                                    if (CantidadTextosInformacion_PorElemento_Valores)
                                                        listaValoresCondicion.AddRange(ListaTextos);
                                                    else
                                                    {
                                                        if (Textos.Count > 0)
                                                            listaValoresCondicion.Add(Textos.ToArray());
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                    if (itemOperando.PosicionActualNumero_CondicionesOperador_Implicacion <
                                                                (itemOperando).Numeros.Intersect(numerosOperandoValores).ToList().Count)
                                                    {
                                                        int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(itemOperando.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                            OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion,
                                                            (itemOperando).Numeros.Intersect(numerosOperandoValores).ToList().Count);

                                                        switch (TipoElementoComparar_TextosInformacion)
                                                        {
                                                            case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:

                                                                if (!IncluirSoloNombreElemento)
                                                                {
                                                                    Textos.AddRange(ejecucion.GenerarTextosInformacion((itemOperando).Numeros.Intersect(numerosOperandoValores).ToList()[indicePosicion].Textos));
                                                                }
                                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                {
                                                                    Textos.Add((itemOperando).Numeros.Intersect(numerosOperandoValores).ToList()[indicePosicion].Nombre);
                                                                }

                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                                Textos.Add((itemOperando).Numeros.Intersect(numerosOperandoValores).ToList()[indicePosicion].Numero.ToString());
                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:

                                                                int cantidad = 0;

                                                                if (!IncluirSoloNombreElemento)
                                                                    cantidad += (itemOperando).Numeros.Intersect(numerosOperandoValores).ToList()[indicePosicion].Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                    cantidad++;

                                                                Textos.Add(cantidad.ToString());

                                                                break;

                                                            case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                                Textos.Add("1");
                                                                break;
                                                        }

                                                    }

                                                    if (Textos.Count > 0)
                                                        listaValoresCondicion.Add(Textos.ToArray());

                                                    break;

                                            }
                                        }
                                    }

                                }
                                else
                                    valoresCondicion = new string[] { };
                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicion:
                                valoresCondicion = ElementoDefinicion_Valores.ObtenerTextos_CondicionDefinicion().ToArray();
                                
                                if (valoresCondicion.Length > 0) 
                                    listaValoresCondicion.Add(valoresCondicion.ToArray());

                                NumerosVinculados_CondicionAnterior.Clear();
                                SubOperandosVinculados_CondicionAnterior.Clear();
                                OperandosVinculados_CondicionAnterior.Clear();
                                ElementosVinculados_CondicionAnterior.Clear();

                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicionLista:

                                switch (OpcionSeleccionNumerosElemento_Condicion_Valores)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                        if (CantidadTextosInformacion_PorElemento_Valores)
                                        {
                                            var listas = ElementoDefinicionListas_Valores.ObtenerTextos_ListaDefinicion();
                                            foreach (var lista in listas)
                                            {
                                                if (lista.TextosInformacion.Count > 0)
                                                    listaValoresCondicion.Add(lista.TextosInformacion.ToArray());
                                            }
                                        }
                                        else
                                        {
                                            List<string> textos = new List<string>();

                                            var listas = ElementoDefinicionListas_Valores.ObtenerTextos_ListaDefinicion();
                                            foreach (var lista in listas)
                                                textos.AddRange(lista.TextosInformacion);

                                            valoresCondicion = textos.ToArray();
                                        }

                                        break;

                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                        if (ElementoDefinicionListas_Valores.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar <
                                        ElementoDefinicionListas_Valores.ListasCadenasTexto.Count)
                                        {
                                            int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(ElementoDefinicionListas_Valores.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar,
                                                OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion,
                                                ElementoDefinicionListas_Valores.ListasCadenasTexto.Count);

                                            valoresCondicion = ElementoDefinicionListas_Valores.ObtenerTextos_ListaDefinicion(indicePosicion).ToArray();
                                        }
                                        else
                                        {
                                            if (ReiniciarPosicion_AlFinalCantidadesOperando_Valores)
                                            {
                                                ElementoDefinicionListas_Valores.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                                                
                                            }
                                            else
                                            {
                                                if (!SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores)
                                                    if (CumpleCondicion_ElementoValores_SinNumeros)
                                                        return true;
                                                    else
                                                        return false;
                                            }
                                        }

                                        break;
                                }
                                
                                if (valoresCondicion.Length > 0)
                                    listaValoresCondicion.Add(valoresCondicion.ToArray());

                                NumerosVinculados_CondicionAnterior.Clear();
                                SubOperandosVinculados_CondicionAnterior.Clear();
                                OperandosVinculados_CondicionAnterior.Clear();
                                ElementosVinculados_CondicionAnterior.Clear();

                                break;
                        }

                        //if (listaValoresCondicion == null || !listaValoresCondicion.Any())
                        //    sinNumerosTextos_Valores = true;

                        if (valoresCondicion == null)
                            valoresCondicion = new string[1] { string.Empty };


                        var comparadorTextos = new ComparadorTextosInformacion(TipoOpcionCondicion_TextosInformacion,
                        ConsiderarTextosInformacionComoCantidades ? TipoOpcionCondicion_ElementoOperacionEntrada :
                        TipoOpcion_CondicionTextosInformacion_Implicacion.Ninguno,
                        Busqueda_TextoBusqueda_Ejecucion, BuscarCualquierTextoInformacion_TextoBusqueda,
                        QuitarEspaciosTemporalmente_CadenaCondicion);

                        ElementoOperacionAritmeticaEjecucion elementoEjecucion = null;

                        if ((OperandoCondicion != null && OperandoSubElemento_Condicion_TextosInformacion == null) ||
                            EsOperandoTextosActual)
                        {
                            elementoEjecucion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(OperandoCondicion);

                            if(EsOperandoTextosActual)
                            {
                                elementoEjecucion = operando;
                            }
                        }
                        else if ((OperandoCondicion != null && OperandoSubElemento_Condicion_TextosInformacion != null) ||
                            EsOperandoTextosActual)
                        {
                            elementoEjecucion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_TextosInformacion);

                            if(EsOperandoTextosActual &&
                                operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                            {
                                elementoEjecucion = operando;
                            }
                        }

                        if (elementoEjecucion != null)
                        {
                            {
                                List<EntidadNumero> listaCantidades = new List<EntidadNumero>();

                                if (elementoEjecucion == operacionCondicionEjecucion ||
                                    elementoEjecucion == (ElementoOperacionAritmeticaEjecucion)operacionInternaCondicionEjecucion)
                                {
                                    listaCantidades = NumerosResultado.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) &&
                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();
                                }
                                else
                                {
                                    listaCantidades = (elementoEjecucion).Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) &&
                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Where(i =>
                                        //(!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                        //(i.ElementosSalidaOperacion_Agrupamiento.Any() &&
                                        //i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion))) &
                                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionInternaCondicionEjecucion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionInternaCondicionEjecucion)))).ToList();
                                }

                                if (elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion >=
                                                listaCantidades.Count)
                                {
                                    if (ReiniciarPosicion_AlFinalCantidadesOperando)
                                    {
                                        elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
                                    }
                                    else
                                    {
                                        if (!SeguirAplicandoCondicion_AlFinalCantidadesOperando)
                                            if (CumpleCondicion_ElementoSinNumeros)
                                                return true;
                                            else
                                                return false;
                                    }
                                }

                                if (((listaCantidades.Contains(numero) &&
                                !(NumerosVinculados_CondicionAnterior.Contains(numero) && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y))))

                                {
                                    if (OperandoSubElemento_Condicion_TextosInformacion == null)
                                    {
                                        switch (OpcionSeleccionNumerosElemento_Condicion_TextosInformacion)
                                        {
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                elementos_CondicionEvaluar.Add(elementoEjecucion);
                                                //elementosAgregar_CondicionEvaluar.Add(elementoEjecucionCondicion_Valores);

                                                int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                OpcionSeleccionNumerosElemento_Condicion_TextosInformacion,
                                                listaCantidades.Count);

                                                numeros_CondicionEvaluar.Add(listaCantidades[indicePosicion]);

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                                elementos_CondicionEvaluar.Add(elementoEjecucion);
                                                //elementosAgregar_CondicionEvaluar.Add(elementoEjecucionCondicion_Valores);

                                                numeros_CondicionEvaluar.AddRange(listaCantidades);

                                                //numerosAgregar_CondicionEvaluar.AddRange(listaCantidades3);
                                                //elementosAgregar_CondicionEvaluar.AddRange(listaCantidades2);

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                                elementos_CondicionEvaluar.Add(elementoEjecucion);
                                                //elementosAgregar_CondicionEvaluar.Add(elementoEjecucionCondicion_Valores);

                                                numeros_CondicionEvaluar.AddRange(listaCantidades.Where(i => listaCantidades.IndexOf(i) <= elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion));
                                                break;
                                        }

                                    }
                                }
                                else if (((listaCantidades.Contains(numero) &&
                                    (NumerosVinculados_CondicionAnterior.Contains(numero) && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y))))
                                {
                                    switch (OpcionSeleccionNumerosElemento_Condicion_TextosInformacion)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                            elementos_CondicionEvaluar.Add(elementoEjecucion);

                                            int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                OpcionSeleccionNumerosElemento_Condicion_TextosInformacion,
                                                listaCantidades.Count);

                                            if (NumerosVinculados_CondicionAnterior.Contains(listaCantidades[indicePosicion]))
                                            {
                                                numeros_CondicionEvaluar.Add(listaCantidades[indicePosicion]);
                                                if (!IncluirSoloNombreElemento)
                                                {
                                                    if(CadenasTextoSon_Clasificadores)
                                                    {
                                                        CantidadTextosCondicion += listaCantidades[indicePosicion].Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                    }
                                                    else
                                                        CantidadTextosCondicion += listaCantidades[indicePosicion].Textos.Count;
                                                }
                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    CantidadTextosCondicion++;

                                                CantidadNumerosCondicion_TextosInformacion++;
                                            }


                                            if (NumerosVinculados_CondicionAnterior.Contains(numero))
                                            {
                                                numeros_CondicionEvaluar.Add(numero);
                                                if (!IncluirSoloNombreElemento)
                                                {
                                                    if(CadenasTextoSon_Clasificadores)
                                                    {
                                                        CantidadTextosCondicion += numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                    }
                                                    else
                                                        CantidadTextosCondicion += numero.Textos.Count;
                                                }
                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    CantidadTextosCondicion++;

                                                CantidadNumerosCondicion_TextosInformacion++;
                                            }

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                            elementos_CondicionEvaluar.Add(elementoEjecucion);
                                            //OperandosVinculados_AgregarCondicionAnterior.Add(elementoEjecucionCondicion_Valores);

                                            //OperandosVinculados_AgregarCondicionAnterior.AddRange(listaCantidades2);
                                            numeros_CondicionEvaluar.AddRange(listaCantidades);
                                            //NumerosVinculados_AgregarCondicionAnterior.AddRange(listaCantidades3);
                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                            elementos_CondicionEvaluar.Add(elementoEjecucion);
                                            //OperandosVinculados_AgregarCondicionAnterior.Add(elementoEjecucionCondicion_Valores);

                                            //OperandosVinculados_AgregarCondicionAnterior.AddRange(listaCantidades2);
                                            numeros_CondicionEvaluar.AddRange(listaCantidades.Where(i => listaCantidades.IndexOf(i) <= elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion));
                                            //NumerosVinculados_AgregarCondicionAnterior.AddRange(listaCantidades3);
                                            break;
                                    }
                                }
                                else if (OperandosVinculados_CondicionAnterior.Contains(elementoEjecucion)
                                    && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                                {
                                    if (OperandoSubElemento_Condicion_TextosInformacion == null)
                                    {
                                        switch (ObtenerOpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior(elementoEjecucion, null))
                                        {
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                elementos_CondicionEvaluar.Add(elementoEjecucion);

                                                int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                OpcionSeleccionNumerosElemento_Condicion_TextosInformacion,
                                                listaCantidades.Count);

                                                if (NumerosVinculados_CondicionAnterior.Contains(listaCantidades[indicePosicion]))
                                                {
                                                    numeros_CondicionEvaluar.Add(listaCantidades[indicePosicion]);

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if(CadenasTextoSon_Clasificadores)
                                                        {
                                                            CantidadTextosCondicion += listaCantidades[indicePosicion].Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                        }
                                                        else
                                                            CantidadTextosCondicion += listaCantidades[indicePosicion].Textos.Count;
                                                    }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                        CantidadTextosCondicion++;

                                                    CantidadNumerosCondicion_TextosInformacion++;
                                                }

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                                elementos_CondicionEvaluar.Add(elementoEjecucion);

                                                foreach (var item in listaCantidades)
                                                {
                                                    if (NumerosVinculados_CondicionAnterior.Contains(item))
                                                    {
                                                        numeros_CondicionEvaluar.Add(item);

                                                        CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());

                                                        if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                        {
                                                            CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                                            indiceCantidadesTextos_Valores++;
                                                        }

                                                        if (!IncluirSoloNombreElemento)
                                                        {
                                                            if (CadenasTextoSon_Clasificadores)
                                                            {
                                                                CantidadTextosCondicion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                                CantidadesTextos.LastOrDefault().CantidadTextosCondicion = item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                            }
                                                            else
                                                            {
                                                                CantidadTextosCondicion += item.Textos.Count;
                                                                CantidadesTextos.LastOrDefault().CantidadTextosCondicion = item.Textos.Count;
                                                            }
                                                        }
                                                        if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                        {
                                                            CantidadTextosCondicion++;
                                                            CantidadesTextos.LastOrDefault().CantidadTextosCondicion++;
                                                        }

                                                        CantidadNumerosCondicion_TextosInformacion++;
                                                    }
                                                }

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                                elementos_CondicionEvaluar.Add(elementoEjecucion);

                                                foreach (var item in listaCantidades.Where(i => listaCantidades.IndexOf(i) <= elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion))
                                                {
                                                    if (NumerosVinculados_CondicionAnterior.Contains(item))
                                                    {
                                                        numeros_CondicionEvaluar.Add(item);

                                                        CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());

                                                        if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                        {
                                                            CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                                            indiceCantidadesTextos_Valores++;
                                                        }

                                                        if (!IncluirSoloNombreElemento)
                                                        {
                                                            if (CadenasTextoSon_Clasificadores)
                                                            {
                                                                CantidadTextosCondicion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                                CantidadesTextos.LastOrDefault().CantidadTextosCondicion = item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                            }
                                                            else
                                                            {
                                                                CantidadTextosCondicion += item.Textos.Count;
                                                                CantidadesTextos.LastOrDefault().CantidadTextosCondicion = item.Textos.Count;
                                                            }
                                                        }
                                                        if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                        {
                                                            CantidadTextosCondicion++;
                                                            CantidadesTextos.LastOrDefault().CantidadTextosCondicion++;
                                                        }

                                                        CantidadNumerosCondicion_TextosInformacion++;
                                                    }
                                                }

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:
                                                elementos_CondicionEvaluar.Add(elementoEjecucion);

                                                foreach (var item in listaCantidades)
                                                {

                                                    numeros_CondicionEvaluar.Add(item);

                                                    CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());

                                                    if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                    {
                                                        CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                                        indiceCantidadesTextos_Valores++;
                                                    }

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores)
                                                        {
                                                            CantidadTextosCondicion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                            CantidadesTextos.LastOrDefault().CantidadTextosCondicion = item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                        }
                                                        else
                                                        {
                                                            CantidadTextosCondicion += item.Textos.Count;
                                                            CantidadesTextos.LastOrDefault().CantidadTextosCondicion = item.Textos.Count;
                                                        }
                                                    }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        CantidadTextosCondicion++;
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion++;
                                                    }

                                                    CantidadNumerosCondicion_TextosInformacion++;

                                                }

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                                elementos_CondicionEvaluar.Add(elementoEjecucion);

                                                foreach (var item in listaCantidades.Where(i => listaCantidades.IndexOf(i) <= elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion))
                                                {

                                                    numeros_CondicionEvaluar.Add(item);

                                                    CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());

                                                    if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                    {
                                                        CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                                        indiceCantidadesTextos_Valores++;
                                                    }

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores)
                                                        {
                                                            CantidadTextosCondicion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                            CantidadesTextos.LastOrDefault().CantidadTextosCondicion = item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                        }
                                                        else
                                                        {
                                                            CantidadTextosCondicion += item.Textos.Count;
                                                            CantidadesTextos.LastOrDefault().CantidadTextosCondicion = item.Textos.Count;
                                                        }
                                                    }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        CantidadTextosCondicion++;
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion++;
                                                    }

                                                    CantidadNumerosCondicion_TextosInformacion++;

                                                }

                                                break;

                                        }
                                    }
                                }
                                else
                                {

                                    elementos_CondicionEvaluar.Add(elementoEjecucion);
                                    numeros_CondicionEvaluar.AddRange(listaCantidades);
                                }
                            }

                        }

                        bool conNumerosTextos = false;

                        if (EntradaCondicion != null)
                        {
                            var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                            var elementosEntrada = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion &
                            asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                            bool sinNumerosTextosElemento = false;
                            conNumerosTextos = false;

                            foreach (var itemEntrada in elementosEntrada)
                            {
                                var elementoEjecucion_Textos = asignacion.ObtenerTextos_CondicionEntrada(itemEntrada.EntradaRelacionada);

                                OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                    new InfoOpcion_VinculadosAnterior()
                                    {
                                        OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion_TextosInformacion,
                                        EntradaRelacionada = itemEntrada.EntradaRelacionada
                                    });

                                switch (OpcionSeleccionNumerosElemento_Condicion_TextosInformacion)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                        //if (elementoEjecucion == elementoOperando)
                                        //{

                                        foreach (var itemOperando in elementoEjecucion_Textos)
                                        {
                                            foreach (var itemValoresCondicion in listaValoresCondicion)
                                            {
                                                valoresCondicion = itemValoresCondicion;

                                                List<string> TextosElemento = new List<string>();
                                                CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());

                                                if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                {
                                                    CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                                    indiceCantidadesTextos_Valores++;
                                                }

                                                if (!IncluirSoloNombreElemento)
                                                {
                                                    CantidadTextosCondicion += itemOperando.Count;
                                                    TextosElemento.AddRange(itemOperando);
                                                    CantidadesTextos.LastOrDefault().CantidadTextosCondicion = itemOperando.Count;
                                                }
                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                {
                                                    CantidadTextosCondicion++;
                                                    TextosElemento.Add(itemEntrada.Nombre);
                                                    CantidadesTextos.LastOrDefault().CantidadTextosCondicion++;
                                                }

                                                CantidadNumerosCondicion_TextosInformacion++;

                                                sinNumerosTextosElemento = false;

                                                if (CumpleCondicion_ElementoSinNumeros &&
                                                    !elementoEjecucion_Textos.Any(i => i != null))
                                                {
                                                    sinNumerosTextosElemento = true;
                                                }
                                                else
                                                {
                                                    conNumerosTextos = true;
                                                }

                                                if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto)
                                                {
                                                    if (((elementoEjecucion_Textos.Any(i => i != null) && !sinNumerosTextosElemento) || (!elementoEjecucion_Textos.Any(i => i != null) && (sinNumerosTextosElemento))) && !comparadorTextos.Interseccion(TextosElemento, valoresCondicion, itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores, true);
                                                        valorCondicion_Iteracion = true;

                                                        {
                                                            TextosInvolucrados.Clear();
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.Clear();
                                                        }

                                                        if (CantidadTextosInformacion_PorElemento |
                                                            CantidadTextosInformacion_PorElemento_Valores)
                                                        {
                                                            TextosInvolucrados.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                        }

                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                        }

                                                        if (CantidadTextosInformacion_PorElemento |
                                                    CantidadTextosInformacion_PorElemento_Valores)
                                                            TextosInvolucrados_ProcesamientoPorCantidad.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                        //asignacion.PosicionesTextos_CumplenCondicion.Add(PosicionActualNumero_CondicionesOperador_Implicacion);
                                                        //NumerosVinculados_CondicionAnterior.Add(itemOperando);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                            itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores,
                                        elementoEjecucionCondicion_Valores_ConjuntoEntrada,
                                        elementoEjecucionCondicion_Valores_Operacion,
                                        elementoEjecucionCondicion_Valores_SubOperacion, CantidadesTextos.LastOrDefault(),
                                                        valoresCondicion);

                                                        comparadorTextos.TextosInformacionInvolucrados.Clear();

                                                        NumerosCumplenCondicion_Elemento += 1;
                                                        NumerosCumplenCondicion_Valores += 1;

                                                        TextosCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_TextosInformacion = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_TextosInformacion = comparadorTextos.TextosCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;

                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }
                                                else
                                                {
                                                    if (comparadorTextos.Interseccion(TextosElemento, valoresCondicion, itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores, true);
                                                        valorCondicion_Iteracion = true;

                                                        {
                                                            TextosInvolucrados.Clear();
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.Clear();
                                                        }

                                                        if (CantidadTextosInformacion_PorElemento |
                                                            CantidadTextosInformacion_PorElemento_Valores)
                                                        {
                                                            TextosInvolucrados.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                        }

                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                        }

                                                        if (CantidadTextosInformacion_PorElemento |
                                                    CantidadTextosInformacion_PorElemento_Valores)
                                                            TextosInvolucrados_ProcesamientoPorCantidad.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                        //asignacion.PosicionesTextos_CumplenCondicion.Add(PosicionActualNumero_CondicionesOperador_Implicacion);
                                                        //NumerosVinculados_CondicionAnterior.Add(itemOperando);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                            itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores,
                                        elementoEjecucionCondicion_Valores_ConjuntoEntrada,
                                        elementoEjecucionCondicion_Valores_Operacion,
                                        elementoEjecucionCondicion_Valores_SubOperacion, CantidadesTextos.LastOrDefault(),
                                                        valoresCondicion);

                                                        comparadorTextos.TextosInformacionInvolucrados.Clear();

                                                        NumerosCumplenCondicion_Elemento += 1;
                                                        NumerosCumplenCondicion_Valores += 1;

                                                        TextosCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_TextosInformacion = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_TextosInformacion = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;


                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }
                                            }

                                            if (!conNumerosTextos)
                                            {
                                                sinNumerosTextos = true;
                                            }
                                        }

                                        break;

                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                        List<string> TextosInformacion_CumplenCondiciones_Anteriores = new List<string>();

                                        for (int PosicionActualNumero = 0; PosicionActualNumero <
                                            elementoEjecucion_Textos.Count; PosicionActualNumero++)
                                        {
                                            var textos = elementoEjecucion_Textos[PosicionActualNumero];

                                            if(!listaValoresCondicion.Any())
                                            {
                                                sinNumerosTextos_Valores = true;
                                            }

                                            conNumerosTextos = false;

                                            foreach (var itemValoresCondicion in listaValoresCondicion)
                                            {
                                                valoresCondicion = itemValoresCondicion;

                                                List<string> TextosElemento = new List<string>();
                                                CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());

                                                if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                {
                                                    CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                                    indiceCantidadesTextos_Valores++;
                                                }

                                                if (!IncluirSoloNombreElemento)
                                                {
                                                    CantidadTextosCondicion += textos.Count;
                                                    TextosElemento.AddRange(textos);
                                                    CantidadesTextos.LastOrDefault().CantidadTextosCondicion = textos.Count;
                                                }
                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                {
                                                    CantidadTextosCondicion++;
                                                    TextosElemento.Add(itemEntrada.Nombre);
                                                    CantidadesTextos.LastOrDefault().CantidadTextosCondicion++;
                                                }

                                                CantidadNumerosCondicion_TextosInformacion++;

                                                sinNumerosTextosElemento = false;

                                                if (CumpleCondicion_ElementoSinNumeros &&
                                                    !elementoEjecucion_Textos.Any(i => i != null))
                                                {
                                                    sinNumerosTextosElemento = true;
                                                }
                                                else
                                                {
                                                    conNumerosTextos = true;
                                                }

                                                List<string> TextosInformacion_CumplenCondiciones_Anteriores_Iteracion = itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores.ToList();

                                                if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto)
                                                {
                                                    if (((elementoEjecucion_Textos.Any(i => i != null) && !sinNumerosTextosElemento) || (!elementoEjecucion_Textos.Any(i => i != null) && (sinNumerosTextosElemento))) &&
                                                        !comparadorTextos.Interseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion, true);
                                                        valorCondicion_Iteracion = true;

                                                        {
                                                            TextosInvolucrados.Clear();
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.Clear();
                                                        }
                                                        
                                                        if (CantidadTextosInformacion_PorElemento |
                                                            CantidadTextosInformacion_PorElemento_Valores)
                                                        {
                                                            TextosInvolucrados.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                        }

                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                        }

                                                        if (CantidadTextosInformacion_PorElemento |
                                                    CantidadTextosInformacion_PorElemento_Valores)
                                                            TextosInvolucrados_ProcesamientoPorCantidad.AddRange(comparadorTextos.TextosInformacionInvolucrados);

                                                        //asignacion.PosicionesTextos_CumplenCondicion.Add(PosicionActualNumero_CondicionesOperador_Implicacion);
                                                        //NumerosVinculados_CondicionAnterior.Add(numero);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion,
                                        elementoEjecucionCondicion_Valores_ConjuntoEntrada,
                                        elementoEjecucionCondicion_Valores_Operacion,
                                        elementoEjecucionCondicion_Valores_SubOperacion, CantidadesTextos.LastOrDefault(),
                                                        valoresCondicion);

                                                        comparadorTextos.TextosInformacionInvolucrados.Clear();

                                                        NumerosCumplenCondicion_Elemento += 1;
                                                        NumerosCumplenCondicion_Valores += 1;

                                                        TextosCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion);
                                                        TextosCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_TextosInformacion = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_TextosInformacion = comparadorTextos.TextosCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion);
                                                        TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;


                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }
                                                else
                                                {
                                                    if (comparadorTextos.Interseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion, true);
                                                        valorCondicion_Iteracion = true;

                                                        {
                                                            TextosInvolucrados.Clear();
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.Clear();
                                                        }
                                                        
                                                        if (CantidadTextosInformacion_PorElemento |
                                                            CantidadTextosInformacion_PorElemento_Valores)
                                                        {
                                                            TextosInvolucrados.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                        }

                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                        }

                                                        if (CantidadTextosInformacion_PorElemento |
                                                    CantidadTextosInformacion_PorElemento_Valores)
                                                            TextosInvolucrados_ProcesamientoPorCantidad.AddRange(comparadorTextos.TextosInformacionInvolucrados);

                                                        //asignacion.PosicionesTextos_CumplenCondicion.Add(PosicionActualNumero_CondicionesOperador_Implicacion);
                                                        //NumerosVinculados_CondicionAnterior.Add(numero);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion,
                                        elementoEjecucionCondicion_Valores_ConjuntoEntrada,
                                        elementoEjecucionCondicion_Valores_Operacion,
                                        elementoEjecucionCondicion_Valores_SubOperacion, CantidadesTextos.LastOrDefault(),
                                                        valoresCondicion);

                                                        comparadorTextos.TextosInformacionInvolucrados.Clear();

                                                        NumerosCumplenCondicion_Elemento += 1;
                                                        NumerosCumplenCondicion_Valores += 1;

                                                        TextosCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion);
                                                        TextosCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_TextosInformacion = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_TextosInformacion = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion);
                                                        TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;


                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }

                                                comparadorTextos.TextosInformacionInvolucrados.Clear();
                                                TextosInformacion_CumplenCondiciones_Anteriores.AddRange(TextosInformacion_CumplenCondiciones_Anteriores.Except(TextosInformacion_CumplenCondiciones_Anteriores_Iteracion.ToList()));
                                            }
                                        }

                                        if (!conNumerosTextos)
                                        {
                                            sinNumerosTextos = true;
                                        }

                                        itemEntrada.TextosInformacion_CumplenCondiciones_Anteriores = TextosInformacion_CumplenCondiciones_Anteriores.ToList();

                                        break;
                                }

                            }
                        }

                        
                        OperandosVinculados_CondicionAnterior.Clear();
                        NumerosVinculados_CondicionAnterior.Clear();

                        

                        foreach (ElementoOperacionAritmeticaEjecucion elementoEjecucionItem in elementos_CondicionEvaluar)
                        {
                            OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                            new InfoOpcion_VinculadosAnterior()
                                            {
                                                OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion_TextosInformacion,
                                                OperandoRelacionado_Ejecucion = elementoEjecucionItem
                                            });

                            var listaNumeros = (elementoEjecucionItem).Numeros;

                            if (elementoEjecucionItem == operacionCondicionEjecucion)
                            {
                                listaNumeros = NumerosResultado;
                            }

                            if (!listaNumeros.Any() | !numeros_CondicionEvaluar.Any())
                                sinNumerosTextos = true;

                            if (!listaValoresCondicion.Any())
                                sinNumerosTextos_Valores = true;

                            bool sinNumerosTextosElemento = false;
                            

                            foreach (var itemOperando in listaNumeros.Intersect(numeros_CondicionEvaluar).ToList())
                            {
                                int indiceValores = 0;

                                foreach (var itemValoresCondicion in listaValoresCondicion)
                                {
                                    valoresCondicion = itemValoresCondicion;

                                    EntidadNumero numeroValoresActual = null;

                                    if (numerosOperandoValores.Any())
                                        numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                    List<string> TextosElemento = new List<string>();

                                    CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionImplicacion());

                                    if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                    {
                                        CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                        indiceCantidadesTextos_Valores++;
                                    }

                                    if (CantidadTextosInformacion_SoloCadenasCumplen)
                                    {
                                        CantidadTextosCondicion += itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.Count;
                                        TextosElemento.AddRange(itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion = itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.Count;
                                    }
                                    else
                                    {
                                        if (!IncluirSoloNombreElemento)
                                        {
                                            if (CadenasTextoSon_Clasificadores)
                                            {
                                                CantidadTextosCondicion += itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                TextosElemento.AddRange(itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList());
                                                CantidadesTextos.LastOrDefault().CantidadTextosCondicion = itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                            }
                                            else
                                            {
                                                CantidadTextosCondicion += itemOperando.Textos.Count;
                                                TextosElemento.AddRange(itemOperando.Textos);
                                                CantidadesTextos.LastOrDefault().CantidadTextosCondicion = itemOperando.Textos.Count;
                                            }
                                        }
                                        if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                        {
                                            CantidadTextosCondicion++;
                                            TextosElemento.Add(itemOperando.Nombre);
                                            CantidadesTextos.LastOrDefault().CantidadTextosCondicion++;
                                        }
                                    }

                                    CantidadNumerosCondicion_TextosInformacion++;
                                    TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada = itemOperando.TextosInformacion_CumplenCondiciones_Anteriores;

                                    sinNumerosTextosElemento = false;

                                    if (CumpleCondicion_ElementoSinNumeros &&
                                                    !TextosElemento.Any(i => i != null))
                                    {
                                        sinNumerosTextosElemento = true;
                                    }
                                    else
                                    {
                                        conNumerosTextos = true;
                                    }

                                    if (numeroValoresActual != null)
                                        TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores = numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores;

                                    if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto)
                                    {
                                        if (((TextosElemento.Any(i => i != null) && !sinNumerosTextosElemento) || (!TextosElemento.Any(i => i != null) && (sinNumerosTextosElemento))) && !comparadorTextos.Interseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada))
                                        {
                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada, true);
                                            valorCondicion_Iteracion = true;

                                            {
                                                TextosInvolucrados.Clear();
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.Clear();
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.Clear();
                                            }
                                            
                                            if (CantidadTextosInformacion_PorElemento |
                                        CantidadTextosInformacion_PorElemento_Valores)
                                            {
                                                TextosInvolucrados.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                            }

                                            if (!(CantidadTextosInformacion_SoloCadenasCumplen |
CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                            {
                                                TextosInvolucrados.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                            }

                                            if (CantidadTextosInformacion_PorElemento |
                                        CantidadTextosInformacion_PorElemento_Valores)
                                                TextosInvolucrados_ProcesamientoPorCantidad.AddRange(comparadorTextos.TextosInformacionInvolucrados);

                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(itemOperando);

                                            NumerosVinculados_CondicionAnterior.AddRange(numeros_CondicionEvaluar.Where(numero =>
                                                (comparadorTextos.TextosInformacionInvolucrados.Contains(numero.Numero.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Contains((CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Count.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Any(i => (CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Contains(i))) &&
                                                !NumerosVinculados_CondicionAnterior.Contains(numero)
                                                ));

                                            NumerosVinculados_CondicionAnterior_Temp.AddRange(numeros_CondicionEvaluar.Where(numero =>
                                                (comparadorTextos.TextosInformacionInvolucrados.Contains(numero.Numero.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Contains((CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Count.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Any(i => (CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Contains(i))) &&
                                                !NumerosVinculados_CondicionAnterior.Contains(numero)
                                                ));

                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.AddRange(numeros_CondicionEvaluar.Where(numero =>
                                                (comparadorTextos.TextosInformacionInvolucrados.Contains(numero.Numero.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Contains((CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Count.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Any(i => (CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Contains(i))) &&
                                                !NumerosVinculados_CondicionAnterior.Contains(numero)
                                                ));

                                            if (!NumerosVinculados_CondicionAnterior.Contains(numeroValoresActual))
                                            {
                                                NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                            }
                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);

                                            if (!OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionItem))
                                            {
                                                OperandosVinculados_CondicionAnterior.Add(elementoEjecucionItem);
                                                OperandosVinculados_CondicionAnterior_Temp.Add(elementoEjecucionItem);
                                            }

                                            AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada,
                                                elementoEjecucionCondicion_Valores_ConjuntoEntrada,
                            elementoEjecucionCondicion_Valores_Operacion,
                            elementoEjecucionCondicion_Valores_SubOperacion,
                            CantidadesTextos.LastOrDefault(),
                                    valoresCondicion);

                                            comparadorTextos.TextosInformacionInvolucrados.Clear();

                                            NumerosCumplenCondicion_Elemento += 1;
                                            NumerosCumplenCondicion_Valores += 1;

                                            TextosCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;
                                            TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                            if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                            {
                                                TextosInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                            }

                                            CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;
                                            CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;

                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada);
                                            TextosCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;
                                            TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;

                                            if (CantidadTextosInformacion_SoloCadenasCumplen)
                                            {
                                                TextosInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                            }

                                            CantidadesTextos.LastOrDefault().TextosCumplenCondicion_TextosInformacion = comparadorTextos.TextosNoCumplenCondicion.Count;
                                            CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_TextosInformacion = comparadorTextos.TextosCumplenCondicion.Count;
                                        }
                                        else
                                        {
                                            NumerosNoCumplenCondicion_Elemento += 1;
                                            NumerosNoCumplenCondicion_Valores += 1;

                                            TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada);
                                            TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;

                                            CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                        }
                                    }
                                    else
                                    {
                                        if (comparadorTextos.Interseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada))
                                        {
                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada, true);
                                            valorCondicion_Iteracion = true;
                                                                                        
                                            {
                                                TextosInvolucrados.Clear();
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.Clear();
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.Clear();
                                            }
                                            
                                            if (CantidadTextosInformacion_PorElemento |
                                        CantidadTextosInformacion_PorElemento_Valores)
                                            {
                                                TextosInvolucrados.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.AddRange(TextosInvolucrados_ProcesamientoPorCantidad);
                                            }

                                            if (!(CantidadTextosInformacion_SoloCadenasCumplen |
CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                            {
                                                TextosInvolucrados.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.AddRange(comparadorTextos.TextosInformacionInvolucrados);
                                            }

                                            if (CantidadTextosInformacion_PorElemento |
                                        CantidadTextosInformacion_PorElemento_Valores)
                                                TextosInvolucrados_ProcesamientoPorCantidad.AddRange(comparadorTextos.TextosInformacionInvolucrados);

                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(itemOperando);

                                            if (!NumerosVinculados_CondicionAnterior.Contains(itemOperando))
                                            {
                                                NumerosVinculados_CondicionAnterior.Add(itemOperando);
                                                NumerosVinculados_CondicionAnterior_Temp.Add(itemOperando);
                                            }

                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.AddRange(numeros_CondicionEvaluar.Where(numero =>
                                                (comparadorTextos.TextosInformacionInvolucrados.Contains(numero.Numero.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Contains((CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Count.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Any(i => (CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Contains(i))) &&
                                                !NumerosVinculados_CondicionAnterior.Contains(numero)
                                                ));

                                            NumerosVinculados_CondicionAnterior.AddRange(numeros_CondicionEvaluar.Where(numero =>
                                                (comparadorTextos.TextosInformacionInvolucrados.Contains(numero.Numero.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Contains((CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Count.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Any(i => (CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Contains(i))) &&
                                                !NumerosVinculados_CondicionAnterior.Contains(numero)
                                                ));

                                            NumerosVinculados_CondicionAnterior_Temp.AddRange(numeros_CondicionEvaluar.Where(numero =>
                                                (comparadorTextos.TextosInformacionInvolucrados.Contains(numero.Numero.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Contains((CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Count.ToString()) ||
                                                comparadorTextos.TextosInformacionInvolucrados.Any(i => (CadenasTextoSon_Clasificadores ? numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList() : numero.Textos).Contains(i))) &&
                                                !NumerosVinculados_CondicionAnterior.Contains(numero)
                                                ));

                                            if (!NumerosVinculados_CondicionAnterior.Contains(numeroValoresActual))
                                            {
                                                NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                            }

                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);

                                            if (!OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionItem))
                                            {
                                                OperandosVinculados_CondicionAnterior.Add(elementoEjecucionItem);
                                                OperandosVinculados_CondicionAnterior_Temp.Add(elementoEjecucionItem);
                                            }

                                            AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada,
                                                elementoEjecucionCondicion_Valores_ConjuntoEntrada,
                            elementoEjecucionCondicion_Valores_Operacion,
                            elementoEjecucionCondicion_Valores_SubOperacion,
                            CantidadesTextos.LastOrDefault(),
                                    valoresCondicion);

                                            comparadorTextos.TextosInformacionInvolucrados.Clear();

                                            NumerosCumplenCondicion_Elemento += 1;
                                            NumerosCumplenCondicion_Valores += 1;

                                            TextosCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;
                                            TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                            if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                            {
                                                TextosInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                            }

                                            CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;
                                            CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;

                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada);
                                            TextosCumplenCondicion_TextosInformacion += comparadorTextos.TextosCumplenCondicion.Count;
                                            TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;

                                            if (CantidadTextosInformacion_SoloCadenasCumplen)
                                            {
                                                TextosInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada_Valores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                            }

                                            CantidadesTextos.LastOrDefault().TextosCumplenCondicion_TextosInformacion = comparadorTextos.TextosCumplenCondicion.Count;
                                            CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_TextosInformacion = comparadorTextos.TextosNoCumplenCondicion.Count;
                                        }
                                        else
                                        {
                                            NumerosNoCumplenCondicion_Elemento += 1;
                                            NumerosNoCumplenCondicion_Valores += 1;

                                            TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada);
                                            TextosNoCumplenCondicion_TextosInformacion += comparadorTextos.TextosNoCumplenCondicion.Count;

                                            CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                        }
                                    }

                                    var dupla = duplasTextosInformacion_Elementos.FirstOrDefault(item => item.TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_ElementoCondicion == itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);

                                    if (dupla != null)
                                    {
                                        dupla.TextosInformacion_CumplenCondiciones_Anteriores_Entrada.AddRange(TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada);
                                    }
                                    else
                                        duplasTextosInformacion_Elementos.Add(new DuplaTextosInformacion_ProcesamientoCondicionImplicacion(TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_Entrada, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores));

                                    indiceValores++;
                                }


                            }

                        }

                        if (!conNumerosTextos)
                        {
                            sinNumerosTextos = true;
                        }

                        break;

                    case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada:

                        elementos_CondicionEvaluar = new List<ElementoEjecucionCalculo>();
                        numeros_CondicionEvaluar = new List<EntidadNumero>();

                        bool CantidadesValores_ConPosiciones_ConNumerosVinculados = false;

                        switch (TipoElemento_Valores)
                        {
                            case TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos:
                                valoresCondicion = Valores_Condicion.Split('|');
                                listaValoresCondicion.Add(valoresCondicion);

                                if (OpcionValorPosicion == TipoOpcionPosicion.Ninguna)
                                {
                                    NumerosVinculados_CondicionAnterior.Clear();
                                    SubOperandosVinculados_CondicionAnterior.Clear();
                                    OperandosVinculados_CondicionAnterior.Clear();
                                }
                                else
                                {
                                    if(NumerosVinculados_CondicionAnterior.Any())
                                        CantidadesValores_ConPosiciones_ConNumerosVinculados = true;
                                }

                            break;

                            case TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.Valores_DesdeElementoOperacion:

                                if (ElementoOperacion_Valores_ElementoAsociado != null ||
                                    EsOperandoValoresActual)
                                {
                                    var elementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores_ElementoAsociado);
                                    var subElementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_Elemento);

                                    if(EsOperandoValoresActual)
                                    {
                                        elementoEjecucionCondicion = operando;

                                        if(operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                            subElementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)operando;
                                    }

                                    if (elementoEjecucionCondicion != null &&
                                        subElementoEjecucionCondicion != null)
                                    {
                                        elementoEjecucionCondicion = subElementoEjecucionCondicion;
                                    }

                                    {

                                        if (elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_Implicacion >=
                                                            (elementoEjecucionCondicion).Numeros.Where(i =>
                                                            (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                            ((ConsiderarValores_ProcesamientoCantidades_Valores &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades_Valores &&
                            !ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Count)
                                        {
                                            if (ReiniciarPosicion_AlFinalCantidadesOperando_Valores)
                                            {
                                                elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
                                                
                                            }
                                            else
                                            {
                                                if (!SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores)
                                                    if (CumpleCondicion_ElementoValores_SinNumeros)
                                                        return true;
                                                    else
                                                        return false;
                                            }
                                        }

                                        List<EntidadNumero> listaCantidades = new List<EntidadNumero>();

                                        if (elementoEjecucionCondicion != operando)
                                        {
                                            listaCantidades = (elementoEjecucionCondicion).Numeros.Where(i =>
                                            (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))) &&
                                            ((ConsiderarValores_ProcesamientoCantidades_Valores &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades_Valores &&
                            !ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();
                                        }
                                        else
                                        {
                                            listaCantidades = (elementoEjecucionCondicion).Numeros.Where(i =>
                                            (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && 
                                                i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) || 
                                                !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))) &&
                                            (!ConsiderarSoloValores_ProcesamientoCantidades_Valores ||
                                                                (ConsiderarSoloValores_ProcesamientoCantidades_Valores && i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Where(i =>
                                                //(!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                                //(i.ElementosSalidaOperacion_Agrupamiento.Any() &&
                                                //i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion))) &
                                                (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                                (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionInternaCondicionEjecucion))) &
                                                (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                                (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionInternaCondicionEjecucion)))).ToList();
                                        }

                                        if (elementoEjecucionCondicion.GetType() == typeof(ElementoEntradaEjecucion))
                                            elementoEjecucionCondicion_Valores_ConjuntoEntrada = (ElementoEntradaEjecucion)elementoEjecucionCondicion;
                                        else if ((elementoEjecucionCondicion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion)))
                                            elementoEjecucionCondicion_Valores_SubOperacion = (ElementoDiseñoOperacionAritmeticaEjecucion)elementoEjecucionCondicion;
                                        else if (elementoEjecucionCondicion.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                                            elementoEjecucionCondicion_Valores_Operacion = (ElementoOperacionAritmeticaEjecucion)elementoEjecucionCondicion;

                                        if (listaCantidades.Contains(numero))

                                        {
                                            switch (OpcionSeleccionNumerosElemento_Condicion_Valores)
                                            {
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    numerosOperandoValores.Add(numero);

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    numerosOperandoValores.AddRange(listaCantidades);
                                                    //numerosAgregar_CondicionEvaluar.AddRange(listaCantidades3);
                                                    //elementosAgregar_CondicionEvaluar.AddRange(listaCantidades2);

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    numerosOperandoValores.AddRange(listaCantidades);
                                                    break;
                                            }

                                        }
                                        else if (OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion)
                                            && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                                        {
                                            switch (ObtenerOpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior(elementoEjecucionCondicion, null))
                                            {
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    if (elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_Implicacion <
                                                        listaCantidades.Count)
                                                    {
                                                        int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                            OpcionSeleccionNumerosElemento_Condicion_Valores,
                                                            listaCantidades.Count);

                                                        if (NumerosVinculados_CondicionAnterior.Contains(listaCantidades[indicePosicion]))
                                                        {
                                                            CantidadNumerosValoresCondicion++;

                                                            numerosOperandoValores.Add(listaCantidades[indicePosicion]);
                                                        }
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    foreach (var item in listaCantidades)
                                                    {
                                                        if (NumerosVinculados_CondicionAnterior.Contains(item))
                                                        {

                                                            CantidadNumerosValoresCondicion++;

                                                            numerosOperandoValores.Add(item);
                                                        }
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    foreach (var item in listaCantidades
                                                        .Where(i => listaCantidades.IndexOf(i) <= elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_Implicacion))
                                                    {
                                                        if (NumerosVinculados_CondicionAnterior.Contains(item))
                                                        {
                                                            CantidadNumerosValoresCondicion++;

                                                            numerosOperandoValores.Add(item);
                                                        }
                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:
                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    foreach (var item in listaCantidades)
                                                    {

                                                        CantidadNumerosValoresCondicion++;

                                                        numerosOperandoValores.Add(item);

                                                    }

                                                    break;

                                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    foreach (var item in listaCantidades
                                                        .Where(i => listaCantidades.IndexOf(i) <= elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_Implicacion))
                                                    {

                                                        CantidadNumerosValoresCondicion++;

                                                        numerosOperandoValores.Add(item);

                                                    }

                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            operandosValores.Add(elementoEjecucionCondicion);

                                            numerosOperandoValores.AddRange(elementoEjecucionCondicion.Numeros);
                                        }
                                    }

                                    
                                    NumerosVinculados_CondicionAnterior.Clear();
                                    OperandosVinculados_CondicionAnterior.Clear();

                                    List<string> Numeros = new List<string>();

                                    foreach (ElementoOperacionAritmeticaEjecucion elementoEjecucionCondicionItem in operandosValores)
                                    {

                                        OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                            new InfoOpcion_VinculadosAnterior()
                                            {
                                                OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion_Valores,
                                                OperandoRelacionado_Ejecucion = elementoEjecucionCondicionItem
                                            });

                                        foreach (var item in (elementoEjecucionCondicionItem).Numeros.Intersect(numerosOperandoValores).ToList())
                                        {
                                            Numeros.Add(item.Numero.ToString());

                                        }

                                    }

                                    valoresCondicion = Numeros.ToArray();
                                    CantidadNumerosValoresCondicion += Numeros.Count;
                                    listaValoresCondicion.Add(valoresCondicion);
                                }
                                else
                                    valoresCondicion = new string[] { };

                                break;
                        }

                        //if (listaValoresCondicion == null || !listaValoresCondicion.Any())
                        //    sinNumerosTextos_Valores = true;

                        if (valoresCondicion == null)
                            valoresCondicion = new string[1] { string.Empty };

                        var elementoEncontrado = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(ElementoCondicion);
                        var subElementoEncontrado = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion);

                        if(EsOperandoActual)
                        {
                            elementoEncontrado = operando;

                            if(operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                subElementoEncontrado = (ElementoOperacionAritmeticaEjecucion)operando;
                        }

                        if (elementoEncontrado != null)
                        {
                            int cantidadNumeros = 0;

                            List<EntidadNumero> numerosElemento = new List<EntidadNumero>();


                            if (OperandoSubElemento_Condicion != null)
                            {
                                elementoEncontrado = subElementoEncontrado;
                            }

                            //if (elementoEncontrado.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                            {
                                if (((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).PosicionActualNumero_CondicionesOperador_Implicacion >=
                                                    ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros.Where(i =>
                                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && 
                                            i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) || 
                                            !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto))) &&
                                                    ((ConsiderarValores_ProcesamientoCantidades_Valores &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades_Valores &&
                            !ConsiderarSoloValores_ProcesamientoCantidades_Valores &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Count)
                                {
                                    if (ReiniciarPosicion_AlFinalCantidadesOperando)
                                    {
                                        ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).PosicionActualNumero_CondicionesOperador_Implicacion = 0;
                                        
                                    }
                                    else
                                    {
                                        if (!SeguirAplicandoCondicion_AlFinalCantidadesOperando &&
                                            (elementoEncontrado.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                                                ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros.Where(i =>
                                                (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                                (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto))) &&
                                                ((ConsiderarValores_ProcesamientoCantidades &&
                                (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                                i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                                (ConsiderarSoloValores_ProcesamientoCantidades &&
                                i.EsCantidadInsertada_ProcesamientoCantidades) ||

                                (!ConsiderarValores_ProcesamientoCantidades &&
                                !ConsiderarSoloValores_ProcesamientoCantidades &&
                                !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Contains(numero)))
                                            if (CumpleCondicion_ElementoSinNumeros)
                                                return true;
                                            else
                                                return false;
                                    }
                                }

                                List<EntidadNumero> listaCantidades = new List<EntidadNumero>();
                                List<EntidadNumero> listaCantidades_Numeros = new List<EntidadNumero>();

                                bool otroOperando = false;
                                bool numeroEnLista = false;

                                if (CantidadesValores_ConPosiciones_ConNumerosVinculados)
                                {
                                    listaCantidades.AddRange(NumerosVinculados_CondicionAnterior);
                                    listaCantidades_Numeros.AddRange(listaCantidades);

                                    if (listaCantidades.Contains(numero))
                                        numeroEnLista = true;

                                    NumerosVinculados_CondicionAnterior.Clear();
                                    SubOperandosVinculados_CondicionAnterior.Clear();
                                    OperandosVinculados_CondicionAnterior.Clear();
                                }
                                else
                                {
                                    if (elementoEncontrado == operacionCondicionEjecucion ||
                                        elementoEncontrado == (ElementoOperacionAritmeticaEjecucion)operacionInternaCondicionEjecucion)
                                    {
                                        if (elementoEncontrado.Clasificadores_Cantidades.Any())
                                        {
                                            listaCantidades = NumerosResultado.Where(i =>
                                            (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                                (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto))) &&
                                            ((ConsiderarValores_ProcesamientoCantidades &&
                                (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                                i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                                (ConsiderarSoloValores_ProcesamientoCantidades &&
                                i.EsCantidadInsertada_ProcesamientoCantidades) ||

                                (!ConsiderarValores_ProcesamientoCantidades &&
                                !ConsiderarSoloValores_ProcesamientoCantidades &&
                                !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();
                                        }
                                        else
                                        {
                                            listaCantidades = NumerosResultado.Where(i =>
                                        ((ConsiderarValores_ProcesamientoCantidades &&
                                (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                                i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                                (ConsiderarSoloValores_ProcesamientoCantidades &&
                                i.EsCantidadInsertada_ProcesamientoCantidades) ||

                                (!ConsiderarValores_ProcesamientoCantidades &&
                                !ConsiderarSoloValores_ProcesamientoCantidades &&
                                !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();
                                        }

                                        listaCantidades_Numeros.AddRange(listaCantidades);

                                        if (listaCantidades.Contains(numero))
                                        {
                                            numeroEnLista = true;
                                        }

                                        otroOperando = true;
                                    }
                                    else
                                    {
                                        listaCantidades = elementoEncontrado.Numeros.Where(i =>
                                        ((ConsiderarValores_ProcesamientoCantidades &&
                                (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                                i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                                (ConsiderarSoloValores_ProcesamientoCantidades &&
                                i.EsCantidadInsertada_ProcesamientoCantidades) ||

                                (!ConsiderarValores_ProcesamientoCantidades &&
                                !ConsiderarSoloValores_ProcesamientoCantidades &&
                                !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();

                                        listaCantidades_Numeros.AddRange(listaCantidades);

                                        if (listaCantidades.Contains(numero))
                                            numeroEnLista = true;
                                    }
                                }

                                switch (TipoSubElemento_Condicion)
                                {
                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                        listaCantidades = listaCantidades
                                            .Select(i => new EntidadNumero() { Numero = listaCantidades.IndexOf(i) + 1, 
                                                Textos = new List<string>() { i.ObtenerPosicionTexto(listaCantidades, OpcionValorPosicion) }
                                            }).ToList();
                                        break;

                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                        listaCantidades = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = listaCantidades.Count
                                                    }};
                                        break;

                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                        listaCantidades = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }};
                                        break;

                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                        listaCantidades = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }};
                                        break;

                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                        listaCantidades = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }};
                                        break;

                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                        listaCantidades = listaCantidades
                                            .Select(i => new EntidadNumero()
                                            {
                                                Numero = i.Textos.Count,
                                            }).ToList();
                                        break;

                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                        listaCantidades = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(elementoEncontrado)
                                                    }};
                                        break;
                                }

                                if (OpcionValorPosicion != TipoOpcionPosicion.Ninguna)
                                {
                                    //List<string> PosicionesAgregadas = new List<string>();
                                    //for (int posicion = 1; posicion <= listaCantidades.LongCount(); posicion++)
                                    //{
                                    //    PosicionesAgregadas.Add(posicion.ToString());
                                    //}

                                    valoresCondicion = listaCantidades.Select(i => { var indice = listaCantidades.IndexOf(i);
                                        return new string(listaCantidades[indice].Numero.ToString() + "|" + i.ObtenerPosicionTexto(listaCantidades, OpcionValorPosicion)); }).ToArray();
                                }

                                if (numeroEnLista)
                                {
                                    switch (OpcionSeleccionNumerosElemento_Condicion)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                            operandosCondicion.Add(elementoEncontrado);

                                            switch (TipoSubElemento_Condicion)
                                            {
                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento:
                                                    numerosOperandoCondicion.Add(numero);
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                                    numerosOperandoCondicion.Add(new EntidadNumero() { Numero = listaCantidades_Numeros.IndexOf(numero) + 1,
                                                        Textos = new List<string>() { numero.ObtenerPosicionTexto(listaCantidades_Numeros, OpcionValorPosicion) }
                                                    }); 
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = listaCantidades_Numeros.Count
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = numero.Textos.Count
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(elementoEncontrado)
                                                    }};
                                                    break;
                                            }
                                            

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                            operandosCondicion.Add(elementoEncontrado);
                                            switch (TipoSubElemento_Condicion)
                                            {
                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento:
                                                    numerosOperandoCondicion.Add(numero);
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                                    numerosOperandoCondicion.Add(new EntidadNumero() { Numero = listaCantidades_Numeros.IndexOf(numero) + 1,
                                                        Textos = new List<string>() { numero.ObtenerPosicionTexto(listaCantidades_Numeros, OpcionValorPosicion) }
                                                    });
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = 1
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = numero.Textos.Count
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                    numerosOperandoCondicion = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(elementoEncontrado)
                                                    }};
                                                    break;
                                            }

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                            operandosCondicion.Add(elementoEncontrado);

                                            numerosOperandoCondicion.AddRange(listaCantidades
                                                .Where(i => listaCantidades.IndexOf(i) <= elementoEncontrado.PosicionActualNumero_CondicionesOperador_Implicacion));
                                            break;
                                    }
                                }
                                else if (OperandosVinculados_CondicionAnterior.Contains(elementoEncontrado) //|| 
                                                                                                            //OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion_Valores))
                        && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                                {
                                    switch (ObtenerOpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior(elementoEncontrado, null))
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                            operandosCondicion.Add(elementoEncontrado);

                                            if (elementoEncontrado.PosicionActualNumero_CondicionesOperador_Implicacion <
                                                listaCantidades.Count)
                                            {
                                                int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEncontrado.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                    OpcionSeleccionNumerosElemento_Condicion,
                                                    listaCantidades.Count);

                                                if (NumerosVinculados_CondicionAnterior.Contains(listaCantidades[indicePosicion]))
                                                {

                                                    CantidadNumerosCondicion_OperacionEntrada++;
                                                    cantidadNumeros++;

                                                    numerosOperandoCondicion.Add(listaCantidades[indicePosicion]);
                                                }
                                            }

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            operandosCondicion.Add(elementoEncontrado);

                                            var listaNumeros_Encontrado = ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros.Where(i =>
                                            (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && 
                                            i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) || 
                                            !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto))) &&
                                            ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();

                                                switch (TipoSubElemento_Condicion)
                                            {
                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                                    listaNumeros_Encontrado = listaNumeros_Encontrado.Select(i => new EntidadNumero() { Numero = listaNumeros_Encontrado.IndexOf(i) + 1,
                                                        Textos = new List<string>() { i.ObtenerPosicionTexto(listaNumeros_Encontrado, OpcionValorPosicion) }
                                                    }).ToList();
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                    listaNumeros_Encontrado = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = listaNumeros_Encontrado.Count
                                                    }} ;
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                    listaNumeros_Encontrado = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                    listaNumeros_Encontrado = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                    listaNumeros_Encontrado = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                    listaNumeros_Encontrado = listaNumeros_Encontrado.Select(i => new EntidadNumero()
                                                    {
                                                        Numero = i.Textos.Count,
                                                    }).ToList();
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                    listaNumeros_Encontrado = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(elementoEncontrado)
                                                    }};
                                                    break;
                                            }

                                            foreach (var item in listaNumeros_Encontrado)
                                            {
                                                if (NumerosVinculados_CondicionAnterior.Contains(item))
                                                {

                                                    CantidadNumerosCondicion_OperacionEntrada++;
                                                    cantidadNumeros++;

                                                    numerosOperandoCondicion.Add(item);
                                                }
                                            }

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                            operandosCondicion.Add(elementoEncontrado);

                                            var listaNumeros_Encontrado2 = ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && 
                                            i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) || 
                                            !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))).Where(i =>
                                            ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                             i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && 
                                            i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) || 
                                            !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= elementoEncontrado.PosicionActualNumero_CondicionesOperador_Implicacion &&
                                            ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();

                                            switch (TipoSubElemento_Condicion)
                                            {
                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                                    listaNumeros_Encontrado2 = listaNumeros_Encontrado2.Select(i => new EntidadNumero() { Numero = listaNumeros_Encontrado2.IndexOf(i) + 1,
                                                        Textos = new List<string>() { i.ObtenerPosicionTexto(listaNumeros_Encontrado2, OpcionValorPosicion) }
                                                    }).ToList();
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                    listaNumeros_Encontrado2 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = listaNumeros_Encontrado2.Count
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                    listaNumeros_Encontrado2 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                    listaNumeros_Encontrado2 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                    listaNumeros_Encontrado2 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                    listaNumeros_Encontrado2 = listaNumeros_Encontrado2.Select(i => new EntidadNumero()
                                                    {
                                                        Numero = i.Textos.Count
                                                    }).ToList();
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                    listaNumeros_Encontrado2 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(elementoEncontrado)
                                                    }};
                                                    break;
                                            }

                                            foreach (var item in listaNumeros_Encontrado2)
                                            {
                                                if (NumerosVinculados_CondicionAnterior.Contains(item))
                                                {

                                                    CantidadNumerosCondicion_OperacionEntrada++;
                                                    cantidadNumeros++;

                                                    numerosOperandoCondicion.Add(item);
                                                }
                                            }

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                            operandosCondicion.Add(elementoEncontrado);

                                            var listaNumeros_Encontrado3 = ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros.Where(i =>
                                            (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && 
                                            i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) || 
                                            !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto))) &&
                                            ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();

                                            switch (TipoSubElemento_Condicion)
                                            {
                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                                    listaNumeros_Encontrado3 = listaNumeros_Encontrado3.Select(i => new EntidadNumero() { Numero = listaNumeros_Encontrado3.IndexOf(i) + 1,
                                                        Textos = new List<string>() { i.ObtenerPosicionTexto(listaNumeros_Encontrado3, OpcionValorPosicion) }
                                                    }).ToList();
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                    listaNumeros_Encontrado3 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = listaNumeros_Encontrado3.Count
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                    listaNumeros_Encontrado3 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                    listaNumeros_Encontrado3 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                    listaNumeros_Encontrado3 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                    listaNumeros_Encontrado3 = listaNumeros_Encontrado3.Select(i => new EntidadNumero()
                                                    {
                                                        Numero = i.Textos.Count
                                                    }).ToList();
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                    listaNumeros_Encontrado3 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(elementoEncontrado)
                                                    }};
                                                    break;
                                            }

                                            foreach (var item in listaNumeros_Encontrado3)
                                            {


                                                CantidadNumerosCondicion_OperacionEntrada++;
                                                cantidadNumeros++;

                                                numerosOperandoCondicion.Add(item);

                                            }

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                            operandosCondicion.Add(elementoEncontrado);

                                            var listaNumeros_Encontrado4 = ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && 
                                            i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) || 
                                            !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))).Where(i =>
                                            ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && 
                                            i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) || 
                                            !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= elementoEncontrado.PosicionActualNumero_CondicionesOperador_Implicacion &&
                                            ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();

                                            switch (TipoSubElemento_Condicion)
                                            {
                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                                    listaNumeros_Encontrado4 = listaNumeros_Encontrado4.Select(i => new EntidadNumero() { Numero = listaNumeros_Encontrado4.IndexOf(i) + 1,
                                                        Textos = new List<string>() { i.ObtenerPosicionTexto(listaNumeros_Encontrado4, OpcionValorPosicion) }
                                                    }).ToList();
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                    listaNumeros_Encontrado4 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = listaNumeros_Encontrado4.Count
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                    listaNumeros_Encontrado4 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                    listaNumeros_Encontrado4 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                    listaNumeros_Encontrado4 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }};
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                    listaNumeros_Encontrado4 = listaNumeros_Encontrado4.Select(i => new EntidadNumero()
                                                    {
                                                        Numero = i.Textos.Count
                                                    }).ToList();
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                    listaNumeros_Encontrado4 = new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(elementoEncontrado)
                                                    }};
                                                    break;

                                            }

                                            foreach (var item in listaNumeros_Encontrado4)
                                            {


                                                CantidadNumerosCondicion_OperacionEntrada++;
                                                cantidadNumeros++;

                                                numerosOperandoCondicion.Add(item);

                                            }

                                            break;
                                    }
                                }
                                else if(otroOperando)
                                {
                                    switch (OpcionSeleccionNumerosElemento_Condicion)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                            if (listaCantidades.Any())
                                                numerosOperandoCondicion.Add(listaCantidades.LastOrDefault());
                                            operandosCondicion.Add(elementoEncontrado);

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:

                                            operandosCondicion.Add(elementoEncontrado);
                                            numerosOperandoCondicion.AddRange(listaCantidades);

                                            break;

                                    }
                                }
                                else
                                {
                                    operandosCondicion.Add(elementoEncontrado);

                                    OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                        new InfoOpcion_VinculadosAnterior()
                                        {
                                            OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion,
                                            OperandoRelacionado_Ejecucion = elementoEncontrado
                                        });

                                    var operacion = (ElementoOperacionAritmeticaEjecucion)elementoEncontrado;

                                    switch (OpcionSeleccionNumerosElemento_Condicion)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:


                                            cantidadNumeros = operacion.ElementosOperacion.Count;
                                            switch (TipoSubElemento_Condicion)
                                            {
                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento:
                                                    numerosOperandoCondicion.AddRange(operacion.Numeros.Where(i =>
                                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList());
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                                    var listaNumerosPosiciones = operacion.Numeros.Where(i =>
                                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count &&
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) ||
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();

                                                    numerosOperandoCondicion.AddRange(listaNumerosPosiciones.Select(i => new EntidadNumero() { Numero = operacion.Numeros.IndexOf(i) + 1,
                                Textos = new List<string>() { i.ObtenerPosicionTexto(listaNumerosPosiciones, OpcionValorPosicion) }
                            }));
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                    numerosOperandoCondicion.Add(new EntidadNumero()
                                                    {
                                                        Numero = operacion.Numeros.Where(i =>
                                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Count
                                                    });
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                    numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }});
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                    numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }});
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                    numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }});
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                    numerosOperandoCondicion.AddRange(operacion.Numeros.Where(i =>
                                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count &&
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) ||
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Select(i => new EntidadNumero() { Numero = i.Textos.Count }).ToList());
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                    numerosOperandoCondicion.Add(new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(operacion)
                                                    });
                                                    break;
                                            }

                                            //operandosCondicion.AddRange(operacion.ElementosOperacion);


                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:

                                            cantidadNumeros = operacion.ElementosOperacion.Count;
                                            switch (TipoSubElemento_Condicion)
                                            {
                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento:
                                                    numerosOperandoCondicion.AddRange(operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).Where(i =>
                                                    operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= operacion.PosicionActualNumero_CondicionesOperador_Implicacion &&
                                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList());
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:

                                                    var listaNumerosPosiciones = operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count &&
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) ||
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).Where(i =>
                                                    operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count &&
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) ||
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= operacion.PosicionActualNumero_CondicionesOperador_Implicacion &&
                                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();

                                                    numerosOperandoCondicion.AddRange(listaNumerosPosiciones.Select(i => new EntidadNumero()
                                                    {
                                                        Numero = operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) + 1,
                                Textos = new List<string>() { i.ObtenerPosicionTexto(listaNumerosPosiciones, OpcionValorPosicion) }
                            }));
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                    numerosOperandoCondicion.Add(new EntidadNumero()
                                                    {
                                                        Numero = operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).Where(i =>
                                                        operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= operacion.PosicionActualNumero_CondicionesOperador_Implicacion &&
                                                        ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Count
                                                    });
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                    numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }});
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                    numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }});
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                    numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }});
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                    numerosOperandoCondicion.AddRange(operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count &&
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) ||
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).Where(i =>
                                                    operacion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count &&
                                    i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) ||
                                    !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                    (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= operacion.PosicionActualNumero_CondicionesOperador_Implicacion &&
                                                    ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Select(i => new EntidadNumero() { Numero = i.Textos.Count }).ToList());
                                                    break;

                                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                    numerosOperandoCondicion.Add(new EntidadNumero()
                                                    {
                                                        Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(operacion)
                                                    });
                                                    break;
                                            }

                                            //operandosCondicion.AddRange(operacion.ElementosOperacion);


                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                            int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(operacion.PosicionActualNumero_CondicionesOperador_Implicacion,
                                                OpcionSeleccionNumerosElemento_Condicion,
                                                operacion.Numeros.Where(i =>
                                                (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                        i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                        !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList().Count);

                                            if (indicePosicion <
                                                operacion.Numeros.Count(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && 
                                        i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) || 
                                        !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                        ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))))
                                            {
                                                cantidadNumeros = 1;

                                                var listaNumeros = operacion.Numeros.Where(i =>
                                                        (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count &&
                                        i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) ||
                                        !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                        ((ConsiderarValores_ProcesamientoCantidades &&
                            (!i.EsCantidadInsertada_ProcesamientoCantidades ||
                            i.EsCantidadInsertada_ProcesamientoCantidades)) ||

                            (ConsiderarSoloValores_ProcesamientoCantidades &&
                            i.EsCantidadInsertada_ProcesamientoCantidades) ||

                            (!ConsiderarValores_ProcesamientoCantidades &&
                            !ConsiderarSoloValores_ProcesamientoCantidades &&
                            !i.EsCantidadInsertada_ProcesamientoCantidades))).ToList();


                                                switch (TipoSubElemento_Condicion)
                                                {
                                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento:
                                                        numerosOperandoCondicion.Add(listaNumeros[indicePosicion]);
                                                        break;

                                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                                        numerosOperandoCondicion.Add(new EntidadNumero() { Numero = indicePosicion + 1,
                                                            Textos = new List<string>() { listaNumeros[indicePosicion].ObtenerPosicionTexto(listaNumeros, OpcionValorPosicion) }
                                                        });
                                                        break;

                                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                                        numerosOperandoCondicion.Add(new EntidadNumero()
                                                        {
                                                            Numero = listaNumeros.Count
                                                        });
                                                        break;

                                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                                        numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualImplicacion,
                                                    }});
                                                        break;

                                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                                        numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualInstanciaImplicacion,
                                                    }});
                                                        break;

                                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                                        numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = PosicionActualIteracionImplicacion,
                                                    }});
                                                        break;

                                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                                        numerosOperandoCondicion.AddRange(new List<EntidadNumero>() { new EntidadNumero()
                                                    {
                                                        Numero = listaNumeros[indicePosicion].Textos.Count
                                                    }});
                                                        break;

                                                    case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                                        numerosOperandoCondicion.Add(new EntidadNumero()
                                                        {
                                                            Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf(operacion)
                                                        });
                                                        break;
                                                }

                                                //operandosCondicion.Add(operacion);
                                            }

                                            break;
                                    }


                                    CantidadNumerosCondicion_OperacionEntrada += cantidadNumeros;
                                }

                                if (elementoEncontrado.Numeros.Contains(numero))
                                {
                                    NumeroIncluidoEnOperando = true;
                                }
                            }

                            //if (evaluarElementosEjecucion && 
                            //    !evaluarElementosCondicionEjecucion)
                            {

                                NumerosVinculados_CondicionAnterior.Clear();
                                OperandosVinculados_CondicionAnterior.Clear();

                                numerosElemento.AddRange(numerosOperandoCondicion);
                                CantidadNumerosCondicion_OperacionEntrada += numerosElemento.Count;

                                List<EntidadNumero> numerosEntidades = new List<EntidadNumero>();

                                var tipoCondicionValores = TipoSubElemento_Condicion_Valores;

                                if (tipoCondicionValores == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.Ninguno ||
                                    TipoElemento_Valores == TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos)
                                {
                                    tipoCondicionValores = TipoSubElemento_Condicion;
                                }

                                if (OpcionValorPosicion == TipoOpcionPosicion.Ninguna)
                                {
                                    foreach (var itemNumero in valoresCondicion)
                                    {
                                        double num = 0;
                                        if (double.TryParse(itemNumero, out num))
                                        {
                                            EntidadNumero elementoNumeroEntidad = new EntidadNumero();
                                            elementoNumeroEntidad.Numero = num;
                                            numerosEntidades.Add(elementoNumeroEntidad);
                                        }
                                        else
                                            NumerosNoCumplenCondicion_Valores++;
                                    }
                                }
                                else
                                {
                                    foreach (var itemNumero in valoresCondicion)
                                    {
                                        string[] partesNumero = itemNumero.Split("|");

                                        double num = 0;
                                        if (double.TryParse(partesNumero[0], out num))
                                        {
                                            EntidadNumero elementoNumeroEntidad = new EntidadNumero();
                                            elementoNumeroEntidad.Numero = num;
                                            elementoNumeroEntidad.Textos.Add(partesNumero[1]);
                                            numerosEntidades.Add(elementoNumeroEntidad);
                                        }
                                    }
                                }

                                if (!numerosElemento.Any())
                                    sinNumerosTextos = true;

                                if (!numerosEntidades.Any())
                                    sinNumerosTextos_Valores = true;

                                if (CantidadNumeros_PorElemento &&
                                    CantidadNumeros_PorElemento_Valores)
                                {
                                    if (TipoOpcionCondicion_ElementoOperacionEntrada == TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA)
                                    {
                                        int indiceValores = 0;

                                        for (int indice = 0; indice < (numerosEntidades.Count > numerosElemento.Count ? numerosEntidades.Count : numerosElemento.Count); indice++)
                                        {
                                            ElementoEjecucionCalculo operandoValoresActual = null;

                                            if (operandosValores.Any())
                                                operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoValoresActual = null;

                                            if (subElementosOperandoValores.Any())
                                                subOperandoValoresActual = subElementosOperandoValores[indiceValores <= subElementosOperandoValores.Count - 1 ? indiceValores : subElementosOperandoValores.Count - 1];

                                            ElementoEjecucionCalculo elementoValoresActual = null;

                                            if (elementosOperandoValores.Any())
                                                elementoValoresActual = elementosOperandoValores[indiceValores <= elementosOperandoValores.Count - 1 ? indiceValores : elementosOperandoValores.Count - 1];

                                            EntidadNumero numeroValoresActual = null;

                                            if (numerosOperandoValores.Any())
                                                numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                            ElementoEjecucionCalculo operandoCondicionActual = null;

                                            if (operandosCondicion.Any())
                                                operandoCondicionActual = operandosCondicion[indiceValores <= operandosCondicion.Count - 1 ? indiceValores : operandosCondicion.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoCondicionActual = null;

                                            if (subElementosOperandoCondicion.Any())
                                                subOperandoCondicionActual = subElementosOperandoCondicion[indiceValores <= subElementosOperandoCondicion.Count - 1 ? indiceValores : subElementosOperandoCondicion.Count - 1];

                                            ElementoEjecucionCalculo elementoCondicionActual = null;

                                            if (elementosOperandoCondicion.Any())
                                                elementoCondicionActual = elementosOperandoCondicion[indiceValores <= elementosOperandoCondicion.Count - 1 ? indiceValores : elementosOperandoCondicion.Count - 1];

                                            EntidadNumero numeroCondicionActual = null;

                                            if (numerosOperandoCondicion.Any())
                                                numeroCondicionActual = numerosOperandoCondicion[indiceValores <= numerosOperandoCondicion.Count - 1 ? indiceValores : numerosOperandoCondicion.Count - 1];


                                            List<EntidadNumero> nums = new List<EntidadNumero>();
                                            List<EntidadNumero> numsElemento = new List<EntidadNumero>();

                                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());
                                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());

                                            if (numerosEntidades.Any())
                                                nums.Add(numerosEntidades[indice < numerosEntidades.Count ? indice : numerosEntidades.Count - 1]);
                                            if (numerosElemento.Any())
                                                numsElemento.Add(numerosElemento[indice < numerosElemento.Count ? indice : numerosElemento.Count - 1]);

                                            if (nums.Any() && !numsElemento.Any(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                            {
                                                valorCondicion_Iteracion = true;

                                                if (operandoCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior_Temp.Add(operandoCondicionActual);
                                                }
                                                if (numeroCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                    TextosInvolucrados.AddRange(numeroCondicionActual.Textos);
                                                }

                                                if (operandoValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                }
                                                if (numeroValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                    TextosInvolucrados.AddRange(numeroValoresActual.Textos);
                                                }

                                                NumerosCumplenCondicion_Elemento += numsElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                NumerosNoCumplenCondicion_Elemento += numsElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion = numsElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion = numsElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                NumerosCumplenCondicion_Valores += nums.Count(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                NumerosNoCumplenCondicion_Valores += nums.Count(numero => numsElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = nums.Count(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = nums.Count(numero => numsElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Elemento += 1;
                                                NumerosNoCumplenCondicion_Valores += 1;
                                                CantidadesNumeros.Remove(CantidadesNumeros.LastOrDefault());
                                                //CantidadesNumeros_Valores.Remove(//CantidadesNumeros_Valores.LastOrDefault());
                                            }

                                            indiceValores++;
                                        }

                                    }
                                    else
                                    {
                                        int indiceValores = 0;

                                        for (int indice = 0; indice < (numerosEntidades.Count > numerosElemento.Count ? numerosEntidades.Count : numerosElemento.Count); indice++)
                                        {
                                            ElementoEjecucionCalculo operandoValoresActual = null;

                                            if (operandosValores.Any())
                                                operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoValoresActual = null;

                                            if (subElementosOperandoValores.Any())
                                                subOperandoValoresActual = subElementosOperandoValores[indiceValores <= subElementosOperandoValores.Count - 1 ? indiceValores : subElementosOperandoValores.Count - 1];

                                            ElementoEjecucionCalculo elementoValoresActual = null;

                                            if (elementosOperandoValores.Any())
                                                elementoValoresActual = elementosOperandoValores[indiceValores <= elementosOperandoValores.Count - 1 ? indiceValores : elementosOperandoValores.Count - 1];

                                            EntidadNumero numeroValoresActual = null;

                                            if (numerosOperandoValores.Any())
                                                numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                            ElementoEjecucionCalculo operandoCondicionActual = null;

                                            if (operandosCondicion.Any())
                                                operandoCondicionActual = operandosCondicion[indiceValores <= operandosCondicion.Count - 1 ? indiceValores : operandosCondicion.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoCondicionActual = null;

                                            if (subElementosOperandoCondicion.Any())
                                                subOperandoCondicionActual = subElementosOperandoCondicion[indiceValores <= subElementosOperandoCondicion.Count - 1 ? indiceValores : subElementosOperandoCondicion.Count - 1];

                                            ElementoEjecucionCalculo elementoCondicionActual = null;

                                            if (elementosOperandoCondicion.Any())
                                                elementoCondicionActual = elementosOperandoCondicion[indiceValores <= elementosOperandoCondicion.Count - 1 ? indiceValores : elementosOperandoCondicion.Count - 1];

                                            EntidadNumero numeroCondicionActual = null;

                                            if (numerosOperandoCondicion.Any())
                                                numeroCondicionActual = numerosOperandoCondicion[indiceValores <= numerosOperandoCondicion.Count - 1 ? indiceValores : numerosOperandoCondicion.Count - 1];


                                            List<EntidadNumero> nums = new List<EntidadNumero>();
                                            List<EntidadNumero> numsElemento = new List<EntidadNumero>();

                                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());
                                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());

                                            if(numerosEntidades.Any())
                                                nums.Add(numerosEntidades[indice < numerosEntidades.Count ? indice : numerosEntidades.Count - 1]);
                                            
                                            if(numerosElemento.Any())
                                                numsElemento.Add(numerosElemento[indice < numerosElemento.Count ? indice : numerosElemento.Count - 1]);

                                            if (numsElemento.Any(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                            {
                                                valorCondicion_Iteracion = true;

                                                if (operandoCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior_Temp.Add(operandoCondicionActual);
                                                }
                                                if (numeroCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                    TextosInvolucrados.AddRange(numeroCondicionActual.Textos);
                                                }

                                                if (operandoValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                }
                                                if (numeroValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                    TextosInvolucrados.AddRange(numeroValoresActual.Textos);
                                                }

                                                NumerosCumplenCondicion_Elemento += numsElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                NumerosNoCumplenCondicion_Elemento += numsElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion = numsElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion = numsElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                NumerosCumplenCondicion_Valores += nums.Count(numero => numsElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                NumerosNoCumplenCondicion_Valores += nums.Count(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = nums.Count(numero => numsElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = nums.Count(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Elemento += 1;
                                                NumerosNoCumplenCondicion_Valores += 1;
                                                CantidadesNumeros.Remove(CantidadesNumeros.LastOrDefault());
                                                //CantidadesNumeros_Valores.Remove(//CantidadesNumeros_Valores.LastOrDefault());
                                            }

                                            indiceValores++;
                                        }
                                    }
                                }
                                else if (CantidadNumeros_PorElemento &&
                                    !CantidadNumeros_PorElemento_Valores)
                                {
                                    if (TipoOpcionCondicion_ElementoOperacionEntrada == TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA)
                                    {
                                        int indiceValores = 0;

                                        for (int indice = 0; indice < numerosEntidades.Count; indice++)
                                        {
                                            ElementoEjecucionCalculo operandoValoresActual = null;

                                            if (operandosValores.Any())
                                                operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoValoresActual = null;

                                            if (subElementosOperandoValores.Any())
                                                subOperandoValoresActual = subElementosOperandoValores[indiceValores <= subElementosOperandoValores.Count - 1 ? indiceValores : subElementosOperandoValores.Count - 1];

                                            ElementoEjecucionCalculo elementoValoresActual = null;

                                            if (elementosOperandoValores.Any())
                                                elementoValoresActual = elementosOperandoValores[indiceValores <= elementosOperandoValores.Count - 1 ? indiceValores : elementosOperandoValores.Count - 1];

                                            EntidadNumero numeroValoresActual = null;

                                            if (numerosOperandoValores.Any())
                                                numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                            ElementoEjecucionCalculo operandoCondicionActual = null;

                                            if (operandosCondicion.Any())
                                                operandoCondicionActual = operandosCondicion[indiceValores <= operandosCondicion.Count - 1 ? indiceValores : operandosCondicion.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoCondicionActual = null;

                                            if (subElementosOperandoCondicion.Any())
                                                subOperandoCondicionActual = subElementosOperandoCondicion[indiceValores <= subElementosOperandoCondicion.Count - 1 ? indiceValores : subElementosOperandoCondicion.Count - 1];

                                            ElementoEjecucionCalculo elementoCondicionActual = null;

                                            if (elementosOperandoCondicion.Any())
                                                elementoCondicionActual = elementosOperandoCondicion[indiceValores <= elementosOperandoCondicion.Count - 1 ? indiceValores : elementosOperandoCondicion.Count - 1];

                                            EntidadNumero numeroCondicionActual = null;

                                            if (numerosOperandoCondicion.Any())
                                                numeroCondicionActual = numerosOperandoCondicion[indiceValores <= numerosOperandoCondicion.Count - 1 ? indiceValores : numerosOperandoCondicion.Count - 1];


                                            List<EntidadNumero> nums = new List<EntidadNumero>();

                                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());

                                            if (numerosEntidades.Any())
                                                nums.Add(numerosEntidades[indice < numerosEntidades.Count ? indice : numerosEntidades.Count - 1]);

                                            if (nums.Any() && !numerosElemento.Any(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                            {
                                                valorCondicion_Iteracion = true;

                                                if (operandoCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior.AddRange(operandosCondicion);
                                                    OperandosVinculados_CondicionAnterior_Temp.AddRange(operandosCondicion);
                                                }
                                                if (numeroCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior.AddRange(numerosOperandoCondicion
                                                        .Where(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));

                                                    NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoCondicion
                                                        .Where(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));


                                                    TextosInvolucrados.AddRange(numeroCondicionActual.Textos);
                                                }

                                                if (operandoValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                }
                                                if (numeroValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                    TextosInvolucrados.AddRange(numeroValoresActual.Textos);
                                                }

                                                NumerosCumplenCondicion_Elemento += numerosElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                NumerosNoCumplenCondicion_Elemento += numerosElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion = numerosElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion = numerosElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                NumerosCumplenCondicion_Valores += nums.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                NumerosNoCumplenCondicion_Valores += nums.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = nums.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = nums.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Elemento += 1;
                                                CantidadesNumeros.Remove(CantidadesNumeros.LastOrDefault());
                                            }

                                            indiceValores++;
                                        }

                                    }
                                    else
                                    {
                                        int indiceValores = 0;

                                        for (int indice = 0; indice < numerosEntidades.Count; indice++)
                                        {
                                            ElementoEjecucionCalculo operandoValoresActual = null;

                                            if (operandosValores.Any())
                                                operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoValoresActual = null;

                                            if (subElementosOperandoValores.Any())
                                                subOperandoValoresActual = subElementosOperandoValores[indiceValores <= subElementosOperandoValores.Count - 1 ? indiceValores : subElementosOperandoValores.Count - 1];

                                            ElementoEjecucionCalculo elementoValoresActual = null;

                                            if (elementosOperandoValores.Any())
                                                elementoValoresActual = elementosOperandoValores[indiceValores <= elementosOperandoValores.Count - 1 ? indiceValores : elementosOperandoValores.Count - 1];

                                            EntidadNumero numeroValoresActual = null;

                                            if (numerosOperandoValores.Any())
                                                numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                            ElementoEjecucionCalculo operandoCondicionActual = null;

                                            if (operandosCondicion.Any())
                                                operandoCondicionActual = operandosCondicion[indiceValores <= operandosCondicion.Count - 1 ? indiceValores : operandosCondicion.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoCondicionActual = null;

                                            if (subElementosOperandoCondicion.Any())
                                                subOperandoCondicionActual = subElementosOperandoCondicion[indiceValores <= subElementosOperandoCondicion.Count - 1 ? indiceValores : subElementosOperandoCondicion.Count - 1];

                                            ElementoEjecucionCalculo elementoCondicionActual = null;

                                            if (elementosOperandoCondicion.Any())
                                                elementoCondicionActual = elementosOperandoCondicion[indiceValores <= elementosOperandoCondicion.Count - 1 ? indiceValores : elementosOperandoCondicion.Count - 1];

                                            EntidadNumero numeroCondicionActual = null;

                                            if (numerosOperandoCondicion.Any())
                                                numeroCondicionActual = numerosOperandoCondicion[indiceValores <= numerosOperandoCondicion.Count - 1 ? indiceValores : numerosOperandoCondicion.Count - 1];


                                            List<EntidadNumero> nums = new List<EntidadNumero>();

                                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());

                                            if (numerosEntidades.Any())
                                                nums.Add(numerosEntidades[indice < numerosEntidades.Count ? indice : numerosEntidades.Count - 1]);

                                            if (numerosElemento.Any(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                            {
                                                valorCondicion_Iteracion = true;

                                                if (operandoCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior.AddRange(operandosCondicion);
                                                    OperandosVinculados_CondicionAnterior_Temp.AddRange(operandosCondicion);
                                                }
                                                if (numeroCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior.AddRange(numerosOperandoCondicion
                                                        .Where(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));
                                                    NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoCondicion
                                                        .Where(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));
                                                    TextosInvolucrados.AddRange(numeroCondicionActual.Textos);
                                                }

                                                if (operandoValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                }
                                                if (numeroValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                    TextosInvolucrados.AddRange(numeroValoresActual.Textos);
                                                }

                                                NumerosCumplenCondicion_Elemento += numerosElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                NumerosNoCumplenCondicion_Elemento += numerosElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion = numerosElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion = numerosElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                NumerosCumplenCondicion_Valores += nums.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                NumerosNoCumplenCondicion_Valores += nums.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = nums.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = nums.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Elemento += 1;
                                                CantidadesNumeros.Remove(CantidadesNumeros.LastOrDefault());
                                            }

                                            indiceValores++;
                                        }
                                    }
                                }
                                else if (!CantidadNumeros_PorElemento &&
                                    CantidadNumeros_PorElemento_Valores)
                                {
                                    if (TipoOpcionCondicion_ElementoOperacionEntrada == TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA)
                                    {
                                        int indiceValores = 0;

                                        for (int indice = 0; indice < numerosElemento.Count; indice++)
                                        {
                                            ElementoEjecucionCalculo operandoValoresActual = null;

                                            if (operandosValores.Any())
                                                operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoValoresActual = null;

                                            if (subElementosOperandoValores.Any())
                                                subOperandoValoresActual = subElementosOperandoValores[indiceValores <= subElementosOperandoValores.Count - 1 ? indiceValores : subElementosOperandoValores.Count - 1];

                                            ElementoEjecucionCalculo elementoValoresActual = null;

                                            if (elementosOperandoValores.Any())
                                                elementoValoresActual = elementosOperandoValores[indiceValores <= elementosOperandoValores.Count - 1 ? indiceValores : elementosOperandoValores.Count - 1];

                                            EntidadNumero numeroValoresActual = null;

                                            if (numerosOperandoValores.Any())
                                                numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                            ElementoEjecucionCalculo operandoCondicionActual = null;

                                            if (operandosCondicion.Any())
                                                operandoCondicionActual = operandosCondicion[indiceValores <= operandosCondicion.Count - 1 ? indiceValores : operandosCondicion.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoCondicionActual = null;

                                            if (subElementosOperandoCondicion.Any())
                                                subOperandoCondicionActual = subElementosOperandoCondicion[indiceValores <= subElementosOperandoCondicion.Count - 1 ? indiceValores : subElementosOperandoCondicion.Count - 1];

                                            ElementoEjecucionCalculo elementoCondicionActual = null;

                                            if (elementosOperandoCondicion.Any())
                                                elementoCondicionActual = elementosOperandoCondicion[indiceValores <= elementosOperandoCondicion.Count - 1 ? indiceValores : elementosOperandoCondicion.Count - 1];

                                            EntidadNumero numeroCondicionActual = null;

                                            if (numerosOperandoCondicion.Any())
                                                numeroCondicionActual = numerosOperandoCondicion[indiceValores <= numerosOperandoCondicion.Count - 1 ? indiceValores : numerosOperandoCondicion.Count - 1];


                                            List<EntidadNumero> numsElemento = new List<EntidadNumero>();

                                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());

                                            numsElemento.Add(numerosElemento[indice < numerosElemento.Count ? indice : numerosElemento.Count - 1]);

                                            if (numerosEntidades.Any() && !numsElemento.Any(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                            {
                                                valorCondicion_Iteracion = true;

                                                if (operandoCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior_Temp.Add(operandoCondicionActual);
                                                }
                                                if (numeroCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                    TextosInvolucrados.AddRange(numeroCondicionActual.Textos);
                                                }

                                                if (operandoValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior.AddRange(operandosValores);
                                                    OperandosVinculados_CondicionAnterior_Temp.AddRange(operandosValores);
                                                }
                                                if (numeroValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior.AddRange(numerosOperandoValores
                                                        .Where(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));
                                                    NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoValores
                                                        .Where(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));

                                                    TextosInvolucrados.AddRange(numeroValoresActual.Textos);
                                                }

                                                NumerosCumplenCondicion_Elemento += numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                NumerosNoCumplenCondicion_Elemento += numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                NumerosCumplenCondicion_Valores += numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                NumerosNoCumplenCondicion_Valores += numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Valores += 1;
                                                CantidadesNumeros.Remove(CantidadesNumeros.LastOrDefault());
                                            }

                                            indiceValores++;
                                        }

                                    }
                                    else
                                    {
                                        int indiceValores = 0;

                                        for (int indice = 0; indice < numerosElemento.Count; indice++)
                                        {
                                            ElementoEjecucionCalculo operandoValoresActual = null;

                                            if (operandosValores.Any())
                                                operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoValoresActual = null;

                                            if (subElementosOperandoValores.Any())
                                                subOperandoValoresActual = subElementosOperandoValores[indiceValores <= subElementosOperandoValores.Count - 1 ? indiceValores : subElementosOperandoValores.Count - 1];

                                            ElementoEjecucionCalculo elementoValoresActual = null;

                                            if (elementosOperandoValores.Any())
                                                elementoValoresActual = elementosOperandoValores[indiceValores <= elementosOperandoValores.Count - 1 ? indiceValores : elementosOperandoValores.Count - 1];

                                            EntidadNumero numeroValoresActual = null;

                                            if (numerosOperandoValores.Any())
                                                numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                            ElementoEjecucionCalculo operandoCondicionActual = null;

                                            if (operandosCondicion.Any())
                                                operandoCondicionActual = operandosCondicion[indiceValores <= operandosCondicion.Count - 1 ? indiceValores : operandosCondicion.Count - 1];

                                            ElementoDiseñoOperacionAritmeticaEjecucion subOperandoCondicionActual = null;

                                            if (subElementosOperandoCondicion.Any())
                                                subOperandoCondicionActual = subElementosOperandoCondicion[indiceValores <= subElementosOperandoCondicion.Count - 1 ? indiceValores : subElementosOperandoCondicion.Count - 1];

                                            ElementoEjecucionCalculo elementoCondicionActual = null;

                                            if (elementosOperandoCondicion.Any())
                                                elementoCondicionActual = elementosOperandoCondicion[indiceValores <= elementosOperandoCondicion.Count - 1 ? indiceValores : elementosOperandoCondicion.Count - 1];

                                            EntidadNumero numeroCondicionActual = null;

                                            if (numerosOperandoCondicion.Any())
                                                numeroCondicionActual = numerosOperandoCondicion[indiceValores <= numerosOperandoCondicion.Count - 1 ? indiceValores : numerosOperandoCondicion.Count - 1];


                                            List<EntidadNumero> numsElemento = new List<EntidadNumero>();

                                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());

                                            numsElemento.Add(numerosElemento[indice < numerosElemento.Count ? indice : numerosElemento.Count - 1]);

                                            if (numsElemento.Any(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                            {
                                                valorCondicion_Iteracion = true;

                                                if (operandoCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior.Add(operandoCondicionActual);
                                                    OperandosVinculados_CondicionAnterior_Temp.Add(operandoCondicionActual);
                                                }
                                                if (numeroCondicionActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior.Add(numeroCondicionActual);
                                                    NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                    TextosInvolucrados.AddRange(numeroCondicionActual.Textos);
                                                }

                                                if (operandoValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);
                                                    OperandosVinculados_CondicionAnterior.AddRange(operandosValores);
                                                    OperandosVinculados_CondicionAnterior_Temp.AddRange(operandosValores);
                                                }
                                                if (numeroValoresActual != null)
                                                {
                                                    CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);
                                                    NumerosVinculados_CondicionAnterior.AddRange(numerosOperandoValores
                                                        .Where(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));
                                                    NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoValores
                                                        .Where(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));

                                                    TextosInvolucrados.AddRange(numeroValoresActual.Textos);
                                                }

                                                NumerosCumplenCondicion_Elemento += numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                NumerosNoCumplenCondicion_Elemento += numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                                NumerosCumplenCondicion_Valores += numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                NumerosNoCumplenCondicion_Valores += numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                                CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                                CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Valores += 1;
                                                CantidadesNumeros.Remove(CantidadesNumeros.LastOrDefault());
                                            }

                                            indiceValores++;
                                        }
                                    }
                                }
                                else if (!CantidadNumeros_PorElemento &&
                                    !CantidadNumeros_PorElemento_Valores)
                                {
                                    if (TipoOpcionCondicion_ElementoOperacionEntrada == TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA)
                                    {
                                        int indiceValores = 0;

                                        ElementoEjecucionCalculo operandoValoresActual = null;

                                        if (operandosValores.Any())
                                            operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

                                        ElementoDiseñoOperacionAritmeticaEjecucion subOperandoValoresActual = null;

                                        if (subElementosOperandoValores.Any())
                                            subOperandoValoresActual = subElementosOperandoValores[indiceValores <= subElementosOperandoValores.Count - 1 ? indiceValores : subElementosOperandoValores.Count - 1];

                                        ElementoEjecucionCalculo elementoValoresActual = null;

                                        if (elementosOperandoValores.Any())
                                            elementoValoresActual = elementosOperandoValores[indiceValores <= elementosOperandoValores.Count - 1 ? indiceValores : elementosOperandoValores.Count - 1];

                                        EntidadNumero numeroValoresActual = null;

                                        if (numerosOperandoValores.Any())
                                            numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                        ElementoEjecucionCalculo operandoCondicionActual = null;

                                        if (operandosCondicion.Any())
                                            operandoCondicionActual = operandosCondicion[indiceValores <= operandosCondicion.Count - 1 ? indiceValores : operandosCondicion.Count - 1];

                                        ElementoDiseñoOperacionAritmeticaEjecucion subOperandoCondicionActual = null;

                                        if (subElementosOperandoCondicion.Any())
                                            subOperandoCondicionActual = subElementosOperandoCondicion[indiceValores <= subElementosOperandoCondicion.Count - 1 ? indiceValores : subElementosOperandoCondicion.Count - 1];

                                        ElementoEjecucionCalculo elementoCondicionActual = null;

                                        if (elementosOperandoCondicion.Any())
                                            elementoCondicionActual = elementosOperandoCondicion[indiceValores <= elementosOperandoCondicion.Count - 1 ? indiceValores : elementosOperandoCondicion.Count - 1];

                                        EntidadNumero numeroCondicionActual = null;

                                        if (numerosOperandoCondicion.Any())
                                            numeroCondicionActual = numerosOperandoCondicion[indiceValores <= numerosOperandoCondicion.Count - 1 ? indiceValores : numerosOperandoCondicion.Count - 1];

                                        CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());

                                        if (numerosEntidades.Any() && !numerosElemento.Any(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                        {
                                            valorCondicion_Iteracion = true;

                                            if (operandoCondicionActual != null)
                                            {
                                                CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                OperandosVinculados_CondicionAnterior.AddRange(operandosCondicion);
                                                OperandosVinculados_CondicionAnterior_Temp.AddRange(operandosCondicion);
                                            }
                                            if (numeroCondicionActual != null)
                                            {
                                                CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                NumerosVinculados_CondicionAnterior.AddRange(numerosOperandoCondicion
                                                    .Where(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));
                                                NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoCondicion
                                                    .Where(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));

                                                TextosInvolucrados.AddRange(numeroCondicionActual.Textos);
                                            }

                                            if (operandoValoresActual != null)
                                            {
                                                CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);
                                                OperandosVinculados_CondicionAnterior.AddRange(operandosValores);
                                                OperandosVinculados_CondicionAnterior_Temp.AddRange(operandosValores);
                                            }
                                            if (numeroValoresActual != null)
                                            {
                                                CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);
                                                NumerosVinculados_CondicionAnterior.AddRange(numerosOperandoValores
                                                    .Where(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));
                                                NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoValores
                                                    .Where(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));

                                                TextosInvolucrados.AddRange(numeroValoresActual.Textos);
                                            }

                                            NumerosCumplenCondicion_Elemento += numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                            NumerosNoCumplenCondicion_Elemento += numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                            CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                            CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                            NumerosCumplenCondicion_Valores += numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                            NumerosNoCumplenCondicion_Valores += numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                            CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                            CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                        }
                                        //if (numeros.Any() && !numerosElemento.Intersect(numeros, new ComparadorNumeros_Condicion(
                                        //            TipoOpcionCondicion_ElementoOperacionEntrada)).Any())
                                        //    valorCondicion = true;
                                    }
                                    else
                                    {
                                        int indiceValores = 0;

                                        ElementoEjecucionCalculo operandoValoresActual = null;

                                        if (operandosValores.Any())
                                            operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

                                        ElementoDiseñoOperacionAritmeticaEjecucion subOperandoValoresActual = null;

                                        if (subElementosOperandoValores.Any())
                                            subOperandoValoresActual = subElementosOperandoValores[indiceValores <= subElementosOperandoValores.Count - 1 ? indiceValores : subElementosOperandoValores.Count - 1];

                                        ElementoEjecucionCalculo elementoValoresActual = null;

                                        if (elementosOperandoValores.Any())
                                            elementoValoresActual = elementosOperandoValores[indiceValores <= elementosOperandoValores.Count - 1 ? indiceValores : elementosOperandoValores.Count - 1];

                                        EntidadNumero numeroValoresActual = null;

                                        if (numerosOperandoValores.Any())
                                            numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count - 1 ? indiceValores : numerosOperandoValores.Count - 1];


                                        ElementoEjecucionCalculo operandoCondicionActual = null;

                                        if (operandosCondicion.Any())
                                            operandoCondicionActual = operandosCondicion[indiceValores <= operandosCondicion.Count - 1 ? indiceValores : operandosCondicion.Count - 1];

                                        ElementoDiseñoOperacionAritmeticaEjecucion subOperandoCondicionActual = null;

                                        if (subElementosOperandoCondicion.Any())
                                            subOperandoCondicionActual = subElementosOperandoCondicion[indiceValores <= subElementosOperandoCondicion.Count - 1 ? indiceValores : subElementosOperandoCondicion.Count - 1];

                                        ElementoEjecucionCalculo elementoCondicionActual = null;

                                        if (elementosOperandoCondicion.Any())
                                            elementoCondicionActual = elementosOperandoCondicion[indiceValores <= elementosOperandoCondicion.Count - 1 ? indiceValores : elementosOperandoCondicion.Count - 1];

                                        EntidadNumero numeroCondicionActual = null;

                                        if (numerosOperandoCondicion.Any())
                                            numeroCondicionActual = numerosOperandoCondicion[indiceValores <= numerosOperandoCondicion.Count - 1 ? indiceValores : numerosOperandoCondicion.Count - 1];

                                        CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionImplicacion());

                                        if (numerosElemento.Any(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                        {
                                            valorCondicion_Iteracion = true;

                                            if (operandoCondicionActual != null)
                                            {
                                                CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                OperandosVinculados_CondicionAnterior.AddRange(operandosCondicion);
                                                OperandosVinculados_CondicionAnterior_Temp.AddRange(operandosCondicion);
                                            }
                                            if (numeroCondicionActual != null)
                                            {
                                                CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                NumerosVinculados_CondicionAnterior.AddRange(numerosOperandoCondicion
                                                    .Where(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));
                                                NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoCondicion
                                                    .Where(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));

                                                TextosInvolucrados.AddRange(numeroCondicionActual.Textos);
                                            }

                                            if (operandoValoresActual != null)
                                            {
                                                CantidadesNumeros.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);
                                                OperandosVinculados_CondicionAnterior.AddRange(operandosValores);
                                                OperandosVinculados_CondicionAnterior_Temp.AddRange(operandosValores);
                                            }
                                            if (numeroValoresActual != null)
                                            {
                                                CantidadesNumeros.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);
                                                NumerosVinculados_CondicionAnterior.AddRange(numerosOperandoValores
                                                    .Where(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));
                                                NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoValores
                                                    .Where(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));

                                                TextosInvolucrados.AddRange(numeroValoresActual.Textos);
                                            }

                                            NumerosCumplenCondicion_Elemento += numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                            NumerosNoCumplenCondicion_Elemento += numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                            CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                            CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                            NumerosCumplenCondicion_Valores += numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                            NumerosNoCumplenCondicion_Valores += numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                            CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                            CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                        }
                                        //if (numerosElemento.Intersect(numeros, new ComparadorNumeros_Condicion(
                                        //            TipoOpcionCondicion_ElementoOperacionEntrada)).Any())
                                        //    valorCondicion = true;
                                    }
                                }

                            }
                        }

                        break;
                }

                switch (TipoElementoCondicion)
                {
                    case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion:

                        switch (TipoTextosInformacion_Valores)
                        {
                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada:

                                switch (OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                        if (ElementoEntrada_Valores != null)
                                        {
                                            var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                            var elementosEntrada_Totales = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == ElementoEntrada_Valores &&
                                            asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                            List<int> totales = new List<int>();

                                            foreach (var itemEntrada in elementosEntrada_Totales)
                                            {
                                                //asignacion.PosicionesTextos_CumplenCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);
                                                itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                            }
                                        }

                                        break;

                                }

                                if (valorCondicion_Iteracion)
                                {
                                    if (ElementoEntrada_Valores != null)
                                    {
                                        var lista = asignacion.ObtenerTextos_CondicionEntrada(ElementoEntrada_Valores, PosicionActualNumero_CondicionesOperador_Implicacion_Entrada);

                                        if (lista.Intersect(TextosInvolucrados).Any())
                                            asignacion.PosicionesTextos_CumplenCondicion.Add(PosicionActualNumero_CondicionesOperador_Implicacion_Entrada);

                                    }
                                }

                                break;
                        }

                        switch (OpcionSeleccionNumerosElemento_Condicion_TextosInformacion)
                        {
                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                if (EntradaCondicion != null)
                                {
                                    var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                    var elementosEntrada = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion &&
                                    asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                    foreach (var itemEntrada in elementosEntrada)
                                    {
                                        //if(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion < totalElementosEntradas)
                                        //asignacion.PosicionesTextos_CumplenCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);

                                        itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                    }
                                }

                                break;
                        }

                        if (valorCondicion_Iteracion)
                        {
                            if (EntradaCondicion != null)
                            {
                                var lista = asignacion.ObtenerTextos_CondicionEntrada(EntradaCondicion, PosicionActualNumero_CondicionesOperador_Implicacion_Entrada);

                                if (lista.Intersect(TextosInvolucrados).Any())
                                    asignacion.PosicionesTextos_CumplenCondicion.Add(PosicionActualNumero_CondicionesOperador_Implicacion_Entrada);

                            }
                        }


                        break;
                }


                if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada &&
                    TipoSubElemento_Condicion == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento &&
                    !NumeroIncluidoEnOperando &&
                    valorCondicion_Iteracion)
                {
                    valorCondicion_Iteracion = false;
                }

                if (valorCondicion == false)
                    valorCondicion = valorCondicion_Iteracion;

            }

            switch (TipoElementoCondicion)
            {
                case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion:

                    switch (TipoTextosInformacion_Valores)
                    {
                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada:
                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion:

                            switch (OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion)
                            {

                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                    if (valorCondicion)
                                    {
                                        List<int> totales = new List<int>();
                                        List<int> posicionesElementoCondicion = new List<int>();

                                        var listas = asignacion.ObtenerTextos_CondicionEntrada(ElementoEntrada_Valores);
                                        posicionesElementoCondicion = ObtenerFilasCumplenCondicion_TextosValores_Involucrados(listas, TextosInvolucrados);

                                        if (EntradaCondicion != null)
                                        {
                                            var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                            var elementosEntrada = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion
                                            && asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                            foreach (var itemEntrada in elementosEntrada)
                                            {
                                                //if(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion < totalElementosEntradas)
                                                //asignacion.PosicionesTextos_CumplenCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);
                                                totales.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.TotalElementos_CondicionesOperador_SeleccionarOrdenar);
                                                //itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                                //posicionesElementoCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);
                                            }
                                        }

                                        if ((OperandoCondicion != null && OperandoSubElemento_Condicion_TextosInformacion == null) ||
                                            EsOperandoTextosActual)
                                        {
                                            var elementoEjecucion = ejecucion.ObtenerElementoEjecucion(OperandoCondicion);

                                            if (EsOperandoTextosActual)
                                                elementoEjecucion = operando;

                                            totales.Add(elementoEjecucion.TotalElementos_CondicionesOperador_SeleccionarOrdenar);
                                            //posicionesElementoCondicion.Add(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion);
                                            
                                        }

                                        if ((OperandoCondicion != null && OperandoSubElemento_Condicion_TextosInformacion != null) ||
                                            EsOperandoTextosActual)
                                        {
                                            var elementoEjecucion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_TextosInformacion);

                                            if (EsOperandoTextosActual &&
                                                operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                                elementoEjecucion = (ElementoDiseñoOperacionAritmeticaEjecucion)operando;

                                            totales.Add(elementoEjecucion.TotalElementos_CondicionesOperador_SeleccionarOrdenar);
                                            //posicionesElementoCondicion.Add(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion);
                                            
                                        }

                                        int totalElementosEntradas = totales.Any() ? totales.Max() : 0;

                                        if (ElementoEntrada_Valores != null)
                                        {
                                            var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                            var elementosEntrada_Totales = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == ElementoEntrada_Valores &
                                asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                            foreach (var itemEntrada in elementosEntrada_Totales)
                                            {
                                                foreach (var posicion in posicionesElementoCondicion)
                                                {
                                                    if (posicion < totalElementosEntradas &
                                                        posicion < itemEntrada.EntradaRelacionada.EntradaProcesada.TotalElementos_CondicionesOperador_SeleccionarOrdenar)
                                                        asignacion.PosicionesTextos_CumplenCondicion.Add(posicion);
                                                    //itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                                }
                                            }
                                        }

                                        //            int totalElementosEntradas = totales.Any() ? totales.Max() : 0;



                                        //if (OperandoCondicion != null && OperandoSubElemento_Condicion_TextosInformacion == null)
                                        //{
                                        //    var elementoEjecucion = ejecucion.ObtenerElementoEjecucion(OperandoCondicion);

                                        //    if (elementoEjecucion != null &&
                                        //        elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion_Entrada < totalElementosEntradas)
                                        //        asignacion.PosicionesTextos_CumplenCondicion.Add(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion_Entrada);
                                        //}

                                        //if (OperandoCondicion != null && OperandoSubElemento_Condicion_TextosInformacion != null)
                                        //{
                                        //    var elementoEjecucion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_TextosInformacion);

                                        //    if (elementoEjecucion != null &&
                                        //        elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion_Entrada < totalElementosEntradas)
                                        //        asignacion.PosicionesTextos_CumplenCondicion.Add(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion_Entrada);
                                        //}
                                    }

                                    if (ElementoEntrada_Valores != null)
                                    {
                                        var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                        var elementosEntrada_Totales = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == ElementoEntrada_Valores &
                            asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                        List<int> totales = new List<int>();

                                        foreach (var itemEntrada in elementosEntrada_Totales)
                                        {
                                            //asignacion.PosicionesTextos_CumplenCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);
                                            if (itemEntrada.EntradaRelacionada.EntradaProcesada != null)
                                                itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                        }
                                    }

                                    break;
                            }



                            break;
                    }

                    OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                        new InfoOpcion_VinculadosAnterior()
                        {
                            OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion,
                            EntradaRelacionada = EntradaCondicion
                        });

                    switch (OpcionSeleccionNumerosElemento_Condicion)
                    {
                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                            if (valorCondicion)
                            {
                                List<int> totales = new List<int>();
                                List<int> posicionesElementoCondicion = new List<int>();

                                var listas = asignacion.ObtenerTextos_CondicionEntrada(EntradaCondicion);
                                posicionesElementoCondicion = ObtenerFilasCumplenCondicion_TextosValores_Involucrados(listas, TextosInvolucrados);

                                if (ElementoEntrada_Valores != null)
                                {
                                    var elementosEntrada_Totales = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == ElementoEntrada_Valores &
                        asignacion.DiseñoTextosInformacion_Relacionado.ElementosAnteriores.Contains(item)).ToList();

                                    foreach (var itemEntrada in elementosEntrada_Totales)
                                    {
                                        //asignacion.PosicionesTextos_CumplenCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);
                                        //itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                        totales.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.TotalElementos_CondicionesOperador_SeleccionarOrdenar);
                                        //posicionesElementoCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);
                                    }
                                }

                                if ((ElementoOperacion_Valores != null && SubElementoOperacion_Valores == null) ||
                                    EsOperandoValoresTextosActual)
                                {
                                    var elementoEjecucion = ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores);

                                    if (EsOperandoValoresTextosActual)
                                        elementoEjecucion = operando;

                                    totales.Add(elementoEjecucion.TotalElementos_CondicionesOperador_SeleccionarOrdenar);
                                    //posicionesElementoCondicion.Add(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion);

                                }

                                if ((ElementoOperacion_Valores != null && SubElementoOperacion_Valores != null) ||
                                    EsOperandoValoresTextosActual)
                                {
                                    var elementoEjecucion = ejecucion.ObtenerSubElementoEjecucion(SubElementoOperacion_Valores);

                                    if (EsOperandoValoresTextosActual &&
                                        operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                        elementoEjecucion = (ElementoDiseñoOperacionAritmeticaEjecucion)operando;

                                    totales.Add(elementoEjecucion.TotalElementos_CondicionesOperador_SeleccionarOrdenar);
                                    //posicionesElementoCondicion.Add(elementoEjecucion.PosicionActualNumero_CondicionesOperador_Implicacion);

                                }

                                int totalElementosEntradas = totales.Any() ? totales.Max() : 0;

                                if (EntradaCondicion != null)
                                {
                                    var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                    var elementosEntrada = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion &
                    asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                    foreach (var itemEntrada in elementosEntrada)
                                    {
                                        foreach (var posicion in posicionesElementoCondicion)
                                        {
                                            if (itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion < totalElementosEntradas &
                                                    itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion < itemEntrada.EntradaRelacionada.EntradaProcesada.TotalElementos_CondicionesOperador_SeleccionarOrdenar)
                                                //if(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion < totalElementosEntradas)
                                                asignacion.PosicionesTextos_CumplenCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);

                                            //itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                        }
                                    }
                                }
                            }

                            if (EntradaCondicion != null)
                            {
                                var asignacionElemento = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.FirstOrDefault(item => item.OperacionRelacionada == operacionCondicionEjecucion.ElementoDiseñoRelacionado);

                                var elementosEntrada = asignacion.DiseñoTextosInformacion_Calculo.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion &
                asignacionElemento != null && asignacionElemento.ElementosAnteriores.Contains(item)).ToList();

                                foreach (var itemEntrada in elementosEntrada)
                                {
                                    //if(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion < totalElementosEntradas)
                                    //asignacion.PosicionesTextos_CumplenCondicion.Add(itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion);

                                    itemEntrada.EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                }
                            }

                            break;
                    }
                    break;
            }

            foreach (var dupla in duplasTextosInformacion_Elementos)
            {
                dupla.TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_ElementoCondicion.Clear();
                dupla.TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_ElementoCondicion.AddRange(dupla.TextosInformacion_CumplenCondiciones_Anteriores_Entrada.ToList());
            }

            if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
            {
                //if ((CantidadTextosInformacion_PorElemento & !CantidadTextosInformacion_PorElemento_Valores) ||
                //    (CantidadTextosInformacion_PorElemento & CantidadTextosInformacion_PorElemento_Valores) ||
                //    (!CantidadTextosInformacion_PorElemento & CantidadTextosInformacion_PorElemento_Valores))
                {
                    bool HayCantidadesCumpleCondicion = false;

                    foreach (var itemCantidad in CantidadesTextos)
                    {
                        bool valorCondicionCantidad_Condicion = true;

                        if (OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.TextosCumplenCondicion_TextosInformacion - itemCantidad.TextosNoCumplenCondicion_TextosInformacion == 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.TextosCumplenCondicion_TextosInformacion - itemCantidad.TextosNoCumplenCondicion_TextosInformacion != itemCantidad.TextosCumplenCondicion_TextosInformacion &&
                                        itemCantidad.TextosCumplenCondicion_TextosInformacion - itemCantidad.TextosNoCumplenCondicion_TextosInformacion != -itemCantidad.TextosNoCumplenCondicion_TextosInformacion)
                                        valorCondicionCantidad_Condicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.TextosCumplenCondicion_TextosInformacion - itemCantidad.TextosNoCumplenCondicion_TextosInformacion < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.TextosCumplenCondicion_TextosInformacion - itemCantidad.TextosNoCumplenCondicion_TextosInformacion > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.TextosCumplenCondicion_TextosInformacion - itemCantidad.TextosNoCumplenCondicion_TextosInformacion != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.TextosCumplenCondicion_TextosInformacion == 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.TextosNoCumplenCondicion_TextosInformacion > 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.TextosCumplenCondicion_TextosInformacion < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.TextosCumplenCondicion_TextosInformacion > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.TextosCumplenCondicion_TextosInformacion != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }

                        bool valorCondicionCantidad_Valores = true;

                        if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores == 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != itemCantidad.TextosCumplenCondicion_Valores &&
                                        itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != -itemCantidad.TextosNoCumplenCondicion_Valores)
                                        valorCondicionCantidad_Valores = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion;
                                            break;
                                    }


                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.TextosCumplenCondicion_Valores == 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.TextosNoCumplenCondicion_Valores > 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion;
                                            break;
                                    }


                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.TextosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.TextosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.TextosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;
                                    }

                                    break;
                            }
                        }

                        if (valorCondicionCantidad_Condicion)
                            HayCantidadesCumpleCondicion = true;
                        else
                            if (CantidadTextosInformacion_PorElemento)
                            QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, true, false, CantidadesTextos);

                        if (valorCondicionCantidad_Valores)
                            HayCantidadesCumpleCondicion = true;
                        else
                            if (CantidadTextosInformacion_PorElemento_Valores)
                            QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, false, true, CantidadesTextos);
                    }

                    if (!HayCantidadesCumpleCondicion)
                        valorCondicion = false;
                }

                if (OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion)
                {
                    switch (OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion)
                    {
                        case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                            if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento == 0)
                                valorCondicion = false;

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento != NumerosCumplenCondicion_Elemento &&
                                NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento != -NumerosNoCumplenCondicion_Elemento)
                                valorCondicion = false;

                            break;

                        case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                            switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion)
                            {
                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion = NumerosCumplenCondicion_Elemento;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion = NumerosCumplenCondicion_Valores;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion = CantidadNumerosCondicion_TextosInformacion;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion = CantidadNumerosValoresCondicion;
                                    break;
                            }

                            switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_TextosInformacion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_TextosInformacion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_TextosInformacion)
                                        valorCondicion = false;
                                    break;
                            }

                            break;
                    }
                }
                else
                {
                    if (!CantidadTextosInformacion_PorElemento & !CantidadTextosInformacion_PorElemento_Valores)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (NumerosCumplenCondicion_Elemento == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (NumerosNoCumplenCondicion_Elemento > 0)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion = NumerosCumplenCondicion_Elemento;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion = NumerosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion = CantidadNumerosCondicion_TextosInformacion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion = CantidadNumerosValoresCondicion;
                                        break;
                                }

                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (NumerosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_TextosInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (NumerosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_TextosInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (NumerosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_TextosInformacion)
                                            valorCondicion = false;
                                        break;
                                }

                                break;
                        }
                    }
                }

                if (OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                {
                    switch (OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                    {
                        case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                            if (TextosCumplenCondicion_TextosInformacion - TextosNoCumplenCondicion_TextosInformacion == 0)
                                valorCondicion = false;

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (TextosCumplenCondicion_TextosInformacion - TextosNoCumplenCondicion_TextosInformacion != TextosCumplenCondicion_TextosInformacion &&
                                TextosCumplenCondicion_TextosInformacion - TextosNoCumplenCondicion_TextosInformacion != -TextosNoCumplenCondicion_TextosInformacion)
                                valorCondicion = false;

                            break;

                        case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                            switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                            {
                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TextosCumplenCondicion_TextosInformacion;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = CantidadTextosCondicion;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = CantidadTextosValoresCondicion;
                                    break;
                            }

                            switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (TextosCumplenCondicion_TextosInformacion - TextosNoCumplenCondicion_TextosInformacion < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (TextosCumplenCondicion_TextosInformacion - TextosNoCumplenCondicion_TextosInformacion > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (TextosCumplenCondicion_TextosInformacion - TextosNoCumplenCondicion_TextosInformacion != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        valorCondicion = false;
                                    break;
                            }

                            break;
                    }
                }
                else
                {
                    if (!CantidadTextosInformacion_PorElemento & !CantidadTextosInformacion_PorElemento_Valores)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (TextosCumplenCondicion_TextosInformacion == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (TextosNoCumplenCondicion_TextosInformacion > 0)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TextosCumplenCondicion_TextosInformacion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = CantidadTextosCondicion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = CantidadTextosValoresCondicion;
                                        break;
                                }

                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (TextosCumplenCondicion_TextosInformacion < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (TextosCumplenCondicion_TextosInformacion > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (TextosCumplenCondicion_TextosInformacion != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;
                                }

                                break;
                        }
                    }
                }
            }
            else if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
            {
                if ((CantidadNumeros_PorElemento & !CantidadNumeros_PorElemento_Valores) ||
                    (CantidadNumeros_PorElemento & CantidadNumeros_PorElemento_Valores) ||
                    (!CantidadNumeros_PorElemento & CantidadNumeros_PorElemento_Valores))
                {
                    bool HayCantidadesCumpleCondicion = false;

                    foreach (var itemCantidad in CantidadesNumeros)
                    {
                        bool valorCondicionCantidad_Condicion = true;

                        if (OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.NumerosCumplenCondicion - itemCantidad.NumerosNoCumplenCondicion == 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.NumerosCumplenCondicion - itemCantidad.NumerosNoCumplenCondicion == itemCantidad.NumerosCumplenCondicion)
                                        valorCondicionCantidad_Condicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.NumerosCumplenCondicion - itemCantidad.NumerosNoCumplenCondicion < CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.NumerosCumplenCondicion - itemCantidad.NumerosNoCumplenCondicion > CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.NumerosCumplenCondicion - itemCantidad.NumerosNoCumplenCondicion != CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.NumerosCumplenCondicion == 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.NumerosNoCumplenCondicion > 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.NumerosCumplenCondicion < CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.NumerosCumplenCondicion > CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.NumerosCumplenCondicion != CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }

                        bool valorCondicionCantidad_Valores = true;

                        if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores == 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != itemCantidad.NumerosCumplenCondicion_Valores &&
                                        itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != -itemCantidad.NumerosNoCumplenCondicion_Valores)
                                        valorCondicionCantidad_Valores = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicionCantidad_Valores = false;
                                            break;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.NumerosCumplenCondicion_Valores == 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.NumerosNoCumplenCondicion_Valores > 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.NumerosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.NumerosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.NumerosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicionCantidad_Valores = false;
                                            break;
                                    }

                                    break;
                            }
                        }

                        if (valorCondicionCantidad_Condicion)
                            HayCantidadesCumpleCondicion = true;
                        else
                            if (CantidadNumeros_PorElemento)
                            QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, true, false, CantidadesNumeros);

                        if (valorCondicionCantidad_Valores)
                            HayCantidadesCumpleCondicion = true;
                        else
                            if (CantidadNumeros_PorElemento_Valores)
                            QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, false, true, CantidadesNumeros);
                    }

                    if (!HayCantidadesCumpleCondicion)
                        valorCondicion = false;
                }

                if (OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                {
                    switch (OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                    {
                        case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                            if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento == 0)
                                valorCondicion = false;

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento == NumerosCumplenCondicion_Elemento)
                                valorCondicion = false;

                            break;

                        case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                            switch (OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                            {
                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_OperacionEntrada = NumerosCumplenCondicion_Elemento;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_OperacionEntrada = NumerosCumplenCondicion_Valores;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                    CantidadSubNumerosCumplenCondicion_OperacionEntrada = CantidadNumerosCondicion_OperacionEntrada;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                    CantidadSubNumerosCumplenCondicion_OperacionEntrada = CantidadNumerosValoresCondicion;
                                    break;
                            }

                            switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                        valorCondicion = false;
                                    break;
                            }

                            break;
                    }
                }
                else
                {
                    if (!CantidadNumeros_PorElemento & !CantidadNumeros_PorElemento_Valores)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (NumerosCumplenCondicion_Elemento == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (NumerosNoCumplenCondicion_Elemento > 0)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_OperacionEntrada = NumerosCumplenCondicion_Elemento;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_OperacionEntrada = NumerosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_OperacionEntrada = CantidadNumerosCondicion_OperacionEntrada;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_OperacionEntrada = CantidadNumerosValoresCondicion;
                                        break;
                                }

                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (NumerosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (NumerosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (NumerosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                            valorCondicion = false;
                                        break;
                                }

                                break;
                        }
                    }
                }
            }

            if (valorCondicion) //&&
                //(TipoElemento_Valores == TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.Valores_DesdeElementoOperacion |
                //TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion))
            {
                if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
                {
                    if ((CantidadNumeros_PorElemento & CantidadNumeros_PorElemento_Valores) ||
                        (CantidadNumeros_PorElemento & !CantidadNumeros_PorElemento_Valores) ||
                        (!CantidadNumeros_PorElemento & CantidadNumeros_PorElemento_Valores))
                    {
                        bool HayCantidadesCumpleCondicion = false;

                        foreach (var itemCantidad in CantidadesNumeros)
                        {
                            bool valorCondicionCantidad_Condicion = true;

                            if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores)
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                                {
                                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                        if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores == 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != itemCantidad.NumerosCumplenCondicion_Valores &&
                                            itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != -itemCantidad.NumerosNoCumplenCondicion_Valores)
                                            valorCondicionCantidad_Condicion = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosValoresCondicion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;
                                        }

                                        break;
                                }
                            }
                            else
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                                {
                                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                        if (itemCantidad.NumerosCumplenCondicion_Valores == 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.NumerosNoCumplenCondicion_Valores > 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosValoresCondicion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;
                                        }

                                        break;
                                }
                            }

                            bool valorCondicionCantidad_Valores = true;

                            if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores)
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                                {
                                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                        if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores == 0)
                                            valorCondicionCantidad_Valores = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != itemCantidad.NumerosCumplenCondicion_Valores &&
                                            itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != -itemCantidad.NumerosNoCumplenCondicion_Valores)
                                            valorCondicionCantidad_Valores = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosValoresCondicion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Valores = false;
                                                break;
                                        }

                                        break;
                                }
                            }
                            else
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                                {
                                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                        if (itemCantidad.NumerosCumplenCondicion_Valores == 0)
                                            valorCondicionCantidad_Valores = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.NumerosNoCumplenCondicion_Valores > 0)
                                            valorCondicionCantidad_Valores = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.NumerosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores = itemCantidad.CantidadNumerosValoresCondicion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores)
                                                    valorCondicionCantidad_Valores = false;
                                                break;
                                        }

                                        break;
                                }
                            }

                            if (valorCondicionCantidad_Condicion)
                                HayCantidadesCumpleCondicion = true;
                            else
                                if (CantidadNumeros_PorElemento)
                                QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, true, false, CantidadesNumeros);

                            if (valorCondicionCantidad_Valores)
                                HayCantidadesCumpleCondicion = true;
                            else
                                if (CantidadNumeros_PorElemento_Valores)
                                QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, false, true, CantidadesNumeros);
                        }

                        if (!HayCantidadesCumpleCondicion)
                            valorCondicion = false;
                    }

                    if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores != NumerosCumplenCondicion_Valores &&
                                    NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores != -NumerosNoCumplenCondicion_Valores)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores = NumerosCumplenCondicion_Elemento;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores = NumerosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosCondicion_OperacionEntrada;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosValoresCondicion;
                                        break;
                                }

                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores)
                                            valorCondicion = false;
                                        break;
                                }

                                break;
                        }
                    }
                    else
                    {
                        if (!CantidadNumeros_PorElemento & !CantidadNumeros_PorElemento_Valores)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (NumerosCumplenCondicion_Valores == 0)
                                        valorCondicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (NumerosNoCumplenCondicion_Valores > 0)
                                        valorCondicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = NumerosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosCondicion_OperacionEntrada;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (NumerosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (NumerosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (NumerosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores)
                                                valorCondicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }
                    }
                }
                else if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    //if ((CantidadTextosInformacion_PorElemento & !CantidadTextosInformacion_PorElemento_Valores) ||
                    //(CantidadTextosInformacion_PorElemento & CantidadTextosInformacion_PorElemento_Valores) ||
                    //(!CantidadTextosInformacion_PorElemento & CantidadTextosInformacion_PorElemento_Valores))
                    {
                        bool HayCantidadesCumpleCondicion = false;

                        foreach (var itemCantidad in CantidadesTextos)
                        {
                            bool valorCondicionCantidad_Condicion = true;

                            if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                        if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores == 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != itemCantidad.TextosCumplenCondicion_Valores &&
                                            itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != -itemCantidad.TextosNoCumplenCondicion_Valores)
                                            valorCondicionCantidad_Condicion = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion;
                                                break;
                                        }


                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;
                                        }

                                        break;
                                }
                            }
                            else
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                        if (itemCantidad.TextosCumplenCondicion_Valores == 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.TextosNoCumplenCondicion_Valores > 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion;
                                                break;
                                        }


                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.TextosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.TextosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.TextosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;
                                        }

                                        break;
                                }
                            }

                            bool valorCondicionCantidad_Valores = true;

                            if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                        if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores == 0)
                                            valorCondicionCantidad_Valores = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != itemCantidad.TextosCumplenCondicion_Valores &&
                                            itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != -itemCantidad.TextosNoCumplenCondicion_Valores)
                                            valorCondicionCantidad_Valores = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion;
                                                break;
                                        }


                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Valores = false;
                                                break;
                                        }

                                        break;
                                }
                            }
                            else
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                        if (itemCantidad.TextosCumplenCondicion_Valores == 0)
                                            valorCondicionCantidad_Valores = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.TextosNoCumplenCondicion_Valores > 0)
                                            valorCondicionCantidad_Valores = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion;
                                                break;
                                        }


                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.TextosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.TextosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.TextosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Valores = false;
                                                break;
                                        }

                                        break;
                                }
                            }

                            if (valorCondicionCantidad_Condicion)
                                HayCantidadesCumpleCondicion = true;
                            else
                                if (CantidadTextosInformacion_PorElemento)
                                QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, true, false, CantidadesTextos);

                            if (valorCondicionCantidad_Valores)
                                HayCantidadesCumpleCondicion = true;
                            else
                                if (CantidadTextosInformacion_PorElemento_Valores)
                                QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, false, true, CantidadesTextos);
                        }

                        if (!HayCantidadesCumpleCondicion)
                            valorCondicion = false;
                    }

                    if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores != NumerosCumplenCondicion_Valores &&
                                    NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores != -NumerosCumplenCondicion_Valores)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = NumerosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = NumerosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = CantidadNumerosCondicion_TextosInformacion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = CantidadNumerosValoresCondicion;
                                        break;
                                }


                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                                            valorCondicion = false;
                                        break;
                                }

                                break;
                        }
                    }
                    else
                    {
                        if (!CantidadTextosInformacion_PorElemento_Valores & !CantidadTextosInformacion_PorElemento)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (NumerosCumplenCondicion_Valores == 0)
                                        valorCondicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (NumerosNoCumplenCondicion_Valores > 0)
                                        valorCondicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = CantidadNumerosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = CantidadNumerosValoresCondicion;
                                            break;
                                    }


                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (NumerosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (NumerosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (NumerosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion)
                                                valorCondicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }
                    }


                    if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (TextosCumplenCondicion_Valores - TextosNoCumplenCondicion_Valores == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (TextosCumplenCondicion_Valores - TextosNoCumplenCondicion_Valores != TextosCumplenCondicion_Valores &&
                                    TextosCumplenCondicion_Valores - TextosNoCumplenCondicion_Valores != -TextosNoCumplenCondicion_Valores)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = CantidadTextosCondicion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = CantidadTextosValoresCondicion;
                                        break;
                                }


                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (TextosCumplenCondicion_Valores - TextosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (TextosCumplenCondicion_Valores - TextosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (TextosCumplenCondicion_Valores - TextosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;
                                }

                                break;
                        }
                    }
                    else
                    {
                        if (!CantidadTextosInformacion_PorElemento_Valores & !CantidadTextosInformacion_PorElemento)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (TextosCumplenCondicion_Valores == 0)
                                        valorCondicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (TextosNoCumplenCondicion_Valores > 0)
                                        valorCondicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = CantidadTextosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = CantidadTextosValoresCondicion;
                                            break;
                                    }


                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (TextosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (TextosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (TextosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }
                    }

                }
            }

            if (CumpleCondicion_ElementoSinNumeros &&
                                sinNumerosTextos)
                valorCondicion = true;

            if (CumpleCondicion_ElementoValores_SinNumeros &&
                                sinNumerosTextos_Valores)
                valorCondicion = true;

            if (valorCondicion && (ConsiderarIncluirCondicionesHijas ||
                    (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones)))
                EstablecerConsiderarOperando(ejecucion,
                    (operando != null && operando.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion)) ? operando : null,
                    (operando != null && operando.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion)) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null, numero);

            if (NegarCondicion)
                return !valorCondicion;
            else
                return valorCondicion;
        }

        private TipoOpcionSeleccionNumerosElemento_Condicion ObtenerOpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior(ElementoEjecucionCalculo elementoEjecucion,
            ElementoDiseñoOperacionAritmeticaEjecucion subElementoEjecucion)
        {
            var opcion = OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.FirstOrDefault(i => i.OperandoRelacionado_Ejecucion == elementoEjecucion |
            i.SubOperandoRelacionado_Ejecucion == subElementoEjecucion);

            if (opcion != null)
                return opcion.OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior;
            else
                return TipoOpcionSeleccionNumerosElemento_Condicion.Ninguna;
        }

        private bool VerificarSiOperandosCorresponden_AEjecucion(EjecucionCalculo ejecucion,
            ElementoEjecucionCalculo elementoOperando = null,
            ElementoDiseñoOperacionAritmeticaEjecucion subElementoOperando = null,
            EntidadNumero numeroOperando = null)
        {
            bool correspondeOperandos = false;

            switch (TipoElementoCondicion)
            {
                case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion:

                    var elementoEjecucionCondicion_Filtrar_Textos = ejecucion.ObtenerElementoEjecucion(OperandoCondicion);
                    var elementoEjecucionValores_Filtrar_Textos = ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores);
                    var subElementoEjecucion_Filtrar_Textos = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_TextosInformacion);
                    var subElementoEjecucionValores_Filtrar_Textos = ejecucion.ObtenerSubElementoEjecucion(SubElementoOperacion_Valores);

                    bool elementoOperandoContiene = false;

                    if (elementoEjecucionCondicion_Filtrar_Textos != null)
                    {
                        elementoOperandoContiene = ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionCondicion_Filtrar_Textos).Numeros.Contains(numeroOperando);
                    }

                    bool subElementoOperandoContiene = false;

                    if (subElementoEjecucion_Filtrar_Textos != null)
                        subElementoOperandoContiene = subElementoEjecucion_Filtrar_Textos.Numeros.Contains(numeroOperando);

                    bool elementoOperandoValoresContiene = false;

                    if (elementoEjecucionValores_Filtrar_Textos != null)
                    {
                        elementoOperandoValoresContiene = ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionValores_Filtrar_Textos).Numeros.Contains(numeroOperando);
                    }
                    else
                        elementoOperandoValoresContiene = true;

                        bool subElementoOperandoValoresContiene = false;

                    if (subElementoEjecucionValores_Filtrar_Textos != null)
                        subElementoOperandoValoresContiene = subElementoEjecucionValores_Filtrar_Textos.Numeros.Contains(numeroOperando);
                    else
                        subElementoOperandoValoresContiene = true;

                    if ((((elementoOperando != null && elementoEjecucionCondicion_Filtrar_Textos != null && (elementoOperando == elementoEjecucionCondicion_Filtrar_Textos | elementoOperandoContiene)) ||
                        (subElementoOperando != null && subElementoEjecucion_Filtrar_Textos != null && (subElementoOperando == subElementoEjecucion_Filtrar_Textos | subElementoOperandoContiene))) ||
                        ((elementoOperando == null && elementoEjecucionCondicion_Filtrar_Textos == null) ||
                        (subElementoOperando == null && subElementoEjecucion_Filtrar_Textos == null)))||

                    (((elementoOperando != null && (elementoOperando == elementoEjecucionValores_Filtrar_Textos | elementoOperandoValoresContiene)) ||
                    (subElementoOperando != null && (subElementoOperando == subElementoEjecucionValores_Filtrar_Textos | subElementoOperandoValoresContiene))) ||
                    ((elementoOperando == null && elementoEjecucionValores_Filtrar_Textos == null) ||
                        (subElementoOperando == null && subElementoEjecucionValores_Filtrar_Textos == null))))
                    {
                        correspondeOperandos = true;
                    }

                    break;

                case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada:

                    var elementoEjecucionCondicion_Filtrar = ejecucion.ObtenerElementoEjecucion(ElementoCondicion);
                    var elementoEjecucionValores_Filtrar = ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores_ElementoAsociado);
                    var subElementoEjecucion_Filtrar = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion);
                    var subElementoEjecucionValores_Filtrar = ejecucion.ObtenerSubElementoEjecucion(SubElementoOperacion_Valores);

                    bool elementoOperandoContiene2 = false;

                    if (elementoEjecucionCondicion_Filtrar != null)
                    {
                        elementoOperandoContiene2 = ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionCondicion_Filtrar).Numeros.Contains(numeroOperando);
                    }

                    bool subElementoOperandoContiene2 = false;

                    if (subElementoEjecucion_Filtrar != null)
                        subElementoOperandoContiene2 = subElementoEjecucion_Filtrar.Numeros.Contains(numeroOperando);

                    bool elementoOperandoValoresContiene2 = false;

                    if (elementoEjecucionValores_Filtrar != null)
                    {
                        elementoOperandoValoresContiene2 = ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionValores_Filtrar).Numeros.Contains(numeroOperando);
                    }
                    else
                        elementoOperandoValoresContiene2 = true;

                    bool subElementoOperandoValoresContiene2 = false;

                    if (subElementoEjecucionValores_Filtrar != null)
                        subElementoOperandoValoresContiene2 = subElementoEjecucionValores_Filtrar.Numeros.Contains(numeroOperando);
                    else
                        subElementoOperandoValoresContiene2 = true;

                    if ((((elementoOperando != null && elementoEjecucionCondicion_Filtrar != null && (elementoOperando == elementoEjecucionCondicion_Filtrar | elementoOperandoContiene2)) ||
                        (subElementoOperando != null && subElementoEjecucion_Filtrar != null && (subElementoOperando == subElementoEjecucion_Filtrar | subElementoOperandoContiene2))) ||
                        ((elementoOperando == null && elementoEjecucionCondicion_Filtrar == null) ||
                        (subElementoOperando == null && subElementoEjecucion_Filtrar == null)))||

                    (((elementoOperando != null && (elementoOperando == elementoEjecucionValores_Filtrar | elementoOperandoValoresContiene2)) ||
                    (subElementoOperando != null && (subElementoOperando == subElementoEjecucionValores_Filtrar | subElementoOperandoValoresContiene2))) ||
                    ((elementoOperando == null && elementoEjecucionValores_Filtrar == null) ||
                        (subElementoOperando == null && subElementoEjecucionValores_Filtrar == null))))
                    {
                        correspondeOperandos = true;
                    }

                    break;
            }

            return correspondeOperandos;
        }

        private List<int> ObtenerFilasCumplenCondicion_TextosValores_Involucrados(List<List<string>> filas , List<string> TextosInvolucrados)
        {
            var comparadorTextos = new ComparadorTextosInformacion(TipoOpcionCondicion_TextosInformacion,
                        ConsiderarTextosInformacionComoCantidades ? TipoOpcionCondicion_ElementoOperacionEntrada :
                        TipoOpcion_CondicionTextosInformacion_Implicacion.Ninguno,
                        Busqueda_TextoBusqueda_Ejecucion, BuscarCualquierTextoInformacion_TextoBusqueda,
                        QuitarEspaciosTemporalmente_CadenaCondicion);

            List<int> posiciones = new List<int>();

            int posicion = 0;
            foreach(var fila in filas)
            {
                if (comparadorTextos.Interseccion(fila, TextosInvolucrados, new List<string>()))
                    posiciones.Add(posicion);

                posicion++;
            }

            return posiciones;
        }

        private void EstablecerConsiderarOperando(EjecucionCalculo ejecucion,
            ElementoEjecucionCalculo elementoOperando = null,
            ElementoDiseñoOperacionAritmeticaEjecucion subElementoOperando = null,
            EntidadNumero numeroOperando = null)
        {
            if (VerificarSiOperandosCorresponden_AEjecucion(ejecucion,
                elementoOperando, subElementoOperando, numeroOperando))
            {
                if (ConsiderarOperandoCondicion_SiCumple)
                {
                    if (numeroOperando != null)
                        numeroOperando.ConsiderarOperandoCondicion_SiCumple = true;
                }
            }
        }

        private void LimpiarVariables_ElementosVinculados(bool seteoLimpiarTemp)
        {
            foreach (var itemCondicion in Condiciones)
                itemCondicion.LimpiarElementosNumerosVinculados_CondicionesAnteriores();

            LimpiarElementosNumerosVinculados_CondicionesAnteriores();

            if(seteoLimpiarTemp)
            {
                {
                    NumerosVinculados_CondicionAnterior_Temp.Clear();
                    OperandosVinculados_CondicionAnterior_Temp.Clear();
                }
            }
        }

        private void LimpiarTextosInformacion_CondicionesAnteriores(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion operando,
            EntidadNumero numero)
        {
            if (operacion != null)
            {
                foreach (var item in operacion.Numeros)
                {
                    item.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                }
            }

            if (operando != null)
            {
                foreach (var item in operando.Numeros)
                {
                    item.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                }
            }

            if (numero != null)
            {
                numero.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
            }
        }

        public void InicializarPosicionesElementosCondicion(EjecucionCalculo ejecucion, DiseñoCalculoTextosInformacion asignaciones)
        {
            //var elementosEntrada = asignaciones.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion).ToList();

            //foreach(var itemEntrada in elementosEntrada)
            //{
            //    itemEntrada.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
            //}

            var elementoCondicion = ejecucion.ObtenerElementoEjecucion(ElementoCondicion);
            if (elementoCondicion != null)
                elementoCondicion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;

            var operandoCondicion = ejecucion.ObtenerElementoEjecucion(OperandoCondicion);
            if (operandoCondicion != null)
                operandoCondicion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;

            var subOperandoCondicion_TextosInformacion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_TextosInformacion);
            if (subOperandoCondicion_TextosInformacion != null)
                subOperandoCondicion_TextosInformacion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;

            var subOperandoCondicion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion);
            if (subOperandoCondicion != null)
                subOperandoCondicion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;

            var elementoValores = ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores);
            if (elementoValores != null)
                elementoValores.PosicionActualNumero_CondicionesOperador_Implicacion = 0;

            var subOperandoValores = ejecucion.ObtenerSubElementoEjecucion(SubElementoOperacion_Valores);
            if (subOperandoValores != null)
                subOperandoValores.PosicionActualNumero_CondicionesOperador_Implicacion = 0;

            var elementoCondicionValores = ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores_ElementoAsociado);
            if (elementoCondicionValores != null)
                elementoCondicionValores.PosicionActualNumero_CondicionesOperador_Implicacion = 0;

            var subOperandoCondicion_Elemento = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_Elemento);
            if (subOperandoCondicion_Elemento != null)
                subOperandoCondicion_Elemento.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
        }

        public void AumentarPosicionesElementosCondicion(EjecucionCalculo ejecucion, ElementoOperacionAritmeticaEjecucion operacion, 
            DiseñoCalculoTextosInformacion asignaciones)
        {
            //var elementosEntrada = asignaciones.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == EntradaCondicion).ToList();

            //foreach (var itemEntrada in elementosEntrada)
            //{
            //    itemEntrada.PosicionActualNumero_CondicionesOperador_Implicacion++;
            //}

            var elementoCondicion = ejecucion.ObtenerElementoEjecucion(ElementoCondicion);
            if (elementoCondicion != null && elementoCondicion != operacion && 
                !operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Contains(elementoCondicion))
                elementoCondicion.PosicionActualNumero_CondicionesOperador_Implicacion++;

            var operandoCondicion = ejecucion.ObtenerElementoEjecucion(OperandoCondicion);
            if (operandoCondicion != null && operandoCondicion != operacion && 
                !operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Contains(operandoCondicion))
                operandoCondicion.PosicionActualNumero_CondicionesOperador_Implicacion++;

            var subOperandoCondicion_TextosInformacion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_TextosInformacion);
            if (subOperandoCondicion_TextosInformacion != null && 
                operacion.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Any(j => j == OperandoSubElemento_Condicion_TextosInformacion) && 
                !operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Any(i => i.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Any(j => j == OperandoSubElemento_Condicion_TextosInformacion)))
                subOperandoCondicion_TextosInformacion.PosicionActualNumero_CondicionesOperador_Implicacion++;

            var subOperandoCondicion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion);
            if (subOperandoCondicion != null && 
                operacion.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Any(j => j == OperandoSubElemento_Condicion) && 
                !operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Any(i => i.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Any(j => j == OperandoSubElemento_Condicion)))
                subOperandoCondicion.PosicionActualNumero_CondicionesOperador_Implicacion++;

            var elementoValores = ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores);
            if (elementoValores != null && elementoValores != operacion && 
                !operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Contains(elementoValores))
                elementoValores.PosicionActualNumero_CondicionesOperador_Implicacion++;

            var subOperandoValores = ejecucion.ObtenerSubElementoEjecucion(SubElementoOperacion_Valores);
            if (subOperandoValores != null && operacion.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Any(j => j == SubElementoOperacion_Valores) && 
                !operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Any(i => i.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Any(j => j == SubElementoOperacion_Valores)))
                subOperandoValores.PosicionActualNumero_CondicionesOperador_Implicacion++;

            var elementoCondicionValores = ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores_ElementoAsociado);
            if (elementoCondicionValores != null && elementoCondicionValores != operacion && 
                !operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Contains(elementoCondicionValores))
                elementoCondicionValores.PosicionActualNumero_CondicionesOperador_Implicacion++;

            var subOperandoCondicion_Elemento = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_Elemento);
            if (subOperandoCondicion_Elemento != null && 
                operacion.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Any(j => j == OperandoSubElemento_Condicion_Elemento) && 
                !operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Any(i => i.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).Any(j => j == OperandoSubElemento_Condicion_Elemento)))
                subOperandoCondicion_Elemento.PosicionActualNumero_CondicionesOperador_Implicacion++;

        }

        private void ActualizarElementosNumerosVinculados_CondicionesAnteriores(CondicionImplicacionTextosInformacion condicion)
        {
            OperandosVinculados_CondicionAnterior.AddRange(condicion.OperandosVinculados_CondicionAnterior);
            SubOperandosVinculados_CondicionAnterior.AddRange(condicion.SubOperandosVinculados_CondicionAnterior);
            ElementosVinculados_CondicionAnterior.AddRange(condicion.ElementosVinculados_CondicionAnterior);
            NumerosVinculados_CondicionAnterior.AddRange(condicion.NumerosVinculados_CondicionAnterior);
            OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.AddRange(condicion.OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior);

        }

        private void LimpiarElementosNumerosVinculados_CondicionesAnteriores()
        {
            OperandosVinculados_AgregarCondicionAnterior.Clear();
            OperandosVinculados_CondicionAnterior.Clear();
            SubOperandosVinculados_AgregarCondicionAnterior.Clear();
            SubOperandosVinculados_CondicionAnterior.Clear();
            ElementosVinculados_AgregarCondicionAnterior.Clear();
            ElementosVinculados_CondicionAnterior.Clear();
            NumerosVinculados_AgregarCondicionAnterior.Clear();
            NumerosVinculados_CondicionAnterior.Clear();
            OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Clear();
        }

        private void AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(List<string> TextosInformacionInvolucrados,
            List<string> TextosAnteriores,
            ElementoEntradaEjecucion elementoEjecucionCondicion_Valores_ConjuntoEntrada,
        ElementoOperacionAritmeticaEjecucion elementoEjecucionCondicion_Valores_Operacion,
        ElementoDiseñoOperacionAritmeticaEjecucion elementoEjecucionCondicion_Valores_SubOperacion,
        InformacionCantidadesTextosInformacion_CondicionImplicacion CantidadesTextos,
        string[] valoresCondicion)
        {
            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion |
                TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
            {
                if (elementoEjecucionCondicion_Valores_Operacion != null)
                {
                    var elementosVinculadosAgregar = ObtenerNumerosVinculados_ElementoCondicion_Valores(elementoEjecucionCondicion_Valores_Operacion, TextosInformacionInvolucrados, TextosAnteriores, valoresCondicion);

                    foreach (var item in elementosVinculadosAgregar)
                    {
                        if (!NumerosVinculados_CondicionAnterior.Contains(item))
                        {
                            NumerosVinculados_CondicionAnterior.Add(item);
                            NumerosVinculados_CondicionAnterior_Temp.Add(item);
                        }
                    }
                    
                    CantidadesTextos.NumerosAsociados_OperandoValores.AddRange(elementosVinculadosAgregar);

                    if (elementosVinculadosAgregar.Any() && !OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion_Valores_Operacion))
                    {
                        OperandosVinculados_CondicionAnterior.Add(elementoEjecucionCondicion_Valores_Operacion);
                        CantidadesTextos.OperandosAsociados_OperandoValores.Add(elementoEjecucionCondicion_Valores_Operacion);
                    }
                }

                if (elementoEjecucionCondicion_Valores_ConjuntoEntrada != null)
                {
                    var numerosVinculadosAgregar = ObtenerNumerosVinculados_ElementoCondicion_Valores(elementoEjecucionCondicion_Valores_ConjuntoEntrada, TextosInformacionInvolucrados, TextosAnteriores, valoresCondicion);
                    
                    foreach (var item in numerosVinculadosAgregar)
                    {
                        if (!NumerosVinculados_CondicionAnterior.Contains(item))
                        {
                            NumerosVinculados_CondicionAnterior.Add(item);
                            NumerosVinculados_CondicionAnterior_Temp.Add(item);
                        }
                    }

                    CantidadesTextos.NumerosAsociados_OperandoValores.AddRange(numerosVinculadosAgregar);

                    if (numerosVinculadosAgregar.Any() && !OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion_Valores_ConjuntoEntrada))
                    {
                        OperandosVinculados_CondicionAnterior.Add(elementoEjecucionCondicion_Valores_ConjuntoEntrada);
                        CantidadesTextos.OperandosAsociados_OperandoValores.Add(elementoEjecucionCondicion_Valores_ConjuntoEntrada);
                    }
                }

                if (elementoEjecucionCondicion_Valores_SubOperacion != null)
                {
                    var numerosVinculadosAgregar = ObtenerNumerosVinculados_ElementoCondicion_Valores(elementoEjecucionCondicion_Valores_SubOperacion, TextosInformacionInvolucrados, TextosAnteriores, valoresCondicion);

                    foreach (var item in numerosVinculadosAgregar)
                    {
                        if (!NumerosVinculados_CondicionAnterior.Contains(item))
                        {
                            NumerosVinculados_CondicionAnterior.Add(item);
                            NumerosVinculados_CondicionAnterior_Temp.Add(item);
                        }
                    }

                    CantidadesTextos.NumerosAsociados_OperandoValores.AddRange(numerosVinculadosAgregar);

                    if (numerosVinculadosAgregar.Any() && !SubOperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion_Valores_SubOperacion))
                    {
                        OperandosVinculados_CondicionAnterior.Add(elementoEjecucionCondicion_Valores_SubOperacion);
                        CantidadesTextos.OperandosAsociados_OperandoValores.Add(elementoEjecucionCondicion_Valores_SubOperacion);
                    }
                }

            }
        }

        private void QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(InformacionCantidadesTextosInformacion_CondicionImplicacion CantidadesTextos,
            bool QuitarOperandosCondicion, bool QuitarOperandosValores, List<InformacionCantidadesTextosInformacion_CondicionImplicacion> ListaCantidades)
        {
            if (QuitarOperandosCondicion)
            {
                var ListaNumeros = ObtenerListaNumerosCantidades_Condicion(ListaCantidades);
                var ListaElementos = ObtenerListaElementosCantidades_Condicion(ListaCantidades);

                while (CantidadesTextos.NumerosAsociados_OperandoCondicion.Any())
                {
                    NumerosVinculados_CondicionAnterior.Remove(CantidadesTextos.NumerosAsociados_OperandoCondicion.FirstOrDefault());
                    CantidadesTextos.NumerosAsociados_OperandoCondicion.Remove(CantidadesTextos.NumerosAsociados_OperandoCondicion.FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoCondicion.Any())
                {
                    ElementosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoCondicion.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoCondicion.Any(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))).FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoCondicion.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))).FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoCondicion.Any(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))).FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoCondicion.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))).FirstOrDefault());
                }
            }

            if (QuitarOperandosValores)
            {
                var ListaNumeros = ObtenerListaNumerosCantidades_Valores(ListaCantidades);
                var ListaElementos = ObtenerListaElementosCantidades_Valores(ListaCantidades);

                while (CantidadesTextos.NumerosAsociados_OperandoValores.Any())
                {
                    NumerosVinculados_CondicionAnterior.Remove(CantidadesTextos.NumerosAsociados_OperandoValores.FirstOrDefault());
                    CantidadesTextos.NumerosAsociados_OperandoValores.Remove(CantidadesTextos.NumerosAsociados_OperandoValores.FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoValores.Any())
                {
                    ElementosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoValores.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoValores.Any(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))).FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoValores.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))).FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoValores.Any(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))).FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoValores.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))).FirstOrDefault());
                }
            }
        }

        private List<ElementoEjecucionCalculo> ObtenerListaElementosCantidades_Valores(List<InformacionCantidadesTextosInformacion_CondicionImplicacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.OperandosAsociados_OperandoValores).ToList();
        }

        private List<EntidadNumero> ObtenerListaNumerosCantidades_Valores(List<InformacionCantidadesTextosInformacion_CondicionImplicacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.NumerosAsociados_OperandoValores).ToList();
        }

        private List<ElementoEjecucionCalculo> ObtenerListaElementosCantidades_Condicion(List<InformacionCantidadesTextosInformacion_CondicionImplicacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.OperandosAsociados_OperandoCondicion).ToList();
        }

        private List<EntidadNumero> ObtenerListaNumerosCantidades_Condicion(List<InformacionCantidadesTextosInformacion_CondicionImplicacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.NumerosAsociados_OperandoCondicion).ToList();
        }

        private void QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(InformacionCantidadesNumerosInformacion_CondicionImplicacion CantidadesTextos,
            bool QuitarOperandosCondicion, bool QuitarOperandosValores, List<InformacionCantidadesNumerosInformacion_CondicionImplicacion> ListaCantidades)
        {
            if (QuitarOperandosCondicion)
            {
                var ListaNumeros = ObtenerListaNumerosCantidades_Condicion(ListaCantidades);
                var ListaElementos = ObtenerListaElementosCantidades_Condicion(ListaCantidades);

                while (CantidadesTextos.NumerosAsociados_OperandoCondicion.Any())
                {
                    NumerosVinculados_CondicionAnterior.Remove(CantidadesTextos.NumerosAsociados_OperandoCondicion.FirstOrDefault());
                    CantidadesTextos.NumerosAsociados_OperandoCondicion.Remove(CantidadesTextos.NumerosAsociados_OperandoCondicion.FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoCondicion.Any())
                {
                    ElementosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoCondicion.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoCondicion.Any(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))).FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoCondicion.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))).FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoCondicion.Any(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))).FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoCondicion.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))).FirstOrDefault());
                }
            }

            if (QuitarOperandosValores)
            {
                var ListaNumeros = ObtenerListaNumerosCantidades_Valores(ListaCantidades);
                var ListaElementos = ObtenerListaElementosCantidades_Valores(ListaCantidades);

                while (CantidadesTextos.NumerosAsociados_OperandoValores.Any())
                {
                    NumerosVinculados_CondicionAnterior.Remove(CantidadesTextos.NumerosAsociados_OperandoValores.FirstOrDefault());
                    CantidadesTextos.NumerosAsociados_OperandoValores.Remove(CantidadesTextos.NumerosAsociados_OperandoValores.FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoValores.Any())
                {
                    ElementosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoValores.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoValores.Any(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))).FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoValores.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))).FirstOrDefault());
                }

                while (CantidadesTextos.OperandosAsociados_OperandoValores.Any(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))).FirstOrDefault());
                    CantidadesTextos.OperandosAsociados_OperandoValores.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))).FirstOrDefault());
                }
            }
        }

        private List<ElementoEjecucionCalculo> ObtenerListaElementosCantidades_Valores(List<InformacionCantidadesNumerosInformacion_CondicionImplicacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.OperandosAsociados_OperandoValores).ToList();
        }

        private List<EntidadNumero> ObtenerListaNumerosCantidades_Valores(List<InformacionCantidadesNumerosInformacion_CondicionImplicacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.NumerosAsociados_OperandoValores).ToList();
        }

        private List<ElementoEjecucionCalculo> ObtenerListaElementosCantidades_Condicion(List<InformacionCantidadesNumerosInformacion_CondicionImplicacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.OperandosAsociados_OperandoCondicion).ToList();
        }

        private List<EntidadNumero> ObtenerListaNumerosCantidades_Condicion(List<InformacionCantidadesNumerosInformacion_CondicionImplicacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.NumerosAsociados_OperandoCondicion).ToList();
        }
        private List<EntidadNumero> ObtenerNumerosVinculados_ElementoCondicion_Valores(ElementoEntradaEjecucion elemento,
            List<string> TextosInformacionInvolucrados, List<string> TextosAnteriores, string[] valoresCondicion)
        {
            List<EntidadNumero> numeros = new List<EntidadNumero>();
            var comparadorTextos = new ComparadorTextosInformacion(TipoOpcionCondicion_TextosInformacion,
                        ConsiderarTextosInformacionComoCantidades ? TipoOpcionCondicion_ElementoOperacionEntrada :
                        TipoOpcion_CondicionTextosInformacion_Implicacion.Ninguno,
                        Busqueda_TextoBusqueda_Ejecucion, BuscarCualquierTextoInformacion_TextoBusqueda,
                        QuitarEspaciosTemporalmente_CadenaCondicion);

            foreach (var itemNumero in elemento.Numeros)
            {
                if (comparadorTextos.Interseccion(TextosInformacionInvolucrados, itemNumero.Textos, TextosAnteriores) &&
                    !valoresCondicion.Except(itemNumero.Textos).Any())
                    numeros.Add(itemNumero);
            }

            return numeros;
        }

        private List<EntidadNumero> ObtenerNumerosVinculados_ElementoCondicion_Valores(ElementoOperacionAritmeticaEjecucion elemento,
            List<string> TextosInformacionInvolucrados, List<string> TextosAnteriores, string[] valoresCondicion)
        {
            List<EntidadNumero> elementos = new List<EntidadNumero>();
            var comparadorTextos = new ComparadorTextosInformacion(TipoOpcionCondicion_TextosInformacion,
                        ConsiderarTextosInformacionComoCantidades ? TipoOpcionCondicion_ElementoOperacionEntrada :
                        TipoOpcion_CondicionTextosInformacion_Implicacion.Ninguno,
                        Busqueda_TextoBusqueda_Ejecucion, BuscarCualquierTextoInformacion_TextoBusqueda,
                        QuitarEspaciosTemporalmente_CadenaCondicion);

            foreach (var itemNumero in elemento.Numeros)
            {
                if (comparadorTextos.Interseccion(TextosInformacionInvolucrados, itemNumero.Textos, TextosAnteriores) &&
                    !valoresCondicion.Except(itemNumero.Textos).Any())
                    elementos.Add(itemNumero);
            }

            return elementos;
        }

        private List<EntidadNumero> ObtenerNumerosVinculados_ElementoCondicion_Valores(ElementoDiseñoOperacionAritmeticaEjecucion elemento,
            List<string> TextosInformacionInvolucrados, List<string> TextosAnteriores, string[] valoresCondicion)
        {
            List<EntidadNumero> numeros = new List<EntidadNumero>();
            var comparadorTextos = new ComparadorTextosInformacion(TipoOpcionCondicion_TextosInformacion,
                        ConsiderarTextosInformacionComoCantidades ? TipoOpcionCondicion_ElementoOperacionEntrada :
                        TipoOpcion_CondicionTextosInformacion_Implicacion.Ninguno,
                        Busqueda_TextoBusqueda_Ejecucion, BuscarCualquierTextoInformacion_TextoBusqueda,
                        QuitarEspaciosTemporalmente_CadenaCondicion);

            foreach (var itemNumero in elemento.Numeros)
            {
                if (comparadorTextos.Interseccion(TextosInformacionInvolucrados, itemNumero.Textos, TextosAnteriores) &&
                    !valoresCondicion.Except(itemNumero.Textos).Any())
                    numeros.Add(itemNumero);
            }

            return numeros;
        }

        public void AgregarCondicionElementoDiseñoCondicion_Condiciones(ref List<DiseñoOperacion> elementos)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.AgregarCondicionElementoDiseñoCondicion_Condiciones(ref elementos);
            }

            if (this.ElementoCondicion != null)
                elementos.Add(ElementoCondicion);

            if (this.ElementoOperacion_Valores != null)
                elementos.Add(ElementoOperacion_Valores);

            if (this.OperandoCondicion != null)
                elementos.Add(OperandoCondicion);
        }

        public void AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref List<DiseñoElementoOperacion> elementos)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref elementos);
            }

            if (OperandoSubElemento_Condicion != null)
                elementos.Add(OperandoSubElemento_Condicion);

            if (OperandoSubElemento_Condicion_TextosInformacion != null)
                elementos.Add(OperandoSubElemento_Condicion_TextosInformacion);
        }

        public bool CompararCantidades(int x, int y, TipoOpcion_CondicionTextosInformacion_Implicacion TipoOpcion)
        {
            switch (TipoOpcion)
            {
                case TipoOpcion_CondicionTextosInformacion_Implicacion.Contiene:
                    return (x.ToString().Trim().ToLower().Contains(y.ToString().Trim().ToLower()));


                case TipoOpcion_CondicionTextosInformacion_Implicacion.EsParteDe:
                    return (y.ToString().Trim().ToLower().Contains(x.ToString().Trim().ToLower()));

                case TipoOpcion_CondicionTextosInformacion_Implicacion.EmpiezaCon:
                    return (x.ToString().Trim().ToLower().StartsWith(y.ToString().Trim().ToLower()));

                case TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA:
                case TipoOpcion_CondicionTextosInformacion_Implicacion.EsIgualA:
                    return (x == y);

                case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorOIgualQue:
                    return (x >= y);

                case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorQue:
                    return (x > y);

                case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorOIgualQue:
                    return (x <= y);

                case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorQue:
                    return (x < y);

                case TipoOpcion_CondicionTextosInformacion_Implicacion.NoContiene:
                    return (!x.ToString().Trim().ToLower().Contains(y.ToString().Trim().ToLower()));

                case TipoOpcion_CondicionTextosInformacion_Implicacion.TerminaCon:
                    return (x.ToString().Trim().ToLower().EndsWith(y.ToString().Trim().ToLower()));

            }

            return false;
        }

        private int ObtenerPosicionCantidades_CondicionEjecucion(int PosicionActual, TipoOpcionSeleccionNumerosElemento_Condicion Opcion, int CantidadTotal)
        {
            int indicePosicion = PosicionActual;

            switch (Opcion)
            {
                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                    if (PosicionActual > 0)
                        indicePosicion = PosicionActual - 1;
                    break;

                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                    if (PosicionActual < CantidadTotal - 1)
                        indicePosicion = PosicionActual + 1;
                    break;

                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                    if(CantidadTotal > 0)
                        indicePosicion = 0;
                    break;

                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:
                    if(CantidadTotal > 1)
                        indicePosicion = 1;
                    break;

                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                    if (CantidadTotal > 1)
                        indicePosicion = CantidadTotal / 2;
                    break;

                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                    if (CantidadTotal - 2 >= 0)
                        indicePosicion = CantidadTotal - 2;
                    break;

                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                    if (CantidadTotal - 1 >= 0)
                        indicePosicion = CantidadTotal - 1;
                    break;
            }

            return indicePosicion;
        }

        public void PrepararTextosBusquedas()
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.PrepararTextosBusquedas();
            }

            Busqueda_TextoBusqueda_Ejecucion = new ElementoInternetOrigenDatosEjecucion(0, 0, 0, 0);
            Busqueda_TextoBusqueda_Ejecucion.TipoOrigenDatos = TipoOrigenDatos.DesdeInternet;

            if (Busqueda_TextoBusqueda != null)
            {
                BusquedaArchivoEjecucion busqueda = new BusquedaArchivoEjecucion();
                busqueda.TextoBusquedaNumero = Busqueda_TextoBusqueda.TextoBusquedaNumero;
                busqueda.TextoBusquedaNumero = busqueda.TextoBusquedaNumero.Replace("\r", string.Empty);
                busqueda.Nombre = Busqueda_TextoBusqueda.Nombre;
                busqueda.Descripcion = Busqueda_TextoBusqueda.Descripcion;
                busqueda.FinalizacionBusqueda = Busqueda_TextoBusqueda.FinalizacionBusqueda;
                busqueda.BusquedaRelacionada_Diseño = Busqueda_TextoBusqueda;

                Busqueda_TextoBusqueda_Ejecucion.Busquedas.Add(busqueda);
            }
        }

        public bool VerificarOpcionPosicionActual(DiseñoOperacion operando)
        {
            List<CondicionImplicacionTextosInformacion> condicionesAsociadasOperando = new List<CondicionImplicacionTextosInformacion>();
            List<CondicionImplicacionTextosInformacion> condicionesAsociadasOperando2 = new List<CondicionImplicacionTextosInformacion>();

            ObtenerCondicionOperandoCondicion_Condiciones_VerificarPosicionActual(ref condicionesAsociadasOperando, operando);
            ObtenerCondicionElementoCondicion_Condiciones_VerificarPosicionActual(ref condicionesAsociadasOperando2, operando);

            return condicionesAsociadasOperando.Any() | condicionesAsociadasOperando2.Any();            
        }
    }

    public class ComparadorTextosInformacion
    {
        public TipoOpcionImplicacion_AsignacionTextoInformacion TipoOpcionCondicion_TextosInformacion { get; set; }
        public TipoOpcion_CondicionTextosInformacion_Implicacion TipoOpcionCondicion_Cantidades { get; set; }
        public List<string> TextosInformacionInvolucrados { get; set; }
        public List<string> TextosCumplenCondicion { get; set; }
        public List<string> TextosNoCumplenCondicion { get; set; }
        List<string> TextosLista;
        ElementoInternetOrigenDatosEjecucion BusquedaEjecucion;
        bool BuscarCualquierTextoInformacion;
        public bool QuitarEspaciosTemporalmente_CadenaCondicion { get; set; }

        public ComparadorTextosInformacion(TipoOpcionImplicacion_AsignacionTextoInformacion tipoOpcion,
            TipoOpcion_CondicionTextosInformacion_Implicacion tipoOpcion_Cantidades,
            ElementoInternetOrigenDatosEjecucion BusquedaEjecucion,
            bool BuscarCualquierTextoInformacion,
            bool quitarEspaciosTemporalmente_CadenaCondicion)
        {
            TipoOpcionCondicion_TextosInformacion = tipoOpcion;
            TipoOpcionCondicion_Cantidades = tipoOpcion_Cantidades;
            TextosInformacionInvolucrados = new List<string>();
            TextosCumplenCondicion = new List<string>();
            TextosNoCumplenCondicion = new List<string>();
            this.BusquedaEjecucion = BusquedaEjecucion;
            this.BuscarCualquierTextoInformacion = BuscarCualquierTextoInformacion;
            QuitarEspaciosTemporalmente_CadenaCondicion = quitarEspaciosTemporalmente_CadenaCondicion;
        }
        private bool Comparar(string x, string y, List<string> TextosAnteriores)
        {
            if ((!string.IsNullOrEmpty(x) &
                !string.IsNullOrEmpty(y)) ||
                (!string.IsNullOrEmpty(x) &&
                string.IsNullOrEmpty(y) &&
                BuscarCualquierTextoInformacion) ||
                (!string.IsNullOrEmpty(x) &
                (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.EsSoloNumero |
                TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.EsTexto |
                TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.NoTieneNumeros |
                TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaSoloLetras |
                TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaSoloSimbolos |
                TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaNumeros)
                ))
            {
                int valorNumero = 0;

                if(QuitarEspaciosTemporalmente_CadenaCondicion)
                {
                    x = x.Replace(" ", string.Empty);
                    y = y.Replace(" ", string.Empty);
                }

                if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoIgual |
                    TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoDistinto |
                    TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorQue |
                    TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorQue |
                    TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorIgualQue |
                    TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorIgualQue)
                {
                    if (!int.TryParse(y.Replace("\t", string.Empty).Trim().ToLower(), out valorNumero))
                        return false;
                }

                bool condicionCumple = false;

                switch (TipoOpcionCondicion_TextosInformacion)
                {
                    case TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaTexto:
                        condicionCumple = x.Replace("\t", string.Empty).Trim().ToLower().Contains(y.Replace("\t", string.Empty).Trim().ToLower());
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.NoContengaTexto:
                        condicionCumple = !x.Replace("\t", string.Empty).Trim().ToLower().Contains(y.Replace("\t", string.Empty).Trim().ToLower());
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.EsParteDe:
                        condicionCumple = y.Replace("\t", string.Empty).Trim().ToLower().Contains(x.Replace("\t", string.Empty).Trim().ToLower());
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.NoEsParteDe:
                        condicionCumple = !y.Replace("\t", string.Empty).Trim().ToLower().Contains(x.Replace("\t", string.Empty).Trim().ToLower());
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.EmpiecenCon:
                        condicionCumple = x.Replace("\t", string.Empty).Trim().ToLower().StartsWith(y.Replace("\t", string.Empty).Trim().ToLower());
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.TerminenCon:
                        condicionCumple = x.Replace("\t", string.Empty).Trim().ToLower().EndsWith(y.Replace("\t", string.Empty).Trim().ToLower());
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto:
                    case TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual:
                        condicionCumple = x.Replace("\t", string.Empty).Trim().ToLower().Equals(y.Replace("\t", string.Empty).Trim().ToLower());
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoIgual:
                        condicionCumple = (TextosLista.IndexOf(x) == valorNumero - 1);
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoDistinto:
                        condicionCumple = !(TextosLista.IndexOf(x) == valorNumero - 1);
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorQue:
                        condicionCumple = (TextosLista.IndexOf(x) > valorNumero - 1);
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorQue:
                        condicionCumple = (TextosLista.IndexOf(x) < valorNumero - 1);
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorIgualQue:
                        condicionCumple = (TextosLista.IndexOf(x) >= valorNumero - 1);
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorIgualQue:
                        condicionCumple = (TextosLista.IndexOf(x) <= valorNumero - 1);
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.CoincidaTextoBusqueda:
                        condicionCumple = RealizarBusqueda(x, y, BuscarCualquierTextoInformacion);
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.EsSoloNumero:

                        double num = 0;
                        if(double.TryParse(x, out num))
                        {
                            condicionCumple = true;
                        }
                        else
                        {
                            condicionCumple = false;
                        }

                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.EsTexto:

                        num = 0;
                        if (double.TryParse(x, out num))
                        {
                            condicionCumple = false;
                        }
                        else
                        {
                            condicionCumple = true;
                        }

                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.NoTieneNumeros:

                        condicionCumple = true;

                        foreach(char caracter in x)
                        {
                            if (char.IsDigit(caracter))
                                condicionCumple = false;
                        }

                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaSoloLetras:
                        condicionCumple = true;

                        foreach (char caracter in x)
                        {
                            if (!char.IsPunctuation(caracter) &&
                                !char.IsLetter(caracter) &&
                                !char.IsWhiteSpace(caracter))
                                condicionCumple = false;
                        }
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaSoloSimbolos:
                        condicionCumple = true;

                        foreach (char caracter in x)
                        {
                            if (!char.IsSymbol(caracter) &&
                                !char.IsWhiteSpace(caracter) &&
                                !char.IsPunctuation(caracter))
                                condicionCumple = false;
                        }
                        break;

                    case TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaNumeros:
                        condicionCumple = false;

                        foreach (char caracter in x)
                        {
                            if (char.IsDigit(caracter))
                                condicionCumple = true;
                        }
                        break;
                }

                if (TipoOpcionCondicion_Cantidades == TipoOpcion_CondicionTextosInformacion_Implicacion.Ninguno)
                    return condicionCumple;
                else
                {
                    if (condicionCumple)
                    {
                        double xNumero = 0;
                        double yNumero = 0;

                        if (double.TryParse(x, out xNumero) &&
                            double.TryParse(y, out yNumero))
                        {

                            switch (TipoOpcionCondicion_Cantidades)
                            {
                                case TipoOpcion_CondicionTextosInformacion_Implicacion.Contiene:
                                    return (xNumero.ToString().Trim().ToLower().Contains(yNumero.ToString().Trim().ToLower()));

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.EsParteDe:
                                    return (yNumero.ToString().Trim().ToLower().Contains(xNumero.ToString().Trim().ToLower()));

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.EmpiezaCon:
                                    return (xNumero.ToString().Trim().ToLower().StartsWith(yNumero.ToString().Trim().ToLower()));

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA:
                                case TipoOpcion_CondicionTextosInformacion_Implicacion.EsIgualA:
                                    return (xNumero == yNumero);

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorOIgualQue:
                                    return (xNumero >= yNumero);

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorQue:
                                    return (xNumero > yNumero);

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorOIgualQue:
                                    return (xNumero <= yNumero);

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorQue:
                                    return (xNumero < yNumero);

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.NoContiene:
                                    return (!xNumero.ToString().Trim().ToLower().Contains(yNumero.ToString().Trim().ToLower()));

                                case TipoOpcion_CondicionTextosInformacion_Implicacion.TerminaCon:
                                    return (xNumero.ToString().Trim().ToLower().EndsWith(yNumero.ToString().Trim().ToLower()));
                            }
                        }
                        else
                            return false;
                    }
                    else
                        return condicionCumple;
                }
            }

            return false;
        }

        public bool Interseccion(IEnumerable<string> lista1, IEnumerable<string> lista2,
            List<string> TextosAnteriores)
        {
            TextosLista = lista1.ToList();

            int CantidadTextos_Cumplen = TextosAnteriores.Count;

            foreach (var item1 in lista1)
            {
                foreach (var item2 in lista2)
                {
                    if (Comparar(item1, item2, TextosAnteriores))
                    {
                        TextosAnteriores.Add(item1);
                        //return true;
                    }
                }
            }

            if (TextosAnteriores.Count > CantidadTextos_Cumplen)
                return true;
            else
                return false;
        }

        public int ContarInterseccion(IEnumerable<string> lista1, IEnumerable<string> lista2, List<string> TextosAnteriores,
            bool contarElementosSegundaLista = false)
        {
            int contarIguales = 0;
            TextosCumplenCondicion.Clear();
            TextosNoCumplenCondicion.Clear();

            TextosLista = lista1.ToList();

            if (lista2.Any(i => !string.IsNullOrEmpty(i)))
            {
                foreach (var item1 in lista1)
                {
                    foreach (var item2 in lista2)
                    {
                        if (Comparar(item1, item2, TextosAnteriores))
                        {
                            //if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual)
                            //{
                            TextosInformacionInvolucrados.Add(item1);
                            //TextosInformacionInvolucrados.Add(item2);//+ "|" + TipoOpcionCondicion_TextosInformacion.ToString() + "|");
                            contarIguales++;

                            if (contarElementosSegundaLista)
                            {
                                if (!TextosCumplenCondicion.Contains(item2))
                                    TextosCumplenCondicion.Add(item2);
                            }
                            else
                            {
                                if (!TextosCumplenCondicion.Contains(item1))
                                    TextosCumplenCondicion.Add(item1);
                            }
                            //}
                        }
                        //else
                        //{
                        //    if (!TextosNoCumplenCondicion.Contains(item2))
                        //        TextosNoCumplenCondicion.Add(item2);
                        //    //    //if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual)
                        //    //    //{
                        //    //        if (!TextosNoCumplenCondicion.Contains(item1))
                        //    //            TextosNoCumplenCondicion.Add(item1);

                        //    //        if (!TextosCumplenCondicion.Contains(item2))
                        //    //            TextosCumplenCondicion.Add(item2);
                        //    //    //}
                        //}
                    }
                }
            }
            else
            {
                foreach (var item1 in lista1)
                {
                    if (Comparar(item1, string.Empty, TextosAnteriores))
                    {
                        //if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual)
                        //{
                        TextosInformacionInvolucrados.Add(item1);

                        if (!TextosCumplenCondicion.Contains(item1))
                            TextosCumplenCondicion.Add(item1);
                    }
                }
            }

            if (contarElementosSegundaLista)
            {
                if (lista2.Any(i => !string.IsNullOrEmpty(i)))
                {
                    foreach (var item2 in lista2)
                    {
                        if (!TextosCumplenCondicion.Contains(item2))
                        {
                            if (!TextosNoCumplenCondicion.Contains(item2))
                                TextosNoCumplenCondicion.Add(item2);
                        }
                    }
                }
            }
            else
            {
                foreach (var item1 in lista1)
                {
                    if (!TextosCumplenCondicion.Contains(item1))
                    {
                        if (!TextosNoCumplenCondicion.Contains(item1))
                            TextosNoCumplenCondicion.Add(item1);
                    }
                }
            }

            return contarIguales;
        }

        private bool RealizarBusqueda(string x, string y, bool BuscarCualquierTextoInformacion)
        {
            if (BusquedaEjecucion != null && BusquedaEjecucion.Busquedas != null && BusquedaEjecucion.Busquedas.Any())
            {
                //BusquedaEjecucion.Busquedas.FirstOrDefault().TextosInformacion.Clear();
                //BusquedaEjecucion.Busquedas.FirstOrDefault().BusquedaValida = false;
                BusquedaArchivoEjecucion Busqueda = BusquedaEjecucion.Busquedas.FirstOrDefault().ReplicarBusqueda();
                BusquedaEjecucion.IndiceProcesamientoTexto = 0;

                BusquedaEjecucion.ContenidoTexto = x.Replace("\t", string.Empty).Trim().ToLower();

                bool continuar = true;
                while (continuar)
                {
                    try
                    {
                        continuar = Busqueda.ProcesarTextoBusqueda(BusquedaEjecucion, true);
                    }
                    catch (Exception e)
                    {

                    }

                    Busqueda.ProcesarValidacion();

                    if (Busqueda.BusquedaValida || (continuar && !Busqueda.BusquedaValida))
                    {
                        if (!Busqueda.BusquedaIniciada) Busqueda.BusquedaIniciada = true;
                        
                    }

                    if (Busqueda.LecturaTerminada)
                    {
                        continuar = false;
                    }
                }

                if (Busqueda.BusquedaValida &&
                    ((!BuscarCualquierTextoInformacion && Busqueda.TextosInformacion.Any(i => i.Replace("\t", string.Empty).Trim().ToLower().Equals(y.Replace("\t", string.Empty).Trim().ToLower()))) ||
                    (BuscarCualquierTextoInformacion && Busqueda.TextosInformacion.Any())))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }

    public class ComparadorNumeros_Condicion : IEqualityComparer<EntidadNumero>
    {
        public TipoOpcion_CondicionTextosInformacion_Implicacion TipoOpcionCondicion_ElementoOperacionEntrada { get; set; }
        public TipoOpcionPosicion OpcionValoresPosicion { get; set; }
        public ComparadorNumeros_Condicion(TipoOpcion_CondicionTextosInformacion_Implicacion tipoOpcion,
            TipoOpcionPosicion OpcionValoresPosicion = TipoOpcionPosicion.Ninguna)
        {
            TipoOpcionCondicion_ElementoOperacionEntrada = tipoOpcion;
            this.OpcionValoresPosicion = OpcionValoresPosicion;
        }
        public bool Equals(EntidadNumero x, EntidadNumero y)
        {
            if (x != null &&
                y != null)
            {
                if (OpcionValoresPosicion == TipoOpcionPosicion.Ninguna)
                {
                    switch (TipoOpcionCondicion_ElementoOperacionEntrada)
                    {
                        case TipoOpcion_CondicionTextosInformacion_Implicacion.Contiene:
                            return (x.Numero.ToString().Trim().ToLower().Contains(y.Numero.ToString().Trim().ToLower()));

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsParteDe:
                            return (y.Numero.ToString().Trim().ToLower().Contains(x.Numero.ToString().Trim().ToLower()));

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EmpiezaCon:
                            return (x.Numero.ToString().Trim().ToLower().StartsWith(y.Numero.ToString().Trim().ToLower()));

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA:
                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsIgualA:
                            return (x.Numero == y.Numero);

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorOIgualQue:
                            return (x.Numero >= y.Numero);

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorQue:
                            return (x.Numero > y.Numero);

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorOIgualQue:
                            return (x.Numero <= y.Numero);

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorQue:
                            return (x.Numero < y.Numero);

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.NoContiene:
                            return (!x.Numero.ToString().Trim().ToLower().Contains(y.Numero.ToString().Trim().ToLower()));

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.TerminaCon:
                            return (x.Numero.ToString().Trim().ToLower().EndsWith(y.Numero.ToString().Trim().ToLower()));

                    }
                }
                else
                {
                    bool valorCondicion_Numeros = false;

                    switch (TipoOpcionCondicion_ElementoOperacionEntrada)
                    {
                        case TipoOpcion_CondicionTextosInformacion_Implicacion.Contiene:
                            valorCondicion_Numeros = (x.Numero.ToString().Trim().ToLower().Contains(y.Numero.ToString().Trim().ToLower()));
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsParteDe:
                            valorCondicion_Numeros =  (y.Numero.ToString().Trim().ToLower().Contains(x.Numero.ToString().Trim().ToLower()));
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EmpiezaCon:
                            valorCondicion_Numeros =  (x.Numero.ToString().Trim().ToLower().StartsWith(y.Numero.ToString().Trim().ToLower()));
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA:
                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsIgualA:
                            valorCondicion_Numeros =  (x.Numero == y.Numero);
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorOIgualQue:
                            valorCondicion_Numeros =  (x.Numero >= y.Numero);
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorQue:
                            valorCondicion_Numeros =  (x.Numero > y.Numero);
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorOIgualQue:
                            valorCondicion_Numeros =  (x.Numero <= y.Numero);
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorQue:
                            valorCondicion_Numeros =  (x.Numero < y.Numero);
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.NoContiene:
                            valorCondicion_Numeros =  (!x.Numero.ToString().Trim().ToLower().Contains(y.Numero.ToString().Trim().ToLower()));
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.TerminaCon:
                            valorCondicion_Numeros =  (x.Numero.ToString().Trim().ToLower().EndsWith(y.Numero.ToString().Trim().ToLower()));
                            break;

                    }

                    if (valorCondicion_Numeros)
                    {
                        switch (OpcionValoresPosicion)
                        {
                            case TipoOpcionPosicion.PosicionPrimera:
                                return y.Textos.Contains("primera");

                            case TipoOpcionPosicion.PosicionSegunda:
                                return y.Textos.Contains("segunda");

                            case TipoOpcionPosicion.PosicionMitad:
                                return y.Textos.Contains("mitad");

                            case TipoOpcionPosicion.PosicionPenultima:
                                return y.Textos.Contains("penultima");

                            case TipoOpcionPosicion.PosicionUltima:
                                return y.Textos.Contains("ultima");
                        }
                    }
                    else
                        return false;
                }
            }

            return false;
        }

        public int GetHashCode(EntidadNumero obj)
        {
            return obj.GetHashCode();
        }
    }

    public class DuplaTextosInformacion_ProcesamientoCondicionImplicacion
    {
        public List<string> TextosInformacion_CumplenCondiciones_Anteriores_Entrada { get; set; }
        public List<string> TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_ElementoCondicion { get; set; }

        public DuplaTextosInformacion_ProcesamientoCondicionImplicacion()
        {
            TextosInformacion_CumplenCondiciones_Anteriores_Entrada = new List<string>();
            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_ElementoCondicion = new List<string>();
        }

        public DuplaTextosInformacion_ProcesamientoCondicionImplicacion(List<string> TextosInformacionEntrada, List<string> TextosInformacion_Iteracion_ElementoCondicion)
        {
            TextosInformacion_CumplenCondiciones_Anteriores_Entrada = TextosInformacionEntrada.ToList();
            TextosInformacion_CumplenCondiciones_Anteriores_Iteracion_ElementoCondicion = TextosInformacion_Iteracion_ElementoCondicion;
        }
    }

    public class InformacionCantidadesTextosInformacion_CondicionImplicacion
    {
        public int CantidadTextosCondicion { get; set; }
        public int CantidadTextosValoresCondicion { get; set; }
        public int TextosCumplenCondicion_TextosInformacion { get; set; }
        public int TextosNoCumplenCondicion_TextosInformacion { get; set; }

        public int TextosCumplenCondicion_Valores { get; set; }
        public int TextosNoCumplenCondicion_Valores { get; set; }
        public List<EntidadNumero> NumerosAsociados_OperandoCondicion { get; set; }
        public List<ElementoEjecucionCalculo> OperandosAsociados_OperandoCondicion { get; set; }
        public List<EntidadNumero> NumerosAsociados_OperandoValores { get; set; }
        public List<ElementoEjecucionCalculo> OperandosAsociados_OperandoValores { get; set; }

        public InformacionCantidadesTextosInformacion_CondicionImplicacion()
        {
            NumerosAsociados_OperandoCondicion = new List<EntidadNumero>();
            OperandosAsociados_OperandoCondicion = new List<ElementoEjecucionCalculo>();

            NumerosAsociados_OperandoValores = new List<EntidadNumero>();
            OperandosAsociados_OperandoValores = new List<ElementoEjecucionCalculo>();
        }
    }

    public class InformacionCantidadesNumerosInformacion_CondicionImplicacion
    {
        public int CantidadNumerosCondicion { get; set; }
        public int CantidadNumerosValoresCondicion { get; set; }
        public int NumerosCumplenCondicion { get; set; }
        public int NumerosNoCumplenCondicion { get; set; }

        public int NumerosCumplenCondicion_Valores { get; set; }
        public int NumerosNoCumplenCondicion_Valores { get; set; }
        public List<EntidadNumero> NumerosAsociados_OperandoCondicion { get; set; }
        public List<ElementoEjecucionCalculo> OperandosAsociados_OperandoCondicion { get; set; }
        public List<EntidadNumero> NumerosAsociados_OperandoValores { get; set; }
        public List<ElementoEjecucionCalculo> OperandosAsociados_OperandoValores { get; set; }

        public InformacionCantidadesNumerosInformacion_CondicionImplicacion()
        {
            NumerosAsociados_OperandoCondicion = new List<EntidadNumero>();
            OperandosAsociados_OperandoCondicion = new List<ElementoEjecucionCalculo>();

            NumerosAsociados_OperandoValores = new List<EntidadNumero>();
            OperandosAsociados_OperandoValores = new List<ElementoEjecucionCalculo>();
        }
    }
}
