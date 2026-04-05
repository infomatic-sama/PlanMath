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
    /// Lógica de interacción para ConjuntoLecturasNavegacionesEntrada.xaml
    /// </summary>
    public partial class ConjuntoLecturasNavegacionesEntrada : UserControl
    {
        public List<LecturaNavegacion> LecturasNavegaciones { get; set; }
        public List<BusquedaTextoArchivo> BusquedasEntrada { get; set; }
        string etiQuet;
        public string Etiqueta
        {
            get { return etiQuet; }
            set
            {
                etiQuet = value;
                etiquetaAgregar.Content += " " + etiQuet.ToLower();
            }
        }
        string etiQuets;
        public string EtiquetaPlural
        {
            get { return etiQuets; }
            set
            {
                etiQuets = value;
                etiqueta.Text = etiQuets.ToLower();
            }
        }
        public ConjuntoLecturasNavegacionesEntrada()
        {
            InitializeComponent();
        }

        public void ListarLecturasNavegaciones()
        {
            lista.Children.Clear();

            int indice = 1;
            bool flagOpcion = false;

            LecturaNavegacion_Ctrl itemAnterior = null;
            foreach(var item in LecturasNavegaciones)
            {
                LecturaNavegacion_Ctrl lecturaNavegacion = new LecturaNavegacion_Ctrl();
                lecturaNavegacion.BusquedasEntrada = BusquedasEntrada;
                lecturaNavegacion.LecturaNavegacion = item;
                lecturaNavegacion.nombreLecturaNavegacion.Text = Etiqueta + " " + indice.ToString();
                lecturaNavegacion.Etiqueta = Etiqueta;
                lecturaNavegacion.EtiquetaPlural = EtiquetaPlural;
                lecturaNavegacion.VistaLecturasNavegaciones = this;
                lecturaNavegacion.BorderBrush = Brushes.Black;

                if (itemAnterior != null && 
                    (!flagOpcion && flagOpcion != item.MismaLecturaBusquedasArchivo))
                {
                    itemAnterior.BorderThickness = new Thickness(0, 0, 0, 0);
                    lecturaNavegacion.BorderThickness = new Thickness(0, 2, 0, 0.3);
                    flagOpcion = true;
                }
                else
                {
                    lecturaNavegacion.BorderThickness = new Thickness(0, 0, 0, 0.3);
                }
                lista.Children.Add(lecturaNavegacion);
                indice++;
                itemAnterior = lecturaNavegacion;
            }
        }

        private void agregar_Click(object sender, RoutedEventArgs e)
        {
            if(LecturasNavegaciones != null)
            {
                int indice = LecturasNavegaciones.IndexOf(LecturasNavegaciones.FirstOrDefault(i => i.MismaLecturaBusquedasArchivo));

                if(indice > -1)
                    LecturasNavegaciones.Insert(indice, new LecturaNavegacion());
                else
                    LecturasNavegaciones.Add(new LecturaNavegacion());
            }

            ListarLecturasNavegaciones();
        }

        public void QuitarLecturaNavegacion(LecturaNavegacion lecturaNavegacion)
        {
            LecturasNavegaciones.Remove(lecturaNavegacion);
            ListarLecturasNavegaciones();
        }

        public void SubirLecturaNavegacion(LecturaNavegacion lecturaNavegacion)
        {
            int indiceInicial = 0;
            int indiceFinal = 0;

            if (lecturaNavegacion.MismaLecturaBusquedasArchivo)
            {
                indiceInicial = LecturasNavegaciones.IndexOf(LecturasNavegaciones.FirstOrDefault(i => i.MismaLecturaBusquedasArchivo));
                indiceFinal = LecturasNavegaciones.Count - 1;
            }
            else
            {
                indiceInicial = 0;
                indiceFinal = LecturasNavegaciones.IndexOf(LecturasNavegaciones.LastOrDefault(i => !i.MismaLecturaBusquedasArchivo));
            }

            int indiceElemento = LecturasNavegaciones.IndexOf(lecturaNavegacion);

            if ((indiceElemento >= indiceInicial &&
                indiceElemento <= indiceFinal) &&
                (indiceElemento - 1 >= indiceInicial &&
                indiceElemento - 1 <= indiceFinal))
            {
                LecturasNavegaciones.RemoveAt(indiceElemento);
                LecturasNavegaciones.Insert(indiceElemento - 1, lecturaNavegacion);

                ListarLecturasNavegaciones();
            }
        }

        public void BajarLecturaNavegacion(LecturaNavegacion lecturaNavegacion)
        {
            int indiceInicial = 0;
            int indiceFinal = 0;

            if (lecturaNavegacion.MismaLecturaBusquedasArchivo)
            {
                indiceInicial = LecturasNavegaciones.IndexOf(LecturasNavegaciones.FirstOrDefault(i => i.MismaLecturaBusquedasArchivo));
                indiceFinal = LecturasNavegaciones.Count - 1;
            }
            else
            {
                indiceInicial = 0;
                indiceFinal = LecturasNavegaciones.IndexOf(LecturasNavegaciones.LastOrDefault(i => !i.MismaLecturaBusquedasArchivo));
            }

            int indiceElemento = LecturasNavegaciones.IndexOf(lecturaNavegacion);

            if ((indiceElemento >= indiceInicial &&
                indiceElemento <= indiceFinal) &&
                (indiceElemento + 1 >= indiceInicial &&
                indiceElemento + 1 <= indiceFinal))
            {                
                LecturasNavegaciones.RemoveAt(indiceElemento);
                LecturasNavegaciones.Insert(indiceElemento + 1, lecturaNavegacion);

                ListarLecturasNavegaciones();
            }
        }

        public void ActualizarPosicion_LecturaNavegacion(LecturaNavegacion lecturaNavegacion)
        {
            LecturasNavegaciones.Remove(lecturaNavegacion);

            if(lecturaNavegacion.MismaLecturaBusquedasArchivo)
            {
                LecturasNavegaciones.Add(lecturaNavegacion);
            }
            else
            {
                int indice = LecturasNavegaciones.IndexOf(LecturasNavegaciones.FirstOrDefault(i => i.MismaLecturaBusquedasArchivo));

                if(indice > -1)
                    LecturasNavegaciones.Insert(indice, lecturaNavegacion);
                else
                    LecturasNavegaciones.Add(lecturaNavegacion);
            }

            ListarLecturasNavegaciones();
        }
    }
}
