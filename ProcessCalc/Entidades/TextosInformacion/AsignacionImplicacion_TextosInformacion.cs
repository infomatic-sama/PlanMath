using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class AsignacionImplicacion_TextosInformacion
    {
        public CondicionImplicacionTextosInformacion Condiciones_TextoCondicion { get; set; }
        [IgnoreDataMember]
        public List<TextosCondicion_Entrada> TextosCondicion { get; set; }
        public DefinicionTextoNombresCantidades DefinicionOpcionesNombresCantidades { get; set; }        
        public List<InstanciaAsignacionImplicacion_TextosInformacion> InstanciasAsignacion { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacionAsignados_Ejecucion;
        [IgnoreDataMember]
        public List<string> TextosInformacionAgregadosInstancias_Resultado;
        [IgnoreDataMember]
        public List<string> TextosInformacionImplicacion_CumplenCondicion;
        [IgnoreDataMember]
        public List<AsociacionTextosInformacionImplicacion_OperandoCondicion> TextosInformacionAgregadosInstancias_Resultado_Operandos;
        [IgnoreDataMember]
        public DiseñoCalculoTextosInformacion DiseñoTextosInformacion_Calculo { get; set; }
        [IgnoreDataMember]
        public DiseñoTextosInformacion DiseñoTextosInformacion_Relacionado { get; set; }
        [IgnoreDataMember]
        public List<int> PosicionesTextos_CumplenCondicion { get; set; }
        [IgnoreDataMember]
        public List<EntidadNumero> Cantidades_Implicacion_CumplenCondicion;
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> OperandosCantidades_Implicacion_CumplenCondicion;
        [IgnoreDataMember]
        public bool AsignarACantidades {  get; set; }
        public AsignacionImplicacion_TextosInformacion()
        {            
            TextosCondicion = new List<TextosCondicion_Entrada>();            
            DefinicionOpcionesNombresCantidades = new DefinicionTextoNombresCantidades();            
            TextosInformacionAsignados_Ejecucion = new List<string>();
            InstanciasAsignacion = new List<InstanciaAsignacionImplicacion_TextosInformacion>();            
            TextosInformacionAgregadosInstancias_Resultado = new List<string>();
            TextosInformacionAgregadosInstancias_Resultado_Operandos = new List<AsociacionTextosInformacionImplicacion_OperandoCondicion>();
            PosicionesTextos_CumplenCondicion = new List<int>();
            TextosInformacionImplicacion_CumplenCondicion = new List<string>();
            Cantidades_Implicacion_CumplenCondicion = new List<EntidadNumero>();
            OperandosCantidades_Implicacion_CumplenCondicion = new List<ElementoEjecucionCalculo>();
        }

        public AsignacionImplicacion_TextosInformacion ReplicarObjeto()
        {
            AsignacionImplicacion_TextosInformacion asignacion = new AsignacionImplicacion_TextosInformacion();
            asignacion.Condiciones_TextoCondicion = Condiciones_TextoCondicion?.ReplicarObjeto();
            asignacion.DefinicionOpcionesNombresCantidades = DefinicionOpcionesNombresCantidades.ReplicarObjeto();
            
            asignacion.InstanciasAsignacion = new List<InstanciaAsignacionImplicacion_TextosInformacion>();
            foreach (var itemInstancia in InstanciasAsignacion)
                asignacion.InstanciasAsignacion.Add(itemInstancia.ReplicarObjeto());

            asignacion.TextosCondicion = new List<TextosCondicion_Entrada>();

            foreach (var itemTextoCondicion in TextosCondicion)
                asignacion.TextosCondicion.Add(itemTextoCondicion.ReplicarObjeto());

            return asignacion;
        }

        public bool VerificarEntradaEn_Condiciones(Entrada entrada)
        {
            if (Condiciones_TextoCondicion != null)
                return Condiciones_TextoCondicion.VerificaEntrada(entrada);
            else
                return false;
        }

        //public List<ListaTextosCondicion_Entrada> ObtenerListaTextos_Condiciones()
        //{
        //    List<ListaTextosCondicion_Entrada> lista = new List<ListaTextosCondicion_Entrada>();
        //    if (Condiciones_TextoCondicion != null)
        //        Condiciones_TextoCondicion.ObtenerListaTextos_Condiciones(ref lista);
        //    return lista;
        //}

        public void QuitarEntradaCondiciones(Entrada entrada)
        {
            if (Condiciones_TextoCondicion != null)
            {
                if (Condiciones_TextoCondicion.EntradaCondicion == entrada)
                    Condiciones_TextoCondicion.EntradaCondicion = null;

                if (Condiciones_TextoCondicion.ElementoEntrada_Valores == entrada)
                    Condiciones_TextoCondicion.ElementoEntrada_Valores = null;
                else
                    Condiciones_TextoCondicion.QuitarCondicionesEntrada(entrada);
            }
        }

        public List<List<string>> ObtenerTextos_CondicionEntrada(Entrada entrada)
        {
            var item = (from T in TextosCondicion where T.EntradaRelacionada == entrada select T.TextosCondicion).ToList();

            if (item == null)
                item = new List<List<string>>();

            return item;
        }

        public List<string> ObtenerTextos_CondicionEntrada(Entrada entrada, int Posicion)
        {
            var item = (from T in TextosCondicion where T.EntradaRelacionada == entrada select T.TextosCondicion).ToList();

            if(item != null && item.Any() && (Posicion >= 0 &
            Posicion <= item.Count - 1))
            {
                return item[Posicion];
            }
            else
                return new List<string>();
        }

        public List<CondicionImplicacionTextosInformacion> ObtenerListaCondiciones_ElementoCondicion_Condiciones(DiseñoOperacion elemento)
        {
            List<CondicionImplicacionTextosInformacion> lista = new List<CondicionImplicacionTextosInformacion>();
            if (Condiciones_TextoCondicion != null)
                Condiciones_TextoCondicion.ObtenerCondicionElementoCondicion_Condiciones(ref lista, elemento);
            return lista;
        }

        public List<CondicionImplicacionTextosInformacion> ObtenerListaCondiciones_ElementoDiseñoCondicion_Condiciones(DiseñoElementoOperacion elemento)
        {
            List<CondicionImplicacionTextosInformacion> lista = new List<CondicionImplicacionTextosInformacion>();
            if (Condiciones_TextoCondicion != null)
                Condiciones_TextoCondicion.ObtenerCondicionElementoDiseñoCondicion_Condiciones(ref lista, elemento);
            return lista;
        }

        public List<CondicionImplicacionTextosInformacion> ObtenerListaCondiciones_OperandoCondicion_Condiciones(DiseñoOperacion elemento)
        {
            List<CondicionImplicacionTextosInformacion> lista = new List<CondicionImplicacionTextosInformacion>();
            if (Condiciones_TextoCondicion != null)
                Condiciones_TextoCondicion.ObtenerCondicionOperandoCondicion_Condiciones(ref lista, elemento);
            return lista;
        }

        public List<CondicionImplicacionTextosInformacion> ObtenerListaCondiciones_Entrada_Condiciones(Entrada entrada)
        {
            List<CondicionImplicacionTextosInformacion> lista = new List<CondicionImplicacionTextosInformacion>();
            if(Condiciones_TextoCondicion != null)
                Condiciones_TextoCondicion.ObtenerCondicionEntrada_Condiciones(ref lista, entrada);
            return lista;
        }

        public List<string> ObtenerConservarTextosInformacion_Operandos(
            ElementoEjecucionCalculo operando,
            EntidadNumero NumeroSubOperando)
        {
            return (from A in TextosInformacionAgregadosInstancias_Resultado_Operandos
                    where
                    A.Operando == operando
                    & (A.NumeroSubOperando == NumeroSubOperando || NumeroSubOperando == null)
                    select A.TextosInformacionAgregadosInstancias_Resultado).FirstOrDefault();
        }

        public void AgregarConservarTextosInformacion_Operandos(List<string> TextosInformacionAgregadosInstancias_Resultado,
            ElementoEjecucionCalculo operando,
            EntidadNumero NumeroSubOperando)
        {
            TextosInformacionAgregadosInstancias_Resultado_Operandos.Add(new AsociacionTextosInformacionImplicacion_OperandoCondicion()
            {
                TextosInformacionAgregadosInstancias_Resultado = TextosInformacionAgregadosInstancias_Resultado,
                Operando = operando,
                NumeroSubOperando = NumeroSubOperando
            });
        }
    }

    public class TextosCondicion_Entrada
    {
        public Entrada EntradaRelacionada { get; set; }
        public List<string> TextosCondicion { get; set; }

        public TextosCondicion_Entrada ReplicarObjeto()
        {
            TextosCondicion_Entrada textos = new TextosCondicion_Entrada();
            textos.EntradaRelacionada = EntradaRelacionada;
            textos.TextosCondicion = TextosCondicion.ToList();

            return textos;
        }
    }

    public class InstanciaAsignacionImplicacion_TextosInformacion
    {
        public string TextoImplicaAsignacion { get; set; }
        public List<AsignacionTextosOperando_Implicacion> Operandos_AsignarTextosInformacionA { get; set; }
        public List<AsignacionTextosOperando_Implicacion> SubOperandos_AsignarTextosInformacionA { get; set; }
        public List<DiseñoOperacion> Operandos_AsignarTextosInformacionCuando { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_AsignarTextosInformacionCuando { get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion { get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion_TodosSusTextos { get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros { get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual { get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones { get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones { get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones { get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos {  get; set; }
        public List<DiseñoOperacion> Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos { get; set; }
        public List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones { get; set; }
        public List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones { get; set; }
        public List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones { get; set; }
        public List<DiseñoTextosInformacion> Entradas_DesdeAsignarTextosInformacion { get; set; }
        public List<DiseñoTextosInformacion> Entradas_DesdeAsignarTextosInformacion_TodosSusTextos { get; set; }
        public List<DiseñoTextosInformacion> Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros { get; set; }
        public List<DiseñoTextosInformacion> Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual { get; set; }
        public List<DiseñoTextosInformacion> Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones { get; set; }
        public List<DiseñoTextosInformacion> Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones { get; set; }
        public List<DiseñoTextosInformacion> Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos { get; set; }
        public List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones { get; set; }
        public List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones { get; set; }
        public List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones { get; set; }
        public List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones { get; set; }
        public List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones { get; set; }
        public List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones { get; set; }
        public List<string> TextosAsignacion { get; set; }
        public bool ReemplazarTextosInformacion { get; set; }
        public bool ConservarTextosInformacion { get; set; }
        public bool AsignarTextosInformacion_Operacion { get; set; }
        public bool QuitarTextosInformacion_RepetidosVariableVector_Actual {  get; set; }
        public bool AsignarTextosInformacion_Condicion { get; set; }
        public bool QuitarTextosInformacion_RepetidosCondicion_Actual { get; set; }
        public bool AsignarTextosInformacion_CondicionOperandos { get; set; }
        public bool QuitarTextosInformacion_RepetidosVariableVector { get; set; }
        public bool AsignarTextosInformacion_CantidadActual { get; set; }
        public bool QuitarTextosInformacion_RepetidosIteracion_Actual { get; set; }
        public bool QuitarTextosInformacion_RepetidosCantidad { get; set; }
        public bool ConsiderarSoloOperandos_CumplanCondiciones { get; set; }
        public bool DigitarTextosInformacion_EnEjecucion { get; set; }
        public bool AsignarCadenasTexto_Clasificadores { get; set; }
        public List<DiseñoOperacion> AsignarTextosInformacion_TextosOperandos { get; set; }
        public List<DiseñoOperacion> QuitarTextosInformacionRepetidos_TextosOperandos { get; set; }
        public List<DiseñoElementoOperacion> AsignarTextosInformacion_TextosSubOperandos { get; set; }
        public List<DiseñoElementoOperacion> QuitarTextosInformacionRepetidos_TextosSubOperandos { get; set; }
        public InstanciaAsignacionImplicacion_TextosInformacion()
        {
            Operandos_DesdeAsignarTextosInformacion = new List<DiseñoOperacion>();
            Entradas_DesdeAsignarTextosInformacion = new List<DiseñoTextosInformacion>();
            Operandos_AsignarTextosInformacionA = new List<AsignacionTextosOperando_Implicacion>();
            Operandos_AsignarTextosInformacionCuando = new List<DiseñoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion = new List<DiseñoElementoOperacion>();
            SubOperandos_AsignarTextosInformacionA = new List<AsignacionTextosOperando_Implicacion>();
            SubOperandos_AsignarTextosInformacionCuando = new List<DiseñoElementoOperacion>();
            TextoImplicaAsignacion = string.Empty;
            TextosAsignacion = new List<string>();
            Entradas_DesdeAsignarTextosInformacion_TodosSusTextos = new List<DiseñoTextosInformacion>();
            Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros = new List<DiseñoTextosInformacion>();
            Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual = new List<DiseñoTextosInformacion>();
            Operandos_DesdeAsignarTextosInformacion_TodosSusTextos = new List<DiseñoOperacion>();
            Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros = new List<DiseñoOperacion>();
            Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual = new List<DiseñoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos = new List<DiseñoElementoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros = new List<DiseñoElementoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual = new List<DiseñoElementoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones = new List<DiseñoElementoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones = new List<DiseñoElementoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos = new List<DiseñoElementoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos = new List<DiseñoElementoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones = new List<DiseñoElementoOperacion>();
            SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();
            Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones = new List<DiseñoTextosInformacion>();
            Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones = new List<DiseñoTextosInformacion>();
            Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos = new List<DiseñoTextosInformacion>();
            Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();
            Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones = new List<DiseñoOperacion>();
            Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones = new List<DiseñoOperacion>();
            Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos = new List<DiseñoOperacion>();
            Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos = new List<DiseñoOperacion>();
            Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones = new List<DiseñoOperacion>();
            Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();
            AsignarTextosInformacion_TextosOperandos = new List<DiseñoOperacion>();
            AsignarTextosInformacion_TextosSubOperandos = new List<DiseñoElementoOperacion>();
            QuitarTextosInformacionRepetidos_TextosOperandos = new List<DiseñoOperacion>();
            QuitarTextosInformacionRepetidos_TextosSubOperandos = new List<DiseñoElementoOperacion>();
        }
        
        public InstanciaAsignacionImplicacion_TextosInformacion ReplicarObjeto()
        {
            InstanciaAsignacionImplicacion_TextosInformacion instancia = new InstanciaAsignacionImplicacion_TextosInformacion();
            instancia.TextoImplicaAsignacion = TextoImplicaAsignacion;

            instancia.Operandos_AsignarTextosInformacionA = new List<AsignacionTextosOperando_Implicacion>();
            foreach(var item in Operandos_AsignarTextosInformacionA)
                instancia.Operandos_AsignarTextosInformacionA.Add(item.ReplicarObjeto());

            instancia.SubOperandos_AsignarTextosInformacionA = new List<AsignacionTextosOperando_Implicacion>();
            foreach (var item in SubOperandos_AsignarTextosInformacionA)
                instancia.SubOperandos_AsignarTextosInformacionA.Add(item.ReplicarObjeto());

            instancia.Operandos_AsignarTextosInformacionCuando = Operandos_AsignarTextosInformacionCuando;
            instancia.SubOperandos_AsignarTextosInformacionCuando = SubOperandos_AsignarTextosInformacionCuando;
            instancia.Operandos_DesdeAsignarTextosInformacion = Operandos_DesdeAsignarTextosInformacion;
            instancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos = Operandos_DesdeAsignarTextosInformacion_TodosSusTextos;
            instancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros = Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros;
            instancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual = Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual;
            instancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones = Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones;
            instancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones = Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones;
            instancia.Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos = Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos;
            instancia.Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos = Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos;
            instancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones = Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones;

            instancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            foreach (var item in Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones)
                instancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Add(item.ReplicarObjeto());

            instancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            foreach (var item in Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones)
                instancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Add(item.ReplicarObjeto());

            instancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();
            foreach (var item in Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones)
                instancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Add(item.ReplicarObjeto());

            instancia.Entradas_DesdeAsignarTextosInformacion = Entradas_DesdeAsignarTextosInformacion;
            instancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos = Entradas_DesdeAsignarTextosInformacion_TodosSusTextos;
            instancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros = Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros;
            instancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual = Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual;
            instancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones = Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones;
            instancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones = Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones;
            instancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos = Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos;

            instancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            foreach (var item in Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones)
                instancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Add(item.ReplicarObjeto());

            instancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            foreach (var item in Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones)
                instancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Add(item.ReplicarObjeto());

            instancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();
            foreach (var item in Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones)
                instancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones.Add(item.ReplicarObjeto());

            instancia.SubOperandos_DesdeAsignarTextosInformacion = SubOperandos_DesdeAsignarTextosInformacion;
            instancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos = SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos;
            instancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros = SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros;
            instancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual = SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual;
            instancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones = SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones;
            instancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones = SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones;
            instancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos = SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos;
            instancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos = SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos;
            instancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones = SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones;

            instancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            foreach (var item in SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones)
                instancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Add(item.ReplicarObjeto());

            instancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();
            foreach (var item in SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones)
                instancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Add(item.ReplicarObjeto());

            instancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();
            foreach (var item in SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones)
                instancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Add(item.ReplicarObjeto());

            instancia.TextosAsignacion = TextosAsignacion.ToList();
            instancia.ReemplazarTextosInformacion = ReemplazarTextosInformacion;
            instancia.ConservarTextosInformacion = ConservarTextosInformacion;
            instancia.AsignarTextosInformacion_Operacion = AsignarTextosInformacion_Operacion;
            instancia.QuitarTextosInformacion_RepetidosCantidad = QuitarTextosInformacion_RepetidosCantidad;
            instancia.QuitarTextosInformacion_RepetidosVariableVector = QuitarTextosInformacion_RepetidosVariableVector;
            instancia.AsignarTextosInformacion_Condicion = AsignarTextosInformacion_Condicion;
            instancia.QuitarTextosInformacion_RepetidosCondicion_Actual = QuitarTextosInformacion_RepetidosCondicion_Actual;
            instancia.AsignarTextosInformacion_CondicionOperandos = AsignarTextosInformacion_CondicionOperandos;
            instancia.QuitarTextosInformacion_RepetidosVariableVector_Actual = QuitarTextosInformacion_RepetidosVariableVector_Actual;
            instancia.AsignarTextosInformacion_CantidadActual = AsignarTextosInformacion_CantidadActual;
            instancia.QuitarTextosInformacion_RepetidosIteracion_Actual = QuitarTextosInformacion_RepetidosIteracion_Actual;
            instancia.ConsiderarSoloOperandos_CumplanCondiciones = ConsiderarSoloOperandos_CumplanCondiciones;
            instancia.DigitarTextosInformacion_EnEjecucion = DigitarTextosInformacion_EnEjecucion;
            instancia.AsignarCadenasTexto_Clasificadores = AsignarCadenasTexto_Clasificadores;

            instancia.AsignarTextosInformacion_TextosOperandos = AsignarTextosInformacion_TextosOperandos.ToList();
            instancia.QuitarTextosInformacionRepetidos_TextosOperandos = QuitarTextosInformacionRepetidos_TextosOperandos.ToList();
            instancia.AsignarTextosInformacion_TextosSubOperandos = AsignarTextosInformacion_TextosSubOperandos.ToList();
            instancia.QuitarTextosInformacionRepetidos_TextosSubOperandos = QuitarTextosInformacionRepetidos_TextosSubOperandos.ToList();

            return instancia;
        }
    }

    public class AsignacionTextosOperando_Implicacion
    {
        public DiseñoOperacion Operando { get; set; }
        public DiseñoElementoOperacion SubOperando { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacionAgregadosInstancias_Resultado;
        [IgnoreDataMember]
        public List<AsociacionTextosInformacionImplicacion_OperandoCondicion> TextosInformacionAgregadosInstancias_Resultado_Operandos;
        public AsignacionTextosOperando_Implicacion()
        {
            TextosInformacionAgregadosInstancias_Resultado = new List<string>();
            TextosInformacionAgregadosInstancias_Resultado_Operandos = new List<AsociacionTextosInformacionImplicacion_OperandoCondicion>();
        }

        public AsignacionTextosOperando_Implicacion ReplicarObjeto()
        {
            AsignacionTextosOperando_Implicacion asignacion = new AsignacionTextosOperando_Implicacion();
            asignacion.Operando = Operando;
            asignacion.SubOperando = SubOperando;

            return asignacion;
        }

        public List<string> ObtenerConservarTextosInformacion_Operandos(
            ElementoEjecucionCalculo operando,
            ElementoEjecucionCalculo SubOperando = null,
            EntidadNumero NumeroSubOperando = null)
        {
            return (from A in TextosInformacionAgregadosInstancias_Resultado_Operandos
                    where
                    A.Operando == operando & A.NumeroSubOperando == NumeroSubOperando
                    select A.TextosInformacionAgregadosInstancias_Resultado).FirstOrDefault();
        }

        public void AgregarConservarTextosInformacion_Operandos(List<string> TextosInformacionAgregadosInstancias_Resultado,
            ElementoEjecucionCalculo operando,
            ElementoEjecucionCalculo SubOperando = null,
            EntidadNumero NumeroSubOperando = null)
        {
            TextosInformacionAgregadosInstancias_Resultado_Operandos.Add(new AsociacionTextosInformacionImplicacion_OperandoCondicion()
            {
                TextosInformacionAgregadosInstancias_Resultado = TextosInformacionAgregadosInstancias_Resultado,
                Operando = operando,
                NumeroSubOperando = NumeroSubOperando
            });
        }
    }

    //public class ListaTextosCondicion_Entrada
    //{
    //    public Entrada EntradaRelacionada { get; set; }
    //    public string TextoCondicion { get; set; }

    //    public static List<ListaTextosCondicion_Entrada> ObtenerCopiaLista(List<ListaTextosCondicion_Entrada> lista)
    //    {
    //        List<ListaTextosCondicion_Entrada> nuevaLista = new List<ListaTextosCondicion_Entrada>();

    //        foreach (var item in lista)
    //        {
    //            ListaTextosCondicion_Entrada nuevoItem = new ListaTextosCondicion_Entrada();
    //            nuevoItem.EntradaRelacionada = item.EntradaRelacionada;
    //            if(item.TextoCondicion != null)
    //                nuevoItem.TextoCondicion = new string(item.TextoCondicion.ToArray());
    //            nuevaLista.Add(nuevoItem);
    //        }

    //        return nuevaLista;
    //    }
    //}

    //public class ListaTextosCondicion_Definicion
    //{
    //    public DiseñoTextosInformacion DefinicionRelacionada { get; set; }
    //    public string TextoCondicion { get; set; }
    //}

    //    public class ComparadorListas_ListaTextosCondicion_Entrada : IEqualityComparer<List<ListaTextosCondicion_Entrada>>
    //{
    //    public bool Equals(List<ListaTextosCondicion_Entrada> x, List<ListaTextosCondicion_Entrada> y)
    //    {
    //        if (x.Count == y.Count)
    //        {
    //            for (int indice = 0; indice < x.Count; indice++)
    //            {
    //                if ((new Comparador_ListaTextosCondicion_Entrada()).Equals(x[indice], y[indice]))
    //                    continue;
    //                else
    //                    return false;
    //            }

    //            return true;
    //        }
    //        else
    //            return false;
    //    }

    //    public int GetHashCode(List<ListaTextosCondicion_Entrada> obj)
    //    {
    //        return obj.GetHashCode();
    //    }
    //}

    //public class Comparador_ListaTextosCondicion_Entrada : IEqualityComparer<ListaTextosCondicion_Entrada>
    //{
    //    public bool Equals(ListaTextosCondicion_Entrada x, ListaTextosCondicion_Entrada y)
    //    {
    //        if (x.EntradaRelacionada == y.EntradaRelacionada &
    //            (new string((x.TextoCondicion != null) ? x.TextoCondicion.ToArray(): new char[0] { })) 
    //            == new string((y.TextoCondicion != null) ? y.TextoCondicion.ToArray() : new char[0] { }))
    //            return true;
    //        else
    //            return false;
    //    }

    //    public int GetHashCode(ListaTextosCondicion_Entrada obj)
    //    {
    //        return obj.GetHashCode();
    //    }
    //}

    public class AsociacionTextosInformacionImplicacion_OperandoCondicion
    {
        public ElementoEjecucionCalculo Operando { get; set; }
        public EntidadNumero NumeroSubOperando { get; set; }
        public List<string> TextosInformacionAgregadosInstancias_Resultado { get; set; }
    }

    public class AsociacionOperandosCondiciones_TextosAsignacion_Implicacion
    {
        public DiseñoOperacion Operando { get; set; }
        public DiseñoElementoOperacion SubOperando { get; set; }
        public DiseñoTextosInformacion Entrada { get; set; }
        public CondicionTextosInformacion Condiciones { get; set; }

        public AsociacionOperandosCondiciones_TextosAsignacion_Implicacion ReplicarObjeto()
        {
            AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosCondiciones_TextosAsignacion_Implicacion();
            asociacion.Operando = Operando;
            asociacion.SubOperando = SubOperando;
            asociacion.Entrada = Entrada;
            asociacion.Condiciones = Condiciones;

            return asociacion;
        }
    }

    public class AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion
    {
        public DiseñoOperacion Operando { get; set; }
        public DiseñoElementoOperacion SubOperando { get; set; }
        public DiseñoTextosInformacion Entrada { get; set; }
        public List<DefinicionTextoNombresCantidades> Definiciones { get; set; }
        public AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion()
        {
            Definiciones = new List<DefinicionTextoNombresCantidades>();
        }

        public AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion ReplicarObjeto()
        {
            AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion();
            asociacion.Operando = Operando;
            asociacion.SubOperando = SubOperando;
            asociacion.Entrada = Entrada;
            asociacion.Definiciones = ReplicarDefiniciones();

            return asociacion;
        }

        public List<DefinicionTextoNombresCantidades> ReplicarDefiniciones()
        {
            if (Definiciones != null)
            {
                List<DefinicionTextoNombresCantidades> resultado = new List<DefinicionTextoNombresCantidades>();

                foreach (var item in Definiciones)
                {
                    resultado.Add(item.ReplicarObjeto());
                }

                return resultado;
            }
            else
                return null;
        }
    }
}
