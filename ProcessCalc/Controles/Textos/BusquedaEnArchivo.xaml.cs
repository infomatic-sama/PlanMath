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
using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
using ProcessCalc.Controles;
using ProcessCalc.Ventanas;
using ProcessCalc.Controles.Textos;
using ProcessCalc.Entidades.Entradas;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para BusquedaEnArchivo.xaml
    /// </summary>
    public partial class BusquedaEnArchivo : UserControl
    {
        private BusquedaTextoArchivo busq;
        public BusquedaTextoArchivo Busqueda
        {
            get
            {
                return busq;
            }
            set
            {
                opcionGeneracionFilas.GroupName = "OpcionesFilasTextosInformacion-" + value.GetHashCode().ToString();
                opcionGeneracionFilas_BusquedasConjuntos.GroupName = "OpcionesFilasTextosInformacion-" + value.GetHashCode().ToString();
                opcionGeneracionFilas_IteracionBusquedasConjuntos.GroupName = "OpcionesFilasTextosInformacion-" + value.GetHashCode().ToString();
                opcionGeneracionFilasBusqueda.GroupName = "OpcionesFilasTextosInformacion-" + value.GetHashCode().ToString();
                opcionNoGeneracionFilas.GroupName = "OpcionesFilasTextosInformacion-" + value.GetHashCode().ToString();

                nombreBusqueda.Text = value.Nombre;
                descripcionBusqueda.Text = value.Descripcion;
                busq = value;

                chkEsConjuntoBusquedas.IsChecked = value.EsConjuntoBusquedas;
                MostrarOcultarOpcionesFilas();
                opcionGeneracionFilas.IsChecked = value.GenerarFilasTextosInformacion_PorCadaElemento;
                opcionGeneracionFilas_BusquedasConjuntos.IsChecked = value.GenerarFilasTextosInformacion_PorCadaElemento_BusquedasConjunto;
                opcionGeneracionFilas_IteracionBusquedasConjuntos.IsChecked = value.GenerarFilasTextosInformacion_PorCadaIteracion_BusquedasConjunto;
                opcionNoGeneracionFilas.IsChecked = value.NoGenerarFilasTextosInformacion;

                if (!value.GenerarFilasTextosInformacion_PorCadaElemento &
                    !value.GenerarFilasTextosInformacion_PorCadaElemento_BusquedasConjunto &
                    !value.GenerarFilasTextosInformacion_PorCadaIteracion_BusquedasConjunto &
                    !value.NoGenerarFilasTextosInformacion)
                    opcionGeneracionFilasBusqueda.IsChecked = true;

                chkIncluirTextosInformacion_BusquedaContenedora.IsChecked = value.IncluirTextosInformacion_BusquedaContenedora;
                opcionEjecutarBusquedaCabeceraIteraciones.IsChecked = value.EjecutarBusquedaCabeceraIteraciones;

                opcionHastaFinalArchivo.GroupName = "Repetir-" + value.GetHashCode().ToString();
                opcionMientrasCondicionesCumplan.GroupName = "Repetir-" + value.GetHashCode().ToString();
                opcionHastaCondicionesCumplan.GroupName = "Repetir-" + value.GetHashCode().ToString();
                opcionNveces.GroupName = "Repetir-" + value.GetHashCode().ToString();

                if (value.EsConjuntoBusquedas)
                {
                    switch (value.FinalizacionConjuntoBusquedas)
                    {
                        case OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo:
                            opcionHastaFinalArchivo.IsChecked = true;
                            break;

                        case OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida:
                            opcionMientrasCondicionesCumplan.IsChecked = true;
                            break;

                        case OpcionFinBusquedaTexto_Archivos.EncontrarNveces:
                            opcionNveces.IsChecked = true;
                            break;

                        case OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida:
                            opcionHastaCondicionesCumplan.IsChecked = true;
                            break;
                    }
                    //if (value.FinalizacionConjuntoBusquedas == OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo)
                    //    opcionHastaFinalArchivo.IsChecked = true;

                    //if (value.FinalizacionConjuntoBusquedas == OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida)
                    //    opcionMientrasCondicionesCumplan.IsChecked = true;

                    //if (value.FinalizacionConjuntoBusquedas == OpcionFinBusquedaTexto_Archivos.EncontrarNveces)
                    //    opcionNveces.IsChecked = true;

                    txtVeces.Text = value.NumeroVecesConjuntoBusquedas.ToString();
                    opcionEstablecerNombresCantidadesBusquedasIteracion.IsChecked = value.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas;
                    opcionEstablecerNombresCantidadesIteracion.IsChecked = value.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas;

                    ListarBusquedas();
                }
            }
        }
        public VistaArchivoEntradaConjuntoNumeros VistaBusquedasArchivo { get; set; }
        public VistaArchivoEntradaTextosInformacion VistaBusquedasArchivo_TextosInformacion { get; set; }
        public VistaURLEntradaConjuntoNumeros VistaBusquedasURL { get; set; }
        public VistaURLEntradaTextosInformacion VistaBusquedasURL_TextosInformacion { get; set; }
        public BusquedaEnArchivo VistaBusquedas { get; set; }
        public bool Quitada { get; set; }
        public bool Agregada { get; set; }
        public Entrada Entrada { get; set; }
        public CondicionConjuntoBusquedas CondicionSeleccionada { get; set; }
        public BusquedaEnArchivo()
        {
            InitializeComponent();
        }

        public void ListarBusquedas()
        {
            contenedorBusquedasConjunto.Children.Clear();

            foreach (var itemBusqueda in busq.ConjuntoBusquedas)
            {
                BusquedaEnArchivo busquedaNumero = new BusquedaEnArchivo();
                busquedaNumero.chkIncluirTextosInformacion_BusquedaContenedora.Visibility = Visibility.Visible;
                busquedaNumero.Entrada = Entrada;
                busquedaNumero.Busqueda = itemBusqueda;                
                busquedaNumero.VistaBusquedas = this;
                busquedaNumero.MostrarOcultarOpcionesTextosInformacion();

                if (VistaBusquedasArchivo != null)
                {
                    busquedaNumero.VistaBusquedasArchivo = VistaBusquedasArchivo;
                }
                else if (VistaBusquedasArchivo_TextosInformacion != null)
                {
                    busquedaNumero.VistaBusquedasArchivo_TextosInformacion = VistaBusquedasArchivo_TextosInformacion;
                }
                else if (VistaBusquedasURL != null)
                {
                    busquedaNumero.VistaBusquedasURL = VistaBusquedasURL;
                }
                else if (VistaBusquedasURL_TextosInformacion != null)
                {
                    busquedaNumero.VistaBusquedasURL_TextosInformacion = VistaBusquedasURL_TextosInformacion;
                }

                contenedorBusquedasConjunto.Children.Add(busquedaNumero);
            }
        }

        private void nombreBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (busq != null)
            {
                busq.Nombre = nombreBusqueda.Text;

                if (VistaBusquedasArchivo != null)
                    VistaBusquedasArchivo.ActualizarNombreBusqueda(busq);
                else if (VistaBusquedasArchivo_TextosInformacion != null)
                    VistaBusquedasArchivo_TextosInformacion.ActualizarNombreBusqueda(busq);
                else if (VistaBusquedasURL != null)
                    VistaBusquedasURL.ActualizarNombreBusqueda(busq);
                else if (VistaBusquedasURL_TextosInformacion != null)
                    VistaBusquedasURL_TextosInformacion.ActualizarNombreBusqueda(busq);
            }
        }

        private void descripcionBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (busq != null) busq.Descripcion = descripcionBusqueda.Text;
        }
       
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Quitada) return;
            if (Agregada)
            {
                Agregada = false;
                return;
            }

            TextoEnArchivo busquedaNumero = new TextoEnArchivo();
            busquedaNumero.Busqueda = busq;

            if (busq.TextoBusquedaNumero.Contains(busquedaNumero.ObtenerCadenaFormatoNumeroGuardar()))
            {
                busquedaNumero.opcionesTextosInformacion.Visibility = Visibility.Collapsed;
                busquedaNumero.opcionNumeroActual.IsChecked = true;
                busq.OpcionTextosInformacion = OpcionTextosInformacionBusqueda.NumeroActual;
            }

            switch (busquedaNumero.Busqueda.OpcionTextosInformacion)
            {
                case OpcionTextosInformacionBusqueda.NumeroActual:
                    busquedaNumero.opcionNumeroActual.IsChecked = true;
                    break;

                case OpcionTextosInformacionBusqueda.UltimoNumeroEncontrado:
                    busquedaNumero.opcionUltimoNumeroEncontrado.IsChecked = true;
                    break;

                case OpcionTextosInformacionBusqueda.SiguienteNumeroAEncontrar:
                    busquedaNumero.opcionSiguienteNumeroAEncontrar.IsChecked = true;
                    break;
            }

            if (busq.TextoBusquedaNumero != null)
            {
                busquedaNumero.textoArchivo.Text = busq.TextoBusquedaNumero.Replace(busquedaNumero.ObtenerCadenaFormatoNumeroGuardar(), busquedaNumero.ObtenerCadenaFormatoNumero());
                busquedaNumero.textoArchivo.Text = busquedaNumero.textoArchivo.Text.Replace(busquedaNumero.ObtenerCadenaFormatoDatosGuardar(), busquedaNumero.ObtenerCadenaFormatoNumero());
                busquedaNumero.textoArchivo.Text = busquedaNumero.textoArchivo.Text.Replace(busquedaNumero.ObtenerCadenaFormatoTextosGuardar(), busquedaNumero.ObtenerCadenaFormatoNumero());
            }
            busquedaNumero.txtVeces.Text = busq.NumeroVecesBusquedaNumero.ToString();
            busquedaNumero.nombreBusqueda.Text = busq.Nombre;
            busquedaNumero.opcionUsarCeros.IsChecked = busq.UsarCantidad_SiNohayNumeros;
            busquedaNumero.txtCantidadUtilizar_NoEncontrados.Text = busq.NumeroUtilizar_NoEncontrados.ToString();

            busquedaNumero.opcionAgregarTextosInformacion_NombresCantidades.IsChecked = true;
            busquedaNumero.opcionReemplazarTextosInformacion_NombresCantidades.IsChecked = busq.ReemplazarTextosInformacion_NombresCantidades;

            busquedaNumero.opcionAsignarTextosInformacionNumeros.SelectedItem = (from ComboBoxItem I in busquedaNumero.opcionAsignarTextosInformacionNumeros.Items where I.Uid == ((int)busq.OpcionAsignarTextosInformacion_Numeros).ToString() select I).FirstOrDefault();
            busquedaNumero.cantidadNumerosAsignarTextosInformacion.Text = busq.CantidadNumeros_TextosInformacion_AsignarNumeros.ToString();
            busquedaNumero.textosInformacion_SeleccionNumeros.TextosInformacion = busq.TextosInformacion_AsignarNumeros;

            busquedaNumero.busquedas_SeleccionNumeros.BusquedasSeleccionadas = busq.Busquedas_AsignarNumeros;

            busquedaNumero.opcionAsignarTextosInformacionNumeros_Iteraciones.SelectedItem = (from ComboBoxItem I in busquedaNumero.opcionAsignarTextosInformacionNumeros_Iteraciones.Items where I.Uid == ((int)busq.OpcionAsignarTextosInformacion_Numeros_Iteraciones).ToString() select I).FirstOrDefault();
            busquedaNumero.cantidadNumerosAsignarTextosInformacion_Iteraciones.Text = busq.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones.ToString();

            if (Entrada != null)
            {
                var ListaBusquedas = ObtenerBusquedasEntrada_AsignarNumeros((Entrada.Tipo == TipoEntrada.ConjuntoNumeros) ? Entrada.BusquedasConjuntoNumeros :
                    (Entrada.Tipo == TipoEntrada.TextosInformacion) ? Entrada.BusquedasTextosInformacion : new List<BusquedaTextoArchivo>());

                busquedaNumero.busquedas_SeleccionNumeros.BusquedasEntrada = ListaBusquedas;
            }
            else
                busquedaNumero.busquedas_SeleccionNumeros.BusquedasEntrada = new List<BusquedaTextoArchivo>();

            busquedaNumero.busquedas_SeleccionNumeros.BusquedaSeleccionada = busq;
            busquedaNumero.busquedas_SeleccionNumeros.OpcionTextosInformacion = busquedaNumero.Busqueda.OpcionTextosInformacion;
            busquedaNumero.busquedas_SeleccionNumeros.ListarBusquedas();

            if (VistaBusquedasArchivo != null)
            {
                VistaBusquedasArchivo.contenedorBusqueda.Children.Clear();
                VistaBusquedasArchivo.contenedorBusqueda.Children.Add(busquedaNumero);
                VistaBusquedasArchivo.MarcarBusquedaSeleccionada(this);
            }
            else if (VistaBusquedasArchivo_TextosInformacion != null)
            {
                busquedaNumero.ModoTextosInformacion = true;
                VistaBusquedasArchivo_TextosInformacion.contenedorBusqueda.Children.Clear();
                VistaBusquedasArchivo_TextosInformacion.contenedorBusqueda.Children.Add(busquedaNumero);
                VistaBusquedasArchivo_TextosInformacion.MarcarBusquedaSeleccionada(this);
            }
            else if (VistaBusquedasURL != null)
            {
                VistaBusquedasURL.contenedorBusqueda.Children.Clear();
                VistaBusquedasURL.contenedorBusqueda.Children.Add(busquedaNumero);
                VistaBusquedasURL.MarcarBusquedaSeleccionada(this);
            }
            else if (VistaBusquedasURL_TextosInformacion != null)
            {
                busquedaNumero.ModoTextosInformacion = true;
                VistaBusquedasURL_TextosInformacion.contenedorBusqueda.Children.Clear();
                VistaBusquedasURL_TextosInformacion.contenedorBusqueda.Children.Add(busquedaNumero);
                VistaBusquedasURL_TextosInformacion.MarcarBusquedaSeleccionada(this);
            }

            if (VistaBusquedas != null)
            {
                //VistaBusquedas.contenedorBusquedasConjunto.Children.Clear();
                //VistaBusquedas.contenedorBusquedasConjunto.Children.Add(busquedaNumero);
                VistaBusquedas.MarcarBusquedaSeleccionada(this);
            }
        }

        private List<BusquedaTextoArchivo> ObtenerBusquedasEntrada_AsignarNumeros(List<BusquedaTextoArchivo> lista)
        {
            List<BusquedaTextoArchivo> listaResultado = new List<BusquedaTextoArchivo>();

            foreach(var itemLista in lista)
            {
                listaResultado.Add(itemLista);
                listaResultado.AddRange(itemLista.ConjuntoBusquedas);
            }

            return listaResultado;
        }

        public void MostrarOcultarOpcionesTextosInformacion()
        {
            if (Entrada != null &&
                Entrada.Tipo != TipoEntrada.TextosInformacion)
            {
                chkIncluirTextosInformacion_BusquedaContenedora.Visibility = Visibility.Collapsed;
                opcionGeneracionFilas.Visibility = Visibility.Collapsed;
                opcionGeneracionFilasBusqueda.Visibility = Visibility.Collapsed;
                opcionGeneracionFilas_BusquedasConjuntos.Visibility = Visibility.Collapsed;
                opcionGeneracionFilas_IteracionBusquedasConjuntos.Visibility = Visibility.Collapsed;
                opcionNoGeneracionFilas.Visibility = Visibility.Collapsed;
            }
        }

        public void ButtonQuitar_Click(object sender, RoutedEventArgs e)
        {
            if (VistaBusquedasArchivo != null)
            {
                VistaBusquedasArchivo.contenedorBusqueda.Children.Clear();
                VistaBusquedasArchivo.MarcarBusquedaSeleccionada(null);
            }
            else if (VistaBusquedasArchivo_TextosInformacion != null)
            {
                VistaBusquedasArchivo_TextosInformacion.contenedorBusqueda.Children.Clear();
                VistaBusquedasArchivo_TextosInformacion.MarcarBusquedaSeleccionada(null);
            }
            else if (VistaBusquedasURL != null)
            {
                VistaBusquedasURL.contenedorBusqueda.Children.Clear();
                VistaBusquedasURL.MarcarBusquedaSeleccionada(null);
            }
            else if (VistaBusquedasURL_TextosInformacion != null)
            {
                VistaBusquedasURL_TextosInformacion.contenedorBusqueda.Children.Clear();
                VistaBusquedasURL_TextosInformacion.MarcarBusquedaSeleccionada(null);
            }

            if (VistaBusquedas != null)
            {
                //VistaBusquedas.contenedorBusquedasConjunto.Children.Clear();
                //VistaBusquedas.contenedorBusquedasConjunto.Children.Add(busquedaNumero);
                VistaBusquedas.MarcarBusquedaSeleccionada(null);
            }
        }

        public void AgregarBusqueda_DesdeVista(object sender, RoutedEventArgs e)
        {
            Button_Click(this, new RoutedEventArgs());
            Agregada = true;

            if (VistaBusquedasArchivo != null)
                VistaBusquedasArchivo.AgregarNuevaBusqueda();
            else if (VistaBusquedasArchivo_TextosInformacion != null)
                VistaBusquedasArchivo_TextosInformacion.AgregarNuevaBusqueda();
            else if (VistaBusquedasURL != null)
                VistaBusquedasURL.AgregarNuevaBusqueda();
            else if (VistaBusquedasURL_TextosInformacion != null)
                VistaBusquedasURL_TextosInformacion.AgregarNuevaBusqueda();            
        }

        public void ActivarDesactivarBotonOtraBusqueda(bool activar)
        {
            btnOtraBusqueda.IsEnabled = activar;
        }

        private void btnQuitarBusqueda_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Quitar la búsqueda de forma permanente?", "Quitar búsqueda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Quitada = true;
                                
                if (VistaBusquedasArchivo != null)
                    VistaBusquedasArchivo.QuitarBusqueda(this);
                else if (VistaBusquedasArchivo_TextosInformacion != null)
                    VistaBusquedasArchivo_TextosInformacion.QuitarBusqueda(this);
                else if (VistaBusquedasURL != null)
                    VistaBusquedasURL.QuitarBusqueda(this);
                else if (VistaBusquedasURL_TextosInformacion != null)
                    VistaBusquedasURL_TextosInformacion.QuitarBusqueda(this);
                
                ButtonQuitar_Click(this, new RoutedEventArgs());

            }
        }

        private void btnQuitarBusquedaInterna_Click(object sender, RoutedEventArgs e)
        {
            if (VistaBusquedas == null)
            {
                btnQuitarBusqueda_Click(sender, e);
            }
            else
            {
                if (MessageBox.Show("¿Quitar la búsqueda de forma permanente?", "Quitar búsqueda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Quitada = true;
                    VistaBusquedas.QuitarBusqueda(this);
                    
                    ButtonQuitar_Click(this, new RoutedEventArgs());
                }
            }
        }

        private void subir_Click(object sender, RoutedEventArgs e)
        {
            if (VistaBusquedas != null && VistaBusquedas.Busqueda != null && busq != null)
            {
                if (VistaBusquedas.Busqueda.ConjuntoBusquedas.Any() && 
                    busq != VistaBusquedas.Busqueda.ConjuntoBusquedas.First())
                {
                    int indiceElemento = VistaBusquedas.Busqueda.ConjuntoBusquedas.FindIndex((i) => i == busq);

                    var elemento = VistaBusquedas.Busqueda.ConjuntoBusquedas[indiceElemento];
                    var elementoAnterior = VistaBusquedas.Busqueda.ConjuntoBusquedas[indiceElemento - 1];

                    VistaBusquedas.Busqueda.ConjuntoBusquedas[indiceElemento] = elementoAnterior;
                    VistaBusquedas.Busqueda.ConjuntoBusquedas[indiceElemento - 1] = elemento;

                    VistaBusquedas.ListarBusquedas();
                }
            }
            else
            {
                if (VistaBusquedasArchivo != null && VistaBusquedasArchivo.Entrada != null && busq != null)
                {
                    if (VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros.Any() && 
                        busq != VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros.First())
                    {
                        int indiceElemento = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros.FindIndex((i) => i == busq);

                        var elemento = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros[indiceElemento];
                        var elementoAnterior = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros[indiceElemento - 1];

                        VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros[indiceElemento] = elementoAnterior;
                        VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros[indiceElemento - 1] = elemento;

                        VistaBusquedasArchivo.ListarBusquedas();
                    }
                }

                if (VistaBusquedasArchivo_TextosInformacion != null && VistaBusquedasArchivo_TextosInformacion.Entrada != null && busq != null)
                {
                    if (VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion.Any() && 
                        busq != VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion.First())
                    {
                        int indiceElemento = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion.FindIndex((i) => i == busq);

                        var elemento = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento];
                        var elementoAnterior = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento - 1];

                        VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento] = elementoAnterior;
                        VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento - 1] = elemento;

                        VistaBusquedasArchivo_TextosInformacion.ListarBusquedas();
                    }
                }

                if (VistaBusquedasURL != null && VistaBusquedasURL.Entrada != null && busq != null)
                {
                    if (VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros.Any() && 
                        busq != VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros.First())
                    {
                        int indiceElemento = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros.FindIndex((i) => i == busq);

                        var elemento = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros[indiceElemento];
                        var elementoAnterior = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros[indiceElemento - 1];

                        VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros[indiceElemento] = elementoAnterior;
                        VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros[indiceElemento - 1] = elemento;

                        VistaBusquedasURL.ListarBusquedas();
                    }
                }

                if (VistaBusquedasURL_TextosInformacion != null && VistaBusquedasURL_TextosInformacion.Entrada != null && busq != null)
                {
                    if (VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion.Any() && 
                        busq != VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion.First())
                    {
                        int indiceElemento = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion.FindIndex((i) => i == busq);

                        var elemento = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento];
                        var elementoAnterior = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento - 1];

                        VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento] = elementoAnterior;
                        VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento - 1] = elemento;

                        VistaBusquedasURL_TextosInformacion.ListarBusquedas();
                    }
                }

            }
        }

        private void bajar_Click(object sender, RoutedEventArgs e)
        {
            if (VistaBusquedas != null && VistaBusquedas.Busqueda != null && busq != null)
            {
                if (VistaBusquedas.Busqueda.ConjuntoBusquedas.Any() && 
                    busq != VistaBusquedas.Busqueda.ConjuntoBusquedas.Last())
                {
                    int indiceElemento = VistaBusquedas.Busqueda.ConjuntoBusquedas.FindIndex((i) => i == busq);

                    var elemento = VistaBusquedas.Busqueda.ConjuntoBusquedas[indiceElemento];
                    var elementoPosterior = VistaBusquedas.Busqueda.ConjuntoBusquedas[indiceElemento + 1];

                    VistaBusquedas.Busqueda.ConjuntoBusquedas[indiceElemento] = elementoPosterior;
                    VistaBusquedas.Busqueda.ConjuntoBusquedas[indiceElemento + 1] = elemento;

                    VistaBusquedas.ListarBusquedas();
                }
            }
            else
            {
                if (VistaBusquedasArchivo != null && VistaBusquedasArchivo.Entrada != null && busq != null)
                {
                    if (VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros.Any() && 
                        busq != VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros.Last())
                    {
                        int indiceElemento = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros.FindIndex((i) => i == busq);

                        var elemento = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros[indiceElemento];
                        var elementoPosterior = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros[indiceElemento + 1];

                        VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros[indiceElemento] = elementoPosterior;
                        VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros[indiceElemento + 1] = elemento;

                        VistaBusquedasArchivo.ListarBusquedas();
                    }
                }

                if (VistaBusquedasArchivo_TextosInformacion != null && VistaBusquedasArchivo_TextosInformacion.Entrada != null && busq != null)
                {
                    if (VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion.Any() && 
                        busq != VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion.Last())
                    {
                        int indiceElemento = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion.FindIndex((i) => i == busq);

                        var elemento = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento];
                        var elementoPosterior = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento + 1];

                        VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento] = elementoPosterior;
                        VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento + 1] = elemento;

                        VistaBusquedasArchivo_TextosInformacion.ListarBusquedas();
                    }
                }

                if (VistaBusquedasURL != null && VistaBusquedasURL.Entrada != null && busq != null)
                {
                    if (VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros.Any() && 
                        busq != VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros.Last())
                    {
                        int indiceElemento = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros.FindIndex((i) => i == busq);

                        var elemento = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros[indiceElemento];
                        var elementoPosterior = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros[indiceElemento + 1];

                        VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros[indiceElemento] = elementoPosterior;
                        VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros[indiceElemento + 1] = elemento;

                        VistaBusquedasURL.ListarBusquedas();
                    }
                }

                if (VistaBusquedasURL_TextosInformacion != null && VistaBusquedasURL_TextosInformacion.Entrada != null && busq != null)
                {
                    if (VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion.Any() && 
                        busq != VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion.Last())
                    {
                        int indiceElemento = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion.FindIndex((i) => i == busq);

                        var elemento = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento];
                        var elementoPosterior = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento + 1];

                        VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento] = elementoPosterior;
                        VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion[indiceElemento + 1] = elemento;

                        VistaBusquedasURL_TextosInformacion.ListarBusquedas();
                    }
                }

            }
        }

        private void chkEsConjuntoBusquedas_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.EsConjuntoBusquedas = (bool)chkEsConjuntoBusquedas.IsChecked;

                if(Busqueda.EsConjuntoBusquedas)
                {
                    lblOtraBusqueda.Visibility = Visibility.Visible;
                    btnOtraBusqueda.Visibility = Visibility.Visible;
                }
                else
                {
                    lblOtraBusqueda.Visibility = Visibility.Collapsed;
                    btnOtraBusqueda.Visibility = Visibility.Collapsed;
                }

                MostrarOcultarOpcionesFilas();

                //if (!Busqueda.ConjuntoBusquedas.Any())
                //    AgregarNuevaBusqueda();

                MostrarOcultarBusquedasConjunto(true);
            }
        }

        private void chkEsConjuntoBusquedas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.EsConjuntoBusquedas = (bool)chkEsConjuntoBusquedas.IsChecked;

                if (Busqueda.EsConjuntoBusquedas)
                {
                    lblOtraBusqueda.Visibility = Visibility.Visible;
                    btnOtraBusqueda.Visibility = Visibility.Visible;
                }
                else
                {
                    lblOtraBusqueda.Visibility = Visibility.Collapsed;
                    btnOtraBusqueda.Visibility = Visibility.Collapsed;
                    Busqueda.OpcionAsignarTextosInformacion_Numeros_Iteraciones = OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.TodasIteraciones;
                }

                MostrarOcultarOpcionesFilas();

                MostrarOcultarBusquedasConjunto(false);
            }
        }

        private void MostrarOcultarBusquedasConjunto(bool mostrar)
        {
            if (mostrar)
            {
                busquedasConjunto.Visibility = Visibility.Visible;
            }
            else
            {
                busquedasConjunto.Visibility = Visibility.Collapsed;
            }
        }

        private void MostrarOcultarOpcionesFilas()
        {
            if(Busqueda.EsConjuntoBusquedas)
            {
                if (Entrada != null &&
                Entrada.Tipo == TipoEntrada.TextosInformacion)
                {
                    opcionGeneracionFilas_BusquedasConjuntos.Visibility = Visibility.Visible;
                    opcionGeneracionFilas_IteracionBusquedasConjuntos.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionGeneracionFilas_BusquedasConjuntos.Visibility = Visibility.Collapsed;
                    opcionGeneracionFilas_IteracionBusquedasConjuntos.Visibility = Visibility.Collapsed;
                }
                opcionGeneracionFilas.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (Entrada != null &&
                Entrada.Tipo == TipoEntrada.TextosInformacion)
                    opcionGeneracionFilas.Visibility = Visibility.Visible;
                else
                    opcionGeneracionFilas.Visibility = Visibility.Collapsed;
                opcionGeneracionFilas_BusquedasConjuntos.Visibility = Visibility.Collapsed;
                opcionGeneracionFilas_IteracionBusquedasConjuntos.Visibility = Visibility.Collapsed;
            }
        }

        private void btnBusquedaInterna_Click(object sender, RoutedEventArgs e)
        {
            Button_Click(this, new RoutedEventArgs());
            Agregada = true;

            AgregarNuevaBusqueda();
        }

        private void opcionNveces_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionNveces.IsChecked == true)
                    Busqueda.FinalizacionConjuntoBusquedas = OpcionFinBusquedaTexto_Archivos.EncontrarNveces;
            }
        }

        private void opcionHastaFinalArchivo_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionHastaFinalArchivo.IsChecked == true)
                    Busqueda.FinalizacionConjuntoBusquedas = OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo;
            }
        }

        private void opcionMientrasCondicionesCumplan_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionMientrasCondicionesCumplan.IsChecked == true)
                {
                    Busqueda.FinalizacionConjuntoBusquedas = OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida;
                    estructuraCondiciones.Visibility = Visibility.Visible;
                    ListarCondicionesConjunto();
                }
            }
        }

        private void opcionHastaCondicionesCumplan_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionHastaCondicionesCumplan.IsChecked == true)
                {
                    Busqueda.FinalizacionConjuntoBusquedas = OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida;
                    estructuraCondiciones.Visibility = Visibility.Visible;
                    ListarCondicionesConjunto();
                }
            }
        }

        private void txtVeces_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numeroVeces = 0;
            int.TryParse(txtVeces.Text, out numeroVeces);
            if (Busqueda != null) Busqueda.NumeroVecesConjuntoBusquedas = numeroVeces;
        }

        public void MarcarBusquedaSeleccionada(BusquedaEnArchivo busquedaSeleccionada)
        {
            foreach (var busqueda in contenedorBusquedasConjunto.Children)
            {
                if (busquedaSeleccionada != null && 
                    busqueda == busquedaSeleccionada)
                {
                    ((BusquedaEnArchivo)busqueda).Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else
                {
                    ((BusquedaEnArchivo)busqueda).Background = null;
                }
            }
        }

        public void AgregarNuevaBusqueda()
        {
            BusquedaTextoArchivo nueva = new BusquedaTextoArchivo();
            busq.ConjuntoBusquedas.Add(nueva);
            nueva.Nombre = "Búsqueda de conjunto " + busq.ConjuntoBusquedas.Count;

            BusquedaEnArchivo nuevaBusqueda = new BusquedaEnArchivo();
            nuevaBusqueda.chkIncluirTextosInformacion_BusquedaContenedora.Visibility = Visibility.Visible;
            nuevaBusqueda.Entrada = Entrada;
            nuevaBusqueda.Busqueda = nueva;
            nuevaBusqueda.VistaBusquedas = this;
            nuevaBusqueda.MostrarOcultarOpcionesTextosInformacion();

            if (VistaBusquedasArchivo != null)
            {
                nuevaBusqueda.VistaBusquedasArchivo = VistaBusquedasArchivo;
            }
            else if (VistaBusquedasArchivo_TextosInformacion != null)
            {
                nuevaBusqueda.VistaBusquedasArchivo_TextosInformacion = VistaBusquedasArchivo_TextosInformacion;
            }
            else if (VistaBusquedasURL != null)
            {
                nuevaBusqueda.VistaBusquedasURL = VistaBusquedasURL;
            }
            else if (VistaBusquedasURL_TextosInformacion != null)
            {
                nuevaBusqueda.VistaBusquedasURL_TextosInformacion = VistaBusquedasURL_TextosInformacion;
            }

            contenedorBusquedasConjunto.Children.Add(nuevaBusqueda);
            nuevaBusqueda.Button_Click(this, new RoutedEventArgs());
        }

        public void QuitarBusqueda(BusquedaEnArchivo busqueda)
        {
            busq.ConjuntoBusquedas.Remove(busqueda.Busqueda);
            contenedorBusquedasConjunto.Children.Remove(busqueda);

            if (VistaBusquedasArchivo != null)
            {
                VistaBusquedasArchivo.contenedorBusqueda.Children.Clear();
                VistaBusquedasArchivo.QuitarBusquedas_LecturasNavegaciones(busqueda.Busqueda);
            }
            else if (VistaBusquedasArchivo_TextosInformacion != null)
            {
                VistaBusquedasArchivo_TextosInformacion.contenedorBusqueda.Children.Clear();
                VistaBusquedasArchivo_TextosInformacion.QuitarBusquedas_LecturasNavegaciones(busqueda.Busqueda);
            }
            else if (VistaBusquedasURL != null)
            {
                VistaBusquedasURL.contenedorBusqueda.Children.Clear();
                VistaBusquedasURL.QuitarBusquedas_LecturasNavegaciones(busqueda.Busqueda);
            }
            else if (VistaBusquedasURL_TextosInformacion != null)
            {
                VistaBusquedasURL_TextosInformacion.contenedorBusqueda.Children.Clear();
                VistaBusquedasURL_TextosInformacion.QuitarBusquedas_LecturasNavegaciones(busqueda.Busqueda);
            }
        }
        
        private void opcionMientrasCondicionesCumplan_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionMientrasCondicionesCumplan.IsChecked == false)
                {
                    estructuraCondiciones.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionHastaCondicionesCumplan_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionHastaCondicionesCumplan.IsChecked == false)
                {
                    estructuraCondiciones.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionConjuntoBusquedas agregar = new AgregarQuitar_CondicionConjuntoBusquedas();

            if (VistaBusquedasArchivo != null)
            {
                agregar.ListaBusquedas = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros;
            }
            else if (VistaBusquedasArchivo_TextosInformacion != null)
            {
                agregar.ListaBusquedas = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion;
            }
            else if (VistaBusquedasURL != null)
            {
                agregar.ListaBusquedas = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros;
            }
            else if (VistaBusquedasURL_TextosInformacion != null)
            {
                agregar.ListaBusquedas = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion;
            }

            agregar.ShowDialog();

            if (agregar.Aceptar)
            {
                agregar.Condicion.CondicionContenedora = CondicionSeleccionada;

                if (CondicionSeleccionada != null)
                {                    
                    CondicionSeleccionada.Condiciones.Add(agregar.Condicion);
                }
                else
                {
                    if (Busqueda.Condiciones == null)
                    {
                        Busqueda.Condiciones = agregar.Condicion;
                    }
                    else
                    {
                        agregar.Condicion.CondicionContenedora = Busqueda.Condiciones;
                        Busqueda.Condiciones.Condiciones.Add(agregar.Condicion);
                    }
                }

                ListarCondicionesConjunto();
            }
        }

        private void ListarCondicionesConjunto()
        {
            listaCondiciones.Children.Clear();

            //foreach (var itemCondicion in Busqueda.Condiciones)
            //{
            if (Busqueda.Condiciones != null)
            {
                EtiquetaCondicionConjuntoBusquedas etiquetaCondicion = new EtiquetaCondicionConjuntoBusquedas();
                etiquetaCondicion.VistaBusquedas = this;
                etiquetaCondicion.Condicion = Busqueda.Condiciones;
                listaCondiciones.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
            }
            //}
        }

        public void DesmarcarCondicionesBusquedas()
        {
            foreach (EtiquetaCondicionConjuntoBusquedas itemCondicion in listaCondiciones.Children)
            {
                itemCondicion.DesmarcarSeleccion();
            }
        }

        private void editarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                AgregarQuitar_CondicionConjuntoBusquedas editar = new AgregarQuitar_CondicionConjuntoBusquedas();

                if (VistaBusquedasArchivo != null)
                {
                    editar.ListaBusquedas = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros;
                }
                else if (VistaBusquedasArchivo_TextosInformacion != null)
                {
                    editar.ListaBusquedas = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion;
                }
                else if (VistaBusquedasURL != null)
                {
                    editar.ListaBusquedas = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros;
                }
                else if (VistaBusquedasURL_TextosInformacion != null)
                {
                    editar.ListaBusquedas = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion;
                }

                editar.ModoEdicion = true;
                editar.Condicion = CondicionSeleccionada;
                editar.ShowDialog();

                if (editar.Aceptar)
                {
                    ListarCondicionesConjunto();
                }
            }
        }

        private void quitarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                if (CondicionSeleccionada.CondicionContenedora != null)
                    CondicionSeleccionada.CondicionContenedora.Condiciones.Remove(CondicionSeleccionada);
                else
                    Busqueda.Condiciones = null;

                CondicionSeleccionada.CondicionContenedora = null;
                CondicionSeleccionada = null;
                ListarCondicionesConjunto();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //listaCondiciones.MaxWidth = listaCondiciones.ActualWidth;
            //estructuraCondiciones.MaxWidth = estructuraCondiciones.ActualWidth;z
            //MaxWidth = ActualWidth;
            //contenedorGrilla.MaxWidth = ActualWidth;
            //busquedasConjunto.MaxWidth = ActualWidth;
            //contenedorCondicionesConjunto.MaxWidth = ActualWidth;
            //contenedorCondicionesNVeces.MaxWidth = ActualWidth;
            //estructuraCondiciones.MaxWidth = ActualWidth;
            //contenedorListaCondiciones.MaxWidth = ActualWidth;
            //listaCondiciones.MaxWidth = ActualWidth;

            //opcionMientrasCondicionesCumplan_Checked(this, null);
            //opcionHastaCondicionesCumplan_Checked(this, null);
        }

        private void moverCondicionAIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                if (CondicionSeleccionada.CondicionContenedora != null)
                {
                    int indice = CondicionSeleccionada.CondicionContenedora.Condiciones.IndexOf(CondicionSeleccionada);

                    if (indice - 1 == -1)
                    {                        
                        CondicionConjuntoBusquedas condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);

                            if (condicionContenedoraDestino != null)
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                condicionContenedoraDestino.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionContenedoraDestino;

                                ListarCondicionesConjunto();
                            }
                        }
                        else
                        {
                            int indiceCondicionContenedora = -1;
                            if (CondicionSeleccionada.CondicionContenedora != Busqueda.Condiciones)
                                indiceCondicionContenedora = Busqueda.Condiciones.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Busqueda.Condiciones.Condiciones.IndexOf(CondicionSeleccionada);

                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            if(indiceCondicionContenedora > -1)
                                Busqueda.Condiciones.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionada);                            
                            else
                                Busqueda.Condiciones.Condiciones.Add(CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = Busqueda.Condiciones;

                            ListarCondicionesConjunto();                            
                        }
                    }
                    else
                    {
                        CondicionConjuntoBusquedas condicionAnterior = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice - 1);

                        if (condicionAnterior.Condiciones.Any())
                        {
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionAnterior.Condiciones.Add(CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = condicionAnterior;

                            ListarCondicionesConjunto();
                        }
                        else
                        {
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            CondicionSeleccionada.CondicionContenedora.Condiciones.Insert(indice - 1, CondicionSeleccionada);

                            ListarCondicionesConjunto();
                        }
                    }
                }
            }
        }

        private void moverCondicionADerecha_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                if (CondicionSeleccionada.CondicionContenedora != null)
                {
                    int indice = CondicionSeleccionada.CondicionContenedora.Condiciones.IndexOf(CondicionSeleccionada);

                    if (indice + 1 == CondicionSeleccionada.CondicionContenedora.Condiciones.Count)
                    {
                        CondicionConjuntoBusquedas condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            
                            CondicionConjuntoBusquedas condicionSiguiente = null;
                            bool agregarFinal = false;
                            if (indiceCondicionContenedora + 1 == condicionContenedoraDestino.Condiciones.Count)
                            {
                                condicionSiguiente = condicionContenedoraDestino.Condiciones.Last();
                                if (condicionSiguiente == CondicionSeleccionada.CondicionContenedora)
                                {
                                    condicionSiguiente = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;
                                    agregarFinal = true;
                                }
                            }
                            else
                                condicionSiguiente = condicionContenedoraDestino.Condiciones.ElementAt(indiceCondicionContenedora + 1);

                            if (condicionSiguiente.Condiciones.Any())
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                if(agregarFinal)
                                    condicionSiguiente.Condiciones.Add(CondicionSeleccionada);
                                else
                                    condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionSiguiente;

                                ListarCondicionesConjunto();
                            }
                            else
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                condicionContenedoraDestino.Condiciones.Add(CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionContenedoraDestino;
                            }
                        }
                        else
                        {
                            int indiceCondicionContenedora = -1;
                            if (CondicionSeleccionada.CondicionContenedora != Busqueda.Condiciones)
                                indiceCondicionContenedora = Busqueda.Condiciones.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Busqueda.Condiciones.Condiciones.IndexOf(CondicionSeleccionada);

                            CondicionConjuntoBusquedas condicionSiguiente = null;
                            bool agregarFinal = false;
                            if (indiceCondicionContenedora + 1 == Busqueda.Condiciones.Condiciones.Count)
                            {
                                condicionSiguiente = Busqueda.Condiciones.Condiciones.Last();
                                if (condicionSiguiente == CondicionSeleccionada.CondicionContenedora)
                                {
                                   agregarFinal = true;
                                }
                            }
                            else
                                condicionSiguiente = Busqueda.Condiciones.Condiciones.ElementAt(indiceCondicionContenedora + 1);

                            if (condicionSiguiente.Condiciones.Any())
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                if(agregarFinal)
                                    condicionSiguiente.Condiciones.Add(CondicionSeleccionada);
                                else
                                    condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionSiguiente;

                                ListarCondicionesConjunto();
                            }
                            else
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                Busqueda.Condiciones.Condiciones.Add(CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = Busqueda.Condiciones;
                            }

                            ListarCondicionesConjunto();
                        }
                    }
                    else
                    {
                        CondicionConjuntoBusquedas condicionSiguiente = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice + 1);

                        if (condicionSiguiente.Condiciones.Any())
                        {
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = condicionSiguiente;

                            ListarCondicionesConjunto();
                        }
                        else
                        {
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            CondicionSeleccionada.CondicionContenedora.Condiciones.Insert(indice + 1, CondicionSeleccionada);

                            ListarCondicionesConjunto();
                        }
                    }
                }
            }            
        }

        private void opcionEstablecerNombresCantidadesIteracion_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas = (bool)opcionEstablecerNombresCantidadesIteracion.IsChecked;
            }
        }

        private void opcionEstablecerNombresCantidadesIteracion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas = (bool)opcionEstablecerNombresCantidadesIteracion.IsChecked;
            }
        }

        private void opcionEstablecerNombresCantidadesBusquedasIteracion_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas = (bool)opcionEstablecerNombresCantidadesBusquedasIteracion.IsChecked;
            }
        }

        private void opcionEstablecerNombresCantidadesBusquedasIteracion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas = (bool)opcionEstablecerNombresCantidadesBusquedasIteracion.IsChecked;
            }
        }

        private void btnDefinirTextosInformacionIteracion_Click(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                SeleccionarOrdenarCantidades seleccionarOrdenar = new SeleccionarOrdenarCantidades();
                seleccionarOrdenar.listaTextos.TextosInformacion = Busqueda.TextosInformacionFijos_ConjuntoBusquedas.ToList();

                bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                if (definicionEstablecida)
                {
                    Busqueda.TextosInformacionFijos_ConjuntoBusquedas = seleccionarOrdenar.listaTextos.TextosInformacion;
                }
            }
        }

        private void opcionesNombresCantidadesIteracion_Click(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (Busqueda.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas == null)
                    Busqueda.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas = new Entidades.TextosInformacion.DefinicionTextoNombresCantidades();

                ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
                establecer.TextosNombre = Busqueda.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas.ReplicarObjeto();
                
                if(establecer.ShowDialog() == true)
                {
                    Busqueda.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas = establecer.TextosNombre;
                }
            }
        }

        private void chkIncluirTextosInformacion_BusquedaContenedora_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.IncluirTextosInformacion_BusquedaContenedora = (bool)chkIncluirTextosInformacion_BusquedaContenedora.IsChecked;
            }
        }

        private void chkIncluirTextosInformacion_BusquedaContenedora_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.IncluirTextosInformacion_BusquedaContenedora = (bool)chkIncluirTextosInformacion_BusquedaContenedora.IsChecked;
            }
        }

        private void opcionGeneracionFilas_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.GenerarFilasTextosInformacion_PorCadaElemento = (bool)opcionGeneracionFilas.IsChecked;
            }
        }

        private void opcionGeneracionFilas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.GenerarFilasTextosInformacion_PorCadaElemento = (bool)opcionGeneracionFilas.IsChecked;
            }
        }

        private void opcionGeneracionFilas_BusquedasConjuntos_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.GenerarFilasTextosInformacion_PorCadaElemento_BusquedasConjunto = (bool)opcionGeneracionFilas_BusquedasConjuntos.IsChecked;
            }
        }

        private void opcionGeneracionFilas_BusquedasConjuntos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.GenerarFilasTextosInformacion_PorCadaElemento_BusquedasConjunto = (bool)opcionGeneracionFilas_BusquedasConjuntos.IsChecked;
            }
        }

        private void opcionGeneracionFilas_IteracionBusquedasConjuntos_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.GenerarFilasTextosInformacion_PorCadaIteracion_BusquedasConjunto = (bool)opcionGeneracionFilas_IteracionBusquedasConjuntos.IsChecked;
            }
        }

        private void opcionGeneracionFilas_IteracionBusquedasConjuntos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.GenerarFilasTextosInformacion_PorCadaIteracion_BusquedasConjunto = (bool)opcionGeneracionFilas_IteracionBusquedasConjuntos.IsChecked;
            }
        }

        private void condicionesBusqueda_Click(object sender, RoutedEventArgs e)
        {
            if(Busqueda != null)
            {
                CondicionesBusquedas condiciones = new CondicionesBusquedas();
                condiciones.VistaBusquedasArchivo = VistaBusquedasArchivo;
                condiciones.VistaBusquedasArchivo_TextosInformacion = VistaBusquedasArchivo_TextosInformacion;
                condiciones.VistaBusquedasURL = VistaBusquedasURL;
                condiciones.VistaBusquedasURL_TextosInformacion = VistaBusquedasURL_TextosInformacion;
                condiciones.CondicionSeleccionada = CondicionSeleccionada;
                condiciones.Busqueda = Busqueda;
                condiciones.ShowDialog();
            }
        }

        private void opcionNoGeneracionFilas_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.NoGenerarFilasTextosInformacion = (bool)opcionNoGeneracionFilas.IsChecked;
            }
        }

        private void opcionNoGeneracionFilas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.NoGenerarFilasTextosInformacion = (bool)opcionNoGeneracionFilas.IsChecked;
            }
        }

        private void opcionEjecutarBusquedaCabeceraIteraciones_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.EjecutarBusquedaCabeceraIteraciones = (bool)opcionEjecutarBusquedaCabeceraIteraciones.IsChecked;
            }
        }

        private void opcionEjecutarBusquedaCabeceraIteraciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.EjecutarBusquedaCabeceraIteraciones = (bool)opcionEjecutarBusquedaCabeceraIteraciones.IsChecked;
            }
        }
    }
}
