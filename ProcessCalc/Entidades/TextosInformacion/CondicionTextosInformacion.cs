using ProcessCalc.Controles;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.OrigenesDatos;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class CondicionTextosInformacion
    {
        public TipoOpcionElemento_Condicion_ImplicacionTextosInformacion TipoElementoCondicion { get; set; }
        public DiseñoOperacion ElementoCondicion { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento_Condicion { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Condicion_Elemento { get; set; }
        public TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion TipoTextosInformacion_Valores { get; set; }
        public TipoOpcionElementoComparar_TextosInformacion TipoElementoComparar_TextosInformacion { get; set; }
        public DiseñoOperacion OperandoCondicion { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento_Condicion_TextosInformacion { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Condicion { get; set; }
        public TipoOpcionImplicacion_AsignacionTextoInformacion TipoOpcionCondicion_TextosInformacion { get; set; }
        public DiseñoOperacion ElementoOperacion_Valores { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento_Valores_TextosInformacion { get; set; }
        public string Valores_Condicion { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_CondicionValores { get; set; }
        public Entrada EntradaTextosInformacion_Condicion { get; set; }
        public Entrada EntradaTextosInformacion_Condicion_Valores { get; set; }
        public CondicionTextosInformacion CondicionContenedora { get; set; }
        public TipoConectorCondiciones_ConjuntoBusquedas TipoConector { get; set; }
        public List<CondicionTextosInformacion> Condiciones { get; set; }
        public int CantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadNumerosCumplenCondicion { get; set; }
        public bool OpcionSaldoCantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaNumerosCumplenCondicion { get; set; }
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
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public int CantidadSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion TipoSubElemento_Condicion { get; set; }
        public TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion TipoSubElemento_Condicion_Valores { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada { get; set; }
        public TipoOpcion_CondicionTextosInformacion_Implicacion TipoOpcionCondicion_ElementoOperacionEntrada { get; set; }
        public TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion TipoElemento_Valores { get; set; }
        public DiseñoOperacion ElementoOperacion_Valores_ElementoAsociado { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento_Condicion_Elemento { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada { get; set; }
        public int CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada { get; set; }
        public bool OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada { get; set; }
        public DiseñoElementoOperacion SubElementoOperacion_Valores { get; set; }
        public BusquedaTextoArchivo Busqueda_TextoBusqueda {  get; set; }
        public bool BuscarCualquierTextoInformacion_TextoBusqueda { get; set; }
        public DiseñoListaCadenasTexto ElementoDefinicionListas_Valores { get; set; }
        public TipoOpcionPosicion OpcionValorPosicion { get; set; }
        public bool EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores { get; set; }
        public bool QuitarEspaciosTemporalmente_CadenaCondicion { get; set; }
        public bool CadenasTextoSon_Clasificadores { get; set; }
        public bool CadenasTextoSon_Clasificadores_Valores { get; set; }

        [IgnoreDataMember]
        public List<string> TextosInformacionInvolucrados { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacionInvolucrados_SeleccionarOrdenar { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacionCumplenCondicion { get; set; }
        [IgnoreDataMember]
        public bool CondicionAnterior_ConValoresNoOperandos { get; set; }
        [IgnoreDataMember]
        public bool ConValoresNoOperandos { get; set; }

        //[IgnoreDataMember]
        //public DiseñoOperacion OperandoCondicion_Valores_Posterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoElementoOperacion OperandoSubElemento_Condicion_Valores_Posterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoOperacion OperandoCondicion_Anterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoElementoOperacion OperandoSubElemento_Condicion_Anterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoOperacion OperandoCondicion_Posterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoElementoOperacion OperandoSubElemento_Condicion_Posterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoOperacion OperandoCondicion_Valores_Anterior { get; set; }
        //[IgnoreDataMember]
        //public DiseñoElementoOperacion OperandoSubElemento_Condicion_Valores_Anterior { get; set; }
        public bool ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones { get; set; }
        public bool ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones { get; set; }
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
        public bool VaciarListaTextosInformacion_CumplenCondicion { get; set; }
        public bool VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple { get; set; }
        public bool CantidadTextosInformacion_PorElemento { get; set; }
        public bool CantidadTextosInformacion_PorElemento_Valores { get; set; }
        public bool CantidadNumeros_PorElemento { get; set; }
        public bool CantidadNumeros_PorElemento_Valores { get; set; }
        public bool CantidadTextosInformacion_SoloCadenasCumplen { get; set; }
        public bool CantidadTextosInformacion_SoloCadenasCumplen_Valores { get; set; }
        public bool ConsiderarTextosInformacionComoCantidades { get; set; }
        public bool EsOperandoActual { get; set; }
        public bool EsOperandoTextosActual { get; set; }
        public bool EsOperandoValoresTextosActual { get; set; }
        public bool EsOperandoValoresActual { get; set; }
        public bool ModoSeleccionEntradas { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesCumplenCondicion { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesNoCumplenCondicion { get; set; }
        public List<DiseñoOperacion> Operandos_AplicarCondiciones { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_AplicarCondiciones { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> SubOperandosVinculados_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosVinculados_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> SubOperandosVinculados_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosVinculados_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_CondicionAnterior_Total { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_CondicionAnterior_Total { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_CondicionAnterior_Temp { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_CondicionAnterior_Temp { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosVinculados_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> SubOperandosVinculados_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosVinculados_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> NumerosVinculados_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<FilaTextosInformacion_Entrada> FilasVinculadas_CondicionContenedora { get; set; }
        [IgnoreDataMember]
        public List<FilaTextosInformacion_Entrada> FilasVinculadas_AgregarCondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<FilaTextosInformacion_Entrada> FilasVinculadas_CondicionAnterior { get; set; }
        [IgnoreDataMember]
        public List<InfoOpcion_VinculadosAnterior> OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior { get; set; }
        public bool IncluirNombreElementoConTextos { get; set; }
        public bool IncluirSoloNombreElemento { get; set; }
        public bool OrdenarAlfabeticamenteNumerosSalidas { get; set; }
        public bool OrdenarAlfabeticamenteNumerosClasificadores { get; set; }
        public TipoOpcion_OrdenamientoNumerosSalidas Tipo_OrdenamientoNumerosSalidas { get; set; }
        public TipoOpcion_OrdenamientoNumerosSalidas Tipo_OrdenamientoNumerosClasificadores { get; set; }
        public bool OrdenamientoSalidasAscendente { get; set; }
        public bool OrdenamientoClasificadoresAscendente { get; set; }
        public bool OrdenamientoSalidasDescendente { get; set; }
        public bool OrdenamientoClasificadoresDescendente { get; set; }
        public bool ContenedorCondiciones { get; set; }
        List<string> TextosInfImplicacion_Instancia;
        [IgnoreDataMember]
        public List<string> TextosInformacionImplicacion_Instancia
        {
            get { return TextosInfImplicacion_Instancia; }

            set
            {
                TextosInfImplicacion_Instancia = value;

                foreach (var itemCondicion in Condiciones)
                {
                    itemCondicion.TextosInformacionImplicacion_Instancia = value;
                }
            }
        }
        List<string> TextosInfImplicacion_Condicion;
        [IgnoreDataMember]
        public List<string> TextosInformacionImplicacion_Condicion
        {
            get
            {
                return TextosInfImplicacion_Condicion;
            }
            set
            {
                TextosInfImplicacion_Condicion = value;

                foreach (var itemCondicion in Condiciones)
                {
                    itemCondicion.TextosInformacionImplicacion_Condicion = value;
                }
            }
        }
        List<string> TextosInfImplicacion_Operacion;
        [IgnoreDataMember]
        public List<string> TextosInformacionImplicacion_Operacion
        {
            get
            {
                return TextosInfImplicacion_Operacion;
            }
            set
            {
                TextosInfImplicacion_Operacion = value;

                foreach (var itemCondicion in Condiciones)
                {
                    itemCondicion.TextosInformacionImplicacion_Operacion = value;
                }
            }
        }
        List<string> TextosInfImplicacion_CumplenCondicion;
        [IgnoreDataMember]
        public List<string> TextosInformacionImplicacion_CumplenCondicion
        {
            get
            {
                return TextosInfImplicacion_CumplenCondicion;
            }
            set
            {
                TextosInfImplicacion_CumplenCondicion = value;

                foreach (var itemCondicion in Condiciones)
                {
                    itemCondicion.TextosInformacionImplicacion_CumplenCondicion = value;
                }
            }
        }
        [IgnoreDataMember]
        public ElementoInternetOrigenDatosEjecucion Busqueda_TextoBusqueda_Ejecucion { get; set; }
        [IgnoreDataMember]
        public bool ValorCondiciones { get; set; }
        public CondicionTextosInformacion()
        {
            Valores_Condicion = string.Empty;
            TipoConector = TipoConectorCondiciones_ConjuntoBusquedas.InicioCondiciones;
            Condiciones = new List<CondicionTextosInformacion>();
            TextosInformacionInvolucrados = new List<string>();
            CantidadNumerosCumplenCondicion = 2;
            OpcionCantidadNumerosCumplenCondicion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaNumerosCumplenCondicion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            Operandos_AplicarCondiciones = new List<DiseñoOperacion>();
            SubOperandos_AplicarCondiciones = new List<DiseñoElementoOperacion>();
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
            FilasVinculadas_CondicionContenedora = new List<FilaTextosInformacion_Entrada>();
            FilasVinculadas_AgregarCondicionAnterior = new List<FilaTextosInformacion_Entrada>();
            FilasVinculadas_CondicionAnterior = new List<FilaTextosInformacion_Entrada>();
            OpcionSeleccionNumerosElemento_Condicion = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
            OpcionSeleccionNumerosElemento_CondicionValores = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
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
            OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            IncluirNombreElementoConTextos = true;
            Tipo_OrdenamientoNumerosSalidas = TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas;
            Tipo_OrdenamientoNumerosClasificadores = TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad;
            OrdenamientoSalidasAscendente = true;
            OrdenamientoClasificadoresAscendente = true;
            TipoElementoComparar_TextosInformacion = TipoOpcionElementoComparar_TextosInformacion.TextosInformacion;
            ConsiderarOperandoCondicion_SiCumple = true;
            ConsiderarIncluirCondicionesHijas = true;
            VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple = true;
            TextosInformacionCumplenCondicion = new List<string>();
            OpcionSeleccionNumerosElemento_Condicion_Elemento = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
            OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_OperacionEntrada = 2;
            TipoElemento_Valores = TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos;
            OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada = TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion;
            OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = 2;
            OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior = new List<InfoOpcion_VinculadosAnterior>();
            OpcionValorPosicion = TipoOpcionPosicion.Ninguna;
            SeguirAplicandoCondicion_AlFinalCantidadesOperando = true;
            SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores = true;
            TextosInformacionInvolucrados_SeleccionarOrdenar = new List<string>();
            OperandosVinculados_CondicionAnterior_Total = new List<ElementoEjecucionCalculo>();
            NumerosVinculados_CondicionAnterior_Total = new List<EntidadNumero>();
            OperandosVinculados_CondicionAnterior_Temp = new List<ElementoEjecucionCalculo>();
            NumerosVinculados_CondicionAnterior_Temp = new List<EntidadNumero>();
            ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones = true;
            ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones = true;
        }

        public CondicionTextosInformacion ReplicarObjeto()
        {
            CondicionTextosInformacion condicion = new CondicionTextosInformacion();

            condicion.CopiarObjeto(this, this.CondicionContenedora);

            //CondicionImplicacionTextosInformacion condicion = null;

            //MemoryStream flujo = new MemoryStream();

            //DataContractSerializer objeto = new DataContractSerializer(typeof(CondicionImplicacionTextosInformacion), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            //objeto.WriteObject(flujo, this);

            //flujo.Position = 0;
            //condicion = (CondicionImplicacionTextosInformacion)objeto.ReadObject(flujo);

            return condicion;
        }

        public void CopiarObjeto(CondicionTextosInformacion condicionACopiar,
            CondicionTextosInformacion condicionContenedoraACopiar)
        {
            if (condicionContenedoraACopiar != null)
                this.CondicionContenedora = condicionContenedoraACopiar;

            this.Condiciones = new List<CondicionTextosInformacion>();
            this.TipoElementoCondicion = condicionACopiar.TipoElementoCondicion;
            this.OperandoCondicion = condicionACopiar.OperandoCondicion;
            this.OperandoSubElemento_Condicion = condicionACopiar.OperandoSubElemento_Condicion;
            this.ElementoCondicion = condicionACopiar.ElementoCondicion;
            this.OpcionSeleccionNumerosElemento_Condicion_Elemento = condicionACopiar.OpcionSeleccionNumerosElemento_Condicion_Elemento;
            this.OperandoSubElemento_Condicion_TextosInformacion = condicionACopiar.OperandoSubElemento_Condicion_TextosInformacion;
            this.TipoConector = condicionACopiar.TipoConector;
            this.TipoOpcionCondicion_TextosInformacion = condicionACopiar.TipoOpcionCondicion_TextosInformacion;
            this.TipoTextosInformacion_Valores = condicionACopiar.TipoTextosInformacion_Valores;
            this.ElementoOperacion_Valores = condicionACopiar.ElementoOperacion_Valores;
            this.OperandoSubElemento_Valores_TextosInformacion = condicionACopiar.OperandoSubElemento_Valores_TextosInformacion;
            this.Valores_Condicion = condicionACopiar.Valores_Condicion;
            this.EntradaTextosInformacion_Condicion = condicionACopiar.EntradaTextosInformacion_Condicion;
            this.EntradaTextosInformacion_Condicion_Valores = condicionACopiar.EntradaTextosInformacion_Condicion_Valores;
            this.OpcionSeleccionNumerosElemento_Condicion = condicionACopiar.OpcionSeleccionNumerosElemento_Condicion;
            this.OpcionSeleccionNumerosElemento_CondicionValores = condicionACopiar.OpcionSeleccionNumerosElemento_CondicionValores;
            this.CantidadNumerosCumplenCondicion = condicionACopiar.CantidadNumerosCumplenCondicion;
            this.OpcionCantidadNumerosCumplenCondicion = condicionACopiar.OpcionCantidadNumerosCumplenCondicion;
            this.OpcionCantidadDeterminadaNumerosCumplenCondicion = condicionACopiar.OpcionCantidadDeterminadaNumerosCumplenCondicion;
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
            this.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
            this.Operandos_AplicarCondiciones = condicionACopiar.Operandos_AplicarCondiciones.ToList();
            this.SubOperandos_AplicarCondiciones = condicionACopiar.SubOperandos_AplicarCondiciones.ToList();
            this.IncluirNombreElementoConTextos = condicionACopiar.IncluirNombreElementoConTextos;
            this.IncluirSoloNombreElemento = condicionACopiar.IncluirSoloNombreElemento;
            this.ContenedorCondiciones = condicionACopiar.ContenedorCondiciones;
            this.TipoElementoComparar_TextosInformacion = condicionACopiar.TipoElementoComparar_TextosInformacion;
            this.NegarCondicion = condicionACopiar.NegarCondicion;
            this.QuitarEspaciosTemporalmente_CadenaCondicion = condicionACopiar.QuitarEspaciosTemporalmente_CadenaCondicion;
            this.SeguirAplicandoCondicion_AlFinalCantidadesOperando = condicionACopiar.SeguirAplicandoCondicion_AlFinalCantidadesOperando;
            this.SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores = condicionACopiar.SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores;
            this.OpcionSaldoCantidadNumerosCumplenCondicion = condicionACopiar.OpcionSaldoCantidadNumerosCumplenCondicion;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
            this.OrdenarAlfabeticamenteNumerosSalidas = condicionACopiar.OrdenarAlfabeticamenteNumerosSalidas;
            this.OrdenarAlfabeticamenteNumerosClasificadores = condicionACopiar.OrdenarAlfabeticamenteNumerosClasificadores;
            this.Tipo_OrdenamientoNumerosSalidas = condicionACopiar.Tipo_OrdenamientoNumerosSalidas;
            this.Tipo_OrdenamientoNumerosClasificadores = condicionACopiar.Tipo_OrdenamientoNumerosClasificadores;
            this.OrdenamientoSalidasAscendente = condicionACopiar.OrdenamientoSalidasAscendente;
            this.OrdenamientoClasificadoresAscendente = condicionACopiar.OrdenamientoClasificadoresAscendente;
            this.OrdenamientoSalidasDescendente = condicionACopiar.OrdenamientoSalidasDescendente;
            this.OrdenamientoClasificadoresDescendente = condicionACopiar.OrdenamientoClasificadoresDescendente;
            this.CumpleCondicion_ElementoSinNumeros = condicionACopiar.CumpleCondicion_ElementoSinNumeros;
            this.CumpleCondicion_ElementoValores_SinNumeros = condicionACopiar.CumpleCondicion_ElementoValores_SinNumeros;
            this.ConectorO_Excluyente = condicionACopiar.ConectorO_Excluyente;
            this.ConsiderarOperandoCondicion_SiCumple = condicionACopiar.ConsiderarOperandoCondicion_SiCumple;
            this.ConsiderarIncluirCondicionesHijas = condicionACopiar.ConsiderarIncluirCondicionesHijas;
            this.VaciarListaTextosInformacion_CumplenCondicion = condicionACopiar.VaciarListaTextosInformacion_CumplenCondicion;
            this.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple = condicionACopiar.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple;
            this.OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada;
            this.CantidadSubNumerosCumplenCondicion_OperacionEntrada = condicionACopiar.CantidadSubNumerosCumplenCondicion_OperacionEntrada;
            this.TipoSubElemento_Condicion = condicionACopiar.TipoSubElemento_Condicion;
            this.TipoSubElemento_Condicion_Valores = condicionACopiar.TipoSubElemento_Condicion_Valores;
            this.TipoOpcionCondicion_ElementoOperacionEntrada = condicionACopiar.TipoOpcionCondicion_ElementoOperacionEntrada;
            this.TipoElemento_Valores = condicionACopiar.TipoElemento_Valores;
            this.ElementoOperacion_Valores_ElementoAsociado = condicionACopiar.ElementoOperacion_Valores_ElementoAsociado;
            this.OperandoSubElemento_Condicion_Elemento = condicionACopiar.OperandoSubElemento_Condicion_Elemento;
            this.OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada = condicionACopiar.OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada;
            this.OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada;
            this.CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = condicionACopiar.CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada;
            this.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = condicionACopiar.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada;
            this.Busqueda_TextoBusqueda = condicionACopiar.Busqueda_TextoBusqueda;
            this.BuscarCualquierTextoInformacion_TextoBusqueda = condicionACopiar.BuscarCualquierTextoInformacion_TextoBusqueda;
            this.CantidadNumeros_PorElemento = condicionACopiar.CantidadNumeros_PorElemento;
            this.CantidadNumeros_PorElemento_Valores = condicionACopiar.CantidadNumeros_PorElemento_Valores;
            this.CantidadTextosInformacion_PorElemento = condicionACopiar.CantidadTextosInformacion_PorElemento;
            this.CantidadTextosInformacion_PorElemento_Valores = condicionACopiar.CantidadTextosInformacion_PorElemento_Valores;
            this.OpcionValorPosicion = condicionACopiar.OpcionValorPosicion;
            this.ElementoDefinicionListas_Valores = condicionACopiar.ElementoDefinicionListas_Valores;
            this.EsOperandoActual = condicionACopiar.EsOperandoActual;
            this.EsOperandoTextosActual = condicionACopiar.EsOperandoTextosActual;
            this.EsOperandoValoresTextosActual = condicionACopiar.EsOperandoValoresTextosActual;
            this.EsOperandoValoresActual = condicionACopiar.EsOperandoValoresActual;
            this.CantidadTextosInformacion_SoloCadenasCumplen = condicionACopiar.CantidadTextosInformacion_SoloCadenasCumplen;
            this.CantidadTextosInformacion_SoloCadenasCumplen_Valores = condicionACopiar.CantidadTextosInformacion_SoloCadenasCumplen_Valores;
            this.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores = condicionACopiar.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores;
            this.CadenasTextoSon_Clasificadores = condicionACopiar.CadenasTextoSon_Clasificadores;
            this.CadenasTextoSon_Clasificadores_Valores = condicionACopiar.CadenasTextoSon_Clasificadores_Valores;
            this.ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones = condicionACopiar.ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones;
            this.ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones = condicionACopiar.ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones;

            foreach (var itemCondicion in condicionACopiar.Condiciones)
            {
                this.Condiciones.Add(new CondicionTextosInformacion());
                this.Condiciones.Last().CopiarObjeto(itemCondicion, this);
            }
        }

        public bool VerificarOperando(DiseñoOperacion operando)
        {
            List<CondicionTextosInformacion> condicionesAsociadasOperando = new List<CondicionTextosInformacion>();
            List<CondicionTextosInformacion> condicionesAsociadasOperando2 = new List<CondicionTextosInformacion>();

            ObtenerCondicionOperandoCondicion_Condiciones(ref condicionesAsociadasOperando, operando);
            ObtenerCondicionElementoCondicion_Condiciones(ref condicionesAsociadasOperando2, operando);

            return condicionesAsociadasOperando.Any() | condicionesAsociadasOperando2.Any();
        }

        public bool VerificarOperandoValores(DiseñoOperacion operando)
        {
            List<CondicionTextosInformacion> condicionesAsociadasOperando = new List<CondicionTextosInformacion>();
            List<CondicionTextosInformacion> condicionesAsociadasOperando2 = new List<CondicionTextosInformacion>();

            ObtenerCondicionOperandoValoresCondicion_Condiciones(ref condicionesAsociadasOperando, operando);
            ObtenerCondicionElementoValoresCondicion_Condiciones(ref condicionesAsociadasOperando2, operando);

            return condicionesAsociadasOperando.Any() | condicionesAsociadasOperando2.Any();
        }

        public bool VerificarOperando_EnCondicion_SiCumple(EjecucionCalculo ejecucion, 
            ElementoOperacionAritmeticaEjecucion operando, EntidadNumero numero)
        {
            foreach(var itemCondicion in Condiciones)
            {
                if (itemCondicion.VerificarOperando_EnCondicion_SiCumple(ejecucion, operando, numero))
                    return true;
            }

            if (!ContenedorCondiciones &&
                (VerificarSiOperandosCorresponden_AEjecucion(ejecucion, true,
                operando.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operando.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero) &&
                        ValorCondiciones && operando.ConsiderarOperandoCondicion_SiCumple && numero.ConsiderarOperandoCondicion_SiCumple))
                return true;
            else
                return false;
        }

        public void ObtenerCondicionOperandoCondicion_Condiciones(ref List<CondicionTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (OperandoCondicion == elemento)
            {
                condiciones.Add(this);
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionOperandoCondicion_Condiciones(ref condiciones, elemento);
            }
        }

        public void ObtenerCondicionOperandoValoresCondicion_Condiciones(ref List<CondicionTextosInformacion> condiciones, DiseñoOperacion elemento)
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

        public void ObtenerCondicionElementoValoresCondicion_Condiciones(ref List<CondicionTextosInformacion> condiciones, DiseñoOperacion elemento)
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

        public void ObtenerCondicionElementoDiseñoCondicion_Condiciones(ref List<CondicionTextosInformacion> condiciones, DiseñoElementoOperacion elemento)
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

        public void ObtenerCondicionElementoCondicion_Condiciones(ref List<CondicionTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (ElementoCondicion == elemento)
            {
                condiciones.Add(this);
            }

            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.ObtenerCondicionElementoCondicion_Condiciones(ref condiciones, elemento);
            }
        }


        public bool EvaluarCondiciones(EjecucionCalculo ejecucion,
            ElementoOperacionAritmeticaEjecucion operacion,
            ElementoDiseñoOperacionAritmeticaEjecucion subOperacion,
            ElementoOperacionAritmeticaEjecucion operando,
            EntidadNumero numero)
        {
            if (ContenedorCondiciones)
            {
                NumerosVinculados_CondicionAnterior_Total.Clear();
                OperandosVinculados_CondicionAnterior_Total.Clear();
                
                if (Condiciones.Any())
                {
                    RestablecerValoresCondicion(this);
                }
            }

            bool valorCondicion = !ContenedorCondiciones ? EvaluarCondicion(ejecucion,
                operacion,
                subOperacion,
                operando,
                numero) : true;//,
                //elementoResultado,
                //subElementoResultado,
                //numeroResultado);

            if (Condiciones.Any())
            {
                //if (valorCondicion)
                //{
                if (!valorCondicion)
                {
                    NumerosVinculados_CondicionAnterior_Temp.Clear();
                    OperandosVinculados_CondicionAnterior_Temp.Clear();
                }

                List<bool> valoresCondicion = new List<bool>();

                if(!ContenedorCondiciones)
                    valoresCondicion.Add(valorCondicion);

                CondicionTextosInformacion condicionAnterior = this;
                CondicionTextosInformacion condicionContenedora = this;

                int indiceCondicion = 0;
                foreach (var itemCondicion in Condiciones)
                {
                    if(itemCondicion == Condiciones.FirstOrDefault())
                    {
                        itemCondicion.CondicionAnterior_ConValoresNoOperandos = ConValoresNoOperandos;
                    }
                    else if (condicionAnterior != null)
                    {
                        itemCondicion.CondicionAnterior_ConValoresNoOperandos = condicionAnterior.ConValoresNoOperandos;
                    }

                    if (itemCondicion != Condiciones.FirstOrDefault() &&
                        condicionContenedora != null)
                        itemCondicion.ActualizarElementosNumerosVinculados_CondicionesAnteriores(condicionContenedora);

                    if(condicionAnterior != null)
                        itemCondicion.ActualizarElementosNumerosVinculados_CondicionesAnteriores(condicionAnterior);

                    //itemCondicion.OperandoCondicion_Anterior = OperandoCondicion_Anterior;
                    //itemCondicion.OperandoSubElemento_Condicion_Anterior = OperandoSubElemento_Condicion_Anterior;
                    //itemCondicion.OperandoCondicion_Valores_Anterior = OperandoCondicion_Valores_Anterior;
                    //itemCondicion.OperandoSubElemento_Condicion_Valores_Anterior = OperandoSubElemento_Condicion_Valores_Anterior;

                    bool valorItemCondicion = itemCondicion.EvaluarCondiciones(ejecucion,
                        operacion,
                        subOperacion,
                        operando,
                        numero);//,
                                //elementoResultado,
                                //subElementoResultado,
                                //numeroResultado);
                    if(!valorItemCondicion)
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

                    //OperandoCondicion_Anterior = itemCondicion.OperandoCondicion_Posterior;
                    //OperandoSubElemento_Condicion_Anterior = itemCondicion.OperandoSubElemento_Condicion_Posterior;
                    //OperandoCondicion_Valores_Anterior = itemCondicion.OperandoCondicion_Valores_Posterior;
                    //OperandoSubElemento_Condicion_Valores_Anterior = itemCondicion.OperandoSubElemento_Condicion_Valores_Posterior;
                    //if (valorItemCondicion)
                    //{
                    if (itemCondicion.CantidadTextosInformacion_SoloCadenasCumplen |
                        itemCondicion.CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                    {
                        TextosInformacionInvolucrados.Clear();
                    }

                    TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(itemCondicion.TextosInformacionInvolucrados));
                        itemCondicion.TextosInformacionInvolucrados.Clear();
                    //}

                    valoresCondicion.Add(valorItemCondicion);

                    if (ContenedorCondiciones &&
                                itemCondicion == Condiciones.First() &&
                                !valorItemCondicion &&
                                Condiciones.Count == 1)
                    {
                        EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                        ValorCondiciones = false;
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

                                    EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                    ValorCondiciones = false;
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

                                    EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                    ValorCondiciones = false;
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

                                    EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                    ValorCondiciones = false;
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

                                EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                ValorCondiciones = false;
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

                                EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                ValorCondiciones = false;
                                return false;
                            }

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion == EvaluacionesCumplenCondicion)
                            {
                                LimpiarVariables_ElementosVinculados(true);
                                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                ValorCondiciones = false;
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

                                        EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                        ValorCondiciones = false;
                                        return false;
                                    }
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion > CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null, 
                        numero);

                                        ValorCondiciones = false;
                                        return false;
                                    }
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (EvaluacionesCumplenCondicion - EvaluacionesNoCumplenCondicion != CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                        ValorCondiciones = false;
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

                                EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                ValorCondiciones = false;
                                return false;
                            }

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (EvaluacionesNoCumplenCondicion > 0)
                            {
                                LimpiarVariables_ElementosVinculados(true);
                                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                ValorCondiciones = false;
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

                                        EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                        ValorCondiciones = false;
                                        return false;
                                    }
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (EvaluacionesCumplenCondicion > CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                        ValorCondiciones = false;
                                        return false;
                                    }
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (EvaluacionesCumplenCondicion != CantidadNumerosCumplenCondicion)
                                    {
                                        LimpiarVariables_ElementosVinculados(true);
                                        LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                                        EstablecerConsiderarOperando(false, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                                        ValorCondiciones = false;
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
                    var condiciones = Condiciones.Where(i => i.ValorCondiciones).ToList();

                    foreach(var condicion in condiciones)
                    {
                        OperandosVinculados_CondicionAnterior_Total.AddRange(condicion.OperandosVinculados_CondicionAnterior_Temp.Distinct());
                        //SubOperandosVinculados_CondicionAnterior_Total.AddRange(condicion.SubOperandosVinculados_CondicionAnterior_Temp.Distinct());
                        //ElementosVinculados_CondicionAnterior_Total.AddRange(condicion.ElementosVinculados_CondicionAnterior_Temp.Distinct());
                        NumerosVinculados_CondicionAnterior_Total.AddRange(condicion.NumerosVinculados_CondicionAnterior_Temp.Distinct());
                    }
                }

                LimpiarVariables_ElementosVinculados(false);
                LimpiarTextosInformacion_CondicionesAnteriores(operacion, operando, numero);

                if ((ConsiderarIncluirCondicionesHijas ||
                    (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones)))
                    EstablecerConsiderarOperando(true, ejecucion, 
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                ValorCondiciones = true;
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
                //LimpiarTextosInformacion_CondicionesAnteriores(operacionCondicionEjecucion, operacionInternaCondicionEjecucion, elementoOperando, subElementoOperando, numeroOperando);

                if ((ConsiderarIncluirCondicionesHijas || 
                    (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones)))
                    EstablecerConsiderarOperando(valorCondicion, ejecucion,
                        operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                        operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                        numero);

                ValorCondiciones = valorCondicion;
                return valorCondicion;
            }
            
        }

        private bool EvaluarCondicion(EjecucionCalculo ejecucion,
            ElementoOperacionAritmeticaEjecucion operacionCondicionEjecucion,
            ElementoDiseñoOperacionAritmeticaEjecucion operacionInternaCondicionEjecucion,
            ElementoOperacionAritmeticaEjecucion operando,
            EntidadNumero numero)
        {
            bool valorCondicion = false;
            bool sinNumerosTextos = false;
            bool sinNumerosTextos_Valores = false;
            bool OperandosIguales = false;

            List<string[]> listaValoresCondicion = new List<string[]>();
            string[] valoresCondicion = null;

            int CantidadNumerosValoresCondicion_TextosInformacion = 0;
            int CantidadNumerosCondicion_TextosInformacion = 0;
            int CantidadTextosCondicion_TextosInformacion = 0;
            int CantidadTextosValoresCondicion_TextosInformacion = 0;

            int CantidadTextosNumeroCondicion = 0;

            int NumerosCumplenCondicion_Elemento = 0;
            int NumerosNoCumplenCondicion_Elemento = 0;

            int NumerosCumplenCondicion_Valores = 0;
            int NumerosNoCumplenCondicion_Valores = 0;

            int TextosCumplenCondicion_Elemento = 0;
            int TextosNoCumplenCondicion_Elemento = 0;

            int TextosCumplenCondicion_Valores = 0;
            int TextosNoCumplenCondicion_Valores = 0;

            List<InformacionCantidadesTextosInformacion_CondicionTextosInformacion> CantidadesTextos = new List<InformacionCantidadesTextosInformacion_CondicionTextosInformacion>();
            int indiceCantidadesTextos_Valores = 0;

            List<InformacionCantidadesNumerosInformacion_CondicionTextosInformacion> CantidadesNumeros = new List<InformacionCantidadesNumerosInformacion_CondicionTextosInformacion>();

            int CantidadNumerosValoresCondicion = 0;
            int CantidadNumerosCondicion_OperacionEntrada = 0;

            ElementoEntradaEjecucion elementoEjecucionCondicion_Valores_ConjuntoEntrada = null;
            ElementoOperacionAritmeticaEjecucion elementoEjecucionCondicion_Valores_Operacion = null;
            ElementoDiseñoOperacionAritmeticaEjecucion elementoEjecucionCondicion_Valores_SubOperacion = null;
            ElementoConjuntoTextosEntradaEjecucion elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion = null;

            var operandosValores = new List<ElementoEjecucionCalculo>();
            var elementosOperandoValores = new List<ElementoEjecucionCalculo>();
            var filasOperandoValores = new List<FilaTextosInformacion_Entrada>();
            var numerosOperandoValores = new List<EntidadNumero>();

            var operandosCondicion = new List<ElementoEjecucionCalculo>();
            var elementosOperandoCondicion = new List<ElementoEjecucionCalculo>();
            var filasOperandoCondicion = new List<FilaTextosInformacion_Entrada>();
            var subElementosOperandoCondicion = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            var numerosOperandoCondicion = new List<EntidadNumero>();

            switch (TipoElementoCondicion)
            {
                case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion:

                    switch (TipoTextosInformacion_Valores)
                    {
                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacionFijos:
                            valoresCondicion = Valores_Condicion.Split('|');
                            listaValoresCondicion.Add(valoresCondicion);
                            ConValoresNoOperandos = true;
                            break;

                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion:
                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada:
                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion:

                            if (((TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion &&
                                ElementoOperacion_Valores != null || EsOperandoValoresActual) || (
                                TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada &&
                                EntradaTextosInformacion_Condicion_Valores != null)) ||
                                ((TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion &&
                                ElementoOperacion_Valores != null || EsOperandoValoresActual) || (
                                TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion &&
                                EntradaTextosInformacion_Condicion_Valores != null)))
                            {
                                bool evaluarElementosEjecucion_Valores = false;
                                bool evaluarElementosCondicionEjecucion_Valores = false;

                                ElementoOperacionAritmeticaEjecucion elementoEjecucionCondicion = null;

                                if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion)
                                {
                                    elementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores);

                                    if (EsOperandoValoresActual)
                                        elementoEjecucionCondicion = operando;
                                }
                                else if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada)
                                {
                                    var elementoEntrada = ejecucion.Calculo.ObtenerCalculoEntradas().ElementosOperaciones.FirstOrDefault(o => o.EntradaRelacionada == EntradaTextosInformacion_Condicion_Valores);
                                    elementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion_EnHistorial(elementoEntrada);
                                }

                                var subElementoEjecucion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Valores_TextosInformacion);

                                if (EsOperandoValoresActual &&
                                    operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                    subElementoEjecucion = (ElementoDiseñoOperacionAritmeticaEjecucion)operando;

                                if (elementoEjecucionCondicion != null)
                                {

                                    if (subElementoEjecucion == null)
                                    {
                                        if (elementoEjecucionCondicion.GetType() == typeof(ElementoEntradaEjecucion))
                                        {
                                            elementoEjecucionCondicion_Valores_ConjuntoEntrada = (ElementoEntradaEjecucion)elementoEjecucionCondicion;


                                        }
                                        else if (elementoEjecucionCondicion.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                                        {
                                            elementoEjecucionCondicion_Valores_Operacion = (ElementoOperacionAritmeticaEjecucion)elementoEjecucionCondicion;

                                        }
                                        else if (elementoEjecucionCondicion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion))
                                        {
                                            elementoEjecucionCondicion_Valores_Operacion = (ElementoOperacionAritmeticaEjecucion)elementoEjecucionCondicion;

                                        }


                                    }

                                    if (subElementoEjecucion != null) //&&
                                                                      //    (subElementoOperando == null || (subElementoOperando != null && subElementoEjecucion != subElementoOperando)))
                                    {
                                        if (subElementoEjecucion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion))
                                        {
                                            elementoEjecucionCondicion_Valores_SubOperacion = subElementoEjecucion;
                                            elementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)subElementoEjecucion;
                                        }
                                    }

                                    if (OpcionSeleccionNumerosElemento_CondicionValores == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion &&
                                        (elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).Contains(numero)))
                                        evaluarElementosEjecucion_Valores = true;
                                    else if (OpcionSeleccionNumerosElemento_CondicionValores == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion && 
                                        ((OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion) &&
                                        (elementoEjecucionCondicion.Numeros.Any(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))) && NumerosVinculados_CondicionAnterior.Contains(i))) ||
                                        (OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion)))
                                        && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y))
                                    {
                                        operandosValores.Add(elementoEjecucionCondicion);

                                        foreach (var item in (elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))))))
                                        {
                                            if (NumerosVinculados_CondicionAnterior.Contains(item))
                                            {
                                                NumerosVinculados_AgregarCondicionAnterior.Add(item);
                                                CantidadNumerosValoresCondicion_TextosInformacion++;

                                                numerosOperandoValores.Add(item);

                                                //CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionTextosInformacion());

                                                switch (TipoElementoComparar_TextosInformacion)
                                                {
                                                    case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                        if (!IncluirSoloNombreElemento)
                                                        {
                                                            if (CadenasTextoSon_Clasificadores_Valores)
                                                            {
                                                                if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                                    CantidadTextosValoresCondicion_TextosInformacion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).Count();
                                                                else
                                                                    CantidadTextosValoresCondicion_TextosInformacion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                            }
                                                            else
                                                            {
                                                                if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                                    CantidadTextosValoresCondicion_TextosInformacion += item.Textos.Intersect(TextosInformacionCumplenCondicion).Count();
                                                                else
                                                                    CantidadTextosValoresCondicion_TextosInformacion += item.Textos.Count;
                                                            }
                                                            //CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion = item.Textos.Count;
                                                        }
                                                        if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                        {
                                                            CantidadTextosValoresCondicion_TextosInformacion++;
                                                            //CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion++;
                                                        }

                                                        break;

                                                    case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                        CantidadTextosValoresCondicion_TextosInformacion += 1;
                                                        //CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion++;
                                                        break;

                                                    case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                        CantidadTextosValoresCondicion_TextosInformacion += 1;
                                                        //CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion++;
                                                        break;

                                                    case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                        CantidadTextosValoresCondicion_TextosInformacion += 1;
                                                        //CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion++;
                                                        break;
                                                }
                                            }
                                        }

                                        evaluarElementosCondicionEjecucion_Valores = true;
                                    }
                                    else
                                    {

                                        List<string> Textos = new List<string>();
                                        List<string[]> ListaTextos = new List<string[]>();

                                        OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                            new InfoOpcion_VinculadosAnterior()
                                            {
                                                OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_CondicionValores,
                                                OperandoRelacionado_Ejecucion = elementoEjecucionCondicion
                                            });

                                        switch (OpcionSeleccionNumerosElemento_CondicionValores)
                                        {
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                                operandosValores.Add(elementoEjecucionCondicion);

                                                int cantidadNums = 0;
                                                foreach (var item in (elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))))))
                                                {
                                                    numerosOperandoValores.Add(item);

                                                    switch (TipoElementoComparar_TextosInformacion)
                                                    {
                                                        case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                            List<string> textos = new List<string>();

                                                            if (!IncluirSoloNombreElemento)
                                                            {
                                                                if (CadenasTextoSon_Clasificadores_Valores)
                                                                {
                                                                    if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                                    {
                                                                        Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).ToList()));
                                                                        textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).ToList()));
                                                                    }
                                                                    else
                                                                    {
                                                                        Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()));
                                                                        textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()));
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                                    {
                                                                        Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos.Intersect(TextosInformacionCumplenCondicion).ToList()));
                                                                        textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos.Intersect(TextosInformacionCumplenCondicion).ToList()));
                                                                    }
                                                                    else
                                                                    {
                                                                        Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos));
                                                                        textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos));
                                                                    }
                                                                }
                                                            }
                                                            if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                            {
                                                                Textos.Add(item.Nombre);
                                                                textos.Add(item.Nombre);
                                                            }

                                                            if(textos.Count > 0)
                                                                ListaTextos.Add(textos.ToArray());
                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                            Textos.Add(item.Numero.ToString());
                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                            int cantidad = 0;
                                                            if (!IncluirSoloNombreElemento)
                                                            {
                                                                if(CadenasTextoSon_Clasificadores_Valores)
                                                                {
                                                                    cantidad += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                                }
                                                                else
                                                                    cantidad += item.Textos.Count;
                                                            }
                                                            if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                cantidad++;

                                                            Textos.Add(cantidad.ToString());
                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                            cantidadNums++;
                                                            break;
                                                    }

                                                }

                                                if (TipoElementoComparar_TextosInformacion == TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento)
                                                {
                                                    Textos.Add(cantidadNums.ToString());
                                                    listaValoresCondicion.Add(Textos.ToArray());
                                                }
                                                else
                                                {
                                                    if (CantidadTextosInformacion_PorElemento_Valores)
                                                        listaValoresCondicion.AddRange(ListaTextos);
                                                    else
                                                    {
                                                        if(Textos.Count > 0)
                                                            listaValoresCondicion.Add(Textos.ToArray());
                                                    }
                                                }

                                                CantidadNumerosValoresCondicion_TextosInformacion += listaValoresCondicion.Sum(Textos => Textos.Length);

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                                operandosValores.Add(elementoEjecucionCondicion);

                                                cantidadNums = 0;
                                                foreach (var item in (elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))))
                                                    .Where(i => (elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar))))
                                                {
                                                    numerosOperandoValores.Add(item);

                                                    switch (TipoElementoComparar_TextosInformacion)
                                                    {
                                                        case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                            List<string> textos = new List<string>();

                                                            if (!IncluirSoloNombreElemento)
                                                            {
                                                                if (CadenasTextoSon_Clasificadores_Valores)
                                                                {
                                                                    if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                                    {
                                                                        Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).ToList()));
                                                                        textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).ToList()));
                                                                    }
                                                                    else
                                                                    {
                                                                        Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()));
                                                                        textos.AddRange(ejecucion.GenerarTextosInformacion(item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()));
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                                    {
                                                                        Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos.Intersect(TextosInformacionCumplenCondicion).ToList()));
                                                                        textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos.Intersect(TextosInformacionCumplenCondicion).ToList()));
                                                                    }
                                                                    else
                                                                    {
                                                                        Textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos));
                                                                        textos.AddRange(ejecucion.GenerarTextosInformacion(item.Textos));
                                                                    }
                                                                }
                                                            }
                                                            if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                            {
                                                                Textos.Add(item.Nombre);
                                                                textos.Add(item.Nombre);
                                                            }

                                                            if (textos.Count > 0)
                                                                ListaTextos.Add(textos.ToArray());
                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                            Textos.Add(item.Numero.ToString());
                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                            int cantidad = 0;
                                                            if (!IncluirSoloNombreElemento)
                                                            {
                                                                if(CadenasTextoSon_Clasificadores_Valores)
                                                                {
                                                                    cantidad += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                                }
                                                                else
                                                                    cantidad += item.Textos.Count;
                                                            }
                                                            if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                cantidad++;

                                                            Textos.Add(cantidad.ToString());
                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                            cantidadNums++;
                                                            break;
                                                    }

                                                }

                                                if (TipoElementoComparar_TextosInformacion == TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento)
                                                {
                                                    Textos.Add(cantidadNums.ToString());
                                                    listaValoresCondicion.Add(Textos.ToArray());
                                                }
                                                else
                                                {
                                                    if (CantidadTextosInformacion_PorElemento_Valores)
                                                        listaValoresCondicion.AddRange(ListaTextos);
                                                    else
                                                    {
                                                        if (Textos.Count > 0)
                                                            listaValoresCondicion.Add(Textos.ToArray());
                                                    }
                                                }

                                                CantidadNumerosValoresCondicion_TextosInformacion += listaValoresCondicion.Sum(Textos => Textos.Length);

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                                if (elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar >=
                                                    (elementoEjecucionCondicion.Numeros.Count(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))))))
                                                {
                                                    if (ReiniciarPosicion_AlFinalCantidadesOperando_Valores)
                                                    {
                                                        elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                                                        
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

                                                operandosValores.Add(elementoEjecucionCondicion);

                                                int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar,
                                                    OpcionSeleccionNumerosElemento_CondicionValores,
                                                    (elementoEjecucionCondicion.Numeros.Count(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))))));

                                                if (indicePosicion <
                                                    (elementoEjecucionCondicion.Numeros.Count(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))))))
                                                {
                                                    numerosOperandoValores.Add((elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion]));

                                                    switch (TipoElementoComparar_TextosInformacion)
                                                    {
                                                        case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                            List<string> textos = new List<string>();

                                                            if (!IncluirSoloNombreElemento)
                                                            {
                                                                if (CadenasTextoSon_Clasificadores_Valores)
                                                                {
                                                                    Textos.AddRange((elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Textos));
                                                                    textos.AddRange((elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()));
                                                                }
                                                                else
                                                                {
                                                                    Textos.AddRange((elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Textos));
                                                                    textos.AddRange((elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Textos));
                                                                }
                                                            }
                                                            if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                            {
                                                                Textos.Add((elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Nombre));
                                                                textos.Add((elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Nombre));
                                                            }

                                                            if (textos.Count > 0)
                                                                ListaTextos.Add(textos.ToArray());

                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                            Textos.Add((elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Numero.ToString()));
                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                            int cantidad = 0;
                                                            if (!IncluirSoloNombreElemento)
                                                            {
                                                                if (CadenasTextoSon_Clasificadores_Valores)
                                                                {
                                                                    cantidad += (elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count);
                                                                }
                                                                else
                                                                {
                                                                    cantidad += (elementoEjecucionCondicion.Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion].Textos.Count);
                                                                }
                                                            }
                                                            if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                                cantidad++;

                                                            Textos.Add(cantidad.ToString());
                                                            break;

                                                        case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                            Textos.Add("1");
                                                            break;
                                                    }

                                                    if (CantidadTextosInformacion_PorElemento_Valores)
                                                        listaValoresCondicion.AddRange(ListaTextos);
                                                    else
                                                    {
                                                        if (Textos.Count > 0)
                                                            listaValoresCondicion.Add(Textos.ToArray());
                                                    }

                                                    CantidadNumerosValoresCondicion_TextosInformacion++;
                                                }
                                                break;

                                        }

                                        CantidadTextosValoresCondicion_TextosInformacion += listaValoresCondicion.Sum(Textos => Textos.Length);
                                    }

                                    if (evaluarElementosEjecucion_Valores &&
                                        (valoresCondicion == null || (valoresCondicion != null && !valoresCondicion.Any())))
                                    {
                                        if (operando != null)
                                        {
                                            elementosOperandoValores.Add(operando);

                                            List<string> valores = new List<string>();
                                            switch (TipoElementoComparar_TextosInformacion)
                                            {
                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                valores.AddRange(operando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion));
                                                            }
                                                            else
                                                                valores.AddRange(operando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList());
                                                        }
                                                        else
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                valores.AddRange(operando.Textos.Intersect(TextosInformacionCumplenCondicion));
                                                            }
                                                            else
                                                                valores.AddRange(operando.Textos);
                                                        }
                                                    }

                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        valores.Add(operando.Nombre);
                                                    }
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:

                                                    //valores.Add(operando.ValorNumerico.ToString());
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                    int cantidad = 0;

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                cantidad += operando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).Count();
                                                            }
                                                            else
                                                                cantidad += operando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList().Count;
                                                        }
                                                        else
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                cantidad += operando.Textos.Intersect(TextosInformacionCumplenCondicion).Count();
                                                            }
                                                            else
                                                                cantidad += operando.Textos.Count;
                                                        }
                                                    }

                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        cantidad++;
                                                    }

                                                    valores.Add(cantidad.ToString());
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                    valores.Add("1");
                                                    break;
                                            }

                                            if (valores.Count > 0)
                                                listaValoresCondicion.Add(valores.ToArray());
                                            CantidadTextosValoresCondicion_TextosInformacion = valores.Count;

                                        }
                                        if (numero != null)
                                        {
                                            numerosOperandoValores.Add(numero);

                                            List<string> valores = new List<string>();
                                            switch (TipoElementoComparar_TextosInformacion)
                                            {
                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                valores.AddRange(numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion));
                                                            }
                                                            else
                                                                valores.AddRange(numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList());
                                                        }
                                                        else
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                valores.AddRange(numero.Textos.Intersect(TextosInformacionCumplenCondicion));
                                                            }
                                                            else
                                                                valores.AddRange(numero.Textos);
                                                        }
                                                    }

                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        valores.Add(numero.Nombre);
                                                    }
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:

                                                    valores.Add(numero.Numero.ToString());
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                    int cantidad = 0;

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                cantidad += numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).Count();
                                                            }
                                                            else
                                                                cantidad += numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                        }
                                                        else
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                cantidad += numero.Textos.Intersect(TextosInformacionCumplenCondicion).Count();
                                                            }
                                                            else
                                                                cantidad += numero.Textos.Count;
                                                        }
                                                    }

                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        cantidad++;
                                                    }

                                                    valores.Add(cantidad.ToString());
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                    valores.Add("1");
                                                    break;
                                            }

                                            if (valores.Count > 0)
                                                listaValoresCondicion.Add(valores.ToArray());
                                            CantidadTextosValoresCondicion_TextosInformacion = valores.Count;

                                        }
                                    }

                                    if (evaluarElementosCondicionEjecucion_Valores)
                                    {
                                        List<string> Textos = new List<string>();
                                        List<string[]> ListaTextos = new List<string[]>();

                                        foreach (var valores in listaValoresCondicion)
                                        {
                                            if (valores != null && valores.Any())
                                            {
                                                foreach (var itemTexto in valores)
                                                {
                                                    Textos.Add(itemTexto);
                                                }

                                                ListaTextos.Add(valores);
                                            }
                                        }

                                        int cantidadNums = 0;

                                        foreach (var itemOperando in NumerosVinculados_AgregarCondicionAnterior)
                                        {
                                            switch (TipoElementoComparar_TextosInformacion)
                                            {
                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                    List<string> textos = new List<string>();

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                Textos.AddRange(itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion));
                                                                textos.AddRange(itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion));
                                                            }
                                                            else
                                                            {
                                                                Textos.AddRange(itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList());
                                                                textos.AddRange(itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                Textos.AddRange(itemOperando.Textos.Intersect(TextosInformacionCumplenCondicion));
                                                                textos.AddRange(itemOperando.Textos.Intersect(TextosInformacionCumplenCondicion));
                                                            }
                                                            else
                                                            {
                                                                Textos.AddRange(itemOperando.Textos);
                                                                textos.AddRange(itemOperando.Textos);
                                                            }
                                                        }
                                                    }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        Textos.Add(itemOperando.Nombre);
                                                        textos.Add(itemOperando.Nombre);
                                                    }

                                                    if (textos.Count > 0)
                                                        ListaTextos.Add(textos.ToArray());

                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                    Textos.Add(itemOperando.Numero.ToString());
                                                    ListaTextos.Add(new string[1] { itemOperando.Numero.ToString() });
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                    int cantidad = 0;
                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                cantidad += itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).Count();
                                                            }
                                                            else
                                                                cantidad += itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                        }
                                                        else
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                cantidad += itemOperando.Textos.Intersect(TextosInformacionCumplenCondicion).Count();
                                                            }
                                                            else
                                                                cantidad += itemOperando.Textos.Count;
                                                        }
                                                    }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                        cantidad++;

                                                    Textos.Add(cantidad.ToString());
                                                    ListaTextos.Add(new string[1] { cantidad.ToString() });
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                    cantidadNums++;
                                                    break;
                                            }

                                            if (!NumerosVinculados_CondicionAnterior.Contains(itemOperando))
                                            {
                                                NumerosVinculados_CondicionAnterior.Add(itemOperando);
                                            }
                                        }

                                        if (TipoElementoComparar_TextosInformacion == TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento)
                                        {
                                            Textos.Add(cantidadNums.ToString());
                                            listaValoresCondicion.Add(Textos.ToArray());
                                        }
                                        else
                                        {
                                            if (CantidadTextosInformacion_PorElemento_Valores)
                                                listaValoresCondicion.AddRange(ListaTextos);
                                            else
                                            {
                                                if (Textos.Count > 0)
                                                    listaValoresCondicion.Add(Textos.ToArray());
                                            }
                                        }

                                        cantidadNums = 0;
                                        ListaTextos.Clear();

                                        foreach (var itemOperando in OperandosVinculados_AgregarCondicionAnterior)
                                        {
                                            switch (TipoElementoComparar_TextosInformacion)
                                            {
                                                case TipoOpcionElementoComparar_TextosInformacion.TextosInformacion:
                                                    List<string> textos = new List<string>();

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                Textos.AddRange(itemOperando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion));
                                                                textos.AddRange(itemOperando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion));
                                                            }
                                                            else
                                                            {
                                                                Textos.AddRange(itemOperando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList());
                                                                textos.AddRange(itemOperando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                Textos.AddRange(itemOperando.Textos.Intersect(TextosInformacionCumplenCondicion));
                                                                textos.AddRange(itemOperando.Textos.Intersect(TextosInformacionCumplenCondicion));
                                                            }
                                                            else
                                                            {
                                                                Textos.AddRange(itemOperando.Textos);
                                                                textos.AddRange(itemOperando.Textos);
                                                            }
                                                        }
                                                    }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        Textos.Add(itemOperando.Nombre);
                                                        textos.Add(itemOperando.Nombre);
                                                    }

                                                    if (textos.Count > 0)
                                                        ListaTextos.Add(textos.ToArray());

                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.NumerosElemento:
                                                    //Textos.Add(itemOperando.ValorNumerico.ToString());
                                                    //ListaTextos.Add(new string[1] { itemOperando.ValorNumerico.ToString() });
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesTextosElemento:
                                                    int cantidad = 0;
                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        if (CadenasTextoSon_Clasificadores_Valores)
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                cantidad += itemOperando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion).Count();
                                                            }
                                                            else
                                                                cantidad += itemOperando.Clasificadores_Cantidades.Select(i => i.CadenaTexto).ToList().Count;
                                                        }
                                                        else
                                                        {
                                                            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                                                            {
                                                                cantidad += itemOperando.Textos.Intersect(TextosInformacionCumplenCondicion).Count();
                                                            }
                                                            else
                                                                cantidad += itemOperando.Textos.Count;
                                                        }
                                                    }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                        cantidad++;

                                                    Textos.Add(cantidad.ToString());
                                                    ListaTextos.Add(new string[1] { cantidad.ToString() });
                                                    break;

                                                case TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento:
                                                    cantidadNums++;
                                                    break;
                                            }

                                            if (!OperandosVinculados_CondicionAnterior.Contains(itemOperando))
                                            {
                                                OperandosVinculados_CondicionAnterior.Add(itemOperando);
                                                OperandosVinculados_CondicionAnterior_Temp.Add(itemOperando);
                                            }
                                        }

                                        if (TipoElementoComparar_TextosInformacion == TipoOpcionElementoComparar_TextosInformacion.CantidadesNumerosElemento)
                                        {
                                            Textos.Add(cantidadNums.ToString());
                                        }
                                        else
                                        {
                                            if (CantidadTextosInformacion_PorElemento_Valores)
                                                listaValoresCondicion.AddRange(ListaTextos);
                                            else
                                            {
                                                if (Textos.Count > 0)
                                                    listaValoresCondicion.Add(Textos.ToArray());
                                            }
                                        }

                                        CantidadTextosValoresCondicion_TextosInformacion += listaValoresCondicion.Sum(Textos => Textos.Length);

                                        NumerosVinculados_AgregarCondicionAnterior.Clear();
                                        OperandosVinculados_AgregarCondicionAnterior.Clear();

                                    }

                                }
                            }
                            else
                                valoresCondicion = new string[] { };
                            break;

                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TodosTextosInformacion_CumplenCondicion:
                            valoresCondicion = TextosInformacionCumplenCondicion.ToArray();

                            if (valoresCondicion.Length > 0)
                                listaValoresCondicion.Add(valoresCondicion.ToArray());

                            CantidadTextosValoresCondicion_TextosInformacion += TextosInformacionCumplenCondicion.Count;

                            break;

                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeCumplenCondicionImplicacion:
                            valoresCondicion = TextosInformacionImplicacion_CumplenCondicion.ToArray();

                            if (valoresCondicion.Length > 0)
                                listaValoresCondicion.Add(valoresCondicion.ToArray());

                            break;

                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacion:
                            valoresCondicion = TextosInformacionImplicacion_Operacion.ToArray();

                            if (valoresCondicion.Length > 0)
                                listaValoresCondicion.Add(valoresCondicion.ToArray());

                            break;

                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacionCondicion:
                            valoresCondicion = TextosInformacionImplicacion_Condicion.ToArray();

                            if (valoresCondicion.Length > 0)
                                listaValoresCondicion.Add(valoresCondicion.ToArray());

                            break;

                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacionInstancia:
                            valoresCondicion = TextosInformacionImplicacion_Instancia.ToArray();

                            if (valoresCondicion.Length > 0)
                                listaValoresCondicion.Add(valoresCondicion.ToArray());

                            break;

                        case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicionLista:

                            switch (OpcionSeleccionNumerosElemento_CondicionValores)
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
                                            OpcionSeleccionNumerosElemento_CondicionValores,
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

                            if (valoresCondicion != null && 
                                valoresCondicion.Length > 0)
                                listaValoresCondicion.Add(valoresCondicion.ToArray());

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

                    if (OperandoCondicion != null ||
                        EntradaTextosInformacion_Condicion != null ||
                        EsOperandoTextosActual)
                    {
                        ElementoOperacionAritmeticaEjecucion elementoEjecucion = null;


                        if (OperandoSubElemento_Condicion_TextosInformacion != null)
                        {
                            elementoEjecucion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion_TextosInformacion);

                            if (EsOperandoTextosActual &&
                                operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                elementoEjecucion = operando;
                        }
                        else
                        {

                            if (EntradaTextosInformacion_Condicion == null)
                            {
                                elementoEjecucion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(OperandoCondicion);

                                if(EsOperandoTextosActual)
                                {
                                    elementoEjecucion = operando;
                                }
                            }
                            else if (EntradaTextosInformacion_Condicion != null)
                            {
                                var elementoEntrada = ejecucion.Calculo.ObtenerCalculoEntradas().ElementosOperaciones.FirstOrDefault(o => o.EntradaRelacionada == EntradaTextosInformacion_Condicion);
                                elementoEjecucion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion_EnHistorial(elementoEntrada);
                            }
                        }

                        if (elementoEjecucion != null)
                        {
                            if (elementoEjecucion.GetType() == typeof(ElementoEntradaEjecucion) ||
                                elementoEjecucion.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) ||
                                elementoEjecucion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion))
                            {
                                List<EntidadNumero> listaNumeros = new List<EntidadNumero>();
                                bool NumerosVinculados = false;

                                if (elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar >=
                                                (elementoEjecucion.Numeros.Where(i =>
                                                (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && i == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores]) || !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                    (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) &&
                                                ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                (i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion) && (!i.ElementosInternosSalidaOperacion_Agrupamiento.Any() ||
                                        i.ElementosInternosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion)))) &
                                (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionCondicionEjecucion)) &
                                (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionCondicionEjecucion)))).Count()))
                                {
                                    if (ReiniciarPosicion_AlFinalCantidadesOperando)
                                    {
                                        elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                                        
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

                                if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion && 
                                    (elementoEjecucion.Numeros.Where(i =>
                                (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && i == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores]) || !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                    (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) &&
                                ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            (i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion) && (!i.ElementosInternosSalidaOperacion_Agrupamiento.Any() ||
                                    i.ElementosInternosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion)))) &
                            (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionCondicionEjecucion)) &
                            (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionCondicionEjecucion)))).Contains(numero) &&
                                !(NumerosVinculados_CondicionAnterior.Contains(numero) && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)))
                                {
                                    listaNumeros.Add(numero);
                                    NumerosVinculados = true;

                                    operandosCondicion.Add(elementoEjecucion);
                                    numerosOperandoCondicion.Add(numero);
                                }
                                else if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion && 
                                    (elementoEjecucion.Numeros.Where(i =>
                                (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && i == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores]) || !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                    (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) &&
                                ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                (i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion) && (!i.ElementosInternosSalidaOperacion_Agrupamiento.Any() ||
                                        i.ElementosInternosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion)))) &
                                (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionCondicionEjecucion)) &
                                (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionCondicionEjecucion)))).Contains(numero) &&
                                    (NumerosVinculados_CondicionAnterior.Contains(numero) && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)))
                                {
                                    operandosCondicion.Add(elementoEjecucion);
                                    
                                    if (NumerosVinculados_CondicionAnterior.Contains(numero))
                                    {
                                        listaNumeros.Add(numero);
                                        numerosOperandoCondicion.Add(numero);
                                        CantidadNumerosCondicion_TextosInformacion++;

                                        if (!IncluirSoloNombreElemento)
                                        {
                                            if(CadenasTextoSon_Clasificadores)
                                            {
                                                CantidadTextosCondicion_TextosInformacion += numero.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                            }
                                            else
                                                CantidadTextosCondicion_TextosInformacion += numero.Textos.Count;
                                        }
                                        if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                            CantidadTextosCondicion_TextosInformacion++;

                                        if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion)
                                            NumerosVinculados = true;
                                    }
                                }
                                else if ((elementoEjecucion.Numeros.Any(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && i == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores]) || !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                    (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) && NumerosVinculados_CondicionAnterior.Contains(i)) &&
                                 TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y))
                                {
                                    operandosCondicion.Add(elementoEjecucion);

                                    foreach (var item in (elementoEjecucion.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && i == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores]) || !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                    (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) &&
                                    ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                (i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion) && (!i.ElementosInternosSalidaOperacion_Agrupamiento.Any() ||
                                        i.ElementosInternosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion)))) &
                                (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionCondicionEjecucion)) &
                                (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionCondicionEjecucion))))))
                                    {
                                        if (NumerosVinculados_CondicionAnterior.Contains(item))
                                        {
                                            listaNumeros.Add(item);
                                            numerosOperandoCondicion.Add(numero);
                                            CantidadNumerosCondicion_TextosInformacion++;

                                            if (!IncluirSoloNombreElemento)
                                            {
                                                if(CadenasTextoSon_Clasificadores)
                                                {
                                                    CantidadTextosCondicion_TextosInformacion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                }
                                                else
                                                    CantidadTextosCondicion_TextosInformacion += item.Textos.Count;
                                            }
                                            if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                CantidadTextosCondicion_TextosInformacion++;

                                            if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion)
                                                NumerosVinculados = true;
                                        }
                                    }
                                }
                                else if (OperandosVinculados_CondicionAnterior.Contains(elementoEjecucion)
                                    && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                                {
                                    operandosCondicion.Add(elementoEjecucion);
                                    foreach (var item in (elementoEjecucion.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && i == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores]) || !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) &&
                                    ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            (i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion) && (!i.ElementosInternosSalidaOperacion_Agrupamiento.Any() ||
                                    i.ElementosInternosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion)))) &
                            (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionCondicionEjecucion)) &
                            (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionCondicionEjecucion))))))
                                    {
                                        if (NumerosVinculados_CondicionAnterior.Contains(item))
                                        {
                                            listaNumeros.Add(item);
                                            numerosOperandoCondicion.Add(numero);
                                            CantidadNumerosCondicion_TextosInformacion++;

                                            if (!IncluirSoloNombreElemento)
                                            {
                                                if(CadenasTextoSon_Clasificadores)
                                                {
                                                    CantidadTextosCondicion_TextosInformacion += item.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                }
                                                else
                                                    CantidadTextosCondicion_TextosInformacion += item.Textos.Count;
                                            }
                                            if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                CantidadTextosCondicion_TextosInformacion++;

                                            if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion)
                                                NumerosVinculados = true;
                                        }
                                    }

                                }
                                else
                                {

                                    OperandosVinculados_CondicionAnterior.Clear();
                                    OperandosVinculados_CondicionAnterior.Add(elementoEjecucion);

                                    NumerosVinculados_CondicionAnterior.Clear();

                                    OperandosVinculados_CondicionAnterior_Temp.Clear();
                                    OperandosVinculados_CondicionAnterior_Temp.Add(elementoEjecucion);

                                    NumerosVinculados_CondicionAnterior_Temp.Clear();



                                    listaNumeros = elementoEjecucion.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count && i == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores]) || !(elementoEjecucion.IndicePosicionClasificadores < elementoEjecucion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucion.Clasificadores_Cantidades[elementoEjecucion.IndicePosicionClasificadores].CadenaTexto))) &&
                                    ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            (i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion) && (!i.ElementosInternosSalidaOperacion_Agrupamiento.Any() ||
                                    i.ElementosInternosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion)))) &
                            (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionCondicionEjecucion)) &
                            (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionCondicionEjecucion)))).ToList();

                                    operandosCondicion.Add(elementoEjecucion);
                                    numerosOperandoCondicion.AddRange(listaNumeros);

                                }

                                OperandosVinculados_CondicionAnterior.Clear();
                                NumerosVinculados_CondicionAnterior.Clear();

                                OperandosVinculados_CondicionAnterior_Temp.Clear();
                                NumerosVinculados_CondicionAnterior_Temp.Clear();

                                OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                        new InfoOpcion_VinculadosAnterior()
                                        {
                                            OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion,
                                            OperandoRelacionado_Ejecucion = elementoEjecucion
                                        });

                                if (!listaValoresCondicion.Any())
                                {
                                    sinNumerosTextos_Valores = true;
                                }

                                if (!listaNumeros.Any())
                                {
                                    sinNumerosTextos = true;
                                }

                                bool sinNumerosTextosElemento = false;
                                bool conNumerosTextos = false;

                                switch (OpcionSeleccionNumerosElemento_Condicion)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                            foreach (var itemOperando in listaNumeros)
                                            {
                                                CantidadNumerosCondicion_TextosInformacion++;

                                                int indiceValores = 0;

                                                foreach (var itemValoresCondicion in listaValoresCondicion)
                                                {
                                                    valoresCondicion = itemValoresCondicion;

                                                    ElementoEjecucionCalculo operandoValoresActual = null;

                                                    if (operandosValores.Any())
                                                        operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];
                                                    EntidadNumero numeroValoresActual = null;

                                                    if (numerosOperandoValores.Any())
                                                        numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count ? indiceValores : numerosOperandoValores.Count - 1];

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


                                                List<string> TextosElemento = new List<string>();
                                                    CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionTextosInformacion());

                                                    if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                    {
                                                        CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion = //CantidadesTextos_Valores[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion_TextosInformacion;
                                                        indiceCantidadesTextos_Valores++;
                                                    }

                                                if (!IncluirSoloNombreElemento)
                                                {
                                                    if (CadenasTextoSon_Clasificadores)
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion += itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                        TextosElemento.AddRange(itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList());
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion = itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                    }
                                                    else
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion += itemOperando.Textos.Count;
                                                        TextosElemento.AddRange(itemOperando.Textos);
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion = itemOperando.Textos.Count;
                                                    }
                                                }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion++;
                                                        TextosElemento.Add(itemOperando.Nombre);
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion++;
                                                    }

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

                                                if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto)
                                                {
                                                    if (((TextosElemento.Any(i => i != null) && !sinNumerosTextosElemento) || (!TextosElemento.Any(i => i != null) && (sinNumerosTextosElemento))) && !comparadorTextos.Interseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores, true);


                                                        {
                                                            TextosInformacionInvolucrados.Clear();
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                        }

                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                        }

                                                        valorCondicion = true;
                                                        TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(itemOperando))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(itemOperando);

                                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(itemOperando);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(itemOperando);

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

                                                            if (ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                        }

                                                        if (!OperandosVinculados_CondicionAnterior.Contains(operandoValoresActual))
                                                        {
                                                            OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                        }

                                                        CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(numeroValoresActual))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);

                                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores,
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
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = comparadorTextos.TextosCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;

                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }
                                                else
                                                {
                                                    if (comparadorTextos.Interseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores, true);

                                                        {
                                                            TextosInformacionInvolucrados.Clear();
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                        }

                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                        }

                                                        valorCondicion = true;
                                                        
                                                        TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(itemOperando))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(itemOperando);

                                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(itemOperando);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(itemOperando);

                                                        if (operandoCondicionActual != null)
                                                        {
                                                            CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                            OperandosVinculados_CondicionAnterior.Add(operandoCondicionActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoCondicionActual);
                                                        }
                                                        if (numeroCondicionActual != null)
                                                        {
                                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                            NumerosVinculados_CondicionAnterior.Add(numeroCondicionActual);

                                                            if (ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                        }

                                                        if (!OperandosVinculados_CondicionAnterior.Contains(operandoValoresActual))
                                                        {
                                                            OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                        }

                                                        CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(numeroValoresActual))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);

                                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores,
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
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }

                                                    indiceValores++;
                                                }
                                            }

                                            if(!conNumerosTextos)
                                        {
                                            sinNumerosTextos = true;
                                        }
                                            //}

                                            break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:

                                        List<EntidadNumero> listaNumerosEvaluar = new List<EntidadNumero>();

                                        if (NumerosVinculados)
                                        {
                                            listaNumerosEvaluar.AddRange(listaNumeros);
                                        }
                                        else
                                        {
                                            listaNumerosEvaluar.AddRange(listaNumeros
                                                    .Where(i => listaNumeros.IndexOf(i) <= elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar));
                                        }

                                        sinNumerosTextosElemento = false;

                                        if(!listaNumerosEvaluar.Any())
                                        {
                                            sinNumerosTextos = true;
                                        }

                                        if(!listaValoresCondicion.Any())
                                        {
                                            sinNumerosTextos_Valores = true;
                                        }
                                        conNumerosTextos = false;

                                        foreach (var itemOperando in listaNumerosEvaluar)
                                        {
                                            CantidadNumerosCondicion_TextosInformacion++;

                                            int indiceValores = 0;

                                            foreach (var itemValoresCondicion in listaValoresCondicion)
                                            {
                                                valoresCondicion = itemValoresCondicion;

                                                ElementoEjecucionCalculo operandoValoresActual = null;

                                                if (operandosValores.Any())
                                                    operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];
                                                EntidadNumero numeroValoresActual = null;

                                                if (numerosOperandoValores.Any())
                                                    numeroValoresActual = numerosOperandoValores[indiceValores <= numerosOperandoValores.Count ? indiceValores : numerosOperandoValores.Count - 1];

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


                                                List<string> TextosElemento = new List<string>();
                                                CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionTextosInformacion());

                                                if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                {
                                                    CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion = //CantidadesTextos_Valores[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion_TextosInformacion;
                                                    indiceCantidadesTextos_Valores++;
                                                }

                                                if (!IncluirSoloNombreElemento)
                                                {
                                                    if (CadenasTextoSon_Clasificadores)
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion += itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                        TextosElemento.AddRange(itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList());
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion = itemOperando.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                    }
                                                    else
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion += itemOperando.Textos.Count;
                                                        TextosElemento.AddRange(itemOperando.Textos);
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion = itemOperando.Textos.Count;
                                                    }
                                                }
                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                {
                                                    CantidadTextosCondicion_TextosInformacion++;
                                                    TextosElemento.Add(itemOperando.Nombre);
                                                    CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion++;
                                                }

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

                                                if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto)
                                                {
                                                    if (((TextosElemento.Any(i => i != null) && !sinNumerosTextosElemento) || (!TextosElemento.Any(i => i != null) && (sinNumerosTextosElemento))) && !comparadorTextos.Interseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores, true);

                                                        {
                                                            TextosInformacionInvolucrados.Clear();
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                        }


                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                        }

                                                        valorCondicion = true;
                                                        
                                                        TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(itemOperando))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(itemOperando);

                                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(itemOperando);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(itemOperando);

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

                                                            if (ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                        }

                                                        if (!OperandosVinculados_CondicionAnterior.Contains(operandoValoresActual))
                                                        {
                                                            OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                        }

                                                        CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(numeroValoresActual))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);

                                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores,
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
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = comparadorTextos.TextosCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;

                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }
                                                else
                                                {
                                                    if (comparadorTextos.Interseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores, true);

                                                        {
                                                            TextosInformacionInvolucrados.Clear();
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                        }


                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                        }

                                                        valorCondicion = true;
                                                        
                                                        TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(itemOperando))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(itemOperando);

                                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(itemOperando);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(itemOperando);

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

                                                            if (ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                        }

                                                        if (!OperandosVinculados_CondicionAnterior.Contains(operandoValoresActual))
                                                        {
                                                            OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                        }

                                                        CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(numeroValoresActual))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);

                                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores,
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
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            itemOperando.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, itemOperando.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }

                                                indiceValores++;
                                            }
                                        }

                                        //}

                                        break;

                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                        listaNumerosEvaluar = new List<EntidadNumero>();

                                        if (NumerosVinculados)
                                        {
                                            listaNumerosEvaluar.AddRange(listaNumeros);
                                        }
                                        else
                                        {
                                            int cantidadNumeros = listaNumeros.Count();

                                            if (elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar <
                                                cantidadNumeros)
                                            {
                                                int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar,
                                                    OpcionSeleccionNumerosElemento_Condicion,
                                                    cantidadNumeros);

                                                listaNumerosEvaluar.Add(listaNumeros[indicePosicion]);

                                            }
                                        }

                                        if (!listaValoresCondicion.Any())
                                        {
                                            sinNumerosTextos_Valores = true;
                                        }

                                        if (!listaNumerosEvaluar.Any())
                                        {
                                            sinNumerosTextos = true;
                                        }

                                        conNumerosTextos = false;

                                        foreach (var numeroItem in listaNumerosEvaluar)
                                        {
                                            CantidadNumerosCondicion_TextosInformacion++;

                                            int indiceValores = 0;

                                            foreach (var itemValoresCondicion in listaValoresCondicion)
                                            {
                                                valoresCondicion = itemValoresCondicion;

                                                ElementoEjecucionCalculo operandoValoresActual = null;

                                                if (operandosValores.Any())
                                                    operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];
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


                                                List<string> TextosElemento = new List<string>();
                                                CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionTextosInformacion());

                                                if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                {
                                                    CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion = //CantidadesTextos_Valores[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion_TextosInformacion;
                                                    indiceCantidadesTextos_Valores++;
                                                }

                                                if (!IncluirSoloNombreElemento)
                                                {
                                                    if (CadenasTextoSon_Clasificadores)
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion += numeroItem.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                        TextosElemento.AddRange(numeroItem.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList());
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion = numeroItem.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Count;
                                                    }
                                                    else
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion += numeroItem.Textos.Count;
                                                        TextosElemento.AddRange(numeroItem.Textos);
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion = numeroItem.Textos.Count;
                                                    }
                                                }
                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                {
                                                    CantidadTextosCondicion_TextosInformacion++;
                                                    TextosElemento.Add(numeroItem.Nombre);
                                                    CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion++;
                                                }

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

                                                if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto)
                                                {
                                                    if (((TextosElemento.Any(i => i != null) && !sinNumerosTextosElemento) || (!TextosElemento.Any(i => i != null) && (sinNumerosTextosElemento))) && !comparadorTextos.Interseccion(TextosElemento, valoresCondicion, numeroItem.TextosInformacion_CumplenCondiciones_Anteriores))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, numeroItem.TextosInformacion_CumplenCondiciones_Anteriores, true);

                                                        {
                                                            TextosInformacionInvolucrados.Clear();
                                                            numeroItem.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                        }

                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            numeroItem.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                        }

                                                        valorCondicion = true;
                                                        
                                                        TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(numeroItem))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(numeroItem);

                                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroItem);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroItem);

                                                        if (operandoCondicionActual != null)
                                                        {
                                                            CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                            OperandosVinculados_CondicionAnterior.Add(operandoCondicionActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoCondicionActual);
                                                        }
                                                        if (numeroCondicionActual != null)
                                                        {
                                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                            NumerosVinculados_CondicionAnterior.Add(numeroCondicionActual);

                                                            if (ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                        }

                                                        if (!OperandosVinculados_CondicionAnterior.Contains(operandoValoresActual))
                                                        {
                                                            OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                        }

                                                        CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(numeroValoresActual))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);

                                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                               numeroItem.TextosInformacion_CumplenCondiciones_Anteriores,
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
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            numeroItem.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, numeroItem.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            numeroItem.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = comparadorTextos.TextosCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, numeroItem.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;

                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }
                                                else
                                                {
                                                    if (comparadorTextos.Interseccion(TextosElemento, valoresCondicion, numeroItem.TextosInformacion_CumplenCondiciones_Anteriores))
                                                    {
                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, numeroItem.TextosInformacion_CumplenCondiciones_Anteriores, true);

                                                        {
                                                            TextosInformacionInvolucrados.Clear();
                                                            numeroItem.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                        }


                                                        if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            numeroItem.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                        }

                                                        valorCondicion = true;
                                                        
                                                        TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(numeroItem))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(numeroItem);

                                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroItem);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroItem);

                                                        if (operandoCondicionActual != null)
                                                        {
                                                            CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoCondicion.Add(operandoCondicionActual);
                                                            OperandosVinculados_CondicionAnterior.Add(operandoCondicionActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoCondicionActual);
                                                        }
                                                        if (numeroCondicionActual != null)
                                                        {
                                                            CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoCondicion.Add(numeroCondicionActual);
                                                            NumerosVinculados_CondicionAnterior.Add(numeroCondicionActual);

                                                            if (ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
                                                        }

                                                        if (!OperandosVinculados_CondicionAnterior.Contains(operandoValoresActual))
                                                        {
                                                            OperandosVinculados_CondicionAnterior.Add(operandoValoresActual);
                                                            OperandosVinculados_CondicionAnterior_Temp.Add(operandoValoresActual);
                                                        }
                                                        CantidadesTextos.LastOrDefault().OperandosAsociados_OperandoValores.Add(operandoValoresActual);

                                                        if (!NumerosVinculados_CondicionAnterior.Contains(numeroValoresActual))
                                                        {
                                                            NumerosVinculados_CondicionAnterior.Add(numeroValoresActual);

                                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                                        }
                                                        CantidadesTextos.LastOrDefault().NumerosAsociados_OperandoValores.Add(numeroValoresActual);

                                                        AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                numeroItem.TextosInformacion_CumplenCondiciones_Anteriores,
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
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            numeroItem.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, numeroItem.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                        {
                                                            TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            numeroItem.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            if (numeroValoresActual != null)
                                                                numeroValoresActual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                        }

                                                        CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = comparadorTextos.TextosCumplenCondicion.Count;
                                                        CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                    }
                                                    else
                                                    {
                                                        NumerosNoCumplenCondicion_Elemento += 1;
                                                        NumerosNoCumplenCondicion_Valores += 1;

                                                        TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, numeroItem.TextosInformacion_CumplenCondiciones_Anteriores);
                                                        TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                        CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                    }
                                                }

                                                indiceValores++;
                                            }
                                        }

                                        if (!conNumerosTextos)
                                        {
                                            sinNumerosTextos = true;
                                        }

                                        break;
                                    }
                                
                            }
                            else if (elementoEjecucion.GetType() == typeof(ElementoConjuntoTextosEntradaEjecucion))
                            {
                                if (elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar >=
                                                ((ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion).FilasTextosInformacion.Count())
                                {
                                    if (ReiniciarPosicion_AlFinalCantidadesOperando)
                                    {
                                        elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                                        
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

                                if (((OperandosVinculados_CondicionAnterior.Contains(elementoEjecucion) &&
                                                ((ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion).FilasTextosInformacion.Any(i => FilasVinculadas_AgregarCondicionAnterior.Contains(i))) ||
                                                (OperandosVinculados_CondicionAnterior.Contains(elementoEjecucion)))
                                    && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                                {
                                    OperandosVinculados_AgregarCondicionAnterior.Add(elementoEjecucion);

                                    if (OperandoSubElemento_Condicion_TextosInformacion == null)
                                    {
                                        foreach (var item in ((ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion).FilasTextosInformacion)
                                        {
                                            if (FilasVinculadas_CondicionAnterior.Contains(item))
                                            {
                                                FilasVinculadas_AgregarCondicionAnterior.Add(item);
                                                CantidadNumerosCondicion_TextosInformacion++;

                                                if (!IncluirSoloNombreElemento)
                                                {
                                                    CantidadTextosCondicion_TextosInformacion += item.TextosInformacion.Count;
                                                }
                                                if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    CantidadTextosCondicion_TextosInformacion++;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    OperandosVinculados_CondicionAnterior.Clear();
                                    OperandosVinculados_CondicionAnterior.Add(elementoEjecucion);

                                    OperandosVinculados_CondicionAnterior_Temp.Clear();
                                    OperandosVinculados_CondicionAnterior_Temp.Add(elementoEjecucion);

                                    FilasVinculadas_CondicionAnterior.Clear();

                                    OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                        new InfoOpcion_VinculadosAnterior()
                                        {
                                            OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion,
                                            OperandoRelacionado_Ejecucion = elementoEjecucion
                                        });

                                    if (!listaValoresCondicion.Any())
                                    {
                                        sinNumerosTextos_Valores = true;
                                    }

                                    switch (OpcionSeleccionNumerosElemento_Condicion)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                            var elementoEjecucion_Actual = (ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion;

                                            FilaTextosInformacion_Entrada numeroValoresActual = null;

                                            List<FilaTextosInformacion_Entrada> FilasInvolucradas = new List<FilaTextosInformacion_Entrada>();

                                            if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando ||
                                                OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando)
                                                FilasInvolucradas = (elementoEjecucion_Actual).FilasTextosInformacion;
                                            else if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual ||
                                                OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual)
                                                FilasInvolucradas = ((ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion_Actual).FilasTextosInformacion
                                                    .Where(i => ((ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion_Actual).FilasTextosInformacion.IndexOf(i) <= elementoEjecucion_Actual.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar).ToList();
                                            else if (OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion ||
                                                OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion ||
                                                OpcionSeleccionNumerosElemento_Condicion == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion)
                                            {
                                                if (elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar <
                                                ((ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion).FilasTextosInformacion.Count())
                                                {
                                                    int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar,
                                                        OpcionSeleccionNumerosElemento_Condicion,
                                                        ((ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion).FilasTextosInformacion.Count());

                                                    FilasInvolucradas.Add(((ElementoConjuntoTextosEntradaEjecucion)elementoEjecucion).FilasTextosInformacion[indicePosicion]);

                                                }
                                            }

                                            bool sinNumerosTextosElemento = false;
                                            bool conNumerosTextos = false;

                                            foreach (var itemOperando in FilasInvolucradas)
                                            {
                                                CantidadNumerosCondicion_TextosInformacion++;

                                                int indiceValores = 0;

                                                foreach (var itemValoresCondicion in listaValoresCondicion)
                                                {
                                                    valoresCondicion = itemValoresCondicion;

                                                    if (filasOperandoValores.Any())
                                                        numeroValoresActual = filasOperandoValores[indiceValores <= filasOperandoValores.Count - 1 ? indiceValores : filasOperandoValores.Count - 1];

                                                    List<string> TextosElemento = new List<string>();
                                                    CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionTextosInformacion());

                                                    if (indiceCantidadesTextos_Valores < CantidadesTextos.Count)
                                                    {
                                                        CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion_TextosInformacion = //CantidadesTextos_Valores[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion_TextosInformacion;
                                                        indiceCantidadesTextos_Valores++;
                                                    }

                                                    if (!IncluirSoloNombreElemento)
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion += itemOperando.TextosInformacion.Count;
                                                        TextosElemento.AddRange(itemOperando.TextosInformacion);
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion = itemOperando.TextosInformacion.Count;
                                                    }
                                                    if (IncluirNombreElementoConTextos || IncluirSoloNombreElemento)
                                                    {
                                                        CantidadTextosCondicion_TextosInformacion++;
                                                        TextosElemento.Add(itemOperando.TextosInformacion.FirstOrDefault());
                                                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion_TextosInformacion++;
                                                    }

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

                                                    if (TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto)
                                                    {
                                                        if (((TextosElemento.Any(i => i != null) && !sinNumerosTextosElemento) || (!TextosElemento.Any(i => i != null) && (sinNumerosTextosElemento))) && !comparadorTextos.Interseccion(TextosElemento, valoresCondicion, elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores))
                                                        {
                                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores, true);

                                                            {
                                                                TextosInformacionInvolucrados.Clear();
                                                                elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                            }

                                                            if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                            {
                                                                TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                                elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            }

                                                            valorCondicion = true;
                                                            TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));

                                                            if (!FilasVinculadas_CondicionAnterior.Contains(itemOperando))
                                                                FilasVinculadas_CondicionAnterior.Add(itemOperando);

                                                            AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                    elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores,
                                                    elementoEjecucionCondicion_Valores_ConjuntoEntrada,
                                                    elementoEjecucionCondicion_Valores_Operacion,
                                                    elementoEjecucionCondicion_Valores_SubOperacion,
                                                    CantidadesTextos.LastOrDefault(),
                                                    valoresCondicion,
                                                    elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion);

                                                            comparadorTextos.TextosInformacionInvolucrados.Clear();

                                                            NumerosCumplenCondicion_Elemento += 1;
                                                            NumerosCumplenCondicion_Valores += 1;

                                                            TextosCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                            TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                            if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                                            {
                                                                TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                                elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            }

                                                            CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                            CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;

                                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores);
                                                            TextosCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;
                                                            TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;

                                                            if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                            {
                                                                TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                                elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosNoCumplenCondicion);
                                                            }

                                                            CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                            CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = comparadorTextos.TextosCumplenCondicion.Count;
                                                        }
                                                        else
                                                        {
                                                            NumerosNoCumplenCondicion_Elemento += 1;
                                                            NumerosNoCumplenCondicion_Valores += 1;

                                                            TextosNoCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;

                                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores);
                                                            TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;

                                                            CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (comparadorTextos.Interseccion(TextosElemento, valoresCondicion, elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores))
                                                        {
                                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores, true);

                                                            {
                                                                TextosInformacionInvolucrados.Clear();
                                                                elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores.Clear();
                                                            }

                                                            if (!(CantidadTextosInformacion_SoloCadenasCumplen |
                                                CantidadTextosInformacion_SoloCadenasCumplen_Valores))
                                                            {
                                                                TextosInformacionInvolucrados.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                                elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));
                                                            }

                                                            valorCondicion = true;
                                                            TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(comparadorTextos.TextosInformacionInvolucrados));

                                                            if (!FilasVinculadas_CondicionAnterior.Contains(itemOperando))
                                                                FilasVinculadas_CondicionAnterior.Add(itemOperando);

                                                            AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(comparadorTextos.TextosInformacionInvolucrados,
                                                    elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores,
                                                    elementoEjecucionCondicion_Valores_ConjuntoEntrada,
                                                    elementoEjecucionCondicion_Valores_Operacion,
                                                    elementoEjecucionCondicion_Valores_SubOperacion,
                                                    CantidadesTextos.LastOrDefault(),
                                                    valoresCondicion,
                                                    elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion);

                                                            comparadorTextos.TextosInformacionInvolucrados.Clear();

                                                            NumerosCumplenCondicion_Elemento += 1;
                                                            NumerosCumplenCondicion_Valores += 1;

                                                            TextosCumplenCondicion_Valores += comparadorTextos.TextosCumplenCondicion.Count;
                                                            TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                            if (CantidadTextosInformacion_SoloCadenasCumplen_Valores)
                                                            {
                                                                TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                                elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            }

                                                            CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = comparadorTextos.TextosCumplenCondicion.Count;
                                                            CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = comparadorTextos.TextosNoCumplenCondicion.Count;

                                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores);
                                                            TextosCumplenCondicion_Elemento += comparadorTextos.TextosCumplenCondicion.Count;
                                                            TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                            if (CantidadTextosInformacion_SoloCadenasCumplen)
                                                            {
                                                                TextosInformacionInvolucrados.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                                elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores.AddRange(comparadorTextos.TextosCumplenCondicion);
                                                            }

                                                            CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = comparadorTextos.TextosCumplenCondicion.Count;
                                                            CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = comparadorTextos.TextosNoCumplenCondicion.Count;
                                                        }
                                                        else
                                                        {
                                                            NumerosNoCumplenCondicion_Elemento += 1;
                                                            NumerosNoCumplenCondicion_Valores += 1;

                                                            TextosNoCumplenCondicion_Valores += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                            comparadorTextos.ContarInterseccion(TextosElemento, valoresCondicion, elementoEjecucion_Actual.TextosInformacion_CumplenCondiciones_Anteriores);
                                                            TextosNoCumplenCondicion_Elemento += comparadorTextos.TextosNoCumplenCondicion.Count;

                                                            CantidadesTextos.Remove(CantidadesTextos.LastOrDefault());
                                                        }
                                                    }

                                                    indiceValores++;
                                                }
                                            }

                                            if (!FilasInvolucradas.Any() || !conNumerosTextos)
                                                sinNumerosTextos = true;


                                            break;
                                    }
                                }
                            }

                        }
                    }

                    break;

                case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada:

                    var elementosAgregar_CondicionEvaluar = new List<ElementoEjecucionCalculo>();
                    var subElementosAgregar_CondicionEvaluar = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
                    var numerosAgregar_CondicionEvaluar = new List<EntidadNumero>();

                    switch (TipoElemento_Valores)
                    {
                        case TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos:
                            valoresCondicion = Valores_Condicion.Split('|');
                            ConValoresNoOperandos = true;
                            break;

                        case TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.Valores_DesdeElementoOperacion:

                            if (ElementoOperacion_Valores_ElementoAsociado != null ||
                                EsOperandoValoresTextosActual)
                            {
                                var elementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores_ElementoAsociado);
                                var subElementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)ejecucion.ObtenerSubElementoEjecucion(SubElementoOperacion_Valores);

                                if(EsOperandoValoresTextosActual)
                                {
                                    elementoEjecucionCondicion = operando;

                                    if(operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                        subElementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)operando;

                                }

                                if (elementoEjecucionCondicion != null)
                                {
                                    if (subElementoEjecucionCondicion != null)
                                    {
                                        if (subElementoEjecucionCondicion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion))
                                        {
                                            elementoEjecucionCondicion = (ElementoOperacionAritmeticaEjecucion)subElementoEjecucionCondicion;
                                        }
                                    }

                                    if (elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar >=
                                                        (elementoEjecucionCondicion).Numeros.Count(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))))
                                    {
                                        if (ReiniciarPosicion_AlFinalCantidadesOperando_Valores)
                                        {
                                            elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                                            
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
                                    List<string> Numeros = new List<string>();

                                    listaCantidades = (elementoEjecucionCondicion).Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                            (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))) &&
                                        //(!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                        //(i.ElementosSalidaOperacion_Agrupamiento.Any() &&
                                        //i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionCondicionEjecucion))) &
                                        ((!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionCondicionEjecucion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionCondicionEjecucion))))).ToList();


                                    if (OpcionValorPosicion != TipoOpcionPosicion.Ninguna)
                                    {
                                        List<string> PosicionesAgregadas = new List<string>();
                                        for (int posicion = 1; posicion <= listaCantidades.LongCount(); posicion++)
                                        {
                                            PosicionesAgregadas.Add(posicion.ToString());
                                        }

                                        valoresCondicion = PosicionesAgregadas.ToArray();
                                    }

                                    elementoEjecucionCondicion_Valores_ConjuntoEntrada = (ElementoEntradaEjecucion)elementoEjecucionCondicion;

                                    if (OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion &&
                                        listaCantidades.Contains(numero))
                                    {
                                        valoresCondicion = new string[] { numero.Numero.ToString() };
                                        numerosOperandoValores.Add(numero);

                                        switch (OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada)
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
                                                
                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                                
                                                operandosValores.Add(elementoEjecucionCondicion);

                                                numerosOperandoValores.AddRange(listaCantidades
                                                    .Where(i => listaCantidades.IndexOf(i) <= elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar));
                                                break;
                                        }
                                    }
                                    else if (!(OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando |
                                OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual) && 
                                        OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion)
                                        && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                                    {
                                        var opcion = ObtenerOpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior(elementoEjecucionCondicion, null);

                                        switch (opcion)
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

                                                if (elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar <
                                                    listaCantidades.Count)
                                                {
                                                    operandosValores.Add(elementoEjecucionCondicion);

                                                    int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar,
                                                        OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada,
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
                                                    .Where(i => listaCantidades.IndexOf(i) <= elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar))
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
                                                    .Where(i => listaCantidades.IndexOf(i) <= elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar))
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

                                        OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                            new InfoOpcion_VinculadosAnterior()
                                            {
                                                OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada,
                                                OperandoRelacionado_Ejecucion = elementoEjecucionCondicion
                                            });

                                        switch (OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada)
                                        {
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                                foreach (var item in (elementoEjecucionCondicion).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))))
                                                {
                                                    numerosOperandoValores.Add(item);
                                                }

                                                break;

                                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                            case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:

                                                foreach (var item in (elementoEjecucionCondicion).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto))))
                                                    .Where(i => (elementoEjecucionCondicion).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar))
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
                                                //operandosValores.Add(elementoEjecucionCondicion);

                                                int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEjecucionCondicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar,
                                                    OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada,
                                                    (elementoEjecucionCondicion).Numeros.Count(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))));

                                                if (indicePosicion <
                                                    (elementoEjecucionCondicion).Numeros.Count(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))))
                                                {
                                                    
                                                    

                                                    numerosOperandoValores.Add((elementoEjecucionCondicion).Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count && i == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores]) || !(elementoEjecucionCondicion.IndicePosicionClasificadores < elementoEjecucionCondicion.Clasificadores_Cantidades.Count))) ||
                                                (elementoEjecucionCondicion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEjecucionCondicion.Clasificadores_Cantidades[elementoEjecucionCondicion.IndicePosicionClasificadores].CadenaTexto)))).ToList()[indicePosicion]);
                                                }

                                                break;
                                        }

                                    }

                                    foreach (ElementoOperacionAritmeticaEjecucion itemOperando in operandosValores)
                                    {
                                        foreach(var itemNumero in itemOperando.Numeros.Intersect(numerosOperandoValores))
                                        {
                                            Numeros.Add(itemNumero.Numero.ToString());
                                        }
                                    }

                                    valoresCondicion = Numeros.ToArray();
                                    CantidadNumerosValoresCondicion += Numeros.Count;

                                    if (ElementoOperacion_Valores_ElementoAsociado != ElementoCondicion ||
                                        !(CondicionAnterior_ConValoresNoOperandos && 
                                        OperandosVinculados_CondicionAnterior.Any(i => i.ElementoDiseñoRelacionado == ElementoCondicion)))
                                    {
                                        OperandosVinculados_CondicionAnterior.Clear();
                                        NumerosVinculados_CondicionAnterior.Clear();

                                        OperandosVinculados_CondicionAnterior_Temp.Clear();
                                        NumerosVinculados_CondicionAnterior_Temp.Clear();

                                        //OperandosIguales = true;
                                    }

                                    if (OpcionValorPosicion != TipoOpcionPosicion.Ninguna)
                                    {
                                        List<string> PosicionesAgregadas = new List<string>();
                                        for (int posicion = 1; posicion <= Numeros.LongCount(); posicion++)
                                        {
                                            PosicionesAgregadas.Add(posicion.ToString());
                                        }

                                        valoresCondicion = PosicionesAgregadas.ToArray();
                                    }

                                }
                            }
                            else
                                valoresCondicion = new string[] { };

                            break;
                    }

                    //if (valoresCondicion == null || !valoresCondicion.Any())
                    //    sinNumerosTextos_Valores = true;

                    if (valoresCondicion == null)
                        valoresCondicion = new string[1] { string.Empty };

                    var elementoEncontrado = ejecucion.ObtenerElementoEjecucion(ElementoCondicion);
                    var subElementoEncontrado = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion);

                    if(EsOperandoActual)
                    {
                        elementoEncontrado = operando;

                        if(operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                            subElementoEncontrado = (ElementoDiseñoOperacionAritmeticaEjecucion)operando;
                    }

                    if (((!EsOperandoActual && !EsOperandoValoresActual) && ElementoOperacion_Valores_ElementoAsociado == ElementoCondicion) ||
                        (EsOperandoActual && (operando.ElementoDiseñoRelacionado == ElementoOperacion_Valores_ElementoAsociado) ||
                        (EsOperandoValoresActual && operando.ElementoDiseñoRelacionado == ElementoCondicion)))
                    {
                        OperandosIguales = true;
                    }

                    if (elementoEncontrado != null)
                    {
                        int cantidadNumeros = 0;
                        List<EntidadNumero> numerosElemento = new List<EntidadNumero>();
                        
                        if (OperandoSubElemento_Condicion != null &&
                            subElementoEncontrado != null)
                        {
                            elementoEncontrado = (ElementoOperacionAritmeticaEjecucion)subElementoEncontrado;
                        }

                        List<EntidadNumero> Numeros = new List<EntidadNumero>();

                        if (elementoEncontrado.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) || 
                            elementoEncontrado.GetType() == typeof(ElementoEntradaEjecucion) ||
                            elementoEncontrado.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion))
                        {
                            Numeros = ((ElementoOperacionAritmeticaEjecucion)elementoEncontrado).Numeros;
                        }

                        if (elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar >=
                                Numeros.Count(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))))
                        {
                            if (ReiniciarPosicion_AlFinalCantidadesOperando)
                            {
                                elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                                
                            }
                            else
                            {
                                if (!SeguirAplicandoCondicion_AlFinalCantidadesOperando &&
                                        Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                            (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))).Contains(numero))
                                    if (CumpleCondicion_ElementoSinNumeros)
                                        return true;
                                    else
                                        return false;
                            }
                        }

                        List<EntidadNumero> listaCantidades = new List<EntidadNumero>();

                        listaCantidades = Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))).ToList();

                        if (OpcionSeleccionNumerosElemento_Condicion_Elemento == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion &&
                            listaCantidades.Contains(numero))
                        {
                            switch (OpcionSeleccionNumerosElemento_Condicion_Elemento)
                            {
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionMitadDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionUltimaDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPenultimaDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionPrimeraDeActualEjecucion:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:

                                    elementosOperandoCondicion.Add(elementoEncontrado);
                                    operandosCondicion.Add(elementoEncontrado);
                                    numerosOperandoCondicion.Add(numero);

                                    break;

                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:

                                    elementosOperandoCondicion.Add(elementoEncontrado);
                                    operandosCondicion.Add(elementoEncontrado);
                                    numerosOperandoCondicion.AddRange(listaCantidades);
                                    break;

                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:

                                    elementosOperandoCondicion.Add(elementoEncontrado);
                                    operandosCondicion.Add(elementoEncontrado);
                                    numerosOperandoCondicion.AddRange(listaCantidades
                                        .Where(i => listaCantidades.IndexOf(i) <= elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar));
                                    break;
                            }
                        }
                        else if (!(OpcionSeleccionNumerosElemento_Condicion_Elemento == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando |
                                OpcionSeleccionNumerosElemento_Condicion_Elemento == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual) &&
                                OperandosVinculados_CondicionAnterior.Contains(elementoEncontrado)
                && TipoConector == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                        {
                            var opcion = ObtenerOpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior(elementoEncontrado, null);
                            
                            switch (opcion)
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

                                    if (elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar <
                                        listaCantidades.Count)
                                    {
                                        int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar,
                                            OpcionSeleccionNumerosElemento_Condicion_Elemento,
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

                                    foreach (var item in Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                        (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))))
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

                                    foreach (var item in Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                        (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto))))
                                        .Where(i => Numeros.Where(i => ((!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                        (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar))
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

                                    foreach (var item in Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                        (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))))
                                    {

                                        CantidadNumerosCondicion_OperacionEntrada++;
                                        cantidadNumeros++;

                                        numerosOperandoCondicion.Add(item);

                                    }

                                    break;

                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                    operandosCondicion.Add(elementoEncontrado);

                                    foreach (var item in Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                        (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto))))
                                        .Where(i => Numeros.Where(i => (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count && i == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores]) || !(elementoEncontrado.IndicePosicionClasificadores < elementoEncontrado.Clasificadores_Cantidades.Count))) ||
                                        (elementoEncontrado.Clasificadores_Cantidades.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == elementoEncontrado.Clasificadores_Cantidades[elementoEncontrado.IndicePosicionClasificadores].CadenaTexto)))).ToList().IndexOf(i) <= elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar))
                                    {

                                        CantidadNumerosCondicion_OperacionEntrada++;
                                        cantidadNumeros++;

                                        numerosOperandoCondicion.Add(item);

                                    }

                                    break;
                            }

                        }
                        else
                        {
                            //operandosCondicion.Add(elementoEncontrado);

                            switch (OpcionSeleccionNumerosElemento_Condicion_Elemento)
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

                                    if (elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar <
                                        listaCantidades.Count)
                                    {
                                        int indicePosicion = ObtenerPosicionCantidades_CondicionEjecucion(elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar,
                                            OpcionSeleccionNumerosElemento_Condicion_Elemento,
                                            listaCantidades.Count);

                                        {                                            
                                            numerosOperandoCondicion.Add(listaCantidades[indicePosicion]);
                                        }
                                    }

                                    break;

                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                    operandosCondicion.Add(elementoEncontrado);

                                    foreach (var item in listaCantidades)
                                    {
                                        {
                                            numerosOperandoCondicion.Add(item);
                                        }
                                    }

                                    break;

                                case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual:
                                    operandosCondicion.Add(elementoEncontrado);

                                    foreach (var item in listaCantidades
                                        .Where(i => listaCantidades.ToList().IndexOf(i) <= elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar))
                                    {                                        
                                        {
                                            numerosOperandoCondicion.Add(item);
                                        }
                                    }

                                    break;

                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando:
                                    operandosCondicion.Add(elementoEncontrado);

                                    foreach (var item in listaCantidades)
                                    {
                                        numerosOperandoCondicion.Add(item);
                                    }

                                    break;

                                case TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual:
                                    operandosCondicion.Add(elementoEncontrado);

                                    foreach (var item in listaCantidades
                                        .Where(i => listaCantidades.ToList().IndexOf(i) <= elementoEncontrado.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar))
                                    {
                                        numerosOperandoCondicion.Add(item);
                                    }

                                    break;
                            }

                        }

                        NumerosVinculados_CondicionAnterior.Clear();
                        OperandosVinculados_CondicionAnterior.Clear();

                        NumerosVinculados_CondicionAnterior_Temp.Clear();
                        OperandosVinculados_CondicionAnterior_Temp.Clear();

                        foreach (ElementoOperacionAritmeticaEjecucion itemOperando in operandosCondicion)
                        {
                            OpcionesSeleccionNumerosElemento_Vinculados_CondicionAnterior.Add(
                                    new InfoOpcion_VinculadosAnterior()
                                    {
                                        OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior = OpcionSeleccionNumerosElemento_Condicion_Elemento,
                                        OperandoRelacionado_Ejecucion = itemOperando
                                    });

                            foreach (var itemNumero in itemOperando.Numeros.Intersect(numerosOperandoCondicion))
                            {
                                numerosElemento.Add(itemNumero);
                            }
                        }

                        cantidadNumeros = numerosElemento.Count;
                        CantidadTextosNumeroCondicion += numerosElemento.Select(i => i.Textos.Count).Sum();

                        if (!numerosElemento.Any())
                            sinNumerosTextos = true;

                        CantidadNumerosCondicion_OperacionEntrada += cantidadNumeros;

                        List<EntidadNumero> numerosEntidades = new List<EntidadNumero>();

                        switch (TipoSubElemento_Condicion)
                        {
                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                
                                {
                                    foreach (var itemNumero in numerosOperandoCondicion)
                                    {
                                        int posicionNumero = listaCantidades.IndexOf(itemNumero) + 1;

                                        EntidadNumero elementoNumeroEntidad2 = new EntidadNumero();
                                        elementoNumeroEntidad2.Numero = posicionNumero;
                                        numerosEntidades.Add(elementoNumeroEntidad2);

                                        string posicionLiteral = string.Empty;

                                        switch (OpcionValorPosicion)
                                        {
                                            case TipoOpcionPosicion.PosicionPrimera:
                                                if (itemNumero == listaCantidades.First())
                                                    posicionLiteral = "primera";
                                                break;

                                            case TipoOpcionPosicion.PosicionSegunda:
                                                if (posicionNumero == 2)
                                                    posicionLiteral = "segunda";
                                                break;

                                            case TipoOpcionPosicion.PosicionMitad:
                                                if (posicionNumero == Math.Round(listaCantidades.LongCount() / 2.0, MidpointRounding.ToPositiveInfinity))
                                                    posicionLiteral = "mitad";
                                                break;

                                            case TipoOpcionPosicion.PosicionPenultima:
                                                if (listaCantidades.LongCount() - posicionNumero == 1)
                                                    posicionLiteral = "penultima";
                                                break;

                                            case TipoOpcionPosicion.PosicionUltima:
                                                if (listaCantidades.LongCount() == posicionNumero)
                                                    posicionLiteral = "ultima";
                                                break;
                                        }

                                        if (!string.IsNullOrEmpty(posicionLiteral))
                                        {
                                            elementoNumeroEntidad2.Textos.Add(posicionLiteral);
                                        }

                                        posicionNumero++;
                                    }
                                }

                                numerosElemento.Clear();
                                numerosElemento.AddRange(numerosEntidades);

                                numerosEntidades.Clear();

                                break;

                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:

                                EntidadNumero elementoNumeroEntidad3 = new EntidadNumero();
                                elementoNumeroEntidad3.Numero = CantidadTextosNumeroCondicion;
                                numerosEntidades.Add(elementoNumeroEntidad3);

                                numerosElemento.Clear();
                                numerosElemento.AddRange(numerosEntidades);

                                numerosEntidades.Clear();

                                break;

                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:

                                EntidadNumero elementoNumeroEntidad4 = new EntidadNumero();
                                elementoNumeroEntidad4.Numero = operacionCondicionEjecucion.ElementosOperacion.IndexOf((ElementoOperacionAritmeticaEjecucion)elementoEncontrado);
                                numerosEntidades.Add(elementoNumeroEntidad4);

                                numerosElemento.Clear();
                                numerosElemento.AddRange(numerosEntidades);

                                numerosEntidades.Clear();

                                break;
                        }

                        var tipoCondicionValores = TipoSubElemento_Condicion_Valores;

                        if (tipoCondicionValores == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.Ninguno ||
                            TipoElemento_Valores == TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos)
                        {
                            tipoCondicionValores = TipoSubElemento_Condicion;
                        }

                        switch (tipoCondicionValores)
                        {
                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento:
                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.Ninguno:

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
                                break;

                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                

                                if (TipoElemento_Valores != TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos)
                                {
                                    {
                                        int posicionNumero = 1;
                                        foreach (var itemNumero in valoresCondicion)
                                        {
                                            EntidadNumero elementoNumeroEntidad2 = new EntidadNumero();
                                            elementoNumeroEntidad2.Numero = posicionNumero;
                                            numerosEntidades.Add(elementoNumeroEntidad2);

                                            string posicionLiteral = string.Empty;

                                            switch (OpcionValorPosicion)
                                            {
                                                case TipoOpcionPosicion.PosicionPrimera:
                                                    if (itemNumero == valoresCondicion.First())
                                                        posicionLiteral = "primera";
                                                    break;

                                                case TipoOpcionPosicion.PosicionSegunda:
                                                    if (posicionNumero == 2)
                                                        posicionLiteral = "segunda";
                                                    break;

                                                case TipoOpcionPosicion.PosicionMitad:
                                                    if (posicionNumero == Math.Round(valoresCondicion.LongLength / 2.0, MidpointRounding.ToZero))
                                                        posicionLiteral = "mitad";
                                                    break;

                                                case TipoOpcionPosicion.PosicionPenultima:
                                                    if (valoresCondicion.LongLength - posicionNumero == 1)
                                                        posicionLiteral = "penultima";
                                                    break;

                                                case TipoOpcionPosicion.PosicionUltima:
                                                    if (valoresCondicion.LongLength == posicionNumero)
                                                        posicionLiteral = "ultima";
                                                    break;
                                            }

                                            if (!string.IsNullOrEmpty(posicionLiteral))
                                            {
                                                elementoNumeroEntidad2.Textos.Add(posicionLiteral);
                                            }

                                            posicionNumero++;
                                        }
                                    }                                    
                                }
                                else
                                {
                                    if (valoresCondicion.Any(i => !string.IsNullOrEmpty(i)))
                                    {
                                        foreach (var itemNumero in valoresCondicion)
                                        {
                                            double num = 0;
                                            if (double.TryParse(itemNumero, out num))
                                            {
                                                EntidadNumero elementoNumeroEntidad2 = new EntidadNumero();
                                                elementoNumeroEntidad2.Numero = num;
                                                numerosEntidades.Add(elementoNumeroEntidad2);

                                                string posicionLiteral = string.Empty;

                                                switch (OpcionValorPosicion)
                                                {
                                                    case TipoOpcionPosicion.PosicionPrimera:
                                                        if (num == 1)
                                                            posicionLiteral = "primera";
                                                        break;

                                                    case TipoOpcionPosicion.PosicionSegunda:
                                                        if (num == 2)
                                                            posicionLiteral = "segunda";
                                                        break;

                                                    case TipoOpcionPosicion.PosicionMitad:
                                                        if (num == Math.Round(listaCantidades.LongCount() / 2.0, MidpointRounding.ToPositiveInfinity))
                                                            posicionLiteral = "mitad";
                                                        break;

                                                    case TipoOpcionPosicion.PosicionPenultima:
                                                        if (listaCantidades.LongCount() - num == 1)
                                                            posicionLiteral = "penultima";
                                                        break;

                                                    case TipoOpcionPosicion.PosicionUltima:
                                                        if (listaCantidades.LongCount() == num)
                                                            posicionLiteral = "ultima";
                                                        break;
                                                }

                                                if (!string.IsNullOrEmpty(posicionLiteral))
                                                {
                                                    elementoNumeroEntidad2.Textos.Add(posicionLiteral);
                                                }
                                            }
                                            else
                                                NumerosNoCumplenCondicion_Valores++;
                                        }
                                    }
                                    else
                                    {
                                        {
                                            long posicionNumero = 0;

                                            string posicionLiteral = string.Empty;

                                            switch (OpcionValorPosicion)
                                            {
                                                case TipoOpcionPosicion.PosicionPrimera:
                                                    posicionNumero = 1;
                                                    posicionLiteral = "primera";
                                                    break;

                                                case TipoOpcionPosicion.PosicionSegunda:
                                                    posicionNumero = 2;
                                                    posicionLiteral = "segunda";
                                                    break;

                                                case TipoOpcionPosicion.PosicionMitad:
                                                    posicionNumero = (long)Math.Round(listaCantidades.LongCount() / 2.0, MidpointRounding.ToPositiveInfinity);
                                                    posicionLiteral = "mitad";
                                                    break;

                                                case TipoOpcionPosicion.PosicionPenultima:
                                                    posicionNumero = listaCantidades.LongCount() - 1;
                                                    posicionLiteral = "penultima";
                                                    break;

                                                case TipoOpcionPosicion.PosicionUltima:
                                                    posicionNumero = listaCantidades.LongCount();
                                                    posicionLiteral = "ultima";
                                                    break;
                                            }

                                            EntidadNumero elementoNumeroEntidad2 = new EntidadNumero();
                                            elementoNumeroEntidad2.Numero = posicionNumero;
                                            numerosEntidades.Add(elementoNumeroEntidad2);

                                            if (!string.IsNullOrEmpty(posicionLiteral))
                                            {
                                                elementoNumeroEntidad2.Textos.Add(posicionLiteral);
                                            }
                                        }
                                    }
                                }
                                
                                break;

                            case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:

                                EntidadNumero elementoNumeroEntidad3 = new EntidadNumero();
                                elementoNumeroEntidad3.Numero = valoresCondicion.LongLength;
                                numerosEntidades.Add(elementoNumeroEntidad3);

                                break;
                        }

                        if(!numerosEntidades.Any())
                        {
                            sinNumerosTextos_Valores = true;
                        }

                        if (!numerosElemento.Any())
                        {
                            sinNumerosTextos = true;
                        }

                        if (CantidadNumeros_PorElemento &&
                            CantidadNumeros_PorElemento_Valores)
                        {
                            int indiceValores = 0;
                            int indiceCantidadCondicion = 0;

                            for (int indice = 0; indice < (numerosEntidades.Count > numerosElemento.Count ? numerosEntidades.Count : numerosElemento.Count); indice++)
                            {
                                ElementoEjecucionCalculo operandoValoresActual = null;

                                if (operandosValores.Any())
                                    operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

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

                                if(numerosEntidades.Any())
                                    nums.Add(numerosEntidades[indice < numerosEntidades.Count ? indice : numerosEntidades.Count - 1]);

                                if(numerosElemento.Any())
                                    numsElemento.Add(numerosElemento[indice < numerosElemento.Count ? indice : numerosElemento.Count - 1]);

                                CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionTextosInformacion());

                                if (TipoOpcionCondicion_ElementoOperacionEntrada == TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA)
                                {
                                    if (nums.Any() && !numsElemento.Any(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                    {
                                        valorCondicion = true;
                                        
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

                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
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

                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                        }

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento = numsElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento = numsElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                        NumerosCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento;
                                        NumerosNoCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento;

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = nums.Count(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = nums.Count(numero => numsElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                        NumerosCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores;
                                        NumerosNoCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores;

                                        if (1 == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento &
                                            1 == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores)
                                        {
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadCondicion = true;
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadValores = true;
                                        }
                                    }
                                    else
                                    {
                                        CantidadesNumeros.Remove(CantidadesNumeros.Last());
                                        //CantidadesNumeros_Valores.Remove(//CantidadesNumeros_Valores.Last());
                                    }
                                }
                                else
                                {
                                    if (numsElemento.Any(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                    {
                                        valorCondicion = true;
                                        
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

                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
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

                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                        }

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento = numsElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento = numsElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                        NumerosCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento;
                                        NumerosNoCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento;

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = nums.Count(numero => numsElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = nums.Count(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                        NumerosCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores;
                                        NumerosNoCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores;

                                        if (1 == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento &
                                            1 == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores)
                                        {
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadCondicion = true;
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadValores = true;
                                        }
                                    }
                                    else
                                    {
                                        CantidadesNumeros.Remove(CantidadesNumeros.Last());
                                        //CantidadesNumeros_Valores.Remove(//CantidadesNumeros_Valores.Last());
                                    }
                                }

                                indiceValores++;

                                if(indiceCantidadCondicion < numerosElemento.Count - 1)
                                {
                                    indiceCantidadCondicion++;
                                }
                            }
                        }
                        else if (!CantidadNumeros_PorElemento &&
                            CantidadNumeros_PorElemento_Valores)
                        {
                            int indiceValores = 0;
                            int indiceCantidadCondicion = 0;

                            foreach (var item in numerosEntidades)
                            {
                                ElementoEjecucionCalculo operandoValoresActual = null;

                                if (operandosValores.Any())
                                    operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

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
                                nums.Add(item);

                                CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionTextosInformacion());

                                if (TipoOpcionCondicion_ElementoOperacionEntrada == TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA)
                                {
                                    if (nums.Any() && !numerosElemento.Any(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                    {
                                        valorCondicion = true;
                                        
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
                                            
                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoCondicion
                                                        .Where(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));
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

                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                        }

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento = numerosElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento = numerosElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                        NumerosCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento;
                                        NumerosNoCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento;

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = nums.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = nums.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                        NumerosCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores;
                                        NumerosNoCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores;

                                        if (VerificarCantidadNumerosCumplenCondicion(true, true, CantidadesNumeros.LastOrDefault(), null, 
                                            numerosElemento.Count, CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento) &&
                                            1 == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores)
                                        {
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadCondicion = true;
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadValores = true;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        CantidadesNumeros.Remove(CantidadesNumeros.Last());
                                    }
                                }
                                else
                                {
                                    if (numerosElemento.Any(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                    {
                                        valorCondicion = true;
                                        
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
                                            
                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoCondicion
                                                        .Where(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));
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

                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroValoresActual);
                                        }

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento = numerosElemento.Count(numero => nums.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento = numerosElemento.Count(numero => nums.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                        NumerosCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento;
                                        NumerosNoCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento;

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = nums.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = nums.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                        NumerosCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores;
                                        NumerosNoCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores;

                                        if (VerificarCantidadNumerosCumplenCondicion(true, true, CantidadesNumeros.LastOrDefault(), null,
                                            numerosElemento.Count, CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento) &&
                                            1 == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores)
                                        {
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadCondicion = true;
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadValores = true;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        CantidadesNumeros.Remove(CantidadesNumeros.Last());
                                    }
                                }

                                indiceValores++;

                                if (indiceCantidadCondicion < numerosElemento.Count - 1)
                                {
                                    indiceCantidadCondicion++;
                                }
                            }
                        }
                        else if (CantidadNumeros_PorElemento &&
                            !CantidadNumeros_PorElemento_Valores)
                        {
                            int indiceValores = 0;
                            int indiceCantidadCondicion = 0;

                            foreach (var item in numerosElemento)
                            {
                                ElementoEjecucionCalculo operandoValoresActual = null;

                                if (operandosValores.Any())
                                    operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

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
                                numsElemento.Add(item);

                                CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionTextosInformacion());

                                if (TipoOpcionCondicion_ElementoOperacionEntrada == TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA)
                                {
                                    if (numerosEntidades.Any() && !numsElemento.Any(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                    {
                                        valorCondicion = true;
                                        
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

                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
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
                                            
                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoValores
                                                        .Where(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));
                                        }

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento = numsElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento = numsElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                        NumerosCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento;
                                        NumerosNoCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento;

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = numerosEntidades.Count(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = numerosEntidades.Count(numero => numsElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                        NumerosCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores;
                                        NumerosNoCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores;

                                        if (1 == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento &&
                                            VerificarCantidadNumerosCumplenCondicion(false, true, CantidadesNumeros.LastOrDefault(), null,
                                            numerosEntidades.Count, CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores))
                                        {
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadCondicion = true;
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadValores = true;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        CantidadesNumeros.Remove(CantidadesNumeros.Last());
                                    }
                                }
                                else
                                {
                                    if (numsElemento.Any(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                    {
                                        valorCondicion = true;
                                        
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

                                            if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.Add(numeroCondicionActual);
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
                                            
                                            if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                                NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoValores
                                                        .Where(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));
                                        }

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento = numsElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento = numsElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                        NumerosCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento;
                                        NumerosNoCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento;

                                        CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = numerosEntidades.Count(numero => numsElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                        CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = numerosEntidades.Count(numero => numsElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                        NumerosCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores;
                                        NumerosNoCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores;

                                        if (1 == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento &
                                            VerificarCantidadNumerosCumplenCondicion(false, true, CantidadesNumeros.LastOrDefault(), null,
                                            numerosEntidades.Count, CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores))
                                        {
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadCondicion = true;
                                            CantidadesNumeros.LastOrDefault().CumpleCantidadValores = true;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        CantidadesNumeros.Remove(CantidadesNumeros.Last());
                                    }
                                }

                                indiceValores++;

                                if (indiceCantidadCondicion < numerosElemento.Count - 1)
                                {
                                    indiceCantidadCondicion++;
                                }
                            }
                        }
                        else if (!CantidadNumeros_PorElemento &&
                            !CantidadNumeros_PorElemento_Valores)
                        {
                            int indiceValores = 0;

                            ElementoEjecucionCalculo operandoValoresActual = null;

                            if (operandosValores.Any())
                                operandoValoresActual = operandosValores[indiceValores <= operandosValores.Count - 1 ? indiceValores : operandosValores.Count - 1];

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

                            CantidadesNumeros.Add(new InformacionCantidadesNumerosInformacion_CondicionTextosInformacion());

                            if (TipoOpcionCondicion_ElementoOperacionEntrada == TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA)
                            {
                                if (numerosEntidades.Any() && !numerosElemento.Any(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                {
                                    valorCondicion = true;
                                    
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
                                                                                
                                        if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                            NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoCondicion
                                                    .Where(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));
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
                                        
                                        if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                            NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoValores
                                                    .Where(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));
                                    }

                                    CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                    CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                    NumerosCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento;
                                    NumerosNoCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento;

                                    CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                    CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                    NumerosCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores;
                                    NumerosNoCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores;

                                    if (numerosElemento.Count == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento &
                                        numerosEntidades.Count == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores)
                                    {
                                        CantidadesNumeros.LastOrDefault().CumpleCantidadCondicion = true;
                                        CantidadesNumeros.LastOrDefault().CumpleCantidadValores = true;
                                    }
                                }
                                //if (numeros.Any() && !numerosElemento.Intersect(numeros, new ComparadorNumeros_Condicion(
                                //            TipoOpcionCondicion_ElementoOperacionEntrada)).Any())
                                //    valorCondicion = true;
                            }
                            else
                            {
                                if (numerosElemento.Any(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))))
                                {
                                    valorCondicion = true;
                                    
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
                                        
                                        if(ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones)
                                            NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoCondicion
                                                    .Where(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numero, numeroCondicion))));
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
                                        
                                        if(ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones)
                                            NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosOperandoValores
                                                    .Where(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada, OpcionValorPosicion)).Equals(numeroCondicion, numero))));
                                    }

                                    CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));
                                    CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento = numerosElemento.Count(numero => numerosEntidades.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numero, numeroCondicion)));

                                    NumerosCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento;
                                    NumerosNoCumplenCondicion_Elemento += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Elemento;

                                    CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => (new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));
                                    CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores = numerosEntidades.Count(numero => numerosElemento.Any(numeroCondicion => !(new ComparadorNumeros_Condicion(TipoOpcionCondicion_ElementoOperacionEntrada)).Equals(numeroCondicion, numero)));

                                    NumerosCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores;
                                    NumerosNoCumplenCondicion_Valores += CantidadesNumeros.LastOrDefault().NumerosNoCumplenCondicion_Valores;

                                    if (numerosElemento.Count == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Elemento &
                                        numerosEntidades.Count == CantidadesNumeros.LastOrDefault().NumerosCumplenCondicion_Valores)
                                    {
                                        CantidadesNumeros.LastOrDefault().CumpleCantidadCondicion = true;
                                        CantidadesNumeros.LastOrDefault().CumpleCantidadValores = true;
                                    }
                                }
                                //if (numerosElemento.Intersect(numeros, new ComparadorNumeros_Condicion(
                                //            TipoOpcionCondicion_ElementoOperacionEntrada)).Any())
                                //    valorCondicion = true;
                            }
                        }

                    }

                    break;
            }

            if (operando != null &&
                numero != null)
            {
                FiltrarTextosInformacionInvolucrados_OperandoEjecucion_Evaluacion_Condiciones(
                    operando.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                    operando.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null,
                    numero, VerificarSiOperandosCorresponden_AEjecucion(ejecucion,
                    false, operando.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                    operando.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null, numero));
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
                                    if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento == 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento == itemCantidad.TextosCumplenCondicion_Elemento)
                                        valorCondicionCantidad_Condicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion_TextosInformacion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
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
                                    if (itemCantidad.TextosCumplenCondicion_Elemento == 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.TextosNoCumplenCondicion_Elemento > 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion_TextosInformacion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.TextosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.TextosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.TextosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
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
                                    if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores == itemCantidad.TextosCumplenCondicion_Valores)
                                        valorCondicionCantidad_Valores = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion_TextosInformacion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
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

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion_TextosInformacion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.TextosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.TextosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.TextosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
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
                            if (NumerosCumplenCondicion_Elemento - NumerosNoCumplenCondicion_Elemento == NumerosCumplenCondicion_Elemento)
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
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion = CantidadNumerosValoresCondicion_TextosInformacion;
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
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion = CantidadNumerosValoresCondicion_TextosInformacion;
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
                            if (TextosCumplenCondicion_Elemento - TextosNoCumplenCondicion_Elemento == 0)
                                valorCondicion = false;

                            break;
                        case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                            if (TextosCumplenCondicion_Elemento - TextosNoCumplenCondicion_Elemento == TextosCumplenCondicion_Elemento)
                                valorCondicion = false;

                            break;

                        case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                            switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                            {
                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TextosCumplenCondicion_Elemento;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = CantidadTextosCondicion_TextosInformacion;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                    CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = CantidadTextosValoresCondicion_TextosInformacion;
                                    break;
                            }

                            switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (TextosCumplenCondicion_Elemento - TextosNoCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (TextosCumplenCondicion_Elemento - TextosNoCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (TextosCumplenCondicion_Elemento - TextosNoCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
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
                                if (TextosCumplenCondicion_Elemento == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (TextosNoCumplenCondicion_Elemento > 0)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TextosCumplenCondicion_Elemento;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = CantidadTextosCondicion_TextosInformacion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = CantidadTextosValoresCondicion_TextosInformacion;
                                        break;
                                }

                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (TextosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (TextosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (TextosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
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
                //if ((CantidadNumeros_PorElemento & !CantidadNumeros_PorElemento_Valores) ||
                //        (CantidadNumeros_PorElemento & CantidadNumeros_PorElemento_Valores) ||
                //        (!CantidadNumeros_PorElemento & CantidadNumeros_PorElemento_Valores))
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
                                    if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento == 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento == itemCantidad.NumerosCumplenCondicion_Elemento)
                                        valorCondicionCantidad_Condicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion_TextosInformacion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_OperacionEntrada)
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
                                    if (itemCantidad.NumerosCumplenCondicion_Elemento == 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.NumerosNoCumplenCondicion_Elemento > 0)
                                        valorCondicionCantidad_Condicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion_TextosInformacion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Condicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }

                        bool valorCondicionCantidad_Valores = true;

                        if (OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento == 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento == itemCantidad.NumerosCumplenCondicion_Elemento)
                                        valorCondicionCantidad_Valores = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion_TextosInformacion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento - itemCantidad.NumerosNoCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Valores = false;
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
                                    if (itemCantidad.NumerosCumplenCondicion_Elemento == 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidad.NumerosNoCumplenCondicion_Elemento > 0)
                                        valorCondicionCantidad_Valores = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion_TextosInformacion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Valores = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidad.NumerosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_OperacionEntrada)
                                                valorCondicionCantidad_Valores = false;
                                            break;
                                    }

                                    break;
                            }
                        }

                        if (valorCondicionCantidad_Condicion)
                        {
                            HayCantidadesCumpleCondicion = true;
                        }
                        else
                        {
                            if (CantidadNumeros_PorElemento)
                            {
                                QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, true, false, CantidadesNumeros);
                                QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior_Temp(itemCantidad, true, false);
                            }
                        }

                        if (valorCondicionCantidad_Valores)
                        {
                            HayCantidadesCumpleCondicion = true;
                        }
                        else
                        {
                            if (CantidadNumeros_PorElemento_Valores)
                            {
                                QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, false, true, CantidadesNumeros);
                                QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior_Temp(itemCantidad, false, true);
                            }
                        }
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
                if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    if ((CantidadTextosInformacion_PorElemento & !CantidadTextosInformacion_PorElemento_Valores) ||
                    (CantidadTextosInformacion_PorElemento & CantidadTextosInformacion_PorElemento_Valores) ||
                    (!CantidadTextosInformacion_PorElemento & CantidadTextosInformacion_PorElemento_Valores))
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
                                        if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento == 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento == itemCantidad.TextosCumplenCondicion_Elemento)
                                            valorCondicionCantidad_Condicion = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Elemento;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion_TextosInformacion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion_TextosInformacion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.TextosCumplenCondicion_Elemento - itemCantidad.TextosNoCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
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
                                        if (itemCantidad.TextosCumplenCondicion_Elemento == 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;
                                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                        if (itemCantidad.TextosNoCumplenCondicion_Elemento > 0)
                                            valorCondicionCantidad_Condicion = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Elemento;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion_TextosInformacion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion_TextosInformacion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.TextosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.TextosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.TextosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
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
                                        if (itemCantidad.TextosCumplenCondicion_Valores - itemCantidad.TextosNoCumplenCondicion_Valores == itemCantidad.TextosCumplenCondicion_Valores)
                                            valorCondicionCantidad_Valores = false;

                                        break;

                                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Elemento;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion_TextosInformacion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion_TextosInformacion;
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

                                        switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                        {
                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Elemento;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.TextosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosCondicion_TextosInformacion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidad.CantidadTextosValoresCondicion_TextosInformacion;
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


                    if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_Valores)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores == NumerosCumplenCondicion_Valores)
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
                                        CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosCondicion_TextosInformacion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosValoresCondicion_TextosInformacion;
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
                        if (!CantidadTextosInformacion_PorElemento & !CantidadTextosInformacion_PorElemento_Valores)
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
                                            CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosValoresCondicion_TextosInformacion;
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

                    if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (TextosCumplenCondicion_Valores - TextosNoCumplenCondicion_Valores == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (TextosCumplenCondicion_Valores - TextosNoCumplenCondicion_Valores == TextosCumplenCondicion_Valores)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TextosCumplenCondicion_Elemento;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = CantidadTextosCondicion_TextosInformacion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = CantidadTextosValoresCondicion_TextosInformacion;
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
                        if (!CantidadTextosInformacion_PorElemento & !CantidadTextosInformacion_PorElemento_Valores)
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

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TextosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = CantidadTextosCondicion_TextosInformacion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = CantidadTextosValoresCondicion_TextosInformacion;
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

                            if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
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
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Elemento;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.CantidadNumerosCondicion_TextosInformacion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion_TextosInformacion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;
                                        }

                                        break;
                                }
                            }
                            else
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
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
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Elemento;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.CantidadNumerosCondicion_TextosInformacion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion_TextosInformacion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Condicion = false;
                                                break;
                                        }

                                        break;
                                }
                            }

                            bool valorCondicionCantidad_Valores = true;

                            if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
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
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Elemento;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.CantidadNumerosCondicion_TextosInformacion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion_TextosInformacion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores - itemCantidad.NumerosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Valores = false;
                                                break;
                                        }

                                        break;
                                }
                            }
                            else
                            {
                                switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
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
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Elemento;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.NumerosCumplenCondicion_Valores;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.CantidadNumerosCondicion_TextosInformacion;
                                                break;

                                            case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                                CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = itemCantidad.CantidadNumerosValoresCondicion_TextosInformacion;
                                                break;
                                        }

                                        switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                        {
                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Valores = false;
                                                break;

                                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                                if (itemCantidad.NumerosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                    valorCondicionCantidad_Valores = false;
                                                break;
                                        }

                                        break;
                                }
                            }

                            if (valorCondicionCantidad_Condicion)
                            {
                                HayCantidadesCumpleCondicion = true;
                            }
                            else
                            {
                                if (CantidadNumeros_PorElemento)
                                {
                                    QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, true, false, CantidadesNumeros);
                                    QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior_Temp(itemCantidad, true, false);
                                }
                            }

                            if (valorCondicionCantidad_Valores)
                            {
                                HayCantidadesCumpleCondicion = true;
                            }
                            else
                            {
                                if (CantidadNumeros_PorElemento_Valores)
                                {
                                    QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(itemCantidad, false, true, CantidadesNumeros);
                                    QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior_Temp(itemCantidad, false, true);
                                }
                            }
                        }

                        if (!HayCantidadesCumpleCondicion)
                            valorCondicion = false;
                    }


                    if (OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
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
                                        CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = NumerosCumplenCondicion_Elemento;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = NumerosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = CantidadNumerosCondicion_OperacionEntrada;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = CantidadNumerosValoresCondicion;
                                        break;
                                }

                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (NumerosCumplenCondicion_Valores - NumerosNoCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
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
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
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
                                            CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = NumerosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = NumerosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = CantidadNumerosCondicion_OperacionEntrada;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = CantidadNumerosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (NumerosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (NumerosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (NumerosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada)
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

            if (((ConsiderarIncluirCondicionesHijas ||
                    (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones))) &&
                    (operando != null && numero != null))
                EstablecerConsiderarOperando(valorCondicion, ejecucion,
                    operando.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operando : null,
                    operando.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operando : null, numero);

            if (VaciarListaTextosInformacion_CumplenCondicion)
            {
                if ((VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple && valorCondicion)
                || (!VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple && !valorCondicion))
                    TextosInformacionCumplenCondicion.Clear();
            }

            if (NegarCondicion)
                return !valorCondicion;
            else
                return valorCondicion;
        }

        private bool VerificarCantidadNumerosCumplenCondicion(bool EsOperandoCondiciones_oValores, 
            bool EsCondicionOperandos_oCadenasTexto, InformacionCantidadesNumerosInformacion_CondicionTextosInformacion infoCantidadesNumeros,
            InformacionCantidadesNumerosInformacion_CondicionTextosInformacion infoCantidadesTextos,
            int CantidadTotal_Numeros,
            int CantidadNumerosActual)
        {
            bool verificacion = false;

            TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumeros = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.Ninguna;
            TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumeros = TipoOpcionCantidadNumerosCumplenCondicion.Ninguno;
            TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumeros = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Ninguno;

            if (EsCondicionOperandos_oCadenasTexto)
            {
                if (EsOperandoCondiciones_oValores)
                {
                    OpcionTipoCantidadSubNumeros = OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada;
                    OpcionCantidadSubNumeros = OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada;
                    OpcionCantidadDeterminadaSubNumeros = OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada;

                }
                else
                {
                    OpcionTipoCantidadSubNumeros = OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada;
                    OpcionCantidadSubNumeros = OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada;
                    OpcionCantidadDeterminadaSubNumeros = OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada;

                }
            }
            else
            {
                if (EsOperandoCondiciones_oValores)
                {
                    OpcionTipoCantidadSubNumeros = OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion;
                    OpcionCantidadSubNumeros = OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion;
                    OpcionCantidadDeterminadaSubNumeros = OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion;

                    //CantidadParametro = CantidadTotal_Numeros - infoCantidadesTextos.NumerosNoCumplenCondicion_Elemento;
                }
                else
                {
                    OpcionTipoCantidadSubNumeros = OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
                    OpcionCantidadSubNumeros = OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
                    OpcionCantidadDeterminadaSubNumeros = OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;

                    //CantidadParametro = CantidadTotal_Numeros - infoCantidadesTextos.NumerosNoCumplenCondicion_Valores;
                }
            }

            switch (OpcionCantidadSubNumeros)
            {
                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                    if (CantidadNumerosActual >= 1)
                        verificacion = true;
                    break;

                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:
                    int CantidadParametro = 0;

                    switch (OpcionTipoCantidadSubNumeros)
                    {
                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija:
                            CantidadParametro = CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada;
                            break;

                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                            CantidadParametro = CantidadTotal_Numeros;
                            break;

                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                            CantidadParametro = CantidadTotal_Numeros - infoCantidadesNumeros.NumerosNoCumplenCondicion_Elemento;
                            break;

                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                            CantidadParametro = CantidadTotal_Numeros - infoCantidadesNumeros.NumerosNoCumplenCondicion_Valores;
                            break;
                    }

                    switch (OpcionCantidadDeterminadaSubNumeros)
                    {
                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                            if (CantidadNumerosActual >= CantidadParametro)
                                verificacion = true;
                            break;

                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                            if (CantidadNumerosActual <= CantidadParametro)
                                verificacion = true;
                            break;

                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                            if (CantidadNumerosActual == CantidadParametro)
                                verificacion = true;

                            break;
                    }

                    break;

                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                    if (CantidadNumerosActual <= CantidadTotal_Numeros)
                        verificacion = true;
                    break;

            }

            return verificacion;
        }

        private void RestablecerValoresCondicion(CondicionTextosInformacion condicion)
        {
            foreach (var itemCondicion in condicion.Condiciones)
                itemCondicion.RestablecerValoresCondicion(itemCondicion);

            condicion.ValorCondiciones = false;
            condicion.CondicionAnterior_ConValoresNoOperandos = false;
            condicion.ConValoresNoOperandos = false;
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
            bool EvaluarOperandoValores,
            ElementoOperacionAritmeticaEjecucion elementoOperando = null,
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
                    var subElementoEjecucionValores_Filtrar_Textos = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Valores_TextosInformacion);

                    bool elementoOperandoContiene = false;

                    if (elementoEjecucionCondicion_Filtrar_Textos != null)
                    {
                        if (elementoEjecucionCondicion_Filtrar_Textos.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                        {
                            elementoOperandoContiene = ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionCondicion_Filtrar_Textos).ElementosOperacion.Contains(elementoOperando);
                        }
                        else if (elementoEjecucionCondicion_Filtrar_Textos.GetType() == typeof(ElementoEntradaEjecucion))
                        {
                            elementoOperandoContiene = ((ElementoEntradaEjecucion)elementoEjecucionCondicion_Filtrar_Textos).Numeros.Contains(numeroOperando);
                        }
                    }

                    bool subElementoOperandoContiene = false;

                    if(subElementoEjecucion_Filtrar_Textos != null)
                        subElementoOperandoContiene = subElementoEjecucion_Filtrar_Textos.Numeros.Contains(numeroOperando);

                    if (((elementoOperando != null && elementoEjecucionCondicion_Filtrar_Textos != null && (elementoOperando == elementoEjecucionCondicion_Filtrar_Textos | elementoOperandoContiene)) ||
                        (subElementoOperando != null && subElementoEjecucion_Filtrar_Textos != null && (subElementoOperando == subElementoEjecucion_Filtrar_Textos | subElementoOperandoContiene))) ||
                        ((elementoOperando == null && elementoEjecucionCondicion_Filtrar_Textos == null) ||
                        (subElementoOperando == null && subElementoEjecucion_Filtrar_Textos == null))) //||

                    //((elementoOperando != null && elementoEjecucionValores_Filtrar != null && elementoOperando == elementoEjecucionValores_Filtrar) ||
                    //(subElementoOperando != null && subElementoEjecucionValores_Filtrar != null && subElementoOperando == subElementoEjecucionValores_Filtrar)))
                    {
                        correspondeOperandos = true;
                    }

                    if(EvaluarOperandoValores)
                    {
                        bool elementoOperandoValoresContiene = false;

                        if (elementoEjecucionValores_Filtrar_Textos != null)
                        {
                            if (elementoEjecucionValores_Filtrar_Textos.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                            {
                                elementoOperandoValoresContiene = ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionValores_Filtrar_Textos).ElementosOperacion.Contains(elementoOperando);
                            }
                            else if (elementoEjecucionValores_Filtrar_Textos.GetType() == typeof(ElementoEntradaEjecucion))
                            {
                                elementoOperandoValoresContiene = ((ElementoEntradaEjecucion)elementoEjecucionValores_Filtrar_Textos).Numeros.Contains(numeroOperando);
                            }
                        }

                        bool subElementoOperandoValoresContiene = false;

                        if(subElementoEjecucionValores_Filtrar_Textos != null)
                            subElementoOperandoValoresContiene = subElementoEjecucionValores_Filtrar_Textos.Numeros.Contains(numeroOperando);

                        if (((elementoOperando != null && elementoEjecucionValores_Filtrar_Textos != null && (elementoOperando == elementoEjecucionValores_Filtrar_Textos | elementoOperandoValoresContiene)) ||
                        (subElementoOperando != null && subElementoEjecucionValores_Filtrar_Textos != null && (subElementoOperando == subElementoEjecucionValores_Filtrar_Textos | subElementoOperandoValoresContiene))) ||
                        ((elementoOperando == null && elementoEjecucionValores_Filtrar_Textos == null) ||
                        (subElementoOperando == null && subElementoEjecucionValores_Filtrar_Textos == null)))
                        {
                            correspondeOperandos = true;
                        }
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
                        if (elementoEjecucionCondicion_Filtrar.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                        {
                            elementoOperandoContiene2 = ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionCondicion_Filtrar).ElementosOperacion.Contains(elementoOperando);
                        }
                        else if (elementoEjecucionCondicion_Filtrar.GetType() == typeof(ElementoEntradaEjecucion))
                        {
                            elementoOperandoContiene2 = ((ElementoEntradaEjecucion)elementoEjecucionCondicion_Filtrar).Numeros.Contains(numeroOperando);
                        }
                    }

                    bool subElementoOperandoContiene2 = false;

                    if(subElementoEjecucion_Filtrar != null)
                        subElementoOperandoContiene2 = subElementoEjecucion_Filtrar.Numeros.Contains(numeroOperando);

                    if (((elementoOperando != null && elementoEjecucionCondicion_Filtrar != null && (elementoOperando == elementoEjecucionCondicion_Filtrar| elementoOperandoContiene2)) ||
                        (subElementoOperando != null && subElementoEjecucion_Filtrar != null && (subElementoOperando == subElementoEjecucion_Filtrar | subElementoOperandoContiene2)))) //||

                    //((elementoOperando != null && elementoEjecucionValores_Filtrar != null && elementoOperando == elementoEjecucionValores_Filtrar) ||
                    //(subElementoOperando != null && subElementoEjecucionValores_Filtrar != null && subElementoOperando == subElementoEjecucionValores_Filtrar)))
                    {
                        correspondeOperandos = true;
                    }

                    if(EvaluarOperandoValores)
                    {
                        bool elementoOperandoValoresContiene2 = false;

                        if (elementoEjecucionValores_Filtrar != null &&
                            elementoEjecucionValores_Filtrar.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                        {
                            elementoOperandoValoresContiene2 = ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionValores_Filtrar).ElementosOperacion.Contains(elementoOperando);
                        }
                        else if (elementoEjecucionValores_Filtrar != null &&
                            elementoEjecucionValores_Filtrar.GetType() == typeof(ElementoEntradaEjecucion))
                        {
                            elementoOperandoValoresContiene2 = ((ElementoEntradaEjecucion)elementoEjecucionValores_Filtrar).Numeros.Contains(numeroOperando);
                        }

                        bool subElementoOperandoValoresContiene2 = false;

                        if(subElementoEjecucionValores_Filtrar != null)
                            subElementoOperandoValoresContiene2 = subElementoEjecucionValores_Filtrar.Numeros.Contains(numeroOperando);

                        if (((elementoOperando != null && elementoEjecucionValores_Filtrar != null && (elementoOperando == elementoEjecucionValores_Filtrar | elementoOperandoValoresContiene2)) ||
                            (subElementoOperando != null && subElementoEjecucionValores_Filtrar != null && (subElementoOperando == subElementoEjecucionValores_Filtrar | subElementoOperandoValoresContiene2))) ||
                            ((elementoOperando == null && elementoEjecucionValores_Filtrar == null) ||
                        (subElementoOperando == null && subElementoEjecucionValores_Filtrar == null)))
                        {
                            correspondeOperandos = true;
                        }
                    }

                    break;
            }

            return correspondeOperandos;
        }

        private void EstablecerConsiderarOperando(bool valorBooleano, EjecucionCalculo ejecucion,
            ElementoOperacionAritmeticaEjecucion elementoOperando = null,
            ElementoDiseñoOperacionAritmeticaEjecucion subElementoOperando = null,
            EntidadNumero numeroOperando = null)
        {
            if (VerificarSiOperandosCorresponden_AEjecucion(ejecucion, true,
                elementoOperando, subElementoOperando, numeroOperando) ||
                (ContenedorCondiciones))
            {
                if (ConsiderarOperandoCondicion_SiCumple ||
                    (ContenedorCondiciones))
                {
                    if (numeroOperando != null &&
                        !numeroOperando.Seteo_ConsiderarOperandoCondicion_SiCumple)
                    {
                        numeroOperando.ConsiderarOperandoCondicion_SiCumple = valorBooleano;

                        if ((valorBooleano &&
                            !VerificarCondicionTotal_Conector(TipoConectorCondiciones_ConjuntoBusquedas.Y)) ||
                            (!valorBooleano &&
                            VerificarCondicionTotal_Conector(TipoConectorCondiciones_ConjuntoBusquedas.Y)))
                            numeroOperando.Seteo_ConsiderarOperandoCondicion_SiCumple = true;
                    }

                    if (!ContenedorCondiciones ||
                        (ContenedorCondiciones))
                    {
                        if (subElementoOperando != null &&
                            !subElementoOperando.Seteo_ConsiderarOperandoCondicion_SiCumple)
                        {
                            subElementoOperando.ConsiderarOperandoCondicion_SiCumple = valorBooleano;

                            if ((valorBooleano &&
                            !VerificarCondicionTotal_Conector(TipoConectorCondiciones_ConjuntoBusquedas.Y)) ||
                            (!valorBooleano &&
                            VerificarCondicionTotal_Conector(TipoConectorCondiciones_ConjuntoBusquedas.Y)))
                                subElementoOperando.Seteo_ConsiderarOperandoCondicion_SiCumple = true;
                        }

                        if (elementoOperando != null &&
                            !elementoOperando.Seteo_ConsiderarOperandoCondicion_SiCumple)
                        {
                            elementoOperando.ConsiderarOperandoCondicion_SiCumple = valorBooleano;

                            if ((valorBooleano &&
                            !VerificarCondicionTotal_Conector(TipoConectorCondiciones_ConjuntoBusquedas.Y)) ||
                            (!valorBooleano &&
                            VerificarCondicionTotal_Conector(TipoConectorCondiciones_ConjuntoBusquedas.Y)))
                                elementoOperando.Seteo_ConsiderarOperandoCondicion_SiCumple = true;
                        }
                    }
                }
            }
        }

        public bool VerificarCondicionTotal_Conector(TipoConectorCondiciones_ConjuntoBusquedas conectorCondiciones)
        {
            if (CondicionContenedora != null)
                return !CondicionContenedora.Condiciones.Any(i => i.TipoConector != conectorCondiciones);
            else
                return true;
        }

        public bool VerificarSoloCondicionesCadenasTextos()
        {
            foreach(var itemCondicion in Condiciones)
            {
                bool soloCondicionesCadenasTextos = itemCondicion.VerificarSoloCondicionesCadenasTextos();
                if (!soloCondicionesCadenasTextos)
                    return false;
            }

            if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                return true;
            else
                return false;
        }

        private void LimpiarVariables_ElementosVinculados(bool seteoLimpiarTemp)
        {
            
            foreach (var itemCondicion in Condiciones)
                itemCondicion.LimpiarElementosNumerosVinculados_CondicionesAnteriores();

            LimpiarElementosNumerosVinculados_CondicionesAnteriores();

            if (seteoLimpiarTemp)
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
            if(operacion != null)
            {
                foreach(var item in operacion.Numeros)
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

        private void FiltrarTextosInformacionInvolucrados_OperandoEjecucion_Evaluacion_Condiciones(ElementoOperacionAritmeticaEjecucion elementoOperando,
            ElementoDiseñoOperacionAritmeticaEjecucion subElementoOperando, EntidadNumero numeroOperando, bool correspondeOperandos)
            //ElementoEjecucionCalculo elementoEjecucionCondicion_Filtrar,
            //ElementoDiseñoOperacionAritmeticaEjecucion elementoEjecucion_Filtrar,
            //ElementoDiseñoOperacionAritmeticaEjecucion subElementoEjecucion_Filtrar)
        {
            List<string> TextosInvolucrados = new List<string>();

            if (elementoOperando != null)
            {
                if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    foreach (var itemTexto in TextosInformacionInvolucrados)
                    {
                        if (elementoOperando.Textos.Contains(itemTexto))
                            TextosInvolucrados.Add(itemTexto);
                    }
                }
                else if(TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
                {
                    TextosInvolucrados.AddRange(elementoOperando.Textos);
                }
            }

            if (subElementoOperando != null)
            {
                if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    foreach (var itemTexto in TextosInformacionInvolucrados)
                    {
                        if (subElementoOperando.Textos.Contains(itemTexto))
                            TextosInvolucrados.Add(itemTexto);
                    }
                }
                else if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
                {
                    TextosInvolucrados.AddRange(subElementoOperando.Textos);
                }
            }

            if (numeroOperando != null)
            {
                if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    foreach (var itemTexto in TextosInformacionInvolucrados)
                    {
                        if (numeroOperando.Textos.Contains(itemTexto))
                            TextosInvolucrados.Add(itemTexto);
                    }
                }
                else if (TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
                {
                    TextosInvolucrados.AddRange(numeroOperando.Textos);
                }
            }

            //if (elementoEjecucionCondicion_Filtrar != null)
            //{
            //    foreach (var itemTexto in TextosInformacionInvolucrados)
            //    {
            //        if (elementoEjecucionCondicion_Filtrar.Textos.Contains(itemTexto))
            //            TextosInvolucrados.Add(itemTexto);
            //    }
            //}

            //if (elementoEjecucion_Filtrar != null)
            //{
            //    foreach (var itemTexto in TextosInformacionInvolucrados)
            //    {
            //        if (elementoEjecucion_Filtrar.Textos.Contains(itemTexto))
            //            TextosInvolucrados.Add(itemTexto);
            //    }
            //}

            //if (subElementoEjecucion_Filtrar != null)
            //{
            //    foreach (var itemTexto in TextosInformacionInvolucrados)
            //    {
            //        if (subElementoEjecucion_Filtrar.Textos.Contains(itemTexto))
            //            TextosInvolucrados.Add(itemTexto);
            //    }
            //}

            if (TextosInvolucrados.Any() && correspondeOperandos)
            {
                TextosInformacionInvolucrados.Clear();
                TextosInformacionInvolucrados.AddRange(TextosInvolucrados);
            }
            else if(!TextosInvolucrados.Any() && !correspondeOperandos)
            {
                TextosInformacionInvolucrados.Clear();
            }
        }

        private void AgregarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(List<string> TextosInformacionInvolucrados,
            List<string> TextosAnteriores,
            ElementoEntradaEjecucion elementoEjecucionCondicion_Valores_ConjuntoEntrada,
        ElementoOperacionAritmeticaEjecucion elementoEjecucionCondicion_Valores_Operacion,
        ElementoDiseñoOperacionAritmeticaEjecucion elementoEjecucionCondicion_Valores_SubOperacion,
        InformacionCantidadesTextosInformacion_CondicionTextosInformacion CantidadesTextos,
        string[] valoresCondicion,
        ElementoConjuntoTextosEntradaEjecucion elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion = null)
        {
            if (TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion |
                TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
            {
                if (elementoEjecucionCondicion_Valores_Operacion != null)
                {
                    var elementosVinculadosAgregar = ObtenerNumerosVinculados_ElementoCondicion_Valores(elementoEjecucionCondicion_Valores_Operacion, TextosInformacionInvolucrados, TextosAnteriores, valoresCondicion);
                    NumerosVinculados_CondicionAnterior.AddRange(elementosVinculadosAgregar);
                    NumerosVinculados_CondicionAnterior_Temp.AddRange(elementosVinculadosAgregar);
                    CantidadesTextos.NumerosAsociados_OperandoValores.AddRange(elementosVinculadosAgregar);

                    if (elementosVinculadosAgregar.Any() && !OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion_Valores_Operacion))
                    {
                        OperandosVinculados_CondicionAnterior.Add(elementoEjecucionCondicion_Valores_Operacion);
                        OperandosVinculados_CondicionAnterior_Temp.Add(elementoEjecucionCondicion_Valores_Operacion);
                        CantidadesTextos.OperandosAsociados_OperandoValores.Add(elementoEjecucionCondicion_Valores_Operacion);
                    }
                }

                if (elementoEjecucionCondicion_Valores_ConjuntoEntrada != null)
                {
                    var numerosVinculadosAgregar = ObtenerNumerosVinculados_ElementoCondicion_Valores(elementoEjecucionCondicion_Valores_ConjuntoEntrada, TextosInformacionInvolucrados, TextosAnteriores, valoresCondicion);
                    NumerosVinculados_CondicionAnterior.AddRange(numerosVinculadosAgregar);
                    NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosVinculadosAgregar);
                    CantidadesTextos.NumerosAsociados_OperandoValores.AddRange(numerosVinculadosAgregar);

                    if (numerosVinculadosAgregar.Any() && !OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion_Valores_ConjuntoEntrada))
                    {
                        OperandosVinculados_CondicionAnterior.Add(elementoEjecucionCondicion_Valores_ConjuntoEntrada);
                        OperandosVinculados_CondicionAnterior_Temp.Add(elementoEjecucionCondicion_Valores_ConjuntoEntrada);
                        CantidadesTextos.OperandosAsociados_OperandoValores.Add(elementoEjecucionCondicion_Valores_ConjuntoEntrada);
                    }
                }

                if (elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion != null)
                {
                    var filasVinculadasAgregar = ObtenerFilasVinculadas_ElementoCondicion_Valores(elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion, TextosInformacionInvolucrados, TextosAnteriores);
                    FilasVinculadas_CondicionAnterior.AddRange(filasVinculadasAgregar);

                    if (filasVinculadasAgregar.Any() && !OperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion))
                    {
                        OperandosVinculados_CondicionAnterior.Add(elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion);
                        OperandosVinculados_CondicionAnterior_Temp.Add(elementoEjecucionCondicion_Valores_ConjuntoEntrada_TextosInformacion);
                    }
                }

                if (elementoEjecucionCondicion_Valores_SubOperacion != null)
                {
                    var numerosVinculadosAgregar = ObtenerNumerosVinculados_ElementoCondicion_Valores(elementoEjecucionCondicion_Valores_SubOperacion, TextosInformacionInvolucrados, TextosAnteriores, valoresCondicion);
                    NumerosVinculados_CondicionAnterior.AddRange(numerosVinculadosAgregar);
                    NumerosVinculados_CondicionAnterior_Temp.AddRange(numerosVinculadosAgregar);
                    CantidadesTextos.NumerosAsociados_OperandoValores.AddRange(numerosVinculadosAgregar);

                    if (numerosVinculadosAgregar.Any() && !SubOperandosVinculados_CondicionAnterior.Contains(elementoEjecucionCondicion_Valores_SubOperacion))
                    {
                        OperandosVinculados_CondicionAnterior.Add(elementoEjecucionCondicion_Valores_SubOperacion);
                        OperandosVinculados_CondicionAnterior_Temp.Add(elementoEjecucionCondicion_Valores_SubOperacion);
                        CantidadesTextos.OperandosAsociados_OperandoValores.Add(elementoEjecucionCondicion_Valores_SubOperacion);
                    }
                }

            }
        }

        private void QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(InformacionCantidadesTextosInformacion_CondicionTextosInformacion CantidadesTextos,
            bool QuitarOperandosCondicion, bool QuitarOperandosValores, List<InformacionCantidadesTextosInformacion_CondicionTextosInformacion> ListaCantidades)
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
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoCondicion.FirstOrDefault());
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

            if(QuitarOperandosValores)
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
                    OperandosVinculados_CondicionAnterior.Remove(CantidadesTextos.OperandosAsociados_OperandoValores.FirstOrDefault());
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

        private List<ElementoEjecucionCalculo> ObtenerListaElementosCantidades_Valores(List<InformacionCantidadesTextosInformacion_CondicionTextosInformacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.OperandosAsociados_OperandoValores).ToList();
        }

        private List<EntidadNumero> ObtenerListaNumerosCantidades_Valores(List<InformacionCantidadesTextosInformacion_CondicionTextosInformacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.NumerosAsociados_OperandoValores).ToList();
        }

        private List<ElementoEjecucionCalculo> ObtenerListaElementosCantidades_Condicion(List<InformacionCantidadesTextosInformacion_CondicionTextosInformacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.OperandosAsociados_OperandoCondicion).ToList();
        }

        private List<EntidadNumero> ObtenerListaNumerosCantidades_Condicion(List<InformacionCantidadesTextosInformacion_CondicionTextosInformacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.NumerosAsociados_OperandoCondicion).ToList();
        }

        private void QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior(InformacionCantidadesNumerosInformacion_CondicionTextosInformacion CantidadesTextos,
            bool QuitarOperandosCondicion, bool QuitarOperandosValores, List<InformacionCantidadesNumerosInformacion_CondicionTextosInformacion> ListaCantidades)
        {
            if (QuitarOperandosCondicion)
            {
                var ListaNumeros = ObtenerListaNumerosCantidades_Condicion(ListaCantidades);
                var ListaElementos = ObtenerListaElementosCantidades_Condicion(ListaCantidades);

                foreach(var item in CantidadesTextos.NumerosAsociados_OperandoCondicion)
                {
                    NumerosVinculados_CondicionAnterior.Remove(item);
                }
                foreach(var item in CantidadesTextos.OperandosAsociados_OperandoCondicion)
                {
                    OperandosVinculados_CondicionAnterior.Remove(item);
                }

                foreach (var item in CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(item);
                }

                foreach (var item in CantidadesTextos.OperandosAsociados_OperandoCondicion.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(item);
                }
            }

            if (QuitarOperandosValores)
            {
                var ListaNumeros = ObtenerListaNumerosCantidades_Valores(ListaCantidades);
                var ListaElementos = ObtenerListaElementosCantidades_Valores(ListaCantidades);

                foreach (var item in CantidadesTextos.NumerosAsociados_OperandoValores)
                {
                    NumerosVinculados_CondicionAnterior.Remove(item);
                }
                foreach (var item in CantidadesTextos.OperandosAsociados_OperandoValores)
                {
                    OperandosVinculados_CondicionAnterior.Remove(item);
                }

                foreach (var item in CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                !((ElementoOperacionAritmeticaEjecucion)i).ElementosOperacion.Any(i => ListaElementos.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(item);
                }

                foreach (var item in CantidadesTextos.OperandosAsociados_OperandoValores.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) &&
                !((ElementoEntradaEjecucion)i).Numeros.Any(i => ListaNumeros.Contains(i))))
                {
                    OperandosVinculados_CondicionAnterior.Remove(item);
                }
            }

        }

        private void QuitarElmentosNumerosVinculados_CondicionValores_CondicionAnterior_Temp(InformacionCantidadesNumerosInformacion_CondicionTextosInformacion CantidadesTextos,
            bool QuitarOperandosCondicion, bool QuitarOperandosValores)
        {

            if (QuitarOperandosCondicion)
            {
                foreach (var item in CantidadesTextos.NumerosAsociados_OperandoCondicion)
                {
                    if(!CantidadesTextos.CumpleCantidadCondicion)
                    {
                        NumerosVinculados_CondicionAnterior_Temp.Remove(item);
                    }
                }

            }

            if (QuitarOperandosValores)
            {
                foreach (var item in CantidadesTextos.NumerosAsociados_OperandoValores)
                {
                    if (!CantidadesTextos.CumpleCantidadValores)
                    {
                        NumerosVinculados_CondicionAnterior_Temp.Remove(item);
                        
                    }
                }
               
            }

        }

        private List<ElementoEjecucionCalculo> ObtenerListaElementosCantidades_Valores(List<InformacionCantidadesNumerosInformacion_CondicionTextosInformacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.OperandosAsociados_OperandoValores).ToList();
        }

        private List<EntidadNumero> ObtenerListaNumerosCantidades_Valores(List<InformacionCantidadesNumerosInformacion_CondicionTextosInformacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.NumerosAsociados_OperandoValores).ToList();
        }

        private List<ElementoEjecucionCalculo> ObtenerListaElementosCantidades_Condicion(List<InformacionCantidadesNumerosInformacion_CondicionTextosInformacion> listaCantidades)
        {
            return listaCantidades.SelectMany(i => i.OperandosAsociados_OperandoCondicion).ToList();
        }

        private List<EntidadNumero> ObtenerListaNumerosCantidades_Condicion(List<InformacionCantidadesNumerosInformacion_CondicionTextosInformacion> listaCantidades)
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

        private List<FilaTextosInformacion_Entrada> ObtenerFilasVinculadas_ElementoCondicion_Valores(ElementoConjuntoTextosEntradaEjecucion elemento,
            List<string> TextosInformacionInvolucrados, List<string> TextosAnteriores)
        {
            List<FilaTextosInformacion_Entrada> filas = new List<FilaTextosInformacion_Entrada>();
            var comparadorTextos = new ComparadorTextosInformacion(TipoOpcionCondicion_TextosInformacion,
                        ConsiderarTextosInformacionComoCantidades ? TipoOpcionCondicion_ElementoOperacionEntrada :
                        TipoOpcion_CondicionTextosInformacion_Implicacion.Ninguno,
                        Busqueda_TextoBusqueda_Ejecucion, BuscarCualquierTextoInformacion_TextoBusqueda,
                        QuitarEspaciosTemporalmente_CadenaCondicion);

            foreach (var itemNumero in elemento.FilasTextosInformacion)
            {
                if (comparadorTextos.Interseccion(TextosInformacionInvolucrados, itemNumero.TextosInformacion, TextosAnteriores))
                    filas.Add(itemNumero);
            }

            return filas;
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

        private void ActualizarElementosNumerosVinculados_CondicionesAnteriores(CondicionTextosInformacion condicion)
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

        public void QuitarCondicionSubElementoDiseñoCondicion_Condiciones(DiseñoElementoOperacion elemento)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.QuitarCondicionSubElementoDiseñoCondicion_Condiciones(elemento);
            }

            if (OperandoSubElemento_Condicion == elemento)
                OperandoSubElemento_Condicion = null;

            if (OperandoSubElemento_Condicion_TextosInformacion == elemento)
                OperandoSubElemento_Condicion_TextosInformacion = null;

            if (OperandoSubElemento_Valores_TextosInformacion == elemento)
                OperandoSubElemento_Valores_TextosInformacion = null;

            if (OperandoSubElemento_Condicion_Elemento == elemento)
                OperandoSubElemento_Condicion_Elemento = null;

            if (SubOperandos_AplicarCondiciones.Contains(elemento))
                SubOperandos_AplicarCondiciones.Remove(elemento);
        }

        public void QuitarCondicionElementoDiseñoCondicion_Condiciones(DiseñoOperacion elemento)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.QuitarCondicionElementoDiseñoCondicion_Condiciones(elemento);
            }

            if (ElementoCondicion == elemento)
                ElementoCondicion = null;

            if (ElementoOperacion_Valores == elemento)
                ElementoOperacion_Valores = null;

            if (ElementoOperacion_Valores_ElementoAsociado == elemento)
                ElementoOperacion_Valores_ElementoAsociado = null;

            if (OperandoCondicion == elemento)
                OperandoCondicion = null;

            if (Operandos_AplicarCondiciones.Contains(elemento))
                Operandos_AplicarCondiciones.Remove(elemento);
        }

        public void AgregarCondicionElementoDiseñoCondicion_Condiciones(ref List<DiseñoOperacion> elementos)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.AgregarCondicionElementoDiseñoCondicion_Condiciones(ref elementos);
            }

            if(ElementoOperacion_Valores != null)
                elementos.Add(ElementoOperacion_Valores);

            if (OperandoCondicion != null)
                elementos.Add(OperandoCondicion);

            //if (Operandos_AplicarCondiciones.Contains(elemento))
            //    Operandos_AplicarCondiciones.Remove(elemento);
        }

        public void AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref List<DiseñoElementoOperacion> elementos)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref elementos);
            }

            if (OperandoSubElemento_Condicion_TextosInformacion != null)
                elementos.Add(OperandoSubElemento_Condicion_TextosInformacion);

            if (OperandoSubElemento_Valores_TextosInformacion != null)
                elementos.Add(OperandoSubElemento_Valores_TextosInformacion);

            //if (SubOperandos_AplicarCondiciones.Contains(elemento))
            //    SubOperandos_AplicarCondiciones.Remove(elemento);
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
                    if (CantidadTotal > 0)
                        indicePosicion = 0;
                    break;

                case TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSegundaDeActualEjecucion:
                    if (CantidadTotal > 1)
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
            foreach(var itemCondicion in Condiciones)
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
    }

    public class InfoOpcion_VinculadosAnterior
    {
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Vinculados_CondicionAnterior { get; set; }
        public ElementoEjecucionCalculo OperandoRelacionado_Ejecucion { get; set; }
        public ElementoDiseñoOperacionAritmeticaEjecucion SubOperandoRelacionado_Ejecucion { get; set; }
        public Entrada EntradaRelacionada { get; set; }
    }

    public class InformacionCantidadesTextosInformacion_CondicionTextosInformacion
    {
        public int CantidadTextosCondicion_TextosInformacion { get; set; }
        public int CantidadTextosValoresCondicion_TextosInformacion { get; set; }
        public int TextosCumplenCondicion_Elemento { get; set; }
        public int TextosNoCumplenCondicion_Elemento { get; set; }

        public int TextosCumplenCondicion_Valores { get; set; }
        public int TextosNoCumplenCondicion_Valores { get; set; }
        public List<EntidadNumero> NumerosAsociados_OperandoCondicion { get; set; }
        public List<ElementoEjecucionCalculo> OperandosAsociados_OperandoCondicion { get; set; }
        public List<EntidadNumero> NumerosAsociados_OperandoValores { get; set; }
        public List<ElementoEjecucionCalculo> OperandosAsociados_OperandoValores { get; set; }
        
        public InformacionCantidadesTextosInformacion_CondicionTextosInformacion()
        {
            NumerosAsociados_OperandoCondicion = new List<EntidadNumero>();
            OperandosAsociados_OperandoCondicion = new List<ElementoEjecucionCalculo>();

            NumerosAsociados_OperandoValores = new List<EntidadNumero>();
            OperandosAsociados_OperandoValores = new List<ElementoEjecucionCalculo>();
        }
    }

    public class InformacionCantidadesNumerosInformacion_CondicionTextosInformacion
    {
        public int CantidadNumerosCondicion_TextosInformacion { get; set; }
        public int CantidadNumerosValoresCondicion_TextosInformacion { get; set; }
        public int NumerosCumplenCondicion_Elemento { get; set; }
        public int NumerosNoCumplenCondicion_Elemento { get; set; }

        public int NumerosCumplenCondicion_Valores { get; set; }
        public int NumerosNoCumplenCondicion_Valores { get; set; }
        public List<EntidadNumero> NumerosAsociados_OperandoCondicion { get; set; }
        public List<ElementoEjecucionCalculo> OperandosAsociados_OperandoCondicion { get; set; }
        public List<EntidadNumero> NumerosAsociados_OperandoValores { get; set; }
        public List<ElementoEjecucionCalculo> OperandosAsociados_OperandoValores { get; set; }
        public bool QuitarCantidadesValores { get; set; }
        public bool CumpleCantidadCondicion { get; set; }
        public bool CumpleCantidadValores { get; set; }
        public InformacionCantidadesNumerosInformacion_CondicionTextosInformacion()
        {
            NumerosAsociados_OperandoCondicion = new List<EntidadNumero>();
            OperandosAsociados_OperandoCondicion = new List<ElementoEjecucionCalculo>();

            NumerosAsociados_OperandoValores = new List<EntidadNumero>();
            OperandosAsociados_OperandoValores = new List<ElementoEjecucionCalculo>();

            QuitarCantidadesValores = true;
        }
    }
}
