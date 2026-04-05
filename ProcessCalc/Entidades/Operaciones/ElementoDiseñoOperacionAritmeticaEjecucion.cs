using ProcessCalc.Controles;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public class ElementoDiseñoOperacionAritmeticaEjecucion : ElementoOperacionAritmeticaEjecucion
    {
        public TipoElementoOperacionEjecucion TipoInterno { get; set; }
        public TipoOpcionOperacion TipoElemento { get; set; }
        public new DiseñoElementoOperacion ElementoDiseñoRelacionado { get; set; }
        public new List<ElementoDiseñoOperacionAritmeticaEjecucion> ElementosOperacion { get; set; }
        public int HashCode_ElementoDiseñoOperacion { get; set; }
        public bool ContieneSalida { get; set; }
        public ElementoEntradaEjecucion EntradaEjecucion { get; set; }
        public ElementoOperacionAritmeticaEjecucion OperacionEjecucion { get; set; }    
        
        public ElementoDiseñoOperacionAritmeticaEjecucion()
        {
            Numeros = new List<EntidadNumero>();
            ElementosOperacion = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionTextosInformacion>();
            Relaciones_TextosInformacion = new List<AsignacionImplicacion_TextosInformacion>();
            Textos = new List<string>();
            TextoInformacionAnterior_SeleccionOrdenamiento = new List<DuplaTextoInformacion_Cantidad_SeleccionarOrdenar>();
            CondicionesFlujo_SeleccionOrdenamiento = new List<CondicionFlujo>();
            NumerosFiltrados_Condiciones = new List<EntidadNumero>();
            TextosInformacionInvolucrados_CondicionSeleccionarOrdenar = new List<string>();
            NumerosFiltrados = new List<EntidadNumero>();            
            Clasificadores_Cantidades = new List<Clasificador>();
        }

        public class DuplaTextoInformacion_Cantidad_SeleccionarOrdenar
        {
            public object ObjetoCantidad { get; set; }
            public string TextoInformacion { get; set; }
        }

        private double Operar_ProcesamientoCantidades_Duplas(DuplaOperacion_AlInsertar_ProcesamientoCantidades dupla, double cantidadOperando) //, double operando, double resultado)
        {
            double cantidad = 0;

            if (dupla.AlInsertar_Operar)
            {
                EntidadNumero numeroOperando = new EntidadNumero();
                numeroOperando.Numero = cantidadOperando;

                switch (dupla.Operacion_AlInsertar)
                {
                    case TipoOperacion_AlInsertar_ProcesamientoCantidades.InversoSumaResta:
                        cantidad = cantidadOperando * (double)(-1);
                        break;

                    case TipoOperacion_AlInsertar_ProcesamientoCantidades.InversoMultiplicacionDivision:
                        cantidad = (double)(1) / cantidadOperando;
                        break;

                    case TipoOperacion_AlInsertar_ProcesamientoCantidades.RedondearCantidades:                        
                        numeroOperando.Redondear(dupla.ConfigRedondeo);
                        cantidad = numeroOperando.Numero;
                        break;
                }
            }
            else
            {
                cantidad = cantidadOperando;
            }

            return cantidad;
        }
               

        public List<EntidadNumero> Numeros_Filtrados(ElementoDiseñoOperacionAritmeticaEjecucion operacion)
        {
            return Numeros.Where(i => (
                                    ((!i.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
            (i.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion))) &&
            ((!i.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
            (i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))))).ToList();
        }
        
    }

    public class DuplaOperacion_AlInsertar_ProcesamientoCantidades
    {
        [IgnoreDataMember]
        public bool AlInsertar_Operar { get; set; }
        [IgnoreDataMember]
        public TipoOperacion_AlInsertar_ProcesamientoCantidades Operacion_AlInsertar { get; set; }
        [IgnoreDataMember]
        public ConfiguracionRedondearCantidades ConfigRedondeo { get; set; }
        [IgnoreDataMember]
        public TipoOpcionElementoAccionProcesamientoCantidades TipoElementoAOperar_OperacionAlInsertar { get; set; }
        [IgnoreDataMember]
        public List<DiseñoOperacion> OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar { get; set; }
        [IgnoreDataMember]
        public string ValorFijo_OperacionAlInsertar { get; set; }
        [IgnoreDataMember]
        public TipoOpcionElementoAccion_InsertarProcesamientoCantidades TipoElemento_OperacionAlInsertar { get; set; }
        [IgnoreDataMember]
        public double ValorPosicion_TipoElemento_OperacionAlInsertar { get; set; }
    }
}
