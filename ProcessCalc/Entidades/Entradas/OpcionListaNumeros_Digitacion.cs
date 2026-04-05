using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class OpcionListaNumeros_Digitacion
    {
        public string Nombre { get; set; }
        public string Texto { get; set; }
        public string NombreCombo
        {
            get
            {
                if (!string.IsNullOrEmpty(Nombre))
                    return Nombre + " - " + Texto;
                else
                    return Texto;
            }
        }

        public OpcionListaNumeros_Digitacion()
        {
            Nombre = string.Empty;
            Texto = string.Empty;
        }

        public OpcionListaNumeros_Digitacion(string nombre, string numero)
        {
            Nombre = nombre;
            Texto = numero;
        }
    }
}
