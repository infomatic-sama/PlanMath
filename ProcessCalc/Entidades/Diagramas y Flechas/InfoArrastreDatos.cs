using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ProcessCalc.Entidades
{
    public class InfoArrastreDatos
    {
        public int IndiceCaracterDatos = -1;
        [NonSerialized] public Rect Position;
        public string IdImagen;
        [NonSerialized] public int Indice;
        //[NonSerialized] public int IndiceCaracterDatosAnterior = -1;
        public bool TextoInformacion;

        public InfoArrastreDatos()
        {
            IndiceCaracterDatos = -1;
        }
    }
}
