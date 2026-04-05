using ProcessCalc.Controles;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using ProcessCalc.Ventanas.Definiciones;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xml.Linq;
using static ProcessCalc.Entidades.ElementoDiseñoOperacionAritmeticaEjecucion;

namespace ProcessCalc.Entidades
{
    public partial class EjecucionCalculo
    {
        public List<string> GenerarTextosInformacion(List<string> textos)
        {
            List<string> lista = new List<string>();

            foreach (var itemTexto in textos)
            {
                if (itemTexto.Contains("|"))
                {
                    string[] subCadenas = itemTexto.Split('|');

                    foreach(var subCadena in subCadenas)
                    {
                        string texto = new string(subCadena.ToArray());
                        lista.Add((texto.Replace("\0", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty)).Trim());
                    }
                }
                else
                {
                    string texto = new string(itemTexto.ToArray());
                    lista.Add((texto.Replace("\0", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty)).Trim());
                }
            }

            return lista;
        }

        public void QuitarTextosInformacion_Repetidos(List<string> Textos, bool quitarRepetidos)
        {
            if (quitarRepetidos)
            {
                List<string> TextosNoRepetidos = new List<string>();

                foreach (var item in Textos)
                {
                    if (!TextosNoRepetidos.Contains(item))
                    {
                        TextosNoRepetidos.Add(item);
                    }
                }

                Textos.Clear();
                Textos.AddRange(TextosNoRepetidos);
            }
        }

        public List<string> ObtenerTextosInformacionSalidas_Elemento(EntidadNumero elemento, CondicionTextosInformacion condiciones)
        {
            List<string> listaTextos = new List<string>();

            switch(condiciones.Tipo_OrdenamientoNumerosSalidas)
            {
                case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:
                    listaTextos.Add(elemento.Nombre);
                    listaTextos.AddRange(elemento.Textos);
                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:
                    listaTextos.AddRange(elemento.Textos);
                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                    listaTextos.Add(elemento.Nombre);
                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas:
                    listaTextos.AddRange(elemento.Textos.Intersect(condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar));
                    break;
            }

            return listaTextos;
        }

        public List<string> ObtenerTextosInformacionClasificadores_Elemento(EntidadNumero elemento, CondicionTextosInformacion condiciones)
        {
            List<string> listaTextos = new List<string>();

            switch (condiciones.Tipo_OrdenamientoNumerosClasificadores)
            {
                case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:
                    listaTextos.Add(elemento.Nombre);
                    listaTextos.AddRange(elemento.Textos);
                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:
                    listaTextos.AddRange(elemento.Textos);
                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                    listaTextos.Add(elemento.Nombre);
                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas:
                    listaTextos.AddRange(elemento.Textos.Intersect(condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar));
                    break;
            }

            return listaTextos;
        }
        public List<string> ObtenerTextosInformacionSalidas_ElementoNumero(EntidadNumero elemento, CondicionTextosInformacion condiciones)
        {
            List<string> listaTextos = new List<string>();

            if (elemento != null)
            {
                switch (condiciones.Tipo_OrdenamientoNumerosSalidas)
                {
                    case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:
                        listaTextos.Add(elemento.Nombre);
                        listaTextos.AddRange(elemento.Textos);
                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:
                        listaTextos.AddRange(elemento.Textos);
                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                        listaTextos.Add(elemento.Nombre);
                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas:
                        listaTextos.AddRange(elemento.Textos.Intersect(condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar));
                        break;
                }
            }

            return listaTextos;
        }

        public List<string> ObtenerTextosInformacionClasificadores_ElementoNumero(EntidadNumero elemento, CondicionTextosInformacion condiciones)
        {
            List<string> listaTextos = new List<string>();

            if (elemento != null)
            {
                switch (condiciones.Tipo_OrdenamientoNumerosClasificadores)
                {
                    case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:
                        listaTextos.Add(elemento.Nombre);
                        listaTextos.AddRange(elemento.Textos);
                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:
                        listaTextos.AddRange(elemento.Textos);
                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                        listaTextos.Add(elemento.Nombre);
                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas:
                        listaTextos.AddRange(elemento.Textos.Intersect(condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar));
                        break;
                }
            }

            return listaTextos;
        }
        public bool CompararTextosInformacionElementosSalidas(List<string> listaTexto1, List<string> listaTexto2)
        {
            if(listaTexto1.Count != listaTexto2.Count)
            {
                return false;
            }
            else
            {
                for(int indice = 0; indice <= listaTexto1.Count - 1; indice++)
                {
                    if (string.Compare(listaTexto1[indice], listaTexto2[indice]) != 0)
                        return false;
                }
            }

            return true;
        }

        public List<EntidadNumero> OrdenarElementosSalida_CondicionTextosInformacion(CondicionTextosInformacion condiciones, List<EntidadNumero> lista)
        {
            if (lista.Any())
            {
                List<EntidadNumero> listaOrdenada = lista;
                int cantidadMaximaTextos = (from E in lista select E.Textos.Count).Max();

                foreach (var item in lista)
                    item.indiceOrdenamiento = -1;

                switch (condiciones.Tipo_OrdenamientoNumerosSalidas)
                {
                    case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                        if (condiciones.OrdenamientoSalidasAscendente)
                        {
                            listaOrdenada = lista.OrderBy(i => i.Nombre).ToList();
                        }
                        else if (condiciones.OrdenamientoSalidasDescendente)
                        {
                            listaOrdenada = lista.OrderByDescending(i => i.Nombre).ToList();
                        }

                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:

                        if (condiciones.OrdenamientoSalidasAscendente)
                        {

                            var listaOrdenamiento = lista.OrderBy(i =>
                        {
                            if (i.Textos.Any())
                            {
                                i.indiceOrdenamiento++;
                                return i.Textos[i.indiceOrdenamiento];
                            }
                            else
                                return null;
                        });

                            for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;
                                            return i.Textos[i.indiceOrdenamiento];
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoSalidasDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    i.indiceOrdenamiento++;
                                    return i.Textos[i.indiceOrdenamiento];
                                }
                                else
                                    return null;
                            });

                            for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;
                                            return i.Textos[i.indiceOrdenamiento];
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }

                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:

                        if (condiciones.OrdenamientoSalidasAscendente)
                        {
                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;
                                            return i.Textos[i.indiceOrdenamiento];
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoSalidasDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;
                                            return i.Textos[i.indiceOrdenamiento];
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }

                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas:

                        if (condiciones.OrdenamientoSalidasAscendente)
                        {
                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoSalidasDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }

                        break;
                }

