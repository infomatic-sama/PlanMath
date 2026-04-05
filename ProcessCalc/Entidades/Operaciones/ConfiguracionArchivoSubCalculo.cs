using ProcessCalc.Entidades.Entradas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessCalc.Entidades.Operaciones
{
    public class ConfiguracionSubCalculo
    {
        private bool modSimple;
        public bool ModoSubCalculo_Simple
        {
            get
            {
                return modSimple;
            }
            set
            {
                SubCalculo_Operacion.ModoSubCalculo_Simple = value;
                modSimple = value;
            }
        }
        public bool EjecutarSubCalculoSin_InfoVisual { get; set; }
        public bool EjecutarSubCalculo_MismaEjecucion { get; set; }
        public Calculo SubCalculo_Operacion { get; set; }
        public ConfiguracionSubCalculo()
        {
            SubCalculo_Operacion = new Calculo();
            SubCalculo_Operacion.AgregarCalculoInicial();
            SubCalculo_Operacion.ID = App.GenerarID_Elemento();
            SubCalculo_Operacion.ModoSubCalculo = true;
            EjecutarSubCalculo_MismaEjecucion = true;
        }
        public ConfiguracionSubCalculo CopiarObjeto()
        {
            ConfiguracionSubCalculo config = new ConfiguracionSubCalculo();
            config.EjecutarSubCalculoSin_InfoVisual = EjecutarSubCalculoSin_InfoVisual;
            config.ModoSubCalculo_Simple = ModoSubCalculo_Simple;
            config.EjecutarSubCalculo_MismaEjecucion = EjecutarSubCalculo_MismaEjecucion;
            config.SubCalculo_Operacion = SubCalculo_Operacion;

            return config;
        }
        public List<Entrada> ObtenerEntradas_SubCalculo()
        {
            if (SubCalculo_Operacion != null)
            {
                return SubCalculo_Operacion.ObtenerTodasEntradas();
            }
            else
                return new List<Entrada>();
        }

        public List<Resultado> ObtenerResultados_SubCalculo()
        {
            if (SubCalculo_Operacion != null)
            {
                return SubCalculo_Operacion.ListaResultados.ToList();
            }
            else
                return new List<Resultado>();
        }
    }
}
