using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class DiseñoTextosInformacion
    {
        public TipoElementoOperacion Tipo { get; set; }
        public double PosicionX { get; set; }
        public double PosicionY { get; set; }
        public double Altura { get; set; }
        public double Anchura { get; set; }
        public List<DiseñoTextosInformacion> ElementosAnteriores { get; set; }
        public List<DiseñoTextosInformacion> ElementosPosteriores { get; set; }
        public string Nombre { get; set; }
        public List<AsignacionImplicacion_TextosInformacion> Relaciones_TextosInformacion { get; set; }
        public DiseñoOperacion OperacionRelacionada { get; set; }
        public DiseñoCalculo CalculoRelacionado { get; set; }
        public DiseñoElementoOperacion ElementoRelacionado { get; set; }
        public List<DiseñoTextosInformacion> Definiciones_TextosInformacion { get; set; }
        public Entrada EntradaRelacionada { get; set; }
        public bool OpcionTextoInformacion_Condicion_AsignacionImplicacion { get; set; }
        public bool OpcionTextoInformacion_Asignacion_AsignacionImplicacion { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacion_CumplenCondiciones_Anteriores { get; set; }
        public DiseñoTextosInformacion()
        {
            ElementosAnteriores = new List<DiseñoTextosInformacion>();
            ElementosPosteriores = new List<DiseñoTextosInformacion>();
            Tipo = TipoElementoOperacion.Ninguna;
            PosicionX = App.PosicionXPredeterminada;
            PosicionY = App.PosicionYPredeterminada;
            Relaciones_TextosInformacion = new List<AsignacionImplicacion_TextosInformacion>();
            Definiciones_TextosInformacion = new List<DiseñoTextosInformacion>();
            OpcionTextoInformacion_Asignacion_AsignacionImplicacion = true;
            TextosInformacion_CumplenCondiciones_Anteriores = new List<string>();
        }

        public List<DiseñoTextosInformacion> ObtenerEntradas()
        {
            List<DiseñoTextosInformacion> entradas = new List<DiseñoTextosInformacion>();

            foreach (var itemAnterior in ElementosAnteriores)
            {
                if (itemAnterior.EntradaRelacionada != null)
                    entradas.Add(itemAnterior);
            }

            return entradas;
        }

        public List<DiseñoTextosInformacion> ObtenerDefiniciones()
        {
            List<DiseñoTextosInformacion> definiciones = new List<DiseñoTextosInformacion>();

            foreach (var itemAnterior in ElementosAnteriores)
            {
                if (itemAnterior.EntradaRelacionada != null)
                    definiciones.Add(itemAnterior);
            }

            return definiciones;
        }

        public List<DiseñoListaCadenasTexto> ObtenerDefinicionesListas()
        {
            List<DiseñoListaCadenasTexto> definiciones = new List<DiseñoListaCadenasTexto>();

            foreach (DiseñoListaCadenasTexto itemAnterior in ElementosAnteriores.Where(i => i.GetType() == typeof(DiseñoListaCadenasTexto)))
            {
                definiciones.Add(itemAnterior);
            }

            return definiciones;
        }

        public bool VerificarEnCondiciones_DefinicionesTextosInformacion(DiseñoElementoOperacion elemento)
        {
            foreach (var itemDiseño in Definiciones_TextosInformacion)
            {
                if (itemDiseño.ElementoRelacionado == elemento)
                    return true;
            }

            return false;
        }

        public List<string> ObtenerTextos_CondicionDefinicion()
        {
            var item = new List<string>();

            foreach (var asignacion in Relaciones_TextosInformacion)
            {
                item.AddRange(asignacion.TextosInformacionAsignados_Ejecucion);
            }

            return item;
        }

        public void InicializarPosicionesElementosEntradas()
        {
            foreach(var itemDefinicion in Definiciones_TextosInformacion)
            {
                itemDefinicion.InicializarPosicionesElementosEntradas();
            }

            if(EntradaRelacionada != null)
            {
                if (EntradaRelacionada.EntradaProcesada != null)
                    EntradaRelacionada.EntradaProcesada.PosicionActualNumero_CondicionesOperador_Implicacion = 0;
            }
        }
    }
}
