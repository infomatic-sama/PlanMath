using ProcessCalc.Controles;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class ElementoConjuntoTextosEntradaEjecucion : ElementoEntradaEjecucion
    {
        public List<FilaTextosInformacion_Entrada> FilasTextosInformacion { get; set; }
        public TipoOpcionTextosInformacionEntrada TipoEntradaConjuntoTextos { get; set; }
        public ElementoConjuntoTextosEntradaEjecucion()
        {
            FilasTextosInformacion = new List<FilaTextosInformacion_Entrada>();
        }

        public new void SeleccionarFiltrarNumeros(ElementoOrigenDatosEjecucion origenDatos)
        {
            int Posicion = ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija;
            bool valorCondiciones = true;

            List<FilaTextosInformacion_Entrada> FilasFiltrada = new List<FilaTextosInformacion_Entrada>();

            switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.Opcion)
            {
                case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarTodosNumeros:

                    switch (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OpcionPosicionInicial)
                    {
                        case TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionInicialFija:
                            FilasFiltrada = FilasTextosInformacion.Take(new Range(ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija - 1, FilasTextosInformacion.Count - ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.PosicionInicialFija + 1)).ToList();
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

                            FilasFiltrada = FilasTextosInformacion.Take(new Range(Posicion - 1, FilasTextosInformacion.Count - Posicion + 1)).ToList();
                            break;
                    }

                    break;

                case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarCantidadDeterminadaNumeros:

                    var listaNumeros = FilasTextosInformacion.ToList();


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

                    FilasFiltrada = listaNumeros;

                    break;
            }

            if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros != null)
            {
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadNumeros_Obtenidos = FilasFiltrada.Count;
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadTextosInformacion_Obtenidos = FilasFiltrada.Select(i => i.TextosInformacion.ToList()).Sum(j => j.Count);
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.PosicionInicialNumeros_Obtenidos = Posicion;
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.PosicionFinalNumeros_Obtenidos = Posicion + FilasFiltrada.Count;
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadTotalNumeros_Entrada = FilasTextosInformacion.Count;
                ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadTotalTextosInformacion_Entrada = FilasTextosInformacion.Select(i => i.TextosInformacion.ToList()).Sum(j => j.Count);
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
                if(config.ShowDialog() == true)
                {
                    ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros = config.Definicion;
                }
            }

            //if (ElementoDiseñoRelacionado.EntradaRelacionada.SeleccionNumeros.OrdenInverso)
            //    FilasTextosInformacion.Reverse();

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
                        FilasTextosInformacion.Clear();
                        break;
                    }
                    else
                    {
                        FilasTextosInformacion = FilasFiltrada.ToList();
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
                        FilasTextosInformacion = FilasFiltrada.ToList();
                    }
                    else
                    {
                        FilasTextosInformacion.Clear();
                    }

                    break;
            }

            if (origenDatos != null)
            {
                origenDatos.PosicionInicialNumeros_Obtenidos_UltimaEjecucion = Posicion;
                origenDatos.PosicionFinalNumeros_Obtenidos_UltimaEjecucion = Posicion + FilasTextosInformacion.Count;
            }
        }
    }

    public class FilaTextosInformacion_Entrada
    {
        public List<string> TextosInformacion { get; set; }
        public int PosicionElemento_Lectura { get; set; }
        public FilaTextosInformacion_Entrada()
        {
            TextosInformacion = new List<string>();
        }
    }
}
