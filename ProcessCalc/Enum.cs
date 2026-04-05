using System.Windows.Controls;

namespace ProcessCalc
{
    public enum OpcionSeleccionada
    {
        Ninguna = 0,
        Informacion = 1,
        EditarInfo = 2,
        Entradas = 3,
        Operaciones = 4,
        Calculos = 5,
        Resultados = 6,
        Ejecutar = 7,
        TextosInformacion = 8
    }

    public enum TipoEntrada
    {
        Ninguno = 0,
        Numero = 1,
        ConjuntoNumeros = 2,
        Calculo = 3,
        TextosInformacion = 4
    }

    public enum TipoOpcionNumeroEntrada
    {
        Ninguno = 0,
        NumeroFijo = 1,
        SeDigita = 2,
        SeObtiene = 3
    }

    public enum TipoDefinicionSeparadores
    {
        Ninguno = 0,
        SeparadorMilesPunto_SeparadorDecimalesComa = 1,
        SeparadorMilesNinguno_SeparadorDecimalesComa = 2,
        SeparadorMilesNinguno_SeparadorDecimalesPunto = 3,
        SeparadorMilesComa_SeparadorDecimalesPunto = 4,
        SeparadorMilesPunto_SeparadorDecimalesNinguno = 5,
        SeparadorMilesComa_SeparadorDecimalesNinguno = 6
    }

    public enum TipoOrigenDatos
    {
        Ninguno = 0,
        Archivo = 1,
        DesdeInternet = 2,
        Excel = 3
    }

    public enum TipoFormatoArchivoEntrada
    {
        Ninguno = 0,
        ArchivoTextoPlano = 1,
        PDF = 2,
        Word = 3,
        TextoPantalla = 4
    }

    public enum TipoArchivo
    {
        Ninguno = 0,
        EquipoLocal = 1,
        RedLocal = 2,
        ServidorFTP = 3,
        Internet = 4
    }

    public enum OpcionBusquedaNumero
    {
        Ninguno = 0,
        BusquedaTexto = 1,
        BusquedaTextoNVeces = 2
    }

    public enum TipoOpcionConjuntoNumerosEntrada
    {
        Ninguno = 0,
        ConjuntoNumerosFijo = 1,
        SeDigita = 2,
        SeObtiene = 3
    }

    public enum TipoOpcionTextosInformacionEntrada
    {
        Ninguno = 0,
        TextosInformacionFijos = 1,
        SeDigita = 2,
        SeObtiene = 3
    }

    public enum TipoElementoOperacion
    {
        Ninguna = 0,
        Linea = 1,
        Entrada = 2,
        Suma = 3,
        Resta = 4,
        Multiplicacion = 5,
        Division = 6,
        Salida = 7,
        Nota = 8,
        Potencia = 9,
        Raiz = 10,
        SeleccionarOrdenar = 11,
        Definicion_TextosInformacion = 12,
        ConjuntoNumerosAgrupado_SeleccionarOrdenar = 13,
        CondicionesFlujo = 14,
        AgrupadorOperaciones = 15,
        Porcentaje = 16,
        Logaritmo = 17,
        Inverso = 18,
        ContarCantidades = 19,
        SeleccionarEntradas = 20,
        Factorial = 21,
        Espera = 22,
        LimpiarDatos = 23,
        RedondearCantidades = 24,
        ArchivoExterno = 25,
        SubCalculo = 26,
        Definicion_ListaCadenasTexto = 27,
    }

    public enum TipoOpcionOperacion
    {
        Ninguno = 0,
        TodosSeparados = 1,
        TodosJuntos = 2,
        PorFila = 3,
        Nota = 4,
        CalculandoPotencias_UnaSolaVez = 5,
        CalculandoPotencias_PorFila = 6,
        CalculandoRaices_UnaSolaVez = 7,
        CalculandoRaices_PorFila = 8,
        SeleccionarOrdenar_TodosSeparados = 9,
        SeleccionarOrdenar_TodosJuntos = 10,
        SeleccionarOrdenar_SoloUnir = 11,
        ConjuntoNumerosAgrupado_SeleccionarOrdenar = 12,
        CondicionesFlujo = 13,
        CondicionesFlujo_PorSeparado = 14,
        PorFilaPorSeparados = 15,
        CalculandoPorcentaje_UnaSolaVez = 16,
        CalculandoPorcentaje_PorFila = 17,
        CalculandoLogaritmo_UnaSolaVez = 18,
        CalculandoLogaritmo_PorFila = 19,
        CalculandoInverso = 20,
        ContandoCantidades_TodosJuntos = 21,
        ContandoCantidades_Separados = 22,
        CalculandoFactorial = 23,
        Espera = 24,
        LimpiarDatos = 25,
        RedondearCantidades = 26,
        ArchivoExterno = 27,
        SubCalculo = 28
    }

