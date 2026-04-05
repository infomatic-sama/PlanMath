using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class OrdenacionNumeros
    {
        public bool OrdenarNumerosDeMenorAMayor { get; set; }
        public bool OrdenarNumerosDeMayorAMenor { get; set; }
        public bool OrdenarNumerosPorCantidad { get; set; }
        public bool OrdenarNumerosPorNombre { get; set; }
        public TipoOpcion_OrdenamientoNumerosSalidas Tipo_OrdenamientoNumeros { get; set; }
        public bool OrdenarTextosInformacionCantidades_Ejecucion { get; set; }
        public bool OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades { get; set; }
        public bool OrdenarTextosDeMenorAMayor { get; set; }
        public bool OrdenarTextosDeMenorAMayor_SinOrdenarCantidades { get; set; }
        public bool OrdenarTextosDeMayorAMenor { get; set; }
        public bool OrdenarTextosDeMayorAMenor_SinOrdenarCantidades { get; set; }
        public int CantidadDivisionTextosInformacion { get; set; }
        public bool CadenaTextoDividida {  get; set; }
        public List<DivisionCadenaTexto_Ordenacion> DivisionesCadenaTexto_Ordenaciones { get; set; }
        public OrdenacionNumeros()
        {
            Tipo_OrdenamientoNumeros = TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion;
            OrdenarTextosDeMenorAMayor = true;
            OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = true;
            CantidadDivisionTextosInformacion = 1;
            DivisionesCadenaTexto_Ordenaciones = new List<DivisionCadenaTexto_Ordenacion>();
        }

        public OrdenacionNumeros CopiarObjeto()
        {
            OrdenacionNumeros resultado = new OrdenacionNumeros();
            
            resultado.OrdenarNumerosDeMenorAMayor = OrdenarNumerosDeMenorAMayor;
            resultado.OrdenarNumerosDeMayorAMenor = OrdenarNumerosDeMayorAMenor;
            resultado.OrdenarNumerosPorCantidad = OrdenarNumerosPorCantidad;
            resultado.OrdenarNumerosPorNombre = OrdenarNumerosPorNombre;
            resultado.Tipo_OrdenamientoNumeros = Tipo_OrdenamientoNumeros;
            resultado.OrdenarTextosInformacionCantidades_Ejecucion = OrdenarTextosInformacionCantidades_Ejecucion;
            resultado.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;
            resultado.OrdenarTextosDeMenorAMayor = OrdenarTextosDeMenorAMayor;
            resultado.OrdenarTextosDeMayorAMenor = OrdenarTextosDeMayorAMenor;
            resultado.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
            resultado.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;
            resultado.CantidadDivisionTextosInformacion = CantidadDivisionTextosInformacion;
            resultado.CadenaTextoDividida = CadenaTextoDividida;
            resultado.DivisionesCadenaTexto_Ordenaciones = DivisionesCadenaTexto_Ordenaciones.ToList();

            return resultado;
        }
        public void ObtenerDivisionesCadena(EntidadNumero item, string cadena)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                var divisionesOrdenadas = DivisionesCadenaTexto_Ordenaciones.OrderBy(i => i.IndiceLetraInicial).ToList();
                foreach (var itemDivision in divisionesOrdenadas)
                {
                    if (itemDivision.IndiceLetraInicial >= 0 & itemDivision.IndiceLetraInicial <= cadena.Length - 1)
                    {
                        if (itemDivision.CantidadLetras >= 1 & itemDivision.CantidadLetras <= cadena.Length - itemDivision.IndiceLetraInicial)
                        {
                            item.DivisionesCadena_Ordenamiento.Add(cadena.Substring(itemDivision.IndiceLetraInicial, itemDivision.CantidadLetras));
                        }
                    }
                }
            }
        }

        public IOrderedEnumerable<EntidadNumero> OrdenarPorDivisiones(List<EntidadNumero> listaOrdenada)
        {
            var listaOrdenamiento = listaOrdenada.OrderBy(i =>
            {
                if (i.DivisionesCadena_Ordenamiento.Any())
                {
                    if (OrdenarTextosInformacionCantidades_Ejecucion)
                        i.OrdenarDivisiones_TextosInformacion(this);

                    i.indiceOrdenamiento++;
                    return i.DivisionesCadena_Ordenamiento[i.indiceOrdenamiento];
                }
                else
                    return null;
            });

            return listaOrdenamiento;
        }

        public IOrderedEnumerable<EntidadNumero> OrdenarPorDivisiones_CantidadMaxima(IOrderedEnumerable<EntidadNumero> listaOrdenamiento,
            int indiceCantidadTextos, int cantidadMaximaTextos)
        {
            for (int indice = indiceCantidadTextos; indice <= cantidadMaximaTextos; indice++)
            {
                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                {
                    if (i.DivisionesCadena_Ordenamiento.Any())
                    {
                        if (OrdenarTextosInformacionCantidades_Ejecucion)
                            i.OrdenarDivisiones_TextosInformacion(this);

                        if (i.indiceOrdenamiento < i.DivisionesCadena_Ordenamiento.Count - 1)
                        {
                            i.indiceOrdenamiento++;
                            return i.DivisionesCadena_Ordenamiento[i.indiceOrdenamiento];
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                });
            }

            return listaOrdenamiento;
        }

        public IOrderedEnumerable<EntidadNumero> OrdenarPorDivisiones_Descending(List<EntidadNumero> listaOrdenada)
        {
            var listaOrdenamiento = listaOrdenada.OrderBy(i =>
            {
                if (i.DivisionesCadena_Ordenamiento.Any())
                {
                    if (OrdenarTextosInformacionCantidades_Ejecucion)
                        i.OrdenarDivisiones_TextosInformacion(this);

                    i.indiceOrdenamiento++;
                    return i.DivisionesCadena_Ordenamiento[i.indiceOrdenamiento];
                }
                else
                    return null;
            });

            return listaOrdenamiento;
        }

        public IOrderedEnumerable<EntidadNumero> OrdenarPorDivisiones_CantidadMaxima_Descending(IOrderedEnumerable<EntidadNumero> listaOrdenamiento,
            int indiceCantidadTextos, int cantidadMaximaTextos)
        {
            for (int indice = indiceCantidadTextos; indice <= cantidadMaximaTextos; indice++)
            {
                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                {
                    if (i.DivisionesCadena_Ordenamiento.Any())
                    {
                        if (OrdenarTextosInformacionCantidades_Ejecucion)
                            i.OrdenarDivisiones_TextosInformacion(this);

                        if (i.indiceOrdenamiento < i.DivisionesCadena_Ordenamiento.Count - 1)
                        {
                            i.indiceOrdenamiento++;
                            return i.DivisionesCadena_Ordenamiento[i.indiceOrdenamiento];
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                });
            }

            return listaOrdenamiento;
        }
    }
}
