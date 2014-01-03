using Siscom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBairrosDAO
    {
        Bairros recuperarPorId(long idCC);

        List<Bairros> recuperarTodos();
    }
}
