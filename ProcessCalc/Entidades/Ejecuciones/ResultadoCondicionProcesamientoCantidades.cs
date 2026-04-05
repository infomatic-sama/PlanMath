using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Ejecuciones
{
    public class ResultadoCondicionProcesamientoCantidades
    {
        [IgnoreDataMember]
        public bool QuitarCantidad_Procesamiento_Operandos;
        [IgnoreDataMember]
        public bool QuitarCantidad_Procesamiento_Resultados;
        [IgnoreDataMember]
        public bool QuitarElemento_Procesamiento_Operandos;
        [IgnoreDataMember]
        public bool QuitarElemento_Procesamiento_Resultados;
        [IgnoreDataMember]
        public bool InsertarCantidad_Procesamiento_Operandos;
        [IgnoreDataMember]
        public bool InsertarCantidad_Procesamiento_Resultados;
        [IgnoreDataMember]
        public bool InsertarElemento_Procesamiento_Operandos;
        [IgnoreDataMember]
        public bool InsertarElemento_Procesamiento_Resultados;
        [IgnoreDataMember]
        public bool InsertarElemento_Procesamiento_ValorFijo;
        [IgnoreDataMember]
        public TipoOpcionElementoAccion_InsertarProcesamientoCantidades InsertarElemento_Procesamiento_Cantidad;
        [IgnoreDataMember]
        public double InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
        [IgnoreDataMember]
        public TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades InsertarUbicacion_Procesamiento_Cantidad;
        [IgnoreDataMember]
        public double InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
        [IgnoreDataMember]
        public double InsertarElemento_Procesamiento_Cantidad_ValorFijo;
        [IgnoreDataMember]
        public List<DuplaOperacion_AlInsertar_ProcesamientoCantidades> OperacionesAlInsertar_ProcesamientoCantidades { get; set; }
        [IgnoreDataMember]
        public bool NoInsertarCantidad_EnPosicion;
        [IgnoreDataMember]
        public List<DiseñoOperacion> OperandosInsertar_CantidadesProcesamientoCantidades { get; set; }
        [IgnoreDataMember]
        public List<DiseñoElementoOperacion> SubOperandosInsertar_CantidadesProcesamientoCantidades { get; set; }
        [IgnoreDataMember]
        public EntidadNumero NumeroInsertado_Asociado {  get; set; }
        [IgnoreDataMember]
        public EntidadNumero subItem_InsertarOperando_Asociado { get; set; }
        [IgnoreDataMember]
        public bool Desplazamiento_PosicionAnterior;
        [IgnoreDataMember]
        public bool Desplazamiento_PosicionPosterior;
        [IgnoreDataMember]
        public string ID {  get; set; }
        public ResultadoCondicionProcesamientoCantidades()
        {
            OperacionesAlInsertar_ProcesamientoCantidades = new List<DuplaOperacion_AlInsertar_ProcesamientoCantidades>();
            OperandosInsertar_CantidadesProcesamientoCantidades = new List<DiseñoOperacion>();
            SubOperandosInsertar_CantidadesProcesamientoCantidades = new List<DiseñoElementoOperacion>();
            OperacionesAlInsertar_ProcesamientoCantidades = new List<DuplaOperacion_AlInsertar_ProcesamientoCantidades>();
        }

        public double Operar_ProcesamientoCantidades(double cantidadOperando, List<EntidadNumero> NumerosResultado, ElementoOperacionAritmeticaEjecucion operando) //, double operando, double resultado)
        {
            double cantidad = 0;
            cantidad = cantidadOperando;

            foreach (var dupla in OperacionesAlInsertar_ProcesamientoCantidades)
            {
                cantidad = Operar_ProcesamientoCantidades_Duplas(dupla, cantidad, NumerosResultado.Select(i => i.Numero).ToList(), operando);
            }

            return cantidad;
        }

        private double Operar_ProcesamientoCantidades_Duplas(DuplaOperacion_AlInsertar_ProcesamientoCantidades dupla, double cantidadOperando,
            List<double> NumerosResultado, ElementoOperacionAritmeticaEjecucion operando) //, double operando, double resultado)
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

                    default:
                        List<double> NumerosOperandos_Operacion = new List<double>();

                        switch (dupla.TipoElementoAOperar_OperacionAlInsertar)
                        {
                            case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                                foreach (var itemOperando in dupla.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar)
                                    NumerosOperandos_Operacion.AddRange(operando.ObtenerNumerosOperando(itemOperando, dupla.TipoElemento_OperacionAlInsertar,
                                        dupla.ValorPosicion_TipoElemento_OperacionAlInsertar).Select(i => i.Numero).ToList());
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                                foreach (var itemOperando in dupla.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar)
                                    NumerosOperandos_Operacion.AddRange(operando.ObtenerNumerosOperando(itemOperando, dupla.TipoElemento_OperacionAlInsertar,
                                        dupla.ValorPosicion_TipoElemento_OperacionAlInsertar).Select(i => i.Numero).ToList());

                                NumerosOperandos_Operacion.AddRange(NumerosResultado);
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                                NumerosOperandos_Operacion.AddRange(NumerosResultado);
                                break;

                            case TipoOpcionElementoAccionProcesamientoCantidades.ValorFijo:
                                var numerosCadena = dupla.ValorFijo_OperacionAlInsertar.Split("|");
                                List<double> numerosFijos = new List<double>();

                                foreach (var itemNumeroCadena in numerosCadena)
                                {
                                    double numero = 0;
                                    if (double.TryParse(itemNumeroCadena, out numero))
                                    {
                                        numerosFijos.Add(numero);
                                    }
                                }

                                NumerosOperandos_Operacion.AddRange(numerosFijos);
                                break;
                        }

                        cantidad = numeroOperando.Numero;
                        foreach (var itemNumero in NumerosOperandos_Operacion)
                        {
                            switch (dupla.Operacion_AlInsertar)
                            {
                                case TipoOperacion_AlInsertar_ProcesamientoCantidades.Raiz:
                                    cantidad = Math.Pow(cantidad, 1 / itemNumero);
                                    break;

                                case TipoOperacion_AlInsertar_ProcesamientoCantidades.Division:
                                    cantidad = cantidad / itemNumero;
                                    break;

                                case TipoOperacion_AlInsertar_ProcesamientoCantidades.Suma:
                                    cantidad = cantidad + itemNumero;
                                    break;

                                case TipoOperacion_AlInsertar_ProcesamientoCantidades.Multiplicacion:
                                    cantidad = cantidad * itemNumero;
                                    break;

                                case TipoOperacion_AlInsertar_ProcesamientoCantidades.Logaritmo:
                                    cantidad = Math.Log(cantidad, itemNumero);
                                    break;

                                case TipoOperacion_AlInsertar_ProcesamientoCantidades.Porcentaje:
                                    cantidad = (cantidad / 100) * itemNumero;
                                    break;

                                case TipoOperacion_AlInsertar_ProcesamientoCantidades.Potencia:
                                    cantidad = Math.Pow(cantidad, itemNumero);
                                    break;

                                case TipoOperacion_AlInsertar_ProcesamientoCantidades.Resta:
                                    cantidad = cantidad - itemNumero;
                                    break;
                            }
                        }

                        break;
                }
            }
            else
            {
                cantidad = cantidadOperando;
            }

            return cantidad;
        }
    }

    public class ResultadoCondicionProcesamientoCantidades_Filas
    {
        [IgnoreDataMember]
        public bool FlagInsertarCantidad_Procesamiento_Operandos { get; set; }
        [IgnoreDataMember]
        public bool FlagInsertarCantidad_Procesamiento_Resultados { get; set; }
        [IgnoreDataMember]
        public bool FlagInsertarElemento_Procesamiento_Operandos { get; set; }
        [IgnoreDataMember]
        public bool FlagInsertarElemento_Procesamiento_Resultados { get; set; }
        [IgnoreDataMember]
        public bool FlagQuitarCantidad_Procesamiento_Operandos { get; set; }
        public bool FlagInsertarElemento_Procesamiento_ValorFijo { get; set; }
        [IgnoreDataMember]
        public bool FlagNoInsertarCantidad_EnPosicion { get; set; }
        [IgnoreDataMember]
        public string IDResultadoCondicionProcesamientoCantidades_Asociado { get; set; }
    }
}
