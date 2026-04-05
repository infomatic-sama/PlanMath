using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class DiseñoCalculoTextosInformacion
    {
        public List<DiseñoTextosInformacion> ElementosTextosInformacion { get; set; }
        public double Ancho { get; set; }
        public double Alto { get; set; }
        public DiseñoCalculoTextosInformacion()
        {
            ElementosTextosInformacion = new List<DiseñoTextosInformacion>();
            Ancho = double.NaN;
            Alto = double.NaN;
        }

        public List<DiseñoTextosInformacion> VerificarEnCondiciones_DefinicionesTextosInformacion(DiseñoOperacion Operacion)
        {
            List<DiseñoTextosInformacion> elementos = new List<DiseñoTextosInformacion>();

            foreach (var itemDiseño in ElementosTextosInformacion)
            {
                if (itemDiseño.OperacionRelacionada == Operacion)
                    elementos.Add(itemDiseño);
            }

            return elementos;
        }
    }
}
