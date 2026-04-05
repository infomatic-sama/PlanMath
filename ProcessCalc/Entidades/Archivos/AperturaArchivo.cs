using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class AperturaArchivo
    {
        public string NombreArchivo { get; set; }
        public string DescripcionCalculo { get; set; }
        public DateTime FechaHora { get; set; }
        public string RutaArchivo { get; set; }
        public int IDProceso { get; set; }
        public string NombreProceso { get; set; }
        public string RutaEjecutableProceso { get; set; }
    }
}
