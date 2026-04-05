using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class OrdenOperando_Elemento
    {
        public DiseñoElementoOperacion ElementoPadre { get; set; }
        public int Orden { get; set; }
    }
}
