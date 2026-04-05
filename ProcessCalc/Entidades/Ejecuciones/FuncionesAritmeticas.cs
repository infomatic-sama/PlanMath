using ProcessCalc.Controles;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Windows.Gaming.Input;
using static ProcessCalc.Entidades.ElementoDiseñoOperacionAritmeticaEjecucion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        public double Factorial(double numero, List<string> log)
        {
            try
            {
                if (numero < 0)
                {
                    log.Add("El número es negativo y no se calcula el factorial de " + numero.ToString("N") + ".");
                    return double.NaN;
                }

                if(double.Round(numero) != numero)
                {
                    
                    log.Add("El número " + numero.ToString("N") + " se ha truncado a " + double.Round(numero).ToString("N") + ", quitando sus decimales para calcular su factorial.");
                    numero = double.Round(numero);
                }

                double resultado = 1;

                for(int indice = (int)numero; indice >= 1; indice--)
                {
                    resultado *= indice;
                }

                return resultado;

            }
            catch (Exception e)
            {
                try { Thread.Sleep(3000); } catch (Exception) { };
                //ConError = true;

                log.Add(e.Message);

                //Thread.CurrentThread.Interrupt();
                //try { Thread.Sleep(Timeout.Infinite); } catch (Exception) { };

                return double.NaN;
            }
        }
        
        private int ObtenerCantidadMayorOperandos_Filas(ElementoOperacionAritmeticaEjecucion operacion)
        {
            int CantidadNumeros = 0;

            List<ElementoOperacionAritmeticaEjecucion> elementosConjuntos = (from E in operacion.ElementosOperacion
                                                                 where (E.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) ||
                                                                 E.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ||
                                                                 (E.GetType() == typeof(ElementoEntradaEjecucion) &&
                                                                 ((ElementoEntradaEjecucion)E).TipoEntrada == TipoEntrada.ConjuntoNumeros))
                                                                 select E).ToList();
            if (elementosConjuntos.Count > 0)
            {
                int CantidadMayor = CantidadNumeros;

                foreach (var itemOperacionPosicion in elementosConjuntos)
                    itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;

                foreach (var itemOperacionNumero in Calculo.TextosInformacion.ElementosTextosInformacion.Where(i =>
                                                i.GetType() == typeof(DiseñoListaCadenasTexto)))
                    ((DiseñoListaCadenasTexto)itemOperacionNumero).PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;

                foreach (var itemElemento in elementosConjuntos)
                {
                    Clasificador clasificadorActual = null;

                    if (itemElemento.IndicePosicionClasificadores >= 0 &&
                        itemElemento.IndicePosicionClasificadores <= itemElemento.Clasificadores_Cantidades.Count - 1)
                        clasificadorActual = itemElemento.Clasificadores_Cantidades[itemElemento.IndicePosicionClasificadores];

                    int cantidad = 0;
                    
                    cantidad = itemElemento.Numeros.Count(i =>
                        
                                                            ((!i.Clasificadores_SeleccionarOrdenar.Any()) ||
                                                            (i.Clasificadores_SeleccionarOrdenar.Contains(clasificadorActual))) &&
                    (!itemElemento.NumerosFiltrados.Any() || (itemElemento.NumerosFiltrados.Any() &
                    itemElemento.NumerosFiltrados.Contains(i))));
                    

                    itemElemento.CantidadNumeros_Clasificador = cantidad;

                    if (cantidad > CantidadMayor)
                        CantidadMayor = cantidad;
                                           
                    foreach (var itemOperacionPosicion in elementosConjuntos)
                        itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;

                    foreach (var itemOperacionNumero in Calculo.TextosInformacion.ElementosTextosInformacion.Where(i =>
                                                i.GetType() == typeof(DiseñoListaCadenasTexto)))
                        ((DiseñoListaCadenasTexto)itemOperacionNumero).PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                }

                CantidadNumeros = CantidadMayor;
            }

            return CantidadNumeros;
        }
    }
}