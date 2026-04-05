using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class ObjetoComparacionEntrada
    {
        public List<string> Textos { get; set; }
        public TipoEntrada Tipo { get; set; }
        public double NumeroFijo { get; set; }
    }
}
