using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Archivos;
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

namespace ProcessCalc.Controles.Archivos
{
    /// <summary>
    /// Lógica de interacción para ConfigEntradas_ArchivoExterno.xaml
    /// </summary>
    public partial class ConfigEntradas_ArchivoExterno : UserControl
    {
        public string NombreEntrada { get; set; }
        public string IDEntrada { get; set; }
        public DiseñoOperacion Operando {  get; set; }
        public DiseñoElementoOperacion SubOperando { get; set; }
        private ConfigTraspasoCantidades_Entrada_ArchivoExterno configCantidades;
        public ConfigTraspasoCantidades_Entrada_ArchivoExterno Config
        {
            get
            { return configCantidades; }
            set 
            {
                opcionUsarElementoConectado.GroupName = "Opciones-" + IDEntrada + this.GetHashCode().ToString();
                opcionUsarEntradaOriginal.GroupName = "Opciones-" + IDEntrada + this.GetHashCode().ToString();
                opcionUsarElementoConectadoEntradaOriginal.GroupName = "Opciones-" + IDEntrada + this.GetHashCode().ToString();

                if (value.Tipo == TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionOperador)
                {
                    textoConfiguracionOpuesta.Text = "del archivo que es ejecutado por este operador.";
                }
                else if(value.Tipo == TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionArchivo)
                {
                    textoConfiguracionOpuesta.Text = "del operador que ejecuta este archivo.";
                }

                opcionUsarEstaConfiguracion.IsChecked = value.UsarConfiguracionOpuesta;

                switch (value.TipoTraspasoCantidades)
                {
                    case TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectado:
                        opcionUsarElementoConectado.IsChecked = true;
                        break;

                    case TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal:
                        opcionUsarEntradaOriginal.IsChecked = true;
                        break;

                    case TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectadoEntradaOriginal:
                        opcionUsarElementoConectadoEntradaOriginal.IsChecked= true;
                        break;
                }
                configCantidades = value; 
            }
        }
        public ConfigEntradas_ArchivoExterno()
        {
            InitializeComponent();
        }

        private void opcionUsarElementoConectado_Checked(object sender, RoutedEventArgs e)
        {
            if(Config != null)
            {
                Config.TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectado;
            }
        }

        private void opcionUsarEntradaOriginal_Checked(object sender, RoutedEventArgs e)
        {
            if (Config != null)
            {
                Config.TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarEntradaOriginal;
            }
        }

        private void opcionUsarElementoConectadoEntradaOriginal_Checked(object sender, RoutedEventArgs e)
        {
            if (Config != null)
            {
                Config.TipoTraspasoCantidades = TipoTraspasoCantidades_ArchivoExterno.UsarElementoConectadoEntradaOriginal;
            }
        }

        private void opcionUsarEstaConfiguracion_Checked(object sender, RoutedEventArgs e)
        {
            if(Config != null)
            {
                Config.UsarConfiguracionOpuesta = (bool)opcionUsarEstaConfiguracion.IsChecked;
            }
        }

        private void opcionUsarEstaConfiguracion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Config != null)
            {
                Config.UsarConfiguracionOpuesta = (bool)opcionUsarEstaConfiguracion.IsChecked;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Operando != null)
            {
                nombreEntradaOperacion.Text = "Entrada " + NombreEntrada + " asociada a la operación " + Operando.NombreCombo;
            }
            else if(SubOperando != null)
            {
                nombreEntradaOperacion.Text = "Entrada " + NombreEntrada + " asociada a la operación " + SubOperando.NombreCombo;
            }
            else
            {
                nombreEntradaOperacion.Text = "Entrada " + NombreEntrada + " asociada a la operación " +
                    "del archivo de cálculo que ejecuta este archivo";
            }
        }
    }
}
