using System;
using System.Collections.Generic;
using System.Linq;

namespace Siscom.Entities
{
    public class Enderecos
    {
        public long id { get; set; }

        public string cep { get; set; }

        public string endereco { get; set; }

        public long id_cidades { get; set; }        

        public long bairro_id { get; set; }

        public Cidades cidade_instc { get; set; }

        public override string ToString()
        {
            return this.endereco;
        }

        public static Enderecos ToEnderecos(Object o)
        {
            Enderecos c = (Enderecos)o;
            return c;
        }
    }
}
