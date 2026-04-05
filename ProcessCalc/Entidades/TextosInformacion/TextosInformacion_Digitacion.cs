using ProcessCalc.Entidades.Entradas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class TextosInformacion_Digitacion
    {
        public string Nombre { get; set; }
        public string Texto { get; set; }
        public ConfiguracionSeleccionPosicionesTextos_Entrada SeleccionNumeros { get; set; }

        public TextosInformacion_Digitacion(string nombre, string texto)
        {
            Nombre = nombre;
            Texto = texto;
            SeleccionNumeros = new ConfiguracionSeleccionPosicionesTextos_Entrada();
        }
        public TextosInformacion_Digitacion()
        {
            Nombre = string.Empty;
            Texto = string.Empty;
            SeleccionNumeros = new ConfiguracionSeleccionPosicionesTextos_Entrada();
        }

        public bool SeleccionarFiltrarPosicion(int Posicion)
        {            
            bool valorCondiciones = true;

            if (SeleccionNumeros.CondicionesSeleccionarNumeros != null)
            {
                SeleccionNumeros.CondicionesSeleccionarNumeros.CantidadNumeros_Obtenidos = Posicion;
                valorCondiciones = SeleccionNumeros.CondicionesSeleccionarNumeros.EvaluarCondiciones(null);
            }
            
            switch (SeleccionNumeros.Opcion)
            {
                case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarTodosNumeros:

                    if(SeleccionNumeros.ConCondiciones)
                    {
                        if (valorCondiciones)
                            return true;
                    }
                    else
                        return true;

                    break;

                case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarCantidadDeterminadaNumeros:

                    string[] posiciones = SeleccionNumeros.PosicionesDeterminadasNumeros.Split(',');

                    foreach(var strPosicion in posiciones)
                    {
                        int intPosicion = 0;
                        if(int.TryParse(strPosicion, out intPosicion))
                        {
                            if (intPosicion == Posicion)
                                return true;
                        }
                    }

                    break;
            }

            return false;
        }
    }
}