    public enum TipoElementoDiseñoOperacion
    {
        Ninguno = 0,
        Entrada = 1,
        FlujoOperacion = 2,
        OpcionOperacion = 3,
        Linea = 4,
        Salida = 5,
        Nota = 6
    }

    public enum TipoElementoEjecucion
    {
        Ninguno = 0,
        Entrada = 1,
        OperacionAritmetica = 2,
        ElementoOperacionAritmetica = 3,
        Calculo = 4
    }

    public enum TipoOperacionAritmeticaEjecucion
    {
        Ninguna = 0,
        Suma = 1,
        Resta = 2,
        Multiplicacion = 3,
        Division = 4,
        Potencia = 5,
        Raiz = 6,
        SeleccionarOrdenar = 7,
        ConjuntoNumerosAgrupado_SeleccionarOrdenar = 8,
        CondicionFlujo = 9,
        Porcentaje = 10,
        Logaritmo = 11,
        Inverso = 12,
        ContarCantidades = 13,
        SeleccionarEntradas = 14,
        Factorial = 15,
        Espera = 16,
        LimpiarDatos = 17,
        RedondearCantidades = 18,
        ArchivoExterno = 19,
        SubCalculo = 20
    }

    public enum EstadoEjecucion
    {
        Ninguno = 0,
        Iniciado = 1,
        Procesado = 2
    }

    public enum TipoElementoOperacionEjecucion
    {
        Ninguno = 0,
        Entrada = 1,
        FlujoOperacion = 2,
        OpcionOperacion = 3
    }

    public enum TipoEstablecerBasesExponentes
    {
        Ninguno = 0,
        EstablecerPorPares = 1,
        EstablecerPorConjuntosResultados = 2
    }

    public enum OpcionCarpetaEntrada
    {
        Ninguna = 0,
        CarpetaEspecificaSeleccionada = 1,
        CarpetaArchivoCalculoEjecucion = 2
    }

    public enum OpcionSeleccionarArchivoEntrada
    {
        Ninguna = 0,
        UtilizarArchivoIndicado = 1,
        SeleccionarArchivoEjecucion = 2,
        ElegirSeleccionarArchivoEjecucionPorEntrada = 3
    }

    public enum OpcionEscribirURLEntrada
    {
        Ninguna = 0,
        UtilizarURLIndicada = 1,
        EscribirURLEjecucion = 2,
        ElegirEscribirURLEjecucionPorEntrada = 3
    }

    public enum OpcionFinBusquedaTexto_Archivos
    {
        Ninguna = 0,
        EncontrarNveces = 1,
        EncontrarMientrasCoincida = 2,
        EncontrarHastaFinalArchivo = 3,
        EncontrarHastaCoincida = 4
    }

    public enum OpcionCantidadNumerosEntrada
    {
        Ninguna = 0,
        AgregarCantidadNumeros = 1,
        UtilizarSoloCantidaadNumeros = 2
    }

    public enum OpcionTextosInformacionBusqueda
    {
        Ninguna = 0,
        NumeroActual = 1,
        UltimoNumeroEncontrado = 2,
        SiguienteNumeroAEncontrar = 3
    }

    public enum OpcionAsignarTextosInformacion_NumerosBusqueda
    {
        Ninguna = 0,
        TodosNumeros = 1,
        CantidadNumerosEspecifica = 2,
        TextosInformacionPrevios = 3,
        BusquedasNumeros = 4
    }

    public enum OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones
    {
        Ninguna = 0,
        TodasIteraciones = 1,
        CantidadIteraciones = 2,
        IteracionActual = 3
    }

