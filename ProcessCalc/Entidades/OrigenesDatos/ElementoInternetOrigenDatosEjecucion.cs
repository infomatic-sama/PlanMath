using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.OrigenesDatos
{
    public class ElementoInternetOrigenDatosEjecucion : ElementoOrigenDatosEjecucion
    {
        public OpcionBusquedaNumero TipoOpcionBusqueda { get; set; }
        public string URL { get; set; }
        public List<BusquedaArchivoEjecucion> Busquedas { get; set; }
        public bool MismaLecturaArchivo { get; set; }
        public string ContenidoTexto { get; set; }
        public ObjetoURL ObjetoURL { get; set; }
        public int IndiceProcesamientoTexto { get; set; }
        public OpcionEscribirURLEntrada ConfiguracionEscribirURL { get; set; }
        public bool ConfigEscribirURL { get; set; }
        public bool EstablecerParametrosEjecucion { get; set; }
        public ElementoInternetOrigenDatosEjecucion(int CantidadNumeros_Obtenidos_UltimaEjecucion,
            int CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
            int PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
            int PosicionFinalNumeros_Obtenidos_UltimaEjecucion) : base(CantidadNumeros_Obtenidos_UltimaEjecucion, 
                CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
                PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
                PosicionFinalNumeros_Obtenidos_UltimaEjecucion)
        {
            Busquedas = new List<BusquedaArchivoEjecucion>();
        }

        public string ObtenerURLEjecucion_Entrada(EjecucionCalculo ejecucion, ElementoEjecucionCalculo itemCalculo, ElementoEntradaEjecucion entrada)
        {
            string url = string.Empty;

            switch (ConfiguracionEscribirURL)
            {
                case OpcionEscribirURLEntrada.UtilizarURLIndicada:
                    url = URL;
                    break;

                case OpcionEscribirURLEntrada.EscribirURLEjecucion:
                    EscribirURLEntrada escribir = new EscribirURLEntrada();
                    escribir.descripcionEntrada.Text = entrada.Nombre;
                    escribir.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                    escribir.seleccionarURL.Text = URL;

                    bool selecciona = (bool)escribir.ShowDialog();
                    if (selecciona == true)
                    {
                        url = escribir.TextoSeleccionado;

                        if (escribir.Pausado)
                        {
                            ejecucion.Pausar();
                        }
                    }
                    else if (selecciona == false)
                    {
                        ejecucion.Detener();
                        return null;
                    }

                    break;

                case OpcionEscribirURLEntrada.ElegirEscribirURLEjecucionPorEntrada:

                    if (ConfigEscribirURL)
                    {
                        escribir = new EscribirURLEntrada();
                        escribir.descripcionEntrada.Text = entrada.Nombre;
                        escribir.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                        escribir.seleccionarURL.Text = URL;

                        selecciona = (bool)escribir.ShowDialog();
                        if (selecciona == true)
                        {
                            url = escribir.TextoSeleccionado;

                            if (escribir.Pausado)
                            {
                                ejecucion.Pausar();
                            }
                        }
                        else if (selecciona == false)
                        {
                            ejecucion.Detener();
                            return null;
                        }
                    }
                    else
                    {
                        url = URL;
                    }

                    break;
            }

            return url;
        }

        public override BusquedaArchivoEjecucion ObtenerBusquedaRelacionadaCondicion(BusquedaTextoArchivo busqueda)
        {
            List<BusquedaArchivoEjecucion> listaBusquedas = new List<BusquedaArchivoEjecucion>();
            foreach (var itemBusqueda in Busquedas)
            {
                if (itemBusqueda.EsConjuntoBusquedas)
                {
                    foreach (var itemBusquedaConjunto in itemBusqueda.ConjuntoBusquedas)
                    {
                        listaBusquedas.Add(itemBusquedaConjunto);
                    }
                }
                listaBusquedas.Add(itemBusqueda);
            }

            BusquedaArchivoEjecucion busquedaEncontrada = (from BusquedaArchivoEjecucion B in listaBusquedas where B.BusquedaRelacionada_Diseño == busqueda select B).FirstOrDefault();
            return busquedaEncontrada;
        }
    }
}
