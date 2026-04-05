using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class EntidadNumero
    {
        public string Nombre { get; set; }
        public List<string> Textos { get; set; }
        public List<ListaTextosDigitados> TextosDigitados { get; set; }
        public double Numero { get; set; }
        [IgnoreDataMember]
        public BusquedaArchivoEjecucion BusquedaRelacionada { get; set; }
        [IgnoreDataMember]
        public bool NoIncluirTextosInformacion_CantidadAInsertar { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosSalidaOperacion_SeleccionarOrdenar
    {
            get
            {
                return elementosSalida_SeleccionarOrdenar;
            }
            set
            {
                elementosSalida_SeleccionarOrdenar = value;
            }
        }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos
        {
            get
            {
                return elementosSalida_SeleccionarOrdenar.Where(i => i.ElementoDiseñoRelacionado != null).ToList();
            }
            set
            {
                elementosSalida_SeleccionarOrdenar = value;
            }
        }
        [IgnoreDataMember]
        public bool CumpleAlgunaCondicion_SeleccionarCantidades { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosSalidaOperacion_Agrupamiento { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> ElementosInternosSalidaOperacion_Agrupamiento { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> ElementosInternosSalidaOperacion_SeleccionarOrdenar { get; set; }
        [IgnoreDataMember]
        public List<ElementoEjecucionCalculo> ElementosSalidaOperacion_CondicionFlujo { get; set; }
        [IgnoreDataMember]
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> ElementosInternosSalidaOperacion_CondicionFlujo { get; set; }
        [IgnoreDataMember]
        public CondicionFlujo CondicionFlujoRelacionada { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacion_CondicionAsignacionAntes { get; set; }
        [IgnoreDataMember]
        public int PosicionElemento_DefinicionNombres { get; set; }
        [IgnoreDataMember]
        public int PosicionElemento_Lectura { get; set; }
        [IgnoreDataMember]
        public int HashCode_NumeroAgregacion_Ejecucion { get; set; }
        [IgnoreDataMember]
        public ElementoOperacionAritmeticaEjecucion itemOperacion_Ejecucion_OrdenamientoSalidas { get; set; }
        [IgnoreDataMember]
        public ElementoOperacionAritmeticaEjecucion itemOperacion_Ejecucion_OrdenamientoClasificadores { get; set; }
        [IgnoreDataMember]
        public int indiceOrdenamiento { get; set; }
        [IgnoreDataMember]
        public bool ConsiderarOperandoCondicion_SiCumple { get; set; }
        [IgnoreDataMember]
        public bool NoConsiderarCondiciones_Implicacion;
        [IgnoreDataMember]
        public bool EsCantidadInsertada_ProcesamientoCantidades { get; set; }
        [IgnoreDataMember]
        public bool EsCantidadInsertada_ProcesamientoCantidadesTemp { get; set; }
        [IgnoreDataMember]
        public List<string> TextosInformacion_CumplenCondiciones_Anteriores { get; set; }
        List<ElementoEjecucionCalculo> elementosSalida_SeleccionarOrdenar;
        [IgnoreDataMember]
        public bool TextosInformacionCondicionesAntes_Asignados { get; set; }
        [IgnoreDataMember]
        public string Agrupacion_PorSeparado { get; set; }
        [IgnoreDataMember]
        public bool AgregarNumero_PorFilas { get; set; }
        [IgnoreDataMember]
        public List<string> DivisionesCadena_Ordenamiento {  get; set; }
        [IgnoreDataMember]
        public string TextoAcompañante { get; set; }
        [IgnoreDataMember]
        public List<Clasificador> Clasificadores_SeleccionarOrdenar { get; set; }
        [IgnoreDataMember]
        public bool ValorCondiciones_SeleccionarOrdenar { get; set; }
        [IgnoreDataMember]
        public bool EsCantidadQuitada_ProcesamientoCantidades { get; set; }
        public List<Clasificador> Clasificadores_SeleccionarOrdenar_Resultados { get; set; }
        [IgnoreDataMember]
        public bool Seteo_ConsiderarOperandoCondicion_SiCumple {  get; set; }
        [IgnoreDataMember]
        public List<Clasificador> Clasificadores_SeleccionarOrdenar_Originales_Temp { get; set; }
        public EntidadNumero() 
        {
            Textos = new List<string>();
            ElementosSalidaOperacion_SeleccionarOrdenar = new List<ElementoEjecucionCalculo>();
            Clasificadores_SeleccionarOrdenar = new List<Clasificador>();
            ElementosSalidaOperacion_CondicionFlujo = new List<ElementoEjecucionCalculo>();
            ElementosSalidaOperacion_Agrupamiento = new List<ElementoEjecucionCalculo>();
            ElementosInternosSalidaOperacion_Agrupamiento = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            TextosInformacion_CondicionAsignacionAntes = new List<string>();
            indiceOrdenamiento = -1;
            ElementosInternosSalidaOperacion_SeleccionarOrdenar = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            ElementosInternosSalidaOperacion_CondicionFlujo = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            TextosInformacion_CumplenCondiciones_Anteriores = new List<string>();
            Numero = 0;
            TextosDigitados = new List<ListaTextosDigitados>();
            DivisionesCadena_Ordenamiento = new List<string>();
            TextoAcompañante = string.Empty;
            Clasificadores_SeleccionarOrdenar_Resultados = new List<Clasificador>();
            Clasificadores_SeleccionarOrdenar_Originales_Temp = new List<Clasificador>();
        }
        public EntidadNumero(string nom, double num, bool? EsCantidadInsertada_ProcesamientoCantidades_ = null)
        {
            Nombre = nom;
            Numero = num;
            Textos = new List<string>();
            ElementosSalidaOperacion_SeleccionarOrdenar = new List<ElementoEjecucionCalculo>();
            Clasificadores_SeleccionarOrdenar = new List<Clasificador>();
            ElementosSalidaOperacion_CondicionFlujo = new List<ElementoEjecucionCalculo>();
            ElementosSalidaOperacion_Agrupamiento = new List<ElementoEjecucionCalculo>();
            ElementosInternosSalidaOperacion_Agrupamiento = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            TextosInformacion_CondicionAsignacionAntes = new List<string>();
            indiceOrdenamiento = -1;
            ElementosInternosSalidaOperacion_SeleccionarOrdenar = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
            ElementosInternosSalidaOperacion_CondicionFlujo = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();

            if (EsCantidadInsertada_ProcesamientoCantidades_ != null &&
                EsCantidadInsertada_ProcesamientoCantidades_.HasValue)
                EsCantidadInsertada_ProcesamientoCantidades = EsCantidadInsertada_ProcesamientoCantidades_.Value;

            TextosInformacion_CumplenCondiciones_Anteriores = new List<string>();
            TextosDigitados = new List<ListaTextosDigitados>();
            DivisionesCadena_Ordenamiento = new List<string>();
            TextoAcompañante = string.Empty;
        }

        public static bool CompararNumeros(EntidadNumero numero1, EntidadNumero numero2)
        {
            if (numero1.HashCode_NumeroAgregacion_Ejecucion == numero2.HashCode_NumeroAgregacion_Ejecucion &&
                (numero1.ElementosSalidaOperacion_Agrupamiento.Intersect(numero2.ElementosSalidaOperacion_Agrupamiento).Any() ||
                numero1.ElementosSalidaOperacion_SeleccionarOrdenar.Intersect(numero2.ElementosSalidaOperacion_SeleccionarOrdenar).Any() ||
                numero1.ElementosSalidaOperacion_CondicionFlujo.Intersect(numero2.ElementosSalidaOperacion_CondicionFlujo).Any()))
                return true;
            else
                return false;

            //if (((!string.IsNullOrEmpty(numero1.Nombre) & !string.IsNullOrEmpty(numero2.Nombre)) &&
            //        (numero1.Nombre.Trim().Replace("\t", string.Empty).ToLower().Equals(numero2.Nombre.Trim().Replace("\t", string.Empty).ToLower()))) ||
            //        (string.IsNullOrEmpty(numero1.Nombre) & string.IsNullOrEmpty(numero2.Nombre)))
            //{
            //    if (numero1.Numero.Equals(numero2.Numero))
            //    {
            //        if (numero1.Textos.Count == numero2.Textos.Count)
            //        {
            //            ComparadorTextosInformacion compararTextos = new ComparadorTextosInformacion(TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual);
            //            if (compararTextos.ContarInterseccion(numero1.Textos, numero2.Textos) == numero1.Textos.Count)
            //            {
            //                if (numero1.ElementosSalidaOperacion_SeleccionarOrdenar.Count == numero2.ElementosSalidaOperacion_SeleccionarOrdenar.Count)
            //                {
            //                    if (ContarInterseccion(numero1.ElementosSalidaOperacion_SeleccionarOrdenar, numero2.ElementosSalidaOperacion_SeleccionarOrdenar) == numero1.ElementosSalidaOperacion_SeleccionarOrdenar.Count)
            //                    {
            //                        return true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //return false;
        }

        private static int ContarInterseccion(IEnumerable<ElementoDiseñoOperacionAritmeticaEjecucion> lista1, IEnumerable<ElementoDiseñoOperacionAritmeticaEjecucion> lista2)
        {
            int contarIguales = 0;

            foreach (var item1 in lista1)
            {
                foreach (var item2 in lista2)
                {
                    if (item1.HashCode_ElementoDiseñoOperacion == item2.HashCode_ElementoDiseñoOperacion)
                        contarIguales++;
                }
            }

            return contarIguales;
        }

        public static bool FiltrarNumeros(EntidadNumero i, ElementoOperacionAritmeticaEjecucion elementoOperacion,
            List<EntidadNumero> NumerosFiltrados)
        {
            return (!i.ElementosSalidaOperacion_SeleccionarOrdenar.Any() || i.ElementosSalidaOperacion_SeleccionarOrdenar.Any() &&
                            i.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(elementoOperacion) && (!i.ElementosInternosSalidaOperacion_SeleccionarOrdenar.Any() ||
                                i.ElementosInternosSalidaOperacion_SeleccionarOrdenar.Contains(elementoOperacion))) &
                            (!i.ElementosSalidaOperacion_CondicionFlujo.Any() || i.ElementosSalidaOperacion_CondicionFlujo.Any() &&
                            i.ElementosSalidaOperacion_CondicionFlujo.Contains(elementoOperacion) && (!i.ElementosInternosSalidaOperacion_CondicionFlujo.Any() ||
                                i.ElementosInternosSalidaOperacion_CondicionFlujo.Contains(elementoOperacion))) &
                            (!NumerosFiltrados.Any() || NumerosFiltrados.Contains(i));
        }

        public void OrdenarTextosInformacion(OrdenacionNumeros ordenacion)
        {
            if(ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
            {
                if (ordenacion.OrdenarTextosDeMenorAMayor)
                    Textos = Textos.OrderBy(i => i).ToList();
                else if(ordenacion.OrdenarTextosDeMayorAMenor)
                    Textos = Textos.OrderByDescending(i => i).ToList();
            }
        }

        public void OrdenarDivisiones_TextosInformacion(OrdenacionNumeros ordenacion)
        {
            if (ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
            {
                if (ordenacion.OrdenarTextosDeMenorAMayor)
                    DivisionesCadena_Ordenamiento = DivisionesCadena_Ordenamiento.OrderBy(i => i).ToList();
                else if (ordenacion.OrdenarTextosDeMayorAMenor)
                    DivisionesCadena_Ordenamiento = DivisionesCadena_Ordenamiento.OrderByDescending(i => i).ToList();
            }
        }

        public void OrdenarTextosInformacion_SinOrdenarCantidades(OrdenacionNumeros ordenacion)
        {
            if (ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
            {
                if (ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades)
                    Textos = Textos.OrderBy(i => i).ToList();
                else if (ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades)
                    Textos = Textos.OrderByDescending(i => i).ToList();
            }
        }

        public EntidadNumero CopiarObjeto(bool? EsCantidadInsertada_ProcesamientoCantidades = null,
            Clasificador ClasificadorActual = null, ElementoOperacionAritmeticaEjecucion operacion = null,
            bool? Traspaso = false)
        {
            EntidadNumero numero = new EntidadNumero();
            numero.BusquedaRelacionada = BusquedaRelacionada;
            numero.CondicionFlujoRelacionada = CondicionFlujoRelacionada;
            numero.ElementosSalidaOperacion_Agrupamiento = ElementosSalidaOperacion_Agrupamiento.ToList();
            numero.ElementosSalidaOperacion_CondicionFlujo = ElementosSalidaOperacion_CondicionFlujo.ToList();
            numero.ElementosSalidaOperacion_SeleccionarOrdenar = ElementosSalidaOperacion_SeleccionarOrdenar.ToList();
            numero.Clasificadores_SeleccionarOrdenar = Clasificadores_SeleccionarOrdenar.ToList();
            numero.ElementosInternosSalidaOperacion_Agrupamiento = ElementosInternosSalidaOperacion_Agrupamiento.ToList();
            numero.ElementosInternosSalidaOperacion_CondicionFlujo = ElementosInternosSalidaOperacion_CondicionFlujo.ToList();
            numero.ElementosInternosSalidaOperacion_SeleccionarOrdenar = ElementosInternosSalidaOperacion_SeleccionarOrdenar.ToList();
            numero.Nombre = Nombre?.ToString();
            numero.Numero = Numero;
            numero.Textos = Textos.ToList();
            numero.TextosInformacion_CondicionAsignacionAntes = TextosInformacion_CondicionAsignacionAntes.ToList();
            numero.Agrupacion_PorSeparado = Agrupacion_PorSeparado;
            numero.AgregarNumero_PorFilas = AgregarNumero_PorFilas;

            if (!(EsCantidadInsertada_ProcesamientoCantidades != null &&
                EsCantidadInsertada_ProcesamientoCantidades.HasValue &&
                EsCantidadInsertada_ProcesamientoCantidades.Value))
                numero.Clasificadores_SeleccionarOrdenar.AddRange(Clasificadores_SeleccionarOrdenar);
            else
            {
                if(Traspaso.HasValue &&
                    Traspaso.Value)
                {
                    numero.Clasificadores_SeleccionarOrdenar.AddRange(Clasificadores_SeleccionarOrdenar);
                }
                else
                    numero.Clasificadores_SeleccionarOrdenar.Add(ClasificadorActual);
            }
            
            if(operacion != null)
            {
                foreach (var itemClasificador in numero.Clasificadores_SeleccionarOrdenar)
                {
                    if (!operacion.Clasificadores_Cantidades.Contains(itemClasificador))
                        operacion.Clasificadores_Cantidades.Add(itemClasificador);
                }
            }

            if (EsCantidadInsertada_ProcesamientoCantidades != null &&
                EsCantidadInsertada_ProcesamientoCantidades.HasValue)
                numero.EsCantidadInsertada_ProcesamientoCantidades = EsCantidadInsertada_ProcesamientoCantidades.Value;

            return numero;
        }

        public void QuitarTextosInformacionCondicionesAntes()
        {
            if (TextosInformacionCondicionesAntes_Asignados)
            {
                foreach (var item in TextosInformacion_CondicionAsignacionAntes)
                {
                    var textosEncontrados = Textos.Where(i => string.Compare(i, item) == 0).ToList();

                    foreach (var itemEncontrado in textosEncontrados)
                        Textos.Remove(itemEncontrado);
                }
            }

            TextosInformacionCondicionesAntes_Asignados = false;
            TextosInformacion_CondicionAsignacionAntes.Clear();
        }

        public bool EvaluarCondicionesLimpieza(ConfiguracionLimpiarDatos config, List<EntidadNumero> Numeros)
        {
            bool NoQuitar = true;

            if (config.QuitarCantidadesDuplicadas)
            {
                bool QuitarCantidadesDuplicadas = config.QuitarCantidadesDuplicadas;
                bool QuitarCantidadesTextosDuplicadas = config.QuitarCantidadesTextosDuplicadas;
                bool QuitarCantidadesTextosDentroDuplicados = config.QuitarCantidadesTextosDentroDuplicados;

                if (QuitarCantidadesDuplicadas)
                {
                    if (!Numeros.Any(i => Numeros.Count(i => Numeros.IndexOf(i) != Numeros.IndexOf(this) && i.Numero == Numero) > 0))
                    {
                        QuitarCantidadesDuplicadas = false;
                    }
                    else
                    {
                        int indicePrimero = Numeros.IndexOf(Numeros.FirstOrDefault(i => i.Numero == Numero));
                        if(indicePrimero == Numeros.IndexOf(this))
                        {
                            QuitarCantidadesDuplicadas = false;
                        }
                    }
                }

                if (QuitarCantidadesTextosDuplicadas)
                {
                    if (!Numeros.Any(i => Numeros.IndexOf(i) != Numeros.IndexOf(this) && !i.Textos.Select(i => i.ToLower()).Except(Textos.Select(i => i.ToLower())).Any()))
                    {
                        QuitarCantidadesTextosDuplicadas = false;
                    }
                    else
                    {
                        int indicePrimero = Numeros.IndexOf(Numeros.FirstOrDefault(i => i.Numero == Numero));
                        if (indicePrimero == Numeros.IndexOf(this))
                        {
                            QuitarCantidadesDuplicadas = false;
                        }
                    }
                }

                if (QuitarCantidadesTextosDentroDuplicados)
                {
                    if (!Numeros.Any(i => !i.Textos.Select(i => i.ToLower()).Except(i.Textos.Select(i => i.ToLower())).Any()))
                    {
                        QuitarCantidadesTextosDentroDuplicados = false;
                    }
                }

                if (config.Conector1_Duplicados == TipoConectorCondiciones_ConjuntoBusquedas.O)
                {
                    if (QuitarCantidadesDuplicadas |
                        QuitarCantidadesTextosDuplicadas)
                    {
                        NoQuitar = false;
                    }
                }
                else if (config.Conector1_Duplicados == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                {
                    if (QuitarCantidadesDuplicadas &
                        QuitarCantidadesTextosDuplicadas)
                    {
                        NoQuitar = false;
                    }
                }

                if (config.Conector2_Duplicados == TipoConectorCondiciones_ConjuntoBusquedas.O)
                {
                    if (!(!NoQuitar |
                        QuitarCantidadesTextosDentroDuplicados))
                    {
                        NoQuitar = true;
                    }
                }
                else if (config.Conector2_Duplicados == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                {
                    if (!(!NoQuitar &
                        QuitarCantidadesTextosDentroDuplicados))
                    {
                        NoQuitar = true;
                    }
                }
            }

            if (config.QuitarCeros)
            {
                bool QuitarCerosConTextos = config.QuitarCerosConTextos;
                bool QuitarCerosSinTextos = config.QuitarCerosSinTextos;

                if (QuitarCerosConTextos)
                {
                    if (!(Numero == 0 && Textos.Any()))
                    {
                        QuitarCerosConTextos = false;
                    }
                }

                if (QuitarCerosSinTextos)
                {
                    if (!(Numero == 0 && !Textos.Any()))
                    {
                        QuitarCerosSinTextos = false;
                    }
                }

                if (config.Conector1_Ceros == TipoConectorCondiciones_ConjuntoBusquedas.O)
                {
                    if (QuitarCerosConTextos |
                        QuitarCerosSinTextos)
                    {
                        NoQuitar = false;
                    }
                }
                else if (config.Conector1_Ceros == TipoConectorCondiciones_ConjuntoBusquedas.Y)
                {
                    if (QuitarCerosConTextos &
                        QuitarCerosSinTextos)
                    {
                        NoQuitar = false;
                    }
                }
            }

            if (config.QuitarCantidadesSinTextos)
            {
                if (!Textos.Any())
                {
                    NoQuitar = false;
                }
            }

            if (config.QuitarNegativas)
            {
                if (Numero < 0)
                {
                    NoQuitar = false;
                }
            }

            return NoQuitar;
        }

        public void Redondear(ConfiguracionRedondearCantidades config)
        {
            if (config != null)
            {
                if (config.RedondearPar_Cercano)
                {
                    Numero = Math.Round(Numero, MidpointRounding.ToEven);
                }
                else if (config.RedondearNumero_CercanoDeCero)
                {
                    Numero = Math.Round(Numero, MidpointRounding.ToZero);
                }
                else if (config.RedondearNumero_LejanoDeCero)
                {
                    Numero = Math.Round(Numero, MidpointRounding.AwayFromZero);
                }
                else if (config.RedondearNumero_Menor)
                {
                    Numero = Math.Round(Numero, MidpointRounding.ToNegativeInfinity);
                }
                else if (config.RedondearNumero_Mayor)
                {
                    Numero = Math.Round(Numero, MidpointRounding.ToPositiveInfinity);
                }
            }
        }
        public string ObtenerTextosInformacion_Cadena(bool paraProcesarClasificadores = false)
        {
            string cadena = string.Empty;

            if (Textos.Any(i => !string.IsNullOrEmpty(i)))
            {
                cadena += " Cadenas de texto: ";

                foreach (var itemTexto in Textos.Where(i => !string.IsNullOrEmpty(i)))
                {
                    if(paraProcesarClasificadores)
                        cadena += itemTexto + (itemTexto == Textos.LastOrDefault() ? string.Empty : "|");
                    else
                        cadena += itemTexto + (itemTexto == Textos.LastOrDefault() ? "." : ", ");
                }
            }

            return cadena;
        }

        public void LimpiarVariables_SeleccionNumero()
        {
            Textos.Clear();
            Clasificadores_SeleccionarOrdenar.Clear();
            ElementosInternosSalidaOperacion_Agrupamiento.Clear();
            ElementosInternosSalidaOperacion_CondicionFlujo.Clear();
            ElementosInternosSalidaOperacion_SeleccionarOrdenar.Clear();
            ElementosSalidaOperacion_Agrupamiento.Clear();
            ElementosSalidaOperacion_CondicionFlujo.Clear();
            ElementosSalidaOperacion_SeleccionarOrdenar.Clear();
            ElementosSalidaOperacion_SeleccionarOrdenar_NoNulos.Clear();
            Agrupacion_PorSeparado = null;
        }

        public string ObtenerPosicionTexto(List<EntidadNumero> Numeros, TipoOpcionPosicion OpcionValorPosicion)
        {
            string posicionLiteral = string.Empty;

            switch (OpcionValorPosicion)
            {
                case TipoOpcionPosicion.PosicionPrimera:
                    if (this == Numeros.First())
                        posicionLiteral = "primera";
                    break;

                case TipoOpcionPosicion.PosicionSegunda:
                    if (Numeros.IndexOf(this) + 1 == 2)
                        posicionLiteral = "segunda";
                    break;

                case TipoOpcionPosicion.PosicionMitad:
                    if (Numeros.IndexOf(this) + 1 == Math.Round(Numeros.Count / 2.0, MidpointRounding.ToPositiveInfinity))
                        posicionLiteral = "mitad";
                    break;

                case TipoOpcionPosicion.PosicionPenultima:
                    if (Numeros.Count - (Numeros.IndexOf(this) + 1) == 1)
                        posicionLiteral = "penultima";
                    break;

                case TipoOpcionPosicion.PosicionUltima:
                    if (Numeros.Count == Numeros.IndexOf(this) + 1)
                        posicionLiteral = "ultima";
                    break;
            }

            return posicionLiteral;
        }
    }
}
