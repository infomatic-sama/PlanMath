using ProcessCalc.Controles.Ctrl_Entradas;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.OrigenesDatos;
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
    /// Lógica de interacción para ParametroSolicitudURL_Entrada.xaml
    /// </summary>
    public partial class ParametroSolicitudURL_Entrada : UserControl
    {
        private ParametroURL param;

        public ParametroURL Parametro
        {
            get
            {
                return param;
            }

            set
            {
                param = value;

                nombreParametro.Text = param.Nombre;
                valorParametro.Text = param.Valor;
                checkParametroNumerico.IsChecked = value.EsNumerico;
                checkEsConjunto.IsChecked = value.EsConjuntoParametros;
                checkParametroNumericoConDecimales.IsChecked = value.EsNumericoConDecimales;

                enviarParametroEnBody.IsChecked = value.ParametrosEnBody;
                enviarParametroEnUrl.IsChecked = value.ParametrosEnUrl;
                
                for (int indice = 1; indice <= value.Nivel; indice++)
                {
                    //if(indice == value.Nivel && 
                    //    value.EsParametroEnConjunto)
                    //{
                    //    //espacio.Text += "\t|";
                    //    borde.BorderThickness = new Thickness(0.3, 0.3, 0.3, 0);
                    //}
                    //else
                    //    borde.BorderThickness = new Thickness(0, 0.3, 0, 0);

                    espacio.Text += '\t';
                }

                if (value.EsParametroEnConjunto &&
                    value.ConjuntoParametros != null)
                {
                    elementoDe.Text = "Elemento de " + value.ConjuntoParametros.Nombre;
                    elementoDe.Visibility = Visibility.Visible;
                }
                else
                    elementoDe.Visibility = Visibility.Collapsed;
            }
        }
        public VistaURLEntradaNumero VistaEntradaNumero { get; set; }
        public ConfiguracionURLEntrada ConfiguracionURL { get; set; }
        public ConfigURLEntrada_Ctrl VistaConfiguracion {  get; set; }
        public ParametroSolicitudURL_Entrada()
        {
            InitializeComponent();
        }

        private void nombreParametro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                    param.Nombre = nombreParametro.Text;
            }
        }

        private void valorParametro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                    param.Valor = valorParametro.Text;
            }
        }

        private void checkParametroNumerico_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                    param.EsNumerico = (bool)checkParametroNumerico.IsChecked;

                checkParametroNumericoConDecimales.Visibility = Visibility.Visible;
            }
        }

        private void checkParametroNumerico_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                    param.EsNumerico = (bool)checkParametroNumerico.IsChecked;

                checkParametroNumericoConDecimales.Visibility = Visibility.Collapsed;
            }
        }

        private void checkParametroNumericoConDecimales_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                    param.EsNumericoConDecimales = (bool)checkParametroNumericoConDecimales.IsChecked;
            }
        }

        private void checkParametroNumericoConDecimales_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                    param.EsNumericoConDecimales = (bool)checkParametroNumericoConDecimales.IsChecked;
            }
        }

        private void checkEsConjunto_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                {
                    param.EsConjuntoParametros = (bool)checkEsConjunto.IsChecked;
                    checkParametroNumerico.Visibility = Visibility.Collapsed;
                    checkParametroNumericoConDecimales.Visibility = Visibility.Collapsed;

                    if (VistaConfiguracion != null)
                    {
                        var parametros = ConfiguracionURL.ParametrosURL.Where(i =>
                        ConfiguracionURL.ParametrosURL.IndexOf(i) > ConfiguracionURL.ParametrosURL.IndexOf(param)).ToList();
                        bool asignar = false;

                        foreach (var paramItem in parametros)
                        {
                            if (paramItem.Nivel == param.Nivel &&
                                asignar)
                                break;

                            if (paramItem.Nivel == param.Nivel + 1)
                            {
                                asignar = true;
                                paramItem.ConjuntoParametros = param;
                                paramItem.EsParametroEnConjunto = true;
                            }
                        }

                        //parametros = VistaEntradaConjuntoNumeros.Entrada.ParametrosURL.Where(i => 
                        //VistaEntradaConjuntoNumeros.Entrada.ParametrosURL.IndexOf(i) > VistaEntradaConjuntoNumeros.Entrada.ParametrosURL.IndexOf(param)).ToList();

                        //foreach (var paramItem in parametros)
                        //{
                        //    paramItem.Nivel++;
                        //}

                        VistaConfiguracion.ListarParametros();
                    }

                    if (VistaEntradaNumero != null)
                    {
                        var parametros = VistaEntradaNumero.Entrada.ListaURLs.FirstOrDefault().ParametrosURL.Where(i =>
                        VistaEntradaNumero.Entrada.ListaURLs.FirstOrDefault().ParametrosURL.IndexOf(i) > VistaEntradaNumero.Entrada.ListaURLs.FirstOrDefault().ParametrosURL.IndexOf(param)).ToList();
                        bool asignar = false;

                        foreach (var paramItem in parametros)
                        {
                            if (paramItem.Nivel == param.Nivel &&
                                asignar)
                                break;

                            if (paramItem.Nivel == param.Nivel + 1)
                            {
                                asignar = true;
                                paramItem.ConjuntoParametros = param;
                                paramItem.EsParametroEnConjunto = true;
                            }
                        }

                        //    VistaEntradaNumero.ListarParametros();
                        //}

                        //if (VistaEntradaTextosInformacion != null)
                        //{
                        //    var parametros = VistaEntradaTextosInformacion.Entrada.ParametrosURL.Where(i =>
                        //    VistaEntradaTextosInformacion.Entrada.ParametrosURL.IndexOf(i) > VistaEntradaTextosInformacion.Entrada.ParametrosURL.IndexOf(param)).ToList();
                        //    bool asignar = false;

                        //    foreach (var paramItem in parametros)
                        //    {
                        //        if (paramItem.Nivel == param.Nivel &&
                        //            asignar)
                        //            break;

                        //        if (paramItem.Nivel == param.Nivel + 1)
                        //        {
                        //            asignar = true;
                        //            paramItem.ConjuntoParametros = param;
                        //            paramItem.EsParametroEnConjunto = true;
                        //        }
                        //    }

                        //    VistaEntradaTextosInformacion.ListarParametros();
                    }
                }
            }
        }

        private void checkEsConjunto_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                {
                    param.EsConjuntoParametros = (bool)checkEsConjunto.IsChecked;
                    checkParametroNumerico.Visibility = Visibility.Visible;

                    if (checkParametroNumerico.IsChecked == true)
                        checkParametroNumerico_Checked(sender, e);
                    else
                        checkParametroNumerico_Unchecked(sender, e);

                    if (VistaConfiguracion != null)
                    {
                        var parametros = ConfiguracionURL.ParametrosURL.Where(i =>
                        ConfiguracionURL.ParametrosURL.IndexOf(i) > ConfiguracionURL.ParametrosURL.IndexOf(param)).ToList();
                        bool asignar = false;

                        foreach (var paramItem in parametros)
                        {
                            if (paramItem.Nivel == param.Nivel &&
                                asignar)
                                break;

                            if (paramItem.Nivel == param.Nivel + 1)
                            {
                                asignar = true;
                                paramItem.ConjuntoParametros = null;
                                paramItem.EsParametroEnConjunto = false;
                            }
                        }

                        VistaConfiguracion.ListarParametros();
                    }

                    if (VistaEntradaNumero != null)
                    {
                        var parametros = VistaEntradaNumero.Entrada.ListaURLs.FirstOrDefault().ParametrosURL.Where(i =>
                        VistaEntradaNumero.Entrada.ListaURLs.FirstOrDefault().ParametrosURL.IndexOf(i) > VistaEntradaNumero.Entrada.ListaURLs.FirstOrDefault().ParametrosURL.IndexOf(param)).ToList();
                        bool asignar = false;

                        foreach (var paramItem in parametros)
                        {
                            if (paramItem.Nivel == param.Nivel &&
                                asignar)
                                break;

                            if (paramItem.Nivel == param.Nivel + 1)
                            {
                                asignar = true;
                                paramItem.ConjuntoParametros = null;
                                paramItem.EsParametroEnConjunto = false;
                            }
                        }

                        VistaEntradaNumero.ListarParametros();
                    }

                    //if (VistaEntradaTextosInformacion != null)
                    //{
                    //    var parametros = VistaEntradaTextosInformacion.Entrada.ParametrosURL.Where(i =>
                    //    VistaEntradaTextosInformacion.Entrada.ParametrosURL.IndexOf(i) > VistaEntradaTextosInformacion.Entrada.ParametrosURL.IndexOf(param)).ToList();
                    //    bool asignar = false;

                    //    foreach (var paramItem in parametros)
                    //    {
                    //        if (paramItem.Nivel == param.Nivel &&
                    //            asignar)
                    //            break;

                    //        if (paramItem.Nivel == param.Nivel + 1)
                    //        {
                    //            asignar = true;
                    //            paramItem.ConjuntoParametros = null;
                    //            paramItem.EsParametroEnConjunto = false;
                    //        }
                    //    }

                    //    VistaEntradaTextosInformacion.ListarParametros();
                    //}
                }
            }
        }

        private void enviarParametroEnBody_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                {
                    param.ParametrosEnBody = (bool)enviarParametroEnBody.IsChecked;

                    if (VistaConfiguracion != null)
                    {
                        //var paramItem = VistaEntradaConjuntoNumeros.Entrada.ParametrosURL.FirstOrDefault(i => i.Nivel == param.Nivel + 1);

                        //if(paramItem != null)
                        //    paramItem.ParametrosEnBody = param.ParametrosEnBody;
                        
                        VistaConfiguracion.ListarParametros();
                    }

                    if (VistaEntradaNumero != null)
                    {
                        VistaEntradaNumero.ListarParametros();
                    }

                    //if (VistaEntradaTextosInformacion != null)
                    //{
                    //    VistaEntradaTextosInformacion.ListarParametros();
                    //}
                }
            }
        }

        private void enviarParametroEnBody_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                {
                    param.ParametrosEnBody = (bool)enviarParametroEnBody.IsChecked;

                    if (VistaConfiguracion != null)
                    {
                        //var paramItem = VistaEntradaConjuntoNumeros.Entrada.ParametrosURL.FirstOrDefault(i => i.Nivel == param.Nivel + 1);

                        //if (paramItem != null)
                        //    paramItem.ParametrosEnBody = param.ParametrosEnBody;

                        VistaConfiguracion.ListarParametros();
                    }

                    if (VistaEntradaNumero != null)
                    {
                        VistaEntradaNumero.ListarParametros();
                    }

                    //if (VistaEntradaTextosInformacion != null)
                    //{
                    //    VistaEntradaTextosInformacion.ListarParametros();
                    //}
                }
            }
        }
        private void enviarParametroEnUrl_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                {
                    param.ParametrosEnUrl = (bool)enviarParametroEnUrl.IsChecked;

                    if (VistaConfiguracion != null)
                    {
                        //var paramItem = VistaEntradaConjuntoNumeros.Entrada.ParametrosURL.FirstOrDefault(i => i.Nivel == param.Nivel + 1);

                        //if (paramItem != null)
                        //    paramItem.ParametrosEnUrl = param.ParametrosEnUrl;

                        VistaConfiguracion.ListarParametros();
                    }

                    if (VistaEntradaNumero != null)
                    {
                        VistaEntradaNumero.ListarParametros();
                    }

                    //if (VistaEntradaTextosInformacion != null)
                    //{
                    //    VistaEntradaTextosInformacion.ListarParametros();
                    //}
                }
            }
        }

        private void enviarParametroEnUrl_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (param != null)
                {
                    param.ParametrosEnUrl = (bool)enviarParametroEnUrl.IsChecked;

                    if (VistaConfiguracion != null)
                    {
                        //var paramItem = VistaEntradaConjuntoNumeros.Entrada.ParametrosURL.FirstOrDefault(i => i.Nivel == param.Nivel + 1);

                        //if (paramItem != null)
                        //    paramItem.ParametrosEnUrl = param.ParametrosEnUrl;

                        VistaConfiguracion.ListarParametros();
                    }

                    if (VistaEntradaNumero != null)
                    {
                        VistaEntradaNumero.ListarParametros();
                    }

                    //if (VistaEntradaTextosInformacion != null)
                    //{
                    //    VistaEntradaTextosInformacion.ListarParametros();
                    //}
                }
            }
        }
    }
}
