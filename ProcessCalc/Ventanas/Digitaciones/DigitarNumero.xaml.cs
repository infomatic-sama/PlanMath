using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para DigitarNumero.xaml
    /// </summary>
    public partial class DigitarNumero : Window
    {
        public double Numero { get; set; }
        public bool Pausado { get; set; }
        public bool UtilizarPrimerConjunto_Automaticamente { get; set; }
        public bool UtilizarSoloTextosPredefinidos { get; set; }
        public bool SeleccionarNumeroDeOpciones { get; set; }
        public List<OpcionListaNumeros_Digitacion> OpcionesListaNumeros { get; set; }
        public Entrada Entrada { get; set; }
        public Calculo CalculoActual { get; set; }
        public bool ModoToolTip { get; set; }
        public DigitarNumero()
        {
            InitializeComponent();
        }

        private void numeroDigitado_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numero = 0;

            if (sender.GetType() == typeof(TextBox))
                double.TryParse(((TextBox)sender).Text, out numero);
            else if (sender.GetType() == typeof(ComboBox))
                double.TryParse(((ComboBox)sender).Text, out numero);

            Numero = numero;
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            if (!ModoToolTip)
            {
                GuardarCantidadesDigitadas();
                GuardarTextosDigitados();
            }

            DialogResult = true;
            Close();
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnPausar_Click(object sender, RoutedEventArgs e)
        {
            if (!ModoToolTip)
            {
                GuardarCantidadesDigitadas();
                GuardarTextosDigitados();
            }

            DialogResult = true;
            Pausado = true;
            Close();
        }

        private void GuardarCantidadesDigitadas()
        {
            Entrada.CantidadesDigitadas.AgregarCantidadDigitada(opcionesNumeros.Text);
            Entrada.CantidadesDigitadas.GuardarCantidadesDigitadas(CalculoActual.RutaArchivo, CalculoActual.ID, Entrada.ID);
        }

        private void GuardarTextosDigitados()
        {
            var textosDigitados = textos.Entrada.TextosDigitados.Where(i => i.IndiceNumero == 0).ToList();

            for (int indiceTexto = 0; indiceTexto < textos.Entrada.Textos.Count; indiceTexto++)
            {
                var textosDigitadosEncontrado = textosDigitados.Where(i => i.IndiceAsociado == indiceTexto).ToList();

                foreach (var textosDigitadosNumero in textosDigitadosEncontrado)
                {
                    if (!textosDigitadosNumero.TextosDigitados.Any(i => i == textos.Entrada.Textos[indiceTexto]))
                    {
                        textosDigitadosNumero.TextosDigitados.Add(textos.Entrada.Textos[indiceTexto]);
                    }
                }
            }

            ListaTextosDigitados.GuardarTextosDigitados(CalculoActual.RutaArchivo, CalculoActual.ID, Entrada.ID, textos.Entrada.TextosDigitados);
        }

        public void CargarTextosDigitados()
        {
            textos.Entrada.TextosDigitados = ListaTextosDigitados.CargarListasTextosDigitadas(CalculoActual.RutaArchivo, CalculoActual.ID, Entrada.ID);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!ModoToolTip)
            {
                textos.Digitando = true;
                textos.UtilizarPrimerConjunto_Automaticamente = UtilizarPrimerConjunto_Automaticamente;
                textos.UtilizarSoloTextosPredefinidos = UtilizarSoloTextosPredefinidos;

                Entrada.CantidadesDigitadas.CargarCantidadesDigitadas(CalculoActual.RutaArchivo, CalculoActual.ID, Entrada.ID);

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
                    numeroDigitado.Visibility = Visibility.Collapsed;
                    opcionesNumeros.Focus();

                    foreach (var item in Entrada.CantidadesDigitadas.NumerosDigitados)
                        opcionesNumeros.Items.Add(item);
                }
            }
            else
            {
                btnPausar.Visibility = Visibility.Collapsed;
                btnDetener.Visibility = Visibility.Visible;
                btnDetener.Content = "Cancelar";
                btnContinuar.Visibility = Visibility.Visible;
                btnContinuar.Content = "Aceptar";
            }            
        }

        private void opcionesListaNumeros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double numero;
            double.TryParse(opcionesListaNumeros.SelectedValue.ToString(), out numero);
            Numero = numero;
        }

        private void opcionesListaNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numero;
            double.TryParse(opcionesListaNumeros.Text.ToString(), out numero);
            Numero = numero;
        }
    }
}
