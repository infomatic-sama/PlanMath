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
    /// Lógica de interacción para ConjuntoCeldasTextosInformacion_Excel.xaml
    /// </summary>
    public partial class ConjuntoCeldasTextosInformacion_Excel : UserControl
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

                entr = value;
            }
        }

        private AsignacionTextoInformacion asig;
        public AsignacionTextoInformacion ParametrosAsignacion
        {
            get
            {
                return asig;
            }
            set
            {
                celdaNumero.Text = value.CeldaNumero;
                celdaTextoInformacion.Text = value.CeldaTextoInformacion;

                asig = value;
            }
        }
        public ConjuntoCeldasExcel Vista { get; set; }
        public ConjuntoCeldasTextosInformacion_Excel()
        {
            InitializeComponent();
        }

        private void celdaNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParametrosAsignacion != null)
                ParametrosAsignacion.CeldaNumero = celdaNumero.Text;
        }

        private void celdaTextoInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParametrosAsignacion != null)
                ParametrosAsignacion.CeldaTextoInformacion = celdaTextoInformacion.Text;
        }

        private void quitarConjunto_Click(object sender, RoutedEventArgs e)
        {
            if (ParametrosExcel != null && ParametrosAsignacion != null)
            {
                if (Vista != null)
                {
                    ParametrosExcel.Asignaciones_TextosInformacion.Remove(ParametrosAsignacion);
                    Vista.conjuntosCeldasTextosInformacion.Children.Remove(this);
                }
            }
        }
    }
}
