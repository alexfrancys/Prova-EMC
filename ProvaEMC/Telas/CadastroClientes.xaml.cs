using ProvaEMC.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class CadastroClientes : Window
    {
        Cliente clienteSelecionado = new Cliente();        

        public CadastroClientes()
        {
            InitializeComponent();
            clienteSelecionado = null;
        }

        public CadastroClientes(Cliente cliente)
        {
            InitializeComponent();
            clienteSelecionado = cliente;
        }


        public int ValidarIdade(DatePicker dataNascimento)
        {
            int idade;

            if (DateTime.Today.Month >= dataNascimento.SelectedDate.Value.Month && DateTime.Today.Day >= dataNascimento.SelectedDate.Value.Day)
                idade = DateTime.Today.Year - dataNascimento.SelectedDate.Value.Year;
            else
                idade = DateTime.Today.Year - dataNascimento.SelectedDate.Value.Year - 1;

            return idade;
        }


        private async void ButtonCadastrar_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {

                using (AlexProva dBalexProva = new AlexProva())
                {
                    if (clienteSelecionado == null) //Valida se é uma nova criação de cliente ou se é uma atualização
                    {
                        
                        Endereco endereco = new Endereco
                        {
                            Rua = TextRua.Text.Trim(),
                            Numero = int.Parse(TextNumero.Text.Trim()),
                            Cep = TextCEP.Text.Trim(),
                            Complemento = TextComplemento.Text.Trim(),
                            Bairro = TextBairro.Text.Trim(),
                            Cidade = TextCidade.Text.Trim(),
                            Estado = TextEstado.Text.Trim(),
                        };

                        Contato contato = new Contato
                        {

                            Nome = TextContatoNome.Text.Trim(),
                            Numero = TextContatoNum.Text.Trim(),
                            Tipo = ComboboxTipo.Text.Trim(),                         

                        };

                        List<Contato> listContatos = new List<Contato>
                    {
                        contato
                    };

                        Cliente cliente = new Cliente
                        {
                            Nome = TextNome.Text.ToString(),
                            Idade = int.Parse(TextIdade.Text),
                            Endereco = endereco,
                            Contato = listContatos,
                            
                        };

                        dBalexProva.Clientes.Add(cliente);
                        await dBalexProva.SaveChangesAsync();


                        MessageBox.Show("Cliente Cadastrado.", "Cliente Cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        var clienteBanco = await dBalexProva.Clientes.Where<Cliente>(x => x.Id.ToString() == clienteSelecionado.Id.ToString()).FirstOrDefaultAsync();

                        clienteBanco.Nome =  TextNome.Text.Trim();
                        clienteBanco.Idade = int.Parse(TextIdade.Text.Trim());
                        clienteBanco.Endereco.Cep = TextCEP.Text.Trim();
                        clienteBanco.Endereco.Rua = TextRua.Text.Trim();
                        clienteBanco.Endereco.Numero = int.Parse(TextNumero.Text.Trim());
                        clienteBanco.Endereco.Complemento =TextComplemento.Text.Trim();
                        clienteBanco.Endereco.Bairro = TextBairro.Text.Trim();
                        clienteBanco.Endereco.Cidade = TextCidade.Text.Trim();
                        clienteBanco.Endereco.Estado = TextEstado.Text.Trim();

                        await dBalexProva.SaveChangesAsync();

                        MessageBox.Show("Cliente Alterado com sucesso", "Cliente Alterado", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            TelaPrincipalClientes TelaPrincipal = new TelaPrincipalClientes();
            TelaPrincipal.Show();
            this.Close();

        }


        private void ButtonBuscarCEP_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(TextCEP.Text))
            {
                using (var ws = new WSCorreios.AtendeClienteClient())
                {
                    try
                    {
                        var endereco = ws.consultaCEP(TextCEP.Text.Trim());

                        TextEstado.Text = endereco.uf;
                        TextCidade.Text = endereco.cidade;
                        TextBairro.Text = endereco.bairro;
                        TextRua.Text = endereco.end;
                        TextComplemento.Text = endereco.complemento2;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Informe um CEP válido...", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void TextDataNascimento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TextIdade.Text = ValidarIdade(TextDataNascimento).ToString();
        }


        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Tem certeza que deseja cancelar o Cadastro?", "Cancelar Cadastro", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (resultado == MessageBoxResult.Yes)
            {
                this.Hide();
                TelaPrincipalClientes telaPrincipalClientes = new TelaPrincipalClientes();
                telaPrincipalClientes.Show();
            }
        }
    }

}
