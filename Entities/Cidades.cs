using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscom.Entities
{
    public class Cidades
    {
        public override string ToString()
        {
            return this.cidade;
        }
        public long id { get; set; }

        public string cidade { get; set; }

        public string uf { get; set; }

        public string cod_ibge { get; set; }

        public int area { get; set; }

        public List<Bairros> bairros { get; set; }

        public List<Enderecos> enderecos { get; set; }
    }
}
