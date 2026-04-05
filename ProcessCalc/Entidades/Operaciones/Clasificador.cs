using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class Clasificador
    {
        public string ID {  get; set; }
        public string CadenaTexto {  get; set; }
        public bool UtilizarCadenasTexto_DeCantidad {  get; set; }
        public List<OperacionCadenaTexto> SeleccionCadenasTexto { get; set; }
                
        public Clasificador()
        {
            SeleccionCadenasTexto = new List<OperacionCadenaTexto>();
        }

        public Clasificador CopiarObjeto()
        {
            Clasificador clasificador = new Clasificador();
            clasificador.ID = ID;
            clasificador.CadenaTexto = CadenaTexto;
            clasificador.UtilizarCadenasTexto_DeCantidad = UtilizarCadenasTexto_DeCantidad;
            clasificador.SeleccionCadenasTexto = SeleccionCadenasTexto;

            return clasificador;
        }
    }
}