    public enum TipoElementoCondicion_ConjuntoBusquedas
    {
        Ninguno = 0,
        TextosInformacionAsignacion_Numeros = 1,
        CantidadTextosInformacion_AsignacionNumeros = 2,
        NumerosEncontrados = 3,
        CantidadNumerosEncontrados = 4,
        BusquedasConjuntoRealizadas = 5,
        TextoBusquedaEncontrado = 6
    }
    public enum TipoElementoCondicion_SeleccionarNumeros_Entrada
    {
        Ninguno = 0,
        CantidadTextosInformacion_Obtenidos = 1,
        CantidadNumeros_Obtenidos = 2,
        CantidadTextosInformacion_Obtenidos_UltimaEjecucion = 3,
        CantidadNumeros_Obtenidos_UltimaEjecucion = 4,
        ValoresFijos = 5,
        PosicionInicialNumeros_Obtenidos = 6,
        PosicionInicialNumeros_Obtenidos_UltimaEjecucion = 7,
        PosicionFinalNumeros_Obtenidos = 8,
        PosicionFinalNumeros_Obtenidos_UltimaEjecucion = 9,
        CantidadTotalTextosInformacion_Entrada = 10,
        CantidadTotalNumeros_Entrada = 11
    }

    public enum TipoOpcionCondicion_ConjuntoBusquedas
    {
        Ninguno = 0,
        EsIgualA = 1,
        EsDistintoA = 2,
        Contiene = 3,
        NoContiene = 4,
        EmpiezaCon = 5,
        TerminaCon = 6,
        MayorQue = 7,
        MenorQue = 8,
        MayorOIgualQue = 9,
        MenorOIgualQue = 10,
        TextoBusquedaCoincida = 11,
        TextoBusquedaNoCoincida = 12
    }

    public enum TipoOpcionCondicion_SeleccionarNumeros_Entrada
    {
        Ninguno = 0,
        EsIgualA = 1,
        EsDistintoA = 2,
        Contiene = 3,
        NoContiene = 4,
        EmpiezaCon = 5,
        TerminaCon = 6,
        MayorQue = 7,
        MenorQue = 8,
        MayorOIgualQue = 9,
        MenorOIgualQue = 10,
        EsParteDe = 11
    }

    public enum TipoConectorCondiciones_ConjuntoBusquedas
    {
        Ninguno = 0,
        InicioCondiciones = 1,
        Y = 2,
        O = 3
    }

    public enum TipoOpcionImplicacion_AsignacionTextoInformacion
    {
        Ninguna = 0,
        TextoIgual = 1,
        ContengaTexto = 2,
        EmpiecenCon = 3,
        TerminenCon = 4,
        TextoDistinto = 5,
        PosicionTextoIgual = 6,
        PosicionTextoDistinto = 7,
        PosicionTextoMayorQue = 8,
        PosicionTextoMenorQue = 9,
        PosicionTextoMayorIgualQue = 10,
        PosicionTextoMenorIgualQue = 11,
        EsParteDe = 12,
        CoincidaTextoBusqueda = 13,
        NoContengaTexto = 14,
        NoEsParteDe = 15,
        EsSoloNumero = 16,
        EsTexto = 17,
        NoTieneNumeros = 18,
        ContengaSoloLetras = 19,
        ContengaSoloSimbolos = 20,
        ContengaNumeros = 21,
    }

    public enum TipoOpcionEvaluacionTextosCondicion_ImplicacionTextosInformacion
    {
        Ninguno = 0,
        SoloNumeros = 1,
        SoloOperando = 2,
        NumerosYOperandos = 3
    }

    public enum TipoOpcionTextoAsignacion_ImplicacionTextosInformacion
    {
        Ninguno = 0,
        TextoFijo = 1,
        TextoDesdeEntrada = 2,
        TextoDesdeOperandos = 3
    }

    public enum TipoOpcionElemento_Condicion_ImplicacionTextosInformacion
    {
        Ninguno = 0,
        TextosInformacion = 1,
        OperacionEntrada = 2
    }

    public enum TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion
    {
        Ninguno = 0,
        TextosInformacionFijos = 1,
        TextosInformacion_DesdeEntrada = 2,
        TextosInformacion_DesdeElementoOperacion = 3,        
        TextosInformacion_DesdeDefinicion = 4,
        TextosInformacion_DesdeImplicacionInstancia = 5,
        TextosInformacion_DesdeImplicacionCondicion = 6,
        TextosInformacion_DesdeImplicacion = 7,
        TextosInformacion_TextosInformacion_CumplenCondicion = 8,
        TextosInformacion_TodosTextosInformacion_CumplenCondicion = 9,
        TextosInformacion_DesdeCumplenCondicionImplicacion = 10,
        TextosInformacion_DesdeDefinicionLista = 11,
    }

