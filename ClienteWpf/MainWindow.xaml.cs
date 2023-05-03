using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
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
using ClienteWpf.Models;
using ClienteWpf.ProvaRemotaRequests;

namespace ClienteWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            var listaSexo = new List<string>() { "Feminino", "Masculino", "Outros" };

            Sexo.ItemsSource = listaSexo;
        }

        async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var tipos = await ProvaRemotaApiRequests.ObterTipos();
                var situacoes = await ProvaRemotaApiRequests.ObterSituacoes();

                await AtualizarClientesAsync();

                TipoCliente.ItemsSource = tipos;
                Situacao.ItemsSource = situacoes;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados da API: " + ex.Message);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TabelaClientes.SelectedItem != null)
            {
                if (TabelaClientes.SelectedItem is ClienteDto)
                {
                    PopularDadosCampos((ClienteDto)TabelaClientes.SelectedItem);
                }
            }

        }

        private void IdCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtnDeletar.IsEnabled = IdCliente.Text.Trim().Length > 0;

        }

        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
        }

        private void BtnDeletar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Tem Certeza que deseja excluir cliente " + NomeCliente.Text + " ?",
                "Confirmação de exclusão", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    int.TryParse(IdCliente.Text, out int idCliente);
                    var resultado = ProvaRemotaApiRequests.DeletarCliente(idCliente);
                    if (resultado.Result)
                    {
                        Task task = AtualizarClientesAsync();
                        MessageBox.Show("Exclusão efetuada com sucesso");
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            ClienteDto clienteDto = new ClienteDto();
            int.TryParse(IdCliente.Text, out int idCliente);
            clienteDto.IdCliente = idCliente;
            clienteDto.Cpf = Cpf.Text;
            clienteDto.Sexo = Sexo.Text;
            clienteDto.Nome = NomeCliente.Text;
            if (TipoCliente.SelectedValue != null)
            {
                int.TryParse(TipoCliente.SelectedValue.ToString(), out int IdTipoCliente);
                clienteDto.tipoCliente = new TipoClienteDto
                {
                    IdTipoCliente = IdTipoCliente,
                    Nome = TipoCliente.SelectedValue.ToString()
                };
            }
            if (Situacao.SelectedValue != null)
            {
                int.TryParse(Situacao.SelectedValue.ToString(), out int IdSituacao);
                clienteDto.Situacao = new SituacaoDto
                {
                    IdSituacao = IdSituacao,
                    NomeSituacao = Situacao.SelectedValue.ToString(),
                    SiglaSituacao = Situacao.SelectedValue.ToString()[0].ToString()
                };
            }

            var context = new ValidationContext(clienteDto, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();


            var isValid = Validator.TryValidateObject(clienteDto, context, results, true);
            StringBuilder mensagensErros = new StringBuilder();
            if (!isValid)
            {
                mensagensErros.AppendLine("Campos inválidos!");
                foreach (var result in results)
                {
                    mensagensErros.Append(result.ToString());
                    mensagensErros.Append(Environment.NewLine);
                }
                MessageBox.Show(mensagensErros.ToString());
            }

            if (clienteDto.IdCliente == 0)
            {
                var clienteAdicionado = await ProvaRemotaApiRequests.AdicionaClienteNovo(clienteDto);
                var mensagem = "";
                if (clienteAdicionado != null)
                {
                    mensagem = "Cliente " + clienteAdicionado.Nome + " Adicionado com sucesso!";
                    Task task = AtualizarClientesAsync();
                }
                else
                {
                    mensagem = "Erro Ao incluir Cliente! Verifique se CPF já foi cadastrado";
                }
                MessageBox.Show(mensagem);

            }
            else
            {
                var updateCliente = new UpdateClienteDto(clienteDto);
                var resultado = ProvaRemotaApiRequests.AlteraCliente(clienteDto.IdCliente, updateCliente);
                var mensagem = "";
                if (resultado.Result)
                {
                    mensagem = "Cliente " + updateCliente.Nome + " Alterado com sucesso!";
                    Task task = AtualizarClientesAsync();
                }
                else
                {
                    mensagem = "Erro Ao atualizar Cliente!";
                }
                MessageBox.Show(mensagem);
            }

        }

        private void LimparCampos()
        {
            Cpf.Text = "";
            NomeCliente.Text = "";
            Sexo.SelectedIndex = 0;
            TipoCliente.SelectedIndex = 0;
            Situacao.SelectedIndex = 0;
            IdCliente.Text = "";
            BtnDeletar.IsEnabled = IdCliente.Text.Trim().Length > 0;

        }
        private async Task AtualizarClientesAsync()
        {
            var clientes = await ProvaRemotaApiRequests.ObterClientes();
            TabelaClientes.ItemsSource = clientes;

            LimparCampos();
        }

        private void PopularDadosCampos(ClienteDto cliente)
        {
            if (cliente != null)
            {
                NomeCliente.Text = cliente.Nome;
                Cpf.Text = cliente.Cpf;
                Sexo.SelectedValue = cliente.Sexo;
                IdCliente.Text = cliente.IdCliente.ToString();
                if (cliente.tipoCliente != null)
                    TipoCliente.SelectedValue = cliente.tipoCliente.IdTipoCliente;

                if (cliente.Situacao != null)
                    Situacao.SelectedValue = cliente.Situacao.IdSituacao;
            }
        }
    }
}
