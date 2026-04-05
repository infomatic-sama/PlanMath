using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class ConfiguracionSeleccionNumeros_Entrada
    {
        public TipoOpcionConfiguracionSeleccionNumeros_Entrada Opcion { get; set; }
        public int CantidadDeterminadaNumeros { get; set; }
        public TipoOpcionConfiguracionDefinicionSeleccionarNumeros_Entrada OpcionConfiguracion { get; set; }
        public CondicionSeleccionCantidadNumeros_Entrada CondicionesSeleccionarNumeros { get; set; }
        public bool ConCondiciones { get; set; }
        public bool OrdenInverso { get; set; }
        public TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial OpcionPosicionInicial { get; set; }
        public int PosicionInicialFija { get; set; }
        public ConfiguracionSeleccionNumeros_Entrada()
        {
            Opcion = TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarTodosNumeros;
            CantidadDeterminadaNumeros = 2;
            OpcionConfiguracion = TipoOpcionConfiguracionDefinicionSeleccionarNumeros_Entrada.DefinicionAutomatica;
            OpcionPosicionInicial = TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial.PosicionInicialFija;
            PosicionInicialFija = 1;
        }

        public ConfiguracionSeleccionNumeros_Entrada CopiarObjeto()
        {
            ConfiguracionSeleccionNumeros_Entrada config = new ConfiguracionSeleccionNumeros_Entrada();
            config.CantidadDeterminadaNumeros = CantidadDeterminadaNumeros;
            config.ConCondiciones = ConCondiciones;
            config.CondicionesSeleccionarNumeros = CondicionesSeleccionarNumeros.CopiarObjeto();
            config.Opcion = Opcion;
            config.OpcionConfiguracion = OpcionConfiguracion;
            config.OpcionPosicionInicial = OpcionPosicionInicial;
            config.OrdenInverso = OrdenInverso;
            config.PosicionInicialFija = PosicionInicialFija;

            return config;
        }
    }
}