    public enum TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion
    {
        Ninguno = 0,
        ValoresFijos = 1,
        Valores_DesdeElementoOperacion = 2
    }

    public enum TipoOpcion_ValoresCondicion_Flujo
    {
        Ninguno = 0,
        ValoresFijos = 1,
        Valores_DesdeElementoOperacion = 2
    }

    public enum TipoOpcion_CondicionTextosInformacion_Implicacion
    {
        Ninguno = 0,
        EsIgualA = 1,
        EsDistintoA = 2,
        Contiene = 3,
        NoContiene = 4,
        EmpiezaCon = 5,
        TerminaCon = 6,
        MayorQue = 7,
        MenorQue = 8,
        MayorOIgualQue = 9,
        MenorOIgualQue = 10,
        EsParteDe = 11
    }

    public enum TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion
    {
        Ninguno = 0,
        NumerosElemento = 1,
        CantidadNumerosElemento = 2,
        PosicionesNumerosElemento = 3,
        PosicionesImplicaciones = 4,
        PosicionesInstanciasImplicaciones = 5,
        PosicionesIteracionesImplicaciones = 6,
        CantidadesCadenasTexto_Numero = 7,
        PosicionesOperandoElemento = 8,
    }

    public enum TipoOpcionesNombreCantidad_TextosInformacion
    {
        Ninguna = 0,
        PrimerTextoInformacion = 1,
        PrimerosNTextosInformacion = 2,
        UltimoTextoInformacion = 3,
        UltimosNTextosInformacion = 4,
        CumplenCondiciones = 5,
        Todos = 6,
        EnPosiciones = 7,
        TextosInformacionFijos = 8,
        TextoInformacionFijoPosicion = 9,
        TextoInformacionFijoNombreElemento = 10,
        TextoInformacionFijoNombreOperacion = 11,
        TextoInformacionFijoNombreNumeroElemento = 12,
        TextoInformacionFijoNombreNumerosFiltrados = 14,
        TodosTextosInformacionNumerosFiltrados = 15,
        PrimerTextoInformacionNumerosFiltrados = 16,
        PrimerosNTextosInformacionNumerosFiltrados = 17,
        UltimoTextoInformacionNumerosFiltrados = 18,
        UltimosNTextosInformacionNumerosFiltrados = 19,
        CumplenCondicionesNumerosFiltrados = 20,
        EnPosicionesNumerosFiltrados = 21,
        TodosTextosInformacionNumero = 22,
        PrimerTextoInformacionNumero = 23,
        PrimerosNTextosInformacionNumero = 24,
        UltimoTextoInformacionNumero = 25,
        UltimosNTextosInformacionNumero = 26,
        CumplenCondicionesNumero = 27,
        EnPosicionesNumero = 28,
        TextoInformacionFijoCantidadElemento = 29,
        TextoInformacionFijoCantidadNumerosFiltrados = 30,
        TextoInformacionFijoCantidadNumeroElemento = 31,
        TextoInformacionFijoCantidadOperacion = 32,
        TextoInformacionFijoPosicionDefinicion = 33,
        TextoInformacionFijoPosicionOperando = 34,
    }

    public enum TipoOpcion_CondicionTextosInformacion_Flujo
    {
        Ninguno = 0,
        EsIgualA = 1,
        EsDistintoA = 2,
        Contiene = 3,
        NoContiene = 4,
        EmpiezaCon = 5,
        TerminaCon = 6,
        MayorQue = 7,
        MenorQue = 8,
        MayorOIgualQue = 9,
        MenorOIgualQue = 10,
        EsParteDe = 11
    }

    public enum TipoSubElemento_EvaluacionCondicion_Flujo
    {
        Ninguno = 0,
        NumerosElemento = 1,
        CantidadNumerosElemento = 2,
        TextosInformacion = 3,
        NombreElemento = 4,
        TextosInformacionCumplenCondicion = 5,
        TodosTextosInformacionCumplenCondicion = 6,
        Clasificadores = 7,
        ClasificadoresCumplenCondicion = 8,
        TodosClasificadoresCumplenCondicion = 9
    }

