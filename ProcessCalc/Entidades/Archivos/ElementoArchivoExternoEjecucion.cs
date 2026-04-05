using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Archivos
{
    public class ElementoArchivoExternoEjecucion
    {
        public OpcionCarpetaEntrada ConfiguracionSeleccionCarpeta { get; set; }
        public OpcionSeleccionarArchivoEntrada ConfiguracionSeleccionArchivo { get; set; }
        public ConfiguracionArchivoEntrada ArchivoEntrada { get; set; }
        public string RutaArchivo { get; set; }
        public List<string> ObtenerRutaArchivoEjecucion_Operador(string rutaArchivoCalculo, 
            EjecucionCalculo ejecucion, ElementoCalculoEjecucion itemCalculo)
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
                            seleccionar.ModoArchivoExterno = true;
                            seleccionar.ModoEjecucion = true;
                            seleccionar.ArchivoEntrada = ArchivoEntrada;
                            seleccionar.RutaCarpeta = RutaArchivo;
                            seleccionar.titulo.Text += "\nEjecución del cálculo " + ejecucion.Calculo.NombreArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                            bool selecciona = (bool)seleccionar.ShowDialog();
                            if (selecciona == true)
                            {
                                ruta = ProcesarCarpetasContenedoras(seleccionar.RutaCarpeta, seleccionar.TextoSeleccionado);

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
                    }

                    break;

                case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                    switch (ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            ruta = RutaArchivo;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:

                            SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                            seleccionar.ModoArchivoExterno = true;
                            seleccionar.ModoEjecucion = true;
                            seleccionar.ArchivoEntrada = ArchivoEntrada;
                            seleccionar.RutaCarpeta = rutaArchivoCalculo;
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
                    }

                    break;
            }

            return ObtenerRutasArchivos_Comodin(ruta);
            
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
                        archivo = archivo.Remove(indiceCadenaCarpeta, ".../".Length);

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

        private List<string> ObtenerRutasArchivos_Comodin(string ruta)
        {
            List<string> rutasArchivos = new List<string>();

            string nombreArchivo = ruta.Substring(ruta.LastIndexOf("\\") + 1);

            if (string.IsNullOrEmpty(nombreArchivo))
            {
                nombreArchivo = Guid.NewGuid().ToString() + ".txt";
                ruta = ruta + nombreArchivo;

                File.Create(ruta).Close();
            }

            nombreArchivo = nombreArchivo.Remove(nombreArchivo.LastIndexOf("."));

            if (nombreArchivo.Contains("*"))
            {
                string carpeta = ruta.Remove(ruta.LastIndexOf("\\"));
                string[] archivos = Directory.GetFiles(carpeta);

                string[] partesNombre = nombreArchivo.Split("*");
                string parteNombre = nombreArchivo.Replace("*", string.Empty);

                foreach (var itemRuta in archivos)
                {
                    string nombreArchivoItem = itemRuta.Substring(itemRuta.LastIndexOf("\\") + 1);
                    nombreArchivoItem = nombreArchivoItem.Remove(nombreArchivoItem.LastIndexOf("."));

                    if (nombreArchivo.StartsWith("*"))
                    {
                        if (nombreArchivoItem.EndsWith(parteNombre))
                        {
                            rutasArchivos.Add(itemRuta);
                        }
                    }
                    else if (nombreArchivo.EndsWith("*"))
                    {
                        if (nombreArchivoItem.StartsWith(parteNombre))
                        {
                            rutasArchivos.Add(itemRuta);
                        }
                    }
                    else if (nombreArchivo.Contains("*"))
                    {
                        if (nombreArchivoItem.StartsWith(partesNombre[0]) &
                            nombreArchivoItem.EndsWith(partesNombre[1]))
                        {
                            rutasArchivos.Add(itemRuta);
                        }
                    }
                }
            }
            else
            {
                rutasArchivos.Add(ruta);
            }

            if (ArchivoEntrada.EsperarArchivos)
            {
                EsperandoArchivos(rutasArchivos);
            }

            return rutasArchivos;
        }

        private void EsperandoArchivos(List<string> archivos)
        {
            bool ExistenTodosArchivos = VerificarExistenciaArchivos(archivos);

            if (!ExistenTodosArchivos)
            {
                int segundos = 0;

                if (ArchivoEntrada.TipoTiempoEspera == TipoTiempoEspera.Segundos)
                {
                    segundos = (int)ArchivoEntrada.TiempoEspera;
                }
                else if (ArchivoEntrada.TipoTiempoEspera == TipoTiempoEspera.Minutos)
                {
                    segundos = (int)(ArchivoEntrada.TiempoEspera * 60);
                }
                else if (ArchivoEntrada.TipoTiempoEspera == TipoTiempoEspera.Horas)
                {
                    segundos = (int)(ArchivoEntrada.TiempoEspera * 3600);
                }
                else if (ArchivoEntrada.TipoTiempoEspera == TipoTiempoEspera.Dias)
                {
                    segundos = (int)(ArchivoEntrada.TiempoEspera * 86400);
                }

                int tiempo = 0;

                while (!ExistenTodosArchivos && tiempo < segundos)
                {
                    Thread.Sleep(1000);
                    tiempo++;
                    ExistenTodosArchivos = VerificarExistenciaArchivos(archivos);
                }
            }
        }

        private bool VerificarExistenciaArchivos(List<string> archivos)
        {
            bool Existen = true;
            foreach (var archivo in archivos)
            {
                if (!File.Exists(archivo))
                    Existen = false;
            }

            return Existen;
        }

        public string ObtenerURLFTP_Ejecucion_Entrada(EjecucionCalculo ejecucion, ElementoEjecucionCalculo itemCalculo)
        {
            string url = string.Empty;

            switch (ConfiguracionSeleccionArchivo)
            {
                case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                    url = RutaArchivo;
                    break;

                case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                    EscribirURLEntrada escribir = new EscribirURLEntrada();
                    escribir.textoURL.Text = "URL FTP";
                    escribir.seleccionarURL.Text = RutaArchivo;
                    escribir.descripcionEntrada.Text = string.Empty;
                    escribir.titulo.Text += "\nEjecución del archivo de cálculo externo " + RutaArchivo + " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                    bool selecciona = (bool)escribir.ShowDialog();
                    if (selecciona == true)
                    {
                        url = escribir.TextoSeleccionado;
                    }
                    else if (selecciona == false)
                    {
                        if (escribir.Pausado)
                        {
                            ejecucion.Pausar();
                        }
                        else
                        {
                            ejecucion.Detener();
                            return null;
                        }
                    }

                    break;
            }

            if (url.Length >= 7 && !url.Substring(0, 6).ToLower().Equals("ftp://") &&
                !url.Substring(0, 7).ToLower().Equals("ftps://"))
                url = "ftp://" + url;
            else if (url.Length >= 7 && !url.Substring(0, 7).ToLower().Equals("ftps://") &&
                !url.Substring(0, 6).ToLower().Equals("ftp://"))
                url = "ftps://" + url;

            return url;
        }

    }
}
