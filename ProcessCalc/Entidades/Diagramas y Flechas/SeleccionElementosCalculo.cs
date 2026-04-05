using Petzold.Media2D;
using ProcessCalc.Controles;
using ProcessCalc.Controles.Notas;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas.Definiciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProcessCalc.Entidades
{
    
    public class SeleccionElementosCalculo
    {
        public EntradaDiseñoOperaciones EntradaSeleccionada;
        public EntradaDiseñoOperaciones EntradaSeleccionadaArrastre;
        public OperacionEspecifica OperacionSeleccionada;
        public OperacionEspecifica OperacionSeleccionadaArrastre;
        public NotaDiagrama NotaSeleccionada;
        public TipoElementoOperacion TipoElementoOperacionSeleccionado;
        public DiseñoOperacion ElementoSeleccionado;
        public bool MostrandoOrdenOperandos;
        public ArrowLine lineaSeleccionada;
        public MouseButtonEventArgs e_SeleccionarElemento;
        public int CantidadZoom = 100;
        public bool MostrandoInformacionElemento;
        public List<UIElement> ElementosEncontrados = new List<UIElement>();
        public int indiceElementosEncontrados = -1;
        public double escalaZoom = 1;
        public string TextoBusquedaDiagrama = string.Empty;
        public List<DiseñoOperacion> ElementosSeleccionados = new List<DiseñoOperacion>();
        public UIElement ElementoDiagramaSeleccionado;
        public List<UIElement> ElementosDiagramaSeleccionados = new List<UIElement>();
        public UIElement ElementoDiagramaSeleccionadoMover;
        public bool MostrandoConfiguracionSeparadores;
    }

    public class SeleccionElementosTextosInformacion
    {
        public EntradaDiseñoOperaciones EntradaSeleccionada;
        public List<UIElement> ElementosDiagramaSeleccionados = new List<UIElement>();
        public MouseButtonEventArgs e_SeleccionarElemento;
        public Definicion_TextosInformacion DefinicionSeleccionada;
        public DefinicionListaCadenasTexto DefinicionListaCadenasTextoSeleccionada;
        public TipoElementoOperacion TipoElementoOperacionSeleccionado;
        public UIElement ElementoDiagramaSeleccionadoMover;
        public DiseñoTextosInformacion ElementoSeleccionado;
        public DiseñoListaCadenasTexto ElementoListaSeleccionado;
        public ArrowLine lineaSeleccionada;
        public List<DiseñoTextosInformacion> ElementosSeleccionados = new List<DiseñoTextosInformacion>();
        public UIElement ElementoDiagramaSeleccionado;
    }
}