    public enum TipoOpcionCantidadNumerosCumplenCondicion
    {
        Ninguno = 0,
        AlMenos1 = 1,
        Todos = 2,
        CantidadDeterminada = 3
    }

    public enum TipoOpcionCantidadDeterminadaNumerosCumplenCondicion
    {
        Ninguno = 0,
        AlMenos = 1,
        ComoMaximo = 2,
        Exactamente = 3
    }

    public enum TipoOpcionSeleccionNumerosElemento_Condicion
    {
        Ninguna = 0,
        PosicionActualEjecucion = 1,
        ConjuntoNumerosOperando = 2,
        TodosNumerosOperando = 5,
        PosicionAnteriorDeActualEjecucion = 3,
        PosicionSiguienteDeActualEjecucion = 4,
        ConjuntoNumerosOperando_PosicionActual = 6,
        TodosNumerosOperando_PosicionActual = 7,
        PosicionPrimeraDeActualEjecucion = 8,
        PosicionSegundaDeActualEjecucion = 9,
        PosicionMitadDeActualEjecucion = 10,
        PosicionPenultimaDeActualEjecucion = 11,
        PosicionUltimaDeActualEjecucion = 12,
    }

    public enum TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion
    {
        Ninguna = 0,
        CantidadFija = 1,
        NumerosOperandoCondicionCumplenCondicion = 2,
        NumerosOperandoValoresCumplenCondicion = 3,
        NumerosOperandoCondicion = 4,
        NumerosOperandoValores = 5
    }

    public enum TipoOperacionEjecucion
    {
        Ninguna = 0,
        OperarTodosJuntos = 1,
        OperarPorSeparado = 2,
        OperarPorFilas = 3,
        OperarPorSeparadoPorFilas = 4
    }

    public enum TipoOpcion_OrdenamientoNumerosSalidas
    {
        Ninguno= 0,
        SegunCondicionesRelacionadas = 1,
        PorNombreYTextosInformacion = 2,
        SoloNombreCantidad = 3,
        SoloTextosInformacionCantidad = 4
    }

    public enum TipoOpcionesFiltroNumeros_NombreCantidad
    {
        Ninguno = 0,
        TodosNumeros = 1,
        NumerosCumplenCondiciones = 2,
        NumerosEnPosiciones = 3
    }

    public enum TipoOpcionCondicionProcesamientoCantidades
    {
        Ninguno = 0,
        InsertarCantidadesSiguientes = 1,
        QuitarCantidadActual = 2,
        MantenerPosicíonActual_Procesamiento = 3,
        DetenerProcesamiento = 4
    }

    public enum TipoOpcionCondicionProcesamientoTextosInformacion
    {
        Ninguno = 0,
        InsertarTextosExistentes = 1,
        EditarTextos = 2,
        QuitarTextos = 3,
        MantenerPosicíonActual_Procesamiento = 4,
        DetenerProcesamiento = 5
    }

    public enum TipoOpcionElementoCondicionProcesamientoCantidades
    {
        Ninguno = 0,
        Operando = 1,
        Resultados = 2,
        OperandosYResultados = 3
    }
    public enum TipoOpcionElementoAccionProcesamientoCantidades
    {
        Ninguno = 0,
        Operando = 1,
        Resultados = 2,
        OperandosYResultados = 3,
        ValorFijo = 4
    }

    public enum TipoOpcionElementoComparar_TextosInformacion
    {
        Ninguno = 0,
        TextosInformacion = 1,
        NumerosElemento = 2,
        CantidadesNumerosElemento = 3,
        CantidadesTextosElemento = 4
    }

    public enum TipoOpcionElementosFijosOperacionPotencia
    {
        Ninguno = 0,
        BaseExponenteOperandos = 1,
        BaseFijaExponenteOperando = 2,
        BaseOperandoExponenteFijo = 3
    }
    public enum TipoOpcionElementosFijosOperacionRaiz
    {
        Ninguno = 0,
        RaizRadicalOperandos = 1,
        RaizFijaRadicalOperando = 2,
        RaizOperandoRadicalFijo = 3
    }
    public enum TipoOpcionElementosFijosOperacionLogaritmo
    {
        Ninguno = 0,
        BaseArgumentoOperandos = 1,
        BaseFijaArgumentoOperando = 2,
        BaseOperandoArgumentoFijo = 3,
        LogaritmoNatural = 4
    }
    public enum TipoOpcionElementosFijosOperacionInverso
    {
        Ninguno = 0,
        InversoSumaResta = 2,
        InversoMultiplicacionDivision = 3
    }

