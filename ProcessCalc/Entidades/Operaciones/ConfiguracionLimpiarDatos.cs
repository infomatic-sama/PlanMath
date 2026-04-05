namespace ProcessCalc.Entidades
{
    public class ConfiguracionLimpiarDatos
    {
        public bool QuitarDuplicados { get; set; }
        public bool QuitarCantidadesDuplicadas { get; set; }
        public TipoConectorCondiciones_ConjuntoBusquedas Conector1_Duplicados { get; set; }
        public bool QuitarCantidadesTextosDuplicadas { get; set; }
        public TipoConectorCondiciones_ConjuntoBusquedas Conector2_Duplicados { get; set; }
        public bool QuitarCantidadesTextosDentroDuplicados { get; set; }
        public bool QuitarCeros {  get; set; }
        public bool QuitarCerosConTextos {  get; set; }
        public TipoConectorCondiciones_ConjuntoBusquedas Conector1_Ceros { get; set; }
        public bool QuitarCerosSinTextos { get; set; }
        public bool QuitarCantidadesSinTextos { get; set; }
        public bool QuitarNegativas {  get; set; }

        public ConfiguracionLimpiarDatos CopiarObjeto()
        {
            ConfiguracionLimpiarDatos nuevaConfig = new ConfiguracionLimpiarDatos();
            nuevaConfig.QuitarDuplicados = QuitarDuplicados;
            nuevaConfig.QuitarCantidadesDuplicadas = QuitarCantidadesDuplicadas;
            nuevaConfig.Conector1_Duplicados = Conector1_Duplicados;
            nuevaConfig.QuitarCantidadesTextosDuplicadas = QuitarCantidadesTextosDuplicadas;
            nuevaConfig.Conector2_Duplicados = Conector2_Duplicados;
            nuevaConfig.QuitarCantidadesTextosDentroDuplicados = QuitarCantidadesTextosDentroDuplicados;
            nuevaConfig.QuitarCeros = QuitarCeros;
            nuevaConfig.QuitarCerosConTextos = QuitarCerosConTextos;
            nuevaConfig.Conector1_Ceros = Conector1_Ceros;
            nuevaConfig.QuitarCerosSinTextos = QuitarCerosSinTextos;
            nuevaConfig.QuitarCantidadesSinTextos = QuitarCantidadesSinTextos;
            nuevaConfig.QuitarNegativas = QuitarNegativas;

            return nuevaConfig;
        }
    }
}