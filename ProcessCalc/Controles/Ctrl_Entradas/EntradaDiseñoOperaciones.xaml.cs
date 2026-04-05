using Newtonsoft.Json.Linq;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using ProcessCalc.Ventanas.Entradas;
using ProcessCalc.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Lógica de interacción para EntradaDiseñoOperaciones.xaml
    /// </summary>
    public partial class EntradaDiseñoOperaciones : UserControl
    {
        private Entrada entr;
        public Entrada Entrada
        {
            get
            {
                return entr;
            }
            set
            {
                EstablecerNombre(value);

                if (!EsEntrada)
                {
                    switch (value.Tipo)
                    {
                        case TipoEntrada.Numero:
                            tipoEntrada.Text = "Variable número";
                            iconoNumero.Visibility = Visibility.Visible;
                            break;
                        case TipoEntrada.ConjuntoNumeros:
                            tipoEntrada.Text = "Vector de números";
                            iconoNumeros.Visibility = Visibility.Visible;
                            break;
                        case TipoEntrada.TextosInformacion:
                            tipoEntrada.Text = "Vector de cadenas de texto";
                            iconoTextosInformacion.Visibility = Visibility.Visible;
                            break;
                        case TipoEntrada.Calculo:
                            tipoEntrada.Text = "Variable o vector retornado del cálculo anterior";
                            iconoCalculo.Visibility = Visibility.Visible;
                            break;
                        case TipoEntrada.Ninguno:
                            tipoEntrada.Text = string.Empty;
                            break;
                    }
                }

                if (EsEntrada && value.ElementoSalidaCalculoAnterior != null && 
                    value.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                {
                    iconoCalculo.Visibility = Visibility.Collapsed;

                    switch (value.ElementoSalidaCalculoAnterior.EntradaRelacionada.Tipo)
                    {
                        case TipoEntrada.Numero:
                            tipoEntrada.Text = "Variable número";
                            iconoNumero.Visibility = Visibility.Visible;
                            break;
                        case TipoEntrada.ConjuntoNumeros:
                            tipoEntrada.Text = "Vector de números";
                            iconoNumeros.Visibility = Visibility.Visible;
                            break;
                        case TipoEntrada.TextosInformacion:
                            tipoEntrada.Text = "Vector de cadenas de texto";
                            iconoTextosInformacion.Visibility = Visibility.Visible;
                            break;
                        case TipoEntrada.Calculo:
                            tipoEntrada.Text = "Variable o vector retornado del cálculo anterior";
                            iconoCalculo.Visibility = Visibility.Visible;
                            break;
                        case TipoEntrada.Ninguno:
                            tipoEntrada.Text = string.Empty;
                            break;
                    }
                }

                if (EsEntradaNueva)
                {
                    tipoEntrada.Text = string.Empty;
                    tipoEntrada.Visibility = Visibility.Collapsed;
                }
                else
                {
                    nombreEntrada.MaxWidth = 150;
                    nombreEntrada.MaxHeight = 150;
                }

                entr = value;
            }
        }
        public VistaDiseñoOperaciones VistaOperaciones { get; set; }
        public bool EnDiagrama { get; set; }
        private DiseñoOperacion disOper;
        public DiseñoOperacion DiseñoOperacion 
        {
            get
            {
                return disOper;
            }
            set
            {
                disOper = value;
            }
        }
        public VistaDiseñoOperacion VistaOperacion { get; set; }
        public bool EnDiagramaOperacion;
        public DiseñoElementoOperacion DiseñoElementoOperacion { get; set; }
        public bool DesdeDiagramaOperacion;
        public bool Bloqueada { get; set; }
        public bool EsEntrada { get; set; }
        public DiseñoTextosInformacion DiseñoTextosInformacion { get; set; }
        public VistaTextosInformacion VistaTextosInformacion { get; set; }
        public bool EsEntradaNueva { get; set; }
        bool clickeado = false;
        bool modoResumido;
        string nombreElemento;
        public bool ModoIconosResumidos
        {
            get
            {
                return modoResumido;
            }

            set
            {
                modoResumido = value;
                if(modoResumido)
                {
                    tipoEntrada.Visibility = Visibility.Collapsed;

                    if (nombreElemento.Contains(" desde "))
                        nombreEntrada.Text = nombreElemento.Remove(nombreElemento.IndexOf(" desde "));
                    else
                        EstablecerNombre(entr);
                }
                else
                {
                    if(!EsEntradaNueva)
                        tipoEntrada.Visibility = Visibility.Visible;

                    EstablecerNombre(entr);
                }
            }
        }

        public Point PuntoMouseClic {  get; set; }
        public bool EnVentanaAdm_CantidadesDigitadas { get; set; }
        public AdmCantidadesDigitadas AdministrarCantidadesDigitadas { get; set; }
        public bool EnDiagramaTextos { get; set; }
        public EntradaDiseñoOperaciones()
        {
            InitializeComponent();
            botonFondo.Background = Brushes.Transparent;

            botonFondo.MouseEnter += BotonFondo_MouseEnter;
            //popup.MouseLeave += BotonFondo_MouseLeave;
        }

        //private void BotonFondo_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    popup.IsOpen = false;
        //}
        private MainWindow ObtenerVentana()
        {
            if (VistaOperacion != null)
            {
                return VistaOperacion.Ventana;
            }
            else if (VistaOperaciones != null)
            {
                return VistaOperaciones.Ventana;
            }
            else if (VistaTextosInformacion != null)
            {
                return VistaTextosInformacion.Ventana;
            }

            return null;
        }

        private void BotonFondo_MouseEnter(object sender, MouseEventArgs e)
        {
            if (VistaOperaciones != null)
            {
                VistaOperaciones.OcultarToolTips(this);
            }
            else if (VistaOperacion != null)
            {
                VistaOperacion.OcultarToolTips(this);
            }

            if ((VistaOperacion != null &&
                this.Parent == VistaOperacion.diagrama &&
                DiseñoElementoOperacion != null) &&
                VistaOperacion.rectanguloSeleccion == null)
            {
                ObtenerVentana().popup.PlacementTarget = this;
                ObtenerVentana().EstablecerSubElementoTooltip_Ejecucion(ObtenerVentana().CalculoActual,
                    DiseñoElementoOperacion);
                ObtenerVentana().popup.IsOpen = true;
            }
            else if ((VistaOperaciones != null &&
                this.Parent == VistaOperaciones.diagrama &&
                DiseñoOperacion != null) &&
                VistaOperaciones.rectanguloSeleccion == null)
            {
                ObtenerVentana().popup.PlacementTarget = this;
                ObtenerVentana().EstablecerElementoTooltip_Ejecucion(ObtenerVentana().CalculoActual,
                    DiseñoOperacion);
                ObtenerVentana().popup.IsOpen = true;
            }
        }

        public void Clic()
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null && ObtenerVentana().popup.Child.IsMouseOver) || ObtenerVentana().popup.Child == null))
            {
                //e.Handled = true;
                return;
            }

            clickeado = true;
            if (VistaOperaciones != null)
                Button_PreviewMouseLeftButtonDown(this, VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento);
            else if (VistaOperacion != null && VistaOperacion.e_SeleccionarElemento != null)
                Button_PreviewMouseLeftButtonDown(this, VistaOperacion.e_SeleccionarElemento);
            else
                Button_PreviewMouseLeftButtonDown(this, null);

            clickeado = false;
        }

        public void EstablecerNombre(Entrada entr)
        {
            if ((DesdeDiagramaOperacion &&
                    DiseñoElementoOperacion != null) || 
                    (Bloqueada && EnDiagramaTextos))
            {
                nombreEntrada.Text = DiseñoElementoOperacion.NombreElemento;
                nombreElemento = DiseñoElementoOperacion.NombreElemento;
            }
            else
            {
                nombreEntrada.Text = entr.Nombre;
                nombreElemento = entr.Nombre;
            }
        }
        public void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(ObtenerVentana().popup.IsOpen && ObtenerVentana().popup.Child != null &&
                ObtenerVentana().popup.Child.IsMouseOver)
            {
                return;
            }

            if (Bloqueada) return;

            if(EnVentanaAdm_CantidadesDigitadas)
            {
                AdministrarCantidadesDigitadas.EntradaSeleccionada = Entrada;
                return;
            }

            if (VistaOperaciones != null)
            {
                VistaOperaciones.OcultarToolTips(this);
            }
            else if (VistaOperacion != null)
            {
                VistaOperacion.OcultarToolTips(this);
            }

            if (e != null)
                PuntoMouseClic = e.GetPosition(this);

            if (edicionNombre.Visibility == Visibility.Collapsed &
                edicionNombre_Entrada.Visibility == Visibility.Collapsed)
            {
                if (e != null && e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
                {
                    if (DesdeDiagramaOperacion &&
                    EnDiagramaOperacion &&
                    DiseñoElementoOperacion != null)
                        MostrarOcultarEdicion(true);
                    else
                        MostrarOcultarEdicion_Entrada(true);
                }
                else if ((e != null && e.LeftButton == MouseButtonState.Pressed) |
                (clickeado && e != null && e.LeftButton == MouseButtonState.Released) | ((VistaOperaciones != null && VistaOperaciones.SeleccionandoElemento) |
                (VistaOperacion != null && VistaOperacion.e_SeleccionarElemento != null) | (VistaTextosInformacion != null && VistaTextosInformacion.SeleccionandoElemento)))
                {
                    if (!DesdeDiagramaOperacion)
                    {
                        if (VistaOperaciones != null)
                        {
                            if (e != null && e.LeftButton == MouseButtonState.Pressed &&
                        VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Any() &&
                        !VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Contains(this))
                            {
                                VistaOperaciones.diagrama_MouseLeftButtonDown(sender, e);
                            }

                            if (!VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Any() ||
                                DiseñoOperacion == null)
                            {
                                if (DiseñoOperacion == null)
                                    VistaOperaciones.diagrama_MouseLeftButtonDown(this, e);

                                VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento = e;
                                VistaOperaciones.ElementoSeleccionado = true;
                                //VistaOperaciones.EstablecerPuntoUbicacionElemento(this, e);
                                VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada = this;
                                VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Entrada;
                                if (EnDiagrama && DiseñoOperacion != null) VistaOperaciones.EstablecerTextoBotonSalida(DiseñoOperacion.ContieneSalida);
                                VistaOperaciones.DestacarElementoSeleccionado();
                                VistaOperaciones.MostrarOrdenOperando_Elemento(disOper);
                                VistaOperaciones.MostrarInfo_Elemento(disOper);
                                VistaOperaciones.MostrarAcumulacion();
                                VistaOperaciones.MostrarConfiguracionSeparadores_Elemento(disOper);
                            }
                            else
                            {
                                VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover = this;
                            }
                        }
                        else if (VistaTextosInformacion != null)
                        {
                            if (e != null && e.LeftButton == MouseButtonState.Pressed &&
                        VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Any() &&
                        !VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Contains(this))
                            {
                                VistaTextosInformacion.diagrama_MouseLeftButtonDown(sender, e);
                            }

                            if (!VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Any() ||
                                DiseñoOperacion == null)
                            {
                                if (DiseñoOperacion == null)
                                    VistaTextosInformacion.diagrama_MouseLeftButtonDown(this, e);

                                VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.e_SeleccionarElemento = e;
                                VistaTextosInformacion.ElementoSeleccionado = true;
                                //VistaOperaciones.EstablecerPuntoUbicacionElemento(this, e);
                                VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada = this;
                                VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Entrada;
                                //if (EnDiagrama && DiseñoOperacion != null) VistaTextosInformacion.EstablecerTextoBotonSalida(DiseñoOperacion.ContieneSalida);
                                VistaTextosInformacion.DestacarElementoSeleccionado();
                            }
                            else
                            {
                                VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover = this;
                            }
                        }
                    }
                    else
                    {
                        if (!VistaOperacion.ElementosDiagramaSeleccionados.Any() ||
                            DiseñoElementoOperacion == null)
                        {
                            if (DiseñoElementoOperacion == null)
                                VistaOperacion.diagrama_MouseLeftButtonDown(this, e);

                            VistaOperacion.ElementoSeleccionado_Bool = true;
                            //VistaOperacion.EstablecerPuntoUbicacionElemento(this, e);
                            VistaOperacion.EntradaSeleccionada = this;
                            VistaOperacion.TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Entrada;
                            if (EnDiagramaOperacion && DiseñoElementoOperacion != null) VistaOperacion.EstablecerTextoBotonSalida(DiseñoElementoOperacion.ContieneSalida);
                            VistaOperacion.DestacarElementoSeleccionado();
                            VistaOperacion.ElementoSeleccionado = DiseñoElementoOperacion;
                            VistaOperacion.MostrarOrdenOperando_Elemento(DiseñoElementoOperacion);
                            VistaOperacion.MostrarInfo_Elemento(DiseñoElementoOperacion);
                            VistaOperacion.MostrarAcumulacion();
                        }
                        else
                        {
                            VistaOperacion.ElementoDiagramaSeleccionadoMover = this;
                        }
                    }

                    if (!clickeado)
                        DragDrop.DoDragDrop(this, entr, DragDropEffects.Move);
                }
                else
                {
                    if ((VistaOperaciones != null && VistaOperaciones.ClicDiagrama) | (VistaOperacion != null && VistaOperacion.ClicDiagrama)
                        | (VistaTextosInformacion != null && VistaTextosInformacion.ClicDiagrama))
                    {
                        Background = SystemColors.HighlightBrush;
                        botonFondo.Background = SystemColors.HighlightBrush;
                    }
                }
            }
        }

        private void botonFondo_Click(object sender, RoutedEventArgs e)
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null && ObtenerVentana().popup.Child.IsMouseOver) || ObtenerVentana().popup.Child == null))
            {
                return;
            }

            if (DiseñoOperacion == null && VistaOperaciones != null)
            {
                VistaOperaciones.AgregarEntrada(sender, null);                
            }
            else if(DiseñoElementoOperacion == null && VistaOperacion != null)
            {
                VistaOperacion.AgregarEntrada(sender, null);
            }
            else if( DiseñoTextosInformacion == null && VistaTextosInformacion != null)
            {
                VistaTextosInformacion.AgregarEntrada(sender, null);
            }
        }

        private void MostrarOcultarEdicion(bool mostrar)
        {
            //OcultarIconos();

            if (mostrar)
            {
                grilla.Visibility = Visibility.Collapsed;
                edicionNombre.Text = DiseñoElementoOperacion.NombreElemento;
                edicionNombre.Visibility = Visibility.Visible;
                btnGuardarNombre.Visibility = Visibility.Visible;
                edicionNombre.Focus();
            }
            else
            {
                btnGuardarNombre.Visibility = Visibility.Collapsed;
                edicionNombre.Visibility = Visibility.Collapsed;
                grilla.Visibility = Visibility.Visible;
            }
        }

        private void MostrarOcultarEdicion_Entrada(bool mostrar)
        {
            //OcultarIconos();
            if (VistaOperaciones != null &&
                VistaOperaciones.CalculoDiseñoSeleccionado != null)
            {
                if (mostrar)
                {
                    grilla.Visibility = Visibility.Collapsed;
                    edicionNombre_Entrada.Text = DiseñoOperacion?.EntradaRelacionada?.NombreCombo;
                    edicionNombre_Entrada.Visibility = Visibility.Visible;
                    btnGuardarNombre_Entrada.Visibility = Visibility.Visible;
                    edicionNombre_Entrada.Focus();
                }
                else
                {
                    btnGuardarNombre_Entrada.Visibility = Visibility.Collapsed;
                    edicionNombre_Entrada.Visibility = Visibility.Collapsed;
                    grilla.Visibility = Visibility.Visible;
                }
            }
        }

        private void botonFondo_Drop(object sender, DragEventArgs e)
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null && ObtenerVentana().popup.Child.IsMouseOver) || ObtenerVentana().popup.Child == null))
            {
                return;
            }

            botonFondo_Click(sender, e);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DiseñoElementoOperacion != null)
            {
                DiseñoElementoOperacion.Anchura = ActualWidth;
                DiseñoElementoOperacion.Altura = ActualHeight;
            }
            else if (DiseñoOperacion != null)
            {
                DiseñoOperacion.Anchura = ActualWidth;
                DiseñoOperacion.Altura = ActualHeight;
            }

            if (entr.NombreEditado)
            {
                entr.NombreEditado = false;

                if (DiseñoElementoOperacion != null)
                {
                    VistaOperacion.DibujarElementosDiseñoOperacion();
                }
                else if (DiseñoOperacion != null)
                {
                    if (VistaTextosInformacion != null)
                    {
                        VistaTextosInformacion.DibujarElementosTextosInformacion(VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades);
                    }

                    if (VistaOperaciones != null)
                    {
                        VistaOperaciones.DibujarElementosOperaciones();
                    }
                }
            }
        }

        private void btnGuardarNombre_Click(object sender, RoutedEventArgs e)
        {
            DiseñoElementoOperacion encontrado = (from E in VistaOperacion.Operacion.ElementosDiseñoOperacion
                                                  where E != DiseñoElementoOperacion && ((E.NombreElemento != null && (E.NombreElemento.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())) |
(E.Nombre != null && (E.Nombre.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())))
                                                  select E).FirstOrDefault();
            if (encontrado == null)
            {
                DiseñoElementoOperacion.NombreElemento = edicionNombre.Text;
                EstablecerNombre(DiseñoElementoOperacion.EntradaRelacionada);
                MostrarOcultarEdicion(false);
                VistaOperacion.Ventana.ActualizarNombresDescripcionesElementoOperacion(DiseñoElementoOperacion);
                //VistaOpciones.CambioTamañoOpcionOperacion(this, null);                
                //VistaOpciones.DibujarElementosDiseñoOperacion();
                UserControl_SizeChanged(this, null);
            }
            else
            {
                MessageBox.Show("Ya existe una variable, vector u operación con este nombre.", "Variable, vector u operación existente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void edicionNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter | e.Key == Key.Return)
                btnGuardarNombre_Click(this, null);
        }

        private void edicionNombre_Entrada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter | e.Key == Key.Return)
                btnGuardarNombre_Entrada_Click(this, null);
        }

        private void btnGuardarNombre_Entrada_Click(object sender, RoutedEventArgs e)
        {
            if (VistaOperaciones != null &&
                VistaOperaciones.CalculoDiseñoSeleccionado != null)
            {
                DiseñoOperacion encontrado = (from E in VistaOperaciones.CalculoDiseñoSeleccionado.ElementosOperaciones
                                              where E != DiseñoOperacion && ((E.NombreCombo != null && (E.NombreCombo.ToLower().Trim() == edicionNombre_Entrada.Text.ToLower().Trim())) |
    (E.Nombre != null && (E.Nombre.ToLower().Trim() == edicionNombre_Entrada.Text.ToLower().Trim())))
                                              select E).FirstOrDefault();
                if (encontrado == null)
                {
                    DiseñoOperacion.EntradaRelacionada.Nombre = edicionNombre_Entrada.Text;
                    EstablecerNombre(DiseñoOperacion.EntradaRelacionada);
                    MostrarOcultarEdicion_Entrada(false);
                    VistaOperaciones.Ventana.ActualizarNombresDescripcionesEntrada(DiseñoOperacion.EntradaRelacionada);
                    //VistaOpciones.CambioTamañoOpcionOperacion(this, null);
                    //VistaOpciones.DibujarElementosDiseñoOperacion();
                    entr.NombreEditado = true;
                    //UserControl_SizeChanged(this, null);                
                }
                else
                {
                    MessageBox.Show("Ya existe una variable, vector u operación con este nombre.", "Variable, vector u operación existente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
