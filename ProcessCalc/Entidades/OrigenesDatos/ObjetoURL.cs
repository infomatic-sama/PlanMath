using Newtonsoft.Json;
using ProcessCalc.Controles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades.OrigenesDatos
{
    public class ObjetoURL
    {
        public string URL { get; set; }
        public HttpWebResponse RespuestaWeb { get; set; }
        public StreamReader FlujoContenidoTexto { get; set; }
        public string Contenido { get; set; }
        public List<ParametroURL> ParametrosSolicitudWeb { get; set; }
        public List<ParametroURL> HeadersSolicitudWeb { get; set; }
        public List<ParametroURL> ParametrosURL { get; set; }
        public ObjetoURL(string url, List<ParametroURL> parametros = null, List<ParametroURL> headersSolicitudWeb = null)
        {
            URL = url;
            HeadersSolicitudWeb = headersSolicitudWeb;

            ParametrosSolicitudWeb = parametros.Where(i => i.ParametrosEnBody).ToList();
            ParametrosURL = parametros.Where(i => i.ParametrosEnUrl).ToList();
        }

        public string ObtenerTexto()
        {
            string contenido = string.Empty;
            try
            {
                //SolicitudWeb.Method = "POST";
                //SolicitudWeb = new HttpClient();
                //SolicitudWeb. = true;
                //WebClient cliente = new WebClient();
                byte[] bytes = null;
                StringContent contenidoParametros = null;
                string url = URL;
                string urlParametros = string.Empty;

                if (ParametrosSolicitudWeb != null && ParametrosSolicitudWeb.Count > 0)
                {
                    var parametros = ObtenerParametrosObjeto(ParametrosSolicitudWeb);
                    string strContenidoParametros = GenerarEstructuraJson(parametros);
                    
                    contenidoParametros = new StringContent(strContenidoParametros, new MediaTypeHeaderValue("application/json"));
                }

                if (ParametrosURL != null && ParametrosURL.Count > 0)
                {
                    var parametros = ObtenerParametrosObjeto(ParametrosURL);
                    string strContenidoParametros = string.Empty;

                    //var parametroNivelCero = ParametrosURL.Where(i => i.Nivel == 0).ToList();
                    //int indiceParametros = 0;

                    for (int indice = 0, indiceParametros = 0; indice < parametros.Count ; indice++, indiceParametros++)
                    {
                        string valor = string.Empty;

                        if (indiceParametros < ParametrosURL.Count - 1)
                        {
                            if (ParametrosURL[indiceParametros + 1].Nivel > ParametrosURL[indiceParametros].Nivel)
                            {
                                GenerarEstructuraJson_Item(parametros.ElementAt(indice), ref indice, ref valor, null, ",", true);

                                //indiceParametros = indice;
                                //continue;
                            }
                        }

                        if (string.IsNullOrEmpty(valor))
                            valor = parametros.ElementAt(indice).Value.ToString();

                        indiceParametros = indice;

                        strContenidoParametros += parametros.ElementAt(indice).Key + "=" + valor;

                        if (indice < parametros.Count - 1)
                            strContenidoParametros = strContenidoParametros + "&";
                    }

                    urlParametros = "?" + strContenidoParametros;

                    //foreach (var param in ParametrosURL)
                    //{
                    //    if (param == ParametrosURL.LastOrDefault())
                    //        urlParametros += param.Nombre + "=" + param.Valor;
                    //    else
                    //        urlParametros += param.Nombre + "=" + param.Valor + "&";
                    //}
                }

                if (URL.Split('?').Length > 1)
                {
                    if (!string.IsNullOrEmpty(urlParametros))
                        url = URL.Split('?')[0] + urlParametros;
                    else
                        url = URL;
                }
                else
                {
                    if (!string.IsNullOrEmpty(urlParametros))
                        url = URL + urlParametros;
                    else
                        url = URL;
                }

                HttpClient solicitador = new HttpClient();

                foreach (var header in HeadersSolicitudWeb)
                {
                    solicitador.DefaultRequestHeaders.Add(header.Nombre, header.Valor);
                }

                var respuesta = solicitador.PostAsync(url, contenidoParametros);

                //bytes = SolicitudWeb.UploadValues(URL, "POST", parametros);
                try
                {
                    Task<byte[]> obtenerDatos = respuesta?.Result?.Content.ReadAsByteArrayAsync();

                    if (respuesta.Result.ReasonPhrase == "Method Not Allowed")
                    {
                        var respuesta2 = solicitador.GetAsync(url);
                        obtenerDatos = respuesta2.Result.Content.ReadAsByteArrayAsync();
                    }

                    bytes = obtenerDatos.Result;
                }
                catch(HttpRequestException e)
                {
                    throw e;
                }

                //RespuestaWeb = (HttpWebResponse)SolicitudWeb.GetResponse();

                //if (RespuestaWeb != null)
                //{
                if (bytes != null && bytes.Length > 0)
                {
                    FlujoContenidoTexto = new StreamReader(new MemoryStream(bytes));
                    if (FlujoContenidoTexto != null)
                    {
                        contenido = FlujoContenidoTexto.ReadToEnd();
                        //FlujoContenidoTexto.BaseStream.Seek(0, SeekOrigin.Begin);
                        FlujoContenidoTexto.Close();
                    }
                }
                //}
            }
            catch (WebException e)
            {
                throw e;
            }

            Contenido = contenido;
            return contenido;
        }

        private string GenerarEstructuraJson(IDictionary<string, object> parametros)
        {
            string strContenidoParametros = string.Empty;
            int indice = 0;

            if (parametros.Count > 1)
            {
                strContenidoParametros = "[";
                
                foreach (var item in parametros)
                {
                    GenerarEstructuraJson_Item(item, ref indice, ref strContenidoParametros, parametros, ",");
                    indice++;
                }

                strContenidoParametros += "]";
            }
            else if (parametros.Count == 1)
            {
                GenerarEstructuraJson_Item(parametros.FirstOrDefault(), ref indice, ref strContenidoParametros, null);
            }

            return strContenidoParametros;
        }

        private void GenerarEstructuraJson_Item(KeyValuePair<string, object> itemValue, ref int indice, ref string cadena, IDictionary<string, object> lista,
            string separador = "", bool sinNombreParametro = false)
        {
            IDictionary<string, object> item = null;
            
            if (itemValue.Value != null && 
                itemValue.Value.GetType() == typeof(ExpandoObject)) 
            {
                item = (IDictionary<string, object>)itemValue.Value;
            }

            if (item != null && 
                item.Keys.Any(i => i.Contains("***|EsConjunto|***")))
            {
                string strContenidoItem = "[";

                int indiceLista = 0;

                foreach (var itemLista in item)
                {
                    GenerarEstructuraJson_Item(itemLista, ref indiceLista, ref strContenidoItem, item, separador);
                    indiceLista++;
                }

                strContenidoItem += "]";

                if (lista == null || 
                    indice == lista.Count - 1)
                    cadena += strContenidoItem;
                else
                    cadena += strContenidoItem + (!string.IsNullOrEmpty(separador) ? separador: string.Empty);
            }
            else if(!itemValue.Key.Contains("***|EsConjunto|***"))
            {
                string strContenidoItem = string.Empty;

                //if (item != null)
                //{
                //    strContenidoItem = JsonConvert.SerializeObject(item);
                //}
                //else
                //{
                if(sinNombreParametro)
                {
                    strContenidoItem = JsonConvert.SerializeObject(itemValue.Value);
                }
                else
                {
                    var itemValor = new ExpandoObject() as IDictionary<string, object>;
                    itemValor.Add(itemValue);

                    strContenidoItem = JsonConvert.SerializeObject(itemValor);
                }
                    
                //}

                if (lista == null || 
                    indice == lista.Count - 1)
                    cadena += strContenidoItem;
                else
                    cadena += strContenidoItem + (!string.IsNullOrEmpty(separador) ? separador : string.Empty);
            }
        }

        private IDictionary<string, object> ObtenerParametrosObjeto(List<ParametroURL> lista)
        {
            var parametros = new ExpandoObject() as IDictionary<string, object>;
            //Type estructuraObjetoParametros = parametros.GetType();
            //List<NameValueCollection> listaParametros = new List<NameValueCollection>();
            IDictionary<string, object> parametroActual = parametros;
            List<IDictionary<string, object>> parametrosAnteriores = new List<IDictionary<string, object>>();
            string ultimoParametro = string.Empty;
            int nivelActual = 0;
            ParametroURL itemListaAnterior = null;

            foreach (var itemLista in lista)
            {
                if (itemLista.Nivel > nivelActual)
                {
                    nivelActual = itemLista.Nivel;
                    parametrosAnteriores.Add(parametroActual);
                    parametroActual[ultimoParametro] = new ExpandoObject();
                    parametroActual = parametroActual[ultimoParametro] as IDictionary<string, object>;

                    if (itemListaAnterior.EsConjuntoParametros)
                    {
                        parametroActual.Add("***|EsConjunto|***" + itemLista.GetHashCode().ToString(), null);
                    }

                    //NameValueCollection parametro = new NameValueCollection();
                    //parametro.Add(itemLista.Nombre, itemLista.Valor);
                    //listaParametros.Add(parametro);
                    //parametros.Add(itemLista.Nombre, itemLista.Valor);
                    //indiceParametros++;
                }
                else if (itemLista.Nivel < nivelActual)
                {
                    //if (itemLista.EsParametroEnConjunto)
                    //{
                    //    parametroActual.Add("FinConjunto", null);
                    //}

                    for (int indice = itemLista.Nivel; indice < nivelActual; indice++)
                    {
                        if (parametrosAnteriores.Any())
                        {
                            parametroActual = parametrosAnteriores.Last();
                            parametrosAnteriores.Remove(parametrosAnteriores.Last());
                        }
                    }

                    nivelActual = itemLista.Nivel;
                }
                //else
                //{
                
                if (itemLista.EsNumerico)
                {
                    if (itemLista.EsNumericoConDecimales)
                    {
                        double numero = 0;
                        double.TryParse(itemLista.Valor, out numero);
                        parametroActual.Add(itemLista.Nombre, numero);
                    }
                    else
                    {
                        int numero = 0;
                        int.TryParse(itemLista.Valor, out numero);
                        parametroActual.Add(itemLista.Nombre, numero);
                    }
                }
                else
                {
                    parametroActual.Add(itemLista.Nombre, itemLista.Valor);
                }
                

                ultimoParametro = itemLista.Nombre;
                itemListaAnterior = itemLista;
                //}                        
            }

            return parametros;
        }
    }
}
