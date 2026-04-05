using PlanMath_para_Excel;
using PlanMath_para_Word;
using ProcessCalc.Controles;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.SubcalculosYArchivosExternos;
using ProcessCalc.Ventanas;
using ProcessCalc.Ventanas.Seleccionar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms.Integration;
using System.Windows.Media.TextFormatting;
using static ProcessCalc.Entidades.ElementoDiseñoOperacionAritmeticaEjecucion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {        
        public bool VerificarTextoInformacion(string texto, List<string> textosInformacion)
        {
            return textosInformacion.Where((i) => i != null && i.ToLower().Trim().Equals(texto.ToLower().Trim())).Any();
        }

        public bool VerificarTextoInformacion(List<string> textos, List<string> textosInformacion, int cantidadTextosInformacion_SeleccionarOrdenar)
        {            
            int cantidadTextosEncontrados = 0;

            foreach (var texto in textos)
            {
                //if (textosInformacion.Any((i) => i != null && CondicionTextosInformacion.VerificarTextoInformacionCondicion(i, texto))) //i.ToLower().Trim().Replace("\t", "").Equals(texto.ToLower().Trim().Replace("\t", ""))))
                if (textosInformacion.Any((i) => i != null && i.ToLower().Trim().Replace("\t", "").Equals(texto.ToLower().Trim().Replace("\t", ""))))
                    cantidadTextosEncontrados++;
            }

            if (cantidadTextosEncontrados == cantidadTextosInformacion_SeleccionarOrdenar)
                return true;
            else
                return false;            
        }
        public bool VerificarTextoInformacion(AsignacionImplicacion_TextosInformacion asignacion,
            ElementoOperacionAritmeticaEjecucion operacionCondicionEjecucion,
            ElementoDiseñoOperacionAritmeticaEjecucion operacionInternaCondicionEjecucion,
            ElementoOperacionAritmeticaEjecucion elementoOperando,
            EntidadNumero numeroOperando,
            List<EntidadNumero> NumerosResultado,
            int PosicionActualImplicacion,
            int PosicionActualInstanciaImplicacion,
            int PosicionActualIteracionImplicacion)
        {
            asignacion.PosicionesTextos_CumplenCondicion.Clear();

            if (asignacion.Condiciones_TextoCondicion != null)
            {
                asignacion.Condiciones_TextoCondicion.PosicionActualImplicacion = PosicionActualImplicacion;
                asignacion.Condiciones_TextoCondicion.PosicionActualInstanciaImplicacion = PosicionActualInstanciaImplicacion;
                asignacion.Condiciones_TextoCondicion.PosicionActualIteracionImplicacion = PosicionActualIteracionImplicacion;

                if (asignacion.Condiciones_TextoCondicion.EvaluarCondiciones(asignacion,
                        this,
                        operacionCondicionEjecucion,
                        operacionInternaCondicionEjecucion,
                        elementoOperando,
                        numeroOperando,
                        NumerosResultado
                        ) && (
                        (numeroOperando != null && numeroOperando.ConsiderarOperandoCondicion_SiCumple)))
                {
                    return true;
                }
            }
            else
                return true;

            return false;
        }

        public List<EntidadNumero> OrdenarNumeros_Elemento(List<EntidadNumero> lista, 
            TipoOpcion_OrdenamientoNumerosSalidas Tipo_OrdenamientoNumerosSalidas,
            bool OrdenamientoSalidasAscendente, bool OrdenamientoSalidasDescendente,
            List<OrdenacionNumeros> Ordenaciones, bool RevertirLista)
        {
            if (!lista.Any()) return lista;

            List<EntidadNumero> listaOrdenada = lista;

            if (RevertirLista)
                listaOrdenada.Reverse();

            int cantidadMaximaTextos = 0;
            int indiceCantidadTextos = 2;

            OrdenacionNumeros OrdenacionActual = null;
            var ListaOrdenaciones = Ordenaciones.GetEnumerator();

            bool UnaIteracion = true;
            bool UltimaIteracon = false;


            foreach (var item in lista)
                item.indiceOrdenamiento = -1;

            bool OrdenarTextosInformacionCantidades_Ejecucion = false;

            if (Ordenaciones.Any())
            {
                UnaIteracion = false;
                ListaOrdenaciones.MoveNext();
                OrdenacionActual = ListaOrdenaciones.Current;

                if (OrdenacionActual != null)
                {
                    cantidadMaximaTextos = OrdenacionActual.CantidadDivisionTextosInformacion;
                }
            }
            else
            {
                cantidadMaximaTextos = (from E in lista select E.Textos.Count).Max();
            }

            do
            {
                bool CadenaTextoDividida = false;

                if (OrdenacionActual != null)
                {
                    CadenaTextoDividida = OrdenacionActual.CadenaTextoDividida;
                }

                if (CadenaTextoDividida)
                {
                    foreach (var item in listaOrdenada)
                        item.DivisionesCadena_Ordenamiento.Clear();
                }

            switch (Tipo_OrdenamientoNumerosSalidas)
            {
                case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                    if (OrdenamientoSalidasAscendente)
                    {
                            if (CadenaTextoDividida)
                            {
                                foreach (var item in listaOrdenada)
                                    OrdenacionActual.ObtenerDivisionesCadena(item, item.Nombre);

                                listaOrdenada = OrdenacionActual.OrdenarPorDivisiones(listaOrdenada).ToList();
                            }
                            else
                                listaOrdenada = listaOrdenada.OrderBy(i => i.Nombre).ToList();
                    }
                    else if (OrdenamientoSalidasDescendente)
                    {
                            if (CadenaTextoDividida)
                            {
                                foreach (var item in listaOrdenada)
                                    OrdenacionActual.ObtenerDivisionesCadena(item, item.Nombre);

                                listaOrdenada = OrdenacionActual.OrdenarPorDivisiones_Descending(listaOrdenada).ToList();
                            }
                            else
                                listaOrdenada = listaOrdenada.OrderByDescending(i => i.Nombre).ToList();
                    }

                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:

                        if (OrdenamientoSalidasAscendente)
                        {
                            if (CadenaTextoDividida)
                            {
                                foreach (var item in listaOrdenada)
                                {
                                    foreach (var itemTexto in item.Textos)
                                        OrdenacionActual.ObtenerDivisionesCadena(item, itemTexto);
                                }

                                var listaOrdenamiento = OrdenacionActual.OrdenarPorDivisiones(listaOrdenada);

                                listaOrdenada = OrdenacionActual.OrdenarPorDivisiones_CantidadMaxima(listaOrdenamiento,
                                    2, cantidadMaximaTextos).ToList();
                            }
                            else
                            {
                                var listaOrdenamiento = listaOrdenada.OrderBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (OrdenarTextosInformacionCantidades_Ejecucion)
                                            i.OrdenarTextosInformacion(OrdenacionActual);

                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                });

                                for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                                {
                                    listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                    {
                                        if (i.Textos.Any())
                                        {
                                            if (OrdenarTextosInformacionCantidades_Ejecucion)
                                                i.OrdenarTextosInformacion(OrdenacionActual);

                                            if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                            {
                                                i.indiceOrdenamiento++;
                                                return i.Textos[i.indiceOrdenamiento];
                                            }
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    });
                                }

                                listaOrdenada = listaOrdenamiento.ToList();
                            }
                        }
                        else if (OrdenamientoSalidasDescendente)
                        {
                            if (CadenaTextoDividida)
                            {
                                foreach (var item in listaOrdenada)
                                {
                                    foreach (var itemTexto in item.Textos)
                                        OrdenacionActual.ObtenerDivisionesCadena(item, itemTexto);
                                }

                                var listaOrdenamiento = OrdenacionActual.OrdenarPorDivisiones_Descending(listaOrdenada);

                                listaOrdenada = OrdenacionActual.OrdenarPorDivisiones_CantidadMaxima_Descending(listaOrdenamiento,
                                    indiceCantidadTextos, cantidadMaximaTextos).ToList();
                            }
                            else
                            {
                                var listaOrdenamiento = listaOrdenada.OrderByDescending(i =>
                        {
                            if (i.Textos.Any())
                            {
                                if (OrdenarTextosInformacionCantidades_Ejecucion)
                                    i.OrdenarTextosInformacion(OrdenacionActual);

                                i.indiceOrdenamiento++;
                                return i.Textos[i.indiceOrdenamiento];
                            }
                            else
                                return null;
                        });

                                for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                                {
                                    listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                    {
                                        if (i.Textos.Any())
                                        {
                                            if (OrdenarTextosInformacionCantidades_Ejecucion)
                                                i.OrdenarTextosInformacion(OrdenacionActual);

                                            if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                            {
                                                i.indiceOrdenamiento++;
                                                return i.Textos[i.indiceOrdenamiento];
                                            }
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    });
                                }

                                listaOrdenada = listaOrdenamiento.ToList();
                            }
                        }

                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:

                        if (OrdenamientoSalidasAscendente)
                        {
                            if (CadenaTextoDividida)
                            {
                                foreach (var item in listaOrdenada)
                                    OrdenacionActual.ObtenerDivisionesCadena(item, item.Nombre);

                                foreach (var item in listaOrdenada)
                                {
                                    foreach (var itemTexto in item.Textos)
                                        OrdenacionActual.ObtenerDivisionesCadena(item, itemTexto);
                                }

                                var listaOrdenamiento = OrdenacionActual.OrdenarPorDivisiones(listaOrdenada);

                                listaOrdenada = OrdenacionActual.OrdenarPorDivisiones_CantidadMaxima(listaOrdenamiento,
                                    indiceCantidadTextos, cantidadMaximaTextos).ToList();
                            }
                            else
                            {
                                var listaOrdenamiento = listaOrdenada.OrderBy(i =>
                        {
                            return i.Nombre;
                        });

                                for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                                {
                                    listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                    {
                                        if (i.Textos.Any())
                                        {
                                            if (OrdenarTextosInformacionCantidades_Ejecucion)
                                                i.OrdenarTextosInformacion(OrdenacionActual);

                                            if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                            {
                                                i.indiceOrdenamiento++;
                                                return i.Textos[i.indiceOrdenamiento];
                                            }
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    });
                                }

                                listaOrdenada = listaOrdenamiento.ToList();
                            }
                        }
                        else if (OrdenamientoSalidasDescendente)
                        {
                            if (CadenaTextoDividida)
                            {
                                foreach (var item in listaOrdenada)
                                    OrdenacionActual.ObtenerDivisionesCadena(item, item.Nombre);

                                foreach (var item in listaOrdenada)
                                {
                                    foreach (var itemTexto in item.Textos)
                                        OrdenacionActual.ObtenerDivisionesCadena(item, itemTexto);
                                }

                                var listaOrdenamiento = OrdenacionActual.OrdenarPorDivisiones_Descending(listaOrdenada);

                                listaOrdenada = OrdenacionActual.OrdenarPorDivisiones_CantidadMaxima_Descending(listaOrdenamiento,
                                    indiceCantidadTextos, cantidadMaximaTextos).ToList();
                            }
                            else
                            {
                                var listaOrdenamiento = listaOrdenada.OrderByDescending(i =>
                            {
                                return i.Nombre;
                            });

                                for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                                {
                                    listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                    {
                                        if (i.Textos.Any())
                                        {
                                            if (OrdenarTextosInformacionCantidades_Ejecucion)
                                                i.OrdenarTextosInformacion(OrdenacionActual);

                                            if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                            {
                                                i.indiceOrdenamiento++;
                                                return i.Textos[i.indiceOrdenamiento];
                                            }
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    });
                                }

                                listaOrdenada = listaOrdenamiento.ToList();
                            }
                        }

                    break;
            }

                if (!UnaIteracion)
                {
                    if (!UltimaIteracon)
                    {
                        OrdenamientoSalidasAscendente = OrdenacionActual.OrdenarNumerosDeMenorAMayor;
                        OrdenamientoSalidasDescendente = OrdenacionActual.OrdenarNumerosDeMayorAMenor;
                        OrdenarTextosInformacionCantidades_Ejecucion = OrdenacionActual.OrdenarTextosInformacionCantidades_Ejecucion;

                        indiceCantidadTextos = cantidadMaximaTextos + 1;
                        cantidadMaximaTextos = OrdenacionActual.CantidadDivisionTextosInformacion;

                        ListaOrdenaciones.MoveNext();
                        OrdenacionActual = ListaOrdenaciones.Current;

                        if (OrdenacionActual == null)
                        {
                            UltimaIteracon = true;
                        }
                    }
                    else
                        UnaIteracion = true;
                }
            } while (!UnaIteracion && OrdenacionActual != null);

            return listaOrdenada;
        }
        
        public void EstablecerElementoSalida_Operacion(EntidadNumero elemento, ElementoDiseñoOperacionAritmeticaEjecucion elementoOperacion)
        {
            List<ElementoEjecucionCalculo> elementos = new List<ElementoEjecucionCalculo>();
            foreach (var itemEtapa in etapas)
            {
                elementos.AddRange(itemEtapa.Elementos);
            }

            var elementoEjecucionSalida = (from E in elementos where E.ElementoDiseñoRelacionado == elementoOperacion.ElementoDiseñoRelacionado.ElementoSalidaOperacion_Agrupamiento select E).FirstOrDefault();

            if (elementoEjecucionSalida != null)
            {
                elemento.ElementosSalidaOperacion_Agrupamiento.Add(elementoEjecucionSalida);

                if (elementoOperacion.ElementoDiseñoRelacionado.ElementoInternoSalidaOperacion_Agrupamiento != null)
                {
                    var elementoInterno = ObtenerSubElementoEjecucion(elementoOperacion.ElementoDiseñoRelacionado.ElementoInternoSalidaOperacion_Agrupamiento);
                    elemento.ElementosInternosSalidaOperacion_Agrupamiento.Add(elementoInterno);
                }
            }
        }
        private void EstablecerElementosSalida_CondicionFlujo(EntidadNumero elemento,
            ElementoDiseñoOperacionAritmeticaEjecucion operacion, CondicionFlujo condicion, bool condicionesCumplen,
            ElementoOperacionAritmeticaEjecucion operacionContenedora, List<EntidadNumero> ListaNumeros)
        {
            List<DiseñoElementoOperacion> elementosSalida;

            if (condicion != null)
            {
                if (condicionesCumplen)
                {
                    elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesCondicionFlujo_ElementosSalida
                                       where E.Condiciones == condicion
                                       select E.ElementoSalida).ToList();
                }
                else
                {
                    elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesCondicionFlujo_ElementosSalida2
                                       where E.Condiciones == condicion
                                       select E.ElementoSalida).ToList();
                }
            }
            else
            {
                elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.ElementosPosteriores select E).ToList();
            }

            if (elementosSalida != null)
            {
                List<ElementoDiseñoOperacionAritmeticaEjecucion> elementos = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
                foreach (var itemEtapa in operacionContenedora.Etapas)
                {
                    elementos.AddRange(itemEtapa.Elementos);
                }

                foreach (var elementoSalida in elementosSalida)
                {
                    var elementoEjecucionSalida = (from E in elementos where E.ElementoDiseñoRelacionado == elementoSalida select E).FirstOrDefault();

                    if (elementoEjecucionSalida != null)
                    {
                        var listaElementosSalida = from E in ListaNumeros.Where(i => i.HashCode_NumeroAgregacion_Ejecucion == elemento.HashCode_NumeroAgregacion_Ejecucion) select E.ElementosSalidaOperacion_CondicionFlujo;
                        
                        bool encontrado = false;
                        foreach (var lista in listaElementosSalida)
                        {
                            if (lista.Contains(elementoEjecucionSalida))
                            {
                                encontrado = true;
                                break;
                            }
                        }

                        if(!encontrado)
                            elemento.ElementosSalidaOperacion_CondicionFlujo.Add(elementoEjecucionSalida);

                        //if (elementoEjecucionSalida.TipoElemento == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                        //    elementoEjecucionSalida.TipoElemento == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados)
                        //{
                        //    ((ElementoDiseñoOperacionAritmeticaEjecucion)elementoEjecucionSalida).CantidadTextosInformacion_SeleccionarOrdenar = operacion.CantidadTextosInformacion_SeleccionarOrdenar;
                        //}

                        //foreach (var itemTexto in textosInformacion)
                        //    elementoEjecucionSalida.TextoInformacionAnterior_SeleccionOrdenamiento.Add(new DuplaTextoInformacion_Cantidad_SeleccionarOrdenar { ObjetoCantidad = elemento, TextoInformacion = itemTexto });

                        //if (elementoEjecucionSalida.Tipo == TipoElementoOperacionEjecucion.OpcionOperacion &&
                        //    elementoEjecucionSalida.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) &&
                        //    ((ElementoDiseñoOperacionAritmeticaEjecucion)elementoEjecucionSalida).TipoElemento == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                        //{
                        //    foreach (var itemElemento in elementoEjecucionSalida.ElementoDiseñoRelacionado.ElementosPosteriores)
                        //    {
                        //        var elementoEncontrado = (from E in elementos where E.ElementoDiseñoRelacionado == itemElemento select E).FirstOrDefault();
                        //        elemento.ElementosSalidaOperacion_SeleccionarOrdenar.Add(elementoEncontrado);

                        //        if (elementoEncontrado.TipoElemento == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                        //    elementoEncontrado.TipoElemento == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados)
                        //        {
                        //            ((ElementoDiseñoOperacionAritmeticaEjecucion)elementoEncontrado).CantidadTextosInformacion_SeleccionarOrdenar = operacion.CantidadTextosInformacion_SeleccionarOrdenar;
                        //        }

                        //        foreach (var itemTexto in textosInformacion)
                        //            elementoEncontrado.TextoInformacionAnterior_SeleccionOrdenamiento.Add(new DuplaTextoInformacion_Cantidad_SeleccionarOrdenar { ObjetoCantidad = elemento, TextoInformacion = itemTexto });
                        //    }
                        //}
                    }
                }
            }
        }

        private void EstablecerElementosSalida_CondicionFlujo(EntidadNumero elemento,
            ElementoOperacionAritmeticaEjecucion operacion, CondicionFlujo condicion, bool condicionesCumplen,
            ElementoOperacionAritmeticaEjecucion operacionContenedora, List<EntidadNumero> ListaNumeros)
        {
            if (operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion))
            {
                EstablecerElementosSalida_CondicionFlujo(elemento, 
                    (ElementoDiseñoOperacionAritmeticaEjecucion)operacion, condicion, condicionesCumplen,
                    operacionContenedora, ListaNumeros);
            }
            else
            {
                List<DiseñoOperacion> elementosSalida;

                if (condicion != null)
                {
                    if (condicionesCumplen)
                    {
                        elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesCondicionesFlujo_ElementosSalida
                                           where E.Condiciones == condicion
                                           select E.ElementoSalida_Operacion).ToList();
                    }
                    else
                    {
                        elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesCondicionesFlujo_ElementosSalida
                                           where E.Condiciones == condicion
                                           select E.ElementoSalida_Operacion).ToList();
                    }
                }
                else
                {
                    elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.ElementosPosteriores select E).ToList();
                }

                if (elementosSalida != null)
                {
                    List<ElementoEjecucionCalculo> elementos = new List<ElementoEjecucionCalculo>();
                    foreach (var itemEtapa in etapas)
                    {
                        elementos.AddRange(itemEtapa.Elementos);
                    }

                    foreach (var elementoSalida in elementosSalida)
                    {
                        var elementoEjecucionSalida = (from E in elementos where E.ElementoDiseñoRelacionado == elementoSalida select E).FirstOrDefault();

                        if (elementoEjecucionSalida != null)
                        {
                            var listaElementosSalida = from E in ListaNumeros.Where(i => i.HashCode_NumeroAgregacion_Ejecucion == elemento.HashCode_NumeroAgregacion_Ejecucion) select E.ElementosSalidaOperacion_CondicionFlujo;

                            bool encontrado = false;
                            foreach (var lista in listaElementosSalida)
                            {
                                if (lista.Contains(elementoEjecucionSalida))
                                {
                                    encontrado = true;
                                    break;
                                }
                            }

                            if (!encontrado)
                                elemento.ElementosSalidaOperacion_CondicionFlujo.Add(elementoEjecucionSalida);

                        }
                    }
                }
            }
        }
        private void ProcesarOperacionSeleccionarEntradas(ElementoOperacionAritmeticaEjecucion operacion,
            ref string strMensajeLogResultados, ref string strMensajeLog, ref string strOperando,
            ElementoCalculoEjecucion itemCalculo)
        {
            switch (operacion.TipoOperacion)
            {
                case TipoOperacionAritmeticaEjecucion.SeleccionarEntradas:
                    strMensajeLogResultados += "seleccionaron variables o vectores de entradas";
                    strMensajeLog += "seleccionaron variables o vectores de entradas";
                    strOperando = "Seleccionando variables o vectores de entradas";

                    strMensajeLogResultados += " de los siguientes variables o vectores:\n";

                    strMensajeLog += " de los siguientes variables o vectores:\n";
                    OperarTodosNumerosJuntosOperacion_SeleccionarEntradas(operacion, ref strMensajeLog, ref strMensajeLogResultados, itemCalculo);
                    
                    break;
            }
        }

        private void OperarTodosNumerosJuntosOperacion_SeleccionarEntradas(ElementoOperacionAritmeticaEjecucion operacion, ref string strMensajeLog, ref string strMensajeLogResultados, ElementoCalculoEjecucion itemCalculo)
        {
            var OperandosOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.Entrada |
            j.Tipo == TipoElementoOperacion.SeleccionarEntradas).Select(i => ObtenerElementoEjecucion(i)).ToList();
            
            foreach (var entrada in OperandosOperacion)
            {
                if(entrada.GetType() == typeof(ElementoEntradaEjecucion))
                {
                    ((ElementoEntradaEjecucion)entrada).EntradaNoSeleccionada = false;
                    ((ElementoEntradaEjecucion)entrada).EntradaProcesadaSeleccion = false;
                }
                else
                {
                    entrada.SeleccionEntradasNoSeleccionada = false;
                    entrada.SeleccionEntradasProcesadaSeleccion = false;
                }
            }

            if (operacion.SeleccionEntradasNoSeleccionada)
            {
                foreach (var entrada in OperandosOperacion)
                {
                    if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                    {
                        ((ElementoEntradaEjecucion)entrada).EntradaNoSeleccionada = true;
                        ((ElementoEntradaEjecucion)entrada).EntradaProcesadaSeleccion = true;
                    }
                    else
                    {
                        entrada.SeleccionEntradasNoSeleccionada = true;
                        entrada.SeleccionEntradasProcesadaSeleccion = true;
                    }
                }

                if (ModoToolTips && TooltipsCalculo != null)
                {
                    TooltipsCalculo.EstablecerDatosTooltip_Elemento_EntradaNoSeleccionada(operacion.ElementoDiseñoRelacionado, TipoOpcionToolTip.Operacion, false);
                    TooltipsCalculo.EstablecerDatosTooltip_Elemento_SeleccionEntradas(operacion.ElementoDiseñoRelacionado, null, TipoOpcionToolTip.Operacion, false);
                }

                return;
            }

            if (ModoToolTips && TooltipsCalculo != null)
            {
                TooltipsCalculo.EstablecerDatosTooltip_Elemento_EntradaNoSeleccionada(operacion.ElementoDiseñoRelacionado, TipoOpcionToolTip.Operacion, true);
                TooltipsCalculo.EstablecerDatosTooltip_Elemento_SeleccionEntradas(operacion.ElementoDiseñoRelacionado, null, TipoOpcionToolTip.Operacion, true);
            }

            DefinirNombresAntes_Ejecucion(operacion);

            if (!ModoToolTips)
            {
                var AsociacionesElementosEntradas = operacion.ElementoDiseñoRelacionado.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Where(i => i.ModoManual).ToList();

                if (operacion.ElementoDiseñoRelacionado.SeleccionManualEntradas &&
                    AsociacionesElementosEntradas.Any())
                {
                    operacion.CantidadNumeros_Ejecucion = 0;

                    SeleccionManualOperaciones_CondicionEntradas seleccionar = new SeleccionManualOperaciones_CondicionEntradas();
                    seleccionar.descripcionCondiciones.Text = "Operador de selección de variables o vectores de entrada: " + operacion.ElementoDiseñoRelacionado.NombreCombo +
                        ". Selecciona las variables o vectores de entradas próximas a ejecutar:";
                    seleccionar.titulo.Text = "Seleccionar próximas variables o vectores de entradas para continuar\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                    seleccionar.Entradas.AddRange(operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(i => AsociacionesElementosEntradas.Any(j => j.ElementoSalida_Operacion == i)).ToList());

                    List<ElementoEntradaEjecucion> entradasSeleccionadas = new List<ElementoEntradaEjecucion>();
                    List<ElementoEjecucionCalculo> seleccionesEntradasSeleccionadas = new List<ElementoEjecucionCalculo>();

                    bool digita = (bool)seleccionar.ShowDialog();
                    if (digita == true)
                    {
                        if (seleccionar.Entradas.Any())
                        {
                            List<ElementoEjecucionCalculo> elementos = new List<ElementoEjecucionCalculo>();

                            foreach (var itemEtapa in etapas)
                            {
                                elementos.AddRange(itemEtapa.Elementos);
                            }

                            foreach (var elementoSalida in seleccionar.Entradas)
                            {
                                var elementoEjecucionSalida = (from E in elementos
                                                               where E.ElementoDiseñoRelacionado == elementoSalida &&
                                                               E.Tipo == TipoElementoEjecucion.Entrada
                                                               select (ElementoEntradaEjecucion)E).FirstOrDefault();

                                if (elementoEjecucionSalida != null)
                                {
                                    entradasSeleccionadas.Add(elementoEjecucionSalida);
                                }
                                else
                                {
                                    var elementoEjecucionSalida2 = (from E in elementos
                                                                    where E.ElementoDiseñoRelacionado == elementoSalida &&
                                                                E.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.SeleccionarEntradas
                                                                    select (ElementoEjecucionCalculo)E).FirstOrDefault();

                                    if (elementoEjecucionSalida2 != null)
                                    {
                                        seleccionesEntradasSeleccionadas.Add(elementoEjecucionSalida2);
                                    }
                                }
                            }

                            var EntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.Entrada).Select(i => ObtenerElementoEjecucion(i)).ToList();
                            List<string> nombreEntradas = new List<string>();

                            foreach (var entrada in EntradasOperacion)
                            {
                                if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                                {
                                    if (seleccionar.Entradas.Contains(entrada.ElementoDiseñoRelacionado))
                                    {
                                        if (!entradasSeleccionadas.Contains(entrada))
                                            ((ElementoEntradaEjecucion)entrada).EntradaNoSeleccionada = true;
                                        else
                                            nombreEntradas.Add(entrada.Nombre);

                                        ((ElementoEntradaEjecucion)entrada).EntradaProcesadaSeleccion = true;
                                    }
                                }
                            }

                            var SeleccionesEntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.SeleccionarEntradas).Select(i => (ElementoEjecucionCalculo)ObtenerElementoEjecucion(i)).ToList();
                            List<string> nombreSeleccionesEntradas = new List<string>();

                            foreach (var entrada in SeleccionesEntradasOperacion)
                            {
                                if (seleccionar.Entradas.Contains(entrada.ElementoDiseñoRelacionado))
                                {
                                    if (!seleccionesEntradasSeleccionadas.Contains(entrada))
                                        entrada.SeleccionEntradasNoSeleccionada = true;
                                    else
                                        nombreSeleccionesEntradas.Add(entrada.Nombre);

                                    entrada.SeleccionEntradasProcesadaSeleccion = true;
                                }
                            }

                            strMensajeLogResultados += "las variables o vectores de entrada siguientes son seleccionadas en el modo manual:\n";
                            strMensajeLog += "las variables o vectores de entrada siguientes son seleccionadas en el modo manual:\n";

                            foreach (var itemSalida in nombreEntradas)
                            {
                                strMensajeLogResultados += itemSalida + "\n";
                                strMensajeLog += itemSalida + "\n";
                            }

                            strMensajeLogResultados += "los operadores de selección de variables o vectores de entrada siguientes son seleccionados en el modo manual:\n";
                            strMensajeLog += "los operadores de selección de variables o vectores de entrada siguientes son seleccionados en el modo manual:\n";

                            foreach (var itemSalida in nombreSeleccionesEntradas)
                            {
                                strMensajeLogResultados += itemSalida + "\n";
                                strMensajeLog += itemSalida + "\n";
                            }
                        }
                        else
                        {
                            strMensajeLogResultados += "Ninguna variable o vector de entrada seleccionada en el modo manual.\n";
                            strMensajeLog += "Ninguna variable o vector de entrada seleccionada en el modo manual.\n";

                            strMensajeLogResultados += "Ninguno operador de selección de variables o vectores de entrada seleccionado en el modo manual.\n";
                            strMensajeLog += "Ninguno operador de selección de variables o vectores de entrada seleccionado en el modo manual.\n";

                            var EntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.Entrada).Select(i => ObtenerElementoEjecucion(i)).ToList();

                            foreach (var entrada in EntradasOperacion)
                            {
                                if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                                {
                                    if (seleccionar.Entradas.Contains(entrada.ElementoDiseñoRelacionado))
                                    {
                                        ((ElementoEntradaEjecucion)entrada).EntradaNoSeleccionada = true;
                                        ((ElementoEntradaEjecucion)entrada).EntradaProcesadaSeleccion = true;
                                    }
                                }
                            }

                            var SeleccionesEntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.SeleccionarEntradas).Select(i => (ElementoEjecucionCalculo)ObtenerElementoEjecucion(i)).ToList();

                            foreach (var entrada in SeleccionesEntradasOperacion)
                            {
                                if (seleccionar.Entradas.Contains(entrada.ElementoDiseñoRelacionado))
                                {
                                    entrada.SeleccionEntradasNoSeleccionada = true;
                                    entrada.SeleccionEntradasProcesadaSeleccion = true;
                                }
                            }
                        }
                    }
                    else if (digita == false)
                    {
                        Detener();
                        return;
                    }

                    DefinirNombresDespues_Ejecucion(operacion);
                }
                else
                {
                    strMensajeLogResultados += "Ninguna variable o vector de entrada seleccionada en el modo manual.\n";
                    strMensajeLog += "Ninguna variable o vector de entrada seleccionada en el modo manual.\n";

                    strMensajeLogResultados += "Ninguno operador de selección de variables o vectores de entrada seleccionado en el modo manual.\n";
                    strMensajeLog += "Ninguno operador de selección de variables o vectores de entrada seleccionado en el modo manual.\n";

                    var EntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.Entrada).Select(i => ObtenerElementoEjecucion(i)).ToList();

                    foreach (var entrada in EntradasOperacion)
                    {
                        if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                        {
                            if (operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(i => AsociacionesElementosEntradas.Any(j => j.ElementoSalida_Operacion == i)).ToList().Contains(entrada.ElementoDiseñoRelacionado))
                            {
                                ((ElementoEntradaEjecucion)entrada).EntradaNoSeleccionada = true;
                                ((ElementoEntradaEjecucion)entrada).EntradaProcesadaSeleccion = true;
                            }
                        }
                    }

                    var SeleccionesEntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.SeleccionarEntradas).Select(i => (ElementoEjecucionCalculo)ObtenerElementoEjecucion(i)).ToList();

                    foreach (var entrada in SeleccionesEntradasOperacion)
                    {
                        if (operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(i => AsociacionesElementosEntradas.Any(j => j.ElementoSalida_Operacion == i)).ToList().Contains(entrada.ElementoDiseñoRelacionado))
                        {
                            entrada.SeleccionEntradasNoSeleccionada = true;
                            entrada.SeleccionEntradasProcesadaSeleccion = true;
                        }
                    }
                }
            }
            
            if (ModoToolTips || (operacion.ElementoDiseñoRelacionado.SeleccionCondicionesEntradas &&                 
                operacion.ElementoDiseñoRelacionado.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Any(i => !i.ModoManual)))
            {
                List<ElementoEjecucionCalculo> elementos = new List<ElementoEjecucionCalculo>();
                
                foreach (var itemEtapa in etapas)
                {
                    elementos.AddRange(itemEtapa.Elementos);
                }

                List<ElementoEntradaEjecucion> entradasSeleccionadas = new List<ElementoEntradaEjecucion>();
                List<ElementoEjecucionCalculo> seleccionesEntradasSeleccionadas = new List<ElementoEjecucionCalculo>();

                var EntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.Entrada |
                j.Tipo == TipoElementoOperacion.SeleccionarEntradas).Select(i => ObtenerElementoEjecucion(i)).ToList();

                if (!ModoToolTips || TooltipsCalculo == null)
                {
                    foreach (var condiciones in operacion.CondicionesTextosInformacion_SeleccionEntradas)
                    {
                        operacion.CantidadNumeros_Ejecucion = 0;

                        foreach (var itemOperacionPosicion in EntradasOperacion)
                        {
                            if (itemOperacionPosicion.GetType() == typeof(ElementoEntradaEjecucion))
                            {
                                itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                                
                                if (condiciones.EvaluarCondiciones(this, operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operacion
                                    : null, operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operacion
                                    : null, (ElementoEntradaEjecucion)itemOperacionPosicion, null))
                                {
                                    foreach (var elementoSalida in operacion.ElementoDiseñoRelacionado.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Where(i => !i.ModoManual
                                    && i.Condiciones == condiciones).Select(j => j.ElementoSalida_Operacion))
                                    {
                                        var elementoEjecucionSalida = (from E in elementos
                                                                       where E.ElementoDiseñoRelacionado == elementoSalida &&
                                                                       E.Tipo == TipoElementoEjecucion.Entrada
                                                                       //E.GetType() == typeof(ElementoEntradaEjecucion)
                                                                       select (ElementoEntradaEjecucion)E).FirstOrDefault();

                                        if (elementoEjecucionSalida != null)
                                        {
                                            entradasSeleccionadas.Add(elementoEjecucionSalida);
                                        }
                                        else
                                        {
                                            var elementoEjecucionSalida2 = (from E in elementos
                                                                            where E.ElementoDiseñoRelacionado == elementoSalida &&
                                                                        E.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.SeleccionarEntradas
                                                                            select (ElementoEjecucionCalculo)E).FirstOrDefault();

                                            if (elementoEjecucionSalida2 != null)
                                            {
                                                seleccionesEntradasSeleccionadas.Add(elementoEjecucionSalida2);
                                            }
                                        }
                                    }
                                }
                            }

                        }                    
                    }                    
                }
                else
                {
                    foreach (var itemOperacionPosicion in EntradasOperacion)
                    {
                        if (TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, itemOperacionPosicion.ElementoDiseñoRelacionado.ID) != null &&
                            //TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, itemOperacionPosicion.ElementoDiseñoRelacionado.ID).EntradaRelacionada != null &&
                            TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, operacion.ElementoDiseñoRelacionado.ID) != null &&
                            TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, operacion.ElementoDiseñoRelacionado.ID).EntradasSeleccionadas_ToolTips.Contains(
                            TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, itemOperacionPosicion.ElementoDiseñoRelacionado.ID)))
                        {
                            {
                                if (itemOperacionPosicion.Tipo == TipoElementoEjecucion.Entrada)
                                {
                                    entradasSeleccionadas.Add((ElementoEntradaEjecucion)itemOperacionPosicion);
                                }
                                else if(itemOperacionPosicion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                                {
                                    seleccionesEntradasSeleccionadas.Add(itemOperacionPosicion);
                                }
                            }
                        }
                    }
                }

                List<string> nombreEntradas = new List<string>();

                foreach (var entrada in EntradasOperacion.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) && !((ElementoEntradaEjecucion)i).EntradaProcesadaSeleccion))
                {
                    if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                    {
                        if (!entradasSeleccionadas.Contains(entrada))
                            ((ElementoEntradaEjecucion)entrada).EntradaNoSeleccionada = true;
                        else
                            nombreEntradas.Add(entrada.Nombre);

                        ((ElementoEntradaEjecucion)entrada).EntradaProcesadaSeleccion = true;
                    }
                }

                var SeleccionesEntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.SeleccionarEntradas).Select(i => (ElementoEjecucionCalculo)ObtenerElementoEjecucion(i)).ToList();
                List<string> nombreSeleccionesEntradas = new List<string>();

                foreach (var entrada in SeleccionesEntradasOperacion.Where(i => !i.SeleccionEntradasProcesadaSeleccion))
                {
                    if (!seleccionesEntradasSeleccionadas.Contains(entrada))
                        entrada.SeleccionEntradasNoSeleccionada = true;
                    else
                        nombreSeleccionesEntradas.Add(entrada.Nombre);

                    entrada.SeleccionEntradasProcesadaSeleccion = true;
                }

                if (entradasSeleccionadas.Any())
                {
                    strMensajeLogResultados += "las variables o vectores de entrada siguientes son seleccionadas en el modo automático:\n";
                    strMensajeLog += "las variables o vectores de entrada siguientes son seleccionadas en el modo automático:\n";

                    foreach (var itemSalida in nombreEntradas)
                    {
                        strMensajeLogResultados += itemSalida + "\n";
                        strMensajeLog += itemSalida + "\n";
                    }
                }
                else
                {
                    strMensajeLogResultados += "Ninguna variable o vector de entrada seleccionada en el modo automático.\n";
                    strMensajeLog += "Ninguna variable o vector de entrada seleccionada en el modo automático.\n";
                }

                if (seleccionesEntradasSeleccionadas.Any())
                {
                    strMensajeLogResultados += "los operadores de selección de variables o vectores de entrada siguientes son seleccionados en el modo automático:\n";
                    strMensajeLog += "los operadores de selección de variables o vectores de entrada siguientes son seleccionados en el modo automático:\n";

                    foreach (var itemSalida in nombreEntradas)
                    {
                        strMensajeLogResultados += itemSalida + "\n";
                        strMensajeLog += itemSalida + "\n";
                    }
                }
                else
                {
                    strMensajeLogResultados += "\"Ninguno operador de selección de variables o vectores de entrada seleccionado en el modo automático.\n";
                    strMensajeLog += "\"Ninguno operador de selección de variables o vectores de entrada seleccionado en el modo automático.\n";
                }
            }
            else
            {
                strMensajeLogResultados += "Ninguna variable o vector de entrada seleccionada en el modo automático.\n";
                strMensajeLog += "Ninguna variable o vector de entrada seleccionada en el modo automático.\n";

                strMensajeLogResultados += "Ninguno operador de selección de variables o vectores de entrada seleccionado en el modo automático.\n";
                strMensajeLog += "Ninguno operador de selección de variables o vectores de entrada seleccionado en el modo automático.\n";

                var EntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.Entrada).Select(i => ObtenerElementoEjecucion(i)).ToList();

                foreach (var entrada in EntradasOperacion.Where(i => i.GetType() == typeof(ElementoEntradaEjecucion) && !((ElementoEntradaEjecucion)i).EntradaProcesadaSeleccion))
                {
                    if (entrada.GetType() == typeof(ElementoEntradaEjecucion))
                    {
                        ((ElementoEntradaEjecucion)entrada).EntradaNoSeleccionada = true;
                        ((ElementoEntradaEjecucion)entrada).EntradaProcesadaSeleccion = true;
                    }
                }

                var SeleccionesEntradasOperacion = operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(j => j.Tipo == TipoElementoOperacion.SeleccionarEntradas).Select(i => (ElementoEjecucionCalculo)ObtenerElementoEjecucion(i)).ToList();

                foreach (var entrada in SeleccionesEntradasOperacion.Where(i => !i.SeleccionEntradasProcesadaSeleccion))
                {
                    entrada.SeleccionEntradasNoSeleccionada = true;
                    entrada.SeleccionEntradasProcesadaSeleccion = true;
                }
            }
        }
                
        private void ProcesarEspera(ElementoOperacionAritmeticaEjecucion operacion,
            ref string strMensajeLogResultados, ref string strMensajeLog, ElementoCalculoEjecucion itemCalculo,
            bool mostrarLog)
        {
            List<EntidadNumero> NumerosResultado = new List<EntidadNumero>();

            if (mostrarLog)
            {
                log.Add("Esperando datos (" + operacion.Nombre + ")...");
                strMensajeLogResultados += "Esperando datos (" + operacion.Nombre + ")...";
            }

            DefinirNombresAntes_Ejecucion(operacion);

            bool terminarVerificaciones = false;
            int cantidadVerificaciones = 0;

            while (!terminarVerificaciones)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                if (operacion.ElementoDiseñoRelacionado.CantidadEsperas_Fijas)
                {
                    if (cantidadVerificaciones == operacion.ElementoDiseñoRelacionado.CantidadVerificaciones)
                    {
                        terminarVerificaciones = true;
                        continue;
                    }
                }
                else
                {
                    if (cantidadVerificaciones > 0)
                    {
                        foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            switch (itemOperacion.Tipo)
                            {
                                case TipoElementoEjecucion.Entrada:
                                    ElementoEntradaEjecucion itemEntrada_Ejecucion = (ElementoEntradaEjecucion)itemOperacion;

                                    itemEntrada_Ejecucion.Numeros.Clear();
                                    itemEntrada_Ejecucion.Clasificadores_Cantidades.Clear();

                                    itemOperacion.Estado = EstadoEjecucion.Ninguno;
                                    ProcesarItemEntrada(itemOperacion, itemCalculo, false, true);
                                    break;
                            }
                        }
                    }

                    bool resultadoCondicionesTextos = true;
                    bool resultadoCondicionesCantidades = true;

                    var condiciones = operacion.ElementoDiseñoRelacionado.CondicionesTextosInformacion_Espera;

                    if(condiciones != null)
                    {
                        List<EntidadNumero> Numeros_Condicion = new List<EntidadNumero>();

                        InicializarVariables_Operacion(operacion);

                        foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            //operacion.InicializarPosicionesClasificadores_Operandos();

                            foreach (var itemClasificador in itemOperacion.Clasificadores_Cantidades)
                            {
                                if (Pausada) Pausar();
                                if (Detenida) return;

                                if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                                //this.itemClasificador = itemClasificador;

                                bool operandoConNumeros = true;
                                bool operandoConCondicionesAplicadas = false;

                                operandoConCondicionesAplicadas = true;

                                int indiceLista = 0;

                                for (indiceLista = 0; indiceLista <= itemOperacion.Numeros.Count(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)) - 1; indiceLista++)
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    EntidadNumero subItem_itemEntrada = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList()[indiceLista];

                                    if ((!subItem_itemEntrada.ElementosSalidaOperacion_Agrupamiento.Any()) ||
(subItem_itemEntrada.ElementosSalidaOperacion_Agrupamiento.Contains(operacion)))
                                    {
                                        if (!(condiciones.EvaluarCondiciones(this, operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operacion
                                : null, operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operacion
                                : null, null, null)))
                                        {
                                            resultadoCondicionesTextos = false;
                                        }

                                        condiciones.TextosInformacionInvolucrados.Clear();

                                        foreach (var itemOperacionPosicion in operacion.ElementosOperacion)
                                            if (itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar < itemOperacionPosicion.TotalElementos_CondicionesOperador_SeleccionarOrdenar - 1)
                                                itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar++;

                                        foreach (var itemOperacionPosicion in Calculo.TextosInformacion.ElementosTextosInformacion.Where(i =>
                                                i.GetType() == typeof(DiseñoListaCadenasTexto)))
                                            if (((DiseñoListaCadenasTexto)itemOperacionPosicion).PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar < 
                                                ((DiseñoListaCadenasTexto)itemOperacionPosicion).ListasCadenasTexto.Count - 1)
                                                ((DiseñoListaCadenasTexto)itemOperacionPosicion).PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar++;
                                    }

                                }

                                itemOperacion.NumerosElementosFiltrados_Condiciones.Clear();

                                if (!itemOperacion.Numeros.Any()) operandoConNumeros = false;

                                if (operandoConNumeros && !operandoConCondicionesAplicadas) operandoConNumeros = false;

                                if (!operandoConNumeros &&
                                    (condiciones.VerificarOperando(itemOperacion.ElementoDiseñoRelacionado) ||
                                    condiciones.VerificarOperandoValores(itemOperacion.ElementoDiseñoRelacionado)))
                                    resultadoCondicionesTextos = false;

                                InicializarVariables_Operacion(operacion);
                                //operacion.AumentarPosicionesClasificadores_Operandos();
                            }
                        }


                    }
                    else
                    {
                        if (!operacion.ElementoDiseñoRelacionado.VerificarCondiciones_Hasta)
                            resultadoCondicionesTextos = false;
                    }

                    var condicion = operacion.ElementoDiseñoRelacionado.CondicionesCantidades_Espera;

                    if(condicion != null)
                    {
                        bool condicionCumple = false;

                        if (operacion.ElementosOperacion.Any())
                        {
                            foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                            {
                                if (Pausada) Pausar();
                                if (Detenida) return;

                                //operacion.InicializarPosicionesClasificadores_Operandos();

                                foreach (var itemClasificador in itemOperacion.Clasificadores_Cantidades)
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                                    //this.itemClasificador = itemClasificador;

                                    condicion.ClasificadorActual = itemClasificador;

                                    if (condicion == null || (condicion != null &&
                                    condicion.EvaluarCondiciones(this, operacion, null, null)))
                                    {
                                        condicionCumple = true;
                                    }

                                    InicializarVariables_Operacion(operacion);
                                    //operacion.AumentarPosicionesClasificadores_Operandos();
                                }
                            }
                        }
                        else
                        {
                            if (condicion == null || (condicion != null &&
                                                        condicion.EvaluarCondiciones(this, operacion, null, null)))
                            {
                                condicionCumple = true;
                            }
                        }

                        if (!condicionCumple)
                            resultadoCondicionesCantidades = false;
                    }
                    else
                    {
                        if (!operacion.ElementoDiseñoRelacionado.VerificarCondiciones_Hasta)
                            resultadoCondicionesCantidades = false;
                    }

                    if (operacion.ElementoDiseñoRelacionado.VerificarCondiciones_Hasta)
                    {
                        if (resultadoCondicionesTextos &&
                            resultadoCondicionesCantidades)
                        {
                            terminarVerificaciones = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (!resultadoCondicionesTextos &&
                            !resultadoCondicionesCantidades)
                        {
                            terminarVerificaciones = true;
                            continue;
                        }
                    }
                }

                cantidadVerificaciones++;

                if (mostrarLog)
                {
                    log.Add("Verificando/esperando " + cantidadVerificaciones.ToString() + "...");
                    strMensajeLogResultados += "Verificando/esperando " + cantidadVerificaciones.ToString() + "...";
                }

                double tiempo = operacion.ElementoDiseñoRelacionado.TiempoEspera;

                if (tiempo > 0)
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoTiempoEspera != TipoTiempoEspera.Segundos)
                    {
                        tiempo = tiempo * 60;
                        if (operacion.ElementoDiseñoRelacionado.TipoTiempoEspera != TipoTiempoEspera.Minutos)
                        {
                            tiempo = tiempo * 60;
                            if (operacion.ElementoDiseñoRelacionado.TipoTiempoEspera != TipoTiempoEspera.Horas)
                            {
                                tiempo = tiempo * 24;
                            }
                        }
                    }

                    while(tiempo > 0)
                    {
                        
                        tiempo--;

                        if (Pausada) Pausar();
                        if (Detenida) return;
                    }
                }
            }

            strMensajeLogResultados += "Traspasando todas las cantidades de los operandos";
            strMensajeLog += "Traspasando todas las cantidades de los operandos";

            OperarSeleccionNumerosOperacion(operacion, ref strMensajeLog, ref strMensajeLogResultados, false, itemCalculo);
        }

    }
}