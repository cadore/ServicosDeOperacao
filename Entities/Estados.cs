﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscom.Entities
{
    public class Estados
    {

        public long id { get; set; }

        public string uf { get; set; }

        public string estado { get; set; }

        public string cod_ibge { get; set; }

        public override string ToString()
        {
            return this.estado;
        }

        public static Estados ToEstados(Object o)
        {
            Estados c = (Estados)o;
            return c;
        }
    }
}
