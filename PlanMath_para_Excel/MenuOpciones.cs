using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanMath_para_Excel
{
    public partial class MenuOpciones
    {
        private void MenuOpciones_Load(object sender, RibbonUIEventArgs e)
        {
            
        }

        private void definirEntrada_Click(object sender, RibbonControlEventArgs e)
        {            
            Entradas.MostrarPanel_DefinirEntradas();

            PlanMath_Excel.InstanciaActual.contenidoPanelDefinirEntradas.ListarFichas();
            PlanMath_Excel.InstanciaActual.contenidoPanelDefinirEntradas.AplicarModoOscuro_SiCorresponde();
            PlanMath_Excel.InstanciaActual.contenidoPanelDefinirEntradas.AgregarPrimeraFicha();
        }

        private void enviarEntradas_Click(object sender, RibbonControlEventArgs e)
        {
            Entradas.MostrarPanel_EnviarEntradas();
            PlanMath_Excel.InstanciaActual.contenidoPanelEnviarEntradas.ActualizarEjecucionSeleccionada = true;
        }

        private void definirEntradaManual_Click(object sender, RibbonControlEventArgs e)
        {
            Entradas.MostrarPanel_DefinirEntradas(true);

            PlanMath_Excel.InstanciaActual.contenidoPanelDefinirEntradas.ListarFichas();
            PlanMath_Excel.InstanciaActual.contenidoPanelDefinirEntradas.AplicarModoOscuro_SiCorresponde();
            PlanMath_Excel.InstanciaActual.contenidoPanelDefinirEntradas.AgregarPrimeraFicha();
        }

        private void enviarEntradasManuales_Click(object sender, RibbonControlEventArgs e)
        {
            Entradas.MostrarPanel_EnviarEntradas(true);
            PlanMath_Excel.InstanciaActual.contenidoPanelEnviarEntradas.ActualizarEjecucionSeleccionada = true;
        }
    }
}
