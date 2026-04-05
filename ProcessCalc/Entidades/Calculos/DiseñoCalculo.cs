using ProcessCalc.Controles;
using ProcessCalc.Controles.Calculos;
using ProcessCalc.Entidades.Ejecuciones.ToolTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class DiseñoCalculo
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Descricion { get; set; }
        public List<Entrada> ListaEntradas { get; set; }
        public List<DiseñoOperacion> ElementosOperaciones { get; set; }
        [AtributoNoComparar]
        public double Ancho { get; set; }
        [AtributoNoComparar]
        public double Alto { get; set; }
        public List<DiseñoCalculo> ElementosAnteriores { get; set; }
        public List<DiseñoCalculo> ElementosPosteriores { get; set; }
        public List<DiseñoCalculo> ElementosContenedoresOperacion { get; set; }
        [AtributoNoComparar]
        public double PosicionX { get; set; }
        [AtributoNoComparar]
        public double PosicionY { get; set; }
        [AtributoNoComparar]
        public double AlturaElemento { get; set; }
        [AtributoNoComparar]
        public double AnchuraElemento { get; set; }
        [IgnoreDataMember]
        public SeleccionElementosCalculo Seleccion;
        [AtributoNoComparar]
        public string Info { get; set; }
        [AtributoNoComparar]
        public bool EsEntradasArchivo { get; set; }
        [AtributoNoComparar]
        public List<DiseñoOperacion> Agrupadores { get; set; }
        [IgnoreDataMember]
        public SeleccionElementosTextosInformacion SeleccionTextos;
        public DiseñoCalculo()
        {
            ListaEntradas = new List<Entrada>();
            ElementosOperaciones = new List<DiseñoOperacion>();
            Ancho = double.NaN;
            Alto = double.NaN;
            ElementosAnteriores = new List<DiseñoCalculo>();
            ElementosPosteriores = new List<DiseñoCalculo>();
            ElementosContenedoresOperacion = new List<DiseñoCalculo>();
            PosicionX = App.PosicionXPredeterminada;
            PosicionY = App.PosicionYPredeterminada;

            EsEntradasArchivo = false;
            //ListaEntradas.Add(new Entrada("Entrada 1"));

            //if (EsEntradasArchivo)
            //{
            //    AgregarEntrada_CalculoEntradas(ListaEntradas.Last());
            //}

            AnchuraElemento = double.NaN;
            AlturaElemento = double.NaN;
            Seleccion = new SeleccionElementosCalculo();
            Agrupadores = new List<DiseñoOperacion>();
            SeleccionTextos = new SeleccionElementosTextosInformacion();
        }
        public DiseñoCalculo(string nombre, string descripcion, bool? esEntradas = null)
        {
            ID = App.GenerarID_Elemento();
            Nombre = nombre;
            Descricion = descripcion;
            ListaEntradas = new List<Entrada>();
            ElementosOperaciones = new List<DiseñoOperacion>();
            Ancho = double.NaN;
            Alto = double.NaN;
            ElementosAnteriores = new List<DiseñoCalculo>();
            ElementosPosteriores = new List<DiseñoCalculo>();
            ElementosContenedoresOperacion = new List<DiseñoCalculo>();
            PosicionX = App.PosicionXPredeterminada;
            PosicionY = App.PosicionYPredeterminada;

            EsEntradasArchivo = ((esEntradas == true) ? true : false);

            //if (EsEntradasArchivo)
            //{
            //    ListaEntradas.Add(new Entrada("Entrada 1"));
            //    AgregarEntrada_CalculoEntradas(ListaEntradas.Last());
            //}

            AnchuraElemento = double.NaN;
            AlturaElemento = double.NaN;
            Seleccion = new SeleccionElementosCalculo();
            Agrupadores = new List<DiseñoOperacion>();
        }

        public DiseñoCalculo(double posicionX, double posicionY)
        {
            PosicionX = posicionX;
            PosicionY = posicionY;
        }

        public void AgregarEntrada_CalculoEntradas(Entrada entrada)
        {
            DiseñoOperacion nuevoElementoOperacion = new DiseñoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.EntradaRelacionada = entrada;
            nuevoElementoOperacion.Tipo = TipoElementoOperacion.Entrada;
            nuevoElementoOperacion.PosicionX = 0;
            nuevoElementoOperacion.PosicionY = 0;

            ElementosOperaciones.Add(nuevoElementoOperacion);
            nuevoElementoOperacion.ContieneSalida = true;
        }

        public void QuitarEntrada_CalculoEntradas(Entrada entrada)
        {
            var DiseñoOperacion = (from D in ElementosOperaciones where D.EntradaRelacionada == entrada select D).FirstOrDefault();
            ElementosOperaciones.Remove(DiseñoOperacion);
        }

        public void QuitarElemento_CalculoEntradas(DiseñoOperacion elemento)
        {
            ElementosOperaciones.Remove(elemento);
        }

        public bool VerificarElementoSiEsAgrupado(DiseñoOperacion elemento)
        {
            List<DiseñoOperacion> listaElementosAgrupados = new List<DiseñoOperacion>();

            foreach (var itemAgrupador in Agrupadores)
                listaElementosAgrupados.AddRange(itemAgrupador.ElementosAgrupados);

            return listaElementosAgrupados.Contains(elemento);
        }

        public void ReordenarElementos_EntradasGenerales()
        {
            foreach(var itemEntrada in ListaEntradas)
            {
                QuitarEntrada_CalculoEntradas(itemEntrada);
            }

            foreach (var itemEntrada in ListaEntradas)
            {
                AgregarEntrada_CalculoEntradas(itemEntrada);
            }
        }
    }
}
