using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Ejecuciones.ToolTips;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.OrigenesDatos;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Controles.Calculos
{
    /// <summary>
    /// Lógica de interacción para ToolTipElementoVisual.xaml
    /// </summary>
    public partial class ToolTipElementoVisual : UserControl
    {
        public DiseñoOperacion ElementoAsociado { get; set; }
        public DatosTooltips_Elementos DatosTooltips_Elementos { get; set; }
        public EjecucionTooltipsCalculo EjecucionToolTips { get; set; }
        public ToolTipElementoVisual()
        {
            InitializeComponent();
        }
      
        public void EstablecerDigitacion(bool digitacion)
        {
            if(digitacion)
            {
                infoDigitacion.Visibility = Visibility.Visible;
                botonDigitacion.Visibility = Visibility.Visible;
            }
            else
            {
                infoDigitacion.Visibility = Visibility.Collapsed;
                botonDigitacion.Visibility = Visibility.Collapsed;
            }
        }

        public void EstablecerSeleccionArchivosEntrada(bool seleccionArchivo)
        {
            if (seleccionArchivo)
            {
                infoSeleccionArchivosEntradas.Visibility = Visibility.Visible;
                botonesSeleccionArchivosEntradas.Visibility = Visibility.Visible;

                botonesSeleccionArchivosEntradas.Children.Clear();

                if (ElementoAsociado != null)
                {
                    var EntradaAsociada = (ElementoEntradaEjecucion)this.EjecucionToolTips.Ejecucion.ObtenerElementoEjecucion(this.EjecucionToolTips.Ejecucion.Calculo.ObtenerElementoDiseño(ElementoAsociado.ID));

                    int posicion = 1;
                    foreach(var itemOrigenDatos in EntradaAsociada.OrigenesDatos)
                    {
                        var ConfigArchivoAsociado = (ElementoArchivoOrigenDatosEjecucion)itemOrigenDatos;

                        if (ConfigArchivoAsociado.TipoOrigenDatos == TipoOrigenDatos.Archivo &&
                            ConfigArchivoAsociado.ConfiguracionSeleccionArchivo != OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado)
                        {
                            Button boton = new Button();
                            boton.Content = "Seleccionar archivo " + posicion.ToString();

                            boton.BorderThickness = new Thickness(1);
                            boton.BorderBrush = Brushes.Black;
                            boton.Margin = new Thickness(2);
                            boton.FontWeight = FontWeights.Bold;
                            boton.VerticalAlignment = VerticalAlignment.Center;

                            boton.Tag = itemOrigenDatos;
                            boton.Click += botonSeleccionArchivosEntradas_Click;
                            botonesSeleccionArchivosEntradas.Children.Add(boton);
                        }

                        posicion++;
                    }
                }
            }
            else
            {
                infoSeleccionArchivosEntradas.Visibility = Visibility.Collapsed;
                botonesSeleccionArchivosEntradas.Visibility = Visibility.Collapsed;
            }
        }

        public void EstablecerDigitacion_TextoEnPantalla(bool digitacion, string textoEnPantalla)
        {
            if (digitacion)
            {
                infoTextoEnPantalla.Text = "Texto en pantalla: " + textoEnPantalla;
                infoTextoEnPantalla.Visibility = Visibility.Visible;
            }
            else
            {
                infoTextoEnPantalla.Text = "Texto en pantalla:";
                infoTextoEnPantalla.Visibility = Visibility.Collapsed;
            }
        }

        public void EstablecerSeleccionEntradas(bool seleccionEntradas)
        {
            if (seleccionEntradas)
            {
                infoSeleccionEntradas.Visibility = Visibility.Visible;
                botonSeleccionEntradas.Visibility = Visibility.Visible;
            }
            else
            {
                infoSeleccionEntradas.Visibility = Visibility.Collapsed;
                botonSeleccionEntradas.Visibility = Visibility.Collapsed;
            }
        }

        public void EstablecerSeleccionEntradas_EntradaNoSeleccionada(bool entradaNoSeleccionada)
        {
            if (entradaNoSeleccionada)
            {
                infoEntradaNoSeleccionada.Visibility = Visibility.Visible;
            }
            else
            {
                infoEntradaNoSeleccionada.Visibility = Visibility.Collapsed;
            }
        }

        private void botonDigitacion_Click(object sender, RoutedEventArgs e)
        {
            ((Popup)Parent).IsOpen = false;
            e.Handled = true;
            
            if (ElementoAsociado != null)
            {
                if (DatosTooltips_Elementos.Digitacion_TextoEnPantalla)
                {
                    DigitarTextoPantalla digitarTexto = new DigitarTextoPantalla();
                    digitarTexto.ModoToolTip = true;
                    digitarTexto.descripcionEntrada.Text = ElementoAsociado.EntradaRelacionada.Nombre;
                    digitarTexto.Texto = DatosTooltips_Elementos.TextoEnPantalla;

                    bool digita = (bool)digitarTexto.ShowDialog();
                    if (digita == true)
                    {
                        ElementoAsociado.TextoEnPantalla_Digitacion_Tooltip = digitarTexto.Texto;

                        RestablecerElementosVisuales();
                        EjecucionToolTips.EstablecerEstadoCambiosToolTips_EntradaEspecifica(
                            true, ElementoAsociado.EntradaRelacionada.ID);
                    }
                }
                else
                {
                    if (ElementoAsociado.EntradaRelacionada.Tipo == TipoEntrada.ConjuntoNumeros)
                    {
                        DigitarConjuntoNumeros digitar = new DigitarConjuntoNumeros();
                        digitar.CalculoActual = EjecucionToolTips.Calculo;
                        digitar.Entrada = ElementoAsociado.EntradaRelacionada;
                        digitar.ModoToolTip = true;
                        digitar.descripcionEntrada.Text = ElementoAsociado.Nombre;
                        digitar.titulo.Text = "Digitar entrada (conjunto de números) de prueba ";
                        digitar.Numeros.AddRange(ElementoAsociado.Cantidades_Digitacion_Tooltip.Select(i => i.CopiarObjeto()));

                        bool digita = (bool)digitar.ShowDialog();
                        if (digita == true)
                        {
                            ElementoAsociado.Cantidades_Digitacion_Tooltip.Clear();
                            ElementoAsociado.Cantidades_Digitacion_Tooltip.AddRange(digitar.Numeros.ToList());
                            
                            RestablecerElementosVisuales();
                            EjecucionToolTips.EstablecerEstadoCambiosToolTips_EntradaEspecifica(
                                true, ElementoAsociado.EntradaRelacionada.ID);
                        }
                    }
                    else if (ElementoAsociado.EntradaRelacionada.Tipo == TipoEntrada.Numero)
                    {
                        DigitarNumero digitar = new DigitarNumero();
                        digitar.ModoToolTip = true;
                        digitar.textos.Entrada = new Entrada("Entrada");
                        digitar.descripcionEntrada.Text = ElementoAsociado.EntradaRelacionada.Nombre;
                        digitar.titulo.Text = "Digitar entrada (número) de prueba ";

                        digitar.Numero = ElementoAsociado.ValorNumerico_Digitacion_Tooltip;
                        digitar.textos.Entrada.Textos.AddRange(ElementoAsociado.TextosInformacion_Digitacion_Tooltip.ToList());

                        bool digita = (bool)digitar.ShowDialog();
                        if (digita == true)
                        {
                            ElementoAsociado.Cantidades_Digitacion_Tooltip.Clear();
                            ElementoAsociado.Cantidades_Digitacion_Tooltip.Add(new Entidades.Operaciones.EntidadNumero()
                            {
                                Numero = digitar.Numero,
                                Textos = digitar.textos.Entrada.Textos.ToList()
                            });

                            ElementoAsociado.ValorNumerico_Digitacion_Tooltip = digitar.Numero;
                            ElementoAsociado.TextosInformacion_Digitacion_Tooltip = digitar.textos.Entrada.Textos.ToList();

                            RestablecerElementosVisuales();
                            EjecucionToolTips.EstablecerEstadoCambiosToolTips_EntradaEspecifica(
                                true, ElementoAsociado.EntradaRelacionada.ID);
                        }
                    }
                }
            }
        }

        private void botonSeleccionEntradas_Click(object sender, RoutedEventArgs e)
        {
            ((Popup)Parent).IsOpen = false;
            e.Handled = true;
            
            if (ElementoAsociado != null)
            {
                SeleccionManualOperaciones_CondicionEntradas seleccionar = new SeleccionManualOperaciones_CondicionEntradas();
                seleccionar.descripcionCondiciones.Text = "Operador de selección de entradas: " + ElementoAsociado.NombreCombo +
                    ". Selecciona las entradas a utilizar:";
                seleccionar.titulo.Text = "Seleccionar entradas para continuar\n";

                seleccionar.Entradas.AddRange(ElementoAsociado.ElementosPosteriores.ToList());
                seleccionar.EntradasSeleccionadas = ElementoAsociado.EntradasSeleccionadas_ToolTips.ToList();

                seleccionar.ModoToolTips = true;

                bool digita = (bool)seleccionar.ShowDialog();
                if (digita == true)
                {
                    
                    ElementoAsociado.EntradasSeleccionadas_ToolTips = seleccionar.Entradas.ToList();
                    ElementoAsociado.Actualizar_ToolTips = true;


                    EjecucionToolTips.ObtenerCalculo(EjecucionToolTips.Calculo.ID).HayCambios = true;

                    EjecucionToolTips.Ejecucion.Detener();
                }
            }
        }

        public void RestablecerElementosVisuales()
        {
            DatosTooltips_Elementos.ValorNumerico = 0;
            if(DatosTooltips_Elementos.TextosInformacion != null)
                DatosTooltips_Elementos.TextosInformacion.Clear();
            DatosTooltips_Elementos.InfoElementos_Numeros.Clear();
        }

        private void botonSeleccionArchivosEntradas_Click(object sender, RoutedEventArgs e)
        {
            var OrigenDatosAsociado = (ElementoOrigenDatosEjecucion)((Button)sender).Tag;

            ((Popup)Parent).IsOpen = false;
            e.Handled = true;

            if (ElementoAsociado != null &&
                OrigenDatosAsociado != null &&
                OrigenDatosAsociado.TipoOrigenDatos == TipoOrigenDatos.Archivo)
            {
                var EntradaAsociada = (ElementoEntradaEjecucion)this.EjecucionToolTips.Ejecucion.ObtenerElementoEjecucion(this.EjecucionToolTips.Ejecucion.Calculo.ObtenerElementoDiseño(ElementoAsociado.ID));

                var ConfigArchivoAsociado = (ElementoArchivoOrigenDatosEjecucion)OrigenDatosAsociado;
                if(ConfigArchivoAsociado != null)
                {
                    string ruta = string.Empty;
                    string rutaArchivoCalculo = this.EjecucionToolTips.Ejecucion.Calculo.RutaArchivo.Remove(this.EjecucionToolTips.Ejecucion.Calculo.RutaArchivo.LastIndexOf("\\"));
                    switch (ConfigArchivoAsociado.ConfiguracionSeleccionCarpeta)
                    {
                        case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                            switch (ConfigArchivoAsociado.ConfiguracionSeleccionArchivo)
                            {
                                case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                    SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                                    seleccionar.ModoEjecucion = true;
                                    seleccionar.Entrada = ElementoAsociado.EntradaRelacionada;
                                    seleccionar.ArchivoEntrada = ConfigArchivoAsociado.ElementoOrigenDatosAsociado_Diseño;
                                    seleccionar.RutaCarpeta = ConfigArchivoAsociado.RutaArchivo;
                                    seleccionar.descripcionEntrada.Text = EntradaAsociada.Nombre;
                                    seleccionar.titulo.Text += "\nEjecución del cálculo " + this.EjecucionToolTips.Ejecucion.Calculo.NombreArchivo; //+ " - " + this.EjecucionToolTips.ObtenerCalculo(this.EjecucionToolTips.Ejecucion.Calculo.ID).Nombre;

                                    bool selecciona = (bool)seleccionar.ShowDialog();
                                    if (selecciona == true)
                                    {
                                        ruta = ConfigArchivoAsociado.ProcesarCarpetasContenedoras(seleccionar.RutaCarpeta, seleccionar.TextoSeleccionado);

                                        if (seleccionar.Pausado)
                                        {
                                            this.EjecucionToolTips.Ejecucion.Pausar();
                                        }
                                    }
                                    else if (selecciona == false)
                                    {
                                        this.EjecucionToolTips.Ejecucion.Detener();
                                        return;
                                    }

                                    break;

                                case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                                    if (ConfigArchivoAsociado.ConfigSeleccionarArchivo)
                                    {
                                        seleccionar = new SeleccionarArchivoEntrada();
                                        seleccionar.ModoEjecucion = true;
                                        seleccionar.Entrada = ElementoAsociado.EntradaRelacionada;
                                        seleccionar.ArchivoEntrada = ConfigArchivoAsociado.ElementoOrigenDatosAsociado_Diseño;
                                        seleccionar.RutaCarpeta = ConfigArchivoAsociado.RutaArchivo;
                                        seleccionar.descripcionEntrada.Text = EntradaAsociada.Nombre;
                                        seleccionar.titulo.Text += "\nEjecución del cálculo " + this.EjecucionToolTips.Ejecucion.Calculo.NombreArchivo; //+ " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                        selecciona = (bool)seleccionar.ShowDialog();
                                        if (selecciona == true)
                                        {
                                            ruta = ConfigArchivoAsociado.ProcesarCarpetasContenedoras(seleccionar.RutaCarpeta, seleccionar.TextoSeleccionado);

                                            if (seleccionar.Pausado)
                                            {
                                                this.EjecucionToolTips.Ejecucion.Pausar();
                                            }
                                        }
                                        else if (selecciona == false)
                                        {
                                            this.EjecucionToolTips.Ejecucion.Detener();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ruta = ConfigArchivoAsociado.RutaArchivo;
                                    }

                                    break;
                            }

                            break;

                        case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                            switch (ConfigArchivoAsociado.ConfiguracionSeleccionArchivo)
                            {
                                case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:

                                    SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                                    seleccionar.ModoEjecucion = true;
                                    seleccionar.Entrada = ElementoAsociado.EntradaRelacionada;
                                    seleccionar.ArchivoEntrada = ConfigArchivoAsociado.ElementoOrigenDatosAsociado_Diseño;
                                    seleccionar.RutaCarpeta = rutaArchivoCalculo;
                                    seleccionar.descripcionEntrada.Text = EntradaAsociada.Nombre;
                                    seleccionar.titulo.Text += "\nEjecución del cálculo " + this.EjecucionToolTips.Ejecucion.Calculo.NombreArchivo; //+ " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                    bool selecciona = (bool)seleccionar.ShowDialog();
                                    if (selecciona == true)
                                    {
                                        ruta = ConfigArchivoAsociado.ProcesarCarpetasContenedoras(rutaArchivoCalculo, seleccionar.TextoSeleccionado);

                                        if (seleccionar.Pausado)
                                        {
                                            this.EjecucionToolTips.Ejecucion.Pausar();
                                        }
                                    }
                                    else if (selecciona == false)
                                    {
                                        this.EjecucionToolTips.Ejecucion.Detener();
                                        return;
                                    }

                                    break;

                                case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:

                                    if (ConfigArchivoAsociado.ConfigSeleccionarArchivo)
                                    {
                                        seleccionar = new SeleccionarArchivoEntrada();
                                        seleccionar.ModoEjecucion = true;
                                        seleccionar.Entrada = ElementoAsociado.EntradaRelacionada;
                                        seleccionar.ArchivoEntrada = ConfigArchivoAsociado.ElementoOrigenDatosAsociado_Diseño;
                                        seleccionar.RutaCarpeta = rutaArchivoCalculo;
                                        seleccionar.descripcionEntrada.Text = EntradaAsociada.Nombre;
                                        seleccionar.titulo.Text += "\nEjecución del cálculo " + this.EjecucionToolTips.Ejecucion.Calculo.NombreArchivo; //+ " - " + itemCalculo.ElementoDiseñoCalculoRelacionado.Nombre;

                                        selecciona = (bool)seleccionar.ShowDialog();
                                        if (selecciona == true)
                                        {
                                            ruta = ConfigArchivoAsociado.ProcesarCarpetasContenedoras(rutaArchivoCalculo, seleccionar.TextoSeleccionado);

                                            if (seleccionar.Pausado)
                                            {
                                                this.EjecucionToolTips.Ejecucion.Pausar();
                                            }
                                        }
                                        else if (selecciona == false)
                                        {
                                            this.EjecucionToolTips.Ejecucion.Detener();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ruta = rutaArchivoCalculo + "\\" + ConfigArchivoAsociado.RutaArchivo;
                                    }

                                    break;
                            }

                            break;
                    }

                    ElementoAsociado.RutaArchivoSeleccionado_Tooltip = ruta;
                }

                ElementoAsociado.Actualizar_ToolTips = true;
                EjecucionToolTips.ObtenerCalculo(EjecucionToolTips.Calculo.ID).HayCambios = true;
                EjecucionToolTips.Ejecucion.Detener();
            }
        }
    }

    public sealed class NumeroItem
    {
        public string? Nombre { get; init; }
        public double Valor { get; init; }
        public IReadOnlyList<string> Textos { get; init; } = Array.Empty<string>();

        // Render amigable (evita loops y LastOrDefault)
        public string TextosPlano => Textos.Count == 0 ? "" : $"Textos: {string.Join(", ", Textos)}.";
    }

    public sealed class ClasificadorGrupo
    {
        public string? Nombre { get; init; }
        public IReadOnlyList<NumeroItem> Numeros { get; init; } = Array.Empty<NumeroItem>();
    }
}
