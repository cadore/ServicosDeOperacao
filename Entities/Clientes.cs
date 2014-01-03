namespace Siscom.Entities
{
    public class Clientes
    {
        public long id { get; set; }

        public string nome { get; set; }

        public string tipo_de_documento { get; set; }

        public string documento { get; set; }

        public string ie { get; set; }

        public bool isento_ICMS { get; set; }

        public string email_principal { get; set; }

        public string email_secundario { get; set; }

        public Enderecos endereco { get; set; }

        public string numero { get; set; }

        public string complemento { get; set; }

        public string cep { get; set; }

        public Bairros bairro { get; set; }

        public Cidades cidade { get; set; }

        public string observacoes { get; set; }

        public string telefone_fixo { get; set; }

        public string telefone_celular { get; set; }

        public bool inativo { get; set; }

        public decimal limite_de_credito { get; set; }

        public bool fornecedor { get; set; }
    }
}