    public enum TipoOpcionElementoAccion_InsertarProcesamientoCantidades
    {
        Ninguno = 0,
        CantidadActual = 1,
        CantidadAnterior = 2,
        CantidadSiguiente = 3,
        CantidadPosicionEspecifica = 4,
        CantidadPosicionEspecificaDesplazadaAnteriores = 5,
        CantidadPosicionEspecificaDesplazadaSiguientes = 6,
        CantidadPosicionEspecificaDesplazadaMultiploAnteriores = 7,
        CantidadPosicionEspecificaDesplazadaMultiploSiguientes = 8,
    }

    public enum TipoOperacion_AlInsertar_ProcesamientoCantidades
    {
        Ninguno = 0,
        InversoSumaResta = 1,
        InversoMultiplicacionDivision = 2,
        RedondearCantidades = 3,
        ContarCantidades = 4,
        Factorial = 5,
        Suma = 6,
        Resta = 7,
        Multiplicacion = 8,
        Division = 9,
        Potencia = 10,
        Raiz = 11,
        Porcentaje = 12,
        Logaritmo = 13,
    }

    public enum TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades
    {
        Ninguno = 0,
        UbicacionCantidadActual = 1,
        UbicacionCantidadAnterior = 2,
        UbicacionCantidadSiguiente = 3,        
        UbicacionEspecifica = 4,
        UbicacionEspecificaDesplazadaAnteriores = 5,
        UbicacionEspecificaDesplazadaSiguientes = 6,
        UbicacionEspecificaDesplazadaMultiploAnteriores = 7,
        UbicacionEspecificaDesplazadaMultiploSiguientes = 8,
        ValorFijo = 9,
    }

    public enum TipoOpcionConfiguracionSeleccionNumeros_Entrada
    {
        Ninguna = 0,
        SeleccionarTodosNumeros = 1,
        SeleccionarCantidadDeterminadaNumeros = 2
    }

    public enum TipoOpcionConfiguracionDefinicionSeleccionarNumeros_Entrada
    {
        Ninguna = 0,
        DefinicionAutomatica = 1,
        DefinicionManual = 2,
        DefinicionAlternada = 3,
        DefinicionAlternada_Condiciones = 4,
        DefinicionAlternada_CondicionesNoCumplen = 5
    }

    public enum TipoTiempoEspera
    {
        Ninguno = 0,
        Segundos = 1,
        Minutos = 2,
        Horas = 3,
        Dias = 4
    }

    public enum TipoOpcionPosicion
    {
        Ninguna = 0,
        PosicionPrimera = 1,
        PosicionSegunda = 2,
        PosicionMitad = 3,
        PosicionPenultima = 4,
        PosicionUltima = 5
    }

    public enum TipoOpcionConfiguracionDefinicionSeleccionarNumeros_PosicionInicial
    {
        Ninguna = 0,
        PosicionInicialFija = 1,
        PosicionInicial_UltimaEjecucion = 2,
        PosicionFinal_UltimaEjecucion = 3
    }

    public enum TipoConfiguracionTraspasoCantidades_ArchivoExterno
    {
        Ninguno = 0,
        ConfiguracionOperador = 1,
        ConfiguracionArchivo = 2
    }

    public enum TipoTraspasoCantidades_ArchivoExterno
    {
        Ninguno = 0,
        UsarElementoConectado = 1,
        UsarEntradaOriginal = 2,
        UsarElementoConectadoEntradaOriginal = 3
    }

    public enum TipoOpcionToolTip
    {
        Ninguno = 0,
        Entrada = 1,
        Operacion = 2,
        ElementoOperacion = 3,
        Calculo = 4
    }

    public enum TipoOperacionCadenaTexto
    {
        Ninguna = 0,
        Mover = 1,
        Copiar = 2,
        Utilizar = 3,
    }
}