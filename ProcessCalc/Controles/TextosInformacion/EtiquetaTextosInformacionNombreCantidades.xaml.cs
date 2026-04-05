using ProcessCalc.Controles.Condiciones;
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
    /// Lógica de interacción para EtiquetaTextosInformacionNombreCantidades.xaml
    /// </summary>
    public partial class EtiquetaTextosInformacionNombreCantidades : UserControl
    {
        public OpcionesNombreCantidad_TextosInformacion OpcionTextos { get; set; }
        public DefinicionTextoNombresCantidades DefinicionTextoNombre_ElementoContenedor { get; set; }
        public OpcionesNombreCantidad_TextosInformacion OpcionTextosSeleccionada_ElementoContenedor { get; set; }
        public FrameworkElement ElementoContenedor { get; set; }
        public string Texto { get; set; }
        public EtiquetaTextosInformacionNombreCantidades()
        {
            InitializeComponent();
        }

        public void MostrarEtiquetaCondiciones()
        {
            textoBoton.Content = string.Empty;
            Texto = string.Empty;

            AgregarTextoCondicion(OpcionTextos);
            
            if (OpcionTextosSeleccionada_ElementoContenedor == OpcionTextos)
            {
                Seleccionar();
            }
        }

        private void AgregarTextoCondicion(OpcionesNombreCantidad_TextosInformacion opcionTextos)
        {
            switch (opcionTextos.TipoOpcion)
            {
                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondiciones:

                    EtiquetaCondicionFlujo etiquetaItemCondicion = new EtiquetaCondicionFlujo();
                    etiquetaItemCondicion.Condiciones_ElementoContenedor = opcionTextos.Condiciones;
                    etiquetaItemCondicion.Condicion = opcionTextos.Condiciones;
                    etiquetaItemCondicion.ElementoContenedor = ElementoContenedor;
                    etiquetaItemCondicion.MostrarEtiquetaCondiciones();

                    Texto += "Cadenas de texto que cumplen las condiciones (si/entonces) '" + etiquetaItemCondicion.Texto + "'";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosiciones:
                    Texto += "Cadenas de texto que están en las siguientes posiciones dentro del vector: " + opcionTextos.PosicionesTextosInformacion;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacion:
                    Texto += "Primeras " + opcionTextos.NPrimerosTextosInformacion.ToString() + " cadenas de texto en el vector";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacion:
                    Texto += "Primera cadena de texto en el vector";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextosInformacionFijos:
                    Texto += "Las siguientes cadenas de texto fijas: " + opcionTextos.TextoInformacionFijo;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.Todos:
                    Texto += "Todas las cadenas de texto en el vector";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacion:
                    Texto += "Últimas " + opcionTextos.NUltimosTextosInformacion.ToString() + " cadenas de texto en el vector";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacion:
                    Texto += "Última cadena de texto en el vector";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreElemento:
                    Texto += "Nombre de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadElemento:
                    Texto += "Valor de la cantidad fija";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreNumeroElemento:
                    Texto += "Nombre del número de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadNumeroElemento:
                    Texto += "Valor del número de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreNumerosFiltrados:
                    Texto += "Nombres de los números de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadNumerosFiltrados:
                    Texto += "Valores de cantidad de los números fijos";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TodosTextosInformacionNumerosFiltrados:
                    Texto += "Todas las cadenas de texto de los números de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacionNumerosFiltrados:
                    Texto += "Primera cadena de texto de los números de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacionNumerosFiltrados:
                    Texto += "Primeras " + opcionTextos.NPrimerosTextosInformacionNumerosFiltrados.ToString() + " cadenas de texto de los números de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacionNumerosFiltrados:
                    Texto += "Última cadena de texto de los números de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacionNumerosFiltrados:
                    Texto += "Últimas " + opcionTextos.NUltimosTextosInformacionNumerosFiltrados.ToString() + " cadenas de texto de los números de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondicionesNumerosFiltrados:
                    etiquetaItemCondicion = new EtiquetaCondicionFlujo();
                    etiquetaItemCondicion.Condiciones_ElementoContenedor = opcionTextos.CondicionesTexto;
                    etiquetaItemCondicion.Condicion = opcionTextos.Condiciones;
                    etiquetaItemCondicion.ElementoContenedor = ElementoContenedor;
                    etiquetaItemCondicion.MostrarEtiquetaCondiciones();

                    Texto += "Cadenas de texto de los números de la variable, vector o retornados que cumplen las condiciones (si/entonces) '" + etiquetaItemCondicion.Texto + "'";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosicionesNumerosFiltrados:
                    Texto += "Cadenas de texto que están en las siguientes posiciones en el vector: " + opcionTextos.PosicionesTextosInformacionNumerosFiltrados + ", de los números de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TodosTextosInformacionNumero:
                    Texto += "Todas las cadenas de texto del número de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacionNumero:
                    Texto += "Primera cadena de texto del número de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacionNumero:
                    Texto += "Primeras " + opcionTextos.NPrimerosTextosInformacionNumero.ToString() + " cadenas de texto del número de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacionNumero:
                    Texto += "Última cadena de texto del número de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacionNumero:
                    Texto += "Últimas " + opcionTextos.NUltimosTextosInformacionNumero.ToString() + " cadenas de texto del número de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondicionesNumero:
                    etiquetaItemCondicion = new EtiquetaCondicionFlujo();
                    etiquetaItemCondicion.Condiciones_ElementoContenedor = opcionTextos.CondicionesTextoNumero;
                    etiquetaItemCondicion.Condicion = opcionTextos.Condiciones;
                    etiquetaItemCondicion.ElementoContenedor = ElementoContenedor;
                    etiquetaItemCondicion.MostrarEtiquetaCondiciones();

                    Texto += "Cadenas de texto que cumplen las condiciones (si/entonces) '" + etiquetaItemCondicion.Texto + "', del número de la variable, vector o retornados";

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosicionesNumero:
                    Texto += "Cadenas de texto que están en las siguientes posiciones en el vector: " + opcionTextos.PosicionesTextosInformacionNumero + ", del número de la variable, vector o retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreOperacion:
                    Texto += "Nombre de la variable o vector retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadOperacion:
                    Texto += "Valor de cantidad de la variable o vector retornados";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicion:
                    Texto += "Posición de la cantidad en el vector";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicionDefinicion:
                    Texto += "Posición de esta definición actual";
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicionOperando:
                    Texto += "Posición del operando actual con respecto a lista de operandos de la operación actual";
                    break;
            }

            if (opcionTextos.TipoOpcion != TipoOpcionesNombreCantidad_TextosInformacion.TextosInformacionFijos &
                opcionTextos.TipoOpcion != TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreOperacion &
                opcionTextos.TipoOpcion != TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadOperacion &
                opcionTextos.TipoOpcion != TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicion)
            {
                if (opcionTextos.Operando != null &&
                    opcionTextos.OperandoSubElemento == null)
                {
                    Texto += " de la variable, vector o retornados '" + opcionTextos.Operando.NombreCombo + "'";
                }

                else if (opcionTextos.OperandoSubElemento != null)
                {
                    Texto += " de la variable, vector o retornados '" + opcionTextos.OperandoSubElemento.NombreCombo + "'";
                }
            }

            if (opcionTextos.IncluirTextosImplica)
            {
                Texto += " (las cadenas de texto restantes, se agregan como cadenas de texto de implicación al número)";
            }

            textoBoton.Content = Texto;
        }

        private void botonFondo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ElementoContenedor != null && ElementoContenedor.GetType() == typeof(ConjuntoOpciones_NombresCantidades))
            {
                ((ConjuntoOpciones_NombresCantidades)ElementoContenedor).DesmarcarCondicionesBusquedas();
                OpcionTextosSeleccionada_ElementoContenedor = null;
            }

            Seleccionar();
        }

        private void Seleccionar()
        {
            Background = SystemColors.HighlightBrush;
            textoBoton.Background = SystemColors.HighlightBrush;
            OpcionTextosSeleccionada_ElementoContenedor = OpcionTextos;
            if (ElementoContenedor != null && ElementoContenedor.GetType() == typeof(ConjuntoOpciones_NombresCantidades))
                ((ConjuntoOpciones_NombresCantidades)ElementoContenedor).OpcionesTextoSeleccionada = OpcionTextos;
        }
        public void DesmarcarSeleccion()
        {
            Background = SystemColors.GradientInactiveCaptionBrush;
            textoBoton.Background = SystemColors.GradientInactiveCaptionBrush;
        }
    }
}
