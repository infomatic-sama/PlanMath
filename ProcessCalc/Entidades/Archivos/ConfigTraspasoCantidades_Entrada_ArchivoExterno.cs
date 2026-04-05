using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Archivos
{
    public class ConfigTraspasoCantidades_Entrada_ArchivoExterno
    {
        public TipoConfiguracionTraspasoCantidades_ArchivoExterno Tipo {  get; set; }
        public TipoTraspasoCantidades_ArchivoExterno TipoTraspasoCantidades {  get; set; }
        public bool UsarConfiguracionOpuesta {  get; set; }

        public ConfigTraspasoCantidades_Entrada_ArchivoExterno()
        {

        }
        public ConfigTraspasoCantidades_Entrada_ArchivoExterno(
            TipoConfiguracionTraspasoCantidades_ArchivoExterno tip)
        {
            Tipo = tip;

            switch(Tipo)
            {
                case TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionOperador:
                    TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectado;
                    break;

                case TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionArchivo:
                    TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal;
                    UsarConfiguracionOpuesta = true;
                    break;
            }
            
        }

        public ConfigTraspasoCantidades_Entrada_ArchivoExterno ReplicarObjeto()
        {
            ConfigTraspasoCantidades_Entrada_ArchivoExterno config = new ConfigTraspasoCantidades_Entrada_ArchivoExterno();
            config.Tipo = Tipo;
            config.TipoTraspasoCantidades = TipoTraspasoCantidades;
            config.UsarConfiguracionOpuesta = UsarConfiguracionOpuesta;

            return config;
        }
    }
}
