using Npgsql;
using Siscom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CidadesDAO
    {
        #region queries

        private static string GET_ALL_STATMENT = @"SELECT id, cidade, uf, cod_ibge, area FROM cidades;";

        private static string GET_ALL_POR_ID = @"SELECT id, cidade, uf, cod_ibge, area FROM cidades WHERE id=:condition;";

        private static string GET_ALL_POR_NOME = @"SELECT id, cidade, uf, cod_ibge, area FROM cidades WHERE cidade=:condition;";

        #endregion

        #region Metodo recuperar Todos registros
        public List<Cidades> recuperarTodos()
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
        //id, cidade, uf, cod_ibge, area
        public Cidades recuperarPorId(long id)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            Cidades cc = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_ID, conn);
                stmt.Parameters.AddWithValue("condition", id);

                dr = stmt.ExecuteReader();

                if (dr.Read())
                {
                    long _id = Convert.ToInt64(dr[0]);
                    string _cidade = dr[1].ToString();
                    string _uf = dr[2].ToString();
                    string _cod_ibge = dr[3].ToString();
                    Int32 _area = Convert.ToInt32(dr[4]);

                    cc = new Cidades()
                    {
                        id = _id,
                        cidade = _cidade,
                        uf = _uf,
                        cod_ibge = _cod_ibge,
                        area = _area
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

        #region Metodo recuperar 1 registro por nome
        //id, cidade, uf, cod_ibge, area
        public Cidades recuperarPorNome(string nome)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            Cidades cc = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_NOME, conn);
                stmt.Parameters.AddWithValue("condition", nome);

                dr = stmt.ExecuteReader();

                if (dr.Read())
                {
                    long _id = Convert.ToInt64(dr[0]);
                    string _cidade = dr[1].ToString();
                    string _uf = dr[2].ToString();
                    string _cod_ibge = dr[3].ToString();
                    Int32 _area = Convert.ToInt32(dr[4]);

                    cc = new Cidades()
                    {
                        id = _id,
                        cidade = _cidade,
                        uf = _uf,
                        cod_ibge = _cod_ibge,
                        area = _area
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
        //SELECT id, bairro FROM bairro;
        private List<Cidades> converteParaLista(NpgsqlDataReader dr)
        {
            List<Cidades> lista = new List<Cidades>();
            while (dr.Read())
            {
                long _id = Convert.ToInt64(dr[0]);
                string _cidade = dr[1].ToString();
                string _uf = dr[2].ToString();
                string _cod_ibge = dr[3].ToString();
                Int32 _area = Convert.ToInt32(dr[4]);

                Cidades c = new Cidades()
                {
                    id = _id,
                    cidade = _cidade,
                    uf = _uf,
                    cod_ibge = _cod_ibge,
                    area = _area
                };                       

                lista.Add(c);
            }
            return lista;
        }
        #endregion
    }
}
