using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class ConfiguracionSeleccionPosicionesTextos_Entrada
    {
        public TipoOpcionConfiguracionSeleccionNumeros_Entrada Opcion { get; set; }
        public string PosicionesDeterminadasNumeros { get; set; }
        public CondicionSeleccionCantidadNumeros_Entrada CondicionesSeleccionarNumeros { get; set; }
        public bool ConCondiciones { get; set; }
        public ConfiguracionSeleccionPosicionesTextos_Entrada()
        {
            Opcion = TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarTodosNumeros;
            PosicionesDeterminadasNumeros = string.Empty;
        }

        public ConfiguracionSeleccionPosicionesTextos_Entrada CopiarObjeto()
        {
            ConfiguracionSeleccionPosicionesTextos_Entrada config = new ConfiguracionSeleccionPosicionesTextos_Entrada();
            config.Opcion = Opcion;
            config.PosicionesDeterminadasNumeros = PosicionesDeterminadasNumeros;
            config.CondicionesSeleccionarNumeros = CondicionesSeleccionarNumeros?.CopiarObjeto();
            config.ConCondiciones = ConCondiciones;


            return config;
        }
    }
}
