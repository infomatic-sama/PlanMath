using ProcessCalc.Entidades;
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

namespace ProcessCalc.Controles.Notas
{
    /// <summary>
    /// Lógica de interacción para NotaDiagrama.xaml
    /// </summary>
    public partial class NotaDiagrama : UserControl
    {
        private DiseñoOperacion disOper;
        public DiseñoOperacion DiseñoOperacion
        {
            get
            {
                return disOper;
            }
            set
            {
                textoNota.Text = value.Info;
                disOper = value;
            }
        }
        public TipoElementoOperacion Tipo { get; set; }
        public VistaDiseñoOperaciones VistaOperaciones { get; set; }
        public VistaDiseñoOperacion VistaOpciones { get; set; }
        public bool EnDiagrama { get; set; }
        private DiseñoElementoOperacion disOperElement;
        public DiseñoElementoOperacion DiseñoElementoOperacion
        {
            get
            {
                return disOperElement;
            }
            set
            {
                textoNota.Text = value.Info;
                disOperElement = value;
            }
        }
        public TipoOpcionOperacion TipoOpcion { get; set; }
        public VistaDiseñoCalculos VistaCalculos { get; set; }
        public bool Bloqueada { get; set; }
        public Point PuntoMouseClic { get; set; }
        public NotaDiagrama()
        {
            InitializeComponent();
        }

        public void Clic()
        {
            textoNota_MouseLeftButtonDown(this, null);
        }

        public void Editar()
        {
            textoNota.Visibility = Visibility.Collapsed;
            textoEdicion.Text = textoNota.Text;
            textoEdicion.Visibility = Visibility.Visible;
            guardarTexto.Visibility = Visibility.Visible;
            textoEdicion.Focus();
        }

        public void Guardar()
        {
            textoNota.Text = textoEdicion.Text;
            textoEdicion.Visibility = Visibility.Collapsed;
            textoNota.Visibility = Visibility.Visible;
            guardarTexto.Visibility = Visibility.Collapsed;
        }

        private void textoEdicion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DiseñoOperacion != null)
                DiseñoOperacion.Info = textoEdicion.Text;
            else if (DiseñoElementoOperacion != null)
                DiseñoElementoOperacion.Info = textoEdicion.Text;
        }

        private void guardarTexto_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void textoNota_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bloqueada) return;

            if (e != null)
                PuntoMouseClic = e.GetPosition(this);

            //VistaOperaciones.ClicNota = true;
            if (e != null && e.ClickCount == 1)
            {
                if (VistaOperaciones != null)
                {
                    if (!VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Any() ||
                        DiseñoOperacion == null)
                    {
                        if (DiseñoOperacion == null)
                            VistaOperaciones.diagrama_MouseLeftButtonDown(this, e);

                        //VistaOperaciones.ElementoMovido = false;
                        VistaOperaciones.ElementoSeleccionado = true;
                        VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada = this;
                        //VistaOperaciones.EstablecerPuntoUbicacionElemento(this, e);
                        VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = Tipo;
                        if (EnDiagrama && DiseñoOperacion != null) VistaOperaciones.EstablecerTextoBotonSalida(DiseñoOperacion.ContieneSalida);
                        VistaOperaciones.DestacarElementoSeleccionado();
                    }
                    else
                    {
                        VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover = this;
                    }
                }
                else if (VistaOpciones != null)
                {
                    if (!VistaOpciones.ElementosDiagramaSeleccionados.Any() ||
                        DiseñoOperacion == null)
                    {
                        if (DiseñoOperacion == null)
                            VistaOpciones.diagrama_MouseLeftButtonDown(this, e);

                        VistaOpciones.ElementoSeleccionado_Bool = true;
                        VistaOpciones.NotaSeleccionada = this;
                        VistaOpciones.TipoElementoDiseñoOperacionSeleccionado = disOperElement.Tipo;
                        if (EnDiagrama && DiseñoElementoOperacion != null) VistaOpciones.EstablecerTextoBotonSalida(DiseñoElementoOperacion.ContieneSalida);
                        VistaOpciones.DestacarElementoSeleccionado();
                    }
                    else
                    {
                        VistaOpciones.ElementoDiagramaSeleccionadoMover = this;
                    }
                }
                else if (VistaCalculos != null)
                {
                    VistaCalculos.Elemento_e = e;
                    VistaCalculos.ElementoSeleccionado_Bool = true;
                    VistaCalculos.NotaSeleccionada = this;
                    VistaCalculos.DestacarElementoSeleccionado();
                }
                DragDrop.DoDragDrop(this, Tipo, DragDropEffects.Move);
            }
            else if (e == null)
            {
                fondo.BorderThickness = new Thickness(1);
            }
            else if (e != null && e.ClickCount == 2)
                Editar();
        }
    }
}
