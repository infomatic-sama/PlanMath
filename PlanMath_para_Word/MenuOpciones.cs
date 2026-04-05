using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanMath_para_Word
{
    public partial class MenuOpciones
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void definirEntrada_Click(object sender, RibbonControlEventArgs e)
        {
            Entradas.MostrarPanel_DefinirEntradas();

            PlanMath_Word.InstanciaActual.contenidoPanelDefinirEntradas.ListarFichas();
            PlanMath_Word.InstanciaActual.contenidoPanelDefinirEntradas.AplicarModoOscuro_SiCorresponde();
            PlanMath_Word.InstanciaActual.contenidoPanelDefinirEntradas.AgregarPrimeraFicha();
        }

        private void enviarEntradas_Click(object sender, RibbonControlEventArgs e)
        {
            Entradas.MostrarPanel_EnviarEntradas();
            PlanMath_Word.InstanciaActual.contenidoPanelEnviarEntradas.ActualizarEjecucionSeleccionada = true;
        }
        private void definirEntradaManual_Click_1(object sender, RibbonControlEventArgs e)
        {
            Entradas.MostrarPanel_DefinirEntradas(true);

            PlanMath_Word.InstanciaActual.contenidoPanelDefinirEntradas.ListarFichas();
            PlanMath_Word.InstanciaActual.contenidoPanelDefinirEntradas.AplicarModoOscuro_SiCorresponde();
            PlanMath_Word.InstanciaActual.contenidoPanelDefinirEntradas.AgregarPrimeraFicha();
        }

        private void enviarEntradasManuales_Click_1(object sender, RibbonControlEventArgs e)
        {
            Entradas.MostrarPanel_EnviarEntradas(true);
            PlanMath_Word.InstanciaActual.contenidoPanelEnviarEntradas.ActualizarEjecucionSeleccionada = true;
        }
    }
}
