using Npgsql;
using Siscom.Entities;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class EstadosDAO
    {
        #region queries

        private static string GET_ALL_STATMENT = @"SELECT id, uf, estados, cod_ibge FROM estados;";

        private static string GET_ALL_POR_ID = @"SELECT id, uf, estados, cod_ibge FROM estados WHERE id=:condition;";

        #endregion

        #region Metodo recuperar Todos registros
        public List<Estados> recuperarTodos()
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
        //id, uf, estados, cod_ibge FROM estados
        public Estados recuperarPorId(long id)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            Estados cc = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_ID, conn);
                stmt.Parameters.AddWithValue("condition", id);

                dr = stmt.ExecuteReader();

                if (dr.Read())
                {
                    long _id = Convert.ToInt64(dr[0]);
                    string _uf = dr[1].ToString();
                    string _estados = dr[2].ToString();
                    string _cod_ibge = dr[3].ToString();

                    cc = new Estados()
                    {
                        id = _id,
                        uf = _uf,
                        estado = _estados,
                        cod_ibge = _cod_ibge
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
        //id, uf, estados, cod_ibge FROM estados
        private List<Estados> converteParaLista(NpgsqlDataReader dr)
        {
            List<Estados> lista = new List<Estados>();
            while (dr.Read())
            {
                long _id = Convert.ToInt64(dr[0]);
                string _uf = dr[1].ToString();
                string _estados = dr[2].ToString();
                string _cod_ibge = dr[3].ToString();

                Estados b = new Estados()
                {
                    id = _id,
                    uf = _uf,
                    estado = _estados,
                    cod_ibge = _cod_ibge
                };
                lista.Add(b);
            }
            return lista;
        }
        #endregion
    }
}
