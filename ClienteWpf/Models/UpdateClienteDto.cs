namespace ClienteWpf.Models
{
    public  class UpdateClienteDto
    {
        public UpdateClienteDto(string nome, string cpf, string sexo, int idTipoCliente, int idSituacao)
        {
            Nome = nome;
            Cpf = cpf;
            Sexo = sexo;
            IdTipoCliente = idTipoCliente;
            IdSituacao = idSituacao;
        }
        public UpdateClienteDto(ClienteDto cliente)
        {
            Nome = cliente.Nome;
            Cpf = cliente.Cpf;
            Sexo = cliente.Sexo;
            if (cliente.tipoCliente!=null)
            {
                IdTipoCliente = cliente.tipoCliente.IdTipoCliente;
            }
            if (cliente.Situacao != null)
            {
                IdSituacao = cliente.Situacao.IdSituacao;
            }

        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Sexo { get; set; }
        public int IdTipoCliente { get; set; }
        public int IdSituacao { get; set; }
    }
}
