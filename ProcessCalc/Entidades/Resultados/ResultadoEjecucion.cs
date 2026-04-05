using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Resultados
{
    public class ResultadoEjecucion
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public bool EsConjuntoNumeros { get; set; }
        public double ValorNumerico { get; set; }
        public List<EntidadNumero> ValoresNumericos { get; set; }
        public bool NoMostrar_SiEsConjunto_SiNoTieneNumeros { get; set; }
        public bool NoMostrar_SiEsCero { get; set; }
        public List<string> TextosInformacion { get; set; }
        public Resultado ResultadoAsociado { get; set; }
        [IgnoreDataMember]
        public ElementoEjecucionCalculo SalidaRelacionada { get; set; }
        public string TextoAcompañante { get; set; }
        public ResultadoEjecucion()
        {
            ValoresNumericos = new List<EntidadNumero>();
            TextosInformacion = new List<string>();
            TextoAcompañante = string.Empty;
        }
    }
}
