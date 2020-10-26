using ProvaEMC.Telas;
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

namespace ProvaEMC
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TelaPrincipalClientes telaPrincipalClientes = new TelaPrincipalClientes();
            telaPrincipalClientes.Show();
        }

        private void Fornecedores_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TelaPrincipalFornecedores telaPrincipalFornecedores = new TelaPrincipalFornecedores();
            telaPrincipalFornecedores.Show();
        }
    }
}
