using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class CondicionSeleccionCantidadNumeros_Entrada
    {
        public TipoElementoCondicion_SeleccionarNumeros_Entrada TipoElementoCondicion { get; set; }
        public TipoOpcionCondicion_SeleccionarNumeros_Entrada TipoOpcionCondicion { get; set; }
        public List<CondicionSeleccionCantidadNumeros_Entrada> Condiciones { get; set; }
        public CondicionSeleccionCantidadNumeros_Entrada CondicionContenedora { get; set; }
        public TipoElementoCondicion_SeleccionarNumeros_Entrada TipoElementoValores { get; set; }
        public string Valores_Condicion { get; set; }
        public TipoConectorCondiciones_ConjuntoBusquedas TipoConector { get; set; }
        public int CantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadNumerosCumplenCondicion OpcionCantidadNumerosCumplenCondicion { get; set; }
        public TipoOpcionCantidadDeterminadaNumerosCumplenCondicion OpcionCantidadDeterminadaNumerosCumplenCondicion { get; set; }
        public bool ConectorO_Excluyente { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesCumplenCondicion { get; set; }
        [IgnoreDataMember]
        public int EvaluacionesNoCumplenCondicion { get; set; }
        [IgnoreDataMember]
        public int CantidadTextosInformacion_Obtenidos { get; set; }
        [IgnoreDataMember]
        public int CantidadNumeros_Obtenidos { get; set; }
        [IgnoreDataMember]
        public int PosicionInicialNumeros_Obtenidos { get; set; }
        [IgnoreDataMember]
        public int PosicionFinalNumeros_Obtenidos { get; set; }
        [IgnoreDataMember]
        public int CantidadTotalTextosInformacion_Entrada { get; set; }
        [IgnoreDataMember]
        public int CantidadTotalNumeros_Entrada { get; set; }
        public CondicionSeleccionCantidadNumeros_Entrada()
        {
            TipoElementoCondicion = TipoElementoCondicion_SeleccionarNumeros_Entrada.Ninguno;
            TipoOpcionCondicion = TipoOpcionCondicion_SeleccionarNumeros_Entrada.Ninguno;
            TipoElementoValores = TipoElementoCondicion_SeleccionarNumeros_Entrada.Ninguno;
            Valores_Condicion = string.Empty;
            TipoConector = TipoConectorCondiciones_ConjuntoBusquedas.InicioCondiciones;
            Condiciones = new List<CondicionSeleccionCantidadNumeros_Entrada>();
            CantidadNumerosCumplenCondicion = 2;
            OpcionCantidadNumerosCumplenCondicion = TipoOpcionCantidadNumerosCumplenCondicion.AlMenos1;
            OpcionCantidadDeterminadaNumerosCumplenCondicion = TipoOpcionCantidadDeterminadaNumerosCumplenCondicion.AlMenos;
        }

        public CondicionSeleccionCantidadNumeros_Entrada CopiarObjeto()
        {
            CondicionSeleccionCantidadNumeros_Entrada condicion = new CondicionSeleccionCantidadNumeros_Entrada();
            condicion.TipoElementoCondicion = TipoElementoCondicion;
            condicion.TipoOpcionCondicion = TipoOpcionCondicion;
            condicion.CondicionContenedora = CondicionContenedora.CopiarObjeto();
            condicion.TipoElementoValores = TipoElementoValores;
            condicion.Valores_Condicion = Valores_Condicion;
            condicion.TipoConector = TipoConector;
            condicion.CantidadNumerosCumplenCondicion = CantidadNumerosCumplenCondicion;
            condicion.OpcionCantidadNumerosCumplenCondicion = OpcionCantidadNumerosCumplenCondicion;
            condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion = OpcionCantidadDeterminadaNumerosCumplenCondicion;
            condicion.ConectorO_Excluyente = ConectorO_Excluyente;

            condicion.Condiciones = new List<CondicionSeleccionCantidadNumeros_Entrada>();
            foreach (var item in Condiciones)
                condicion.Condiciones.Add(item.CopiarObjeto());

            return condicion;
        }

        public bool EvaluarCondiciones(ElementoOrigenDatosEjecucion origenDatosEntrada)
        {
            bool valorCondicion = EvaluarCondicion(origenDatosEntrada);

            if (Condiciones.Any())
            {
                //if (valorCondicion)
                //{
                List<bool> valoresCondicion = new List<bool>();
                valoresCondicion.Add(valorCondicion);

                int indiceCondicion = 0;
                foreach (var itemCondicion in Condiciones)
                {
                    bool valorItemCondicion = itemCondicion.EvaluarCondiciones(origenDatosEntrada);

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
                    }

                    indiceCondicion++;
                }

                EvaluacionesCumplenCondicion++;

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

        private bool EvaluarCondicion(ElementoOrigenDatosEjecucion origenDatosEntrada)
        {
            bool valorCondicion = false;
                        
            string[] valoresCondicion = Valores_Condicion.Split('|');

            foreach (var itemValorCondicion in valoresCondicion)
            {
                switch (TipoElementoCondicion)
                {
                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTextosInformacion_Obtenidos:

                        int valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (CantidadTextosInformacion_Obtenidos != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (CantidadTextosInformacion_Obtenidos == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (CantidadTextosInformacion_Obtenidos.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(CantidadTextosInformacion_Obtenidos.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (CantidadTextosInformacion_Obtenidos.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (CantidadTextosInformacion_Obtenidos >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (CantidadTextosInformacion_Obtenidos > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (CantidadTextosInformacion_Obtenidos <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (CantidadTextosInformacion_Obtenidos < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!CantidadTextosInformacion_Obtenidos.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!CantidadTextosInformacion_Obtenidos.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadNumeros_Obtenidos:
                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.Ninguno:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (CantidadNumeros_Obtenidos != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (CantidadNumeros_Obtenidos == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (CantidadNumeros_Obtenidos.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(CantidadNumeros_Obtenidos.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (CantidadNumeros_Obtenidos.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (CantidadNumeros_Obtenidos >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (CantidadNumeros_Obtenidos > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (CantidadNumeros_Obtenidos <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (CantidadNumeros_Obtenidos < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!CantidadNumeros_Obtenidos.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!CantidadNumeros_Obtenidos.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!origenDatosEntrada.CantidadTextosInformacion_Obtenidos_UltimaEjecucion.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadNumeros_Obtenidos_UltimaEjecucion:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!origenDatosEntrada.CantidadNumeros_Obtenidos_UltimaEjecucion.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionInicialNumeros_Obtenidos:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (PosicionInicialNumeros_Obtenidos != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (PosicionInicialNumeros_Obtenidos == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (PosicionInicialNumeros_Obtenidos.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(PosicionInicialNumeros_Obtenidos.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (PosicionInicialNumeros_Obtenidos.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (PosicionInicialNumeros_Obtenidos >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (PosicionInicialNumeros_Obtenidos > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (PosicionInicialNumeros_Obtenidos <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (PosicionInicialNumeros_Obtenidos < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!PosicionInicialNumeros_Obtenidos.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!PosicionInicialNumeros_Obtenidos.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!origenDatosEntrada.PosicionInicialNumeros_Obtenidos_UltimaEjecucion.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionFinalNumeros_Obtenidos:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (PosicionFinalNumeros_Obtenidos != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (PosicionFinalNumeros_Obtenidos == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (PosicionFinalNumeros_Obtenidos.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(PosicionFinalNumeros_Obtenidos.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (PosicionFinalNumeros_Obtenidos.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (PosicionFinalNumeros_Obtenidos >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (PosicionFinalNumeros_Obtenidos > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (PosicionFinalNumeros_Obtenidos <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (PosicionFinalNumeros_Obtenidos < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!PosicionFinalNumeros_Obtenidos.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!PosicionFinalNumeros_Obtenidos.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!origenDatosEntrada.PosicionFinalNumeros_Obtenidos_UltimaEjecucion.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTotalTextosInformacion_Entrada:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (CantidadTotalTextosInformacion_Entrada != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (CantidadTotalTextosInformacion_Entrada == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (CantidadTotalTextosInformacion_Entrada.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(CantidadTotalTextosInformacion_Entrada.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (CantidadTotalTextosInformacion_Entrada.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (CantidadTotalTextosInformacion_Entrada >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (CantidadTotalTextosInformacion_Entrada > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (CantidadTotalTextosInformacion_Entrada <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (CantidadTotalTextosInformacion_Entrada < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!CantidadTotalTextosInformacion_Entrada.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!CantidadTotalTextosInformacion_Entrada.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;

                    case TipoElementoCondicion_SeleccionarNumeros_Entrada.CantidadTotalNumeros_Entrada:

                        valor = 0;

                        if (int.TryParse(itemValorCondicion, out valor))
                        {
                            switch (TipoOpcionCondicion)
                            {
                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsDistintoA:
                                    if (CantidadTotalNumeros_Entrada != valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsIgualA:
                                    if (CantidadTotalNumeros_Entrada == valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.Contiene:
                                    if (CantidadTotalNumeros_Entrada.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EsParteDe:
                                    if (valor.ToString().Contains(CantidadTotalNumeros_Entrada.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.EmpiezaCon:
                                    if (CantidadTotalNumeros_Entrada.ToString().StartsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorOIgualQue:
                                    if (CantidadTotalNumeros_Entrada >= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MayorQue:
                                    if (CantidadTotalNumeros_Entrada > valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorOIgualQue:
                                    if (CantidadTotalNumeros_Entrada <= valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.MenorQue:
                                    if (CantidadTotalNumeros_Entrada < valor)
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.NoContiene:
                                    if (!CantidadTotalNumeros_Entrada.ToString().Contains(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                                case TipoOpcionCondicion_SeleccionarNumeros_Entrada.TerminaCon:
                                    if (!CantidadTotalNumeros_Entrada.ToString().EndsWith(valor.ToString()))
                                        valorCondicion = true;
                                    break;

                            }
                        }

                        break;
                }
            }

            return valorCondicion;
        }
    }
}
