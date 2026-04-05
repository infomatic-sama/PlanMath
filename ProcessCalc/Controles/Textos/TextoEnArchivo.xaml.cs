using ProcessCalc.Controles.Textos;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para TextoEnArchivo.xaml
    /// </summary>
    public partial class TextoEnArchivo : UserControl
    {
        //private int IndiceCaracterNumeroAnterior = -1;        
        private const string cadenaFormatoNumero = "     ";
        private const string cadenaFormatoNumeroGuardar = "/*n*/";
        private const string cadenaFormatoDatosGuardar = "/*d*/";
        private const string cadenaFormatoTextosGuardar = "/*t*/";
        private bool ModificandoTexto = true;
        //private bool NumeroBorrado = false;
        private Image imagenNumero;
        private bool DesdeImagenNumeroTexto;
        private bool DesdeImagenDatosTexto;
        private bool arrastreDatos;
        private InfoArrastreDatos datosActualImagen;
        private List<InfoArrastreDatos> datosBorrados;
        private bool click;
        private string IdImagenSeleccionada;
        //private int indiceSeleccionado;
        public BusquedaTextoArchivo Busqueda { get; set; }
        //private bool LineaVacia;
        private bool ConNumero;
        private bool teclaFlechaDerechaPresionada;
        private bool teclaFlechaIzquierdaPresionada;
        private const int espacioIcono = 2;
        private bool textoInformacion_Arrastre = false;
        public bool ModoTextosInformacion { get; set; }
        public CondicionConjuntoBusquedas CondicionSeleccionada { get; set; }
        public CondicionConjuntoBusquedas CondicionSeleccionadaFiltros { get; set; }
        public bool ModoBusqueda { get; set; }
        public TextoEnArchivo()
        {
            InitializeComponent();
            datosBorrados = new List<InfoArrastreDatos>();
        }

        private void textoArchivo_KeyUp(object sender, KeyEventArgs e)
        {
            //indiceSeleccionado = 0;
            
        }

        public string ObtenerCadenaFormatoNumeroGuardar()
        {
            return cadenaFormatoNumeroGuardar;
        }

        public string ObtenerCadenaFormatoNumero()
        {
            return cadenaFormatoNumero;
        }

        public string ObtenerCadenaFormatoDatosGuardar()
        {
            return cadenaFormatoDatosGuardar;
        }
        public string ObtenerCadenaFormatoTextosGuardar()
        {
            return cadenaFormatoTextosGuardar;
        }

        private void numero_Arrastre(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                click = true;
                textoInformacion_Arrastre = false;

                if (sender == imagenNumero)
                    DesdeImagenNumeroTexto = true;
                else
                    DesdeImagenNumeroTexto = false;
                arrastreDatos = false;
                DragDrop.DoDragDrop(this, numero.Source, DragDropEffects.Move);
            }
        }

        private void datos_Arrastre(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                click = true;
                if (sender == datos)
                {
                    DesdeImagenDatosTexto = true;
                    textoInformacion_Arrastre = false;
                }
                else
                {
                    DesdeImagenDatosTexto = false;
                    IdImagenSeleccionada = ((Image)sender).Tag.ToString();

                }
                arrastreDatos = true;
                DragDrop.DoDragDrop(this, datos.Source, DragDropEffects.Move);
            }
        }

        private void textos_Arrastre(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                click = true;
                if (sender == textos)
                {
                    DesdeImagenDatosTexto = true;
                    textoInformacion_Arrastre = true;
                }
                else
                {
                    DesdeImagenDatosTexto = false;
                    IdImagenSeleccionada = ((Image)sender).Tag.ToString();

                }
                arrastreDatos = true;
                DragDrop.DoDragDrop(this, datos.Source, DragDropEffects.Move);
            }
        }

        private void textoArchivo_Drop(object sender, DragEventArgs e)
        {
            if (click)
            {
                if (arrastreDatos)
                {
                    InfoArrastreDatos nuevoDatos;
                    if (DesdeImagenDatosTexto)
                    {
                        nuevoDatos = new InfoArrastreDatos();
                        nuevoDatos.TextoInformacion = textoInformacion_Arrastre;
                        Busqueda.DatosFormatoBusquedaTexto.Add(nuevoDatos);
                        nuevoDatos.IdImagen = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        nuevoDatos = (from InfoArrastreDatos I in Busqueda.DatosFormatoBusquedaTexto where I.IdImagen == IdImagenSeleccionada select I).FirstOrDefault();
                    }

                    if (nuevoDatos != null)
                    {
                        //nuevoDatos.IndiceCaracterDatosAnterior = nuevoDatos.IndiceCaracterDatos;
                        nuevoDatos.IndiceCaracterDatos = textoArchivo.GetCharacterIndexFromPoint(e.GetPosition(contenidoImagen), true);
                        
                        //if (nuevoDatos.IndiceCaracterDatosAnterior > -1 && 
                        //    nuevoDatos.IndiceCaracterDatos > nuevoDatos.IndiceCaracterDatosAnterior)
                        //    nuevoDatos.IndiceCaracterDatos += cadenaFormatoNumero.Length;
                        if (nuevoDatos.IndiceCaracterDatos >= textoArchivo.Text.Length)
                            nuevoDatos.IndiceCaracterDatos = textoArchivo.Text.Length - 1;
                        
                        if(nuevoDatos.IndiceCaracterDatos < textoArchivo.Text.Length)
                            nuevoDatos.Position = textoArchivo.GetRectFromCharacterIndex(nuevoDatos.IndiceCaracterDatos);
                        datosActualImagen = nuevoDatos;

                        //ProcesarTextoImagenDatos(e);
                        textoArchivo.Text = textoArchivo.Text.Insert(nuevoDatos.IndiceCaracterDatos, cadenaFormatoNumero);
                    }

                    if (DesdeImagenNumeroTexto) ConNumero = true;
                }
                else
                {
                    //IndiceCaracterNumeroAnterior = Busqueda.IndiceCaracterNumero;
                    Busqueda.IndiceCaracterNumero = textoArchivo.GetCharacterIndexFromPoint(e.GetPosition(contenidoImagen), true);
                    //if (Busqueda.IndiceCaracterNumero > IndiceCaracterNumeroAnterior)
                    //    Busqueda.IndiceCaracterNumero += cadenaFormatoNumero.Length;
                    Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);

                    //ProcesarTextoImagen(e);
                    textoArchivo.Text = textoArchivo.Text.Insert(Busqueda.IndiceCaracterNumero, cadenaFormatoNumero);
                    numero.AllowDrop = false;

                    MostrarOcultarOpciones_TextosInformacion(false);
                }

                //ReubicarNumeroDatos();
                click = false;
            }
        }

        private void textoArchivo_DragOver(object sender, DragEventArgs e)
        {
            if (!arrastreDatos)
            {
                if (imagenNumero == null)
                    e.Handled = numero.AllowDrop;
                else
                {
                    if (DesdeImagenNumeroTexto)
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
            }
            else
                e.Handled = true;
        }

        private void textoArchivo_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (!arrastreDatos)
            {
                if (imagenNumero == null)
                    e.Handled = numero.AllowDrop;
                else
                {
                    if (DesdeImagenNumeroTexto)
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
            }
            else
                e.Handled = true;
        }

        private void textoArchivo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded) return;

            foreach (var edicion in e.Changes)
            {
                int diferenciaCaracteres = edicion.AddedLength - edicion.RemovedLength;
                bool borrarIcono = false;

                if (ConNumero)
                {
                    if (diferenciaCaracteres > 0 && posicionActualOriginal > -1)
                    {
                        if (Busqueda.IndiceCaracterNumero >= posicionActualOriginal &&
                            Busqueda.IndiceCaracterNumero <= posicionActualOriginal + diferenciaCaracteres)
                            borrarIcono = true;
                    }
                    else if (diferenciaCaracteres < 0 && posicionActualOriginal > -1)
                    {
                        if (Busqueda.IndiceCaracterNumero >= posicionActualOriginal &&
                            Busqueda.IndiceCaracterNumero <= posicionActualOriginal - diferenciaCaracteres)
                            borrarIcono = true;
                    }

                    if (borrarIcono)
                    {
                        Busqueda.IndiceCaracterNumero = -1;
                        imagenNumero = null;
                        numero.AllowDrop = true;
                        //NumeroBorrado = false;
                        ConNumero = false;

                        MostrarOcultarOpciones_TextosInformacion(true);
                    }
                    else
                    {
                        if (posicionActualOriginal > -1 && posicionActualOriginal < Busqueda.IndiceCaracterNumero)
                        {
                            Busqueda.IndiceCaracterNumero += diferenciaCaracteres;
                            Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);
                        }


                    }
                }

                List<InfoArrastreDatos> datosBorrados = new List<InfoArrastreDatos>();

                foreach (var itemDatosArrastre in Busqueda.DatosFormatoBusquedaTexto)
                {
                    if (click && datosActualImagen == itemDatosArrastre) continue;

                    borrarIcono = false;

                    if (diferenciaCaracteres > 0 && posicionActualOriginal > -1)
                    {
                        if (itemDatosArrastre.IndiceCaracterDatos >= posicionActualOriginal &&
                            itemDatosArrastre.IndiceCaracterDatos <= posicionActualOriginal + diferenciaCaracteres)
                            borrarIcono = true;
                    }
                    else if (diferenciaCaracteres < 0 && posicionActualOriginal > -1)
                    {
                        if (itemDatosArrastre.IndiceCaracterDatos >= posicionActualOriginal &&
                            itemDatosArrastre.IndiceCaracterDatos <= posicionActualOriginal - diferenciaCaracteres)
                            borrarIcono = true;
                    }

                    if (borrarIcono)
                    {
                        datosBorrados.Add(itemDatosArrastre);

                        if (datosActualImagen != null) datosActualImagen = null;
                    }
                    else
                    {
                        if (posicionActualOriginal > -1 && posicionActualOriginal < itemDatosArrastre.IndiceCaracterDatos)
                        {
                            itemDatosArrastre.IndiceCaracterDatos += diferenciaCaracteres;
                            itemDatosArrastre.Position = textoArchivo.GetRectFromCharacterIndex(itemDatosArrastre.IndiceCaracterDatos);
                        }


                    }
                }

                while (datosBorrados.Any())
                {
                    Busqueda.DatosFormatoBusquedaTexto.Remove(datosBorrados.FirstOrDefault());
                    datosBorrados.Remove(datosBorrados.FirstOrDefault());
                }
            }

            EstablecerTextoGuardar();

            ReubicarNumeroDatos();

        }

        private void textoArchivo_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //if (LineaVacia) return;

            try
            {
                if (ModificandoTexto == false && Busqueda != null)
                {
                    //if (textoArchivo.SelectionLength == 0)
                    //{
                    //    datosBorrados.Clear();
                    //    NumeroBorrado = false;
                    //}
                    //else
                    //{
                    //if (indiceSeleccionado == 0) indiceSeleccionado = textoArchivo.SelectionStart;
                    if (teclaFlechaDerechaPresionada | teclaFlechaIzquierdaPresionada)
                    {

                        if (Busqueda != null && (textoArchivo.SelectionStart >= Busqueda.IndiceCaracterNumero &&
                            textoArchivo.SelectionStart <= Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length))
                        {
                            if (teclaFlechaDerechaPresionada)
                            {
                                //indiceSeleccionado = Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length + espacioIcono;

                                ModificandoTexto = true;
                                textoArchivo.SelectionStart = Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length + espacioIcono;
                                ModificandoTexto = false;

                                teclaFlechaDerechaPresionada = false;
                            }
                            else if (teclaFlechaIzquierdaPresionada)
                            {
                                //indiceSeleccionado = Busqueda.IndiceCaracterNumero - espacioIcono;

                                ModificandoTexto = true;
                                textoArchivo.SelectionStart = Busqueda.IndiceCaracterNumero - espacioIcono;
                                ModificandoTexto = false;

                                teclaFlechaIzquierdaPresionada = false;
                            }
                        }

                        if (Busqueda != null)
                        {
                            foreach (var itemInfoArrastre in Busqueda.DatosFormatoBusquedaTexto.OrderBy((i) => i.IndiceCaracterDatos))
                            {
                                if (Busqueda != null && (textoArchivo.SelectionStart >= itemInfoArrastre.IndiceCaracterDatos &&
                                textoArchivo.SelectionStart <= itemInfoArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length))
                                {
                                    if (teclaFlechaDerechaPresionada)
                                    {
                                        //indiceSeleccionado = itemInfoArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length + espacioIcono;

                                        ModificandoTexto = true;
                                        textoArchivo.SelectionStart = itemInfoArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length + espacioIcono;
                                        ModificandoTexto = false;

                                        teclaFlechaDerechaPresionada = false;
                                    }
                                    else if (teclaFlechaIzquierdaPresionada)
                                    {
                                        //indiceSeleccionado = itemInfoArrastre.IndiceCaracterDatos - espacioIcono;

                                        ModificandoTexto = true;
                                        textoArchivo.SelectionStart = itemInfoArrastre.IndiceCaracterDatos - espacioIcono;
                                        ModificandoTexto = false;

                                        teclaFlechaIzquierdaPresionada = false;
                                    }
                                }
                            }
                        }
                    }
                    //}

                    if (textoArchivo.SelectionLength > 0)
                    {
                        ModificandoTexto = true;

                        //int cantidadCaracteres = ((ConNumero) ? cadenaFormatoNumero.Length : 0);

                        int indiceSeleccion;
                        bool seleccionTextoHaciaDerecha = false;

                        indiceSeleccion = ObtenerIndiceSeleccion(ref seleccionTextoHaciaDerecha);

                        if (ConNumero)
                        {
                            if (indiceSeleccion >= Busqueda.IndiceCaracterNumero &
                            indiceSeleccion <= Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length)
                            {
                                if (seleccionTextoHaciaDerecha)
                                {
                                    textoArchivo.Select(textoArchivo.SelectionStart, cadenaFormatoNumero.Length);
                                    indiceSeleccion += cadenaFormatoNumero.Length;
                                }
                                else
                                {
                                    textoArchivo.Select(textoArchivo.SelectionStart, -cadenaFormatoNumero.Length);
                                    indiceSeleccion -= cadenaFormatoNumero.Length;
                                }

                                //indiceSeleccion = ObtenerIndiceSeleccion(ref seleccionTextoHaciaDerecha);
                            }

                            //if (seleccionTextoHaciaDerecha)
                            //{
                            //    if (textoArchivo.SelectionStart < Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length &
                            //        textoArchivo.SelectionStart < Busqueda.IndiceCaracterNumero &
                            //        indiceSeleccion > Busqueda.IndiceCaracterNumero &
                            //        indiceSeleccion > Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length)
                            //    {
                            //        NumeroBorrado = true;
                            //    }
                            //    else
                            //    {
                            //        NumeroBorrado = false;
                            //    }
                            //}
                            //else
                            //{
                            //    if (indiceSeleccion < Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length &
                            //        indiceSeleccion < Busqueda.IndiceCaracterNumero &
                            //        textoArchivo.SelectionStart > Busqueda.IndiceCaracterNumero &
                            //        textoArchivo.SelectionStart > Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length)
                            //    {
                            //        NumeroBorrado = true;
                            //    }
                            //    else
                            //    {
                            //        NumeroBorrado = false;
                            //    }
                            //}
                        }
                        //else if (textoArchivo.SelectionStart >= Busqueda.IndiceCaracterNumero &
                        //    textoArchivo.SelectionStart <= Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length)
                        //{
                        //    textoArchivo.SelectionStart = Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length;
                        //    textoArchivo.SelectionLength = indiceSeleccionado - textoArchivo.SelectionStart;
                        //}

                        foreach (var itemDatosArrastre in Busqueda.DatosFormatoBusquedaTexto)
                        {
                            if (indiceSeleccion >= itemDatosArrastre.IndiceCaracterDatos &
                            indiceSeleccion <= itemDatosArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length)
                            {
                                if (seleccionTextoHaciaDerecha)
                                {
                                    textoArchivo.Select(textoArchivo.SelectionStart, cadenaFormatoNumero.Length);
                                    indiceSeleccion += cadenaFormatoNumero.Length;
                                }
                                else
                                {
                                    textoArchivo.Select(textoArchivo.SelectionStart, -cadenaFormatoNumero.Length);
                                    indiceSeleccion -= cadenaFormatoNumero.Length;
                                }

                                //indiceSeleccion = ObtenerIndiceSeleccion(ref seleccionTextoHaciaDerecha);
                            }

                            //if (seleccionTextoHaciaDerecha)
                            //{
                            //    if (textoArchivo.SelectionStart < itemDatosArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length &
                            //        textoArchivo.SelectionStart < itemDatosArrastre.IndiceCaracterDatos &
                            //        indiceSeleccion > itemDatosArrastre.IndiceCaracterDatos &&
                            //        indiceSeleccion > itemDatosArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length)
                            //    {
                            //        datosBorrados.Add(itemDatosArrastre);
                            //    }
                            //    else
                            //    {
                            //        datosBorrados.Remove(itemDatosArrastre);
                            //    }
                            //}
                            //else
                            //{
                            //    if (indiceSeleccion < itemDatosArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length &&
                            //        indiceSeleccion < itemDatosArrastre.IndiceCaracterDatos &
                            //        textoArchivo.SelectionStart > itemDatosArrastre.IndiceCaracterDatos &
                            //        textoArchivo.SelectionStart > itemDatosArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length)
                            //    {
                            //        datosBorrados.Add(itemDatosArrastre);
                            //    }
                            //    else
                            //    {
                            //        datosBorrados.Remove(itemDatosArrastre);
                            //    }
                            //}
                            //else if (textoArchivo.SelectionStart >= itemDatosArrastre.IndiceCaracterDatos &
                            //    textoArchivo.SelectionStart <= itemDatosArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length)
                            //{
                            //    textoArchivo.SelectionStart = itemDatosArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length;

                            //    if (indiceSeleccionado >= textoArchivo.SelectionStart)
                            //        textoArchivo.SelectionLength = indiceSeleccionado - textoArchivo.SelectionStart;
                            //    //else
                            //    //    textoArchivo.SelectionLength = 0;
                            //}
                        }

                        ModificandoTexto = false;

                    }
                    //else if (textoArchivo.SelectionStart > -1)
                    //{
                    //    ModificandoTexto = true;

                    //    if (ConNumero)
                    //    {
                    //        if (textoArchivo.SelectionStart >= Busqueda.IndiceCaracterNumero - espacioIcono &
                    //        textoArchivo.SelectionStart <= Busqueda.IndiceCaracterNumero + cadenaFormatoNumero.Length + espacioIcono)
                    //        {
                    //            NumeroBorrado = true;
                    //        }
                    //        else
                    //        {
                    //            NumeroBorrado = false;
                    //        }
                    //    }

                    //    foreach (var itemDatosArrastre in Busqueda.DatosFormatoBusquedaTexto)
                    //    {
                    //        if (textoArchivo.SelectionStart >= itemDatosArrastre.IndiceCaracterDatos - espacioIcono &
                    //        textoArchivo.SelectionStart <= itemDatosArrastre.IndiceCaracterDatos + cadenaFormatoNumero.Length + espacioIcono)
                    //        {
                    //            datosBorrados.Add(itemDatosArrastre);
                    //        }
                    //        else
                    //        {
                    //            datosBorrados.Remove(itemDatosArrastre);
                    //        }
                    //    }

                    //    ModificandoTexto = false;
                    //}

                    posicionActualOriginal = textoArchivo.SelectionStart;
                }
            }
            catch (Exception) { }
        }

        private int ObtenerIndiceSeleccion(ref bool seleccionTextoHaciaDerecha)
        {
            int indiceSeleccion = 0;
            seleccionTextoHaciaDerecha = false;

            if (textoArchivo.SelectionStart < textoArchivo.SelectionStart + textoArchivo.SelectionLength)
            {
                indiceSeleccion = textoArchivo.SelectionStart + textoArchivo.SelectionLength;
                seleccionTextoHaciaDerecha = true;
            }
            else
            {
                indiceSeleccion = textoArchivo.SelectionStart - textoArchivo.SelectionLength;
                seleccionTextoHaciaDerecha = false;
            }

            return indiceSeleccion;
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ModoTextosInformacion)
            {
                textoNormal.Visibility = Visibility.Collapsed;
                textoTextosInformacion.Visibility = Visibility.Visible;

                opcionesTextosInformacion.Visibility = Visibility.Collapsed;
                opcionesModoNormal.Visibility = Visibility.Collapsed;
                opcionesModoTextosInformacion.Visibility = Visibility.Visible;

                textoEtiqueta.Visibility = Visibility.Collapsed;
                textoEtiquetaTextosInformacion.Visibility = Visibility.Visible;

                textoIconoNumero.Visibility = Visibility.Collapsed;
                numero.Visibility = Visibility.Collapsed;
                //textoIconoDatos.Visibility = Visibility.Collapsed;
                //datos.Visibility = Visibility.Collapsed;
            }

            if (nombreBusqueda.Text.Equals("Nombre búsqueda seleccionada"))
                nombreBusqueda.Text = string.Empty;

            if (Busqueda != null)
            {
                if (Busqueda.IndiceCaracterNumero > -1 | Busqueda.DatosFormatoBusquedaTexto.Count > 0)
                {
                    if (!ModoBusqueda)
                    {
                        if (Busqueda.IndiceCaracterNumero > -1)
                            Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);

                        foreach (var itemArrastreDatos in Busqueda.DatosFormatoBusquedaTexto)
                        {
                            if(itemArrastreDatos.IndiceCaracterDatos < textoArchivo.Text.Length)
                                itemArrastreDatos.Position = textoArchivo.GetRectFromCharacterIndex(itemArrastreDatos.IndiceCaracterDatos);
                        }
                    }

                    ReubicarNumeroDatos();
                }
                if (Busqueda.IndiceCaracterNumero > -1)
                {
                    numero.AllowDrop = false;
                }

                switch (Busqueda.FinalizacionBusqueda)
                {
                    case OpcionFinBusquedaTexto_Archivos.EncontrarNveces:
                        opcionNveces.IsChecked = true;
                        break;

                    case OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida:
                        opcionMientrasCondicionesCumplan.IsChecked = true;
                        break;

                    case OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida:
                        opcionHastaCondicionesCumplan.IsChecked = true;
                        break;

                    case OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo:
                        opcionHastaFinalArchivo.IsChecked = true;
                        break;
                }

                ListarCodificaciones_MostrarCodificacionSeleccionada(Busqueda.Codificacion);
                ListarCondicionesConjunto_Filtros();
            }
            ModificandoTexto = false;

            MostrarMensaje_IconosJuntos();

            if(ModoBusqueda)
            {
                opcionesCondicionesBusquedaRepeticiones.Visibility = Visibility.Collapsed;
                opcionesCondicionesFiltrosBusqueda.Visibility = Visibility.Collapsed;
                opcionesCodificacion.Visibility = Visibility.Collapsed;
                opcionesTextosInformacion.Visibility = Visibility.Collapsed;
                opcionesModoNormal.Visibility = Visibility.Collapsed;
                opcionesModoTextosInformacion.Visibility = Visibility.Collapsed;
            }
        }

        private void textoArchivo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            contenidoImagen.RenderSize = textoArchivo.RenderSize;
        }

        private void txtVeces_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numeroVeces = 0;
            int.TryParse(txtVeces.Text, out numeroVeces);
            if (Busqueda != null) Busqueda.NumeroVecesBusquedaNumero = numeroVeces;
        }

        private void textoArchivo_KeyDown(object sender, KeyEventArgs e)
        {
            teclaFlechaDerechaPresionada = false;
            teclaFlechaIzquierdaPresionada = false;

            if (e.Key == Key.Right)
                teclaFlechaDerechaPresionada = true;
            if (e.Key == Key.Left)
                teclaFlechaIzquierdaPresionada = true;

            //posicionActualOriginal = textoArchivo.SelectionStart;
        }

        private void opcionNveces_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionNveces.IsChecked == true)
                    Busqueda.FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarNveces;
            }
        }

        private void opcionMientrasCondicionesCumplan_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionMientrasCondicionesCumplan.IsChecked == true)
                    Busqueda.FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida;

                textoCondicionesBusqueda.Visibility = Visibility.Visible;
                opcionesCondicionesBusqueda.Visibility = Visibility.Visible;
                listaCondiciones.Visibility = Visibility.Visible;

                ListarCondicionesConjunto();
            }
        }

        private void opcionHastaFinalArchivo_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionHastaFinalArchivo.IsChecked == true)
                    Busqueda.FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo;
            }
        }

        private void ListarCodificaciones_MostrarCodificacionSeleccionada(string codificacionSeleccionada)
        {
            EncodingInfo[] codificaciones = Encoding.GetEncodings();

            opcionCodificacion.Items.Clear();
            foreach (var codificacion in codificaciones)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = codificacion.DisplayName;
                item.Tag = codificacion.Name;

                opcionCodificacion.Items.Add(item);

                if (!string.IsNullOrEmpty(codificacionSeleccionada) &&
                    ((ComboBoxItem)item).Tag.ToString().Equals(codificacionSeleccionada))
                    opcionCodificacion.SelectedItem = item;
            }

            
        }

        private void opcionCodificacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionCodificacion.SelectedItem != null)
                {
                    Busqueda.Codificacion = ((ComboBoxItem)opcionCodificacion.SelectedItem).Tag.ToString();
                }
            }
        }

        private void opcionNumeroActual_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionNumeroActual.IsChecked == true)
                    Busqueda.OpcionTextosInformacion = OpcionTextosInformacionBusqueda.NumeroActual;

                opcionAsignarTextosInformacionNumeros.Visibility = Visibility.Collapsed;
                opcionAsignarTextosInformacionNumeros_Iteraciones.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionUltimoNumeroEncontrado_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionUltimoNumeroEncontrado.IsChecked == true)
                    Busqueda.OpcionTextosInformacion = OpcionTextosInformacionBusqueda.UltimoNumeroEncontrado;

                opcionAsignarTextosInformacionNumeros.Visibility = Visibility.Visible;
                opcionAsignarTextosInformacionNumeros_Iteraciones.Visibility = Visibility.Visible;

                if (busquedas_SeleccionNumeros.Visibility == Visibility.Visible)
                {
                    busquedas_SeleccionNumeros.OpcionTextosInformacion = Busqueda.OpcionTextosInformacion;
                    busquedas_SeleccionNumeros.ListarBusquedas();
                }
            }
        }

        private void opcionSiguienteNumeroAEncontrar_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionSiguienteNumeroAEncontrar.IsChecked == true)
                    Busqueda.OpcionTextosInformacion = OpcionTextosInformacionBusqueda.SiguienteNumeroAEncontrar;

                opcionAsignarTextosInformacionNumeros.Visibility = Visibility.Visible;
                opcionAsignarTextosInformacionNumeros.Visibility = Visibility.Visible;
                opcionIteracionActual.Visibility = Visibility.Visible;

                if (busquedas_SeleccionNumeros.Visibility == Visibility.Visible)
                {
                    busquedas_SeleccionNumeros.OpcionTextosInformacion = Busqueda.OpcionTextosInformacion;
                    busquedas_SeleccionNumeros.ListarBusquedas();
                }
            }
        }

        private void opcionUsarCeros_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionUsarCeros.IsChecked == true)
                    Busqueda.UsarCantidad_SiNohayNumeros = true;
            }
        }

        private void opcionUsarCeros_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionUsarCeros.IsChecked == false)
                    Busqueda.UsarCantidad_SiNohayNumeros = false;
            }
        }

        private void textoArchivo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //posicionActualOriginal = textoArchivo.SelectionStart;
        }

        private void opcionAsignarTextosInformacionNumeros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Busqueda != null)
                Busqueda.OpcionAsignarTextosInformacion_Numeros = (OpcionAsignarTextosInformacion_NumerosBusqueda)int.Parse(((ComboBoxItem)opcionAsignarTextosInformacionNumeros.SelectedItem).Uid);

            textosInformacion_SeleccionNumeros.Visibility = Visibility.Collapsed;
            busquedas_SeleccionNumeros.Visibility = Visibility.Collapsed;

            switch (Busqueda.OpcionAsignarTextosInformacion_Numeros)
            {
                case OpcionAsignarTextosInformacion_NumerosBusqueda.TextosInformacionPrevios:
                    textosInformacion_SeleccionNumeros.Visibility = Visibility.Visible;
                    break;

                case OpcionAsignarTextosInformacion_NumerosBusqueda.BusquedasNumeros:
                    busquedas_SeleccionNumeros.Visibility = Visibility.Visible;
                    busquedas_SeleccionNumeros.ListarBusquedas();
                    break;
            }
        }

        private void cantidadNumerosAsignarTextosInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cantidadNumeros = 0;
            int.TryParse(cantidadNumerosAsignarTextosInformacion.Text, out cantidadNumeros);
            if (Busqueda != null) Busqueda.CantidadNumeros_TextosInformacion_AsignarNumeros = cantidadNumeros;
        }

        private void btnDefinirTextosInformacionFijos_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarOrdenarCantidades seleccionarOrdenar = new SeleccionarOrdenarCantidades();
            seleccionarOrdenar.listaTextos.TextosInformacion = Busqueda.TextosInformacionFijos.ToList();

            bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
            if (definicionEstablecida)
            {
                Busqueda.TextosInformacionFijos = seleccionarOrdenar.listaTextos.TextosInformacion;
            }
        }

        private void opcionesNombresCantidades_Click(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (Busqueda.DefinicionOpcionesNombresCantidades == null)
                    Busqueda.DefinicionOpcionesNombresCantidades = new Entidades.TextosInformacion.DefinicionTextoNombresCantidades();

                ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
                establecer.TextosNombre = Busqueda.DefinicionOpcionesNombresCantidades.ReplicarObjeto();
                
                if(establecer.ShowDialog() == true)
                {
                    Busqueda.DefinicionOpcionesNombresCantidades = establecer.TextosNombre;
                }
            }
        }

        private void numero_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (numero.AllowDrop)
            {
                if (textoArchivo.SelectionStart >= 0 && textoArchivo.SelectionLength == 0)
                {
                    //int posicionInicial = textoArchivo.SelectionStart - 1;

                    //if (textoArchivo.SelectionLength > 0)
                    //{
                    //    textoArchivo.Text = textoArchivo.Text.Remove(posicionInicial, textoArchivo.SelectionLength + 1);
                    //    textoArchivo.SelectionStart = posicionInicial;
                    //}

                    //IndiceCaracterNumeroAnterior = Busqueda.IndiceCaracterNumero;
                    Busqueda.IndiceCaracterNumero = textoArchivo.SelectionStart;

                    if (Busqueda.IndiceCaracterNumero >= textoArchivo.Text.Length)
                        Busqueda.IndiceCaracterNumero = textoArchivo.Text.Length - 1;

                    Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);

                    //ProcesarTextoImagen(e);
                    textoArchivo.Text = textoArchivo.Text.Insert(Busqueda.IndiceCaracterNumero, cadenaFormatoNumero);
                    numero.AllowDrop = false;

                    MostrarOcultarOpciones_TextosInformacion(false);

                    //ReubicarNumeroDatos();

                    //textoArchivo.Text += "  ";
                    //textoArchivo.SelectionStart = posicionInicial + cadenaFormatoNumero.Length + 2;
                    //textoArchivo.Focus();
                }
            }
        }

        private void datos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AgregarIconoDatos_TextoInformacion(false);
        }

        private void AgregarIconoDatos_TextoInformacion(bool textoInformacion_Clic)
        {
            if (textoArchivo.SelectionStart >= 0 && textoArchivo.SelectionLength == 0)
            {
                //int posicionInicial = textoArchivo.SelectionStart;

                //if (textoArchivo.SelectionLength > 0)
                //{
                //    textoArchivo.Text = textoArchivo.Text.Remove(posicionInicial, textoArchivo.SelectionLength);
                //    textoArchivo.SelectionStart = posicionInicial;
                //}

                InfoArrastreDatos nuevoDatos = new InfoArrastreDatos();
                nuevoDatos.TextoInformacion = textoInformacion_Clic;
                Busqueda.DatosFormatoBusquedaTexto.Add(nuevoDatos);
                nuevoDatos.IdImagen = Guid.NewGuid().ToString();

                //nuevoDatos.IndiceCaracterDatosAnterior = nuevoDatos.IndiceCaracterDatos;
                nuevoDatos.IndiceCaracterDatos = textoArchivo.SelectionStart;

                if (nuevoDatos.IndiceCaracterDatos >= textoArchivo.Text.Length)
                    nuevoDatos.IndiceCaracterDatos = textoArchivo.Text.Length - 1;

                nuevoDatos.Position = textoArchivo.GetRectFromCharacterIndex(nuevoDatos.IndiceCaracterDatos);
                datosActualImagen = nuevoDatos;

                //ProcesarTextoImagenDatos(e);
                textoArchivo.Text = textoArchivo.Text.Insert(nuevoDatos.IndiceCaracterDatos, cadenaFormatoNumero);

                //ProcesarTextoImagenDatos(null);

                //EstablecerTextoGuardar();
                //ReubicarNumeroDatos();

                //textoArchivo.Text += "  ";
                //textoArchivo.SelectionStart = posicionInicial + cadenaFormatoNumero.Length + 2;
                //textoArchivo.Focus();
            }
        }

        private void textos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AgregarIconoDatos_TextoInformacion(true);
        }

        private void opcionHastaCondicionesCumplan_Checked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                if (opcionHastaCondicionesCumplan.IsChecked == true)
                    Busqueda.FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida;

                textoCondicionesBusqueda.Visibility = Visibility.Visible;
                opcionesCondicionesBusqueda.Visibility = Visibility.Visible;
                listaCondiciones.Visibility = Visibility.Visible;

                ListarCondicionesConjunto();
            }
        }

        private void ListarCondicionesConjunto()
        {
            listaCondiciones.Children.Clear();

            //foreach (var itemCondicion in Busqueda.Condiciones)
            //{
            if (Busqueda.CondicionesTextoBusqueda != null)
            {
                EtiquetaCondicionConjuntoBusquedas etiquetaCondicion = new EtiquetaCondicionConjuntoBusquedas();
                etiquetaCondicion.VistaBusquedaTexto = this;
                etiquetaCondicion.Condicion = Busqueda.CondicionesTextoBusqueda;
                listaCondiciones.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
            }
            //}
        }

        private void ListarCondicionesConjunto_Filtros()
        {
            listaCondicionesFiltros.Children.Clear();

            //foreach (var itemCondicion in Busqueda.Condiciones)
            //{
            if (Busqueda.CondicionesTextoBusqueda_Filtros != null)
            {
                EtiquetaCondicionConjuntoBusquedas etiquetaCondicion = new EtiquetaCondicionConjuntoBusquedas();
                etiquetaCondicion.VistaBusquedaTexto = this;
                etiquetaCondicion.ModoFiltros = true;
                etiquetaCondicion.Condicion = Busqueda.CondicionesTextoBusqueda_Filtros;
                listaCondicionesFiltros.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
            }
            //}
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionConjuntoBusquedas agregar = new AgregarQuitar_CondicionConjuntoBusquedas();
            agregar.ListaBusquedas = new List<BusquedaTextoArchivo>() { Busqueda };
            agregar.ModoTextoBusqueda = true;

            agregar.ShowDialog();

            if (agregar.Aceptar)
            {
                agregar.Condicion.CondicionContenedora = CondicionSeleccionada;

                if (CondicionSeleccionada != null)
                {
                    agregar.Condicion.CondicionContenedora = CondicionSeleccionada;
                    CondicionSeleccionada.Condiciones.Add(agregar.Condicion);
                }
                else
                {
                    if (Busqueda.CondicionesTextoBusqueda == null)
                    {
                        Busqueda.CondicionesTextoBusqueda = new CondicionConjuntoBusquedas();
                        Busqueda.CondicionesTextoBusqueda.ContenedorCondiciones = true;
                    }
                    
                    agregar.Condicion.CondicionContenedora = Busqueda.CondicionesTextoBusqueda;
                    Busqueda.CondicionesTextoBusqueda.Condiciones.Add(agregar.Condicion);
                    
                }

                ListarCondicionesConjunto();
            }
        }

        private void editarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                AgregarQuitar_CondicionConjuntoBusquedas editar = new AgregarQuitar_CondicionConjuntoBusquedas();
                editar.ListaBusquedas = new List<BusquedaTextoArchivo>() { Busqueda };
                editar.ModoTextoBusqueda = true;

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
                    Busqueda.CondicionesTextoBusqueda = null;

                CondicionSeleccionada.CondicionContenedora = null;
                CondicionSeleccionada = null;
                ListarCondicionesConjunto();
            }
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
                            if (CondicionSeleccionada.CondicionContenedora != Busqueda.CondicionesTextoBusqueda)
                                indiceCondicionContenedora = Busqueda.CondicionesTextoBusqueda.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Busqueda.CondicionesTextoBusqueda.Condiciones.IndexOf(CondicionSeleccionada);

                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            if (indiceCondicionContenedora > -1)
                                Busqueda.CondicionesTextoBusqueda.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionada);
                            else
                                Busqueda.CondicionesTextoBusqueda.Condiciones.Add(CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = Busqueda.CondicionesTextoBusqueda;

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
                                if (agregarFinal)
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
                            if (CondicionSeleccionada.CondicionContenedora != Busqueda.CondicionesTextoBusqueda)
                                indiceCondicionContenedora = Busqueda.CondicionesTextoBusqueda.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Busqueda.CondicionesTextoBusqueda.Condiciones.IndexOf(CondicionSeleccionada);

                            CondicionConjuntoBusquedas condicionSiguiente = null;
                            bool agregarFinal = false;
                            if (indiceCondicionContenedora + 1 == Busqueda.CondicionesTextoBusqueda.Condiciones.Count)
                            {
                                condicionSiguiente = Busqueda.CondicionesTextoBusqueda.Condiciones.Last();
                                if (condicionSiguiente == CondicionSeleccionada.CondicionContenedora)
                                {
                                    agregarFinal = true;
                                }
                            }
                            else
                                condicionSiguiente = Busqueda.CondicionesTextoBusqueda.Condiciones.ElementAt(indiceCondicionContenedora + 1);

                            if (condicionSiguiente.Condiciones.Any())
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                if (agregarFinal)
                                    condicionSiguiente.Condiciones.Add(CondicionSeleccionada);
                                else
                                    condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionSiguiente;

                                ListarCondicionesConjunto();
                            }
                            else
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                Busqueda.CondicionesTextoBusqueda.Condiciones.Add(CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = Busqueda.CondicionesTextoBusqueda;
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

        public void DesmarcarCondicionesBusquedas()
        {
            foreach (EtiquetaCondicionConjuntoBusquedas itemCondicion in listaCondiciones.Children)
            {
                itemCondicion.DesmarcarSeleccion();
            }
        }

        public void DesmarcarCondicionesBusquedas_Filtros()
        {
            foreach (EtiquetaCondicionConjuntoBusquedas itemCondicion in listaCondicionesFiltros.Children)
            {
                itemCondicion.DesmarcarSeleccion();
            }
        }

        private void opcionMientrasCondicionesCumplan_Unchecked(object sender, RoutedEventArgs e)
        {
            textoCondicionesBusqueda.Visibility = Visibility.Collapsed;
            opcionesCondicionesBusqueda.Visibility = Visibility.Collapsed;
            listaCondiciones.Visibility = Visibility.Collapsed;
        }

        private void opcionHastaCondicionesCumplan_Unchecked(object sender, RoutedEventArgs e)
        {
            textoCondicionesBusqueda.Visibility = Visibility.Collapsed;
            opcionesCondicionesBusqueda.Visibility = Visibility.Collapsed;
            listaCondiciones.Visibility = Visibility.Collapsed;
        }

        private void opcionReemplazarTextosInformacion_NombresCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if(Busqueda != null)
            {
                Busqueda.ReemplazarTextosInformacion_NombresCantidades = (bool)opcionReemplazarTextosInformacion_NombresCantidades.IsChecked;
            }
        }

        private void opcionReemplazarTextosInformacion_NombresCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Busqueda != null)
            {
                Busqueda.ReemplazarTextosInformacion_NombresCantidades = (bool)opcionReemplazarTextosInformacion_NombresCantidades.IsChecked;
            }
        }

        private void agregarCondicionFiltros_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionConjuntoBusquedas agregar = new AgregarQuitar_CondicionConjuntoBusquedas();
            agregar.ListaBusquedas = new List<BusquedaTextoArchivo>() { Busqueda };
            agregar.ModoTextoBusqueda = true;

            agregar.ShowDialog();

            if (agregar.Aceptar)
            {
                agregar.Condicion.CondicionContenedora = CondicionSeleccionadaFiltros;

                if (CondicionSeleccionadaFiltros != null)
                {
                    CondicionSeleccionadaFiltros.Condiciones.Add(agregar.Condicion);
                }
                else
                {
                    if (Busqueda.CondicionesTextoBusqueda_Filtros == null)
                    {
                        Busqueda.CondicionesTextoBusqueda_Filtros = agregar.Condicion;
                    }
                    else
                    {
                        agregar.Condicion.CondicionContenedora = Busqueda.CondicionesTextoBusqueda_Filtros;
                        Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.Add(agregar.Condicion);
                    }
                }

                ListarCondicionesConjunto_Filtros();
            }
        }

        private void editarCondicionFiltros_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionadaFiltros != null)
            {
                AgregarQuitar_CondicionConjuntoBusquedas editar = new AgregarQuitar_CondicionConjuntoBusquedas();
                editar.ListaBusquedas = new List<BusquedaTextoArchivo>() { Busqueda };
                editar.ModoTextoBusqueda = true;

                editar.ModoEdicion = true;
                editar.Condicion = CondicionSeleccionadaFiltros;
                editar.ShowDialog();

                if (editar.Aceptar)
                {
                    ListarCondicionesConjunto_Filtros();
                }
            }
        }

        private void quitarCondicionFiltros_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionadaFiltros != null)
            {
                if (CondicionSeleccionadaFiltros.CondicionContenedora != null)
                    CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.Remove(CondicionSeleccionadaFiltros);
                else
                    Busqueda.CondicionesTextoBusqueda_Filtros = null;

                CondicionSeleccionadaFiltros.CondicionContenedora = null;
                CondicionSeleccionadaFiltros = null;
                ListarCondicionesConjunto_Filtros();
            }
        }

        private void moverCondicionAIzquierdaFiltros_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionadaFiltros != null)
            {
                if (CondicionSeleccionadaFiltros.CondicionContenedora != null)
                {
                    int indice = CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.IndexOf(CondicionSeleccionadaFiltros);

                    if (indice - 1 == -1)
                    {
                        CondicionConjuntoBusquedas condicionContenedoraDestino = CondicionSeleccionadaFiltros.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionadaFiltros.CondicionContenedora);

                            if (condicionContenedoraDestino != null)
                            {
                                CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                                condicionContenedoraDestino.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionadaFiltros);
                                CondicionSeleccionadaFiltros.CondicionContenedora = condicionContenedoraDestino;

                                ListarCondicionesConjunto_Filtros();
                            }
                        }
                        else
                        {
                            int indiceCondicionContenedora = -1;
                            if (CondicionSeleccionadaFiltros.CondicionContenedora != Busqueda.CondicionesTextoBusqueda_Filtros)
                                indiceCondicionContenedora = Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.IndexOf(CondicionSeleccionadaFiltros.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.IndexOf(CondicionSeleccionadaFiltros);

                            CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                            if (indiceCondicionContenedora > -1)
                                Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionadaFiltros);
                            else
                                Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.Add(CondicionSeleccionadaFiltros);
                            CondicionSeleccionadaFiltros.CondicionContenedora = Busqueda.CondicionesTextoBusqueda_Filtros;

                            ListarCondicionesConjunto_Filtros();
                        }
                    }
                    else
                    {
                        CondicionConjuntoBusquedas condicionAnterior = CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.ElementAt(indice - 1);

                        if (condicionAnterior.Condiciones.Any())
                        {
                            CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionAnterior.Condiciones.Add(CondicionSeleccionadaFiltros);
                            CondicionSeleccionadaFiltros.CondicionContenedora = condicionAnterior;

                            ListarCondicionesConjunto_Filtros();
                        }
                        else
                        {
                            CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                            CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.Insert(indice - 1, CondicionSeleccionadaFiltros);

                            ListarCondicionesConjunto_Filtros();
                        }
                    }
                }
            }
        }

        private void moverCondicionADerechaFiltros_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionadaFiltros != null)
            {
                if (CondicionSeleccionadaFiltros.CondicionContenedora != null)
                {
                    int indice = CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.IndexOf(CondicionSeleccionadaFiltros);

                    if (indice + 1 == CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.Count)
                    {
                        CondicionConjuntoBusquedas condicionContenedoraDestino = CondicionSeleccionadaFiltros.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionadaFiltros.CondicionContenedora);

                            CondicionConjuntoBusquedas condicionSiguiente = null;
                            bool agregarFinal = false;
                            if (indiceCondicionContenedora + 1 == condicionContenedoraDestino.Condiciones.Count)
                            {
                                condicionSiguiente = condicionContenedoraDestino.Condiciones.Last();
                                if (condicionSiguiente == CondicionSeleccionadaFiltros.CondicionContenedora)
                                {
                                    condicionSiguiente = CondicionSeleccionadaFiltros.CondicionContenedora.CondicionContenedora;
                                    agregarFinal = true;
                                }
                            }
                            else
                                condicionSiguiente = condicionContenedoraDestino.Condiciones.ElementAt(indiceCondicionContenedora + 1);

                            if (condicionSiguiente.Condiciones.Any())
                            {
                                CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                                if (agregarFinal)
                                    condicionSiguiente.Condiciones.Add(CondicionSeleccionadaFiltros);
                                else
                                    condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionadaFiltros);
                                CondicionSeleccionadaFiltros.CondicionContenedora = condicionSiguiente;

                                ListarCondicionesConjunto_Filtros();
                            }
                            else
                            {
                                CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                                condicionContenedoraDestino.Condiciones.Add(CondicionSeleccionadaFiltros);
                                CondicionSeleccionadaFiltros.CondicionContenedora = condicionContenedoraDestino;
                            }
                        }
                        else
                        {
                            int indiceCondicionContenedora = -1;
                            if (CondicionSeleccionadaFiltros.CondicionContenedora != Busqueda.CondicionesTextoBusqueda_Filtros)
                                indiceCondicionContenedora = Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.IndexOf(CondicionSeleccionadaFiltros.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.IndexOf(CondicionSeleccionadaFiltros);

                            CondicionConjuntoBusquedas condicionSiguiente = null;
                            bool agregarFinal = false;
                            if (indiceCondicionContenedora + 1 == Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.Count)
                            {
                                condicionSiguiente = Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.Last();
                                if (condicionSiguiente == CondicionSeleccionadaFiltros.CondicionContenedora)
                                {
                                    agregarFinal = true;
                                }
                            }
                            else
                                condicionSiguiente = Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.ElementAt(indiceCondicionContenedora + 1);

                            if (condicionSiguiente.Condiciones.Any())
                            {
                                CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                                if (agregarFinal)
                                    condicionSiguiente.Condiciones.Add(CondicionSeleccionadaFiltros);
                                else
                                    condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionadaFiltros);
                                CondicionSeleccionadaFiltros.CondicionContenedora = condicionSiguiente;

                                ListarCondicionesConjunto_Filtros();
                            }
                            else
                            {
                                CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                                Busqueda.CondicionesTextoBusqueda_Filtros.Condiciones.Add(CondicionSeleccionadaFiltros);
                                CondicionSeleccionadaFiltros.CondicionContenedora = Busqueda.CondicionesTextoBusqueda_Filtros;
                            }

                            ListarCondicionesConjunto_Filtros();
                        }
                    }
                    else
                    {
                        CondicionConjuntoBusquedas condicionSiguiente = CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.ElementAt(indice + 1);

                        if (condicionSiguiente.Condiciones.Any())
                        {
                            CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionadaFiltros);
                            CondicionSeleccionadaFiltros.CondicionContenedora = condicionSiguiente;

                            ListarCondicionesConjunto_Filtros();
                        }
                        else
                        {
                            CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.RemoveAt(indice);
                            CondicionSeleccionadaFiltros.CondicionContenedora.Condiciones.Insert(indice + 1, CondicionSeleccionadaFiltros);

                            ListarCondicionesConjunto_Filtros();
                        }
                    }
                }
            }
        }

        private void txtCantidadUtilizar_NoEncontrados_TextChanged(object sender, TextChangedEventArgs e)
        {            
            int numero = 0;
            int.TryParse(txtCantidadUtilizar_NoEncontrados.Text, out numero);
            if (Busqueda != null) Busqueda.NumeroUtilizar_NoEncontrados = numero;            
        }

        private void opcionAsignarTextosInformacionNumeros_Iteraciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Busqueda != null)
                Busqueda.OpcionAsignarTextosInformacion_Numeros_Iteraciones = (OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones)int.Parse(((ComboBoxItem)opcionAsignarTextosInformacionNumeros_Iteraciones.SelectedItem).Uid);

        }

        private void cantidadNumerosAsignarTextosInformacion_Iteraciones_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cantidadNumeros = 0;
            int.TryParse(cantidadNumerosAsignarTextosInformacion_Iteraciones.Text, out cantidadNumeros);
            if (Busqueda != null) Busqueda.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones = cantidadNumeros;
        }

        private void opcionSiguienteNumeroAEncontrar_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionIteracionActual.Visibility = Visibility.Collapsed;
        }
    }
}
