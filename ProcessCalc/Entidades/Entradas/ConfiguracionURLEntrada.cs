using ProcessCalc.Entidades.OrigenesDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class ConfiguracionURLEntrada
    {
        public string URLEntrada { get; set; }
        public List<ParametroURL> ParametrosURL { get; set; }
        public List<ParametroURL> HeadersURL { get; set; }
        public OpcionEscribirURLEntrada ConfiguracionEscribirURL { get; set; }
        public bool EstablecerParametrosEjecucion { get; set; }
        public bool EstablecerLecturasNavegaciones_Busquedas { get; set; }
        public List<LecturaNavegacion> LecturasNavegaciones { get; set; }
        public List<string> TextosInformacionFijos { get; set; }
        public ConfiguracionURLEntrada() 
        {
            ParametrosURL = new List<ParametroURL>();
            HeadersURL = new List<ParametroURL>();
            ConfiguracionEscribirURL = OpcionEscribirURLEntrada.UtilizarURLIndicada;
            LecturasNavegaciones = new List<LecturaNavegacion>();
            TextosInformacionFijos = new List<string>();
        }
    }
}
