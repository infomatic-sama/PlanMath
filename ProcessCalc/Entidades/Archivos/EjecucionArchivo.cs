using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class EjecucionArchivo
    {
        public DateTime FechaHora { get; set; }
        public string DescripcionCalculo { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public EjecucionCalculo Ejecucion { get; set; }
    }
}
