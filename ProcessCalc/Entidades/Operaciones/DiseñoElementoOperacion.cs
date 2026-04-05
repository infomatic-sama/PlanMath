using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Controles.Calculos;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Ejecuciones.ToolTips;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class DiseñoElementoOperacion
    {
        public string ID { get; set; }
        public TipoElementoDiseñoOperacion Tipo { get; set; }
        public Entrada EntradaRelacionada { get; set; }
        public double PosicionX { get; set; }
        public double PosicionY { get; set; }
        public List<DiseñoElementoOperacion> ElementosAnteriores { get; set; }
        public List<DiseñoElementoOperacion> ElementosPosteriores { get; set; }
        public double Altura { get; set; }
        public double Anchura { get; set; }
        public TipoOpcionOperacion TipoOpcionOperacion { get; set; }
        public string Nombre { get; set; }
        public DiseñoOperacion ElementoDiseñoRelacionado { get; set; }
        public bool ContieneSalida { get; set; }
        public List<DiseñoElementoOperacion> ElementosContenedoresOperacion { get; set; }
        public string NombreElemento { get; set; }
        public List<OrdenOperando_Elemento> OrdenOperandos { get; set; }
        public bool ConAcumulacion { get; set; }
        public string Info { get; set; }
        public int CantidadRecalculo { get; set; }
        public TipoEstablecerBasesExponentes TipoEstablecerAgrupacion { get; set; }
        public bool SeguirOperandoFilas_ConUltimoNumero { get; set; }
        public bool SeguirOperandoFilas_ConElementoNeutro { get; set; }
        public List<CondicionesAsignacionSalidas_TextosInformacion> CondicionesTextosInformacion_SeleccionOrdenamiento { get; set; }
        public List<DiseñoElementoOperacion> SalidasAgrupamiento_SeleccionOrdenamiento { get; set; }
        public List<AsociacionTextoInformacion_ElementoSalida> AsociacionesTextosInformacion_ElementosSalida { get; set; }
        public List<AsociacionTextoInformacion_Clasificador> AsociacionesTextosInformacion_Clasificadores { get; set; }
        public DiseñoOperacion ElementoSalidaOperacion_Agrupamiento { get; set; }
        public DiseñoElementoOperacion ElementoInternoSalidaOperacion_Agrupamiento { get; set; }
        public List<AsociacionCondicionFlujo_ElementoSalida> AsociacionesCondicionFlujo_ElementosSalida { get; set; }
        public List<AsociacionCondicionFlujo_ElementoSalida> AsociacionesCondicionFlujo_ElementosSalida2 { get; set; }
        public List<DiseñoElementoOperacion> SalidasAgrupamiento_CondicionFlujo { get; set; }
        public List<CondicionFlujo> CondicionesFlujo_SeleccionOrdenamiento { get; set; }
        public bool ModoManual_CondicionFlujo { get; set; }
        public bool ModoSeleccionManual_SeleccionarOrdenar { get; set; }
        public bool Ejecutar_SiTieneOtrosOperandosValidos { get; set; }
        public bool IncluirAsignacionTextosInformacionAntes { get; set; }
        public bool IncluirAsignacionTextosInformacionDespues { get; set; }
        public OrdenarNumerosElemento OrdenarNumerosAntesEjecucion { get; set; }
        public OrdenarNumerosElemento OrdenarNumerosDespuesEjecucion { get; set; }
        public string NombreCombo
        {
            get
            {
                return NombreElemento;
            }
        }
        public bool UtilizarDefinicionNombres_AsignacionTextosInformacion { get; set; }
        public DefinicionTextoNombresCantidades DefinicionOpcionesNombresCantidades { get; set; }
        public bool AgregarCantidadComoTextoInformacion { get; set; }
        public bool AgregarNombreComoTextoInformacion { get; set; }
        public bool AgregarNumeroComoTextoInformacion { get; set; }
        public bool AgregarCantidadComoNumero { get; set; }
        public bool IncluirCantidadNumero { get; set; }
        public bool AsignarTextosInformacion_OperandosResultados { get; set; }
        public bool AsignarTextosInformacion_AntesImplicaciones { get; set; }
        public bool AsignarTextosInformacion_DespuesImplicaciones { get; set; }
        public bool AsignarTextosInformacion_AntesEjecucion { get; set; }
        public bool AsignarTextosInformacion_DespuesEjecucion { get; set; }
        public bool AsignarTextosInformacionImplicaciones_AntesEjecucion { get; set; }
        public bool AsignarTextosInformacionImplicaciones_DespuesEjecucion { get; set; }
        public bool QuitarClasificadores_AntesEjecucion { get; set; }
        public bool QuitarClasificadores_DespuesEjecucion { get; set; }
        public bool OrdenarClasificadores_AntesEjecucion { get; set; }
        public bool OrdenarClasificadores_DespuesEjecucion { get; set; }
        public bool OrdenarClasificadoresDeMenorAMayor_AntesEjecucion { get; set; }
        public bool OrdenarClasificadoresDeMayorAMenor_AntesEjecucion { get; set; }
        public bool OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion { get; set; }
        public bool OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion { get; set; }
        public List<OrdenarNumerosElemento> OrdenarNumeros_AntesEjecucion { get; set; }
        public List<OrdenarNumerosElemento> OrdenarNumeros_DespuesEjecucion { get; set; }
        public CondicionTextosInformacion CondicionesTextosInformacionOperandosResultados { get; set; }
        public bool AsignarTextosInformacionCondiciones_OperandosResultados { get; set; }
        public List<DiseñoElementoOperacion> OperandosTextosInformacionOperandosResultados { get; set; }
        public bool AlgunosOperandosTextosInformacionOperandosResultados { get; set; }
        public bool NingunOperandoTextosInformacionOperandosResultados { get; set; }
        public List<CondicionProcesamientoCantidades> ProcesamientoCantidades { get; set; }
        public bool NoConservarCambiosOperandos_ProcesamientoCantidades { get; set; }
        public List<CondicionProcesamientoTextosInformacion> ProcesamientoTextosInformacion { get; set; }
        public bool NoConservarCambiosOperandos_ProcesamientoTextosInformacion { get; set; }
        public bool AplicarProcesamientoAntesImplicacionesTextosInformacion { get; set; }
        public bool AplicarProcesamientoDespuesImplicacionesTextosInformacion { get; set; }
        public bool PorcentajeRelativo { get; set; }
        public double ValorOpcionElementosFijos { get; set; }
        public TipoOpcionElementosFijosOperacionPotencia OpcionElementosFijosPotencia { get; set; }
        public TipoOpcionElementosFijosOperacionRaiz OpcionElementosFijosRaiz { get; set; }
        public TipoOpcionElementosFijosOperacionLogaritmo OpcionElementosFijosLogaritmo { get; set; }
        public TipoOpcionElementosFijosOperacionInverso OpcionElementosFijosInverso { get; set; }
        public List<DiseñoOperacion> OperandosInsertar_CantidadesProcesamientoCantidades { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosInsertar_CantidadesProcesamientoCantidades { get; set; }
        public bool DivisionZero_Continuar { get; set; }
        public List<AgrupacionOperando_PorSeparado> AgrupacionesOperandos_PorSeparado { get; set; }
        public List<DiseñoElementoOperacion[]> EntradasSalidasOperacion_Espera { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasSubOperandos_ArchivoExterno { get; set; }
        public List<AsociacionResultadoOperando_ArchivoExterno> ResultadosSubOperandos_ArchivoExterno { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasSubOperandos_SubCalculo { get; set; }
        public List<AsociacionResultadoOperando_ArchivoExterno> ResultadosSubOperandos_SubCalculo { get; set; }
        public List<DiseñoElementoOperacion[]> EntradasSalidasOperacion_LimpiarDatos { get; set; }
        public List<DiseñoElementoOperacion[]> EntradasSalidasOperacion_RedondearCantidades { get; set; }
        public double TiempoEspera { get; set; }
        public TipoTiempoEspera TipoTiempoEspera { set; get; }
        public bool CantidadEsperas_Fijas { get; set; }
        public int CantidadVerificaciones { get; set; }
        public CondicionTextosInformacion CondicionesTextosInformacion_Espera { get; set; }
        public CondicionFlujo CondicionesCantidades_Espera { get; set; }
        public bool VerificarCondiciones_Hasta { get; set; }
        public bool EjecutarImplicacionesAntes_Espera { get; set; }
        public bool EjecutarImplicacionesDespues_Espera { get; set; }
        public bool EjecutarImplicacionesDurante_Espera { get; set; }
        public bool EjecutarImplicacionesAntes_Limpieza { get; set; }
        public bool EjecutarImplicacionesDespues_Limpieza { get; set; }
        public bool EjecutarImplicacionesDurante_Limpieza { get; set; }
        public bool EjecutarImplicacionesAntes_Redondeo { get; set; }
        public bool EjecutarImplicacionesDespues_Redondeo { get; set; }
        public bool EjecutarImplicacionesDurante_Redondeo { get; set; }
        public bool QuitarTextosInformacion_Repetidos { get; set; }
        public ConfiguracionLimpiarDatos ConfigLimpiarDatos { get; set; }
        public ConfiguracionLimpiarDatos ConfigLimpiezaDatosResultados { get; set; }
        public bool LimpiarDatosResultados { get; set; }
        public ConfiguracionRedondearCantidades ConfigRedondeo { get; set; }
        public bool RedondearCantidadesResultados { get; set; }
        public ConfiguracionRedondearCantidades ConfigRedondeoResultados { get; set; }
        public ConfiguracionArchivoExterno ConfigArchivoExterno { get; set; }
        public ConfiguracionSubCalculo ConfigSubCalculo { get; set; }
        public List<AsignacionAgrupacionOperando_PorSeparado> AgrupacionesAsignadasOperandos_PorSeparado { get; set; }
        public ConfiguracionOperacionesCadenasTexto ConfigOperaciones_CadenasTexto { get; set; }
        public bool NoConsiderarEjecucion { get; set; }
        public List<Clasificador> Clasificadores {  get; set; }
        public bool EjecutarLogicasTextos_DespuesEjecucion { get; set; }
        public bool EjecutarLogicasTextos_AntesEjecucion { get; set; }
        public bool EjecutarLogicasCantidades_DespuesEjecucion { get; set; }
        public bool EjecutarLogicasCantidades_AntesEjecucion { get; set; }
        [IgnoreDataMember]
        public bool ConCambios_ToolTips { get; set; }
        [IgnoreDataMember]
        public bool Actualizar_ToolTips { get; set; }
        public DiseñoElementoOperacion()
        {
            ElementosAnteriores = new List<DiseñoElementoOperacion>();
            ElementosPosteriores = new List<DiseñoElementoOperacion>();
            Tipo = TipoElementoDiseñoOperacion.Ninguno;
            ElementosContenedoresOperacion = new List<DiseñoElementoOperacion>();
            OrdenOperandos = new List<OrdenOperando_Elemento>();
            CantidadRecalculo = 2;
            TipoEstablecerAgrupacion = TipoEstablecerBasesExponentes.EstablecerPorPares;
            CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionesAsignacionSalidas_TextosInformacion>();
            SalidasAgrupamiento_SeleccionOrdenamiento = new List<DiseñoElementoOperacion>();
            AsociacionesTextosInformacion_ElementosSalida = new List<AsociacionTextoInformacion_ElementoSalida>();
            AsociacionesTextosInformacion_Clasificadores = new List<AsociacionTextoInformacion_Clasificador>();
            AsociacionesTextosInformacion_Clasificadores = new List<AsociacionTextoInformacion_Clasificador>();
            AsociacionesCondicionFlujo_ElementosSalida = new List<AsociacionCondicionFlujo_ElementoSalida>();
            AsociacionesCondicionFlujo_ElementosSalida2 = new List<AsociacionCondicionFlujo_ElementoSalida>();
            SalidasAgrupamiento_CondicionFlujo = new List<DiseñoElementoOperacion>();
            CondicionesFlujo_SeleccionOrdenamiento = new List<CondicionFlujo>();
            IncluirAsignacionTextosInformacionDespues = true;
            DefinicionOpcionesNombresCantidades = new DefinicionTextoNombresCantidades();
            AsignarTextosInformacion_OperandosResultados = true;
            AsignarTextosInformacion_DespuesImplicaciones = true;
            OrdenarNumeros_AntesEjecucion = new List<OrdenarNumerosElemento>();
            OrdenarNumeros_DespuesEjecucion = new List<OrdenarNumerosElemento>();
            OperandosTextosInformacionOperandosResultados = new List<DiseñoElementoOperacion>();
            ProcesamientoCantidades = new List<CondicionProcesamientoCantidades>();
            ProcesamientoTextosInformacion = new List<CondicionProcesamientoTextosInformacion>();
            OpcionElementosFijosPotencia = TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos;
            OpcionElementosFijosRaiz = TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos;
            OpcionElementosFijosLogaritmo = TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos;
            OpcionElementosFijosInverso = TipoOpcionElementosFijosOperacionInverso.InversoSumaResta;
            OperandosInsertar_CantidadesProcesamientoCantidades = new List<DiseñoOperacion>();
            SubOperandosInsertar_CantidadesProcesamientoCantidades = new List<DiseñoElementoOperacion>();
            AgrupacionesOperandos_PorSeparado = new List<AgrupacionOperando_PorSeparado>();
            AplicarProcesamientoDespuesImplicacionesTextosInformacion = true;
            EntradasSalidasOperacion_Espera = new List<DiseñoElementoOperacion[]>();
            EntradasSalidasOperacion_LimpiarDatos = new List<DiseñoElementoOperacion[]>();
            TiempoEspera = 1;
            TipoTiempoEspera = TipoTiempoEspera.Segundos;
            CantidadEsperas_Fijas = true;
            CantidadVerificaciones = 1;
            VerificarCondiciones_Hasta = true;
            EjecutarImplicacionesDespues_Espera = true;
            ConfigLimpiarDatos = new ConfiguracionLimpiarDatos();
            ConfigLimpiezaDatosResultados = new ConfiguracionLimpiarDatos();
            ConfigRedondeo = new ConfiguracionRedondearCantidades();
            ConfigRedondeoResultados = new ConfiguracionRedondearCantidades();
            EntradasSalidasOperacion_RedondearCantidades = new List<DiseñoElementoOperacion[]>();
            EntradasSubOperandos_ArchivoExterno = new List<AsociacionEntradaOperando_ArchivoExterno>();
            ResultadosSubOperandos_ArchivoExterno = new List<AsociacionResultadoOperando_ArchivoExterno>();
            ConfigArchivoExterno = new ConfiguracionArchivoExterno();
            AgrupacionesAsignadasOperandos_PorSeparado = new List<AsignacionAgrupacionOperando_PorSeparado>();
            EntradasSubOperandos_SubCalculo = new List<AsociacionEntradaOperando_ArchivoExterno>();
            ResultadosSubOperandos_SubCalculo = new List<AsociacionResultadoOperando_ArchivoExterno>();
            ConfigSubCalculo = new ConfiguracionSubCalculo();
            ConfigOperaciones_CadenasTexto = new ConfiguracionOperacionesCadenasTexto();
            Clasificadores = new List<Clasificador>();
            AsignarTextosInformacion_DespuesEjecucion = true;
            AsignarTextosInformacionImplicaciones_DespuesEjecucion = true;
            EjecutarLogicasTextos_AntesEjecucion = true;
            EjecutarLogicasCantidades_DespuesEjecucion = true;
            SeguirOperandoFilas_ConElementoNeutro = true;
        }

        public bool VerificarCambios(DiseñoElementoOperacion UltimoEstadoEntrada, ComparadorObjetos ComparadorObjetos)
        {
            bool hayCambios = false;

            ComparadorObjetos._paresVisitados.Clear();

            if (!ComparadorObjetos.CompararObjetos(this, UltimoEstadoEntrada))
                hayCambios = true;


            return hayCambios;
        }
        public void OrdenarOperandos(DiseñoElementoOperacion elementoPadre)
        {
            List<OrdenOperando_Elemento> operandos = (from E in OrdenOperandos where E.ElementoPadre == elementoPadre orderby E.Orden ascending select E).ToList();
            int orden = 1;
            foreach (var item in operandos)
            {
                item.Orden = orden;
                orden++;
            }
        }

        public OrdenOperando_Elemento Operando(DiseñoElementoOperacion elemento)
        {
            OrdenOperando_Elemento operando = (from E in OrdenOperandos where E.ElementoPadre == elemento select E).FirstOrDefault();
            return operando;
        }

        public void AgregarOrdenOperando(DiseñoElementoOperacion elementoPadre)
        {
            OrdenOperando_Elemento nuevoOrdenOperando = new OrdenOperando_Elemento();
            nuevoOrdenOperando.ElementoPadre = elementoPadre;
            nuevoOrdenOperando.Orden = elementoPadre.ElementosAnteriores.ToList().Count;
            OrdenOperandos.Add(nuevoOrdenOperando);
        }

        public void QuitarOrdenOperando(DiseñoElementoOperacion elementoPadre)
        {
            OrdenOperando_Elemento ordenOperando = (from E in OrdenOperandos where E.ElementoPadre == elementoPadre select E).FirstOrDefault();
            OrdenOperandos.Remove(ordenOperando);
            OrdenarOperandosHijos(elementoPadre);
        }

        public void OrdenarTodosOperandos()
        {
            foreach (var elementoPadre in ElementosAnteriores)
            {
                List<OrdenOperando_Elemento> operandos = (from E in OrdenOperandos where E.ElementoPadre == elementoPadre orderby E.Orden ascending select E).ToList();
                int orden = 1;
                foreach (var item in operandos)
                {
                    item.Orden = orden;
                    orden++;
                }
            }
        }

        public void OrdenarOperandosHijos(DiseñoElementoOperacion elementoPadre)
        {
            int orden = 1;
            foreach (var elementoHijo in elementoPadre.ElementosAnteriores.OrderBy((i) => ((
            (from E in i.OrdenOperandos where E.ElementoPadre == elementoPadre select E).FirstOrDefault() != null) ?
            (from E in i.OrdenOperandos where E.ElementoPadre == elementoPadre select E).FirstOrDefault().Orden : new OrdenOperando_Elemento().Orden)))
            {
                OrdenOperando_Elemento operando = (from E in elementoHijo.OrdenOperandos where E.ElementoPadre == elementoPadre select E).FirstOrDefault();
                if (operando != null)
                {
                    operando.Orden = orden;
                    orden++;
                }
            }
        }

        public List<CondicionProcesamientoCantidades> CopiarProcesamientoCantidades()
        {
            List<CondicionProcesamientoCantidades> procesamiento = new List<CondicionProcesamientoCantidades>();

            foreach (var item in ProcesamientoCantidades)
                procesamiento.Add(item.ReplicarObjeto());

            return procesamiento;
        }

        public List<CondicionProcesamientoTextosInformacion> CopiarProcesamientoTextosInformacion()
        {
            List<CondicionProcesamientoTextosInformacion> procesamiento = new List<CondicionProcesamientoTextosInformacion>();

            foreach (var item in ProcesamientoTextosInformacion)
                procesamiento.Add(item.ReplicarObjeto());

            return procesamiento;
        }

        public List<AgrupacionOperando_PorSeparado> ObtenerAgrupaciones()
        {
            List<AgrupacionOperando_PorSeparado> agrupaciones = new List<AgrupacionOperando_PorSeparado>();
            foreach (var item in ElementosAnteriores)
            {
                foreach (var itemAgrupacion in item.AgrupacionesOperandos_PorSeparado)
                {
                    if (!agrupaciones.Any(i => i.NombreAgrupacion == itemAgrupacion.NombreAgrupacion &&
                    i.OperacionElementoRelacionado == itemAgrupacion.OperacionElementoRelacionado))
                    {
                        agrupaciones.Add(itemAgrupacion);
                    }
                }
            }

            return agrupaciones;
        }
    }
}
