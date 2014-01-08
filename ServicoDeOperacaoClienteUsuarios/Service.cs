using Repository;
using Siscom.Entities;
using System.Collections.Generic;
namespace ServicoDeOperacaoClienteUsuarios
{    
    public class Service : IService
    {

        #region Metodos clifor
        public List<Clientes> retornaTodosCliFor()
        {
            return new ClientesDAO().recuperarTodos();
        }
        public List<Clientes> retornaCliForPorNome(string value)
        {
            return new ClientesDAO().recuperarPorNome(value);
        }
        public List<Clientes> retornaCliForPorDocumento(string value)
        {
            return new ClientesDAO().recuperarPorDocumento(value);
        }
        public Clientes retornaPorId(long value)
        {
            return new ClientesDAO().recuperarPorId(value);
        }
        public int salvar(Clientes c)
        {
            return new ClientesDAO().salvar(c);
        }

        public int countClassClientes(string value, string coluna)
        {
            return new ClientesDAO().count(value, coluna);
        }
        #endregion

        #region Metodos cidades
        public List<Cidades> recuperarTodasAsCidades()
        {
            return new CidadesDAO().recuperarTodos();
        }

        public Cidades recuperarCidadePorId(long value)
        {
            return new CidadesDAO().recuperarPorId(value);
        }

        public Cidades recuperarCidadePorNome(string value)
        {
            return new CidadesDAO().recuperarPorNome(value);
        }
        #endregion

        #region Metodos bairros
        public List<Bairros> recuperarTodosBairros()
        {
            return new BairrosDAO().recuperarTodos();
        }

        public Bairros recuperarBairroPorId(long value)
        {
            return new BairrosDAO().recuperarPorId(value);
        }

        public List<Bairros> recuperarBairroPorIdCidade(long value)
        {
            return new BairrosDAO().recuperarPorIdCidade(value);
        }
        #endregion

        #region Metodos enderecos
        public List<Enderecos> recuperarTodosEnderecos()
        {
            return new EnderecosDAO().recuperarTodos();
        }

        public Enderecos recuperarEnderecoPorId(long value)
        {
            return new EnderecosDAO().recuperarPorId(value);
        }

        public List<Enderecos> recuperaEnderecosPorIdCidade(long value)
        {
            return new EnderecosDAO().recuperarPorIdCidade(value);
        }

        public Enderecos recuperaEnderecoPorNome(string value)
        {
            return new EnderecosDAO().recuperarPorNome(value);
        }
        #endregion

        #region Metodos estados
        public List<Estados> recuperarTodosEstados()
        {
            return new EstadosDAO().recuperarTodos();
        }

        public Estados recuperarEstadoPorId(long value)
        {
            return new EstadosDAO().recuperarPorId(value);
        }
        #endregion
    }
}
