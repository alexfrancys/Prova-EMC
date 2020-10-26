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
    /// Lógica interna para TelaPrincipalFornecedores.xaml
    /// </summary>
    public partial class TelaPrincipalFornecedores : Window
    {
        public TelaPrincipalFornecedores()
        {
            InitializeComponent();
            LerFornecedoresAsync();
        }
        public async void LerFornecedoresAsync()
        {
            try
            {
                using (AlexProva dBAlexProva = new AlexProva())
                {
                    List<Fornecedor> lista = new List<Fornecedor>();

                    lista = await dBAlexProva.Fornecedores.ToListAsync();

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
            CadastroFornecedor cadastroFornecedor = new CadastroFornecedor();
            cadastroFornecedor.Show();
        }

        private void ButtonContatos_Click(object sender, RoutedEventArgs e)
        {
            var fornecedor = (Fornecedor)TabelaView.SelectedCells[0].Item;

            TelaContatos telaContatos = new TelaContatos(fornecedor);
            telaContatos.Show();


        }

        private void ButtonAlterar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Tem certeza que deseja alterar o Cliente?", "Alterar Cliente", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (resultado == MessageBoxResult.Yes)
            {
                var fornecedor = (Fornecedor)TabelaView.SelectedCells[0].Item;

                CadastroFornecedor cadastroFornecedor = new CadastroFornecedor(fornecedor);
                cadastroFornecedor.Show();

                cadastroFornecedor.TextNome.Text = fornecedor.Nome;
                cadastroFornecedor.TextPrazoEntrega.Text = fornecedor.PrazoEntrega.ToString();
                cadastroFornecedor.TextCEP.Text = fornecedor.Endereco.Cep;
                cadastroFornecedor.TextRua.Text = fornecedor.Endereco.Rua;
                cadastroFornecedor.TextNumero.Text = fornecedor.Endereco.Numero.ToString();
                cadastroFornecedor.TextComplemento.Text = fornecedor.Endereco.Complemento;
                cadastroFornecedor.TextBairro.Text = fornecedor.Endereco.Bairro;
                cadastroFornecedor.TextCidade.Text = fornecedor.Endereco.Cidade;
                cadastroFornecedor.TextEstado.Text = fornecedor.Endereco.Estado;

                cadastroFornecedor.TextContatoNome.IsEnabled = false;
                cadastroFornecedor.TextContatoNum.IsEnabled = false;
                cadastroFornecedor.ComboboxTipo.IsEnabled = false;

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
                    using (AlexProva dbAlexProva = new AlexProva())
                    {
                        var fornecedorSelecionado = (Fornecedor)TabelaView.SelectedCells[0].Item;

                        Fornecedor fornecedoraDeletar = await dbAlexProva.Fornecedores.FindAsync(fornecedorSelecionado.Id);

                        dbAlexProva.Contatos.RemoveRange(fornecedoraDeletar.Contato);  //Remove os contatos relacionados 
                        dbAlexProva.Fornecedores.Remove(fornecedoraDeletar);  //Remove o cliente


                        await dbAlexProva.SaveChangesAsync();

                        MessageBox.Show("O Fornecedor foi Deletado", "Fornecedor Deletado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                this.Hide();
                TelaPrincipalFornecedores telaPrincipalFornecedores = new TelaPrincipalFornecedores();
                telaPrincipalFornecedores.Show();

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
                    List<Fornecedor> lista = new List<Fornecedor>();

                    lista = await dBAlexProva.Fornecedores.ToListAsync();

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
