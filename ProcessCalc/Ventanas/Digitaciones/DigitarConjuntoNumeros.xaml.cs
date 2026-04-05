using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para DigitarConjuntoNumeros.xaml
    /// </summary>
    public partial class DigitarConjuntoNumeros : Window
    {
        public bool Pausado { get; set; }
        public List<EntidadNumero> Numeros { get; set; }
        public int CantidadDecimalesCantidades { get; set; }
        public List<ConjuntoTextosInformacion_Digitacion> ConjuntoTextosInformacionDigitacion { get; set; }
        public bool UtilizarPrimerConjunto_Automaticamente { get; set; }
        public bool SeleccionarNumeroDeOpciones { get; set; }
        public List<OpcionListaNumeros_Digitacion> OpcionesListaNumeros { get; set; }
        public bool FijarCantidadNumerosDigitacion { get; set; }
        public bool UtilizarSoloTextosPredefinidos { get; set; }
        public int CantidadNumerosDigitacion { get; set; }
        public Entrada Entrada { get; set; }
        public Calculo CalculoActual {  get; set; }
        public bool ModoToolTip { get; set; }
        public DigitarConjuntoNumeros()
        {
            Numeros = new List<EntidadNumero>();
            OpcionesListaNumeros = new List<OpcionListaNumeros_Digitacion>();
            InitializeComponent();
        }

        public void CargarTextosDigitados(int indiceNumero)
        {
            var textosDigitados = ListaTextosDigitados.CargarListasTextosDigitadas(CalculoActual.RutaArchivo, CalculoActual.ID, Entrada.ID);

            if (indiceNumero > -1)
            {
                //for(int indiceNumero = 0; indiceNumero < Numeros.Count; indiceNumero++)
                //{
                //if (Numeros[indiceNumero].)
                //{
                    var textosDigitadosNumero = textosDigitados.Where(i => i.IndiceNumero == indiceNumero).ToList();

                if(textosDigitadosNumero != null &&
                    textosDigitadosNumero.Any() &&
                    !Numeros[indiceNumero].TextosDigitados.Any())
                    Numeros[indiceNumero].TextosDigitados = textosDigitadosNumero;
                //}
                //}
            }
            else
            {
                for (indiceNumero = 0; indiceNumero < Numeros.Count; indiceNumero++)
                {
                    var textosDigitadosNumero = textosDigitados.Where(i => i.IndiceNumero == indiceNumero).ToList();

                    if (textosDigitadosNumero != null &&
                    textosDigitadosNumero.Any() &&
                    !Numeros[indiceNumero].TextosDigitados.Any())
                        Numeros[indiceNumero].TextosDigitados = textosDigitadosNumero;
                }
            }
        }

        private void GuardarTextosDigitados()
        {
            for (int indiceNumero = 0; indiceNumero < Numeros.Count; indiceNumero++)
            {
                //var textosDigitados = Numeros[indiceNumero].TextosDigitados;//.Where(i => i.IndiceNumero == indiceNumero).ToList();

                for (int indiceTexto = 0; indiceTexto < Numeros[indiceNumero].Textos.Count; indiceTexto++)
                {
                    var textosDigitadosEncontrado = Numeros[indiceNumero].TextosDigitados.Where(i => i.IndiceAsociado == indiceTexto).ToList();

                    foreach (var textosDigitadosNumero in textosDigitadosEncontrado)
                    {
                        if(!textosDigitadosNumero.TextosDigitados.Any(i => i == Numeros[indiceNumero].Textos[indiceTexto]))
                        {
                            textosDigitadosNumero.TextosDigitados.Add(Numeros[indiceNumero].Textos[indiceTexto]);
                        }
                    }
                }
            }

            ListaTextosDigitados.GuardarTextosDigitados(CalculoActual.RutaArchivo, CalculoActual.ID, Entrada.ID, 
                Numeros.SelectMany(i => i.TextosDigitados).ToList());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!ModoToolTip)
            {
                Entrada.CantidadesDigitadas.CargarCantidadesDigitadas(CalculoActual.RutaArchivo, CalculoActual.ID, Entrada.ID);

                if (FijarCantidadNumerosDigitacion)
                {
                    for (int indice = 1; indice <= CantidadNumerosDigitacion; indice++)
                    {
                        AgregarNuevoNumero();
                    }
                }
                else
                {
                    if (Numeros.Any())
                    {
                        int indice = 1;
                        foreach (var numero in Numeros)
                        {
                            AgregarNumero(numero, indice);
                            indice++;
                        }
                    }
                    else
                        AgregarNuevoNumero();
                }

                if (listaNumeros.Children.Count > 0)
                    ((Numero)listaNumeros.Children[0]).opcionesNumeros.Focus();

                if (FijarCantidadNumerosDigitacion &&
                    CantidadNumerosDigitacion > 0)
                {
                    listaTextoNumeros.MaxLines = CantidadNumerosDigitacion;
                }                
            }
            else
            {
                if (Numeros.Any())
                {
                    int indice = 1;
                    foreach (var numero in Numeros)
                    {
                        AgregarNumero(numero, indice);
                        indice++;
                    }
                }
                else
                    AgregarNuevoNumero();

                btnPausar.Visibility = Visibility.Collapsed;
                btnDetener.Visibility = Visibility.Visible;
                btnDetener.Content = "Cancelar";
                btnContinuar.Visibility = Visibility.Visible;
                btnContinuar.Content = "Aceptar";
            }
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

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            if (!ModoToolTip)
            {
                GuardarCantidadesDigitadas();
                GuardarTextosDigitados();
                ObtenerNumerosDeTextos();
            }

            DialogResult = true;
            Close();
        }

        private void GuardarCantidadesDigitadas()
        {
            foreach(Numero Numero in listaNumeros.Children)
            {
                Entrada.CantidadesDigitadas.AgregarCantidadDigitada(Numero.opcionesNumeros.Text);
            }

            Entrada.CantidadesDigitadas.GuardarCantidadesDigitadas(CalculoActual.RutaArchivo, CalculoActual.ID, Entrada.ID);
        }

        private void ObtenerNumerosDeTextos()
        {
            if (textoNumeros.Visibility == Visibility.Visible)
            {
                Numeros.Clear();

                string[] lineasTexto = listaTextoNumeros.Text.Split('\n');

                foreach (var itemLineaTexto in lineasTexto)
                {
                    if (string.IsNullOrEmpty(itemLineaTexto)) continue;

                    List<string> textos = itemLineaTexto.Split(';').ToList();
                    List<string> TextosInformacion = new List<string>();
                    double numero = 0;
                    string nombreNumero = string.Empty;

                    foreach (var itemTexto in textos)
                    {
                        if (itemTexto != textos.Last() &&
                            itemTexto != textos.First())
                            TextosInformacion.Add(itemTexto);
                        else if (itemTexto == textos.First())
                            nombreNumero = itemTexto;
                        else if(itemTexto == textos.Last())
                        {
                            double.TryParse(itemTexto, out numero);
                        }
                    }

                    EntidadNumero nuevo = new EntidadNumero();
                    nuevo.Nombre = nombreNumero;

                    nuevo.Textos.AddRange(TextosInformacion);
                    nuevo.Numero = numero;

                    Numeros.Add(nuevo);

                    if (FijarCantidadNumerosDigitacion &&
                        CantidadNumerosDigitacion > 0 &&
                        Numeros.Count == CantidadNumerosDigitacion)
                        break;
                }
            }
        }

        public void AgregarNuevoNumero()
        {
            EntidadNumero nuevo = new EntidadNumero();            
            Numeros.Add(nuevo);
            nuevo.PosicionElemento_Lectura = Numeros.Count;
            nuevo.Nombre = "Número " + Numeros.Count;
            Numero nuevoNumero = new Numero();            
            nuevoNumero.Digitando = true;
            nuevoNumero.VistaDigitacion = this;
            nuevoNumero.NoAgregarOtrosNumeros = FijarCantidadNumerosDigitacion;
            nuevoNumero.EntNumero = nuevo;
            nuevoNumero.textos.Numero = nuevoNumero.EntNumero;
            nuevoNumero.textos.ConjuntosTextosInformacionDigitacion = ConjuntoTextosInformacionDigitacion;
            nuevoNumero.UtilizarPrimerConjunto_Automaticamente = UtilizarPrimerConjunto_Automaticamente;
            nuevoNumero.UtilizarSoloTextosPredefinidos = UtilizarSoloTextosPredefinidos;
            nuevoNumero.SeleccionarNumeroDeOpciones = SeleccionarNumeroDeOpciones;
            nuevoNumero.OpcionesListaNumeros = OpcionesListaNumeros;
            nuevoNumero.Entrada = Entrada;
            nuevoNumero.textos.IndiceNumero = Numeros.IndexOf(nuevo);
            nuevoNumero.textos.DigitacionConjuntoNumeros = this;
            listaNumeros.Children.Add(nuevoNumero);
        }

        public void AgregarNumero(EntidadNumero nuevo, int indice)
        {
            nuevo.PosicionElemento_Lectura = indice;
            nuevo.Nombre = "Número " + indice.ToString();
            Numero nuevoNumero = new Numero();
            nuevoNumero.Digitando = true;
            nuevoNumero.VistaDigitacion = this;
            nuevoNumero.NoAgregarOtrosNumeros = FijarCantidadNumerosDigitacion;
            nuevoNumero.EntNumero = nuevo;
            nuevoNumero.textos.Numero = nuevoNumero.EntNumero;
            nuevoNumero.textos.ConjuntosTextosInformacionDigitacion = ConjuntoTextosInformacionDigitacion;
            nuevoNumero.UtilizarPrimerConjunto_Automaticamente = UtilizarPrimerConjunto_Automaticamente;
            nuevoNumero.UtilizarSoloTextosPredefinidos = UtilizarSoloTextosPredefinidos;
            nuevoNumero.SeleccionarNumeroDeOpciones = SeleccionarNumeroDeOpciones;
            nuevoNumero.OpcionesListaNumeros = OpcionesListaNumeros;
            nuevoNumero.Entrada = Entrada;
            nuevoNumero.textos.IndiceNumero = indice - 1;
            nuevoNumero.textos.DigitacionConjuntoNumeros = this;
            listaNumeros.Children.Add(nuevoNumero);
        }

        public void QuitarNumero(Numero numero)
        {
            if (!ModoToolTip &&
                Entrada != null)
            {
                var textosDigitados = Entrada.TextosDigitados.Where(i => i.IndiceNumero == Numeros.IndexOf(numero.EntNumero)).ToList();

                while (textosDigitados.Any())
                {
                    Entrada.TextosDigitados.Remove(textosDigitados.FirstOrDefault());
                    textosDigitados.Remove(textosDigitados.FirstOrDefault());
                }
            }

            Numeros.Remove(numero.EntNumero);
            listaNumeros.Children.Remove(numero);

            //((Numero)listaNumeros.Children[listaNumeros.Children.Count - 1]).btnOtroNumero.IsEnabled = true;
            if (listaNumeros.Children.Count == 0) AgregarNuevoNumero();
        }

        private void vistaOpcionLista_Click(object sender, RoutedEventArgs e)
        {
            ObtenerNumerosDeTextos();

            textoNumeros.Visibility = Visibility.Collapsed;
            listaNumeros.Visibility = Visibility.Visible;

            listaNumeros.Children.Clear();

            int indiceNumero = 0;
            foreach (var itemNumero in Numeros)
            {
                Numero nuevoNumero = new Numero();
                nuevoNumero.Digitando = true;
                nuevoNumero.VistaDigitacion = this;
                nuevoNumero.NoAgregarOtrosNumeros = FijarCantidadNumerosDigitacion;
                nuevoNumero.EntNumero = itemNumero;
                nuevoNumero.textos.Numero = nuevoNumero.EntNumero;
                nuevoNumero.textos.ConjuntosTextosInformacionDigitacion = ConjuntoTextosInformacionDigitacion;
                nuevoNumero.UtilizarPrimerConjunto_Automaticamente = UtilizarPrimerConjunto_Automaticamente;
                nuevoNumero.UtilizarSoloTextosPredefinidos = UtilizarSoloTextosPredefinidos;
                nuevoNumero.SeleccionarNumeroDeOpciones = SeleccionarNumeroDeOpciones;
                nuevoNumero.OpcionesListaNumeros = OpcionesListaNumeros;
                nuevoNumero.textos.IndiceNumero = indiceNumero;
                nuevoNumero.textos.DigitacionConjuntoNumeros = this;
                listaNumeros.Children.Add(nuevoNumero);
                indiceNumero++;
            }
        }

        private void vistaOpcionTexto_Click(object sender, RoutedEventArgs e)
        {
            listaNumeros.Visibility = Visibility.Collapsed;
            textoNumeros.Visibility = Visibility.Visible;

            string strNumeros = string.Empty;
            foreach (var itemNumero in Numeros)
            {
                string strLinea = string.Empty;
                strLinea = itemNumero.Nombre + ";";

                foreach (var itemTexto in itemNumero.Textos)
                {
                    strLinea += itemTexto + ";";
                }
                strLinea += itemNumero.Numero.ToString("N" + CantidadDecimalesCantidades.ToString()) + "\n";

                strNumeros += strLinea;
            }

            listaTextoNumeros.Text = strNumeros;
        }

        private void QuitarUltimaLinea()
        {            
            if (FijarCantidadNumerosDigitacion &&
                CantidadNumerosDigitacion > 0 &&
                listaTextoNumeros.Text.Any() &&
                (listaTextoNumeros.Text.Split("\r\n").Length > CantidadNumerosDigitacion |
                listaTextoNumeros.Text.Split("\n").Length > CantidadNumerosDigitacion))
            {
                if(listaTextoNumeros.Text.LastIndexOf("\r\n") > -1)
                    listaTextoNumeros.Text = listaTextoNumeros.Text.Remove(listaTextoNumeros.Text.LastIndexOf("\r\n"));

                else if (listaTextoNumeros.Text.LastIndexOf("\n") > -1)
                    listaTextoNumeros.Text = listaTextoNumeros.Text.Remove(listaTextoNumeros.Text.LastIndexOf("\n"));

                listaTextoNumeros.Select(listaTextoNumeros.Text.Length, 0);
            }            
        }

        private void listaTextoNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuitarUltimaLinea();
        }
    }
}
