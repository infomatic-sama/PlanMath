using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlanMath_para_Excel.Entradas;

namespace ProcessCalc.Entidades
{
    public class ElementoArchivoExcelOrigenDatosEjecucion : ElementoOrigenDatosEjecucion
    {
        public string RutaArchivo { get; set; }
        public List<Entrada_Desde_Excel> ParametrosExcel { get; set; }
        public OpcionSeleccionarArchivoEntrada ConfiguracionSeleccionArchivo { get; set; }
        public OpcionCarpetaEntrada ConfiguracionSeleccionCarpeta { get; set; }
        public bool ConfigSeleccionarArchivo { get; set; }
        public bool UsarURLLibro { get; set; }
        public string ObtenerRutaArchivoEjecucion_Entrada(string rutaArchivoCalculo, EjecucionCalculo ejecucion, ElementoEjecucionCalculo itemCalculo, ElementoEntradaEjecucion entrada)
        {
            string ruta = string.Empty;

            switch (ConfiguracionSeleccionCarpeta)
            {
                case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                    switch (ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            ruta = RutaArchivo;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                            seleccionar.ModoEjecucion = true;
                            seleccionar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                            seleccionar.ArchivoEntrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ListaArchivos.FirstOrDefault();
                            seleccionar.RutaCarpeta = RutaArchivo;
                            seleccionar.descripcionEntrada.Text = entrada.Nombre;
                            seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                            
                            bool selecciona = (bool)seleccionar.ShowDialog();
                            if (selecciona == true)
                            {
                                ruta = ProcesarCarpetasContenedoras(RutaArchivo, seleccionar.TextoSeleccionado);

                                if (seleccionar.Pausado)
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

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                            if (ConfigSeleccionarArchivo)
                            {
                                seleccionar = new SeleccionarArchivoEntrada();
                                seleccionar.ModoEjecucion = true;
                                seleccionar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                                seleccionar.RutaCarpeta = RutaArchivo.Remove(RutaArchivo.LastIndexOf("\\"));
                                seleccionar.descripcionEntrada.Text = entrada.Nombre;
                                seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                                
                                selecciona = (bool)seleccionar.ShowDialog();
                                if (selecciona == true)
                                {
                                    ruta = ProcesarCarpetasContenedoras(RutaArchivo.Remove(RutaArchivo.LastIndexOf("\\")), seleccionar.TextoSeleccionado);

                                    if (seleccionar.Pausado)
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
                                ruta = RutaArchivo;
                            }

                            break;
                    }

                    break;

                case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                    switch (ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            ruta = rutaArchivoCalculo + "\\" + RutaArchivo;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:

                            SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                            seleccionar.ModoEjecucion = true;
                            seleccionar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                            seleccionar.ArchivoEntrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ListaArchivos.FirstOrDefault();
                            seleccionar.RutaCarpeta = rutaArchivoCalculo;
                            seleccionar.descripcionEntrada.Text = entrada.Nombre;
                            seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                            
                            bool selecciona = (bool)seleccionar.ShowDialog();
                            if (selecciona == true)
                            {
                                ruta = ProcesarCarpetasContenedoras(rutaArchivoCalculo, seleccionar.TextoSeleccionado);

                                if (seleccionar.Pausado)
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

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                            if (ConfigSeleccionarArchivo)
                            {
                                seleccionar = new SeleccionarArchivoEntrada();
                                seleccionar.ModoEjecucion = true;
                                seleccionar.Entrada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada;
                                seleccionar.RutaCarpeta = rutaArchivoCalculo;
                                seleccionar.descripcionEntrada.Text = entrada.Nombre;
                                seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;
                                
                                selecciona = (bool)seleccionar.ShowDialog();
                                if (selecciona == true)
                                {
                                    ruta = ProcesarCarpetasContenedoras(rutaArchivoCalculo, seleccionar.TextoSeleccionado);

                                    if (seleccionar.Pausado)
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
                                ruta = rutaArchivoCalculo + "\\" + RutaArchivo;
                            }

                            break;
                    }

                    break;
            }

            return ruta;
        }

        private string ProcesarCarpetasContenedoras(string carpeta, string archivo)
        {
            if (archivo.Contains(".../"))
            {
                while (true)
                {
                    int indiceCadenaCarpeta = archivo.LastIndexOf(".../");
                    if (indiceCadenaCarpeta >= 0)
                    {
                        archivo = archivo.Remove(indiceCadenaCarpeta, (".../").Length);

                        int indiceSubCarpeta = carpeta.LastIndexOf("\\");

                        if (indiceSubCarpeta > -1)
                            carpeta = carpeta.Remove(indiceSubCarpeta);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            return carpeta + "\\" + archivo;
        }
        public override BusquedaArchivoEjecucion ObtenerBusquedaRelacionadaCondicion(BusquedaTextoArchivo busqueda)
        {
            return null;
        }

        public ElementoArchivoExcelOrigenDatosEjecucion(int CantidadNumeros_Obtenidos_UltimaEjecucion,
            int CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
            int PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
            int PosicionFinalNumeros_Obtenidos_UltimaEjecucion) : base(CantidadNumeros_Obtenidos_UltimaEjecucion, 
                CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
                PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
                PosicionFinalNumeros_Obtenidos_UltimaEjecucion)
        {
            
        }
    }
}
