using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Condiciones
{
    public class CondicionFlujo
    {
        public DiseñoOperacion OperandoCondicion { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento_Condicion { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_Condicion { get; set; }
        public TipoOpcion_CondicionTextosInformacion_Flujo TipoOpcionCondicion_TextosInformacion { get; set; }
        public TipoSubElemento_EvaluacionCondicion_Flujo TipoSubElemento_Condicion { get; set; }
        public TipoSubElemento_EvaluacionCondicion_Flujo TipoSubElemento_Valores { get; set; }
        public TipoOpcion_ValoresCondicion_Flujo Tipo_Valores { get; set; }
        public DiseñoOperacion ElementoOperacion_Valores { get; set; }
        public string Valores_Condicion { get; set; }
        public CondicionFlujo CondicionContenedora { get; set; }
        public TipoConectorCondiciones_ConjuntoBusquedas TipoConector { get; set; }
        public List<CondicionFlujo> Condiciones { get; set; }
        public int CantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaNumerosCumplenCondicion { get; set; }
        public int CantidadSubNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion { get; set; }
        public int CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoTextoInformacion { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion { get; set; }
        public int CantidadSubNumerosCumplenCondicion_Valores { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_Valores { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_Valores { get; set; }
        public int CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion { get; set; }
        public bool NegarCondicion { get; set; }
        public bool CumpleCondicion_ElementoSinNumeros {  get; set; }
        public bool CumpleCondicion_ElementoValores_SinNumeros { get; set; }
        public bool ConectorO_Excluyente { get; set; }
        public bool ConsiderarOperandoCondicion_SiCumple {  get; set; }
        public bool ConsiderarIncluirCondicionesHijas {  get; set; }
        public bool VaciarListaTextosInformacion_CumplenCondicion { get; set; }
        public bool VaciarListaTextosInformacion_CumplenCondicion_Valores { get; set; }
        public bool CantidadTextosInformacion_PorElemento { get; set; }
        public bool CantidadTextosInformacion_PorElemento_Valores { get; set; }
        public bool VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple {  get; set; }
        public bool VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacionCumplenCondicion { get; set; }
        [IgnoreDataMember]        
        public List<string> TextosInformacionCumplenCondicion_Valores { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesCumplenCondicion { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesNoCumplenCondicion { get; set; }
        public List<DiseñoOperacion> Operandos_AplicarCondiciones { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_AplicarCondiciones { get; set; }
        public DiseñoElementoOperacion ElementoSubOperacion_Valores { get; set; }
        public TipoOpcionSeleccionNumerosElemento_Condicion OpcionSeleccionNumerosElemento_CondicionValores { get; set; }
        public bool EsOperandoActual { get; set; }
        public bool EsOperandoValoresActual { get; set; }
        public bool ContenedorCondiciones { get; set; }
        [IgnoreDataMember]
        public Clasificador ClasificadorActual {  get; set; }
        public CondicionFlujo()
        {
            Valores_Condicion = string.Empty;
            TipoConector = TipoConectorCondiciones_ConjuntoBusquedas.InicioCondiciones;
            Tipo_Valores = TipoOpcion_ValoresCondicion_Flujo.ValoresFijos;
            Condiciones = new List<CondicionFlujo>();
            TextosInformacionCumplenCondicion = new List<string>();
            TextosInformacionCumplenCondicion_Valores = new List<string>();
            CantidadNumerosCumplenCondicion = 2;
            OpcionCantidadNumerosCumplenCondicion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaNumerosCumplenCondicion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            Operandos_AplicarCondiciones = new List<DiseñoOperacion>();
            SubOperandos_AplicarCondiciones = new List<DiseñoElementoOperacion>();
            OpcionSeleccionNumerosElemento_Condicion = TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando;
            OpcionSeleccionNumerosElemento_CondicionValores = TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando;
            CantidadSubNumerosCumplenCondicion = 2;
            OpcionCantidadSubNumerosCumplenCondicion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = 2;
            OpcionCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoTextoInformacion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_Valores = 2;
            OpcionCantidadSubNumerosCumplenCondicion_Valores = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_Valores = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = 2;
            OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
            OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.CantidadFija;
            ConsiderarOperandoCondicion_SiCumple = true;
            ConsiderarIncluirCondicionesHijas = true;
            VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple = true;
        }

        public CondicionFlujo ReplicarObjeto()
        {
            CondicionFlujo condicion = new CondicionFlujo();

            condicion.CopiarObjeto(this, this.CondicionContenedora);

            return condicion;
        }

        public void CopiarObjeto(CondicionFlujo condicionACopiar,
            CondicionFlujo condicionContenedoraACopiar)
        {
            if (condicionContenedoraACopiar != null)
                this.CondicionContenedora = condicionContenedoraACopiar;

            this.Condiciones = new List<CondicionFlujo>();
            this.OperandoCondicion = condicionACopiar.OperandoCondicion;
            this.OperandoSubElemento_Condicion = condicionACopiar.OperandoSubElemento_Condicion;
            this.TipoConector = condicionACopiar.TipoConector;
            this.TipoOpcionCondicion_TextosInformacion = condicionACopiar.TipoOpcionCondicion_TextosInformacion;
            this.TipoSubElemento_Condicion = condicionACopiar.TipoSubElemento_Condicion;
            this.TipoSubElemento_Valores = condicionACopiar.TipoSubElemento_Valores;
            this.Tipo_Valores = condicionACopiar.Tipo_Valores;
            this.ElementoOperacion_Valores = condicionACopiar.ElementoOperacion_Valores;
            this.Valores_Condicion = condicionACopiar.Valores_Condicion;
            this.ElementoSubOperacion_Valores = condicionACopiar.ElementoSubOperacion_Valores;
            this.OpcionSeleccionNumerosElemento_Condicion = condicionACopiar.OpcionSeleccionNumerosElemento_Condicion;
            this.CantidadNumerosCumplenCondicion = condicionACopiar.CantidadNumerosCumplenCondicion;
            this.OpcionCantidadNumerosCumplenCondicion = condicionACopiar.OpcionCantidadNumerosCumplenCondicion;
            this.OpcionCantidadDeterminadaNumerosCumplenCondicion = condicionACopiar.OpcionCantidadDeterminadaNumerosCumplenCondicion;
            this.CantidadSubNumerosCumplenCondicion = condicionACopiar.CantidadSubNumerosCumplenCondicion;
            this.OpcionCantidadSubNumerosCumplenCondicion = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion;
            this.CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = condicionACopiar.CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion;
            this.OpcionCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = condicionACopiar.OpcionCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion;
            this.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoTextoInformacion = condicionACopiar.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoTextoInformacion;
            this.OpcionTipoCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = condicionACopiar.OpcionTipoCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion;
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
            this.ElementoSubOperacion_Valores = condicionACopiar.ElementoSubOperacion_Valores;
            this.OpcionSeleccionNumerosElemento_CondicionValores = condicionACopiar.OpcionSeleccionNumerosElemento_CondicionValores;
            this.ContenedorCondiciones = condicionACopiar.ContenedorCondiciones;
            this.NegarCondicion = condicionACopiar.NegarCondicion;
            this.CumpleCondicion_ElementoSinNumeros = condicionACopiar.CumpleCondicion_ElementoSinNumeros;
            this.ConectorO_Excluyente = condicionACopiar.ConectorO_Excluyente;
            this.ConsiderarOperandoCondicion_SiCumple = condicionACopiar.ConsiderarOperandoCondicion_SiCumple;
            this.ConsiderarIncluirCondicionesHijas = condicionACopiar.ConsiderarIncluirCondicionesHijas;
            this.VaciarListaTextosInformacion_CumplenCondicion = condicionACopiar.VaciarListaTextosInformacion_CumplenCondicion;
            this.VaciarListaTextosInformacion_CumplenCondicion_Valores = condicionACopiar.VaciarListaTextosInformacion_CumplenCondicion_Valores;
            this.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple = condicionACopiar.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple;
            this.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores = condicionACopiar.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores;
            this.EsOperandoActual = condicionACopiar.EsOperandoActual;
            this.EsOperandoValoresActual = condicionACopiar.EsOperandoValoresActual;

            foreach (var itemCondicion in condicionACopiar.Condiciones)
            {
                this.Condiciones.Add(new CondicionFlujo());
                this.Condiciones.Last().CopiarObjeto(itemCondicion, this);
            }
        }

        public bool EvaluarCondiciones(EjecucionCalculo ejecucion,
            ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion operando,
            EntidadNumero numero,
            bool primeraCondicion = true)
        {
            bool valorCondicion = !ContenedorCondiciones ? EvaluarCondicion(ejecucion,
                operacion,
                operando,
                numero) : true;

            if (Condiciones.Any())
            {
                List<bool> valoresCondicion = new List<bool>();

                if (!ContenedorCondiciones)
                    valoresCondicion.Add(valorCondicion);

                int indiceCondicion = 0;
                foreach (var itemCondicion in Condiciones)
                {
                    bool valorItemCondicion = itemCondicion.EvaluarCondiciones(ejecucion,
                        operacion,
                        operando,
                        numero,
                        false);
                                        
                    valoresCondicion.Add(valorItemCondicion);

                    if (ContenedorCondiciones &&
                                itemCondicion == Condiciones.First() &&
                                !valorItemCondicion &&
                                Condiciones.Count == 1)
                    {
                        if (primeraCondicion)
                        {
                            numero?.QuitarTextosInformacionCondicionesAntes();
                        }

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

                                    if (primeraCondicion)
                                    {
                                        numero?.QuitarTextosInformacionCondicionesAntes();
                                    }

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

                                    if (primeraCondicion)
                                    {
                                        numero?.QuitarTextosInformacionCondicionesAntes();
                                    }

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

                                    if (primeraCondicion)
                                    {
                                        numero?.QuitarTextosInformacionCondicionesAntes();
                                    }

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

                                if (primeraCondicion)
                                {
                                    numero?.QuitarTextosInformacionCondicionesAntes();
                                }

                                return false;
                            }

                            break;
                    }

                    indiceCondicion++;
                }

                if (valorCondicion)
                    EvaluacionesCumplenCondicion++;
                else
                    EvaluacionesNoCumplenCondicion++;

                switch (OpcionCantidadNumerosCumplenCondicion)
                {
                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                        if (EvaluacionesCumplenCondicion == 0)
                        {
                            if (primeraCondicion)
                            {
                                numero?.QuitarTextosInformacionCondicionesAntes();
                            }

                            return false;
                        }

                        break;
                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                        if (EvaluacionesNoCumplenCondicion > 0)
                        {
                            if (primeraCondicion)
                            {
                                numero?.QuitarTextosInformacionCondicionesAntes();
                            }

                            return false;
                        }

                        break;

                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:
                        switch (OpcionCantidadDeterminadaNumerosCumplenCondicion)
                        {
                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                if (EvaluacionesCumplenCondicion < CantidadNumerosCumplenCondicion)
                                {
                                    if (primeraCondicion)
                                    {
                                        numero?.QuitarTextosInformacionCondicionesAntes();
                                    }

                                    return false;
                                }
                                break;

                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                if (EvaluacionesCumplenCondicion > CantidadNumerosCumplenCondicion)
                                {
                                    if (primeraCondicion)
                                    {
                                        numero?.QuitarTextosInformacionCondicionesAntes();
                                    }

                                    return false;
                                }
                                break;

                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                if (EvaluacionesCumplenCondicion != CantidadNumerosCumplenCondicion)
                                {
                                    if (primeraCondicion)
                                    {
                                        numero?.QuitarTextosInformacionCondicionesAntes();
                                    }

                                    return false;
                                }
                                break;
                        }

                        break;
                }

                if (primeraCondicion)
                {
                    numero?.QuitarTextosInformacionCondicionesAntes();
                }

                if ((ConsiderarIncluirCondicionesHijas || (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones)))
                    EstablecerConsiderarOperando(numero);
                
                return true;
            }
            else
            {
                if (!ContenedorCondiciones)
                {
                    if (valorCondicion)
                        EvaluacionesCumplenCondicion++;
                    else
                        EvaluacionesNoCumplenCondicion++;

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

                    if (primeraCondicion)
                    {
                        numero?.QuitarTextosInformacionCondicionesAntes();
                    }

                    if (valorCondicion && (ConsiderarIncluirCondicionesHijas ||
                        (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones)))
                        EstablecerConsiderarOperando(numero);
                }

                return valorCondicion;
            }
        }

        private bool EvaluarCondicion(EjecucionCalculo ejecucion,
            ElementoOperacionAritmeticaEjecucion operacionActual,
            ElementoEjecucionCalculo operando,
            EntidadNumero numero)
        {
            bool valorCondicion = false;
            bool sinNumerosTextos = false;
            bool sinNumerosTextos_Valores = false;

            string[] valoresCondicion = null;

            int CantidadNumerosValoresCondicion = 0;
            int CantidadNumerosCondicion = 0;
            int CantidadTextosCondicion = 0;
            int CantidadTextosValoresCondicion = 0;

            int NumerosCumplenCondicion_Elemento = 0;
            int NumerosNoCumplenCondicion_Elemento = 0;

            int NumerosCumplenCondicion_Valores = 0;
            int NumerosNoCumplenCondicion_Valores = 0;

            int TextosCumplenCondicion_Elemento = 0;
            int TextosNoCumplenCondicion_Elemento = 0;

            int TextosCumplenCondicion_Valores = 0;
            int TextosNoCumplenCondicion_Valores = 0;

            List<InformacionCantidadesTextosInformacion_CondicionFlujo> CantidadesTextos = new List<InformacionCantidadesTextosInformacion_CondicionFlujo>();
            List<InformacionCantidadesTextosInformacion_CondicionFlujo> CantidadesTextos_Valores = new List<InformacionCantidadesTextosInformacion_CondicionFlujo>();
            int indiceCantidadesTextos_Valores = 0;

            switch (Tipo_Valores)
            {
                case TipoOpcion_ValoresCondicion_Flujo.ValoresFijos:
                    valoresCondicion = Valores_Condicion.Split('*');
                    CantidadTextosValoresCondicion = valoresCondicion.Length;
                    CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                    CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = CantidadTextosValoresCondicion;

                    CantidadNumerosValoresCondicion = valoresCondicion.Length;

                    break;

                case TipoOpcion_ValoresCondicion_Flujo.Valores_DesdeElementoOperacion:
                    if (ElementoOperacion_Valores != null ||
                        EsOperandoValoresActual)
                    {
                        var elementoEjecucionCondicion = ejecucion.ObtenerElementoEjecucion(ElementoOperacion_Valores);
                        var subElementoEjecucionCondicion = ejecucion.ObtenerSubElementoEjecucion(ElementoSubOperacion_Valores);

                        if(EsOperandoValoresActual)
                        {
                            elementoEjecucionCondicion = operando;

                            if(operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                                subElementoEjecucionCondicion = (ElementoDiseñoOperacionAritmeticaEjecucion)operando;
                        }

                        List<string> numerosElemento_Valores = new List<string>();

                        ElementoOperacionAritmeticaEjecucion operacion_Valores = null;

                        if (elementoEjecucionCondicion != null &&
                            subElementoEjecucionCondicion == null)
                        {
                            operacion_Valores = (ElementoOperacionAritmeticaEjecucion)elementoEjecucionCondicion;

                        }
                        else if (elementoEjecucionCondicion != null &&
                            subElementoEjecucionCondicion != null)
                        {
                            operacion_Valores = (ElementoOperacionAritmeticaEjecucion)subElementoEjecucionCondicion;
                        }

                        if (operacion_Valores != null)

                        {
                            var tipoCondicionValores = TipoSubElemento_Valores;

                            if (tipoCondicionValores == TipoSubElemento_EvaluacionCondicion_Flujo.Ninguno)
                            {
                                tipoCondicionValores = TipoSubElemento_Condicion;
                            }

                            switch (tipoCondicionValores)
                            {
                                case TipoSubElemento_EvaluacionCondicion_Flujo.NumerosElemento:

                                    switch (OpcionSeleccionNumerosElemento_CondicionValores)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:

                                            numerosElemento_Valores.AddRange(operacion_Valores.Numeros.Where(i =>
                                            ((!operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count && i == operacion_Valores.Clasificadores_Cantidades[operacion_Valores.IndicePosicionClasificadores]) || !(operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count))) ||
                                                    (operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                            ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                    i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                    i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                    i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))).Select(j => j.Numero.ToString("N" + ejecucion.Calculo.CantidadDecimalesCantidades.ToString())));

                                            break;
                                    }


                                    CantidadNumerosValoresCondicion = numerosElemento_Valores.Count;
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_Flujo.CantidadNumerosElemento:

                                    switch (OpcionSeleccionNumerosElemento_CondicionValores)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            numerosElemento_Valores.Add(operacion_Valores.Numeros.Count(i =>
                                            ((!operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count && i == operacion_Valores.Clasificadores_Cantidades[operacion_Valores.IndicePosicionClasificadores]) || !(operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count))) ||
                                            (operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                            ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))).ToString());

                                            break;
                                    }

                                    CantidadNumerosValoresCondicion = 1;

                                    break;

                                case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion:

                                    switch (OpcionSeleccionNumerosElemento_CondicionValores)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            foreach (var itemTexto in operacion_Valores.Numeros.Where(i =>
                                            ((!operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count && i == operacion_Valores.Clasificadores_Cantidades[operacion_Valores.IndicePosicionClasificadores]) || !(operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count))) ||
                                            (operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                            ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                numerosElemento_Valores.AddRange(itemTexto.Textos.ToList());

                                            break;
                                    }


                                    CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                                    CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacionCumplenCondicion:

                                    switch (OpcionSeleccionNumerosElemento_CondicionValores)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            foreach (var itemTexto in operacion_Valores.Numeros.Where(i =>
                                            ((!operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count && i == operacion_Valores.Clasificadores_Cantidades[operacion_Valores.IndicePosicionClasificadores]) || !(operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count))) ||
                                            (operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                            ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                numerosElemento_Valores.AddRange(itemTexto.Textos.Intersect(TextosInformacionCumplenCondicion_Valores).ToList());

                                            break;
                                    }


                                    CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                                    CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_Flujo.TodosTextosInformacionCumplenCondicion:
                                case TipoSubElemento_EvaluacionCondicion_Flujo.TodosClasificadoresCumplenCondicion:
                                    numerosElemento_Valores.AddRange(TextosInformacionCumplenCondicion_Valores);

                                    CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                                    CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento:

                                    switch (OpcionSeleccionNumerosElemento_CondicionValores)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            foreach (var itemTexto in operacion_Valores.Numeros.Where(i =>
                                            ((!operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count && i == operacion_Valores.Clasificadores_Cantidades[operacion_Valores.IndicePosicionClasificadores]) || !(operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count))) ||
                                            (operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                            ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                numerosElemento_Valores.Add(itemTexto.Nombre);

                                            break;
                                    }


                                    CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                                    CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_Flujo.Clasificadores:

                                    switch (OpcionSeleccionNumerosElemento_CondicionValores)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            foreach (var itemTexto in operacion_Valores.Numeros.Where(i =>
                                            ((!operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count && i == operacion_Valores.Clasificadores_Cantidades[operacion_Valores.IndicePosicionClasificadores]) || !(operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count))) ||
                                            (operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                            ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                numerosElemento_Valores.AddRange(itemTexto.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList());

                                            break;
                                    }


                                    CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                                    CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_Flujo.ClasificadoresCumplenCondicion:

                                    switch (OpcionSeleccionNumerosElemento_CondicionValores)
                                    {
                                        case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:
                                            foreach (var itemTexto in operacion_Valores.Numeros.Where(i =>
                                            ((!operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count && i == operacion_Valores.Clasificadores_Cantidades[operacion_Valores.IndicePosicionClasificadores]) || !(operacion_Valores.IndicePosicionClasificadores < operacion_Valores.Clasificadores_Cantidades.Count))) ||
                                            (operacion_Valores.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                            ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                            i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                            i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                numerosElemento_Valores.AddRange(itemTexto.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList().Intersect(TextosInformacionCumplenCondicion_Valores).ToList());

                                            break;
                                    }


                                    CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    CantidadesTextos_Valores.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                                    CantidadesTextos_Valores.LastOrDefault().CantidadTextosValoresCondicion = numerosElemento_Valores.Count;
                                    break;
                            }
                        }

                        valoresCondicion = numerosElemento_Valores.ToArray();
                    }
                    break;
            }

            if (!valoresCondicion.Any())
                sinNumerosTextos_Valores = true;

            ElementoOperacionAritmeticaEjecucion operacion = null;
            int cantidadNumeros = 0;
            List<EntidadNumero> numerosElemento = new List<EntidadNumero>();

            var compararTextos = new ComparadorTextosInformacion_Flujo(TipoOpcionCondicion_TextosInformacion);
            List<string> textosInformacionElemento = new List<string>();

            var elementoEncontrado = ejecucion.ObtenerElementoEjecucion(OperandoCondicion);

            if (EsOperandoActual)
                elementoEncontrado = operando;

            if (elementoEncontrado != null)
            {
                if (OperandoSubElemento_Condicion == null)
                {
                    if (elementoEncontrado.Tipo == TipoElementoEjecucion.Entrada |
                        elementoEncontrado.Tipo == TipoElementoEjecucion.OperacionAritmetica |
                        elementoEncontrado.Tipo == TipoElementoEjecucion.ElementoOperacionAritmetica)
                    {
                        operacion = (ElementoOperacionAritmeticaEjecucion)elementoEncontrado;
                    }
                }
                else
                {
                    var subElementoEncontrado_ = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion);

                    if (EsOperandoActual &&
                        operando is ElementoDiseñoOperacionAritmeticaEjecucion)
                        subElementoEncontrado_ = (ElementoDiseñoOperacionAritmeticaEjecucion)operando;

                    if (subElementoEncontrado_ != null)
                    {
                        if (subElementoEncontrado_.TipoInterno == TipoElementoOperacionEjecucion.Entrada)
                        {
                            var entrada = subElementoEncontrado_.EntradaEjecucion;

                            if (entrada.TipoEntrada == TipoEntrada.ConjuntoNumeros)
                            {
                                operacion = (ElementoOperacionAritmeticaEjecucion)entrada;
                            }
                        }
                        else
                        {
                            operacion = ejecucion.ObtenerSubElementoEjecucion(OperandoSubElemento_Condicion);

                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            switch (TipoSubElemento_Condicion)
            {
                case TipoSubElemento_EvaluacionCondicion_Flujo.NumerosElemento:
                case TipoSubElemento_EvaluacionCondicion_Flujo.CantidadNumerosElemento:

                    if (operacion != null)
                    {
                        switch (OpcionSeleccionNumerosElemento_Condicion)
                        {
                            case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:

                                cantidadNumeros = operacion.Numeros.Count(i =>
                                ((!operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && i == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores]) || !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                        i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual))));

                                numerosElemento.AddRange(operacion.Numeros.Where((i =>
                                ((!operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && i == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores]) || !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual))))));

                                break;
                        }
                    }
                    else
                    {
                        return false;
                    }

                    CantidadNumerosCondicion = cantidadNumeros;

                    if (numerosElemento.Any())
                    {
                        switch (TipoSubElemento_Condicion)
                        {
                            case TipoSubElemento_EvaluacionCondicion_Flujo.CantidadNumerosElemento:

                                switch (TipoOpcionCondicion_TextosInformacion)
                                {
                                    case TipoOpcion_CondicionTextosInformacion_Flujo.Contiene:
                                        if (valoresCondicion.Any(item => cantidadNumeros.ToString().Trim().ToLower().Contains(item.Trim().ToLower())))
                                        {
                                            valorCondicion = true;
                                            NumerosCumplenCondicion_Valores += valoresCondicion.Count(item => cantidadNumeros.ToString().Trim().ToLower().Contains(item.Trim().ToLower()));
                                            NumerosNoCumplenCondicion_Valores += valoresCondicion.Count(item => !cantidadNumeros.ToString().Trim().ToLower().Contains(item.Trim().ToLower()));
                                        }

                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsParteDe:
                                        if (valoresCondicion.Any(item => item.ToString().Trim().ToLower().Contains(cantidadNumeros.ToString().Trim().ToLower())))
                                        {
                                            valorCondicion = true;
                                            NumerosCumplenCondicion_Valores += valoresCondicion.Count(item => item.ToString().Trim().ToLower().Contains(cantidadNumeros.ToString().Trim().ToLower()));
                                            NumerosNoCumplenCondicion_Valores += valoresCondicion.Count(item => !item.ToString().Trim().ToLower().Contains(cantidadNumeros.ToString().Trim().ToLower()));
                                        }

                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.EmpiezaCon:
                                        if (valoresCondicion.Any(item => cantidadNumeros.ToString().Trim().ToLower().StartsWith(item.Trim().ToLower())))
                                        {
                                            valorCondicion = true;
                                            NumerosCumplenCondicion_Valores += valoresCondicion.Count(item => cantidadNumeros.ToString().Trim().ToLower().StartsWith(item.Trim().ToLower()));
                                            NumerosNoCumplenCondicion_Valores += valoresCondicion.Count(item => !cantidadNumeros.ToString().Trim().ToLower().StartsWith(item.Trim().ToLower()));
                                        }

                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsDistintoA:
                                        if (valoresCondicion.Any(item =>
                                        {
                                            int numero = 0;
                                            if (int.TryParse(item, out numero))
                                            {
                                                if (cantidadNumeros != numero)
                                                {
                                                    NumerosCumplenCondicion_Valores++;
                                                    NumerosCumplenCondicion_Elemento++;
                                                    return true;
                                                }
                                                else
                                                {
                                                    NumerosNoCumplenCondicion_Valores++;
                                                    NumerosNoCumplenCondicion_Elemento++;
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Valores++;
                                                NumerosNoCumplenCondicion_Elemento++;
                                                return false;
                                            }
                                        }))
                                            valorCondicion = true;

                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsIgualA:
                                        if (valoresCondicion.Any(item =>
                                        {
                                            int numero = 0;
                                            if (int.TryParse(item, out numero))
                                            {
                                                if (cantidadNumeros == numero)
                                                {
                                                    NumerosCumplenCondicion_Valores++;
                                                    NumerosCumplenCondicion_Elemento++;
                                                    return true;
                                                }
                                                else
                                                {
                                                    NumerosNoCumplenCondicion_Valores++;
                                                    NumerosNoCumplenCondicion_Elemento++;
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Valores++;
                                                NumerosNoCumplenCondicion_Elemento++;
                                                return false;
                                            }
                                        }))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.MayorOIgualQue:
                                        if (valoresCondicion.Any(item =>
                                        {
                                            int numero = 0;
                                            if (int.TryParse(item, out numero))
                                            {
                                                if (cantidadNumeros >= numero)
                                                {
                                                    NumerosCumplenCondicion_Valores++;
                                                    NumerosCumplenCondicion_Elemento++;
                                                    return true;
                                                }
                                                else
                                                {
                                                    NumerosNoCumplenCondicion_Valores++;
                                                    NumerosNoCumplenCondicion_Elemento++;
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Valores++;
                                                NumerosNoCumplenCondicion_Elemento++;
                                                return false;
                                            }
                                        }))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.MayorQue:
                                        if (valoresCondicion.Any(item =>
                                        {
                                            int numero = 0;
                                            if (int.TryParse(item, out numero))
                                            {
                                                if (cantidadNumeros > numero)
                                                {
                                                    NumerosCumplenCondicion_Valores++;
                                                    NumerosCumplenCondicion_Elemento++;
                                                    return true;
                                                }
                                                else
                                                {
                                                    NumerosNoCumplenCondicion_Valores++;
                                                    NumerosNoCumplenCondicion_Elemento++;
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Valores++;
                                                NumerosNoCumplenCondicion_Elemento++;
                                                return false;
                                            }
                                        }))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.MenorOIgualQue:
                                        if (valoresCondicion.Any(item =>
                                        {
                                            int numero = 0;
                                            if (int.TryParse(item, out numero))
                                            {
                                                if (cantidadNumeros <= numero)
                                                {
                                                    NumerosCumplenCondicion_Valores++;
                                                    NumerosCumplenCondicion_Elemento++;
                                                    return true;
                                                }
                                                else
                                                {
                                                    NumerosNoCumplenCondicion_Valores++;
                                                    NumerosNoCumplenCondicion_Elemento++;
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Valores++;
                                                NumerosNoCumplenCondicion_Elemento++;
                                                return false;
                                            }
                                        }))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.MenorQue:
                                        if (valoresCondicion.Any(item =>
                                        {
                                            int numero = 0;
                                            if (int.TryParse(item, out numero))
                                            {
                                                if (cantidadNumeros < numero)
                                                {
                                                    NumerosCumplenCondicion_Valores++;
                                                    NumerosCumplenCondicion_Elemento++;
                                                    return true;
                                                }
                                                else
                                                {
                                                    NumerosNoCumplenCondicion_Valores++;
                                                    NumerosNoCumplenCondicion_Elemento++;
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                NumerosNoCumplenCondicion_Valores++;
                                                NumerosNoCumplenCondicion_Elemento++;
                                                return false;
                                            }
                                        }))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.NoContiene:
                                        if (!valoresCondicion.Any(item => cantidadNumeros.ToString().Trim().ToLower().Contains(item.Trim().ToLower())))
                                        {
                                            valorCondicion = true;
                                            NumerosCumplenCondicion_Valores += valoresCondicion.Count(item => !cantidadNumeros.ToString().Trim().ToLower().Contains(item.Trim().ToLower()));
                                            NumerosNoCumplenCondicion_Valores += valoresCondicion.Count(item => cantidadNumeros.ToString().Trim().ToLower().Contains(item.Trim().ToLower()));
                                        }
                                        break;

                                    case TipoOpcion_CondicionTextosInformacion_Flujo.TerminaCon:
                                        if (valoresCondicion.Any(item => cantidadNumeros.ToString().Trim().ToLower().EndsWith(item.Trim().ToLower())))
                                        {
                                            valorCondicion = true;
                                            NumerosCumplenCondicion_Valores += valoresCondicion.Count(item => cantidadNumeros.ToString().Trim().ToLower().EndsWith(item.Trim().ToLower()));
                                            NumerosNoCumplenCondicion_Valores += valoresCondicion.Count(item => !cantidadNumeros.ToString().Trim().ToLower().EndsWith(item.Trim().ToLower()));
                                        }

                                        break;
                                }

                                break;

                            case TipoSubElemento_EvaluacionCondicion_Flujo.NumerosElemento:
                                List<EntidadNumero> numeros = new List<EntidadNumero>();

                                foreach (var itemNumero in valoresCondicion)
                                {
                                    double num = 0;
                                    if (double.TryParse(itemNumero, out num))
                                    {
                                        EntidadNumero elementoNumero = new EntidadNumero();
                                        elementoNumero.Numero = num;
                                        numeros.Add(elementoNumero);
                                    }
                                    else
                                    {
                                        NumerosNoCumplenCondicion_Valores++;
                                    }
                                }

                                if (TipoOpcionCondicion_TextosInformacion == TipoOpcion_CondicionTextosInformacion_Flujo.EsDistintoA)
                                {
                                    if (numeros.Any() && !numerosElemento.Any(numero => numeros.Any(numeroCondicion => (new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion))))
                                    {
                                        valorCondicion = true;
                                        NumerosCumplenCondicion_Elemento += numerosElemento.Count(numero => numeros.Any(numeroCondicion => !(new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion)));
                                        NumerosNoCumplenCondicion_Elemento += numerosElemento.Count(numero => numeros.Any(numeroCondicion => (new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion)));

                                        NumerosCumplenCondicion_Valores += numerosElemento.Count(numero => numeros.Any(numeroCondicion => !(new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion)));
                                        NumerosNoCumplenCondicion_Valores += numerosElemento.Count(numero => numeros.Any(numeroCondicion => (new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion)));
                                    }
                                }
                                else
                                {
                                    if (numerosElemento.Any(numero => numeros.Any(numeroCondicion => (new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion))))
                                    {
                                        valorCondicion = true;
                                        NumerosCumplenCondicion_Elemento += numerosElemento.Count(numero => numeros.Any(numeroCondicion => (new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion)));
                                        NumerosNoCumplenCondicion_Elemento += numerosElemento.Count(numero => numeros.Any(numeroCondicion => !(new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion)));

                                        NumerosCumplenCondicion_Valores += numerosElemento.Count(numero => numeros.Any(numeroCondicion => (new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion)));
                                        NumerosNoCumplenCondicion_Valores += numerosElemento.Count(numero => numeros.Any(numeroCondicion => !(new ComparadorNumeros_Condicion_Flujo(TipoOpcionCondicion_TextosInformacion)).Equals(numero, numeroCondicion)));
                                    }
                                }

                                break;
                        }
                    }
                    else
                    {
                        sinNumerosTextos = true;
                    }

                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion:
                case TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento:
                case TipoSubElemento_EvaluacionCondicion_Flujo.Clasificadores:
                case TipoSubElemento_EvaluacionCondicion_Flujo.ClasificadoresCumplenCondicion:

                    switch (TipoSubElemento_Condicion)
                    {
                        case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion:

                            if (operacion != null)
                            {
                                switch (OpcionSeleccionNumerosElemento_Condicion)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:

                                        foreach (var itemTextos in (from E in operacion.Numeros.Where((i =>
                                        ((!operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && i == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores]) || !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                                                    ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                                    select E.Textos))
                                            textosInformacionElemento.AddRange(itemTextos);


                                        break;
                                }
                            }
                            else
                            {
                                return false;
                            }

                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacionCumplenCondicion:
                            if (operacion != null)
                            {
                                switch (OpcionSeleccionNumerosElemento_Condicion)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:

                                        foreach (var itemTextos in (from E in operacion.Numeros.Where((i =>
                                        ((!operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && i == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores]) || !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                                                    ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                                    select E.Textos))
                                            textosInformacionElemento.AddRange(itemTextos.Intersect(TextosInformacionCumplenCondicion));


                                        break;
                                }
                            }
                            else
                            {
                                return false;
                            }

                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.TodosTextosInformacionCumplenCondicion:
                        case TipoSubElemento_EvaluacionCondicion_Flujo.TodosClasificadoresCumplenCondicion:
                            textosInformacionElemento.AddRange(TextosInformacionCumplenCondicion);
                                break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento:

                            if (operacion != null)
                            {
                                switch (OpcionSeleccionNumerosElemento_Condicion)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:

                                        foreach (var itemTextos in (from E in operacion.Numeros.Where((i =>
                                        ((!operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && i == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores]) || !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                                                    ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                                    select E.Nombre))
                                            textosInformacionElemento.Add(itemTextos);


                                        break;
                                }
                            }
                            else
                            {
                                return false;
                            }

                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.Clasificadores:

                            if (operacion != null)
                            {
                                switch (OpcionSeleccionNumerosElemento_Condicion)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:

                                        foreach (var itemTextos in (from E in operacion.Numeros.Where((i =>
                                        ((!operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && i == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores]) || !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                                                    ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                                    select E.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()))
                                            textosInformacionElemento.AddRange(itemTextos);


                                        break;
                                }
                            }
                            else
                            {
                                return false;
                            }

                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.ClasificadoresCumplenCondicion:
                            if (operacion != null)
                            {
                                switch (OpcionSeleccionNumerosElemento_Condicion)
                                {
                                    case TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando:

                                        foreach (var itemTextos in (from E in operacion.Numeros.Where((i =>
                                        ((!operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => (operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count && i == operacion.Clasificadores_Cantidades[operacion.IndicePosicionClasificadores]) || !(operacion.IndicePosicionClasificadores < operacion.Clasificadores_Cantidades.Count))) ||
                                        (operacion.Clasificadores_Cantidades.Contains(ClasificadorActual) &&
                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i == ClasificadorActual))) &&
                                                                    ((!i.ElementosSalidaOperacion_Agrupamiento.Any() ||
                                i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacionActual)) & (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacionActual)))))
                                                                    select E.Clasificadores_SeleccionarOrdenar.Select(i => i.CadenaTexto).ToList()))
                                            textosInformacionElemento.AddRange(itemTextos.Intersect(TextosInformacionCumplenCondicion));


                                        break;
                                }
                            }
                            else
                            {
                                return false;
                            }

                            break;
                    }

                    if (textosInformacionElemento.Any())
                    {
                        if (TipoOpcionCondicion_TextosInformacion == TipoOpcion_CondicionTextosInformacion_Flujo.EsDistintoA)
                        {
                            if (textosInformacionElemento.Any() && !compararTextos.Interseccion(textosInformacionElemento, valoresCondicion))
                            {
                                TextosInformacionCumplenCondicion.AddRange(textosInformacionElemento);
                                TextosInformacionCumplenCondicion_Valores.AddRange(valoresCondicion);
                                valorCondicion = true;

                                NumerosCumplenCondicion_Elemento += 1;
                                NumerosCumplenCondicion_Valores += 1;

                                CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                                CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos_Valores[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                if (indiceCantidadesTextos_Valores < CantidadesTextos_Valores.Count - 1)
                                    indiceCantidadesTextos_Valores++;

                                compararTextos.ContarInterseccion(textosInformacionElemento, valoresCondicion);
                                TextosCumplenCondicion_Elemento += compararTextos.TextosCumplenCondicion.Count;
                                TextosNoCumplenCondicion_Elemento += compararTextos.TextosNoCumplenCondicion.Count;

                                CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = compararTextos.TextosCumplenCondicion.Count;
                                CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = compararTextos.TextosNoCumplenCondicion.Count;

                                compararTextos.ContarInterseccion(valoresCondicion, textosInformacionElemento);
                                TextosCumplenCondicion_Valores += compararTextos.TextosCumplenCondicion.Count;
                                TextosNoCumplenCondicion_Valores += compararTextos.TextosNoCumplenCondicion.Count;

                                CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = compararTextos.TextosCumplenCondicion.Count;
                                CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = compararTextos.TextosNoCumplenCondicion.Count;
                            }
                            else
                            {
                                NumerosNoCumplenCondicion_Elemento += 1;
                                NumerosNoCumplenCondicion_Valores += 1;
                            }
                        }
                        else
                        {
                            if (compararTextos.Interseccion(textosInformacionElemento, valoresCondicion))
                            {
                                compararTextos.TextosInformacionInvolucrados.Clear();
                                compararTextos.ContarInterseccion(textosInformacionElemento, valoresCondicion);

                                TextosInformacionCumplenCondicion.AddRange(ejecucion.GenerarTextosInformacion(compararTextos.TextosInformacionInvolucrados));
                                TextosInformacionCumplenCondicion_Valores.AddRange(ejecucion.GenerarTextosInformacion(compararTextos.TextosInformacionInvolucrados));
                                valorCondicion = true;

                                NumerosCumplenCondicion_Elemento += 1;
                                NumerosCumplenCondicion_Valores += 1;
                                                               
                                CantidadesTextos.Add(new InformacionCantidadesTextosInformacion_CondicionFlujo());
                                CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadesTextos_Valores[indiceCantidadesTextos_Valores].CantidadTextosValoresCondicion;
                                if (indiceCantidadesTextos_Valores < CantidadesTextos_Valores.Count - 1)
                                    indiceCantidadesTextos_Valores++;

                                TextosCumplenCondicion_Elemento += compararTextos.TextosCumplenCondicion.Count;
                                TextosNoCumplenCondicion_Elemento += compararTextos.TextosNoCumplenCondicion.Count;

                                CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Elemento = compararTextos.TextosCumplenCondicion.Count;
                                CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Elemento = compararTextos.TextosNoCumplenCondicion.Count;

                                compararTextos.ContarInterseccion(valoresCondicion, textosInformacionElemento);
                                TextosCumplenCondicion_Valores += compararTextos.TextosCumplenCondicion.Count;
                                TextosNoCumplenCondicion_Valores += compararTextos.TextosNoCumplenCondicion.Count;

                                CantidadesTextos.LastOrDefault().TextosCumplenCondicion_Valores = compararTextos.TextosCumplenCondicion.Count;
                                CantidadesTextos.LastOrDefault().TextosNoCumplenCondicion_Valores = compararTextos.TextosNoCumplenCondicion.Count;

                            }
                            else
                            {
                                NumerosNoCumplenCondicion_Elemento += 1;
                                NumerosNoCumplenCondicion_Valores += 1;
                            }
                        }
                    }
                    else
                    {
                        sinNumerosTextos = true;
                    }

                    CantidadTextosCondicion = textosInformacionElemento.Count;

                    if (CantidadesTextos.LastOrDefault() != null)
                    {
                        CantidadesTextos.LastOrDefault().CantidadTextosCondicion = textosInformacionElemento.Count;
                        CantidadesTextos.LastOrDefault().CantidadTextosValoresCondicion = CantidadTextosValoresCondicion;
                    }

                    break;
            }


            switch (OpcionCantidadSubNumerosCumplenCondicion)
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

                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion)
                    {
                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                            CantidadSubNumerosCumplenCondicion = NumerosCumplenCondicion_Elemento;
                            break;

                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                            CantidadSubNumerosCumplenCondicion = NumerosCumplenCondicion_Valores;
                            break;

                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                            CantidadSubNumerosCumplenCondicion = CantidadNumerosCondicion;
                            break;

                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                            CantidadSubNumerosCumplenCondicion = CantidadNumerosValoresCondicion;
                            break;
                    }

                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion)
                    {
                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                            if (NumerosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion)
                                valorCondicion = false;
                            break;

                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                            if (NumerosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion)
                                valorCondicion = false;
                            break;

                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                            if (NumerosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion)
                                valorCondicion = false;
                            break;
                    }

                    break;
            }

            if (TipoSubElemento_Condicion == TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion |
                TipoSubElemento_Condicion == TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento |
                TipoSubElemento_Condicion == TipoSubElemento_EvaluacionCondicion_Flujo.Clasificadores |
                TipoSubElemento_Valores == TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion |
                TipoSubElemento_Valores == TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento |
                TipoSubElemento_Valores == TipoSubElemento_EvaluacionCondicion_Flujo.Clasificadores)
            {
                if (CantidadTextosInformacion_PorElemento)
                {
                    foreach(var itemCantidades in CantidadesTextos)
                    {
                        switch (OpcionCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                        {
                            case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                if (itemCantidades.TextosCumplenCondicion_Elemento == 0)
                                    valorCondicion = false;

                                break;
                            case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                if (itemCantidades.TextosNoCumplenCondicion_Elemento > 0)
                                    valorCondicion = false;

                                break;

                            case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                switch (OpcionTipoCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = itemCantidades.TextosCumplenCondicion_Elemento;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                        CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = itemCantidades.TextosCumplenCondicion_Valores;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                        CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = itemCantidades.CantidadTextosCondicion;
                                        break;

                                    case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                        CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = itemCantidades.CantidadTextosValoresCondicion;
                                        break;
                                }

                                switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoTextoInformacion)
                                {
                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                        if (itemCantidades.TextosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                        if (itemCantidades.TextosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;

                                    case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                        if (itemCantidades.TextosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                                            valorCondicion = false;
                                        break;
                                }

                                break;
                        }
                    }
                }
                else
                {
                    switch (OpcionCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
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

                            switch (OpcionTipoCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                            {
                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = TextosCumplenCondicion_Elemento;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                    CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = TextosCumplenCondicion_Valores;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                    CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = CantidadTextosCondicion;
                                    break;

                                case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                    CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = CantidadTextosValoresCondicion;
                                    break;
                            }

                            switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                    if (TextosCumplenCondicion_Elemento < CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                    if (TextosCumplenCondicion_Elemento > CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                                        valorCondicion = false;
                                    break;

                                case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                    if (TextosCumplenCondicion_Elemento != CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion)
                                        valorCondicion = false;
                                    break;
                            }

                            break;
                    }
                }
            }

            if(valorCondicion &&
                Tipo_Valores == TipoOpcion_ValoresCondicion_Flujo.Valores_DesdeElementoOperacion)
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
                                CantidadSubNumerosCumplenCondicion_Valores = CantidadNumerosCondicion;
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

                if (TipoSubElemento_Condicion == TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion |
                    TipoSubElemento_Condicion == TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento |
                    TipoSubElemento_Valores == TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion |
                    TipoSubElemento_Valores == TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento)
                {
                    if (CantidadTextosInformacion_PorElemento_Valores)
                    {
                        foreach (var itemCantidades in CantidadesTextos)
                        {
                            switch (OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                            {
                                case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                                    if (itemCantidades.TextosCumplenCondicion_Valores == 0)
                                        valorCondicion = false;

                                    break;
                                case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                                    if (itemCantidades.TextosNoCumplenCondicion_Valores > 0)
                                        valorCondicion = false;

                                    break;

                                case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:

                                    switch (OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicionCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidades.TextosCumplenCondicion_Elemento;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValoresCumplenCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidades.TextosCumplenCondicion_Valores;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoCondicion:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidades.CantidadTextosCondicion;
                                            break;

                                        case TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion.NumerosOperandoValores:
                                            CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = itemCantidades.CantidadTextosValoresCondicion;
                                            break;
                                    }

                                    switch (OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                    {
                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                            if (itemCantidades.TextosCumplenCondicion_Valores < CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                            if (itemCantidades.TextosCumplenCondicion_Valores > CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicion = false;
                                            break;

                                        case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                            if (itemCantidades.TextosCumplenCondicion_Valores != CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion)
                                                valorCondicion = false;
                                            break;
                                    }

                                    break;
                            }
                        }
                    }
                    else
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

            if (CumpleCondicion_ElementoSinNumeros &&
                                sinNumerosTextos)
                valorCondicion = true;

            if (CumpleCondicion_ElementoValores_SinNumeros &&
                                sinNumerosTextos_Valores)
                valorCondicion = true;

            if (valorCondicion && (ConsiderarIncluirCondicionesHijas ||
                    (!ConsiderarIncluirCondicionesHijas && ContenedorCondiciones)))
                EstablecerConsiderarOperando(numero);

            if(VaciarListaTextosInformacion_CumplenCondicion)
            {
                if ((VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple && valorCondicion)
                || (!VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple && !valorCondicion))
                    TextosInformacionCumplenCondicion.Clear();
            }

            if (VaciarListaTextosInformacion_CumplenCondicion_Valores)
            {
                if ((VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores && valorCondicion)
                || (!VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores && !valorCondicion))
                    TextosInformacionCumplenCondicion_Valores.Clear();
            }

            if (NegarCondicion)
                return !valorCondicion;
            else
                return valorCondicion;
        }

        private void EstablecerConsiderarOperando(EntidadNumero numeroOperando)
        {
            if(ConsiderarOperandoCondicion_SiCumple)
            {
                if(numeroOperando != null)
                    numeroOperando.ConsiderarOperandoCondicion_SiCumple = true;
            }
        }

        public class ComparadorNumeros_Condicion_Flujo : IEqualityComparer<EntidadNumero>
        {
            public TipoOpcion_CondicionTextosInformacion_Flujo TipoOpcionCondicion_ElementoOperacionEntrada { get; set; }
            public ComparadorNumeros_Condicion_Flujo(TipoOpcion_CondicionTextosInformacion_Flujo tipoOpcion)
            {
                TipoOpcionCondicion_ElementoOperacionEntrada = tipoOpcion;
            }
            public bool Equals(EntidadNumero x, EntidadNumero y)
            {
                switch (TipoOpcionCondicion_ElementoOperacionEntrada)
                {
                    case TipoOpcion_CondicionTextosInformacion_Flujo.Contiene:
                        return (x.Numero.ToString().Trim().ToLower().Contains(y.Numero.ToString().Trim().ToLower()));

                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsParteDe:
                        return (y.Numero.ToString().Trim().ToLower().Contains(x.Numero.ToString().Trim().ToLower()));

                    case TipoOpcion_CondicionTextosInformacion_Flujo.EmpiezaCon:
                        return (x.Numero.ToString().Trim().ToLower().StartsWith(y.Numero.ToString().Trim().ToLower()));

                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsDistintoA:
                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsIgualA:
                        return (x.Numero == y.Numero);

                    case TipoOpcion_CondicionTextosInformacion_Flujo.MayorOIgualQue:
                        return (x.Numero >= y.Numero);

                    case TipoOpcion_CondicionTextosInformacion_Flujo.MayorQue:
                        return (x.Numero > y.Numero);

                    case TipoOpcion_CondicionTextosInformacion_Flujo.MenorOIgualQue:
                        return (x.Numero <= y.Numero);

                    case TipoOpcion_CondicionTextosInformacion_Flujo.MenorQue:
                        return (x.Numero < y.Numero);

                    case TipoOpcion_CondicionTextosInformacion_Flujo.NoContiene:
                        return (!x.Numero.ToString().Trim().ToLower().Contains(y.Numero.ToString().Trim().ToLower()));

                    case TipoOpcion_CondicionTextosInformacion_Flujo.TerminaCon:
                        return (x.Numero.ToString().Trim().ToLower().EndsWith(y.Numero.ToString().Trim().ToLower()));

                }

                return false;
            }

            public int GetHashCode(EntidadNumero obj)
            {
                return obj.GetHashCode();
            }
        }

        public class ComparadorTextosInformacion_Flujo
        {
            public TipoOpcion_CondicionTextosInformacion_Flujo TipoOpcionCondicion_TextosInformacion { get; set; }
            public List<string> TextosInformacionInvolucrados { get; set; }
            public List<string> TextosCumplenCondicion { get; set; }
            public List<string> TextosNoCumplenCondicion { get; set; }
            public ComparadorTextosInformacion_Flujo(TipoOpcion_CondicionTextosInformacion_Flujo tipoOpcion)
            {
                TipoOpcionCondicion_TextosInformacion = tipoOpcion;
                TextosInformacionInvolucrados = new List<string>();
                TextosCumplenCondicion = new List<string>();
                TextosNoCumplenCondicion = new List<string>();
            }
            private bool Comparar(string x, string y)
            {
                switch (TipoOpcionCondicion_TextosInformacion)
                {
                    case TipoOpcion_CondicionTextosInformacion_Flujo.Contiene:
                    case TipoOpcion_CondicionTextosInformacion_Flujo.NoContiene:
                        return x.Replace("\t", string.Empty).Trim().ToLower().Contains(y.Replace("\t", string.Empty).Trim().ToLower());

                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsParteDe:
                        return y.Replace("\t", string.Empty).Trim().ToLower().Contains(x.Replace("\t", string.Empty).Trim().ToLower());

                    case TipoOpcion_CondicionTextosInformacion_Flujo.EmpiezaCon:
                        return x.Replace("\t", string.Empty).Trim().ToLower().StartsWith(y.Replace("\t", string.Empty).Trim().ToLower());

                    case TipoOpcion_CondicionTextosInformacion_Flujo.TerminaCon:
                        return x.Replace("\t", string.Empty).Trim().ToLower().EndsWith(y.Replace("\t", string.Empty).Trim().ToLower());

                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsDistintoA:
                    case TipoOpcion_CondicionTextosInformacion_Flujo.EsIgualA:
                        return x.Replace("\t", string.Empty).Trim().ToLower().Equals(y.Replace("\t", string.Empty).Trim().ToLower());

                    case TipoOpcion_CondicionTextosInformacion_Flujo.MayorOIgualQue:
                        if (string.Compare(x.Replace("\t", string.Empty).Trim().ToLower(), y.Replace("\t", string.Empty).Trim().ToLower()) >= 0)
                            return true;
                        else
                            return false;

                    case TipoOpcion_CondicionTextosInformacion_Flujo.MayorQue:
                        if (string.Compare(x.Replace("\t", string.Empty).Trim().ToLower(), y.Replace("\t", string.Empty).Trim().ToLower()) > 0)
                            return true;
                        else
                            return false;

                    case TipoOpcion_CondicionTextosInformacion_Flujo.MenorOIgualQue:
                        if (string.Compare(x.Replace("\t", string.Empty).Trim().ToLower(), y.Replace("\t", string.Empty).Trim().ToLower()) <= 0)
                            return true;
                        else
                            return false;

                    case TipoOpcion_CondicionTextosInformacion_Flujo.MenorQue:
                        if (string.Compare(x.Replace("\t", string.Empty).Trim().ToLower(), y.Replace("\t", string.Empty).Trim().ToLower()) < 0)
                            return true;
                        else
                            return false;

                }

                return false;
            }

            public bool Interseccion(IEnumerable<string> lista1, IEnumerable<string> lista2)
            {
                foreach (var item1 in lista1)
                {
                    foreach (var item2 in lista2)
                    {
                        if (Comparar(item1, item2))
                            return true;
                    }
                }

                return false;
            }

            public int ContarInterseccion(IEnumerable<string> lista1, IEnumerable<string> lista2)
            {
                int contarIguales = 0;
                TextosCumplenCondicion.Clear();
                TextosNoCumplenCondicion.Clear();
                
                foreach (var item1 in lista1)
                {
                    foreach (var item2 in lista2)
                    {
                        if (Comparar(item1, item2))
                        {
                            TextosInformacionInvolucrados.Add(item1); //+ "|" + TipoOpcionCondicion_TextosInformacion.ToString() + "|");
                            contarIguales++;

                            if (!TextosCumplenCondicion.Contains(item1))
                                TextosCumplenCondicion.Add(item1);
                        }
                        else
                        {
                            if (!TextosNoCumplenCondicion.Contains(item1))
                                TextosNoCumplenCondicion.Add(item1);
                        }
                    }
                }

                return contarIguales;
            }
        }

        public void QuitarCondicionSubElementoDiseñoCondicion_Condiciones(DiseñoElementoOperacion elemento)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.QuitarCondicionSubElementoDiseñoCondicion_Condiciones(elemento);
            }

            if (OperandoSubElemento_Condicion == elemento)
            {
                //if (CondicionContenedora != null)
                //    CondicionContenedora.Condiciones.Remove(this);
                OperandoSubElemento_Condicion = null;
            }

            if (SubOperandos_AplicarCondiciones.Contains(elemento))
                SubOperandos_AplicarCondiciones.Remove(elemento);
        }

        public void QuitarCondicionElementoDiseñoCondicion_Condiciones(DiseñoOperacion elemento)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.QuitarCondicionElementoDiseñoCondicion_Condiciones(elemento);
            }

            if (ElementoOperacion_Valores == elemento)
            {
                //if (CondicionContenedora != null)
                //    CondicionContenedora.Condiciones.Remove(this);
                ElementoOperacion_Valores = null;
            }

            if (Operandos_AplicarCondiciones.Contains(elemento))
                Operandos_AplicarCondiciones.Remove(elemento);

            if(OperandoCondicion == elemento)
            {
                //if (CondicionContenedora != null)
                //    CondicionContenedora.Condiciones.Remove(this);
                OperandoCondicion = null;
            }
        }

        public void AgregarCondicionElementoDiseñoCondicion_Condiciones(ref List<DiseñoOperacion> elementos)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.AgregarCondicionElementoDiseñoCondicion_Condiciones(ref elementos);
            }

            if(ElementoOperacion_Valores != null)
                elementos.Add(ElementoOperacion_Valores);

            //if (Operandos_AplicarCondiciones.Contains(elemento))
            //    Operandos_AplicarCondiciones.Remove(elemento);

            if(OperandoCondicion != null)
                elementos.Add(OperandoCondicion);
        }

        public void AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref List<DiseñoElementoOperacion> elementos)
        {
            foreach (var itemCondicion in Condiciones)
            {
                itemCondicion.AgregarCondicionSubElementoDiseñoCondicion_Condiciones(ref elementos);
            }

            if (OperandoSubElemento_Condicion != null)
            {
                elementos.Add(OperandoSubElemento_Condicion);
            }

            //if (SubOperandos_AplicarCondiciones.Contains(elemento))
            //    SubOperandos_AplicarCondiciones.Remove(elemento);
        }
    }

    public class InformacionCantidadesTextosInformacion_CondicionFlujo
    {
        public int CantidadTextosCondicion { get; set; }
        public int CantidadTextosValoresCondicion { get; set; }
        public int TextosCumplenCondicion_Elemento { get; set; }
        public int TextosNoCumplenCondicion_Elemento { get; set; }

        public int TextosCumplenCondicion_Valores { get; set; }
        public int TextosNoCumplenCondicion_Valores { get; set; }
    }
}
