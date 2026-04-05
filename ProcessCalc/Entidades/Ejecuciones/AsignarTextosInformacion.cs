using ProcessCalc.Controles;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using Windows.Media.Devices.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        public void AsignarTextosInformacion_ElementosOperacion(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion elementoOperando,            
            EntidadNumero numeroElementoOperando,
            EntidadNumero numeroElementoResultado,
            List<EntidadNumero> NumerosResultado,
            bool EstablecerNombre,
            bool operacionInterna)
        {
            RecorrerInstanciasImplicaciones(operacion,
                elementoOperando,
                numeroElementoOperando,
                numeroElementoResultado,
                NumerosResultado,
                EstablecerNombre,
                operacionInterna);
        }

        private void RecorrerInstanciasImplicaciones(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion elementoOperando,
            EntidadNumero numeroElementoOperando,
            EntidadNumero numeroElementoResultado,
            List<EntidadNumero> NumerosResultado,
            bool EstablecerNombre,
            bool operacionInterna)
        {
            bool nombresSeteados = false;

            List<string> TextosInformacionAgregadosCantidadActual_Resultado_Operandos = new List<string>();
            int PosicionActualImplicacion = 0;
            int PosicionActualIteracionImplicacion = 0;

            List<string> TextosPreviosNumero_Operando = new List<string>();
            List<string> TextosPreviosNumero_Resultado = new List<string>();

            TextosPreviosNumero_Operando.AddRange(numeroElementoOperando.Textos);
            TextosPreviosNumero_Resultado.AddRange(numeroElementoResultado.Textos);

            foreach(var itemAsignacion in operacion.Relaciones_TextosInformacion)
                itemAsignacion.TextosInformacionImplicacion_CumplenCondicion.Clear();

            foreach (var definicionTextoInformacion in operacion.Relaciones_TextosInformacion)
            {
                bool cumpleAlgunaCondicion = false;

                int PosicionActualInstanciaImplicacion = 0;
                foreach (var itemInstancia in definicionTextoInformacion.InstanciasAsignacion)
                {
                    definicionTextoInformacion.AsignarACantidades = false;
                    definicionTextoInformacion.Cantidades_Implicacion_CumplenCondicion.Clear();
                    definicionTextoInformacion.OperandosCantidades_Implicacion_CumplenCondicion.Clear();

                    if ((itemInstancia.Operandos_AsignarTextosInformacionCuando.Any() && itemInstancia.Operandos_AsignarTextosInformacionCuando.Any(item => item == elementoOperando.ElementoDiseñoRelacionado |
                    (elementoOperando.ElementoDiseñoRelacionado.EntradaRelacionada != null && item == elementoOperando.ElementoDiseñoRelacionado.EntradaRelacionada.ElementoSalidaCalculoAnterior))) ||
                        ((itemInstancia.ConsiderarSoloOperandos_CumplanCondiciones || definicionTextoInformacion.Condiciones_TextoCondicion == null) &&
                        ((definicionTextoInformacion.Condiciones_TextoCondicion != null && definicionTextoInformacion.Condiciones_TextoCondicion.VerificarOperando(elementoOperando.ElementoDiseñoRelacionado)) ||
                        definicionTextoInformacion.Condiciones_TextoCondicion == null)) ||
                        (!itemInstancia.ConsiderarSoloOperandos_CumplanCondiciones || definicionTextoInformacion.Condiciones_TextoCondicion == null))
                    {

                        List<string> TextosInformacionAgregadosInstancias_Resultado_Operandos =
                        definicionTextoInformacion.ObtenerConservarTextosInformacion_Operandos(elementoOperando, numeroElementoOperando);

                        if (TextosInformacionAgregadosInstancias_Resultado_Operandos == null)
                        {
                            TextosInformacionAgregadosInstancias_Resultado_Operandos = new List<string>();
                            definicionTextoInformacion.AgregarConservarTextosInformacion_Operandos(TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                elementoOperando, numeroElementoOperando);
                        }

                        {
                            if (definicionTextoInformacion.Condiciones_TextoCondicion != null)
                            {
                                definicionTextoInformacion.Condiciones_TextoCondicion.NumerosVinculados_CondicionAnterior.Clear();
                                definicionTextoInformacion.Condiciones_TextoCondicion.OperandosVinculados_CondicionAnterior.Clear();
                            }

                            bool cumpleCondicionesNumeros = VerificarTextoInformacion(definicionTextoInformacion,
                                operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operacion : null,
                                operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operacion : null,
                                elementoOperando, numeroElementoOperando, NumerosResultado, PosicionActualImplicacion, PosicionActualInstanciaImplicacion, PosicionActualIteracionImplicacion);

                            //if(!cumpleCondicionesNumeros)
                            //{
                            //    cumpleCondicionesNumeros = VerificarTextoInformacion(definicionTextoInformacion,
                            //    operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operacion : null,
                            //    operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operacion : null,
                            //    elementoOperando, numeroElementoResultado, NumerosResultado);
                            //}

                            if (cumpleCondicionesNumeros)
                            {
                                bool asignarACantidad = false;

                                if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Any())
                                {
                                    var operandoAsignarDesde = itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.FirstOrDefault(i => i == elementoOperando.ElementoDiseñoRelacionado | i == operacion.ElementoDiseñoRelacionado);

                                    if (operandoAsignarDesde != null)
                                    {
                                        var condiciones = itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.Operando == operandoAsignarDesde)?.Condiciones;
                                        if (condiciones != null)
                                        {
                                            condiciones.TextosInformacionImplicacion_Instancia = TextosInformacionAgregadosInstancias_Resultado_Operandos;
                                            condiciones.TextosInformacionImplicacion_Condicion = definicionTextoInformacion.TextosInformacionAgregadosInstancias_Resultado;
                                            condiciones.TextosInformacionImplicacion_Operacion = operacion.TextosInformacionAgregadosInstancias_Resultado;
                                            condiciones.TextosInformacionImplicacion_CumplenCondicion = definicionTextoInformacion.TextosInformacionImplicacion_CumplenCondicion;

                                            if (definicionTextoInformacion.Condiciones_TextoCondicion != null &&
                                                condiciones.Condiciones.Any())
                                            {
                                                condiciones.Condiciones.FirstOrDefault().NumerosVinculados_CondicionAnterior.Clear();
                                                condiciones.Condiciones.FirstOrDefault().OperandosVinculados_CondicionAnterior.Clear();

                                                condiciones.Condiciones.FirstOrDefault().NumerosVinculados_CondicionAnterior.AddRange(definicionTextoInformacion.Condiciones_TextoCondicion.NumerosVinculados_CondicionAnterior_Total);
                                                condiciones.Condiciones.FirstOrDefault().OperandosVinculados_CondicionAnterior.AddRange(definicionTextoInformacion.Condiciones_TextoCondicion.OperandosVinculados_CondicionAnterior_Total);
                                            }

                                            condiciones.TextosInformacionInvolucrados.Clear();

                                            if (operacion.ElementoDiseñoRelacionado == operandoAsignarDesde)
                                            {
                                                operacion.Numeros.AddRange(NumerosResultado);
                                            }

                                            if (condiciones.EvaluarCondiciones(this, operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operacion : null,
                                                    operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operacion : null,
                                                    operandoAsignarDesde == elementoOperando.ElementoDiseñoRelacionado ? elementoOperando : operacion,
                                                    operandoAsignarDesde == elementoOperando.ElementoDiseñoRelacionado ? numeroElementoOperando :
                                                    numeroElementoResultado))
                                            {
                                                asignarACantidad = true;

                                                definicionTextoInformacion.AsignarACantidades = true;
                                                definicionTextoInformacion.Cantidades_Implicacion_CumplenCondicion.AddRange(condiciones.NumerosVinculados_CondicionAnterior_Total.Distinct());
                                                definicionTextoInformacion.OperandosCantidades_Implicacion_CumplenCondicion.AddRange(condiciones.OperandosVinculados_CondicionAnterior_Total.Distinct());
                                            }

                                            if (operacion.ElementoDiseñoRelacionado == operandoAsignarDesde)
                                            {
                                                operacion.Numeros.Clear();
                                            }

                                        }
                                        else
                                        {
                                            asignarACantidad = true;
                                            definicionTextoInformacion.AsignarACantidades = true;
                                        }
                                    }
                                }
                                else if (!itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Any())
                                {
                                    asignarACantidad = true;
                                    definicionTextoInformacion.AsignarACantidades = true;
                                }

                                if (asignarACantidad)
                                {

                                    if (itemInstancia.ReemplazarTextosInformacion)
                                    {
                                        numeroElementoResultado.Textos.Clear();

                                        if (itemInstancia.ConservarTextosInformacion)
                                        {
                                            numeroElementoResultado.Textos.AddRange(TextosPreviosNumero_Resultado);
                                        }
                                    }

                                    if (itemInstancia.AsignarTextosInformacion_CantidadActual)
                                    {
                                        numeroElementoResultado.Textos.AddRange(GenerarTextosInformacion(TextosInformacionAgregadosCantidadActual_Resultado_Operandos));
                                    }

                                    if (itemInstancia.AsignarTextosInformacion_Operacion)
                                    {
                                        numeroElementoResultado.Textos.AddRange(GenerarTextosInformacion(operacion.TextosInformacionAgregadosInstancias_Resultado));
                                    }

                                    if (itemInstancia.AsignarTextosInformacion_Condicion)
                                    {
                                        numeroElementoResultado.Textos.AddRange(GenerarTextosInformacion(definicionTextoInformacion.TextosInformacionAgregadosInstancias_Resultado));
                                    }

                                    if (itemInstancia.AsignarTextosInformacion_CondicionOperandos)
                                    {
                                        numeroElementoResultado.Textos.AddRange(GenerarTextosInformacion(TextosInformacionAgregadosInstancias_Resultado_Operandos));
                                    }

                                    if (itemInstancia.AsignarTextosInformacion_TextosOperandos.Any())
                                    {
                                        foreach (var itemOperando in itemInstancia.AsignarTextosInformacion_TextosOperandos)
                                        {
                                            var operandoEjecucion = ObtenerElementoEjecucion(itemOperando);

                                            List<string> TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando =
                                                definicionTextoInformacion.ObtenerConservarTextosInformacion_Operandos(operandoEjecucion, null);

                                            if (TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando != null)
                                            {

                                                if (itemInstancia.QuitarTextosInformacionRepetidos_TextosOperandos.Contains(itemOperando))
                                                {
                                                    QuitarTextosInformacion_Repetidos(TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando, true);
                                                }

                                                EstablecerTextosInformacion_Elemento(
                                                    itemInstancia,
                                                    elementoOperando,
                                            numeroElementoResultado,
                                            definicionTextoInformacion,
                                            null,
                                            operacion,
                                            GenerarTextosInformacion(TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando),
                                            TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                            TextosInformacionAgregadosCantidadActual_Resultado_Operandos, false);
                                            }
                                        }

                                    }

                                    if (!string.IsNullOrEmpty(itemInstancia.TextoImplicaAsignacion))
                                    {
                                        EstablecerTextosInformacion_Elemento(
                                            itemInstancia,
                                            elementoOperando,
                                            numeroElementoResultado,
                                            definicionTextoInformacion,
                                            null,
                                            operacion,
                                            itemInstancia.TextoImplicaAsignacion.Split('|').ToList(),
                                            TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                            TextosInformacionAgregadosCantidadActual_Resultado_Operandos, false);
                                    }

                                    if (itemInstancia.DigitarTextosInformacion_EnEjecucion)
                                    {
                                        SeleccionarOrdenarCantidades seleccionarOrdenar = new SeleccionarOrdenarCantidades();
                                        seleccionarOrdenar.listaTextos.TextosInformacion = new List<string>();
                                        seleccionarOrdenar.descripcionEntrada.Text = operacion.Nombre;
                                        seleccionarOrdenar.titulo.Text = "Digitar vector de cadenas de texto para variable o vector de entrada " + "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + SubCalculoActual.Nombre;
                                        seleccionarOrdenar.ModoImplicacion = true;

                                        bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                                        if (definicionEstablecida)
                                        {
                                            EstablecerTextosInformacion_Elemento(
                                                itemInstancia,
                                                elementoOperando,
                                            numeroElementoResultado,
                                            definicionTextoInformacion,
                                            null,
                                            operacion,
                                            GenerarTextosInformacion(seleccionarOrdenar.listaTextos.TextosInformacion),
                                            TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                            TextosInformacionAgregadosCantidadActual_Resultado_Operandos, false);
                                        }
                                        else if (definicionEstablecida == false)
                                        {
                                            Detener();
                                            return;
                                        }
                                    }

                                    EstablecerTextosInformacion_Elemento(
                                        itemInstancia,
                                        elementoOperando,
                                        numeroElementoResultado,
                                            definicionTextoInformacion,
                                            null,
                                            operacion,
                                            itemInstancia.TextosAsignacion,
                                            TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                            TextosInformacionAgregadosCantidadActual_Resultado_Operandos, false);

                                    List<string> Cantidad = new List<string>();

                                    if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Any(item => item == elementoOperando.ElementoDiseñoRelacionado))
                                    {
                                        Cantidad.Add(numeroElementoResultado.Numero.ToString());
                                    }

                                    if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Any(item => item == elementoOperando.ElementoDiseñoRelacionado))
                                    {
                                        Cantidad.Add(elementoOperando.Numeros.Count.ToString());
                                    }

                                    EstablecerTextosInformacion_Elemento(
                                        itemInstancia,
                                        elementoOperando,
                                        numeroElementoResultado,
                                            definicionTextoInformacion,
                                            null,
                                            operacion,
                                            Cantidad,
                                            TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                            TextosInformacionAgregadosCantidadActual_Resultado_Operandos, false);

                                    EstablecerTextosInformacion_DesdeEntradas(
                                        itemInstancia,
                                        elementoOperando,
                                        definicionTextoInformacion,
                                        numeroElementoResultado,
                                        operacion,
                                        TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                        TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
                                        definicionTextoInformacion.Condiciones_TextoCondicion != null ?
                                        definicionTextoInformacion.Condiciones_TextoCondicion.VerificarOpcionPosicionActual(elementoOperando.ElementoDiseñoRelacionado) :
                                        false);

                                    EstablecerTextosInformacion_DesdeElementos(
                                        itemInstancia,
                                        definicionTextoInformacion,
                                        elementoOperando,
                                        numeroElementoResultado,
                                        operacion,
                                        TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                        TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
                                        definicionTextoInformacion.Condiciones_TextoCondicion != null ?
                                        definicionTextoInformacion.Condiciones_TextoCondicion.VerificarOpcionPosicionActual(elementoOperando.ElementoDiseñoRelacionado) :
                                        false,
                                        NumerosResultado);

                                    EstablecerTextosInformacion_HaciaElementos(
                                        itemInstancia,
                                        definicionTextoInformacion,
                                        elementoOperando,
                                        numeroElementoOperando,
                                        operacion,
                                        TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                        TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
                                        TextosPreviosNumero_Operando,
                                        NumerosResultado);
                                }
                            }
                        }

                        PosicionActualInstanciaImplicacion++;
                        PosicionActualIteracionImplicacion++;

                        if (itemInstancia.QuitarTextosInformacion_RepetidosVariableVector)
                        {
                            QuitarTextosInformacion_Repetidos(TextosInformacionAgregadosInstancias_Resultado_Operandos, true);
                        }

                        if (itemInstancia.QuitarTextosInformacion_RepetidosVariableVector_Actual)
                        {
                            QuitarTextosInformacion_Repetidos(operacion.TextosInformacionAgregadosInstancias_Resultado, true);
                        }

                        if (itemInstancia.QuitarTextosInformacion_RepetidosIteracion_Actual)
                        {
                            QuitarTextosInformacion_Repetidos(TextosInformacionAgregadosCantidadActual_Resultado_Operandos, true);
                        }

                        if (itemInstancia.QuitarTextosInformacion_RepetidosCondicion_Actual)
                        {
                            QuitarTextosInformacion_Repetidos(definicionTextoInformacion.TextosInformacionAgregadosInstancias_Resultado, true);
                        }
                    }
                }

                if (cumpleAlgunaCondicion && EstablecerNombre && (!nombresSeteados || (nombresSeteados && definicionTextoInformacion.DefinicionOpcionesNombresCantidades.OpcionesTextos.Any())))
                {
                    if (operacionInterna)
                        EstablecerNombreCantidad(numeroElementoResultado, definicionTextoInformacion.DefinicionOpcionesNombresCantidades, 1, operacion, (ElementoDiseñoOperacionAritmeticaEjecucion)elementoOperando);
                    else
                        EstablecerNombreCantidad(numeroElementoResultado, definicionTextoInformacion.DefinicionOpcionesNombresCantidades, 1, operacion, elementoOperando);

                    nombresSeteados = true;
                }

                PosicionActualImplicacion++;
            }
        }

        private void EstablecerTextosInformacion_Elemento(
            InstanciaAsignacionImplicacion_TextosInformacion itemInstancia,
            ElementoOperacionAritmeticaEjecucion elementoOperando,
            EntidadNumero elementoNumero,
            AsignacionImplicacion_TextosInformacion definicionTextoInformacion,
            AsignacionTextosOperando_Implicacion operandoAsignacion,
            ElementoOperacionAritmeticaEjecucion operacion,
            List<string> TextosInformacion,
            List<string> TextosInformacionAgregadosInstancias_Resultado_Operandos,
            List<string> TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
            bool AsignarHaciaOperandos)
        {
            if (itemInstancia.AsignarCadenasTexto_Clasificadores)
            {
                if (elementoNumero != null)
                {
                    var listaCadenasTexto = GenerarTextosInformacion(TextosInformacion);

                    foreach (var itemTexto in listaCadenasTexto)
                    {
                        var clasificador = operacion.Clasificadores_Cantidades.FirstOrDefault(i => i.CadenaTexto == itemTexto);
                        bool nuevoClasificador = false;

                        if (clasificador == null)
                        {
                            clasificador = new Clasificador()
                            {
                                CadenaTexto = itemTexto,
                                ID = App.GenerarID_Elemento(),
                            };

                            nuevoClasificador = true;
                        }

                        if (!elementoNumero.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemTexto))
                        {
                            elementoNumero.Clasificadores_SeleccionarOrdenar.Add(clasificador);

                            if (nuevoClasificador)
                            {
                                operacion.Clasificadores_Cantidades.Add(clasificador);
                            }
                        }

                        if(AsignarHaciaOperandos)
                        {
                            var clasificadorOperando = elementoOperando.Clasificadores_Cantidades.FirstOrDefault(i => i.CadenaTexto == itemTexto);
                            bool nuevoClasificadorOperando = false;

                            if (clasificadorOperando == null)
                            {
                                clasificadorOperando = new Clasificador()
                                {
                                    CadenaTexto = itemTexto,
                                    ID = App.GenerarID_Elemento(),
                                };

                                nuevoClasificadorOperando = true;
                            }

                            if (!elementoNumero.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemTexto))
                            {
                                if (nuevoClasificadorOperando)
                                {
                                    elementoOperando.Clasificadores_Cantidades.Add(clasificadorOperando);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (elementoNumero != null)
                    elementoNumero.Textos.AddRange(GenerarTextosInformacion(TextosInformacion));

                if (itemInstancia.QuitarTextosInformacion_RepetidosCantidad)
                {
                    QuitarTextosInformacion_Repetidos(elementoNumero.Textos, true);
                }

                definicionTextoInformacion.TextosInformacionAsignados_Ejecucion.AddRange(GenerarTextosInformacion(TextosInformacion));
                operacion.TextosInformacionAgregadosInstancias_Resultado.AddRange(GenerarTextosInformacion(TextosInformacion));

                if (operandoAsignacion != null)
                    operandoAsignacion.TextosInformacionAgregadosInstancias_Resultado.AddRange(GenerarTextosInformacion(TextosInformacion));
                else if (definicionTextoInformacion != null)
                    definicionTextoInformacion.TextosInformacionAgregadosInstancias_Resultado.AddRange(GenerarTextosInformacion(TextosInformacion));

                TextosInformacionAgregadosInstancias_Resultado_Operandos.AddRange(GenerarTextosInformacion(TextosInformacion));
                TextosInformacionAgregadosCantidadActual_Resultado_Operandos.AddRange(GenerarTextosInformacion(TextosInformacion));
            }
        }

        private void EstablecerTextosInformacion_DesdeEntradas(InstanciaAsignacionImplicacion_TextosInformacion itemInstancia,
            ElementoOperacionAritmeticaEjecucion elementoOperando,
            AsignacionImplicacion_TextosInformacion definicionTextoInformacion,
            EntidadNumero elementoResultadoNumero,
            ElementoOperacionAritmeticaEjecucion operacion,
            List<string> TextosInformacionAgregadosInstancias_Resultado_Operandos,
            List<string> TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
            bool PosicionActualDesdeCondiciones)
        {
            foreach (var entrada in itemInstancia.Entradas_DesdeAsignarTextosInformacion.Where(i => i.EntradaRelacionada != null &&
                            i.EntradaRelacionada.Tipo == TipoEntrada.TextosInformacion))
            {
                var textosFilas = definicionTextoInformacion.ObtenerTextos_CondicionEntrada(entrada.EntradaRelacionada).ToList();

                ElementoOperacionAritmeticaEjecucion itemOperacion = new ElementoOperacionAritmeticaEjecucion();
                foreach (var itemTextos in textosFilas)
                {
                    itemOperacion.ElementosOperacion.Add(new ElementoOperacionAritmeticaEjecucion() { Textos = GenerarTextosInformacion(itemTextos) });
                }

                bool asignarDesdeEntrada = true;

                if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones.Any(item => item == entrada))
                {
                    var condiciones = itemInstancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.Entrada == entrada)?.Condiciones;
                    if (condiciones != null)
                    {
                        condiciones.TextosInformacionImplicacion_Instancia = TextosInformacionAgregadosInstancias_Resultado_Operandos;
                        condiciones.TextosInformacionImplicacion_Condicion = definicionTextoInformacion.TextosInformacionAgregadosInstancias_Resultado;
                        condiciones.TextosInformacionImplicacion_Operacion = operacion.TextosInformacionAgregadosInstancias_Resultado;
                        condiciones.TextosInformacionImplicacion_CumplenCondicion = definicionTextoInformacion.TextosInformacionImplicacion_CumplenCondicion;

                        itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
                        List<ElementoOperacionAritmeticaEjecucion> ElementosAQuitar = new List<ElementoOperacionAritmeticaEjecucion>();

                        foreach (var itemOperando in itemOperacion.ElementosOperacion)
                        {
                            condiciones.TextosInformacionInvolucrados.Clear();
                            if (condiciones.EvaluarCondiciones(this, itemOperacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? itemOperacion : null,
                                itemOperacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)itemOperacion : null,
                                itemOperando, null))
                                ElementosAQuitar.Add(itemOperando);

                            itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                        }

                        while(ElementosAQuitar.Any())
                        {
                            textosFilas.RemoveAt(itemOperacion.ElementosOperacion.IndexOf(ElementosAQuitar.FirstOrDefault()));
                            itemOperacion.ElementosOperacion.Remove(ElementosAQuitar.FirstOrDefault());                            
                            ElementosAQuitar.Remove(ElementosAQuitar.FirstOrDefault());
                        }

                        if (!itemOperacion.ElementosOperacion.Any())
                            asignarDesdeEntrada = false;
                    }
                }

                if (asignarDesdeEntrada == false) continue;

                List<string> textos = new List<string>();

                if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos.Any(item => item == entrada))
                {
                    if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Any(item => item == entrada) &&
                        !PosicionActualDesdeCondiciones)
                    {
                        foreach (var item in textosFilas)
                        {
                            textos.AddRange(GenerarTextosInformacion(item));
                        }
                    }

                    if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Any(item => item == entrada) ||
                        PosicionActualDesdeCondiciones)
                    {
                        foreach (int PosicionActualNumero_CondicionesOperador_Implicacion in definicionTextoInformacion.PosicionesTextos_CumplenCondicion)
                        {
                            textos.AddRange(GenerarTextosInformacion(textosFilas[PosicionActualNumero_CondicionesOperador_Implicacion]));
                        }
                    }
                }

                if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones.Any(item => item == entrada))
                {
                    var condiciones = itemInstancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.Entrada == entrada)?.Condiciones;
                    if (condiciones != null)
                    {
                        condiciones.TextosInformacionImplicacion_Instancia = TextosInformacionAgregadosInstancias_Resultado_Operandos;
                        condiciones.TextosInformacionImplicacion_Condicion = definicionTextoInformacion.TextosInformacionAgregadosInstancias_Resultado;
                        condiciones.TextosInformacionImplicacion_Operacion = operacion.TextosInformacionAgregadosInstancias_Resultado;
                        condiciones.TextosInformacionImplicacion_CumplenCondicion = definicionTextoInformacion.TextosInformacionImplicacion_CumplenCondicion;

                        itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
                        foreach (var itemOperando in itemOperacion.ElementosOperacion)
                        {
                            bool asignarTextos = false;

                            if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Any(item => item == entrada) &&
                                !PosicionActualDesdeCondiciones)
                            {
                                asignarTextos = true;
                            }
                            else if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Any(item => item == entrada) ||
                                PosicionActualDesdeCondiciones)
                            {
                                if(definicionTextoInformacion.PosicionesTextos_CumplenCondicion.Contains(itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion))
                                {
                                    asignarTextos = true;
                                }
                            }

                            if (asignarTextos)
                            {
                                condiciones.TextosInformacionInvolucrados.Clear();
                                if(condiciones.EvaluarCondiciones(this, itemOperacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion)? itemOperacion : null,
                                    itemOperacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)itemOperacion : null,
                                    itemOperando, null))
                                    textos.AddRange(condiciones.TextosInformacionInvolucrados);
                            }

                            itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                        }
                    }                    
                }

                if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos.Any(item => item == entrada))
                {
                    var listaDefiniciones = itemInstancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones.FirstOrDefault(item => item.Entrada == entrada)?.Definiciones;
                    if (listaDefiniciones != null)
                    {
                        foreach (var itemDefinicionTexto in listaDefiniciones)
                        {
                            foreach (var itemOpcionesDefinicion in itemDefinicionTexto.OpcionesTextos)
                            {
                                if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Any(item => item == entrada) ||
                                    PosicionActualDesdeCondiciones)
                                {
                                    foreach (int PosicionActualNumero_CondicionesOperador_Implicacion in definicionTextoInformacion.PosicionesTextos_CumplenCondicion)
                                    {
                                        var elementoEjecucionEntrada = new ElementoOperacionAritmeticaEjecucion() { Textos = textosFilas[PosicionActualNumero_CondicionesOperador_Implicacion] };

                                        var texto = EstablecerDefinicionNombreCantidad(itemOpcionesDefinicion, PosicionActualNumero_CondicionesOperador_Implicacion + 1, itemDefinicionTexto.PosicionActualDefinicion, 1, operacion, elementoEjecucionEntrada, true);

                                        if (!string.IsNullOrEmpty(texto))
                                            textos.Add(texto);
                                    }
                                }
                                else if (itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Any(item => item == entrada) &&
                                    !PosicionActualDesdeCondiciones)
                                {
                                    for (int PosicionActualNumero_CondicionesOperador_Implicacion = 0; PosicionActualNumero_CondicionesOperador_Implicacion < textosFilas.Count; PosicionActualNumero_CondicionesOperador_Implicacion++)
                                    {
                                        var elementoEjecucionEntrada = new ElementoOperacionAritmeticaEjecucion() { Textos = textosFilas[PosicionActualNumero_CondicionesOperador_Implicacion] };

                                        var texto = EstablecerDefinicionNombreCantidad(itemOpcionesDefinicion, PosicionActualNumero_CondicionesOperador_Implicacion + 1, itemDefinicionTexto.PosicionActualDefinicion, 1, operacion, elementoEjecucionEntrada, true);

                                        if (!string.IsNullOrEmpty(texto))
                                            textos.Add(texto);
                                    }
                                }
                            }

                            itemDefinicionTexto.PosicionActualDefinicion++;
                        }

                    }
                }

                EstablecerTextosInformacion_Elemento(
                    itemInstancia,
                    elementoOperando,
                    elementoResultadoNumero,
                    definicionTextoInformacion,
                    null,
                    operacion,
                    textos,
                    TextosInformacionAgregadosInstancias_Resultado_Operandos,
                    TextosInformacionAgregadosCantidadActual_Resultado_Operandos, false);
            }
        }

        private void EstablecerTextosInformacion_DesdeElementos(InstanciaAsignacionImplicacion_TextosInformacion itemInstancia,            
            AsignacionImplicacion_TextosInformacion definicionTextoInformacion,
            ElementoOperacionAritmeticaEjecucion elementoOperando,
            EntidadNumero numeroElemento,
            ElementoOperacionAritmeticaEjecucion operacion,
            List<string> TextosInformacionAgregadosInstancias_Resultado_Operandos,
            List<string> TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
            bool PosicionActualDesdeCondiciones,
            List<EntidadNumero> NumerosResultado)
        {
            if (elementoOperando != null)
            {
                foreach (var operandoDesdeAsignar in itemInstancia.Operandos_DesdeAsignarTextosInformacion)
                {
                    var elementoOperandoDesdeAsignar = (ElementoOperacionAritmeticaEjecucion)this.ObtenerElementoEjecucion(operandoDesdeAsignar);

                    if (definicionTextoInformacion.AsignarACantidades)
                    {
                        var ListaNumerosOriginal = elementoOperandoDesdeAsignar.Numeros;
                        var PosicionOriginalImplicacion = elementoOperandoDesdeAsignar.PosicionActualNumero_CondicionesOperador_Implicacion;
                        var PosicionOriginalSeleccionarOrdenar = elementoOperandoDesdeAsignar.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar;

                        if (operacion == elementoOperandoDesdeAsignar)
                        {
                            operacion.Numeros.AddRange(NumerosResultado);
                        }
                        else
                        {
                            elementoOperandoDesdeAsignar.Numeros = definicionTextoInformacion.Cantidades_Implicacion_CumplenCondicion;
                            elementoOperandoDesdeAsignar.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
                            elementoOperandoDesdeAsignar.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                        }

                        List<string> textos = new List<string>();

                        {
                            if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos.Any(item => item == operandoDesdeAsignar))
                            {
                                if ((itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Any(item => item == operandoDesdeAsignar)))
                                {

                                    foreach (var cantidadItem in definicionTextoInformacion.Cantidades_Implicacion_CumplenCondicion)
                                    {
                                        textos.AddRange(elementoOperandoDesdeAsignar.TodosTextosInformacion_ElementoActual_Implicacion(cantidadItem));
                                    }
                                }

                                if ((itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Any(item => item == operandoDesdeAsignar)))
                                {

                                    foreach (var cantidadItem in definicionTextoInformacion.Cantidades_Implicacion_CumplenCondicion)
                                    {
                                        textos.AddRange((PosicionActualDesdeCondiciones ?
                                                elementoOperandoDesdeAsignar.TextosInformacion_ElementoActual_Implicacion(cantidadItem) :
                                                elementoOperandoDesdeAsignar.TodosTextosInformacion_ElementoActual_Implicacion(cantidadItem)).ToList());
                                    }
                                }
                            }

                            if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Any(item => item == operandoDesdeAsignar))
                            {
                                var condiciones = itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.Operando == operandoDesdeAsignar)?.Condiciones;
                                if (condiciones != null)
                                {
                                    condiciones.TextosInformacionImplicacion_Instancia = TextosInformacionAgregadosInstancias_Resultado_Operandos;
                                    condiciones.TextosInformacionImplicacion_Condicion = definicionTextoInformacion.TextosInformacionAgregadosInstancias_Resultado;
                                    condiciones.TextosInformacionImplicacion_Operacion = operacion.TextosInformacionAgregadosInstancias_Resultado;
                                    condiciones.TextosInformacionImplicacion_CumplenCondicion = definicionTextoInformacion.TextosInformacionImplicacion_CumplenCondicion;
                                    foreach (var cantidadItem in definicionTextoInformacion.Cantidades_Implicacion_CumplenCondicion)
                                    {
                                        condiciones.TextosInformacionInvolucrados.Clear();
                                        if (condiciones.EvaluarCondiciones(this, operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operacion : null,
                                                operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operacion : null,
                                                elementoOperandoDesdeAsignar, cantidadItem))
                                        {
                                            foreach (var itemTexto in condiciones.TextosInformacionInvolucrados)
                                            {
                                                if (textos.Contains(itemTexto))
                                                {
                                                    textos.AddRange(condiciones.TextosInformacionInvolucrados);
                                                }
                                            }
                                        }
                                    }
                                }

                                if ((itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Any(item => item == operandoDesdeAsignar) &&
                                    (condiciones != null &&
                                    (condiciones.CantidadTextosInformacion_PorElemento | condiciones.CantidadTextosInformacion_PorElemento_Valores))) ||
                                    PosicionActualDesdeCondiciones)

                                {
                                    foreach (var cantidadItem in definicionTextoInformacion.Cantidades_Implicacion_CumplenCondicion)
                                    {
                                        textos = textos.Intersect(elementoOperandoDesdeAsignar.TextosInformacion_ElementoActual_Implicacion(cantidadItem)).ToList();
                                    }
                                }
                            }

                            if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Any(item => item == operandoDesdeAsignar))
                            {
                                var listaDefiniciones = itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.FirstOrDefault(item => item.Operando == operandoDesdeAsignar)?.Definiciones;
                                if (listaDefiniciones != null)
                                {
                                    int posicionOperando = operacion.ElementosOperacion.IndexOf(elementoOperandoDesdeAsignar);

                                    foreach (var itemDefinicionTexto in listaDefiniciones)
                                    {
                                        foreach (var itemOpcionesDefinicion in itemDefinicionTexto.OpcionesTextos)
                                        {
                                            var texto = EstablecerDefinicionNombreCantidad(itemOpcionesDefinicion, elementoOperandoDesdeAsignar.PosicionActualNumero_CondicionesOperador_Implicacion + 1, itemDefinicionTexto.PosicionActualDefinicion + 1, posicionOperando + 1, operacion, elementoOperandoDesdeAsignar, true);

                                            if (!string.IsNullOrEmpty(texto))
                                                textos.Add(texto);
                                        }

                                        itemDefinicionTexto.PosicionActualDefinicion++;
                                    }
                                }
                            }
                        }

                        EstablecerTextosInformacion_Elemento(
                                        itemInstancia,
                                        elementoOperando,
                                            numeroElemento,
                                            definicionTextoInformacion,
                                            null,
                                            operacion,
                                            textos,
                                        TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                        TextosInformacionAgregadosCantidadActual_Resultado_Operandos, false);

                        if (operacion == elementoOperandoDesdeAsignar)
                        {
                            operacion.Numeros.Clear();
                        }
                        else
                        {
                            elementoOperandoDesdeAsignar.Numeros = ListaNumerosOriginal;
                            elementoOperandoDesdeAsignar.PosicionActualNumero_CondicionesOperador_Implicacion = PosicionOriginalImplicacion;
                            elementoOperandoDesdeAsignar.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = PosicionOriginalSeleccionarOrdenar;
                        }
                    }
                }
            }
        }

        private void EstablecerTextosInformacion_HaciaElementos(InstanciaAsignacionImplicacion_TextosInformacion itemInstancia,
            AsignacionImplicacion_TextosInformacion definicionTextoInformacion,
            ElementoOperacionAritmeticaEjecucion elementoOperando,
            EntidadNumero numeroElementoOperando,            
            ElementoOperacionAritmeticaEjecucion operacion,
            List<string> TextosInformacionAgregadosInstancias_Resultado_Operandos,
            List<string> TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
            List<string> TextosPreviosNumero,
            List<EntidadNumero> NumerosResultado)
        {
            if (elementoOperando != null)
            {
                var operandoAsignacion = itemInstancia.Operandos_AsignarTextosInformacionA.FirstOrDefault(item => item.Operando == elementoOperando.ElementoDiseñoRelacionado);

                if (operandoAsignacion != null)
                {
                    {                        
                        if (numeroElementoOperando != null)
                        {
                            if (itemInstancia.ReemplazarTextosInformacion)
                            {
                                numeroElementoOperando.Textos.Clear();

                                if (itemInstancia.ConservarTextosInformacion)
                                {
                                    numeroElementoOperando.Textos.AddRange(TextosPreviosNumero);
                                }
                            }

                            if (itemInstancia.AsignarTextosInformacion_CantidadActual)
                            {
                                numeroElementoOperando.Textos.AddRange(GenerarTextosInformacion(TextosInformacionAgregadosCantidadActual_Resultado_Operandos));
                            }

                            if (itemInstancia.AsignarTextosInformacion_Operacion)
                            {
                                numeroElementoOperando.Textos.AddRange(GenerarTextosInformacion(operacion.TextosInformacionAgregadosInstancias_Operando));
                            }

                            if (itemInstancia.AsignarTextosInformacion_Condicion)
                            {
                                numeroElementoOperando.Textos.AddRange(GenerarTextosInformacion(operandoAsignacion.TextosInformacionAgregadosInstancias_Resultado));
                            }

                            if (itemInstancia.AsignarTextosInformacion_CondicionOperandos)
                            {
                                numeroElementoOperando.Textos.AddRange(GenerarTextosInformacion(TextosInformacionAgregadosInstancias_Resultado_Operandos));
                            }

                            if (itemInstancia.AsignarTextosInformacion_TextosOperandos.Any())
                            {
                                foreach (var itemOperando in itemInstancia.AsignarTextosInformacion_TextosOperandos)
                                {
                                    var operandoEjecucion = ObtenerElementoEjecucion(itemOperando);

                                    List<string> TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando =
                                        definicionTextoInformacion.ObtenerConservarTextosInformacion_Operandos(operandoEjecucion, null);

                                    if (TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando != null)
                                    {
                                        if (itemInstancia.QuitarTextosInformacionRepetidos_TextosOperandos.Contains(itemOperando))
                                        {
                                            QuitarTextosInformacion_Repetidos(TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando, true);
                                        }

                                        EstablecerTextosInformacion_Elemento(
                                            itemInstancia,
                                            elementoOperando,
                                    numeroElementoOperando,
                                    definicionTextoInformacion,
                                    null,
                                    operacion,
                                    GenerarTextosInformacion(TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando),
                                    TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                    TextosInformacionAgregadosCantidadActual_Resultado_Operandos, true);
                                    }
                                }

                            }

                            if (itemInstancia.AsignarTextosInformacion_TextosSubOperandos.Any())
                            {
                                foreach (var itemOperando in itemInstancia.AsignarTextosInformacion_TextosSubOperandos)
                                {
                                    var operandoEjecucion = ObtenerSubElementoEjecucion(itemOperando);

                                    List<string> TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando =
                                        definicionTextoInformacion.ObtenerConservarTextosInformacion_Operandos(operandoEjecucion, null);

                                    if (TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando != null)
                                    {
                                        if (itemInstancia.QuitarTextosInformacionRepetidos_TextosSubOperandos.Contains(itemOperando))
                                        {
                                            QuitarTextosInformacion_Repetidos(TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando, true);
                                        }

                                        EstablecerTextosInformacion_Elemento(
                                            itemInstancia,
                                            elementoOperando,
                                    numeroElementoOperando,
                                    definicionTextoInformacion,
                                    null,
                                    operacion,
                                    GenerarTextosInformacion(TextosInformacionAgregadosInstancias_Resultado_Operandos_Operando),
                                    TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                    TextosInformacionAgregadosCantidadActual_Resultado_Operandos, true);
                                    }
                                }

                            }

                            if (!string.IsNullOrEmpty(itemInstancia.TextoImplicaAsignacion))
                            {
                                EstablecerTextosInformacion_Elemento(
                                    itemInstancia,
                                    elementoOperando,
                                    numeroElementoOperando,
                                    definicionTextoInformacion,
                                    operandoAsignacion,
                                    operacion,
                                    itemInstancia.TextoImplicaAsignacion.Split('|').ToList(),
                                    TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                    TextosInformacionAgregadosCantidadActual_Resultado_Operandos, true);
                            }

                            EstablecerTextosInformacion_Elemento(
                                itemInstancia,
                                elementoOperando,
                                numeroElementoOperando,
                                    definicionTextoInformacion,
                                    operandoAsignacion,
                                    operacion,
                                    itemInstancia.TextosAsignacion,
                                    TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                    TextosInformacionAgregadosCantidadActual_Resultado_Operandos, true);

                            EstablecerTextosInformacion_DesdeEntradas(
                                itemInstancia,
                                elementoOperando,
                                definicionTextoInformacion,
                                numeroElementoOperando,
                                operacion,
                                TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
                                definicionTextoInformacion.Condiciones_TextoCondicion != null ?
                                definicionTextoInformacion.Condiciones_TextoCondicion.VerificarOpcionPosicionActual(elementoOperando.ElementoDiseñoRelacionado) :
                                false);

                            EstablecerTextosInformacion_DesdeElementos(
                                itemInstancia,
                                definicionTextoInformacion,
                                elementoOperando,
                                numeroElementoOperando,
                                operacion,
                                TextosInformacionAgregadosInstancias_Resultado_Operandos,
                                TextosInformacionAgregadosCantidadActual_Resultado_Operandos,
                                definicionTextoInformacion.Condiciones_TextoCondicion != null ?
                                definicionTextoInformacion.Condiciones_TextoCondicion.VerificarOpcionPosicionActual(elementoOperando.ElementoDiseñoRelacionado) :
                                false,
                                NumerosResultado);

                        }
                    }
                }
            }
        }

        public void AsignarTextosInformacion_Elemento(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion, EntidadNumero numeroDesde, 
            EntidadNumero numero,
            bool NoIncluirTextosInformacion_CantidadAInsertar, bool AsignarTextos,
            bool desdeFilas)
        {
            if (AsignarTextos)
            {
                if (!NoIncluirTextosInformacion_CantidadAInsertar)
                {
                    if(itemOperacion != null &&
                        numero != null &&
                        numeroDesde != null)
                    {
                        if (!operacion.ElementoDiseñoRelacionado.NingunOperandoTextosInformacionOperandosResultados &&
                                                                           (!operacion.ElementoDiseñoRelacionado.AlgunosOperandosTextosInformacionOperandosResultados ||
                                                                           (operacion.ElementoDiseñoRelacionado.AlgunosOperandosTextosInformacionOperandosResultados &&
                                                                           operacion.ElementoDiseñoRelacionado.OperandosTextosInformacionOperandosResultados.Any(
                                                                               item => item == itemOperacion.ElementoDiseñoRelacionado))))
                        {
                            if (operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_OperandosResultados)
                                numero.Textos.AddRange(GenerarTextosInformacion(numeroDesde.Textos));
                            else if (operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionCondiciones_OperandosResultados &&
                                                                                                operacion.ElementoDiseñoRelacionado.CondicionesTextosInformacionOperandosResultados != null)
                            {
                                operacion.ElementoDiseñoRelacionado.CondicionesTextosInformacionOperandosResultados.TextosInformacionInvolucrados.Clear();
                                if (operacion.ElementoDiseñoRelacionado.CondicionesTextosInformacionOperandosResultados.EvaluarCondiciones(this,
                                    operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? operacion : null,
                                    operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ? (ElementoDiseñoOperacionAritmeticaEjecucion)operacion : null,
                                    itemOperacion, numeroDesde))
                                {

                                    if (!desdeFilas)
                                        numero.Textos.AddRange(GenerarTextosInformacion(operacion.ElementoDiseñoRelacionado.CondicionesTextosInformacionOperandosResultados.TextosInformacionInvolucrados));
                                    else
                                    {
                                        
                                        if (operacion.ElementoDiseñoRelacionado.CondicionesTextosInformacionOperandosResultados.
                                            VerificarOperando_EnCondicion_SiCumple(this, itemOperacion, numeroDesde))
                                        {
                                            itemOperacion.TextosInformacionInvolucrados_CondicionTextos.AddRange(GenerarTextosInformacion(operacion.ElementoDiseñoRelacionado.CondicionesTextosInformacionOperandosResultados.TextosInformacionInvolucrados));
                                        }
                                        else
                                            itemOperacion.TieneCondicionesTextos_NoCumplen = true;
                                    }
                                }
                                else
                                {
                                    if(desdeFilas)
                                    {
                                        itemOperacion.TieneCondicionesTextos_NoCumplen = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            RealizarOperacionesCadenasTexto(numero.Textos, operacion.ElementoDiseñoRelacionado.ConfigOperaciones_CadenasTexto);
            QuitarTextosInformacion_Repetidos(numero.Textos, operacion.ElementoDiseñoRelacionado.QuitarTextosInformacion_Repetidos);
        }

        public void RealizarOperacionesCadenasTexto(List<string> ListaTextos, ConfiguracionOperacionesCadenasTexto config)
        {
            foreach(var itemOperacion in config.Operaciones)
            {
                if(itemOperacion.PosicionInicial >= 0 &&
                    itemOperacion.PosicionInicial < ListaTextos.Count)
                {
                    var cadena = ListaTextos[itemOperacion.PosicionInicial];
                    int desplazamiento = 0;

                    if (itemOperacion.PosicionFinal >= 0 &&
                    itemOperacion.PosicionFinal < ListaTextos.Count)
                    {
                        ListaTextos.Insert(itemOperacion.PosicionFinal, cadena);

                        if (itemOperacion.PosicionFinal < itemOperacion.PosicionInicial)
                            desplazamiento++;
                    }   

                    if (itemOperacion.Tipo == TipoOperacionCadenaTexto.Mover)
                    {
                        ListaTextos.RemoveAt(itemOperacion.PosicionInicial + desplazamiento);
                    }
                }
            }
        }
    }
}
