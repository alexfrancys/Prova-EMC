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
    /// Lógica interna para TelaAdicionarContato.xaml
    /// </summary>
    public partial class TelaAdicionarContato : Window
    {
        Cliente clienteTabela = new Cliente();
        Fornecedor fornecedorTabela = new Fornecedor();
        public TelaAdicionarContato()
        {
            InitializeComponent();
        }

        public TelaAdicionarContato(Cliente contato)
        {
            InitializeComponent();
            clienteTabela = contato;
            fornecedorTabela = null;
        }

        public TelaAdicionarContato(Fornecedor contato)
        {
            InitializeComponent();
            clienteTabela = null;
            fornecedorTabela = contato;
        }


        private async void ButtonCadastrar_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fornecedorTabela == null)
                {
                    using (AlexProva dBAlexProva = new AlexProva())
                    {
                        var cliente = await dBAlexProva.Clientes.Where<Cliente>(x => x.Id == clienteTabela.Id).FirstOrDefaultAsync();

                        Contato contato = new Contato
                        {
                            Nome = TextNome.Text.Trim(),
                            Numero = TextNumTel.Text.Trim(),
                            Tipo = TextTipo.Text.Trim(),
                            Cliente = cliente,

                        };

                        dBAlexProva.Contatos.Add(contato);
                        await dBAlexProva.SaveChangesAsync();
                    }
                    MessageBox.Show("Contato Cadastrado.", "Contato Cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    using (AlexProva dBAlexProva = new AlexProva())
                    {
                        var fornecedor = await dBAlexProva.Fornecedores.Where<Fornecedor>(x => x.Id == fornecedorTabela.Id).FirstOrDefaultAsync();

                        Contato contato = new Contato
                        {
                            Nome = TextNome.Text.Trim(),
                            Numero = TextNumTel.Text.Trim(),
                            Tipo = TextTipo.Text.Trim(),
                            Fornecedor = fornecedor,

                        };

                        dBAlexProva.Contatos.Add(contato);
                        await dBAlexProva.SaveChangesAsync();
                    }
                    MessageBox.Show("Contato Cadastrado.", "Contato Cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();

            if (fornecedorTabela == null)
            {
                TelaContatos telaContatos = new TelaContatos(clienteTabela);
                telaContatos.Show();
            }
            else
            {
                TelaContatos telaContatos = new TelaContatos(fornecedorTabela);
                telaContatos.Show();
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Tem certeza que deseja cancelar o Cadastro?", "Cancelar Cadastro", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (resultado == MessageBoxResult.Yes)
            {
                this.Hide();

                if (fornecedorTabela == null)
                {
                    TelaContatos telaContatos = new TelaContatos(clienteTabela);
                    telaContatos.Show();
                }
                else
                {
                    TelaContatos telaContatos = new TelaContatos(fornecedorTabela);
                    telaContatos.Show();
                }
            }
        }
    }
}
