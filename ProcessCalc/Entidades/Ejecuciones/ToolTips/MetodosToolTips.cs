using ProcessCalc.Controles;
using ProcessCalc.Controles.Calculos;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Ejecuciones.ToolTips;
using ProcessCalc.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProcessCalc
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EjecucionTooltipsCalculo ObtenerEjecucionToolTips(Calculo calculo)
        {
            return EjecucionesToolTips.FirstOrDefault(i => i.Calculo == calculo);
        }

        public void EstablecerElementoCalculoTooltip_Ejecucion(Calculo calculo, DiseñoCalculo diseñoCalculo)
        {
            var ejecucion = ObtenerEjecucionToolTips(calculo);
            if (ejecucion != null)
            {
                var datosElemento = ejecucion.ObtenerDatosElementoCalculo_Tooltip(diseñoCalculo.ID);
                ejecucion.MostrarElementoVisual_ToolTip(datosElemento);
            }
        }

        public void EstablecerElementoTooltip_Ejecucion(Calculo calculo, DiseñoOperacion elemento)
        {
            var ejecucion = ObtenerEjecucionToolTips(calculo);
            if (ejecucion != null)
            {
                var datosElemento = ejecucion.ObtenerDatosElemento_Tooltip(elemento.ID);
                ejecucion.MostrarElementoVisual_ToolTip(datosElemento);
            }
        }

        public void EstablecerSubElementoTooltip_Ejecucion(Calculo calculo, DiseñoElementoOperacion elemento)
        {
            var ejecucion = ObtenerEjecucionToolTips(calculo);
            if (ejecucion != null)
            {
                var datosElemento = ejecucion.ObtenerDatosSubElemento_Tooltip(elemento.ID);
                ejecucion.MostrarElementoVisual_ToolTip(datosElemento);
            }
        }
    }
}
