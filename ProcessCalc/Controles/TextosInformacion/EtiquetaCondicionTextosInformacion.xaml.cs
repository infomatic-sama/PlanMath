using ProcessCalc.Entidades.TextosInformacion;
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

namespace ProcessCalc.Controles.TextosInformacion
{
    /// <summary>
    /// Lógica de interacción para EtiquetaCondicionTextosInformacion.xaml
    /// </summary>
    public partial class EtiquetaCondicionTextosInformacion : UserControl
    {
        public CondicionTextosInformacion Condicion { get; set; }
        public CondicionTextosInformacion Condiciones_ElementoContenedor { get; set; }
        public CondicionTextosInformacion CondicionSeleccionada_ElementoContenedor { get; set; }
        public FrameworkElement ElementoContenedor { get; set; }
        public string Texto { get; set; }
        public EtiquetaCondicionTextosInformacion()
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
                    EtiquetaCondicionTextosInformacion etiquetaItemCondicion = new EtiquetaCondicionTextosInformacion();
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

        private void AgregarTextoCondicion(CondicionTextosInformacion condicion)
        {
            TextBlock etiqueta = new TextBlock();
            etiqueta.Margin = new Thickness(10);

            switch (condicion.TipoElementoCondicion)
            {
                case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion:

                    if (!condicion.IncluirSoloNombreElemento)
                        etiqueta.Text += " Cadenas de texto ";
                    if (condicion.IncluirNombreElementoConTextos && !condicion.IncluirSoloNombreElemento)
                        etiqueta.Text += " y nombre de la variable ";
                    if (!condicion.IncluirNombreElementoConTextos && condicion.IncluirSoloNombreElemento)
                        etiqueta.Text += " Nombre de la variable ";

                    if (condicion.OperandoCondicion != null &&
                        condicion.OperandoSubElemento_Condicion_TextosInformacion == null)
                    {
                        etiqueta.Text += " de '" + condicion.OperandoCondicion.NombreCombo + "' ";
                    }

                    if (condicion.EntradaTextosInformacion_Condicion != null)
                    {
                        etiqueta.Text += " de '" + condicion.EntradaTextosInformacion_Condicion.Nombre + "' ";
                    }

                    else if (condicion.OperandoSubElemento_Condicion_TextosInformacion != null)
                    {
                        etiqueta.Text += " de '" + condicion.OperandoSubElemento_Condicion_TextosInformacion.NombreCombo + "' ";
                    }

                    switch (condicion.TipoOpcionCondicion_TextosInformacion)
                    {
                        case TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaTexto:
                            etiqueta.Text += " que contengan el texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.NoContengaTexto:
                            etiqueta.Text += " que no contengan el texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.EsParteDe:
                            etiqueta.Text += " que sea parte del texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.NoEsParteDe:
                            etiqueta.Text += " que no sea parte del texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.TerminenCon:
                            etiqueta.Text += " que terminen con el texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.EmpiecenCon:
                            etiqueta.Text += " que empiecen con el texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.TextoDistinto:
                            etiqueta.Text += " que sean distinto al texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual:
                            etiqueta.Text += " que sean el texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoIgual:
                            etiqueta.Text += " que posición de la cadena de texto en el vector sea igual a ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoDistinto:
                            etiqueta.Text += " que posición de la cadena de texto en el vector sea distinto a ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorQue:
                            etiqueta.Text += " que posición de la cadena de texto en el vector sea mayor que ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorQue:
                            etiqueta.Text += " que posición de la cadena de texto en el vector sea menor que ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorIgualQue:
                            etiqueta.Text += " que posición de la cadena de texto en el vector sea mayor o igual que ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorIgualQue:
                            etiqueta.Text += " que posición de la cadena de texto en el vector sea menor o igual que ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.CoincidaTextoBusqueda:
                            etiqueta.Text += " que coincida con la cadena de texto mediante la cadena de texto total de búsqueda ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.EsSoloNumero:
                            etiqueta.Text += " que sea sólo número ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.EsTexto:
                            etiqueta.Text += " que sea texto ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.NoTieneNumeros:
                            etiqueta.Text += " que no contenga números ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaSoloLetras:
                            etiqueta.Text += " que contenga sólo letras ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaSoloSimbolos:
                            etiqueta.Text += " que contenga sólo símbolos ";
                            break;

                        case TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaNumeros:
                            etiqueta.Text += " que contenga npumeros ";
                            break;
                    }

                    if (condicion.TipoOpcionCondicion_TextosInformacion != TipoOpcionImplicacion_AsignacionTextoInformacion.EsSoloNumero &&
                        condicion.TipoOpcionCondicion_TextosInformacion != TipoOpcionImplicacion_AsignacionTextoInformacion.EsTexto &&
                        condicion.TipoOpcionCondicion_TextosInformacion != TipoOpcionImplicacion_AsignacionTextoInformacion.NoTieneNumeros &&
                        condicion.TipoOpcionCondicion_TextosInformacion != TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaSoloLetras &&
                        condicion.TipoOpcionCondicion_TextosInformacion != TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaSoloSimbolos &&
                        condicion.TipoOpcionCondicion_TextosInformacion != TipoOpcionImplicacion_AsignacionTextoInformacion.ContengaNumeros)
                    {
                        switch (condicion.TipoTextosInformacion_Valores)
                        {
                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacionFijos:
                                string strPosicion = condicion.Valores_Condicion;

                                if (condicion.TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoDistinto |
                                        condicion.TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoIgual |
                                        condicion.TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorIgualQue |
                                        condicion.TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorQue |
                                        condicion.TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorIgualQue |
                                        condicion.TipoOpcionCondicion_TextosInformacion == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorQue)
                                {
                                    int posicion = 0;
                                    if (int.TryParse(strPosicion, out posicion))
                                    {
                                        posicion--;
                                        strPosicion = posicion.ToString();
                                    }
                                }

                                etiqueta.Text += "'" + strPosicion + "'";
                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion:
                                if (condicion.ElementoOperacion_Valores != null &&
                                    condicion.OperandoSubElemento_Valores_TextosInformacion == null)
                                    etiqueta.Text += "de la variable, vector de números o retornados '" + condicion.ElementoOperacion_Valores.NombreCombo + "'";

                                else if (condicion.OperandoSubElemento_Valores_TextosInformacion != null)
                                {
                                    etiqueta.Text += " de la variable, vector de números o retornados '" + condicion.OperandoSubElemento_Valores_TextosInformacion.NombreCombo + "' ";
                                }

                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion:
                                if (condicion.ElementoOperacion_Valores != null &&
                                    condicion.OperandoSubElemento_Valores_TextosInformacion == null)
                                    etiqueta.Text += "de la variable, vector de números o retornados '" + condicion.ElementoOperacion_Valores.NombreCombo + "' que cumplan esta condición ";

                                else if (condicion.OperandoSubElemento_Valores_TextosInformacion != null)
                                {
                                    etiqueta.Text += " de la variable, vector de números o retornados '" + condicion.OperandoSubElemento_Valores_TextosInformacion.NombreCombo + "' que cumplan esta condición ";
                                }

                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TodosTextosInformacion_CumplenCondicion:
                                etiqueta.Text += " acumulados que cumplan esta condición ";
                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada:
                                if (condicion.EntradaTextosInformacion_Condicion_Valores != null)
                                    etiqueta.Text += "de la variable o vector de números de entrada '" + condicion.EntradaTextosInformacion_Condicion_Valores.Nombre + "'";
                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacionInstancia:
                                etiqueta.Text += " de las cadenas de texto asignadas en la lógica actual de implicación ";
                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacionCondicion:
                                etiqueta.Text += " de las cadenas de texto asignadas en la condición (si/entonces) de la lógica actual de implicación ";
                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacion:
                                etiqueta.Text += " de todas las cadenas de texto asignadas de todas las lógicas en la implicación ";
                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeCumplenCondicionImplicacion:
                                etiqueta.Text += " de todas las cadenas de texto asignadas que cumplen las condiciones (si/entonces) de la implicación ";
                                break;

                            case TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicionLista:
                                etiqueta.Text += " de las cadenas de texto de las definiciones de listas de cadenas de texto ";
                                break;
                        }
                    }

                    break;

                case TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada:

                    switch (condicion.TipoSubElemento_Condicion)
                    {
                        case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento:
                            etiqueta.Text += " Números de la variable, vector de números o retornados ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                            etiqueta.Text += " Cantidad de números de la variable, vector de números o retornados ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                            etiqueta.Text += " Posiciones de números de la variable, vector de números o retornados ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                            etiqueta.Text += " Posiciones de lógicas de implicaciones de asignaciones de cadenas de texto de números de la variable, vector de números o retornados ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                            etiqueta.Text += " Posiciones de instancias de lógicas de implicaciones de asignaciones de cadenas de texto de números de la variable, vector de números o retornados ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                            etiqueta.Text += " Posiciones de iteraciones de instancias de lógicas de implicaciones de asignaciones de cadenas de texto de números de la variable, vector de números o retornados ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                            etiqueta.Text += " Cantidad de cadenas de texto de números de la variable, vector de números o retornados ";
                            break;

                        case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                            etiqueta.Text += " Posiciones de operandos de números de la variable, vector de números o retornados ";
                            break;
                    }

                    if (condicion.ElementoCondicion != null)
                    {
                        if (condicion.OperandoSubElemento_Condicion == null)
                        {
                            etiqueta.Text += "'" + condicion.ElementoCondicion.NombreCombo + "' del cálculo ";
                        }

                        else if (condicion.OperandoSubElemento_Condicion != null)
                        {
                            etiqueta.Text += "'" + condicion.OperandoSubElemento_Condicion.NombreCombo + "' ";
                        }
                    }
                    //else if (condicion.EntradaCondicion != null)
                    //{
                    //    etiqueta.Text += "Entrada de textos de información '" + condicion.EntradaCondicion.NombreCombo + "' ";
                    //}
                    switch (condicion.OpcionValorPosicion)
                    {
                        case TipoOpcionPosicion.PosicionPrimera:
                            etiqueta.Text += " que sea la primera ";
                            break;

                        case TipoOpcionPosicion.PosicionSegunda:
                            etiqueta.Text += " que sea la segunda ";
                            break;

                        case TipoOpcionPosicion.PosicionMitad:
                            etiqueta.Text += " que sea la mitad ";
                            break;

                        case TipoOpcionPosicion.PosicionPenultima:
                            etiqueta.Text += " que sea la penúltima ";
                            break;

                        case TipoOpcionPosicion.PosicionUltima:
                            etiqueta.Text += " que sea la última ";
                            break;
                    }

                    switch (condicion.TipoOpcionCondicion_ElementoOperacionEntrada)
                    {
                        case TipoOpcion_CondicionTextosInformacion_Implicacion.Contiene:
                            etiqueta.Text += " que contengan ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsParteDe:
                            etiqueta.Text += " que sean parte de ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EmpiezaCon:
                            etiqueta.Text += " que empiecen con ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsDistintoA:
                            etiqueta.Text += " que no sean ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.EsIgualA:
                            etiqueta.Text += " que sean ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorOIgualQue:
                            etiqueta.Text += " que mayores o iguales que ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MayorQue:
                            etiqueta.Text += " que sean mayores que ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorOIgualQue:
                            etiqueta.Text += " que sean menores o iguales que ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.MenorQue:
                            etiqueta.Text += " que sean menores que ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.NoContiene:
                            etiqueta.Text += " que no contengan ";
                            break;

                        case TipoOpcion_CondicionTextosInformacion_Implicacion.TerminaCon:
                            etiqueta.Text += " que terminen con ";
                            break;
                    }

                    switch (condicion.TipoElemento_Valores)
                    {
                        case TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos:
                            string strPosicion = condicion.Valores_Condicion;

                            if (condicion.TipoSubElemento_Condicion == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento)
                            {
                                int posicion = 0;
                                if (int.TryParse(strPosicion, out posicion))
                                {
                                    posicion--;
                                    strPosicion = posicion.ToString();
                                }
                            }

                            etiqueta.Text += "'" + strPosicion + "'";
                            break;

                        case TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.Valores_DesdeElementoOperacion:
                            switch (condicion.TipoSubElemento_Condicion_Valores)
                            {
                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento:
                                    etiqueta.Text += " Números de la variable, vector de números o retornados ";
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadNumerosElemento:
                                    etiqueta.Text += " Cantidad de números de la variable, vector de números o retornados ";
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento:
                                    etiqueta.Text += " Posiciones de números de la variable, vector de números o retornados ";
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesImplicaciones:
                                    etiqueta.Text += " Posiciones de lógicas de implicaciones de asignaciones de cadenas de texto de números de la variable, vector de números o retornados ";
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesInstanciasImplicaciones:
                                    etiqueta.Text += " Posiciones de instancias de lógicas de implicaciones de asignaciones de cadenas de texto de números de la variable, vector de números o retornados ";
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesIteracionesImplicaciones:
                                    etiqueta.Text += " Posiciones de iteraciones de instancias de lógicas de implicaciones de asignaciones de cadenas de texto de números de la variable, vector de números o retornados ";
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.CantidadesCadenasTexto_Numero:
                                    etiqueta.Text += " Cantidad de cadenas de texto de números de la variable, vector de números o retornados ";
                                    break;

                                case TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesOperandoElemento:
                                    etiqueta.Text += " Posiciones de operandos de números de la variable, vector de números o retornados ";
                                    break;
                            }

                            if (condicion.ElementoOperacion_Valores_ElementoAsociado != null &&
                                condicion.OperandoSubElemento_Condicion_Elemento == null)
                                etiqueta.Text += "'" + condicion.ElementoOperacion_Valores_ElementoAsociado.NombreCombo + "'";

                            else if (condicion.OperandoSubElemento_Condicion_Elemento != null)
                                etiqueta.Text += "'" + condicion.OperandoSubElemento_Condicion_Elemento.NombreCombo + "'";
                            break;
                    }

                    break;
            }

            if (condicion.NegarCondicion)
                etiqueta.Text += " (Negación) ";

            Texto += etiqueta.Text;
            textosCondiciones.Children.Add(etiqueta);
        }

