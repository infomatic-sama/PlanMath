using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class DefinicionTextoNombresCantidades
    {
        public List<OpcionesNombreCantidad_TextosInformacion> OpcionesTextos { get; set; }
        public bool DefinirNombresAntesEjecucion_Elemento { get; set; }
        public bool DefinirNombresDuranteEjecucion_Elemento { get; set; }
        public bool DefinirNombresDespuesEjecucion_Elemento { get; set; }
        public bool DefinirNombresNumeros_Elemento { get; set; }
        public bool DefinirNombres_Elemento { get; set; }
        [IgnoreDataMember]
        public int PosicionActualDefinicion { get; set; }
        public DefinicionTextoNombresCantidades()
        {
            OpcionesTextos = new List<OpcionesNombreCantidad_TextosInformacion>();
            DefinirNombresDuranteEjecucion_Elemento = true;
            DefinirNombresNumeros_Elemento = true;
        }

        public DefinicionTextoNombresCantidades ReplicarObjeto()
        {
            DefinicionTextoNombresCantidades definicion = new DefinicionTextoNombresCantidades();

            definicion.CopiarObjeto(this);
            return definicion;
        }

        public void CopiarObjeto(DefinicionTextoNombresCantidades definicionACopiar)
        {
            this.OpcionesTextos = new List<OpcionesNombreCantidad_TextosInformacion>();
            this.DefinirNombresDuranteEjecucion_Elemento = definicionACopiar.DefinirNombresDuranteEjecucion_Elemento;
            this.DefinirNombresDespuesEjecucion_Elemento = definicionACopiar.DefinirNombresDespuesEjecucion_Elemento;
            this.DefinirNombresAntesEjecucion_Elemento = definicionACopiar.DefinirNombresAntesEjecucion_Elemento;
            this.DefinirNombresNumeros_Elemento = definicionACopiar.DefinirNombresNumeros_Elemento;
            this.DefinirNombres_Elemento = definicionACopiar.DefinirNombres_Elemento;

            foreach (var itemCondicion in definicionACopiar.OpcionesTextos)
            {
                this.OpcionesTextos.Add(new OpcionesNombreCantidad_TextosInformacion());
                this.OpcionesTextos.Last().CopiarObjeto(itemCondicion);
            }
        }
    }
}
