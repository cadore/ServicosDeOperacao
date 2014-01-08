using Npgsql;
using Siscom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EnderecosDAO
    {
        #region queries

        private static string GET_ALL_STATMENT = @"SELECT id, cep, endereco, bairro_id, id_cidades FROM enderecos;";

        private static string GET_ALL_POR_ID = @"SELECT id, cep, endereco, bairro_id, id_cidades FROM enderecos WHERE id=:condition;";

        private static string GET_ALL_POR_ID_CIDADE = @"SELECT id, cep, endereco, bairro_id, id_cidades FROM enderecos WHERE id_cidades=:condition;";

        private static string GET_ALL_POR_NOME = @"SELECT id, cep, endereco, bairro_id, id_cidades FROM enderecos WHERE endereco=:condition;";

        #endregion

        #region Metodo recuperar Todos registros
        public List<Enderecos> recuperarTodos()
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_STATMENT, conn);
                dr = stmt.ExecuteReader();

                return converteParaLista(dr);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(String.Format("    {0}\n    {1}", ex.Message, ex.InnerException));
            }
            finally
            {
                GerenteDeConexoes.closeAll(conn);
            }
        }
        #endregion

        #region Metodo recuperar 1 registro por id
        //id, cep, endereco, bairro_id, id_cidades
        public Enderecos recuperarPorId(long id)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null; 
            NpgsqlDataReader dr = null;

            Enderecos cc = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_ID, conn);
                stmt.Parameters.AddWithValue("condition", id);

                dr = stmt.ExecuteReader();

                if (dr.Read())
                {
                    long _id = Convert.ToInt64(dr[0]);
                    string _cep = dr[1].ToString();
                    string _endereco = dr[2].ToString();
                    long _bairro_id = Convert.ToInt64(dr[3]);
                    long _id_cidades = Convert.ToInt64(dr[4]);

                    cc = new Enderecos()
                    {
                        id = _id,
                        cep = _cep,
                        endereco = _endereco,
                        bairro_id = _bairro_id,
                        id_cidades = _id_cidades
                    };
                }

                return cc;
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(String.Format("    {0}\n    {1}", ex.Message, ex.InnerException));
            }
            finally
            {
                GerenteDeConexoes.closeAll(conn);
            }
        }
        #endregion

        #region Metodo recuperar por id cidade
        public List<Enderecos> recuperarPorIdCidade(long id_cidade)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_ID_CIDADE, conn);
                stmt.Parameters.AddWithValue("condition", id_cidade);
                dr = stmt.ExecuteReader();

                return converteParaLista(dr);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(String.Format("    {0}\n    {1}", ex.Message, ex.InnerException));
            }
            finally
            {
                GerenteDeConexoes.closeAll(conn);
            }
        }
        #endregion

        #region Metodo recuperar 1 registro por nome
        public Enderecos recuperarPorNome(string nome)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            Enderecos cc = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_NOME, conn);
                stmt.Parameters.AddWithValue("condition", nome);

                dr = stmt.ExecuteReader();

                if (dr.Read())
                {
                    long _id = Convert.ToInt64(dr[0]);
                    string _cep = dr[1].ToString();
                    string _endereco = dr[2].ToString();
                    long _bairro_id = Convert.ToInt64(dr[3]);
                    long _id_cidades = Convert.ToInt64(dr[4]);

                    cc = new Enderecos()
                    {
                        id = _id,
                        cep = _cep,
                        endereco = _endereco,
                        bairro_id = _bairro_id,
                        id_cidades = _id_cidades
                    };
                }

                return cc;
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(String.Format("    {0}\n    {1}", ex.Message, ex.InnerException));
            }
            finally
            {
                GerenteDeConexoes.closeAll(conn);
            }
        }
        #endregion

        #region Metodo converte datareader para list<T>
        //id, cep, endereco, bairro_id, id_cidades
        private List<Enderecos> converteParaLista(NpgsqlDataReader dr)
        {
            List<Enderecos> lista = new List<Enderecos>();
            while (dr.Read())
            {
                long _id = Convert.ToInt64(dr[0]);
                string _cep = dr[1].ToString();
                string _endereco = dr[2].ToString();
                long _bairro_id = Convert.ToInt64(dr[3]);
                long _id_cidades = Convert.ToInt64(dr[4]);

                Enderecos b = new Enderecos()
                {
                    id = _id,
                    cep = _cep,
                    endereco = _endereco,
                    bairro_id = _bairro_id,
                    id_cidades = _id_cidades
                };
                lista.Add(b);
            }
            return lista;
        }
        #endregion
    }
}
