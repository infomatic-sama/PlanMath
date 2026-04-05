using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using ProcessCalc.Vistas;
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

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para Numero.xaml
    /// </summary>
    public partial class Numero : UserControl
    {
        public bool EsPrimerNumero { get; set; }
        public EntidadNumero num;
        public bool Digitando { get; set; }
        public bool UtilizarPrimerConjunto_Automaticamente { get; set; }
        public bool SeleccionarNumeroDeOpciones { get; set; }
        public List<OpcionListaNumeros_Digitacion> OpcionesListaNumeros { get; set; }
        public bool UtilizarSoloTextosPredefinidos { get; set; }
        public EntidadNumero EntNumero
        {
            get
            {
                return num;
            }

            set
            {
                nombreNumero.Text = value.Nombre;
                numeroDigitado.Text = value.Numero.ToString();
                opcionesNumeros.Text = value.Numero.ToString();

                num = value;
            }
        }

        public VistaConjuntoNumerosEntrada VistaNumeros { get; set; }
        public DigitarConjuntoNumeros VistaDigitacion { get; set; }
        public bool NoAgregarOtrosNumeros { get; set; }
        public Entrada Entrada { get; set; }
        public Numero()
        {
            InitializeComponent();
        }

        private void nombreNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(num != null) num.Nombre = nombreNumero.Text;
        }

        private void btnOtroNumero_Click(object sender, RoutedEventArgs e)
        {
            if (Digitando)
            {
                VistaDigitacion.AgregarNuevoNumero();
            }
            else
            {
                VistaNumeros.AgregarNuevoNumero();
            }
        }

        private void btnQuitarNumero_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Quitar esta variable de número de forma permanente?", "Quitar variable de número", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(Digitando)
                    VistaDigitacion.QuitarNumero(this);
                else
                    VistaNumeros.QuitarNumero(this);
            }
        }

        private void numeroDigitado_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numero = 0;

            if(sender.GetType() == typeof(TextBox))
                double.TryParse(((TextBox)sender).Text, out numero);
            else if (sender.GetType() == typeof(ComboBox))
                double.TryParse(((ComboBox)sender).Text, out numero);

            if (EntNumero != null) EntNumero.Numero = numero;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textos.Digitando = Digitando;
            textos.UtilizarPrimerConjunto_Automaticamente = UtilizarPrimerConjunto_Automaticamente;
            textos.UtilizarSoloTextosPredefinidos = UtilizarSoloTextosPredefinidos;

            if(NoAgregarOtrosNumeros)
            {
                opcionesOtraEntrada.Visibility = Visibility.Collapsed;
            }

            if (Digitando)
            {
                if (OpcionesListaNumeros.Any())
                {
                    numeroDigitado.Visibility = Visibility.Collapsed;
                    opcionesListaNumeros.Visibility = Visibility.Visible;

                    if (SeleccionarNumeroDeOpciones)
                    {
                        opcionesListaNumeros.IsEditable = false;
                    }
                    else
                    {
                        opcionesListaNumeros.IsEditable = true;
                    }

                    opcionesListaNumeros.DisplayMemberPath = "NombreCombo";
                    opcionesListaNumeros.SelectedValuePath = "Texto";
                    opcionesListaNumeros.ItemsSource = OpcionesListaNumeros;

                    opcionesListaNumeros.SelectedIndex = 0;
                }
                else
                {
                    opcionesNumeros.Visibility = Visibility.Visible;
                    numeroDigitado.Visibility= Visibility.Collapsed;

                    if (Entrada != null)
                    {
                        foreach (var item in Entrada.CantidadesDigitadas.NumerosDigitados)
                            opcionesNumeros.Items.Add(item);
                    }
                    else
                    {
                        opcionesNumeros.Visibility = Visibility.Collapsed;
                        numeroDigitado.Visibility = Visibility.Visible;
                    }
                }
                
            }
        }

        private void opcionesListaNumeros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double numero = 0;
            double.TryParse(opcionesListaNumeros.SelectedValue.ToString(), out numero);
            if (EntNumero != null) EntNumero.Numero = numero;
        }

        private void opcionesListaNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numero = 0;
            double.TryParse(opcionesListaNumeros.Text.ToString(), out numero);
            if (EntNumero != null) EntNumero.Numero = numero;
        }
    }
}
