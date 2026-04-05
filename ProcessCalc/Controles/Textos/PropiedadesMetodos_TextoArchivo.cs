using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace ProcessCalc.Controles
{
    public partial class TextoEnArchivo : UserControl
    {
        int posicionActualOriginal = -1;
        //private void ProcesarTextoImagen(DragEventArgs e)
        //{
        //    if (IndiceCaracterNumeroAnterior > -1)
        //    {
        //        if (IndiceCaracterNumeroAnterior > textoArchivo.Text.Length - 1)
        //            IndiceCaracterNumeroAnterior = textoArchivo.Text.Length - 1;

        //        ModificandoTexto = true;
        //        textoArchivo.Text = textoArchivo.Text.Remove(IndiceCaracterNumeroAnterior, cadenaFormatoNumero.Length);
        //        ModificandoTexto = false;

        //        if (IndiceCaracterNumeroAnterior < Busqueda.IndiceCaracterNumero)
        //            Busqueda.IndiceCaracterNumero -= cadenaFormatoNumero.Length;

        //        Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);
        //        foreach (var itemInfoArrastre in Busqueda.DatosFormatoBusquedaTexto.OrderBy((i) => i.IndiceCaracterDatos))
        //        {                    
        //            if (IndiceCaracterNumeroAnterior < itemInfoArrastre.IndiceCaracterDatos)
        //            {
        //                itemInfoArrastre.IndiceCaracterDatos -= cadenaFormatoNumero.Length;
        //            }

        //            if (IndiceCaracterNumeroAnterior < itemInfoArrastre.IndiceCaracterDatosAnterior)
        //            {
        //                itemInfoArrastre.IndiceCaracterDatosAnterior -= cadenaFormatoNumero.Length;
        //            }

        //            itemInfoArrastre.Position = textoArchivo.GetRectFromCharacterIndex(itemInfoArrastre.IndiceCaracterDatos);
        //        }
        //    }

        //    bool insertarCadenaNumero = false;

        //    if (Busqueda != null && textoArchivo.Text.Length - Busqueda.IndiceCaracterNumero <= cadenaFormatoNumero.Length)
        //    {
        //        insertarCadenaNumero = true;
        //    }
        //    else if (string.Compare(textoArchivo.Text.Substring(Busqueda.IndiceCaracterNumero, cadenaFormatoNumero.Length), cadenaFormatoNumero) != 0)
        //    {
        //        insertarCadenaNumero = true;
        //    }

        //    if (insertarCadenaNumero)
        //    {
        //        ModificandoTexto = true;
        //        textoArchivo.Text = textoArchivo.Text.Insert(Busqueda.IndiceCaracterNumero, cadenaFormatoNumero);
        //        ModificandoTexto = false;

        //        Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);
                
        //        foreach (var itemInfoArrastre in Busqueda.DatosFormatoBusquedaTexto.OrderBy((i) => i.IndiceCaracterDatos))
        //        {
        //            if (Busqueda.IndiceCaracterNumero < itemInfoArrastre.IndiceCaracterDatos)
        //            {
        //                itemInfoArrastre.IndiceCaracterDatos += cadenaFormatoNumero.Length;
        //            }

        //            if (Busqueda.IndiceCaracterNumero < itemInfoArrastre.IndiceCaracterDatosAnterior)
        //            {
        //                itemInfoArrastre.IndiceCaracterDatosAnterior += cadenaFormatoNumero.Length;
        //            }

        //            itemInfoArrastre.Position = textoArchivo.GetRectFromCharacterIndex(itemInfoArrastre.IndiceCaracterDatos);
        //        }
        //    }

        //    EstablecerTextoGuardar();
        //}

        //private void ProcesarTextoImagenDatos(DragEventArgs e)
        //{
        //    if (datosActualImagen.IndiceCaracterDatosAnterior > -1)
        //    {
        //        if (datosActualImagen.IndiceCaracterDatosAnterior > textoArchivo.Text.Length - 1)
        //            datosActualImagen.IndiceCaracterDatosAnterior = textoArchivo.Text.Length - 1;

        //        ModificandoTexto = true;
        //        textoArchivo.Text = textoArchivo.Text.Remove(datosActualImagen.IndiceCaracterDatosAnterior, cadenaFormatoNumero.Length);
        //        ModificandoTexto = false;

        //        if (datosActualImagen.IndiceCaracterDatosAnterior < Busqueda.IndiceCaracterNumero)
        //        {
        //            Busqueda.IndiceCaracterNumero -= cadenaFormatoNumero.Length;
        //        }

        //        if (datosActualImagen.IndiceCaracterDatosAnterior < IndiceCaracterNumeroAnterior)
        //        {
        //            IndiceCaracterNumeroAnterior -= cadenaFormatoNumero.Length;
        //        }

        //        if (datosActualImagen.IndiceCaracterDatosAnterior < datosActualImagen.IndiceCaracterDatos)
        //        {
        //            datosActualImagen.IndiceCaracterDatos -= cadenaFormatoNumero.Length;                    
        //        }

        //        datosActualImagen.Position = textoArchivo.GetRectFromCharacterIndex(datosActualImagen.IndiceCaracterDatos); 
        //        Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);

        //        foreach (var itemInfoArrastre in Busqueda.DatosFormatoBusquedaTexto.OrderBy((i) => i.IndiceCaracterDatos))
        //        {
        //            if (datosActualImagen.IndiceCaracterDatosAnterior < itemInfoArrastre.IndiceCaracterDatos)
        //            {
        //                itemInfoArrastre.IndiceCaracterDatos -= cadenaFormatoNumero.Length;
        //            }
        //            if (datosActualImagen.IndiceCaracterDatosAnterior < itemInfoArrastre.IndiceCaracterDatosAnterior)
        //            {
        //                itemInfoArrastre.IndiceCaracterDatosAnterior -= cadenaFormatoNumero.Length;
        //            }

        //            itemInfoArrastre.Position = textoArchivo.GetRectFromCharacterIndex(itemInfoArrastre.IndiceCaracterDatos);
        //        }
        //    }

        //    bool insertarCadenaDatos = false;

        //    if (Busqueda != null && textoArchivo.Text.Length - datosActualImagen.IndiceCaracterDatos <= cadenaFormatoNumero.Length)
        //    {
        //        insertarCadenaDatos = true;
        //    }
        //    else if (string.Compare(textoArchivo.Text.Substring(datosActualImagen.IndiceCaracterDatos, cadenaFormatoNumero.Length), cadenaFormatoNumero) != 0)
        //    {
        //        insertarCadenaDatos = true;
        //    }

        //    if (insertarCadenaDatos)
        //    {
        //        ModificandoTexto = true;
        //        textoArchivo.Text = textoArchivo.Text.Insert(datosActualImagen.IndiceCaracterDatos, cadenaFormatoNumero);
        //        ModificandoTexto = false;

        //        if (ConNumero && datosActualImagen.IndiceCaracterDatos < Busqueda.IndiceCaracterNumero)
        //        {
        //            Busqueda.IndiceCaracterNumero += cadenaFormatoNumero.Length;
        //        }

        //        if (datosActualImagen.IndiceCaracterDatos < IndiceCaracterNumeroAnterior)
        //        {
        //            IndiceCaracterNumeroAnterior += cadenaFormatoNumero.Length;
        //        }

        //        if(ConNumero)
        //            Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);
                
        //        datosActualImagen.Position = textoArchivo.GetRectFromCharacterIndex(datosActualImagen.IndiceCaracterDatos);

        //        foreach (var itemInfoArrastre in Busqueda.DatosFormatoBusquedaTexto.OrderBy((i) => i.IndiceCaracterDatos))
        //        {
                    
        //            if (datosActualImagen.IndiceCaracterDatos < itemInfoArrastre.IndiceCaracterDatos)
        //            {
        //                itemInfoArrastre.IndiceCaracterDatos += cadenaFormatoNumero.Length;
        //            }

        //            if (datosActualImagen.IndiceCaracterDatos < itemInfoArrastre.IndiceCaracterDatosAnterior)
        //            {
        //                itemInfoArrastre.IndiceCaracterDatosAnterior += cadenaFormatoNumero.Length;
        //            }

        //            itemInfoArrastre.Position = textoArchivo.GetRectFromCharacterIndex(itemInfoArrastre.IndiceCaracterDatos);
        //        }

        //    }

        //    EstablecerTextoGuardar();
        //}

        private void ReubicarNumeroDatos()
        {
            contenidoImagen.Children.Clear();

            ImageDrawing dibujador = new ImageDrawing();
            dibujador.ImageSource = datos.Source;
            dibujador.Rect = new Rect(datos.RenderSize);

            ImageDrawing dibujadorTextos = new ImageDrawing();
            dibujadorTextos.ImageSource = textos.Source;
            dibujadorTextos.Rect = new Rect(textos.RenderSize);

            DrawingImage imagenDibujada = new DrawingImage();
            imagenDibujada.Drawing = dibujador;

            DrawingImage imagenDibujadaTextos = new DrawingImage();
            imagenDibujadaTextos.Drawing = dibujadorTextos;

            foreach (var itemInfoArrastre in Busqueda.DatosFormatoBusquedaTexto)
            {
                if (itemInfoArrastre.IndiceCaracterDatos > -1)
                {
                    System.Windows.Controls.Image imagen = new System.Windows.Controls.Image();
                    imagen.Stretch = Stretch.Uniform;
                    imagen.Source = itemInfoArrastre.TextoInformacion ? imagenDibujadaTextos : imagenDibujada;
                    imagen.Tag = itemInfoArrastre.IdImagen;

                    if(itemInfoArrastre.TextoInformacion)
                        imagen.MouseMove += textos_Arrastre;
                    else
                        imagen.MouseMove += datos_Arrastre;

                    contenidoImagen.Children.Add(imagen);

                    Canvas.SetTop(imagen, itemInfoArrastre.Position.Y);
                    Canvas.SetLeft(imagen, itemInfoArrastre.Position.X);
                }
            }

            if (Busqueda.IndiceCaracterNumero > -1 && IsLoaded)
            {
                ImageDrawing dibujadorNumero = new ImageDrawing();
                dibujadorNumero.ImageSource = numero.Source;
                dibujadorNumero.Rect = new Rect(numero.RenderSize);

                DrawingImage imagenDibujadaNumero = new DrawingImage();
                imagenDibujadaNumero.Drawing = dibujadorNumero;

                System.Windows.Controls.Image imagenNumeroImg = new System.Windows.Controls.Image();
                imagenNumeroImg.Stretch = Stretch.Uniform;
                imagenNumeroImg.Source = imagenDibujadaNumero;

                imagenNumeroImg.MouseMove += numero_Arrastre;
                imagenNumero = imagenNumeroImg;
                contenidoImagen.Children.Add(imagenNumeroImg);

                Canvas.SetTop(imagenNumeroImg, Busqueda.Position.Y);
                Canvas.SetLeft(imagenNumeroImg, Busqueda.Position.X);

                ConNumero = true;
            }
        }

        //private void ProcesarTexto(TextChangedEventArgs e)
        //{
        //    //int posicionActual = textoArchivo.SelectionStart;
        //    int cantidadCaracteres = e.Changes.Last().AddedLength - e.Changes.Last().RemovedLength;

        //    if (Busqueda.IndiceCaracterNumero > -1 && Busqueda.IndiceCaracterNumero > posicionActualOriginal)
        //    {
        //        int cantidadCaracteresADesplazar = 0;

        //        //if (posicionActual <= Busqueda.IndiceCaracterNumero + 1)
        //        //{
        //            cantidadCaracteresADesplazar = cantidadCaracteres;
        //        //}
        //        //else
        //        //{
        //        //    cantidadCaracteresADesplazar = Busqueda.IndiceCaracterNumero + 1 - cantidadCaracteres;
        //        //}

        //        if (Busqueda != null)
        //        {
        //            Busqueda.IndiceCaracterNumero += cantidadCaracteresADesplazar;
        //        }

        //        if (IndiceCaracterNumeroAnterior > -1)
        //        {
        //            //if (posicionActual <= IndiceCaracterNumeroAnterior + 1)
        //            //{
        //                cantidadCaracteresADesplazar = cantidadCaracteres;
        //            //}
        //            //else
        //            //{
        //            //    cantidadCaracteresADesplazar = IndiceCaracterNumeroAnterior + 1 - cantidadCaracteres;
        //            //}

        //            IndiceCaracterNumeroAnterior += cantidadCaracteresADesplazar;
        //            //IndiceCaracterNumeroAnterior += cadenaFormatoNumero.Length;
        //        }
                
        //        if (Busqueda.IndiceCaracterNumero > -1)
        //            Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(Busqueda.IndiceCaracterNumero);
        //        else
        //            Busqueda.Position = textoArchivo.GetRectFromCharacterIndex(0);
        //    }

        //    foreach (var itemInfoArrastre in Busqueda.DatosFormatoBusquedaTexto.OrderBy((i) => i.IndiceCaracterDatos))
        //    {
        //        //int cantidadCaracteresADesplazar = 0;
        //        if (itemInfoArrastre.IndiceCaracterDatos > -1 && itemInfoArrastre.IndiceCaracterDatos > posicionActualOriginal)
        //        {
        //            //if (posicionActual <= itemInfoArrastre.IndiceCaracterDatos + 1)
        //            //{
        //            int cantidadCaracteresADesplazar = cantidadCaracteres;
        //            //}
        //            //else
        //            //{
        //            //    cantidadCaracteresADesplazar = itemInfoArrastre.IndiceCaracterDatos + 1 - cantidadCaracteres;
        //            //}

        //            itemInfoArrastre.IndiceCaracterDatos += cantidadCaracteresADesplazar;
        //            //itemInfoArrastre.IndiceCaracterDatos += cadenaFormatoNumero.Length;

        //            if (itemInfoArrastre.IndiceCaracterDatosAnterior > -1 && itemInfoArrastre.IndiceCaracterDatosAnterior > posicionActualOriginal)
        //            {
        //                //if (posicionActual <= itemInfoArrastre.IndiceCaracterDatosAnterior + 1)
        //                //{
        //                cantidadCaracteresADesplazar = cantidadCaracteres;
        //                //}
        //                //else
        //                //{
        //                //    cantidadCaracteresADesplazar = itemInfoArrastre.IndiceCaracterDatosAnterior + 1 - cantidadCaracteres;
        //                //}

        //                itemInfoArrastre.IndiceCaracterDatosAnterior += cantidadCaracteresADesplazar;
        //                //itemInfoArrastre.IndiceCaracterDatosAnterior += cadenaFormatoNumero.Length;
        //            }

        //            if (itemInfoArrastre.IndiceCaracterDatos > -1)
        //                itemInfoArrastre.Position = textoArchivo.GetRectFromCharacterIndex(itemInfoArrastre.IndiceCaracterDatos);
        //            else
        //                itemInfoArrastre.Position = textoArchivo.GetRectFromCharacterIndex(0);
        //        }
        //    }

        //    //textoArchivo.SelectionStart = posicionActual;
        //}

        public void EstablecerTextoGuardar()
        {
            textoArchivo.Text = ProcesarSaltosLinea(textoArchivo.Text);

            string texto = textoArchivo.Text;
            Busqueda.TextoBusquedaNumero = texto;
            
            if (Busqueda != null)
            {
                if (Busqueda.IndiceCaracterNumero > -1)
                {
                    int cantCaracteres = cadenaFormatoNumero.Length;

                    if (cadenaFormatoNumero.Length > Busqueda.TextoBusquedaNumero.Substring(Busqueda.IndiceCaracterNumero).Length)
                        cantCaracteres = Busqueda.TextoBusquedaNumero.Substring(Busqueda.IndiceCaracterNumero).Length;

                    Busqueda.TextoBusquedaNumero = Busqueda.TextoBusquedaNumero.Remove(Busqueda.IndiceCaracterNumero, cantCaracteres);
                    Busqueda.TextoBusquedaNumero = Busqueda.TextoBusquedaNumero.Insert(Busqueda.IndiceCaracterNumero, cadenaFormatoNumeroGuardar);
                }

                foreach (var item in Busqueda.DatosFormatoBusquedaTexto)
                {
                    if (item.IndiceCaracterDatos > -1)
                    {
                        int cantCaracteres = cadenaFormatoNumero.Length;

                        if (item.IndiceCaracterDatos < Busqueda.TextoBusquedaNumero.Length)
                        {
                            if (cadenaFormatoNumero.Length > Busqueda.TextoBusquedaNumero.Substring(item.IndiceCaracterDatos).Length)
                                cantCaracteres = Busqueda.TextoBusquedaNumero.Substring(item.IndiceCaracterDatos).Length;

                            Busqueda.TextoBusquedaNumero = Busqueda.TextoBusquedaNumero.Remove(item.IndiceCaracterDatos, cantCaracteres);
                            Busqueda.TextoBusquedaNumero = Busqueda.TextoBusquedaNumero.Insert(item.IndiceCaracterDatos,
                                item.TextoInformacion ? cadenaFormatoTextosGuardar : cadenaFormatoDatosGuardar);
                        }
                    }
                }

            }

            //if (Busqueda.DatosFormatoBusquedaTexto.Count > 0)
            //{
            //    Busqueda.TextoBusquedaNumero = Busqueda.TextoBusquedaNumero.Replace(cadenaFormatoNumero, cadenaFormatoDatosGuardar);
            //}
            MostrarMensaje_IconosJuntos();
        }

        private string ProcesarSaltosLinea(string cadena)
        {
            cadena = cadena.Replace("\r\n", "\r");
            cadena = cadena.Replace("\r", "\r\n");

            return cadena;
        }

        //public int QuitarLineasVacias()
        //{
        //    int indiceCaracter = 0;
        //    int cantidadCaracteresQuitados = 0;

        //    for (int indiceLinea = 0; indiceLinea <= textoArchivo.LineCount - 1; indiceLinea++)
        //    {
        //        //if (indiceLinea < textoArchivo.LineCount - 1)
        //        //{
        //            if (textoArchivo.GetLineText(indiceLinea).Equals("\n"))
        //            {
        //                textoArchivo.Text = textoArchivo.Text.Remove(indiceCaracter, 1);
        //                cantidadCaracteresQuitados += 1;
        //                indiceCaracter -= 1;
        //            }
        //            else if (textoArchivo.GetLineText(indiceLinea).Equals("\r\n"))
        //            {
        //                textoArchivo.Text = textoArchivo.Text.Remove(indiceCaracter, 2);
        //                cantidadCaracteresQuitados += 2;
        //                indiceCaracter -= 2;
        //            }
        //        //}

        //        indiceCaracter += textoArchivo.GetLineText(indiceLinea).Length;
        //    }

        //    return cantidadCaracteresQuitados;
        //}

        private void MostrarMensaje_IconosJuntos()
        {
            if (Busqueda != null)
            {
                if (Busqueda.TextoBusquedaNumero.Replace(" ", string.Empty).ToLower().Contains(cadenaFormatoDatosGuardar + cadenaFormatoDatosGuardar) |
                    Busqueda.TextoBusquedaNumero.Replace(" ", string.Empty).ToLower().Contains(cadenaFormatoDatosGuardar + cadenaFormatoNumeroGuardar) |
                    Busqueda.TextoBusquedaNumero.Replace(" ", string.Empty).ToLower().Contains(cadenaFormatoNumeroGuardar + cadenaFormatoDatosGuardar) |
                    Busqueda.TextoBusquedaNumero.Replace(" ", string.Empty).ToLower().Contains(cadenaFormatoTextosGuardar + cadenaFormatoTextosGuardar) |
                    Busqueda.TextoBusquedaNumero.Replace(" ", string.Empty).ToLower().Contains(cadenaFormatoTextosGuardar + cadenaFormatoNumeroGuardar) |
                    Busqueda.TextoBusquedaNumero.Replace(" ", string.Empty).ToLower().Contains(cadenaFormatoNumeroGuardar + cadenaFormatoTextosGuardar) |
                    Busqueda.TextoBusquedaNumero.Replace(" ", string.Empty).ToLower().Contains(cadenaFormatoTextosGuardar + cadenaFormatoDatosGuardar) |
                    Busqueda.TextoBusquedaNumero.Replace(" ", string.Empty).ToLower().Contains(cadenaFormatoDatosGuardar + cadenaFormatoTextosGuardar))
                {
                    errorIconosJuntos.Visibility = Visibility.Visible;
                }
                else
                {
                    errorIconosJuntos.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void MostrarOcultarOpciones_TextosInformacion(bool mostrar)
        {
            if (mostrar)
            {
                opcionesTextosInformacion.Visibility = Visibility.Visible;
            }
            else
            {
                opcionesTextosInformacion.Visibility = Visibility.Collapsed;
                opcionNumeroActual.IsChecked = true;
                Busqueda.OpcionTextosInformacion = OpcionTextosInformacionBusqueda.NumeroActual;
            }
        }
    }
}