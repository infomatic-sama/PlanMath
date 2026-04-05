using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class ConfiguracionRedondearCantidades
    {
        public bool RedondearPar_Cercano {  get; set; }
        public bool RedondearNumero_LejanoDeCero { get; set; }
        public bool RedondearNumero_CercanoDeCero { get; set; }
        public bool RedondearNumero_Mayor { get; set; }
        public bool RedondearNumero_Menor { get; set; }
        public ConfiguracionRedondearCantidades CopiarObjeto()
        {
            ConfiguracionRedondearCantidades nuevaConfig = new ConfiguracionRedondearCantidades();
            nuevaConfig.RedondearPar_Cercano = RedondearPar_Cercano;
            nuevaConfig.RedondearNumero_LejanoDeCero = RedondearNumero_LejanoDeCero;
            nuevaConfig.RedondearNumero_CercanoDeCero = RedondearNumero_CercanoDeCero;
            nuevaConfig.RedondearNumero_Mayor = RedondearNumero_Mayor;
            nuevaConfig.RedondearNumero_Menor = RedondearNumero_Menor;

            return nuevaConfig;
        }
    }
}
