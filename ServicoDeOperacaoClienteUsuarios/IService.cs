using Siscom.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServicoDeOperacaoClienteUsuarios
{
    [ServiceContract]
    public interface IService
    {
        #region Metodos clifor
        [OperationContract]
        List<Clientes> retornaTodosCliFor();

        [OperationContract]
        List<Clientes> retornaCliForPorNome(string value);

        [OperationContract]
        List<Clientes> retornaCliForPorDocumento(string value);

        [OperationContract]
        Clientes retornaPorId(long value);

        [OperationContract]
        int salvar(Clientes c);

        [OperationContract]
        int countClassClientes(string value, string coluna);
        #endregion

        #region Metodos cidades
        [OperationContract]
        List<Cidades> recuperarTodasAsCidades();

        [OperationContract]
        Cidades recuperarCidadePorId(long value);

        [OperationContract]
        Cidades recuperarCidadePorNome(string value);
        #endregion

        #region Metodos bairros
        [OperationContract]
        List<Bairros> recuperarTodosBairros();

        [OperationContract]
        Bairros recuperarBairroPorId(long value);

        [OperationContract]
        List<Bairros> recuperarBairroPorIdCidade(long value);
        #endregion

        #region Metods enderecos
        [OperationContract]
        List<Enderecos> recuperarTodosEnderecos();

        [OperationContract]
        Enderecos recuperarEnderecoPorId(long value);

        [OperationContract]
        List<Enderecos> recuperaEnderecosPorIdCidade(long value);

        [OperationContract]
        Enderecos recuperaEnderecoPorNome(string value);
        #endregion

        #region Metods estados
        [OperationContract]
        List<Estados> recuperarTodosEstados();

        [OperationContract]
        Estados recuperarEstadoPorId(long value);
        #endregion
    }
}
