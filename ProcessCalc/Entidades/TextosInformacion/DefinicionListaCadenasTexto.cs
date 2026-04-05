using ProcessCalc.Entidades.Entradas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class DiseñoListaCadenasTexto : DiseñoTextosInformacion
    {
        [IgnoreDataMember]
        public List<FilaTextosInformacion_Entrada> ListasCadenasTexto { get; set; }
        [IgnoreDataMember]
        public int PosicionActualNumero_CondicionesOperador_SeleccionarOrdenar { get; set; }
        public DiseñoListaCadenasTexto()
        {
            ListasCadenasTexto = new List<FilaTextosInformacion_Entrada>();
        }

        public List<FilaTextosInformacion_Entrada> ObtenerTextos_ListaDefinicion()
        {
            var item = new List<FilaTextosInformacion_Entrada>();
            item.AddRange(ListasCadenasTexto);

            return item;
        }

        public List<string> ObtenerTextos_ListaDefinicion(int indicePosicion)
        {
            var item = new List<string>();

            if(indicePosicion >= 0 &&
                indicePosicion <= ListasCadenasTexto.Count - 1)
                item.AddRange(ListasCadenasTexto[indicePosicion].TextosInformacion);

            return item;
        }
    }
}
