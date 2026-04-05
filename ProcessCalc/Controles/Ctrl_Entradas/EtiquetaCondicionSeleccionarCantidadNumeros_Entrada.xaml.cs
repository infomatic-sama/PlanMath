using ProcessCalc.Controles.Textos;
using ProcessCalc.Entidades.Entradas;
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

namespace ProcessCalc.Controles.Ctrl_Entradas
{
    /// <summary>
    /// Lógica de interacción para EtiquetaCondicionSeleccionarCantidadNumeros_Entrada.xaml
    /// </summary>
    public partial class EtiquetaCondicionSeleccionarCantidadNumeros_Entrada : UserControl
    {
        public CondicionSeleccionCantidadNumeros_Entrada Condicion { get; set; }
        public ConfigSeleccionCantidades_Entrada VistaConfiguracionSeleccionarNumeros { get; set; }
        public ConfigSeleccionPosicionesTextos_Entrada VistaConfiguracionSeleccionarPosiciones { get; set; }
        public EtiquetaCondicionSeleccionarCantidadNumeros_Entrada()
        {
            InitializeComponent();
        }

        public void MostrarEtiquetaCondiciones()
        {
            textosCondiciones.Children.Clear();

            if (Condicion.Condiciones.Any())
            {
                AgregarTextoConectorCondicion(Condicion);

                if (VistaConfiguracionSeleccionarNumeros != null)
                {
                    if (!(this.Condicion.CondicionContenedora == null && Condicion == VistaConfiguracionSeleccionarNumeros.Definicion.CondicionesSeleccionarNumeros))
                        AgregarAbrirParentesis(this);
                }
                else if (VistaConfiguracionSeleccionarPosiciones != null)
                {
                    if (!(this.Condicion.CondicionContenedora == null && Condicion == VistaConfiguracionSeleccionarPosiciones.Definicion.CondicionesSeleccionarNumeros))
                        AgregarAbrirParentesis(this);
                }

                AgregarTextoCondicion(Condicion);

                foreach (var itemCondicion in Condicion.Condiciones)
                {
                    EtiquetaCondicionSeleccionarCantidadNumeros_Entrada etiquetaItemCondicion = new EtiquetaCondicionSeleccionarCantidadNumeros_Entrada();

                    if (VistaConfiguracionSeleccionarNumeros != null)
                        etiquetaItemCondicion.VistaConfiguracionSeleccionarNumeros = VistaConfiguracionSeleccionarNumeros;
                    else if (VistaConfiguracionSeleccionarPosiciones != null)
                        etiquetaItemCondicion.VistaConfiguracionSeleccionarPosiciones = VistaConfiguracionSeleccionarPosiciones;

                    etiquetaItemCondicion.Condicion = itemCondicion;
                    textosCondiciones.Children.Add(etiquetaItemCondicion);
                    etiquetaItemCondicion.MostrarEtiquetaCondiciones();
                }

                if (VistaConfiguracionSeleccionarNumeros != null)
                {
                    if (!(this.Condicion.CondicionContenedora == null && Condicion == VistaConfiguracionSeleccionarNumeros.Definicion.CondicionesSeleccionarNumeros))
                        AgregarCerrarParentesis(this);
                }
                else if (VistaConfiguracionSeleccionarPosiciones != null)
                {
                    if (!(this.Condicion.CondicionContenedora == null && Condicion == VistaConfiguracionSeleccionarPosiciones.Definicion.CondicionesSeleccionarNumeros))
                        AgregarCerrarParentesis(this);
                }
            }
            else
            {
                AgregarTextoConectorCondicion(Condicion);
                AgregarTextoCondicion(Condicion);
            }


            if (VistaConfiguracionSeleccionarNumeros != null)
            {
                if (VistaConfiguracionSeleccionarNumeros.CondicionSeleccionada == Condicion)
                {
                    Seleccionar();
                }
            }
            else if (VistaConfiguracionSeleccionarPosiciones != null)
            {
                if (VistaConfiguracionSeleccionarPosiciones.CondicionSeleccionada == Condicion)
                {
                    Seleccionar();
                }
            }
        }

