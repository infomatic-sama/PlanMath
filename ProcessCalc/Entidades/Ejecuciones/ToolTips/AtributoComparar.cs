using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Ejecuciones.ToolTips
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class AtributoNoComparar : Attribute
    {
        public AtributoNoComparar()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class AtributoComparar : Attribute
    {
        public AtributoComparar()
        {
        }
    }
}
