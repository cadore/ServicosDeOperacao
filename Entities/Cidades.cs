using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscom.Entities
{
    public class Cidades
    {
        public long id { get; set; }

        public string cidade { get; set; }

        public string uf { get; set; }

        public string cod_ibge { get; set; }

        public int area { get; set; }

        public List<Bairros> bairros { get; set; }

        public List<Enderecos> enderecos { get; set; }

        public override string ToString()
        {
            return this.cidade;
        }

        public static Cidades ToCidades(Object o)
        {
            Cidades c = (Cidades)o;
            return c;
        }
    }
}
