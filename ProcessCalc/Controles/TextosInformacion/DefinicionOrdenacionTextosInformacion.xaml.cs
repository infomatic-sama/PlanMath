using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Ventanas.Digitaciones;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Xml.Linq;

namespace ProcessCalc.Controles.TextosInformacion
{
    /// <summary>
    /// Lógica de interacción para DefinicionOrdenacionTextosInformacion.xaml
    /// </summary>
    public partial class DefinicionOrdenacionTextosInformacion : UserControl
    {
        public OrdenacionNumeros Ordenacion { get; set; }
        public int Indice { get; set; }
        public DefinicionOrdenacionTextosInformacion()
        {            
            InitializeComponent();            
        }

        private void cantidadDivisionTextosInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numeroVeces = 0;
            int.TryParse(cantidadDivisionTextosInformacion.Text, out numeroVeces);
            if (Ordenacion != null) Ordenacion.CantidadDivisionTextosInformacion = numeroVeces;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            opcionOrdenarNumerosDeMenorAMayor.GroupName += "_" + Indice.ToString();
            opcionOrdenarNumerosDeMayorAMenor.GroupName += "_" + Indice.ToString();
            opcionOrdenarTextosDeMenorAMayor.GroupName += "_" + Indice.ToString();
            opcionOrdenarTextosDeMayorAMenor.GroupName += "_" + Indice.ToString();
            opcionCadenaTextoCompleta.GroupName += "_" + Indice.ToString();
            opcionCadenaTextoDividida.GroupName += "_" + Indice.ToString();

            if (Ordenacion != null)
            {
                cantidadDivisionTextosInformacion.Text = Ordenacion.CantidadDivisionTextosInformacion.ToString();
                opcionOrdenarTextosInformacionCantidades.IsChecked = Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;
                opcionOrdenarTextosDeMenorAMayor.IsChecked = Ordenacion.OrdenarTextosDeMenorAMayor;
                opcionOrdenarTextosDeMayorAMenor.IsChecked = Ordenacion.OrdenarTextosDeMayorAMenor;
                opcionOrdenarTextosInformacionCantidades_SinOrdenarCantidades.IsChecked = Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;
                opcionOrdenarTextosDeMenorAMayor_SinOrdenarCantidades.IsChecked = Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
                opcionOrdenarTextosDeMayorAMenor_SinOrdenarCantidades.IsChecked = Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;
                opcionOrdenarNumerosDeMenorAMayor.IsChecked = Ordenacion.OrdenarNumerosDeMenorAMayor;
                opcionOrdenarNumerosDeMayorAMenor.IsChecked = Ordenacion.OrdenarNumerosDeMayorAMenor;

                opcionCadenaTextoCompleta.IsChecked = !Ordenacion.CadenaTextoDividida;
                opcionCadenaTextoDividida.IsChecked = Ordenacion.CadenaTextoDividida;
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades.IsChecked;

                if (Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                {
                    opcionOrdenarTextosDeMenorAMayor.Visibility = Visibility.Visible;
                    opcionOrdenarTextosDeMayorAMenor.Visibility = Visibility.Visible;                    
                }
                else
                {
                    opcionOrdenarTextosDeMenorAMayor.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosDeMayorAMenor.Visibility = Visibility.Collapsed;
                }

                opcionOrdenarTextosInformacionCantidades_SinOrdenarCantidades.Visibility = Visibility.Visible;
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades.IsChecked;

                opcionOrdenarTextosDeMenorAMayor.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor.IsChecked;
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_SinOrdenarCantidades.IsChecked;

                if (Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
                {
                    opcionOrdenarTextosDeMenorAMayor_SinOrdenarCantidades.Visibility = Visibility.Visible;
                    opcionOrdenarTextosDeMayorAMenor_SinOrdenarCantidades.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionOrdenarTextosDeMenorAMayor_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosDeMayorAMenor_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_SinOrdenarCantidades.IsChecked;

                opcionOrdenarTextosDeMenorAMayor_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionCadenaTextoCompleta_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.CadenaTextoDividida = (bool)!opcionCadenaTextoCompleta.IsChecked;
                divisionesCadenaTexto.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionCadenaTextoDividida_Checked(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                Ordenacion.CadenaTextoDividida = (bool)opcionCadenaTextoDividida.IsChecked;
                divisionesCadenaTexto.Visibility = Visibility.Visible;                
            }
        }

        private void divisionesCadenaTexto_Click(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                DivisionesPorCadenaTexto_Ordenaciones definirDivisiones = new DivisionesPorCadenaTexto_Ordenaciones();
                definirDivisiones.Ordenacion = Ordenacion;
                definirDivisiones.ShowDialog();
            }
        }
    }
}
