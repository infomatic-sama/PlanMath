using ProcessCalc.Entidades.Entradas;
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
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Config_CredencialesFTP.xaml
    /// </summary>
    public partial class Config_CredencialesFTP : Window
    {
        private CredencialesFTP config;
        public CredencialesFTP Configuracion
        {
            set
            {
                chkUsuarioAnonimo.IsChecked = value.UsuarioAnonimo;
                txtUsuario.Text = value.NombreUsuario;
                txtClave.Password = value.Clave;

                config = value;
            }

            get
            {
                return config;
            }
        }
        
        public Config_CredencialesFTP()
        {
            InitializeComponent();
        }

        private void chkUsuarioAnonimo_Checked(object sender, RoutedEventArgs e)
        {
            if (config != null)
                config.UsuarioAnonimo = true;

            txtUsuario.IsEnabled = false;
            txtClave.IsEnabled = false;
        }

        private void chkUsuarioAnonimo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (config != null)
                config.UsuarioAnonimo = false;

            txtUsuario.IsEnabled = true;
            txtClave.IsEnabled = true;
        }

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(config != null)
                config.NombreUsuario = txtUsuario.Text;
        }

        private void txtClave_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (config != null)
                config.Clave = txtClave.Password;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
