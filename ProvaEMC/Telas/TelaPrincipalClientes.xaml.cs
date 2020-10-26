using ProvaEMC.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace ProvaEMC.Telas
{
    /// <summary>
    /// Lógica interna para TelaPrincipalClientes.xaml
    /// </summary>
    public partial class TelaPrincipalClientes : Window
    {
        public TelaPrincipalClientes()
        {
            InitializeComponent();
            LerClientesAsync();
        }

        public async void LerClientesAsync()
        {
            try
            {
                using (AlexProva dBAlexProva = new AlexProva())
                {
                    List<Cliente> lista = new List<Cliente>();

                    lista = await dBAlexProva.Clientes.ToListAsync();

                    foreach (var x in lista)
                    {

                        TabelaView.Items.Add(x);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CadastroClientes cadastroClientes = new CadastroClientes();
            cadastroClientes.Show();
        }

        private void ButtonContatos_Click(object sender, RoutedEventArgs e)
        {
            var cliente = (Cliente)TabelaView.SelectedCells[0].Item;

            TelaContatos telaContatos = new TelaContatos(cliente);
            telaContatos.Show();


        }

        private void ButtonAlterar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Tem certeza que deseja alterar o Cliente?", "Alterar Cliente", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (resultado == MessageBoxResult.Yes)
            {
                var cliente = (Cliente)TabelaView.SelectedCells[0].Item;

                CadastroClientes cadastroClientes = new CadastroClientes(cliente);
                cadastroClientes.Show();

                cadastroClientes.TextNome.Text = cliente.Nome;
                cadastroClientes.TextIdade.Text = cliente.Idade.ToString();
                cadastroClientes.TextCEP.Text = cliente.Endereco.Cep;
                cadastroClientes.TextRua.Text = cliente.Endereco.Rua;
                cadastroClientes.TextNumero.Text = cliente.Endereco.Numero.ToString();
                cadastroClientes.TextComplemento.Text = cliente.Endereco.Complemento;
                cadastroClientes.TextBairro.Text = cliente.Endereco.Bairro;
                cadastroClientes.TextCidade.Text = cliente.Endereco.Cidade;
                cadastroClientes.TextEstado.Text = cliente.Endereco.Estado;

                cadastroClientes.TextContatoNome.IsEnabled = false;
                cadastroClientes.TextContatoNum.IsEnabled = false;
                cadastroClientes.ComboboxTipo.IsEnabled = false;

                this.Close();
            }
        }

        private async void ButtonDeletar_ClickAsync(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Tem certeza que deseja excluir o Cliente?", "Deletar Cliente", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    using(AlexProva dbAlexProva = new AlexProva())
                    {
                        var clienteSelecionado = (Cliente)TabelaView.SelectedCells[0].Item;

                        Cliente clienteaDeletar = await dbAlexProva.Clientes.FindAsync(clienteSelecionado.Id);

                        dbAlexProva.Contatos.RemoveRange(clienteaDeletar.Contato);  //Remove os contatos relacionados 
                        dbAlexProva.Clientes.Remove(clienteaDeletar);  //Remove o cliente

                        
                        await dbAlexProva.SaveChangesAsync();

                        MessageBox.Show("O Cliente foi Deletado", "Cliente Deletado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                this.Hide();
                TelaPrincipalClientes telaPrincipalClientes = new TelaPrincipalClientes();
                telaPrincipalClientes.Show();

            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private async void TextBusca_TextChangedAsync(object sender, TextChangedEventArgs e)
        {

            TabelaView.Items.Clear();
            try
            {
                using (AlexProva dBAlexProva = new AlexProva())
                {
                    List<Cliente> lista = new List<Cliente>();

                    lista = await dBAlexProva.Clientes.ToListAsync();

                    foreach (var x in lista)
                    {
                        if (x.Nome.Contains(TextBusca.Text))
                        {
                            TabelaView.Items.Add(x);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


