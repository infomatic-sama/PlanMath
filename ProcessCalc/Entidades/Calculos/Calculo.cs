using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Controles;
using ProcessCalc.Entidades.Ejecuciones.ToolTips;
using ProcessCalc.Entidades.OrigenesDatos;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Xml;
using ProcessCalc.Entidades.Operaciones;

namespace ProcessCalc.Entidades
{
    [System.Xml.Serialization.XmlInclude(typeof(Rect))]
    [System.Xml.Serialization.XmlInclude(typeof(ParametroURL))]
    [System.Xml.Serialization.XmlInclude(typeof(DiseñoListaCadenasTexto))]
    [KnownType(typeof(DiseñoListaCadenasTexto))]
    public class Calculo
    {        
        public string ID { get; set; }
        public string Descripcion { get; set; }
        public string Entradas { get; set; }
        public string Resultados { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public List<DiseñoCalculo> Calculos { get; set; }
        public List<Resultado> ListaResultados { get; set; }
        public string VersionAplicacion { get; set; }
        [AtributoNoComparar]
        public double Ancho { get; set; }
        [AtributoNoComparar]
        public double Alto { get; set; }
        [IgnoreDataMember]
        public DiseñoCalculo SubCalculoSeleccionado { get; set; }
        [IgnoreDataMember]
        public DiseñoCalculo SubCalculoSeleccionado_Operaciones { get; set; }
        [IgnoreDataMember]
        public DiseñoCalculo SubCalculoSeleccionado_Entradas { get; set; }
        [IgnoreDataMember]
        public DiseñoCalculo SubCalculoSeleccionado_VistaCalculo { get; set; }
        [IgnoreDataMember]
        public DiseñoTextosInformacion DefinicionSeleccionada { get; set; }
        [IgnoreDataMember]
        public DiseñoListaCadenasTexto DefinicionListaCadenasTextoSeleccionada { get; set; }
        [IgnoreDataMember]
        public List<DiseñoTextosInformacion> ElementosSeleccionados_TextosInformacion { get; set; }
        [IgnoreDataMember]
        public DiseñoCalculo SubCalculoSeleccionado_TextosInformacion { get; set; }
        [AtributoNoComparar]
        public List<DiseñoOperacion> Notas { get; set; }
        public DiseñoCalculoTextosInformacion TextosInformacion { get; set; }
        [IgnoreDataMember]
        public int TabSeleccionada_TextosInformacion { get; set; }
        [AtributoNoComparar]
        public bool MostrarInformacionElementos_InformeResultados { get; set; }
        [AtributoNoComparar]
        public int CantidadDecimalesCantidades { get; set; }
        public bool NoMostrarCeros_InformeEjecucionResultados { get; set; }
        public bool NoMostrarCantidadesSinTextos_InformeEjecucionResultados { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasOperandos_ArchivoExterno { get; set; }
        [IgnoreDataMember]
        public bool HayCambios { get; set; }
        public bool ModoSubCalculo {  get; set; }
        public bool ModoSubCalculo_Simple { get; set; }
        public Calculo()
        {
            Calculos = new List<DiseñoCalculo>();
            Calculos.Add(new DiseñoCalculo("Variables o vectores generales", string.Empty, true));
            Calculos.Last().EsEntradasArchivo = true;
            //Calculos.Add(new DiseñoCalculo("Cálculo 1", string.Empty));
            //Calculos.Last().ElementosAnteriores.Add(Calculos.First());
            //Calculos.First().ElementosPosteriores.Add(Calculos.Last());
            //Calculos.First().ElementosContenedoresOperacion.Add(Calculos.Last());
            ListaResultados = new List<Resultado>();
            Ancho = double.NaN;
            Alto = double.NaN;
            Notas = new List<DiseñoOperacion>();
            TextosInformacion = new DiseñoCalculoTextosInformacion();
            TabSeleccionada_TextosInformacion = -1;
            CantidadDecimalesCantidades = 2;
            EntradasOperandos_ArchivoExterno = new List<AsociacionEntradaOperando_ArchivoExterno>();
        }

        public void CrearCalculoEntradas()
        {
            if (Calculos.Any())
            {
                Calculos.Insert(0, new DiseñoCalculo("Variables o vectores generales", string.Empty, true));
                Calculos.ElementAt(1).ElementosAnteriores.Add(Calculos.First());
                Calculos.First().ElementosPosteriores.Add(Calculos.ElementAt(1));
                Calculos.First().ElementosContenedoresOperacion.Add(Calculos.ElementAt(1));
            }
            else
                Calculos.Add(new DiseñoCalculo("Variables o vectores generales", string.Empty, true));

            Calculos.First().EsEntradasArchivo = true;            
        }

        public void AgregarCalculoInicial()
        {
            Calculos.Add(new DiseñoCalculo("Cálculo 1", string.Empty));
            Calculos.Last().ElementosAnteriores.Add(Calculos.First());
            Calculos.First().ElementosPosteriores.Add(Calculos.Last());
            Calculos.First().ElementosContenedoresOperacion.Add(Calculos.Last());
        }

        public Calculo ReplicarObjeto()
        {
            Calculo calculo = null;

            MemoryStream flujo = new MemoryStream();

            DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(flujo, this);

            flujo.Position = 0;
            calculo = (Calculo)objeto.ReadObject(flujo);

            return calculo;
        }

        public static bool VerificarIgualdad(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;
            else
            {
                for (int indice = 0; indice <= array1.Length - 1; indice++)
                {
                    if (array1[indice] != array2[indice])
                        return false;
                }
            }

            return true;
        }

        public DiseñoCalculo ObtenerCalculoEntradas()
        {
            return (from DiseñoCalculo E in Calculos where E.EsEntradasArchivo select E).FirstOrDefault();
        }

        public bool VerificarSiElementoEs_EntradaGeneral(Entrada elemento)
        {
            DiseñoCalculo calculoEntradas = ObtenerCalculoEntradas();

            if (calculoEntradas != null)
                return calculoEntradas.ListaEntradas.Any(i => i == elemento);
            else
                return false;
        }

        public List<DiseñoOperacion> ObtenerTodosAgrupadores()
        {
            List<DiseñoOperacion> agrupadores = new List<DiseñoOperacion>();

            foreach (var itemCalculo in Calculos)
                agrupadores.AddRange(itemCalculo.ElementosOperaciones.Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones));

            return agrupadores;
        }        

        public List<Entrada> ObtenerTodasEntradas()
        {
            List<Entrada> listaEntradas = new List<Entrada>();

            foreach(var itemCalculo in Calculos.Where(i => !i.EsEntradasArchivo))
            {
                listaEntradas.AddRange(itemCalculo.ListaEntradas.Where(i => i.Tipo != TipoEntrada.TextosInformacion).ToList());
            }

            return listaEntradas;
        }

        public void ProcesarIDs_Elementos(bool idRepetido)
        {
            if (string.IsNullOrEmpty(ID) || !ID.Contains("-") ||
                idRepetido)
                ID = App.GenerarID_Elemento();

            foreach (var itemCalculo in Calculos)
            {
                foreach (var itemEntrada in itemCalculo.ListaEntradas)
                {
                    if (string.IsNullOrEmpty(itemEntrada.ID) || !itemEntrada.ID.Contains("-") ||
                idRepetido)
                        itemEntrada.ID = App.GenerarID_Elemento();
                }

                foreach (var itemElemento in itemCalculo.ElementosOperaciones)
                {
                    if (string.IsNullOrEmpty(itemCalculo.ID) || !itemCalculo.ID.Contains("-") ||
                idRepetido)
                        itemElemento.ID = App.GenerarID_Elemento();

                    foreach (var itemSubElemento in itemElemento.ElementosDiseñoOperacion)
                    {
                        if (string.IsNullOrEmpty(itemSubElemento.ID) || !itemSubElemento.ID.Contains("-") ||
                idRepetido)
                            itemSubElemento.ID = App.GenerarID_Elemento();
                    }
                }

                if (string.IsNullOrEmpty(itemCalculo.ID) || !itemCalculo.ID.Contains("-") ||
                idRepetido)
                    itemCalculo.ID = App.GenerarID_Elemento();
            }
        }

        public void AgregarConfiguracionEntrada_Ejecucion(Entrada entrada)
        {
            //if(entrada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeDigita |
            //    entrada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeDigita |
            //    entrada.TipoOpcionTextosInformacion == TipoOpcionTextosInformacionEntrada.SeDigita)
            //{
                var entradaEncontrada = EntradasOperandos_ArchivoExterno.FirstOrDefault(i => i.Entrada == entrada);
                if(entradaEncontrada == null)
                {
                    EntradasOperandos_ArchivoExterno.Add(new AsociacionEntradaOperando_ArchivoExterno(TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionArchivo)
                    {
                        Entrada = entrada,
                        Configuracion = new ConfigTraspasoCantidades_Entrada_ArchivoExterno(TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionArchivo)
                    });
                }
            //}
        }

        public void QuitarConfiguracionEntrada_Ejecucion(Entrada entrada)
        {
            //if (entrada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeDigita |
            //    entrada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeDigita)
            //{
                var entradaEncontrada = EntradasOperandos_ArchivoExterno.FirstOrDefault(i => i.Entrada == entrada);
                if (entradaEncontrada != null)
                {
                    EntradasOperandos_ArchivoExterno.Remove(entradaEncontrada);
                }
            //}
        }

        public bool VerificarConfiguracionEntrada_Ejecucion(Entrada entrada)
        {
            var entradaEncontrada = EntradasOperandos_ArchivoExterno.FirstOrDefault(i => i.Entrada == entrada);

            if (entradaEncontrada != null)
            {
                return true;
            }
            else
                return false;            
        }

        public DiseñoCalculo ObtenerCalculoDiseño(string ID)
        {
            return Calculos.FirstOrDefault(i => i.ID == ID);
        }

        public DiseñoOperacion ObtenerElementoDiseño(string ID)
        {
            foreach (var itemCalculo in Calculos)
            {
                var itemElemento = itemCalculo.ElementosOperaciones.FirstOrDefault(i => i.ID == ID);
                if (itemElemento != null)
                    return itemElemento;
            }

            return null;
        }

        public Entrada ObtenerEntradaDiseño(string ID)
        {
            foreach (var itemCalculo in Calculos)
            {
                var itemElemento = itemCalculo.ListaEntradas.FirstOrDefault(i => i.ID == ID);
                if (itemElemento != null)
                    return itemElemento;
            }

            return null;
        }
        public DiseñoElementoOperacion ObtenerSubElementoDiseño(string ID)
        {
            foreach (var itemCalculo in Calculos)
            {
                foreach (var itemElemento in itemCalculo.ElementosOperaciones)
                {
                    var itemSubElemento = itemElemento.ElementosDiseñoOperacion.FirstOrDefault(i => i.ID == ID);
                    if (itemSubElemento != null)
                        return itemSubElemento;
                }
            }

            return null;
        }

        public void EstablecerEntradaProcesada_TextosInformacion(Entrada entrada, string IDEntrada)
        {
            if (TextosInformacion != null)
            {
                foreach (var item in TextosInformacion.ElementosTextosInformacion)
                {
                    if (item.EntradaRelacionada != null &&
                        item.EntradaRelacionada.ID == IDEntrada)
                        item.EntradaRelacionada = entrada;
                }
            }
        }
        public bool VerificarMostrarElemento_InformeEjecucionResultados(EntidadNumero numero)
        {
            return VerificarMostrarCantidad_InformeEjecucionResultados(numero.Numero, numero.Textos);
        }
        private bool VerificarMostrarCantidad_InformeEjecucionResultados(double numero, List<string> Textos)
        {
            if (NoMostrarCantidadesSinTextos_InformeEjecucionResultados)
            {
                if (!Textos.Any(i => !string.IsNullOrEmpty(i)))
                    return false;
            }

            if (NoMostrarCeros_InformeEjecucionResultados)
            {
                if (numero == 0) return false;
            }

            return true;
        }
    }
}