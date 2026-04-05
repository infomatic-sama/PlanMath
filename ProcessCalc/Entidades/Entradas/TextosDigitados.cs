using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessCalc.Entidades.Entradas
{
    public class ListaTextosDigitados
    {
        public int IndiceAsociado {  get; set; }
        public int IndiceNumero { get; set; }
        public List<string> TextosDigitados { get; set; }

        public ListaTextosDigitados()
        {
            TextosDigitados = new List<string>();
        }

        public static List<ListaTextosDigitados> CargarListasTextosDigitadas(string rutaArchivoCalculo, string IDArchivoCalculo, string IDEntrada)
        {
            if (!string.IsNullOrEmpty(rutaArchivoCalculo))
            {
                XmlReader leer = XmlReader.Create(App.RutaArchivo_TextosDigitados);
                DataContractSerializer objeto = new DataContractSerializer(typeof(Registros_ListasTextosDigitados), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                Registros_ListasTextosDigitados registros = (Registros_ListasTextosDigitados)objeto.ReadObject(leer);
                leer.Close();

                var registro = registros.Registros.Where(i => i.RutaArchivoCalculo == rutaArchivoCalculo & i.IDArchivoCalculo == IDArchivoCalculo).FirstOrDefault();

                if (registro != null)
                {
                    var numerosDigitados = registro.RegistrosEntradas.Where(i => i.IDEntrada == IDEntrada).FirstOrDefault();

                    if (numerosDigitados != null)
                    {
                        return numerosDigitados.TextosDigitados;
                    }
                }
            }

            return new List<ListaTextosDigitados>();
        }

        public static void GuardarTextosDigitados(string rutaArchivoCalculo, string IDArchivoCalculo, string IDEntrada, List<ListaTextosDigitados> listaTextos)
        {
            if (!string.IsNullOrEmpty(rutaArchivoCalculo))
            {
                XmlReader lee = XmlReader.Create(App.RutaArchivo_TextosDigitados);
                DataContractSerializer objeto = new DataContractSerializer(typeof(Registros_ListasTextosDigitados), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                Registros_ListasTextosDigitados registros = (Registros_ListasTextosDigitados)objeto.ReadObject(lee);
                lee.Close();

                var registro = registros.Registros.Where(i => i.RutaArchivoCalculo == rutaArchivoCalculo & i.IDArchivoCalculo == IDArchivoCalculo).FirstOrDefault();

                if (registro != null)
                {
                    var textosDigitados = registro.RegistrosEntradas.Where(i => i.IDEntrada == IDEntrada).FirstOrDefault();

                    if (textosDigitados != null)
                    {
                        textosDigitados.TextosDigitados = listaTextos;
                    }
                    else
                    {
                        registro.RegistrosEntradas.Add(new RegistroEntrada_ListaTextosDigitados()
                        {
                            IDEntrada = IDEntrada,
                            TextosDigitados = listaTextos
                        });
                    }
                }
                else
                {
                    registros.Registros.Add(new Registro_ListaTextosDigitados()
                    {
                        IDArchivoCalculo = IDArchivoCalculo,
                        RutaArchivoCalculo = rutaArchivoCalculo,
                        RegistrosEntradas = new List<RegistroEntrada_ListaTextosDigitados> { new RegistroEntrada_ListaTextosDigitados()
                        {
                            IDEntrada = IDEntrada,
                            TextosDigitados = listaTextos
                        } }
                    });
                }

                XmlWriter guarda = XmlWriter.Create(App.RutaArchivo_TextosDigitados);
                DataContractSerializer objetoGuardar = new DataContractSerializer(typeof(Registros_ListasTextosDigitados), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                objetoGuardar.WriteObject(guarda, registros);
                guarda.Close();
            }
        }
    }

    public class Registros_ListasTextosDigitados
    {
        public List<Registro_ListaTextosDigitados> Registros { get; set; }
        public Registros_ListasTextosDigitados()
        {
            Registros = new List<Registro_ListaTextosDigitados>();
        }
    }

    public class Registro_ListaTextosDigitados
    {
        public string RutaArchivoCalculo { get; set; }
        public string IDArchivoCalculo { get; set; }
        public List<RegistroEntrada_ListaTextosDigitados> RegistrosEntradas { get; set; }
        public Registro_ListaTextosDigitados()
        {
            RegistrosEntradas = new List<RegistroEntrada_ListaTextosDigitados>();
        }
    }

    public class RegistroEntrada_ListaTextosDigitados
    {
        public string IDEntrada { get; set; }
        public List<ListaTextosDigitados> TextosDigitados { get; set; }
        public RegistroEntrada_ListaTextosDigitados()
        {
            TextosDigitados = new List<ListaTextosDigitados>();
        }
    }
}
