using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.OrigenesDatos
{
    [DataContractAttribute]
    public class ParametroURL
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public int Nivel { get; set; }
        [DataMember]
        public bool EsNumerico { get; set; }
        [DataMember]
        public bool EsNumericoConDecimales { get; set; }
        [DataMember]
        public ParametroURL ConjuntoParametros { get; set; }
        [DataMember]
        public bool EsConjuntoParametros { get; set; }
        [DataMember]
        public bool EsParametroEnConjunto { get; set; }
        [DataMember]
        public bool ParametrosEnUrl { get; set; }
        [DataMember]
        public bool ParametrosEnBody { get; set; }

        public ParametroURL(string nombre, string valor, ParametroURL enConjunto = null)
        {
            Nombre = nombre;
            Valor = valor;
            ConjuntoParametros = enConjunto;

            if (enConjunto != null)
                EsParametroEnConjunto = true;
            else
                EsParametroEnConjunto = false;

            if (enConjunto != null)
                Nivel = enConjunto.Nivel + 1;
        }
    }
}
