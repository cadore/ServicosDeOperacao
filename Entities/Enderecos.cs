using System;
using System.Collections.Generic;
using System.Linq;

namespace Siscom.Entities
{
    public class Enderecos
    {
        public override string ToString()
        {
            return this.endereco;
        }
        public long id { get; set; }

        public string cep { get; set; }

        public string endereco { get; set; }

        public Cidades cidade { get; set; }

        public long bairro_id { get; set; }

    }
}
