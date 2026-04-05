using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class ConjuntoTextosInformacion_Digitacion
    {
        public string Nombre { get; set; }
        public List<TextosInformacion_Digitacion> TextosInformacion { get; set; }
        public bool UtilizarCiclicamente {  get; set; }
        public bool ConTextosInformacion_EntradasAnteriores { get; set; }
        public Entrada EntradaAnterior_TextosInformacion_Predefinidos { get; set; }
        public ConjuntoTextosInformacion_Digitacion(string nombre)
        {
            Nombre = nombre;
            TextosInformacion = new List<TextosInformacion_Digitacion>();
            UtilizarCiclicamente = true;
        }

        public ConjuntoTextosInformacion_Digitacion()
        {
            Nombre = string.Empty;
            TextosInformacion = new List<TextosInformacion_Digitacion>();
            UtilizarCiclicamente = true;
        }
    }
}
