using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles.Condiciones
{
    /// <summary>
    /// Lógica de interacción para EtiquetaCondicionFlujo.xaml
    /// </summary>
    public partial class EtiquetaCondicionFlujo : UserControl
    {
        public CondicionFlujo Condicion { get; set; }
        public CondicionFlujo Condiciones_ElementoContenedor { get; set; }
        public CondicionFlujo CondicionSeleccionada_ElementoContenedor { get; set; }
        public FrameworkElement ElementoContenedor { get; set; }
        public string Texto { get; set; }
        public EtiquetaCondicionFlujo()
        {
            InitializeComponent();
        }

        public void MostrarEtiquetaCondiciones()
        {
            textosCondiciones.Children.Clear();
            Texto = string.Empty;

            if (Condicion.Condiciones.Any())
            {
                if (!Condicion.ContenedorCondiciones)
                {
                    AgregarTextoConectorCondicion(Condicion);

                    if (!(this.Condicion.CondicionContenedora == null && Condicion == Condiciones_ElementoContenedor))
                        AgregarAbrirParentesis(this);

                    AgregarTextoCondicion(Condicion);
                }

                foreach (var itemCondicion in Condicion.Condiciones)
                {
                    EtiquetaCondicionFlujo etiquetaItemCondicion = new EtiquetaCondicionFlujo();
                    etiquetaItemCondicion.Condiciones_ElementoContenedor = Condiciones_ElementoContenedor;
                    etiquetaItemCondicion.CondicionSeleccionada_ElementoContenedor = CondicionSeleccionada_ElementoContenedor;
                    etiquetaItemCondicion.Condicion = itemCondicion;
                    etiquetaItemCondicion.ElementoContenedor = ElementoContenedor;
                    textosCondiciones.Children.Add(etiquetaItemCondicion);
                    etiquetaItemCondicion.MostrarEtiquetaCondiciones();
                    Texto += etiquetaItemCondicion.Texto;
                }

                if (!Condicion.ContenedorCondiciones)
                {
                    if (!(this.Condicion.CondicionContenedora == null && Condicion == Condiciones_ElementoContenedor))
                        AgregarCerrarParentesis(this);
                }
            }
            else
            {
                if (!Condicion.ContenedorCondiciones)
                {
                    AgregarTextoConectorCondicion(Condicion);
                    AgregarTextoCondicion(Condicion);
                }
            }

            if (CondicionSeleccionada_ElementoContenedor == Condicion)
            {
                Seleccionar();
            }
        }

        private void AgregarTextoCondicion(CondicionFlujo condicion)
        {
            TextBlock etiqueta = new TextBlock();
            etiqueta.Margin = new Thickness(10);

            switch (condicion.TipoSubElemento_Condicion)
            {
                case TipoSubElemento_EvaluacionCondicion_Flujo.NumerosElemento:
                    etiqueta.Text += " Números de la variable, vector u operación ";
                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.CantidadNumerosElemento:
                    etiqueta.Text += " Cantidad de números de la variable, vector u operación ";
                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion:
                    etiqueta.Text += " Cadenas de texto ";
                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacionCumplenCondicion:
                    etiqueta.Text += " Cadenas de texto que cumplen la condicional (si/entonces) ";
                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.TodosTextosInformacionCumplenCondicion:
                    etiqueta.Text += " Todas las cadenas de texto que cumplen la condicional (si/entonces) ";
                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento:
                    etiqueta.Text += " Nombre de la variable, vector u operación ";
                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.Clasificadores:
                    etiqueta.Text += " Clasificadores ";
                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.ClasificadoresCumplenCondicion:
                    etiqueta.Text += " Clasificadores que cumplen la condicional (si/entonces) ";
                    break;

                case TipoSubElemento_EvaluacionCondicion_Flujo.TodosClasificadoresCumplenCondicion:
                    etiqueta.Text += " Todos los clasificadores que cumplen la condicional (si/entonces) ";
                    break;
            }

            if (condicion.TipoSubElemento_Condicion != TipoSubElemento_EvaluacionCondicion_Flujo.TodosTextosInformacionCumplenCondicion &&
                condicion.TipoSubElemento_Condicion != TipoSubElemento_EvaluacionCondicion_Flujo.TodosClasificadoresCumplenCondicion)
            {
                if (condicion.OperandoCondicion != null &&
                    condicion.OperandoSubElemento_Condicion == null)
                {
                    etiqueta.Text += " de '" + condicion.OperandoCondicion.NombreCombo + "' ";
                }

                else if (condicion.OperandoSubElemento_Condicion != null)
                {
                    etiqueta.Text += " de '" + condicion.OperandoSubElemento_Condicion.NombreCombo + "' ";
                }
            }

            switch (condicion.TipoOpcionCondicion_TextosInformacion)
            {
                case TipoOpcion_CondicionTextosInformacion_Flujo.Contiene:
                    etiqueta.Text += " que contengan ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.EsParteDe:
                    etiqueta.Text += " que sean parte de ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.EmpiezaCon:
                    etiqueta.Text += " que empiecen con ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.EsDistintoA:
                    etiqueta.Text += " que no sean ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.EsIgualA:
                    etiqueta.Text += " que sean ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.MayorOIgualQue:
                    etiqueta.Text += " que mayores o iguales que ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.MayorQue:
                    etiqueta.Text += " que sean mayores que ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.MenorOIgualQue:
                    etiqueta.Text += " que sean menores o iguales que ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.MenorQue:
                    etiqueta.Text += " que sean menores que ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.NoContiene:
                    etiqueta.Text += " que no contengan ";
                    break;

                case TipoOpcion_CondicionTextosInformacion_Flujo.TerminaCon:
                    etiqueta.Text += " que terminen con ";
                    break;
            }

            switch (condicion.Tipo_Valores)
            {
                case TipoOpcion_ValoresCondicion_Flujo.ValoresFijos:
                    if(!string.IsNullOrEmpty(condicion.Valores_Condicion))
                        etiqueta.Text += "'" + condicion.Valores_Condicion + "'";
                    break;

                case TipoOpcion_ValoresCondicion_Flujo.Valores_DesdeElementoOperacion:

                    switch (condicion.TipoSubElemento_Valores)
                    {
                        case TipoSubElemento_EvaluacionCondicion_Flujo.NumerosElemento:
                            etiqueta.Text += " Números ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.CantidadNumerosElemento:
                            etiqueta.Text += " Cantidad de números ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacion:
                            etiqueta.Text += " Cadenas de texto ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.TextosInformacionCumplenCondicion:
                            etiqueta.Text += " Cadenas de texto que cumplen la condicional (si/entonces) ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.TodosTextosInformacionCumplenCondicion:
                            etiqueta.Text += " Todas las cadenas de texto que cumplen la condicional (si/entonces) ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.NombreElemento:
                            etiqueta.Text += " Nombre ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.Clasificadores:
                            etiqueta.Text += " Clasificadores ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.ClasificadoresCumplenCondicion:
                            etiqueta.Text += " Clasificadores que cumplen la condicional (si/entonces) ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_Flujo.TodosClasificadoresCumplenCondicion:
                            etiqueta.Text += " Todos los clasificadores que cumplen la condicional (si/entonces) ";
                            break;
                    }

                    if (condicion.ElementoOperacion_Valores != null &&
                        condicion.ElementoSubOperacion_Valores == null)
                        etiqueta.Text += " de la variable, vector u operación '" + condicion.ElementoOperacion_Valores.NombreCombo + "'";

                    else if (condicion.ElementoSubOperacion_Valores != null)
                        etiqueta.Text += " de la variable, vector u operación '" + condicion.ElementoSubOperacion_Valores.NombreCombo + "'";
                    break;
            }

            if (condicion.NegarCondicion)
                etiqueta.Text += " (Negación) ";

            Texto += etiqueta.Text;
            textosCondiciones.Children.Add(etiqueta);
        }

        private void AgregarTextoConectorCondicion(CondicionFlujo condicion)
        {
            TextBlock etiqueta = new TextBlock();
            etiqueta.Margin = new Thickness(10);

            switch (condicion.TipoConector)
            {
                case TipoConectorCondiciones_ConjuntoBusquedas.Y:
                    etiqueta.Text += " y ";
                    break;

                case TipoConectorCondiciones_ConjuntoBusquedas.O:
                    etiqueta.Text += " o ";
                    break;
            }

            Texto += etiqueta.Text;
            textosCondiciones.Children.Add(etiqueta);
        }

        private void AgregarAbrirParentesis(EtiquetaCondicionFlujo etiqueta)
        {
            TextBlock etiquetaTexto = new TextBlock();
            etiquetaTexto.Margin = new Thickness(10);

            etiquetaTexto.Text += " ( ";

            Texto += etiquetaTexto.Text;
            etiqueta.textosCondiciones.Children.Add(etiquetaTexto);
        }

        private void AgregarCerrarParentesis(EtiquetaCondicionFlujo etiqueta)
        {
            TextBlock etiquetaTexto = new TextBlock();
            etiquetaTexto.SizeChanged += EtiquetaTexto_SizeChanged;
            etiquetaTexto.Margin = new Thickness(10);

            etiquetaTexto.Text += " ) ";
            Texto += etiquetaTexto.Text;
            etiqueta.textosCondiciones.Children.Add(etiquetaTexto);
        }

        private void EtiquetaTexto_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (!double.IsNaN(((TextBlock)sender).ActualHeight))
                    ((TextBlock)sender).FontSize = ((TextBlock)sender).ActualHeight * 0.7;
            }
            catch (Exception) { }
        }

        private void botonFondo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ElementoContenedor != null && ElementoContenedor.GetType() == typeof(EstablecerCondiciones_Flujo))
            {
                ((EstablecerCondiciones_Flujo)ElementoContenedor).DesmarcarCondicionesBusquedas();
                CondicionSeleccionada_ElementoContenedor = null;
            }

            Seleccionar();
        }

        private void Seleccionar()
        {
            Background = SystemColors.HighlightBrush;
            botonFondo.Background = SystemColors.HighlightBrush;
            CondicionSeleccionada_ElementoContenedor = Condicion;
            if (ElementoContenedor != null && ElementoContenedor.GetType() == typeof(EstablecerCondiciones_Flujo))
                ((EstablecerCondiciones_Flujo)ElementoContenedor).CondicionSeleccionada = Condicion;
        }

        public void DesmarcarSeleccion()
        {
            Background = SystemColors.GradientInactiveCaptionBrush;
            botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;

            if ((from UIElement E in textosCondiciones.Children where E.GetType() == typeof(EtiquetaCondicionFlujo) select E).Any())
            {
                foreach (var itemCondicion in (from UIElement E in textosCondiciones.Children where E.GetType() == typeof(EtiquetaCondicionFlujo) select E))
                {
                    ((EtiquetaCondicionFlujo)itemCondicion).DesmarcarSeleccion();
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (double.IsInfinity(textosCondiciones.MaxWidth))
                textosCondiciones.MaxWidth = ElementoContenedor.ActualWidth;
        }
    }
}
