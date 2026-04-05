using ProcessCalc.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PlanMath_para_Excel.Entradas;

namespace ProcessCalc.Controles.Ctrl_Entradas
{
    /// <summary>
    /// Lógica de interacción para ConjuntoCeldasExcel.xaml
    /// </summary>
    public partial class ConjuntoCeldasExcel : UserControl
    {
        private Entrada_Desde_Excel entr;
        public Entrada_Desde_Excel ParametrosExcel 
        {
            get 
            {
                return entr;
            }
            set 
            {
                hoja.Text = value.Hoja;
                celdas.Text = value.Celdas;

                conjuntosCeldasTextosInformacion.Children.Clear();
                foreach (var itemConjuntoCeldas in value.Asignaciones_TextosInformacion)
                {
                    ConjuntoCeldasTextosInformacion_Excel conjunto = new ConjuntoCeldasTextosInformacion_Excel();
                    conjuntosCeldasTextosInformacion.Children.Add(conjunto);
                    conjunto.ParametrosAsignacion = itemConjuntoCeldas;
                    conjunto.ParametrosExcel = value;
                    conjunto.Vista = this;
                }

                entr = value;
            }
        }

        public VistaArchivoExcelEntradaConjuntoNumeros Vista { get; set; }
        public VistaArchivoExcelEntradaTextosInformacion Vista_TextosInformacion { get; set; }
        public ConjuntoCeldasExcel()
        {
            InitializeComponent();
        }

        private void hoja_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParametrosExcel != null)
                ParametrosExcel.Hoja = hoja.Text;
        }

        private void celdas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParametrosExcel != null)
                ParametrosExcel.Celdas = celdas.Text;
        }

        private void quitarConjunto_Click(object sender, RoutedEventArgs e)
        {
            if (Vista != null)
            {
                if (Vista.Entrada != null)
                {
                    Vista.Entrada.ParametrosExcel.Remove(ParametrosExcel);
                    Vista.conjuntosCeldas.Children.Remove(this);
                }
            }
            else if (Vista_TextosInformacion != null)
            {
                if (Vista_TextosInformacion.Entrada != null)
                {
                    Vista_TextosInformacion.Entrada.ParametrosExcel.Remove(ParametrosExcel);
                    Vista_TextosInformacion.conjuntosCeldas.Children.Remove(this);
                }
            }
        }
    }
}
