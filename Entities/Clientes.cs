using System;
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

        public long id_cidades { get; set; }

        public string numero { get; set; }

        public string complemento { get; set; }

        public string cep { get; set; }

        public long id_enderecos { get; set; }

        public long id_bairros { get; set; }

        public string observacoes { get; set; }

        public string telefone_fixo { get; set; }

        public string telefone_celular { get; set; }

        public bool inativo { get; set; }

        public decimal limite_de_credito { get; set; }

        public Enderecos endereco_instc { get; set; }
        public Bairros bairro_instc { get; set; }
        public Cidades cidade_instc { get; set; }

        public override string ToString()
        {
            return this.nome;
        }
        public static Clientes ToClientes(Object o)
        {
            Clientes c = (Clientes)o;
            return c;
        }
    }
}
