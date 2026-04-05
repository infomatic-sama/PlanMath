using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessCalc.Entidades.Entradas
{
    public class ListaCantidadesDigitadas
    {
        public List<string> NumerosDigitados {  get; set; }

        public ListaCantidadesDigitadas() 
        {
            NumerosDigitados = new List<string>();
        }
        
        public void CargarCantidadesDigitadas(string rutaArchivoCalculo, string IDArchivoCalculo, string IDEntrada)
        {
            if(!string.IsNullOrEmpty(rutaArchivoCalculo))
            { 
                XmlReader leer = XmlReader.Create(App.RutaArchivo_CantidadesDigitadas);
                DataContractSerializer objeto = new DataContractSerializer(typeof(Registros_ListasCantidadesDigitadas), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                Registros_ListasCantidadesDigitadas registros = (Registros_ListasCantidadesDigitadas)objeto.ReadObject(leer);
                leer.Close();

                var registro = registros.Registros.Where(i => i.RutaArchivoCalculo == rutaArchivoCalculo & i.IDArchivoCalculo == IDArchivoCalculo).FirstOrDefault();

                if(registro != null)
                {
                    var numerosDigitados = registro.RegistrosEntradas.Where(i => i.IDEntrada == IDEntrada).FirstOrDefault();

                    if(numerosDigitados != null)
                    {
                        NumerosDigitados = numerosDigitados.NumerosDigitados;
                    }
                }
            }
        }

        public void GuardarCantidadesDigitadas(string rutaArchivoCalculo, string IDArchivoCalculo, string IDEntrada)
        {
            if (!string.IsNullOrEmpty(rutaArchivoCalculo))
            {
                XmlReader lee = XmlReader.Create(App.RutaArchivo_CantidadesDigitadas);
                DataContractSerializer objeto = new DataContractSerializer(typeof(Registros_ListasCantidadesDigitadas), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                Registros_ListasCantidadesDigitadas registros = (Registros_ListasCantidadesDigitadas)objeto.ReadObject(lee);
                lee.Close();

                var registro = registros.Registros.Where(i => i.RutaArchivoCalculo == rutaArchivoCalculo & i.IDArchivoCalculo == IDArchivoCalculo).FirstOrDefault();

                if (registro != null)
                {
                    var numerosDigitados = registro.RegistrosEntradas.Where(i => i.IDEntrada == IDEntrada).FirstOrDefault();

                    if (numerosDigitados != null)
                    {
                        numerosDigitados.NumerosDigitados = NumerosDigitados;
                    }
                    else
                    {
                        registro.RegistrosEntradas.Add(new RegistroEntrada_ListaCantidadesDigitadas()
                        {
                            IDEntrada = IDEntrada,
                            NumerosDigitados = NumerosDigitados
                        });
                    }
                }
                else
                {
                    registros.Registros.Add(new Registro_ListaCantidadesDigitadas()
                    {
                        IDArchivoCalculo = IDArchivoCalculo,
                        RutaArchivoCalculo = rutaArchivoCalculo,
                        RegistrosEntradas = new List<RegistroEntrada_ListaCantidadesDigitadas> { new RegistroEntrada_ListaCantidadesDigitadas()
                        {
                            IDEntrada = IDEntrada,
                            NumerosDigitados = NumerosDigitados
                        } }
                    });
                }

                XmlWriter guarda = XmlWriter.Create(App.RutaArchivo_CantidadesDigitadas);
                DataContractSerializer objetoGuardar = new DataContractSerializer(typeof(Registros_ListasCantidadesDigitadas), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                objetoGuardar.WriteObject(guarda, registros);
                guarda.Close();
            }
        }

        public void QuitarRegistroCantidadesDigitadas(string rutaArchivoCalculo, string IDArchivoCalculo, string IDEntrada)
        {            
            XmlReader lee = XmlReader.Create(App.RutaArchivo_CantidadesDigitadas);
            DataContractSerializer objeto = new DataContractSerializer(typeof(Registros_ListasCantidadesDigitadas), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            Registros_ListasCantidadesDigitadas registros = (Registros_ListasCantidadesDigitadas)objeto.ReadObject(lee);
            lee.Close();

            var registro = registros.Registros.Where(i => i.RutaArchivoCalculo == rutaArchivoCalculo & i.IDArchivoCalculo == IDArchivoCalculo).FirstOrDefault();

            if (registro != null)
            {
                var numerosDigitados = registro.RegistrosEntradas.Where(i => i.IDEntrada == IDEntrada).FirstOrDefault();

                if (numerosDigitados != null)
                {
                    registro.RegistrosEntradas.Remove(numerosDigitados);
                }
            }

            XmlWriter guarda = XmlWriter.Create(App.RutaArchivo_CantidadesDigitadas);
            DataContractSerializer objetoGuardar = new DataContractSerializer(typeof(Registros_ListasCantidadesDigitadas), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objetoGuardar.WriteObject(guarda, registros);
            guarda.Close();            
        }

        public void AgregarCantidadDigitada(string numero)
        {
            if(!string.IsNullOrEmpty(numero) && 
                !NumerosDigitados.Contains(numero))
            {
                NumerosDigitados.Add(numero);
            }
        }

        public void QuitarCantidadDigitada(string numero)
        {
            if (!string.IsNullOrEmpty(numero) &&
                NumerosDigitados.Contains(numero))
            {
                NumerosDigitados.Remove(numero);
            }
        }

        public void VaciarCantidadesDigitadas()
        {
            NumerosDigitados.Clear();
        }
    }

    public class Registros_ListasCantidadesDigitadas
    {
        public List<Registro_ListaCantidadesDigitadas> Registros {  get; set; }
        public Registros_ListasCantidadesDigitadas()
        {
            Registros = new List<Registro_ListaCantidadesDigitadas>();
        }
    }

    public class Registro_ListaCantidadesDigitadas
    {
        public string RutaArchivoCalculo { get; set; }
        public string IDArchivoCalculo { get; set; }
        public List<RegistroEntrada_ListaCantidadesDigitadas> RegistrosEntradas { get; set; }
        public Registro_ListaCantidadesDigitadas()
        {
            RegistrosEntradas = new List<RegistroEntrada_ListaCantidadesDigitadas>();
        }
    }

    public class RegistroEntrada_ListaCantidadesDigitadas
    {
        public string IDEntrada { get; set; }
        public List<string> NumerosDigitados { get; set; }
        public RegistroEntrada_ListaCantidadesDigitadas()
        {
            NumerosDigitados = new List<string>();
        }
    }
}
