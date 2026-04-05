using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class Resultado
    {
        public string ID { get; set; }
        public DiseñoOperacion SalidaRelacionada { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool NoMostrar_SiEsConjunto_SiNoTieneNumeros { get; set; }
        public bool NoMostrar_SiEsCero { get; set; }
        public List<Clasificador> Clasificadores_Cantidades { get; set; }

        public Resultado()
        {
            Clasificadores_Cantidades = new List<Clasificador>();
        }
    }
}
