using Microsoft.VisualBasic.Logging;
using ProcessCalc.Controles;
using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.OrigenesDatos;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class ElementoEntradaEjecucion : ElementoOperacionAritmeticaEjecucion
    {
        public TipoEntrada TipoEntrada { get; set; }
        public List<ElementoOrigenDatosEjecucion> OrigenesDatos { get; set; }
        public List<string> TextosInformacionFijos { get; set; }
        public TipoOperacionAritmeticaEjecucion OperacionInterna { get; set; }
        public List<string> TextosInformacion_OperacionInterna { get; set; }
        public bool EntradaNoSeleccionada { get; set; }
        public bool EntradaProcesadaSeleccion { get; set; }        
        public TipoOpcionConjuntoNumerosEntrada TipoEntradaConjuntoNumeros { get; set; }
        public bool UtilizarCantidadNumeros { get; set; }
        public OpcionCantidadNumerosEntrada OpcionCantidadNumeros { get; set; }
        public ElementoEntradaEjecucion()
        {
            TextosInformacionFijos = new List<string>();
            OperacionInterna = TipoOperacionAritmeticaEjecucion.Ninguna;
            TextosInformacion_OperacionInterna = new List<string>();
            OrigenesDatos = new List<ElementoOrigenDatosEjecucion>();
            Numeros = new List<EntidadNumero>();
        }

        public new ElementoEntradaEjecucion CopiarObjeto()
        {
            ElementoEntradaEjecucion elemento = new ElementoEntradaEjecucion();
            elemento.ConsiderarProcesamiento_Agrupamiento = ConsiderarProcesamiento_Agrupamiento;
            elemento.ConsiderarProcesamiento_CondicionFlujo = ConsiderarProcesamiento_CondicionFlujo;
            elemento.ConsiderarProcesamiento_SeleccionarOrdenar = ConsiderarProcesamiento_SeleccionarOrdenar;
            elemento.ElementoDiseñoCalculoRelacionado = ElementoDiseñoCalculoRelacionado;
            elemento.ElementoDiseñoRelacionado = ElementoDiseñoRelacionado;
            elemento.Nombre = Nombre;
            elemento.Textos = Textos.ToList();
            elemento.TipoEntrada = TipoEntrada;
            elemento.OrigenesDatos = OrigenesDatos;
            elemento.TextosInformacionFijos = TextosInformacionFijos;
            elemento.OperacionInterna = OperacionInterna;
            elemento.TextosInformacion_OperacionInterna = TextosInformacion_OperacionInterna;
            elemento.EntradaNoSeleccionada = EntradaNoSeleccionada;
            elemento.EntradaProcesadaSeleccion = EntradaProcesadaSeleccion;
            elemento.TipoEntradaConjuntoNumeros = TipoEntradaConjuntoNumeros;
            elemento.UtilizarCantidadNumeros = UtilizarCantidadNumeros;
            elemento.OpcionCantidadNumeros = OpcionCantidadNumeros;

            return elemento;
        }
        
        public void SeleccionarFiltrarNumeros(ElementoOrigenDatosEjecucion origenDatos)
        {
            int Posicion = ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija;
            bool valorCondiciones = true;

            List<EntidadNumero> NumerosFiltrados = new List<EntidadNumero>();

            switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.Opcion)
            {
                case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarTodosNumeros:

                    switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OpcionPosicionInicial)
                    {
                        case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionInicialFija:
                            NumerosFiltrados = Numeros.Take(new Range(ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija - 1, Numeros.Count - ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija + 1)).ToList();
                            break;

                        case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionInicial_UltimaEjecucion:
                        case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionFinal_UltimaEjecucion:

                            switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OpcionPosicionInicial)
                            {
                                case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionInicial_UltimaEjecucion:
                                    Posicion = origenDatos != null ? origenDatos.PosicionInicialNumeros_Obtenidos_UltimaEjecucion : 0;
                                    if (Posicion == 0)
                                        Posicion = ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija;
                                    break;

                                case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionFinal_UltimaEjecucion:
                                    Posicion = origenDatos != null ? origenDatos.PosicionFinalNumeros_Obtenidos_UltimaEjecucion : 0;
                                    if (Posicion == 0)
                                        Posicion = ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija;
                                    break;
                            }

                            NumerosFiltrados = Numeros.Take(new Range(Posicion - 1, Numeros.Count - Posicion + 1)).ToList();
                            break;
                    }

                    break;

                case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarCantidadDeterminadaNumeros:

                    var listaNumeros = Numeros.ToList();


                    if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros < listaNumeros.Count)
                    {
                        switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OpcionPosicionInicial)
                        {
                            case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionInicialFija:
                                if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OrdenInverso)
                                    listaNumeros = listaNumeros.Take(
                                            new Range(ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija -
                                            ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros > 0 ?
                                            ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija -
                                            ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros : 0,
                                            ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros)).ToList();
                                else
                                    listaNumeros = listaNumeros.Take(
                                        new Range(ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija - 1,
                                        listaNumeros.Count < (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija - 1) +
                                        ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros ?
                                        (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija - 1) +
                                        ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros :
                                        listaNumeros.Count - ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija + 1)).ToList();

                                break;

                            case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionInicial_UltimaEjecucion:
                            case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionFinal_UltimaEjecucion:

                                switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OpcionPosicionInicial)
                                {
                                    case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionInicial_UltimaEjecucion:
                                        Posicion = origenDatos != null ? origenDatos.PosicionInicialNumeros_Obtenidos_UltimaEjecucion : 0;
                                        if (Posicion == 0)
                                            Posicion = ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija;
                                        break;

                                    case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionFinal_UltimaEjecucion:
                                        Posicion = origenDatos != null ? origenDatos.PosicionFinalNumeros_Obtenidos_UltimaEjecucion : 0;
                                        if (Posicion == 0)
                                            Posicion = ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija;
                                        break;
                                }

                                if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OrdenInverso)
                                    listaNumeros = listaNumeros.Take(
                                            new Range(Posicion -
                                            ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros > 0 ?
                                            Posicion -
                                            ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros : 0,
                                            ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros)).ToList();
                                else
                                    listaNumeros = listaNumeros.Take(
                                        new Range(Posicion - 1,
                                        listaNumeros.Count < (Posicion - 1) +
                                        ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros ?
                                        (Posicion - 1) +
                                        ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CantidadDeterminadaNumeros :
                                        listaNumeros.Count - Posicion + 1)).ToList();
                                break;

                        }
                    }

                    NumerosFiltrados = listaNumeros;

                    break;
            }

            if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros != null)
            {
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadNumeros_Obtenidos = NumerosFiltrados.Count;
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.PosicionInicialNumeros_Obtenidos = Posicion;
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.PosicionFinalNumeros_Obtenidos = Posicion + NumerosFiltrados.Count;
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadTextosInformacion_Obtenidos = NumerosFiltrados.Select(i => i.Textos.ToList()).Sum(j => j.Count);
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadTotalNumeros_Entrada = Numeros.Count;
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadTotalTextosInformacion_Entrada = Numeros.Select(i => i.Textos.ToList()).Sum(j => j.Count);
                valorCondiciones = ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.EvaluarCondiciones(OrigenesDatos.FirstOrDefault());
            }

            bool mostrarConfig = false;

            switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OpcionConfiguracion)
            {
                case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_Entrada.DefinicionAutomatica:
                    break;

                case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_Entrada.DefinicionManual:
                    mostrarConfig = true;
                    break;

                case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_Entrada.DefinicionAlternada:

                    if (VerificarCambios())
                    {
                        mostrarConfig = true;
                    }
                    break;

                case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_Entrada.DefinicionAlternada_Condiciones:
                    if (valorCondiciones)
                    {
                        mostrarConfig = true;
                    }

                    break;

                case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_Entrada.DefinicionAlternada_CondicionesNoCumplen:
                    if (!valorCondiciones)
                    {
                        mostrarConfig = true;
                    }

                    break;
            }

            if (mostrarConfig)
            {
                ConfigSeleccionCantidades_Entrada config = new ConfigSeleccionCantidades_Entrada();
                config.Definicion = ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CopiarObjeto();
                if (config.ShowDialog() == true)
                {
                    ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros = config.Definicion;
                }
            }

            //if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OrdenInverso)
            //    Numeros.Reverse();

            switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.Opcion)
            {
                case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarTodosNumeros:

                    bool seleccionarNumeros = true;

                    if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.ConCondiciones)
                    {
                        seleccionarNumeros = valorCondiciones;
                    }

                    if (!seleccionarNumeros)
                    {
                        Numeros.Clear();
                        break;
                    }
                    else
                    {
                        Numeros = NumerosFiltrados.ToList();
                    }

                    break;

                case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarCantidadDeterminadaNumeros:

                    seleccionarNumeros = true;

                    if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.ConCondiciones)
                    {
                        seleccionarNumeros = valorCondiciones;
                    }

                    if (seleccionarNumeros)
                    {
                        Numeros = NumerosFiltrados.ToList();
                    }
                    else
                    {
                        Numeros.Clear();
                    }

                    break;
            }

            if (origenDatos != null)
            {
                origenDatos.PosicionInicialNumeros_Obtenidos_UltimaEjecucion = Posicion;
                origenDatos.PosicionFinalNumeros_Obtenidos_UltimaEjecucion = Posicion + Numeros.Count;
            }
        }

        public List<EntidadNumero> Numeros_Filtrados(ElementoOperacionAritmeticaEjecucion operacion)
        {
            return Numeros.Where(i => (((!i.ElementosSalidaOperacion_Agrupamiento.Any()) ||
                                    (i.ElementosSalidaOperacion_Agrupamiento.Contains(operacion))))).ToList();
        }
              

        public void AgregarClasificadoresGenericos_DesdeCantidades()
        {
            foreach (var itemElemento in Numeros)
            {
                foreach (var itemClasificador in itemElemento.Clasificadores_SeleccionarOrdenar)
                {
                    if (!Clasificadores_Cantidades.Contains(itemClasificador))
                    {
                        Clasificadores_Cantidades.Add(itemClasificador);
                    }
                }
            }
        }

        public static void RealizarOperacionInterna(ElementoEntradaEjecucion entrada, 
            ref bool ConError, ref string log, EjecucionCalculo ejecucion)
        {
            EntidadNumero numeroResultado = new EntidadNumero();

            foreach (var itemNumero in entrada.Numeros)
            {
                if (ejecucion.Pausada) ejecucion.Pausar();
                if (ejecucion.Detenida) return;

                switch (entrada.OperacionInterna)
                {
                    case TipoOperacionAritmeticaEjecucion.Suma:
                        numeroResultado.Numero += itemNumero.Numero;

                        break;

                    case TipoOperacionAritmeticaEjecucion.Resta:
                        if (itemNumero == entrada.Numeros.First())
                            numeroResultado.Numero = itemNumero.Numero;
                        else
                            numeroResultado.Numero -= itemNumero.Numero;
                        break;

                    case TipoOperacionAritmeticaEjecucion.Multiplicacion:
                        if (itemNumero == entrada.Numeros.First())
                            numeroResultado.Numero = itemNumero.Numero;
                        else
                            numeroResultado.Numero *= itemNumero.Numero;
                        break;

                    case TipoOperacionAritmeticaEjecucion.Division:

                        if (itemNumero == entrada.Numeros.First())
                        {
                            numeroResultado.Numero = itemNumero.Numero;
                        }
                        else
                        {
                            if (itemNumero.Numero != 0)
                                numeroResultado.Numero /= itemNumero.Numero;
                            else
                            {
                                ConError = true;
                                log = "Error de división por cero.";
                                return;
                            }
                        }

                        break;
                }
            }

            entrada.Numeros.Clear();
            entrada.Numeros.Add(numeroResultado);

        }

        public static void RealizarOperacionInterna_PotenciaRaiz(ElementoEntradaEjecucion entrada,
            ref bool ConError, ref string log, EjecucionCalculo ejecucion)
        {
            List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

            switch (entrada.OperacionInterna)
            {
                case TipoOperacionAritmeticaEjecucion.Potencia:

                    bool utilizandoBase = true;
                    List<EntidadNumero> basesPotencia = new List<EntidadNumero>();
                    List<EntidadNumero> exponentesPotencia = new List<EntidadNumero>();
                    
                    //string strOperandoPotencia = string.Empty;
                    //string strOperandosResultados = string.Empty;
                    
                    //int indiceResultadosMensaje = 0;

                    //List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

                    //int indiceNumero = 0;
                    foreach (var itemNumero in entrada.Numeros)
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        //strOperandoPotencia = string.Empty;

                        //string strBaseExponente = string.Empty;
                        //if (utilizandoBase)
                        //    strBaseExponente = "Base: ";
                        //else
                        //    strBaseExponente = "Exponente: ";

                        //strOperandoPotencia += strBaseExponente + " " + itemNumero.Nombre;
                        //strOperandosResultados += strOperandoPotencia + " ";
                        //strMensajeLog += strOperandoPotencia + "\n";

                        if (utilizandoBase)
                        {
                            basesPotencia.Add(new EntidadNumero(string.Empty, itemNumero.Numero));
                            utilizandoBase = false;
                        }
                        else
                        {
                            exponentesPotencia.Add(new EntidadNumero(string.Empty, itemNumero.Numero));

                            foreach (EntidadNumero basePotencia in basesPotencia)
                            {
                                if (ejecucion.Pausada) ejecucion.Pausar();
                                if (ejecucion.Detenida) return;

                                foreach (EntidadNumero exponentePotencia in exponentesPotencia)
                                {
                                    if (ejecucion.Pausada) ejecucion.Pausar();
                                    if (ejecucion.Detenida) return;

                                    EntidadNumero resultadoPotencia = new EntidadNumero();
                                    resultadoPotencia.Numero = Math.Pow(basePotencia.Numero, exponentePotencia.Numero);

                                    //string cadenaParteNumericaNombre = string.Empty;

                                    //if (indiceNumero > 0)
                                    //    cadenaParteNumericaNombre = ", posición " + (indiceNumero + 1).ToString();

                                    //resultadoPotencia.Nombre = "Potencia de '" + entrada.Nombre + "'" + cadenaParteNumericaNombre;

                                    numerosOperacion.Add(resultadoPotencia);
                                    //indiceNumero++;
                                }
                            }

                            //strMensajeLogResultados += "Potencia con " + strOperandosResultados + " de resultados:\n";

                            //for (int indice = indiceResultadosMensaje; ; indice++)
                            //{
                            //    ElementoEjecucionCalculo subItem_ElementoOperacion = ElementosOperacion[indice];
                            //    strMensajeLogResultados += subItem_ElementoOperacion.ValorNumerico.ToString() + "\n";

                            //    if (indice == ElementosOperacion.Count - 1)
                            //    {
                            //        indiceResultadosMensaje = indice + 1;
                            //        break;
                            //    }
                            //}

                            //if (ElementosOperacion.Count > 0) strMensajeLogResultados += "\n";
                            //strOperandoPotencia = string.Empty;
                            //strOperandosResultados = string.Empty;
                            utilizandoBase = true;

                            basesPotencia.Clear();
                            exponentesPotencia.Clear();
                        }
                    }

                    entrada.Numeros.Clear();
                    entrada.Numeros.AddRange(numerosOperacion);

                    break;

                case TipoOperacionAritmeticaEjecucion.Raiz:

                    bool utilizandoRaiz = true;
                    List<EntidadNumero> raicesRaiz = new List<EntidadNumero>();
                    List<EntidadNumero> radicalesRaiz = new List<EntidadNumero>();
                    
                    //string strOperandoRaiz = string.Empty;
                    //string strOperandosResultados = string.Empty;
                    
                    //int indiceResultadosMensaje = 0;

                    //List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

                    //int indiceNumero = 0;
                    foreach (var itemNumero in entrada.Numeros)
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        //strOperandoRaiz = string.Empty;

                        //string strRaizRadical = string.Empty;
                        //if (utilizandoRaiz)
                        //    strRaizRadical = "Raíz: ";
                        //else
                        //    strRaizRadical = "Radical: ";

                        //strOperandoRaiz += strRaizRadical + " " + itemEntrada.Nombre;
                        //strOperandosResultados += strOperandoRaiz + " ";
                        //strMensajeLog += strOperandoRaiz + "\n";

                        if (utilizandoRaiz)
                        {
                            raicesRaiz.Add(new EntidadNumero(string.Empty, itemNumero.Numero));
                            utilizandoRaiz = false;
                        }
                        else
                        {
                            radicalesRaiz.Add(new EntidadNumero(string.Empty, itemNumero.Numero));

                            foreach (EntidadNumero raizRaiz in raicesRaiz)
                            {
                                if (ejecucion.Pausada) ejecucion.Pausar();
                                if (ejecucion.Detenida) return;

                                foreach (EntidadNumero radicalRaiz in radicalesRaiz)
                                {
                                    if (ejecucion.Pausada) ejecucion.Pausar();
                                    if (ejecucion.Detenida) return;

                                    EntidadNumero resultadoRaiz = new EntidadNumero();
                                    resultadoRaiz.Numero = Math.Pow(radicalRaiz.Numero, 1 / raizRaiz.Numero);

                                    //string cadenaParteNumericaNombre = string.Empty;

                                    //if (indiceNumero > 0)
                                    //    cadenaParteNumericaNombre = ", posición " + (indiceNumero + 1).ToString();

                                    //resultadoRaiz.Nombre = "Raíz de '" + entrada.Nombre + "'" + cadenaParteNumericaNombre;

                                    numerosOperacion.Add(resultadoRaiz);
                                    //indiceNumero++;
                                }
                            }

                            //strMensajeLogResultados += "Raíz con " + strOperandosResultados + " de resultados:\n";

                            //for (int indice = indiceResultadosMensaje; ; indice++)
                            //{
                            //    ElementoEjecucionCalculo subItem_ElementoOperacion = ElementosOperacion[indice];
                            //    strMensajeLogResultados += subItem_ElementoOperacion.ValorNumerico.ToString() + "\n";

                            //    if (indice == ElementosOperacion.Count - 1)
                            //    {
                            //        indiceResultadosMensaje = indice + 1;
                            //        break;
                            //    }
                            //}

                            //if (ElementosOperacion.Count > 0) strMensajeLogResultados += "\n";
                            //strOperandoRaiz = string.Empty;
                            //strOperandosResultados = string.Empty;
                            utilizandoRaiz = true;

                            raicesRaiz.Clear();
                            radicalesRaiz.Clear();
                        }
                    }

                    entrada.Numeros.Clear();
                    entrada.Numeros.AddRange(numerosOperacion);

                    break;
            }
        }

        public static void RealizarOperacionInterna_SeleccionarOrdenar(ElementoEntradaEjecucion entrada, EjecucionCalculo ejecucion)
        {
            List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

            foreach (var texto in entrada.TextosInformacion_OperacionInterna)
            {
                if (ejecucion.Pausada) ejecucion.Pausar();
                if (ejecucion.Detenida) return;

                int cantidadNumeros = 0;
                foreach (var itemNumero in entrada.Numeros)
                {
                    if (ejecucion.Pausada) ejecucion.Pausar();
                    if (ejecucion.Detenida) return;

                    if (ejecucion.VerificarTextoInformacion(texto, itemNumero.Textos))
                    {
                        //strMensajeLogResultados += "Cantidad seleccionada: " + itemEntrada.Nombre + "\n";
                        //strMensajeLog += "Cantidad seleccionada: " + itemEntrada.Nombre + "\n";

                        numerosOperacion.Add(itemNumero);
                        cantidadNumeros++;
                    }
                }

                if (entrada.UtilizarCantidadNumeros)
                {
                    EntidadNumero numeroAgregar = new EntidadNumero("Cantidad de números del vector de entrada '" + entrada.Nombre + "' - '" + texto + "'", cantidadNumeros);
                    numeroAgregar.Textos.Add("cantidad");
                    numeroAgregar.Textos.Add(texto);

                    if (entrada.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.AgregarCantidadNumeros)
                    {
                        numerosOperacion.Add(numeroAgregar);
                    }
                    else if (entrada.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.UtilizarSoloCantidaadNumeros)
                    {
                        numerosOperacion.Clear();
                        numerosOperacion.Add(numeroAgregar);
                    }
                }
            }

            entrada.Numeros.Clear();
            entrada.Numeros.AddRange(numerosOperacion.Distinct());
        }
        public static void RealizarOperacionInterna_Porcentaje(ElementoEntradaEjecucion entrada,
                ref bool ConError, ref string log, EjecucionCalculo ejecucion)
        {
            List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

            switch (entrada.OperacionInterna)
            {
                case TipoOperacionAritmeticaEjecucion.Porcentaje:

                    bool utilizandoPorcentaje = true;
                    List<EntidadNumero> porcentajes = new List<EntidadNumero>();
                    List<EntidadNumero> cantidades = new List<EntidadNumero>();

                    //string strOperandoPotencia = string.Empty;
                    //string strOperandosResultados = string.Empty;

                    //int indiceResultadosMensaje = 0;

                    //List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

                    //int indiceNumero = 0;
                    foreach (var itemNumero in entrada.Numeros)
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        //strOperandoPotencia = string.Empty;

                        //string strBaseExponente = string.Empty;
                        //if (utilizandoBase)
                        //    strBaseExponente = "Base: ";
                        //else
                        //    strBaseExponente = "Exponente: ";

                        //strOperandoPotencia += strBaseExponente + " " + itemNumero.Nombre;
                        //strOperandosResultados += strOperandoPotencia + " ";
                        //strMensajeLog += strOperandoPotencia + "\n";

                        if (utilizandoPorcentaje)
                        {
                            porcentajes.Add(new EntidadNumero(string.Empty, itemNumero.Numero));
                            utilizandoPorcentaje = false;
                        }
                        else
                        {
                            cantidades.Add(new EntidadNumero(string.Empty, itemNumero.Numero));

                            foreach (EntidadNumero porcentaje in porcentajes)
                            {
                                if (ejecucion.Pausada) ejecucion.Pausar();
                                if (ejecucion.Detenida) return;

                                foreach (EntidadNumero cantidad in cantidades)
                                {
                                    if (ejecucion.Pausada) ejecucion.Pausar();
                                    if (ejecucion.Detenida) return;

                                    EntidadNumero resultadoPorcentaje = new EntidadNumero();
                                    resultadoPorcentaje.Numero = (cantidad.Numero / (double)100.0) * porcentaje.Numero;

                                    //string cadenaParteNumericaNombre = string.Empty;

                                    //if (indiceNumero > 0)
                                    //    cadenaParteNumericaNombre = ", posición " + (indiceNumero + 1).ToString();

                                    //resultadoPotencia.Nombre = "Potencia de '" + entrada.Nombre + "'" + cadenaParteNumericaNombre;

                                    numerosOperacion.Add(resultadoPorcentaje);
                                    //indiceNumero++;
                                }
                            }

                            //strMensajeLogResultados += "Potencia con " + strOperandosResultados + " de resultados:\n";

                            //for (int indice = indiceResultadosMensaje; ; indice++)
                            //{
                            //    ElementoEjecucionCalculo subItem_ElementoOperacion = ElementosOperacion[indice];
                            //    strMensajeLogResultados += subItem_ElementoOperacion.ValorNumerico.ToString() + "\n";

                            //    if (indice == ElementosOperacion.Count - 1)
                            //    {
                            //        indiceResultadosMensaje = indice + 1;
                            //        break;
                            //    }
                            //}

                            //if (ElementosOperacion.Count > 0) strMensajeLogResultados += "\n";
                            //strOperandoPotencia = string.Empty;
                            //strOperandosResultados = string.Empty;
                            utilizandoPorcentaje = true;

                            porcentajes.Clear();
                            cantidades.Clear();
                        }
                    }

                    entrada.Numeros.Clear();
                    entrada.Numeros.AddRange(numerosOperacion);

                    break;
            }
        }
        public static void RealizarOperacionInterna_Logaritmo(ElementoEntradaEjecucion entrada,
            ref bool ConError, ref string log, EjecucionCalculo ejecucion)
        {
            List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

            switch (entrada.OperacionInterna)
            {
                case TipoOperacionAritmeticaEjecucion.Logaritmo:

                    bool utilizandoBase = true;
                    List<EntidadNumero> basesLogaritmo = new List<EntidadNumero>();
                    List<EntidadNumero> argumentosLogaritmo = new List<EntidadNumero>();

                    //string strOperandoPotencia = string.Empty;
                    //string strOperandosResultados = string.Empty;

                    //int indiceResultadosMensaje = 0;

                    //List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

                    //int indiceNumero = 0;
                    foreach (var itemNumero in entrada.Numeros)
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        //strOperandoPotencia = string.Empty;

                        //string strBaseExponente = string.Empty;
                        //if (utilizandoBase)
                        //    strBaseExponente = "Base: ";
                        //else
                        //    strBaseExponente = "Exponente: ";

                        //strOperandoPotencia += strBaseExponente + " " + itemNumero.Nombre;
                        //strOperandosResultados += strOperandoPotencia + " ";
                        //strMensajeLog += strOperandoPotencia + "\n";

                        if (utilizandoBase)
                        {
                            basesLogaritmo.Add(new EntidadNumero(string.Empty, itemNumero.Numero));
                            utilizandoBase = false;
                        }
                        else
                        {
                            argumentosLogaritmo.Add(new EntidadNumero(string.Empty, itemNumero.Numero));

                            foreach (EntidadNumero baseLogaritmo in basesLogaritmo)
                            {
                                if (ejecucion.Pausada) ejecucion.Pausar();
                                if (ejecucion.Detenida) return;

                                foreach (EntidadNumero argumentoLogaritmo in argumentosLogaritmo)
                                {
                                    if (ejecucion.Pausada) ejecucion.Pausar();
                                    if (ejecucion.Detenida) return;

                                    EntidadNumero resultadoLogaritmo = new EntidadNumero();
                                    resultadoLogaritmo.Numero = Math.Log(argumentoLogaritmo.Numero, baseLogaritmo.Numero);

                                    //string cadenaParteNumericaNombre = string.Empty;

                                    //if (indiceNumero > 0)
                                    //    cadenaParteNumericaNombre = ", posición " + (indiceNumero + 1).ToString();

                                    //resultadoPotencia.Nombre = "Potencia de '" + entrada.Nombre + "'" + cadenaParteNumericaNombre;

                                    numerosOperacion.Add(resultadoLogaritmo);
                                    //indiceNumero++;
                                }
                            }

                            //strMensajeLogResultados += "Potencia con " + strOperandosResultados + " de resultados:\n";

                            //for (int indice = indiceResultadosMensaje; ; indice++)
                            //{
                            //    ElementoEjecucionCalculo subItem_ElementoOperacion = ElementosOperacion[indice];
                            //    strMensajeLogResultados += subItem_ElementoOperacion.ValorNumerico.ToString() + "\n";

                            //    if (indice == ElementosOperacion.Count - 1)
                            //    {
                            //        indiceResultadosMensaje = indice + 1;
                            //        break;
                            //    }
                            //}

                            //if (ElementosOperacion.Count > 0) strMensajeLogResultados += "\n";
                            //strOperandoPotencia = string.Empty;
                            //strOperandosResultados = string.Empty;
                            utilizandoBase = true;

                            basesLogaritmo.Clear();
                            argumentosLogaritmo.Clear();
                        }
                    }

                    entrada.Numeros.Clear();
                    entrada.Numeros.AddRange(numerosOperacion);

                    break;
            }
        }

        public static void RealizarOperacionInterna_Inverso(ElementoEntradaEjecucion entrada,
            ref bool ConError, ref string log, EjecucionCalculo ejecucion)
        {
            List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

            switch (entrada.OperacionInterna)
            {
                case TipoOperacionAritmeticaEjecucion.Inverso:

                    foreach (var itemNumero in entrada.Numeros)
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        switch (entrada.ElementoDiseñoRelacionado.EntradaRelacionada.OpcionElementosFijosInverso)
                        {
                            case TipoOpcionElementosFijosOperacionInverso.InversoSumaResta:
                                itemNumero.Numero = itemNumero.Numero * (double)(-1);
                                break;

                            case TipoOpcionElementosFijosOperacionInverso.InversoMultiplicacionDivision:
                                itemNumero.Numero = (double)(1) / itemNumero.Numero;
                                break;
                        }

                        numerosOperacion.Add(itemNumero);

                    }

                    entrada.Numeros.Clear();
                    entrada.Numeros.AddRange(numerosOperacion);

                    break;
            }
        }

        public static void RealizarOperacionInterna_Factorial(ElementoEntradaEjecucion entrada,
            ref bool ConError, ref string log, EjecucionCalculo ejecucion, List<string> logTextos)
        {
            List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

            switch (entrada.OperacionInterna)
            {
                case TipoOperacionAritmeticaEjecucion.Factorial:

                    foreach (var itemNumero in entrada.Numeros)
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        itemNumero.Numero = ejecucion.Factorial(itemNumero.Numero, logTextos);
                        numerosOperacion.Add(itemNumero);
                    }

                    entrada.Numeros.Clear();
                    entrada.Numeros.AddRange(numerosOperacion);

                    break;
            }
        }

        public static void RealizarOperacionInterna_LimpiarDatos(ElementoEntradaEjecucion entrada, EjecucionCalculo ejecucion)
        {
            List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

            switch (entrada.OperacionInterna)
            {
                case TipoOperacionAritmeticaEjecucion.LimpiarDatos:

                    foreach (var itemNumero in entrada.Numeros)
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        if (itemNumero.EvaluarCondicionesLimpieza(entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ConfigLimpiarDatos_OperacionInterna,
                                                            entrada.Numeros))
                            numerosOperacion.Add(itemNumero);

                    }

                    entrada.Numeros.Clear();
                    entrada.Numeros.AddRange(numerosOperacion);

                    break;
            }
        }

        public static void RealizarOperacionInterna_RedondearCantidades(ElementoEntradaEjecucion entrada, EjecucionCalculo ejecucion)
        {
            List<EntidadNumero> numerosOperacion = new List<EntidadNumero>();

            switch (entrada.OperacionInterna)
            {
                case TipoOperacionAritmeticaEjecucion.RedondearCantidades:

                    foreach (var itemNumero in entrada.Numeros)
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        itemNumero.Redondear(entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ConfigRedondeo_OperacionInterna);
                        numerosOperacion.Add(itemNumero);
                    }

                    entrada.Numeros.Clear();
                    entrada.Numeros.AddRange(numerosOperacion);

                    break;
            }
        }

        public void LeerBusquedasEntradaArchivo(ElementoArchivoOrigenDatosEjecucion archivo, ElementoEntradaEjecucion numeros,
            ElementoEntradaEjecucion entrada, bool MismaLecturaArchivo, bool mostrarLog, ref bool ConError, EjecucionCalculo ejecucion, List<FileStream> archivosAbiertos,
            List<ElementoArchivoOrigenDatosEjecucion> ArchivosOrigenesDatos,
            ref string rutaTemporalArchivoFTP, ref string RutaArchivo, HttpResponseMessage respuestaArchivoFTP, List<string> log, List<string> logResultados,
            LecturaNavegacion lecturaNavegacion = null, bool archivoTemporal_EntradaManual = false)
        {
            if (MismaLecturaArchivo)
            {
                bool archivoTemporal = false;

                if (archivoTemporal_EntradaManual)
                {
                    rutaTemporalArchivoFTP = RutaArchivo;
                    archivo.TipoFormatoArchivo_Entrada = TipoFormatoArchivoEntrada.ArchivoTextoPlano;
                    archivoTemporal = true;
                }
                else
                {
                    switch (archivo.TipoArchivo)
                    {
                        case TipoArchivo.EquipoLocal:
                        case TipoArchivo.RedLocal:
                            rutaTemporalArchivoFTP = RutaArchivo;
                            break;

                        case TipoArchivo.ServidorFTP:
                        case TipoArchivo.Internet:

                            rutaTemporalArchivoFTP = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();
                            archivoTemporal = true;
                            ElementoArchivoOrigenDatosEjecucion.GuardarArchivoTemporalEntrada_FTP(rutaTemporalArchivoFTP, respuestaArchivoFTP);
                            break;
                    }
                }

                Encoding codificacion = null;

                if(archivo.Busquedas.FirstOrDefault() != null &&
                    !string.IsNullOrEmpty(archivo.Busquedas.FirstOrDefault().Codificacion))
                {
                    try
                    {
                        codificacion = Encoding.GetEncoding(archivo.Busquedas.FirstOrDefault().Codificacion);
                    }
                    catch (Exception)
                    {
                        codificacion = Encoding.Default;
                    }
                }
                else
                {
                    codificacion = Encoding.Default;
                }

                rutaTemporalArchivoFTP = archivo.ObtenerArchivoTextoPlano_Temporal(rutaTemporalArchivoFTP, archivoTemporal, codificacion,
                        ref mostrarLog, log, ref ConError, ejecucion, entrada.ElementoDiseñoRelacionado.EntradaRelacionada, entrada);

                string ruta = rutaTemporalArchivoFTP;
                while (archivosAbiertos.Any(i => i.Name == ruta && i.CanRead))
                    Thread.Sleep(10);

                while (true)
                {
                    try
                    {
                        archivo.lectorArchivo = new StreamReader(new FileStream(rutaTemporalArchivoFTP, FileMode.OpenOrCreate), false);
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                }

                archivo.lector = (FileStream)archivo.lectorArchivo.BaseStream;
                archivosAbiertos.Add(archivo.lector);
                ArchivosOrigenesDatos.Add(archivo);
            }

            List<BusquedaArchivoEjecucion> busquedasAsignarNumeros = new List<BusquedaArchivoEjecucion>();

            int indiceBusqueda = 1;

            for (int indice = 0; indice < archivo.Busquedas.Count; indice++)
            {
                if (ejecucion.Pausada) ejecucion.Pausar();
                if (ejecucion.Detenida) return;

                archivo.Busquedas[indice] = archivo.Busquedas[indice].ReplicarBusqueda();
                BusquedaArchivoEjecucion.EjecutarBusquedas_ArchivoUrl(archivo.Busquedas[indice], mostrarLog, archivo, null, ref rutaTemporalArchivoFTP,
                    ref RutaArchivo, ref respuestaArchivoFTP, archivosAbiertos, ArchivosOrigenesDatos, log, logResultados, entrada, ref ConError,
                    ejecucion, busquedasAsignarNumeros, numeros, ref indiceBusqueda, null, lecturaNavegacion,
                    Encoding.GetEncoding(archivo.Busquedas[indice].Codificacion), MismaLecturaArchivo);
                numeros.CantidadSubElementosProcesados++;
            }

            int posicion_2 = 0;
            foreach (var itemNumero in numeros.Numeros)
            {
                if (ejecucion.Pausada) ejecucion.Pausar();
                if (ejecucion.Detenida) return;

                posicion_2++;
                ejecucion.EstablecerNombreCantidad(itemNumero, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades, posicion_2, null, entrada);
            }

            if (numeros.UtilizarCantidadNumeros)
            {
                EntidadNumero numeroAgregar = new EntidadNumero("Cantidad de números del vector de entrada '" + numeros.Nombre + "'", numeros.Numeros.Count);
                numeroAgregar.Textos.Add("cantidad");

                if (numeros.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.AgregarCantidadNumeros)
                {
                    numeros.Numeros.Add(numeroAgregar);
                }
                else if (numeros.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.UtilizarSoloCantidaadNumeros)
                {
                    numeros.Numeros.Clear();
                    numeros.Numeros.Add(numeroAgregar);
                }
            }

            if (MismaLecturaArchivo)
            {
                archivo.lector.Close();
                archivo.lectorArchivo.Close();
                archivosAbiertos.Remove(archivo.lector);
                archivo.CaracteresCorridos = 0;
                archivo.CaracteresDescartados = 0;

                if (((archivo.TipoArchivo == TipoArchivo.ServidorFTP |
                            archivo.TipoArchivo == TipoArchivo.Internet) ||
                            ((archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                            archivo.TipoArchivo == TipoArchivo.RedLocal) &&
                            (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.ArchivoTextoPlano ||
                            archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla))) &
                !string.IsNullOrEmpty(rutaTemporalArchivoFTP) &
                File.Exists(rutaTemporalArchivoFTP))
                    File.Delete(rutaTemporalArchivoFTP);

                ArchivosOrigenesDatos.Remove(archivo);
            }
        }

        public void LeerBusquedasEntradaURL(ElementoInternetOrigenDatosEjecucion url, ElementoEntradaEjecucion numeros,
             ElementoEntradaEjecucion entrada, bool MismaLecturaArchivo, bool mostrarLog, ref bool ConError, EjecucionCalculo ejecucion, List<FileStream> archivosAbiertos,
             List<ElementoArchivoOrigenDatosEjecucion> ArchivosOrigenesDatos,
            ref string rutaTemporalArchivoFTP, ref string RutaArchivo, HttpResponseMessage respuestaArchivoFTP, List<string> log,
            List<string> logResultados, LecturaNavegacion lecturaNavegacion = null)
        {
            if (MismaLecturaArchivo)
            {
                //archivo.lector = new FileStream(archivo.RutaArchivo, FileMode.OpenOrCreate);
                url.ContenidoTexto = url.ObjetoURL.ObtenerTexto().Replace(" ", string.Empty);
            }

            int indiceBusqueda = 1;

            List<BusquedaArchivoEjecucion> busquedasAsignarNumeros = new List<BusquedaArchivoEjecucion>();

            foreach (var busqueda in url.Busquedas)
            {
                if (ejecucion.Pausada) ejecucion.Pausar();
                if (ejecucion.Detenida) return;

                rutaTemporalArchivoFTP = string.Empty;
                RutaArchivo = string.Empty;
                respuestaArchivoFTP = null;

                BusquedaArchivoEjecucion.EjecutarBusquedas_ArchivoUrl(busqueda, mostrarLog, null, url, ref rutaTemporalArchivoFTP,
                    ref RutaArchivo, ref respuestaArchivoFTP, archivosAbiertos, ArchivosOrigenesDatos, log, logResultados, entrada, ref ConError,
                    ejecucion, busquedasAsignarNumeros, numeros, ref indiceBusqueda, null, lecturaNavegacion, Encoding.GetEncoding(busqueda.Codificacion),
                    MismaLecturaArchivo);
                numeros.CantidadSubElementosProcesados++;

            }

            //int posicion_ = 0;
            //foreach (var itemNumero in numeros.Numeros)
            //{
            //    posicion_++;
            //    EstablecerNombreCantidad(itemNumero, entrada.ElementoDiseñoRelacionado.EntradaRelacionada.DefinicionOpcionesNombresCantidades, posicion_, null, entrada, null, false);
            //}

            if (numeros.UtilizarCantidadNumeros)
            {
                EntidadNumero numeroAgregar = new EntidadNumero("Cantidad de números del vector de entrada '" + numeros.Nombre + "'", numeros.Numeros.Count);
                numeroAgregar.Textos.Add("cantidad");

                if (numeros.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.AgregarCantidadNumeros)
                {
                    numeros.Numeros.Add(numeroAgregar);
                }
                else if (numeros.OpcionCantidadNumeros == OpcionCantidadNumerosEntrada.UtilizarSoloCantidaadNumeros)
                {
                    numeros.Numeros.Clear();
                    numeros.Numeros.Add(numeroAgregar);
                }
            }
        }

        public void LeerBusquedasEntradaArchivo_TextosInformacion(ElementoArchivoOrigenDatosEjecucion archivo, ElementoConjuntoTextosEntradaEjecucion textos,
            ElementoEntradaEjecucion entrada, bool MismaLecturaArchivo, bool mostrarLog, ref bool ConError, EjecucionCalculo ejecucion, List<FileStream> archivosAbiertos,
            List<ElementoArchivoOrigenDatosEjecucion> ArchivosOrigenesDatos,
            ref string rutaTemporalArchivoFTP, ref string RutaArchivo, HttpResponseMessage respuestaArchivoFTP, List<string> log, List<string> logResultados,
            LecturaNavegacion lecturaNavegacion = null)
        {
            if (MismaLecturaArchivo)
            {
                bool archivoTemporal = false;

                switch (archivo.TipoArchivo)
                {
                    case TipoArchivo.EquipoLocal:
                    case TipoArchivo.RedLocal:
                        rutaTemporalArchivoFTP = RutaArchivo;
                        break;

                    case TipoArchivo.ServidorFTP:
                    case TipoArchivo.Internet:

                        rutaTemporalArchivoFTP = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();
                        archivoTemporal = true;
                        ElementoArchivoOrigenDatosEjecucion.GuardarArchivoTemporalEntrada_FTP(rutaTemporalArchivoFTP, respuestaArchivoFTP);
                        break;
                }

                Encoding codificacion = null;

                if (archivo.Busquedas.FirstOrDefault() != null &&
                    !string.IsNullOrEmpty(archivo.Busquedas.FirstOrDefault().Codificacion))
                {
                    try
                    {
                        codificacion = Encoding.GetEncoding(archivo.Busquedas.FirstOrDefault().Codificacion);
                    }
                    catch (Exception)
                    {
                        codificacion = Encoding.Default;
                    }
                }
                else
                {
                    codificacion = Encoding.Default;
                }

                rutaTemporalArchivoFTP = archivo.ObtenerArchivoTextoPlano_Temporal(rutaTemporalArchivoFTP, archivoTemporal, codificacion,
                    ref mostrarLog, log, ref ConError, ejecucion, entrada.ElementoDiseñoRelacionado.EntradaRelacionada, entrada);

                string ruta = rutaTemporalArchivoFTP;
                while (archivosAbiertos.Any(i => i.Name == ruta && i.CanRead))
                    Thread.Sleep(10);

                while (true)
                {
                    try
                    {
                        archivo.lectorArchivo = new StreamReader(new FileStream(rutaTemporalArchivoFTP, FileMode.OpenOrCreate), false);
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                }

                archivo.lector = (FileStream)archivo.lectorArchivo.BaseStream;
                archivosAbiertos.Add(archivo.lector);
                ArchivosOrigenesDatos.Add(archivo);
            }

            int indiceBusqueda = 1;

            List<BusquedaArchivoEjecucion> busquedasAsignarNumeros = new List<BusquedaArchivoEjecucion>();

            for (int indice = 0; indice < archivo.Busquedas.Count; indice++)
            {
                if (ejecucion.Pausada) ejecucion.Pausar();
                if (ejecucion.Detenida) return;

                archivo.Busquedas[indice] = archivo.Busquedas[indice].ReplicarBusqueda();
                BusquedaArchivoEjecucion.EjecutarBusquedas_ArchivoUrl_TextosInformacion(archivo.Busquedas[indice], mostrarLog, archivo, null, ref rutaTemporalArchivoFTP,
                ref RutaArchivo, ref respuestaArchivoFTP, archivosAbiertos, ArchivosOrigenesDatos, log, logResultados,  entrada, ref ConError,
                    ejecucion, textos, ref indiceBusqueda, null, null, null, lecturaNavegacion, Encoding.GetEncoding(archivo.Busquedas[indice].Codificacion));

                textos.CantidadSubElementosProcesados++;
            }

            if (MismaLecturaArchivo)
            {
                archivo.lector.Close();
                archivo.lectorArchivo.Close();
                archivosAbiertos.Remove(archivo.lector);
                archivo.CaracteresCorridos = 0;
                archivo.CaracteresDescartados = 0;

                if (((archivo.TipoArchivo == TipoArchivo.ServidorFTP |
                            archivo.TipoArchivo == TipoArchivo.Internet) ||
                            ((archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                            archivo.TipoArchivo == TipoArchivo.RedLocal) &&
                            (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.ArchivoTextoPlano ||
                            archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla))) &
                !string.IsNullOrEmpty(rutaTemporalArchivoFTP) &
                File.Exists(rutaTemporalArchivoFTP))
                    File.Delete(rutaTemporalArchivoFTP);

                ArchivosOrigenesDatos.Remove(archivo);
            }
        }

        public void LeerBusquedasEntradaURL_TextosInformacion(ElementoInternetOrigenDatosEjecucion url, ElementoConjuntoTextosEntradaEjecucion textos,
            ElementoEntradaEjecucion entrada, bool MismaLecturaArchivo, bool mostrarLog, ref bool ConError, EjecucionCalculo ejecucion, List<FileStream> archivosAbiertos,
            List<ElementoArchivoOrigenDatosEjecucion> ArchivosOrigenesDatos,
            ref string rutaTemporalArchivoFTP, ref string RutaArchivo, HttpResponseMessage respuestaArchivoFTP, List<string> log, List<string> logResultados, 
            LecturaNavegacion lecturaNavegacion = null)
        {
            if (MismaLecturaArchivo)
            {
                //archivo.lector = new FileStream(archivo.RutaArchivo, FileMode.OpenOrCreate);
                url.ContenidoTexto = url.ObjetoURL.ObtenerTexto().Replace(" ", string.Empty);
            }
            int indiceBusqueda = 1;

            List<BusquedaArchivoEjecucion> busquedasAsignarNumeros = new List<BusquedaArchivoEjecucion>();

            foreach (var busqueda in url.Busquedas)
            {
                if (ejecucion.Pausada) ejecucion.Pausar();
                if (ejecucion.Detenida) return;

                rutaTemporalArchivoFTP = string.Empty;
                RutaArchivo = string.Empty;
                respuestaArchivoFTP = null;

                BusquedaArchivoEjecucion.EjecutarBusquedas_ArchivoUrl_TextosInformacion(busqueda, mostrarLog, null, url, ref rutaTemporalArchivoFTP,
                    ref RutaArchivo, ref respuestaArchivoFTP, archivosAbiertos, ArchivosOrigenesDatos, log, logResultados, entrada, ref ConError,
                    ejecucion, textos, ref indiceBusqueda, null, null, null, lecturaNavegacion, Encoding.GetEncoding(busqueda.Codificacion));

                textos.CantidadSubElementosProcesados++;
                //if (!url.MismaLecturaArchivo) url.lector.Close();
            }
        }

        public bool VerificarCambios()
        {
            throw new NotImplementedException();
        }
    }
}