        private void AgregarTextoConectorCondicion(CondicionTextosInformacion condicion)
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

        private void AgregarAbrirParentesis(EtiquetaCondicionTextosInformacion etiqueta)
        {
            TextBlock etiquetaTexto = new TextBlock();
            etiquetaTexto.Margin = new Thickness(10);

            etiquetaTexto.Text += " ( ";

            Texto += etiquetaTexto.Text;
            etiqueta.textosCondiciones.Children.Add(etiquetaTexto);
        }

        private void AgregarCerrarParentesis(EtiquetaCondicionTextosInformacion etiqueta)
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
            if (ElementoContenedor != null && ElementoContenedor.GetType() == typeof(EstablecerCondiciones_TextosInformacion))
            {
                ((EstablecerCondiciones_TextosInformacion)ElementoContenedor).DesmarcarCondicionesBusquedas();
                CondicionSeleccionada_ElementoContenedor = null;
            }

            Seleccionar();
        }

        private void Seleccionar()
        {
            Background = SystemColors.HighlightBrush;
            botonFondo.Background = SystemColors.HighlightBrush;
            CondicionSeleccionada_ElementoContenedor = Condicion;
            if (ElementoContenedor != null && ElementoContenedor.GetType() == typeof(EstablecerCondiciones_TextosInformacion))
                ((EstablecerCondiciones_TextosInformacion)ElementoContenedor).CondicionSeleccionada = Condicion;
        }

        public void DesmarcarSeleccion()
        {
            Background = SystemColors.GradientInactiveCaptionBrush;
            botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;

            if ((from UIElement E in textosCondiciones.Children where E.GetType() == typeof(EtiquetaCondicionTextosInformacion) select E).Any())
            {
                foreach (var itemCondicion in (from UIElement E in textosCondiciones.Children where E.GetType() == typeof(EtiquetaCondicionTextosInformacion) select E))
                {
                    ((EtiquetaCondicionTextosInformacion)itemCondicion).DesmarcarSeleccion();
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
