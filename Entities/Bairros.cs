﻿using System;
namespace Siscom.Entities
{
    public class Bairros
    {
        public long id { get; set; }

        public string bairro { get; set; }

        public long id_cidades { get; set; }

        public Cidades cidade_instc { get; set; }

        public override string ToString()
        {
            return this.bairro;
        }
        public static Bairros ToCidades(Object o)
        {
            Bairros c = (Bairros) o;
            return c;
        }
    }
}
