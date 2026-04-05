using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class OpcionesNombreCantidad_TextosInformacion
    {
        public TipoOpcionesNombreCantidad_TextosInformacion TipoOpcion { get; set; }
        public CondicionFlujo Condiciones { get; set; }
        public int NPrimerosTextosInformacion { get; set; }
        public int NUltimosTextosInformacion { get; set; }
        public bool IncluirTextosImplica { get; set; }
        public DiseñoOperacion Operando { get; set; }
        public DiseñoElementoOperacion OperandoSubElemento { get; set; }
        public string PosicionesTextosInformacion { get; set; }
        public string TextoInformacionFijo { get; set; }
        [IgnoreDataMember]
        public EntidadNumero SubNumero { get; set; }
        public string PosicionesTextosInformacionNumero_Elemento { get; set; }
        public TipoOpcionesFiltroNumeros_NombreCantidad TipoOpcionFiltroNumeros { get; set; }
        public CondicionFlujo CondicionesFiltroNumeros { get; set; }
        public int NPrimerosTextosInformacionNumerosFiltrados { get; set; }
        public int NUltimosTextosInformacionNumerosFiltrados { get; set; }
        public string PosicionesTextosInformacionNumerosFiltrados { get; set; }
        public int NPrimerosTextosInformacionNumero { get; set; }
        public int NUltimosTextosInformacionNumero { get; set; }
        public CondicionFlujo CondicionesTexto { get; set; }
        public CondicionFlujo CondicionesTextoNumero { get; set; }
        public string PosicionesTextosInformacionNumero { get; set; }
        public bool IncluirComillas { get; set; }
        public OpcionesNombreCantidad_TextosInformacion()
        {
            TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacion;
            NPrimerosTextosInformacion = 2;
            NUltimosTextosInformacion = 2;
            TextoInformacionFijo = string.Empty;
            PosicionesTextosInformacion = string.Empty;
            Condiciones = new CondicionFlujo();
            PosicionesTextosInformacionNumero_Elemento = string.Empty;
            TipoOpcionFiltroNumeros = TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros;
            CondicionesFiltroNumeros = new CondicionFlujo();
            NPrimerosTextosInformacionNumerosFiltrados = 2;
            NUltimosTextosInformacionNumerosFiltrados = 2;
            PosicionesTextosInformacionNumerosFiltrados = string.Empty;
            NPrimerosTextosInformacionNumero = 2;
            NUltimosTextosInformacionNumero = 2;
            CondicionesTexto = new CondicionFlujo();
            CondicionesTextoNumero = new CondicionFlujo();
            PosicionesTextosInformacionNumero = string.Empty;
        }

        public void CopiarObjeto(OpcionesNombreCantidad_TextosInformacion opcionACopiar)
        {
            this.IncluirTextosImplica = opcionACopiar.IncluirTextosImplica;
            this.IncluirComillas = opcionACopiar.IncluirComillas;
            this.Condiciones = opcionACopiar.Condiciones.ReplicarObjeto();
            this.CondicionesFiltroNumeros = opcionACopiar.CondicionesFiltroNumeros.ReplicarObjeto();
            this.CondicionesTexto = opcionACopiar.CondicionesTexto.ReplicarObjeto();
            this.CondicionesTextoNumero = opcionACopiar.CondicionesTextoNumero.ReplicarObjeto();
            this.NPrimerosTextosInformacion = opcionACopiar.NPrimerosTextosInformacion;
            this.NPrimerosTextosInformacionNumero = opcionACopiar.NPrimerosTextosInformacionNumero;
            this.NPrimerosTextosInformacionNumerosFiltrados = opcionACopiar.NPrimerosTextosInformacionNumerosFiltrados;
            this.NUltimosTextosInformacion = opcionACopiar.NUltimosTextosInformacion;
            this.NUltimosTextosInformacionNumero = opcionACopiar.NUltimosTextosInformacionNumero;
            this.NUltimosTextosInformacionNumerosFiltrados = opcionACopiar.NUltimosTextosInformacionNumerosFiltrados;
            this.Operando = opcionACopiar.Operando;
            this.OperandoSubElemento = opcionACopiar.OperandoSubElemento;
            this.PosicionesTextosInformacion = opcionACopiar.PosicionesTextosInformacion;
            this.PosicionesTextosInformacionNumero = opcionACopiar.PosicionesTextosInformacionNumero;
            this.PosicionesTextosInformacionNumerosFiltrados = opcionACopiar.PosicionesTextosInformacionNumerosFiltrados;
            this.PosicionesTextosInformacionNumero_Elemento = opcionACopiar.PosicionesTextosInformacionNumero_Elemento;
            this.TextoInformacionFijo = opcionACopiar.TextoInformacionFijo;
            this.TipoOpcion = opcionACopiar.TipoOpcion;
            this.TipoOpcionFiltroNumeros = opcionACopiar.TipoOpcionFiltroNumeros;
        }
    }
}
