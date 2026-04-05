using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Condiciones
{
    public class CondicionProcesamientoTextosInformacion
    {
        public TipoOpcionCondicionProcesamientoTextosInformacion Tipo { get; set; }
        public TipoOpcionElementoCondicionProcesamientoCantidades TipoElementoDesde { get; set; }
        public CondicionImplicacionTextosInformacion CondicionesTextosInformacion { get; set; }
        public bool FiltrarPorElementos { get; set; }
        public bool FiltrarPorNumeros { get; set; }
        public bool AplicarProcesamiento_SinCondiciones { get; set; }
        public bool AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades { get; set; }
        public bool AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades { get; set; }
        public TipoOpcionElementoAccionProcesamientoCantidades TipoElementoDonde { get; set; }
        public TipoOpcionElementoAccion_InsertarProcesamientoCantidades UbicacionElementoAccion { get; set; }
        public TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades TipoUbicacionAccion_Insertar { get; set; }
        public string ValorFijo_Insercion { get; set; }
        public List<DiseñoOperacion> OperandosInsertar_CantidadesProcesamientoTextos_Desde { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosInsertar_CantidadesProcesamientoTextos_Desde { get; set; }
        public List<DiseñoOperacion> OperandosInsertar_CantidadesProcesamientoTextos_Donde { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosInsertar_CantidadesProcesamientoTextos_Donde { get; set; }
        public bool ReiniciarAcumulacion_OperacionPorFilas { get; set; }
        public bool ProcesarCadenasTextos_SinCumplirCondiciones_Textos { get; set; }
        public CondicionProcesamientoTextosInformacion()
        {
            FiltrarPorNumeros = true;
            OperandosInsertar_CantidadesProcesamientoTextos_Desde = new List<DiseñoOperacion>();
            SubOperandosInsertar_CantidadesProcesamientoTextos_Desde = new List<DiseñoElementoOperacion>();
            OperandosInsertar_CantidadesProcesamientoTextos_Donde = new List<DiseñoOperacion>();
            SubOperandosInsertar_CantidadesProcesamientoTextos_Donde = new List<DiseñoElementoOperacion>();
            UbicacionElementoAccion = TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual;
            TipoUbicacionAccion_Insertar = TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadActual;
        }

        public CondicionProcesamientoTextosInformacion ReplicarObjeto()
        {
            CondicionProcesamientoTextosInformacion condicion = new CondicionProcesamientoTextosInformacion();
            condicion.Tipo = Tipo;
            condicion.TipoElementoDesde = TipoElementoDesde;
            condicion.CondicionesTextosInformacion = CondicionesTextosInformacion?.ReplicarObjeto();
            condicion.FiltrarPorElementos = FiltrarPorElementos;
            condicion.FiltrarPorNumeros = FiltrarPorNumeros;
            condicion.AplicarProcesamiento_SinCondiciones = AplicarProcesamiento_SinCondiciones;
            condicion.ReiniciarAcumulacion_OperacionPorFilas = ReiniciarAcumulacion_OperacionPorFilas;
            condicion.ProcesarCadenasTextos_SinCumplirCondiciones_Textos = ProcesarCadenasTextos_SinCumplirCondiciones_Textos;
            condicion.AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades = AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades;
            condicion.AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades = AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades;
            condicion.TipoElementoDonde = TipoElementoDonde;
            condicion.TipoUbicacionAccion_Insertar = TipoUbicacionAccion_Insertar;
            condicion.OperandosInsertar_CantidadesProcesamientoTextos_Desde = OperandosInsertar_CantidadesProcesamientoTextos_Desde.ToList();
            condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Desde = SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.ToList();
            condicion.OperandosInsertar_CantidadesProcesamientoTextos_Donde = OperandosInsertar_CantidadesProcesamientoTextos_Donde.ToList();
            condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Donde = SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.ToList();
            condicion.ValorFijo_Insercion = ValorFijo_Insercion;
            condicion.UbicacionElementoAccion = UbicacionElementoAccion;

            return condicion;
        }

        public string MostrarEtiquetaCondiciones()
        {
            string texto = string.Empty;

            switch (Tipo)
            {
                case TipoOpcionCondicionProcesamientoTextosInformacion.InsertarTextosExistentes:
                case TipoOpcionCondicionProcesamientoTextosInformacion.EditarTextos:
                    
                    string strCantidadesDe = string.Empty;

                    switch (TipoElementoDesde)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            strCantidadesDe = "las variables o vectores retornados de esta operación ";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            strCantidadesDe = "las variables o vectores retornados ";

                            if (OperandosInsertar_CantidadesProcesamientoTextos_Desde.Any())
                            {
                                foreach (var operando in OperandosInsertar_CantidadesProcesamientoTextos_Desde)
                                {
                                    strCantidadesDe += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDe = strCantidadesDe.Remove(strCantidadesDe.Length - 2);
                            }

                            if (SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Any())
                            {
                                strCantidadesDe += " y los números del vector de la operación ";

                                foreach (var operando in SubOperandosInsertar_CantidadesProcesamientoTextos_Desde)
                                {
                                    strCantidadesDe += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDe = strCantidadesDe.Remove(strCantidadesDe.Length - 2);
                            }

                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            strCantidadesDe = "las variables o vectores retornados de esta operación ";

                            if (OperandosInsertar_CantidadesProcesamientoTextos_Desde.Any())
                            {
                                foreach (var operando in OperandosInsertar_CantidadesProcesamientoTextos_Desde)
                                {
                                    strCantidadesDe += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDe = strCantidadesDe.Remove(strCantidadesDe.Length - 2);
                            }

                            if (SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Any())
                            {
                                strCantidadesDe += " y los números del vector de la operación ";

                                foreach (var operando in SubOperandosInsertar_CantidadesProcesamientoTextos_Desde)
                                {
                                    strCantidadesDe += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDe = strCantidadesDe.Remove(strCantidadesDe.Length - 2);
                            }

                            strCantidadesDe += " y resultados";

                            break;
                    }

                    switch(Tipo)
                    {
                        case TipoOpcionCondicionProcesamientoTextosInformacion.InsertarTextosExistentes:
                            texto += "Insertar nuevas cadenas de texto en " + strCantidadesDe;
                            break;

                        case TipoOpcionCondicionProcesamientoTextosInformacion.EditarTextos:
                            texto += "Editar cadenas de texto de " + strCantidadesDe;
                            break;
                    }

                    string strCantidadesDesde = string.Empty;

                    switch (TipoElementoDonde)
                    {
                        case TipoOpcionElementoAccionProcesamientoCantidades.Resultados:
                            strCantidadesDesde = "los resultados";
                            break;

                        case TipoOpcionElementoAccionProcesamientoCantidades.Operando:
                            strCantidadesDesde = "las variables o vectores retornados de esta operación";

                            if (OperandosInsertar_CantidadesProcesamientoTextos_Donde.Any())
                            {
                                foreach (var operando in OperandosInsertar_CantidadesProcesamientoTextos_Donde)
                                {
                                    strCantidadesDesde += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDesde = strCantidadesDesde.Remove(strCantidadesDesde.Length - 2);
                            }

                            if (SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Any())
                            {
                                strCantidadesDesde += " y los números del vector de la operación ";

                                foreach (var operando in SubOperandosInsertar_CantidadesProcesamientoTextos_Donde)
                                {
                                    strCantidadesDesde += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDesde = strCantidadesDesde.Remove(strCantidadesDesde.Length - 2);
                            }

                            break;

                        case TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados:
                            strCantidadesDesde = "las variables o vectores retornados de esta operación ";

                            if (OperandosInsertar_CantidadesProcesamientoTextos_Donde.Any())
                            {
                                foreach (var operando in OperandosInsertar_CantidadesProcesamientoTextos_Donde)
                                {
                                    strCantidadesDesde += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDesde = strCantidadesDesde.Remove(strCantidadesDesde.Length - 2);
                            }

                            if (SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Any())
                            {
                                strCantidadesDesde += " y los números del vector de la operación ";

                                foreach (var operando in SubOperandosInsertar_CantidadesProcesamientoTextos_Donde)
                                {
                                    strCantidadesDesde += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDesde = strCantidadesDesde.Remove(strCantidadesDesde.Length - 2);
                            }

                            strCantidadesDesde += " y las variables o vectores retornados de esta operación";

                            break;

                        case TipoOpcionElementoAccionProcesamientoCantidades.ValorFijo:
                            strCantidadesDesde = "valor de variable fijo: " + ValorFijo_Insercion;
                            break;
                    }


                    if (CondicionesTextosInformacion != null)
                    {
                        texto += ", que cumplan las siguientes condiciones (si/entonces): ";
                        EtiquetaCondicionImplicacionTextosInformacion etiqueta = new EtiquetaCondicionImplicacionTextosInformacion();
                        etiqueta.Condicion = CondicionesTextosInformacion;
                        etiqueta.MostrarEtiquetaCondiciones();
                        texto += etiqueta.Texto;
                    }

                    //if (FiltrarPorElementos &
                    //    FiltrarPorNumeros)
                    //    texto += " para los elementos y sus números.";
                    //else if (FiltrarPorNumeros &
                    //    !FiltrarPorNumeros)
                    //    texto += " para los elementos.";
                    //else if (!FiltrarPorNumeros &
                    //    FiltrarPorNumeros)
                    //    texto += " para los números de los elementos.";

                    break;

                case TipoOpcionCondicionProcesamientoTextosInformacion.QuitarTextos:
                    
                    strCantidadesDe = string.Empty;

                    switch (TipoElementoDesde)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            strCantidadesDe = "las variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            strCantidadesDe = "las variables o vectores retornados";

                            if (OperandosInsertar_CantidadesProcesamientoTextos_Desde.Any())
                            {
                                foreach (var operando in OperandosInsertar_CantidadesProcesamientoTextos_Desde)
                                {
                                    strCantidadesDe += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDe = strCantidadesDe.Remove(strCantidadesDe.Length - 2);
                            }

                            if (SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Any())
                            {
                                strCantidadesDe += " y los números del vector de la operación ";

                                foreach (var operando in SubOperandosInsertar_CantidadesProcesamientoTextos_Desde)
                                {
                                    strCantidadesDe += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDe = strCantidadesDe.Remove(strCantidadesDe.Length - 2);
                            }
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            strCantidadesDe = "las variables o vectores retornados ";

                            if (OperandosInsertar_CantidadesProcesamientoTextos_Desde.Any())
                            {
                                foreach (var operando in OperandosInsertar_CantidadesProcesamientoTextos_Desde)
                                {
                                    strCantidadesDe += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDe = strCantidadesDe.Remove(strCantidadesDe.Length - 2);
                            }

                            if (SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Any())
                            {
                                strCantidadesDe += " y los números del vector de la operación ";

                                foreach (var operando in SubOperandosInsertar_CantidadesProcesamientoTextos_Desde)
                                {
                                    strCantidadesDe += " '" + operando.NombreCombo + "', ";
                                }

                                strCantidadesDe = strCantidadesDe.Remove(strCantidadesDe.Length - 2);
                            }

                            strCantidadesDe += " y las variables o vectores retornados de esta operación";

                            break;
                    }

                    texto += "Quitar cadenas de texto de " + strCantidadesDe;

                    if (CondicionesTextosInformacion != null)
                    {
                        texto += ", que cumplan las siguientes condiciones (si/entonces): ";
                        EtiquetaCondicionImplicacionTextosInformacion etiqueta = new EtiquetaCondicionImplicacionTextosInformacion();
                        etiqueta.Condicion = CondicionesTextosInformacion;
                        etiqueta.MostrarEtiquetaCondiciones();
                        texto += etiqueta.Texto;
                    }

                    //if (FiltrarPorElementos &
                    //    FiltrarPorNumeros)
                    //    texto += " para los elementos y sus números.";
                    //else if (FiltrarPorNumeros &
                    //    !FiltrarPorNumeros)
                    //    texto += " para los elementos.";
                    //else if (!FiltrarPorNumeros &
                    //    FiltrarPorNumeros)
                    //    texto += " para los números de los elementos.";                    

                    break;

                case TipoOpcionCondicionProcesamientoTextosInformacion.MantenerPosicíonActual_Procesamiento:
                    string strCantidades = string.Empty;

                    switch (TipoElementoDesde)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            strCantidades = "las variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            strCantidades = "las variables o vectores retornados";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            strCantidades = "las variables, vectores retornados de esta operación o las variables o vectores retornados";
                            break;
                    }

                    texto += "Mantener el procesamiento en las cantidades actuales, que son " + strCantidades;

                    if (CondicionesTextosInformacion != null)
                    {
                        texto += " que cumplan las siguientes condiciones (si/entonces): ";
                        EtiquetaCondicionImplicacionTextosInformacion etiqueta = new EtiquetaCondicionImplicacionTextosInformacion();
                        etiqueta.Condicion = CondicionesTextosInformacion;
                        etiqueta.MostrarEtiquetaCondiciones();
                        texto += etiqueta.Texto;
                    }

                    //if (FiltrarPorElementos &
                    //    FiltrarPorNumeros)
                    //    texto += " para los elementos y sus números.";
                    //else if (FiltrarPorNumeros &
                    //    !FiltrarPorNumeros)
                    //    texto += " para los elementos.";
                    //else if (!FiltrarPorNumeros &
                    //    FiltrarPorNumeros)
                    //    texto += " para los números de los elementos.";

                    break;

                case TipoOpcionCondicionProcesamientoTextosInformacion.DetenerProcesamiento:
                    strCantidades = string.Empty;

                    switch (TipoElementoDesde)
                    {
                        case TipoOpcionElementoCondicionProcesamientoCantidades.Resultados:
                            strCantidades = "las variables o vectores retornados de esta operación";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.Operando:
                            strCantidades = "las variables o vectores retornados";
                            break;

                        case TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados:
                            strCantidades = "las variables o vectores retornados de esta operación o las variables o vectores retornados";
                            break;
                    }

                    texto += "Detener el procesamiento en las cantidades actuales, que son " + strCantidades;

                    if (CondicionesTextosInformacion != null)
                    {
                        texto += " que cumplan las siguientes condiciones (si/entonces): ";
                        EtiquetaCondicionImplicacionTextosInformacion etiqueta = new EtiquetaCondicionImplicacionTextosInformacion();
                        etiqueta.Condicion = CondicionesTextosInformacion;
                        etiqueta.MostrarEtiquetaCondiciones();
                        texto += etiqueta.Texto;
                    }

                    //if (FiltrarPorElementos &
                    //    FiltrarPorNumeros)
                    //    texto += " para los elementos y sus números.";
                    //else if (FiltrarPorNumeros &
                    //    !FiltrarPorNumeros)
                    //    texto += " para los elementos.";
                    //else if (!FiltrarPorNumeros &
                    //    FiltrarPorNumeros)
                    //    texto += " para los números de los elementos.";

                    break;
            }

            return texto;
        }

        public bool VerificaEntrada(Entrada entrada)
        {
            if (OperandosInsertar_CantidadesProcesamientoTextos_Desde.Where(j => j.EntradaRelacionada != null).Select(i => i.EntradaRelacionada).Contains(entrada))
                return true;
            else
            {
                if (CondicionesTextosInformacion != null)
                {
                    foreach (var itemCondicion in CondicionesTextosInformacion.Condiciones)
                    {
                        if (itemCondicion.VerificaEntrada(entrada))
                            return true;
                    }

                    return false;
                }
                else
                    return false;
            }
        }

        public bool VerificarOperando(DiseñoOperacion operando)
        {
            List<CondicionProcesamientoTextosInformacion> condicionesAsociadasOperando = new List<CondicionProcesamientoTextosInformacion>();

            ObtenerCondicionElementoCondicion_Condiciones(ref condicionesAsociadasOperando, operando);

            return condicionesAsociadasOperando.Any();
        }

        public bool VerificarSubOperando(DiseñoElementoOperacion operando)
        {
            List<CondicionProcesamientoTextosInformacion> condicionesAsociadasOperando = new List<CondicionProcesamientoTextosInformacion>();

            ObtenerCondicionElementoDiseñoCondicion_Condiciones(ref condicionesAsociadasOperando, operando);

            return condicionesAsociadasOperando.Any();
        }

        public void ObtenerCondicionElementoCondicion_Condiciones(ref List<CondicionProcesamientoTextosInformacion> condiciones, DiseñoOperacion elemento)
        {
            if (OperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(elemento)) //||
                //OperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(elemento))
            {
                condiciones.Add(this);
            }
        }

        public void ObtenerCondicionElementoDiseñoCondicion_Condiciones(ref List<CondicionProcesamientoTextosInformacion> condiciones, DiseñoElementoOperacion elemento)
        {
            if (SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(elemento)) //||
                //SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(elemento))
            {
                condiciones.Add(this);
            }
        }

        public void QuitarElemento(DiseñoOperacion elemento)
        {
            CondicionesTextosInformacion.QuitarReferenciasCondicionesElemento(elemento);

            if (elemento.EntradaRelacionada != null)
                CondicionesTextosInformacion.QuitarReferenciasCondicionesEntrada(elemento.EntradaRelacionada);

            if(OperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(elemento))
                OperandosInsertar_CantidadesProcesamientoTextos_Desde.Remove(elemento);

            if(OperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(elemento))
                OperandosInsertar_CantidadesProcesamientoTextos_Donde.Remove(elemento);
        }

        public void QuitarSubElemento(DiseñoElementoOperacion elemento)
        {
            CondicionesTextosInformacion.QuitarReferenciasCondicionesElemento_Interno(elemento);

            if (elemento.EntradaRelacionada != null)
                CondicionesTextosInformacion.QuitarReferenciasCondicionesEntrada(elemento.EntradaRelacionada);

            if(SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(elemento))
                SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Remove(elemento);

            if(SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(elemento))
                SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Remove(elemento);
        }
    }
}
