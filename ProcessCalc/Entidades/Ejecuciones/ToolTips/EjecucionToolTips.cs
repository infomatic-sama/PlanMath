using ProcessCalc.Controles;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.OrigenesDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        public void EstablecerToolTipEntrada(ElementoEjecucionCalculo item, DiseñoOperacion elementoAsociado)
        {
            bool mostrarDigitacion = false;

            if (item.ElementoDiseñoRelacionado.DesdeTextoEnPantalla)
            {
                TooltipsCalculo.EstablecerDatosTooltip_Elemento_Digitacion_TextoEnPantalla(item.ElementoDiseñoRelacionado, TipoOpcionToolTip.Entrada);
                mostrarDigitacion = true;
            }
            else if(item.ElementoDiseñoRelacionado.DesdeOffice_EnvioManual)
            {
                TooltipsCalculo.EstablecerDatosTooltip_Elemento_Digitacion(item.ElementoDiseñoRelacionado, (ElementoEntradaEjecucion)item, TipoOpcionToolTip.Entrada);
                mostrarDigitacion = true;                
            }

            if (((ElementoEntradaEjecucion)item).TipoEntrada == TipoEntrada.ConjuntoNumeros)
            {
                if (((ElementoEntradaEjecucion)item).TipoEntradaConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeDigita)
                {
                    var elementoAsociadoDigitacion = TooltipsCalculo.EstablecerDatosTooltip_Elemento_Digitacion(item.ElementoDiseñoRelacionado, (ElementoEntradaEjecucion)item, TipoOpcionToolTip.Entrada);

                    ((ElementoEntradaEjecucion)item).Numeros.Clear();
                    ((ElementoEntradaEjecucion)item).Numeros.AddRange(elementoAsociadoDigitacion.Cantidades_Digitacion_Tooltip.Select(i => i.CopiarObjeto()));
                    ((ElementoEntradaEjecucion)item).AgregarClasificadoresGenericos();                    
                    
                }
                else
                {
                    if(((ElementoEntradaEjecucion)item).OrigenesDatos.Any(i => i.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion) && ((ElementoArchivoOrigenDatosEjecucion)i).ConfiguracionSeleccionArchivo != OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado))
                        TooltipsCalculo.EstablecerDatosTooltip_Elemento_SeleccionArchivosEntrada(item.ElementoDiseñoRelacionado, (ElementoEntradaEjecucion)item, TipoOpcionToolTip.Entrada);
                    else
                        TooltipsCalculo.EstablecerDatosTooltip_Elemento_NoSeleccionArchivosEntrada(item.ElementoDiseñoRelacionado, TipoOpcionToolTip.Entrada);
                                        
                    if (!mostrarDigitacion)
                        TooltipsCalculo.EstablecerDatosTooltip_Elemento_NoDigitacion(item.ElementoDiseñoRelacionado, elementoAsociado, TipoOpcionToolTip.Entrada);

                    TooltipsCalculo.EstablecerDatosTooltip_Elemento(item.ElementoDiseñoRelacionado,
                        ((ElementoEntradaEjecucion)item).Numeros.Select(i => i.CopiarObjeto()).ToList(), 
                        TipoOpcionToolTip.Entrada, item.Clasificadores_Cantidades,
                        item.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion,
                                    item.ElementoDiseñoRelacionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion);
                    
                }
            }
            else
            {
                if (!mostrarDigitacion)
                    TooltipsCalculo.EstablecerDatosTooltip_Elemento_NoDigitacion(item.ElementoDiseñoRelacionado, null, TipoOpcionToolTip.Entrada);
            }


            //if (elementoAsociado != null && 
            //    item.ElementoDiseñoRelacionado.EntradaRelacionada != null)
            //    item.ElementoDiseñoRelacionado.EntradaRelacionada.ConCambios_ToolTips = false;
        }
        public void ObtenerDatosToolTipEntrada(ElementoEjecucionCalculo item, DiseñoOperacion elementoAsociado)
        {
            if (((ElementoEntradaEjecucion)item).TipoEntrada == TipoEntrada.ConjuntoNumeros)
            {
                if (((ElementoEntradaEjecucion)item).TipoEntradaConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeDigita)
                {
                    ((ElementoEntradaEjecucion)item).Numeros.Clear();
                    ((ElementoEntradaEjecucion)item).Numeros.AddRange(item.ElementoDiseñoRelacionado.Cantidades_Digitacion_Tooltip.Select(i => i.CopiarObjeto()));
                    ((ElementoEntradaEjecucion)item).AgregarClasificadoresGenericos();
                }
                else
                {
                    List<EntidadNumero> numeros = new List<EntidadNumero>();

                    TooltipsCalculo.ObtenerDatosTooltip_Elemento(item.ElementoDiseñoRelacionado,
                       numeros, ((ElementoEntradaEjecucion)item));

                }
            }
        }

        public void EstablecerToolTip_EntradaNoSeleccionada(ElementoEjecucionCalculo item, bool Seleccionada)
        {
            TooltipsCalculo.EstablecerDatosTooltip_Elemento_EntradaNoSeleccionada(item.ElementoDiseñoRelacionado, TipoOpcionToolTip.Entrada, Seleccionada);
        }
    }
}
