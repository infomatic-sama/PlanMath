using ProcessCalc.Entidades.Ejecuciones.ToolTips;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc
{
    public class ComparadorObjetos
    {
        public HashSet<RefPair> _paresVisitados = new(new RefPairComparer());
        public bool CompararObjetos<T>(T obj1, T obj2) where T : class
        {

            if (obj1 == null || obj2 == null) return obj1 == obj2;

            // corta ciclos por PAR (obj1,obj2)
            var par = new RefPair(obj1, obj2);
            if (_paresVisitados.Contains(par)) return true;
            _paresVisitados.Add(par);

            if (obj1 == null || obj2 == null)
            {
                return obj1 == obj2;
            }
            else
            {

                Type tipo = typeof(T);
                PropertyInfo[] propiedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => (!Attribute.IsDefined(p, typeof(IgnoreDataMemberAttribute)) &
                    !Attribute.IsDefined(p, typeof(AtributoNoComparar))) ||
                    Attribute.IsDefined(p, typeof(AtributoComparar))).ToArray();

                foreach (var propiedad in propiedades)
                {
                    object valor1 = propiedad.GetValue(obj1);
                    object valor2 = propiedad.GetValue(obj2);

                    if (valor1 == null && valor2 == null)
                        continue;

                    if (valor1 == null || valor2 == null)
                        return false;

                    Type tipoPropiedad = propiedad.PropertyType;

                    // Si es un tipo primitivo, enum, string o struct, comparar directamente
                    if (tipoPropiedad.IsPrimitive || tipoPropiedad.IsEnum || tipoPropiedad == typeof(string) || tipoPropiedad.IsValueType)
                    {
                        if (!valor1.Equals(valor2))
                            return false;
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(tipoPropiedad))
                    {
                        Type tipoElemento = ObtenerTipoElementoColeccion(tipoPropiedad);

                        if (!CompararColecciones((IEnumerable)valor1, (IEnumerable)valor2, tipoElemento))
                            return false;
                    }
                    // Si es un objeto complejo, hacer una comparación recursiva
                    else
                    {
                        bool sonIguales = (bool)typeof(ComparadorObjetos)
                   .GetMethod(nameof(CompararObjetos), BindingFlags.Public | BindingFlags.Instance)
                   .MakeGenericMethod(tipoPropiedad)
                   .Invoke(this, new object[] { valor1, valor2 });

                        if (!sonIguales)
                            return false;
                    }
                }

                return true;
            }
        }

        private bool CompararColecciones(IEnumerable col1, IEnumerable col2, Type tipoPropiedad)
        {
            if (col1 == null || col2 == null) return col1 == col2;

            // Corta ciclos por PAR (col1,col2)
            var par = new RefPair(col1, col2);
            if (_paresVisitados.Contains(par)) return true;
            _paresVisitados.Add(par);

            var lista1 = col1.Cast<object>().ToList();
            var lista2 = col2.Cast<object>().ToList();

            //var lista1 = col1.Cast<object>().ToList().Where(i => !ObjetosComparados.Contains(i)).ToList();
            //var lista2 = col2.Cast<object>().ToList().Where(i => !ObjetosComparados.Contains(i)).ToList();

            if (lista1.Count != lista2.Count)
                return false;

            for (int i = 0; i < lista1.Count; i++)
            {
                object item1 = lista1[i];
                object item2 = lista2[i];

                if (item1 == null && item2 == null)
                    continue;

                if (item1 == null || item2 == null)
                    return false;

                if (tipoPropiedad.IsPrimitive || tipoPropiedad.IsEnum || tipoPropiedad == typeof(string) || tipoPropiedad.IsValueType)
                {
                    if (!item1.Equals(item2))
                        return false;
                }
                else if (typeof(IEnumerable).IsAssignableFrom(item1.GetType()))
                {
                    Type tipoElemento = ObtenerTipoElementoColeccion(item1.GetType());

                    if (!CompararColecciones((IEnumerable)item1, (IEnumerable)item2, tipoElemento))
                        return false;
                }
                else
                {
                    bool sonIguales = (bool)typeof(ComparadorObjetos)
                        .GetMethod(nameof(CompararObjetos), BindingFlags.Public | BindingFlags.Instance)
                        .MakeGenericMethod(item1.GetType())
                        .Invoke(this, new object[] { item1, item2 });

                    if (!sonIguales)
                        return false;
                }
            }

            return true;
        }

        private Type ObtenerTipoElementoColeccion(Type tipoColeccion)
        {
            if (tipoColeccion.IsGenericType)
                return tipoColeccion.GetGenericArguments()[0]; // Obtiene el tipo del elemento si es genérico
            return typeof(object);
        }

        sealed class ReferenceEqualityComparer : IEqualityComparer<object>
        {
            public new bool Equals(object x, object y) => ReferenceEquals(x, y);
            public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }

        // Par de referencias (para "ya comparé este par")
        public readonly struct RefPair
        {
            public readonly object A;
            public readonly object B;
            public RefPair(object a, object b) { A = a; B = b; }
        }

        sealed class RefPairComparer : IEqualityComparer<RefPair>
        {
            public bool Equals(RefPair x, RefPair y) =>
                ReferenceEquals(x.A, y.A) && ReferenceEquals(x.B, y.B);
            public int GetHashCode(RefPair p)
            {
                unchecked
                {
                    int h1 = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(p.A);
                    int h2 = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(p.B);
                    return (h1 * 397) ^ h2;
                }
            }
        }
    }
}