                return listaOrdenada;
            }
            else
                return lista;
        }
        public List<EntidadNumero> OrdenarElementosSalida_CondicionTextosInformacion_Clasificadores(CondicionTextosInformacion condiciones, List<EntidadNumero> lista)
        {
            if (lista.Any())
            {
                List<EntidadNumero> listaOrdenada = lista;
                int cantidadMaximaTextos = (from E in lista select E.Textos.Count).Max();

                foreach (var item in lista)
                    item.indiceOrdenamiento = -1;

                switch (condiciones.Tipo_OrdenamientoNumerosClasificadores)
                {
                    case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                        if (condiciones.OrdenamientoClasificadoresAscendente)
                        {
                            listaOrdenada = lista.OrderBy(i => i.Nombre).ToList();
                        }
                        else if (condiciones.OrdenamientoClasificadoresDescendente)
                        {
                            listaOrdenada = lista.OrderByDescending(i => i.Nombre).ToList();
                        }

                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:

                        if (condiciones.OrdenamientoClasificadoresAscendente)
                        {

                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    i.indiceOrdenamiento++;
                                    return i.Textos[i.indiceOrdenamiento];
                                }
                                else
                                    return null;
                            });

                            for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;
                                            return i.Textos[i.indiceOrdenamiento];
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoClasificadoresDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    i.indiceOrdenamiento++;
                                    return i.Textos[i.indiceOrdenamiento];
                                }
                                else
                                    return null;
                            });

                            for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;
                                            return i.Textos[i.indiceOrdenamiento];
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }

                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:

                        if (condiciones.OrdenamientoClasificadoresAscendente)
                        {
                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;
                                            return i.Textos[i.indiceOrdenamiento];
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoClasificadoresDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;
                                            return i.Textos[i.indiceOrdenamiento];
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }

                        break;

                    case TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas:

                        if (condiciones.OrdenamientoClasificadoresAscendente)
                        {
                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoClasificadoresDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }

                        break;
                }

                return listaOrdenada;
            }
            else
                return lista;
        }
        public List<EntidadNumero> OrdenarElementosNumerosSalida_CondicionTextosInformacion(CondicionTextosInformacion condiciones, List<EntidadNumero> lista)
        {
            if (!lista.Any()) return lista;

            List<EntidadNumero> listaOrdenada = lista;
            int cantidadMaximaTextos = (from E in lista select E.Textos.Count).Max();

            foreach (var item in lista)
                item.indiceOrdenamiento = -1;

            switch (condiciones.Tipo_OrdenamientoNumerosSalidas)
            {
                case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                    if (condiciones.OrdenamientoSalidasAscendente)
                    {
                        listaOrdenada = lista.OrderBy(i => i.Nombre).ToList();
                    }
                    else if (condiciones.OrdenamientoSalidasDescendente)
                    {
                        listaOrdenada = lista.OrderByDescending(i => i.Nombre).ToList();
                    }

                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:

                    if (condiciones.OrdenamientoSalidasAscendente)
                    {

                        var listaOrdenamiento = lista.OrderBy(i =>
                        {
                            if (i.Textos.Any())
                            {
                                i.indiceOrdenamiento++;
                                return i.Textos[i.indiceOrdenamiento];
                            }
                            else
                                return null;
                        });

                        for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                        {
                            listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                    {
                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });
                        }

                        listaOrdenada = listaOrdenamiento.ToList();
                    }
                    else if (condiciones.OrdenamientoSalidasDescendente)
                    {
                        var listaOrdenamiento = lista.OrderByDescending(i =>
                        {
                            if (i.Textos.Any())
                            {
                                i.indiceOrdenamiento++;
                                return i.Textos[i.indiceOrdenamiento];
                            }
                            else
                                return null;
                        });

                        for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                        {
                            listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                    {
                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });
                        }

                        listaOrdenada = listaOrdenamiento.ToList();
                    }

                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:

                    if (condiciones.OrdenamientoSalidasAscendente)
                    {
                        var listaOrdenamiento = lista.OrderBy(i =>
                        {
                            return i.Nombre;
                        });

                        for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                        {
                            listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                    {
                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });
                        }

                        listaOrdenada = listaOrdenamiento.ToList();
                    }
                    else if (condiciones.OrdenamientoSalidasDescendente)
                    {
                        var listaOrdenamiento = lista.OrderByDescending(i =>
                        {
                            return i.Nombre;
                        });

                        for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                        {
                            listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                    {
                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });
                        }

                        listaOrdenada = listaOrdenamiento.ToList();
                    }

                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas:

                    if(condiciones.IncluirNombreElementoConTextos)
                    {
                        if (condiciones.OrdenamientoSalidasAscendente)
                        {
                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoSalidasDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                    }
                    else if (condiciones.IncluirSoloNombreElemento)
                    {
                        if (condiciones.OrdenamientoSalidasAscendente)
                        {
                            listaOrdenada = lista.OrderBy(i => i.Nombre).ToList();
                        }
                        else if (condiciones.OrdenamientoSalidasDescendente)
                        {
                            listaOrdenada = lista.OrderByDescending(i => i.Nombre).ToList();
                        }
                    }
                    else
                    {
                        if (condiciones.OrdenamientoSalidasAscendente)
                        {

                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    i.indiceOrdenamiento++;

                                    if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                        return i.Textos[i.indiceOrdenamiento];
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });

                            for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoSalidasDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    i.indiceOrdenamiento++;

                                    if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                        return i.Textos[i.indiceOrdenamiento];
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });

                            for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                    }

                    break;
            }

            return listaOrdenada;
        }
        public List<EntidadNumero> OrdenarElementosNumerosSalida_CondicionTextosInformacion_Clasificadores(CondicionTextosInformacion condiciones, List<EntidadNumero> lista)
        {
            if (!lista.Any()) return lista;

            List<EntidadNumero> listaOrdenada = lista;
            int cantidadMaximaTextos = (from E in lista select E.Textos.Count).Max();

            foreach (var item in lista)
                item.indiceOrdenamiento = -1;

            switch (condiciones.Tipo_OrdenamientoNumerosClasificadores)
            {
                case TipoOpcion_OrdenamientoNumerosSalidas.SoloNombreCantidad:
                    if (condiciones.OrdenamientoClasificadoresAscendente)
                    {
                        listaOrdenada = lista.OrderBy(i => i.Nombre).ToList();
                    }
                    else if (condiciones.OrdenamientoClasificadoresDescendente)
                    {
                        listaOrdenada = lista.OrderByDescending(i => i.Nombre).ToList();
                    }

                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SoloTextosInformacionCantidad:

                    if (condiciones.OrdenamientoClasificadoresAscendente)
                    {

                        var listaOrdenamiento = lista.OrderBy(i =>
                        {
                            if (i.Textos.Any())
                            {
                                i.indiceOrdenamiento++;
                                return i.Textos[i.indiceOrdenamiento];
                            }
                            else
                                return null;
                        });

                        for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                        {
                            listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                    {
                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });
                        }

                        listaOrdenada = listaOrdenamiento.ToList();
                    }
                    else if (condiciones.OrdenamientoClasificadoresDescendente)
                    {
                        var listaOrdenamiento = lista.OrderByDescending(i =>
                        {
                            if (i.Textos.Any())
                            {
                                i.indiceOrdenamiento++;
                                return i.Textos[i.indiceOrdenamiento];
                            }
                            else
                                return null;
                        });

                        for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                        {
                            listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                    {
                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });
                        }

                        listaOrdenada = listaOrdenamiento.ToList();
                    }

                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.PorNombreYTextosInformacion:

                    if (condiciones.OrdenamientoClasificadoresAscendente)
                    {
                        var listaOrdenamiento = lista.OrderBy(i =>
                        {
                            return i.Nombre;
                        });

                        for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                        {
                            listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                    {
                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });
                        }

                        listaOrdenada = listaOrdenamiento.ToList();
                    }
                    else if (condiciones.OrdenamientoClasificadoresDescendente)
                    {
                        var listaOrdenamiento = lista.OrderByDescending(i =>
                        {
                            return i.Nombre;
                        });

                        for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                        {
                            listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                    {
                                        i.indiceOrdenamiento++;
                                        return i.Textos[i.indiceOrdenamiento];
                                    }
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });
                        }

                        listaOrdenada = listaOrdenamiento.ToList();
                    }

                    break;

                case TipoOpcion_OrdenamientoNumerosSalidas.SegunCondicionesRelacionadas:

                    if (condiciones.IncluirNombreElementoConTextos)
                    {
                        if (condiciones.OrdenamientoClasificadoresAscendente)
                        {
                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if(condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoClasificadoresDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                return i.Nombre;
                            });

                            for (int indice = 1; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                    }
                    else if (condiciones.IncluirSoloNombreElemento)
                    {
                        if (condiciones.OrdenamientoClasificadoresAscendente)
                        {
                            listaOrdenada = lista.OrderBy(i => i.Nombre).ToList();
                        }
                        else if (condiciones.OrdenamientoClasificadoresDescendente)
                        {
                            listaOrdenada = lista.OrderByDescending(i => i.Nombre).ToList();
                        }
                    }
                    else
                    {
                        if (condiciones.OrdenamientoClasificadoresAscendente)
                        {

                            var listaOrdenamiento = lista.OrderBy(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    i.indiceOrdenamiento++;

                                    if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                        return i.Textos[i.indiceOrdenamiento];
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });

                            for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenBy(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                        else if (condiciones.OrdenamientoClasificadoresDescendente)
                        {
                            var listaOrdenamiento = lista.OrderByDescending(i =>
                            {
                                if (i.Textos.Any())
                                {
                                    i.indiceOrdenamiento++;

                                    if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                        return i.Textos[i.indiceOrdenamiento];
                                    else
                                        return null;
                                }
                                else
                                    return null;
                            });

                            for (int indice = 2; indice <= cantidadMaximaTextos; indice++)
                            {
                                listaOrdenamiento = listaOrdenamiento.ThenByDescending(i =>
                                {
                                    if (i.Textos.Any())
                                    {
                                        if (i.indiceOrdenamiento < i.Textos.Count - 1)
                                        {
                                            i.indiceOrdenamiento++;

                                            if (condiciones.TextosInformacionInvolucrados_SeleccionarOrdenar.Contains(i.Textos[i.indiceOrdenamiento]))
                                                return i.Textos[i.indiceOrdenamiento];
                                            else
                                                return null;
                                        }
                                        else
                                            return null;
                                    }
                                    else
                                        return null;
                                });
                            }

                            listaOrdenada = listaOrdenamiento.ToList();
                        }
                    }

                    break;
            }

            return listaOrdenada;
        }

        public void EstablecerTextosInformacion_SeleccionarOrdenar(ElementoEjecucionCalculo elemento, object objetoCantidad,
            List<string> textosInformacionElemento)
        {
            List<string> elementosAnteriores = new List<string>();

            //foreach (var elementoAnterior in elemento.ElementoDiseñoRelacionado.ElementosAnteriores)
            //{
            //    foreach (var itemEtapa in etapas)
            //    {
            //        foreach (var itemElemento in itemEtapa.Elementos)
            //        {
            //            if (itemElemento.ElementoDiseñoRelacionado == elementoAnterior)
            //            {
            //                var elementosEncontrados = itemElemento.TextoInformacionAnterior_SeleccionOrdenamiento.Where(item => item.ObjetoCantidad == objetoCantidad).ToList();

            //                if (elementosEncontrados != null)
            //                {
            //                    foreach (var elementoEncontrado in elementosEncontrados)
            //                        elementosAnteriores.Add(elementoEncontrado.TextoInformacion);
            //                }
            //            }
            //        }
            //    }
            //}

            var elementosEncontrados = elemento.TextoInformacionAnterior_SeleccionOrdenamiento.Where(item => item.ObjetoCantidad == objetoCantidad).ToList();

            if (elementosEncontrados != null)
            {
                foreach (var elementoEncontrado in elementosEncontrados)
                    elementosAnteriores.Add(elementoEncontrado.TextoInformacion);
            }


            //var elementosAAgregarTextoInformacion = (from E in elementosAnteriores
            //                                         where E.Tipo == TipoElementoEjecucion.OperacionAritmetica &&
            //                                         ((ElementoOperacionAritmeticaEjecucion)E).TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar
            //                                         select E).ToList();

            foreach (var itemElemento in elementosAnteriores)
            {
                //foreach (var listaTextos in textosInformacionElemento)
                //{
                if (!textosInformacionElemento.Contains(itemElemento))
                    textosInformacionElemento.Add(itemElemento);
                //}
            }
        }

        public void EstablecerTextosInformacion_SeleccionarOrdenar_Operacion(ElementoDiseñoOperacionAritmeticaEjecucion elemento, 
            ElementoOperacionAritmeticaEjecucion operacionContenedora, object objetoCantidad, List<string> textosInformacionElemento)
        {
            //List<string> elementosAnteriores_Operacion = new List<string>();
            
            //foreach (var elementoAnterior in elemento.ElementoDiseñoRelacionado.ElementosAnteriores)
            //{
            //    foreach (var itemEtapa in operacionContenedora.Etapas)
            //    {
            //        foreach (var itemElemento in itemEtapa.Elementos)
            //        {
            //            if (itemElemento.ElementoDiseñoRelacionado == elementoAnterior)
            //            {
            //                if (itemElemento.OperacionEjecucion != null)
            //                {
            //                    foreach (var itemEtapa_Operacion in etapas)
            //                    {
            //                        foreach (var itemElemento_Operacion in itemEtapa_Operacion.Elementos)
            //                        {
            //                            if (itemElemento_Operacion.ElementoDiseñoRelacionado == itemElemento.OperacionEjecucion.ElementoDiseñoRelacionado &&
            //                                itemElemento_Operacion.TextoInformacionAnterior_SeleccionOrdenamiento.TryGetValue(objetoCantidad, out textosInformacion))
            //                            {
            //                                elementosAnteriores_Operacion.Add(textosInformacion);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}


            ////var elementosAAgregarTextoInformacion_Operacion = (from E in elementosAnteriores_Operacion
            ////                                         where E.Tipo == TipoElementoEjecucion.OperacionAritmetica &&
            ////                                         ((ElementoOperacionAritmeticaEjecucion)E).TipoOperacion == TipoOperacionAritmeticaEjecucion.SeleccionarOrdenar
            ////                                         select E).ToList();

            //foreach (var itemElemento in elementosAnteriores_Operacion)
            //{
            //    foreach (var listaTextos in elemento.TextosInformacion_SeleccionOrdenamiento)
            //    {
            //        if (!listaTextos.Contains(itemElemento))
            //            listaTextos.Add(itemElemento);
            //    }
            //}

            List<string> elementosAnteriores = new List<string>();

            //foreach (var elementoAnterior in elemento.ElementoDiseñoRelacionado.ElementosAnteriores)
            //{
            //    foreach (var itemEtapa in operacionContenedora.Etapas)
            //    {
            //        foreach (var itemElemento in itemEtapa.Elementos)
            //        {
            //            if (itemElemento.ElementoDiseñoRelacionado == elementoAnterior)
            //            {
            //                var elementosEncontrados = itemElemento.TextoInformacionAnterior_SeleccionOrdenamiento.Where(item => item.ObjetoCantidad == objetoCantidad).ToList();

            //                if (elementosEncontrados != null)
            //                {
            //                    foreach (var elementoEncontrado in elementosEncontrados)
            //                        elementosAnteriores.Add(elementoEncontrado.TextoInformacion);
            //                }
            //            }
            //        }
            //    }
            //}

            var elementosEncontrados = elemento.TextoInformacionAnterior_SeleccionOrdenamiento.Where(item => item.ObjetoCantidad == objetoCantidad).ToList();

            if (elementosEncontrados != null)
            {
                foreach (var elementoEncontrado in elementosEncontrados)
                    elementosAnteriores.Add(elementoEncontrado.TextoInformacion);
            }


            //var elementosAAgregarTextoInformacion = (from E in elementosAnteriores
            //                                         where E.TipoElemento == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
            //                                         E.TipoElemento == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados
            //                                         select E).ToList();

            foreach (var itemElemento in elementosAnteriores)
            {
                //foreach (var listaTextos in elemento.TextosInformacion_SeleccionOrdenamiento)
                //{
                    if (!textosInformacionElemento.Contains(itemElemento))
                    textosInformacionElemento.Add(itemElemento);
                //}
            }
        }
        public void AgregarTextosInformacionEntrada_Definicion(ElementoConjuntoTextosEntradaEjecucion entrada, ElementoCalculoEjecucion itemCalculo)
        {
            var definicionesEntrada = (from DiseñoTextosInformacion D in Calculo.TextosInformacion.ElementosTextosInformacion
                                             where D.EntradaRelacionada == entrada.ElementoDiseñoRelacionado.EntradaRelacionada
                                             select D).ToList();

            foreach (var itemDefinicionEntrada in definicionesEntrada)
            {
                var definiciones = (from DiseñoTextosInformacion D in Calculo.TextosInformacion.ElementosTextosInformacion
                                    where itemDefinicionEntrada.ElementosPosteriores.Contains(D) & D.ElementosAnteriores.Contains(itemDefinicionEntrada)
                                    select D).ToList();

                foreach (var definicion in definiciones)
                {
                    List<AsignacionImplicacion_TextosInformacion> listaDefinicionesEntrada = new List<AsignacionImplicacion_TextosInformacion>();

                    List<AsignacionImplicacion_TextosInformacion> definicionTextosInformacionEntrada = (from D in definicion.Relaciones_TextosInformacion where D.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion.Any(item => item.EntradaRelacionada == entrada.ElementoDiseñoRelacionado.EntradaRelacionada)) 
                                                                                                        | D.VerificarEntradaEn_Condiciones(entrada.ElementoDiseñoRelacionado.EntradaRelacionada)
                                                                                                        //entrada.ElementoDiseñoRelacionado.EntradaRelacionada.BusquedasTextosInformacion.Any(i => i == D.BusquedaRelacionada | i.ConjuntoBusquedas.Any(j => j == D.BusquedaRelacionada)) 
                                                                                                        select D).ToList();

                    foreach (var itemDefinicion in definicionTextosInformacionEntrada)
                    {
                        foreach (var itemInstancia in itemDefinicion.InstanciasAsignacion)
                        {
                            //if (itemDefinicion.VerificarEntradaEn_Condiciones(entrada.ElementoDiseñoRelacionado.EntradaRelacionada))
                            //{
                                itemDefinicion.TextosCondicion.AddRange(entrada.FilasTextosInformacion.Select(item => new TextosCondicion_Entrada()
                                {
                                    EntradaRelacionada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada,
                                    TextosCondicion = GenerarTextosInformacion(item.TextosInformacion)
                                }));
                            //}
                        }
                    }

                    //listaDefinicionesEntrada.AddRange(definicion.Relaciones_TextosInformacion.Except(definicionTextosInformacionEntrada));
                    //definicion.Relaciones_TextosInformacion.Clear();
                    //definicion.Relaciones_TextosInformacion.AddRange(listaDefinicionesEntrada);
                    
                    foreach (var itemDefinicionOperacion in definicion.Definiciones_TextosInformacion)
                    {
                        List<AsignacionImplicacion_TextosInformacion> listaRelaciones_Operacion = new List<AsignacionImplicacion_TextosInformacion>();

                        List<AsignacionImplicacion_TextosInformacion> definicionTextosInformacionEntrada_itemDefinicion = (from D in itemDefinicionOperacion.Relaciones_TextosInformacion
                                                                                                                           where D.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion.Any(item => item.EntradaRelacionada == entrada.ElementoDiseñoRelacionado.EntradaRelacionada)
                                                                                                                                                                                               | D.VerificarEntradaEn_Condiciones(entrada.ElementoDiseñoRelacionado.EntradaRelacionada))
                                                                                                                           //entrada.ElementoDiseñoRelacionado.EntradaRelacionada.BusquedasTextosInformacion.Any(i => i == D.BusquedaRelacionada | i.ConjuntoBusquedas.Any(j => j == D.BusquedaRelacionada))
                                                                                                                           select D).ToList();

                        foreach (var itemDefinicion in definicionTextosInformacionEntrada_itemDefinicion)
                        {
                            foreach (var itemInstancia in itemDefinicion.InstanciasAsignacion)
                            {
                                //if (itemDefinicion.VerificarEntradaEn_Condiciones(entrada.ElementoDiseñoRelacionado.EntradaRelacionada))
                                //{
                                    itemDefinicion.TextosCondicion.AddRange(entrada.FilasTextosInformacion.Select(item => new TextosCondicion_Entrada()
                                    {
                                        EntradaRelacionada = entrada.ElementoDiseñoRelacionado.EntradaRelacionada,
                                        TextosCondicion = GenerarTextosInformacion(item.TextosInformacion)
                                    }));
                                //}
                            }
                        }

                        //listaRelaciones_Operacion.AddRange(itemDefinicionOperacion.Relaciones_TextosInformacion.Except(definicionTextosInformacionEntrada_itemDefinicion));
                        //itemDefinicionOperacion.Relaciones_TextosInformacion.Clear();
                        //itemDefinicionOperacion.Relaciones_TextosInformacion.AddRange(listaRelaciones_Operacion);
                    }
                }
            }
        }

        public void AgregarTextosInformacionEntrada_DefinicionListasCadenas(ElementoConjuntoTextosEntradaEjecucion entrada, ElementoCalculoEjecucion itemCalculo)
        {
            var definicionesEntrada = (from DiseñoTextosInformacion D in Calculo.TextosInformacion.ElementosTextosInformacion
                                       where D.GetType() == typeof(DiseñoListaCadenasTexto) &&
                                       D.ElementosAnteriores.Where(i => i.EntradaRelacionada != null)
                                       .Select(i => i.EntradaRelacionada).Any(i => i.ID == entrada.ElementoDiseñoRelacionado.EntradaRelacionada.ID)
                                       select (DiseñoListaCadenasTexto)D).ToList();

            foreach (var itemDefinicionEntrada in definicionesEntrada)
            {
                itemDefinicionEntrada.ListasCadenasTexto.AddRange(entrada.FilasTextosInformacion);                
            }
        }
        public List<AsignacionImplicacion_TextosInformacion> ObtenerDefinicionesTextosInformacion_Operacion(ElementoOperacionAritmeticaEjecucion operacion)
        {
            List<AsignacionImplicacion_TextosInformacion> Relaciones_TextosInformacion = new List<AsignacionImplicacion_TextosInformacion>();

            var definicionesSinRelaciones = (from DiseñoTextosInformacion D in Calculo.TextosInformacion.ElementosTextosInformacion
                                             where //D.ElementosAnteriores.Count == 0 & D.ElementosPosteriores.Count == 0 &
                                             D.OperacionRelacionada == operacion.ElementoDiseñoRelacionado select D).ToList();
            
            foreach (var definicion in definicionesSinRelaciones)
            {
                foreach(var item in definicion.Relaciones_TextosInformacion)
                {
                    item.DiseñoTextosInformacion_Calculo = Calculo.TextosInformacion;
                    item.DiseñoTextosInformacion_Relacionado = definicion;

                    if (item.Condiciones_TextoCondicion != null)
                        item.Condiciones_TextoCondicion.PrepararTextosBusquedas();
                }

                Relaciones_TextosInformacion.AddRange(definicion.Relaciones_TextosInformacion);
            }

            //var definicionesConRelaciones = (from DiseñoTextosInformacion D in Calculo.TextosInformacion.ElementosTextosInformacion
            //                                 where D.ElementosAnteriores.Count > 0 &
            //                                 D.OperacionRelacionada == operacion.ElementoDiseñoRelacionado
            //                                 select D).ToList();

            //foreach (var definicion in definicionesConRelaciones)
            //{
            //    //bool agregarDefiniciones = true;
            //    //RecorrerDefiniciones_VerificarRelacionesOperacion(definicion, operacion.ElementoDiseñoRelacionado,
            //    //    ref agregarDefiniciones);
                
            //    //if (agregarDefiniciones)
            //    Relaciones_TextosInformacion.AddRange(definicion.Relaciones_TextosInformacion);
            //}

            return Relaciones_TextosInformacion;
        }

        public List<AsignacionImplicacion_TextosInformacion> ObtenerDefinicionesTextosInformacion_ElementoOperacion(ElementoDiseñoOperacionAritmeticaEjecucion elemento)
        {
            List<AsignacionImplicacion_TextosInformacion> Relaciones_TextosInformacion = new List<AsignacionImplicacion_TextosInformacion>();

            var definiciones = (from DiseñoTextosInformacion D in Calculo.TextosInformacion.ElementosTextosInformacion
                                from DiseñoTextosInformacion D1 in D.Definiciones_TextosInformacion
                                             where D1.ElementoRelacionado == elemento.ElementoDiseñoRelacionado
                                             select D1).ToList();

            foreach (var definicion in definiciones)
            {
                foreach (var item in definicion.Relaciones_TextosInformacion)
                {
                    item.DiseñoTextosInformacion_Calculo = Calculo.TextosInformacion;
                    item.DiseñoTextosInformacion_Relacionado = definicion;

                    if(item.Condiciones_TextoCondicion != null)
                        item.Condiciones_TextoCondicion.PrepararTextosBusquedas();
                }

                Relaciones_TextosInformacion.AddRange(definicion.Relaciones_TextosInformacion);
            }

            return Relaciones_TextosInformacion;
        }

        private void EstablecerElementosSalida_SeleccionarOrdenar(EntidadNumero elemento,
            ElementoOperacionAritmeticaEjecucion operacion, CondicionTextosInformacion textosInformacion,
            ElementoOperacionAritmeticaEjecucion operando, List<EntidadNumero> ListaElementos,
            ElementoOperacionAritmeticaEjecucion operacionContenedora,
            int indiceElementoSalida = -1)
        {
            List<DiseñoOperacion> elementosSalida;
            List<DiseñoElementoOperacion> elementosInternosSalida = new List<DiseñoElementoOperacion>();

            if (textosInformacion != null)
            {
                if (indiceElementoSalida == -1)
                {
                    elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida
                                       where E.CondicionesAsociadas == textosInformacion & E.ElementoSalida_Operacion != null &&
                                       ((E.SiCondicionesCumplen && textosInformacion.ValorCondiciones) || (E.SiCondicionesNoCumplen && !textosInformacion.ValorCondiciones))
                                       select E.ElementoSalida_Operacion).ToList();

                    foreach(var itemInternoSalida in (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida
                                                      where E.CondicionesAsociadas == textosInformacion & E.ElementoSalida_Operacion != null &&
                                       ((E.SiCondicionesCumplen && textosInformacion.ValorCondiciones) || (E.SiCondicionesNoCumplen && !textosInformacion.ValorCondiciones))
                                                      select E.ElementosSalidas).ToList())
                    elementosInternosSalida.AddRange(itemInternoSalida);
                }
                else
                {
                    if ((from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida
                         where E.CondicionesAsociadas == textosInformacion
                         select E.ElementoSalida_Operacion).Any())
                    {
                        var elementos = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida
                                         where E.CondicionesAsociadas == textosInformacion & E.ElementoSalida_Operacion != null &&
               ((E.SiCondicionesCumplen && textosInformacion.ValorCondiciones) || (E.SiCondicionesNoCumplen && !textosInformacion.ValorCondiciones))
                                         select E.ElementoSalida_Operacion).ToList();

                        if (elementos.Any() &&
                            indiceElementoSalida < elementos.Count)
                        {
                            elementosSalida = new List<DiseñoOperacion>() { elementos.ElementAt(indiceElementoSalida) };
                        }
                        else
                        {
                            elementosSalida = new List<DiseñoOperacion>();
                        }

                        var subElementos = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_ElementosSalida
                                            where E.CondicionesAsociadas == textosInformacion & E.ElementoSalida_Operacion != null &&
                                ((E.SiCondicionesCumplen && textosInformacion.ValorCondiciones) || (E.SiCondicionesNoCumplen && !textosInformacion.ValorCondiciones))
                                            select E).ToList();

                        if (subElementos.Any() &&
                            indiceElementoSalida < subElementos.Count)
                        {
                            elementosInternosSalida = subElementos.ElementAt(indiceElementoSalida).ElementosSalidas;
                        }
                        else
                        {
                            elementosInternosSalida = new List<DiseñoElementoOperacion>();
                        }
                    }
                    else
                    {
                        elementosSalida = new List<DiseñoOperacion>();
                    }
                }

                if (!elementosSalida.Any() && (operando.ElementoDiseñoRelacionado.ModoUnir_SeleccionarOrdenar | 
                    operando.ElementoDiseñoRelacionado.ModoSeleccionManual_SeleccionarOrdenar))
                {
                    elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.ElementosPosteriores select E).ToList();
                }
            }
            else
            {
                elementosSalida = (from E in operacion.ElementoDiseñoRelacionado.ElementosPosteriores select E).ToList();
            }

            if (elementosSalida != null)
            {
                List<ElementoEjecucionCalculo> elementos = new List<ElementoEjecucionCalculo>();
                foreach (var itemEtapa in etapas)
                {
                    elementos.AddRange(itemEtapa.Elementos);
                }

                if (operacionContenedora != null)
                {
                    foreach (var itemEtapa in operacionContenedora.Etapas)
                    {
                        elementos.AddRange(itemEtapa.Elementos);
                    }
                }

                foreach (var elementoSalida in elementosSalida)
                {
                    var elementoEjecucionSalida = (from E in elementos where E.ElementoDiseñoRelacionado == elementoSalida select E).FirstOrDefault();

                    if (elementoEjecucionSalida != null)
                    {
                        var listaElementosSalida = from E in ListaElementos.Where(i => i.HashCode_NumeroAgregacion_Ejecucion == elemento.HashCode_NumeroAgregacion_Ejecucion) select E.ElementosSalidaOperacion_SeleccionarOrdenar;

                        bool encontrado = false;
                        foreach (var lista in listaElementosSalida)
                        {
                            if (lista.Contains(elementoEjecucionSalida))
                            {
                                encontrado = true;
                                break;
                            }
                        }

                        if (!encontrado)
                        {
                            if(!elemento.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(elementoEjecucionSalida))
                                elemento.ElementosSalidaOperacion_SeleccionarOrdenar.Add(elementoEjecucionSalida);
                            
                            foreach(var itemInternoSalida in 
                                elementosInternosSalida.Where(item => elementoEjecucionSalida.ElementoDiseñoRelacionado != null &&
                                elementoEjecucionSalida.ElementoDiseñoRelacionado.ElementosDiseñoOperacion.Contains(item)))
                            {
                                var elementoInternoEncontrado = ObtenerSubElementoEjecucion(itemInternoSalida);

                                if(elementoInternoEncontrado != null)
                                    elemento.ElementosInternosSalidaOperacion_SeleccionarOrdenar.Add(elementoInternoEncontrado);
                            }

                            if(indiceElementoSalida >= 0)
                            {
                                ListaElementos.Add(elemento);
                            }

                            if (elementoEjecucionSalida.Tipo == TipoElementoEjecucion.OperacionAritmetica)
                            {
                                ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionSalida).CantidadTextosInformacion_SeleccionarOrdenar = operacion.CantidadTextosInformacion_SeleccionarOrdenar;
                            }

                            if (textosInformacion != null)
                            {
                                foreach (var itemTexto in textosInformacion.TextosInformacionInvolucrados)
                                    elementoEjecucionSalida.TextoInformacionAnterior_SeleccionOrdenamiento.Add(new DuplaTextoInformacion_Cantidad_SeleccionarOrdenar { ObjetoCantidad = elemento, TextoInformacion = itemTexto });

                                if (elementoEjecucionSalida.Tipo == TipoElementoEjecucion.OperacionAritmetica &&
                                    elementoEjecucionSalida.GetType() == typeof(ElementoOperacionAritmeticaEjecucion) &&
                                    ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionSalida).TipoOperacion == TipoOperacionAritmeticaEjecucion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                                {
                                    foreach (var itemTexto in operacion.CondicionesTextosInformacion_SeleccionOrdenamiento)
                                    {
                                        if (!((ElementoOperacionAritmeticaEjecucion)elementoEjecucionSalida).CondicionesTextosInformacion_SeleccionOrdenamiento.Contains(itemTexto))
                                        {
                                            ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionSalida).CondicionesTextosInformacion_SeleccionOrdenamiento.Add(itemTexto);
                                            ((ElementoOperacionAritmeticaEjecucion)elementoEjecucionSalida).TextosInformacionInvolucrados_CondicionSeleccionarOrdenar.AddRange(GenerarTextosInformacion(textosInformacion.TextosInformacionInvolucrados));
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }
            }
        }

        Clasificador ultimoClasificadorAgregado;

        private void EstablecerClasificadores_SeleccionarOrdenar(EntidadNumero elemento,
            ElementoOperacionAritmeticaEjecucion operacion, CondicionTextosInformacion textosInformacion,
            ElementoOperacionAritmeticaEjecucion operando, string CadenaTextoClasificador,
            bool CrearClasificador, int indiceClasificador = -1)
        {
            List<Clasificador> clasificadores;
            
            if (textosInformacion != null)
            {
                if (indiceClasificador == -1)
                {
                    clasificadores = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_Clasificadores
                                       where E.CondicionesAsociadas == textosInformacion & E.ElementoClasificador != null &&
                                       ((E.SiCondicionesCumplen && elemento.ValorCondiciones_SeleccionarOrdenar) || (E.SiCondicionesNoCumplen && !elemento.ValorCondiciones_SeleccionarOrdenar))
                                       select E.ElementoClasificador).ToList();
                    
                }
                else
                {
                    if ((from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_Clasificadores
                         where E.CondicionesAsociadas == textosInformacion
                         select E.ElementoClasificador).Any())
                    {
                        var elementos = (from E in operacion.ElementoDiseñoRelacionado.AsociacionesTextosInformacion_Clasificadores
                                         where E.CondicionesAsociadas == textosInformacion & E.ElementoClasificador != null &&
               ((E.SiCondicionesCumplen && elemento.ValorCondiciones_SeleccionarOrdenar) || (E.SiCondicionesNoCumplen && !elemento.ValorCondiciones_SeleccionarOrdenar))
                                         select E.ElementoClasificador).ToList();

                        if (elementos.Any() &&
                            indiceClasificador < elementos.Count)
                        {
                            clasificadores = new List<Clasificador>() { elementos.ElementAt(indiceClasificador) };                            
                        }
                        else
                        {
                            clasificadores = new List<Clasificador>();
                        }

                    }
                    else
                    {
                        clasificadores = new List<Clasificador>();
                    }
                }

                if (!clasificadores.Any() && (operando.ElementoDiseñoRelacionado.ModoUnir_SeleccionarOrdenar |
                    operando.ElementoDiseñoRelacionado.ModoSeleccionManual_SeleccionarOrdenar))
                {
                    clasificadores = (from E in operacion.ElementoDiseñoRelacionado.Clasificadores select E).ToList();
                }
            }
            else
            {
                clasificadores = (from E in operacion.ElementoDiseñoRelacionado.Clasificadores select E).ToList();
            }

            if (clasificadores != null)
            {

                foreach (var elementoClasificador in clasificadores)
                {
                    
                    if(elementoClasificador.UtilizarCadenasTexto_DeCantidad)
                    {
                        if(indiceClasificador == -1)
                        {
                            CadenaTextoClasificador = ObtenerCadenasTexto_Clasificador(elementoClasificador.SeleccionCadenasTexto, CadenaTextoClasificador);
                        }

                        elementoClasificador.CadenaTexto = CadenaTextoClasificador;
                    }

                    Clasificador clasificador;

                    if (CrearClasificador || elementoClasificador.UtilizarCadenasTexto_DeCantidad)
                    {
                        if ((ultimoClasificadorAgregado == null) ||
                            (ultimoClasificadorAgregado.CadenaTexto != elementoClasificador.CadenaTexto))
                        {
                            clasificador = new Clasificador()
                            {
                                CadenaTexto = elementoClasificador.CadenaTexto.ToString(),
                                ID = App.GenerarID_Elemento(),
                                UtilizarCadenasTexto_DeCantidad = elementoClasificador.UtilizarCadenasTexto_DeCantidad
                            };

                            ultimoClasificadorAgregado = clasificador;
                        }
                        else
                            clasificador = ultimoClasificadorAgregado;
                    }
                    else
                    {
                        clasificador = elementoClasificador;
                    }

                    if (!elemento.Clasificadores_SeleccionarOrdenar.Contains(clasificador))
                        {
                            elemento.Clasificadores_SeleccionarOrdenar.Add(clasificador);

                        
                            if (!operacion.Clasificadores_Cantidades.Contains(clasificador))
                                operacion.Clasificadores_Cantidades.Add(clasificador);

                        }
                        
                }
            }
        }
        public string ObtenerCadenasTexto_Clasificador(List<OperacionCadenaTexto> SeleccionCadenasTexto, string CadenaTextoClasificador)
        {
            string[] separarCadenas = CadenaTextoClasificador.Split(":");
            List<string> cadenasTexto = new List<string>();

            separarCadenas = separarCadenas[1].Split("|");

            for (int indice = 0; indice < separarCadenas.LongLength; indice++)
            {
                var cadenaInsertar = separarCadenas[indice].Trim();
                cadenasTexto.Add(cadenaInsertar);
            }

            if (cadenasTexto.Any())
            {
                List<string> cadenasTextoSeleccionadas = new List<string>();
                                
                foreach(var seleccionCadena in SeleccionCadenasTexto)
                {
                    cadenasTextoSeleccionadas.AddRange(cadenasTexto.Where(i => cadenasTexto.IndexOf(i) == seleccionCadena.PosicionInicial).ToList());
                }

                return string.Join(" , ", cadenasTextoSeleccionadas);
            }
            else
                return string.Empty;
        }
        private string EstablecerDefinicionNombreCantidad(OpcionesNombreCantidad_TextosInformacion opciones, 
            int posicionActual, int posicionActualDefinicion, int posicionActualOperando, 
            ElementoOperacionAritmeticaEjecucion operacion,
            ElementoOperacionAritmeticaEjecucion elementoOperando = null,
            bool desdeImplicaciones = false)
        {
            int cantidad = 0;
            List<string> textos2 = new List<string>();
            List<string> textos = new List<string>();
            string Nombre = string.Empty;
            List<string> TextosNoIncluidos = new List<string>();
            List<string> TextosElementos = new List<string>();

            List<string> TextosRelacionados = new List<string>();
            ElementoOperacionAritmeticaEjecucion operacionContenedora = null;
            ElementoEjecucionCalculo elemento = null;

            List<EntidadNumero> Numeros = new List<EntidadNumero>();

            string textosImplica = string.Empty;
            bool operandoSeteado = false;
            
            if(desdeImplicaciones &&
                opciones.Operando == null)
            {
                opciones.Operando = elementoOperando.ElementoDiseñoRelacionado;
                operandoSeteado = true;
            }

            if (opciones.Operando != null && opciones.OperandoSubElemento == null)
            {
                elemento = ObtenerElementoEjecucion(opciones.Operando);
                
                if (elemento != null)
                {
                    if (elemento.Textos.Any())
                    {
                        TextosRelacionados.AddRange(GenerarTextosInformacion(elemento.Textos));
                    }

                    if (elemento.Tipo == TipoElementoEjecucion.Entrada)
                    {
                        TextosRelacionados.AddRange(((ElementoEntradaEjecucion)elemento).TextosInformacionFijos);

                    }

                    Numeros.AddRange((elemento.NumerosFiltrados_Condiciones.Any() ?
                                                        elemento.NumerosFiltrados_Condiciones : ((ElementoOperacionAritmeticaEjecucion)elemento).Numeros.Where(i =>
                                                        i.Clasificadores_SeleccionarOrdenar.Any(i => i == elemento.Clasificadores_Cantidades[elemento.IndicePosicionClasificadores]) && (
                        //(!i.ElementosSalidaOperacion_Agrupamiento.Any() || (i.ElementosSalidaOperacion_Agrupamiento.Any() &
                        //i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionContenedora))) &
                        (!i.ElementosSalidaOperacion_CondicionFlujo.Any() || (i.ElementosSalidaOperacion_CondicionFlujo.Any() &
                        i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                        (!i.ElementosSalidaOperacion_SeleccionarOrdenar.Any() || (i.ElementosSalidaOperacion_SeleccionarOrdenar.Any() &
                        i.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion))))).ToList()));

                    TextosRelacionados = TextosRelacionados.Where(i => !string.IsNullOrEmpty(i)).ToList();

                }
            }

            if (opciones.Operando != null && opciones.OperandoSubElemento != null)
            {
                elemento = ObtenerSubElementoEjecucion(opciones.OperandoSubElemento);

                if (elemento != null)
                {
                    EntidadNumero elementoNumero = ((ElementoDiseñoOperacionAritmeticaEjecucion)elemento).Numeros[posicionActual];

                    if (elemento.Textos.Any())
                    {
                        TextosRelacionados.AddRange(GenerarTextosInformacion(elemento.Textos));
                    }

                    Numeros.AddRange((elemento.NumerosFiltrados_Condiciones.Any() ?
                                                    elemento.NumerosFiltrados_Condiciones : ((ElementoDiseñoOperacionAritmeticaEjecucion)elemento).Numeros.Where(i =>
                                                    i.Clasificadores_SeleccionarOrdenar.Any(i => i == elemento.Clasificadores_Cantidades[elemento.IndicePosicionClasificadores]) && (
                    //(!i.ElementosSalidaOperacion_Agrupamiento.Any() || (i.ElementosSalidaOperacion_Agrupamiento.Any() &
                    //i.ElementosSalidaOperacion_Agrupamiento.Contains(operacionContenedora))) &
                    (!i.ElementosSalidaOperacion_CondicionFlujo.Any() || (i.ElementosSalidaOperacion_CondicionFlujo.Any() &
                    i.ElementosSalidaOperacion_CondicionFlujo.Contains(operacion))) &
                    (!i.ElementosSalidaOperacion_SeleccionarOrdenar.Any() || (i.ElementosSalidaOperacion_SeleccionarOrdenar.Any() &
                    i.ElementosSalidaOperacion_SeleccionarOrdenar.Contains(operacion))))).ToList()));
                    
                    TextosRelacionados = TextosRelacionados.Where(i => !string.IsNullOrEmpty(i)).ToList();

                }
            }
            
            if (opciones.Operando == null && opciones.OperandoSubElemento == null)
            {                
                operacionContenedora = operacion;

                if (operacionContenedora.Textos.Any())
                {
                    TextosRelacionados.AddRange(GenerarTextosInformacion(operacionContenedora.Textos));
                }                
            }

            TextosRelacionados = TextosRelacionados.Where(i => !string.IsNullOrEmpty(i)).ToList();

            switch (opciones.TipoOpcion)
            {
                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacion:
                    Nombre = (opciones.IncluirComillas ? "'" : string.Empty) + TextosRelacionados.FirstOrDefault() + (opciones.IncluirComillas ? "'" : string.Empty);

                    if (opciones.IncluirTextosImplica)
                    {
                        foreach (var itemTexto in TextosRelacionados)
                        {
                            if (itemTexto != TextosRelacionados.FirstOrDefault())
                            {
                                if (itemTexto != TextosRelacionados.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacion:
                    cantidad = 0;

                    foreach (var itemTexto in TextosRelacionados)
                    {
                        cantidad++;
                        if (cantidad <= opciones.NPrimerosTextosInformacion)
                        {
                            if (cantidad < opciones.NPrimerosTextosInformacion)
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                            else
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                        }
                        else
                        {
                            if (opciones.IncluirTextosImplica)
                            {
                                if (itemTexto != TextosRelacionados.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacion:
                    textos2 = TextosRelacionados.ToList();
                    textos2.Reverse();

                    Nombre = (opciones.IncluirComillas ? "'" : string.Empty) + textos2.FirstOrDefault() + (opciones.IncluirComillas ? "'" : string.Empty);

                    if (opciones.IncluirTextosImplica)
                    {
                        foreach (var itemTexto in textos2)
                        {
                            if (itemTexto != textos2.FirstOrDefault())
                            {
                                if (itemTexto != textos2.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacion:
                    cantidad = 0;
                    textos = TextosRelacionados.ToList();
                    textos.Reverse();

                    foreach (var itemTexto in textos)
                    {
                        cantidad++;
                        if (cantidad <= opciones.NUltimosTextosInformacion)
                        {
                            if (cantidad < opciones.NUltimosTextosInformacion)
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                            else
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                        }
                        else
                        {
                            if (opciones.IncluirTextosImplica)
                            {
                                if (itemTexto != textos.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.Todos:

                    foreach (var itemTexto in TextosRelacionados)
                    {
                        if (itemTexto != TextosRelacionados.LastOrDefault())
                            Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                        else
                            Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                    }

                    break;
            }

            switch (opciones.TipoOpcion)
            {
                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondiciones:
                    opciones.Condiciones.ClasificadorActual = itemClasificador;

                    opciones.Condiciones.TextosInformacionCumplenCondicion.Clear();
                    opciones.Condiciones.EvaluarCondiciones(this, operacion, (ElementoOperacionAritmeticaEjecucion)elemento, null);

                    TextosNoIncluidos = new List<string>();
                    foreach (var itemTexto in elemento.Textos)
                    {
                        if (!opciones.Condiciones.TextosInformacionCumplenCondicion.Contains(itemTexto))
                        {
                            TextosNoIncluidos.Add(itemTexto);
                        }
                    }

                    TextosNoIncluidos = TextosNoIncluidos.Where(i => !string.IsNullOrEmpty(i)).ToList();

                    foreach (var itemTexto in elemento.Textos)
                    {
                        if (opciones.Condiciones.TextosInformacionCumplenCondicion.Contains(itemTexto))
                        {
                            if (itemTexto != opciones.Condiciones.TextosInformacionCumplenCondicion.LastOrDefault())
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                            else
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                        }
                        else
                        {
                            if (opciones.IncluirTextosImplica)
                            {
                                if (itemTexto != TextosNoIncluidos.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosiciones:
                    string[] posiciones = opciones.PosicionesTextosInformacion.Split(',');

                    foreach (var itemPosicion in posiciones)
                    {
                        int intPosicion = 0;
                        if (int.TryParse(itemPosicion, out intPosicion))
                        {
                            int posicion = 1;
                            foreach (var itemTexto in TextosRelacionados)
                            {
                                if (intPosicion == posicion)
                                {
                                    if (itemTexto != TextosRelacionados.LastOrDefault())
                                        Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                    else
                                        Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                                }

                                posicion++;
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreElemento:
                    if (!string.IsNullOrEmpty(elemento.Nombre))
                        Nombre += elemento.Nombre;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreNumeroElemento:

                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {
                        if (!string.IsNullOrEmpty(Numeros[posicionActual - 1].Nombre))
                            Nombre += Numeros[posicionActual - 1].Nombre;
                    }

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadNumeroElemento:

                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {
                        Nombre += Numeros[posicionActual - 1].Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString());
                    }

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TodosTextosInformacionNumero:

                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {
                        //if (!string.IsNullOrEmpty(Numeros[posicionActual - 1].Nombre))
                        //    Nombre += Numeros[posicionActual - 1].Nombre;

                        foreach (var itemTexto in Numeros[posicionActual - 1].Textos)
                        {
                            if (itemTexto != Numeros[posicionActual - 1].Textos.LastOrDefault())
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                            else
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                        }
                    }


                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacionNumero:

                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {

                        Nombre = (opciones.IncluirComillas ? "'" : string.Empty) + Numeros[posicionActual - 1].Textos.FirstOrDefault() + (opciones.IncluirComillas ? "'" : string.Empty);

                        if (opciones.IncluirTextosImplica)
                        {
                            foreach (var itemTexto in Numeros[posicionActual - 1].Textos)
                            {
                                if (itemTexto != Numeros[posicionActual - 1].Textos.FirstOrDefault())
                                {
                                    if (itemTexto != Numeros[posicionActual - 1].Textos.LastOrDefault())
                                        textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                    else
                                        textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                                }
                            }
                        }
                    }


                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacionNumero:

                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {
                        cantidad = 0;

                        foreach (var itemTexto in Numeros[posicionActual - 1].Textos)
                        {
                            cantidad++;
                            if (cantidad <= opciones.NPrimerosTextosInformacionNumero)
                            {
                                if (cantidad < opciones.NPrimerosTextosInformacionNumero)
                                    Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                            else
                            {
                                if (opciones.IncluirTextosImplica)
                                {
                                    if (itemTexto != Numeros[posicionActual - 1].Textos.LastOrDefault())
                                        textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                    else
                                        textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                                }
                            }
                        }
                    }


                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacionNumero:

                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {

                        textos2 = Numeros[posicionActual - 1].Textos.ToList();
                        textos2.Reverse();

                        Nombre = (opciones.IncluirComillas ? "'" : string.Empty) + textos2.FirstOrDefault() + (opciones.IncluirComillas ? "'" : string.Empty);

                        if (opciones.IncluirTextosImplica)
                        {
                            foreach (var itemTexto in textos2)
                            {
                                if (itemTexto != textos2.FirstOrDefault())
                                {
                                    if (itemTexto != textos2.LastOrDefault())
                                        textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                    else
                                        textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                                }
                            }
                        }
                    }


                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacionNumero:

                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {
                        cantidad = 0;
                        textos = Numeros[posicionActual - 1].Textos.ToList();
                        textos.Reverse();

                        foreach (var itemTexto in textos)
                        {
                            cantidad++;
                            if (cantidad <= opciones.NUltimosTextosInformacionNumero)
                            {
                                if (cantidad < opciones.NUltimosTextosInformacionNumero)
                                    Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                            else
                            {
                                if (opciones.IncluirTextosImplica)
                                {
                                    if (itemTexto != textos.LastOrDefault())
                                        textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                    else
                                        textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                                }
                            }
                        }
                    }


                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondicionesNumero:
                    TextosRelacionados.Clear();
                    TextosNoIncluidos = new List<string>();
                    TextosElementos = new List<string>();


                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {
                        opciones.CondicionesTextoNumero.ClasificadorActual = itemClasificador;

                        opciones.CondicionesTextoNumero.TextosInformacionCumplenCondicion.Clear();
                        opciones.CondicionesTextoNumero.EvaluarCondiciones(this, operacionContenedora, null, Numeros[posicionActual - 1]);
                        TextosRelacionados.AddRange(GenerarTextosInformacion(opciones.CondicionesTextoNumero.TextosInformacionCumplenCondicion));
                        TextosElementos.AddRange(Numeros[posicionActual - 1].Textos);

                    }

                    foreach (var itemTexto in TextosElementos)
                    {
                        if (!TextosRelacionados.Contains(itemTexto))
                        {
                            TextosNoIncluidos.Add(itemTexto);
                        }
                    }

                    TextosNoIncluidos = TextosNoIncluidos.Where(i => !string.IsNullOrEmpty(i)).ToList();

                    foreach (var itemTexto in TextosElementos)
                    {
                        if (TextosRelacionados.Contains(itemTexto))
                        {
                            if (itemTexto != TextosRelacionados.LastOrDefault())
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                            else
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                        }
                        else
                        {
                            if (opciones.IncluirTextosImplica)
                            {
                                if (itemTexto != TextosNoIncluidos.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosicionesNumero:
                    TextosRelacionados.Clear();

                    if (posicionActual > 0 && posicionActual <= Numeros.Count &&
                    Numeros.Any())
                    {
                        TextosRelacionados.AddRange(Numeros[posicionActual - 1].Textos);
                    }

                    posiciones = opciones.PosicionesTextosInformacionNumero.Split(',');

                    foreach (var itemPosicion in posiciones)
                    {
                        int intPosicion = 0;
                        if (int.TryParse(itemPosicion, out intPosicion))
                        {
                            int posicion = 1;
                            foreach (var itemTexto in TextosRelacionados)
                            {
                                if (intPosicion == posicion)
                                {
                                    if (itemTexto != TextosRelacionados.LastOrDefault())
                                        Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                    else
                                        Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                                }

                                posicion++;
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreNumerosFiltrados:

                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:

                            foreach (var itemNumero in Numeros)
                            {
                                if (!string.IsNullOrEmpty(itemNumero.Nombre))
                                {
                                    if (itemNumero == Numeros.Last())
                                        Nombre += itemNumero.Nombre;
                                    else
                                        Nombre += itemNumero.Nombre + ", ";
                                }
                            }

                            break;
                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:

                            foreach (var itemNumero in Numeros)
                            {
                                if (!string.IsNullOrEmpty(itemNumero.Nombre))
                                {
                                    opciones.CondicionesFiltroNumeros.ClasificadorActual = itemClasificador;

                                    if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                    {
                                        if (itemNumero == Numeros.Last())
                                            Nombre += itemNumero.Nombre;
                                        else
                                            Nombre += itemNumero.Nombre + ", ";
                                    }
                                }
                            }

                            break;
                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            string[] posicionesNumero = opciones.PosicionesTextosInformacionNumero_Elemento.Split(',');

                            foreach (var itemPosicion in posicionesNumero)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;

                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                        {
                                            if (!string.IsNullOrEmpty(itemNumero.Nombre))
                                            {
                                                if (intPosicion == Numeros.Count)
                                                    Nombre += itemNumero.Nombre;
                                                else
                                                    Nombre += itemNumero.Nombre + ", ";
                                            }
                                        }
                                        posicion++;
                                    }

                                }
                            }
                            break;
                    }

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadNumerosFiltrados:

                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:

                            foreach (var itemNumero in Numeros)
                            {
                                if (itemNumero == Numeros.Last())
                                    Nombre += itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString());
                                else
                                    Nombre += itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ", ";
                            }

                            break;
                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:

                            foreach (var itemNumero in Numeros)
                            {
                                opciones.CondicionesFiltroNumeros.ClasificadorActual = itemClasificador;

                                if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                {
                                    if (itemNumero == Numeros.Last())
                                        Nombre += itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString());
                                    else
                                        Nombre += itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ", ";
                                }
                            }

                            break;
                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            string[] posicionesNumero = opciones.PosicionesTextosInformacionNumero_Elemento.Split(',');

                            foreach (var itemPosicion in posicionesNumero)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;

                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                        {
                                            if (intPosicion == Numeros.Count)
                                                Nombre += itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString());
                                            else
                                                Nombre += itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + ", ";
                                        }
                                        posicion++;
                                    }

                                }
                            }
                            break;
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TodosTextosInformacionNumerosFiltrados:

                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }
                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                opciones.CondicionesFiltroNumeros.ClasificadorActual = itemClasificador;

                                if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                    TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }

                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            TextosRelacionados.Clear();
                            posiciones = opciones.PosicionesTextosInformacionNumerosFiltrados.Split(',');

                            foreach (var itemPosicion in posiciones)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;
                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                            TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));

                                        posicion++;
                                    }
                                }
                            }

                            break;

                    }

                    foreach (var itemTexto in TextosRelacionados)
                    {
                        if (itemTexto != TextosRelacionados.LastOrDefault())
                            Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                        else
                            Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                    }

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacionNumerosFiltrados:
                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }

                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                opciones.CondicionesFiltroNumeros.ClasificadorActual = itemClasificador;

                                if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                    TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }
                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            TextosRelacionados.Clear();
                            posiciones = opciones.PosicionesTextosInformacionNumerosFiltrados.Split(',');

                            foreach (var itemPosicion in posiciones)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;
                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                            TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));

                                        posicion++;
                                    }
                                }
                            }


                            break;

                    }

                    Nombre = (opciones.IncluirComillas ? "'" : string.Empty) + TextosRelacionados.FirstOrDefault() + (opciones.IncluirComillas ? "'" : string.Empty);

                    if (opciones.IncluirTextosImplica)
                    {
                        foreach (var itemTexto in TextosRelacionados)
                        {
                            if (itemTexto != TextosRelacionados.FirstOrDefault())
                            {
                                if (itemTexto != TextosRelacionados.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacionNumerosFiltrados:
                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }
                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                opciones.CondicionesFiltroNumeros.ClasificadorActual = itemClasificador;

                                if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                    TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }


                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            TextosRelacionados.Clear();
                            posiciones = opciones.PosicionesTextosInformacionNumerosFiltrados.Split(',');

                            foreach (var itemPosicion in posiciones)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;
                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                            TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));

                                        posicion++;
                                    }
                                }
                            }


                            break;

                    }

                    cantidad = 0;

                    foreach (var itemTexto in TextosRelacionados)
                    {
                        cantidad++;
                        if (cantidad <= opciones.NPrimerosTextosInformacion)
                        {
                            if (cantidad < opciones.NPrimerosTextosInformacion)
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                            else
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                        }
                        else
                        {
                            if (opciones.IncluirTextosImplica)
                            {
                                if (itemTexto != TextosRelacionados.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacionNumerosFiltrados:
                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }
                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                opciones.CondicionesFiltroNumeros.ClasificadorActual = itemClasificador;

                                if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                    TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }

                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            TextosRelacionados.Clear();
                            posiciones = opciones.PosicionesTextosInformacionNumerosFiltrados.Split(',');

                            foreach (var itemPosicion in posiciones)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;
                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                            TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));

                                        posicion++;
                                    }
                                }
                            }


                            break;

                    }

                    textos2 = TextosRelacionados.ToList();
                    textos2.Reverse();

                    Nombre = (opciones.IncluirComillas ? "'" : string.Empty) + textos2.FirstOrDefault() + (opciones.IncluirComillas ? "'" : string.Empty);

                    if (opciones.IncluirTextosImplica)
                    {
                        foreach (var itemTexto in textos2)
                        {
                            if (itemTexto != textos2.FirstOrDefault())
                            {
                                if (itemTexto != textos2.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacionNumerosFiltrados:
                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }

                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                opciones.CondicionesFiltroNumeros.ClasificadorActual = itemClasificador;

                                if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                    TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }


                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            TextosRelacionados.Clear();
                            posiciones = opciones.PosicionesTextosInformacionNumerosFiltrados.Split(',');

                            foreach (var itemPosicion in posiciones)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;
                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                            TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));

                                        posicion++;
                                    }
                                }
                            }

                            break;

                    }

                    cantidad = 0;
                    textos = TextosRelacionados.ToList();
                    textos.Reverse();

                    foreach (var itemTexto in textos)
                    {
                        cantidad++;
                        if (cantidad <= opciones.NUltimosTextosInformacion)
                        {
                            if (cantidad < opciones.NUltimosTextosInformacion)
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                            else
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                        }
                        else
                        {
                            if (opciones.IncluirTextosImplica)
                            {
                                if (itemTexto != textos.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondicionesNumerosFiltrados:
                    TextosRelacionados.Clear();
                    TextosNoIncluidos = new List<string>();
                    TextosElementos = new List<string>();

                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                opciones.CondicionesTexto.ClasificadorActual = itemClasificador;
                                //TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                                opciones.CondicionesTexto.TextosInformacionCumplenCondicion.Clear();
                                opciones.CondicionesTexto.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero);
                                TextosRelacionados.AddRange(GenerarTextosInformacion(opciones.CondicionesTexto.TextosInformacionCumplenCondicion));
                                TextosElementos.AddRange(itemNumero.Textos);
                            }

                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                {
                                    opciones.CondicionesTexto.ClasificadorActual = itemClasificador;
                                    //TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                                    opciones.CondicionesTexto.TextosInformacionCumplenCondicion.Clear();
                                    opciones.CondicionesTexto.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero);
                                    TextosRelacionados.AddRange(GenerarTextosInformacion(opciones.CondicionesTexto.TextosInformacionCumplenCondicion));
                                    TextosElementos.AddRange(itemNumero.Textos);
                                }
                            }


                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            TextosRelacionados.Clear();
                            posiciones = opciones.PosicionesTextosInformacionNumerosFiltrados.Split(',');

                            foreach (var itemPosicion in posiciones)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;
                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                        {
                                            opciones.CondicionesTexto.ClasificadorActual = itemClasificador;
                                            //TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                                            opciones.CondicionesTexto.TextosInformacionCumplenCondicion.Clear();
                                            opciones.CondicionesTexto.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero);
                                            TextosRelacionados.AddRange(GenerarTextosInformacion(opciones.CondicionesTexto.TextosInformacionCumplenCondicion));
                                            TextosElementos.AddRange(itemNumero.Textos);
                                        }

                                        posicion++;
                                    }
                                }
                            }


                            break;

                    }

                    foreach (var itemTexto in TextosElementos)
                    {
                        if (!TextosRelacionados.Contains(itemTexto))
                        {
                            TextosNoIncluidos.Add(itemTexto);
                        }
                    }

                    TextosNoIncluidos = TextosNoIncluidos.Where(i => !string.IsNullOrEmpty(i)).ToList();

                    foreach (var itemTexto in TextosElementos)
                    {
                        if (TextosRelacionados.Contains(itemTexto))
                        {
                            if (itemTexto != TextosRelacionados.LastOrDefault())
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                            else
                                Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                        }
                        else
                        {
                            if (opciones.IncluirTextosImplica)
                            {
                                if (itemTexto != TextosNoIncluidos.LastOrDefault())
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                else
                                    textosImplica += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                            }
                        }
                    }
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosicionesNumerosFiltrados:
                    switch (opciones.TipoOpcionFiltroNumeros)
                    {
                        case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }


                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:
                            TextosRelacionados.Clear();

                            foreach (var itemNumero in Numeros)
                            {
                                opciones.CondicionesFiltroNumeros.ClasificadorActual = itemClasificador;

                                if (opciones.CondicionesFiltroNumeros.EvaluarCondiciones(this, operacionContenedora, (ElementoOperacionAritmeticaEjecucion)elemento, itemNumero))
                                    TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));
                            }


                            break;

                        case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                            TextosRelacionados.Clear();
                            posiciones = opciones.PosicionesTextosInformacionNumerosFiltrados.Split(',');

                            foreach (var itemPosicion in posiciones)
                            {
                                int intPosicion = 0;
                                if (int.TryParse(itemPosicion, out intPosicion))
                                {
                                    int posicion = 1;
                                    foreach (var itemNumero in Numeros)
                                    {
                                        if (intPosicion == posicion)
                                            TextosRelacionados.AddRange(GenerarTextosInformacion(itemNumero.Textos));

                                        posicion++;
                                    }
                                }
                            }


                            break;

                    }

                    posiciones = opciones.PosicionesTextosInformacionNumerosFiltrados.Split(',');

                    foreach (var itemPosicion in posiciones)
                    {
                        int intPosicion = 0;
                        if (int.TryParse(itemPosicion, out intPosicion))
                        {
                            int posicion = 1;
                            foreach (var itemTexto in TextosRelacionados)
                            {
                                if (intPosicion == posicion)
                                {
                                    if (itemTexto != TextosRelacionados.LastOrDefault())
                                        Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "', " : ", ");
                                    else
                                        Nombre += (opciones.IncluirComillas ? "'" : string.Empty) + itemTexto + (opciones.IncluirComillas ? "'" : string.Empty);
                                }

                                posicion++;
                            }
                        }
                    }

                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreOperacion:
                    if (operacionContenedora != null &&
                        !string.IsNullOrEmpty(operacionContenedora.Nombre))
                        Nombre += operacionContenedora.Nombre;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadOperacion:
                    if (operacionContenedora != null)
                        Nombre += string.Join(" , ", operacionContenedora.Numeros.Select(i => i.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString())));
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicion:
                    Nombre += (posicionActual - 1).ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicionDefinicion:
                    Nombre += (posicionActualDefinicion - 1).ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicionOperando:
                    Nombre += (posicionActualOperando - 1).ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextosInformacionFijos:
                    if (!string.IsNullOrEmpty(opciones.TextoInformacionFijo))
                        Nombre += opciones.TextoInformacionFijo;
                    break;
            }

            if (!string.IsNullOrEmpty(textosImplica))
                Nombre += " que implica " + textosImplica;

            if (operandoSeteado)
            {
                opciones.Operando = null;
            }

            return Nombre;
        }
        public void EstablecerNombreCantidad(EntidadNumero numero, DefinicionTextoNombresCantidades opciones,
            int posicionActual, ElementoOperacionAritmeticaEjecucion operacion, 
            ElementoOperacionAritmeticaEjecucion elementoOperando = null)
        {
            if (opciones != null)
            {
                int posicionOperando = 0;
                if (elementoOperando != null &&
                    operacion != null)
                    posicionOperando = operacion.ElementosOperacion.IndexOf(elementoOperando);

                //elemento.Nombre = string.Empty;
                string nombreCadena = string.Empty;
                foreach (var itemDefinicionOpciones in opciones.OpcionesTextos)
                {
                    string cadena = EstablecerDefinicionNombreCantidad(itemDefinicionOpciones, posicionActual + 1, 
                        opciones.PosicionActualDefinicion + 1, posicionOperando + 1, operacion, elementoOperando);

                    if (!string.IsNullOrEmpty(cadena) &&
                        cadena != "''")
                        nombreCadena += cadena + " ";
                }

                if (!string.IsNullOrEmpty(nombreCadena.Trim()))
                {
                    if (opciones.DefinirNombresNumeros_Elemento)
                        numero.Nombre = nombreCadena;

                    if (opciones.DefinirNombres_Elemento)
                        operacion.Nombre = nombreCadena;

                    opciones.PosicionActualDefinicion++;
                }
            }
        }

        public void ProcesarTextosInformacionElemento(List<string> TextosInformacionElemento,
            List<string> TextosInformacionElementoDesde, CondicionProcesamientoTextosInformacion itemCondiciones)
        {
            if ((itemCondiciones.CondicionesTextosInformacion != null &&
                itemCondiciones.CondicionesTextosInformacion.Valor_Condicion) ||
                itemCondiciones.CondicionesTextosInformacion == null)
            {
                List<string> TextosInformacionCondiciones = itemCondiciones.CondicionesTextosInformacion != null ? itemCondiciones.CondicionesTextosInformacion.TextosInvolucrados : new List<string>();
                List<string> TextosInvolucrados = new List<string>();

                if (((!itemCondiciones.ProcesarCadenasTextos_SinCumplirCondiciones_Textos && TextosInformacionCondiciones.Any()) &&
                    itemCondiciones.TipoUbicacionAccion_Insertar != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.ValorFijo))
                {
                    TextosInvolucrados = TextosInformacionElementoDesde.Intersect(TextosInformacionCondiciones).ToList();
                }
                else
                {
                    if (itemCondiciones.TipoUbicacionAccion_Insertar != TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.ValorFijo)
                    {
                        if (itemCondiciones.ProcesarCadenasTextos_SinCumplirCondiciones_Textos)
                            TextosInvolucrados = TextosInformacionElementoDesde.ToList();
                    }
                    else
                        TextosInvolucrados = new List<string>() { itemCondiciones.ValorFijo_Insercion };                    
                }

                switch (itemCondiciones.Tipo)
                {
                    case TipoOpcionCondicionProcesamientoTextosInformacion.InsertarTextosExistentes:
                        TextosInformacionElemento.AddRange(TextosInvolucrados);
                        break;

                    case TipoOpcionCondicionProcesamientoTextosInformacion.QuitarTextos:
                        List<string> TextosAQuitar = new List<string>();

                        foreach (var item in TextosInformacionElemento)
                        {
                            foreach (var itemCondicion in TextosInvolucrados)
                            {
                                if ((new string(item.ToArray())).Equals(new string(itemCondicion.ToArray())))
                                    TextosAQuitar.Add(item);
                            }
                        }

                        while (TextosAQuitar.Any())
                        {
                            TextosInformacionElemento.Remove(TextosAQuitar.FirstOrDefault());
                            TextosAQuitar.Remove(TextosAQuitar.FirstOrDefault());
                        }

                        break;

                    case TipoOpcionCondicionProcesamientoTextosInformacion.EditarTextos:

                        TextosInformacionElemento.Clear();
                        TextosInformacionElemento.AddRange(TextosInvolucrados);

                        break;
                }
            }
        }        

        public List<string> SeleccionarCantidadProcesamiento_TextosInformacion(ElementoOperacionAritmeticaEjecucion itemOperacion,
            TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades Ubicacion, CondicionProcesamientoTextosInformacion itemCondiciones
            , Clasificador itemClasificador)
        {

            var ListaNumerosFiltrada = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto) &&
                    ElementoOperacionAritmeticaEjecucion.FiltrarNumeros(i, itemOperacion, itemOperacion.NumerosFiltrados)).ToList();

            if(ListaNumerosFiltrada.Any())
            {

                int indiceInsercion = -1;
                switch (Ubicacion)
                {
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadActual:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion <= ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion;
                        else
                            indiceInsercion = ListaNumerosFiltrada.Count - 1;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion > 0)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion - 1;
                        else
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion <= ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion + 1;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.ValorFijo:
                        indiceInsercion = -2;
                        break;
                }

                if (indiceInsercion > -1)
                    return ListaNumerosFiltrada[indiceInsercion].Textos;
                else if (indiceInsercion == -2)
                    return new List<string>() { itemCondiciones.ValorFijo_Insercion };
            }

            return new List<string>();
        }

        public List<string> SeleccionarCantidadProcesamiento_TextosInformacion_Resultado(List<ElementoEjecucionCalculo> ElementosResultado,
            TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades Ubicacion, CondicionProcesamientoTextosInformacion itemCondiciones)
        {
            if (ElementosResultado.Any())
            {
                int indiceInsercion = -1;
                switch (Ubicacion)
                {
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadActual:
                        indiceInsercion = ElementosResultado.Count - 1;
                        break;
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente:
                        indiceInsercion = ElementosResultado.Count;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior:
                        indiceInsercion = ElementosResultado.Count - 1;
                        break;
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.ValorFijo:
                        indiceInsercion = -2;
                        break;
                }

                if (indiceInsercion > -1)
                    return ElementosResultado[indiceInsercion].Textos;
                else if (indiceInsercion == -2)
                    return new List<string>() { itemCondiciones.ValorFijo_Insercion };
            }

            return new List<string>();
        }

        public List<string> SeleccionarCantidadProcesamiento_TextosInformacion(ElementoDiseñoOperacionAritmeticaEjecucion itemOperacion,
            TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades Ubicacion, CondicionProcesamientoTextosInformacion itemCondiciones
            , Clasificador itemClasificador)
        {
            var ListaNumerosFiltrada = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto) && 
            EntidadNumero.FiltrarNumeros(i, itemOperacion, itemOperacion.NumerosFiltrados)).ToList();

            if(ListaNumerosFiltrada.Any())
            {
                int indiceInsercion = -1;
                switch (Ubicacion)
                {
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadActual:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion <= ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion;
                        else
                            indiceInsercion = itemOperacion.Numeros.Count - 1;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion > 0)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion - 1;
                        else
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion <= ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion + 1;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.ValorFijo:
                        indiceInsercion = -2;
                        break;
                }

                if (indiceInsercion > -1)
                    return ListaNumerosFiltrada[indiceInsercion].Textos;
                else if (indiceInsercion == -2)
                    return new List<string>() { itemCondiciones.ValorFijo_Insercion };
            }

            return new List<string>();
        }

        public List<string> SeleccionarCantidadProcesamiento_TextosInformacion_Resultado(List<EntidadNumero> ElementosResultado,
            TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades Ubicacion, CondicionProcesamientoTextosInformacion itemCondiciones,
            int PosicionResultado, int indiceAdicional_ResultadosPorFilas)
        {
            if (ElementosResultado.Any())
            {
                int indiceInsercion = -1;
                switch (Ubicacion)
                {
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadActual:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadSiguiente:
                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.ValorFijo:
                        indiceInsercion = PosicionResultado - indiceAdicional_ResultadosPorFilas;

                        if (indiceInsercion > ElementosResultado.Count)
                            indiceInsercion = ElementosResultado.Count;
                        else if (indiceInsercion > ElementosResultado.Count - 1)
                            indiceInsercion = ElementosResultado.Count - 1;
                        break;

                    case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionCantidadAnterior:
                        indiceInsercion = PosicionResultado - 1 - indiceAdicional_ResultadosPorFilas;

                        if (indiceInsercion > ElementosResultado.Count - 2)
                            indiceInsercion = ElementosResultado.Count - 2;
                        break;
                    //case TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.ValorFijo:
                    //    indiceInsercion = -2;
                    //    break;
                }

                if (indiceInsercion > -1)
                {
                    return ElementosResultado[indiceInsercion].Textos;
                }

                //else if (indiceInsercion == -2)
                //    return new List<string>() { itemCondiciones.ValorFijo_Insercion };
            }

            return new List<string>();
        }

        public List<string> SeleccionarCantidadUbicacionProcesamiento_TextosInformacion(ElementoOperacionAritmeticaEjecucion itemOperacion,
            TipoOpcionElementoAccion_InsertarProcesamientoCantidades Ubicacion, CondicionProcesamientoTextosInformacion itemCondiciones
            , Clasificador itemClasificador)
        {
            var ListaNumerosFiltrada = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto) &&
                        ElementoOperacionAritmeticaEjecucion.FiltrarNumeros(i, itemOperacion, itemOperacion.NumerosFiltrados)).ToList();

            if(ListaNumerosFiltrada.Any())
            {

                int indiceInsercion = -1;
                switch (Ubicacion)
                {
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion <= ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion;
                        else
                            indiceInsercion = ListaNumerosFiltrada.Count - 1;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadAnterior:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion > 0)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion - 1;
                        else
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadSiguiente:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion < ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion + 1;
                        break;
                }

                if (indiceInsercion > -1)
                    return ListaNumerosFiltrada[indiceInsercion].Textos;
            }

            return new List<string>();
        }

        public List<string> SeleccionarCantidadUbicacionProcesamiento_TextosInformacion_Resultado(List<ElementoEjecucionCalculo> ElementosResultado,
            TipoOpcionElementoAccion_InsertarProcesamientoCantidades Ubicacion, CondicionProcesamientoTextosInformacion itemCondiciones,
            int indiceAdicional_ResultadosPorFilas)
        {
            if (ElementosResultado.Any())
            {
                int indiceInsercion = -1;
                switch (Ubicacion)
                {
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadSiguiente:
                        indiceInsercion = ElementosResultado.Count - 1 - indiceAdicional_ResultadosPorFilas;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadAnterior:
                        indiceInsercion = ElementosResultado.Count - 2 - indiceAdicional_ResultadosPorFilas;
                        break;
                }

                if (indiceInsercion > -1)
                    return ElementosResultado[indiceInsercion].Textos;
            }

            return new List<string>();
        }

        public List<string> SeleccionarCantidadUbicacionProcesamiento_TextosInformacion(ElementoDiseñoOperacionAritmeticaEjecucion itemOperacion,
            TipoOpcionElementoAccion_InsertarProcesamientoCantidades Ubicacion, CondicionProcesamientoTextosInformacion itemCondiciones
            , Clasificador itemClasificador)
        {
            var ListaNumerosFiltrada = itemOperacion.Numeros.Where(i => i.Clasificadores_SeleccionarOrdenar.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto) &&
            EntidadNumero.FiltrarNumeros(i, itemOperacion, itemOperacion.NumerosFiltrados)).ToList();

            if(ListaNumerosFiltrada.Any())
            {
                int indiceInsercion = -1;
                switch (Ubicacion)
                {
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion <= ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion;
                        else
                            indiceInsercion = itemOperacion.Numeros.Count - 1;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadAnterior:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion > 0)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion - 1;
                        else
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadSiguiente:
                        if (itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion < ListaNumerosFiltrada.Count - 1)
                            indiceInsercion = itemOperacion.PosicionActualNumero_CondicionesOperador_Implicacion + 1;
                        break;
                }

                if (indiceInsercion > -1)
                    return ListaNumerosFiltrada[indiceInsercion].Textos;
            }

            return new List<string>();
        }

        public List<string> SeleccionarCantidadUbicacionProcesamiento_TextosInformacion_Resultado(List<EntidadNumero> ElementosResultado,
            TipoOpcionElementoAccion_InsertarProcesamientoCantidades Ubicacion, CondicionProcesamientoTextosInformacion itemCondiciones,
            int PosicionResultado, int indiceAdicional_ResultadosPorFilas)
        {
            if (ElementosResultado.Any())
            {
                int indiceInsercion = -1;
                switch (Ubicacion)
                {
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadActual:
                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadSiguiente:
                        indiceInsercion = PosicionResultado - indiceAdicional_ResultadosPorFilas;
                        break;

                    case TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadAnterior:
                        indiceInsercion = PosicionResultado - 1 - indiceAdicional_ResultadosPorFilas;
                        break;
                }

                if (indiceInsercion > -1)
                    return ElementosResultado[indiceInsercion].Textos;
            }

            return new List<string>();
        }
    }
}