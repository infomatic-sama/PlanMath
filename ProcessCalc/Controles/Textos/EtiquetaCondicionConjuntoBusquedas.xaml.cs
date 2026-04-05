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

namespace ProcessCalc.Controles.Textos
{
    /// <summary>
    /// Lógica de interacción para EtiquetaCondicionConjuntoBusquedas.xaml
    /// </summary>
    public partial class EtiquetaCondicionConjuntoBusquedas : UserControl
    {
        public CondicionConjuntoBusquedas Condicion { get; set; }
        public BusquedaEnArchivo VistaBusquedas { get; set; }
        public CondicionesBusquedas VistaCondicionesBusqueda { get; set; }
        public TextoEnArchivo VistaBusquedaTexto { get; set; }
        public bool ModoFiltros { get; set; }
        public EtiquetaCondicionConjuntoBusquedas()
        {
            InitializeComponent();
        }

        public void MostrarEtiquetaCondiciones()
        {
            textosCondiciones.Children.Clear();
            
            if (Condicion.Condiciones.Any())
            {
                if (!Condicion.ContenedorCondiciones)
                {
                    AgregarTextoConectorCondicion(Condicion);

                    if (VistaBusquedas != null)
                    {
                        if (VistaBusquedas.CondicionSeleccionada != Condicion)
                        {
                            AgregarAbrirParentesis(this);
                        }
                    }
                    else if (VistaBusquedaTexto != null)
                    {
                        if (ModoFiltros)
                        {
                            if (VistaBusquedaTexto.CondicionSeleccionadaFiltros != Condicion)
                            {
                                AgregarAbrirParentesis(this);
                            }
                        }
                        else
                        {
                            if (VistaBusquedaTexto.CondicionSeleccionada != Condicion)
                            {
                                AgregarAbrirParentesis(this);
                            }
                        }
                    }
                    else if (VistaCondicionesBusqueda != null)
                    {
                        if (VistaCondicionesBusqueda.CondicionSeleccionada != Condicion)
                        {
                            AgregarAbrirParentesis(this);
                        }
                    }
                     
                    AgregarTextoCondicion(Condicion);
                }
                
                foreach (var itemCondicion in Condicion.Condiciones)
                {
                    EtiquetaCondicionConjuntoBusquedas etiquetaItemCondicion = new EtiquetaCondicionConjuntoBusquedas();
                    
                    if(VistaBusquedas != null)
                        etiquetaItemCondicion.VistaBusquedas = VistaBusquedas;
                    else if(VistaBusquedaTexto != null)
                        etiquetaItemCondicion.VistaBusquedaTexto = VistaBusquedaTexto;
                    else if (VistaCondicionesBusqueda != null)
                        etiquetaItemCondicion.VistaCondicionesBusqueda = VistaCondicionesBusqueda;

                    etiquetaItemCondicion.Condicion = itemCondicion;
                    textosCondiciones.Children.Add(etiquetaItemCondicion);
                    etiquetaItemCondicion.MostrarEtiquetaCondiciones();
                }

                if (!Condicion.ContenedorCondiciones)
                {
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


            if (VistaBusquedas != null)
            {
                if (VistaBusquedas.CondicionSeleccionada == Condicion)
                {
                    Seleccionar();
                }
            }
            else if(VistaBusquedaTexto != null)
            {
                if (ModoFiltros)
                {
                    if (VistaBusquedaTexto.CondicionSeleccionadaFiltros == Condicion)
                    {
                        Seleccionar();
                    }
                }
                else
                {
                    if (VistaBusquedaTexto.CondicionSeleccionada == Condicion)
                    {
                        Seleccionar();
                    }
                }
            }
            else if (VistaCondicionesBusqueda != null)
            {
                if (VistaCondicionesBusqueda.CondicionSeleccionada == Condicion)
                {
                    Seleccionar();
                }
            }
        }

        private void AgregarTextoCondicion(CondicionConjuntoBusquedas condicion)
        {
            TextBlock etiqueta = new TextBlock();
            etiqueta.Margin = new Thickness(10);

            switch(condicion.TipoElementoCondicion)
            {
                case TipoElementoCondicion_ConjuntoBusquedas.TextosInformacionAsignacion_Numeros:
                    etiqueta.Text += " Cadenas de texto a asignar ";

                    switch (condicion.TipoOpcionCondicion)
                    {
                        case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                            etiqueta.Text += " sean iguales a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                            etiqueta.Text += " sean distintos a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                            etiqueta.Text += " que contengan ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                            etiqueta.Text += " que empiecen con ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                            etiqueta.Text += " que sean mayores o iguales (alfabéticamente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                            etiqueta.Text += " que sean mayores (alfabéticamente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                            etiqueta.Text += " que sean menores o iguales (alfabéticamente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                            etiqueta.Text += " que sean menores (alfabéticamente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                            etiqueta.Text += " que no contengan ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                            etiqueta.Text += " que terminen con ";
                            break;
                    }

                    break;

                case TipoElementoCondicion_ConjuntoBusquedas.CantidadTextosInformacion_AsignacionNumeros:
                    etiqueta.Text += " Cantidad de cadenas de texto a asignar ";

                    switch (condicion.TipoOpcionCondicion)
                    {
                        case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                            etiqueta.Text += " sea igual a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                            etiqueta.Text += " sea distinto a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                            etiqueta.Text += " que contenga (textualmente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                            etiqueta.Text += " que empiece (textualmente) con ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                            etiqueta.Text += " que sea mayor o igual a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                            etiqueta.Text += " que sea mayor a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                            etiqueta.Text += " que sea menor o igual a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                            etiqueta.Text += " que sea menor a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                            etiqueta.Text += " que no contenga (textualmente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                            etiqueta.Text += " que termine (textualmente) con ";
                            break;
                    }

                    break;

                case TipoElementoCondicion_ConjuntoBusquedas.NumerosEncontrados:
                    etiqueta.Text += " Números de variables, vectores de entradas o retornados encontrados ";

                    switch (condicion.TipoOpcionCondicion)
                    {
                        case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                            etiqueta.Text += " sean iguales a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                            etiqueta.Text += " sean distintos a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                            etiqueta.Text += " que contengan (textualmente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                            etiqueta.Text += " que empiecen (textualmente) con ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                            etiqueta.Text += " que sean mayores o iguales a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                            etiqueta.Text += " que sean mayores a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                            etiqueta.Text += " que sean menores o iguales a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                            etiqueta.Text += " que sean menores a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                            etiqueta.Text += " que no contengan (textualmente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                            etiqueta.Text += " que terminen (textualmente) con ";
                            break;
                    }

                    break;

                case TipoElementoCondicion_ConjuntoBusquedas.CantidadNumerosEncontrados:
                    etiqueta.Text += " Cantidad de números de variables, vectores de entradas o retornados encontrados ";

                    switch (condicion.TipoOpcionCondicion)
                    {
                        case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                            etiqueta.Text += " sea igual a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                            etiqueta.Text += " sea distinto a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                            etiqueta.Text += " que contengan (textualmente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                            etiqueta.Text += " que empiecen (textualmente) con ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                            etiqueta.Text += " que sean mayores o iguales a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                            etiqueta.Text += " que sean mayores a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                            etiqueta.Text += " que sean menores o iguales a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                            etiqueta.Text += " que sean menores a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                            etiqueta.Text += " que no contengan (textualmente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                            etiqueta.Text += " que terminen (textualmente) con ";
                            break;
                    }

                    break;

                case TipoElementoCondicion_ConjuntoBusquedas.BusquedasConjuntoRealizadas:
                    etiqueta.Text += " Cantidad de búsquedas de conjunto realizadas en el bucle ";

                    switch (condicion.TipoOpcionCondicion)
                    {
                        case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                            etiqueta.Text += " sea igual a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                            etiqueta.Text += " sea distinto a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                            etiqueta.Text += " que contenga (textualmente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                            etiqueta.Text += " que empiece (textualmente) con ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                            etiqueta.Text += " que sea mayor o igual a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                            etiqueta.Text += " que sea mayor a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                            etiqueta.Text += " que sea menor o igual a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                            etiqueta.Text += " que sea menor a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                            etiqueta.Text += " que no contengan (textualmente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                            etiqueta.Text += " que termine (textualmente) con ";
                            break;
                    }

                    break;

                case TipoElementoCondicion_ConjuntoBusquedas.TextoBusquedaEncontrado:
                    etiqueta.Text += " Cadena de texto de búsqueda encontrado ";

                    switch (condicion.TipoOpcionCondicion)
                    {
                        case TipoOpcionCondicion_ConjuntoBusquedas.TextoBusquedaCoincida:
                            etiqueta.Text += " coincida con el especificado ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.TextoBusquedaNoCoincida:
                            etiqueta.Text += " no coincida con el especificado ";
                            break;
                           
                        case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                            etiqueta.Text += " sean iguales a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                            etiqueta.Text += " sean distintos a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                            etiqueta.Text += " que contengan ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                            etiqueta.Text += " que empiecen con ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                            etiqueta.Text += " que sean mayores o iguales (alfabéticamente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                            etiqueta.Text += " que sean mayores (alfabéticamente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                            etiqueta.Text += " que sean menores o iguales (alfabéticamente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                            etiqueta.Text += " que sean menores (alfabéticamente) a ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                            etiqueta.Text += " que no contengan ";
                            break;

                        case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                            etiqueta.Text += " que terminen con ";
                            break;                           

                    }

                    break;
            }

            if ((condicion.TipoElementoCondicion == TipoElementoCondicion_ConjuntoBusquedas.TextoBusquedaEncontrado && (condicion.TipoOpcionCondicion != TipoOpcionCondicion_ConjuntoBusquedas.TextoBusquedaCoincida &
                condicion.TipoOpcionCondicion != TipoOpcionCondicion_ConjuntoBusquedas.TextoBusquedaNoCoincida)) || condicion.TipoElementoCondicion != TipoElementoCondicion_ConjuntoBusquedas.TextoBusquedaEncontrado)
            {
                etiqueta.Text += " '" + condicion.Valores_Condicion + "' ";

                if(condicion.BusquedaCondicion != null)
                    etiqueta.Text += " en '" + condicion.BusquedaCondicion.Nombre + "' ";
            }

            textosCondiciones.Children.Add(etiqueta);
        }

        private void AgregarTextoConectorCondicion(CondicionConjuntoBusquedas condicion)
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
        private void AgregarAbrirParentesis(EtiquetaCondicionConjuntoBusquedas etiqueta)
        {
            TextBlock etiquetaTexto = new TextBlock();
            etiquetaTexto.Margin = new Thickness(10);           
            
            etiquetaTexto.Text += " ( ";    
            etiqueta.textosCondiciones.Children.Add(etiquetaTexto);
        }

        private void AgregarCerrarParentesis(EtiquetaCondicionConjuntoBusquedas etiqueta)
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
            if (VistaBusquedas != null)
            {
                VistaBusquedas.DesmarcarCondicionesBusquedas();
                VistaBusquedas.CondicionSeleccionada = null;
            }
            else if(VistaBusquedaTexto != null)
            {
                if (ModoFiltros)
                {
                    VistaBusquedaTexto.DesmarcarCondicionesBusquedas_Filtros();
                    VistaBusquedaTexto.CondicionSeleccionadaFiltros = null;
                }
                else
                {
                    VistaBusquedaTexto.DesmarcarCondicionesBusquedas();
                    VistaBusquedaTexto.CondicionSeleccionada = null;
                }
            }
            else if (VistaCondicionesBusqueda != null)
            {
                VistaCondicionesBusqueda.DesmarcarCondicionesBusquedas();
                VistaCondicionesBusqueda.CondicionSeleccionada = null;
            }

            Seleccionar();
        }

        private void Seleccionar()
        {
            Background = SystemColors.HighlightBrush;
            botonFondo.Background = SystemColors.HighlightBrush;

            if (VistaBusquedas != null)
                VistaBusquedas.CondicionSeleccionada = Condicion;
            else if (VistaBusquedaTexto != null)
            {
                if (ModoFiltros)
                {
                    VistaBusquedaTexto.CondicionSeleccionadaFiltros = Condicion;
                }
                else
                {
                    VistaBusquedaTexto.CondicionSeleccionada = Condicion;
                }
            }
            else if (VistaCondicionesBusqueda != null)
                VistaCondicionesBusqueda.CondicionSeleccionada = Condicion;
        }

        public void DesmarcarSeleccion()
        {
            Background = SystemColors.GradientInactiveCaptionBrush;
            botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;

            if ((from UIElement E in textosCondiciones.Children where E.GetType() == typeof(EtiquetaCondicionConjuntoBusquedas) select E).Any())
            {
                foreach (var itemCondicion in (from UIElement E in textosCondiciones.Children where E.GetType() == typeof(EtiquetaCondicionConjuntoBusquedas) select E))
                {
                    ((EtiquetaCondicionConjuntoBusquedas)itemCondicion).DesmarcarSeleccion();
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (double.IsInfinity(textosCondiciones.MaxWidth))
            {
                if(VistaBusquedas != null)
                    textosCondiciones.MaxWidth = VistaBusquedas.ActualWidth;
                else if(VistaBusquedaTexto != null)
                    textosCondiciones.MaxWidth = VistaBusquedaTexto.ActualWidth;
                else if (VistaCondicionesBusqueda != null)
                    textosCondiciones.MaxWidth = VistaCondicionesBusqueda.ActualWidth;
            }
        }
    }
}
