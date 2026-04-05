using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Archivos
{
    public class AsociacionEntradaOperando_ArchivoExterno
    {
        public DiseñoOperacion Operacion { get; set; }
        public DiseñoElementoOperacion SubOperacion { get; set; }
        public Entrada Entrada { get; set; }
        public ConfigTraspasoCantidades_Entrada_ArchivoExterno Configuracion { get; set; }
        public string IDEntrada_Externa { get; set; }
        public AsociacionEntradaOperando_ArchivoExterno()
        {

        }
        public AsociacionEntradaOperando_ArchivoExterno(TipoConfiguracionTraspasoCantidades_ArchivoExterno tip)
        {
            Configuracion = new ConfigTraspasoCantidades_Entrada_ArchivoExterno(tip);
        }

        public AsociacionEntradaOperando_ArchivoExterno ReplicarObjeto()
        {
            AsociacionEntradaOperando_ArchivoExterno asociacion = new AsociacionEntradaOperando_ArchivoExterno();
            asociacion.Operacion = Operacion;
            asociacion.SubOperacion = SubOperacion;
            asociacion.Entrada = Entrada;
            asociacion.Configuracion = Configuracion.ReplicarObjeto();
            asociacion.IDEntrada_Externa = IDEntrada_Externa;

            return asociacion;
        }
    }
}