        private void AgregarTextoCondicion(CondicionSeleccionCantidadNumeros_Entrada condicion)
        {
            TextBlock etiqueta = new TextBlock();
            etiqueta.Margin = new Thickness(10);

            switch (condicion.TipoElementoCondicion)
            {
                case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTextosInformacion_Obtenidos:
                    etiqueta.Text += " Cantidad de cadenas de texto obtenidas ";

                    break;

                case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadNumeros_Obtenidos:
                    etiqueta.Text += " Cantidad de variables de números obtenidas ";

                    break;

                case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion:
                    etiqueta.Text += " Cantidad de cadenas de texto obtenidas de la última ejecución ";

                    break;

                case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadNumeros_Obtenidos_UltimaEjecucion:
                    etiqueta.Text += " Cantidad de variables de números obtenidas de la última ejecución ";

                    break;

                case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionInicialNumeros_Obtenidos:
                    etiqueta.Text += " Posición inicial de variables de números obtenidos en un vector ";

                    break;               

                case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion:
                    etiqueta.Text += " Posición inicial de variables de números obtenidas en un vector, de la última ejecución ";

                    break;                
                                    
                case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionFinalNumeros_Obtenidos:
                    etiqueta.Text += " Posición final de variables de números obtenidos en un vector ";

                    break;

                case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion:
                    etiqueta.Text += " Posición final de variables de números obtenidas en un vector, de la última ejecución ";

                    break;

                case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTotalTextosInformacion_Entrada:
                    etiqueta.Text += " Cantidad total de cadenas de texto del vector de la entrada ";

                    break;

                case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTotalNumeros_Entrada:
                    etiqueta.Text += " Cantidad total de variables de números del vector de la entrada ";

                    break;
            }

            switch (condicion.TipoOpcionCondicion)
            {
                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                    etiqueta.Text += " sea igual a ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                    etiqueta.Text += " sea distinto a ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                    etiqueta.Text += " que contenga (textualmente) ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                    etiqueta.Text += " que se parte de (textualmente) ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                    etiqueta.Text += " que empiece (textualmente) con ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                    etiqueta.Text += " que comience (textualmente) con ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                    etiqueta.Text += " que termine (textualmente) con ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                    etiqueta.Text += " que sea mayor o igual a ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                    etiqueta.Text += " que sea mayor a ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                    etiqueta.Text += " que sea menor o igual a ";
                    break;

                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                    etiqueta.Text += " que sea menor a ";
                    break;


            }

            if (condicion.TipoElementoValores == TipoElementoCondicion_SeleccionarNumeros_Entrada.ValoresFijos)
            {
                etiqueta.Text += " '" + condicion.Valores_Condicion + "' ";

            }
            else
            {
                switch (condicion.TipoElementoValores)
                {
                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTextosInformacion_Obtenidos:
                        etiqueta.Text += " Cantidad de cadenas de texto obtenidas ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadNumeros_Obtenidos:
                        etiqueta.Text += " Cantidad de variables de números obtenidas ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion:
                        etiqueta.Text += " Cantidad de cadenas de texto obtenidas de la última ejecución ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadNumeros_Obtenidos_UltimaEjecucion:
                        etiqueta.Text += " Cantidad de variables de números obtenidas de la última ejecución ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionInicialNumeros_Obtenidos:
                        etiqueta.Text += " Posición inicial de variables de números obtenidos en un vector ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion:
                        etiqueta.Text += " Posición inicial de variables de números obtenidas en un vector, de la última ejecución ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionFinalNumeros_Obtenidos:
                        etiqueta.Text += " Posición final de variables de números obtenidos en un vector ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion:
                        etiqueta.Text += " Posición final de variables de números obtenidas en un vector, de la última ejecución ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTotalTextosInformacion_Entrada:
                        etiqueta.Text += " Cantidad total de cadenas de texto del vector de la entrada ";

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTotalNumeros_Entrada:
                        etiqueta.Text += " Cantidad total de variables de números del vector de la entrada ";

                        break;
                }
            }

            textosCondiciones.Children.Add(etiqueta);
        }

        private void AgregarTextoConectorCondicion(CondicionSeleccionCantidadNumeros_Entrada condicion)
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

            textosCondiciones.Children.Add(etiqueta);
        }
        private void AgregarAbrirParentesis(EtiquetaCondicionSeleccionarCantidadNumeros_Entrada etiqueta)
        {
            TextBlock etiquetaTexto = new TextBlock();
            etiquetaTexto.Margin = new Thickness(10);

            etiquetaTexto.Text += " ( ";
            etiqueta.textosCondiciones.Children.Add(etiquetaTexto);
        }

        private void AgregarCerrarParentesis(EtiquetaCondicionSeleccionarCantidadNumeros_Entrada etiqueta)
        {
            TextBlock etiquetaTexto = new TextBlock();
            etiquetaTexto.SizeChanged += EtiquetaTexto_SizeChanged;
            etiquetaTexto.Margin = new Thickness(10);

            etiquetaTexto.Text += " ) ";
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
            if (VistaConfiguracionSeleccionarNumeros != null)
            {
                VistaConfiguracionSeleccionarNumeros.DesmarcarCondicionesBusquedas();
                VistaConfiguracionSeleccionarNumeros.CondicionSeleccionada = null;
            }
            else if (VistaConfiguracionSeleccionarPosiciones != null)
            {
                VistaConfiguracionSeleccionarPosiciones.DesmarcarCondicionesBusquedas();
                VistaConfiguracionSeleccionarPosiciones.CondicionSeleccionada = null;
            }

            Seleccionar();
        }

        private void Seleccionar()
        {
            Background = SystemColors.HighlightBrush;
            botonFondo.Background = SystemColors.HighlightBrush;

            if (VistaConfiguracionSeleccionarNumeros != null)
                VistaConfiguracionSeleccionarNumeros.CondicionSeleccionada = Condicion;
            else if (VistaConfiguracionSeleccionarPosiciones != null)
                VistaConfiguracionSeleccionarPosiciones.CondicionSeleccionada = Condicion;
        }

        public void DesmarcarSeleccion()
        {
            Background = SystemColors.GradientInactiveCaptionBrush;
            botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;

            if ((from UIElement E in textosCondiciones.Children where E.GetType() == typeof(EtiquetaCondicionSeleccionarCantidadNumeros_Entrada) select E).Any())
            {
                foreach (var itemCondicion in (from UIElement E in textosCondiciones.Children where E.GetType() == typeof(EtiquetaCondicionSeleccionarCantidadNumeros_Entrada) select E))
                {
                    ((EtiquetaCondicionSeleccionarCantidadNumeros_Entrada)itemCondicion).DesmarcarSeleccion();
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (double.IsInfinity(textosCondiciones.MaxWidth))
            {
                if (VistaConfiguracionSeleccionarNumeros != null)
                    textosCondiciones.MaxWidth = VistaConfiguracionSeleccionarNumeros.contenedorListaCondiciones.ActualWidth;
                else if (VistaConfiguracionSeleccionarPosiciones != null)
                    textosCondiciones.MaxWidth = VistaConfiguracionSeleccionarPosiciones.contenedorListaCondiciones.ActualWidth;
            }
        }
    }
}
