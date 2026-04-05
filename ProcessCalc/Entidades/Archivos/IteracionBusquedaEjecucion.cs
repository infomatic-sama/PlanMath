using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Archivos
{
    public class IteracionBusquedaEjecucion
    {
        public List<EntidadNumero> Numeros {  get; set; }
        public List<ElementoOperacionAritmeticaEjecucion> ElementosOperacion {  get; set; }

        public IteracionBusquedaEjecucion()
        {
            Numeros = new List<EntidadNumero>();
            ElementosOperacion = new List<ElementoOperacionAritmeticaEjecucion>();
        }
    }
}
