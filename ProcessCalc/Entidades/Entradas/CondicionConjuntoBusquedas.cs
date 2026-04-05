using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class CondicionConjuntoBusquedas
    {
        public BusquedaTextoArchivo BusquedaCondicion { get; set; }
        public TipoElementoCondicion_ConjuntoBusquedas TipoElementoCondicion { get; set; }
        public TipoOpcionCondicion_ConjuntoBusquedas TipoOpcionCondicion { get; set; }
        public string Valores_Condicion { get; set; }
        public CondicionConjuntoBusquedas CondicionContenedora { get; set; }
        public TipoConectorCondiciones_ConjuntoBusquedas TipoConector { get; set; }
        public List<CondicionConjuntoBusquedas> Condiciones { get; set; }
        public int CantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaNumerosCumplenCondicion { get; set; }
        public bool ContenedorCondiciones { get; set; }
        public bool ConectorO_Excluyente { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesCumplenCondicion { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesNoCumplenCondicion { get; set; }
        public CondicionConjuntoBusquedas()
        {
            TipoElementoCondicion = TipoElementoCondicion_ConjuntoBusquedas.Ninguno;
            TipoOpcionCondicion = TipoOpcionCondicion_ConjuntoBusquedas.Ninguno;
            Valores_Condicion = string.Empty;
            TipoConector = TipoConectorCondiciones_ConjuntoBusquedas.InicioCondiciones;
            Condiciones = new List<CondicionConjuntoBusquedas>();
            CantidadNumerosCumplenCondicion = 2;
            OpcionCantidadNumerosCumplenCondicion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaNumerosCumplenCondicion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
        }

        public bool EvaluarCondiciones(ElementoOrigenDatosEjecucion origenDatosEntrada, bool TextoBusquedaDistinto = false,
            string TextoBusquedaEncontrado = "")
        {
            BusquedaArchivoEjecucion busquedaEjecucion = origenDatosEntrada.ObtenerBusquedaRelacionadaCondicion(BusquedaCondicion);

            bool valorCondicion = true;

            if(!ContenedorCondiciones)
                valorCondicion = EvaluarCondicion(busquedaEjecucion, TextoBusquedaDistinto, TextoBusquedaEncontrado);

            if (Condiciones.Any())
            {
                //if (valorCondicion)
                //{
                List<bool> valoresCondicion = new List<bool>();
                valoresCondicion.Add(valorCondicion);

                int indiceCondicion = 0;
                foreach (var itemCondicion in Condiciones)
                    {
                        bool valorItemCondicion = itemCondicion.EvaluarCondiciones(origenDatosEntrada, TextoBusquedaDistinto, TextoBusquedaEncontrado);

                    valoresCondicion.Add(valorItemCondicion);

                    switch (itemCondicion.TipoConector)
                    {
                        case TipoConectorCondiciones_ConjuntoBusquedas.O:

                            if (itemCondicion != Condiciones.Last() &&
                            Condiciones[indiceCondicion + 1].TipoConector != itemCondicion.TipoConector)
                            {
                                if (!((itemCondicion.ConectorO_Excluyente &&
                                    valoresCondicion.Count(i => i) == 1) ||
                                    (!itemCondicion.ConectorO_Excluyente && 
                                    valoresCondicion.Contains(true))))
                                {
                                    EvaluacionesNoCumplenCondicion++;
                                    return false;
                                }
                                else
                                {
                                    valoresCondicion.Clear();
                                    continue;
                                }
                            }
                            else if (itemCondicion == Condiciones.Last())
                            {
                                if (!((itemCondicion.ConectorO_Excluyente &&
                                    valoresCondicion.Count(i => i) == 1) ||
                                    (!itemCondicion.ConectorO_Excluyente &&
                                    valoresCondicion.Contains(true))))
                                {
                                    EvaluacionesNoCumplenCondicion++;
                                    return false;
                                }
                            }


                            break;

                        case TipoConectorCondiciones_ConjuntoBusquedas.Y:

                            if (itemCondicion != Condiciones.Last() &&
                        Condiciones[indiceCondicion + 1].TipoConector != itemCondicion.TipoConector)
                            {
                                if (valoresCondicion.Contains(false))
                                {
                                    EvaluacionesNoCumplenCondicion++;
                                    return false;
                                }
                                else
                                {
                                    valoresCondicion.Clear();
                                    continue;
                                }
                            }
                            else if (itemCondicion == Condiciones.Last() && valoresCondicion.Contains(false))
                            {
                                EvaluacionesNoCumplenCondicion++;
                                return false;
                            }

                            break;

                        case TipoConectorCondiciones_ConjuntoBusquedas.InicioCondiciones:
                            if(valorItemCondicion)
                            {
                                EvaluacionesCumplenCondicion++;
                            }
                            else
                            {
                                EvaluacionesNoCumplenCondicion++;
                            }

                            break;
                    }

                    indiceCondicion++;
                }

                switch (OpcionCantidadNumerosCumplenCondicion)
                {
                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                        if (EvaluacionesCumplenCondicion == 0)
                            return false;

                        break;
                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                        if (EvaluacionesNoCumplenCondicion > 0)
                            return false;

                        break;

                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:
                        switch (OpcionCantidadDeterminadaNumerosCumplenCondicion)
                        {
                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                if (EvaluacionesCumplenCondicion < CantidadNumerosCumplenCondicion)
                                    return false;
                                break;

                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                if (EvaluacionesCumplenCondicion > CantidadNumerosCumplenCondicion)
                                    return false;
                                break;

                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                if (EvaluacionesCumplenCondicion != CantidadNumerosCumplenCondicion)
                                    return false;
                                break;
                        }

                        break;
                }

                return true;
                //}
                //else
                //    return valorCondicion;
            }
            else
            {
                if (valorCondicion)
                    EvaluacionesCumplenCondicion++;
                else
                    EvaluacionesNoCumplenCondicion++;

                switch (OpcionCantidadNumerosCumplenCondicion)
                {
                    case TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1:
                        if (EvaluacionesCumplenCondicion == 0)
                            valorCondicion = false;

                        break;
                    case TipoOpcionCantidadNumerosCumplenCondicion.Todos:
                        if (EvaluacionesNoCumplenCondicion > 0)
                            valorCondicion = false;

                        break;

                    case TipoOpcionCantidadNumerosCumplenCondicion.CantidadDeterminada:
                        switch (OpcionCantidadDeterminadaNumerosCumplenCondicion)
                        {
                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos:
                                if (EvaluacionesCumplenCondicion < CantidadNumerosCumplenCondicion)
                                    valorCondicion = false;
                                break;

                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.ComoMaximo:
                                if (EvaluacionesCumplenCondicion > CantidadNumerosCumplenCondicion)
                                    valorCondicion = false;
                                break;

                            case TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.Exactamente:
                                if (EvaluacionesCumplenCondicion != CantidadNumerosCumplenCondicion)
                                    valorCondicion = false;
                                break;
                        }

                        break;
                }

                return valorCondicion;
            }
        }

        private bool EvaluarCondicion(BusquedaArchivoEjecucion BusquedaCondicion, bool TextoBusquedaDistinto = false,
            string TextoBusquedaEncontrado = "")
        {
            bool valorCondicion = false;

            if (BusquedaCondicion != null)
            {                
                string[] valoresCondicion = Valores_Condicion.Split('|');

                foreach (var itemValorCondicion in valoresCondicion)
                {
                    switch (TipoElementoCondicion)
                    {
                        case TipoElementoCondicion_ConjuntoBusquedas.BusquedasConjuntoRealizadas:

                            int valor = 0;

                            if (int.TryParse(itemValorCondicion, out valor))
                            {
                                switch (TipoOpcionCondicion)
                                {
                                    case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                                        if (BusquedaCondicion.BusquedasConjuntoEjecutadas != valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                                        if (BusquedaCondicion.BusquedasConjuntoEjecutadas == valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                                        if (BusquedaCondicion.BusquedasConjuntoEjecutadas.ToString().Contains(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                                        if (BusquedaCondicion.BusquedasConjuntoEjecutadas.ToString().StartsWith(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                                        if (BusquedaCondicion.BusquedasConjuntoEjecutadas >= valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                                        if (BusquedaCondicion.BusquedasConjuntoEjecutadas > valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                                        if (BusquedaCondicion.BusquedasConjuntoEjecutadas <= valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                                        if (BusquedaCondicion.BusquedasConjuntoEjecutadas < valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                                        if (!BusquedaCondicion.BusquedasConjuntoEjecutadas.ToString().Contains(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                                        if (!BusquedaCondicion.BusquedasConjuntoEjecutadas.ToString().EndsWith(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                }
                            }

                            break;

                        case TipoElementoCondicion_ConjuntoBusquedas.CantidadNumerosEncontrados:

                            valor = 0;

                            if (int.TryParse(itemValorCondicion, out valor))
                            {
                                switch (TipoOpcionCondicion)
                                {
                                    case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                                        if (BusquedaCondicion.CantidadNumerosEncontrados != valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                                        if (BusquedaCondicion.CantidadNumerosEncontrados == valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                                        if (BusquedaCondicion.CantidadNumerosEncontrados.ToString().Contains(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                                        if (BusquedaCondicion.CantidadNumerosEncontrados.ToString().StartsWith(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                                        if (BusquedaCondicion.CantidadNumerosEncontrados >= valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                                        if (BusquedaCondicion.CantidadNumerosEncontrados > valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                                        if (BusquedaCondicion.CantidadNumerosEncontrados <= valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                                        if (BusquedaCondicion.CantidadNumerosEncontrados < valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                                        if (!BusquedaCondicion.CantidadNumerosEncontrados.ToString().Contains(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                                        if (!BusquedaCondicion.CantidadNumerosEncontrados.ToString().EndsWith(valor.ToString()))
                                            valorCondicion = true;
                                        break;
                                }
                            }

                            break;

                        case TipoElementoCondicion_ConjuntoBusquedas.CantidadTextosInformacion_AsignacionNumeros:

                            valor = 0;

                            if (int.TryParse(itemValorCondicion, out valor))
                            {
                                switch (TipoOpcionCondicion)
                                {
                                    case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                                        if (BusquedaCondicion.CantidadTextosInformacionEncontrados != valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                                        if (BusquedaCondicion.CantidadTextosInformacionEncontrados == valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                                        if (BusquedaCondicion.CantidadTextosInformacionEncontrados.ToString().Contains(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                                        if (BusquedaCondicion.CantidadTextosInformacionEncontrados.ToString().StartsWith(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                                        if (BusquedaCondicion.CantidadTextosInformacionEncontrados >= valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                                        if (BusquedaCondicion.CantidadTextosInformacionEncontrados > valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                                        if (BusquedaCondicion.CantidadTextosInformacionEncontrados <= valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                                        if (BusquedaCondicion.CantidadTextosInformacionEncontrados < valor)
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                                        if (!BusquedaCondicion.CantidadTextosInformacionEncontrados.ToString().Contains(valor.ToString()))
                                            valorCondicion = true;
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                                        if (!BusquedaCondicion.CantidadTextosInformacionEncontrados.ToString().EndsWith(valor.ToString()))
                                            valorCondicion = true;
                                        break;
                                }
                            }

                            break;

                        case TipoElementoCondicion_ConjuntoBusquedas.NumerosEncontrados:

                            if (BusquedaCondicion.NumerosEncontrados.Count == 0)
                            {
                                valorCondicion = true;
                                break;
                            }

                            valor = 0;

                            if (int.TryParse(itemValorCondicion, out valor))
                            {
                                switch (TipoOpcionCondicion)
                                {
                                    case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero != valor)
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }

                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero == valor)
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero.ToString().Contains(valor.ToString()))
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero.ToString().StartsWith(valor.ToString()))
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero >= valor)
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero > valor)
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero <= valor)
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero < valor)
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (!itemNumero.ToString().Contains(valor.ToString()))
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                                        foreach (var itemNumero in BusquedaCondicion.NumerosEncontrados)
                                        {
                                            if (itemNumero.ToString().EndsWith(valor.ToString()))
                                            {
                                                valorCondicion = true;
                                                break;
                                            }
                                        }
                                        break;
                                }
                            }

                            break;

                        case TipoElementoCondicion_ConjuntoBusquedas.TextosInformacionAsignacion_Numeros:

                            //if (BusquedaCondicion.TextosInformacionEncontrados.Count == 0)
                            //{
                            //    valorCondicion = true;
                            //    break;
                            //}

                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (string.Compare(itemTexto.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) != 0)
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }

                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (string.Compare(itemTexto.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) == 0)
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (itemTexto.Trim().ToLower().Contains(itemValorCondicion.Trim().ToLower()))
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (itemTexto.Trim().ToLower().StartsWith(itemValorCondicion.Trim().ToLower()))
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (string.Compare(itemTexto.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) >= 0)
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (string.Compare(itemTexto.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) > 0)
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (string.Compare(itemTexto.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) <= 0)
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (string.Compare(itemTexto.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) < 0)
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (!itemTexto.Trim().ToLower().Contains(itemValorCondicion.Trim().ToLower()))
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                                    foreach (var itemTexto in BusquedaCondicion.TextosInformacionEncontrados)
                                    {
                                        if (itemTexto.Trim().ToLower().EndsWith(itemValorCondicion.Trim().ToLower()))
                                        {
                                            valorCondicion = true;
                                            break;
                                        }
                                    }
                                    break;
                            }

                            break;

                        case TipoElementoCondicion_ConjuntoBusquedas.TextoBusquedaEncontrado:

                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_ConjuntoBusquedas.TextoBusquedaCoincida:
                                    if (!TextoBusquedaDistinto)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.TextoBusquedaNoCoincida:
                                    if (TextoBusquedaDistinto)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.EsDistintoA:
                                    
                                    if (string.Compare(TextoBusquedaEncontrado.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) != 0)
                                    {
                                        valorCondicion = true;
                                        break;
                                    }                                    

                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.EsIgualA:
                                    
                                    if (string.Compare(TextoBusquedaEncontrado.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) == 0)
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.Contiene:
                                    
                                    if (TextoBusquedaEncontrado.Trim().ToLower().Contains(itemValorCondicion.Trim().ToLower()))
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.EmpiezaCon:
                                    
                                    if (TextoBusquedaEncontrado.Trim().ToLower().StartsWith(itemValorCondicion.Trim().ToLower()))
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.MayorOIgualQue:
                                    
                                    if (string.Compare(TextoBusquedaEncontrado.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) >= 0)
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.MayorQue:
                                    
                                    if (string.Compare(TextoBusquedaEncontrado.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) > 0)
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.MenorOIgualQue:
                                    
                                    if (string.Compare(TextoBusquedaEncontrado.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) <= 0)
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.MenorQue:
                                    
                                    if (string.Compare(TextoBusquedaEncontrado.Trim().ToLower(), itemValorCondicion.Trim().ToLower()) < 0)
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.NoContiene:
                                    
                                    if (!TextoBusquedaEncontrado.Trim().ToLower().Contains(itemValorCondicion.Trim().ToLower()))
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;

                                case TipoOpcionCondicion_ConjuntoBusquedas.TerminaCon:
                                    
                                    if (TextoBusquedaEncontrado.Trim().ToLower().EndsWith(itemValorCondicion.Trim().ToLower()))
                                    {
                                        valorCondicion = true;
                                        break;
                                    }
                                    
                                    break;
                            }

                            break;
                    }
                }
                
            }

            return valorCondicion;
        }

        public void LimpiarBusqueda()
        {
            EvaluacionesCumplenCondicion = 0;
            EvaluacionesNoCumplenCondicion = 0;
        }
    }
}
