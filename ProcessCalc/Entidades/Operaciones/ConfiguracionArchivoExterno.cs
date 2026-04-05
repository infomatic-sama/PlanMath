using ProcessCalc.Entidades.Entradas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProcessCalc.Entidades.Operaciones
{
    public class ConfiguracionArchivoExterno
    {
        public ConfiguracionArchivoEntrada Archivo { get; set; }
        public bool EjecutarArchivoSin_InfoVisual { get; set; }
        public bool EjecutarArchivo_MismaEjecucion { get; set; }
        public CredencialesFTP CredencialesFTP { get; set; }
        public ConfiguracionArchivoExterno()
        {
            this.Archivo = new ConfiguracionArchivoEntrada()
            {
                ConfiguracionSeleccionArchivo = OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado,
                ConfiguracionSeleccionCarpeta = OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion,
                EsperarArchivos = false,
                TipoArchivo = TipoArchivo.EquipoLocal
            };

            EjecutarArchivo_MismaEjecucion = true;
        }
        public List<Entrada> ObtenerEntradas_ArchivoCalculo()
        {
            string RutaArchivo = Archivo.RutaArchivoEntrada;

            if (Archivo != null &&
                Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion)
            {
                RutaArchivo = Archivo.RutaArchivoPlantilla;
            }

            if(Archivo != null &&
                !string.IsNullOrEmpty(RutaArchivo) &&
                File.Exists(RutaArchivo))
            {
                try
                {
                    XmlReader guarda = XmlReader.Create(RutaArchivo);
                    DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                    Calculo calculo = (Calculo)objeto.ReadObject(guarda);
                    guarda.Close();

                    return calculo.ObtenerTodasEntradas();

                }
                catch (Exception e) 
                {
                    MessageBox.Show("Error al obtener las entradas del archivo '" + RutaArchivo + "'. Error: " + e.Message);
                    return new List<Entrada>();
                }
            }
            else 
                return new List<Entrada>();
        }

        public List<Resultado> ObtenerResultados_ArchivoCalculo()
        {
            string RutaArchivo = Archivo.RutaArchivoEntrada;

            if (Archivo != null &&
                Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion)
            {
                RutaArchivo = Archivo.RutaArchivoPlantilla;
            }

            if (Archivo != null &&
                !string.IsNullOrEmpty(RutaArchivo) &&
                File.Exists(RutaArchivo))
            {
                try
                {
                    XmlReader guarda = XmlReader.Create(RutaArchivo);
                    DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                    Calculo calculo = (Calculo)objeto.ReadObject(guarda);
                    guarda.Close();

                    return calculo.ListaResultados.ToList();

                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al obtener los resultados del archivo '" +  RutaArchivo + "'. Error: " + e.Message);
                    return new List<Resultado>();
                }
            }
            else
                return new List<Resultado>();
        }

        public ConfiguracionArchivoExterno CopiarObjeto()
        {
            ConfiguracionArchivoExterno config = new ConfiguracionArchivoExterno();

            config.Archivo = Archivo.CopiarObjeto();
            config.EjecutarArchivoSin_InfoVisual = EjecutarArchivoSin_InfoVisual;
            config.EjecutarArchivo_MismaEjecucion = EjecutarArchivo_MismaEjecucion;

            return config;
        }
    }
}
