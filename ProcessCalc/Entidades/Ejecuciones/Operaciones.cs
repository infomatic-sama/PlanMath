using ProcessCalc.Controles;
using ProcessCalc.Controles.Calculos;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Ejecuciones;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.SubcalculosYArchivosExternos;
using ProcessCalc.Ventanas;
using ProcessCalc.Ventanas.Seleccionar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Xml;
using System.Xml.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static ProcessCalc.Entidades.ElementoDiseñoOperacionAritmeticaEjecucion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        private void RealizarOperaciones(EtapaEjecucion etapa, ElementoCalculoEjecucion itemCalculo, bool mostrarLog)
        {
            foreach (var item in etapa.Elementos)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                string strMensajeLog = string.Empty;
                string strMensajeLogResultados = string.Empty;
                string strOperando = string.Empty;

                switch (item.Tipo)
                {
                    case TipoElementoEjecucion.OperacionAritmetica:
                        if (item.Estado == EstadoEjecucion.Procesado) continue;

                        item.Estado = EstadoEjecucion.Iniciado;

                        if (AgrupadorCambio != item.ElementoDiseñoRelacionado.AgrupadorContenedor)
                        {
                            AgrupadorActual = item.ElementoDiseñoRelacionado.AgrupadorContenedor;
                            AgrupadorCambio = item.ElementoDiseñoRelacionado.AgrupadorContenedor;
                        }

                        ElementoOperacionAritmeticaEjecucion operacion = (ElementoOperacionAritmeticaEjecucion)item;

                        string info = string.Empty;
                        bool actualizarToolTip = true;

                        if (!ModoToolTips || (ModoToolTips && ModoEjecucionExterna && TooltipsCalculo == null) ||
                            (ModoToolTips && TooltipsCalculo != null
                            && (item.ConModificaciones_ToolTipMostrado(TooltipsCalculo, this) ||
                            (TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID) != null &&
                            (TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID).ConCambios_ToolTips ||
                            TooltipsCalculo.ObtenerElementoDiseño(Calculo.ID, item.ElementoDiseñoRelacionado.ID).Actualizar_ToolTips)))))
                        {
                            if (operacion.VerificarEjecucion_OperandosConCondiciones() ||
                                operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarEntradas ||
                                operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.ArchivoExterno)
                            {
                                if (Calculo.MostrarInformacionElementos_InformeResultados &&
                                !string.IsNullOrEmpty(item.ElementoDiseñoRelacionado.Info))
                                {
                                    info = " (" + item.ElementoDiseñoRelacionado.Info + ") ";
                                }

                                OrdenarCantidades_Operacion_Antes(operacion, operacion.ElementoDiseñoRelacionado.OrdenarNumerosAntesEjecucion,
                                    operacion.ElementoDiseñoRelacionado.OrdenarNumeros_AntesEjecucion);

                                if(operacion.ElementoDiseñoRelacionado.QuitarClasificadores_AntesEjecucion)
                                {
                                    foreach (var itemOperacion in operacion.ElementosOperacion)
                                    {
                                        foreach (var itemNumero in itemOperacion.Numeros)
                                        {
                                            itemNumero.Clasificadores_SeleccionarOrdenar_Originales_Temp.AddRange(itemNumero.Clasificadores_SeleccionarOrdenar);
                                            itemNumero.Clasificadores_SeleccionarOrdenar.Clear();
                                        }

                                        itemOperacion.Clasificadores_Cantidades_Originales_Temp.AddRange(itemOperacion.Clasificadores_Cantidades);
                                        itemOperacion.Clasificadores_Cantidades.Clear();
                                        itemOperacion.AgregarClasificadoresGenericos();
                                    }

                                    operacion.Clasificadores_Cantidades.Clear();
                                }
                                else if(operacion.ElementoDiseñoRelacionado.OrdenarClasificadores_AntesEjecucion)
                                {
                                    foreach (var itemOperacion in operacion.ElementosOperacion)
                                    {
                                        itemOperacion.Clasificadores_Cantidades_Originales_Temp.AddRange(itemOperacion.Clasificadores_Cantidades);

                                        List<Clasificador> listaClasificadoresOrdenada = new List<Clasificador>();

                                        if (operacion.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMenorAMayor_AntesEjecucion)
                                        {
                                            listaClasificadoresOrdenada = itemOperacion.Clasificadores_Cantidades.OrderBy(i => i.CadenaTexto).ToList();
                                        }
                                        else if (operacion.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMayorAMenor_AntesEjecucion)
                                        {
                                            listaClasificadoresOrdenada = itemOperacion.Clasificadores_Cantidades.OrderByDescending(i => i.CadenaTexto).ToList();
                                        }

                                        itemOperacion.Clasificadores_Cantidades.Clear();
                                        itemOperacion.Clasificadores_Cantidades.AddRange(listaClasificadoresOrdenada);
                                    }
                                }

                                strMensajeLogResultados = "Para calcular y obtener el valor de " + item.ElementoDiseñoRelacionado.Nombre + info + ", se ";
                                strMensajeLog = "Para calcular y obtener el valor de " + item.ElementoDiseñoRelacionado.Nombre + info + ", se ";

                                switch (operacion.TipoOperacion)
                                {
                                    case TipoOperacionAritmeticaEjecucion.Suma:
                                    case TipoOperacionAritmeticaEjecucion.Resta:
                                    case TipoOperacionAritmeticaEjecucion.Multiplicacion:
                                    case TipoOperacionAritmeticaEjecucion.Division:
                                    case TipoOperacionAritmeticaEjecucion.Potencia:
                                    case TipoOperacionAritmeticaEjecucion.Raiz:
                                    case TipoOperacionAritmeticaEjecucion.Porcentaje:
                                    case TipoOperacionAritmeticaEjecucion.Logaritmo:
                                    case TipoOperacionAritmeticaEjecucion.Inverso:
                                    case TipoOperacionAritmeticaEjecucion.Factorial:
                                    case TipoOperacionAritmeticaEjecucion.RedondearCantidades:
                                    case TipoOperacionAritmeticaEjecucion.ContarCantidades:

                                        switch (operacion.TipoOperacion)
                                        {
                                            case TipoOperacionAritmeticaEjecucion.Suma:
                                                strMensajeLogResultados += "sumaron";
                                                strMensajeLog += "sumaron";
                                                strOperando = "Sumando";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Resta:
                                                strMensajeLogResultados += "restaron";
                                                strMensajeLog += "restaron";
                                                strOperando = "Restando";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Multiplicacion:
                                                strMensajeLogResultados += "multiplicaron";
                                                strMensajeLog += "multiplicaron";
                                                strOperando = "Multiplicando";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Division:
                                                strMensajeLogResultados += "dividieron";
                                                strMensajeLog += "dividieron";
                                                strOperando = "Dividiendo";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Potencia:
                                                strMensajeLogResultados += "calcularon potencias";
                                                strMensajeLog += "calcularon potencias";
                                                strOperando = "Calculando potencias";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Raiz:
                                                strMensajeLogResultados += "calcularon raíces";
                                                strMensajeLog += "calcularon raíces";
                                                strOperando = "Calculando raíces";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Porcentaje:
                                                strMensajeLogResultados += "calcularon porcentajes";
                                                strMensajeLog += "calcularon porcentajes";
                                                strOperando = "Calculando porcentajes";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Logaritmo:
                                                strMensajeLogResultados += "calcularon logaritmos";
                                                strMensajeLog += "calcularon logaritmos";
                                                strOperando = "Calculando logaritmos";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Inverso:
                                                strMensajeLogResultados += "calcularon los inversos de ";
                                                strMensajeLog += "calcularon los inversos de ";
                                                strOperando = "Calculando los inversos de ";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.Factorial:
                                                strMensajeLogResultados += "calcularon los factoriales de ";
                                                strMensajeLog += "calcularon los factoriales de ";
                                                strOperando = "Calculando los factoriales de ";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.RedondearCantidades:
                                                strMensajeLogResultados += "redondearon las cantidades de ";
                                                strMensajeLog += "redondearon las cantidades de ";
                                                strOperando = "Redondeando las cantidades de ";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.ContarCantidades:
                                                strMensajeLogResultados += "se contaron las cantidades de";
                                                strMensajeLog += "se contaron las cantidades de";
                                                strOperando = "Contando cantidades";
                                                break;
                                        }

                                        strMensajeLogResultados += " las variables o vectores siguientes con sus valores:\n";
                                        strMensajeLog += " las variables o vectores siguientes con sus valores:\n";

                                        if (operacion.ElementoDiseñoRelacionado.DefinicionSimple_Operacion)
                                        {
                                            switch (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion)
                                            {
                                                case TipoOperacionEjecucion.OperarTodosJuntos:
                                                    strMensajeLog += " todos los números juntos de los siguientes variables o vectores:\n";
                                                    strMensajeLogResultados += " todos los números juntos de los siguientes variables o vectores:\n";
                                                    break;

                                                case TipoOperacionEjecucion.OperarPorSeparado:
                                                    strMensajeLog += " los siguientes variables o vectores por separado:\n";
                                                    strMensajeLogResultados += " los variables o vectores operandos por separado:\n";
                                                    break;

                                                case TipoOperacionEjecucion.OperarPorFilas:
                                                    strMensajeLog += " los siguientes variables o vectores por filas del vector:\n";
                                                    strMensajeLogResultados += " los siguientes variables o vectores por filas del vector:\n";
                                                    break;

                                                case TipoOperacionEjecucion.OperarPorSeparadoPorFilas:
                                                    strMensajeLog += " los siguientes variables o vectores por separado y por filas del vector:\n";
                                                    strMensajeLogResultados += " los siguientes variables o vectores por separado y por filas del vector:\n";
                                                    break;

                                            }

                                            if ((operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Logaritmo &&
                                                operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural) ||
                                                operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Inverso ||
                                                operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Factorial ||
                                                operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.RedondearCantidades)
                                            {
                                                operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarPorSeparadoPorFilas;
                                                operacion.ElementoDiseñoRelacionado.ConAcumulacion = false;
                                            }

                                            OperarNumerosOperacion(operacion, ref strMensajeLog, ref strMensajeLogResultados, false);

                                        }
                                        else
                                        {
                                            item.SetearToolTipMostrado_ElementosInternos(this);
                                            OperarNumerosOperacion_Interna(operacion, ref strMensajeLog, ref strMensajeLogResultados,
                                                ref info, ref strOperando, itemCalculo, mostrarLog);
                                        }

                                        break;

                                    case TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar:
                                    case TipoOperacionAritmeticaEjecucion.CondicionFlujo:
                                    case TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                                    case TipoOperacionAritmeticaEjecucion.LimpiarDatos:

                                        switch (operacion.TipoOperacion)
                                        {
                                            case TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar:
                                                strMensajeLogResultados += "seleccionaron y ordenaron cantidades";
                                                strMensajeLog += "seleccionaron y ordenaron cantidades";
                                                strOperando = "Seleccionando y ordenando cantidades";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.CondicionFlujo:
                                                strMensajeLogResultados += "seleccionaron todas las cantidades";
                                                strMensajeLog += "seleccionaron todas las cantidades";
                                                strOperando = "Seleccionando todas las cantidades";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                                                strMensajeLogResultados += "traspasaron todas las cantidades";
                                                strMensajeLog += "traspasaron todas las cantidades";
                                                strOperando = "Traspasando todas las cantidades";
                                                break;

                                            case TipoOperacionAritmeticaEjecucion.LimpiarDatos:
                                                strMensajeLogResultados += "limpiaron cantidades\n";
                                                strMensajeLog += "limpiaron cantidades:\n";
                                                strOperando = "Limpiando cantidades";
                                                break;
                                        }

                                        if (((operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar && operacion.DefinicionSimple_TextosInformacion) ||
                                            (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo && operacion.DefinicionSimple_CondicionesFlujo)) ||
                                            operacion.ElementoDiseñoRelacionado.DefinicionSimple_Operacion)
                                        {
                                            strMensajeLog += " de los siguientes variables o vectores:\n";
                                            strMensajeLogResultados += " de los siguientes variables o vectores:\n";
                                            OperarSeleccionNumerosOperacion(operacion, ref strMensajeLog, ref strMensajeLogResultados, false, itemCalculo);
                                        }
                                        else
                                        {
                                            item.SetearToolTipMostrado_ElementosInternos(this);
                                            OperarNumerosOperacion_Interna(operacion, ref strMensajeLog, ref strMensajeLogResultados,
                                                ref info, ref strOperando, itemCalculo, mostrarLog);
                                        }

                                        break;

                                    case TipoOperacionAritmeticaEjecucion.SeleccionarEntradas:
                                        ProcesarOperacionSeleccionarEntradas(operacion,
                                            ref strMensajeLogResultados, ref strMensajeLog, ref strOperando, itemCalculo);
                                        break;

                                    case TipoOperacionAritmeticaEjecucion.Espera:

                                        strMensajeLogResultados += "Esperando datos\n";
                                        strMensajeLog += "Esperando datos:\n";
                                        strOperando = "Esperando datos";

                                        ProcesarEspera(operacion,
                                            ref strMensajeLogResultados, ref strMensajeLog, itemCalculo, mostrarLog);
                                        break;



                                    case TipoOperacionAritmeticaEjecucion.ArchivoExterno:

                                        strMensajeLogResultados += "ejecutarán los archivos de cálculos externos:\n";
                                        strMensajeLog += "ejecutarán los archivos de cálculos externos:\n";
                                        strOperando = "ejecutando archivos de cálculos externos";

                                        ProcesarArchivoExterno(operacion,
                                            ref strMensajeLogResultados, ref strMensajeLog, itemCalculo, mostrarLog);
                                        break;

                                    case TipoOperacionAritmeticaEjecucion.SubCalculo:

                                        strMensajeLogResultados += "ejecutarán los subcálculos:\n";
                                        strMensajeLog += "ejecutarán los subcálculos:\n";
                                        strOperando = "ejecutando subcálculos";

                                        ProcesarSubCalculo(operacion,
                                            ref strMensajeLogResultados, ref strMensajeLog, itemCalculo, mostrarLog);
                                        break;
                                }

                                if (operacion.ElementoDiseñoRelacionado.QuitarClasificadores_AntesEjecucion)
                                {
                                    foreach (var itemOperacion in operacion.ElementosOperacion)
                                    {
                                        foreach (var itemNumero in itemOperacion.Numeros)
                                        {
                                            itemNumero.Clasificadores_SeleccionarOrdenar.Clear();
                                            itemNumero.Clasificadores_SeleccionarOrdenar.AddRange(itemNumero.Clasificadores_SeleccionarOrdenar_Originales_Temp);
                                            itemNumero.Clasificadores_SeleccionarOrdenar_Originales_Temp.Clear();
                                        }

                                        itemOperacion.Clasificadores_Cantidades.Clear();
                                        itemOperacion.Clasificadores_Cantidades.AddRange(itemOperacion.Clasificadores_Cantidades_Originales_Temp);
                                        itemOperacion.Clasificadores_Cantidades_Originales_Temp.Clear();
                                    }
                                }
                                else if (operacion.ElementoDiseñoRelacionado.OrdenarClasificadores_AntesEjecucion)
                                {
                                    foreach (var itemOperacion in operacion.ElementosOperacion)
                                    {
                                        itemOperacion.Clasificadores_Cantidades.Clear();
                                        itemOperacion.Clasificadores_Cantidades.AddRange(itemOperacion.Clasificadores_Cantidades_Originales_Temp);
                                        itemOperacion.Clasificadores_Cantidades_Originales_Temp.Clear();
                                    }
                                }

                                if (operacion.ElementoDiseñoRelacionado.QuitarClasificadores_DespuesEjecucion)
                                {
                                    foreach (var itemNumero in operacion.Numeros)
                                    {
                                        itemNumero.Clasificadores_SeleccionarOrdenar.Clear();
                                    }

                                    operacion.Clasificadores_Cantidades.Clear();
                                    operacion.AgregarClasificadoresGenericos();
                                }
                                else if(operacion.ElementoDiseñoRelacionado.OrdenarClasificadores_DespuesEjecucion)
                                {                                    
                                    List<Clasificador> listaClasificadoresOrdenada = new List<Clasificador>();

                                    if (operacion.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion)
                                    {
                                        listaClasificadoresOrdenada = operacion.Clasificadores_Cantidades.OrderBy(i => i.CadenaTexto).ToList();
                                    }
                                    else if (operacion.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion)
                                    {
                                        listaClasificadoresOrdenada = operacion.Clasificadores_Cantidades.OrderByDescending(i => i.CadenaTexto).ToList();
                                    }

                                    operacion.Clasificadores_Cantidades.Clear();
                                    operacion.Clasificadores_Cantidades.AddRange(listaClasificadoresOrdenada);                                    
                                }

                                OrdenarCantidades_Operacion_Despues(operacion, operacion.ElementoDiseñoRelacionado.OrdenarNumerosDespuesEjecucion,
                                    operacion.ElementoDiseñoRelacionado.OrdenarNumeros_DespuesEjecucion);

                                if (operacion.ElementoDiseñoRelacionado.LimpiarDatosResultados)
                                    operacion.LimpiarCantidades_Comportamiento();

                                if (operacion.ElementoDiseñoRelacionado.RedondearCantidadesResultados)
                                    operacion.RedondearCantidades_Comportamiento();

                                strMensajeLogResultados += "Los resultados de " + item.ElementoDiseñoRelacionado.Nombre + " son:\n";
                                strMensajeLog += "Los resultados de " + item.ElementoDiseñoRelacionado.Nombre + " son:\n";

                                foreach (var itemClasificador in operacion.Clasificadores_Cantidades)
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            operacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                                    if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                                    {
                                        strMensajeLog += "\n" + itemClasificador.CadenaTexto + ":\n";
                                        strMensajeLogResultados += "\n" + itemClasificador.CadenaTexto + ":\n";
                                    }
                                    else
                                    {
                                        strMensajeLog += "\n";
                                        strMensajeLogResultados += "\n";
                                    }

                                    foreach (var itemNumero in operacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)))
                                    {
                                        if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemNumero))
                                        {
                                            strMensajeLogResultados += itemNumero.Nombre + ", su valor es: " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                                            strMensajeLog += itemNumero.Nombre + ", su valor es: " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                                        }
                                    }
                                }
                            }
                            else
                                if (item.ContieneSalida_Ejecucion)
                                item.ContieneSalida_Ejecucion = false;
                        }
                        else
                        {
                            if (item.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                            {
                                if (TooltipsCalculo != null)
                                {
                                    TooltipsCalculo.ObtenerDatosTooltip_Elemento_Persistencia(item.ElementoDiseñoRelacionado,
                                        ((ElementoOperacionAritmeticaEjecucion)item).Numeros, (ElementoOperacionAritmeticaEjecucion)item);
                                    actualizarToolTip = false;
                                }

                                if (!operacion.ElementoDiseñoRelacionado.DefinicionSimple_Operacion ||
                                    !operacion.ElementoDiseñoRelacionado.DefinicionSimple_CondicionesFlujo ||
                                    !operacion.ElementoDiseñoRelacionado.DefinicionSimple_TextosInformacion)
                                {
                                    OperarNumerosOperacion_Interna(operacion, ref strMensajeLog, ref strMensajeLogResultados,
                                        ref info, ref strOperando, itemCalculo, mostrarLog);
                                }
                            }
                        }

                        if (ModoToolTips && TooltipsCalculo != null &&
                            actualizarToolTip)
                        {
                            if (item.GetType() == typeof(ElementoOperacionAritmeticaEjecucion))
                            {
                                TooltipsCalculo.EstablecerDatosTooltip_Elemento(item.ElementoDiseñoRelacionado,
                                    ((ElementoOperacionAritmeticaEjecucion)item).Numeros.Select(i => i.CopiarObjeto()).ToList(),
                                    TipoOpcionToolTip.Operacion, item.Clasificadores_Cantidades, 
                                    operacion.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
                                    operacion.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);
                                item.ToolTipMostrado = true;

                                if(((ElementoOperacionAritmeticaEjecucion)item).ActualizarTooltips_Operandos)
                                {
                                    foreach(var itemOperacion in ((ElementoOperacionAritmeticaEjecucion)item).ElementosOperacion)
                                    {
                                        TooltipsCalculo.EstablecerDatosTooltip_Elemento(itemOperacion.ElementoDiseñoRelacionado,
                                    ((ElementoOperacionAritmeticaEjecucion)itemOperacion).Numeros.Select(i => i.CopiarObjeto()).ToList(),
                                    TipoOpcionToolTip.Operacion, itemOperacion.Clasificadores_Cantidades,
                                    operacion.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
                                    operacion.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);
                                        itemOperacion.ToolTipMostrado = true;
                                    }
                                }
                            }
                        }
                        

                            item.Estado = EstadoEjecucion.Procesado;
                        CantidadElementosEjecucionProcesados++;
                        ActualizarPorcentajeAvance();

                        if (Pausada) Pausar();
                        if (Detenida) return;

                        if (mostrarLog)
                        {
                            if (!string.IsNullOrEmpty(strMensajeLog))
                                log.Add(strMensajeLog);
                            if (!string.IsNullOrEmpty(strMensajeLogResultados))
                                InformeResultados.TextoLog.Add(strMensajeLogResultados);
                        }

                        if (item.ContieneSalida_Ejecucion)
                            Salidas.Add((ElementoOperacionAritmeticaEjecucion)item);

                        break;
                }
            }
        }

        private void OperarNumerosOperacion(ElementoOperacionAritmeticaEjecucion operacion, ref string strMensajeLog, ref string strMensajeLogResultados,
            bool operacionInterna)
        {
            bool sinOperandos = true;
            int indiceNumero = 0;

            bool utilizandoPrimeraCantidad = true;
            string strOperandoPares = string.Empty;
            string strOperandoPares2 = string.Empty;
            string strOperandosResultados = string.Empty;

            int indiceResultadosMensaje = 0;
            int indiceNumeroElemento = 0;

            List<EntidadNumero> primerasCantidades_ParesOperandos = new List<EntidadNumero>();
            List<EntidadNumero> segundasCantidades_ParesOperandos = new List<EntidadNumero>();

            operacion.CantidadNumeros_Ejecucion = 0;

            foreach (var itemOperacion in operacion.ElementosOperacion)
            {
                itemOperacion.FinalIndicePosicionClasificadores = false;
                itemOperacion.IndicePosicionClasificadores = 0;
                itemOperacion.CantidadNumeros_Clasificador = 0;
            }

            List<EntidadNumero> NumerosResultado = new List<EntidadNumero>();
            List<EntidadNumero> NumerosResultadoTotal = new List<EntidadNumero>();

            int cantidadClasificadores = GenerarCantidadResultados(operacion);

            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                DefinirNombresAntes_Ejecucion(operacion);

            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                operacion.InicializarPosicionesClasificadores_Operandos();

            while (operacion.ElementosOperacion.Any(i => !i.FinalIndicePosicionClasificadores))
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                int IndiceNumeroActualElementoOperacion = 0;
                int CantidadNumeros = 1;

                double cantidadAcumulada = 0;

                bool FlagDetenerProcesamiento = false;
                bool FlagMantenerProcesamiento = false;

                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                {
                    Procesar_ListaNumerosFilas_IteracionEjecucion(operacion, ref CantidadNumeros, ref indiceNumero,
                        ref strMensajeLog, ref strMensajeLogResultados);
                }

                foreach (var itemOperacion in operacion.ElementosOperacion)
                {
                    itemOperacion.InicializarPosicionesClasificadores_Operandos();
                }

                for (int indiceClasificadores = 0; indiceClasificadores < cantidadClasificadores; indiceClasificadores++)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    NumerosResultado.Add(new EntidadNumero());
                    cantidadAcumulada = 0;

                    int cantidadTotalNumerosAgrupados = 0;
                    int cantidadNumerosAgregados_Pares = 0;

                    bool iteracionAnulada = false;

                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                    {
                        Procesar_ListaNumerosFilas_IteracionEjecucion(operacion, ref CantidadNumeros, ref indiceNumero,
                            ref strMensajeLog, ref strMensajeLogResultados);
                    }

                    IndiceNumeroActualElementoOperacion = 0;

                    int cantidadNumeros_PorSeparado = 0;

                    while (IndiceNumeroActualElementoOperacion < CantidadNumeros)
                    {
                        if (Pausada) Pausar();
                        if (Detenida) return;

                        EntidadNumero numeroResultadoFila = new EntidadNumero();
                        numeroResultadoFila.AgregarNumero_PorFilas = true;

                        FlagDetenerProcesamiento = false;
                        FlagMantenerProcesamiento = false;

                        do
                        {
                            bool conAgrupaciones = false;
                            bool conCambioAgrupacion = true;
                            bool conAgrupacionRecorrida = false;

                            int cantidadNumerosAgrupados = 0;

                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                            {
                                switch(operacion.TipoOperacion)
                                {
                                    case TipoOperacionAritmeticaEjecucion.Suma:
                                    case TipoOperacionAritmeticaEjecucion.Resta:
                                    case TipoOperacionAritmeticaEjecucion.Multiplicacion:
                                    case TipoOperacionAritmeticaEjecucion.Division:

                                        //if (this.itemClasificador == null)
                                        //{
                                        //    this.itemClasificador = operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion).
                                        //        FirstOrDefault().Clasificadores_Cantidades.FirstOrDefault();
                                        //}

                                        ProcesarFilas_IteracionEjecucion(operacion, numeroResultadoFila, NumerosResultado, ref IndiceNumeroActualElementoOperacion,
                                    indiceClasificadores, ref FlagDetenerProcesamiento, ref FlagMantenerProcesamiento, ref conAgrupaciones,
                                    ref cantidadNumerosAgrupados, CantidadNumeros);

                                        break;
                                }
                            }

                            bool agregarNumero = true;
                            int cantidadNumeros_Fila = 0;
                            bool ElementosNoConjuntos = false;

                            bool operandoConNumeros = true;

                            string strParesOperandos = string.Empty;
                            string strParesOperandos2 = string.Empty;
                            string strParOperando = string.Empty;

                            int indiceAgrupacionesPorFilas = 0;
                            //int cantidadParcialNumerosAgrupados = cantidadNumerosAgrupados;
                            int cantidadIteracionesAnuladas_ItemsOperacion = 0;
                            
                            foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                            {
                                if (Pausada) Pausar();
                                if (Detenida) return;

                                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado)
                                {
                                    //if (conAgrupaciones)
                                    {
                                        conAgrupacionRecorrida = operacion.ObtenerCantidadElementosAgrupados_AgrupacionRecorrida(itemOperacion);
                                    }
                                }

                                List<Clasificador> ClasificadoresIteracion = new List<Clasificador>();

                                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos |
                                    operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                {
                                    if(indiceClasificadores <= itemOperacion.Clasificadores_Cantidades.Count - 1)
                                        ClasificadoresIteracion.Add(itemOperacion.Clasificadores_Cantidades[indiceClasificadores]);
                                }
                                else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado |
                                    operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                {
                                    ClasificadoresIteracion.AddRange(itemOperacion.Clasificadores_Cantidades);
                                }

                                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado)
                                    operacion.InicializarPosicionesClasificadores_Operandos();
                                else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                    itemOperacion.IndicePosicionClasificadores = 0;

                                bool iteracionAnulada_ItemOperacion = false;

                                foreach (var itemClasificador in ClasificadoresIteracion)
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                                        itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)))
                                    {
                                        //if (!operacion.ElementosOperacion.Any(itemOperacion => itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))))
                                        {
                                            //iteracionAnulada = true;

                                            iteracionAnulada_ItemOperacion = true;
                                            cantidadIteracionesAnuladas_ItemsOperacion++;

                                            indiceClasificadores--;
                                            
                                            itemOperacion.Clasificadores_Cantidades.Remove(
                                                    itemOperacion.Clasificadores_Cantidades.FirstOrDefault(i => i.CadenaTexto == itemClasificador.CadenaTexto));

                                            cantidadClasificadores = GenerarCantidadResultados(operacion);

                                            itemOperacion.CantidadNumeros_Clasificador = 0;

                                            break;
                                        }
                                    }

                                    this.itemClasificador = itemClasificador;

                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                    {
                                        if(itemOperacion.NumeroResultado_PorFilas != null)
                                            AgregarClasificador_Cantidad(operacion, itemClasificador, itemOperacion.NumeroResultado_PorFilas);
                                    }
                                    else
                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorSeparado)
                                    {
                                        AgregarClasificador_Cantidad(operacion, itemClasificador, NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado + cantidadNumerosAgregados_Pares]);
                                    }

                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado)
                                    {
                                        if (itemOperacion.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Any(i => i.OperacionRelacionada == operacion.ElementoDiseñoRelacionado))
                                        {
                                            var agrupacion = itemOperacion.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionRelacionada == operacion.ElementoDiseñoRelacionado).FirstOrDefault();

                                            var resultadoAgrupado = NumerosResultado.Where(i => i.Agrupacion_PorSeparado == agrupacion.NombreAgrupacion).Select(i => (EntidadNumero)i).FirstOrDefault();
                                            if (resultadoAgrupado != null)
                                            {
                                                NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado + cantidadNumerosAgregados_Pares] = resultadoAgrupado;
                                            }
                                            else
                                            {
                                                NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado + cantidadNumerosAgregados_Pares].Agrupacion_PorSeparado = agrupacion.NombreAgrupacion;
                                            }
                                        }
                                    }
                                    else if(operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                    {
                                    
                                        if (itemOperacion == operacion.ElementosOperacion.First(i => i.Clasificadores_Cantidades
                            .Any(i => i == itemOperacion.Clasificadores_Cantidades[itemOperacion.IndicePosicionClasificadores])))
                                        {
                                            if (operacion.ElementoDiseñoRelacionado.ConAcumulacion)
                                            {
                                                itemOperacion.NumeroResultado_PorFilas.Numero = cantidadAcumulada;
                                            }
                                        }
                                    }

                                    {
                                        if (Pausada) Pausar();
                                        if (Detenida) return;

                                        if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                                        {
                                            strMensajeLog += "\n" + itemClasificador.CadenaTexto + ":\n";
                                            strMensajeLogResultados += "\n" + itemClasificador.CadenaTexto + ":\n";
                                        }
                                        else
                                        {
                                            strMensajeLog += "\n";
                                            strMensajeLogResultados += "\n";
                                        }

                                        strOperandoPares = string.Empty;
                                        indiceNumeroElemento = indiceNumero;

                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                            InicializarVariables_Operacion(operacion);

                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                            sinOperandos = true;

                                        if (operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.Potencia &&
                                            operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.Raiz &&
                                            operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.Porcentaje &&
                                            operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.Logaritmo)
                                            operandoConNumeros = true;

                                        bool conNumeros = false;

                                        string info = string.Empty;

                                        if (Calculo.MostrarInformacionElementos_InformeResultados &&
                                            !string.IsNullOrEmpty(itemOperacion.ElementoDiseñoRelacionado.Info))
                                        {
                                            info = " (" + itemOperacion.ElementoDiseñoRelacionado.Info + ") ";
                                        }

                                        strMensajeLog += itemOperacion.Nombre + info + "\n";
                                        strMensajeLogResultados += itemOperacion.Nombre + info + "\n";

                                        strMensajeLogResultados += itemOperacion.Nombre + info + ", sus valores son:\n";
                                        strMensajeLog += itemOperacion.Nombre + info + ", sus valores son:\n";

                                        if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Potencia)
                                        {
                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                            {
                                                if (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosPotencia != TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos)
                                                {
                                                    switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosPotencia)
                                                    {
                                                        case TipoOpcionElementosFijosOperacionPotencia.BaseFijaExponenteOperando:
                                                            strOperandoPares += "Base: " + operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos.ToString();
                                                            strParesOperandos = "Exponentes (" + itemOperacion.Nombre + info + "): ";
                                                            strParesOperandos2 = "Exponentes (" + itemOperacion.Nombre + info + "): ";
                                                            break;

                                                        case TipoOpcionElementosFijosOperacionPotencia.BaseOperandoExponenteFijo:
                                                            strParesOperandos = "Bases (" + itemOperacion.Nombre + info + "): ";
                                                            strParesOperandos2 = "Bases (" + itemOperacion.Nombre + info + "): ";
                                                            strOperandoPares += "Exponente: " + operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos.ToString();
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    if (utilizandoPrimeraCantidad)
                                                        strParesOperandos = "Bases (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                    else
                                                        strParesOperandos = "Exponentes (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";

                                                    if (utilizandoPrimeraCantidad)
                                                        strParesOperandos2 = "Bases (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                    else
                                                        strParesOperandos2 = "Exponentes (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                }
                                            }
                                            else
                                            {
                                                strParOperando = string.Empty;
                                                if (utilizandoPrimeraCantidad)
                                                    strParOperando = "Base: ";
                                                else
                                                    strParOperando = "Exponente: ";
                                            }
                                        }
                                        else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Raiz)
                                        {
                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                            {
                                                if (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosRaiz != TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos)
                                                {
                                                    switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosRaiz)
                                                    {
                                                        case TipoOpcionElementosFijosOperacionRaiz.RaizFijaRadicalOperando:
                                                            strOperandoPares += "Raíz: " + operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos.ToString();
                                                            strParesOperandos = "Radicales (" + itemOperacion.Nombre + info + "): ";
                                                            strParesOperandos2 = "Radicales (" + itemOperacion.Nombre + info + "): ";
                                                            break;

                                                        case TipoOpcionElementosFijosOperacionRaiz.RaizOperandoRadicalFijo:
                                                            strParesOperandos = "Raíces (" + itemOperacion.Nombre + info + "): ";
                                                            strParesOperandos2 = "Raíces (" + itemOperacion.Nombre + info + "): ";
                                                            strOperandoPares += "Radical: " + operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos.ToString();
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    if (utilizandoPrimeraCantidad)
                                                        strParesOperandos = "Raíces (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                    else
                                                        strParesOperandos = "Radicales (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";

                                                    if (utilizandoPrimeraCantidad)
                                                        strParesOperandos2 = "Raíces (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                    else
                                                        strParesOperandos2 = "Radicales (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                }
                                            }
                                            else
                                            {
                                                strParOperando = string.Empty;
                                                if (utilizandoPrimeraCantidad)
                                                    strParOperando = "Raíz: ";
                                                else
                                                    strParOperando = "Radical: ";
                                            }
                                        }
                                        else if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Porcentaje)
                                        {
                                            if (utilizandoPrimeraCantidad)
                                                strParesOperandos = "Porcentajes a calcular (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                            else
                                                strParesOperandos = "Cantidades de donde se calcularán (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";

                                            if (utilizandoPrimeraCantidad)
                                                strParesOperandos2 = "Porcentajes a calcular (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                            else
                                                strParesOperandos2 = "Cantidades de donde se calcularán (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";

                                        }
                                        else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Logaritmo)
                                        {
                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                            {
                                                if (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)
                                                {
                                                    switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo)
                                                    {
                                                        case TipoOpcionElementosFijosOperacionLogaritmo.BaseFijaArgumentoOperando:
                                                            strOperandoPares += "Base: " + operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos.ToString();
                                                            strParesOperandos = "Argumentos (" + itemOperacion.Nombre + info + "): ";
                                                            strParesOperandos2 = "Argumentos (" + itemOperacion.Nombre + info + "): ";
                                                            break;

                                                        case TipoOpcionElementosFijosOperacionLogaritmo.BaseOperandoArgumentoFijo:
                                                            strParesOperandos = "Bases (" + itemOperacion.Nombre + info + "): ";
                                                            strParesOperandos2 = "Bases (" + itemOperacion.Nombre + info + "): ";
                                                            strOperandoPares += "Argumento: " + operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos.ToString();
                                                            break;

                                                        case TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural:
                                                            strParesOperandos = "Base e: ";
                                                            strParesOperandos2 = "Base e: ";
                                                            strOperandoPares += "Argumento: " + operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos.ToString();

                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    if (utilizandoPrimeraCantidad)
                                                        strParesOperandos = "Bases (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                    else
                                                        strParesOperandos = "Argumentos (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";

                                                    if (utilizandoPrimeraCantidad)
                                                        strParesOperandos2 = "Bases (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                    else
                                                        strParesOperandos2 = "Argumentos (" + itemOperacion.Nombre + info + itemOperacion.ObtenerTextosInformacion_Cadena() + "): ";
                                                }
                                            }
                                            else
                                            {
                                                strParOperando = string.Empty;
                                                if (utilizandoPrimeraCantidad)
                                                    strParOperando = "Base: ";
                                                else
                                                    strParOperando = "Argumento: ";
                                            }
                                        }

                                        List<EntidadNumero> ListaOriginal = itemOperacion.Numeros.ToList();

                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                            ProcesarNumerosOperando_AntesEjecucion(operacion);

                                        List<EntidadNumero> ListaNumeros = new List<EntidadNumero>();

                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                        {
                                            ListaNumeros = itemOperacion.Numeros.Where(
                                            i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)).ToList();
                                        }
                                        else
                                        {
                                            EntidadNumero numeroFila = null;

                                            if (IndiceNumeroActualElementoOperacion < itemOperacion.Numeros.Count(i => i.Clasificadores_SeleccionarOrdenar
                                                    .Any(i => i.CadenaTexto == itemOperacion.Clasificadores_Cantidades[indiceClasificadores].CadenaTexto) &&
                                                    (!itemOperacion.NumerosFiltrados.Any() ||
                                                            itemOperacion.NumerosFiltrados.Contains(i))))
                                            {
                                                numeroFila = itemOperacion.Numeros.Where((i =>
                                                            i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemOperacion.Clasificadores_Cantidades[indiceClasificadores].CadenaTexto) &&
                                                            (!itemOperacion.NumerosFiltrados.Any() ||
                                                    itemOperacion.NumerosFiltrados.Contains(i)))).ToList()[IndiceNumeroActualElementoOperacion];
                                            }
                                            else
                                            {
                                                if (operacion.ElementoDiseñoRelacionado.SeguirOperandoFilas_ConUltimoNumero)
                                                {
                                                    numeroFila = itemOperacion.Numeros.LastOrDefault(i => i.Clasificadores_SeleccionarOrdenar
                                                                    .Any(i => i.CadenaTexto == itemOperacion.Clasificadores_Cantidades[indiceClasificadores].CadenaTexto) &&
                                                                    (!itemOperacion.NumerosFiltrados.Any() ||
                                                    itemOperacion.NumerosFiltrados.Contains(i)));
                                                }
                                                else if (operacion.ElementoDiseñoRelacionado.SeguirOperandoFilas_ConElementoNeutro)
                                                {
                                                    numeroFila = operacion.ObtenerValorElementoNeutro(itemOperacion.Numeros.LastOrDefault(i => i.Clasificadores_SeleccionarOrdenar
                                                                    .Any(i => i.CadenaTexto == itemOperacion.Clasificadores_Cantidades[indiceClasificadores].CadenaTexto) &&
                                                                    (!itemOperacion.NumerosFiltrados.Any() ||
                                                    itemOperacion.NumerosFiltrados.Contains(i))));
                                                    
                                                }
                                            }

                                            if (numeroFila != null)
                                            {
                                                ListaNumeros = new List<EntidadNumero>() { numeroFila };
                                            }
                                        }

                                        int indiceLista = 0;
                                        int cantidadCantidades = NumerosResultado.Count - 1;

                                        for (indiceLista = 0; indiceLista <= ListaNumeros.Count - 1; indiceLista++)
                                        {
                                            if (Pausada) Pausar();
                                            if (Detenida) return;

                                            int indiceCantidad = indiceClasificadores + cantidadNumeros_PorSeparado + cantidadNumerosAgregados_Pares;
                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                            {
                                                indiceCantidad = IndiceNumeroActualElementoOperacion +
                                                    (conAgrupaciones ? cantidadTotalNumerosAgrupados : 0) + indiceAgrupacionesPorFilas +
                                                    cantidadNumerosAgregados_Pares;
                                            }
                                            else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                            {
                                                if(operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.ContarCantidades)
                                                    indiceCantidad = cantidadCantidades + indiceLista;
                                            }

                                            EntidadNumero numero = ListaNumeros[indiceLista];

                                            int indiceInsercion = -1;

                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                            {
                                                operacion.LimpiarEstadoProcesamiento();

                                                if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                                                {
                                                    operacion.ProcesarCantidades(this, operacion, itemOperacion, numero, NumerosResultado[indiceCantidad], itemClasificador, NumerosResultado);
                                                }

                                                foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                {
                                                    var subItem_Insertar = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                        operacion, ListaNumeros, indiceLista, ref indiceInsercion);

                                                    if (itemResultado.InsertarCantidad_Procesamiento_Operandos &&
                                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                                    {
                                                        itemResultado.subItem_InsertarOperando_Asociado = subItem_Insertar;
                                                    }
                                                }
                                            }

                                            do
                                            {

                                                if ((itemOperacion.Tipo == TipoElementoEjecucion.Entrada && ((!numero.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                            (numero.ElementosSalidaOperacion_Agrupamiento.Contains(operacion)))) ||

                                            (itemOperacion.Tipo == TipoElementoEjecucion.OperacionAritmetica && (
                                                ((!numero.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                                                    (numero.ElementosSalidaOperacion_Agrupamiento.Contains(operacion))) &
                                                                    ((!numero.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                                                    (numero.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion))) &
                                                                    ((!numero.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
                                                                    (numero.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))))))
                                                {
                                                    if (!itemOperacion.NumerosFiltrados.Any() || (itemOperacion.NumerosFiltrados.Any() & itemOperacion.NumerosFiltrados.Contains(numero)))
                                                    {
                                                        if ((operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos ||
                                                            operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas) ||

                                                            ((operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado ||
                                                            operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas) &&
                                                            ((!operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any() ||
                    (operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any(j =>
                                                itemOperacion.ElementoDiseñoRelacionado.ObtenerAgrupaciones().Any(
                                                    i => i == j.Agrupacion &&
                                                    i.NombreAgrupacion == j.Agrupacion.NombreAgrupacion &&
                                                    j.OperandoAsociado == itemOperacion.ElementoDiseñoRelacionado &&
                                                    j.ElementoAsociado == operacion.ElementoDiseñoRelacionado &&
                                                    j.Agrupacion.NombreAgrupacion == numero.Agrupacion_PorSeparado)))))))
                                                        {
                                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas ||
                                                                (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas &&
                                                                ((!numero.EsCantidadQuitada_ProcesamientoCantidades &&
                                                                        !numero.EsCantidadInsertada_ProcesamientoCantidades) ||
                                                                        (numero.EsCantidadInsertada_ProcesamientoCantidades))))
                                                            {
                                                                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                                                {
                                                                    AgregarClasificador_Cantidad(operacion, itemClasificador, NumerosResultado[indiceCantidad]);

                                                                    if (operacion.ElementoDiseñoRelacionado.ConAcumulacion)
                                                                    {
                                                                        NumerosResultado[indiceCantidad].Numero = cantidadAcumulada;
                                                                    }
                                                                }

                                                                List<EntidadNumero> numeroInsertado_Operandos = new List<EntidadNumero>();
                                                                List<EntidadNumero> numeroInsertado_Resultados = new List<EntidadNumero>();

                                                                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas &&
                                                                    operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarTodosJuntos &&
                                                                    operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorSeparado)
                                                                {
                                                                    InsertarProcesarCantidades_Operacion(operacion,
                                                                        itemOperacion, numero,
                                                                        NumerosResultado[indiceClasificadores], numeroInsertado_Operandos, numeroInsertado_Resultados, ListaNumeros, NumerosResultado, indiceInsercion,
                                                                        ref indiceClasificadores, ref indiceCantidad, ref indiceLista, ref cantidadNumeros_PorSeparado,
                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion &&
                                                                        operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones);

                                                                    foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                                    {
                                                                        if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                        {
                                                                            itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                            ListaNumeros.RemoveAt(indiceLista);
                                                                            numero = ListaNumeros[indiceLista];
                                                                        }
                                                                    }
                                                                }

                                                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                {
                                                                    strMensajeLogResultados += numero.Nombre + ", " + numero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numero.ObtenerTextosInformacion_Cadena() + "\n";
                                                                    strMensajeLog += numero.Nombre + ", " + numero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numero.ObtenerTextosInformacion_Cadena() + "\n";
                                                                }

                                                                bool esPrimero = operacion.EsPrimerNumero(itemOperacion, numero, itemClasificador,
                                                                    conAgrupacionRecorrida);

                                                                switch (operacion.TipoOperacion)
                                                                {
                                                                    case TipoOperacionAritmeticaEjecucion.Suma:

                                                                        NumerosResultado[indiceCantidad].Numero += numero.Numero;

                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Resta:

                                                                        if (esPrimero || (conAgrupaciones && conCambioAgrupacion))
                                                                        {
                                                                            NumerosResultado[indiceCantidad].Numero = numero.Numero;
                                                                        }
                                                                        else
                                                                        {
                                                                            NumerosResultado[indiceCantidad].Numero -= numero.Numero;
                                                                        }

                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Multiplicacion:

                                                                        if (esPrimero || (conAgrupaciones && conCambioAgrupacion))
                                                                        {
                                                                            NumerosResultado[indiceCantidad].Numero = numero.Numero;
                                                                        }
                                                                        else
                                                                        {
                                                                            NumerosResultado[indiceCantidad].Numero *= numero.Numero;
                                                                        }

                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Division:

                                                                        if (esPrimero || (conAgrupaciones && conCambioAgrupacion))
                                                                        {
                                                                            NumerosResultado[indiceCantidad].Numero = numero.Numero;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (numero.Numero != 0)
                                                                            {
                                                                                NumerosResultado[indiceCantidad].Numero /= numero.Numero;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (!operacion.ElementoDiseñoRelacionado.DivisionZero_Continuar)
                                                                                {
                                                                                    try {  } catch (Exception) { }
                                                                                    ;
                                                                                    ConError = true;

                                                                                    log.Add("Error de división por cero.");

                                                                                    Detener();
                                                                                    return;
                                                                                }
                                                                            }
                                                                        }

                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Potencia:

                                                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas &&
                                                                            operacion.ElementoDiseñoRelacionado.OpcionElementosFijosPotencia != TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos)
                                                                        {
                                                                            switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosPotencia)
                                                                            {
                                                                                case TipoOpcionElementosFijosOperacionPotencia.BaseOperandoExponenteFijo:

                                                                                    primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(NumerosResultado[indiceClasificadores + cantidadNumerosAgregados_Pares]))
                                                                                    {
                                                                                        strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                    }

                                                                                    break;

                                                                                case TipoOpcionElementosFijosOperacionPotencia.BaseFijaExponenteOperando:

                                                                                    segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(NumerosResultado[indiceClasificadores + cantidadNumerosAgregados_Pares]))
                                                                                    {
                                                                                        strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                    }

                                                                                    break;
                                                                            }
                                                                        }
                                                                        else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                                                        {
                                                                            if (utilizandoPrimeraCantidad)
                                                                            {
                                                                                primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(NumerosResultado[indiceClasificadores + cantidadNumerosAgregados_Pares]))
                                                                                {
                                                                                    strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(NumerosResultado[indiceClasificadores + cantidadNumerosAgregados_Pares]))
                                                                                {
                                                                                    strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                }
                                                                            }
                                                                        }
                                                                        else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                                        {
                                                                            if (utilizandoPrimeraCantidad)
                                                                            {
                                                                                primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                            }
                                                                            else
                                                                            {
                                                                                segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                            }
                                                                        }

                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Raiz:

                                                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas &&
                                                                            operacion.ElementoDiseñoRelacionado.OpcionElementosFijosRaiz != TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos)
                                                                        {
                                                                            switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosRaiz)
                                                                            {
                                                                                case TipoOpcionElementosFijosOperacionRaiz.RaizOperandoRadicalFijo:

                                                                                    segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                    {
                                                                                        strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                    }

                                                                                    break;

                                                                                case TipoOpcionElementosFijosOperacionRaiz.RaizFijaRadicalOperando:

                                                                                    primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                    {
                                                                                        strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                    }

                                                                                    break;
                                                                            }
                                                                        }
                                                                        else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                                                        {
                                                                            if (utilizandoPrimeraCantidad)
                                                                            {
                                                                                primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                {
                                                                                    strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                {
                                                                                    strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                }
                                                                            }
                                                                        }
                                                                        else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                                        {
                                                                            if (utilizandoPrimeraCantidad)
                                                                            {
                                                                                primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                            }
                                                                            else
                                                                            {
                                                                                segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                            }
                                                                        }


                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Porcentaje:
                                                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                                                        {
                                                                            if (utilizandoPrimeraCantidad)
                                                                            {
                                                                                primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                {
                                                                                    strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                {
                                                                                    strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                }
                                                                            }
                                                                        }
                                                                        else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                                        {
                                                                            if (utilizandoPrimeraCantidad)
                                                                            {
                                                                                primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                            }
                                                                            else
                                                                            {
                                                                                segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                            }
                                                                        }
                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Logaritmo:

                                                                        if (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural)
                                                                        {
                                                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas &&
                                                                                operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)
                                                                            {
                                                                                switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo)
                                                                                {
                                                                                    case TipoOpcionElementosFijosOperacionLogaritmo.BaseOperandoArgumentoFijo:

                                                                                        primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                        if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                        {
                                                                                            strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                        }

                                                                                        break;

                                                                                    case TipoOpcionElementosFijosOperacionLogaritmo.BaseFijaArgumentoOperando:

                                                                                        segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                        if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                        {
                                                                                            strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                        }

                                                                                        break;
                                                                                }
                                                                            }
                                                                            else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                                                            {
                                                                                if (utilizandoPrimeraCantidad)
                                                                                {
                                                                                    primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                    {
                                                                                        strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                                                    {
                                                                                        strParesOperandos += numero.Nombre + numero.ObtenerTextosInformacion_Cadena() + ", ";
                                                                                    }
                                                                                }
                                                                            }
                                                                            else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                                            {
                                                                                if (utilizandoPrimeraCantidad)
                                                                                {
                                                                                    primerasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                }
                                                                                else
                                                                                {
                                                                                    segundasCantidades_ParesOperandos.Add(numero.CopiarObjeto(false, itemClasificador));

                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            numero.Numero = Math.Log(numero.Numero);
                                                                        }

                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Inverso:

                                                                        switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosInverso)
                                                                        {
                                                                            case TipoOpcionElementosFijosOperacionInverso.InversoSumaResta:
                                                                                NumerosResultado[indiceCantidad].Numero = numero.Numero * (double)(-1);
                                                                                break;

                                                                            case TipoOpcionElementosFijosOperacionInverso.InversoMultiplicacionDivision:
                                                                                NumerosResultado[indiceCantidad].Numero = (double)(1) / numero.Numero;
                                                                                break;
                                                                        }

                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.Factorial:
                                                                        NumerosResultado[indiceCantidad].Numero = Factorial(numero.Numero, log);
                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.RedondearCantidades:
                                                                        NumerosResultado[indiceCantidad] = numero.CopiarObjeto(false, itemClasificador);
                                                                        NumerosResultado[indiceCantidad].Redondear(operacion.ElementoDiseñoRelacionado.ConfigRedondeo);

                                                                        break;

                                                                    case TipoOperacionAritmeticaEjecucion.ContarCantidades:
                                                                        NumerosResultado[indiceCantidad].Numero += 1;

                                                                        break;
                                                                }

                                                                foreach (var itemNumeroInsertado in numeroInsertado_Resultados)
                                                                {
                                                                    var itemResultado = operacion.ResultadosCondiciones_ProcesamientoCantidades.FirstOrDefault(i => i.NumeroInsertado_Asociado == itemNumeroInsertado);

                                                                    if (itemNumeroInsertado != null && itemResultado != null &&
                                                                                            !itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                                                                        itemNumeroInsertado.Numero = itemResultado.Operar_ProcesamientoCantidades(itemNumeroInsertado.Numero, NumerosResultado, operacion);
                                                                }

                                                                operacion.SetearNumeroResultadoSiguiente_Agrupaciones(
                                                                           conAgrupaciones, NumerosResultado, indiceCantidad);

                                                                if (!(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Potencia &&
                                                               utilizandoPrimeraCantidad) &&
                                                               !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Raiz &&
                                                               utilizandoPrimeraCantidad) &&
                                                               !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Porcentaje &&
                                                               utilizandoPrimeraCantidad) &&
                                                               !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Logaritmo &&
                                                               operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural &&
                                                               utilizandoPrimeraCantidad))
                                                                {
                                                                    if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones)
                                                                    {
                                                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                                                                        {

                                                                            InsertarProcesarCantidades_Operacion(operacion,
                                                                                itemOperacion, numero,
                                                                                NumerosResultado[indiceClasificadores], numeroInsertado_Operandos, numeroInsertado_Resultados,
                                                                                ListaNumeros, NumerosResultado, indiceInsercion,
                                                                                ref indiceClasificadores, ref indiceCantidad, ref indiceLista, ref cantidadNumeros_PorSeparado,
                                                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion &&
                                                                                operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones);

                                                                            foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                                            {
                                                                                if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                                {
                                                                                    itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                                    ListaNumeros.RemoveAt(indiceLista);
                                                                                    numero = ListaNumeros[indiceLista];
                                                                                }
                                                                            }

                                                                        }
                                                                        else
                                                                        {
                                                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                                                            {
                                                                                InsertarProcesarCantidades_Operacion(operacion,
                                                                                    itemOperacion, numero,
                                                                                    NumerosResultado[indiceClasificadores], numeroInsertado_Operandos, numeroInsertado_Resultados,
                                                                                    ListaNumeros, NumerosResultado, indiceInsercion,
                                                                                    ref indiceClasificadores, ref indiceCantidad, ref indiceLista, ref cantidadNumeros_PorSeparado,
                                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion &&
                                                                                    operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones);

                                                                                foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                                                {
                                                                                    if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                                    {
                                                                                        itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                                        ListaNumeros.RemoveAt(indiceLista);
                                                                                        numero = ListaNumeros[indiceLista];
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                    ProcesarTextos_IteracionEjecucion(operacion, itemOperacion, numero, NumerosResultado[indiceCantidad], NumerosResultado,
                                                                    true, operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                                                                    operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas ? 
                                                                    indiceCantidad : indiceLista, 
                                                                    operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                                                                    operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas ? true : false, 
                                                                    operacionInterna, operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesEjecucion,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_AntesEjecucion,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesImplicaciones,
                                                                        operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_AntesEjecucion, 0, itemClasificador);

                                                                if (!(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Potencia &&
                                                               utilizandoPrimeraCantidad) &&
                                                               !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Raiz &&
                                                               utilizandoPrimeraCantidad) &&
                                                               !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Porcentaje &&
                                                               utilizandoPrimeraCantidad) &&
                                                               !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Logaritmo &&
                                                               operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural &&
                                                               utilizandoPrimeraCantidad))
                                                                {
                                                                    if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones)
                                                                    {
                                                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                                                                        {

                                                                            InsertarProcesarCantidades_Operacion(operacion,
                                                                                itemOperacion, numero,
                                                                                NumerosResultado[indiceClasificadores], numeroInsertado_Operandos, numeroInsertado_Resultados,
                                                                                ListaNumeros, NumerosResultado, indiceInsercion,
                                                                                ref indiceClasificadores, ref indiceCantidad, ref indiceLista, ref cantidadNumeros_PorSeparado,
                                                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion &&
                                                                                operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones);

                                                                            foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                                            {
                                                                                if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                                {
                                                                                    itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                                    ListaNumeros.RemoveAt(indiceLista);
                                                                                    numero = ListaNumeros[indiceLista];
                                                                                }
                                                                            }

                                                                        }
                                                                        else
                                                                        {
                                                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                                                            {
                                                                                InsertarProcesarCantidades_Operacion(operacion,
                                                                                    itemOperacion, numero,
                                                                                    NumerosResultado[indiceClasificadores], numeroInsertado_Operandos, numeroInsertado_Resultados,
                                                                                    ListaNumeros, NumerosResultado, indiceInsercion,
                                                                                    ref indiceClasificadores, ref indiceCantidad, ref indiceLista, ref cantidadNumeros_PorSeparado,
                                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion &&
                                                                                    operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones);

                                                                                foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                                                {
                                                                                    if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                                    {
                                                                                        itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                                        ListaNumeros.RemoveAt(indiceLista);
                                                                                        numero = ListaNumeros[indiceLista];
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                if (!(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Potencia &&
                                                                    utilizandoPrimeraCantidad) &&
                                                                    !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Raiz &&
                                                                    utilizandoPrimeraCantidad) &&
                                                                    !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Porcentaje &&
                                                                    utilizandoPrimeraCantidad) &&
                                                                    !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Logaritmo &&
                                                                    operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural &&
                                                                    utilizandoPrimeraCantidad))
                                                                {
                                                                    conNumeros = true;

                                                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                                                                    {
                                                                        itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                                                        operacion.AumentarPosicionesElementosExterioresCondiciones(this);
                                                                        operacion.AumentarPosicionesElementosOperandos(this, itemOperacion);
                                                                    }
                                                                    else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                                                    {
                                                                        if (conNumeros) ElementosNoConjuntos = true;

                                                                        ProcesarFilaResultado_PorFilas_IteracionOperacion(operacion, NumerosResultado[indiceCantidad], NumerosResultado,
                                                                            numero, itemOperacion, ref indiceCantidad,
                                                                            ref CantidadNumeros, FlagDetenerProcesamiento, ref agregarNumero, cantidadNumeros_Fila, ElementosNoConjuntos, ref cantidadAcumulada,
                                                                            ref indiceClasificadores, ref indiceNumero, ref sinOperandos, ref strMensajeLog, ref strMensajeLogResultados, ref operandoConNumeros,
                                                                            operacionInterna);
                                                                        
                                                                        itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                                                        operacion.AumentarPosicionesElementosExterioresCondiciones(this);
                                                                        operacion.AumentarPosicionesElementosOperandos(this, itemOperacion);
                                                                    }
                                                                }
                                                                else if ((operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Potencia &&
                                                                    utilizandoPrimeraCantidad) ||
                                                                    (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Raiz &&
                                                                    utilizandoPrimeraCantidad) ||
                                                                    (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Porcentaje &&
                                                                    utilizandoPrimeraCantidad) ||
                                                                    (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Logaritmo &&
                                                                    operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural &&
                                                                    utilizandoPrimeraCantidad))
                                                                {
                                                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                                    {
                                                                        if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(NumerosResultado[indiceCantidad]))
                                                                        {
                                                                            strOperandoPares += strParOperando + " " + NumerosResultado[indiceCantidad].Nombre + NumerosResultado[indiceCantidad].ObtenerTextosInformacion_Cadena();
                                                                        }

                                                                        strOperandosResultados += strOperandoPares2 + " ";
                                                                        strMensajeLog += strOperandoPares + "\n";
                                                                        strMensajeLogResultados += strOperandoPares + "\n";
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                                {
                                                                    operandoConNumeros = false;
                                                                    agregarNumero = false;
                                                                    NumerosResultado[indiceClasificadores].AgregarNumero_PorFilas = false;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                            {
                                                                operandoConNumeros = false;
                                                                agregarNumero = false;
                                                                NumerosResultado[indiceClasificadores].AgregarNumero_PorFilas = false;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                        {
                                                            operandoConNumeros = false;
                                                            agregarNumero = false;
                                                            NumerosResultado[indiceClasificadores].AgregarNumero_PorFilas = false;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                                    {
                                                        operandoConNumeros = false;
                                                        agregarNumero = false;
                                                        NumerosResultado[indiceClasificadores].AgregarNumero_PorFilas = false;
                                                    }
                                                }

                                                //if (!(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Potencia) &&
                                                //    !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Raiz) &&
                                                //    !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Porcentaje) &&
                                                //    !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Logaritmo &&
                                                //        operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural))
                                                //{
                                                //    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorSeparadoPorFilas &&
                                                //        operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                                //    {
                                                //        itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                                //        operacion.AumentarPosicionesElementosExterioresCondiciones(this);
                                                //    }
                                                //}

                                                if (operacion.DetenerProcesamiento)
                                                    break;

                                            } while (operacion.MantenerProcesamiento_Operandos |
                                                                        operacion.MantenerProcesamiento_Resultados);

                                            if (operacion.DetenerProcesamiento)
                                                break;

                                            
                                        }

                                        if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones)
                                        {
                                            operacion.InicializarPosicionesElementosExterioresCondiciones(this);
                                            operacion.InicializarPosicionesElementosOperandos(this, itemOperacion);
                                        }

                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas &
                                            operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarTodosJuntos)
                                            ProcesarNumerosOperando_DespuesEjecucion(operacion, itemOperacion, ListaOriginal, ListaNumeros, itemClasificador);

                                        if (itemOperacion.Numeros.Count > 0) strMensajeLogResultados += "\n";
                                        if (itemOperacion.Numeros.Count > 0) strMensajeLog += "\n";

                                        if (operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Potencia &&
                                            operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Raiz &&
                                            operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Porcentaje &&
                                            !(operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Logaritmo &&
                                                operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural))
                                        {
                                            if (!conNumeros) operandoConNumeros = false;
                                        }
                                        else
                                        {
                                            bool setearTextos = false;

                                            if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Potencia &&
                                                operacion.ElementoDiseñoRelacionado.OpcionElementosFijosPotencia != TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos)
                                            {
                                                switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosPotencia)
                                                {
                                                    case TipoOpcionElementosFijosOperacionPotencia.BaseOperandoExponenteFijo:
                                                        segundasCantidades_ParesOperandos.Add(new EntidadNumero(string.Empty, operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos));

                                                        break;

                                                    case TipoOpcionElementosFijosOperacionPotencia.BaseFijaExponenteOperando:
                                                        primerasCantidades_ParesOperandos.Add(new EntidadNumero(string.Empty, operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos));

                                                        break;
                                                }

                                                setearTextos = true;
                                            }

                                            else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Raiz &&
                                                operacion.ElementoDiseñoRelacionado.OpcionElementosFijosRaiz != TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos)
                                            {
                                                switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosRaiz)
                                                {
                                                    case TipoOpcionElementosFijosOperacionRaiz.RaizOperandoRadicalFijo:
                                                        primerasCantidades_ParesOperandos.Add(new EntidadNumero(string.Empty, operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos));

                                                        break;

                                                    case TipoOpcionElementosFijosOperacionRaiz.RaizFijaRadicalOperando:
                                                        segundasCantidades_ParesOperandos.Add(new EntidadNumero(string.Empty, operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos));

                                                        break;
                                                }

                                                setearTextos = true;
                                            }
                                            else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Logaritmo &&
                                                operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)
                                            {
                                                switch (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo)
                                                {
                                                    case TipoOpcionElementosFijosOperacionLogaritmo.BaseOperandoArgumentoFijo:
                                                        segundasCantidades_ParesOperandos.Add(new EntidadNumero(string.Empty, operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos));

                                                        break;

                                                    case TipoOpcionElementosFijosOperacionLogaritmo.BaseFijaArgumentoOperando:
                                                        primerasCantidades_ParesOperandos.Add(new EntidadNumero(string.Empty, operacion.ElementoDiseñoRelacionado.ValorOpcionElementosFijos));

                                                        break;
                                                }

                                                setearTextos = true;
                                            }


                                            if (setearTextos)
                                            {
                                                strParesOperandos = strParesOperandos.Trim().Remove(strParesOperandos.Trim().Length - 1) + " ";
                                                strParesOperandos2 = strParesOperandos2.Trim().Remove(strParesOperandos2.Trim().Length - 1) + " ";

                                                strOperandoPares += strParesOperandos;
                                                strOperandoPares2 += strParesOperandos2;

                                                strMensajeLog += strOperandoPares + "\n";
                                                strMensajeLogResultados += strOperandoPares + "\n";

                                                strOperandosResultados += strOperandoPares2 + " ";
                                            }
                                        }

                                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                                        {

                                            bool procesarResultados = false;

                                            if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Potencia)
                                            {
                                                if (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosPotencia != TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos)
                                                {
                                                    procesarResultados = true;
                                                }
                                            }
                                            else if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Raiz)
                                            {
                                                if (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosRaiz != TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos)
                                                {
                                                    procesarResultados = true;
                                                }
                                            }
                                            //else if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Porcentaje)
                                            //{
                                            //    procesarResultados = true;
                                            //}
                                            else if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Logaritmo &&
                                                operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural)
                                            {
                                                if (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)
                                                {
                                                    procesarResultados = true;
                                                }
                                            }
                                            else
                                            {
                                                if (operandoConNumeros) sinOperandos = false;
                                            }

                                            if (procesarResultados)
                                            {
                                                ProcesarResultados_OperacionPorPares(primerasCantidades_ParesOperandos, segundasCantidades_ParesOperandos,
                                                        operacion, itemOperacion, NumerosResultado, ref utilizandoPrimeraCantidad, ref indiceClasificadores, ref strMensajeLog,
                                                        ref strMensajeLogResultados, ref indiceNumero, ref strOperandosResultados, ref indiceResultadosMensaje,
                                                        ref strOperandoPares, ref operandoConNumeros, ref sinOperandos, ref cantidadNumerosAgregados_Pares);
                                            }
                                            else
                                            {
                                                if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Potencia ||
                                                    operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Raiz ||
                                                    operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Porcentaje ||
                                                    (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Logaritmo &&
                                                    operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural))
                                                {
                                                    if ((operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.Porcentaje &&
                                                        (operacion.ElementoDiseñoRelacionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos ||
                                                        operacion.ElementoDiseñoRelacionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos ||
                                                        operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)) ||
                                                        operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Porcentaje)
                                                    {
                                                        if (utilizandoPrimeraCantidad)
                                                        {
                                                            strParesOperandos = strParesOperandos.Trim().Remove(strParesOperandos.Trim().Length - 1) + " ";
                                                            strParesOperandos2 = strParesOperandos2.Trim().Remove(strParesOperandos2.Trim().Length - 1) + " ";

                                                            strOperandoPares += strParesOperandos;
                                                            strOperandoPares2 += strParesOperandos2;

                                                            strMensajeLog += strOperandoPares + "\n";
                                                            strMensajeLogResultados += strOperandoPares + "\n";

                                                            strOperandosResultados += strOperandoPares2 + " ";


                                                            utilizandoPrimeraCantidad = false;
                                                        }
                                                        else if (!utilizandoPrimeraCantidad)
                                                        {
                                                            strParesOperandos = strParesOperandos.Trim().Remove(strParesOperandos.Trim().Length - 1) + " ";
                                                            strParesOperandos2 = strParesOperandos2.Trim().Remove(strParesOperandos2.Trim().Length - 1) + " ";

                                                            strOperandoPares += strParesOperandos;
                                                            strOperandoPares2 += strParesOperandos2;

                                                            strMensajeLog += strOperandoPares + "\n";
                                                            strMensajeLogResultados += strOperandoPares + "\n";

                                                            strOperandosResultados += strOperandoPares2 + " ";

                                                            ProcesarResultados_OperacionPorPares(primerasCantidades_ParesOperandos, segundasCantidades_ParesOperandos,
                                                                            operacion, itemOperacion, NumerosResultado, ref utilizandoPrimeraCantidad, ref indiceClasificadores, ref strMensajeLog,
                                                                            ref strMensajeLogResultados, ref indiceNumero, ref strOperandosResultados, ref indiceResultadosMensaje,
                                                                            ref strOperandoPares, ref operandoConNumeros, ref sinOperandos, ref cantidadNumerosAgregados_Pares);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                        {
                                            if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Potencia ||
                                                operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Raiz ||
                                                operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Porcentaje ||
                                                (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Logaritmo &&
                                                operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural))
                                            {
                                                if (!utilizandoPrimeraCantidad)
                                                {
                                                    ProcesarFilaResultado_PorFilas_IteracionOperacion_PorPares(operacion, itemOperacion, primerasCantidades_ParesOperandos,
                                                        segundasCantidades_ParesOperandos, NumerosResultado, IndiceNumeroActualElementoOperacion, ref CantidadNumeros,
                                                        FlagDetenerProcesamiento, ref agregarNumero, cantidadNumeros_Fila, ElementosNoConjuntos, ref cantidadAcumulada,
                                                        ref indiceClasificadores, ref indiceNumero, ref sinOperandos, ref strMensajeLog, ref strMensajeLogResultados, ref operandoConNumeros,
                                                        operacionInterna);

                                                    utilizandoPrimeraCantidad = true;
                                                }
                                                else
                                                    utilizandoPrimeraCantidad = false;
                                            }
                                            if (operandoConNumeros) sinOperandos = false;
                                            if (conNumeros) ElementosNoConjuntos = true;

                                        }
                                        else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado)
                                        {
                                            foreach(var itemNumeroResultado in NumerosResultado)
                                            {
                                                if (itemNumeroResultado.EsCantidadInsertada_ProcesamientoCantidadesTemp)
                                                {
                                                    cantidadNumeros_PorSeparado++;
                                                    itemNumeroResultado.EsCantidadInsertada_ProcesamientoCantidadesTemp = false;
                                                }
                                            }

                                            if (NumerosResultado.Count(i => i == NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado]) > 1)
                                            {
                                                while (NumerosResultado.Count(i => i == NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado]) > 1)
                                                {
                                                    NumerosResultado.Remove(NumerosResultado.FirstOrDefault(i => i.Agrupacion_PorSeparado == NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado].Agrupacion_PorSeparado));
                                                    cantidadNumeros_PorSeparado--;
                                                }
                                            }

                                            AgregarClasificador_Cantidad(operacion, itemClasificador, NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado]);

                                            int cantidadFilas_Adicionales_NoUso = 0;

                                            if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones &&
                                                (conAgrupacionRecorrida || !itemOperacion.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Any()))
                                            {
                                                InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado],
                                                        itemOperacion, NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado], NumerosResultado, ListaNumeros, ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros,
                                                        ref indiceClasificadores, false, ref cantidadNumeros_PorSeparado, ref IndiceNumeroActualElementoOperacion, ref cantidadFilas_Adicionales_NoUso,
                                                        operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                                            }

                                            if (conAgrupacionRecorrida || !itemOperacion.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Any())
                                            {
                                                ProcesarTextos_IteracionEjecucion(operacion, itemOperacion,
                                                NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado], NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado], NumerosResultado,
                                                true, indiceLista, false, operacionInterna, operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesEjecucion,
                                                operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_DespuesEjecucion,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesImplicaciones,
                                                                        operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_DespuesEjecucion, 0, itemClasificador);
                                            }

                                            if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones &&
                                                (conAgrupacionRecorrida || !itemOperacion.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Any()))
                                            {
                                                InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado],
                                                        itemOperacion, NumerosResultado[indiceClasificadores + cantidadNumeros_PorSeparado], NumerosResultado, ListaNumeros, ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros,
                                                        ref indiceClasificadores, false, ref cantidadNumeros_PorSeparado, ref IndiceNumeroActualElementoOperacion, ref cantidadFilas_Adicionales_NoUso,
                                                        operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                                            }

                                            itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                            operacion.AumentarPosicionesElementosExterioresCondiciones(this);
                                            operacion.AumentarPosicionesElementosOperandos(this, itemOperacion);

                                            if (itemOperacion != operacion.ElementosOperacion.Where(i => !i.ElementoDiseñoRelacionado.NoConservarCambiosOperandos_ProcesamientoCantidades).LastOrDefault())
                                            {                                                
                                                NumerosResultado.Add(new EntidadNumero());
                                                cantidadNumeros_PorSeparado++;
                                            }

                                            indiceNumero++;

                                            operacion.CantidadSubElementosProcesados++;
                                            if (operandoConNumeros) sinOperandos = false;
                                        }
                                        else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                        {
                                            indiceNumero++;
                                        }

                                        if (operacion.DetenerProcesamiento)
                                            break;
                                    }

                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                                        operacion.AumentarPosicionesClasificadores_Operandos();
                                }

                                if (!iteracionAnulada_ItemOperacion)
                                {
                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                    {
                                        strMensajeLogResultados += "\n";
                                        strMensajeLog += "\n";
                                    }

                                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                    {
                                        if (conAgrupaciones)
                                        {
                                            conCambioAgrupacion = operacion.ObtenerCantidadElementosAgrupados_CambioAgrupacion(itemOperacion);
                                            indiceAgrupacionesPorFilas += (conCambioAgrupacion ? 1 : 0);
                                        }
                                    }
                                }
                            }

                            if (operacion.ElementosOperacion.Count(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion) == cantidadIteracionesAnuladas_ItemsOperacion)
                                iteracionAnulada = true;

                            if (!iteracionAnulada)
                            {
                                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                                {
                                    if (!(operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Potencia ||
                                        operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Raiz ||
                                        operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Porcentaje ||
                                        (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Logaritmo &&
                                        operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural)))
                                    {
                                        int cantidadFilas_Adicionales_NoUso = 0;

                                        if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones)
                                        {
                                            bool conNumerosNoUso = false;

                                            foreach (var itemOperacionNumero in operacion.ElementosOperacion.Where(i => !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                                            {
                                                InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, NumerosResultado[indiceClasificadores],
                                                        itemOperacionNumero, NumerosResultado[indiceClasificadores], NumerosResultado, itemOperacionNumero.Numeros, 
                                                        ref strMensajeLog, ref strMensajeLogResultados, ref conNumerosNoUso,
                                                        ref indiceClasificadores, false, ref cantidadNumeros_PorSeparado, ref IndiceNumeroActualElementoOperacion, ref cantidadFilas_Adicionales_NoUso,
                                                        operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                                            }
                                        }
                                        //foreach (var itemOperacion in operacion.ElementosOperacion)
                                        {
                                            ProcesarTextos_IteracionEjecucion(operacion, null,
                                                NumerosResultado[indiceClasificadores], NumerosResultado[indiceClasificadores], NumerosResultado,
                                                true, indiceClasificadores, false, operacionInterna, operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesEjecucion,
                                                true, operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones,
                                                operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesImplicaciones,
                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_DespuesEjecucion, 0, itemClasificador);
                                        }

                                        if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones)
                                        {
                                            bool conNumerosNoUso = false;

                                            foreach (var itemOperacionNumero in operacion.ElementosOperacion.Where(i => !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                                            {
                                                InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, NumerosResultado[indiceClasificadores],
                                                    itemOperacionNumero, NumerosResultado[indiceClasificadores], NumerosResultado, itemOperacionNumero.Numeros, 
                                                    ref strMensajeLog, ref strMensajeLogResultados, ref conNumerosNoUso,
                                                    ref indiceClasificadores, false, ref cantidadNumeros_PorSeparado, ref IndiceNumeroActualElementoOperacion, ref cantidadFilas_Adicionales_NoUso,
                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                                            }
                                        }
                                    }

                                    operacion.AumentarPosicionesClasificadores_Operandos();
                                }

                                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                {
                                    cantidadTotalNumerosAgrupados += cantidadNumerosAgrupados;

                                    if (!((operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Potencia ||
                                                    operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Raiz ||
                                                    operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Porcentaje ||
                                                    (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Logaritmo &&
                                                    operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural))
                                                    && !utilizandoPrimeraCantidad))
                                    {
                                        bool operandoConNumeros_NoUso = false;
                                        ProcesarFilaResultado_PorFilas_IteracionOperacion(operacion, NumerosResultado[IndiceNumeroActualElementoOperacion], NumerosResultado,
                                            null, null, ref IndiceNumeroActualElementoOperacion, ref CantidadNumeros, FlagDetenerProcesamiento, ref agregarNumero,
                                            cantidadNumeros_Fila, ElementosNoConjuntos, ref cantidadAcumulada, ref indiceClasificadores, ref indiceNumero, ref sinOperandos,
                                            ref strMensajeLog, ref strMensajeLogResultados, ref operandoConNumeros_NoUso, operacionInterna);

                                        if ((operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Potencia ||
                                                    operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Raiz ||
                                                    operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Porcentaje ||
                                                    (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.Logaritmo &&
                                                    operacion.ElementoDiseñoRelacionado.OpcionElementosFijosLogaritmo != TipoOpcionElementosFijosOperacionLogaritmo.LogaritmoNatural))
                                            && sinOperandos)
                                            sinOperandos = false;
                                    }
                                }
                                else
                                    IndiceNumeroActualElementoOperacion++;
                            }
                            else
                                break;

                        } while (operacion.MantenerProcesamiento_Resultados |
                        FlagMantenerProcesamiento);

                        if (operacion.DetenerProcesamiento |
                            FlagDetenerProcesamiento)
                            break;

                        if (!iteracionAnulada)
                        {
                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                            {
                                strMensajeLogResultados += "\n";
                                strMensajeLog += "\n";
                            }
                        }
                        else
                            break;
                    }

                    if (!iteracionAnulada)
                    {
                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
                        {
                            foreach (var itemOperacion in operacion.ElementosOperacion)
                            {
                                itemOperacion.FinalIndicePosicionClasificadores = true;
                            }

                            foreach (var itemOperacion in operacion.ElementosOperacion)
                            {
                                itemOperacion.AumentarPosicionesClasificadores_Operandos();
                            }
                        }
                        else
                        {
                            foreach (var itemOperacion in operacion.ElementosOperacion)
                            {
                                if (!itemOperacion.FinalIndicePosicionClasificadores)
                                {
                                    if (itemOperacion.IndicePosicionClasificadores < itemOperacion.Clasificadores_Cantidades.Count)
                                    {
                                        if (IndiceNumeroActualElementoOperacion >= itemOperacion.CantidadNumeros_Clasificador)
                                            itemOperacion.IndicePosicionClasificadores++;
                                    }

                                    if (itemOperacion.IndicePosicionClasificadores == itemOperacion.Clasificadores_Cantidades.Count)
                                    {
                                        itemOperacion.FinalIndicePosicionClasificadores = true;
                                        itemOperacion.IndicePosicionClasificadores = itemOperacion.Clasificadores_Cantidades.Count - 1;
                                    }
                                }
                            }

                            //if (!operacion.ElementosOperacion.Any(i => !i.FinalIndicePosicionClasificadores))
                            //    NumerosResultado.Remove(NumerosResultado.LastOrDefault());

                            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                                ProcesarNumerosOperando_DespuesEjecucion(operacion, null, null, null, null);

                            NumerosResultado.Remove(NumerosResultado.LastOrDefault());

                            NumerosResultadoTotal.AddRange(NumerosResultado.ToList());
                            NumerosResultado.Clear();

                            foreach (var itemOperacion in operacion.ElementosOperacion)
                            {
                                itemOperacion.AumentarPosicionesClasificadores_Operandos();
                            }

                            continue;
                        }

                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                        {
                            if (operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.ContarCantidades)
                                NumerosResultado.Remove(NumerosResultado.LastOrDefault());
                        }
                    }
                    else
                    {
                        NumerosResultado.Remove(NumerosResultado.LastOrDefault());
                        iteracionAnulada = false;
                    }
                }

            }

            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
            {
                NumerosResultadoTotal.AddRange(NumerosResultado.ToList());
                NumerosResultado.Clear();
            }

            operacion.Numeros.AddRange(NumerosResultadoTotal.Where(i => !i.EsCantidadQuitada_ProcesamientoCantidades).ToList());

            foreach(var itemNumero in operacion.Numeros)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                itemNumero.EsCantidadInsertada_ProcesamientoCantidades = false;
            }

            foreach(var itemOperacion in operacion.ElementosOperacion)
            {
                foreach (var itemNumero in itemOperacion.Numeros)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    itemNumero.EsCantidadInsertada_ProcesamientoCantidades = false;
                }
            }

            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                operacion.CantidadSubElementosProcesados++;

            DefinirNombresDespues_Ejecucion(operacion);

            AgregarCantidadesAdicionales(operacion);

            strMensajeLogResultados += "El resultado de " + operacion.Nombre + " es: \n";
            strMensajeLog += "El resultado de " + operacion.Nombre + " es: \n";

            foreach (var itemClasificador in operacion.Clasificadores_Cantidades)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            operacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                {
                    strMensajeLog += "\n" + itemClasificador.CadenaTexto + ":\n";
                    strMensajeLogResultados += "\n" + itemClasificador.CadenaTexto + ":\n";
                }
                else
                {
                    strMensajeLog += "\n";
                    strMensajeLogResultados += "\n";
                }

                foreach (var itemNumero in operacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)))
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemNumero))
                    {
                        strMensajeLogResultados += itemNumero.Nombre + " es: " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                        strMensajeLog += itemNumero.Nombre + " es: " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                    }
                }


            }

            if (operacion.ContieneSalida_Ejecucion && sinOperandos)
                operacion.ContieneSalida_Ejecucion = false;

            if (!operacion.Clasificadores_Cantidades.Any())
            {
                operacion.AgregarClasificadoresGenericos();

            }

            if (operacion.Numeros.Count > 0) strMensajeLogResultados += "\n";
            if (operacion.Numeros.Count > 0) strMensajeLog += "\n";
        }

        private void AgregarClasificador_Cantidad(ElementoOperacionAritmeticaEjecucion operacion, Clasificador itemClasificador, EntidadNumero numero)
        {
            //if (!operacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto) &&
            //                           !operacion.Clasificadores_Cantidades.Contains(itemClasificador))
            
            {
                var clasificadorAAgregar = operacion.Clasificadores_Cantidades.
                    FirstOrDefault(i => i.CadenaTexto == itemClasificador.CadenaTexto);

                if (clasificadorAAgregar == null)
                {
                    clasificadorAAgregar = itemClasificador;

                    if ((string.IsNullOrEmpty(clasificadorAAgregar.CadenaTexto) &&
                        !operacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) ||
                        !string.IsNullOrEmpty(clasificadorAAgregar.CadenaTexto))
                        operacion.Clasificadores_Cantidades.Add(clasificadorAAgregar);
                }

                if ((string.IsNullOrEmpty(clasificadorAAgregar.CadenaTexto) &&
                        !operacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) ||
                        !string.IsNullOrEmpty(clasificadorAAgregar.CadenaTexto))
                    numero.Clasificadores_SeleccionarOrdenar.Add(clasificadorAAgregar);

                if (!operacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)))
                {
                    var clasificadorGenerico = operacion.Clasificadores_Cantidades.
                    FirstOrDefault(i => string.IsNullOrEmpty(i.CadenaTexto));

                    if(clasificadorGenerico != null)
                    {
                        operacion.Clasificadores_Cantidades.Remove(clasificadorGenerico);
                    }
                }

                if (!numero.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)))
                {
                    var clasificadorGenerico = numero.Clasificadores_SeleccionarOrdenar.
                    FirstOrDefault(i => string.IsNullOrEmpty(i.CadenaTexto));

                    if (clasificadorGenerico != null)
                    {
                        numero.Clasificadores_SeleccionarOrdenar.Remove(clasificadorGenerico);
                    }
                }
            }
        }

        private void OperarNumerosOperacion_Interna(ElementoOperacionAritmeticaEjecucion operacion, ref string strMensajeLog, ref string strMensajeLogResultados,
            ref string info, ref string strOperando, ElementoCalculoEjecucion itemCalculo, bool mostrarLog)
        {
            DefinirNombresAntes_Ejecucion(operacion);

            strMensajeLog += " los siguientes variables o vectores:\n";
            strMensajeLogResultados += " los siguientes variables o vectores:\n";
            List<ElementoDiseñoOperacionAritmeticaEjecucion> SalidasOperacion = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();

            foreach (var etapaOperacion in operacion.Etapas)
            {
                foreach (var itemEtapa in etapaOperacion.Elementos
                    .Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion)
                    .OrderBy((i) => i.TipoInterno != TipoElementoOperacionEjecucion.OpcionOperacion))
                {
                    bool ejecutadoTooltipMostrado = false;

                    switch (itemEtapa.TipoInterno)
                    {
                        case TipoElementoOperacionEjecucion.Entrada:

                            ElementoEntradaEjecucion entradaOperacion = itemEtapa.EntradaEjecucion;

                            if (!ModoToolTips ||
                                (ModoToolTips && TooltipsCalculo != null && itemEtapa.ToolTipAMostrar))
                            {

                                strMensajeLog += entradaOperacion.Nombre + info + "\n";
                                strMensajeLogResultados += entradaOperacion.Nombre + info + "\n";

                                itemEtapa.Textos.AddRange(GenerarTextosInformacion(entradaOperacion.Textos));

                                foreach (var itemEntrada in entradaOperacion.Numeros)
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    itemEtapa.Numeros.Add(itemEntrada.CopiarObjeto());
                                    EstablecerElementoSalida_Operacion(itemEtapa.Numeros.LastOrDefault(), itemEtapa);
                                }

                                foreach (var itemClasificador in itemEtapa.Numeros.SelectMany(i => i.Clasificadores_SeleccionarOrdenar))
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    if (!itemEtapa.Clasificadores_Cantidades.Contains(itemClasificador))
                                        itemEtapa.Clasificadores_Cantidades.Add(itemClasificador);
                                }

                                ejecutadoTooltipMostrado = true;
                            }

                            break;

                        case TipoElementoOperacionEjecucion.FlujoOperacion:

                            if (!ModoToolTips ||
                                (ModoToolTips && TooltipsCalculo != null && itemEtapa.ToolTipAMostrar))
                            {
                                if (itemEtapa.OperacionEjecucion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                                {
                                    foreach (var itemPosterior in itemEtapa.ElementoDiseñoRelacionado.ElementosPosteriores)
                                    {
                                        foreach (var itemEtapa_Posterior in operacion.Etapas)
                                        {
                                            foreach (var itemPosterior_Ejecucion in itemEtapa_Posterior.Elementos)
                                            {
                                                if (itemPosterior_Ejecucion.ElementoDiseñoRelacionado == itemPosterior)
                                                {
                                                    if (itemPosterior_Ejecucion.TipoElemento == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                                                        itemPosterior_Ejecucion.TipoElemento == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados)
                                                        itemPosterior_Ejecucion.CantidadTextosInformacion_SeleccionarOrdenar++;

                                                }
                                            }
                                        }

                                    }
                                }

                                foreach (var itemNumero in itemEtapa.OperacionEjecucion.Numeros)
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    if (!operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any() ||
    (operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any(j =>
    itemEtapa.OperacionEjecucion.ElementoDiseñoRelacionado.ObtenerAgrupaciones().Any(
    i => i == j.Agrupacion &&
    i.NombreAgrupacion == j.Agrupacion.NombreAgrupacion &&
    j.OperandoAsociado == itemEtapa.OperacionEjecucion.ElementoDiseñoRelacionado &&
    j.ElementoAsociado == operacion.ElementoDiseñoRelacionado &&
    j.Agrupacion.NombreAgrupacion == itemNumero.Agrupacion_PorSeparado))))
                                    {
                                        if ((!itemNumero.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                        (itemNumero.ElementosSalidaOperacion_Agrupamiento.Contains(operacion)))
                                        {

                                            if ((!itemNumero.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                            (itemNumero.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion)))
                                            {
                                                if ((!itemNumero.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
                                                (itemNumero.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion)))
                                                {
                                                    if ((!itemNumero.ElementosInternosSalidaOperacion_Agrupamiento.Any()) ||
                                            (itemNumero.ElementosInternosSalidaOperacion_Agrupamiento.Contains(itemEtapa)))
                                                    {

                                                        if ((!itemNumero.ElementosInternosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                                        (itemNumero.ElementosInternosSalidaOperacion_SeleccionarOrdenar.Contains(itemEtapa)))
                                                        {
                                                            if ((!itemNumero.ElementosInternosSalidaOperacion_CondicionFlujo.Any()) ||
                                                            (itemNumero.ElementosInternosSalidaOperacion_CondicionFlujo.Contains(itemEtapa)))
                                                            {
                                                                EntidadNumero numero = new EntidadNumero();
                                                                numero.Numero = itemNumero.Numero;
                                                                numero.Textos.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                                                                numero.Nombre = itemNumero.Nombre;
                                                                numero.Clasificadores_SeleccionarOrdenar.AddRange(itemNumero.Clasificadores_SeleccionarOrdenar);

                                                                foreach (var itemClasificador in itemNumero.Clasificadores_SeleccionarOrdenar)
                                                                {
                                                                    if (!itemEtapa.Clasificadores_Cantidades.Contains(itemClasificador))
                                                                        itemEtapa.Clasificadores_Cantidades.Add(itemClasificador);
                                                                }

                                                                EstablecerElementoSalida_Operacion(numero, itemEtapa);

                                                                itemEtapa.Numeros.Add(numero);

                                                                var objetoNumeroTextos = (from E in itemEtapa.OperacionEjecucion.TextoInformacionAnterior_SeleccionOrdenamiento
                                                                                          where E.ObjetoCantidad == itemNumero
                                                                                          select E).ToList();

                                                                if (objetoNumeroTextos != null)
                                                                {
                                                                    foreach (var itemPosterior in itemEtapa.ElementoDiseñoRelacionado.ElementosPosteriores)
                                                                    {
                                                                        foreach (var itemEtapa_Posterior in operacion.Etapas)
                                                                        {
                                                                            foreach (var itemPosterior_Ejecucion in itemEtapa_Posterior.Elementos)
                                                                            {
                                                                                if (itemPosterior_Ejecucion.ElementoDiseñoRelacionado == itemPosterior)
                                                                                {
                                                                                    foreach (var itemTexto in objetoNumeroTextos)
                                                                                        itemPosterior_Ejecucion.TextoInformacionAnterior_SeleccionOrdenamiento.Add(new DuplaTextoInformacion_Cantidad_SeleccionarOrdenar { ObjetoCantidad = numero, TextoInformacion = itemTexto.TextoInformacion });

                                                                                }
                                                                            }
                                                                        }

                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                ejecutadoTooltipMostrado = true;
                            }

                            break;

                        case TipoElementoOperacionEjecucion.OpcionOperacion:

                            if (!ModoToolTips ||
                                (ModoToolTips && TooltipsCalculo != null
                                && itemEtapa.ConModificaciones_ToolTipMostrado(TooltipsCalculo, this)))
                            {
                                info = string.Empty;

                                if (Calculo.MostrarInformacionElementos_InformeResultados &&
                                    !string.IsNullOrEmpty(itemEtapa.ElementoDiseñoRelacionado.Info))
                                {
                                    info = " (" + itemEtapa.ElementoDiseñoRelacionado.Info + ") ";
                                }

                                if (itemEtapa.VerificarEjecucion_OperandosConCondiciones())
                                {
                                    strMensajeLogResultados += strOperando + " los siguientes variables o vectores, utilizando la definición de " + itemEtapa.ElementoDiseñoRelacionado.NombreElemento + info + ":\n";
                                    strMensajeLog += strOperando + " los siguientes variables o vectores, utilizando la definición de " + itemEtapa.ElementoDiseñoRelacionado.NombreElemento + info + ":\n";

                                    foreach (var subitem in itemEtapa.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                                    {
                                        info = string.Empty;

                                        if (Calculo.MostrarInformacionElementos_InformeResultados &&
                                            !string.IsNullOrEmpty(subitem.ElementoDiseñoRelacionado.Info))
                                        {
                                            info = " (" + itemEtapa.ElementoDiseñoRelacionado.Info + ") ";
                                        }

                                        switch (subitem.TipoInterno)
                                        {
                                            case TipoElementoOperacionEjecucion.Entrada:
                                                ElementoEntradaEjecucion entradaSubItem = subitem.EntradaEjecucion;

                                                strMensajeLog += subitem.ElementoDiseñoRelacionado.NombreCombo + "\n";
                                                strMensajeLogResultados += subitem.ElementoDiseñoRelacionado.NombreCombo + "\n";

                                                strMensajeLogResultados += entradaSubItem.Nombre + ", sus valores son:\n";
                                                strMensajeLog += entradaSubItem.Nombre + ", sus valores son:\n";

                                                foreach (var itemEntrada in entradaSubItem.Numeros)
                                                {
                                                    if (Pausada) Pausar();
                                                    if (Detenida) return;

                                                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemEntrada))
                                                    {
                                                        strMensajeLogResultados += itemEntrada.Nombre + ", " + itemEntrada.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemEntrada.ObtenerTextosInformacion_Cadena() + "\n";
                                                        strMensajeLog += itemEntrada.Nombre + ", " + itemEntrada.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemEntrada.ObtenerTextosInformacion_Cadena() + "\n";
                                                    }
                                                }

                                                if (entradaSubItem.Numeros.Count > 0) strMensajeLogResultados += "\n";
                                                if (entradaSubItem.Numeros.Count > 0) strMensajeLog += "\n";

                                                break;

                                            case TipoElementoOperacionEjecucion.FlujoOperacion:

                                                strMensajeLog += subitem.ElementoDiseñoRelacionado.NombreCombo + "\n";
                                                strMensajeLogResultados += subitem.ElementoDiseñoRelacionado.NombreCombo + "\n";

                                                strMensajeLogResultados += subitem.Nombre + ", sus valores son:\n";
                                                strMensajeLog += subitem.Nombre + ", sus valores son:\n";

                                                foreach (var itemNumero in subitem.OperacionEjecucion.Numeros)
                                                {
                                                    if (Pausada) Pausar();
                                                    if (Detenida) return;

                                                    if (!operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any() ||
    (operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any(j =>
    subitem.OperacionEjecucion.ElementoDiseñoRelacionado.ObtenerAgrupaciones().Any(
    i => i == j.Agrupacion &&
    i.NombreAgrupacion == j.Agrupacion.NombreAgrupacion &&
    j.OperandoAsociado == subitem.OperacionEjecucion.ElementoDiseñoRelacionado &&
    j.ElementoAsociado == operacion.ElementoDiseñoRelacionado &&
    j.Agrupacion.NombreAgrupacion == itemNumero.Agrupacion_PorSeparado))))
                                                    {
                                                        if ((!itemNumero.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                            (itemNumero.ElementosSalidaOperacion_Agrupamiento.Contains(operacion)))
                                                        {

                                                            if ((!itemNumero.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                                            (itemNumero.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion)))
                                                            {
                                                                if ((!itemNumero.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
                                                                (itemNumero.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion)))
                                                                {
                                                                    if ((!itemNumero.ElementosInternosSalidaOperacion_Agrupamiento.Any()) ||
                                            (itemNumero.ElementosInternosSalidaOperacion_Agrupamiento.Contains(subitem)))
                                                                    {

                                                                        if ((!itemNumero.ElementosInternosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                                                        (itemNumero.ElementosInternosSalidaOperacion_SeleccionarOrdenar.Contains(subitem)))
                                                                        {
                                                                            if ((!itemNumero.ElementosInternosSalidaOperacion_CondicionFlujo.Any()) ||
                                                                            (itemNumero.ElementosInternosSalidaOperacion_CondicionFlujo.Contains(subitem)))
                                                                            {
                                                                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemNumero))
                                                                                {
                                                                                    strMensajeLogResultados += itemNumero.Nombre + ", " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                                                                                    strMensajeLog += itemNumero.Nombre + ", " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (subitem.OperacionEjecucion.ElementosOperacion.Count > 0) strMensajeLogResultados += "\n";
                                                    if (subitem.OperacionEjecucion.ElementosOperacion.Count > 0) strMensajeLog += "\n";
                                                }


                                                break;

                                            case TipoElementoOperacionEjecucion.OpcionOperacion:
                                                strMensajeLogResultados += subitem.ElementoDiseñoRelacionado.NombreElemento + ", utilizando la definición.\n";
                                                strMensajeLog += subitem.ElementoDiseñoRelacionado.NombreElemento + ", utilizando la definición.\n";
                                                break;
                                        }
                                    }

                                    ElementoOperacionAritmeticaEjecucion operacionInterna = TraspasarObjetoDiseño_OperacionInterna(itemEtapa, operacion);
                                    bool operacionDeSeleccion = false;
                                    bool operacionDeCalculo = false;

                                    switch (itemEtapa.TipoElemento)
                                    {
                                        case TipoOpcionOperacion.TodosJuntos:
                                        case TipoOpcionOperacion.CalculandoPotencias_UnaSolaVez:
                                        case TipoOpcionOperacion.CalculandoRaices_UnaSolaVez:
                                        case TipoOpcionOperacion.CalculandoPorcentaje_UnaSolaVez:
                                        case TipoOpcionOperacion.CalculandoLogaritmo_UnaSolaVez:


                                            strMensajeLog += "Todos los números juntos de estas variables o vectores.\n";
                                            strMensajeLogResultados += "Todos los números juntos de estas variables o vectores.\n";

                                            operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarTodosJuntos;
                                            operacionDeCalculo = true;
                                            break;

                                        case TipoOpcionOperacion.TodosSeparados:
                                            strMensajeLog += "Estas variables o vectores por separado, obteniendo un vector de números.\n";
                                            strMensajeLogResultados += "Estas variables o vectores por separado, obteniendo un vector de números.\n";

                                            operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarPorSeparado;
                                            operacionDeCalculo = true;
                                            break;
                                        case TipoOpcionOperacion.PorFila:
                                        case TipoOpcionOperacion.CalculandoPotencias_PorFila:
                                        case TipoOpcionOperacion.CalculandoRaices_PorFila:
                                        case TipoOpcionOperacion.CalculandoPorcentaje_PorFila:
                                        case TipoOpcionOperacion.CalculandoLogaritmo_PorFila:

                                            strMensajeLog += "Las filas de los números de estas variables o vectores, obteniendo un vector de números\n";
                                            strMensajeLogResultados += "Las filas de los números de estas variables o vectores, obteniendo un vector de números\n";

                                            operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarPorFilas;
                                            operacionDeCalculo = true;
                                            break;

                                        case TipoOpcionOperacion.PorFilaPorSeparados:
                                        case TipoOpcionOperacion.CalculandoFactorial:
                                        case TipoOpcionOperacion.CalculandoInverso:
                                        case TipoOpcionOperacion.ContandoCantidades_TodosJuntos:
                                        case TipoOpcionOperacion.RedondearCantidades:
                                            strMensajeLog += "Las filas de los números de estas variables o vectores por separado, obteniendo un vector de números\n";
                                            strMensajeLogResultados += "Las filas de los números de estas variables o vectores por separado, obteniendo un vector de números\n";

                                            operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarPorSeparadoPorFilas;
                                            operacionDeCalculo = true;

                                            break;

                                        case TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos:
                                        case TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados:
                                        case TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir:
                                        case TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                                        case TipoOpcionOperacion.CondicionesFlujo:
                                        case TipoOpcionOperacion.CondicionesFlujo_PorSeparado:
                                            strMensajeLog += "Las cantidades de estas variables o vectores, obteniendo un vector de números\n";
                                            strMensajeLogResultados += "Las cantidades de estas variables o vectores, obteniendo un vector de números\n";

                                            switch (itemEtapa.TipoElemento)
                                            {
                                                case TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos:
                                                    operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarTodosJuntos;
                                                    operacionInterna.ElementoDiseñoRelacionado.Tipo = TipoElementoOperacion.SeleccionarOrdenar;
                                                    operacionInterna.TipoOperacion = TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar;

                                                    break;
                                                case TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados:
                                                    operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarPorSeparado;
                                                    operacionInterna.ElementoDiseñoRelacionado.Tipo = TipoElementoOperacion.SeleccionarOrdenar;
                                                    operacionInterna.TipoOperacion = TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar;

                                                    break;
                                                case TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir:                                                
                                                    operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarTodosJuntos;
                                                    operacionInterna.ElementoDiseñoRelacionado.Tipo = TipoElementoOperacion.SeleccionarOrdenar;
                                                    operacionInterna.TipoOperacion = TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar;
                                                    break;

                                                case TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                                                    operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarTodosJuntos;
                                                    operacionInterna.ElementoDiseñoRelacionado.Tipo = TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;
                                                    operacionInterna.TipoOperacion = TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;

                                                    break;
                                                case TipoOpcionOperacion.CondicionesFlujo:
                                                    operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarTodosJuntos;
                                                    operacionInterna.ElementoDiseñoRelacionado.Tipo = TipoElementoOperacion.CondicionesFlujo;
                                                    operacionInterna.TipoOperacion = TipoOperacionAritmeticaEjecucion.CondicionFlujo;

                                                    break;

                                                case TipoOpcionOperacion.CondicionesFlujo_PorSeparado:
                                                    operacionInterna.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion = TipoOperacionEjecucion.OperarPorSeparado;
                                                    operacionInterna.ElementoDiseñoRelacionado.Tipo = TipoElementoOperacion.CondicionesFlujo;
                                                    operacionInterna.TipoOperacion = TipoOperacionAritmeticaEjecucion.CondicionFlujo;

                                                    break;
                                            }

                                            
                                            operacionDeSeleccion = true;

                                            break;

                                        case TipoOpcionOperacion.Espera:
                                            strMensajeLog += "Esperando datos:\n";
                                            strMensajeLogResultados += "Esperando datos:\n";

                                            operacionDeCalculo = true;
                                            break;

                                        case TipoOpcionOperacion.LimpiarDatos:
                                            strMensajeLog += "Limpiando datos:\n";
                                            strMensajeLogResultados += "Limpiando datos:\n";

                                            operacionDeCalculo = true;
                                            break;

                                        case TipoOpcionOperacion.ArchivoExterno:
                                            strMensajeLog += "Ejecutando archivo de cálculo externo:\n";
                                            strMensajeLogResultados += "Ejecutando archivo de cálculo externo:\n";
                                            ProcesarArchivoExterno(itemEtapa, ref strMensajeLog, ref strMensajeLogResultados, itemCalculo, mostrarLog);

                                            break;

                                        case TipoOpcionOperacion.SubCalculo:
                                            strMensajeLog += "Ejecutando subcálculo:\n";
                                            strMensajeLogResultados += "Ejecutando subcálculo:\n";
                                            ProcesarSubCalculo(itemEtapa, ref strMensajeLog, ref strMensajeLogResultados, itemCalculo, mostrarLog);

                                            break;
                                    }

                                    if (operacionDeCalculo)
                                        OperarNumerosOperacion(operacionInterna, ref strMensajeLog, ref strMensajeLogResultados, true);
                                    else if (operacionDeSeleccion)
                                        OperarSeleccionNumerosOperacion(operacionInterna, ref strMensajeLog, ref strMensajeLogResultados, true, itemCalculo, 
                                            operacion);

                                    foreach (var numero in itemEtapa.Numeros)
                                    {
                                        EstablecerElementoSalida_Operacion(numero, itemEtapa);
                                    }
                                }

                                ejecutadoTooltipMostrado = true;
                            }

                            break;
                    }

                    if (ejecutadoTooltipMostrado)
                    {
                        if (itemEtapa.ElementoDiseñoRelacionado.LimpiarDatosResultados)
                            itemEtapa.LimpiarCantidades_Comportamiento();

                        if (itemEtapa.ElementoDiseñoRelacionado.RedondearCantidadesResultados)
                            itemEtapa.RedondearCantidades_Comportamiento();

                        itemEtapa.AgregarClasificadoresGenericos();

                        if (itemEtapa.ContieneSalida)
                            SalidasOperacion.Add(itemEtapa);

                        if (ModoToolTips && TooltipsCalculo != null)
                        {
                            TooltipsCalculo.EstablecerDatosTooltip_Elemento(itemEtapa.ElementoDiseñoRelacionado,
                                itemEtapa.Numeros.Select(i => i.CopiarObjeto()).ToList(),
                                TipoOpcionToolTip.ElementoOperacion, itemEtapa.Clasificadores_Cantidades,
                                itemEtapa.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
                                    itemEtapa.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);

                            itemEtapa.ToolTipMostrado = true;
                        }
                        operacion.CantidadSubElementosProcesados++;

                        strMensajeLogResultados += "\nEl resultado de " + itemEtapa.Nombre + " son las cantidades siguientes: \n";
                        strMensajeLog += "\nEl resultado de " + itemEtapa.Nombre + " son las cantidades siguientes: \n";

                        foreach (var itemClasificador in itemEtapa.Clasificadores_Cantidades)
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemEtapa.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                            if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                            {
                                strMensajeLog += "\n" + itemClasificador.CadenaTexto + ":\n";
                                strMensajeLogResultados += "\n" + itemClasificador.CadenaTexto + ":\n";
                            }
                            else
                            {
                                strMensajeLog += "\n";
                                strMensajeLogResultados += "\n";
                            }

                            foreach (var itemNumero in itemEtapa.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)))
                            {
                                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemNumero))
                                {
                                    strMensajeLogResultados += itemNumero.Nombre + ", su valor es: " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                                    strMensajeLog += itemNumero.Nombre + ", su valor es: " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                                }
                            }
                        }

                        strMensajeLog += "\n";
                        strMensajeLogResultados += "\n";
                    }
                    else
                    {
                        if(TooltipsCalculo != null)
                            TooltipsCalculo.ObtenerDatosTooltip_Elemento_Persistencia(itemEtapa.ElementoDiseñoRelacionado,
                                        itemEtapa.Numeros, itemEtapa);
                    }
                }
            }
            strMensajeLog += "\n";
            strMensajeLogResultados += "\n";

            operacion.CantidadNumeros_Ejecucion = 0;

            List<ElementoEjecucionCalculo> SalidasAgrupamiento = new List<ElementoEjecucionCalculo>();
            List<ElementoDiseñoOperacionAritmeticaEjecucion> SalidasElementosInternosAgrupamiento = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();

            foreach (var itemSalida in SalidasOperacion)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                foreach (var subItemSalida in itemSalida.Numeros)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    EntidadNumero elemento = new EntidadNumero();
                    elemento.Numero = subItemSalida.Numero;
                    elemento.Nombre = subItemSalida.Nombre;
                    elemento.Textos.AddRange(GenerarTextosInformacion(subItemSalida.Textos));
                    elemento.ElementosSalidaOperacion_Agrupamiento.AddRange(subItemSalida.ElementosSalidaOperacion_Agrupamiento);
                    elemento.ElementosInternosSalidaOperacion_Agrupamiento.AddRange(subItemSalida.ElementosInternosSalidaOperacion_Agrupamiento);
                    SalidasAgrupamiento.AddRange(subItemSalida.ElementosSalidaOperacion_Agrupamiento);
                    SalidasElementosInternosAgrupamiento.AddRange(subItemSalida.ElementosInternosSalidaOperacion_Agrupamiento);
                    elemento.Clasificadores_SeleccionarOrdenar.AddRange(subItemSalida.Clasificadores_SeleccionarOrdenar);

                    foreach (var itemClasificador in subItemSalida.Clasificadores_SeleccionarOrdenar)
                    {
                        if (!operacion.Clasificadores_Cantidades.Contains(itemClasificador))
                            operacion.Clasificadores_Cantidades.Add(itemClasificador);
                    }

                    operacion.Numeros.Add(elemento);

                }

                if (itemSalida.Numeros.Count > 0) strMensajeLogResultados += "\n";

                operacion.CantidadNumeros_Ejecucion++;
            }

        }

        private ElementoOperacionAritmeticaEjecucion TraspasarObjetoDiseño_OperacionInterna(ElementoDiseñoOperacionAritmeticaEjecucion itemEtapa,
            ElementoOperacionAritmeticaEjecucion operacionContenedora)
        {
            ElementoOperacionAritmeticaEjecucion operacionInterna = TraspasarObjetoDiseño_OperacionInterna_Preparar(itemEtapa, operacionContenedora);
                        
            ComparadorObjetos comparador = new ComparadorObjetos();

            //foreach (var operando in operacionInterna.ElementosOperacion)
            //{
            //    foreach (var item in operando.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida)
            //    {
            //        foreach (var itemCondicion in operando.CondicionesTextosInformacion_SeleccionOrdenamiento)
            //        {
            //            if (comparador.CompararObjetos(itemCondicion, item.CondicionesAsociadas))
            //            {
            //                item.CondicionesAsociadas = itemCondicion;
            //            }
            //        }

            //        foreach (var itemEtapa_Etapas in operacionContenedora.Etapas)
            //        {
            //            foreach (var itemOperando in itemEtapa_Etapas.Elementos)
            //            {
            //                if (item.ElementoSalida_Operacion.ID == itemOperando.ElementoDiseñoRelacionado.ID)
            //                    item.ElementoSalida_Operacion = operacionInterna.ElementoDiseñoRelacionado;

            //                //var itemOperandoEncontrado = item.ElementosSalidas.FirstOrDefault(i => i.ID == itemOperando.ID ||
            //                //i.ID == operacionContenedora.ElementoDiseñoRelacionado.ID);
            //                //if (itemOperandoEncontrado != null)
            //                //{
            //                //    item.ElementosSalidas.Remove(itemOperandoEncontrado);
            //                //    item.elementos.Add(itemOperando);
            //                //}
            //            }
            //        }
            //    }
            //}

            
            foreach (var item in operacionInterna.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida)
            {
                foreach (var itemCondicion in operacionInterna.CondicionesTextosInformacion_SeleccionOrdenamiento)
                {
                    if (comparador.CompararObjetos(itemCondicion, item.CondicionesAsociadas))
                    {
                        item.CondicionesAsociadas = itemCondicion;

                        foreach(var itemOperandoCondicion in operacionInterna.ElementosOperacion)
                        {
                            var itemOperandoEncontrado = item.CondicionesAsociadas.Operandos_AplicarCondiciones.FirstOrDefault(i => 
                            i.ID == itemOperandoCondicion.ElementoDiseñoRelacionado.ID ||
                            i.ID == operacionContenedora.ElementoDiseñoRelacionado.ID);
                            if (itemOperandoEncontrado != null)
                            {
                                item.CondicionesAsociadas.Operandos_AplicarCondiciones.Remove(itemOperandoEncontrado);
                                item.CondicionesAsociadas.Operandos_AplicarCondiciones.Add(itemOperandoCondicion.ElementoDiseñoRelacionado);
                            }
                        }
                    }
                }

                foreach (var itemEtapa_Etapas in operacionContenedora.Etapas)
                {
                    foreach (var itemOperando in itemEtapa_Etapas.Elementos)
                    {
                        if (item.ElementoSalida.ID == itemOperando.ElementoDiseñoRelacionado.ID)
                            item.ElementoSalida_Operacion = ((ElementoOperacionAritmeticaEjecucion)itemOperando).ElementoDiseñoRelacionado;

                        //var itemOperandoEncontrado = item.ElementosSalidas.FirstOrDefault(i => i.ID == itemOperando.ID ||
                        //i.ID == operacionContenedora.ElementoDiseñoRelacionado.ID);
                        //if (itemOperandoEncontrado != null)
                        //{
                        //    item.ElementosSalidas.Remove(itemOperandoEncontrado);
                        //    item.elementos.Add(itemOperando);
                        //}
                    }
                }
            }

            operacionInterna.Tipo = TipoElementoEjecucion.OperacionAritmetica;
            operacionInterna.TipoOperacion = operacionContenedora.TipoOperacion;

            ////Se resetean los objetos que hacen referencia a esta operacion
            //foreach(var itemOperacion in operacionInterna.ElementosOperacion)
            //{
            //    foreach(var itemNumero in itemOperacion.Numeros)
            //    {
            //        var itemOperandoEncontrado = itemNumero.ElementosSalidaOperacion_SeleccionarOrdenar.FirstOrDefault(i => 
            //        i.ElementoDiseñoRelacionado?.ID == operacionInterna.ElementoDiseñoRelacionado.ID);
            //        if (itemOperandoEncontrado != null)
            //        {
            //            itemNumero.ElementosSalidaOperacion_SeleccionarOrdenar.Remove(itemOperandoEncontrado);
            //            itemNumero.ElementosSalidaOperacion_SeleccionarOrdenar.Add(operacionInterna);
            //        }
            //    }
            //}

            return operacionInterna;
        }

        private ElementoOperacionAritmeticaEjecucion TraspasarObjetoDiseño_OperacionInterna_Preparar(ElementoDiseñoOperacionAritmeticaEjecucion itemEtapa,
            ElementoOperacionAritmeticaEjecucion operacionContenedora)
        {
            ElementoOperacionAritmeticaEjecucion operacionInterna = (ElementoOperacionAritmeticaEjecucion)itemEtapa;

            if (operacionInterna.ElementoDiseñoRelacionado == null)
            {
                operacionInterna.ElementoDiseñoRelacionado = new DiseñoOperacion();

                var tipoOrigen = itemEtapa.ElementoDiseñoRelacionado.GetType();
                var tipoDestino = operacionInterna.ElementoDiseñoRelacionado.GetType();

                foreach (var itemOperando in itemEtapa.ElementosOperacion)
                    operacionInterna.ElementosOperacion.Add(TraspasarObjetoDiseño_OperacionInterna_Preparar(itemOperando, operacionContenedora));

                foreach (var propOrigen in tipoOrigen.GetProperties())
                {
                    var propDestino = tipoDestino.GetProperty(propOrigen.Name);
                    if (propDestino != null && propDestino.CanWrite && propOrigen.CanRead &&
                        propDestino.PropertyType == propOrigen.PropertyType &&
                        propDestino.Name != "ElementosOperacion")
                    {
                        var valor = propOrigen.GetValue(itemEtapa.ElementoDiseñoRelacionado);
                        propDestino.SetValue(operacionInterna.ElementoDiseñoRelacionado, valor);
                    }
                }
            }

            if (itemEtapa.TipoInterno == TipoElementoOperacionEjecucion.Entrada)
                operacionInterna.Tipo = TipoElementoEjecucion.Entrada;
            else if (itemEtapa.TipoInterno == TipoElementoOperacionEjecucion.FlujoOperacion |
                itemEtapa.TipoInterno == TipoElementoOperacionEjecucion.OpcionOperacion)
                operacionInterna.Tipo = TipoElementoEjecucion.OperacionAritmetica;

            operacionInterna.TipoOperacion = operacionContenedora.TipoOperacion;

            return operacionInterna;
        }

        private void OperarSeleccionNumerosOperacion(ElementoOperacionAritmeticaEjecucion operacion, ref string strMensajeLog, ref string strMensajeLogResultados,
            bool operacionInterna, ElementoCalculoEjecucion itemCalculo, ElementoOperacionAritmeticaEjecucion operacionContenedora = null)
        {
            List<EntidadNumero> NumerosResultado = new List<EntidadNumero>();
            int indiceClasificadores = -1;

            List<CondicionTextosInformacion> ListaCondiciones = new List<CondicionTextosInformacion>();
            List<CondicionFlujo> ListaCondicionesFlujo = new List<CondicionFlujo>();

            if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
            {
                if (operacion.ElementoDiseñoRelacionado.ModoSeleccionManual_SeleccionarOrdenar)
                {
                    ListaCondiciones.Add(new CondicionTextosInformacion());
                    ListaCondiciones.FirstOrDefault().Operandos_AplicarCondiciones.AddRange(operacion.ElementosOperacion.Select(i => i.ElementoDiseñoRelacionado));
                }
                else
                {
                    ListaCondiciones.AddRange(operacion.CondicionesTextosInformacion_SeleccionOrdenamiento);
                }
            }
            else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo)
            {
                if (operacion.ElementoDiseñoRelacionado.ModoManual_CondicionFlujo)
                {
                    ListaCondicionesFlujo.Add(new CondicionFlujo());
                    ListaCondicionesFlujo.FirstOrDefault().Operandos_AplicarCondiciones.AddRange(operacion.ElementosOperacion.Select(i => i.ElementoDiseñoRelacionado));

                    SeleccionManualOperaciones_CondicionFlujo seleccionar = new SeleccionManualOperaciones_CondicionFlujo();
                    seleccionar.descripcionCondiciones.Text = "Operador de condiciones (si/entonces): " + operacion.ElementoDiseñoRelacionado.NombreCombo +
                        ". Selecciona las variables o vectores próximas a ejecutar:";
                    seleccionar.titulo.Text = "Seleccionar próximas variables o vectores para continuar\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                    seleccionar.Operaciones.AddRange(operacion.ElementoDiseñoRelacionado.ElementosPosteriores.Where(i => !i.NoConsiderarEjecucion));

                    bool digita = (bool)seleccionar.ShowDialog();
                    if (digita == true)
                    {
                        foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                        {
                            if (seleccionar.Operaciones.Contains(itemOperacion.ElementoDiseñoRelacionado))
                            {
                                itemOperacion.ElementoDiseñoRelacionado.NoConsiderarEjecucion = false;
                            }
                            else
                            {
                                itemOperacion.ElementoDiseñoRelacionado.NoConsiderarEjecucion = true;
                            }
                        }
                    }
                    else if (digita == false)
                    {
                        Detener();
                        return;
                    }
                }
                else
                {
                    ListaCondicionesFlujo.AddRange(operacion.CondicionesFlujo_SeleccionOrdenamiento);
                }
            }
            else
            {
                ListaCondiciones.Add(new CondicionTextosInformacion());
                ListaCondiciones.FirstOrDefault().Operandos_AplicarCondiciones.AddRange(operacion.ElementosOperacion.Select(i => i.ElementoDiseñoRelacionado));
            }

            operacion.CantidadTextosInformacion_SeleccionarOrdenar++;

            DefinirNombresAntes_Ejecucion(operacion);
            operacion.InicializarPosicionesClasificadores_Operandos();

            List<EntidadNumero> ElementosProcesados = new List<EntidadNumero>();

            for (int indiceCondicion = 0; (ListaCondiciones.Any() && indiceCondicion < ListaCondiciones.Count) ||
                (ListaCondicionesFlujo.Any() && indiceCondicion < ListaCondicionesFlujo.Count); indiceCondicion++)
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                CondicionTextosInformacion condiciones = null;

                if (indiceCondicion <= ListaCondiciones.Count - 1)
                    condiciones = ListaCondiciones[indiceCondicion];

                CondicionFlujo condicionesFlujo = null;

                if (indiceCondicion <= ListaCondicionesFlujo.Count - 1)
                    condicionesFlujo = ListaCondicionesFlujo[indiceCondicion];

                bool condicionCumple = false;

                List<EntidadNumero> Numeros_Condicion = new List<EntidadNumero>();
                List<EntidadNumero> Numeros_Condicion_Clasificadores = new List<EntidadNumero>();
                
                operacion.CantidadNumeros_Ejecucion = 0;
                InicializarVariables_Operacion(operacion);

                if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                {
                    foreach (var itemOperacionPosicion in operacion.ElementosOperacion)
                        itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;

                    foreach (var itemOperacionNumero in Calculo.TextosInformacion.ElementosTextosInformacion.Where(i =>
                                                i.GetType() == typeof(DiseñoListaCadenasTexto)))
                        ((DiseñoListaCadenasTexto)itemOperacionNumero).PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                }

                operacion.InicializarPosicionesClasificadores_Operandos();
                int CantidadClasificadores = operacion.ElementosOperacion.Max(i => i.Clasificadores_Cantidades.Count());

                for (int indice = 0; indice < CantidadClasificadores; indice++)
                {
                    foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                    {
                        if (Pausada) Pausar();
                        if (Detenida) return;

                        try
                        {
                            {
                                this.itemClasificador = itemOperacion.Clasificadores_Cantidades[itemOperacion.IndicePosicionClasificadores];

                                InicializarVariables_Operacion(operacion);

                                if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                                {
                                    foreach (var itemOperacionPosicion in operacion.ElementosOperacion)
                                        itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;

                                    foreach (var itemOperacionNumero in Calculo.TextosInformacion.ElementosTextosInformacion.Where(i =>
                                                                i.GetType() == typeof(DiseñoListaCadenasTexto)))
                                        ((DiseñoListaCadenasTexto)itemOperacionNumero).PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;
                                }

                                if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo)
                                {
                                    condicionCumple = false;
                                }

                                {

                                    if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                                    {
                                        strMensajeLog += "\n" + itemClasificador.CadenaTexto + ":\n";
                                        strMensajeLogResultados += "\n" + itemClasificador.CadenaTexto + ":\n";
                                    }
                                    else
                                    {
                                        strMensajeLog += "\n";
                                        strMensajeLogResultados += "\n";
                                    }

                                    string info = string.Empty;

                                    if (Calculo.MostrarInformacionElementos_InformeResultados &&
                                        !string.IsNullOrEmpty(itemOperacion.ElementoDiseñoRelacionado.Info))
                                    {
                                        info = " (" + itemOperacion.ElementoDiseñoRelacionado.Info + ") ";
                                    }

                                    strMensajeLog += itemOperacion.Nombre + info + "\n";
                                    strMensajeLogResultados += itemOperacion.Nombre + info + "\n";

                                    if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo)
                                    {
                                        condicionesFlujo.ClasificadorActual = this.itemClasificador;

                                        if (condicionesFlujo == null || (condicionesFlujo != null &&
                                        condicionesFlujo.EvaluarCondiciones(this, operacion, itemOperacion, null)))
                                        {
                                            condicionCumple = true;
                                        }
                                    }

                                    strMensajeLogResultados += itemOperacion.Nombre + info + ", sus valores son:\n";
                                    strMensajeLog += itemOperacion.Nombre + info + ", sus valores son:\n";

                                    List<EntidadNumero> ListaOriginal = itemOperacion.Numeros.Where(
                                                               i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)).ToList();

                                    ProcesarNumerosOperando_AntesEjecucion(operacion);

                                    List<EntidadNumero> ListaNumeros = new List<EntidadNumero>();

                                    if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                                    {
                                        if (operacion.ElementoDiseñoRelacionado.ModoSeleccionManual_SeleccionarOrdenar)
                                        {
                                            SeleccionarCantidadesOperando seleccionar = new SeleccionarCantidadesOperando();
                                            seleccionar.CalculoRelacionado = Calculo;
                                            seleccionar.descripcionEntrada.Text = itemOperacion.Nombre;
                                            seleccionar.titulo.Text = "Seleccionar cantidades de la variable o vector " + "\nEjecución del cálculo " + Calculo.NombreArchivo + " - " + SubCalculoActual.Nombre;

                                            seleccionar.ListaNumerosOriginal = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)).ToList();

                                            bool digita = (bool)seleccionar.ShowDialog();
                                            if (digita == true)
                                            {
                                                ListaNumeros.AddRange(seleccionar.ListaNumerosSeleccionados);
                                            }
                                            else if (digita == false)
                                            {
                                                Detener();
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            ListaNumeros = itemOperacion.Numeros.Where(
                                                               i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)).ToList();
                                        }
                                    }
                                    else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo ||
                                        operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.LimpiarDatos ||
                                        operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar ||
                                        operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Espera)
                                    {
                                        ListaNumeros = itemOperacion.Numeros.Where(
                                                              i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)).ToList();
                                    }

                                    int indiceLista = 0;

                                    for (indiceLista = 0; indiceLista <= ListaNumeros.Count - 1; indiceLista++)
                                    {
                                        if (Pausada) Pausar();
                                        if (Detenida) return;

                                        EntidadNumero numero = ListaNumeros[indiceLista];
                                        EntidadNumero numeroSeleccionado = numero.CopiarObjeto(false, itemClasificador);
                                        numeroSeleccionado.LimpiarVariables_SeleccionNumero();

                                        do
                                        {
                                            if ((itemOperacion.Tipo == TipoElementoEjecucion.Entrada && ((!numero.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                                            (numero.ElementosSalidaOperacion_Agrupamiento.Contains(operacion)))) ||

                                                            (itemOperacion.Tipo == TipoElementoEjecucion.OperacionAritmetica && (
                                                                ((!numero.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                                                                    (numero.ElementosSalidaOperacion_Agrupamiento.Contains(operacion))) &
                                                                                    ((!numero.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                                                                    (numero.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion))) &
                                                                                    ((!numero.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
                                                                                    (numero.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))))))
                                            {
                                                if (!itemOperacion.NumerosFiltrados.Any() || (itemOperacion.NumerosFiltrados.Any() & itemOperacion.NumerosFiltrados.Contains(numero)))
                                                {
                                                    bool procesarTextos = false;

                                                    if ((operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.LimpiarDatos &&
                                                        operacion.ElementoDiseñoRelacionado.EjecutarImplicacionesAntes_Limpieza) ||
                                                        (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar &&
                                                        operacion.ElementoDiseñoRelacionado.IncluirAsignacionTextosInformacionAntes) ||
                                                        (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar &&
                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_AntesEjecucion) ||
                                                            (operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.LimpiarDatos &&
                                                            operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar &&
                                                            operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar))
                                                    {
                                                        procesarTextos = true;
                                                    }

                                                    int cantidadFilas_Adicionales_NoUso = 0;

                                                    bool procesar = true;

                                                    if((operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar &&
                                                        !operacion.ElementoDiseñoRelacionado.IncluirAsignacionTextosInformacionAntes) ||
                                                        operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                                                    {
                                                        procesar = false;
                                                    }

                                                    if (procesar)
                                                    {
                                                        if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones)
                                                        {
                                                            bool conNumeros_NoUso = false;
                                                            int cantidadNumeros_PorSeparado_NoUso = 0;

                                                            operacion.LimpiarEstadoProcesamiento();

                                                            InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, numeroSeleccionado,
                                                                itemOperacion, numeroSeleccionado,
                                                                NumerosResultado, ListaNumeros, ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso, ref indiceClasificadores,
                                                                true, ref cantidadNumeros_PorSeparado_NoUso,
                                                                ref indiceLista, ref cantidadFilas_Adicionales_NoUso,
                                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion |
                                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);

                                                            foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                            {
                                                                if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                {
                                                                    itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                    ListaNumeros.RemoveAt(indiceLista);
                                                                    numero = ListaNumeros[indiceLista];
                                                                    indiceClasificadores--;

                                                                    numeroSeleccionado = numero.CopiarObjeto(false, itemClasificador);
                                                                    numeroSeleccionado.Textos.Clear();
                                                                }
                                                            }
                                                        }

                                                        if (procesarTextos)
                                                        {
                                                            ProcesarTextos_IteracionEjecucion(operacion, itemOperacion, numero, numeroSeleccionado,
                                                                                            NumerosResultado, true, indiceLista, false, operacionInterna,
                                                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesEjecucion |
                                                                                                operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesEjecucion,
                                                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_AntesEjecucion |
                                                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_DespuesEjecucion,
                                                                                operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones,
                                                                                operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesImplicaciones,
                                                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_AntesEjecucion |
                                                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_DespuesEjecucion, 0, itemClasificador);
                                                        }

                                                        if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones)
                                                        {
                                                            bool conNumeros_NoUso = false;
                                                            int cantidadNumeros_PorSeparado_NoUso = 0;

                                                            operacion.LimpiarEstadoProcesamiento();

                                                            InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, numeroSeleccionado,
                                                                itemOperacion, numeroSeleccionado,
                                                                NumerosResultado, ListaNumeros, ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso, ref indiceClasificadores,
                                                                true, ref cantidadNumeros_PorSeparado_NoUso,
                                                                ref indiceLista, ref cantidadFilas_Adicionales_NoUso,
                                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion |
                                                                operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);

                                                            foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                            {
                                                                if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                {
                                                                    itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                    ListaNumeros.RemoveAt(indiceLista);
                                                                    numero = ListaNumeros[indiceLista];
                                                                    indiceClasificadores--;

                                                                    numeroSeleccionado = numero.CopiarObjeto(false, itemClasificador);
                                                                    numeroSeleccionado.Textos.Clear();
                                                                }
                                                            }
                                                        }
                                                    }

                                                    bool seleccionar = false;

                                                    if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                                                    {
                                                        if (operacion.ElementoDiseñoRelacionado.ModoUnir_SeleccionarOrdenar)
                                                        {
                                                            seleccionar = true;
                                                        }
                                                        else
                                                        {
                                                            var textosInformacionElemento = new List<string>();
                                                            
                                                            condiciones.TextosInformacionInvolucrados.Clear();

                                                            condiciones.EvaluarCondiciones(this, operacion.GetType() != typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ?
                                                                operacion : (ElementoOperacionAritmeticaEjecucion)operacion, operacion.GetType() == typeof(ElementoDiseñoOperacionAritmeticaEjecucion) ?
                                                                (ElementoDiseñoOperacionAritmeticaEjecucion)operacion : null, itemOperacion, numeroSeleccionado);

                                                            if ((!condiciones.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores &&
                                                                numeroSeleccionado.ConsiderarOperandoCondicion_SiCumple) ||
                                                                (condiciones.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores))
                                                            {
                                                                if (!condiciones.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores)
                                                                    numero.CumpleAlgunaCondicion_SeleccionarCantidades = true;
                                                                else
                                                                {
                                                                    condiciones.ValorCondiciones = !numero.CumpleAlgunaCondicion_SeleccionarCantidades;
                                                                }

                                                                textosInformacionElemento.AddRange(GenerarTextosInformacion(condiciones.TextosInformacionInvolucrados));

                                                                EstablecerTextosInformacion_SeleccionarOrdenar(operacion, numeroSeleccionado, textosInformacionElemento);
                                                                if ((condiciones.VerificarSoloCondicionesCadenasTextos() && 
                                                                    VerificarTextoInformacion(textosInformacionElemento, numero.Textos, textosInformacionElemento.Count) && 
                                                                    condiciones.ValorCondiciones) ||
                                                                    (!condiciones.VerificarSoloCondicionesCadenasTextos() && 
                                                                    condiciones.ValorCondiciones))
                                                                {
                                                                    condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.AddRange(textosInformacionElemento);
                                                                    seleccionar = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo)
                                                    {
                                                        if (operacion.ElementoDiseñoRelacionado.ModoManual_CondicionFlujo)
                                                        {
                                                            seleccionar = true;
                                                        }
                                                        else
                                                        {
                                                            if (condicionCumple)
                                                                seleccionar = true;
                                                        }
                                                    }
                                                    else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.LimpiarDatos)
                                                    {
                                                        if (numero.EvaluarCondicionesLimpieza(operacion.ElementoDiseñoRelacionado.ConfigLimpiarDatos,
                                                                            itemOperacion.ElementosOperacion_Filtrados(operacion)))
                                                        {
                                                            seleccionar = true;
                                                        }
                                                    }
                                                    else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                                                    {
                                                        seleccionar = true;
                                                    }
                                                    else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Espera)
                                                    {
                                                        seleccionar = true;
                                                    }

                                                    if (seleccionar &&
                                                        NumerosResultado.Any(i => EntidadNumero.CompararNumeros(i, numero)))
                                                    {
                                                        seleccionar = false;
                                                    }


                                                    if (seleccionar)
                                                    {
                                                        if (!(itemOperacion.Numeros.Contains(numero) &&
                                                            ((condiciones != null && condiciones.Operandos_AplicarCondiciones.Any(i => i == itemOperacion.ElementoDiseñoRelacionado)) ||
                                        (condicionesFlujo != null && condicionesFlujo.Operandos_AplicarCondiciones.Any(i => i == itemOperacion.ElementoDiseñoRelacionado)))))

                                                        {
                                                            seleccionar = false;
                                                        }
                                                    }

                                                    if (seleccionar)
                                                    {
                                                        if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar |
                                                            operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo |
                                                            operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Espera |
                                                            operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.LimpiarDatos)
                                                        {
                                                            numeroSeleccionado.Textos.AddRange(GenerarTextosInformacion(numero.Textos));
                                                        }

                                                        int cantidadNumeros_PorSeparado_NoUso = 0;

                                                        if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numero))
                                                        {
                                                            strMensajeLogResultados += numero.Nombre + ", " + numero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numero.ObtenerTextosInformacion_Cadena() + "\n";
                                                            strMensajeLog += numero.Nombre + ", " + numero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numero.ObtenerTextosInformacion_Cadena() + "\n";
                                                        }

                                                        NumerosResultado.Add(numeroSeleccionado);
                                                        indiceClasificadores++;
                                                        itemOperacion.NumerosElementosFiltrados_Condiciones.Add(NumerosResultado[indiceClasificadores]);
                                                        operacion.CantidadNumeros_Ejecucion++;

                                                        AgregarClasificador_Cantidad(operacion, itemClasificador, numeroSeleccionado);

                                                        NumerosResultado[indiceClasificadores].HashCode_NumeroAgregacion_Ejecucion = numero.GetHashCode();
                                                        itemOperacion.NumerosElementosFiltrados_Condiciones.Add(NumerosResultado[indiceClasificadores]);

                                                        if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                                                        {
                                                            foreach (var itemTexto in operacion.CondicionesTextosInformacion_SeleccionOrdenamiento)
                                                            {
                                                                itemTexto.TextosInformacionInvolucrados.AddRange(GenerarTextosInformacion(operacion.TextosInformacionInvolucrados_CondicionSeleccionarOrdenar));
                                                                EstablecerElementosSalida_SeleccionarOrdenar(NumerosResultado[indiceClasificadores], operacion, itemTexto, itemOperacion, ElementosProcesados, operacionContenedora);
                                                            }
                                                        }

                                                        if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.LimpiarDatos)
                                                        {
                                                            NumerosResultado[indiceClasificadores].ElementosSalidaOperacion_SeleccionarOrdenar.Add(new ElementoDiseñoOperacionAritmeticaEjecucion());

                                                            var elementosSalida = operacion.ElementoDiseñoRelacionado.EntradasSalidas_LimpiarDatos.Where(i => i[0] == itemOperacion.ElementoDiseñoRelacionado);
                                                            foreach (var item in elementosSalida)
                                                            {
                                                                var elementoEjecucionSalida = ObtenerElementoEjecucion(item[1]);

                                                                if (elementoEjecucionSalida != null)
                                                                    NumerosResultado[indiceClasificadores].ElementosSalidaOperacion_SeleccionarOrdenar.Add(elementoEjecucionSalida);
                                                            }
                                                        }

                                                        if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar)
                                                        {
                                                            NumerosResultado[indiceClasificadores].ElementosSalidaOperacion_SeleccionarOrdenar.Add(new ElementoDiseñoOperacionAritmeticaEjecucion());

                                                            if (!condiciones.OrdenarAlfabeticamenteNumerosSalidas)
                                                                EstablecerElementosSalida_SeleccionarOrdenar(NumerosResultado[indiceClasificadores], operacion, condiciones, itemOperacion, ElementosProcesados, operacionContenedora);
                                                            else
                                                            {
                                                                NumerosResultado[indiceClasificadores].itemOperacion_Ejecucion_OrdenamientoSalidas = itemOperacion;
                                                                Numeros_Condicion.Add(NumerosResultado[indiceClasificadores]);
                                                            }

                                                            NumerosResultado[indiceClasificadores].ValorCondiciones_SeleccionarOrdenar = condiciones.ValorCondiciones;
                                                            if (!condiciones.OrdenarAlfabeticamenteNumerosClasificadores)
                                                                EstablecerClasificadores_SeleccionarOrdenar(NumerosResultado[indiceClasificadores], operacion, condiciones, itemOperacion,
                                                        NumerosResultado[indiceClasificadores].ObtenerTextosInformacion_Cadena(true), false);
                                                            else
                                                            {
                                                                NumerosResultado[indiceClasificadores].itemOperacion_Ejecucion_OrdenamientoClasificadores = itemOperacion;
                                                                Numeros_Condicion_Clasificadores.Add(NumerosResultado[indiceClasificadores]);
                                                            }
                                                        }
                                                        else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo)
                                                        {
                                                            NumerosResultado[indiceClasificadores].CondicionFlujoRelacionada = condicionesFlujo;
                                                            NumerosResultado[indiceClasificadores].ElementosSalidaOperacion_CondicionFlujo.Add(new ElementoEjecucionCalculo());
                                                            EstablecerElementosSalida_CondicionFlujo(NumerosResultado[indiceClasificadores],
                                                                operacion, condicionesFlujo, condicionCumple, operacionContenedora, ElementosProcesados);
                                                        }
                                                        else if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.Espera)
                                                        {
                                                            NumerosResultado[indiceClasificadores].ElementosSalidaOperacion_SeleccionarOrdenar.Add(new ElementoDiseñoOperacionAritmeticaEjecucion());

                                                            var elementosSalida = operacion.ElementoDiseñoRelacionado.EntradasSalidas_Espera.Where(i => i[0] == itemOperacion.ElementoDiseñoRelacionado);
                                                            foreach (var item in elementosSalida)
                                                            {
                                                                var elementoEjecucionSalida = ObtenerElementoEjecucion(item[1]);

                                                                if (elementoEjecucionSalida != null)
                                                                    NumerosResultado[indiceClasificadores].ElementosSalidaOperacion_SeleccionarOrdenar.Add(elementoEjecucionSalida);
                                                            }
                                                        }

                                                        procesarTextos = false;

                                                        if ((operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.LimpiarDatos &&
                                                            operacion.ElementoDiseñoRelacionado.EjecutarImplicacionesDespues_Limpieza) ||
                                                            (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar &&
                                                            operacion.ElementoDiseñoRelacionado.IncluirAsignacionTextosInformacionDespues) ||
                                                            (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar &&
                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_DespuesEjecucion) ||
                                                            (operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.LimpiarDatos &&
                                                            operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar &&
                                                            operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar))
                                                        {
                                                            procesarTextos = true;
                                                        }

                                                        procesar = true;

                                                        if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar &&
                                                            !operacion.ElementoDiseñoRelacionado.IncluirAsignacionTextosInformacionDespues)
                                                        {
                                                            procesar = false;
                                                        }

                                                        if (procesar)
                                                        {
                                                            cantidadFilas_Adicionales_NoUso = 0;

                                                            if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones)
                                                            {
                                                                bool conNumeros_NoUso = false;
                                                                cantidadNumeros_PorSeparado_NoUso = 0;

                                                                operacion.LimpiarEstadoProcesamiento();

                                                                InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, NumerosResultado[indiceClasificadores],
                                                                    itemOperacion, NumerosResultado[indiceClasificadores],
                                                                    NumerosResultado, ListaNumeros, ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso, ref indiceClasificadores,
                                                                    true, ref cantidadNumeros_PorSeparado_NoUso,
                                                                    ref indiceLista, ref cantidadFilas_Adicionales_NoUso,
                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion |
                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);

                                                                foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                                {
                                                                    if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                    {
                                                                        itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                        ListaNumeros.RemoveAt(indiceLista);
                                                                        numero = ListaNumeros[indiceLista];
                                                                        indiceClasificadores--;

                                                                        numeroSeleccionado = numero.CopiarObjeto(false, itemClasificador);
                                                                        numeroSeleccionado.Textos.Clear();
                                                                    }
                                                                }
                                                            }

                                                            if (procesarTextos)
                                                            {
                                                                ProcesarTextos_IteracionEjecucion(operacion, itemOperacion, numero, NumerosResultado[indiceClasificadores],
                                                                                                NumerosResultado, true, indiceLista, false, operacionInterna,
                                                                                                operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesEjecucion |
                                                                                                operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesEjecucion,
                                                                                                operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_AntesEjecucion |
                                                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_DespuesEjecucion,
                                                                                    operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones,
                                                                                    operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesImplicaciones,
                                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_AntesEjecucion |
                                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_DespuesEjecucion, 0, itemClasificador);
                                                            }

                                                            if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones)
                                                            {
                                                                bool conNumeros_NoUso = false;
                                                                cantidadNumeros_PorSeparado_NoUso = 0;

                                                                operacion.LimpiarEstadoProcesamiento();

                                                                InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, NumerosResultado[indiceClasificadores],
                                                                    itemOperacion, NumerosResultado[indiceClasificadores],
                                                                    NumerosResultado, ListaNumeros, ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso, ref indiceClasificadores,
                                                                    true, ref cantidadNumeros_PorSeparado_NoUso,
                                                                    ref indiceLista, ref cantidadFilas_Adicionales_NoUso,
                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion |
                                                                    operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);

                                                                foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                                                {
                                                                    if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                                                    {
                                                                        itemOperacion.Numeros.RemoveAt(indiceLista);
                                                                        ListaNumeros.RemoveAt(indiceLista);
                                                                        numero = ListaNumeros[indiceLista];
                                                                        indiceClasificadores--;

                                                                        numeroSeleccionado = numero.CopiarObjeto(false, itemClasificador);
                                                                        numeroSeleccionado.Textos.Clear();
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento &&
                                    !operacion.ElementoDiseñoRelacionado.UtilizarDefinicionNombres_AsignacionTextosInformacion)
                                                        {
                                                            EstablecerNombreCantidad(numero, operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades,
                                                                indiceLista, operacion, itemOperacion);
                                                        }
                                                    }

                                                    itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                                    operacion.AumentarPosicionesElementosExterioresCondiciones(this);
                                                    operacion.AumentarPosicionesElementosOperandos(this, itemOperacion);

                                                    foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                                                        itemOperacionNumero.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar++;

                                                    operacion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar++;

                                                    foreach (var itemOperacionNumero in Calculo.TextosInformacion.ElementosTextosInformacion.Where(i =>
                                                    i.GetType() == typeof(DiseñoListaCadenasTexto)))
                                                        ((DiseñoListaCadenasTexto)itemOperacionNumero).PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar++;

                                                }
                                            }

                                            if (operacion.DetenerProcesamiento)
                                                break;

                                        } while (operacion.MantenerProcesamiento_Operandos |
                                                                                        operacion.MantenerProcesamiento_Resultados);

                                        if (operacion.DetenerProcesamiento)
                                            break;

                                    }

                                    ProcesarNumerosOperando_DespuesEjecucion(operacion, itemOperacion, ListaOriginal, ListaNumeros, itemClasificador);

                                    if (itemOperacion.Numeros.Count > 0) strMensajeLogResultados += "\n";
                                    if (itemOperacion.Numeros.Count > 0) strMensajeLog += "\n";

                                    InicializarVariables_Operacion(operacion);
                                }

                                strMensajeLogResultados += "\n";
                                strMensajeLog += "\n";
                            }
                        }
                        catch (Exception) { }
                    }

                    operacion.AumentarPosicionesClasificadores_Operandos();
                }

                if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar &&
                    !operacion.ElementoDiseñoRelacionado.ModoSeleccionManual_SeleccionarOrdenar)
                {
                    if (condiciones.OrdenarAlfabeticamenteNumerosSalidas &&
                        NumerosResultado.Any() && Numeros_Condicion.Any())
                    {
                        Numeros_Condicion = OrdenarElementosSalida_CondicionTextosInformacion(condiciones, Numeros_Condicion);

                        int indiceElementoSalida = 0;
                        List<string> TextosInformacion_ElementosSalidas = new List<string>();
                        ElementosProcesados.Clear();
                        TextosInformacion_ElementosSalidas = ObtenerTextosInformacionSalidas_Elemento(Numeros_Condicion.FirstOrDefault(), condiciones);
                        int cantidadConjuntosTextos = 0;

                        foreach (var itemNumero in Numeros_Condicion)
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            if (!CompararTextosInformacionElementosSalidas(TextosInformacion_ElementosSalidas, ObtenerTextosInformacionSalidas_Elemento(itemNumero, condiciones)))
                            {
                                cantidadConjuntosTextos++;

                                TextosInformacion_ElementosSalidas = ObtenerTextosInformacionSalidas_Elemento(itemNumero, condiciones);
                                if (indiceElementoSalida < (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida
                                                            where E.CondicionesAsociadas == condiciones & E.ElementoSalida_Operacion != null
                                                            select E.ElementoSalida_Operacion).Count() - 1)
                                {
                                    if ((from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida
                                         where E.CondicionesAsociadas == condiciones & E.ElementoSalida_Operacion != null
                                         select E).ToList()[indiceElementoSalida].CantidadConjuntosTextosInformacion == cantidadConjuntosTextos)
                                    {
                                        indiceElementoSalida++;
                                        cantidadConjuntosTextos = 0;
                                    }
                                }
                            }

                            EstablecerElementosSalida_SeleccionarOrdenar(itemNumero, operacion, condiciones, itemNumero.itemOperacion_Ejecucion_OrdenamientoSalidas, ElementosProcesados, operacionContenedora, indiceElementoSalida);

                        }
                    }

                    if (condiciones.OrdenarAlfabeticamenteNumerosClasificadores &&
                        NumerosResultado.Any() && Numeros_Condicion_Clasificadores.Any())
                    {
                        Numeros_Condicion_Clasificadores = OrdenarElementosSalida_CondicionTextosInformacion_Clasificadores(condiciones, Numeros_Condicion_Clasificadores);

                        int indiceClasificador = 0;
                        List<string> TextosInformacion_Clasificadores = new List<string>();
                        TextosInformacion_Clasificadores = ObtenerTextosInformacionClasificadores_Elemento(Numeros_Condicion_Clasificadores.FirstOrDefault(), condiciones);
                        int cantidadConjuntosTextos = 0;

                        int indiceConjuntosTextos = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_Clasificadores
                                                     where E.CondicionesAsociadas == condiciones & E.ElementoClasificador != null
                                                     select E).ToList()[indiceClasificador].CantidadConjuntosTextosInformacion;

                        foreach (var itemNumero in Numeros_Condicion_Clasificadores)
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            if (!CompararTextosInformacionElementosSalidas(TextosInformacion_Clasificadores, ObtenerTextosInformacionClasificadores_Elemento(itemNumero, condiciones)))
                            {
                                cantidadConjuntosTextos++;

                                TextosInformacion_Clasificadores = ObtenerTextosInformacionClasificadores_Elemento(itemNumero, condiciones);
                                if (indiceClasificador < (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_Clasificadores
                                                          where E.CondicionesAsociadas == condiciones & E.ElementoClasificador != null
                                                          select E.ElementoClasificador).Count() - 1)
                                {
                                    if ((from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_Clasificadores
                                         where E.CondicionesAsociadas == condiciones & E.ElementoClasificador != null
                                         select E).ToList()[indiceClasificador].CantidadConjuntosTextosInformacion == cantidadConjuntosTextos)
                                    {
                                        indiceClasificador++;

                                        indiceConjuntosTextos = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_Clasificadores
                                                                 where E.CondicionesAsociadas == condiciones & E.ElementoClasificador != null
                                                                 select E).ToList()[indiceClasificador].CantidadConjuntosTextosInformacion;

                                        cantidadConjuntosTextos = 0;
                                    }
                                }
                            }

                            EstablecerClasificadores_SeleccionarOrdenar(itemNumero, operacion, condiciones, itemNumero.itemOperacion_Ejecucion_OrdenamientoClasificadores,
                                string.Join(" , ", TextosInformacion_Clasificadores.Take(indiceConjuntosTextos)), true, indiceClasificador);
                        }
                    }
                }

                if (operacion.TipoOperacion == TipoOperacionAritmeticaEjecucion.CondicionFlujo)
                {
                    strMensajeLogResultados += "Cantidades transferidas de :\n";
                    strMensajeLog += "Cantidades transferidas de : \n";

                    foreach (var itemOperacion in operacion.ElementosOperacion.Where(j => condicionesFlujo.Operandos_AplicarCondiciones.Any(i => i == j.ElementoDiseñoRelacionado)).Distinct())
                    {
                        strMensajeLogResultados += itemOperacion.ElementoDiseñoRelacionado.NombreCombo + "\n";
                        strMensajeLog += itemOperacion.ElementoDiseñoRelacionado.NombreCombo + "\n";
                    }
                }
            }

            operacion.Numeros.AddRange(NumerosResultado);

            //foreach (var itemClasificador in operacion.Numeros.SelectMany(i => i.Clasificadores_SeleccionarOrdenar))
            //{
            //    if (Pausada) Pausar();
            //    if (Detenida) return;

            //    if (!operacion.Clasificadores_Cantidades.Contains(itemClasificador))
            //        operacion.Clasificadores_Cantidades.Add(itemClasificador);
            //}

            operacion.CantidadSubElementosProcesados++;

            DefinirNombresDespues_Ejecucion(operacion);

            AgregarCantidadesAdicionales(operacion);

            strMensajeLogResultados += "El resultado de " + operacion.Nombre + " es: \n";
            strMensajeLog += "El resultado de " + operacion.Nombre + " es: \n";

            foreach (var itemClasificador in operacion.Clasificadores_Cantidades)
            {
                if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            operacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                {
                    strMensajeLog += "\n" + itemClasificador.CadenaTexto + ":\n";
                    strMensajeLogResultados += "\n" + itemClasificador.CadenaTexto + ":\n";
                }
                else
                {
                    strMensajeLog += "\n";
                    strMensajeLogResultados += "\n";
                }

                foreach (var itemNumero in operacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)))
                {
                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(itemNumero))
                    {
                        strMensajeLogResultados += itemNumero.Nombre + " es: " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                        strMensajeLog += itemNumero.Nombre + " es: " + itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + itemNumero.ObtenerTextosInformacion_Cadena() + "\n";
                    }
                }


            }

            if (!operacion.Clasificadores_Cantidades.Any())
            {
                operacion.AgregarClasificadoresGenericos();

            }

            if (operacion.Numeros.Count > 0) strMensajeLogResultados += "\n";
            if (operacion.Numeros.Count > 0) strMensajeLog += "\n";
        }

        private void InicializarVariables_Operacion(ElementoOperacionAritmeticaEjecucion operacion)
        {
            operacion.LimpiarEstadoProcesamiento();

            foreach (var itemOperacionPosicion in operacion.ElementosOperacion)
                itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;

            foreach (var itemOperacionPosicion in operacion.ElementosOperacion)
                itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;

            foreach (var itemOperacionNumero in Calculo.TextosInformacion.ElementosTextosInformacion.Where(i =>
                                                i.GetType() == typeof(DiseñoListaCadenasTexto)))
                ((DiseñoListaCadenasTexto)itemOperacionNumero).PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar = 0;

            operacion.InicializarPosicionesElementosExterioresCondiciones(this);
            operacion.InicializarPosicionesElementosOperandos(this, null);

            operacion.LimpiarTextosInformacionAgregadosInstancias_Resultado_Operacion();
            operacion.LimpiarTextosInformacionAgregadosInstancias_Resultado_Condiciones();
            operacion.LimpiarTextosInformacionAgregadosInstancias_Resultado_Operandos();
        }

        private void ProcesarTextos_IteracionEjecucion(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            EntidadNumero numero,
            EntidadNumero numeroResultado,
            List<EntidadNumero> NumerosResultado,
            bool porCadaCantidad,
            int posicionActual,
            bool desdeFilas,
            bool operacionInterna,
            bool AsignarTextosInformacion,
            bool AsignarTextosInformacionImplicaciones,
            bool AsignarTextosInformacion_AntesImplicaciones,
            bool AsignarTextosInformacion_DespuesImplicaciones,
            bool EjecutarLogicas,
            int filasAdicionalesResultado,
            Clasificador ClasificadorActual)
        {
            if (EjecutarLogicas && 
                operacion.ElementoDiseñoRelacionado.AplicarProcesamientoAntesImplicacionesTextosInformacion)
            {
                if (!porCadaCantidad)
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos &&
                        operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                    {
                        foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                        {
                            InsertarProcesarTextos_Operacion(operacion, itemOperacionNumero, numeroResultado, NumerosResultado, desdeFilas);
                        }
                    }
                    else
                        InsertarProcesarTextos_Operacion(operacion, itemOperacion, numeroResultado, NumerosResultado, desdeFilas);
                }
                else
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado)
                    {
                        foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                        {
                            InsertarProcesarTextos_ItemOperacion(operacion, itemOperacionNumero, numero, numeroResultado, NumerosResultado, desdeFilas, filasAdicionalesResultado);
                        }
                    }
                    else
                    {
                        InsertarProcesarTextos_ItemOperacion(operacion, itemOperacion, numero, numeroResultado, NumerosResultado, desdeFilas, filasAdicionalesResultado);
                    }
                }
            }

            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos &&
                        operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
            {
                foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                {
                    AsignarTextosCantidad_Implicaciones_Operacion(operacion, itemOperacionNumero, numero, numeroResultado, NumerosResultado, porCadaCantidad,
                posicionActual, desdeFilas, operacionInterna, AsignarTextosInformacion, AsignarTextosInformacionImplicaciones,
                AsignarTextosInformacion_AntesImplicaciones,
                AsignarTextosInformacion_DespuesImplicaciones, ClasificadorActual);
                }
            }
            else
            {
                AsignarTextosCantidad_Implicaciones_Operacion(operacion, itemOperacion, numero, numeroResultado, NumerosResultado, porCadaCantidad,
                posicionActual, desdeFilas, operacionInterna, AsignarTextosInformacion, AsignarTextosInformacionImplicaciones,
                AsignarTextosInformacion_AntesImplicaciones,
                AsignarTextosInformacion_DespuesImplicaciones, ClasificadorActual);
            }

            if (EjecutarLogicas &&
                operacion.ElementoDiseñoRelacionado.AplicarProcesamientoDespuesImplicacionesTextosInformacion)
            {
                if (!porCadaCantidad)
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos &&
                        operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                    {
                        foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                        {
                            InsertarProcesarTextos_Operacion(operacion, itemOperacionNumero, numeroResultado, NumerosResultado, desdeFilas);
                        }
                    }
                    else
                        InsertarProcesarTextos_Operacion(operacion, itemOperacion, numeroResultado, NumerosResultado, desdeFilas);
                }
                else
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado)
                    {
                        foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                        {
                            InsertarProcesarTextos_ItemOperacion(operacion, itemOperacionNumero, numero, numeroResultado, NumerosResultado, desdeFilas, filasAdicionalesResultado);
                        }
                    }
                    else
                    {
                        InsertarProcesarTextos_ItemOperacion(operacion, itemOperacion, numero, numeroResultado, NumerosResultado, desdeFilas, filasAdicionalesResultado);
                    }
                }
            }
        }

        private void ProcesarNumerosOperando_DespuesEjecucion(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            List<EntidadNumero> ListaOriginal,
            List<EntidadNumero> ListaNumeros,
            Clasificador itemClasificador)
        {
            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas)
            {
                if (operacion.ElementoDiseñoRelacionado.NoConservarCambiosOperandos_ProcesamientoCantidades)
                {
                    itemOperacion.Numeros = ListaOriginal;
                    operacion.ActualizarTooltips_Operandos = true;
                }
                //else
                //{
                //    int indiceInicial = itemOperacion.Numeros.IndexOf(itemOperacion.Numeros.FirstOrDefault(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)));
                //    foreach (var item in ListaNumeros)
                //    {
                //        if (itemOperacion.Numeros.Contains(item))
                //            itemOperacion.Numeros.Remove(item);
                //    }

                //    if (itemOperacion.Numeros.Any() &&
                //        indiceInicial > -1)
                //        itemOperacion.Numeros.InsertRange(indiceInicial, ListaNumeros);
                //    else
                //        itemOperacion.Numeros.AddRange(ListaNumeros);
                //}
            }
            else
            {
                foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                {
                    if (operacion.ElementoDiseñoRelacionado.NoConservarCambiosOperandos_ProcesamientoCantidades)
                    {
                        itemOperacionNumero.Numeros = itemOperacionNumero.ListaOriginal_Numeros;
                        operacion.ActualizarTooltips_Operandos = true;
                    }
                }
            }
        }

        private void ProcesarNumerosOperando_AntesEjecucion(ElementoOperacionAritmeticaEjecucion operacion)
        {
            if (operacion.ElementoDiseñoRelacionado.NoConservarCambiosOperandos_ProcesamientoCantidades)
            {
                foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                {
                    itemOperacion.ListaOriginal_Numeros = itemOperacion.Numeros.ToList();
                    operacion.ActualizarTooltips_Operandos = true;
                }
            }
        }

        private void DefinirNombresAntes_Ejecucion(ElementoOperacionAritmeticaEjecucion operacion)
        {
            if (operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades.DefinirNombresAntesEjecucion_Elemento)
            {
                DefinirNombresOperacion(operacion);
            }
        }

        private void DefinirNombresDurante_Ejecucion(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            EntidadNumero numeroResultado,
            int IndiceNumeroActualElementoOperacion)
        {
            if (operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento &&
                            !operacion.ElementoDiseñoRelacionado.UtilizarDefinicionNombres_AsignacionTextosInformacion)
            {
                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorFilas &&
                    operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion != TipoOperacionEjecucion.OperarPorSeparado)
                {
                    EstablecerNombreCantidad(numeroResultado, operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades, IndiceNumeroActualElementoOperacion, operacion);
                }
                else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado)
                {
                    //foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                    {
                        int indice = operacion.ElementosOperacion.IndexOf(itemOperacion);

                        EstablecerNombreCantidad(numeroResultado, operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades, indice, operacion, itemOperacion);
                    }
                }
                else if(operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                {
                    foreach (var itemOperacionItem in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                    {
                        if(itemOperacionItem.NumeroResultado_PorFilas != null)
                            numeroResultado = itemOperacionItem.NumeroResultado_PorFilas;
                        EstablecerNombreCantidad(numeroResultado, operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades, IndiceNumeroActualElementoOperacion, operacion);
                    }
                }
            }
        }

        private void DefinirNombresDespues_Ejecucion(ElementoOperacionAritmeticaEjecucion operacion)
        {
            if (operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades.DefinirNombresDespuesEjecucion_Elemento)
            {
                DefinirNombres_ResultadosOperacion(operacion);
            }
        }

        private void DefinirNombresOperacion(ElementoOperacionAritmeticaEjecucion operacion)
        {
            if (!operacion.ElementoDiseñoRelacionado.UtilizarDefinicionNombres_AsignacionTextosInformacion)
            {
                foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                {
                    //operacion.InicializarPosicionesClasificadores_Operandos();

                    //foreach (var itemClasificador in itemOperacion.Clasificadores_Cantidades)
                    {
                        if (Pausada) Pausar();
                        if (Detenida) return;

                        if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                        //operacion.AumentarPosicionesClasificadores_Operandos();
                        //this.itemClasificador = itemClasificador;

                        int posicion = 1;


                        foreach (var itemNumero in itemOperacion.Numeros
                            .Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)))
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            EstablecerNombreCantidad(itemNumero, operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades, posicion, operacion, itemOperacion);
                            posicion++;
                        }
                    }
                }
            }
        }

        private void DefinirNombres_ResultadosOperacion(ElementoOperacionAritmeticaEjecucion operacion)
        {
            if (!operacion.ElementoDiseñoRelacionado.UtilizarDefinicionNombres_AsignacionTextosInformacion)
            {
                foreach (var itemResultado in operacion.Numeros)
                {
                    foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                    {
                        //operacion.InicializarPosicionesClasificadores_Operandos();

                        //foreach (var itemClasificador in itemOperacion.Clasificadores_Cantidades)
                        {
                            if (Pausada) Pausar();
                            try {  } catch (Exception) { }
                            ;
                            if (Detenida) return;

                            if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                            //operacion.AumentarPosicionesClasificadores_Operandos();
                            //this.itemClasificador = itemClasificador;

                            int posicion = 1;


                            foreach (var itemNumero in itemOperacion.Numeros
                                .Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)))
                            {
                                EstablecerNombreCantidad(itemResultado, operacion.ElementoDiseñoRelacionado.DefinicionOpcionesNombresCantidades, posicion, operacion, itemOperacion);
                                posicion++;
                            }
                        }
                    }
                }
            }
        }

        private EntidadNumero SetearInsercion_ProcesamientoCantidades(ResultadoCondicionProcesamientoCantidades itemResultado,
            ElementoOperacionAritmeticaEjecucion operacion,
            List<EntidadNumero> ListaNumeros,
            int indiceLista,
            ref int indiceInsercion,
            bool restarUnIndice = false)
        {
            
                EntidadNumero subItem_InsertarOperando = null;

                switch (itemResultado.InsertarElemento_Procesamiento_Cantidad)
                {
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual:
                        if (indiceLista - (restarUnIndice ? 1 : 0) >= 0 &&
                        indiceLista - (restarUnIndice ? 1 : 0) < ListaNumeros.Count - 1 - (restarUnIndice ? 1 : 0))
                            subItem_InsertarOperando = ListaNumeros[indiceLista - (restarUnIndice ? 1 : 0)];
                        else
                            subItem_InsertarOperando = ListaNumeros.LastOrDefault();

                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadAnterior:
                        if (indiceLista - (restarUnIndice ? 1 : 0) > 0)
                            subItem_InsertarOperando = ListaNumeros[indiceLista - 1 - (restarUnIndice ? 1 : 0)];
                        else
                            subItem_InsertarOperando = ListaNumeros[indiceLista - (restarUnIndice ? 1 : 0)];
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadSiguiente:
                        if (indiceLista - (restarUnIndice ? 1 : 0) < ListaNumeros.Count - 1 - (restarUnIndice ? 1 : 0))
                            subItem_InsertarOperando = ListaNumeros[indiceLista + 1 - (restarUnIndice ? 1 : 0)];
                        else
                            subItem_InsertarOperando = ListaNumeros[indiceLista - (restarUnIndice ? 1 : 0)];
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecifica:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaAnteriores:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaSiguientes:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploAnteriores:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploSiguientes:
                        int indiceFijo = 0;

                        switch (itemResultado.InsertarElemento_Procesamiento_Cantidad)
                        {
                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecifica:
                                indiceFijo = (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaAnteriores:
                                indiceFijo = indiceLista - (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaSiguientes:
                                indiceFijo = indiceLista + (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploAnteriores:
                                indiceFijo = indiceLista - indiceLista * (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploSiguientes:
                                indiceFijo = indiceLista + indiceLista * (int)itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorPosicion;
                                break;

                        }

                        if (indiceFijo - (restarUnIndice ? 1 : 0) >= 0 &&
                            indiceFijo - (restarUnIndice ? 1 : 0) <= ListaNumeros.Count - 1 - (restarUnIndice ? 1 : 0))
                            subItem_InsertarOperando = ListaNumeros[indiceFijo - (restarUnIndice ? 1 : 0)];
                        else
                            subItem_InsertarOperando = ListaNumeros.LastOrDefault();
                        break;
                }

                switch (itemResultado.InsertarUbicacion_Procesamiento_Cantidad)
                {
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadActual:
                        if (indiceLista > 0)
                            indiceInsercion = indiceLista - (restarUnIndice ? 1 : 0);
                        else
                            indiceInsercion = indiceLista;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior:
                        if (indiceLista - (restarUnIndice ? 1 : 0) > 0)
                            indiceInsercion = indiceLista - 1 - (restarUnIndice ? 1 : 0);
                        else
                        {
                            if (itemResultado.NoInsertarCantidad_EnPosicion)
                            {
                                subItem_InsertarOperando = null;
                                itemResultado.InsertarCantidad_Procesamiento_Operandos = false;
                                itemResultado.InsertarCantidad_Procesamiento_Resultados = false;
                            }
                        }
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente:
                        if (indiceLista - (restarUnIndice ? 1 : 0) <= ListaNumeros.Count - 1 - (restarUnIndice ? 1 : 0))
                            indiceInsercion = indiceLista + 1 - (restarUnIndice ? 1 : 0);
                        else
                        {
                            if (itemResultado.NoInsertarCantidad_EnPosicion)
                            {
                                subItem_InsertarOperando = null;
                                itemResultado.InsertarCantidad_Procesamiento_Operandos = false;
                                itemResultado.InsertarCantidad_Procesamiento_Resultados = false;
                            }
                        }
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecifica:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaAnteriores:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaSiguientes:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploAnteriores:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploSiguientes:
                        int indiceCalculado = 0;

                        if (indiceLista > 0)
                            indiceCalculado = indiceLista - (restarUnIndice ? 1 : 0);
                        else
                            indiceCalculado = indiceLista;

                        switch (itemResultado.InsertarUbicacion_Procesamiento_Cantidad)
                        {
                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecifica:
                                indiceCalculado = (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaAnteriores:
                                indiceCalculado = indiceCalculado - (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaSiguientes:
                                indiceCalculado = indiceCalculado + (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploAnteriores:
                                indiceCalculado = indiceCalculado - indiceCalculado * (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;

                            case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploSiguientes:
                                indiceCalculado = indiceCalculado + indiceCalculado * (int)itemResultado.InsertarUbicacion_Procesamiento_Cantidad_ValorPosicion;
                                break;
                        }

                        if (indiceCalculado >= 0 &&
                            indiceCalculado <= ListaNumeros.Count - 1)
                            indiceInsercion = indiceCalculado;
                        else
                        {
                            if (itemResultado.NoInsertarCantidad_EnPosicion)
                            {
                                subItem_InsertarOperando = null;
                                itemResultado.InsertarCantidad_Procesamiento_Operandos = false;
                                itemResultado.InsertarCantidad_Procesamiento_Resultados = false;
                            }
                        }

                        break;
                }
            

            return subItem_InsertarOperando;
        }

        private void InsertarProcesarCantidades_Operacion(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            EntidadNumero numero,
            EntidadNumero numeroResultado,
            List<EntidadNumero> numeroInsertado_Operandos,
            List<EntidadNumero> numeroInsertado_Resultados,
            List<EntidadNumero> ListaNumeros,
            List<EntidadNumero> ListaNumerosResultado,
            int indiceInsercion,
            ref int indiceClasificadores,
            ref int indiceCantidad,
            ref int indiceLista,
            ref int cantidadNumeros_PorSeparado,
            bool EjecutarLogicas)
        {
            if (EjecutarLogicas)
            {
                foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                {
                    if (itemResultado.InsertarCantidad_Procesamiento_Operandos &&
                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                    {
                        if(itemResultado.subItem_InsertarOperando_Asociado != null)
                        {
                            EntidadNumero numeroInsertado = null;

                            if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                                numeroInsertado = new EntidadNumero() { Numero = itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo };
                            else
                                numeroInsertado = itemResultado.subItem_InsertarOperando_Asociado.CopiarObjeto(true, itemClasificador, operacion, true);
                            
                            numeroInsertado.EsCantidadInsertada_ProcesamientoCantidades = true;
                            numeroInsertado.Numero = itemResultado.Operar_ProcesamientoCantidades(numeroInsertado.Numero, ListaNumeros, operacion);

                            if (itemResultado.InsertarElemento_Procesamiento_Operandos)
                            {
                                if (indiceInsercion > -1)
                                {
                                    ListaNumeros.Insert(indiceInsercion, numeroInsertado);
                                    itemOperacion.Numeros.Insert(indiceInsercion, numeroInsertado);
                                }
                                else
                                {
                                    ListaNumeros.Add(numeroInsertado);
                                    itemOperacion.Numeros.Add(numeroInsertado);
                                }

                                if (numero.NoIncluirTextosInformacion_CantidadAInsertar)
                                {
                                    if (indiceInsercion > -1)
                                    {
                                        ListaNumeros[indiceInsercion].Textos.Clear();
                                        itemOperacion.Numeros[indiceInsercion].Textos.Clear();
                                    }
                                    else
                                    {
                                        ListaNumeros.LastOrDefault().Textos.Clear();
                                        itemOperacion.Numeros.LastOrDefault().Textos.Clear();
                                    }
                                }

                                if(itemResultado.Desplazamiento_PosicionAnterior)
                                {
                                    if (indiceLista > 0)
                                        indiceLista--;

                                    if (indiceClasificadores > 0)
                                    {
                                        indiceCantidad--;
                                        //indiceClasificadores--;
                                    }
                                }

                                if(itemResultado.Desplazamiento_PosicionPosterior)
                                {
                                    if (indiceLista < ListaNumeros.Count - 1)
                                        indiceLista++;

                                    if (indiceClasificadores < ListaNumeros.Count - 1)
                                    {
                                        indiceCantidad++;
                                        //indiceClasificadores++;
                                    }
                                }
                            }

                            if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                            {
                                if (indiceInsercion > -1)
                                {
                                    ListaNumerosResultado.Insert(indiceInsercion, numeroInsertado);
                                }
                                else
                                {
                                    ListaNumerosResultado.Add(numeroInsertado);
                                }

                                if (numero.NoIncluirTextosInformacion_CantidadAInsertar)
                                {
                                    if (indiceInsercion > -1)
                                    {
                                        ListaNumerosResultado[indiceInsercion].Textos.Clear();
                                    }
                                    else
                                    {
                                        ListaNumerosResultado.LastOrDefault().Textos.Clear();
                                    }
                                }

                                if(itemResultado.Desplazamiento_PosicionAnterior)
                                {
                                    if (indiceLista > 0)
                                        indiceLista--;

                                    if (indiceClasificadores > 0)
                                    {
                                        indiceCantidad--;
                                        //indiceClasificadores--;
                                    }
                                }

                                if(itemResultado.Desplazamiento_PosicionPosterior)
                                {
                                    if (indiceLista < ListaNumerosResultado.Count - 1)
                                        indiceLista++;

                                    if (indiceClasificadores < ListaNumerosResultado.Count - 1)
                                    {
                                        indiceCantidad++;
                                        //indiceClasificadores++;
                                    }
                                }
                            }

                            numeroInsertado_Operandos.Add(numeroInsertado);

                            //insertar = true;
                        }

                    }

                    if (itemResultado.InsertarCantidad_Procesamiento_Resultados)
                    {
                        {
                            EntidadNumero numeroInsertado = null;

                            if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                                numeroInsertado = new EntidadNumero()
                                {
                                    Numero = itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo
                                };
                            else
                                numeroInsertado = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                        operacion, ListaNumerosResultado, indiceLista, ref indiceInsercion);

                            numeroInsertado = numeroInsertado.CopiarObjeto(true, itemClasificador, operacion, true);

                            numeroInsertado.EsCantidadInsertada_ProcesamientoCantidades = true;
                            numeroInsertado.EsCantidadInsertada_ProcesamientoCantidadesTemp = true;

                            numeroInsertado.Numero = itemResultado.Operar_ProcesamientoCantidades(numeroInsertado.Numero, ListaNumerosResultado, operacion);
                            AgregarClasificador_Cantidad(operacion, itemClasificador, numeroInsertado);

                            if (itemResultado.InsertarElemento_Procesamiento_Operandos)
                            {
                                if (indiceInsercion > -1)
                                {
                                    ListaNumeros.Insert(indiceInsercion, numeroInsertado);
                                    itemOperacion.Numeros.Insert(indiceInsercion, numeroInsertado);
                                }
                                else
                                {
                                    ListaNumeros.Add(numeroInsertado);
                                    itemOperacion.Numeros.Add(numeroInsertado);
                                }

                                if (numero.NoIncluirTextosInformacion_CantidadAInsertar)
                                {
                                    if (indiceInsercion > -1)
                                    {
                                        ListaNumeros[indiceInsercion].Textos.Clear();
                                        itemOperacion.Numeros[indiceInsercion].Textos.Clear();
                                    }
                                    else
                                    {
                                        ListaNumeros.LastOrDefault().Textos.Clear();
                                        itemOperacion.Numeros.LastOrDefault().Textos.Clear();
                                    }
                                }

                                if (itemResultado.Desplazamiento_PosicionAnterior)
                                {
                                    if (indiceLista > 0)
                                        indiceLista--;

                                    if (indiceClasificadores > 0)
                                    {
                                        indiceCantidad--;
                                        //indiceClasificadores--;
                                    }
                                }

                                if (itemResultado.Desplazamiento_PosicionPosterior)
                                {
                                    if (indiceLista < ListaNumeros.Count - 1)
                                        indiceLista++;

                                    if (indiceClasificadores < ListaNumeros.Count - 1)
                                    {
                                        indiceCantidad++;
                                        //indiceClasificadores++;
                                    }
                                }
                            }

                            if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                            {
                                if (indiceInsercion > -1)
                                {
                                    ListaNumerosResultado.Insert(indiceInsercion, numeroInsertado);
                                }
                                else
                                {
                                    ListaNumerosResultado.Add(numeroInsertado);
                                }

                                if (numero.NoIncluirTextosInformacion_CantidadAInsertar)
                                {
                                    if (indiceInsercion > -1)
                                    {
                                        ListaNumerosResultado[indiceInsercion].Textos.Clear();
                                    }
                                    else
                                    {
                                        ListaNumerosResultado.LastOrDefault().Textos.Clear();
                                    }
                                }

                                if (itemResultado.Desplazamiento_PosicionAnterior)
                                {
                                    cantidadNumeros_PorSeparado--;

                                    if (indiceLista > 0)
                                        indiceLista--;

                                    if (indiceClasificadores > 0)
                                    {
                                        indiceCantidad--;
                                        //indiceClasificadores--;
                                    }
                                }

                                if(itemResultado.Desplazamiento_PosicionPosterior)
                                {
                                    cantidadNumeros_PorSeparado++;

                                    if (indiceLista < ListaNumerosResultado.Count - 1)
                                        indiceLista++;

                                    if (indiceClasificadores < ListaNumerosResultado.Count - 1)
                                    {
                                        indiceCantidad++;
                                        //indiceClasificadores++;
                                    }
                                }
                            }

                            itemResultado.NumeroInsertado_Asociado = numeroInsertado;
                            numeroInsertado_Resultados.Add(numeroInsertado);

                            //insertar = true;
                        }

                    }
                }
            }
        }

        private void InsertarQuitarProcesarCantidades_Resultados_Operacion(ElementoOperacionAritmeticaEjecucion operacion,
            EntidadNumero numero,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            EntidadNumero numeroResultado,
            List<EntidadNumero> NumerosResultado,
            List<EntidadNumero> ListaNumeros,
            ref string strMensajeLog,
            ref string strMensajeLogResultados,
            ref bool conNumeros,
            ref int indiceClasificadores,
            bool Setear_IndiceClasificadores,
            ref int cantidadNumeros_PorSeparado,
            ref int IndiceNumeroActualElementoOperacion,
            ref int cantidadFilas_Adicionales,
            bool EjecutarLogicas)
        {
            if (EjecutarLogicas)
            {
                operacion.InicializarPosicionesElementosOperandos(this, null);

                if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos)
                {
                    operacion.LimpiarEstadoProcesamiento();
                    
                    if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                    {
                        foreach (var itemOperacionItem in operacion.ElementosOperacion)
                        {
                            operacion.ProcesarCantidades(this, operacion, itemOperacionItem, numero, numeroResultado, itemClasificador, NumerosResultado);
                        }
                    }

                    if (operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Potencia &&
                        operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Raiz &&
                        operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Porcentaje &&
                        operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Logaritmo)
                    {
                        int indiceQuitar = NumerosResultado.Count;

                        foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                        {
                            if (itemResultado.QuitarCantidad_Procesamiento_Resultados)
                            {
                                indiceQuitar--;
                                NumerosResultado[indiceQuitar].EsCantidadQuitada_ProcesamientoCantidades = true;
                            }
                        }

                        foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                        {                            
                            {
                                int indiceInsercion = -1;
                                
                                EntidadNumero numeroInsertadoResultado_Obtenido = null;

                                if (itemResultado.InsertarCantidad_Procesamiento_Resultados)
                                {
                                    numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                                operacion, NumerosResultado, indiceClasificadores, ref indiceInsercion);

                                    if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                    {
                                        ProcesarBloqueInsertarCantidades_Resultados_OperarTodos(numeroInsertadoResultado_Obtenido, itemResultado, operacion, itemOperacion, 
                                            ListaNumeros, NumerosResultado, indiceInsercion, ref indiceClasificadores, false, 
                                            numeroResultado, ref conNumeros, ref strMensajeLogResultados, ref strMensajeLog);
                                    }

                                    if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                    {
                                        ProcesarBloqueInsertarCantidades_Resultados_OperarTodos(numeroInsertadoResultado_Obtenido, itemResultado, operacion,  itemOperacion,
                                            NumerosResultado, NumerosResultado, indiceInsercion, ref indiceClasificadores, Setear_IndiceClasificadores,
                                            numeroResultado, ref conNumeros, ref strMensajeLogResultados, ref strMensajeLog);
                                    }
                                }

                                indiceInsercion = -1;

                                numeroInsertadoResultado_Obtenido = null;

                                if (itemResultado.InsertarCantidad_Procesamiento_Operandos)
                                {
                                    numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                            operacion, ListaNumeros, indiceClasificadores, ref indiceInsercion);

                                    if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                    {
                                        ProcesarBloqueInsertarCantidades_Resultados_OperarTodos(numeroInsertadoResultado_Obtenido, itemResultado, operacion, itemOperacion,
                                            ListaNumeros, ListaNumeros, indiceInsercion, ref indiceClasificadores, false, 
                                            numeroResultado, ref conNumeros, ref strMensajeLogResultados, ref strMensajeLog);
                                    }

                                    if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                    {
                                        ProcesarBloqueInsertarCantidades_Resultados_OperarTodos(numeroInsertadoResultado_Obtenido, itemResultado, operacion, itemOperacion,
                                            NumerosResultado, ListaNumeros, indiceInsercion, ref indiceClasificadores, Setear_IndiceClasificadores,
                                            numeroResultado, ref conNumeros, ref strMensajeLogResultados, ref strMensajeLog);
                                    }
                                }
                            }
                        }
                    }


                }
                else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado)
                {
                    operacion.LimpiarEstadoProcesamiento();

                    if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                    {
                        operacion.ProcesarCantidades(this, operacion, itemOperacion, numero, numeroResultado, itemClasificador, NumerosResultado);
                    }

                    int indiceQuitar = NumerosResultado.Count;

                    foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                    {
                        if (itemResultado.QuitarCantidad_Procesamiento_Resultados)
                        {
                            indiceQuitar--;
                            NumerosResultado[indiceQuitar].EsCantidadQuitada_ProcesamientoCantidades = true;
                        }
                    }

                    foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                    {
                        { 
                            int indiceInsercion = -1;
                            EntidadNumero numeroInsertadoResultado_Obtenido = null;

                            if (itemResultado.InsertarCantidad_Procesamiento_Resultados)
                            {
                                numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                        operacion, NumerosResultado, indiceClasificadores + cantidadNumeros_PorSeparado, ref indiceInsercion);


                                if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                {

                                    ProcesarBloqueInsertarCantidades_Resultados_PorSeparado(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                        itemOperacion, ListaNumeros, NumerosResultado, indiceInsercion, ref indiceClasificadores, false, 
                                        false, ref cantidadNumeros_PorSeparado,
                                        ref strMensajeLogResultados, ref strMensajeLog);
                                }

                                if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                {

                                    ProcesarBloqueInsertarCantidades_Resultados_PorSeparado(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                        itemOperacion, NumerosResultado, NumerosResultado, indiceInsercion, ref indiceClasificadores, Setear_IndiceClasificadores, 
                                        false, ref cantidadNumeros_PorSeparado, ref strMensajeLogResultados, ref strMensajeLog);
                                }
                            }

                            indiceInsercion = -1;
                            numeroInsertadoResultado_Obtenido = null;

                            if (itemResultado.InsertarCantidad_Procesamiento_Operandos)
                            {
                                numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                        operacion, ListaNumeros, indiceClasificadores + cantidadNumeros_PorSeparado, ref indiceInsercion);


                                if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                {

                                    ProcesarBloqueInsertarCantidades_Resultados_PorSeparado(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                        itemOperacion, ListaNumeros, ListaNumeros, indiceInsercion, ref indiceClasificadores, false,
                                        true, ref cantidadNumeros_PorSeparado,
                                        ref strMensajeLogResultados, ref strMensajeLog);
                                }

                                if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                {

                                    ProcesarBloqueInsertarCantidades_Resultados_PorSeparado(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                        itemOperacion, NumerosResultado, ListaNumeros, indiceInsercion, ref indiceClasificadores, Setear_IndiceClasificadores,
                                        true, ref cantidadNumeros_PorSeparado,
                                        ref strMensajeLogResultados, ref strMensajeLog);
                                }
                            }
                        }
                    }
                }
                else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                {
                    if (operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Potencia &&
                        operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Raiz &&
                        operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Porcentaje &&
                        operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.Logaritmo)
                    {
                        int indiceQuitar = NumerosResultado.Count;

                        foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                        {
                            if (itemResultado.QuitarCantidad_Procesamiento_Resultados)
                            {
                                indiceQuitar--;
                                NumerosResultado[indiceQuitar].EsCantidadQuitada_ProcesamientoCantidades = true;
                            }
                        }

                        foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                        {
                            {
                                {
                                    int indiceInsercion = -1;                                    
                                    EntidadNumero numeroInsertadoResultado_Obtenido = null;

                                    if (itemResultado.InsertarCantidad_Procesamiento_Resultados)
                                    {
                                        numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                                operacion, NumerosResultado, IndiceNumeroActualElementoOperacion, ref indiceInsercion);

                                        if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                        {
                                            ProcesarBloqueInsertarCantidades_Resultados_PorFilas1(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                                ListaNumeros, NumerosResultado, indiceInsercion, ref IndiceNumeroActualElementoOperacion, ref cantidadFilas_Adicionales,
                                                ref strMensajeLogResultados, ref strMensajeLog);
                                        }

                                        if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                        {
                                            ProcesarBloqueInsertarCantidades_Resultados_PorFilas1(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                                NumerosResultado, NumerosResultado, indiceInsercion, ref IndiceNumeroActualElementoOperacion, ref cantidadFilas_Adicionales,
                                                ref strMensajeLogResultados, ref strMensajeLog);
                                        }
                                    }

                                    indiceInsercion = -1;
                                    numeroInsertadoResultado_Obtenido = null;

                                    if (itemResultado.InsertarCantidad_Procesamiento_Operandos)
                                    {
                                        numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                                operacion, ListaNumeros, IndiceNumeroActualElementoOperacion, ref indiceInsercion);

                                        if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                        {
                                            ProcesarBloqueInsertarCantidades_Resultados_PorFilas1(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                                ListaNumeros, ListaNumeros, indiceInsercion, ref IndiceNumeroActualElementoOperacion, ref cantidadFilas_Adicionales,
                                                ref strMensajeLogResultados, ref strMensajeLog);
                                        }

                                        if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                        {
                                            ProcesarBloqueInsertarCantidades_Resultados_PorFilas1(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                                NumerosResultado, ListaNumeros, indiceInsercion, ref IndiceNumeroActualElementoOperacion, ref cantidadFilas_Adicionales,
                                                ref strMensajeLogResultados, ref strMensajeLog);
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        operacion.LimpiarEstadoProcesamiento();
                        if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                        {
                            operacion.ProcesarCantidades(this, operacion, itemOperacion, numero, numeroResultado, itemClasificador, NumerosResultado, true);
                        }

                        int indiceQuitar = NumerosResultado.Count;

                        foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                        {
                            if (itemResultado.QuitarCantidad_Procesamiento_Resultados)
                            {
                                indiceQuitar--;
                                NumerosResultado[indiceQuitar].EsCantidadQuitada_ProcesamientoCantidades = true;
                            }
                        }

                        //foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                        //{
                        //    if (itemResultado.QuitarCantidad_Procesamiento_Resultados)
                        //    {
                        //        foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                        //        {
                        //            EntidadNumero numeroResultadoItem = itemOperacionNumero.NumeroResultado_PorFilas;
                        //            if (NumerosResultado.Contains(numeroResultadoItem) && numeroResultadoItem.AgregarNumero_PorFilas)
                        //            {
                        //                numeroResultadoItem.EsCantidadQuitada_ProcesamientoCantidades = true;
                        //            }
                        //        }
                        //    }
                        //}
                        foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                        {
                            int indiceInsercion = -1;
                            EntidadNumero numeroInsertadoResultado_Obtenido = null;

                            if (itemResultado.InsertarCantidad_Procesamiento_Resultados)
                            {
                                numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                        operacion, NumerosResultado, indiceClasificadores, ref indiceInsercion);

                                if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                {
                                    ProcesarBloqueInsertarCantidades_Resultados_PorFilas2(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                        ListaNumeros, NumerosResultado, indiceInsercion, indiceClasificadores, ref IndiceNumeroActualElementoOperacion,
                                        ref strMensajeLogResultados, ref strMensajeLog);
                                }

                                if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                {
                                    ProcesarBloqueInsertarCantidades_Resultados_PorFilas2(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                        NumerosResultado, NumerosResultado, indiceInsercion, indiceClasificadores, ref IndiceNumeroActualElementoOperacion,
                                        ref strMensajeLogResultados, ref strMensajeLog);
                                }
                            }

                            indiceInsercion = -1;
                            numeroInsertadoResultado_Obtenido = null;

                            if (itemResultado.InsertarCantidad_Procesamiento_Operandos)
                            {
                                numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                        operacion, ListaNumeros, indiceClasificadores, ref indiceInsercion);

                                if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                {
                                    ProcesarBloqueInsertarCantidades_Resultados_PorFilas2(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                        ListaNumeros, ListaNumeros, indiceInsercion, indiceClasificadores, ref IndiceNumeroActualElementoOperacion,
                                        ref strMensajeLogResultados, ref strMensajeLog);
                                }

                                if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                {
                                    ProcesarBloqueInsertarCantidades_Resultados_PorFilas2(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                        NumerosResultado, ListaNumeros, indiceInsercion, indiceClasificadores, ref IndiceNumeroActualElementoOperacion,
                                        ref strMensajeLogResultados, ref strMensajeLog);
                                }
                            }
                        }
                    }
                }
                else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                {
                    operacion.LimpiarEstadoProcesamiento();

                    if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                    {
                        operacion.ProcesarCantidades(this, operacion, itemOperacion, numero, numeroResultado, itemClasificador, NumerosResultado, true);
                    }

                    int indiceQuitar = NumerosResultado.Count;

                    foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                    {
                        if (itemResultado.QuitarCantidad_Procesamiento_Resultados)
                        {
                            indiceQuitar--;
                            NumerosResultado[indiceQuitar].EsCantidadQuitada_ProcesamientoCantidades = true;
                        }
                    }

                    foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                    {
                        
                        {
                            {
                                int indiceInsercion = -1;
                                EntidadNumero numeroInsertadoResultado_Obtenido = null;

                                if (itemResultado.InsertarCantidad_Procesamiento_Resultados)
                                {
                                    numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                            operacion, NumerosResultado, indiceClasificadores, ref indiceInsercion);

                                    if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                    {
                                        ProcesarBloqueInsertarCantidades_Resultados_PorFilasPorSeparado(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                            ListaNumeros, NumerosResultado, indiceInsercion, ref indiceClasificadores, false, 
                                            ref strMensajeLogResultados, ref strMensajeLog);
                                    }

                                    if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                    {
                                        ProcesarBloqueInsertarCantidades_Resultados_PorFilasPorSeparado(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                            NumerosResultado, NumerosResultado, indiceInsercion, ref indiceClasificadores, Setear_IndiceClasificadores, 
                                            ref strMensajeLogResultados, ref strMensajeLog);
                                    }
                                }

                                indiceInsercion = -1;
                                numeroInsertadoResultado_Obtenido = null;

                                if (itemResultado.InsertarCantidad_Procesamiento_Operandos)
                                {
                                    numeroInsertadoResultado_Obtenido = SetearInsercion_ProcesamientoCantidades(itemResultado,
                                                            operacion, ListaNumeros, indiceClasificadores, ref indiceInsercion);

                                    if (itemResultado.InsertarElemento_Procesamiento_Operandos &&
                                        itemResultado.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(itemOperacion.ElementoDiseñoRelacionado))
                                    {
                                        ProcesarBloqueInsertarCantidades_Resultados_PorFilasPorSeparado(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                            ListaNumeros, ListaNumeros, indiceInsercion, ref indiceClasificadores, false, 
                                            ref strMensajeLogResultados, ref strMensajeLog);
                                    }

                                    if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                    {
                                        ProcesarBloqueInsertarCantidades_Resultados_PorFilasPorSeparado(numeroInsertadoResultado_Obtenido, itemResultado, operacion,
                                            NumerosResultado, ListaNumeros, indiceInsercion, ref indiceClasificadores, Setear_IndiceClasificadores, 
                                            ref strMensajeLogResultados, ref strMensajeLog);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ProcesarBloqueInsertarCantidades_Resultados_OperarTodos(EntidadNumero numeroInsertadoResultado_Obtenido,
            ResultadoCondicionProcesamientoCantidades itemResultado, ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            List<EntidadNumero> ListaNumerosHacia, List<EntidadNumero> ListaNumerosDesde, int indiceInsercion, ref int indiceClasificadores,
            bool Setear_IndiceClasificadores, EntidadNumero numeroResultado, ref bool conNumeros, ref string strMensajeLogResultados, ref string strMensajeLog)
        {
            if (numeroInsertadoResultado_Obtenido != null)
            {
                EntidadNumero numeroInsertadoResultado = null;

                numeroInsertadoResultado = numeroInsertadoResultado_Obtenido.CopiarObjeto(true, itemClasificador, operacion, true);

                if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                {
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo, ListaNumerosDesde, operacion);
                }
                else
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(numeroInsertadoResultado.Numero, ListaNumerosDesde, operacion);

                numeroInsertadoResultado.EsCantidadInsertada_ProcesamientoCantidades = true;

                if (indiceInsercion > -1 &&
        !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
        (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
        indiceInsercion > indiceClasificadores)))
                    ListaNumerosHacia.Insert(indiceInsercion, numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));
                else
                    ListaNumerosHacia.Add(numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));

                if (ListaNumerosHacia[indiceClasificadores].NoIncluirTextosInformacion_CantidadAInsertar)
                {
                    if (indiceInsercion > -1 &&
        !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
        (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
        indiceInsercion > indiceClasificadores)))
                        ListaNumerosHacia[indiceInsercion].Textos.Clear();
                    else
                        ListaNumerosHacia.LastOrDefault().Textos.Clear();
                }

                AsignarTextosInformacion_Elemento(operacion, itemOperacion, numeroInsertadoResultado_Obtenido, numeroResultado, 
                    numeroInsertadoResultado_Obtenido.NoIncluirTextosInformacion_CantidadAInsertar, 
                    operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones, false);

                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numeroInsertadoResultado))
                {
                    strMensajeLogResultados += "Se agrega " + numeroInsertadoResultado.Nombre + ", " + numeroInsertadoResultado.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numeroInsertadoResultado.ObtenerTextosInformacion_Cadena() + "\n";
                    strMensajeLog += "Se agrega " + numeroInsertadoResultado.Nombre + ", " + numeroInsertadoResultado.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numeroInsertadoResultado.ObtenerTextosInformacion_Cadena() + "\n";
                }

                conNumeros = true;

                if (Setear_IndiceClasificadores &&
                    indiceClasificadores < ListaNumerosHacia.Count - 1)
                    indiceClasificadores++;
            }
        }

        private void ProcesarBloqueInsertarCantidades_Resultados_PorSeparado(EntidadNumero numeroInsertadoResultado_Obtenido,
            ResultadoCondicionProcesamientoCantidades itemResultado, ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            List<EntidadNumero> ListaNumerosHacia, List<EntidadNumero> ListaNumerosDesde, int indiceInsercion,
            ref int indiceClasificadores, bool Setear_IndiceClasificadores, bool ModificarOperandos_SetearCantidadNumeros_PorSeparado, 
            ref int cantidadNumeros_PorSeparado, ref string strMensajeLogResultados, ref string strMensajeLog)
        {
            if (numeroInsertadoResultado_Obtenido != null)
            {
                EntidadNumero numeroInsertadoResultado = null;

                numeroInsertadoResultado = numeroInsertadoResultado_Obtenido.CopiarObjeto(true, itemClasificador, operacion, true);

                if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                {
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo, ListaNumerosDesde, operacion);
                }
                else
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(numeroInsertadoResultado.Numero, ListaNumerosDesde, operacion);

                numeroInsertadoResultado.EsCantidadInsertada_ProcesamientoCantidades = true;

                if (indiceInsercion > -1 &&
                    !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
            (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
            indiceInsercion > indiceClasificadores + cantidadNumeros_PorSeparado)))
                {
                    ListaNumerosHacia.Insert(indiceInsercion, numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));
                    if (ModificarOperandos_SetearCantidadNumeros_PorSeparado)
                    {
                        itemOperacion.Numeros.Insert(indiceInsercion, numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));
                    }
                }
                else
                {
                    ListaNumerosHacia.Add(numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));
                    if (ModificarOperandos_SetearCantidadNumeros_PorSeparado)
                    {
                        itemOperacion.Numeros.Add(numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));
                    }
                }

                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(ListaNumerosHacia.Last()))
                {
                    strMensajeLogResultados += "Se agrega la cantidad: " + ListaNumerosHacia.Last().Nombre + " : " + ListaNumerosHacia.Last().Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ListaNumerosHacia.Last().ObtenerTextosInformacion_Cadena() + "\n";
                    strMensajeLog += "Se agrega la cantidad: " + ListaNumerosHacia.Last().Nombre + " : " + ListaNumerosHacia.Last().Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ListaNumerosHacia.Last().ObtenerTextosInformacion_Cadena() + "\n";
                }

                if (ListaNumerosHacia[indiceClasificadores + cantidadNumeros_PorSeparado].NoIncluirTextosInformacion_CantidadAInsertar)
                {
                    if (indiceInsercion > -1 &&
                        !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
            (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
            indiceInsercion > indiceClasificadores + cantidadNumeros_PorSeparado)))
                    {
                        ListaNumerosHacia[indiceInsercion].Textos.Clear();
                        if (ModificarOperandos_SetearCantidadNumeros_PorSeparado)
                        {
                            itemOperacion.Numeros[indiceInsercion].Textos.Clear();
                        }
                    }
                    else
                    {
                        ListaNumerosHacia.LastOrDefault().Textos.Clear();
                        if (ModificarOperandos_SetearCantidadNumeros_PorSeparado)
                        {
                            itemOperacion.Numeros.LastOrDefault().Textos.Clear();
                        }
                    }
                }

                operacion.PosicionActualNumero_CondicionesOperador_Implicacion++;

                if(ModificarOperandos_SetearCantidadNumeros_PorSeparado &&
                    indiceClasificadores + cantidadNumeros_PorSeparado < ListaNumerosHacia.Count - 1)
                    cantidadNumeros_PorSeparado++;

                if (Setear_IndiceClasificadores &&
                    indiceClasificadores + cantidadNumeros_PorSeparado < ListaNumerosHacia.Count - 1)
                    indiceClasificadores++;

                if (itemResultado.Desplazamiento_PosicionAnterior)
                {
                    if (indiceClasificadores + cantidadNumeros_PorSeparado > 0)
                    {
                        cantidadNumeros_PorSeparado--;
                        operacion.PosicionActualNumero_CondicionesOperador_Implicacion--;
                    }
                }

                if (itemResultado.Desplazamiento_PosicionPosterior)
                {
                    if (indiceClasificadores + cantidadNumeros_PorSeparado < ListaNumerosHacia.Count - 1)
                    {
                        cantidadNumeros_PorSeparado++;
                        operacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                    }
                }
            }
        }

        private void ProcesarBloqueInsertarCantidades_Resultados_PorFilas1(EntidadNumero numeroInsertadoResultado_Obtenido,
            ResultadoCondicionProcesamientoCantidades itemResultado, ElementoOperacionAritmeticaEjecucion operacion,
            List<EntidadNumero> ListaNumerosHacia, List<EntidadNumero> ListaNumerosDesde, int indiceInsercion,
            ref int IndiceNumeroActualElementoOperacion, ref int cantidadFilas_Adicionales,
            ref string strMensajeLogResultados, ref string strMensajeLog)
        {
            if (numeroInsertadoResultado_Obtenido != null)
            {
                EntidadNumero numeroInsertadoResultado = null;
                numeroInsertadoResultado = numeroInsertadoResultado_Obtenido.CopiarObjeto(true, itemClasificador, operacion, true);

                if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                {
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo, ListaNumerosDesde, operacion);
                }
                else
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(numeroInsertadoResultado.Numero, ListaNumerosDesde, operacion);

                numeroInsertadoResultado.EsCantidadInsertada_ProcesamientoCantidades = true;

                if (indiceInsercion > -1)
                    ListaNumerosHacia.Insert(indiceInsercion, numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));
                else
                    ListaNumerosHacia.Add(numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));

                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numeroInsertadoResultado))
                {
                    strMensajeLogResultados += "Se agrega la cantidad: " + numeroInsertadoResultado.Nombre + " : " + numeroInsertadoResultado.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numeroInsertadoResultado.ObtenerTextosInformacion_Cadena() + "\n";
                    strMensajeLog += "Se agrega la cantidad: " + numeroInsertadoResultado.Nombre + " : " + numeroInsertadoResultado.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numeroInsertadoResultado.ObtenerTextosInformacion_Cadena() + "\n";
                }

                if (numeroInsertadoResultado.NoIncluirTextosInformacion_CantidadAInsertar)
                {
                    if (indiceInsercion > -1)
                        ListaNumerosHacia[indiceInsercion].Textos.Clear();
                    else
                        ListaNumerosHacia[ListaNumerosHacia.Count - 2].Textos.Clear();
                }

                operacion.PosicionActualNumero_CondicionesOperador_Implicacion++;

                if(IndiceNumeroActualElementoOperacion < ListaNumerosHacia.Count - 1)
                    IndiceNumeroActualElementoOperacion++;
                cantidadFilas_Adicionales++;
            }
        }

        private void ProcesarBloqueInsertarCantidades_Resultados_PorFilas2(EntidadNumero numeroInsertadoResultado_Obtenido,
            ResultadoCondicionProcesamientoCantidades itemResultado, ElementoOperacionAritmeticaEjecucion operacion,
            List<EntidadNumero> ListaNumerosHacia, List<EntidadNumero> ListaNumerosDesde, int indiceInsercion, int indiceClasificadores,
            ref int IndiceNumeroActualElementoOperacion, ref string strMensajeLogResultados, ref string strMensajeLog)
        {
            if (numeroInsertadoResultado_Obtenido != null)
            {
                EntidadNumero numeroInsertadoResultado = null;
                numeroInsertadoResultado = numeroInsertadoResultado_Obtenido.CopiarObjeto(true, itemClasificador, operacion, true);

                if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                {
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo, ListaNumerosDesde, operacion);
                }
                else
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(numeroInsertadoResultado.Numero, ListaNumerosDesde, operacion);

                numeroInsertadoResultado.EsCantidadInsertada_ProcesamientoCantidades = true;

                if (indiceInsercion > -1 &&
                        !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
        (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
        indiceInsercion > indiceClasificadores)))
                    ListaNumerosHacia.Insert(indiceInsercion, numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));
                else
                    ListaNumerosHacia.Add(numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));

                if (ListaNumerosHacia[indiceClasificadores].NoIncluirTextosInformacion_CantidadAInsertar)
                {
                    if (indiceInsercion > -1 &&
                        !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
        (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
        indiceInsercion > indiceClasificadores)))
                        ListaNumerosHacia[indiceInsercion].Textos.Clear();
                    else
                        ListaNumerosHacia.LastOrDefault().Textos.Clear();
                }

                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(ListaNumerosHacia.Last()))
                {
                    strMensajeLogResultados += "Se agrega la cantidad: " + ListaNumerosHacia.Last().Nombre + " : " + ListaNumerosHacia.Last().Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ListaNumerosHacia.Last().ObtenerTextosInformacion_Cadena() + "\n";
                    strMensajeLog += "Se agrega la cantidad: " + ListaNumerosHacia.Last().Nombre + " : " + ListaNumerosHacia.Last().Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ListaNumerosHacia.Last().ObtenerTextosInformacion_Cadena() + "\n";
                }

                if (ListaNumerosHacia[indiceClasificadores].NoIncluirTextosInformacion_CantidadAInsertar)
                {
                    if (indiceInsercion > -1 &&
                        !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
        (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
        indiceInsercion > indiceClasificadores)))
                        ListaNumerosHacia[indiceInsercion].Textos.Clear();
                    else
                        ListaNumerosHacia[ListaNumerosHacia.Count - 2].Textos.Clear();
                }

                operacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                
                if(IndiceNumeroActualElementoOperacion < ListaNumerosHacia.Count - 1)
                    IndiceNumeroActualElementoOperacion++;
            }
        }

        private void ProcesarBloqueInsertarCantidades_Resultados_PorFilasPorSeparado(EntidadNumero numeroInsertadoResultado_Obtenido,
            ResultadoCondicionProcesamientoCantidades itemResultado, ElementoOperacionAritmeticaEjecucion operacion,
            List<EntidadNumero> ListaNumerosHacia, List<EntidadNumero> ListaNumerosDesde, int indiceInsercion, ref int indiceClasificadores,
            bool Setear_IndiceClasificadores, ref string strMensajeLogResultados, ref string strMensajeLog)
        {
            if (numeroInsertadoResultado_Obtenido != null)
            {
                EntidadNumero numeroInsertadoResultado = null;
                numeroInsertadoResultado = numeroInsertadoResultado_Obtenido.CopiarObjeto(true, itemClasificador, operacion, true);

                if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                {
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(itemResultado.InsertarElemento_Procesamiento_Cantidad_ValorFijo, ListaNumerosDesde, operacion);
                }
                else
                    numeroInsertadoResultado.Numero = itemResultado.Operar_ProcesamientoCantidades(numeroInsertadoResultado.Numero, ListaNumerosDesde, operacion);

                numeroInsertadoResultado.EsCantidadInsertada_ProcesamientoCantidades = true;

                if (indiceInsercion > -1 &&
                    !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
        (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
        indiceInsercion > indiceClasificadores)))
                {
                    ListaNumerosHacia.Insert(indiceInsercion, numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));
                }
                else
                    ListaNumerosHacia.Add(numeroInsertadoResultado.CopiarObjeto(true, itemClasificador, operacion, true));

                if (ListaNumerosHacia[indiceClasificadores].NoIncluirTextosInformacion_CantidadAInsertar)
                {
                    if (indiceInsercion > -1 &&
                    !(itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
        (itemResultado.InsertarUbicacion_Procesamiento_Cantidad != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
        indiceInsercion > indiceClasificadores)))
                        ListaNumerosHacia[indiceInsercion].Textos.Clear();
                    else
                        ListaNumerosHacia.LastOrDefault().Textos.Clear();
                }

                //indiceClasificadores++;
                operacion.PosicionActualNumero_CondicionesOperador_Implicacion++;

                if (Setear_IndiceClasificadores &&
                    indiceClasificadores < ListaNumerosHacia.Count - 1)
                    indiceClasificadores++;

                if(itemResultado.Desplazamiento_PosicionAnterior)
                {
                    if (indiceClasificadores > 0)
                    {
                        //indiceClasificadores--;
                        operacion.PosicionActualNumero_CondicionesOperador_Implicacion--;
                    }
                }

                if(itemResultado.Desplazamiento_PosicionPosterior)
                {
                    if (indiceClasificadores < ListaNumerosHacia.Count - 1)
                    {
                        //indiceClasificadores++;
                        operacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                    }
                }

                if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(ListaNumerosHacia.LastOrDefault()))
                {
                    strMensajeLogResultados += "Se agrega cantidad: " + ListaNumerosHacia.LastOrDefault().Nombre + " : " + ListaNumerosHacia.LastOrDefault().Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ListaNumerosHacia.LastOrDefault().ObtenerTextosInformacion_Cadena() + "\n";
                    strMensajeLog += "Se agrega cantidad: " + ListaNumerosHacia.LastOrDefault().Nombre + " : " + ListaNumerosHacia.LastOrDefault().Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ListaNumerosHacia.LastOrDefault().ObtenerTextosInformacion_Cadena() + "\n";
                }
            }
        }
        private int GenerarCantidadResultados(ElementoOperacionAritmeticaEjecucion operacion)
        {
            return operacion.ElementosOperacion
                    .Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion)
                    .Max(i => i.Clasificadores_Cantidades.Select(i => i.CadenaTexto).Distinct().ToList().Count);
        }

        private void InsertarProcesarTextos_Operacion(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            EntidadNumero numeroResultado,
            List<EntidadNumero> NumerosResultado,
            bool desdeFilas)
        {
            itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
            operacion.InicializarPosicionesElementosExterioresCondiciones(this);
            operacion.InicializarPosicionesElementosOperandos(this, itemOperacion);

            List<EntidadNumero> ListaOriginal = new List<EntidadNumero>();
            List<EntidadNumero> ListaNumeros = new List<EntidadNumero>();

            //if (desdeFilas)
            //{
            //    ListaOriginal = NumerosResultado.ToList();

            //    ListaNumeros = NumerosResultado.Where(
            //    i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList();
            //}
            //else

            ListaOriginal = itemOperacion.Numeros.ToList();

            if (itemOperacion.Tipo == TipoElementoEjecucion.Entrada)
            {
                ListaNumeros = itemOperacion.Numeros.Where(
                i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)).ToList();
            }
            else
            {
                ListaNumeros = itemOperacion.Numeros.Where(
                i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList();
            }

            int indiceLista = 0;

            for (indiceLista = 0; indiceLista <= ListaNumeros.Count - 1; indiceLista++)
            {
                EntidadNumero numero = ListaNumeros[indiceLista];

                do
                {
                    if ((itemOperacion.Tipo == TipoElementoEjecucion.Entrada && ((!numero.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                (numero.ElementosSalidaOperacion_Agrupamiento.Contains(operacion)))) ||

                (itemOperacion.Tipo == TipoElementoEjecucion.OperacionAritmetica && (
                    ((!numero.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                        (numero.ElementosSalidaOperacion_Agrupamiento.Contains(operacion))) &
                                        ((!numero.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                        (numero.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion))) &
                                        ((!numero.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
                                        (numero.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))))))
                    {
                        if (!itemOperacion.NumerosFiltrados.Any() || (itemOperacion.NumerosFiltrados.Any() & itemOperacion.NumerosFiltrados.Contains(numero)))
                        {
                            InsertarProcesarTextos_ItemOperacion(operacion, itemOperacion, numero, numeroResultado, NumerosResultado, desdeFilas, 0);
                        }
                    }

                    itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                    operacion.AumentarPosicionesElementosExterioresCondiciones(this);
                    operacion.AumentarPosicionesElementosOperandos(this, itemOperacion);

                    if (operacion.DetenerProcesamiento)
                        break;

                } while (operacion.MantenerProcesamiento_Operandos |
                                                    operacion.MantenerProcesamiento_Resultados);

                if (operacion.DetenerProcesamiento)
                    break;
            }
        }

        private void InsertarProcesarTextos_ItemOperacion(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            EntidadNumero numero,
            EntidadNumero numeroResultado,
            List<EntidadNumero> NumerosResultado,
            bool desdeFilas,
            int filasAdicionalesResultado)
        {
            if (operacion.ElementoDiseñoRelacionado.ProcesamientoTextosInformacion.Any())
            {
                operacion.ProcesarCantidades_Textos(this, operacion, itemOperacion, numero, numeroResultado, NumerosResultado, itemClasificador, filasAdicionalesResultado, desdeFilas);
            }
        }

        private void AsignarTextosCantidad_Implicaciones_Operacion(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion, EntidadNumero numero, EntidadNumero numeroResultado, List<EntidadNumero> NumerosResultado,
            bool porCadaCantidad, int posicionActual, bool desdeFilas, bool operacionInterna, bool AsignarTextosInformacion, bool AsignarTextosInformacionImplicaciones,
            bool AsignarTextosInformacion_AntesImplicaciones, bool AsignarTextosInformacion_DespuesImplicaciones, Clasificador ClasificadorActual)
        {
            if (AsignarTextosInformacion &&
                AsignarTextosInformacion_AntesImplicaciones)
            {
                if (desdeFilas)
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                    {
                        AsignarTextosInformacion_Elemento(operacion, itemOperacion, numero, numeroResultado, false, AsignarTextosInformacion_AntesImplicaciones, desdeFilas);
                    }
                    else
                    {
                        foreach (var itemOperacionItem in operacion.ElementosOperacion)
                        {
                            itemOperacionItem.TextosInformacionInvolucrados_CondicionTextos.Clear();
                            itemOperacionItem.TieneCondicionesTextos_NoCumplen = false;

                            var listaNumeros = itemOperacionItem.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!itemOperacionItem.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (itemOperacionItem.IndicePosicionClasificadores < itemOperacionItem.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == itemOperacionItem.Clasificadores_Cantidades[itemOperacionItem.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(itemOperacionItem.IndicePosicionClasificadores < itemOperacionItem.Clasificadores_Cantidades.Count))) ||
                                                (itemOperacionItem.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto)))).ToList().Where(i =>
                                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacion)))).ToList();

                            if (listaNumeros.Any() &&
                                posicionActual <= listaNumeros.Count - 1)
                            {
                                AsignarTextosInformacion_Elemento(operacion, itemOperacionItem, listaNumeros[posicionActual], numeroResultado, false, AsignarTextosInformacion_AntesImplicaciones, desdeFilas);
                            }
                        }

                        if (!operacion.ElementosOperacion.Any(i => i.TieneCondicionesTextos_NoCumplen))
                        {
                            numeroResultado.Textos.AddRange(GenerarTextosInformacion(operacion.ElementosOperacion.SelectMany(i => i.TextosInformacionInvolucrados_CondicionTextos).ToList()));
                        }
                    }
                }
                else if (porCadaCantidad)
                {
                    if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                    {
                        AsignarTextosInformacion_Elemento(operacion, itemOperacion, numero, numeroResultado, false, AsignarTextosInformacion_AntesImplicaciones, desdeFilas);
                    }
                    else
                    {
                        foreach (var itemOperacionItem in operacion.ElementosOperacion)
                        {
                            var listaNumeros = itemOperacionItem.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!itemOperacionItem.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (itemOperacionItem.IndicePosicionClasificadores < itemOperacionItem.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == itemOperacionItem.Clasificadores_Cantidades[itemOperacionItem.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(itemOperacionItem.IndicePosicionClasificadores < itemOperacionItem.Clasificadores_Cantidades.Count))) ||
                                                (itemOperacionItem.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto)))).ToList().Where(i =>
                                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacion)))).ToList();

                            foreach (var numeroItem in listaNumeros)
                                AsignarTextosInformacion_Elemento(operacion, itemOperacionItem, numeroItem, numeroResultado, false, AsignarTextosInformacion_AntesImplicaciones, desdeFilas);
                        }
                    }
                }
                else
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos &&
                        operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                    {
                        foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                        {
                            var listaNumeros = itemOperacionNumero.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!itemOperacionNumero.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (itemOperacionNumero.IndicePosicionClasificadores < itemOperacionNumero.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == itemOperacionNumero.Clasificadores_Cantidades[itemOperacionNumero.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(itemOperacionNumero.IndicePosicionClasificadores < itemOperacionNumero.Clasificadores_Cantidades.Count))) ||
                                                (itemOperacionNumero.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto)))).ToList().Where(i =>
                                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacion)))).ToList();

                            foreach (var numeroItem in listaNumeros)
                                AsignarTextosInformacion_Elemento(operacion, itemOperacionNumero, numeroItem, numeroResultado, false, AsignarTextosInformacion_AntesImplicaciones, desdeFilas);
                        }
                    }
                    else
                        AsignarTextosInformacion_Elemento(operacion, itemOperacion, numero, numeroResultado, false, AsignarTextosInformacion_AntesImplicaciones, desdeFilas);
                }

                DefinirNombresDurante_Ejecucion(operacion, itemOperacion, numeroResultado, posicionActual);
            }

            if (AsignarTextosInformacionImplicaciones)
            {
                if (porCadaCantidad)
                {
                    if(desdeFilas)
                    {
                        foreach (var itemOperacionItem in operacion.ElementosOperacion)
                        {
                            {
                                AsignarTextosInformacion_ElementosOperacion(operacion, itemOperacionItem, numero, numeroResultado, NumerosResultado,
                                        operacion.ElementoDiseñoRelacionado.UtilizarDefinicionNombres_AsignacionTextosInformacion, operacionInterna);

                            }
                        }
                    }
                    else
                    {
                        if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos &&
                        operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                        {
                            AsignarTextosInformacion_ElementosOperacion(operacion, itemOperacion, numero, numeroResultado, NumerosResultado,
                                    operacion.ElementoDiseñoRelacionado.UtilizarDefinicionNombres_AsignacionTextosInformacion, operacionInterna);
                        }
                        else
                        {
                            itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
                            operacion.InicializarPosicionesElementosExterioresCondiciones(this, false);
                            operacion.InicializarPosicionesElementosOperandos(this, itemOperacion);

                            foreach (var numeroItem in itemOperacion.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!itemOperacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (itemOperacion.IndicePosicionClasificadores < itemOperacion.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == itemOperacion.Clasificadores_Cantidades[itemOperacion.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(itemOperacion.IndicePosicionClasificadores < itemOperacion.Clasificadores_Cantidades.Count))) ||
                                                (itemOperacion.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto)))).ToList().Where(i =>
                                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacion)))).ToList())
                            {
                                AsignarTextosInformacion_ElementosOperacion(operacion, itemOperacion, numeroItem, numeroResultado, NumerosResultado,
                                        operacion.ElementoDiseñoRelacionado.UtilizarDefinicionNombres_AsignacionTextosInformacion, operacionInterna);

                                itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion++;
                                operacion.AumentarPosicionesElementosExterioresCondiciones(this, false);
                                operacion.AumentarPosicionesElementosOperandos(this, itemOperacion);
                            }
                        }
                    }
                }
                else
                    AsignarTextosInformacion_ElementosOperacion(operacion, itemOperacion, numero, numeroResultado, NumerosResultado,
                                    operacion.ElementoDiseñoRelacionado.UtilizarDefinicionNombres_AsignacionTextosInformacion, operacionInterna);
            }

            if (AsignarTextosInformacion &&
                AsignarTextosInformacion_DespuesImplicaciones)
            {
                if (desdeFilas)
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                    {
                        AsignarTextosInformacion_Elemento(operacion, itemOperacion, numero, numeroResultado, false, AsignarTextosInformacion_DespuesImplicaciones, desdeFilas);
                    }
                    else
                    {
                        foreach (var itemOperacionItem in operacion.ElementosOperacion)
                        {
                            itemOperacionItem.TextosInformacionInvolucrados_CondicionTextos.Clear();
                            itemOperacionItem.TieneCondicionesTextos_NoCumplen = false;

                            var listaNumeros = itemOperacionItem.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!itemOperacionItem.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (itemOperacionItem.IndicePosicionClasificadores < itemOperacionItem.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == itemOperacionItem.Clasificadores_Cantidades[itemOperacionItem.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(itemOperacionItem.IndicePosicionClasificadores < itemOperacionItem.Clasificadores_Cantidades.Count))) ||
                                                (itemOperacionItem.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto)))).ToList().Where(i =>
                                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacion)))).ToList();

                            if (listaNumeros.Any() &&
                                posicionActual <= listaNumeros.Count - 1)
                            {
                                AsignarTextosInformacion_Elemento(operacion, itemOperacionItem, listaNumeros[posicionActual], numeroResultado, false, AsignarTextosInformacion_DespuesImplicaciones, desdeFilas);
                            }
                        }

                        if (!operacion.ElementosOperacion.Any(i => i.TieneCondicionesTextos_NoCumplen))
                        {
                            numeroResultado.Textos.AddRange(GenerarTextosInformacion(operacion.ElementosOperacion.SelectMany(i => i.TextosInformacionInvolucrados_CondicionTextos).ToList()));
                        }
                    }
                }
                else if (porCadaCantidad)
                {
                    if (operacion.ElementoDiseñoRelacionado.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                    {
                        AsignarTextosInformacion_Elemento(operacion, itemOperacion, numero, numeroResultado, false, AsignarTextosInformacion_DespuesImplicaciones, desdeFilas);
                    }
                    else
                    {
                        foreach (var itemOperacionItem in operacion.ElementosOperacion)
                        {
                            var listaNumeros = itemOperacionItem.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!itemOperacionItem.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (itemOperacionItem.IndicePosicionClasificadores < itemOperacionItem.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == itemOperacionItem.Clasificadores_Cantidades[itemOperacionItem.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(itemOperacionItem.IndicePosicionClasificadores < itemOperacionItem.Clasificadores_Cantidades.Count))) ||
                                                (itemOperacionItem.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto)))).ToList().Where(i =>
                                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacion)))).ToList();

                            foreach (var numeroItem in listaNumeros)
                                AsignarTextosInformacion_Elemento(operacion, itemOperacionItem, numeroItem, numeroResultado, false, AsignarTextosInformacion_DespuesImplicaciones, desdeFilas);
                        }
                    }
                }
                else
                {
                    if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarTodosJuntos &&
                        operacion.ElementoDiseñoRelacionado.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                    {
                        foreach (var itemOperacionNumero in operacion.ElementosOperacion)
                        {
                            var listaNumeros = itemOperacionNumero.Numeros.Where(i =>
                                    (!i.Clasificadores_SeleccionarOrdenar.Any(i => !string.IsNullOrEmpty(i.CadenaTexto)) || (!itemOperacionNumero.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => (itemOperacionNumero.IndicePosicionClasificadores < itemOperacionNumero.Clasificadores_Cantidades.Count &&
                                                i.CadenaTexto == itemOperacionNumero.Clasificadores_Cantidades[itemOperacionNumero.IndicePosicionClasificadores].CadenaTexto) ||
                                                !(itemOperacionNumero.IndicePosicionClasificadores < itemOperacionNumero.Clasificadores_Cantidades.Count))) ||
                                                (itemOperacionNumero.Clasificadores_Cantidades.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto) &&
                                                i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == ClasificadorActual.CadenaTexto)))).ToList().Where(i =>
                                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() ||
                                        (i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() ||
                                        (i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Any() &&
                                        i.ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Contains(operacion)))).ToList();

                            foreach (var numeroItem in listaNumeros)
                                AsignarTextosInformacion_Elemento(operacion, itemOperacionNumero, numeroItem, numeroResultado, false, AsignarTextosInformacion_DespuesImplicaciones, desdeFilas); ;
                        }
                    }
                    else
                        AsignarTextosInformacion_Elemento(operacion, itemOperacion, numero, numeroResultado, false, AsignarTextosInformacion_DespuesImplicaciones, desdeFilas);
                }

                DefinirNombresDurante_Ejecucion(operacion, itemOperacion, numeroResultado, posicionActual);
            }
        }

        private void AgregarCantidadesAdicionales(ElementoOperacionAritmeticaEjecucion operacion)
        {
            int cantidad = operacion.Numeros.Count;

            if (operacion.ElementoDiseñoRelacionado.AgregarCantidadComoNumero)
            {
                cantidad += (operacion.ElementoDiseñoRelacionado.IncluirCantidadNumero) ? 1 : 0;

                operacion.Numeros.Add(new EntidadNumero()
                {
                    Nombre = "Cantidad de números del vector",
                    Numero = cantidad,
                });
                operacion.Numeros.LastOrDefault().Textos.Add("cantidad");

                operacion.AgregarClasificadoresGenericos();
            }

            if (operacion.ElementoDiseñoRelacionado.AgregarCantidadComoTextoInformacion)
            {
                operacion.Textos.Add(cantidad.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()));

                foreach (var numero in operacion.Numeros)
                {
                    numero.Textos.Add(cantidad.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()));
                }
            }

            if (operacion.ElementoDiseñoRelacionado.AgregarNombreComoTextoInformacion)
            {
                operacion.Textos.Add(operacion.Nombre);

                foreach (var numero in operacion.Numeros)
                {
                    numero.Textos.Add(numero.Nombre);
                }
            }

            if (operacion.ElementoDiseñoRelacionado.AgregarNumeroComoTextoInformacion)
            {
                foreach (var numero in operacion.Numeros)
                {
                    numero.Textos.Add(numero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()));
                }
            }
        }

        private void Procesar_ListaNumerosFilas_IteracionEjecucion(ElementoOperacionAritmeticaEjecucion operacion,
            ref int CantidadNumeros, ref int indiceNumero, ref string strMensajeLog, ref string strMensajeLogResultados)
        {
            CantidadNumeros = ObtenerCantidadMayorOperandos_Filas(operacion);

            strMensajeLog += "\n";
            strMensajeLogResultados += "\n";

            foreach (var itemOperacion in operacion.ElementosOperacion)
            {
                foreach (var itemClasificador in itemOperacion.Clasificadores_Cantidades)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                    if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto))
                    {
                        strMensajeLog += itemClasificador.CadenaTexto + ", ";
                        strMensajeLogResultados += itemClasificador.CadenaTexto + ", ";
                    }
                }
            }

            indiceNumero = 0;
            InicializarVariables_Operacion(operacion);
            DefinirNombresAntes_Ejecucion(operacion);

            foreach (var itemOperando in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
            {
                string info = string.Empty;

                if (Calculo.MostrarInformacionElementos_InformeResultados &&
                    !string.IsNullOrEmpty(itemOperando.ElementoDiseñoRelacionado.Info))
                {
                    info = " (" + itemOperando.ElementoDiseñoRelacionado.Info + ") ";
                }

                strMensajeLogResultados += itemOperando.Nombre + info + "\n";
                strMensajeLog += itemOperando.Nombre + info + "\n";
            }

            strMensajeLogResultados += "Cantidades que se operan por filas del vector:\n";
            strMensajeLog += "Cantidades que se operan por filas del vector:\n";

            ProcesarNumerosOperando_AntesEjecucion(operacion);
        }

        private void ProcesarFilas_IteracionEjecucion(ElementoOperacionAritmeticaEjecucion operacion, EntidadNumero numeroResultadoFila,
            List<EntidadNumero> NumerosResultado, ref int IndiceNumeroActualElementoOperacion, int indiceClasificadores,
            ref bool FlagDetenerProcesamiento, ref bool FlagMantenerProcesamiento, ref bool conAgrupaciones,
            ref int cantidadNumerosAgrupados, int CantidadNumeros)
        {
            operacion.LimpiarEstadoProcesamiento();

            foreach (var itemOperacion in operacion.ElementosOperacion)
                itemOperacion.CantidadFilasInsertadas_ProcesamientoCantidades = 0;

            operacion.LimpiarCantidadesInsertadas();

            foreach (var itemOperacion in operacion.ElementosOperacion)
            {
                itemOperacion.NumeroResultado_PorFilas = null;
            }

            EntidadNumero noAgrupadoActual = null;
            int IndicePrimerNumero = 0;

            if(NumerosResultado.Any())
            {
                IndicePrimerNumero = NumerosResultado.Count - 1;
            }

            foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
            {
                //itemOperacion.NumeroResultado_PorFilas = numeroResultadoFila;

                if (itemOperacion.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Any(i => i.OperacionRelacionada == operacion.ElementoDiseñoRelacionado))
                {
                    var agrupacion = itemOperacion.ElementoDiseñoRelacionado.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionRelacionada == operacion.ElementoDiseñoRelacionado).FirstOrDefault();

                    if (!operacion.ElementosOperacion.Where(i => i.NumeroResultado_PorFilas != null &&
                    i.NumeroResultado_PorFilas.Agrupacion_PorSeparado == agrupacion.NombreAgrupacion).Any())
                    {
                        itemOperacion.NumeroResultado_PorFilas = new EntidadNumero();
                        itemOperacion.NumeroResultado_PorFilas.Agrupacion_PorSeparado = agrupacion.NombreAgrupacion;
                        itemOperacion.NumeroResultado_PorFilas.AgregarNumero_PorFilas = true;
                        //AgregarClasificador_Cantidad(operacion, itemClasificador, itemOperacion.NumeroResultado_PorFilas);

                        if (itemOperacion != operacion.ElementosOperacion.FirstOrDefault(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                        {
                            NumerosResultado.Add(itemOperacion.NumeroResultado_PorFilas);
                            cantidadNumerosAgrupados++;
                        }
                        else
                            NumerosResultado[IndicePrimerNumero] = itemOperacion.NumeroResultado_PorFilas;

                        conAgrupaciones = true;
                    }
                    else
                    {
                        itemOperacion.NumeroResultado_PorFilas = operacion.ElementosOperacion.Where(i => i.NumeroResultado_PorFilas != null &&
                        i.NumeroResultado_PorFilas.Agrupacion_PorSeparado == agrupacion.NombreAgrupacion)
                            .Select(j => j.NumeroResultado_PorFilas).FirstOrDefault();
                    }
                }
                else
                {
                    if ((noAgrupadoActual != null &&
                        !NumerosResultado.Contains(noAgrupadoActual)) ||
                        noAgrupadoActual == null)
                    {
                        itemOperacion.NumeroResultado_PorFilas = new EntidadNumero();
                        itemOperacion.NumeroResultado_PorFilas.AgregarNumero_PorFilas = true;
                        //AgregarClasificador_Cantidad(operacion, itemClasificador, itemOperacion.NumeroResultado_PorFilas);

                        if (itemOperacion != operacion.ElementosOperacion.FirstOrDefault(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                        {
                            NumerosResultado.Add(itemOperacion.NumeroResultado_PorFilas);
                            noAgrupadoActual = NumerosResultado.LastOrDefault();
                            cantidadNumerosAgrupados++;
                        }
                        else
                        {
                            noAgrupadoActual = itemOperacion.NumeroResultado_PorFilas;
                            NumerosResultado[IndicePrimerNumero] = noAgrupadoActual;
                        }
                    }
                    else
                    {
                        itemOperacion.NumeroResultado_PorFilas = noAgrupadoActual;
                    }
                }
            }

            foreach (var itemOperacion in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
            {
                if (Pausada) Pausar();
                if (Detenida) return;

                if (IndiceNumeroActualElementoOperacion < itemOperacion.Numeros.Count(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemOperacion.Clasificadores_Cantidades[indiceClasificadores])))
                {
                    if (!itemOperacion.NumerosFiltrados.Any() || (itemOperacion.NumerosFiltrados.Any() & itemOperacion.NumerosFiltrados.Contains(
                        itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemOperacion.Clasificadores_Cantidades[indiceClasificadores])).ToList()[IndiceNumeroActualElementoOperacion])))
                    {
                        EntidadNumero numero = itemOperacion.Numeros
                            .Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemOperacion.Clasificadores_Cantidades[indiceClasificadores])).ToList()[IndiceNumeroActualElementoOperacion];

                        if (!operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any() ||
(operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any(j =>
itemOperacion.ElementoDiseñoRelacionado.ObtenerAgrupaciones().Any(
i => i == j.Agrupacion &&
i.NombreAgrupacion == j.Agrupacion.NombreAgrupacion &&
j.OperandoAsociado == itemOperacion.ElementoDiseñoRelacionado &&
j.ElementoAsociado == operacion.ElementoDiseñoRelacionado &&
j.Agrupacion.NombreAgrupacion == numero.Agrupacion_PorSeparado))))
                        {
                            if ((!numero.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                        (numero.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion)))
                            {
                                if ((!numero.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
                                (numero.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion)))
                                {
                                    operacion.LimpiarEstadoProcesamiento();
                                    itemOperacion.ResultadosCondiciones_ProcesamientoCantidades_Filas.Clear();

                                    if (operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion && 
                                        operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                                    {
                                        operacion.ProcesarCantidades(this, operacion, itemOperacion, numero, NumerosResultado[indiceClasificadores],
                                            itemOperacion.Clasificadores_Cantidades[indiceClasificadores], NumerosResultado, true);
                                    }

                                    foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                    {
                                        ResultadoCondicionProcesamientoCantidades_Filas itemResultado_Flags = new ResultadoCondicionProcesamientoCantidades_Filas();

                                        if (itemResultado.InsertarCantidad_Procesamiento_Operandos |
                                            itemResultado.InsertarCantidad_Procesamiento_Resultados) //&&
                                                                                                //!(operacion.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior &&
                                                                                                //    IndiceNumeroActualElementoOperacion == 0 && operacion.NoInsertarCantidad_EnPosicion) &&
                                                                                                //!(operacion.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente &&
                                                                                                //    IndiceNumeroActualElementoOperacion == CantidadNumeros - 1 && operacion.NoInsertarCantidad_EnPosicion))
                                        {
                                            if(itemResultado.InsertarCantidad_Procesamiento_Operandos)
                                                itemResultado_Flags.FlagInsertarCantidad_Procesamiento_Operandos = true;

                                            if (itemResultado.InsertarCantidad_Procesamiento_Resultados)
                                                itemResultado_Flags.FlagInsertarCantidad_Procesamiento_Resultados = true;

                                            if (itemResultado.InsertarElemento_Procesamiento_Operandos)
                                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_Operandos = true;


                                            if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_Resultados = true;

                                            if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                                                itemResultado_Flags.FlagInsertarElemento_Procesamiento_ValorFijo = true;

                                            if (itemResultado.NoInsertarCantidad_EnPosicion)
                                                itemResultado_Flags.FlagNoInsertarCantidad_EnPosicion = true;

                                        }

                                        if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                        {
                                            itemResultado_Flags.FlagQuitarCantidad_Procesamiento_Operandos = true;
                                        }

                                        itemResultado_Flags.IDResultadoCondicionProcesamientoCantidades_Asociado = itemResultado.ID;
                                        itemOperacion.ResultadosCondiciones_ProcesamientoCantidades_Filas.Add(itemResultado_Flags);
                                    }

                                    if (operacion.DetenerProcesamiento)
                                        FlagDetenerProcesamiento = true;

                                    if (operacion.MantenerProcesamiento_Operandos)
                                        FlagMantenerProcesamiento = true;


                                }
                            }
                        }
                    }
                }
                else
                {
                    if (itemOperacion.Numeros.Any((i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemOperacion.Clasificadores_Cantidades[indiceClasificadores]) && (!itemOperacion.NumerosFiltrados.Any() ||
                itemOperacion.NumerosFiltrados.Contains(i)))) ||
                operacion.ElementoDiseñoRelacionado.SeguirOperandoFilas_ConElementoNeutro)
                    {
                        EntidadNumero numero = null;

                        if (operacion.ElementoDiseñoRelacionado.SeguirOperandoFilas_ConUltimoNumero)
                        {
                            if (!itemOperacion.NumerosFiltrados.Any() || (itemOperacion.NumerosFiltrados.Any() & itemOperacion.NumerosFiltrados.Contains(itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemOperacion.Clasificadores_Cantidades[indiceClasificadores])).ToList().Last((i => (!itemOperacion.NumerosFiltrados.Any() ||
                itemOperacion.NumerosFiltrados.Contains(i)))))))
                            {
                                numero = itemOperacion.Numeros
                                    .Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemOperacion.Clasificadores_Cantidades[indiceClasificadores])).ToList().Last((i => (!itemOperacion.NumerosFiltrados.Any() ||
                itemOperacion.NumerosFiltrados.Contains(i))));


                            }
                        }
                        else if (operacion.ElementoDiseñoRelacionado.SeguirOperandoFilas_ConElementoNeutro)
                        {
                            numero = new EntidadNumero();
                        }

                        if (numero != null)
                        {
                            if (!operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any() ||
(operacion.ElementoDiseñoRelacionado.AgrupacionesAsignadasOperandos_PorSeparado.Any(j =>
itemOperacion.ElementoDiseñoRelacionado.ObtenerAgrupaciones().Any(
i => i == j.Agrupacion &&
i.NombreAgrupacion == j.Agrupacion.NombreAgrupacion &&
j.OperandoAsociado == itemOperacion.ElementoDiseñoRelacionado &&
j.ElementoAsociado == operacion.ElementoDiseñoRelacionado &&
j.Agrupacion.NombreAgrupacion == numero.Agrupacion_PorSeparado))))
                            {
                                if ((!numero.ElementosSalidaOperacion_SeleccionarOrdenar.Any()) ||
                                        (numero.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion)))
                                {
                                    if ((!numero.ElementosSalidaOperacion_CondicionFlujo.Any()) ||
                                    (numero.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion)))
                                    {
                                        operacion.LimpiarEstadoProcesamiento();
                                        itemOperacion.ResultadosCondiciones_ProcesamientoCantidades_Filas.Clear();

                                        if (operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_AntesEjecucion && 
                                            operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                                        {
                                            operacion.ProcesarCantidades(this, operacion, itemOperacion, numero, NumerosResultado[indiceClasificadores],
                                             itemOperacion.Clasificadores_Cantidades[indiceClasificadores], NumerosResultado, true);
                                        }

                                        foreach (var itemResultado in operacion.ResultadosCondiciones_ProcesamientoCantidades)
                                        {
                                            ResultadoCondicionProcesamientoCantidades_Filas itemResultado_Flags = new ResultadoCondicionProcesamientoCantidades_Filas();

                                            if ((itemResultado.InsertarCantidad_Procesamiento_Operandos | itemResultado.InsertarCantidad_Procesamiento_Resultados) &&
                                        !((itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecifica ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaAnteriores ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaSiguientes ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploAnteriores ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploSiguientes) &&
                                            IndiceNumeroActualElementoOperacion == 0 && itemResultado.NoInsertarCantidad_EnPosicion) &&
                                        !((itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecifica ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaAnteriores ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaSiguientes ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploAnteriores ||
                                        itemResultado.InsertarUbicacion_Procesamiento_Cantidad == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploSiguientes) &&
                                            IndiceNumeroActualElementoOperacion == CantidadNumeros - 1 && itemResultado.NoInsertarCantidad_EnPosicion))
                                            {
                                                if (itemResultado.InsertarCantidad_Procesamiento_Operandos)
                                                    itemResultado_Flags.FlagInsertarCantidad_Procesamiento_Operandos = true;

                                                if (itemResultado.InsertarCantidad_Procesamiento_Resultados)
                                                    itemResultado_Flags.FlagInsertarCantidad_Procesamiento_Resultados = true;

                                                if (itemResultado.InsertarElemento_Procesamiento_Operandos)
                                                    itemResultado_Flags.FlagInsertarElemento_Procesamiento_Operandos = true;

                                                if (itemResultado.InsertarElemento_Procesamiento_Resultados)
                                                    itemResultado_Flags.FlagInsertarElemento_Procesamiento_Resultados = true;

                                                if (itemResultado.InsertarElemento_Procesamiento_ValorFijo)
                                                    itemResultado_Flags.FlagInsertarElemento_Procesamiento_ValorFijo = true;

                                                if (itemResultado.NoInsertarCantidad_EnPosicion)
                                                    itemResultado_Flags.FlagNoInsertarCantidad_EnPosicion = true;

                                            }

                                            if (itemResultado.QuitarCantidad_Procesamiento_Operandos)
                                            {
                                                itemResultado_Flags.FlagQuitarCantidad_Procesamiento_Operandos = true;
                                            }

                                            itemResultado_Flags.IDResultadoCondicionProcesamientoCantidades_Asociado = itemResultado.ID;
                                            itemOperacion.ResultadosCondiciones_ProcesamientoCantidades_Filas.Add(itemResultado_Flags);
                                        }
                                    }

                                    if (operacion.DetenerProcesamiento)
                                        FlagDetenerProcesamiento = true;

                                    if (operacion.MantenerProcesamiento_Operandos)
                                        FlagMantenerProcesamiento = true;
                                }
                            }
                        }
                    }
                }

                if (FlagDetenerProcesamiento)
                    break;
            }

        }

        private void ProcesarFilaResultado_PorFilas_IteracionOperacion(ElementoOperacionAritmeticaEjecucion operacion,
            EntidadNumero numeroResultado,
            List<EntidadNumero> NumerosResultado,
            EntidadNumero numero,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            ref int IndiceNumeroActualElementoOperacion,
            ref int CantidadNumeros,
            bool FlagDetenerProcesamiento,
            ref bool agregarNumero,
            int cantidadNumeros_Fila,
            bool ElementosNoConjuntos,
            ref double cantidadAcumulada,
            ref int indiceClasificadores,
            ref int indiceNumero,
            ref bool sinOperandos,
            ref string strMensajeLog,
            ref string strMensajeLogResultados,
            ref bool operandoConNumeros,
            bool operacionInterna)
        {
            if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
            {
                operacion.CantidadSubElementosProcesados++;

                List<EntidadNumero> listaResultadosTemp = new List<EntidadNumero>();
                foreach (var itemOperacionItem in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                {
                    EntidadNumero numeroResultadoItemOperacion = itemOperacionItem.NumeroResultado_PorFilas;

                    if (numeroResultadoItemOperacion != null)
                    {
                        if (!listaResultadosTemp.Contains(numeroResultadoItemOperacion) && numeroResultadoItemOperacion.AgregarNumero_PorFilas)
                            listaResultadosTemp.Add(numeroResultadoItemOperacion);
                    }
                }

                //operacion.LimpiarEstadoProcesamiento();

                //var listaResultadosCondiciones = operacion.ResultadosCondiciones_ProcesamientoCantidades.Select(i => i.ID).ToList();

                //if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                //{
                //    foreach (var itemOperacionNumero in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                //    {
                //        foreach (var numeroResultadoItem in listaResultadosTemp)
                //        {
                //            operacion.ProcesarCantidades(this, operacion, itemOperacionNumero, numeroResultadoItem, numeroResultadoItem, itemClasificador, NumerosResultado, true);
                //        }
                //    }
                //}

                foreach (var itemOperacionItem in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                {
                    if (Pausada) Pausar();
                    try {  } catch (Exception) { };
                    if (Detenida) return;

                    foreach (var numeroResultadoItemOperacion in listaResultadosTemp)
                    {
                        operacion.NivelarCantidadElementos_OperandosPorFila_ProcesamientoCantidades(this, operacion, itemOperacionItem, ref IndiceNumeroActualElementoOperacion, numeroResultadoItemOperacion,
                            ref CantidadNumeros, NumerosResultado, itemClasificador);
                    }

                    if (FlagDetenerProcesamiento)
                        break;
                }

                if (agregarNumero && cantidadNumeros_Fila <= 1 && !ElementosNoConjuntos) agregarNumero = false;

                if (operacion.ElementoDiseñoRelacionado.ConAcumulacion)
                {
                    listaResultadosTemp = new List<EntidadNumero>();
                    foreach (var itemOperacionItem in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                    {
                        EntidadNumero numeroResultadoItem = itemOperacionItem.NumeroResultado_PorFilas;
                        if (!listaResultadosTemp.Contains(numeroResultadoItem) && numeroResultadoItem.AgregarNumero_PorFilas)
                            listaResultadosTemp.Add(numeroResultadoItem);

                    }

                    cantidadAcumulada = 0;
                    foreach (var numeroResultadoItem in listaResultadosTemp)
                        cantidadAcumulada += numeroResultadoItem.Numero;
                }

                if (!agregarNumero)
                {
                    foreach (var itemOperacionItem in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                    {
                        EntidadNumero numeroResultadoItem = itemOperacionItem.NumeroResultado_PorFilas;
                        if (NumerosResultado.Contains(numeroResultadoItem) && !numeroResultadoItem.AgregarNumero_PorFilas)
                        {
                            NumerosResultado.Remove(numeroResultadoItem);
                            //indiceClasificadores--;
                        }
                    }

                    indiceNumero--;
                    sinOperandos = true;
                }
                else
                {
                    NumerosResultado.Add(new EntidadNumero());                                        
                }

                int cantidadFilas_Adicionales = 0;
                int filasAdicionalesResultado = listaResultadosTemp.Count - 1;

                foreach (var numeroResultadoItem in listaResultadosTemp)
                {
                    if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones)
                    {
                        //if (numeroResultadoItem != numeroResultado)
                        {
                            operacion.LimpiarEstadoProcesamiento();
                            operacion.LimpiarCantidadesInsertadas();

                            if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                            {
                                foreach (var itemOperacionNumero in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                                {
                                    operacion.ProcesarCantidades(this, operacion, itemOperacionNumero, numeroResultadoItem, numeroResultadoItem, itemClasificador, NumerosResultado, true);
                                }
                            }
                        }

                        bool conNumeros_NoUso = false; int indiceClasificadores_NoUso = 0;
                        int cantidadNumeros_PorSeparado_NoUso = 0;

                        int Indice = IndiceNumeroActualElementoOperacion;

                        foreach (var itemOperacionNumero in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                        {
                            InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, numeroResultadoItem, itemOperacionNumero, numeroResultadoItem, NumerosResultado,
                            operacion.ElementosOperacion.SelectMany(i => new List<EntidadNumero>() { i.Numeros.ElementAtOrDefault(Indice) }).ToList(),
                            ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso, ref indiceClasificadores_NoUso, false, ref cantidadNumeros_PorSeparado_NoUso,
                            ref IndiceNumeroActualElementoOperacion,
                            ref cantidadFilas_Adicionales, operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                        }
                    }

                    ProcesarTextos_IteracionEjecucion(operacion, null, numeroResultadoItem, numeroResultadoItem, NumerosResultado, true,
                        IndiceNumeroActualElementoOperacion, true, operacionInterna, operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesEjecucion,
                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_DespuesEjecucion,
                                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones,
                                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesImplicaciones,
                                                                            operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_DespuesEjecucion, 
                                                                            filasAdicionalesResultado, itemClasificador);

                    if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones)
                    {
                        operacion.LimpiarEstadoProcesamiento();
                        operacion.LimpiarCantidadesInsertadas();

                        if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                        {
                            foreach (var itemOperacionNumero in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                            {
                                operacion.ProcesarCantidades(this, operacion, itemOperacionNumero, numeroResultadoItem, numeroResultadoItem, itemClasificador, NumerosResultado, true);
                            }
                        }

                        bool conNumeros_NoUso = false; int indiceClasificadores_NoUso = 0;
                        int cantidadNumeros_PorSeparado_NoUso = 0;

                        int Indice = IndiceNumeroActualElementoOperacion;

                        foreach (var itemOperacionNumero in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                        {
                            InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, numeroResultadoItem, itemOperacionNumero, numeroResultadoItem, NumerosResultado,
                            operacion.ElementosOperacion.SelectMany(i => new List<EntidadNumero>() { i.Numeros.ElementAtOrDefault(Indice) }).ToList(),
                            ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso, ref indiceClasificadores_NoUso, false, ref cantidadNumeros_PorSeparado_NoUso,
                            ref IndiceNumeroActualElementoOperacion,
                            ref cantidadFilas_Adicionales, operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                        }
                    }

                    filasAdicionalesResultado--;
                }

                if (operacion.ElementoDiseñoRelacionado.ConAcumulacion)
                {
                    operacion.ReiniciarAcumulacion_OperandosPorFila_ProcesamientoCantidades(ref cantidadAcumulada);
                }

                IndiceNumeroActualElementoOperacion++;

                CantidadNumeros = ObtenerCantidadMayorOperandos_Filas(operacion);
                CantidadNumeros += cantidadFilas_Adicionales;

                if (IndiceNumeroActualElementoOperacion < 0)
                    IndiceNumeroActualElementoOperacion = 0;
                else if (IndiceNumeroActualElementoOperacion > CantidadNumeros - 1)
                    IndiceNumeroActualElementoOperacion = CantidadNumeros;

                    foreach (var itemOperacionPosicion in operacion.ElementosOperacion)
                        itemOperacionPosicion.PosicionActualNumero_CondicionesOperador_Implicacion++;

                operacion.AumentarPosicionesElementosExterioresCondiciones(this);
            }
            else if (operacion.ElementoDiseñoRelacionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
            {
                operacion.CantidadSubElementosProcesados++;

                if (operandoConNumeros) sinOperandos = false;

                if (agregarNumero && cantidadNumeros_Fila <= 1 && !ElementosNoConjuntos) agregarNumero = false;

                if (operacion.ElementoDiseñoRelacionado.ConAcumulacion)
                {
                    cantidadAcumulada = numeroResultado.Numero;
                }

                if (!agregarNumero)
                {
                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(numeroResultado))
                    {
                        strMensajeLogResultados += numeroResultado.Nombre + " : " + numeroResultado.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numeroResultado.ObtenerTextosInformacion_Cadena() + "\n";
                        strMensajeLog += numeroResultado.Nombre + " : " + numeroResultado.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + numeroResultado.ObtenerTextosInformacion_Cadena() + "\n";
                    }

                    if (!NumerosResultado.Contains(numeroResultado) && !numeroResultado.AgregarNumero_PorFilas)
                    {
                        NumerosResultado.Remove(numeroResultado);
                    }

                    indiceNumero--;
                    sinOperandos = true;
                }
                else
                {
                    if(operacion.TipoOperacion != TipoOperacionAritmeticaEjecucion.ContarCantidades)
                        NumerosResultado.Add(new EntidadNumero());
                }

                int cantidadFilas_Adicionales_NoUso = 0;

                if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones)
                {
                    bool conNumeros_NoUso = false;
                    int cantidadNumeros_PorSeparado_NoUso = 0;

                    int Indice = IndiceNumeroActualElementoOperacion;

                    InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, numero, itemOperacion, numeroResultado, NumerosResultado,
                        operacion.ElementosOperacion.SelectMany(i => new List<EntidadNumero>() { i.Numeros.ElementAtOrDefault(Indice) }).ToList(),
                        ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso, ref indiceClasificadores, false, ref cantidadNumeros_PorSeparado_NoUso, 
                        ref IndiceNumeroActualElementoOperacion,
                        ref cantidadFilas_Adicionales_NoUso, operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                }

                ProcesarTextos_IteracionEjecucion(operacion, itemOperacion, numero, numeroResultado,
                                                                            NumerosResultado, true, IndiceNumeroActualElementoOperacion, true, operacionInterna,
                                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesEjecucion,
                                                                            operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_DespuesEjecucion,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesImplicaciones,
                                                                        operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_DespuesEjecucion, 0, itemClasificador);

                if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones)
                {
                    operacion.LimpiarEstadoProcesamiento();
                    operacion.LimpiarCantidadesInsertadas();

                    if (operacion.ElementoDiseñoRelacionado.ProcesamientoCantidades.Any())
                    {
                        foreach (var itemOperacionNumero in operacion.ElementosOperacion.Where(i => i.ElementoDiseñoRelacionado != null && !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
                        {
                            operacion.ProcesarCantidades(this, operacion, itemOperacionNumero, NumerosResultado[IndiceNumeroActualElementoOperacion],
                                NumerosResultado[IndiceNumeroActualElementoOperacion], itemClasificador, NumerosResultado, true);
                        }
                    }

                    bool conNumeros_NoUso = false;
                    int cantidadNumeros_PorSeparado_NoUso = 0;

                    int Indice = IndiceNumeroActualElementoOperacion;

                    InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, numero, itemOperacion, numeroResultado, NumerosResultado,
                        operacion.ElementosOperacion.SelectMany(i => new List<EntidadNumero>() { i.Numeros.ElementAtOrDefault(Indice) }).ToList(),
                        ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso, ref indiceClasificadores, false, ref cantidadNumeros_PorSeparado_NoUso, 
                        ref IndiceNumeroActualElementoOperacion,
                        ref cantidadFilas_Adicionales_NoUso, operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                }
            }
        }

        private void ProcesarResultados_OperacionPorPares(List<EntidadNumero> primerasCantidades_ParesOperandos,
            List<EntidadNumero> segundasCantidades_ParesOperandos,
            ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            List<EntidadNumero> NumerosResultado,
            ref bool utilizandoPrimeraCantidad,
            ref int indiceClasificadores,
            ref string strMensajeLog,
            ref string strMensajeLogResultados,
            ref int indiceNumero,
            ref string strOperandosResultados,
            ref int indiceResultadosMensaje,
            ref string strOperandoPares,
            ref bool operandoConNumeros,
            ref bool sinOperandos,
            ref int cantidadNumerosAgregados_Pares)
        {
            List<EntidadNumero> NumerosObtenidos_Potencia = new List<EntidadNumero>();
            bool hayNumerosObtenidos = false;

            int indiceOperando = 0;
            foreach (EntidadNumero primeraCantidad in primerasCantidades_ParesOperandos)
            {
                foreach (EntidadNumero segundaCantidad in segundasCantidades_ParesOperandos)
                {
                    EntidadNumero resultadoParCantidades = new EntidadNumero();

                    AgregarClasificador_Cantidad(operacion, itemClasificador, resultadoParCantidades);

                    switch (operacion.ElementoDiseñoRelacionado.Tipo)
                    {
                        case TipoElementoOperacion.Potencia:
                            resultadoParCantidades.Numero = Math.Pow(primeraCantidad.Numero, segundaCantidad.Numero);

                            break;

                        case TipoElementoOperacion.Raiz:
                            resultadoParCantidades.Numero = Math.Pow(primeraCantidad.Numero, 1 / segundaCantidad.Numero);
                            break;

                        case TipoElementoOperacion.Porcentaje:
                            if (operacion.ElementoDiseñoRelacionado.PorcentajeRelativo)
                                resultadoParCantidades.Numero = (primeraCantidad.Numero / segundaCantidad.Numero) * (double)100.0;
                            else
                                resultadoParCantidades.Numero = (segundaCantidad.Numero / (double)100.0) * primeraCantidad.Numero;
                            break;

                        case TipoElementoOperacion.Logaritmo:
                            resultadoParCantidades.Numero = Math.Log(segundaCantidad.Numero, primeraCantidad.Numero);
                            break;
                    }

                    if (!hayNumerosObtenidos)
                    {
                        NumerosResultado[indiceClasificadores + cantidadNumerosAgregados_Pares] = resultadoParCantidades;
                        hayNumerosObtenidos = true;
                    }
                    else
                    {
                        NumerosResultado.Add(resultadoParCantidades);
                        //indiceClasificadores++;
                        cantidadNumerosAgregados_Pares++;
                    }

                    int indiceActual = indiceClasificadores + cantidadNumerosAgregados_Pares;
                    int cantidadFilas_Adicionales_NoUso = 0;

                    if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_AntesImplicaciones)
                    {
                        bool conNumeros_NoUso = false;
                        int cantidadNumeros_PorSeparado_NoUso = 0;

                        InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, resultadoParCantidades, itemOperacion,
                            resultadoParCantidades, NumerosResultado, itemOperacion.Numeros, ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso,
                            ref indiceActual, false, ref cantidadNumeros_PorSeparado_NoUso, ref indiceActual, ref cantidadFilas_Adicionales_NoUso, 
                            operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                    }

                    ProcesarTextos_IteracionEjecucion(operacion, itemOperacion, resultadoParCantidades, resultadoParCantidades, NumerosResultado,
                        true, indiceOperando, false, false, operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesEjecucion |
                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesEjecucion,
                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_AntesEjecucion |
                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacionImplicaciones_DespuesEjecucion,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_AntesImplicaciones,
                                                                        operacion.ElementoDiseñoRelacionado.AsignarTextosInformacion_DespuesImplicaciones,
                                                                        operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_AntesEjecucion |
                                                                        operacion.ElementoDiseñoRelacionado.EjecutarLogicasTextos_DespuesEjecucion, 0, itemClasificador);

                    if (operacion.ElementoDiseñoRelacionado.ProcesarCantidades_DespuesImplicaciones)
                    {
                        bool conNumeros_NoUso = false;
                        int cantidadNumeros_PorSeparado_NoUso = 0;

                        InsertarQuitarProcesarCantidades_Resultados_Operacion(operacion, resultadoParCantidades, itemOperacion,
                            resultadoParCantidades, NumerosResultado, itemOperacion.Numeros, ref strMensajeLog, ref strMensajeLogResultados, ref conNumeros_NoUso,
                            ref indiceActual, false, ref cantidadNumeros_PorSeparado_NoUso, ref indiceActual, ref cantidadFilas_Adicionales_NoUso, 
                            operacion.ElementoDiseñoRelacionado.EjecutarLogicasCantidades_DespuesEjecucion);
                    }

                    operacion.AumentarPosicionesElementosExterioresCondiciones(this);
                    operacion.AumentarPosicionesElementosOperandos(this, itemOperacion);

                    operacion.CantidadNumeros_Ejecucion++;
                    indiceNumero++;
                }
                indiceOperando++;
            }

            string strNombreOperacion = string.Empty;

            switch (operacion.ElementoDiseñoRelacionado.Tipo)
            {
                case TipoElementoOperacion.Potencia:
                    strNombreOperacion = "Potencia";
                    break;

                case TipoElementoOperacion.Raiz:
                    strNombreOperacion = "Raíz";
                    break;

                case TipoElementoOperacion.Porcentaje:
                    strNombreOperacion = "Porcentaje";
                    break;

                case TipoElementoOperacion.Logaritmo:
                    strNombreOperacion = "Logaritmo";
                    break;
            }

            strMensajeLog += strNombreOperacion + " con " + strOperandosResultados + " de resultados:\n";
            strMensajeLogResultados += strNombreOperacion + " con " + strOperandosResultados + " de resultados:\n";

            if (NumerosResultado.Any() &&
                indiceResultadosMensaje < NumerosResultado.Count - 1)
            {
                for (int indice = indiceResultadosMensaje; ; indice++)
                {
                    EntidadNumero subItem_ElementoOperacion = NumerosResultado[indice];

                    if (Calculo.VerificarMostrarElemento_InformeEjecucionResultados(subItem_ElementoOperacion))
                    {
                        strMensajeLog += subItem_ElementoOperacion.Nombre + ": " + subItem_ElementoOperacion.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + subItem_ElementoOperacion.ObtenerTextosInformacion_Cadena() + "\n";
                        strMensajeLogResultados += subItem_ElementoOperacion.Nombre + ": " + subItem_ElementoOperacion.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + subItem_ElementoOperacion.ObtenerTextosInformacion_Cadena() + "\n";
                    }

                    if (indice == NumerosResultado.Count - 1)
                    {
                        indiceResultadosMensaje = indice + 1;
                        break;
                    }
                }
            }

            if (itemOperacion != operacion.ElementosOperacion.LastOrDefault(i => !i.ElementoDiseñoRelacionado.NoConsiderarEjecucion))
            {
                NumerosResultado.Add(new EntidadNumero());
                AgregarClasificador_Cantidad(operacion, itemClasificador, NumerosResultado.LastOrDefault());
                cantidadNumerosAgregados_Pares++;
            }

            if (hayNumerosObtenidos) strMensajeLog += "\n";
            if (hayNumerosObtenidos) strMensajeLogResultados += "\n";

            strOperandoPares = string.Empty;
            strOperandosResultados = string.Empty;
            utilizandoPrimeraCantidad = true;

            primerasCantidades_ParesOperandos.Clear();
            segundasCantidades_ParesOperandos.Clear();

            if (!hayNumerosObtenidos) operandoConNumeros = false;
            if (operandoConNumeros) sinOperandos = false;
        }

        private void ProcesarFilaResultado_PorFilas_IteracionOperacion_PorPares(ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion itemOperacion,
            List<EntidadNumero> primerasCantidades_ParesOperandos,
            List<EntidadNumero> segundasCantidades_ParesOperandos,
            List<EntidadNumero> NumerosResultado,
            int IndiceNumeroActualElementoOperacion,
            ref int CantidadNumeros,
            bool FlagDetenerProcesamiento,
            ref bool agregarNumero,
            int cantidadNumeros_Fila,
            bool ElementosNoConjuntos,
            ref double cantidadAcumulada,
            ref int indiceClasificadores,
            ref int indiceNumero,
            ref bool sinOperandos,
            ref string strMensajeLog,
            ref string strMensajeLogResultados,
            ref bool operandoConNumeros,
            bool operacionInterna)
        {
            operacion.CantidadSubElementosProcesados++;

            int CantidadMayor = 0;
            if (primerasCantidades_ParesOperandos.Count > CantidadMayor)
                CantidadMayor = primerasCantidades_ParesOperandos.Count;
            if (segundasCantidades_ParesOperandos.Count > CantidadMayor)
                CantidadMayor = segundasCantidades_ParesOperandos.Count;

            bool hayNumerosObtenidos = !(IndiceNumeroActualElementoOperacion == 0 &&
                1 == operacion.ElementosOperacion.IndexOf(itemOperacion));

            for (int indicePosicionResultado = 0; indicePosicionResultado < CantidadMayor; indicePosicionResultado++)
            {
                if (indicePosicionResultado <= primerasCantidades_ParesOperandos.Count - 1 &&
                    indicePosicionResultado <= segundasCantidades_ParesOperandos.Count - 1)
                {
                    EntidadNumero primeraCantidad = primerasCantidades_ParesOperandos[indicePosicionResultado];
                    EntidadNumero segundaCantidad = segundasCantidades_ParesOperandos[indicePosicionResultado];

                    EntidadNumero resultadoParCantidades = new EntidadNumero();

                    switch (operacion.ElementoDiseñoRelacionado.Tipo)
                    {
                        case TipoElementoOperacion.Potencia:
                            resultadoParCantidades.Numero = Math.Pow(primeraCantidad.Numero, segundaCantidad.Numero);
                            break;

                        case TipoElementoOperacion.Raiz:
                            resultadoParCantidades.Numero = Math.Pow(primeraCantidad.Numero, 1 / segundaCantidad.Numero);
                            break;

                        case TipoElementoOperacion.Porcentaje:
                            if (operacion.ElementoDiseñoRelacionado.PorcentajeRelativo)
                                resultadoParCantidades.Numero = (primeraCantidad.Numero / segundaCantidad.Numero) * (double)100.0;
                            else
                                resultadoParCantidades.Numero = (segundaCantidad.Numero / (double)100.0) * primeraCantidad.Numero;
                            break;

                        case TipoElementoOperacion.Logaritmo:
                            resultadoParCantidades.Numero = Math.Log(segundaCantidad.Numero, primeraCantidad.Numero);
                            break;
                    }


                    if (!hayNumerosObtenidos)
                    {
                        NumerosResultado[indiceClasificadores] = resultadoParCantidades;
                        hayNumerosObtenidos = true;
                    }
                    else
                    {
                        NumerosResultado.Add(resultadoParCantidades);
                        //indiceClasificadores++;
                    }

                    ProcesarFilaResultado_PorFilas_IteracionOperacion(operacion, resultadoParCantidades, NumerosResultado, resultadoParCantidades,
                        itemOperacion, ref IndiceNumeroActualElementoOperacion, ref CantidadNumeros, FlagDetenerProcesamiento, ref agregarNumero,
                        cantidadNumeros_Fila, ElementosNoConjuntos, ref cantidadAcumulada, ref indiceClasificadores, ref indiceNumero,
                        ref sinOperandos, ref strMensajeLog, ref strMensajeLogResultados, ref operandoConNumeros, operacionInterna);

                    if (sinOperandos)
                        sinOperandos = false;

                    if (operacion.DetenerProcesamiento)
                        break;
                }
            }

            primerasCantidades_ParesOperandos.Clear();
            segundasCantidades_ParesOperandos.Clear();
        }

        private void OrdenarCantidades_Operacion_Antes(ElementoOperacionAritmeticaEjecucion operacion,
            OrdenarNumerosElemento OrdenarNumerosEjecucion, List<OrdenarNumerosElemento> OrdenamientosNumerosEjecucion)
        {
            if (OrdenarNumerosEjecucion != null)
            {
                foreach (var itemOperacion in operacion.ElementosOperacion)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    foreach (var itemClasificador in itemOperacion.Clasificadores_Cantidades)
                    {
                        if (Pausada) Pausar();
                        if (Detenida) return;

                        if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                        List<ElementoOperacionAritmeticaEjecucion> ElementosOperacion = new List<ElementoOperacionAritmeticaEjecucion>();
                        List<EntidadNumero> Numeros = new List<EntidadNumero>();

                        if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor)
                        {
                            Numeros = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList();

                            if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosPorCantidad)
                                Numeros = Numeros.OrderBy(i => i.Numero).ToList();
                            else if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosPorNombre)
                            {
                                //operacion.ElementosOperacion = operacion.ElementosOperacion.OrderBy(i => i.Nombre).ToList();
                                if (OrdenarNumerosEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                                    itemOperacion.OrdenarTextosInformacion_ElementosLista(Numeros, OrdenarNumerosEjecucion.Ordenacion);

                                Numeros = OrdenarNumeros_Elemento(Numeros,
                                    OrdenarNumerosEjecucion.Ordenacion.Tipo_OrdenamientoNumeros,
                                OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor,
                                OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor,
                                OrdenarNumerosEjecucion.Ordenaciones,
                                OrdenarNumerosEjecucion.RevertirListaTextos);
                            }
                        }
                        else if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor)
                        {                            
                            Numeros = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList();

                            if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosPorCantidad)
                                Numeros = Numeros.OrderByDescending(i => i.Numero).ToList();
                            else if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosPorNombre)
                            {
                                //operacion.ElementosOperacion = operacion.ElementosOperacion.OrderByDescending(i => i.Nombre).ToList();
                                if (OrdenarNumerosEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                                    itemOperacion.OrdenarTextosInformacion_ElementosLista(Numeros, OrdenarNumerosEjecucion.Ordenacion);

                                Numeros = OrdenarNumeros_Elemento(Numeros,
                                    OrdenarNumerosEjecucion.Ordenacion.Tipo_OrdenamientoNumeros,
                                OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor,
                                OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor,
                                OrdenarNumerosEjecucion.Ordenaciones,
                                OrdenarNumerosEjecucion.RevertirListaTextos);
                            }
                        }

                        if (OrdenarNumerosEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
                                operacion.OrdenarTextosInformacion_ElementosLista_SinOrdenarCantidades(Numeros, OrdenarNumerosEjecucion.Ordenacion);

                        if (Numeros.Any())
                        {
                            foreach (var itemCantidad in Numeros)
                            {
                                if (Pausada) Pausar();
                                if (Detenida) return;

                                itemOperacion.Numeros.Remove(itemCantidad);
                            }

                            if (itemOperacion.Numeros.Any())
                                itemOperacion.Numeros.InsertRange(0, Numeros);
                            else
                                itemOperacion.Numeros.AddRange(Numeros);
                        }
                    }
                }
            }

            if (operacion.ElementosOperacion != null)
            {
                foreach (var itemOrdenar in operacion.ElementosOperacion)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    OrdenarNumerosElemento ordenar = (from E in OrdenamientosNumerosEjecucion
                                                      where
                                                     E.Operando == itemOrdenar.ElementoDiseñoRelacionado
                                                      select E).FirstOrDefault();

                    if (ordenar != null)
                    {
                        foreach (var itemClasificador in itemOrdenar.Clasificadores_Cantidades)
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemOrdenar.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                            List<EntidadNumero> Numeros = new List<EntidadNumero>();

                            if (itemOrdenar.GetType() == typeof(ElementoEntradaEjecucion))
                            {
                                Numeros = itemOrdenar.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList();

                                if (ordenar.Ordenacion.OrdenarNumerosPorNombre &&
                                    ordenar.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                                    itemOrdenar.OrdenarTextosInformacion_ElementosLista(Numeros, ordenar.Ordenacion);

                                itemOrdenar.OrdenarElementos(Numeros, ordenar.Ordenacion.OrdenarNumerosDeMenorAMayor,
                                    ordenar.Ordenacion.OrdenarNumerosDeMayorAMenor, ordenar.Ordenacion.OrdenarNumerosPorCantidad, ordenar.Ordenacion.OrdenarNumerosPorNombre,
                                    ordenar.Ordenacion.Tipo_OrdenamientoNumeros, this, ordenar.Ordenaciones, ordenar.RevertirListaTextos);

                                if (ordenar.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
                                    itemOrdenar.OrdenarTextosInformacion_ElementosLista_SinOrdenarCantidades(Numeros, ordenar.Ordenacion);

                            }

                            if (Numeros.Any())
                            {
                                foreach (var itemCantidad in Numeros)
                                {
                                    if (Pausada) Pausar();
                                    if (Detenida) return;

                                    itemOrdenar.Numeros.Remove(itemCantidad);
                                }

                                if (itemOrdenar.Numeros.Any())
                                    itemOrdenar.Numeros.InsertRange(0, Numeros);
                                else
                                    itemOrdenar.Numeros.AddRange(Numeros);
                            }
                        }
                    }
                }
            }
        }
        private void OrdenarCantidades_Operacion_Despues(ElementoOperacionAritmeticaEjecucion itemOperacion,
            OrdenarNumerosElemento OrdenarNumerosEjecucion, List<OrdenarNumerosElemento> OrdenamientosNumerosEjecucion)
        {
            if (OrdenarNumerosEjecucion != null)
            {
                foreach (var itemClasificador in itemOperacion.Clasificadores_Cantidades)
                    {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemOperacion.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                    List<ElementoOperacionAritmeticaEjecucion> ElementosOperacion = new List<ElementoOperacionAritmeticaEjecucion>();
                        List<EntidadNumero> Numeros = new List<EntidadNumero>();

                        if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor)
                        {
                            Numeros = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList();

                            if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosPorCantidad)
                                Numeros = Numeros.OrderBy(i => i.Numero).ToList();
                            else if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosPorNombre)
                            {
                                //operacion.ElementosOperacion = operacion.ElementosOperacion.OrderBy(i => i.Nombre).ToList();
                                if (OrdenarNumerosEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                                    itemOperacion.OrdenarTextosInformacion_ElementosLista(Numeros, OrdenarNumerosEjecucion.Ordenacion);

                                Numeros = OrdenarNumeros_Elemento(Numeros,
                                    OrdenarNumerosEjecucion.Ordenacion.Tipo_OrdenamientoNumeros,
                                OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor,
                                OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor,
                                OrdenarNumerosEjecucion.Ordenaciones,
                                OrdenarNumerosEjecucion.RevertirListaTextos);
                            }
                        }
                        else if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor)
                        {
                            Numeros = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList();

                            if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosPorCantidad)
                                Numeros = Numeros.OrderByDescending(i => i.Numero).ToList();
                            else if (OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosPorNombre)
                            {
                                //operacion.ElementosOperacion = operacion.ElementosOperacion.OrderByDescending(i => i.Nombre).ToList();
                                if (OrdenarNumerosEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                                    itemOperacion.OrdenarTextosInformacion_ElementosLista(Numeros, OrdenarNumerosEjecucion.Ordenacion);

                                Numeros = OrdenarNumeros_Elemento(Numeros,
                                    OrdenarNumerosEjecucion.Ordenacion.Tipo_OrdenamientoNumeros,
                                OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor,
                                OrdenarNumerosEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor,
                                OrdenarNumerosEjecucion.Ordenaciones,
                                OrdenarNumerosEjecucion.RevertirListaTextos);
                            }
                        }

                        if (OrdenarNumerosEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
                            itemOperacion.OrdenarTextosInformacion_ElementosLista_SinOrdenarCantidades(Numeros, OrdenarNumerosEjecucion.Ordenacion);

                        if (Numeros.Any())
                        {
                            foreach (var itemCantidad in Numeros)
                            {
                                itemOperacion.Numeros.Remove(itemCantidad);
                            }

                            if (itemOperacion.Numeros.Any())
                                itemOperacion.Numeros.InsertRange(0, Numeros);
                            else
                                itemOperacion.Numeros.AddRange(Numeros);
                        }
                    }                
            }

            if (itemOperacion.ElementosOperacion != null)
            {
                foreach (var itemOrdenar in itemOperacion.ElementosOperacion)
                {
                    if (Pausada) Pausar();
                    if (Detenida) return;

                    OrdenarNumerosElemento ordenar = (from E in OrdenamientosNumerosEjecucion
                                                      where
                                                     E.Operando == itemOrdenar.ElementoDiseñoRelacionado
                                                      select E).FirstOrDefault();

                    if (ordenar != null)
                    {
                        foreach (var itemClasificador in itemOrdenar.Clasificadores_Cantidades)
                        {
                            if (Pausada) Pausar();
                            if (Detenida) return;

                            if (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                            itemOrdenar.Clasificadores_Cantidades.Any(i => !string.IsNullOrEmpty(i.CadenaTexto))) continue;

                            List<EntidadNumero> Numeros = new List<EntidadNumero>();

                            if (itemOrdenar.GetType() == typeof(ElementoEntradaEjecucion))
                            {
                                Numeros = itemOrdenar.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i == itemClasificador)).ToList();

                                if (ordenar.Ordenacion.OrdenarNumerosPorNombre &&
                                    ordenar.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                                    itemOrdenar.OrdenarTextosInformacion_ElementosLista(Numeros, ordenar.Ordenacion);

                                itemOrdenar.OrdenarElementos(Numeros, ordenar.Ordenacion.OrdenarNumerosDeMenorAMayor,
                                    ordenar.Ordenacion.OrdenarNumerosDeMayorAMenor, ordenar.Ordenacion.OrdenarNumerosPorCantidad, ordenar.Ordenacion.OrdenarNumerosPorNombre,
                                    ordenar.Ordenacion.Tipo_OrdenamientoNumeros, this, ordenar.Ordenaciones, ordenar.RevertirListaTextos);

                                if (ordenar.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
                                    itemOrdenar.OrdenarTextosInformacion_ElementosLista_SinOrdenarCantidades(Numeros, ordenar.Ordenacion);

                            }

                            if (Numeros.Any())
                            {
                                foreach (var itemCantidad in Numeros)
                                {
                                    itemOrdenar.Numeros.Remove(itemCantidad);
                                }

                                if (itemOrdenar.Numeros.Any())
                                    itemOrdenar.Numeros.InsertRange(0, Numeros);
                                else
                                    itemOrdenar.Numeros.AddRange(Numeros);
                            }
                        }
                    }
                }
            }
        }
    }

    public class TemporalResolver : DataContractResolver
    {
        private const string Ns = "http://schemas.datacontract.org/2004/07/ProcessCalc.Entidades";

        // XML -> .NET
        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            if (typeName == "DiseñoElementoOperacion" && typeNamespace == Ns)
            {
                // cuando veas este nombre en el XML, créame un DiseñoOperacion
                return typeof(ProcessCalc.Entidades.DiseñoOperacion);
            }

            // todo lo demás, que lo resuelva el default
            return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, knownTypeResolver);
        }

        // .NET -> XML (no lo usaremos porque no vamos a serializar con este resolver)
        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver,
            out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            // dejamos que el serializer normal lo haga
            return knownTypeResolver.TryResolveType(type, declaredType, knownTypeResolver, out typeName, out typeNamespace);
        }
    }

}
