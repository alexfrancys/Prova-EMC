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
    /// Lógica interna para CadastroFornecedor.xaml
    /// </summary>
    public partial class CadastroFornecedor : Window
    {
        Fornecedor fornecedorSelecionado = new Fornecedor();
        public CadastroFornecedor()
        {
            InitializeComponent();
            fornecedorSelecionado = null;
        }

        public CadastroFornecedor(Fornecedor fornecedor)
        {
            InitializeComponent();
            fornecedorSelecionado = fornecedor;
        }



        private async void ButtonCadastrar_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {

                using (AlexProva dBalexProva = new AlexProva())
                {
                    if (fornecedorSelecionado == null) //Valida se é uma nova criação do Fornecedor ou se é uma atualização
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

                        Fornecedor fornecedor = new Fornecedor
                        {
                            Nome = TextNome.Text.ToString(),
                            PrazoEntrega = int.Parse(TextPrazoEntrega.Text.Trim()),
                            Endereco = endereco,
                            Contato = listContatos,
                        };

                        dBalexProva.Fornecedores.Add(fornecedor);
                        await dBalexProva.SaveChangesAsync();


                        MessageBox.Show("Fornecedor Cadastrado.", "Fornecedor Cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        var fornecedorBanco = await dBalexProva.Fornecedores.Where<Fornecedor>(x => x.Id.ToString() == fornecedorSelecionado.Id.ToString()).FirstOrDefaultAsync();

                        fornecedorBanco.Nome = TextNome.Text.Trim();
                        fornecedorBanco.PrazoEntrega = int.Parse(TextPrazoEntrega.Text.Trim());
                        fornecedorBanco.Endereco.Cep = TextCEP.Text.Trim();
                        fornecedorBanco.Endereco.Rua = TextRua.Text.Trim();
                        fornecedorBanco.Endereco.Numero = int.Parse(TextNumero.Text.Trim());
                        fornecedorBanco.Endereco.Complemento = TextComplemento.Text.Trim();
                        fornecedorBanco.Endereco.Bairro = TextBairro.Text.Trim();
                        fornecedorBanco.Endereco.Cidade = TextCidade.Text.Trim();
                        fornecedorBanco.Endereco.Estado = TextEstado.Text.Trim();

                        await dBalexProva.SaveChangesAsync();

                        MessageBox.Show("Fornecedor alterado com sucesso", "Fornecedor Alterado", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            TelaPrincipalFornecedores telaPrincipalFornecedores = new TelaPrincipalFornecedores();
            telaPrincipalFornecedores.Show();
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


        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Tem certeza que deseja cancelar o Cadastro?", "Cancelar Cadastro", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (resultado == MessageBoxResult.Yes)
            {
                this.Hide();
                TelaPrincipalFornecedores telaPrincipalFornecedores = new TelaPrincipalFornecedores();
                telaPrincipalFornecedores.Show();
            }
        }
    }

}

