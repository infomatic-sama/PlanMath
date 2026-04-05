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
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace ProcessCalc.Entidades
{
    public class DiseñoOperacion
    {
        public string ID { get; set; }
        public List<DiseñoOperacion> ElementosAnteriores { get; set; }
        public List<DiseñoOperacion> ElementosPosteriores { get; set; }
        public TipoElementoOperacion Tipo { get; set; }
        public Entrada EntradaRelacionada { get; set; }
        [AtributoNoComparar]
        public double PosicionX { get; set; }
        [AtributoNoComparar]
        public double PosicionY { get; set; }
        [AtributoNoComparar]
        public double Altura { get; set; }
        [AtributoNoComparar]
        public double Anchura { get; set; }
        public bool ContieneSalida { get; set; }
        public string Nombre { get; set; }
        public List<DiseñoElementoOperacion> ElementosDiseñoOperacion { get; set; }
        [AtributoNoComparar]
        public double AltoDiagrama { get; set; }
        [AtributoNoComparar]
        public double AnchoDiagrama { get; set; }
        public List<DiseñoOperacion> ElementosContenedoresOperacion { get; set; }
        //public EtapaEjecucion EtapaAnterior { get; set; }
        public List<OrdenOperando> OrdenOperandos { get; set; }
        [AtributoNoComparar]
        public string Info { get; set; }
        public bool DefinicionSimple_TextosInformacion { get; set; }
        public List<CondicionesAsignacionSalidas_TextosInformacion> CondicionesTextosInformacion_SeleccionOrdenamiento { get; set; }
        public List<DiseñoOperacion> SalidasAgrupamiento_SeleccionOrdenamiento { get; set; }
        public List<AsociacionTextoInformacion_ElementoSalida> AsociacionesTextosInformacion_ElementosSalida { get; set; }
        public List<AsociacionTextoInformacion_Clasificador> AsociacionesTextosInformacion_Clasificadores { get; set; }
        public bool ModoUnir_SeleccionarOrdenar { get; set; }
        public bool ModoSeleccionManual_SeleccionarOrdenar { get; set; }
        public bool DefinicionSimple_CondicionesFlujo { get; set; }
        public List<CondicionFlujo> CondicionesFlujo_SeleccionOrdenamiento { get; set; }
        public List<CondicionTextosInformacion> CondicionesTextosInformacion_SeleccionEntradas { get; set; }
        public List<AsociacionCondicionFlujo_ElementoSalida> AsociacionesCondicionesFlujo_ElementosSalida { get; set; }
        public List<AsociacionCondicionFlujo_ElementoSalida> AsociacionesCondicionesFlujo_ElementosSalida2 { get; set; }
        public List<AsociacionCondicionTextosInformacion_Entradas_ElementoSalida> AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida { get; set; }
        public List<DiseñoOperacion> SalidasAgrupamiento_SeleccionCondicionesFlujo { get; set; }
        public bool ModoManual_CondicionFlujo { get; set; }
        public List<DiseñoOperacion> ElementosAgrupados { get; set; }
        public DiseñoOperacion AgrupadorContenedor { get; set; }
        public bool IncluirAsignacionTextosInformacionAntes { get; set; }
        public bool IncluirAsignacionTextosInformacionDespues { get; set; }
        public List<DiseñoOperacion> ElementosAnterioresAgrupados
        {
            get
            {
                return ObtenerTodosElementosAnterioresAgrupados();
            }
        }
        public List<DiseñoOperacion> ElementosPosterioresAgrupados
        {
            get
            {
                return ObtenerTodosElementosPosterioresAgrupados();
            }
        }
        public string NombreCombo 
        { 
            get
            {
                if (Tipo == TipoElementoOperacion.Entrada &&
                    !EntradaRelacionada.EjecutarDeFormaGeneral)
                {
                    if (EntradaRelacionada.ElementoSalidaCalculoAnterior != null)
                        return EntradaRelacionada.ElementoSalidaCalculoAnterior.NombreCombo;
                    else
                        return EntradaRelacionada.Nombre;
                }
                else
                {
                    if (Tipo == TipoElementoOperacion.Entrada && 
                        EntradaRelacionada.EjecutarDeFormaGeneral)
                        return EntradaRelacionada.Nombre;
                    else
                        return Nombre;
                }
            }
        }
        public bool Ejecutar_SiTieneOtrosOperandosValidos { get; set; }
        public bool UtilizarDefinicionNombres_AsignacionTextosInformacion { get; set; }
        public DefinicionTextoNombresCantidades DefinicionOpcionesNombresCantidades { get; set; }
        public bool AgregarCantidadComoTextoInformacion { get; set; }
        public bool AgregarNombreComoTextoInformacion { get; set; }
        public bool AgregarNumeroComoTextoInformacion { get; set; }
        public bool AgregarCantidadComoNumero { get; set; }
        public bool IncluirCantidadNumero { get; set; }
        public OrdenarNumerosElemento OrdenarNumerosAntesEjecucion { get; set; }
        public OrdenarNumerosElemento OrdenarNumerosDespuesEjecucion { get; set; }
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
        public bool DefinicionSimple_Operacion { get; set; }
        public TipoOperacionEjecucion TipoOperacion_Ejecucion { get; set; }
        public bool ConAcumulacion { get; set; }
        public bool SeguirOperandoFilas_ConUltimoNumero { get; set; }
        public bool SeguirOperandoFilas_ConElementoNeutro { get; set; }
        public List<OrdenarNumerosElemento> OrdenarNumeros_AntesEjecucion { get; set; }
        public List<OrdenarNumerosElemento> OrdenarNumeros_DespuesEjecucion { get; set; }
        public bool IncluirAsignacionDentroDefinicionNormal { get; set; }
        public CondicionTextosInformacion CondicionesTextosInformacionOperandosResultados { get; set; }
        public bool AsignarTextosInformacionCondiciones_OperandosResultados { get; set; }
        public List<DiseñoOperacion> OperandosTextosInformacionOperandosResultados { get; set; }
        public bool AlgunosOperandosTextosInformacionOperandosResultados { get; set; }
        public bool NingunOperandoTextosInformacionOperandosResultados { get; set; }
        public List<CondicionProcesamientoCantidades> ProcesamientoCantidades { get; set; }
        public bool NoConservarCambiosOperandos_ProcesamientoCantidades { get; set; }
        public bool ProcesarCantidades_AntesImplicaciones { get; set; }
        public bool ProcesarCantidades_DespuesImplicaciones { get; set; }
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
        public bool DivisionZero_Continuar { get; set; }
        public bool SeleccionManualEntradas { get; set; }
        public bool SeleccionCondicionesEntradas { get; set; }
        public List<AgrupacionOperando_PorSeparado> AgrupacionesOperandos_PorSeparado { get; set; }
        public List<DiseñoOperacion[]> EntradasSalidas_Espera { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasOperandos_ArchivoExterno { get; set; }
        public List<AsociacionResultadoOperando_ArchivoExterno> ResultadosOperandos_ArchivoExterno { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasOperandos_SubCalculo { get; set; }
        public List<AsociacionResultadoOperando_ArchivoExterno> ResultadosOperandos_SubCalculo { get; set; }
        public List<DiseñoOperacion[]> EntradasSalidas_LimpiarDatos { get; set; }
        public List<DiseñoOperacion[]> EntradasSalidas_RedondearCantidades { get; set; }
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
        [IgnoreDataMember]
        public bool ModoEjecucionExterna_CantidadesFijas { get; set; }
        public double ValorNumerico_Digitacion_Tooltip { get; set; }
        public List<string> TextosInformacion_Digitacion_Tooltip { get; set;}
        public List<EntidadNumero> Cantidades_Digitacion_Tooltip { get; set;}
        public List<DiseñoOperacion> EntradasSeleccionadas_ToolTips { get; set; }
        public List<AsignacionAgrupacionOperando_PorSeparado> AgrupacionesAsignadasOperandos_PorSeparado {  get; set; }
        public DefinicionTextoNombresCantidades DefinicionOpcionesNombresResultados { get; set; }
        public string TextoEnPantalla_Digitacion_Tooltip { get; set;}
        [IgnoreDataMember]
        public bool DesdeTextoEnPantalla { get; set; }
        [IgnoreDataMember]
        public bool DesdeOffice_EnvioManual { get; set; }
        public ConfiguracionOperacionesCadenasTexto ConfigOperaciones_CadenasTexto { get; set; }
        [IgnoreDataMember]
        public DateTime? fechaUltimoEstadoArchivo {  get; set; }
        public bool NoConsiderarEjecucion {  get; set; }
        public List<Clasificador> Clasificadores { get; set; }
        public bool EjecutarLogicasTextos_DespuesEjecucion { get; set; }
        public bool EjecutarLogicasTextos_AntesEjecucion { get; set; }
        public bool EjecutarLogicasCantidades_DespuesEjecucion { get; set; }
        public bool EjecutarLogicasCantidades_AntesEjecucion { get; set; }
        [IgnoreDataMember]
        public bool ConCambios_ToolTips { get; set; }
        [IgnoreDataMember]
        public bool Actualizar_ToolTips { get; set; }
        public string RutaArchivoSeleccionado_Tooltip { get; set; }
        public DiseñoOperacion()
        {
            ElementosAnteriores = new List<DiseñoOperacion>();
            ElementosPosteriores = new List<DiseñoOperacion>();
            Tipo = TipoElementoOperacion.Ninguna;
            ElementosDiseñoOperacion = new List<DiseñoElementoOperacion>();
            AnchoDiagrama = double.NaN;
            AltoDiagrama = double.NaN;
            ElementosContenedoresOperacion = new List<DiseñoOperacion>();
            OrdenOperandos = new List<OrdenOperando>();
            CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionesAsignacionSalidas_TextosInformacion>();
            SalidasAgrupamiento_SeleccionOrdenamiento = new List<DiseñoOperacion>();
            AsociacionesTextosInformacion_ElementosSalida = new List<AsociacionTextoInformacion_ElementoSalida>();
            AsociacionesTextosInformacion_Clasificadores = new List<AsociacionTextoInformacion_Clasificador>();
            CondicionesFlujo_SeleccionOrdenamiento = new List<CondicionFlujo>();
            CondicionesTextosInformacion_SeleccionEntradas = new List<CondicionTextosInformacion>();
            AsociacionesCondicionesFlujo_ElementosSalida = new List<AsociacionCondicionFlujo_ElementoSalida>();
            AsociacionesCondicionesFlujo_ElementosSalida2 = new List<AsociacionCondicionFlujo_ElementoSalida>();
            AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida = new List<AsociacionCondicionTextosInformacion_Entradas_ElementoSalida>();
            SalidasAgrupamiento_SeleccionCondicionesFlujo = new List<DiseñoOperacion>();
            ElementosAgrupados = new List<DiseñoOperacion>();
            //ElementosPosterioresAgrupados = new List<DiseñoOperacion>();
            //ElementosAnterioresAgrupados = new List<DiseñoOperacion>();
            IncluirAsignacionTextosInformacionDespues = true;
            DefinicionOpcionesNombresCantidades = new DefinicionTextoNombresCantidades();
            AsignarTextosInformacion_OperandosResultados = true;
            AsignarTextosInformacion_AntesImplicaciones = true;
            DefinicionSimple_Operacion = true;
            DefinicionSimple_TextosInformacion = true;
            DefinicionSimple_CondicionesFlujo = true;
            TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarTodosJuntos;
            OrdenarNumeros_AntesEjecucion = new List<OrdenarNumerosElemento>();
            OrdenarNumeros_DespuesEjecucion = new List<OrdenarNumerosElemento>();
            OperandosTextosInformacionOperandosResultados = new List<DiseñoOperacion>();
            ProcesamientoCantidades = new List<CondicionProcesamientoCantidades>();
            ProcesamientoTextosInformacion = new List<CondicionProcesamientoTextosInformacion>();
            OpcionElementosFijosPotencia = TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos;
            OpcionElementosFijosRaiz = TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos;
            OpcionElementosFijosLogaritmo = TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos;
            OpcionElementosFijosInverso = TipoOpcionElementosFijosOperacionInverso.InversoSumaResta;            
            AgrupacionesOperandos_PorSeparado = new List<AgrupacionOperando_PorSeparado>();
            AplicarProcesamientoDespuesImplicacionesTextosInformacion = true;
            EntradasSalidas_Espera = new List<DiseñoOperacion[]>();
            EntradasSalidas_LimpiarDatos = new List<DiseñoOperacion[]>();
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
            EntradasSalidas_RedondearCantidades = new List<DiseñoOperacion[]>();
            EntradasOperandos_ArchivoExterno = new List<AsociacionEntradaOperando_ArchivoExterno>();
            ResultadosOperandos_ArchivoExterno = new List<AsociacionResultadoOperando_ArchivoExterno>();
            ConfigArchivoExterno = new ConfiguracionArchivoExterno();
            TextosInformacion_Digitacion_Tooltip = new List<string>();
            Cantidades_Digitacion_Tooltip = new List<EntidadNumero>();
            EntradasSeleccionadas_ToolTips = new List<DiseñoOperacion>();
            AgrupacionesAsignadasOperandos_PorSeparado = new List<AsignacionAgrupacionOperando_PorSeparado>();
            ConfigSubCalculo = new ConfiguracionSubCalculo();
            EntradasOperandos_SubCalculo = new List<AsociacionEntradaOperando_ArchivoExterno>();
            ResultadosOperandos_SubCalculo = new List<AsociacionResultadoOperando_ArchivoExterno>();
            DefinicionOpcionesNombresResultados = new DefinicionTextoNombresCantidades();
            TextoEnPantalla_Digitacion_Tooltip = string.Empty;
            ConfigOperaciones_CadenasTexto = new ConfiguracionOperacionesCadenasTexto();
            Clasificadores = new List<Clasificador>();
            AsignarTextosInformacion_DespuesEjecucion = true;
            AsignarTextosInformacionImplicaciones_DespuesEjecucion = true;
            EjecutarLogicasTextos_DespuesEjecucion = true;
            EjecutarLogicasCantidades_DespuesEjecucion = true;
            SeguirOperandoFilas_ConElementoNeutro = true;
            RutaArchivoSeleccionado_Tooltip = string.Empty;
        }

        public bool VerificarCambios(DiseñoOperacion UltimoEstadoEntrada, ComparadorObjetos ComparadorObjetos)
        {
            bool hayCambios = false;

            ComparadorObjetos._paresVisitados.Clear();

            if (!ComparadorObjetos.CompararObjetos(this, UltimoEstadoEntrada))
                hayCambios = true;


            return hayCambios;
        }

        public void OrdenarOperandos(DiseñoOperacion elementoPadre)
        {
            List<OrdenOperando> operandos = (from E in OrdenOperandos where E.ElementoPadre == elementoPadre orderby E.Orden ascending select E).ToList();
            int orden = 1;
            foreach (var item in operandos)
            {
                item.Orden = orden;
                orden++;
            }
        }

        public OrdenOperando Operando(DiseñoOperacion elemento)
        {
            OrdenOperando operando = (from E in OrdenOperandos where E.ElementoPadre == elemento select E).FirstOrDefault();
            return operando;
        }

        public void AgregarOrdenOperando(DiseñoOperacion elementoPadre)
        {
            OrdenOperando nuevoOrdenOperando = new OrdenOperando();
            nuevoOrdenOperando.ElementoPadre = elementoPadre;
            nuevoOrdenOperando.Orden = elementoPadre.ElementosAnteriores.ToList().Count;
            OrdenOperandos.Add(nuevoOrdenOperando);
        }

        public void QuitarOrdenOperando(DiseñoOperacion elementoPadre)
        {
            OrdenOperando ordenOperando = (from E in OrdenOperandos where E.ElementoPadre == elementoPadre select E).FirstOrDefault();
            OrdenOperandos.Remove(ordenOperando);
            OrdenarOperandosHijos(elementoPadre);
        }

        public void OrdenarOperandosHijos(DiseñoOperacion elementoPadre)
        {
            int orden = 1;
            foreach (var elementoHijo in elementoPadre.ElementosAnteriores.OrderBy((i) => (
            (from E in i.OrdenOperandos where E.ElementoPadre == elementoPadre select E).FirstOrDefault() != null
            ? (from E in i.OrdenOperandos where E.ElementoPadre == elementoPadre select E).FirstOrDefault() : new OrdenOperando()).Orden))
            {
                OrdenOperando operando = (from E in elementoHijo.OrdenOperandos where E.ElementoPadre == elementoPadre select E).FirstOrDefault();
                if (operando != null)
                {
                    operando.Orden = orden;
                    orden++;
                }
            }
        }

        private List<DiseñoOperacion> ObtenerTodosElementosAnterioresAgrupados()
        {
            List<DiseñoOperacion> lista = new List<DiseñoOperacion>();

            foreach (var item in ElementosAgrupados.Where(i =>
            (i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAnteriores.Any() && i.ElementosAnteriores.Count(j => ElementosAgrupados.Contains(j)) < i.ElementosAnteriores.Count()) ||
            (i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAnterioresAgrupados.Any() && i.ElementosAnterioresAgrupados.Count(j => ElementosAgrupados.Contains(j)) < i.ElementosAnterioresAgrupados.Count())))
            {
                if(item.Tipo != TipoElementoOperacion.AgrupadorOperaciones)
                    lista.AddRange(item.ElementosAnteriores.Where(j => !ElementosAgrupados.Contains(j)));
                else
                    lista.AddRange(item.ElementosAnterioresAgrupados.Where(j => !ElementosAgrupados.Contains(j)));
            }

            return lista;
        }

        private List<DiseñoOperacion> ObtenerTodosElementosPosterioresAgrupados()
        {
            List<DiseñoOperacion> lista = new List<DiseñoOperacion>();
            
            foreach (var item in ElementosAgrupados.Where(i =>
            (i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && i.ElementosPosteriores.Any() && i.ElementosPosteriores.Count(j => ElementosAgrupados.Contains(j)) < i.ElementosPosteriores.Count()) ||
            (i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosPosterioresAgrupados.Any() && i.ElementosPosterioresAgrupados.Count(j => ElementosAgrupados.Contains(j)) < i.ElementosPosterioresAgrupados.Count())))
            {
                if (item.Tipo != TipoElementoOperacion.AgrupadorOperaciones)
                    lista.AddRange(item.ElementosPosteriores.Where(j => !ElementosAgrupados.Contains(j)));
                else
                    lista.AddRange(item.ElementosPosterioresAgrupados.Where(j => !ElementosAgrupados.Contains(j)));
            }

            return lista;
        }

        public List<DiseñoOperacion> ObtenerTodosElementosAgrupadoresAnteriores(List<DiseñoOperacion> Elementos)
        {
            List<DiseñoOperacion> lista = new List<DiseñoOperacion>();

            foreach (var item in ElementosAgrupados.Where(i =>
            (i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAnteriores.Any() && i.ElementosAnteriores.Count(j => ElementosAgrupados.Contains(j)) < i.ElementosAnteriores.Count()) ||
            (i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAnterioresAgrupados.Any() && i.ElementosAnterioresAgrupados.Count(j => ElementosAgrupados.Contains(j)) < i.ElementosAnterioresAgrupados.Count())))
            {                
                lista.AddRange(Elementos.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones) &&
                j.ElementosPosterioresAgrupados.Any(h => h == item)));
            }

            return lista.Distinct().ToList();
        }

        public List<DiseñoOperacion> ObtenerTodosElementosAgrupadoresPosteriores(List<DiseñoOperacion> Elementos)
        {
            List<DiseñoOperacion> lista = new List<DiseñoOperacion>();

            foreach (var item in ElementosAgrupados.Where(i =>
            (i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && i.ElementosPosteriores.Any() && i.ElementosPosteriores.Count(j => ElementosAgrupados.Contains(j)) < i.ElementosPosteriores.Count()) ||
            (i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosPosterioresAgrupados.Any() && i.ElementosPosterioresAgrupados.Count(j => ElementosAgrupados.Contains(j)) < i.ElementosPosterioresAgrupados.Count())))
            {
                lista.AddRange(Elementos.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones) &&
                j.ElementosAnterioresAgrupados.Any(h => h == item)));
            }

            return lista.Distinct().ToList();
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

        public List<DiseñoElementoOperacion> ObtenerElementosIniciales_FlujoOperacion()
        {
            return ElementosDiseñoOperacion.Where(i => !i.ElementosAnteriores.Any() && 
            i.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion).ToList();
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

        public bool VerificarCambios_ArchivoSubCalculo()
        {
            if (Tipo == TipoElementoOperacion.ArchivoExterno)
            {
                if (ConfigArchivoExterno != null)
                {
                    if (!string.IsNullOrEmpty(ConfigArchivoExterno.Archivo.RutaArchivoEntrada))
                    {
                        var infoArchivo = new FileInfo(ConfigArchivoExterno.Archivo.RutaArchivoEntrada);

                        var diferencia = infoArchivo.LastWriteTime - fechaUltimoEstadoArchivo;
                        if(diferencia != TimeSpan.Zero)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public void ObtenerFechaUltimoEstadoArchivo()
        {
            if (Tipo == TipoElementoOperacion.ArchivoExterno)
            {
                if (ConfigArchivoExterno != null)
                {
                    if (!string.IsNullOrEmpty(ConfigArchivoExterno.Archivo.RutaArchivoEntrada))
                    {
                        var infoArchivo = new FileInfo(ConfigArchivoExterno.Archivo.RutaArchivoEntrada);
                        fechaUltimoEstadoArchivo = infoArchivo.LastWriteTime;
                    }
                }
            }            
        }

        public void AgregarClasificadoresGenericos_CantidadesDigitacion_Entrada(ElementoEntradaEjecucion Elemento)
        {

            Clasificador clasificadorGenerico = Elemento.Clasificadores_Cantidades.FirstOrDefault(i => string.IsNullOrEmpty(i.CadenaTexto));

            if (clasificadorGenerico == null)
            {
                clasificadorGenerico = new Clasificador();
                clasificadorGenerico.CadenaTexto = string.Empty;

                if (Elemento.Clasificadores_Cantidades.Any())
                    Elemento.Clasificadores_Cantidades.Insert(0, clasificadorGenerico);
                else
                    Elemento.Clasificadores_Cantidades.Add(clasificadorGenerico);
            }

            foreach (var itemElemento in Cantidades_Digitacion_Tooltip)
            {
                var clasificadoresAQuitar_Item = itemElemento.Clasificadores_SeleccionarOrdenar.Where(i => string.IsNullOrEmpty(i.CadenaTexto)).ToList();

                while (clasificadoresAQuitar_Item.Any())
                {
                    itemElemento.Clasificadores_SeleccionarOrdenar.Remove(clasificadoresAQuitar_Item.FirstOrDefault());
                    clasificadoresAQuitar_Item.Remove(clasificadoresAQuitar_Item.FirstOrDefault());
                }

                if (!itemElemento.Clasificadores_SeleccionarOrdenar.Any())
                {
                    itemElemento.Clasificadores_SeleccionarOrdenar.Add(clasificadorGenerico);
                }
            }
        }
    }
}
