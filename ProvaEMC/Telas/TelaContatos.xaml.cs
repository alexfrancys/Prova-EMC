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
    /// Lógica interna para TelaContatos.xaml
    /// </summary>
    public partial class TelaContatos : Window
    {
        Cliente clienteSelecionado = new Cliente();
        Fornecedor fornecedorSelecionado = new Fornecedor();

        public TelaContatos()
        {
            InitializeComponent();

        }

        public TelaContatos(Cliente x)
        {
            InitializeComponent();
            LerContatosCliente(x);
            clienteSelecionado = x;
            fornecedorSelecionado = null;
        }

        public TelaContatos(Fornecedor x)
        {
            InitializeComponent();
            LerContatosFornecedor(x);
            fornecedorSelecionado = x;
            clienteSelecionado = null;
        }


        public async void LerContatosCliente(Cliente cliente)
        {
            try
            {
                using (AlexProva dBAlexProva = new AlexProva())
                {

                    var listaContatos = await dBAlexProva.Contatos.Where(x => x.Cliente.Id == cliente.Id).ToListAsync();

                    foreach (var x in listaContatos)
                    {
                        DataGridContatos.Items.Add(x);

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void LerContatosFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using (AlexProva dBAlexProva = new AlexProva())
                {

                    var listaContatos = await dBAlexProva.Contatos.Where(x => x.Fornecedor.Id == fornecedor.Id).ToListAsync();

                    foreach (var x in listaContatos)
                    {

                        DataGridContatos.Items.Add(x);

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

            if (fornecedorSelecionado == null)
            {
                TelaAdicionarContato telaAdicionarContato = new TelaAdicionarContato(clienteSelecionado);
                telaAdicionarContato.Show();
            }
            else
            {
                TelaAdicionarContato telaAdicionarContato = new TelaAdicionarContato(fornecedorSelecionado);
                telaAdicionarContato.Show();
            }

        }

        private async void ButtonDeletar_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
               
                    using (AlexProva dbAlexProva = new AlexProva())
                    {
                        var idContato = (Contato)DataGridContatos.SelectedCells[0].Item;

                        var contatoSelecionado = await dbAlexProva.Contatos.Where(x => x.ContatoId == idContato.ContatoId).FirstOrDefaultAsync();                       

                        dbAlexProva.Contatos.Remove(contatoSelecionado);

                        await dbAlexProva.SaveChangesAsync();

                        MessageBox.Show("O Contato foi Deletado", "Contato Deletado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }            
   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}

