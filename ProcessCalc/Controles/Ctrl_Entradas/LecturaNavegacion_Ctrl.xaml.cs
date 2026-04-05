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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles.Ctrl_Entradas
{
    /// <summary>
    /// Lógica de interacción para LecturaNavegacion_Ctrl.xaml
    /// </summary>
    public partial class LecturaNavegacion_Ctrl : UserControl
    {
        public LecturaNavegacion LecturaNavegacion { get; set; }
        public List<BusquedaTextoArchivo> BusquedasEntrada { get; set; }
        string etiQuet;
        public string Etiqueta
        {
            get { return etiQuet; }
            set
            {
                etiQuet = value;
                opcionMismaLectura.Content = "La misma " + etiQuet.ToLower() + ".";
                etiqueta.Text = etiQuet.ToLower();
            }
        }
        string etiQuets;
        public string EtiquetaPlural
        {
            get { return etiQuets; }
            set
            {
                etiQuets = value;
                opcionLecturasDistintas.Content = etiQuets + " distintas.";
            }
        }
        public ConjuntoLecturasNavegacionesEntrada VistaLecturasNavegaciones { get; set; }
        bool Clic = false;
        public LecturaNavegacion_Ctrl()
        {
            InitializeComponent();
        }

        private void opcionMismaLectura_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (LecturaNavegacion != null)
                {
                    LecturaNavegacion.MismaLecturaBusquedasArchivo = (bool)opcionMismaLectura.IsChecked;

                    if (VistaLecturasNavegaciones != null && Clic)
                        VistaLecturasNavegaciones.ActualizarPosicion_LecturaNavegacion(LecturaNavegacion);
                }
            }
        }

        private void opcionLecturasDistintas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (LecturaNavegacion != null)
                {
                    LecturaNavegacion.MismaLecturaBusquedasArchivo = !(bool)opcionLecturasDistintas.IsChecked;

                    if (VistaLecturasNavegaciones != null && Clic)
                        VistaLecturasNavegaciones.ActualizarPosicion_LecturaNavegacion(LecturaNavegacion);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(LecturaNavegacion != null)
            {
                opcionMismaLectura.IsChecked = LecturaNavegacion.MismaLecturaBusquedasArchivo;
                opcionLecturasDistintas.IsChecked = !LecturaNavegacion.MismaLecturaBusquedasArchivo;

                ListarBusquedas();
                Clic = true;
            }
        }

        private void quitar_Click(object sender, RoutedEventArgs e)
        {
            if (VistaLecturasNavegaciones != null)
                VistaLecturasNavegaciones.QuitarLecturaNavegacion(LecturaNavegacion);
        }

        private void subir_Click(object sender, RoutedEventArgs e)
        {
            if (VistaLecturasNavegaciones != null)
                VistaLecturasNavegaciones.SubirLecturaNavegacion(LecturaNavegacion);
        }

        private void bajar_Click(object sender, RoutedEventArgs e)
        {
            if (VistaLecturasNavegaciones != null)
                VistaLecturasNavegaciones.BajarLecturaNavegacion(LecturaNavegacion);
        }

        private void ListarBusquedas()
        {
            foreach(var itemBusqueda in BusquedasEntrada)
            {
                ListarBusqueda(itemBusqueda);
            }
        }

        private void ListarBusqueda(BusquedaTextoArchivo busqueda)
        {
            CheckBox checkBusqueda = new CheckBox();
            checkBusqueda.Tag = busqueda;
            checkBusqueda.Content = busqueda.Nombre;
            checkBusqueda.Margin = new Thickness(10);
            checkBusqueda.Padding = new Thickness(5);

            checkBusqueda.IsChecked = LecturaNavegacion.BusquedasARealizar.Contains(busqueda);

            checkBusqueda.Checked += CheckBusqueda_Checked;
            checkBusqueda.Unchecked += CheckBusqueda_Unchecked;

            listaBusquedas.Children.Add(checkBusqueda);

            if (busqueda.EsConjuntoBusquedas)
            {
                foreach(var itemBusqueda in busqueda.ConjuntoBusquedas)
                {
                    ListarBusqueda(itemBusqueda);
                }
            }
        }

        private void CheckBusqueda_Unchecked(object sender, RoutedEventArgs e)
        {
            var busqueda = (BusquedaTextoArchivo)((CheckBox)sender).Tag;

            if (LecturaNavegacion.BusquedasARealizar.Contains(busqueda))
            {
                LecturaNavegacion.BusquedasARealizar.Remove(busqueda);
            }
        }

        private void CheckBusqueda_Checked(object sender, RoutedEventArgs e)
        {
            var busqueda = (BusquedaTextoArchivo)((CheckBox)sender).Tag;

            if(!LecturaNavegacion.BusquedasARealizar.Contains(busqueda))
            {
                LecturaNavegacion.BusquedasARealizar.Add(busqueda);
            }
        }
    }
}
