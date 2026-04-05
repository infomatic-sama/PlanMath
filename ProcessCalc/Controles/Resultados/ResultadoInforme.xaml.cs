using ProcessCalc.Entidades.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Lógica de interacción para ResultadoInforme.xaml
    /// </summary>
    public partial class ResultadoInforme : UserControl
    {
        public ResultadoEjecucion Resultado { get; set; }
        public int CantidadDecimalesCantidades { get; set; }
        public bool ModoTodos { get; set; }
        public StackPanel ListaResultados { get; set; }
        public ResultadoInforme()
        {
            InitializeComponent();
        }

        private void copiarPortapapeles_Click(object sender, RoutedEventArgs e)
        {
            string cadenaACopiar = ObtenerTexto_CopiarPortapapeles();

            if (!string.IsNullOrEmpty(cadenaACopiar))
            {
                Clipboard.SetText(cadenaACopiar);
            }
        }

        public string ObtenerTexto_CopiarPortapapeles()
        {
            string cadenaACopiar = string.Empty;

            cadenaACopiar += nombre.Text + "\t";
            cadenaACopiar += descripcion.Text;
            if (!Resultado.EsConjuntoNumeros)
                cadenaACopiar += "\t" + Resultado.ValorNumerico.ToString("N" + CantidadDecimalesCantidades.ToString());
            else
            {
                //foreach (var itemNumero in contenidoNumerosResultado.Children)
                //{
                //    cadenaACopiar += "\n" + ((ResultadoInforme)itemNumero).nombre.Text;
                //    cadenaACopiar += "\t" + ((ResultadoInforme)itemNumero).descripcion.Text;
                //    cadenaACopiar += "\t" + ((ResultadoInforme)itemNumero).valorNumerico.Text;
                //}

                foreach (var itemClasificador in Resultado.ValoresNumericos.SelectMany(i => i.Clasificadores_SeleccionarOrdenar_Resultados).Distinct())
                {
                    if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto) ||
                        (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                        !Resultado.ValoresNumericos.SelectMany(i => i.Clasificadores_SeleccionarOrdenar_Resultados).Distinct().Any(i => !string.IsNullOrEmpty(i.CadenaTexto))))
                    {
                        cadenaACopiar += "\n" + itemClasificador.CadenaTexto;
                        cadenaACopiar += "\n";

                        foreach (var itemNumero in Resultado.ValoresNumericos.Where(i => i.Clasificadores_SeleccionarOrdenar_Resultados.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)))
                        {
                            cadenaACopiar += "\n" + itemNumero.Nombre;
                            cadenaACopiar += "\t" + itemNumero.Numero.ToString("N" + CantidadDecimalesCantidades.ToString());

                            cadenaACopiar += "\t";
                            foreach (var itemTexto in itemNumero.Textos)
                            {
                                cadenaACopiar += itemTexto;
                                if (itemTexto != itemNumero.Textos.LastOrDefault())
                                    cadenaACopiar += ", ";
                            }
                        }

                        cadenaACopiar += "\n";
                    }
                }
            }

            return cadenaACopiar;
        }

        private void copiarPortapapelesNumero_Click(object sender, RoutedEventArgs e)
        {
            string cadenaACopiar = ObtenerTexto_CopiarPortapapelesNumero();

            if (!string.IsNullOrEmpty(cadenaACopiar))
            {
                Clipboard.SetText(cadenaACopiar);
            }
        }

        public string ObtenerTexto_CopiarPortapapelesNumero()
        {
            string cadenaACopiar = string.Empty;

            if (!Resultado.EsConjuntoNumeros)
                cadenaACopiar += Resultado.ValorNumerico.ToString("N" + CantidadDecimalesCantidades.ToString());
            else
            {
                //foreach (var itemNumero in contenidoNumerosResultado.Children)
                //{
                //    cadenaACopiar += ((ResultadoInforme)itemNumero).valorNumerico.Text + "\n";
                //}

                foreach (var itemClasificador in Resultado.ValoresNumericos.SelectMany(i => i.Clasificadores_SeleccionarOrdenar_Resultados).Distinct())
                {
                    if (!string.IsNullOrEmpty(itemClasificador.CadenaTexto) ||
                        (string.IsNullOrEmpty(itemClasificador.CadenaTexto) &&
                        !Resultado.ValoresNumericos.SelectMany(i => i.Clasificadores_SeleccionarOrdenar_Resultados).Distinct().Any(i => !string.IsNullOrEmpty(i.CadenaTexto))))
                    {
                        cadenaACopiar += "\n" + itemClasificador.CadenaTexto;
                        cadenaACopiar += "\n";

                        foreach (var itemNumero in Resultado.ValoresNumericos.Where(i => i.Clasificadores_SeleccionarOrdenar_Resultados.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)))
                        {
                            cadenaACopiar += itemNumero.Numero.ToString("N" + CantidadDecimalesCantidades.ToString()) + "\n";
                        }

                        cadenaACopiar += "\n";
                    }
                }
            }

            return cadenaACopiar;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(ModoTodos)
            {
                contenidoCabecera.Visibility = Visibility.Collapsed;
                contenidoCabeceraTodos.Visibility = Visibility.Visible;
                contenidoResultado.Visibility = Visibility.Collapsed;
            }
        }

        private void copiarPortapapelesTodos_Click(object sender, RoutedEventArgs e)
        {
            string cadenaACopiar = string.Empty;

            if (ListaResultados != null)
            {
                foreach (var itemResultado in ListaResultados.Children)
                {
                    if(itemResultado.GetType () == typeof(ResultadoInforme) &&
                        itemResultado != this)
                    {
                        var item = (ResultadoInforme)itemResultado;

                        cadenaACopiar += item.ObtenerTexto_CopiarPortapapeles();
                        cadenaACopiar += "\n\n";
                    }
                }
            }

            if (!string.IsNullOrEmpty(cadenaACopiar))
            {
                Clipboard.SetText(cadenaACopiar);
            }
        }

        private void copiarPortapapelesNumeroTodos_Click(object sender, RoutedEventArgs e)
        {
            string cadenaACopiar = string.Empty;

            if (ListaResultados != null)
            {
                foreach (var itemResultado in ListaResultados.Children)
                {
                    if (itemResultado.GetType() == typeof(ResultadoInforme) &&
                        itemResultado != this)
                    {
                        var item = (ResultadoInforme)itemResultado;

                        cadenaACopiar += item.ObtenerTexto_CopiarPortapapelesNumero();
                        cadenaACopiar += "\n\n";
                    }
                }
            }

            if (!string.IsNullOrEmpty(cadenaACopiar))
            {
                Clipboard.SetText(cadenaACopiar);
            }
        }
    }
}
